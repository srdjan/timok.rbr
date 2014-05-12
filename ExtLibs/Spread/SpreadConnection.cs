/*
 * The Spread Toolkit.
 *     
 * The contents of this file are subject to the Spread Open-Source
 * License, Version 1.0 (the ``License''); you may not use
 * this file except in compliance with the License.  You may obtain a
 * copy of the License at:
 *
 * http://www.spread.org/license/
 *
 * or in the file ``license.txt'' found in this distribution.
 *
 * Software distributed under the License is distributed on an AS IS basis, 
 * WITHOUT WARRANTY OF ANY KIND, either express or implied. See the License 
 * for the specific language governing rights and limitations under the 
 * License.
 *
 * The Creators of Spread are:
 *  Yair Amir, Michal Miskin-Amir, Jonathan Stanton.
 *
 *  Copyright (C) 1993-2001 Spread Concepts LLC <spread@spreadconcepts.com>
 *
 *  All Rights Reserved.
 *
 * Major Contributor(s):
 * ---------------
 *    Dan Schoenblum   dansch@cnds.jhu.edu - Java Interface Developer.
 *    John Schultz     jschultz@cnds.jhu.edu - contribution to process group membership.
 *    Theo Schlossnagle theos@cnds.jhu.edu - Perl library and Skiplists.
 *
 */

using System;
using System.Net;
using System.Net.Sockets;
using System.Collections;
using System.Threading;

namespace Timok.SpreadLib 
{
	public class SpreadConnection {
		internal const int DEFAULT_SPREAD_PORT = 4803;
		internal const int MAX_PRIVATE_NAME = 10;
		internal const int MAX_MESSAGE_LENGTH = 140000;
		internal const int MAX_GROUP_NAME = 32;
		private const byte SP_MAJOR_VERSION = 3;
		private const byte SP_MINOR_VERSION = 16;
		private const byte SP_PATCH_VERSION = 0;
		private const int ACCEPT_SESSION = 1;
		internal const int MAX_AUTH_NAME = 30;
		internal const int MAX_AUTH_METHODS = 3;
		private const string DEFAULT_AUTH_NAME = "NULL";
		private const int ENDIAN_TYPE = unchecked((int)0x80000080);
		
		private bool connected = false;
		private string address;
		private int port;
		private bool priority;
		private bool groupMembership;
		private TcpClient socket;
		private NetworkStream stream;
		private string authName = DEFAULT_AUTH_NAME;
		private SpreadGroup group;
		private byte[] buffer;
		private int receivedLength = 0;
		private int receiveState = INIT_RECEIVE;
		private int numGroups;
		bool daemonEndianMismatch;
		private const int INIT_RECEIVE = 0;
		private const int HEADER_RECEIVE = 1;
		private const int OLD_TYPE_RECEIVE = 2;
		private const int GROUPS_RECEIVE = 3;
		private const int DATA_RECEIVE = 4;
		private object rsynchro = new object();
		private SpreadMessage receivedMessage;
		private bool messageWaiting = false;

		public delegate void MessageHandler(SpreadMessage msg);
		public event MessageHandler OnRegularMessage;
		public event MessageHandler OnMembershipMessage;
		
		private void AsyncReceive(IAsyncResult ar) {
			bool readAsync = false;
			do {
				if (receiveState == INIT_RECEIVE) {
					buffer = new byte[48];
					receivedLength = 0;
					receiveState = HEADER_RECEIVE;
					receivedMessage = new SpreadMessage();
				} else {
					if (receivedLength != buffer.Length) {
						int size = stream.EndRead(ar);
						if (size == 0)
							throw new SpreadException("Connection closed while reading");
						receivedLength += size;
					}
					if (receivedLength == buffer.Length) 
						switch (receiveState) {
							case HEADER_RECEIVE:
								byte[] header = buffer;

								int headerIndex = 0;
								// Get service type.
								receivedMessage.ServiceType = toInt(header, headerIndex);
								headerIndex += 4;

								// Get the sender.
								receivedMessage.Sender = toGroup(header, headerIndex);
								headerIndex += MAX_GROUP_NAME;

								// Get the number of groups.
								numGroups = toInt(header, headerIndex);
								headerIndex += 4;

								// Get the hint/type.
								int hint = toInt(header, headerIndex);
								headerIndex += 4;

								// Get the data length.
								int dataLen = toInt(header, headerIndex);
								headerIndex += 4;

								// Does the header need to be flipped?
								// (Checking for a daemon server endian-mismatch)
								if(sameEndian(receivedMessage.ServiceType) == false) {
									// Flip them.
									receivedMessage.ServiceType = flip(receivedMessage.ServiceType);
									numGroups = flip(numGroups);
									dataLen = flip(dataLen);
		
									// The daemon endian-mismatch.
									daemonEndianMismatch = true;
								}
								else {
									// The daemon endian-mismatch.
									daemonEndianMismatch = false;
								}

								receivedMessage.Data = new byte[dataLen];
		
								// Is this a regular message?
								if( receivedMessage.IsRegular || receivedMessage.IsReject ) {
									// Does the hint need to be flipped?
									// (Checking for a sender endian-mismatch)
									if(sameEndian(hint) == false) {
										hint = flip(hint);
										receivedMessage.EndianMismatch = true;
									}
									else {
										receivedMessage.EndianMismatch = false;
									}
			
									// Get the type from the hint.
									hint = clearEndian(hint);
									hint >>= 8;
									hint &= 0x0000FFFF;
									receivedMessage.Type = (short)hint;
								}
								else {
									// This is not a regular message.
									receivedMessage.Type = -1;
									receivedMessage.EndianMismatch = false;
								}
								if( receivedMessage.IsReject ) {
									buffer = new byte[4];
									receivedLength = 0;
									receiveState = OLD_TYPE_RECEIVE;
								} else {
									buffer = new byte[numGroups * MAX_GROUP_NAME];
									receivedLength = 0;
									receiveState = GROUPS_RECEIVE;
								}
								break;
							case OLD_TYPE_RECEIVE:
								int oldType = toInt(buffer, 0);
								if ( sameEndian(receivedMessage.ServiceType) == false )
									oldType = flip(oldType);
								receivedMessage.ServiceType = (SpreadMessage.REJECT_MESS | oldType);
								buffer = new byte[numGroups * MAX_GROUP_NAME];
								receivedLength = 0;
								receiveState = GROUPS_RECEIVE;
								break;
							case GROUPS_RECEIVE:
								// Clear the endian type.
								receivedMessage.ServiceType = clearEndian(receivedMessage.ServiceType);

								// Get the groups from the buffer.
								for(int bufferIndex = 0 ; bufferIndex < buffer.Length ; bufferIndex += MAX_GROUP_NAME) {
									// Translate the name into a group and add it to the vector.
									receivedMessage.AddGroup(toGroup(buffer, bufferIndex));
								}
								buffer = receivedMessage.Data;
								receivedLength = 0;
								receiveState = DATA_RECEIVE;
								break;
							case DATA_RECEIVE:
								// Is it a membership message?
								if(receivedMessage.IsMembership) {
									// Create a membership info object.
									receivedMessage.membershipInfo = new MembershipInfo(this, receivedMessage, daemonEndianMismatch);
			
									// Is it a regular membership message?
									if(receivedMessage.membershipInfo.IsRegularMembership) {
										// Find which of these groups is the local connection.
										receivedMessage.Type = (short)receivedMessage.groups.IndexOf(PrivateGroup);
									}
								}
								else {
									// There's no membership info.
									receivedMessage.membershipInfo = null;
								}
								if (OnRegularMessage !=null  && receivedMessage.IsRegular) {
									OnRegularMessage(receivedMessage);
								}
								else if (OnMembershipMessage != null && receivedMessage.IsMembership) {
									OnMembershipMessage(receivedMessage);
								}
								else {
									lock (rsynchro) {
										messageWaiting = true;
										Monitor.Pulse(rsynchro);
										Monitor.Wait(rsynchro);
									}
								}
								buffer = new byte[48];
								receivedLength = 0;
								receiveState = HEADER_RECEIVE;
								receivedMessage = new SpreadMessage();
								break;
						}
				}
				try {
					// read sync if data available
					while(stream.DataAvailable && receivedLength < buffer.Length) {
						int size = stream.Read(buffer,receivedLength,buffer.Length-receivedLength);
						if (size == 0)
							throw new SpreadException("Connection closed while reading");
						receivedLength += size;
					} 
					if (receivedLength != buffer.Length) {
						// no more data - continue async read
						stream.BeginRead(buffer,receivedLength,buffer.Length-receivedLength,new AsyncCallback(AsyncReceive),null);
						readAsync = true;
					}
				} 
				catch (Exception e) {
					throw new SpreadException("BeginRead(): " + e);
				}
			} while (!readAsync);
		}

		private void StartReceiving() {
		}
		// Clears the int's endian type.
		private static int clearEndian(int i) {
			return (i & ~ENDIAN_TYPE);
		}
		// Endian-flips the int.
		internal static int flip(int i) {
			return (int)(((i >> 24) & 0x000000ff) | (int)((i >>  8) & 0x0000ff00) | (int)((i <<  8) & 0x00ff0000) | (int)((i << 24) & 0xff000000));
		}
		// Checks if the int is the same-endian type as the local machine.
		private static bool sameEndian(int i) {
			return ((i & ENDIAN_TYPE) == 0);
		}
		// Gets a string from an array of bytes.
		internal SpreadGroup toGroup(byte[] buffer, int bufferIndex) {
			try {
				for(int end = bufferIndex ; end < buffer.Length ; end++) {
					if(buffer[end] == 0) {
						// Get the group name.
						String name = System.Text.Encoding.ASCII.GetString(buffer, bufferIndex, end - bufferIndex);
					
						// Return the group.
						return new SpreadGroup(this, name);
					}
				}
			}
			catch(Exception e) {
				// Already checked for this exception in connect.
				Console.WriteLine(e);
			}
	
			return null;
		}
		// Gets an int from an array of bytes.
		internal static int toInt(byte[] buffer, int bufferIndex) {
			int i0 = (buffer[bufferIndex++] & 0xFF);
			int i1 = (buffer[bufferIndex++] & 0xFF);
			int i2 = (buffer[bufferIndex++] & 0xFF);
			int i3 = (buffer[bufferIndex++] & 0xFF);
		
			return ((i0 << 24) | (i1 << 16) | (i2 << 8) | (i3));
		}	
		// Puts an int into an array of bytes.
		private static void toBytes(int i, byte[] buffer, int bufferIndex) {
			buffer[bufferIndex++] = (byte)((i >> 24) & 0xFF);
			buffer[bufferIndex++] = (byte)((i >> 16) & 0xFF);
			buffer[bufferIndex++] = (byte)((i >> 8 ) & 0xFF);
			buffer[bufferIndex++] = (byte)((i      ) & 0xFF);
		}
		// Puts a group name into an array of bytes.
		private static void toBytes(SpreadGroup group, byte[] buffer, int bufferIndex) {
			// Get the group's name.
			byte[] name;
			try {
				name = System.Text.Encoding.ASCII.GetBytes(group.ToString());
			}
			catch(Exception e) {
				// Already checked for this exception in connect.
				name = new byte[0];
				Console.WriteLine(e);
			}
		
			// Put a cap on the length.
			int len = name.Length;
			if(len > MAX_GROUP_NAME)
				len = MAX_GROUP_NAME;

			// Copy the name into the buffer.
			System.Array.Copy(name,0,buffer,bufferIndex,len);
			for( ; len < MAX_GROUP_NAME ; len++ )
				buffer[bufferIndex + len] = 0;
		}
		// Get the private group name.
		private void ReadGroup() {
			// Read the length.
			int len;
			try {
				len = stream.ReadByte();
			}
			catch(Exception e) {
				throw new SpreadException("read(): " + e);
			}
		
			// Check for no more data.
			if(len == -1) {
				throw new SpreadException("Connection closed during connect attempt");
			}
		
			// Read the name.
			byte[] buffer = new byte[len];
			int numRead;
			try {
				numRead = stream.Read(buffer,0,buffer.Length);
			}
			catch(Exception e) {
				throw new SpreadException("read(): " + e);
			}
		
			// Check for not enough data.
			if(numRead != len) {
				throw new SpreadException("Connection closed during connect attempt");
			}
		
			// Store the group.
			group = new SpreadGroup(this, System.Text.Encoding.ASCII.GetString(buffer,0,buffer.Length));
		}

		// Checks the daemon version.
		private void CheckVersion(){		
			// Read the version.
			int majorVersion;
			try {
				majorVersion = stream.ReadByte();
			}
			catch(Exception e) {
				throw new SpreadException("read(): " + e);
			}
		
			// Read the sub-version.
			int minorVersion;
			try {
				minorVersion = stream.ReadByte();
			}
			catch(Exception e) {
				throw new SpreadException("read(): " + e);
			}
		
			// Read the patch-version.
			int patchVersion;
			try {
				patchVersion = stream.ReadByte();
			}
			catch(Exception e) {
				throw new SpreadException("read(): " + e);
			}
		
			// Check for no more data.
			if((majorVersion == -1) || (minorVersion == -1) || (patchVersion == -1) ) {
				throw new SpreadException("Connection closed during connect attempt");
			}
		
			// Check the version.
			int version = ( (majorVersion*10000) + (minorVersion*100) + patchVersion );
			if(version < 30100) {
				throw new SpreadException("Old version " + majorVersion + "." + minorVersion + "." + patchVersion + " not supported");
			}
			if((version < 30800) && (priority)) {
				throw new SpreadException("Old version " + majorVersion + "." + minorVersion + "." + patchVersion + " does not support priority");
			}
		}

		// Checks for an accept message.
		private void CheckAccept() {
			// Read the connection response.
			int accepted;
			try {
				accepted = stream.ReadByte();
			}
			catch(Exception e) {
				throw new SpreadException("ReadByte(): " + e);
			}
		
			// Check for no more data.
			if(accepted == -1) {
				throw new SpreadException("Connection closed during connect attempt");
			}
		
			// Was it accepted?
			if(accepted != ACCEPT_SESSION) {
				throw new SpreadException("Connection attempt rejected=" + (byte)accepted);
			}
		}
	
		private void ReadAuthMethods() {
			int len;
			try {
				len = stream.ReadByte();
			}
			catch(Exception e) {
				throw new SpreadException("ReadByte(): " + e);
			}
		
			// System.out.println("readAuthMethods: len is " + len);
			// Check for no more data.
			if(len == -1) {
				throw new SpreadException("Connection closed during connect attempt to read authlen");
			}
			// Check if it was a response code
			if( len < -1 ) {
				throw new SpreadException("Connection attempt rejected=" + (byte)len);
			}

			// Read the name.
			byte[] buffer = new byte[len];
			int numRead;
			try {
				numRead = stream.Read(buffer,0,buffer.Length);
			}
			catch(Exception e) {
				throw new SpreadException("Read(): " + e);
			}
		
			// System.out.println("readAuthMethods: string is " + new String(buffer));
			// Check for not enough data.
			if(numRead != len) {
				throw new SpreadException("Connection clsoed during connect attempt to read authnames");
			}
		
			// for now we ignore the list.
		}

		// Sends the choice of auth methods  message.
		private void SendAuthMethod() {
			int len = authName.Length;
			byte[] buffer = new Byte[MAX_AUTH_NAME*MAX_AUTH_METHODS];
			try {
				System.Text.Encoding.ASCII.GetBytes(authName,0,len,buffer,0);
			}
			catch(Exception e) {
				throw new SpreadException("GetBytes(): " + e);
			}		
			for( ; len < (MAX_AUTH_NAME*MAX_AUTH_METHODS) ; len++ )
				buffer[len] = 0;

			// Send the connection message.
			try {
				stream.Write(buffer,0,buffer.Length);
			}
			catch(Exception e) {
				throw new SpreadException("write(): " + e);
			}		
		}

		private void SetBufferSizes() {
			try {
				for(int i = 10 ; i <= 200 ; i += 5) {
					// The size to try.
					int size = (1024 * i);
				
					// Set the buffer sizes.
					socket.SendBufferSize = size;
					socket.ReceiveBufferSize = size;
				
					// Check the actual sizes.  If smaller, then the max was hit.
					if((socket.SendBufferSize < size) || socket.ReceiveBufferSize < size) {
						break;
					}
				}
			}
			catch(SocketException e) {
				throw new SpreadException("Send/ReceiveBufferSize: " + e);
			}
		}

		private void SendConnect(string privateName) {
			int len = (privateName == null ? 0 : privateName.Length);
			if(len > MAX_PRIVATE_NAME) {
				privateName = privateName.Substring(0, MAX_PRIVATE_NAME);
				len = MAX_PRIVATE_NAME;
			}
			byte[] buffer = new byte[len + 5];
			buffer[0] = SP_MAJOR_VERSION;
			buffer[1] = SP_MINOR_VERSION;
			buffer[2] = SP_PATCH_VERSION;

			buffer[3] = 0;
			if(groupMembership) buffer[3] |= 0x01;
			if(priority) buffer[3] |= 0x10;

			buffer[4] = (byte)len;
		
			if(len > 0) System.Text.Encoding.ASCII.GetBytes(privateName,0,len,buffer,5);

			try {
				stream.Write(buffer,0,buffer.Length);
			}
			catch(Exception ex) {
				throw new SpreadException("write(): " + ex);
			}
		}

		public void Connect(string address,int port, string user,bool priority,bool groupMembership) {
			if (connected)
				throw new SpreadException("Already connected.");		

			this.address = address;
			this.port = port;
			this.priority = priority;
			this.groupMembership = groupMembership;

			if (address == null) address = IPAddress.Loopback.ToString();

			if (port == 0) port = DEFAULT_SPREAD_PORT;

			try {
				socket = new TcpClient(address, port);
			} catch (Exception ex) {
				throw new SpreadException("Socket(): " + ex);
			}
			try {
				stream = socket.GetStream();
			} catch (Exception e) {
				throw new SpreadException("GetStream(): " + e);
			}

			SetBufferSizes();

			SendConnect(user);

			ReadAuthMethods();

			SendAuthMethod();

			CheckAccept();

			CheckVersion();

			ReadGroup();

			connected = true;

			AsyncReceive(null);
		}
		// Disconnects from the spread daemon.
		/**
		 * Disconnects the connection to the daemon.  Nothing else should be done with this connection
		 * after disconnecting it.
		 * 
		 * @throws  SpreadException  if there is no connection or there is an error disconnecting
		 * @see  SpreadConnection#connect(InetAddress, int, String, boolean, boolean)
		 */
		public void Disconnect() {
			// Check if we're connected.
			if(connected == false) {
				throw new SpreadException("Not connected.");
			}
		
			// Get a new message.
			SpreadMessage killMessage = new SpreadMessage();
		
			// Send it to our private group.
			killMessage.AddGroup(group);
		
			// Set the service type.
			killMessage.ServiceType = SpreadMessage.KILL_MESS;
		
			// Send the message.
			Multicast(killMessage);
		
			// Close the socket.
			try {
				socket.Close();
			}
			catch(Exception e) {
				throw new SpreadException("close(): " + e);
			}
		}

		public SpreadGroup PrivateGroup {
			get {
				if (!connected) return null;
				return group;
			}
		}
		// Sends the array of messages.
		/**
		 * mcasts an array of messages.  Each message will be sent to all the groups specified in
		 * the message.
		 * 
		 * @param  messages  the messages to mcast
		 * @throws  SpreadException  if there is no connection or if there is any error sending the messages
		 */
		public void Multicast(SpreadMessage[] messages){
			// Go through the array.
			for(int i = 0 ; i < messages.Length ; i++) {
				// Send this message.
				Multicast(messages[i]);
			}
		}
		// Sends the message.
		/**
			 * mcasts a message.  The message will be sent to all the groups specified in
			 * the message.
			 * 
			 * @param  message  the message to mcast
			 * @throws  SpreadException  if there is no connection or if there is any error sending the message
			 */
		public void Multicast(SpreadMessage message){
			// Check if we're connected.
			if(connected == false) {
				throw new SpreadException("Not connected.");
			}
	
			// The groups this message is going to.
			SpreadGroup[] groups = message.Groups;
		
			// The message data.
			byte[] data = message.Data;
		
			// Calculate the total number of bytes.
			int numBytes = 16;  // serviceType, numGroups, type/hint, dataLen
			numBytes += MAX_GROUP_NAME;  // private group
			numBytes += (MAX_GROUP_NAME * groups.Length);  // groups
		
			if (numBytes + data.Length > MAX_MESSAGE_LENGTH ) {
				throw new SpreadException("Message is too long for a Spread Message");
			}
			// Allocate the send buffer.
			byte[] buffer = new byte[numBytes];
			int bufferIndex = 0;
		
			// The service type.
			toBytes(message.ServiceType, buffer, bufferIndex);
			bufferIndex += 4;

			// The private group.
			toBytes(group, buffer, bufferIndex);
			bufferIndex += MAX_GROUP_NAME;
		
			// The number of groups.
			toBytes(groups.Length, buffer, bufferIndex);
			bufferIndex += 4;
		
			// The service type and hint.
			toBytes(((int)message.Type << 8) & 0x00FFFF00, buffer, bufferIndex);
			bufferIndex += 4;
		
			// The data length.
			toBytes(data.Length, buffer, bufferIndex);
			bufferIndex += 4;
		
			// The group names.
			for(int i = 0 ; i < groups.Length ; i++) {
				toBytes(groups[i], buffer, bufferIndex);
				bufferIndex += MAX_GROUP_NAME;
			}
		
			try {
				stream.Write(buffer,0,buffer.Length);
				stream.Write(data,0,data.Length);
			}
			catch(Exception e) {
				throw new SpreadException("Write(): " + e.ToString());
			}
		}

		// Returns true if there are messages waiting.
		/**
		 * Returns true if there are any messages waiting on this connection.
		 * 
		 * @return true if there is at least one message that can be received
		 * @throws  SpreadException  if there is no connection or if there is an error checking for messages
		 */
		public bool Poll(){
			// Check if we're connected.
			if(connected == false) {
				throw new SpreadException("Not connected.");
			}
			bool ret;
			lock (rsynchro) {
				ret = messageWaiting;
			}
			return ret;
		}

		public SpreadMessage Receive() {
			SpreadMessage m;
			lock(rsynchro) {
				while (!messageWaiting) Monitor.Wait(rsynchro);
				m = receivedMessage;
				messageWaiting = false;
				Monitor.Pulse(rsynchro);
			}
			return m;
		}
	
		// Receives numMessages new messages.
		/**
		 * Receives <code>numMessages</code> messages on the connection and returns them in an array.
		 * If there are not <code>numMessages</code> messages waiting, the call will block until there are
		 * enough messages available.
		 * 
		 * @param  numMessages  the number of messages to receive
		 * @return an array of messages
		 * @throws  SpreadException  if there is no connection or if there is any error reading the messages
		 */
		public SpreadMessage[] Receive(int numMessages) {
			// Allocate the messages array.
			SpreadMessage[] messages = new SpreadMessage[numMessages];
			for(int i = 0 ; i < numMessages ; i++) {
				messages[i] = Receive();
			}
			// Return the array.
			return messages;
		}
	}
}
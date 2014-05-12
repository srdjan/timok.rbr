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
using System.Threading;

namespace Timok.SpreadLib 
{
	public class RecThread {
		private SpreadConnection connection;
		public  bool threadSuspended;

		public RecThread(SpreadConnection pConn) {
			connection = pConn;
		}
	
		public void Run() {
			while (true) {
				try {
					//NOTE: don't display every message, use for special debugging cases only
					//displayMessage(connection.Receive());
					//

					if (threadSuspended) {
						lock (this) {
							while (threadSuspended) {
								Monitor.Wait(this);
							}
						}
					}
				} 
				catch (Exception e) {
					Console.WriteLine(e);
				}
			}
		}

		//------------------------ Private -------------------------------------------------------
		private void displayMessage(SpreadMessage msg) {
			try {
				Console.WriteLine("*****************RECTHREAD Received Message************");
				if (msg.IsRegular) {
					Console.Write("Received a ");
					if(msg.IsUnreliable)
						Console.Write("UNRELIABLE");
					else if(msg.IsReliable)
						Console.Write("RELIABLE");
					else if(msg.IsFifo)
						Console.Write("FIFO");
					else if(msg.IsCausal)
						Console.Write("CAUSAL");
					else if(msg.IsAgreed)
						Console.Write("AGREED");
					else if(msg.IsSafe)
						Console.Write("SAFE");
					Console.WriteLine(" message.");
				
					Console.WriteLine("Sent by  " + msg.Sender + ".");
				
					Console.WriteLine("Type is " + msg.Type + ".");
				
					if(msg.EndianMismatch == true)
						Console.WriteLine("There is an endian mismatch.");
					else
						Console.WriteLine("There is no endian mismatch.");
				
					SpreadGroup[] groups = msg.Groups;
					Console.WriteLine("To " + groups.Length + " groups.");
				
					byte[] data = msg.Data;
					Console.WriteLine("The data is " + data.Length + " bytes.");
				
					Console.WriteLine("The message is: " + System.Text.Encoding.ASCII.GetString(data));
				}
				else if ( msg.IsMembership ) {
					MembershipInfo info = msg.MembershipInfo;
				
					if (info.IsRegularMembership) {
						SpreadGroup[] groups = msg.Groups;
				
						Console.WriteLine("Received a REGULAR membership.");
						Console.WriteLine("For group " + info.Group + ".");
						Console.WriteLine("With " + groups.Length + " members.");
						Console.WriteLine("I am member " + msg.Type + ".");
						for (int i = 0 ; i < groups.Length ; i++)
							Console.WriteLine("  " + groups[i]);
				
						Console.WriteLine("Group ID is " + info.GroupID);
				
						Console.Write("Due to ");
						if (info.IsCausedByJoin) {
							Console.WriteLine("the JOIN of " + info.Joined + ".");
						}
						else if (info.IsCausedByLeave) {
							Console.WriteLine("the LEAVE of " + info.Left + ".");
						}
						else if (info.IsCausedByDisconnect) {
							Console.WriteLine("the DISCONNECT of " + info.Disconnected + ".");
						}
						else if (info.IsCausedByNetwork) {
							SpreadGroup[] stayed = info.Stayed;
							Console.WriteLine("NETWORK change.");
							Console.WriteLine("VS set has " + stayed.Length + " members:");
							for (int i = 0 ; i < stayed.Length ; i++)
								Console.WriteLine("  " + stayed[i]);
						}
					}
					else if (info.IsTransition) {
						Console.WriteLine("Received a TRANSITIONAL membership for group " + info.Group);
					}
					else if (info.IsSelfLeave) {
						Console.WriteLine("Received a SELF-LEAVE message for group " + info.Group);
					}
				} 
				else if ( msg.IsReject ) {
					// Received a Reject message 
					Console.Write("Received a ");
					if (msg.IsUnreliable)
						Console.Write("UNRELIABLE");
					else if (msg.IsReliable)
						Console.Write("RELIABLE");
					else if (msg.IsFifo)
						Console.Write("FIFO");
					else if (msg.IsCausal)
						Console.Write("CAUSAL");
					else if (msg.IsAgreed)
						Console.Write("AGREED");
					else if (msg.IsSafe)
						Console.Write("SAFE");
					Console.WriteLine(" REJECTED message.");
				
					Console.WriteLine("Sent by  " + msg.Sender + ".");
				
					Console.WriteLine("Type is " + msg.Type + ".");
				
					if (msg.EndianMismatch == true)
						Console.WriteLine("There is an endian mismatch.");
					else
						Console.WriteLine("There is no endian mismatch.");
				
					SpreadGroup[] groups = msg.Groups;
					Console.WriteLine("To " + groups.Length + " groups.");
				
					byte[] data = msg.Data;
					Console.WriteLine("The data is " + data.Length + " bytes.");
				
					Console.WriteLine("The message is: " + System.Text.Encoding.ASCII.GetString(data));
				} 
				else {
					Console.WriteLine("Message is of unknown type: " + msg.ServiceType );
				}
			}
			catch (Exception e) {
				Console.WriteLine(e);
				Environment.Exit(1);
			}
		}
	}
}
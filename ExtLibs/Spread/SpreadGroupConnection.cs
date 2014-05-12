using System;
using System.Text;
using System.IO;
using System.Threading;

using Timok.Core;
using T = Timok.Core.Logging.TimokLogger;

namespace Timok.SpreadLib 
{
	public delegate void ExtMessageHandler(string pSender, byte[] msg);

	sealed public class SpreadGroupConnection : IDisposable {
		SpreadConnection connection;
		SpreadGroup group;
	
		// True if there is a listening thread.
		bool listening;
		RecThread rt;

		SpreadConnection.MessageHandler regularMessageHandler;
		SpreadConnection.MessageHandler membershipMessageHandler;
		ExtMessageHandler extRegularMessageHandler;
		ExtMessageHandler extMembershipMessageHandler;

		//TODO: get from app.config
		string userName;
		string address;
		short port;

		public SpreadGroupConnection(string pUserName, string pAddress, short pPort, string pGroup, ExtMessageHandler pExtRegularMsgeHandler, ExtMessageHandler pExtMembershipMsgHandler) {
			userName = pUserName;
			address = pAddress;
			port = pPort;
			connect(pGroup);
			extRegularMessageHandler = pExtRegularMsgeHandler;
			extMembershipMessageHandler = pExtMembershipMsgHandler;
		}

		//--SendAnd Recieve
		public void SendAndReceive(string pToGroup, string pMessage) {
			send(pToGroup, pMessage, false);
		}
		public void SendAndReceive(string pMessage) {
			send(group.ToString(), pMessage, false);
		}

		//--Send
		public void Send(string pToGroup, string pMessage) {
			send(pToGroup,  pMessage, true);
		}
		public void Send(string pMessage) {
			send(group.ToString(), pMessage, true);
		}

		public void Dispose() {
			disconnect();
		}

		//------------------------------ Private ----------------------------------------
		private void send(string pToGroup, string pMessage, bool pIsSelfDiscard) {
			SpreadMessage _spreadMsg = new SpreadMessage();
			_spreadMsg.IsAgreed = true;	//  _spreadMsg.IsSafe = true;
			_spreadMsg.IsSelfDiscard = pIsSelfDiscard;
			_spreadMsg.AddGroup(pToGroup);
			_spreadMsg.Data = System.Text.Encoding.ASCII.GetBytes(pMessage);

			// Send msg and wait for response
			try {
				connection.Multicast(_spreadMsg);
				//				if ( ! pIsSelfDiscard) {
				//					SyncLock.Wait();
				//				}
			}
			catch (Exception _ex) {
				T.LogStatus("Multicast Exception: " + _ex);
				throw;
			}
		}

		private void regularMsgReceived(SpreadMessage pMessage) {
			//TODO: test if selfDiscarded messages would be actually sent back by spread deamon
			extRegularMessageHandler(pMessage.Sender.ToString(), pMessage.Data);

			//			if (pMessage.Sender == me) {
			//				SyncLock.Pulse();
			//			}
		}

		private void membershipMsgReceived(SpreadMessage pMessage) {
			extMembershipMessageHandler(pMessage.Sender.ToString(), pMessage.Data);
		}

		private void connect(string pGroup) {
			// Establish the spread connection.
			try {
				connection = new SpreadConnection();
				connection.Connect(address, port, userName, false, true);
			}
			catch (SpreadException _ex) {
				T.LogCritical("There was an error connecting to the daemon: " + _ex);
				Environment.Exit(1);
			}
			catch (Exception _ex) {
				T.LogCritical("Can't find the daemon " + address + " Exception: " + _ex);
				Environment.Exit(1);
			}

			// start receiving thread
			rt = new RecThread(connection);
			Thread rtt = new Thread(new ThreadStart(rt.Run));
			rtt.Start();

			// start listener
			regularMessageHandler = new SpreadConnection.MessageHandler(regularMsgReceived);
			connection.OnRegularMessage += regularMessageHandler;

			membershipMessageHandler += new SpreadConnection.MessageHandler(membershipMsgReceived);
			connection.OnMembershipMessage += membershipMessageHandler;
			lock (rt) {
				rt.threadSuspended = true;
			}
			listening = true;
			T.LogStatus("Listening: " + listening);

			// join group
			group = new SpreadGroup();
			group.Join(connection, pGroup);
			T.LogStatus("Joined: " + group + ".");
		}

		private void disconnect() {
			// leave group
			if (group != null) {
				group.Leave();
				T.LogDebug("Left " + group + ".");
			}
			else {
				T.LogDebug("No group to leave.");
			}

			// stop listener
			if (listening) {
				if (regularMessageHandler != null) {
					connection.OnRegularMessage -= regularMessageHandler;
				}
				if (membershipMessageHandler != null) {
					connection.OnMembershipMessage -= membershipMessageHandler;
				}
				if (rt.threadSuspended)
					lock (rt) {
						Monitor.Pulse(rt);
						rt.threadSuspended = false;
					}
			}

			// Disconnect.
			connection.Disconnect();
		}

		#region display message helper
		private void display(SpreadMessage msg) {
			try {
				if (msg.IsRegular) {
					T.LogDebug("Received a ");
					if (msg.IsUnreliable)
						T.LogDebug("UNRELIABLE");
					else if (msg.IsReliable)
						T.LogDebug("RELIABLE");
					else if (msg.IsFifo)
						T.LogDebug("FIFO");
					else if (msg.IsCausal)
						T.LogDebug("CAUSAL");
					else if (msg.IsAgreed)
						T.LogDebug("AGREED");
					else if (msg.IsSafe)
						T.LogDebug("SAFE");
					T.LogDebug(" message.");
				
					T.LogDebug("Sent by  " + msg.Sender + ".");
				
					T.LogDebug("Type is " + msg.Type + ".");
				
					if (msg.EndianMismatch == true)
						T.LogDebug("There is an endian mismatch.");
					else
						T.LogDebug("There is no endian mismatch.");
				
					SpreadGroup[] groups = msg.Groups;
					T.LogDebug("To " + groups.Length + " groups.");
				
					byte[] data = msg.Data;
					T.LogDebug("The data is " + data.Length + " bytes.");
				
					T.LogDebug("The message is: " + System.Text.Encoding.ASCII.GetString(data));
				}
				else if (msg.IsMembership) {
					MembershipInfo info = msg.MembershipInfo;
				
					if (info.IsRegularMembership) {
						SpreadGroup[] groups = msg.Groups;
				
						T.LogDebug("Received a REGULAR membership.");
						T.LogDebug("For group " + info.Group + ".");
						T.LogDebug("With " + groups.Length + " members.");
						T.LogDebug("I am member " + msg.Type + ".");
						for (int i = 0 ; i < groups.Length ; i++)
							T.LogDebug("  " + groups[i]);
				
						T.LogDebug("Group ID is " + info.GroupID);
				
						T.LogDebug("Due to ");
						if (info.IsCausedByJoin) {
							T.LogDebug("the JOIN of " + info.Joined + ".");
						}
						else if (info.IsCausedByLeave) {
							T.LogDebug("the LEAVE of " + info.Left + ".");
						}
						else if (info.IsCausedByDisconnect) {
							T.LogDebug("the DISCONNECT of " + info.Disconnected + ".");
						}
						else if (info.IsCausedByNetwork) {
							SpreadGroup[] stayed = info.Stayed;
							T.LogDebug("NETWORK change.");
							T.LogDebug("VS set has " + stayed.Length + " members:");
							for (int i = 0 ; i < stayed.Length ; i++)
								T.LogDebug("  " + stayed[i]);
						}
					}
					else if (info.IsTransition) {
						T.LogDebug("Received a TRANSITIONAL membership for group " + info.Group);
					}
					else if (info.IsSelfLeave) {
						T.LogDebug("Received a SELF-LEAVE message for group " + info.Group);
					}
				} 
				else if ( msg.IsReject ) {
					// Received a Reject message 
					T.LogDebug("Received a ");
					if(msg.IsUnreliable)
						T.LogDebug("UNRELIABLE");
					else if (msg.IsReliable)
						T.LogDebug("RELIABLE");
					else if (msg.IsFifo)
						T.LogDebug("FIFO");
					else if (msg.IsCausal)
						T.LogDebug("CAUSAL");
					else if (msg.IsAgreed)
						T.LogDebug("AGREED");
					else if (msg.IsSafe)
						T.LogDebug("SAFE");
					T.LogDebug(" REJECTED message.");
				
					T.LogDebug("Sent by  " + msg.Sender + ".");
				
					T.LogDebug("Type is " + msg.Type + ".");
				
					if (msg.EndianMismatch == true)
						T.LogDebug("There is an endian mismatch.");
					else
						T.LogDebug("There is no endian mismatch.");
				
					SpreadGroup[] groups = msg.Groups;
					T.LogDebug("To " + groups.Length + " groups.");
				
					byte[] data = msg.Data;
					T.LogDebug("The data is " + data.Length + " bytes.");
				
					T.LogDebug("The message is: " + System.Text.Encoding.ASCII.GetString(data));
				} 
				else {
					T.LogDebug("Message is of unknown type: " + msg.ServiceType );
				}
			}
			catch (Exception e) {
				T.LogDebug("Exception: " + e);
				Environment.Exit(1);
			}
		}	
		#endregion
	}
}
//using System;
//using System.IO;
//using System.Xml;
//using System.Text;
//using System.Runtime.Serialization;
//using System.Runtime.Serialization.Formatters.Soap;
//using System.Threading;
//using System.Reflection;
//using System.Diagnostics;
//
//using Timok.Core;
//using Timok.Core.Instrumentation;
//using T = Timok.Core.Logging.TimokLogger;
//
//namespace Timok.SpreadLib 
//{
////	internal class AppConstants {
////		public static short NodeId = 1;//TODO: read from spread.conf ??
////	}
////
////	internal class Folders {
////		public static string RbrSignalFilePath = @"C:\Libs\Spread\cspread.User";
////	}
//
////	public class SpreadContext {
////		private static SpreadGroupConnection spreadGroupConnection;
////		public static void Init() {
////			//TODO: spread transaction lifecycle issues here, 
////			//-- should be able to dispose! create new? how do we reconnect after connection reset?
////			ExtMessageHandler _regularMsgHandler = new ExtMessageHandler(TransactionMsg.RegularMsgReceived);
////			ExtMessageHandler _membershipMsgHandler = new ExtMessageHandler(TransactionMsg.MembershipMsgReceived);
////			spreadGroupConnection = new SpreadGroupConnection("laptop", null, 0, "rbr", _regularMsgHandler, _membershipMsgHandler);
////		}
////	}
//
//	//----------------------------------------------------------------------
//	[Serializable]
//	public class Message {
//		object message;
//
//		[NonSerialized]
//		static long globalSequence;
//		
//		[NonSerialized]
//		long instanceSequence;
//		
//		[NonSerialized]
//		static SpreadGroupConnection spreadConnection;
//
//		bool isFromRemoteNode { get { return NodeId != AppConstants.NodeId; } }
//		bool isLocalResponse { get { return NodeId == AppConstants.NodeId; } }
//
//		public string GlobalId;
//
//		public long Sequence { get { return instanceSequence; } }
//		public short NodeId { get { return AppConstants.NodeId; } }
//
//		//TODO: remove !
//		[NonSerialized]
//		static Stopwatch stopwatch = new Stopwatch();
//
//		static Message() { 
//			globalSequence = getLastSequence();
//			
//			//TODO: spread transaction lifecycle issues here, 
//			//-- should be able to dispose! create new? how do we reconnect after connection reset?
//			ExtMessageHandler _regularMsgHandler = new ExtMessageHandler(TransactionMsg.RegularMsgReceived);
//			ExtMessageHandler _membershipMsgHandler = new ExtMessageHandler(TransactionMsg.MembershipMsgReceived);
//			spreadConnection = new SpreadGroupConnection("laptop", null, 0, "rbr", _regularMsgHandler, _membershipMsgHandler);
//		}
//
//		//-- needed for serialiazation
//		public Message() { }
//
//		public Message(object pMessage) {
//			message = pMessage;
//
//			instanceSequence = Interlocked.Increment(ref globalSequence);
//			GlobalId = AppConstants.NodeId.ToString("D3") + instanceSequence.ToString("D19");
//		}
//
//
//		//-- SEND
//		public void Send() {
//			//T.LogInfo("==Sent[" + State + "]: " + GlobalId);
//
//			//TODO: remove
//			stopwatch.Start();
//			Console.WriteLine("Send[" + GlobalId + "]");
//
//			spreadConnection.Send(this.serialize()); //TODO: handle exception
//			SyncLock.Wait();
//		}
//
//		//-- RECEIVE
//		public static void MembershipMsgReceived(byte[] pMessage) {
//			T.LogDebug("TransactionMsg.MembershipMsgReceived | Msg: " + System.Text.Encoding.ASCII.GetString(pMessage));
//		}
//
//		/*
//		 * ------------------------------------------------
//		 * within Transaction:
//		 * ReplicationManager.RegisterReadSet(...)
//		 * ...
//		 * ReplicationManager.RetreiveWriteSet(...)
//		 * 
//		 * 
//		 * ------------------------------------------------
//		 * OnRegularMsgReceived:
//		 *	if (_receivedMsg.isFromRemoteNode) {
//		 *		//(1) execute writeSet
//		 *		Write(_receivedMsg.WriteSet);
//		 *		
//		 *		//store remote writeSet for each running Transaction, 
//		 *		//so that run conflict resolution when they reach Commit stage
//		 *		foreach (Transaction _t in RunningTransactions) {
//		 *			ReplicationManager.Store(_receivedMsg.WriteSet);
//		 *		}
//		 *	}
//		 *	else {	//just go ahead, commit
//		 *		SyncLock.Pulse();
//		 *	}
//		 * 
//		 * ------------------------------------------------
//		 * On Transaction.Commit()
//		 *  //check if anybody has writen to my ReadSet since I've red it...== conflict
//		 *	foreach (WriteSet _ws in ReplicationManager.ConflictCandidates(this) {
//		 *		if (ReplicationManager.CheckForConflict(this, _ws)) {
//		 *			Rollback();
//		 *		}
//		 *	}
//		 * 
//		 * 
//		 * 
//		 * 
//		 * public static void GenericTxMethod(ReadCriteria[] pReadCriteria, params object[] pArgs) {
//					using (Transaction _tx = new Transaction(ReadPredicates[] pReadPredicates, pArgs)) {
//						try {
//							// done by Tx ctor: _tx.ReadSet = _dbgetReadSet(pReadCriteria);
//
//							_db.PartnerCollection.Insert(pPartner);
//							if (pPartner.IsWebAccessEnabled) {
//								//PWD hash set should be done only after insert
//								//in order to have a salt (coposed out of salt_seed and a customer_id)
//
//								//at this point the PWD should be in a clear form
//								//rewrite it with Hashed value
//								//NOTE: !!! hashed PWD will be different on each server
//								SaltHashedPwd sh = SaltHashedPwd.FromClearPwd(pPartner.Pwd, pPartner.Salt);
//								pPartner.Pwd = sh.Value;
//							}
//							else {
//								pPartner.Login_name = null;
//								pPartner.Pwd = null;
//							}
//
//							if ( ! _db.PartnerCollection.Update(pPartner)) {
//								throw new ApplicationException("Failed to add Partner; name:" + pPartner.Name);
//							}
//							_tx.Commit();
//						}
//						catch {
//							throw;
//						}
//					}
//				}
//
//					Transaction internaly creates Db
//					Transaction intercepts views, selects, joins, updates, deletes, inserts to create ReadSet predicates
//					Transaction gathers WriteSet from the db ?
//					using (TTransaction _tx = new TTransaction()) {
//						try {
//							_tx.DbAdapter.Query();
//							
//							_tx.Commit();
//						}
//						catch {
//							throw;
//						}
//					}
//		  
//		 * 
//		 * 
//		 */
//		public static void RegularMsgReceived(byte[] pMessage) {
//			//T.LogDebug("TransactionMsg.RegularMsgReceived | Msg: " + System.Text.Encoding.ASCII.GetString(pMessage));
//			TransactionMsg _receivedTxMsg = deserialize(pMessage);
//			if (_receivedTxMsg == null) {
//				T.LogCritical("Error Serializing: " + System.Text.Encoding.ASCII.GetString(pMessage));
//				return;
//			}
//			T.LogDebug("Received Msg [" + _receivedTxMsg.GlobalId + "]");
//
//			//-- Received Request from Remote node
//			//------------------------------------------------------------------------------
//			if (_receivedTxMsg.isFromRemoteNode) { 
//				Transaction _localTx = ReplicationManager.Find(_receivedTxMsg);
//				if (_localTx != null) {
//					_localTx.Rollback();		//TODO: this is conflict. _localTx is executing on another thread, how do we stop it?
//				}
//				else {
//					_receivedTxMsg.execute();  //should unlock beginLock ?, and don't take begin Lock again ???
//				}
//				return;
//			}
//			
//			//-- Reponse to this node's request
//			//------------------------------------------------------------------------------
//			if (_receivedTxMsg.isLocalResponse) {
//				stopwatch.Stop();
//				Console.WriteLine("Receive[" + _receivedTxMsg.GlobalId + "], time elapsed: " + stopwatch.ElapsedMilliseconds);
//				stopwatch.Reset();
//
//				//-- no remote transaction was conflicting, so we simply complete this transaction
//				SyncLock.Pulse();
//				return;
//			}
//			T.LogCritical("Unknown TxMsg Type: [" + _receivedTxMsg.NodeId + "]");
//		}
//
//		//--------------------- Private -------------------------------------------------
//		private string serialize() {
//			string _str = null;
//
//			try {
//				using (MemoryStream _memStream = new MemoryStream()) {
//					IFormatter _formatter = new SoapFormatter();
//					_formatter.Serialize(_memStream, this);
//					_str = Encoding.ASCII.GetString(_memStream.GetBuffer());
//				}
//			}
//			catch (Exception _ex) {
//				T.LogCritical("TransactionMsg.serialize | Exception: " + _ex);
//			}
//			return _str;
//		}
//
//		private static Message deserialize(byte[] pMessage) {
//			Message _txMsg = null;
//
//			try {
//				using (MemoryStream _memStream = new MemoryStream(pMessage)) {
//					IFormatter _formatter = new SoapFormatter();
//					_txMsg = (TransactionMsg) _formatter.Deserialize(_memStream);
//					if (_txMsg == null) {
//						T.LogCritical("ERROR deserializing: " + System.Text.Encoding.ASCII.GetString(pMessage));
//					}
//				}
//			}
//			catch (Exception _ex) {
//				T.LogCritical("Deserialize Exception: [" + System.Text.Encoding.ASCII.GetString(pMessage) + "][Ex: " + _ex);
//				_txMsg = null;
//			}
//			return _txMsg;
//		}	
//
//		private bool execute() {
//			//T.LogDebug("ConsumerRbr.executeTransaction | Invoke TxMg: " + TypeFullName + " " + MethodFullName + " " + MethodParameters);
//
//			try {
//				Assembly _assembly = getLoadedAssembly(AssemblyFullName);
//				if (_assembly == null) {
//					throw new Exception("MESSAGE: Assembly: " + AssemblyFullName + " NOT FOUND");
//				}
//
//				Type _type = _assembly.GetType(TypeFullName, true);
//				if (_type == null) {
//					throw new Exception("MESSAGE: Type: " + TypeFullName + " NOT FOUND");
//				}
//
//				MethodInfo _method = _type.GetMethod(MethodFullName);
//				if (_method == null) {
//					throw new Exception("MESSAGE: Method: " + MethodFullName + " NOT FOUND");
//				}
//				
//				object _result = _method.Invoke(_type, MethodParameters); //new object[1] { pTxMsg.Argument });
//			}
//			catch (Exception _ex) {
//				T.LogCritical("InternalInvoke Exception: " + _ex);
//				return false;
//			}
//			return true;
//		}	
//
//		private static Assembly getLoadedAssembly(string pFullName) {
//			Assembly[] _assemblies = AppDomain.CurrentDomain.GetAssemblies();
//			foreach (Assembly _asm in _assemblies) {
//				if (_asm.FullName == pFullName) {
//					return _asm;
//				}
//			}
//			return null;
//		}
//
//		private static long getLastSequence() {
//			long _lastSequence = 0;
//
//			try {
//				if (File.Exists(Folders.RbrSignalFilePath)) {
//					using (TextReader _fs = new StreamReader(Folders.RbrSignalFilePath)) {
//						return _lastSequence = long.Parse(_fs.ReadLine());
//					}
//				}
//
//				using (FileStream _fs = new FileStream(Folders.RbrSignalFilePath, FileMode.Create, FileAccess.Write)) {
//					using(StreamWriter _sw = new StreamWriter(_fs)) {
//						_sw.WriteLine(_lastSequence.ToString());
//					}
//				}
//			}
//			catch (Exception _ex) {
//				T.LogCritical("TransactionMsg.getLastSequence | Exception parsing sequence: " + _ex.Message);
//			}
//			return _lastSequence;
//		}
//	}
//}
//

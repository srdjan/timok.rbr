using System;
using System.Data;
using Timok.Core;
using Timok.Rbr.Core.Config;

namespace Timok.Rbr.Core {
	public class Transaction : IDisposable {
		readonly IConnection connection;
		readonly CallingMethodInfo grandParentCallingMethodInfo;
		readonly CallingMethodInfo parentCallingMethodInfo;
		readonly IDbTransaction transaction;
		readonly TransactionMsg txMsg;
		bool completed;

		public Transaction(IConnection pDbConnection, object pArg1) : this(pDbConnection) {
			if (isExecutingLocaly) {
				parentCallingMethodInfo.MethodParameters = new object[1];
				parentCallingMethodInfo.MethodParameters[0] = Cloner.Clone(pArg1);
				txMsg = new TransactionMsg(parentCallingMethodInfo, Configuration.Instance.Folders.AuditFolder);
			}
			transaction = connection.BeginTransaction();
		}

		public Transaction(IConnection pDbConnection, object pArg1, object pArg2) : this(pDbConnection) {
			if (isExecutingLocaly) {
				parentCallingMethodInfo.MethodParameters = new object[2];
				parentCallingMethodInfo.MethodParameters[0] = Cloner.Clone(pArg1);
				parentCallingMethodInfo.MethodParameters[1] = Cloner.Clone(pArg2);
				txMsg = new TransactionMsg(parentCallingMethodInfo, Configuration.Instance.Folders.AuditFolder);
			}
			transaction = connection.BeginTransaction();
		}

		public Transaction(IConnection pDbConnection, object pArg1, object pArg2, object pArg3) : this(pDbConnection) {
			if (isExecutingLocaly) {
				parentCallingMethodInfo.MethodParameters = new object[3];
				parentCallingMethodInfo.MethodParameters[0] = Cloner.Clone(pArg1);
				parentCallingMethodInfo.MethodParameters[1] = Cloner.Clone(pArg2);
				parentCallingMethodInfo.MethodParameters[2] = Cloner.Clone(pArg3);
				txMsg = new TransactionMsg(parentCallingMethodInfo, Configuration.Instance.Folders.AuditFolder);
			}
			transaction = connection.BeginTransaction();
		}

		public Transaction(IConnection pDbConnection, object pArg1, object pArg2, object pArg3, object pArg4) : this(pDbConnection) {
			if (isExecutingLocaly) {
				parentCallingMethodInfo.MethodParameters = new object[4];
				parentCallingMethodInfo.MethodParameters[0] = Cloner.Clone(pArg1);
				parentCallingMethodInfo.MethodParameters[1] = Cloner.Clone(pArg2);
				parentCallingMethodInfo.MethodParameters[2] = Cloner.Clone(pArg3);
				parentCallingMethodInfo.MethodParameters[3] = Cloner.Clone(pArg4);
				txMsg = new TransactionMsg(parentCallingMethodInfo, Configuration.Instance.Folders.AuditFolder);
			}
			transaction = connection.BeginTransaction();
		}

		public Transaction(IConnection pDbConnection, object pArg1, object pArg2, object pArg3, object pArg4, object pArg5): this(pDbConnection) {
			if (isExecutingLocaly) {
				parentCallingMethodInfo.MethodParameters = new object[5];
				parentCallingMethodInfo.MethodParameters[0] = Cloner.Clone(pArg1);
				parentCallingMethodInfo.MethodParameters[1] = Cloner.Clone(pArg2);
				parentCallingMethodInfo.MethodParameters[2] = Cloner.Clone(pArg3);
				parentCallingMethodInfo.MethodParameters[3] = Cloner.Clone(pArg4);
				parentCallingMethodInfo.MethodParameters[4] = Cloner.Clone(pArg5);
				txMsg = new TransactionMsg(parentCallingMethodInfo, Configuration.Instance.Folders.AuditFolder);
			}
			transaction = connection.BeginTransaction();
		}

		public Transaction(IConnection pDbConnection, object pArg1, object pArg2, object pArg3, object pArg4, object pArg5, object pArg6): this(pDbConnection) {
			if (isExecutingLocaly) {
				parentCallingMethodInfo.MethodParameters = new object[6];
				parentCallingMethodInfo.MethodParameters[0] = Cloner.Clone(pArg1);
				parentCallingMethodInfo.MethodParameters[1] = Cloner.Clone(pArg2);
				parentCallingMethodInfo.MethodParameters[2] = Cloner.Clone(pArg3);
				parentCallingMethodInfo.MethodParameters[3] = Cloner.Clone(pArg4);
				parentCallingMethodInfo.MethodParameters[4] = Cloner.Clone(pArg5);
				parentCallingMethodInfo.MethodParameters[5] = Cloner.Clone(pArg6);
				txMsg = new TransactionMsg(parentCallingMethodInfo, Configuration.Instance.Folders.AuditFolder);
			}
			transaction = connection.BeginTransaction();
		}

		internal Transaction(IConnection pDbConnection) {
			completed = false;
			connection = pDbConnection;
			txMsg = null;

			grandParentCallingMethodInfo = StackInfo.Get(4);
			if (grandParentCallingMethodInfo == null) {
				throw new Exception("grandParentCallingMethodInfo == null");
			}

			parentCallingMethodInfo = StackInfo.Get(3);
			if (parentCallingMethodInfo == null) {
				throw new Exception("parentCallingMethodInfo == null");
			}
		}

		bool isExecutingLocaly {
			get { return grandParentCallingMethodInfo.FullName.IndexOf("InternalInvoke") < 0; }
		}

		public void Dispose() {
			if (! completed) {
				//T.LogRbr(LogSeverity.Critical, "Transaction.Dispose", "Rollback invoked");
				transaction.Rollback();
			}
		}

		//-- CommitTransaction --------------
		public void Commit() {
			//-- Serialize TransactionMsg if local tx
			if (isExecutingLocaly) {
				txMsg.Serialize();
			}
			transaction.Commit();
			completed = true;
		}
	}
}
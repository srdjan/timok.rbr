using System.Data;

namespace Timok.Rbr.Core
{
	public interface IConnection {
		IDbTransaction BeginTransaction();
		IDbTransaction BeginTransaction(IsolationLevel isolationLevel);
		void CommitTransaction();
		void RollbackTransaction();
	}
}

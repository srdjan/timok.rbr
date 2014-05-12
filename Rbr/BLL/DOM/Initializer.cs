using System;
using System.Threading;
using Timok.Logger;
using Timok.Rbr.Core.Config;
using Timok.Rbr.DAL.RbrDatabase;

namespace Timok.Rbr.BLL.DOM {
	public class Initializer {
		public Initializer() {
		}

		public bool WaitUntilSQLStarts() {
			try {
				for (var _retryCount = 1; _retryCount <= Configuration.Instance.Db.NumberOfDbOpenRetries; _retryCount++) {
					if (isSqlEngineRunning()) {
						return true;
					}
					TimokLogger.Instance.LogRbr(LogSeverity.Critical, "Initializer.WaitUntilSQLStarts", string.Format("Retry {0}, FAILED while checking if SQL started", _retryCount));
					Thread.Sleep(1000);
				}
			}
			catch (Exception _ex) {
				TimokLogger.Instance.LogRbr(LogSeverity.Critical, "Initializer.WaitUntilSQLStarts", "FAILED while checking if SQL started, Exception\r\n" + _ex);
			}
			TimokLogger.Instance.LogRbr(LogSeverity.Error, "Initializer.WaitUntilSQLStarts", "SQL Server didn't start on time.");
			return false;
		}

		static int counter = 0;
		bool isSqlEngineRunning() {
			try {
				using (var _rbrDb = new Rbr_Db()) {
					_rbrDb.Close();
					return true;
				}
			}
			catch (Exception _ex) {
				if (counter++ % 10 == 0) {
					TimokLogger.Instance.LogRbr(LogSeverity.Critical, "Initializer.isSqlEngineRunning", "Exception\r\n" + _ex);
				}
			}
			return false;
		}
	}
}
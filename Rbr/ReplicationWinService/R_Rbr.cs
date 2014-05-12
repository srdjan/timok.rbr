using System;
using Timok.Logger;
using Timok.Rbr.BLL.Entities;
using Timok.Rbr.BLL.ImportExport.DialPlan;
using Timok.Rbr.Core;

namespace Timok.Rbr.Replication {
	public class R_Rbr : ReplicationBase {
		R_Rbr() { }

		public static bool Publish(Node pNode, string pFilePath) {
			bool _result;
			try {
				_result = Send(pNode, pFilePath);
				TimokLogger.Instance.LogRbr(LogSeverity.Status, "R_Rbr.Publish", string.Format("Finished, File={0}", pFilePath));
			}
			catch (Exception _ex) {
				TimokLogger.Instance.LogRbr(LogSeverity.Critical, "R_Rbr.Publish", string.Format("File={0}, Exception:\r\n{1} ", pFilePath, _ex));
				return false;
			}
			return _result;
		}

		public static bool Consume(string pFilePath) {
			try {
				if (isImportFile(pFilePath)) {
					new DialPlanImportExportTask().Run(pFilePath);
				}
				else {
					var _txMsg = TransactionMsg.Deserialize(pFilePath);
					if (_txMsg == null) {
						TimokLogger.Instance.LogRbr(LogSeverity.Critical, "R_Rbr.Consume", string.Format("TransactionMsg.Deserialize Error, File={0}", pFilePath));
						return false;
					}
					_txMsg.Execute();
				}

				TimokLogger.Instance.LogRbr(LogSeverity.Status, "R_Rbr.Consume", string.Format("Finished, File={0}", pFilePath));
			}
			catch (Exception _ex) {
				TimokLogger.Instance.LogRbr(LogSeverity.Critical, "R_Rbr.Consume", string.Format("File={0}, Exception:\r\n{1}", pFilePath, _ex));
				return false;
			}
			return true;
		}

		//--------------------------------- private -------------------------------------------
		static bool isImportFile(string pFilePath) { return pFilePath.ToLower().IndexOf(".import") > 0; }
	}
}
using System;
using Timok.Logger;
using Timok.Rbr.BLL.DOM;
using Timok.Rbr.BLL.Entities;

namespace Timok.Rbr.Replication {
	public class R_Cdr : ReplicationBase {
		R_Cdr() {}

		public static bool Publish(Node pNode, string pFilePath) {
			try {
				if (pNode == null) {
					TimokLogger.Instance.LogRbr(LogSeverity.Critical, "R_Cdr.Publish", "Couldn't find Admin Node");
					return false;
				}

				Send(pNode, pFilePath);
				TimokLogger.Instance.LogRbr(LogSeverity.Status, "R_Cdr.Publish", string.Format("Finished, File={0}", pFilePath));
			}
			catch (Exception _ex) {
				TimokLogger.Instance.LogRbr(LogSeverity.Status, "R_Cdr.Publish", string.Format("File={0}, Exception:\r\n{1}", pFilePath, _ex));
				return false;
			}
			return true;
		}

		public static bool Consume(string pFilePath) {
			bool _result;
			try {
				_result = Cdr.DeserializeAndImport(pFilePath);
				TimokLogger.Instance.LogRbr(LogSeverity.Status, "R_Cdr.Consume", string.Format("Finished, File={0}", pFilePath));
			}
			catch (Exception _ex) {
				TimokLogger.Instance.LogRbr(LogSeverity.Status, "R_Cdr.Consume", string.Format("File={0}, Exception:\r\n{1}", pFilePath, _ex));
				return false;
			}
			return _result;
		}
	}
}
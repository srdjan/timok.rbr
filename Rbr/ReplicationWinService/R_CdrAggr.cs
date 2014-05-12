using System;
using Timok.Logger;
using Timok.Rbr.BLL.DOM;
using Timok.Rbr.BLL.Entities;

namespace Timok.Rbr.Replication {
	public class R_CdrAggr : ReplicationBase {
		R_CdrAggr() { }

		public static bool Publish(Node pNode, string pFilePath) {
			try {
				if (pNode == null) {
					TimokLogger.Instance.LogRbr(LogSeverity.Critical, "R_CdrAggr.Publish", "Couldn't find Admin Node");
					return false;
				}

				Send(pNode, pFilePath);
				TimokLogger.Instance.LogRbr(LogSeverity.Status, "R_CdrAggr.Publish", string.Format("Finished, File={0}", pFilePath));
			}
			catch (Exception _ex) {
				TimokLogger.Instance.LogRbr(LogSeverity.Critical, "R_CdrAggr.Publish", string.Format("File={0}, Exception:\r\n{1} ", pFilePath, _ex));
				return false;
			}
			return true;
		}

		public static bool Consume(string pFilePath) {
			bool _result;
			try {
				_result = CdrAggregate.DeserializeAndImport(pFilePath);
				TimokLogger.Instance.LogRbr(LogSeverity.Status, "R_CdrAggr.Consume", string.Format("Finished, File={0}", pFilePath));
			}
			catch (Exception _ex) {
				TimokLogger.Instance.LogRbr(LogSeverity.Critical, "R_CdrAggr.Consume", string.Format("File={0}, Exception:\r\n{1} ", pFilePath, _ex));
				return false;
			}
			return _result;
		}
	}
}
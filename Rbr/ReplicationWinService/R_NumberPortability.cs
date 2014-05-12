using System;
using System.IO;
using Timok.Logger;
using Timok.Rbr.BLL.Controllers;
using Timok.Rbr.Core.Config;

namespace Timok.Rbr.Replication {
	public class R_NumberPortability {
		public static bool Consume(string pFilePath) {
			try {
				TimokLogger.Instance.LogRbr(LogSeverity.Status, "R_NumberPortability.Consume", string.Format("Finished, File={0}", pFilePath));
				return deserializeAndImport(pFilePath);
			}
			catch (Exception _ex) {
				TimokLogger.Instance.LogRbr(LogSeverity.Critical, "R_NumberPortability.Consume", string.Format("File={0}, Exception:\r\n{1} ", pFilePath, _ex));
				return false;
			}
		}

		//--  RN,RouteID,TN,Status
		//		55112,123,3432325090,0
		//--
		public static bool deserializeAndImport(string pFilePath) {
			TimokLogger.Instance.LogRbr(LogSeverity.Debug, "NumberPortability.DeserializeAndImport", "Started");

			using (var _tr = new StreamReader(pFilePath)) {
				string _line;
				while ((_line = _tr.ReadLine()) != null) {
					string[] _tokens = _line.Split(new[] {','});

					string _rountingNumber = _tokens[0].Trim();
					string _routeID = _tokens[1].Trim();
					string _dialedNumber = _tokens[2].Trim();
					NumberPortabilityRequestStatus _status = getStatus(_tokens[3].Trim());
					TimokLogger.Instance.LogRbr(LogSeverity.Status, string.Format("NumberPortability Entry: {0}-{1}-{2}-{3}", _rountingNumber, _routeID, _dialedNumber, _status));

					if (_status == NumberPortabilityRequestStatus.Upsert) {
						(new NumberPortabilityController()).Upsert(_routeID, _rountingNumber, _dialedNumber);
					}
					else if (_status == NumberPortabilityRequestStatus.Delete) {
						(new NumberPortabilityController()).Delete(_routeID, _rountingNumber, _dialedNumber);
					}
					else {
						throw new Exception(string.Format("Number Portability: Invalid text file format, Status not known: {0}", _line));
					}
				}
			}
			return true;
		}

		//----------------------------------------- Private -------------------------------------------
		static NumberPortabilityRequestStatus getStatus(string pStatus) {
			int _status;
			if (!int.TryParse(pStatus, out _status)) {
				throw new Exception("Number Portability: Invalid text file format, Status not an inTimokLogger.Instance.");
			}
			return (NumberPortabilityRequestStatus) Enum.Parse(typeof (NumberPortabilityRequestStatus), pStatus);
		}
	}
}
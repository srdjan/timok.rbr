using System;
using Timok.Logger;
using Timok.NetworkLib.Udp;
using Timok.Rbr.Core.Config;
using Timok.Rbr.Core.LogManagement;

namespace Timok.Rbr.Core {
	public class RbrClient {
		public static RbrClient Instance = new RbrClient();
		readonly UdpClient udpClient;

		RbrClient() {
			udpClient = new UdpClient(Configuration.Instance.Main.HostIP, AppConstants.UdpRbrClientStartPort, AppConstants.UdpRbrClientPortRange, Configuration.Instance.Main.HostIP, AppConstants.UdpRbrServerStartPort, TimokLogger.Instance.LogRbr);
		}

		//------------------- Rbr API ------------------------------------------------
		public long GetNextSequence() {
			try {
				var _parameters = new string[] { };

				//-- Send command
				string _strResult;
				var _success = udpClient.SendAndReceive(RbrApi.Name, RbrApi.OnGetNextSequence, _parameters, out _strResult);
				if (_success) {
					//T.LogRbr(LogSeverity.Debug, "RbrClient.GetNextSequence", string.Format("Returned={0}", _strResult));
					var _outParams = UdpMessageParser.GetOutParameters(_strResult);
					if (_outParams.Length == 2 && _outParams[0] == "true") {
						return long.Parse(_outParams[1]);
					}
				}
				throw new Exception("Rbr communication error");
			}
			catch (Exception _ex) {
				throw new Exception("Rbr communication exception", _ex);
			}
		}

		//--------------------- Logger API -------------------------------------------
		public bool SetLogSeverity(LogSeverity pLevel) {
			var _parameters = new[] { pLevel.ToString() };
			return udpClient.Send(LoggerAPI.Name, LoggerAPI.SetLogSeverity, _parameters);
		}

		public bool GetLogSeverity(out LogSeverity pLogSeverity) {
			pLogSeverity = LogSeverity.Debug; //default

			var _parameters = new string[] { };

			//-- Send command
			string _strResult;
			var _result = udpClient.SendAndReceive(LoggerAPI.Name, LoggerAPI.GetLogSeverity, _parameters, out _strResult);
			if (_result) {
				var _params = UdpMessageParser.GetOutParameters(_strResult);
				if (_params.Length > 0 && _params[0] == "1") {
					pLogSeverity = (LogSeverity) Enum.Parse(typeof(LogSeverity), _params[1]);
				}
			}
			return _result;
		}

		//--------------------- Call Stats API -------------------------------------------
		public CallStats GetCallStatistics(CallStatsType pCallStatsType, int pKey) {
			try {
				var _parameters = new[] { ((int) pCallStatsType).ToString(), pKey.ToString() };

				//-- Send command
				string _strResult;
				var _success = udpClient.SendAndReceive(RbrApi.Name, RbrApi.OnGetCallStats, _parameters, out _strResult);
				if (_success) {
					var _outParams = UdpMessageParser.GetOutParameters(_strResult);
					return new CallStats(_outParams);
				}
				throw new Exception("Rbr communication error");
			}
			catch (Exception _ex) {
				throw new Exception("Rbr communication exception", _ex);
			}
		}
	}
}
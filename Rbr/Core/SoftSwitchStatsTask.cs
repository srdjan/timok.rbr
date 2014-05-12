using System;
using System.Net;
using System.Threading;
using Timok.Core.BackgroundProcessing;
using Timok.Logger;

namespace Timok.Rbr.Core {
	public class SoftSwitchStatsTask : ITask {
		readonly IPAddress ipAddress;
		readonly int port;
		const int INTERVAL_IN_SECONDS = 3;
		const int CONNECTION_TIMEOUT = 10;
		LogDelegate log;

		public SoftSwitchStatsTask(string pIPAddress, LogDelegate pLog) {
			IPAddress.TryParse(pIPAddress, out ipAddress);
			port = 7000;
			log = pLog;
		}

		#region ITask Members

		BackgroundWorker host;
		public BackgroundWorker Host { set { host = value; } }

		public void Run(object pSender, WorkerEventArgs pArgs) {
			using (var _gkStatusPort = new GkStatusPort(ipAddress, port, CONNECTION_TIMEOUT)) {
				if (_gkStatusPort.IsConnected) {
					while (! host.CancellationPending) {
						try {
							string _response;
							_gkStatusPort.SendCommand("s", 20, out _response);
							dataReceived(_response);
							Thread.Sleep(TimeSpan.FromSeconds(INTERVAL_IN_SECONDS));
						}
						catch (Exception _ex) {
							log(LogSeverity.Error, "SoftSwitchStatsTask.Run", "Exception: " + _ex);
							pArgs.Cancel = true;
							pArgs.Result = _ex;
							host.CancelAsync();
						}
					}
				}
				else {
					log(LogSeverity.Status, "SoftSwitchStatsTask.Run", string.Format("Cannot connect to {0}:{1}", ipAddress, port));
					throw new Exception("Cannot connect to [" + ipAddress + ":" + port + "]");
				}
				_gkStatusPort.Dispose();
				log(LogSeverity.Debug, "SoftSwitchStatsTask.Run", "CancellationPending - exit while loop.");
			}
		}

		public void CancelAsync() {
			host.CancelAsync();
		}

		#endregion

		void dataReceived(string pData) {
			if (host != null) {
				var _callStats = parse(pData);
				host.ReportDataReceived(_callStats);
			}
		}

		/*
		0	-- Endpoint Statistics --
		1	Total Endpoints: 123  Terminals: 0  Gateways: 123  NATed: 0
		2	Cached Endpoints: 0  Terminals: 0  Gateways: 0
		3	-- Call Statistics --
		4	Current Calls: 0 Active: 0 From Neighbor: 0 From Parent: 0
		5	Total Calls: 67989  Successful: 6336  From Neighbor: 56151  From Parent: 0
		6	Startup: Sat, 05 Mar 2005 01:58:44 -0500   Running: 10 days 16:10:30
		7	;
	  */

		CallStats parse(string pData) {
			var _callStats = new CallStats();

			if (!string.IsNullOrEmpty(pData)) {
				var _lines = pData.Replace("\r", "").Split("\n".ToCharArray());
				var _startLine = _lines[6];

				_callStats.Startup = _startLine.Substring(8, _startLine.IndexOf("Running:") - 8).Trim();
				_callStats.Running = _startLine.Substring(_startLine.IndexOf("Running:") + 8).Trim();

				var _currCallsLine = _lines[4];
				var _currCalls = _currCallsLine.Split(":".ToCharArray());
				_callStats.ConnectingCalls = int.Parse(_currCalls[1].Replace("Active", ""));
				_callStats.ConnectedCalls = int.Parse(_currCalls[2].Replace("From Neighbor", ""));

				var _totalLine = _lines[5];
				var _totCalls = _totalLine.Split(":".ToCharArray());
				_callStats.TotalCalls = int.Parse(_totCalls[1].Replace("Successful", ""));
				_callStats.TotalSuccessfullCalls = int.Parse(_totCalls[2].Replace("From Neighbor", ""));
			}
			return _callStats;
		}
	}
}
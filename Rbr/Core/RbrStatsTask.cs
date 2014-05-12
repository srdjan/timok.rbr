using System;
using System.Threading;
using Timok.Core.BackgroundProcessing;
using Timok.Rbr.Core.Config;

namespace Timok.Rbr.Core {
	public class RbrStatsTask : ITask {

		const int INTERVAL_IN_SECONDS = 3;

		BackgroundWorker host;

		public BackgroundWorker Host { set { host = value; } }

		public void Run(object pSender, WorkerEventArgs pArgs) {
			while (! host.CancellationPending) {
				try {
					var _callStats = RbrClient.Instance.GetCallStatistics(CallStatsType.Total, -1);
					host.ReportDataReceived(_callStats);
					Thread.Sleep(TimeSpan.FromSeconds(INTERVAL_IN_SECONDS));
				}
				catch (Exception _ex) {
					pArgs.Cancel = true;
					pArgs.Result = _ex;
					host.CancelAsync();
				}
			}
		}

		public void CancelAsync() {
			host.CancelAsync();
		}
	}
}
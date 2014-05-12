using System;
using Timok.Core.BackgroundProcessing;
using Timok.Core.DbLib;
using Timok.Logger;

namespace Timok.Rbr.Core {
	public class CdrExportTask : ITask {
		BCPExportInfo bcpExportInfo;
		CdrExportResult cdrExportResult;
		LogDelegate log;
		BackgroundWorker host;

		public CdrExportTask(BCPExportInfo pBCPExportInfo, LogDelegate pLog) {
			bcpExportInfo = pBCPExportInfo;
			log = pLog;
		}

		public CdrExportResult CdrExportResult { get { return cdrExportResult; } }

		#region ITask Members

		public BackgroundWorker Host { set { host = value; } }

		public void Run(object pSender, WorkerEventArgs pArgs) {
			try {
				host.WorkCompleted += host_WorkCompleted;

				var _res = BCPExportHelper.Run(bcpExportInfo);

				cdrExportResult = new CdrExportResult(_res.RecordsExported, _res.ClockTimeMilliseconds, _res.ClockTimeMsg);

				if (_res.ErrorDescription != null) {
					cdrExportResult.FilePath = string.Empty;
					cdrExportResult.ExportException = new Exception(_res.ErrorDescription);
					log(LogSeverity.Error, "CdrExportTask.Run", _res.ErrorDescription);
				}
				else {
					cdrExportResult.FilePath = bcpExportInfo.FilePath;
				}
			}
			catch (Exception _ex) {
				log(LogSeverity.Error, "CdrExportTask.Run", string.Format("Exception:\r\n{0}", _ex));
			}
		}

		public void CancelAsync() {
			host.CancelAsync();
		}

		#endregion

		void host_WorkCompleted(object sender, WorkCompletedEventArgs e) {
			host.ReportDataReceived(cdrExportResult);
		}
	}

	public struct CdrExportResult {
		public long ClockTimeMilliseconds;
		public string ClockTimeMsg;
		public Exception ExportException;
		public string FilePath;
		public int RecordsExported;

		public CdrExportResult(int pRecordsExported, long pClockTimeMilliseconds, string pClockTimeMsg) {
			RecordsExported = pRecordsExported;
			ClockTimeMilliseconds = pClockTimeMilliseconds;
			ClockTimeMsg = pClockTimeMsg;
			ExportException = null;
			FilePath = string.Empty;
		}

		public override string ToString() {
			return string.Format("RecordsExported: {0}; {1}", RecordsExported, ClockTimeMsg);
		}
	}
}
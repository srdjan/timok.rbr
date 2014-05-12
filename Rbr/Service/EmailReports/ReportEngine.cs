using System;
using Timok.Logger;

namespace Timok.Rbr.Service.EmailReports {
	internal class ReportEngine {
		
		public void Run(DateTime pRunForDate, bool pSendEmail) {
			try {
				TimokLogger.Instance.LogRbr(LogSeverity.Debug, "ReportEngine.Run", "Daily Mintes Report [RunNow] starting");

				var _report = new DailyMinutesReport();
				string _subject, _body;
				_report.Run(pRunForDate, out _subject, out _body);

				if (pSendEmail) {
					TimokLogger.Instance.LogEmail(_subject, _body);
				}

				TimokLogger.Instance.LogRbr(LogSeverity.Debug, "ReportEngine.Run", "Daily Mintes Report finished");
			}
			catch (Exception _ex) {
				TimokLogger.Instance.LogRbr(LogSeverity.Critical, "ReportEngine.Run", string.Format("Exception:\r\n{0}", _ex));
			}
		}
	}
}
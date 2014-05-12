using System;
using System.Configuration;
using System.IO;
using System.Text;
using Timok.Core;
using Timok.Logger;
using Timok.Rbr.BLL.DOM;

namespace Timok.Rbr.Service.EmailReports {
	public class DailyMinutesReport {
		readonly string dateFormat = "yyyy-MM-dd";
		const string SEARCH_PATTERN = "CDR*.txt";
		readonly string reportFileNamePrefix = "DailyMinutes";
		readonly string reportFileExt = "txt";

		public DailyMinutesReport() {
			try {
				dateFormat = (ConfigurationManager.AppSettings["DateFormat"] != null && ConfigurationManager.AppSettings["DateFormat"].Trim().Length > 0)
				             	? ConfigurationManager.AppSettings["DateFormat"]
				             	: dateFormat;

				reportFileNamePrefix = (ConfigurationManager.AppSettings["ReportFileNamePrefix"] != null &&
				                        ConfigurationManager.AppSettings["ReportFileNamePrefix"].Trim().Length > 0)
				                       	? ConfigurationManager.AppSettings["ReportFileNamePrefix"]
				                       	: reportFileNamePrefix;

				reportFileExt = (ConfigurationManager.AppSettings["ReportFileExt"] != null && ConfigurationManager.AppSettings["ReportFileExt"].Trim().Length > 0)
				                	? ConfigurationManager.AppSettings["ReportFileExt"]
				                	: reportFileExt;

				TimokLogger.Instance.LogRbr(LogSeverity.Debug, "DailyMinutesReport.Ctor", string.Format("ArchiveDir: {0}\r\n\tdateFormat: {1}\r\n\tsearchPattern: {2}\r\n\treportFileNamePrefix: {3}\r\n\treportFileExt: {4}", Core.Config.Configuration.Instance.Folders.ArchiveFolder, dateFormat, SEARCH_PATTERN, reportFileNamePrefix, reportFileExt));
			}
			catch (Exception _ex) {
				throw new ApplicationException("Exception:\r\n" + _ex, _ex);
			}
		}

		public string Run(out string pSubject, out string pBody) {
			return Run(DateTime.Today.AddDays(-1), out pSubject, out pBody);
		}

		public string Run(DateTime pDate, out string pSubject, out string pBody) {
			return run(pDate, out pSubject, out pBody);
		}

		//---------------------------------------------------------------------------------------------------------------------------------------------
		string run(DateTime pDate, out string pSubject, out string pBody) {
			pBody = "";
			pSubject = "";
			string _reportFilePath;
			var _body = new StringBuilder();

			try {
				TimokLogger.Instance.LogRbr(LogSeverity.Debug, "DailyMinutesReport.run", string.Format("Date for DailyMinutesReport: {0}", pDate.ToShortDateString()));

				var _processDir = Path.Combine(Core.Config.Configuration.Instance.Folders.ArchiveFolder, pDate.ToString(dateFormat));
				if (! Directory.Exists(_processDir)) {
					Directory.CreateDirectory(_processDir);
				}
				TimokLogger.Instance.LogRbr(LogSeverity.Debug, "DailyMinutesReport.run", string.Format("ProcessDir: {0}", _processDir));

				_reportFilePath = Path.Combine(_processDir, reportFileNamePrefix + pDate.ToString(dateFormat) + "." + reportFileExt);

				_body.Append(getCustomerRouteMinutesSummary(pDate));
				_body.Append(getEndpointAsrSummary(pDate));

				writeToFile(_reportFilePath, _body);

				pSubject = "Daily report: " + pDate.ToString(dateFormat);
			}
			catch (Exception _ex) {
				throw new Exception("DailyMinutesReport failed; Error:\r\n" + _ex, _ex);
			}
			pBody = _body.ToString();
			return _reportFilePath;
		}

		void writeToFile(string pReportFilePath, StringBuilder pBody) {
			try {
				using (var _sw = new StreamWriter(pReportFilePath)) {
					_sw.WriteLine(pBody.ToString());
				}
			}
			catch (Exception _ex) {
				TimokLogger.Instance.LogRbr(LogSeverity.Critical, "DailyMinutesReport.writeToFile", string.Format("DailyMinutesReport.writeToFile() failed. Exception:\r\n{0}", _ex));
			}
		}

		string getEndpointAsrSummary(DateTime pDate) {
			var _sb = new StringBuilder();
			var _endpointASRList = Cdr.GetTermEndpointsASR(TimokDate.ParseToShortTimokDate(pDate));
			_sb.Append("\r\nTerminating TG ASR (All/Connected)\r\n");
			_sb.Append("-------------------------------------------------------\r\n");
			var _totalCalls = 0;
			var _totalConectedCalls = 0;
			foreach (var _endpointASR in _endpointASRList) {
				_sb.Append(_endpointASR.Alias);
				_sb.Append(" [TG" + _endpointASR.EndpointId + "]:" + "\t\t\t" + calculateTotalASR(_endpointASR.Calls, _endpointASR.ConnectedCalls) + "\t\t\t(" +
				           _endpointASR.Calls + "/" + _endpointASR.ConnectedCalls + ")\r\n");
				//_sb.Append(" [TG" + _endpointAsr.EndpointId + "]:" + "\t\t\t" + _endpointAsr.Asr.ToString("0%") + "\t\t\t(" + _endpointAsr.Calls + "/" + _endpointAsr.ConnectedCalls + ")\r\n");
				_totalCalls += _endpointASR.Calls;
				_totalConectedCalls += _endpointASR.ConnectedCalls;
			}
			_sb.Append("\r\n" + "Total:\t\t\t" + calculateTotalASR(_totalCalls, _totalConectedCalls) + "\t\t\t(" + _totalCalls + "/" + _totalConectedCalls + ")\r\n");
			return _sb.ToString();
		}

		string getCustomerRouteMinutesSummary(DateTime pDate) {
			var _sb = new StringBuilder();
			var _customerRouteMinutesSummaries = Cdr.GetCustomerRouteMinutesSummaries(TimokDate.ParseToShortTimokDate(pDate));
			_sb.Append("Inbound Country Summary (Minutes)\r\n");
			_sb.Append("-------------------------------------------------------\r\n");
			decimal _totalMinutes = 0;

			foreach (var _routeMinutesSummary in _customerRouteMinutesSummaries) {
				_sb.Append(_routeMinutesSummary.Name + ":" + "\t\t\t" + _routeMinutesSummary.Minutes.ToString("0.00") + "\r\n");
				_totalMinutes = decimal.Add(_totalMinutes, _routeMinutesSummary.Minutes);
			}
			_sb.Append("\r\n" + "Total Minutes:\t\t" + _totalMinutes.ToString("0.00") + "\r\n\r\n");
			return _sb.ToString();
		}

		static string calculateTotalASR(int pTgCalls, int pTgConnectedCalls) {
			decimal _percentage = 0;
			if (pTgCalls > 0) {
				_percentage = decimal.Divide(pTgConnectedCalls, pTgCalls);
			}
			return _percentage.ToString("0%");
		}
	}
}
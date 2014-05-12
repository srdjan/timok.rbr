using System;
using System.Configuration;
using System.Diagnostics;
using System.IO;
using System.Text;

namespace Timok.Logger {
	public class TimokLogger : ILogger {
		public static TimokLogger Instance = new TimokLogger(ConfigurationManager.AppSettings.Get("Win"), Path.Combine(ConfigurationManager.AppSettings.Get("RbrRoot"), "Log"));
#if DEBUG
		readonly Logger consoleLogger; // only for debug mode
#endif
		readonly Logger fileLogger; //All
		readonly Logger eventLogLogger; //Filter only Critical and Fatal LogSeverity
		Logger emailLogger; //Filter only Critical and Fatal LogSeverity

		readonly CompositeLogger logger;
		readonly TimokLogEntryFormatter timokLogEntryFormatter;

		public LogSeverity LogSeverity {
			get { return logger.SeverityThreshold; } 
			set { logger.SeverityThreshold = value; }
		}

		public string LogFolder { get; private set; }

		public TimokLogger(string pAppName, string pLogFolder) {
			LogFolder = Path.Combine(pLogFolder, "Log");
			EventLog.WriteEntry("Application", string.Format("\r\nTimokLogger.Ctor LogFolder: {0}", LogFolder), EventLogEntryType.Information, 1);

			var _logMessage = new StringBuilder();
			try {
				if (! Directory.Exists(LogFolder)) {
					Directory.CreateDirectory(LogFolder);
				}
				_logMessage.Append("\r\nLogFolder: " + LogFolder);

				var _logFilePath = Path.Combine(LogFolder, pAppName + ".{0}.log");

				timokLogEntryFormatter = new TimokLogEntryFormatter {FormatString = "yyyy/MM/dd HH:mm:ss"};

				//--Instantiate loggers and assign custom formatter
				fileLogger = new RollingFileLogger(new RollingFileLogger.RollOverDateStrategy(_logFilePath, "yyyy-MM-dd-HH")) {Formatter = timokLogEntryFormatter};

				eventLogLogger = new EventLogLogger {Formatter = timokLogEntryFormatter};

				//--Now instantiate a CompositeLogger
				logger = new CompositeLogger();
				logger.AddLogger("file", fileLogger);
				logger.AddLogger("eventLog", eventLogLogger);

#if DEBUG
				consoleLogger = TextWriterLogger.NewConsoleLogger();
				consoleLogger.Formatter = timokLogEntryFormatter;
				logger.AddLogger("console", consoleLogger);
#endif

				//--Set Log Severity
				var _severity = ConfigurationManager.AppSettings["LogSeverity"];
				if (!string.IsNullOrEmpty(_severity)) {
					logger.SeverityThreshold = (LogSeverity) Enum.Parse(typeof (LogSeverity), _severity);
				}
				_logMessage.Append("\r\nLogSeverity: " + logger.SeverityThreshold);
			}
			catch (Exception _ex) {
				_logMessage.Append("\r\nException: " + _ex);
				writeToEventLog(_logMessage, EventLogEntryType.Error);
				return;
			}
			writeToEventLog(_logMessage, EventLogEntryType.Information);
		}

		public void AddEmailLogger(string pFolder, string pRbrServer, string pEmailServer, string pEmailFrom, string pEmailPassword, string pEmailTo, SetForSendingDelegate pSetForSendingDelegate) {
			if (emailLogger != null) {
				return;
			}
			emailLogger = new EmailLogger(pFolder, pRbrServer, pEmailServer, pEmailPassword, pEmailFrom, pEmailTo, pSetForSendingDelegate) {Formatter = timokLogEntryFormatter};
			logger.AddLogger("emailLog", emailLogger);
		}

		public void LogEmail(string pSubject, string pBody) {
			if (emailLogger != null) {
				//NOTE: IGOR - send Critical LogSeverity, otherwize the EmailLogger will not send
				emailLogger.Log(LogSeverity.Critical, pSubject, pBody);
			}
		}

		public void LogRbr(LogSeverity pLogSeverity, string pLogMsg) {
		  logger.Log(pLogSeverity, new StackFrame(2).GetMethod().Name, pLogMsg);
		}

		public void LogRbr(LogSeverity pLogSeverity, string pFullMethodName, string pLogMsg) {
			logger.Log(pLogSeverity, pFullMethodName, pLogMsg);
		}

		static void writeToEventLog(StringBuilder pLogMessage, EventLogEntryType pEventLogEntryType) {
			try {
				EventLog.WriteEntry("Application", string.Format("\r\nTimokLogger.Ctor, \r\n{0}", pLogMessage), pEventLogEntryType, 1);
			}
			catch {}
		}
	}
}
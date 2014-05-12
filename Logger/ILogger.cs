namespace Timok.Logger {
	public delegate void LogDelegate(LogSeverity pLogSeverity, string pLocation, string pMessage);

	public interface ILogger {
		LogSeverity LogSeverity { get; set; }
		string LogFolder { get; }
		void AddEmailLogger(string pFolder, string pRbrServer, string pEmailServer, string pEmailFrom, string pEmailPassword, string pEmailTo, SetForSendingDelegate pSetForSendingDelegate);
		void LogEmail(string pSubject, string pBody);
		void LogRbr(LogSeverity pLogSeverity, string pLogMsg);
		void LogRbr(LogSeverity pLogSeverity, string pFullMethodName, string pLogMsg);
	}
}
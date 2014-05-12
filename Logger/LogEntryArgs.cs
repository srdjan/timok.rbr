

namespace Timok.Logger {

	public class LogEntryArgs {
		public LogSeverity Severity { get; private set; }
		public string Message { get; private set; }

		public LogEntryArgs(LogSeverity pSeverity, string pMessage) {
			Severity = pSeverity;
			Message = pMessage;
		}
	}
}
using System.Text;

namespace Timok.Logger {
	sealed public class TimokLogEntryFormatter : LogEntryFormatter {
		protected internal override string AsString(LogEntry pLogEntry) {
			var _logString = new StringBuilder();
			_logString.Append(string.Format("{0,-10} <{1}> ", string.Format("<{0}>", pLogEntry.SeverityString), DateString(pLogEntry)));

			if (pLogEntry.Application != null ) {
				_logString.Append(string.Format("<{0}>", pLogEntry.Application));
			}

			if (pLogEntry.Category != null ) {
				try {	_logString.Append(string.Format(" {0}", pLogEntry.Category)); } catch {	}
			}

			_logString.Append(string.Format(" {0}", pLogEntry.Message));
			return _logString.ToString();
		}

		public new string DateString(LogEntry pLogEntry) {
			return  pLogEntry.Date.ToString(FormatString);
		}
	}
}
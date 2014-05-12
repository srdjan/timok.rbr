using System;
using System.IO;

namespace RbrCommon {
	public static class Logger {
		const string  logFileFolder = @"c:\Timok\Rbr\Current\Logs";
		readonly static string logPath;

		static Logger() {
			//logPath = string.Format("{0}\\Logger_{1}_{2}_{3}.log", logFileFolder, DateTime.Now.Month, DateTime.Now.Day, DateTime.Now.Year);
			//if (!Directory.Exists(logFileFolder)) {
			//  Directory.CreateDirectory(logFileFolder);
			//}
			//Log("\r\n------------------------------------------------------------------------\r\n");
		}

		public static void Log(string pMessage) {
			//using (var _sw = File.AppendText(logPath)) {
			//  _sw.WriteLine("{0}\t{1}", DateTime.Now, pMessage);
			//}
		}

		//public void Log(string pSource, string pMessage, EventLogEntryType pEntryType) {
		//  try {
		//    if (!EventLog.SourceExists(pSource)) {
		//      EventLog.CreateEventSource(pSource, "Application");
		//    }

		//    EventLog.WriteEntry(pSource, pMessage, pEntryType);
		//  }
		//  catch (Exception _ex) {
		//    Log("Logger.Log", _ex.ToString(), EventLogEntryType.Error);
		//  }
		//}

		//---------------------------------- Private -----------------------------------------
	}
}
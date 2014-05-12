

namespace Timok.Logger {
	public static class LoggerFactory {
		static ILogger logger;
		public static ILogger Create(string pAppName, string pLogFolder) {
			if (logger == null) {
				logger = new TimokLogger(pAppName, pLogFolder);
			}
			return logger;
		} 
	}
}
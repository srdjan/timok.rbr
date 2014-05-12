using System;
using Timok.Logger;
using Timok.NetworkLib.Udp;

namespace Timok.Rbr.Core.LogManagement {
	public class SetLogSeverity : CommandBase {
		readonly ILogger logger;
		string mSeverityLevel;

		public SetLogSeverity(ILogger pLogger) : base() {
			logger = pLogger;
			nameAndVersion = LoggerAPI.SetLogSeverity;
		}

		/// <summary>Executes command and sends datagram back to client </summary>
		protected override void execute() {
			try {
				mSeverityLevel = parameters[0];
				logger.LogSeverity = (LogSeverity) Enum.Parse(typeof(LogSeverity), mSeverityLevel);
				AddResponseField("1", true);
			}
			catch (Exception _ex) {
				logger.LogRbr(LogSeverity.Critical, "SetLogSeverity.Execute", string.Format("Exception\r\n{0}", _ex));
				AddResponseField("0", true);
			}
		}
	}
}
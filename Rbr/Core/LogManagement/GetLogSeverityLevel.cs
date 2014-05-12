using System;
using Timok.Logger;
using Timok.NetworkLib.Udp;

namespace Timok.Rbr.Core.LogManagement {
	public class GetLogSeverity : CommandBase {
		readonly ILogger logger;
		public GetLogSeverity(ILogger pLogger) : base() {
			logger = pLogger;
			nameAndVersion = LoggerAPI.GetLogSeverity;
		}

		/// <summary>Executes command and sends datagram back to client </summary>
		protected override void execute() {
			try {
				AddResponseField("1", false);
				AddResponseField(logger.LogSeverity.ToString(), true);
			}
			catch (Exception _ex) {
				logger.LogRbr(LogSeverity.Critical, "GetLogSeverity.Execute:", string.Format("Exception\r\n{0}", _ex));
				AddResponseField("0", true);
			}
		}
	}
}
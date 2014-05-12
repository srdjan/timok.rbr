using Timok.Core;

namespace Timok.Rbr.Core.Config {
	public interface IEmailConfiguration {
		string Email { get; }
		string ClientEmailPassword { get; }
		string ClientEmailServer { get; }
		string ClientFrom { get; }
		string ClientTo { get; }
		string SupportEmailPassword { get; }
		string SupportEmailServer { get; }
		string SupportFrom { get; }
		string SupportTo { get; }
	}

	public class EmailConfiguration : IEmailConfiguration {
		public string Email { get { return "Email"; } }
		public string ClientEmailPassword { get; private set; }
		public string ClientEmailServer { get; private set; }
		public string ClientFrom { get; private set; }
		public string ClientTo { get; private set; }
		public string SupportEmailPassword { get; private set; }
		public string SupportEmailServer { get; private set; }
		public string SupportFrom { get; private set; }
		public string SupportTo { get; private set; }
		readonly IMainConfiguration main;

		public EmailConfiguration(IMainConfiguration pMain) {
			main = pMain;

			SupportTo = AppConfig.GetValue(main.RbrConfigFile, "emailSettings", "SupportTo");
			SupportFrom = AppConfig.GetValue(main.RbrConfigFile, "emailSettings", "SupportFrom");
			SupportEmailServer = AppConfig.GetValue(main.RbrConfigFile, "emailSettings", "SupportEmailServer");
			SupportEmailPassword = AppConfig.GetValue(main.RbrConfigFile, "emailSettings", "SupportEmailPassword");
			ClientTo = AppConfig.GetValue(main.RbrConfigFile, "emailSettings", "ClientTo");
			ClientFrom = AppConfig.GetValue(main.RbrConfigFile, "emailSettings", "ClientFrom");
			ClientEmailServer = AppConfig.GetValue(main.RbrConfigFile, "emailSettings", "ClientEmailServer");
			ClientEmailPassword = AppConfig.GetValue(main.RbrConfigFile, "emailSettings", "ClientEmailPassword");
		}
	}
}
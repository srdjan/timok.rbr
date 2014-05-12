using System;
using Timok.Rbr.Core.Config;

namespace Timok.Rbr.Core {
	[Serializable]
	public class ScriptInfo {
		public ScriptAuthenticationType AuthenticationType;
		public readonly int NumberOfSubSessions;
		public decimal PerUnit;
		public int PinLength;
		public BalancePromptType PromptType;
		public ScriptLanguage ScriptLanguage;
		public ScriptType ScriptType;
		string serviceName;

		public string RbrRoot { get; private set; }

		public string ServiceName {
			get { return serviceName; }
			set {
				serviceName = value.StartsWith("CU_S_") ? value.Substring(5) : value;
			}
		}

    //-- default constructor
		public ScriptInfo() {
			ServiceName = string.Empty;
			ScriptLanguage = ScriptLanguage.English;
			PinLength = 9;
			ScriptType = ScriptType.PhoneCard;
			AuthenticationType = ScriptAuthenticationType.Card;
			PromptType = BalancePromptType.Money;
			PerUnit = 0;
			NumberOfSubSessions = 1;
			RbrRoot = AppConstants.RbrRoot;
		}

		public override string ToString() {
			return
				string.Format("Name={0}, Language={1}, PinLength={2}, RetailType={3}, PromptType={4}, NumberOfSubSessions={5}, PerUnit={6}",
				              ServiceName,
				              ScriptLanguage,
				              PinLength,
				              ScriptType,
				              PromptType,
				              NumberOfSubSessions,
				              PerUnit);
		}
	}
}
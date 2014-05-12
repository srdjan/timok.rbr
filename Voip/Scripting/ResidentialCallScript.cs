using Timok.Logger;
using Timok.Rbr.Core;
using Timok.Rbr.Core.Config;

namespace Timok.IVR.Scripting {
	internal class ResidentialCallScript : ScriptBase {

		public ResidentialCallScript(ScriptInfo pScript, ISession pSession, CallStatistics pCallStatistics)
			: base(pScript, pSession, pCallStatistics) {
			Script.AuthenticationType = ScriptAuthenticationType.ANI;
		}

		protected override void ExecuteScript() {
			Play(promptManager.Balance(account.Balance), true, true, true);

			GetDestNumber();

			Authorize(ServiceType.Retail);

			Play(promptManager.TimeLimitPrompt(LegIn.PromptTimeLimit), true, true, true);

			MakeCall();
		}
	}
}
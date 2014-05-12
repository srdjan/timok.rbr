using Timok.Rbr.Core;
using Timok.Rbr.Core.Config;

namespace Timok.IVR.Scripting {
	internal class PhoneCardCallScript : ScriptBase {
		public PhoneCardCallScript(ScriptInfo pScript, ISession pSession, CallStatistics pCallStatistics) : base(pScript, pSession, pCallStatistics) {
			Script.AuthenticationType = ScriptAuthenticationType.Card;
			Script.PinLength += 3;	//NOTE: this is so that we pickup '#' terminator too, since standard prompt directs user to pres '#' after the PinNumber
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
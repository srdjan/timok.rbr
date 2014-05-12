using Timok.Rbr.Core;
using Timok.Rbr.Core.Config;

namespace Timok.IVR.Scripting {
	internal class LDCallScript : ScriptBase {
		public LDCallScript(ScriptInfo pScript, ISession pSession, CallStatistics pCallStatistics) : base(pScript, pSession, pCallStatistics) {
			Script.AuthenticationType = ScriptAuthenticationType.ANI;
		}

		protected override void ExecuteScript() {
			Authorize(ServiceType.Retail);
			MakeCall();
		}
	}
}
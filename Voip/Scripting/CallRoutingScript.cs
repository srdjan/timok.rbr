using Timok.Rbr.Core;
using Timok.Rbr.Core.Config;

namespace Timok.IVR.Scripting {
	internal class CallRoutingCallScript : ScriptBase {
		public CallRoutingCallScript(ScriptInfo pScript, ISession pSession, CallStatistics pCallStatistics) : base(pScript, pSession, pCallStatistics) {
			Script.AuthenticationType = ScriptAuthenticationType.IP;
		}

		protected override void ExecuteScript() {
			Authorize(ServiceType.Wholesale);
			MakeCall();
		}
	}
}
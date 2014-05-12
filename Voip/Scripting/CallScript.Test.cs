using Timok.Rbr.Core;
using T = Timok.Core.Logger.TimokLogger;

namespace Timok.IVR.Scripting {
	internal class TestCallScript : SessionScriptBase {
		public TestCallScript(RbrResult pInitResult, ISession pSession, ScriptInfo pScriptInfo) : base(pInitResult, pSession, pScriptInfo) {}

		protected override void run() {
			Play(promptManager.TimeLimitPrompt(600), false, true);

			MakeCall("2013335555", "192.168.1.8", EndPointProtocol.SIP); //EndPointProtocol.H323

			WaitForCallEnd(600);
		}
	}
}
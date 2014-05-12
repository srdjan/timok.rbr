using System;
using Timok.Core.Logging;
using Timok.Rbr.Core;
using T = Timok.Core.Logger.TimokLogger;

namespace Timok.IVR.Scripting {
	public class SessionScriptFactory {
		SessionScriptFactory() { }

		public static readonly SessionScriptFactory Instance = new SessionScriptFactory();

		public bool Run(ISessionChannel pChannel, string pOrigIPAddress, string pAccessNumber, string pANI) {
			ISessionScript _sessionScript = create(pChannel, pOrigIPAddress, pAccessNumber, pANI);
			if (_sessionScript == null) {
				return false;
			}

			_sessionScript.Run(true);
			return true;
		}

		//------------------------------------------------ Private --------------------------------------------------
		ISessionScript create(ISessionChannel pChannel, string pOrigIPAddress, string pAccessNumber, string pANI) {
			ISessionScript _sessionScript;

			try {
				ISession _session = new Session(pChannel, pOrigIPAddress, pAccessNumber, pANI);
				ScriptInfo _scriptInfo = new ScriptInfo();
				_sessionScript = new TestCallScript(RbrResult.Success, _session, _scriptInfo);
			}
			catch (Exception _ex) {
				T.LogRbr(LogSeverity.Critical, "SessionScriptFactory.create", string.Format("CallFlowFactory.Create, Exception:\r\n{0}", _ex));
				return null;
			}
			return _sessionScript;
		}
	}
}
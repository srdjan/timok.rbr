using System;
using System.Threading;
using Timok.Core.Logging;
using Timok.Rbr.Core;
using Timok.Rbr.Service;
using T = Timok.Core.Logger.TimokLogger;

namespace Timok.IVR.Scripting {
	public class SessionScriptFactory {
		readonly IRbrDispatcher rbr;

		SessionScriptFactory() {
			rbr = RbrDispatcher.Instance;
		}

		public static readonly SessionScriptFactory Instance = new SessionScriptFactory();

		public bool Run(ISessionChannel pChannel, string pOrigIPAddress, string pAccessNumber, string pANI) {
			ISessionScript _sessionScript = create(pChannel, pOrigIPAddress, pAccessNumber, pANI);
			if (_sessionScript == null) {
				return false;
			}

			if (_sessionScript.Welcome()) {
				for (int _i = 1; _i <= _sessionScript.NumberOfSubSessions; _i++) {
					if ( ! _sessionScript.Authenticate(_i)) {
						return false;
					}
					_sessionScript.Run(_i == _sessionScript.NumberOfSubSessions);
				}
			}
			return true;
		}

		//------------------------------------------------ Private --------------------------------------------------
		ISessionScript create(ISessionChannel pChannel, string pOrigIPAddress, string pAccessNumber, string pANI) {
			ISessionScript _sessionScript;

			try {
				ISession _session = new Session(pChannel, pOrigIPAddress, pAccessNumber, pANI);
				Thread.SetData(Thread.GetNamedDataSlot(Thread.CurrentThread.Name), _session.Id);

				ScriptInfo _scriptInfo;
				RbrResult _rc = rbr.InitRetailSession(_session, out _scriptInfo);
				if (_scriptInfo == null) {
					return null;
				}

				switch (_scriptInfo.ScriptType) {
					case ScriptType.PhoneCard: {
							_scriptInfo.PinLength += 1;	//NOTE: this is so that we pickup '#' terminator too, since standard prompt directs user to pres '#' after the PinNumber
							_sessionScript = new PhoneCardCallScript(_rc, _session, _scriptInfo);
						break;
					}
					case ScriptType.Residential: {
						_sessionScript = new ResidentialCallScript(_rc, _session, _scriptInfo);
						break;
					}
					case ScriptType.LD: {
						_sessionScript = new LDCallScript(_rc, _session, _scriptInfo);
						break;
					}
					case ScriptType.LadyGuadalupe: {
							_scriptInfo.PinLength += 1;	//NOTE: this is so that we pickup '#' terminator too, since standard prompt directs user to pres '#' after the PinNumber
						_sessionScript = new LadyGuadalupeCallScript(_rc, _session, _scriptInfo);
						break;
					}
					default: {
						T.LogRbr(LogSeverity.Critical, "SessionScript.create", string.Format("Error: Unknown RetailType {0}", _scriptInfo));
						return null;
					}
				}
			}
			catch (Exception _ex) {
				T.LogRbr(LogSeverity.Critical, "SessionScriptFactory.create", string.Format("CallFlowFactory.Create, Exception:\r\n{0}", _ex));
				return null;
			}

			return _sessionScript;
		}
	}
}
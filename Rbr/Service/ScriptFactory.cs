using System;
using Timok.Logger;
using Timok.Rbr.BLL.DOM;
using Timok.Rbr.Core;
using Timok.Rbr.Core.Config;

namespace Timok.Rbr.Service {
	public sealed class ScriptFactory {
		const string FILLER = "Wholesale";
		const string INIT_SESSION_LABEL = "ScriptFactory.Create";
		const string VALIDATE_ANI_LABEL = "ScriptFactory.validateANI";
		readonly CPSMeter globalCPS;
		
		public ScriptFactory() {
			globalCPS = new CPSMeter("Global", Configuration.Instance.Main.GlobalCPSLimit, TimokLogger.Instance.LogRbr);
		}

		public ScriptInfo Create(ISession pSession) {
			ScriptInfo _script = null;

			try {
				TimokLogger.Instance.LogRbr(LogSeverity.Status, INIT_SESSION_LABEL, string.Format("Request: ANI={0}, IP={1}, AccessNumber={2}, DestNumber={3}", pSession.ANI, pSession.OrigIPAddress, pSession.AccessNumber, pSession.DestNumber));
				globalCPS.TakeSample();

				var _origEP = Endpoint.Get(pSession.OrigIPAddress);
				if (_origEP == null) {
					TimokLogger.Instance.LogRbr(LogSeverity.Error, INIT_SESSION_LABEL, string.Format("Endpoint NOT FOUND, {0}", pSession.OrigIPAddress));
					return _script;
				}
				_origEP.TakeSample();

				if (Configuration.Instance.Main.ANIRequired) {	//NOTE: this should be CustomerAcct property
					TimokLogger.Instance.LogRbr(LogSeverity.Debug, INIT_SESSION_LABEL, "Validating ANI");
					validateANI(pSession.ANI);
				}

				var _retailService = RetailService.Get(pSession.AccessNumber);
				if (_retailService == null) {
					TimokLogger.Instance.LogRbr(LogSeverity.Debug, INIT_SESSION_LABEL, "Get Wholesale Script");
					_script = getWholesaleScript();
				}
				else {
					TimokLogger.Instance.LogRbr(LogSeverity.Debug, INIT_SESSION_LABEL, "Get CustomerAcctId");
					pSession.CustomerAcctId = CustomerAcct.GetCustomerAcctId(pSession.AccessNumber);
					_script = _retailService.GetRetailScript();
				}
			}
			catch (RbrException _rbrex) {
				TimokLogger.Instance.LogRbr(LogSeverity.Error, INIT_SESSION_LABEL, string.Format("RbrException, from: {0}\r\n{1}", _rbrex.Source, _rbrex.Message));
			}
			catch (Exception _ex) {
				TimokLogger.Instance.LogRbr(LogSeverity.Critical, INIT_SESSION_LABEL, string.Format("Exception:\r\n{0}", _ex));
			}
			finally {
				TimokLogger.Instance.LogRbr(LogSeverity.Status, INIT_SESSION_LABEL, string.Format("Response: ScriptInfo={0}", _script == null ? "null" : _script.ToString()));
			}
			return _script;
		}

		//--------------------------- private helpers -------------------------------------------
		static ScriptInfo getWholesaleScript() {
			var _scriptInfo = new ScriptInfo
			{
				ServiceName = FILLER,
				PinLength = 0,
				PromptType = BalancePromptType.None,
				PerUnit = 0,
				ScriptLanguage = ScriptLanguage.English,
				ScriptType = ScriptType.Wholesale
			};
			return _scriptInfo;
		}

		static void validateANI(string pANI) {
			long _ani;
			long.TryParse(pANI, out _ani);
			if (_ani == 0) {
				throw new RbrException(RbrResult.ANI_Invalid, VALIDATE_ANI_LABEL, string.Format("ANI={0}", pANI));
			}
		}
	}
}
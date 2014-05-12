using System;
using Timok.Rbr.Core;
using T = Timok.Core.Logging.TimokLogger;

namespace Timok.Rbr.Service {
	public sealed class RbrStub : IRbrDispatcher {
		public string LogFolder { get { return T.LogFolder; } }

		/// ----------------------------------------------------------------------------------------
		///                                    RETAIL                                                  
		/// ----------------------------------------------------------------------------------------
		public RbrResult InitRetailSession(ISession pSession, out ScriptInfo pScriptInfo) {
			T.LogDebug(string.Format(">> {0}, ANI={1}, OrigIP={2}, AccessNumber={3}", pSession.Id, pSession.ANI, pSession.OrigIPAddress, pSession.AccessNumber));

			//-- create default ScriptInfo:
			pScriptInfo = new ScriptInfo();
			pScriptInfo.RbrRoot = AppConstants.RbrRoot;
			pScriptInfo.ScriptLanguage = ScriptLanguage.English;
			pScriptInfo.PinLength = 9;
			pScriptInfo.ScriptType = ScriptType.PhoneCard;

			T.LogDebug(string.Format("<< ScriptInfo={0}", pScriptInfo));

			return RbrResult.Success;
		}

		public RbrResult AuthenticateRetail(ISession pSession,
																													out int pRetialAcctId,
																													out short pCustomerAcctId,
																													out long pSerialNumber,
																													out decimal pStartingBalance,
																													out decimal pBalance,
																													out int pBonusMinutes, 
																													out bool pNeverUsed) {
			pRetialAcctId = 1000;
			pCustomerAcctId = 1000;
			pSerialNumber = 11110;
			pStartingBalance = pBalance = new Decimal(10.0);
			pBonusMinutes = 0;
			pNeverUsed = false;

			return RbrResult.Success;
		}

		public void SetRetailAcctNotInUse(ISession pSession) {
			T.LogDebug(string.Format(">> {0}, ANI={1}, DNIS={2}, Card_number={3}", pSession.Id, pSession.ANI, pSession.AccessNumber, pSession.CardNumber));
		}


		public RbrResult AuthorizeRetail(ISession pSession, 
																												out string pOutDestNumber, 
																												out string pCalledIpAndPort, 
																												out short pCustomerAcctId, 
																												out short pCarrierAcctId, 
																												out int pCustomerRouteId, 
																												out int pCarrierRouteId, 
																												out int pTimeLimit, 
																												out int pPromptTimeLimit) {
			pOutDestNumber = pSession.DestNumber; //it may be cleared of inbound prefix and overwritten by outbound Prefix, IntlDialCode
			pCalledIpAndPort = AppConstants.TestTermIP;
			pCustomerAcctId = 1000;
			pCarrierAcctId = 1001;
			pCustomerRouteId = 1234;
			pCarrierRouteId = 4321;
			pTimeLimit = 30;
			pPromptTimeLimit = 30;

			T.LogDebug(string.Format(">> {0}, OrigIP={1}, ANI={2}, AccessNumber={3}, Card_number={4}, DestNumber={5}", pSession.Id, pSession.OrigIPAddress, pSession.ANI, pSession.AccessNumber, pSession.CardNumber, pSession.DestNumber));

			return RbrResult.Success;
		}

		/// <summary> 
		/// ----------------------------------------------------------------------------------------
		/// </summary>
		public void CallConnect(string pCallId, string pAccessNumber, string pOrigIP, string pTermIP, short pCustomerAcctId, int pCustomerRouteId, short pCarrierAcctId, int pCarrierRouteId, int pLeg1Seconds) {
			T.LogDebug(string.Format(">> {0}, AccessNumber={1}, OrigIP={2}, TermIP={3}, CustId={4}, CustRouteId={5}, CarrierId={6}, CarrierRouteId={7}, Leg1Secs={8}", 
				/**/												pCallId,	
				/**/												pAccessNumber, 
				/**/												pOrigIP, 
				/**/												pTermIP, 
				/**/												pCustomerAcctId, 
				/**/												pCustomerRouteId, 
				/**/												pCarrierAcctId, 
				/**/												pCarrierRouteId, 
				/**/												pLeg1Seconds));
		}

		/// <summary> 
		/// ----------------------------------------------------------------------------------------
		/// </summary>
		public bool CallComplete(string pCallId, string pCdr) {
			T.LogDebug(string.Format(">> {0}, Cdr={1}", pCallId, pCdr));
			return true;
		}

		public void CacheCallingIP(string pCallId, string pDestNumber, string pCallingIp) { }

		//-- On data changed Externaly
		public void RbrSync(IChangedObject[] pChangedObjects) {
			T.LogDebug("NumberOfArgs: " + pChangedObjects.Length);
			//foreach (ChangedObject _changedObject in pChangedObjects) {
			//  Repository.Imdb.Invalidate(_changedObject.TxType, _changedObject.Argument);
			//}
		}

		public long GetNextSequence() {
			throw new NotImplementedException();
		}

		//--- Call Statistics
		public CallStats GetCallStats() {
			return null; // CallStatistics.Instance.GetTotalCallStats();
		}

		public CallStats GetCustomerCallStats(short pCustomerAcctId) {
			return null; // CallStatistics.Instance.GetCustomerCallStats(pCustomerAcctId);
		}

		public CallStats GetCarrierCallStats(short pCarrierAcctId) {
			return null; // CallStatistics.Instance.GetCarrierCallStats(pCarrierAcctId);
		}

		public RbrResult RegisterH323(string pCallId, string pIp, string pAlias) {
			return RbrResult.Success;
		}

		public RbrResult UnregisterH323(string pCallId, string pIp, string pAlias) {
			return RbrResult.Success;
		}

		public RbrResult AuthenticateSIP(string pCallId, string pCallingIp, string pUserId, out string pPassword) {
			pPassword = String.Empty;
			return RbrResult.Success;
		}

		public RbrResult RegisterSIP(string pCallId, string pUserId) {
			//NOTE: no need to send back passwd, done in Auth, out string pPassword) {
			return RbrResult.Success;
		}

		public RbrResult AuthorizeWholesale(string pCallId, byte pAttempt, string pOrigIP, string pAlias, string pANI, string pDestNumber, 
			/**/																																					out string pOutANI, 
			/**/																																					out string pOutDestNumber, 
			/**/																																					out string pBillingIpAndPort, 
			/**/																																					out string pCalledIpAndPort, 
			/**/																																					out int pCalledType, 
			/**/																																					out string pProtocol, 
			/**/																																					out short pCustomerAcctId, 
			/**/																																					out short pCarrierAcctId, 
			/**/																																					out int pCustomerRouteId, 
			/**/																																					out int pCarrierRouteId, 
			/**/																																					out int pTimeLimit) {
			pOutANI = pANI; //it may be overwritten by outbound ANI in GetTermination()
			pOutDestNumber = pDestNumber; //it may be cleared of inbound prefix and overwritten by outbound Prefix, IntlDialCode
			pBillingIpAndPort = String.Empty;
			pCalledIpAndPort = String.Empty;
			pCalledType = 0;
			pProtocol = string.Empty;
			pCustomerAcctId = 0;
			pCarrierAcctId = 0;
			pCustomerRouteId = 0;
			pCarrierRouteId = 0;
			pTimeLimit = 0;
			return RbrResult.Success;
		}
	}
}

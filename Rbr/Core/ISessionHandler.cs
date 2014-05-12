using System.Collections.Generic;
using Timok.Rbr.Core.Config;

namespace Timok.Rbr.Core {
	public interface ISessionHandler {
		//-- Retail
		//--------------------------------------------------------------------------------------------
		RbrResult AuthenticateRetail(ISession pSession, out Account pAccount);
		RbrResult AuthorizeRetail(ISession pSession, out LegIn pLegIn, out List<LegOut> pLegOutOptions);
		void SetRetailAcctNotInUse(ISession pSession);

		//-- Wholesale
		//--------------------------------------------------------------------------------------------
		RbrResult AuthorizeWholesale(string pCallId, string pCallingIp, string pCallingAlias, string pANI, string pDestNumber,
																 out LegIn pLegIn,
																 out List<LegOut> pLegOutOptions);

		//-- All
		//--------------------------------------------------------------------------------------------
		bool CallConnect(string pCallId, string pAccessNumber, string pOrigIP, string pTermIP, short pCustomerId, int pCustomerRouteId, short pCarrierId, int pCarrierRouteId, int pLeg1Seconds);
		bool CallComplete(string pGuid, string pCallId, string pCdr);
		CallState OnConnected(short pCarrierAcctId, short pCustomerAcctId);
		void OnCompleted(CallState pState, short pCarrierAcctId, short pCustomerAcctId);
		CallState OnStarted();
		CallState OnConnecting(LegIn pLegIn, LegOut pLegOut);
	}
}

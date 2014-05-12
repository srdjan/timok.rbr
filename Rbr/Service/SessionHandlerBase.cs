using System;
using System.Collections.Generic;
using Timok.Logger;
using Timok.NetworkLib;
using Timok.Rbr.BLL.DOM;
using Timok.Rbr.BLL.Entities;
using Timok.Rbr.Core;
using Timok.Rbr.Core.Config;

namespace Timok.Rbr.Service {
	public abstract class SessionHandlerBase : ISessionHandler {
		protected readonly IConfiguration configuration;
		readonly CallStatistics callStatistics;
		protected readonly ILogger logger;
		protected RoutingService routingService;
		protected CdrAggrExporter cdrAggrExporter; 
		const string CALLCONNECT_LABEL = "SessionFlowHandler.CallConnect";
		const string CALL_COMPLETE_LABEL = "SessionFlowHandler.CallComplete";

		protected SessionHandlerBase(CallStatistics pCallStatistics) {
			callStatistics = pCallStatistics;
			routingService = new RoutingService();
		}

		public abstract RbrResult AuthenticateRetail(ISession pSession, out Account pAccount);
		public abstract RbrResult AuthorizeRetail(ISession pSession, out LegIn pLegIn, out List<LegOut> pLegOutOptions);
		public abstract void SetRetailAcctNotInUse(ISession pSession);
		public abstract RbrResult AuthorizeWholesale(string pCallId, string pOrigIP, string pAlias, string pANI, string pDestNumber, out LegIn pLegIn, out List<LegOut> pLegOutOptions);

		/// <summary> 
		/// ----------------------------------------------------------------------------------------
		/// </summary>
		public bool CallConnect(string pCallId, string pAccessNumber, string pOrigIP, string pTermIP, short pCustomerAcctId, int pCustomerBaseRouteId, short pCarrierAcctId, int pCarrierBaseRouteId, int pLeg1Seconds) {
			try {
				var _currentNode = new CurrentNode();
				if (_currentNode.IsSIP) {
					logger.LogRbr(LogSeverity.Status, CALLCONNECT_LABEL, string.Format("Request: AccessNumber={0}, IP={1}, TermIP={2}, CustId={3}, CustBaseRouteId={4}, CarrierId={5}, CarrierBaseRouteId={6}, Leg1Secs={7}",
						/**/												pAccessNumber,
						/**/												pOrigIP,
						/**/												pTermIP,
						/**/												pCustomerAcctId,
						/**/												pCustomerBaseRouteId,
						/**/												pCarrierAcctId,
						/**/												pCarrierBaseRouteId,
						/**/												pLeg1Seconds));
				}

				//-- change call status to connected if Call type CallCenter2
				cdrAggrExporter.OnCallConnect(pAccessNumber, pCarrierAcctId, pCarrierBaseRouteId, pCustomerAcctId, pCustomerBaseRouteId, pLeg1Seconds, pOrigIP, pTermIP);

				if (_currentNode.IsSIP) {
					logger.LogRbr(LogSeverity.Status, CALLCONNECT_LABEL, string.Format("Response: {0}", true));
				}
			}
			catch (RbrException _rbrex) {
				logger.LogRbr(LogSeverity.Error, CALLCONNECT_LABEL, string.Format("RbrException, from: {0}:\r\n{1}", _rbrex.Source, _rbrex.Message));
			}
			catch (Exception _ex) {
				logger.LogRbr(LogSeverity.Critical, CALLCONNECT_LABEL, string.Format("Exception:\r\n{0}", _ex));
			}
			return true;
		}

		/// <summary> 
		/// ----------------------------------------------------------------------------------------
		/// </summary>
		public bool CallComplete(string pGuid, string pCallId, string pCdr) {
			var _result = 0;
			Cdr _cdr = null;

			try {
				var _currentNode = new CurrentNode();

				if (_currentNode.IsSIP) {
					logger.LogRbr(LogSeverity.Status, CALL_COMPLETE_LABEL, string.Format("Request: Cdr={0}|{1}", pCallId, pCdr));
				}

				_cdr = new Cdr(pGuid, pCdr, false);

				var _billingService = new BillingService(configuration, logger);
				_result = _billingService.ProcessCallComplete(_cdr);
				
				_result += _cdr.Insert();

				if (_currentNode.IsSIP) {
					logger.LogRbr(LogSeverity.Status, CALL_COMPLETE_LABEL, string.Format("Response: {0}", _result == 0));
				}
			}
			catch (RbrException _rbrex) {
				logger.LogRbr(LogSeverity.Error, CALL_COMPLETE_LABEL, string.Format("RbrException, from: {0}, {1}", _rbrex.Source, _rbrex.Message));
			}
			catch (Exception _ex) {
				logger.LogRbr(LogSeverity.Critical, CALL_COMPLETE_LABEL, string.Format("Exception:\r\n{0}", _ex));
			}
			finally {
				if (_cdr != null) {
					cdrAggrExporter.OnCallComplete(_cdr);
				}
			}
			return _result == 0;
		}

		//TODO unify with CallConnect()
		public CallState OnConnected(short pCarrierAcctId, short pCustomerAcctId) {
			return callStatistics.OnConnected(pCarrierAcctId, pCustomerAcctId);
		}

		public void OnCompleted(CallState pState, short pCarrierAcctId, short pCustomerAcctId) {
			callStatistics.OnCompleted(pState, pCarrierAcctId, pCustomerAcctId);
		}

		public CallState OnStarted() {
			return callStatistics.OnStarted();
		}

		public CallState OnConnecting(LegIn pLegIn, LegOut pLegOut) {
			cdrAggrExporter.OnCallSetup(string.Empty, pLegOut.CarrierAcctId,
														pLegOut.CarrierBaseRouteId,
														pLegIn.CustomerAcctId,
														pLegIn.CustomerRouteId,
														IPUtil.ExtractIPAddress(pLegIn.IPAndPort),
														IPUtil.ExtractIPAddress(pLegOut.DestIPAndPort));

      return callStatistics.OnConnecting(pLegIn, pLegOut);
		}
	}
}
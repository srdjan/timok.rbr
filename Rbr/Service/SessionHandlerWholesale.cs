using System;
using System.Collections.Generic;
using Timok.Logger;
using Timok.NetworkLib;
using Timok.Rbr.BLL.DOM;
using Timok.Rbr.Core;
using Timok.Rbr.Core.Config;

namespace Timok.Rbr.Service {
	public class SessionHandlerWholesale : SessionHandlerBase {
		const string AUTHORIZE_WHOLESALE_LABEL = "SessionFlowHandler.AuthorizeWholesale";

		public SessionHandlerWholesale(CallStatistics pCallStatistics) : base(pCallStatistics) {}
		public override RbrResult AuthenticateRetail(ISession pSession, out Account pAccount) {
			throw new NotImplementedException();
		}

		public override RbrResult AuthorizeRetail(ISession pSession, out LegIn pLegIn, out List<LegOut> pLegOutOptions) {
			throw new NotImplementedException();
		}

		public override void SetRetailAcctNotInUse(ISession pSession) {
			throw new NotImplementedException();
		}

		public override RbrResult AuthorizeWholesale(string pCallId,
																				string pOrigIP,
																				string pAlias,
																				string pANI,
																				string pDestNumber,
																				out LegIn pLegIn,
																				out List<LegOut> pLegOutOptions) {
			pLegIn = new LegIn
			{
				ANI = pANI,
				CustomerAcctId = 0,
				CustomerRouteId = 0,
				IP = pOrigIP,
				IPAndPort = String.Empty,
				PromptTimeLimit = 0
			};

			pLegOutOptions = null;
			var _outDestNumber = pDestNumber; //it may be cleared of inbound prefix and overwritten by outbound Prefix, IntlDialCode

			try {
				//-- Resolve OrigEndPoint and strip off origPrefix intl -dialcode:
				var _origEP = Endpoint.Get(pOrigIP);
				if (_origEP == null) {
					throw new RbrException(RbrResult.OrigEP_NotFound, AUTHORIZE_WHOLESALE_LABEL, string.Format("OrigEP IP={0}", pOrigIP));
				}
				_origEP.TakeSample();
				pLegIn.IPAndPort = string.Format("{0}:{1}", _origEP.IPAddress, _origEP.Port);
				if (_origEP.Status != Status.Active) {
					throw new RbrException(RbrResult.OrigEP_NotActive, AUTHORIZE_WHOLESALE_LABEL, string.Format("OrigEP IP={0}", _origEP.IPAddress));
				}

				//-- Get inbound prefix and clean destNumber
				var _prefixIn = _origEP.GetPrefixIn(_outDestNumber);
				_outDestNumber = routingService.CleanDestNumber(_prefixIn, _outDestNumber);

				//-- First try to get CustomerAcct:
				var _customerAcct = CustomerAcct.Get(_origEP, _prefixIn);
				if (_customerAcct == null) {
					throw new RbrException(RbrResult.Customer_NotFound, AUTHORIZE_WHOLESALE_LABEL, string.Format("CustomerAcct NOT FOUND, OrigEPId={0}, Prefix={1}", _origEP.Id, _prefixIn));
				}
				pLegIn.CustomerAcctId = _customerAcct.Id;

				//-- Limit CustomerAcct number of Calls
				if (CustomerAcct.NumberOfCallsCounter.ContainsKey(_customerAcct.Id)) {
					if (CustomerAcct.NumberOfCallsCounter[_customerAcct.Id] > _customerAcct.MaxNumberOfCalls) {
						throw new RbrException(RbrResult.Customer_MaxCallsReached, AUTHORIZE_WHOLESALE_LABEL, string.Format("CustomerAcct ID={0}, MaxCalls={1}", _customerAcct.Id, CustomerAcct.NumberOfCallsCounter[_customerAcct.Id]));
					}
				}
				else {
					CustomerAcct.NumberOfCallsCounter.Add(_customerAcct.Id, 0);
				}
				CustomerAcct.NumberOfCallsCounter[_customerAcct.Id] += 1;

				//-- Get and validate customer route:
				var _customerRoute = CustomerRoute.Get(_customerAcct.ServiceId, _customerAcct.CallingPlanId, _customerAcct.RoutingPlanId, _outDestNumber);
				pLegIn.CustomerRouteId = _customerRoute.BaseRouteId;
				if (_customerRoute.Status != Status.Active) {
					throw new RbrException(RbrResult.Customer_RouteBlocked, AUTHORIZE_WHOLESALE_LABEL, string.Format("Customer Route BLOCKED, RouteId={0}", _customerRoute));
				}

				//-- Authorize CustomerAcct and get TimeLimit (if PrepaidEnabled):
				var _timeLimit = _customerAcct.Authorize(_customerRoute);

				//-- Get best termination choice:
				pLegOutOptions = routingService.GetTerminationByDestination(_customerAcct, _customerRoute, _outDestNumber, _timeLimit);

				//TODO: Move up to IVR for additional LCR calls!!
				cdrAggrExporter.OnCallSetup(string.Empty,
																		pLegOutOptions[0].CarrierAcctId,
																		pLegOutOptions[0].CarrierBaseRouteId,
																		_customerAcct.Id,
																		pLegIn.CustomerRouteId,
																		_origEP.IPAddress,
																		IPUtil.ExtractIPAddress(pLegOutOptions[0].DestIPAndPort));
			}
			catch (RbrException _rbrex) {
				logger.LogRbr(LogSeverity.Error, AUTHORIZE_WHOLESALE_LABEL, string.Format("RbrException, from: {0}\r\n{1}", _rbrex.Source, _rbrex.Message));
				return _rbrex.RbrResult;
			}
			catch (Exception _ex) {
				logger.LogRbr(LogSeverity.Critical, AUTHORIZE_WHOLESALE_LABEL, string.Format("Exception:\r\n{0}", _ex));
				return RbrResult.ExceptionThrown;
			}
			return RbrResult.Success;
		}
	}
}
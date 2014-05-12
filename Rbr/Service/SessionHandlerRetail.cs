using System;
using System.Collections.Generic;
using Timok.Logger;
using Timok.NetworkLib;
using Timok.Rbr.BLL.DOM;
using Timok.Rbr.Core;
using Timok.Rbr.Core.Config;

namespace Timok.Rbr.Service {
	public sealed class SessionHandlerRetail : SessionHandlerBase {
		const string AUTHENTICATE_RETAIL_LABEL = "SessionHandlerRetail.AuthenticateRetail";
		const string AUTHORIZE_RETAIL_LABEL = "SessionHandlerRetail.Authorize";
		const string SET_RETAIL_ACCT_NOT_IN_USE_LABEL = "SessionHandlerRetail.SetRetailAcctNotInUse";

		public SessionHandlerRetail(CallStatistics pCallStatistics) : base(pCallStatistics) {}

		public override RbrResult AuthenticateRetail(ISession pSession, out Account pAccount) {
			pAccount = new Account();

			try {
				logger.LogRbr(LogSeverity.Status, AUTHENTICATE_RETAIL_LABEL, string.Format("Request: ANI={0}, AccessNumber={1}, CardNumber={2}", pSession.ANI, pSession.AccessNumber, pSession.CardNumber));

				var _retailService = RetailService.Get(pSession.AccessNumber);

				var _retailAcct = _retailService.GetRetailAccount(pSession);
				pAccount.RetialAcctId = _retailAcct.Id;
				pAccount.SerialNumber = _retailAcct.SerialNumber;
				pAccount.StartingBalance = _retailAcct.StartingBalance;
				pAccount.Balance = _retailAcct.CurrentBalance;
				pAccount.BonusMinutes = _retailAcct.CurrentBonusBalance;
				pAccount.NeverUsed = _retailAcct.NeverUsed;
				pAccount.Obtained = true;

				var _minimumBalance = decimal.Zero;
				if (pSession.InfoDigits > 0) {
					_minimumBalance += _retailService.PayphoneSurchargeInfo.Cost;
				}
				_minimumBalance += _retailService.AccessNumberSurchargeInfo.Cost;

				if (pAccount.Balance == 0 || pAccount.Balance < _minimumBalance) {
					if (pAccount.BonusMinutes > 0) {
						throw new NotImplementedException("Bonus Minutes NOT implemented!");
					}
					throw new RbrException(RbrResult.Retail_NoBalance, AUTHENTICATE_RETAIL_LABEL, string.Format("Balance less Then MinimumBalance! ANI={0}, Card_number={1}", pSession.ANI, pSession.CardNumber));
				}

				if (_retailAcct.AcctStatus != Status.Active) {
					throw new RbrException(RbrResult.Retail_AcctNotActive, AUTHENTICATE_RETAIL_LABEL, string.Format("Status NOT ACTIVE! ANI={0}, Card_number={1}, Status={2}", pSession.ANI, pSession.CardNumber, _retailAcct.AcctStatus));
				}

				if (_retailAcct.DateToExpire <= DateTime.Today) {
					throw new RbrException(RbrResult.Retail_AcctExpired, AUTHENTICATE_RETAIL_LABEL, string.Format("Status NOT ACTIVE! ANI={0}, Card_number={1}, Status={2}", pSession.ANI, pSession.CardNumber, _retailAcct.AcctStatus));
				}

				logger.LogRbr(LogSeverity.Status, AUTHENTICATE_RETAIL_LABEL, string.Format("Response: RetailAcctId={0}, CustomerAcctId={1}, SerialNumber={2}, Balance={3}, BonusMinutes={4}", pAccount.RetialAcctId, _retailAcct.CustomerAcctId, pAccount.SerialNumber, pAccount.Balance, pAccount.BonusMinutes));
				_retailAcct.AcctStatus = Status.InUse;
			}
			catch (RbrException _rbrex) {
				logger.LogRbr(LogSeverity.Error, AUTHENTICATE_RETAIL_LABEL, string.Format("RbrException, from: {0}\r\n{1}", _rbrex.Source, _rbrex.Message));
				return _rbrex.RbrResult;
			}
			catch (Exception _ex) {
				logger.LogRbr(LogSeverity.Critical, AUTHENTICATE_RETAIL_LABEL, string.Format("Exception:\r\n{0}", _ex));
				return RbrResult.ExceptionThrown;
			}
			return RbrResult.Success;
		}

		public override RbrResult AuthorizeRetail(ISession pSession, out LegIn pLegIn, out List<LegOut> pLegOutOptions) {
			pLegIn = new LegIn
			{
				ANI = pSession.ANI,
				IP = pSession.OrigIPAddress,
				IPAndPort = string.Empty,
				CustomerAcctId = 0,
				CustomerRouteId = 0,
				PromptTimeLimit = 0
			};

			pLegOutOptions = null;

			try {
				logger.LogRbr(LogSeverity.Status, AUTHORIZE_RETAIL_LABEL, string.Format("Request: IP={0}, ANI={1}, AccessNumber={2}, CardNumber={3}, DestNumber={4}", pSession.OrigIPAddress, pSession.ANI, pSession.AccessNumber, pSession.CardNumber, pSession.DestNumber));

				var _retailService = RetailService.Get(pSession.AccessNumber);
				var _retailAcct = _retailService.GetRetailAccount(pSession);

				var _customerAcct = CustomerAcct.Get(_retailAcct.CustomerAcctId);

				if (CustomerAcct.NumberOfCallsCounter.ContainsKey(_customerAcct.Id)) {
					if (CustomerAcct.NumberOfCallsCounter[_customerAcct.Id] > _customerAcct.MaxNumberOfCalls) {
						throw new RbrException(RbrResult.Customer_MaxCallsReached, AUTHORIZE_RETAIL_LABEL, string.Format("CustomerAcct ID={0}, MaxCalls={1}", _customerAcct.Id, CustomerAcct.NumberOfCallsCounter[_customerAcct.Id]));
					}
				}
				else {
					CustomerAcct.NumberOfCallsCounter.Add(_customerAcct.Id, 0);
				}
				CustomerAcct.NumberOfCallsCounter[_customerAcct.Id] += 1;

				var _destNumber = routingService.CleanDestNumber(string.Empty, pSession.DestNumber);
				var _customerRoute = CustomerRoute.Get(_customerAcct.ServiceId, _customerAcct.CallingPlanId, _customerAcct.RoutingPlanId, _destNumber);
				pLegIn.CustomerRouteId = _customerRoute.BaseRouteId;

				//-- Authorize Wholesale Account
				var _wholesaleTimeLimit = _customerAcct.Authorize(_customerRoute);

				//-- Authorize Retail Account
				int _timeLimit;
				_retailService.Authorize(pSession, _retailAcct, _customerRoute, out _timeLimit, out pLegIn.PromptTimeLimit);
				if (_timeLimit > _wholesaleTimeLimit) {
					logger.LogRbr(LogSeverity.Status, AUTHORIZE_RETAIL_LABEL, string.Format("Customer MaxCallTime REACHED, CustomerAcctId={0}, TimeLimit={1}", _customerAcct.Id, _wholesaleTimeLimit));
					_timeLimit = _wholesaleTimeLimit;
				}
				if (_timeLimit == 0) {
					throw new RbrException(RbrResult.Retail_NoBalanceForRoute, AUTHORIZE_RETAIL_LABEL, string.Format("NOT Enough Balance for Route, CustomerAcctId={0}, (W) RouteId={1}", _customerAcct.Id, _customerRoute.WholesaleRouteId));
				}
				//-- propmpt multiplier
				if (_customerRoute.Multiplier > 0) {
					pLegIn.PromptTimeLimit = (_timeLimit * _customerRoute.Multiplier) / 100;
				}
				else {
					pLegIn.PromptTimeLimit = _timeLimit;
				}

				pLegOutOptions = routingService.GetTerminationByDestination(_customerAcct, _customerRoute, _destNumber, _timeLimit);

				_retailAcct.UpdateUsage();

				//TODO: Move up to IVR for additional LCR calls!!
				cdrAggrExporter.OnCallSetup(pSession.AccessNumber, pLegOutOptions[0].CarrierAcctId, pLegOutOptions[0].CarrierBaseRouteId, _customerAcct.Id, pLegIn.CustomerRouteId, pSession.OrigIPAddress, IPUtil.ExtractIPAddress(pLegOutOptions[0].DestIPAndPort));

				logger.LogRbr(LogSeverity.Status, AUTHORIZE_RETAIL_LABEL, string.Format("Response: Dest#={0}, CalledIP={1}, CustId={2}, CarrierId={3}, CustRouteId={4}, CarrierRouteId={5}, TimeLimit={6}, PTimeLimit={7}",
					/**/																																		pLegOutOptions[0].DestNumber,
					/**/																																		pLegOutOptions[0].DestIPAndPort,
					/**/																																		_customerAcct.Id,
					/**/																																		pLegOutOptions[0].CarrierAcctId,
					/**/																																		_customerRoute.BaseRouteId,
					/**/																																		pLegOutOptions[0].CarrierBaseRouteId,
					/**/																																		pLegOutOptions[0].TimeLimit,
					/**/																																		pLegIn.PromptTimeLimit));
			}
			catch (RbrException _rbrex) {
				logger.LogRbr(LogSeverity.Error, AUTHORIZE_RETAIL_LABEL, string.Format("RbrException, from: {0}\r\n{1}", _rbrex.Source, _rbrex.Message));
				return _rbrex.RbrResult;
			}
			catch (Exception _ex) {
				logger.LogRbr(LogSeverity.Critical, AUTHORIZE_RETAIL_LABEL, string.Format("Exception:\r\n{0}", _ex));
				return RbrResult.ExceptionThrown;
			}
			return RbrResult.Success;
		}

		//-- NOTE: to be used only at the end of call!!
		//-- Status.InUse set in AuthenticateRetail (above)
		public override void SetRetailAcctNotInUse(ISession pSession) {
			try {
				logger.LogRbr(LogSeverity.Status, SET_RETAIL_ACCT_NOT_IN_USE_LABEL, string.Format("Request: ANI={0}, DNIS={1}, Card_number={2}", pSession.ANI, pSession.AccessNumber, pSession.CardNumber));

				var _retailService = RetailService.Get(pSession.AccessNumber);

				var _retailAcct = _retailService.GetRetailAccount(pSession);
				if (_retailAcct.AcctStatus != Status.InUse) {
					throw new Exception(string.Format("Unexpected RetailAcct Status={0}, ANI={1}, CardNumber={2}", _retailAcct.AcctStatus, pSession.ANI, pSession.CardNumber));
				}
				_retailAcct.AcctStatus = Status.Active;
			}
			catch (RbrException _rbrex) {
				logger.LogRbr(LogSeverity.Critical, SET_RETAIL_ACCT_NOT_IN_USE_LABEL, string.Format("RbrException, from: {0}\r\n{1}", _rbrex.Source, _rbrex.Message));
			}
			catch (Exception _ex) {
				logger.LogRbr(LogSeverity.Critical, SET_RETAIL_ACCT_NOT_IN_USE_LABEL, string.Format("Exception:\r\n{0}", _ex));
			}
		}

		public override RbrResult AuthorizeWholesale(string pCallId, string pOrigIP, string pAlias, string pANI, string pDestNumber, out LegIn pLegIn, out List<LegOut> pLegOutOptions) {
			throw new NotImplementedException();
		}
	}
}
using System;
using Timok.Logger;
using Timok.Rbr.Core;
using Timok.Rbr.Core.Config;
using Timok.Rbr.DAL.RbrDatabase;
using Timok.Rbr.DOM;

namespace Timok.Rbr.BLL.DOM {
	public sealed class CustomerRoute : Route {
		readonly WholesaleRouteRow wholesaleRouteRow;
		public int WholesaleRouteId { get { return wholesaleRouteRow.Wholesale_route_id; } }
		public short ServiceId { get { return wholesaleRouteRow.Service_id; } }
		public short Multiplier { get { return 0; } } //TODO: move to RetailRoute: wholesaleRouteRow.Multiplier; } }
		public bool WithBonusMinutes { get { return false; } } //TODO: move to RetailRoute:  wholesaleRouteRow.WithBonusMinutes; } }
		readonly int baseRouteId;
		readonly int routingPlanId;

		RoutingAlgorithmType routingAlgorithmType = RoutingAlgorithmType.Unknown;
		public RoutingAlgorithmType RoutingAlgorithmType {
			get {
				if (routingAlgorithmType == RoutingAlgorithmType.Unknown) {
					try {
						using (var _db = new Rbr_Db()) {
							var _routingPlanDetailRow = _db.RoutingPlanDetailCollection.GetByPrimaryKey(routingPlanId, baseRouteId);
							if (_routingPlanDetailRow != null) {
								routingAlgorithmType = (RoutingAlgorithmType) _routingPlanDetailRow.Routing_algorithm;
							}
							else {
								routingAlgorithmType = RoutingAlgorithmType.Invalid;
							}
						}
					}
					catch (Exception _ex) {
						TimokLogger.Instance.LogRbr(LogSeverity.Critical, "CustomerRoute.RoutingAlgorithmType", string.Format("GetRoutingAlgorithmType Exception:\r\n{0}", _ex));
					}
				}
				return routingAlgorithmType;
			}
		}

		CustomerRoute(WholesaleRouteRow pWholesaleRouteRow, int pRoutingPlanId, RouteRow pRouteRow) : base(pRouteRow) {
			wholesaleRouteRow = pWholesaleRouteRow;
			routingPlanId = pRoutingPlanId;
			baseRouteId = pRouteRow.Route_id;

			using (var _db = new Rbr_Db()) {
				var _serviceRow = _db.ServiceCollection.GetByPrimaryKey(wholesaleRouteRow.Service_id);
				if (_serviceRow == null) {
					throw new Exception(string.Format("CustomerRoute.Ctor | ServiceRow NOT FOUND, Service_id:{0}", wholesaleRouteRow.Service_id));
				}
				ratingType = _serviceRow.RatingType;
			}
		}

		//--------------------------------------- Static methods ------------------------------------------
		public static CustomerRoute Get(short pServiceId, int pRoutingPlanId, int pBaseRouteId) {
			var _customerRoute = getCustomerRoute(pServiceId, pRoutingPlanId, pBaseRouteId);
			if (_customerRoute == null) {
				TimokLogger.Instance.LogRbr(LogSeverity.Error, "CustomerRoute.Get", string.Format("CustomerRoute NOT found, ServiceId: {0} BaseRouteId: {1}", pServiceId, pBaseRouteId));
			}
			return _customerRoute;
		}

		public static CustomerRoute Get(short pServiceId, int pCallingPlanId, int pRoutingPlanId, string pDestNumber) {
			DialCodeRow _dialCodeRow;
			using (var _db = new Rbr_Db()) {
				_dialCodeRow = _db.DialCodeCollection.GetFirstByCallingPlanIdDialedNumber(pCallingPlanId, pDestNumber);
			}
			if (_dialCodeRow == null) {
				throw new RbrException(RbrResult.Customer_DialCodeNotFound,
				                       "CustomerRoute.Get",
				                       string.Format("ServiceId {0}, CallingPlanId: {1} DestNumber: {2}", pServiceId, pCallingPlanId, pDestNumber));
			}

			var _customerRoute = getCustomerRoute(pServiceId, pRoutingPlanId, _dialCodeRow.Route_id);
			if (_customerRoute == null) {
				throw new RbrException(RbrResult.Customer_RouteNotFound,
				                       "CustomerRoute.Get",
				                       string.Format("ServiceId={0}, CallingPlanId={1}, DestNumber={2}", pServiceId, pCallingPlanId, pDestNumber));
			}
			return _customerRoute;
		}

		//-------------------------- Public ---------------------------------------------------------------
		public void GetCallCenterCost(DateTime pStartTime, int pDuration, out decimal pWholesaleCost, out short pWholesaleRoundedSeconds, out decimal pRetailCost, out short pRetailRoundedSeconds) {
			pWholesaleRoundedSeconds = 0;
			pWholesaleCost = decimal.Zero;
			pRetailRoundedSeconds = 0;
			pRetailCost = decimal.Zero;

			if (ratingType == RatingType.Disabled) {
				return;
			}

			var _rateInfo = RateInfo.GetWholesaleRateInfo(WholesaleRouteId, pStartTime);
			if (_rateInfo == null) {
				TimokLogger.Instance.LogRbr(LogSeverity.Error, "CustomerRoute.GetWholesaleCost", string.Format("Cost is zero, RouteId={0}, for Duration={1}", routeRow.Route_id, pDuration));
				return;
			}

			//-- call center wholesale
			if (ratingType == RatingType.PerCall) {
				pWholesaleCost = _rateInfo.GetPerCallCost();
				pWholesaleRoundedSeconds = (short)( pDuration / 60 + pDuration % 60 > 0 ? 60 : 0 );
			}
			else if (ratingType == RatingType.TimeBased) {
				pWholesaleCost = _rateInfo.GetCost(pDuration, null, null, out pWholesaleRoundedSeconds);
			}
			else {
				throw new RbrException(RbrResult.Rate_TypeUnknown, "CustomerRoute.GetWholesaleCost", string.Format("{0}", ratingType));
			}

			//-- call center retail		NOTE: hack, storing retail rates as Weekend rates
			_rateInfo.CurrentTypeOfDayChoice = TypeOfDayChoice.Weekend;
			if (ratingType == RatingType.PerCall) {
				pRetailCost = _rateInfo.GetPerCallCost();
				pRetailRoundedSeconds = (short)( pDuration / 60 + pDuration % 60 > 0 ? 60 : 0 );
			}
			else if (ratingType == RatingType.TimeBased) {
				pRetailCost = _rateInfo.GetCost(pDuration, null, null, out pRetailRoundedSeconds);
			}
			else {
				throw new RbrException(RbrResult.Rate_TypeUnknown, "CustomerRoute.GetCallCenterCost", string.Format("{0}", ratingType));
			}
		}

		public decimal GetWholesaleCost(DateTime pStartTime, int pDuration, out short pRoundedSeconds) {
			pRoundedSeconds = 0;
			var _cost = decimal.Zero;

			if (ratingType == RatingType.Disabled) {
				return _cost;
			}

			var _rateInfo = RateInfo.GetWholesaleRateInfo(WholesaleRouteId, pStartTime);
			if (_rateInfo != null) {
				if (ratingType == RatingType.PerCall) {
					_cost = _rateInfo.GetPerCallCost();
					pRoundedSeconds = (short)( pDuration / 60 + pDuration % 60 > 0 ? 60 : 0 );
				}
				else if (ratingType == RatingType.TimeBased) {
					_cost = _rateInfo.GetCost(pDuration, null, null, out pRoundedSeconds);
				}
				else {
					throw new RbrException(RbrResult.Rate_TypeUnknown, "CustomerRoute.GetWholesaleCost", string.Format("{0}", ratingType));
				}
			}

			if (_cost == 0) {
				TimokLogger.Instance.LogRbr(LogSeverity.Error, "CustomerRoute.GetWholesaleCost", string.Format("Cost is zero, RouteId={0}, for Duration={1}", routeRow.Route_id, pDuration));
			}
			return _cost;
		}

		public decimal GetRetailCost(DateTime pStartTime, int pDuration, SurchargeInfo pAccessNumberSurcharge, SurchargeInfo pPayphoneSurcharge, out short pRoundedSeconds) {
			pRoundedSeconds = 0;
			var _cost = decimal.Zero;

			if (ratingType == RatingType.Disabled) {
				return _cost;
			}

			var _rateInfo = RateInfo.GetWholesaleRateInfo(WholesaleRouteId, pStartTime);
			if (_rateInfo != null) {
				if (ratingType == RatingType.PerCall) {
					if (pDuration > 0) {
						_cost += _rateInfo.GetPerCallCost();
					}

					if (pAccessNumberSurcharge != null) {
						_cost += pAccessNumberSurcharge.Cost;
					}

					if (pPayphoneSurcharge != null) {
						_cost += pPayphoneSurcharge.Cost;
					}

					pRoundedSeconds = (short) (pDuration / 60 + pDuration % 60 > 0 ? 60 : 0);
				}
				else if (ratingType == RatingType.TimeBased) {
					_cost = _rateInfo.GetCost(pDuration, pAccessNumberSurcharge, pPayphoneSurcharge, out pRoundedSeconds);
				}
				else {
					throw new RbrException(RbrResult.Rate_TypeUnknown, "CustomerRoute.GetWholesaleCost", string.Format("{0}", ratingType));
				}
			}

			if (_cost == 0) {
				TimokLogger.Instance.LogRbr(LogSeverity.Error, "CustomerRoute.GetWholesaleCost_Ex", string.Format("Cost is zero, RouteId={0}, for Duration={1}", routeRow.Route_id, pDuration));
			}
			return _cost;
		}

		public int GetTimeLimit(decimal pCurrentBalance) {
			int _timeLimit = 0;

			if (ratingType == RatingType.Disabled) {
				return _timeLimit;
			}

			var _rateInfo = RateInfo.GetWholesaleRateInfo(WholesaleRouteId, DateTime.Now);
			if (_rateInfo != null) {
				if (ratingType == RatingType.PerCall) {
					_timeLimit = Configuration.Instance.Main.PerCallTimeLimit; //_rateInfo.GetTimeLimit();
				}
				else if (ratingType == RatingType.TimeBased) {
					_timeLimit = _rateInfo.GetTimeLimit(pCurrentBalance, null, null);
				}
				else {
					throw new RbrException(RbrResult.Rate_TypeUnknown, "CustomerRoute.GetTimeLimit", string.Format("{0}", ratingType));
				}
			}

			if (_timeLimit == 0) {
				TimokLogger.Instance.LogRbr(LogSeverity.Error, "CustomerRoute.GetTimeLimit", string.Format("TimeLimit is zero, RouteId={0}, for Amount={1}", routeRow.Route_id, pCurrentBalance));
			}
			return _timeLimit;
		}

		public int GetTimeLimit(decimal pCurrentBalance, SurchargeInfo pAccessNumberSurcharge, SurchargeInfo pPayphoneSurcharge) {
			int _timeLimit = 0;

			if (ratingType == RatingType.Disabled) {
				return _timeLimit;
			}

			var _rateInfo = RateInfo.GetWholesaleRateInfo(WholesaleRouteId, DateTime.Now);
			if (_rateInfo != null) {
				if (ratingType == RatingType.PerCall) {
					//TODO: this should move to Authenticate? it doesn't depend on dialedNumber (Route)
					if (pCurrentBalance < (pAccessNumberSurcharge.Cost + pPayphoneSurcharge.Cost + _rateInfo.GetPerCallCost())) {
						_timeLimit = 0;
					}
					else {
						_timeLimit = Configuration.Instance.Main.PerCallTimeLimit; //_rateInfo.GetTimeLimit();
					}
				}
				else if (ratingType == RatingType.TimeBased) {
					_timeLimit = _rateInfo.GetTimeLimit(pCurrentBalance, pAccessNumberSurcharge, pPayphoneSurcharge);
				}
				else {
					throw new RbrException(RbrResult.Rate_TypeUnknown, "CustomerRoute.GetTimeLimit", string.Format("{0}", ratingType));
				}
			}

			if (_timeLimit == 0) {
				TimokLogger.Instance.LogRbr(LogSeverity.Error, "CustomerRoute.GetTimeLimit_Ex", string.Format("TimeLimit is zero, RouteId={0}, for Amount={1}", routeRow.Route_id, pCurrentBalance));
			}
			return _timeLimit;
		}

		//--------------------------- private -----------------------------
		static CustomerRoute getCustomerRoute(short pServiceId, int pRoutingPlanId, int pBaseRouteId) {
			CustomerRoute _customerRoute = null;

			using (var _db = new Rbr_Db()) {
				var _routeRow = _db.RouteCollection.GetByPrimaryKey(pBaseRouteId);
				if (_routeRow != null) {
					var _wholesaleRouteRow = _db.WholesaleRouteCollection.GetByServiceIdBaseRouteId(pServiceId, pBaseRouteId);
					if (_wholesaleRouteRow != null) {
						_customerRoute = new CustomerRoute(_wholesaleRouteRow, pRoutingPlanId, _routeRow);
					}
					else {
						TimokLogger.Instance.LogRbr(LogSeverity.Error, "CustomerRoute.getCustomerRoute", string.Format("ServiceRouteRow NOT FOUND: {0}, {1}", pServiceId, pBaseRouteId));
					}
				}
				else {
					TimokLogger.Instance.LogRbr(LogSeverity.Error, "CustomerRoute.getCustomerRoute", string.Format("CustomerRouteRow NOT FOUND: {0}, {1}", pServiceId, pBaseRouteId));
				}
			}
			return _customerRoute;
		}
	}
}
using System;
using Timok.Logger;
using Timok.Rbr.Core.Config;
using Timok.Rbr.DAL.RbrDatabase;
using AppConstants=Timok.Rbr.Core.Config.AppConstants;

namespace Timok.Rbr.BLL.DOM {
	public sealed class CarrierRoute {
		readonly CarrierRouteRow carrierRouteRow;

		int carrierRouteId { get { return carrierRouteRow.Carrier_route_id; } }
		short carrierAcctId { get { return carrierRouteRow.Carrier_acct_id; } }
		readonly RatingType ratingType;

		public short CarrierAcctId { get; private set; }
		public string Name { get; private set; }
		public int BaseRouteId { get; private set; }

		//--------------------------------- Constructors -------------------------------------------------
		public CarrierRoute(CarrierRouteRow pCarrierRouteRow) {
			carrierRouteRow = pCarrierRouteRow;
			CarrierAcctId = carrierRouteRow.Carrier_acct_id;

			//-- set name
			RouteRow _routeRow;
			using (var _db = new Rbr_Db()) {
				_routeRow = _db.RouteCollection.GetByPrimaryKey(carrierRouteRow.Route_id);
			}
			Name = _routeRow == null ? AppConstants.Unknown : _routeRow.Name;
			BaseRouteId = _routeRow == null ? carrierRouteRow.Route_id : _routeRow.Route_id;

			//-- set ratingtype
			CarrierAcctRow _carrierAcctRow;
			using (var _db = new Rbr_Db()) {
				_carrierAcctRow = _db.CarrierAcctCollection.GetByPrimaryKey(carrierAcctId);
			}
			if (_carrierAcctRow == null) {
				throw new Exception(string.Format("CarrierRoute.Ctor | CarrierAcctRow NOT FOUND, carrierAcctId:{0}", carrierAcctId));
			}
			ratingType = _carrierAcctRow.RatingType;
		}

		//------------------------------------ Public Static ---------------------------------------------------
		public static CarrierRoute Get(short pCarrierAcctId, int pRouteId) {
			CarrierRouteRow _carrierRouteRow;
			using (var _db = new Rbr_Db()) {
				_carrierRouteRow = _db.CarrierRouteCollection.GetByCarrierAcctIdRouteId(pCarrierAcctId, pRouteId);
			}

			if (_carrierRouteRow == null) {
				TimokLogger.Instance.LogRbr(LogSeverity.Debug, "CarrierRoute.Get", string.Format("CarrierRouteRow NOT FOUND: carrierAcctId={0}, RouteId={1}", pCarrierAcctId, pRouteId));
				return null;
			}
			return new CarrierRoute(_carrierRouteRow);
		}

		//public static CarrierRoute Get(int pCarrierRouteId) {
		//  CarrierRouteRow _carrierRouteRow;
		//  using (var _db = new Rbr_Db()) {
		//    _carrierRouteRow = _db.CarrierRouteCollection.GetByPrimaryKey(pCarrierRouteId);
		//  }

		//  if (_carrierRouteRow == null) {
		//    TimokLogger.Instance.LogRbr(LogSeverity.Debug, "CarrierRoute.GetLCR", string.Format("CarrierRouteRow NOT FOUND: carrierRouteId={0}", pCarrierRouteId));
		//    return null;
		//  }
		//  return new CarrierRoute(_carrierRouteRow);
		//}

		//----------------------------------- Public Instance ---------------------------------------
		//NOTE: combine other variables to get the LCRRank 
		public int GetLCRCostRank() {
			var _rateInfo = RateInfo.GetCarrierRateInfo(carrierRouteId, DateTime.Now);
			if (_rateInfo != null && ratingType != RatingType.Disabled) {
				return _rateInfo.GetNormalizedCost();
			}
			return int.MaxValue;
		}

		public Endpoint GetFirstTermEndpoint() {
			using (var _db = new Rbr_Db()) {
				var _carrierAcctEPMapRows = _db.CarrierAcctEPMapCollection.GetByCarrier_route_id(carrierRouteRow.Carrier_route_id);
				if (_carrierAcctEPMapRows != null && _carrierAcctEPMapRows.Length > 0) {
					var _endpointRow = _db.EndPointCollection.GetByPrimaryKey(_carrierAcctEPMapRows[0].End_point_id);
					return new Endpoint(_endpointRow);
				}
			}
			TimokLogger.Instance.LogRbr(LogSeverity.Error, "CarrierRoute.GetFirstTermEndpoint", string.Format("TermEP NotFound, carrierRouteId={0}", carrierRouteId));
			return null;
		}

		public decimal GetCost(DateTime pStartTime, int pDuration, out short pRoundedSeconds) {
			pRoundedSeconds = 0;
			var _cost = decimal.Zero;

			if (ratingType == RatingType.Disabled) {
				return _cost;
			}

			var _rateInfo = RateInfo.GetCarrierRateInfo(carrierRouteId, pStartTime);
			if (_rateInfo != null) {
				_cost = _rateInfo.GetCost(pDuration, null, null, out pRoundedSeconds);
				if (_cost == 0) {
					TimokLogger.Instance.LogRbr(LogSeverity.Error, "CarrierRoute.GetCost", string.Format("Cost is zero, carrierRouteId={0}, for Duration={1}", carrierRouteRow.Carrier_route_id, pDuration));
				}
			}
			return _cost;
		}

		public int GetTimeLimit(decimal pCurrentBalance) {
			var _timeLimit = 0;

			if (ratingType == RatingType.Disabled) {
				return _timeLimit;
			}

			var _rateInfo = RateInfo.GetCarrierRateInfo(carrierRouteId, DateTime.Now);
			if (_rateInfo != null) {
				_timeLimit = _rateInfo.GetTimeLimit(pCurrentBalance, null, null);
				if (_timeLimit == 0) {
					TimokLogger.Instance.LogRbr(LogSeverity.Error, "CarrierRoute.GetTimeLimit", string.Format("TimeLimit is zero, RouteId={0}, for Amount={1}", carrierRouteRow.Carrier_route_id, pCurrentBalance));
				}
			}
			return _timeLimit;
		}
	}
}
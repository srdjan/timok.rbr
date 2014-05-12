using System;
using System.Collections;
using System.ComponentModel;
using Timok.Core;
using Timok.Rbr.Core.Config;
using Timok.Rbr.DAL.RbrDatabase;
using Timok.Rbr.DTO;

namespace Timok.Rbr.BLL.Managers {
	internal class CarrierRouteManager {
		CarrierRouteManager() { }

		#region Route Getters

		internal static RatedRouteDto Get(Rbr_Db pDb, int pCarrierRouteId) {
			var _carrierRouteRow = pDb.CarrierRouteCollection.GetByPrimaryKey(pCarrierRouteId);
			var _carrierAcct = CarrierAcctManager.GetAcct(pDb, _carrierRouteRow.Carrier_acct_id);
			return Get(pDb, _carrierAcct, _carrierRouteRow);
		}

		internal static RatedRouteDto Get(Rbr_Db pDb, short pCarrierAcctId, int pBaseRouteId) {
			var _carrierRouteRow = pDb.CarrierRouteCollection.GetByCarrierAcctIdRouteId(pCarrierAcctId, pBaseRouteId);
			var _carrierAcct = CarrierAcctManager.GetAcct(pDb, pCarrierAcctId);
			return Get(pDb, _carrierAcct, _carrierRouteRow);
		}

		internal static RatedRouteDto Get(Rbr_Db pDb, CarrierAcctDto pCarrierAcct, CarrierRouteRow pCarrierRouteRow) {
			if (pCarrierRouteRow == null) {
				return null;
			}
			var _activeCarrierRouteEPMapCount = pDb.CarrierAcctEPMapCollection.GetActiveCountByCarrierRouteId(pCarrierRouteRow.Carrier_route_id);
			var _carrierRouteState = (_activeCarrierRouteEPMapCount > 0) ? RouteState.Valid : RouteState.NoActiveEndpoints;

			if (pCarrierAcct.IsRatingEnabled) {
				//get Rates status
				bool _allRatesValid;
				CarrierRateHistoryRow _carrierRateHistoryRow = pDb.CarrierRateHistoryCollection.GetByCarrierRouteIdDate(pCarrierRouteRow.Carrier_route_id, DateTime.Today);
				if (_carrierRateHistoryRow == null) {
					_allRatesValid = false;
				}
				else {
					_allRatesValid = pDb.RateCollection.HasAllValidRates(_carrierRateHistoryRow.Rate_info_id);
				}
				_carrierRouteState = (_allRatesValid) ? _carrierRouteState | RouteState.Valid : _carrierRouteState | RouteState.NoRates;
			}
			var _baseRoute = RoutingManager.GetBaseRoute(pDb, pCarrierRouteRow.Route_id);
			var _route = mapToRoute(pCarrierRouteRow, pCarrierAcct, _baseRoute, _carrierRouteState);
			return _route;
		}

		internal static RatedRouteDto GetDefaultRoute(Rbr_Db pDb, CarrierAcctDto pCarrierAcct, RouteState pValid) {
			var _defaultCarrierRouteRow = pDb.CarrierRouteCollection.GetByPrimaryKey(-pCarrierAcct.CarrierAcctId);
			//if (pCarrierAcctRow.IsRatingEnabled) {
			//NOTE: DefaultRatingInfo is always created no metter what
			//and it should be loaded as well no metter what
			//}
			var _baseRoute = RoutingManager.GetBaseRoute(pDb, _defaultCarrierRouteRow.Route_id);
			return mapToRoute(_defaultCarrierRouteRow, pCarrierAcct, _baseRoute, pValid);
		}

		internal static RatedRouteDto[] GetByCarrierAcctIdCountryId(Rbr_Db pDb, short pCarrierAcctId, int pCountryId) {
			var _list = new ArrayList();
			var _carrierAcct = CarrierAcctManager.GetAcct(pDb, pCarrierAcctId);
			var _carrierRouteRows = pDb.CarrierRouteCollection.GetByCarrierAcctIdCountryId(pCarrierAcctId, pCountryId);
			foreach (var _carrierRouteRow in _carrierRouteRows) {
				var _route = Get(pDb, _carrierAcct, _carrierRouteRow);
				_list.Add(_route);
			}
			if (_list.Count > 1) {
				var _sortInfos = new[]
				                 	{
				                 		new SortInfo(RatedRouteDto.CountryName_PropName, ListSortDirection.Ascending),
				                 		new SortInfo(RatedRouteDto.Name_PropName, ListSortDirection.Ascending)
				                 	};
				_list.Sort(new GenericComparer(_sortInfos));
			}
			return (RatedRouteDto[]) _list.ToArray(typeof (RatedRouteDto));
		}

		internal static RatedRouteDto[] GetByCarrierAcctId(Rbr_Db pDb, short pCarrierAcctId) {
			var _list = new ArrayList();
			var _carrierAcct = CarrierAcctManager.GetAcct(pDb, pCarrierAcctId);
			var _carrierRouteRows = pDb.CarrierRouteCollection.GetByCarrier_acct_id(pCarrierAcctId);
			foreach (var _carrierRouteRow in _carrierRouteRows) {
				var _route = Get(pDb, _carrierAcct, _carrierRouteRow);
				_list.Add(_route);
			}

			if (_list.Count > 1) {
				var _sortInfos = new[]
				                 	{
				                 		new SortInfo(RatedRouteDto.CountryName_PropName, ListSortDirection.Ascending),
				                 		new SortInfo(RatedRouteDto.Name_PropName, ListSortDirection.Ascending)
				                 	};
				_list.Sort(new GenericComparer(_sortInfos));
			}
			return (RatedRouteDto[]) _list.ToArray(typeof (RatedRouteDto));
		}

		#endregion Route Getters

		#region Route Actions

		internal static void Add(Rbr_Db pDb, CarrierAcctDto pCarrierAcct, int[] pSelectedBaseRouteIds) {
			if (pCarrierAcct != null && pSelectedBaseRouteIds != null && pSelectedBaseRouteIds.Length > 0) {
				foreach (int _baseRouteId in pSelectedBaseRouteIds) {
					int _carrierRouteId;
					Add(pDb, pCarrierAcct, _baseRouteId, out _carrierRouteId);
				}
			}
		}

		internal static void Add(Rbr_Db pDb, CarrierAcctDto pCarrierAcct, int pBaseRouteId, out int pCarrierRouteId) {
			var _carrierRouteRow = new CarrierRouteRow();
			_carrierRouteRow.Carrier_acct_id = pCarrierAcct.CarrierAcctId;
			_carrierRouteRow.Acd_target = pCarrierAcct.DefaultRoute.ACDTarget;
			_carrierRouteRow.Acd_time_window = pCarrierAcct.DefaultRoute.ACDTimeWindow;
			_carrierRouteRow.Asr_target = pCarrierAcct.DefaultRoute.ASRTarget;
			_carrierRouteRow.Asr_time_window = pCarrierAcct.DefaultRoute.ASRTimeWindow;
			_carrierRouteRow.Next_ep = pCarrierAcct.DefaultRoute.NextEP;

			if (pBaseRouteId > 0) {
				//Regular ServiceRoute
				_carrierRouteRow.Route_id = pBaseRouteId;
				_carrierRouteRow.RouteStatus = Status.Active;
			}
			else {
				//Default CarrierRoute, set CarrierRouteId = -CarrierAcctId, NO Base RouteId
				pCarrierAcct.DefaultRoute.RatedRouteId = -pCarrierAcct.CarrierAcctId;
				_carrierRouteRow.Carrier_route_id = pCarrierAcct.DefaultRoute.RatedRouteId;
				_carrierRouteRow.RouteStatus = Status.Blocked; //for Default ServiceRoute
			}

			pDb.CarrierRouteCollection.Insert(_carrierRouteRow);

			if (pCarrierAcct.IsRatingEnabled || pBaseRouteId == 0) {
				//always add Def RatingInfo for Def Route 
				RatingManager.AddDefaultRatingInfo(pDb, _carrierRouteRow.Carrier_route_id, pCarrierAcct.DefaultRatingInfo, RouteType.Carrier);
			}
			pCarrierRouteId = _carrierRouteRow.Carrier_route_id;
		}

		internal static void Update(Rbr_Db pDb, RatedRouteDto pRoute) {
			//TODO: ??? confirm what else should be changed ???
			var _carrierRouteRow = mapToCarrierRouteRow(pRoute);
			pDb.CarrierRouteCollection.Update(_carrierRouteRow);
		}

		internal static void Delete(Rbr_Db pDb, RatedRouteDto pRoute) {
			//NOTE: !!! NO DELETE !!!, Archive only
			pRoute.Status = Status.Archived;
			Update(pDb, pRoute);
		}

		#endregion Route Actions

		#region Mappings

		static RatedRouteDto mapToRoute(CarrierRouteRow pCarrierRouteRow, CarrierAcctDto pCarrierAcct, BaseRouteDto pBaseRoute, RouteState pCarrierRouteState) {
			if (pCarrierRouteRow == null) {
				return null;
			}

			var _route = new RatedRouteDto();
			_route.RatedRouteId = pCarrierRouteRow.Carrier_route_id;
			_route.Status = pCarrierRouteRow.RouteStatus;
			_route.ACDTarget = pCarrierRouteRow.Acd_target;
			_route.ACDTimeWindow = pCarrierRouteRow.Acd_time_window;
			_route.ASRTarget = pCarrierRouteRow.Asr_target;
			_route.ASRTimeWindow = pCarrierRouteRow.Asr_time_window;
			_route.NextEP = pCarrierRouteRow.Next_ep;

			_route.AccountId = pCarrierAcct.CarrierAcctId;
			_route.AccountName = pCarrierAcct.Name;
			_route.AccountStatus = pCarrierAcct.Status;

			_route.BaseRoute = pBaseRoute;
			_route.RouteState = pCarrierRouteState;

			_route.DefaultRatingInfo = pCarrierAcct.DefaultRatingInfo;

			return _route;
		}

		static CarrierRouteRow mapToCarrierRouteRow(RatedRouteDto pRoute) {
			if (pRoute == null) {
				return null;
			}

			var _carrierRouteRow = new CarrierRouteRow();
			_carrierRouteRow.Carrier_route_id = pRoute.RatedRouteId;
			_carrierRouteRow.RouteStatus = pRoute.Status;
			_carrierRouteRow.Acd_target = pRoute.ACDTarget;
			_carrierRouteRow.Acd_time_window = pRoute.ACDTimeWindow;
			_carrierRouteRow.Asr_target = pRoute.ASRTarget;
			_carrierRouteRow.Asr_time_window = pRoute.ASRTimeWindow;
			_carrierRouteRow.Next_ep = pRoute.NextEP;

			_carrierRouteRow.Carrier_acct_id = pRoute.AccountId;

			if (pRoute.BaseRouteId == 0) {
				_carrierRouteRow.IsRoute_idNull = true;
			}
			else {
				_carrierRouteRow.Route_id = pRoute.BaseRouteId;
			}

			return _carrierRouteRow;
		}

		#endregion Mappings
	}
}
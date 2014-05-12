using System;
using System.Collections.Generic;
using Timok.Rbr.DAL.RbrDatabase;
using Timok.Rbr.DTO;
using Timok.Rbr.Core.Config;

namespace Timok.Rbr.BLL.Managers {
	internal class CustomerRouteManager {
		CustomerRouteManager() { }

		#region Route Getters

		internal static RatedRouteDto Get(Rbr_Db pDb, int pWholesaleRouteId) {
			var _wholesaleRouteRow = pDb.WholesaleRouteCollection.GetByPrimaryKey(pWholesaleRouteId);
			if (_wholesaleRouteRow != null) {
				var _service = ServiceManager.GetService(pDb, _wholesaleRouteRow.Service_id);
				return Get(pDb, _service, _service.DefaultRoutingPlanId, _wholesaleRouteRow);
			}
			return null;
		}

		internal static RatedRouteDto[] Get(Rbr_Db pDb, short pServiceId, int pCountryId) {
			var _wholesaleRouteRows = pDb.WholesaleRouteCollection.GetByService_idCountry_id(pServiceId, pCountryId);
			
			var _list = new List<RatedRouteDto>();
			foreach (var _wholesaleRouteRow in _wholesaleRouteRows) {
				var _service = ServiceManager.GetService(pDb, _wholesaleRouteRow.Service_id);
				var _route = Get(pDb, _service, _service.DefaultRoutingPlanId, _wholesaleRouteRow);
				_list.Add(_route);
			}

			var _routes = _list.ToArray();
			if (_routes.Length > 1) {
				_routes = RoutingManager.SortRatedRoutes(_routes);
			}
			return _routes;
		}

		//internal static RatedRouteDto[] Get(Rbr_Db pDb, short pServiceId, int pRoutingPlanId) {
		//  var _list = new List<RatedRouteDto>();
		//  var _wholesaleRouteRows = pDb.WholesaleRouteCollection.GetByService_id(pServiceId);
		//  var _service = ServiceManager.GetService(pDb, pServiceId);
		//  foreach (var _wholesaleRouteRow in _wholesaleRouteRows) {
		//    var _route = Get(pDb, _service, pRoutingPlanId, _wholesaleRouteRow);
		//    _list.Add(_route);
		//  }

		//  var _routes = _list.ToArray();
		//  if (_routes.Length > 1) {
		//    _routes = RoutingManager.SortRatedRoutes(_routes);
		//  }
		//  return _routes;
		//}

		internal static RatedRouteDto GetRouteByServiceIdBaseRouteId(Rbr_Db pDb, short pServiceId, int pBaseRouteId) {
			var _wholesaleRouteRow = pDb.WholesaleRouteCollection.GetByServiceIdBaseRouteId(pServiceId, pBaseRouteId);
			var _service = ServiceManager.GetService(pDb, pServiceId);
			return Get(pDb, _service, _service.DefaultRoutingPlanId, _wholesaleRouteRow);
		}

		//NOTE: Used by importer
		internal static RatedRouteDto GetRouteByServiceIdBaseRouteId(Rbr_Db pDb, short pServiceId, int pBaseRouteId, int pRoutingPlanId) {
			var _wholesaleRouteRow = pDb.WholesaleRouteCollection.GetByServiceIdBaseRouteId(pServiceId, pBaseRouteId);
			var _service = ServiceManager.GetService(pDb, pServiceId);
			return Get(pDb, _service, pRoutingPlanId, _wholesaleRouteRow);
		}

		internal static int GetCount(Rbr_Db pDb, int pBaseRouteId) { return pDb.WholesaleRouteCollection.GetCount(pBaseRouteId); }

		public static RatedRouteDto Get(Rbr_Db pDb, ServiceDto pService, int pRoutingPlanId, WholesaleRouteRow pWholesaleRouteRow) {
			try {
				if (pWholesaleRouteRow == null) {
					return null;
				}
				var _routingPlanDetailRow = pDb.RoutingPlanDetailCollection.GetByPrimaryKey(pRoutingPlanId, pWholesaleRouteRow.Route_id);
				var _routingPlanId = 0;
				var _routingAlgorithmType = RoutingAlgorithmType.Unknown;
				if (_routingPlanDetailRow != null) {
					_routingPlanId = _routingPlanDetailRow.Routing_plan_id;
					_routingAlgorithmType = _routingPlanDetailRow.Algorithm;
				}
				var _routeState = (_routingPlanDetailRow != null) ? RouteState.Valid : RouteState.NotInRoutingPlan;

				if (pService.IsRatingEnabled) {
					//get Rates status
					bool _allRatesValid;
					var _wholesaleRateHistoryRow = pDb.WholesaleRateHistoryCollection.GetByWholesaleRouteIdDate(pWholesaleRouteRow.Wholesale_route_id, DateTime.Today);
					if (_wholesaleRateHistoryRow == null) {
						_allRatesValid = false;
					}
					else {
						_allRatesValid = pDb.RateCollection.HasAllValidRates(_wholesaleRateHistoryRow.Rate_info_id);
					}
					_routeState = (_allRatesValid) ? _routeState | RouteState.Valid : _routeState | RouteState.NoRates;
				}
				var _baseRoute = RoutingManager.GetBaseRoute(pDb, pWholesaleRouteRow.Route_id);
				return mapToRoute(pWholesaleRouteRow, _routingPlanId, _routingAlgorithmType, pService, _baseRoute, _routeState);
			}
			catch (Exception _ex) {
				var _exc = _ex;
			}
			return null;
		}

		#endregion Route Getters

		#region Route Actions

		internal static void Add(Rbr_Db pDb, short pServiceId, int[] pSelectedBaseRouteIds) {
			if (pServiceId > 0 && pSelectedBaseRouteIds != null && pSelectedBaseRouteIds.Length > 0) {
				foreach (int _baseRouteId in pSelectedBaseRouteIds) {
					int _routeId; //NOTE: just a holder, not in use
					Add(pDb, pServiceId, _baseRouteId, out _routeId);
				}
			}
		}

		internal static void AddDefault(Rbr_Db pDb, ServiceDto pService, out int pDefaultWholesaleRouteId) {
			var _wholesaleRouteRow = new WholesaleRouteRow();
			_wholesaleRouteRow.Service_id = pService.ServiceId;

			//DefaultWholesaleRoute, set WholesaleRouteId = -ServiceId, NO Base RouteId
			pService.DefaultRoute.RatedRouteId = -pService.ServiceId;
			_wholesaleRouteRow.IsRoute_idNull = true;
			_wholesaleRouteRow.Wholesale_route_id = pService.DefaultRoute.RatedRouteId;
			_wholesaleRouteRow.RouteStatus = Status.Blocked; //for Default WholesaleServiceRoute

			pDb.WholesaleRouteCollection.Insert(_wholesaleRouteRow);

			//always add Def RatingInfo for Def WholesaleRoute
			RatingManager.AddDefaultRatingInfo(pDb, _wholesaleRouteRow.Wholesale_route_id, pService.DefaultRatingInfo, RouteType.Wholesale);

			pDefaultWholesaleRouteId = _wholesaleRouteRow.Wholesale_route_id;
		}

		internal static void Add(Rbr_Db pDb, short pServiceId, int pBaseRouteId, out int pRouteId) {
			pRouteId = 0;

			if (pBaseRouteId == 0) {
				throw new ArgumentException("BaseRouteId cannot be 0.", "pBaseRouteId");
			}

			var _service = ServiceManager.GetService(pDb, pServiceId);
			var _wholesaleRouteRow = new WholesaleRouteRow();
			_wholesaleRouteRow.Service_id = _service.ServiceId;
			_wholesaleRouteRow.Route_id = pBaseRouteId;
			_wholesaleRouteRow.RouteStatus = Status.Active;

			pDb.WholesaleRouteCollection.Insert(_wholesaleRouteRow);

			if (_service.IsRatingEnabled) {
				RatingManager.AddDefaultRatingInfo(pDb, _wholesaleRouteRow.Wholesale_route_id, _service.DefaultRatingInfo, RouteType.Wholesale);
			}

			pRouteId = _wholesaleRouteRow.Wholesale_route_id;
		}

		internal static void Update(Rbr_Db pDb, RatedRouteDto pRoute) {
			var _wholesaleRouteRow = mapToWholesaleRouteRow(pRoute);
			pDb.WholesaleRouteCollection.Update(_wholesaleRouteRow);
			//pDb.AddChangedObject(new CustomerRouteKey(TxType.Delete, _wholesaleRouteRow.Wholesale_route_id));
		}

		internal static void Delete(Rbr_Db pDb, RatedRouteDto pRoute) {
			//NOTE: !!! NO DELETE !!! , Archive only
			pRoute.Status = Status.Archived;
			Update(pDb, pRoute);
		}

		#endregion Route Actions

		#region Mappings

		static RatedRouteDto mapToRoute(WholesaleRouteRow pWholesaleRouteRow, int pRoutingPlanId, RoutingAlgorithmType pRoutingAlgorithmType, ServiceDto pService, BaseRouteDto pBaseRoute, RouteState pRouteState) {
			if (pWholesaleRouteRow == null) {
				return null;
			}

			var _route = new RatedRouteDto
					{
						RatedRouteId = pWholesaleRouteRow.Wholesale_route_id, 
						AccountId = pService.ServiceId, 
						AccountName = pService.Name, 
						AccountStatus = pService.Status, 
						RoutingPlanId = pRoutingPlanId, 
						Algorithm = pRoutingAlgorithmType, 
						Status = pWholesaleRouteRow.RouteStatus, 
						BaseRoute = pBaseRoute, 
						RouteState = pRouteState, 
						DefaultRatingInfo = pService.DefaultRatingInfo
					};

			return _route;
		}

		static WholesaleRouteRow mapToWholesaleRouteRow(RatedRouteDto pRoute) {
			if (pRoute == null) {
				return null;
			}

			var _wholesaleRouteRow = new WholesaleRouteRow();
			_wholesaleRouteRow.Wholesale_route_id = pRoute.RatedRouteId;
			//TODO: should we add ServiceId to Route
			_wholesaleRouteRow.Service_id = pRoute.AccountId;
			_wholesaleRouteRow.RouteStatus = pRoute.Status;

			if (pRoute.BaseRouteId == 0) {
				_wholesaleRouteRow.IsRoute_idNull = true;
			}
			else {
				_wholesaleRouteRow.Route_id = pRoute.BaseRouteId;
			}

			return _wholesaleRouteRow;
		}

		#endregion Mappings
	}
}
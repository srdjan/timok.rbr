using System;
using System.Collections.Generic;
using System.ComponentModel;
using Timok.Core;
using Timok.Rbr.Core.Config;
using Timok.Rbr.DAL.RbrDatabase.Base;
using Timok.Rbr.DTO;
using Timok.Rbr.DAL.RbrDatabase;

namespace Timok.Rbr.BLL.Managers {
	internal class RoutingManager {
		private RoutingManager() {}

		#region Getters
		
		internal static RouteRow Get(Rbr_Db pDb, int pRouteId) {
			return pDb.RouteCollection.GetByPrimaryKey(pRouteId);
		}

		static internal RouteRow GetByName(Rbr_Db pDb, int pCallingPlanId, string pName) {
			return pDb.RouteCollection.GetByName(pCallingPlanId, pName);
		}

		internal static RouteRow[] GetAll(Rbr_Db pDb) {
			return pDb.RouteCollection.GetAll();
		}

		public static RouteRow[] GetRoutesByRoutingNumber(Rbr_Db pDb, int pRoutingNumber) {
			var _list = new List<RouteRow>();
			var _routes = pDb.RouteCollection.GetAllCarrierRoutes();
			foreach (var _route in _routes) {
				if (pRoutingNumber == _route.Routing_number) {
					_list.Add(_route);
				}
			}
			return _list.ToArray();
		}

		internal static RouteRow GetProper(Rbr_Db pDb, int pCallingPlanId, int pCountryId) {
			return pDb.RouteCollection.GetProperByCallingPlanIdCountryId(pCallingPlanId, pCountryId);
		}

		internal static RouteRow[] GetByCallingPlanId(Rbr_Db pDb, int pCallingPlanId) {
			return pDb.RouteCollection.GetByCalling_plan_id(pCallingPlanId);
		}

		//internal static RouteRow[] GetAll(Rbr_Db pDb, OwnershipType pOwnership) {
		//  return pDb.RouteCollection.GetActiveByOwnershipType(pOwnership);
		//}

		internal static RouteRow[] GetByCallingPlanIdCountryId(Rbr_Db pDb, int pCallingPlanId, int pCountryId) {
			var _list = new List<RouteRow>();
			var _routes = pDb.RouteCollection.GetByCallingPlanIdCountryId(pCallingPlanId, pCountryId);
			foreach (var _route in _routes) {
				if (_route.IsProper) {
					_list.Insert(0, _route);
				}
				else {
					_list.Add(_route);
				}
			}
			return _list.ToArray();
		}

		internal static RouteRow[] GetByCallingPlanIdRoutingPlanId(Rbr_Db pDb, int pCallingPlanId, int pRoutingPlanId) {
			var _list = new List<RouteRow>();
			var _routes = pDb.RouteCollection.GetByCallingPlanIdRoutingPlanId(pCallingPlanId, pRoutingPlanId);
			foreach (var _route in _routes) {
				if (_route.IsProper) {
					_list.Insert(0, _route);
				}
				else {
					_list.Add(_route);
				}
			}
			return _list.ToArray();
		}

		internal static RouteRow[] GetByCountryId(Rbr_Db pDb, int pCountryId) {
			var _list = new List<RouteRow>();
			var _routes = pDb.RouteCollection.GetByCountry_id(pCountryId);
			foreach (var _route in _routes) {
				if (_route.IsProper) {
					_list.Insert(0, _route);
				}
				else {
					_list.Add(_route);
				}
			}

			var _genericComparer = new GenericComparer();
			_genericComparer.SortInfos.Add(new SortInfo(RouteRow.calling_plan_id_PropName, ListSortDirection.Ascending));
			_genericComparer.SortInfos.Add(new SortInfo(RouteRow.IsProper_PropName, ListSortDirection.Descending));
			_genericComparer.SortInfos.Add(new SortInfo(RouteRow.BreakoutName_PropName, ListSortDirection.Ascending));

			_list.Sort((IComparer<RouteRow>) _genericComparer);
			return _list.ToArray();
		}

		internal static BaseRouteDto[] GetUnusedByCallingPlanIdRoutingPlanId(Rbr_Db pDb, int pCallingPlanId, int pRoutingPlanId) {
			var _routeRows = pDb.RouteCollection.GetUnusedByCallingPlanIdRoutingPlanId(pCallingPlanId, pRoutingPlanId);
			var _callingPlan = CallingPlanManager.GetCallingPlan(pDb, pCallingPlanId);
			var _routeList = getBaseRouteList(pDb, _callingPlan, _routeRows);
			return sortBaseRoutes(_routeList.ToArray());
		}

/*
		public static BaseRouteDto[] GetAllCarrierRoutes(Rbr_Db pDb, string pDialedNumber) {
			var _list = new List<BaseRouteDto>();

			var _routeRows = pDb.RouteCollection.GetAllCarrierRoutes(pDialedNumber);
			if (_routeRows != null) {
				foreach (var _routeRow in _routeRows) {
					var _baseRouteDto = GetBaseRoute(pDb, _routeRow.Route_id);
					if (_baseRouteDto != null) {
						_list.Add(_baseRouteDto);
					}
				}
			}
			return _list.ToArray();
		}
*/

		internal static BaseRouteDto[] GetBaseRoutesByCountryId(Rbr_Db pDb, int pCallingPlanId, int pCountryId) {
			var _list = new List<BaseRouteDto>();

			var _routeRows = GetByCallingPlanIdCountryId(pDb, pCallingPlanId, pCountryId); 
			if (_routeRows != null) {
				foreach (var _routeRow in _routeRows) {
					var _baseRouteDto = GetBaseRoute(pDb, _routeRow.Route_id);
					if (_baseRouteDto != null) {
						_list.Add(_baseRouteDto);
					}
				}
			}
			return _list.ToArray();
		}

		internal static BaseRouteDto[] GetBaseRoutesByRoutingPlanId(Rbr_Db pDb, int pCallingPlanId, int pRoutingPlanId) {
			RouteRow[] _routeRows;
			if (pRoutingPlanId > 0) {
				_routeRows = GetByCallingPlanIdRoutingPlanId(pDb, pCallingPlanId, pRoutingPlanId);
			}
			else {
				_routeRows = GetByCallingPlanId(pDb, pCallingPlanId);
			}

			var _callingPlan = CallingPlanManager.GetCallingPlan(pDb, pCallingPlanId);
			var _routeList = getBaseRouteList(pDb, _callingPlan, _routeRows);
			return sortBaseRoutes(_routeList.ToArray());
		}

		internal static BaseRouteDto[] GetUnusedBaseRoutes(Rbr_Db pDb, short pAccountId, int pCallingPlanId, ViewContext pContext, int pRoutingPlanId) {
			var _callingPlan = CallingPlanManager.GetCallingPlan(pDb, pCallingPlanId);

			RouteRow[] _routeRows = null;
			if (pContext == ViewContext.Carrier) {
				_routeRows = pDb.RouteCollection.GetUnusedByCallingPlanIdCarrierAcctId(pCallingPlanId, pAccountId);
			}
			else if (pContext == ViewContext.Service) {
				_routeRows = pDb.RouteCollection.GetUnusedByCallingPlanIdRoutingPlanIdServiceId(pCallingPlanId, pRoutingPlanId, pAccountId);
			}

			if (_routeRows == null || _routeRows.Length == 0) {
				return null;
			}

			var _baseRoutes = new List<BaseRouteDto>();
			foreach (var _routeRow in _routeRows) {
				var _country = CallingPlanManager.GetCountry(pDb, _routeRow.Country_id);

				var _dialCodeCount = CallingPlanManager.GetDialCodeCount(pDb, _routeRow.Route_id);
				var _routeState = ( _dialCodeCount > 0 ) ? RouteState.Valid : RouteState.NoDialCodes;

				_baseRoutes.Add(mapToBaseRoute(_routeRow, _country, _callingPlan, _routeState));
			}
			return sortBaseRoutes(_baseRoutes.ToArray());
		}

		internal static BaseRouteDto GetBaseRoute(Rbr_Db pDb, int pBaseRouteId) {
			if (pBaseRouteId == 0) {
				return null;
			}

			var _routeRow = Get(pDb, pBaseRouteId);
			if (_routeRow == null) {
				return null;
			}
			var _callingPlan = CallingPlanManager.GetCallingPlan(pDb, _routeRow.Calling_plan_id);
			var _country = CallingPlanManager.GetCountry(pDb, _routeRow.Country_id);

			var _dialCodeCount = pDb.DialCodeCollection.GetCount(_routeRow.Route_id);
			var _routeState = ( _dialCodeCount > 0 ) ? RouteState.Valid : RouteState.NoDialCodes;
			return mapToBaseRoute(_routeRow, _country, _callingPlan, _routeState);
		}

		internal static BaseRouteDto GetProperRoute(Rbr_Db pDb, int pCallingPlanId, int pCountryId) {
			var _properRouteRow = GetProper(pDb, pCallingPlanId, pCountryId);
			if (_properRouteRow == null) {
				return null;
			}

			var _callingPlan = CallingPlanManager.GetCallingPlan(pDb, pCallingPlanId);
			var _country = CallingPlanManager.GetCountry(pDb, pCountryId);
			var _dialCodeCount = CallingPlanManager.GetDialCodeCount(pDb, _properRouteRow.Route_id);
			var _routeState = ( _dialCodeCount > 0 ) ? RouteState.Valid : RouteState.NoDialCodes;
			return mapToBaseRoute(_properRouteRow, _country, _callingPlan, _routeState);
		}

		//internal static RoutingPlanDetailDto[] GetRoutingPlanDetails(Rbr_Db pDb, int pRoutingPlanId) {
		//  if (pRoutingPlanId == 0) {
		//    return null;
		//  }
		//  var _list = new List<RoutingPlanDetailDto>();
		//  var _routingPlan = GetRoutingPlan(pDb, pRoutingPlanId);
		//  var _routingPlanDetailRows = pDb.RoutingPlanDetailCollection.GetByRouting_plan_id(pRoutingPlanId);
		//  foreach (var _routingPlanDetailRow in _routingPlanDetailRows) {
		//    var _baseRoute = GetBaseRoute(pDb, _routingPlanDetailRow.Route_id);
		//    var _terminationsCount = 1;
		//    //TODO: get Route State for LCR Routes
		//    if (_routingPlanDetailRow.Algorithm != RoutingAlgorithmType.LCR) {
		//      _terminationsCount = pDb.TerminationChoiceCollection.GetCountByRoutingPlanIdRouteId(_routingPlanDetailRow.Routing_plan_id, _routingPlanDetailRow.Route_id);
		//    }
		//    var _routingPlanDetailRouteState = ( _terminationsCount > 0 ) ? RouteState.Valid : RouteState.NoTerminations;
		//    _list.Add(mapToRoutingPlanDetail(_routingPlanDetailRow, _baseRoute, _routingPlan, _routingPlanDetailRouteState));
		//  }

		//  var _routingPlanDetails = _list.ToArray();
		//  if (_routingPlanDetails.Length > 1) {
		//    SortRoutingPlanDetailByCountryNameAndRouteName(ref _routingPlanDetails);
		//  }
		//  return _routingPlanDetails;
		//}

		internal static RoutingPlanDetailDto[] GetRoutingPlanDetails(Rbr_Db pDb, int pRoutingPlanId, int pCountryId) {
			if (pRoutingPlanId == 0 || pCountryId == 0) {
				return null;
			}
			var _list = new List<RoutingPlanDetailDto>();
			var _routingPlan = GetRoutingPlan(pDb, pRoutingPlanId);
			var _routingPlanDetailRows = pDb.RoutingPlanDetailCollection.GetByRoutingPlanIdCountryId(pRoutingPlanId, pCountryId);
			foreach (var _routingPlanDetailRow in _routingPlanDetailRows) {
				var _baseRoute = GetBaseRoute(pDb, _routingPlanDetailRow.Route_id);
				var _terminationsCount = 1;
				//TODO: get Route State for LCR Routes
				if (_routingPlanDetailRow.Algorithm != RoutingAlgorithmType.LCR) {
					_terminationsCount = pDb.TerminationChoiceCollection.GetCountByRoutingPlanIdRouteId(_routingPlanDetailRow.Routing_plan_id, _routingPlanDetailRow.Route_id);
				}
				var _routingPlanDetailRouteState = ( _terminationsCount > 0 ) ? RouteState.Valid : RouteState.NoTerminations;
				_list.Add(mapToRoutingPlanDetail(_routingPlanDetailRow, _baseRoute, _routingPlan, _routingPlanDetailRouteState));
			}

			var _routingPlanDetails = _list.ToArray();
			if (_routingPlanDetails.Length > 1) {
				SortRoutingPlanDetailByCountryNameAndRouteName(ref _routingPlanDetails);
			}
			return _routingPlanDetails;
		}

		internal static RoutingPlanDetailDto GetRoutingPlanDetail(Rbr_Db pDb, int pRoutingPlanId, int pBaseRouteId) {
			if (pRoutingPlanId == 0 || pBaseRouteId == 0) {
				return null;
			}
			var _routingPlan = GetRoutingPlan(pDb, pRoutingPlanId);
			var _baseRoute = GetBaseRoute(pDb, pBaseRouteId);
			var _routingPlanDetailRow = pDb.RoutingPlanDetailCollection.GetByPrimaryKey(pRoutingPlanId, pBaseRouteId);
			if (_routingPlanDetailRow == null) {
				return null;
			}

			var _terminationsCount = 1;
			//TODO: get Route State for LCR Routes
			if (_routingPlanDetailRow.Algorithm != RoutingAlgorithmType.LCR) {
				_terminationsCount = pDb.TerminationChoiceCollection.GetCountByRoutingPlanIdRouteId(_routingPlanDetailRow.Routing_plan_id, _routingPlanDetailRow.Route_id);
			}
			var _routingPlanDetailRouteState = ( _terminationsCount > 0 ) ? RouteState.Valid : RouteState.NoTerminations;
			return mapToRoutingPlanDetail(_routingPlanDetailRow, _baseRoute, _routingPlan, _routingPlanDetailRouteState);
		}

		internal static RoutingPlanDto GetRoutingPlan(Rbr_Db pDb, int pRoutingPlanId) {
			var _routingPlanRow = pDb.RoutingPlanCollection.GetByPrimaryKey(pRoutingPlanId);
			var _callingPlan = CallingPlanManager.GetCallingPlan(pDb, _routingPlanRow.Calling_plan_id);
			return mapToRoutingPlan(_routingPlanRow, _callingPlan);
		}

		internal static RoutingPlanDto[] GetRoutingPlans(Rbr_Db pDb) {
			var _list = new List<RoutingPlanDto>();
			var _routingPlanRows = pDb.RoutingPlanCollection.GetAll();
			foreach (var _routingPlanRow in _routingPlanRows) {
				var _callingPlan = CallingPlanManager.GetCallingPlan(pDb, _routingPlanRow.Calling_plan_id);
				_list.Add(mapToRoutingPlan(_routingPlanRow, _callingPlan));
			}
			return _list.ToArray();
		}

		internal static RoutingPlanDto[] GetRoutingPlans(Rbr_Db pDb, int pCallingPlanId) {
			var _list = new List<RoutingPlanDto>();
			var _callingPlan = CallingPlanManager.GetCallingPlan(pDb, pCallingPlanId);
			var _routingPlanRows = pDb.RoutingPlanCollection.GetByCalling_plan_id(pCallingPlanId);
			foreach (var _routingPlanRow in _routingPlanRows) {
				_list.Add(mapToRoutingPlan(_routingPlanRow, _callingPlan));
			}
			return _list.ToArray();
		}

		internal static TerminationChoiceDto GetTerminationChoice(Rbr_Db pDb, int pTerminationChoiceId) {
			var _terminationChoiceRow = pDb.TerminationChoiceCollection.GetByPrimaryKey(pTerminationChoiceId);
			var _route = CarrierRouteManager.Get(pDb, _terminationChoiceRow.Carrier_route_id);
			return mapToTerminationChoice(_terminationChoiceRow, _route);
		}

		internal static TerminationChoiceDto[] GetAll(Rbr_Db pDb, int pRoutingPlanId, int pRouteId) {
			var _list = new List<TerminationChoiceDto>();
			var _terminationChoiceRows = pDb.TerminationChoiceCollection.GetByRoutingPlanIdRouteId_OrderByPriority(pRoutingPlanId, pRouteId);
			foreach (var _terminationChoiceRow in _terminationChoiceRows) {
				var _route = CarrierRouteManager.Get(pDb, _terminationChoiceRow.Carrier_route_id);
				_list.Add(mapToTerminationChoice(_terminationChoiceRow, _route));
			}
			return _list.ToArray();
		}

		public static TerminationRouteDto[] GetAvailableTerminations(Rbr_Db pDb, int pRouteId) {
			var _availableTerminationRoutes = new List<TerminationRouteDto>();
			var _terminationRouteViewRows = pDb.TerminationRouteViewCollection.GetAvailableByCustomerBaseRouteId(pRouteId, false);
			foreach (var _terminationRouteViewRow in _terminationRouteViewRows) {
				//NOTE: check if [Active]Carrier's [Active]Route has [Active]Endpoints that will support thisroute/dialcode
				var _activeCarrierRouteEPMapCount = pDb.CarrierAcctEPMapCollection.GetActiveCountByCarrierRouteId(_terminationRouteViewRow.Carrier_route_id);
				if (_activeCarrierRouteEPMapCount > 0) {
					//NOTE: as long as Carrier's Status is checked in GetActiveCountByCarrierRouteId, no need to do it here
					_availableTerminationRoutes.Add( new TerminationRouteDto(
																																		_terminationRouteViewRow.Carrier_route_id,
																																		_terminationRouteViewRow.Route_name,
																																		_terminationRouteViewRow.Carrier_acct_id,
																																		_terminationRouteViewRow.Calling_plan_id,
																																		_terminationRouteViewRow.Carrier_acct_name,
																																		0,
																																		_terminationRouteViewRow.PartialCoverage,
																																		_activeCarrierRouteEPMapCount > 0));
				}
			}
			return _availableTerminationRoutes.ToArray();
		}

		#endregion Getters

		#region Sorters
		internal static void SortRoutingPlanDetailByCountryNameAndRouteName(ref RoutingPlanDetailDto[] pRoutingPlanDetails) {
			#region !!! SORTING PROBLEM
			/*
      try to create files(or folders) with these names:

      Australia-Mobile
      Australia-Proper
      Australian Ext. Ter.-Proper

      from first look it is the order they should be sorted, but
      if you sort them in the Win Explorer, the result is (looks like it ingnores "-" during the sort ?):

      Australia-Mobile
      Australian Ext. Ter.-Proper
      Australia-Proper

      Same result if you use GenericComparer for sorting.
      If you return this list from SQL, using ORDER BY, it will be sorted correctly (as in the first example).
      I wonder why...
      Just be aware of cases like this.
    */
			#endregion !!! SORTING PROBLEM

			//this will fix the above problem
			var _sortInfos = new[] { 
								new SortInfo(RoutingPlanDetailDto.CountryName_PropName, ListSortDirection.Ascending), 
								new SortInfo(RoutingPlanDetailDto.IsProper_PropName, ListSortDirection.Descending), //NOTE: Descending here so the Proper (as bool=true) is first
								new SortInfo(RoutingPlanDetailDto.BaseRouteName_PropName, ListSortDirection.Ascending)
			};

			Array.Sort(pRoutingPlanDetails, new GenericComparer(_sortInfos));
		}

		static BaseRouteDto[] sortBaseRoutes(BaseRouteDto[] pRoutes) {
			#region !!! SORTING PROBLEM

			/*
      try to create files(or folders) with these names:

      Australia-Mobile
      Australia-Proper
      Australian Ext. Ter.-Proper

      from first look it is the order they should be sorted, but
      if you sort them in the Win Explorer, the result is (looks like it ingnores "-" during the sort ?):

      Australia-Mobile
      Australian Ext. Ter.-Proper
      Australia-Proper

      Same result if you use GenericComparer for sorting.
      If you return this list from SQL, using ORDER BY, it will be sorted correctly (as in the first example).
      I wonder why...
      Just be aware of cases like this.
    */

			#endregion !!! SORTING PROBLEM

			//this will fix the above problem
			var _sortInfos = new[] {
                             	new SortInfo(BaseRouteDto.CountryName_PropName, ListSortDirection.Ascending), 
                             	new SortInfo(BaseRouteDto.Name_PropName, ListSortDirection.Ascending)
                             };
			Array.Sort(pRoutes, new GenericComparer(_sortInfos));
			return pRoutes;
		}

		internal static RatedRouteDto[] SortRatedRoutes(RatedRouteDto[] pRoutes) {
			#region !!! SORTING PROBLEM
			/*
      try to create files(or folders) with these names:

      Australia-Mobile
      Australia-Proper
      Australian Ext. Ter.-Proper

      from first look it is the order they should be sorted, but
      if you sort them in the Win Explorer, the result is (looks like it ingnores "-" during the sort ?):

      Australia-Mobile
      Australian Ext. Ter.-Proper
      Australia-Proper

      Same result if you use GenericComparer for sorting.
      If you return this list from SQL, using ORDER BY, it will be sorted correctly (as in the first example).
      I wonder why...
      Just be aware of cases like this.
    */
			#endregion !!! SORTING PROBLEM

			//this will fix the above problem
			var _sortInfos = new[] { 
               	new SortInfo(RatedRouteDto.CountryName_PropName, ListSortDirection.Ascending), 
               	new SortInfo(RatedRouteDto.IsProper_PropName, ListSortDirection.Descending), //NOTE: Descending here so the Proper (as bool=true) is first
               	new SortInfo(RatedRouteDto.Name_PropName, ListSortDirection.Ascending)
			};

			Array.Sort(pRoutes, new GenericComparer(_sortInfos));
			return pRoutes;
		}

		internal static RouteRow[] SortRouteRows(RouteRow[] pRouteRows) {
			#region !!! SORTING PROBLEM
			/*
      try to create files(or folders) with these names:

      Australia-Mobile
      Australia-Proper
      Australian Ext. Ter.-Proper

      from first look it is the order they should be sorted, but
      if you sort them in the Win Explorer, the result is (looks like it ingnores "-" during the sort ?):

      Australia-Mobile
      Australian Ext. Ter.-Proper
      Australia-Proper

      Same result if you use GenericComparer for sorting.
      If you return this list from SQL, using ORDER BY, it will be sorted correctly (as in the first example).
      I wonder why...
      Just be aware of cases like this.
    */
			#endregion !!! SORTING PROBLEM

			//this will fix the above problem
			var _sortInfos = new[] { 
                             	new SortInfo(RouteRow.CountryName_PropName, ListSortDirection.Ascending), 
                             	new SortInfo(RouteRow.IsProper_PropName, ListSortDirection.Descending), //NOTE: Descending here so the Proper (as bool=true) is first
                             	new SortInfo(RouteRow.BreakoutName_PropName, ListSortDirection.Ascending)
			};

			Array.Sort(pRouteRows, new GenericComparer(_sortInfos));
			return pRouteRows;
		}

		#endregion

		#region Actions

		//internal static void Add(Rbr_Db pDb, RouteRow pRouteRow/*, DialCodeRow[] pDialCodeRows*/) {
		//  pDb.RouteCollection.Insert(pRouteRow);
		//}

		//internal static void Update(Rbr_Db pDb, RouteRow pRouteRow/*, DialCodeRow[] pDialCodeRows*/) {
		//  pDb.RouteCollection.Update(pRouteRow);
		//}

		internal static void AddBaseRoute(Rbr_Db pDb, BaseRouteDto pBaseRoute) {
			var _routeRow = MapToRouteRow(pBaseRoute);
			pDb.RouteCollection.Insert(_routeRow);
			pBaseRoute.BaseRouteId = _routeRow.Route_id;

			if (pBaseRoute.IsProper) {
				addDialCodeForProperBaseRoute(pDb, pBaseRoute);
			}
		}

		//NOTE: no more TermChoices deletion on DialCode change/delete, 
		//we will run BG process to show/mark PartialCovarage and NoCovarage TermChoces in the GUI
		internal static void UpdateBaseRoute(Rbr_Db pDb, BaseRouteDto pBaseRoute) {
			var _routeRow = MapToRouteRow(pBaseRoute);
			pDb.RouteCollection.Update(_routeRow);
			//pDb.AddChangedObject(new BaseRouteKey(TxType.Delete, 0, 0, _routeRow.Route_id));
		}

		internal static void DeleteBaseRoute(Rbr_Db pDb, BaseRouteDto pBaseRoute) {
			//NOTE: NO DELETE, Archice only
			pBaseRoute.BaseStatus = Status.Archived;
			UpdateBaseRoute(pDb, pBaseRoute);
		}

		internal static int AddRoutingPlan(Rbr_Db pDb, RoutingPlanDto pRoutingPlan) {
			var _routingPlanRow = mapToRoutingPlanRow(pRoutingPlan);
			pDb.RoutingPlanCollection.Insert(_routingPlanRow);
			return _routingPlanRow.Routing_plan_id;
		}

		internal static void UpdateRoutingPlan(Rbr_Db pDb, RoutingPlanDto pRoutingPlan) {
			var _routingPlanRow = mapToRoutingPlanRow(pRoutingPlan);
			pDb.RoutingPlanCollection.Update(_routingPlanRow);
		}

		internal static void DeleteRoutingPlan(Rbr_Db pDb, RoutingPlanDto pRoutingPlan) {
			var _routingPlanRow = mapToRoutingPlanRow(pRoutingPlan);

			//TODO: NEW DAL 
			//pDb.TerminationChoiceCollection.DeleteByRoutingPlanId(pRoutingPlan.RoutingPlanId);
			pDb.RoutingPlanDetailCollection.DeleteByRouting_plan_id(_routingPlanRow.Routing_plan_id);
			pDb.RoutingPlanCollection.Delete(_routingPlanRow);
		}

		internal static void AddRoutingPlanDetails(Rbr_Db pDb, RoutingPlanDto pRoutingPlan, int[] pSelectedBaseRouteIds, RoutingAlgorithmType pDefaultRoutingAlgorithmType) {
			if (pRoutingPlan != null && pSelectedBaseRouteIds != null && pSelectedBaseRouteIds.Length > 0) {
				foreach (var _baseRouteId in pSelectedBaseRouteIds) {
					var _routingPlanDetailRow = new RoutingPlanDetailRow();
					_routingPlanDetailRow.Routing_plan_id = pRoutingPlan.RoutingPlanId;
					_routingPlanDetailRow.Route_id = _baseRouteId;
					_routingPlanDetailRow.Algorithm = pDefaultRoutingAlgorithmType;
					AddRoutingPlanDetailRow(pDb, _routingPlanDetailRow);
				}
			}
		}

		internal static void AddRoutingPlanDetail(Rbr_Db pDb, RoutingPlanDetailDto pRoutingPlanDetail) {
			var _routingPlanDetailRow = mapToRoutingPlanDetailRow(pRoutingPlanDetail);
			AddRoutingPlanDetailRow(pDb, _routingPlanDetailRow);
		}

		internal static void AddRoutingPlanDetailRow(Rbr_Db pDb, RoutingPlanDetailRow pRoutingPlanDetailRow) {
			pDb.RoutingPlanDetailCollection.Insert(pRoutingPlanDetailRow);
		}

		internal static void UpdateRoutingPlanDetail(Rbr_Db pDb, RoutingPlanDetailDto pRoutingPlanDetail) {
			var _routingPlanDetailRow = mapToRoutingPlanDetailRow(pRoutingPlanDetail);
			pDb.RoutingPlanDetailCollection.Update(_routingPlanDetailRow);
		}

		internal static void DeleteRoutingPlanDetail(Rbr_Db pDb, RoutingPlanDetailDto pRoutingPlanDetail) {
			var _routingPlanDetailRow = mapToRoutingPlanDetailRow(pRoutingPlanDetail);
			DeleteTerminationChoices(pDb, _routingPlanDetailRow.Routing_plan_id, _routingPlanDetailRow.Route_id);
			//TODO: NEW DAL - ??? should we delete LCRBlackList auto, or throw?
			pDb.LCRBlackListCollection.DeleteByRouting_plan_id_Route_id(_routingPlanDetailRow.Routing_plan_id, _routingPlanDetailRow.Route_id);
			pDb.RoutingPlanDetailCollection.Delete(_routingPlanDetailRow);
		}

		public static void CloneRoutingPlanDetails(Rbr_Db pDb, RoutingPlanDto pNewRoutingPlan, RoutingPlanDto pRoutingPlanToClone) {
			//1. get Details for existing RoutingPlan
			var _existingRoutingPlanDetailRows = pDb.RoutingPlanDetailCollection.GetByRouting_plan_id(pRoutingPlanToClone.RoutingPlanId);
			foreach (var _existingRoutingPlanDetailRow in _existingRoutingPlanDetailRows) {
				//1.1 clone/insert RoutingPlanDetail for a new RoutingPlan
				var _newRoutingPlanDetail = new RoutingPlanDetailRow();
				_newRoutingPlanDetail.Routing_plan_id = pNewRoutingPlan.RoutingPlanId;
				_newRoutingPlanDetail.Route_id = _existingRoutingPlanDetailRow.Route_id;
				_newRoutingPlanDetail.Routing_algorithm = _existingRoutingPlanDetailRow.Routing_algorithm;
				pDb.RoutingPlanDetailCollection.Insert(_newRoutingPlanDetail);

				//1.2 get TermChoices for existing RoutingPlanDetail
				var _existingTermChoiceRows = pDb.TerminationChoiceCollection.GetByRoutingPlanIdRouteId_OrderByPriority(_existingRoutingPlanDetailRow.Routing_plan_id, _existingRoutingPlanDetailRow.Route_id);
				foreach (var _existingTermChoiceRow in _existingTermChoiceRows) {
					//1.2.1 clone/insert TermChoice for a new RoutingPlan
					var _newTermChoiceRow = new TerminationChoiceRow();
					_newTermChoiceRow.Routing_plan_id = _newRoutingPlanDetail.Routing_plan_id;
					_newTermChoiceRow.Route_id = _newRoutingPlanDetail.Route_id;
					_newTermChoiceRow.Priority = _existingTermChoiceRow.Priority;
					_newTermChoiceRow.Carrier_route_id = _existingTermChoiceRow.Carrier_route_id;
					pDb.TerminationChoiceCollection.Insert(_newTermChoiceRow);
				}

				////1.3 get LCRBlackList for existing RoutingPlanDetail
				//LCRBlackListRow[] _existingLCRBlackListRows = pDb.LCRBlackListCollection.GetByRouting_plan_id_Route_id(_existingRoutingPlanDetailRow.Routing_plan_id, _existingRoutingPlanDetailRow.Route_id);
				//foreach (LCRBlackListRow _existingLCRBlackListRow in _existingLCRBlackListRows) {
				//  //1.3.1 clone/insert LCRBlackList for a new RoutingPlan
				//  LCRBlackListRow _newLCRBlackListRow = new LCRBlackListRow();
				//  _newLCRBlackListRow.Routing_plan_id = _newRoutingPlanDetail.Routing_plan_id;
				//  _newLCRBlackListRow.Route_id = _newRoutingPlanDetail.Route_id;
				//  _newLCRBlackListRow.Carrier_acct_id = _existingLCRBlackListRow.Carrier_acct_id;
				//  pDb.LCRBlackListCollection.Insert(_newLCRBlackListRow);
				//}
			}
		}

		internal static void AddTerminationChoice(Rbr_Db pDb, int pRoutingPlanId, int pRouteId, int pCarrierRouteId) {
			var _terminationChoiceRow = new TerminationChoiceRow();
			_terminationChoiceRow.Routing_plan_id = pRoutingPlanId;
			_terminationChoiceRow.Route_id = pRouteId;
			_terminationChoiceRow.Carrier_route_id = pCarrierRouteId;
			_terminationChoiceRow.Priority = 0;
			_terminationChoiceRow.Version = 0;
			pDb.TerminationChoiceCollection.Insert(_terminationChoiceRow);
//			updatePrioritiesManual(pDb, pRoutingPlanId, pRouteId);
		}

		public static void UpdateTerminationChoices(Rbr_Db pDb, TerminationChoiceDto[] pTerminationChoices) {
			if (pTerminationChoices == null || pTerminationChoices.Length == 0) {
				return;
			}

			pDb.TerminationChoiceCollection.DeleteByRouting_plan_id_Route_id(pTerminationChoices[0].RoutingPlanId, pTerminationChoices[0].RouteId);

			foreach (var _terminationChoice in pTerminationChoices) {
				pDb.TerminationChoiceCollection.Insert(mapToTerminationChoiceRow(_terminationChoice));
			}

//			updatePrioritiesManual(pDb, pTerminationChoices[0].RoutingPlanId, pTerminationChoices[0].RouteId);
		}

		internal static void DeleteTerminationChoices(Rbr_Db pDb, int pRoutingPlanId, int pRouteId) {
			pDb.TerminationChoiceCollection.DeleteByRouting_plan_id_Route_id(pRoutingPlanId, pRouteId);
//			updatePrioritiesManual(pDb, pRoutingPlanId, pRouteId);
		}

		internal static void DeleteTerminationChoice(Rbr_Db pDb, TerminationChoiceDto pTerminationChoice) {
			pDb.TerminationChoiceCollection.DeleteByPrimaryKey(pTerminationChoice.TerminationChoiceId);
			updatePrioritiesManual(pDb, pTerminationChoice.RoutingPlanId, pTerminationChoice.RouteId);
		}

		#endregion Actions

		#region mappers

		static RoutingPlanDto mapToRoutingPlan(RoutingPlanRow_Base pRoutingPlanRow, CallingPlanDto pCallingPlan) {
			if (pRoutingPlanRow == null) {
				return null;
			}

			var _routingPlan = new RoutingPlanDto();
			_routingPlan.RoutingPlanId = pRoutingPlanRow.Routing_plan_id;
			_routingPlan.Name = pRoutingPlanRow.Name;
			_routingPlan.CallingPlan = pCallingPlan;
			_routingPlan.VirtualSwitchId = pRoutingPlanRow.Virtual_switch_id;

			return _routingPlan;
		}

		static RoutingPlanRow mapToRoutingPlanRow(RoutingPlanDto pRoutingPlan) {
			if (pRoutingPlan == null) {
				return null;
			}

			var _routingPlanRow = new RoutingPlanRow();
			_routingPlanRow.Routing_plan_id = pRoutingPlan.RoutingPlanId;
			_routingPlanRow.Name = pRoutingPlan.Name;
			_routingPlanRow.Calling_plan_id = pRoutingPlan.CallingPlanId;
			_routingPlanRow.Virtual_switch_id = pRoutingPlan.VirtualSwitchId;

			return _routingPlanRow;
		}

		static RoutingPlanDetailRow mapToRoutingPlanDetailRow(RoutingPlanDetailDto pRoutingPlanDetail) {
			if (pRoutingPlanDetail == null) {
				return null;
			}

			var _routingPlanDetailRow = new RoutingPlanDetailRow();
			_routingPlanDetailRow.Routing_plan_id = pRoutingPlanDetail.RoutingPlanId;
			_routingPlanDetailRow.Route_id = pRoutingPlanDetail.BaseRouteId;
			_routingPlanDetailRow.Algorithm = pRoutingPlanDetail.Algorithm;

			return _routingPlanDetailRow;
		}

		static RoutingPlanDetailDto mapToRoutingPlanDetail(RoutingPlanDetailRow pRoutingPlanDetailRow, BaseRouteDto pBaseRoute, RoutingPlanDto pRoutingPlan, RouteState pRoutingPlanDetailRouteState) {
			if (pRoutingPlanDetailRow == null) {
				return null;
			}

			var _routingPlanDetail = new RoutingPlanDetailDto();
			_routingPlanDetail.RoutingPlan = pRoutingPlan;
			_routingPlanDetail.BaseRoute = pBaseRoute;
			_routingPlanDetail.Algorithm = pRoutingPlanDetailRow.Algorithm;

			_routingPlanDetail.RouteState = pRoutingPlanDetailRouteState;

			return _routingPlanDetail;
		}

		//internal static BaseRouteDto[] MapToBaseRoutes(Rbr_Db pDb, RouteRow[] pRouteRows, CountryRow[] pCountryRows, CallingPlanRow pCallingPlanRow) {
		//  ArrayList _list = new ArrayList();
		//  if (pRouteRows != null) {
		//    foreach (RouteRow _routeRow in pRouteRows) {
		//      CountryRow _countryRow = getCountry(_routeRow.Country_id, pCountryRows);
		//      int _dialCodeCount = pDb.DialCodeCollection.GetCount( /*_routeRow.Calling_plan_id,*/ _routeRow.Route_id);
		//      RouteState _routeState = ( _dialCodeCount > 0 ) ? RouteState.Valid : RouteState.NoDialCodes;
		//      BaseRouteDto _baseRoute = MapToBaseRoute(_routeRow, _countryRow, pCallingPlanRow, _routeState);
		//      _list.Add(_baseRoute);
		//    }
		//  }
		//  return (BaseRouteDto[])_list.ToArray(typeof(BaseRouteDto));
		//}

		public static BaseRouteDto MapToBaseRoute(Rbr_Db pDb, RouteRow pRouteRow) {//}, CountryRow pCountryRow, CallingPlanRow pCallingPlanRow, RouteState pRouteState) {
			if (pRouteRow == null) {
				return null;
			}

			var _baseRoute = new BaseRouteDto();
			_baseRoute.BaseRouteId = pRouteRow.Route_id;
			_baseRoute.Name = pRouteRow.Name;
			_baseRoute.BaseStatus = pRouteRow.RouteStatus;
			_baseRoute.CallingPlan = CallingPlanManager.GetCallingPlan(pDb, pRouteRow.Calling_plan_id);
			_baseRoute.Country = CallingPlanManager.GetCountry(pDb, pRouteRow.Country_id);
			_baseRoute.RoutingNumber = pRouteRow.Routing_number;
			_baseRoute.Version = pRouteRow.Version;
			_baseRoute.RouteState = RouteState.Valid;

			return _baseRoute;
		}

		static BaseRouteDto mapToBaseRoute(RouteRow pRouteRow, CountryDto pCountry, CallingPlanDto pCallingPlan, RouteState pRouteState) {
			if (pRouteRow == null) {
				return null;
			}

			var _baseRoute = new BaseRouteDto();
			_baseRoute.BaseRouteId = pRouteRow.Route_id;
			_baseRoute.Name = pRouteRow.Name;
			_baseRoute.BaseStatus = pRouteRow.RouteStatus;
			_baseRoute.CallingPlan = pCallingPlan;
			_baseRoute.Country = pCountry;
			_baseRoute.RoutingNumber = pRouteRow.Routing_number;
			_baseRoute.Version = pRouteRow.Version;
			_baseRoute.RouteState = pRouteState;

			return _baseRoute;
		}

		//static RouteRow[] mapToRouteRows(BaseRouteDto[] pBaseRoutes) {
		//  ArrayList _list = new ArrayList();
		//  if (pBaseRoutes != null) {
		//    foreach (BaseRouteDto _route in pBaseRoutes) {
		//      RouteRow _routeRow = MapToRouteRow(_route);
		//      _list.Add(_routeRow);
		//    }
		//  }
		//  return (RouteRow[])_list.ToArray(typeof(RouteRow));
		//}

		internal static RouteRow MapToRouteRow(BaseRouteDto pBaseRoute) {
			if (pBaseRoute == null) {
				return null;
			}

			var _routeRow = new RouteRow();
			_routeRow.Route_id = pBaseRoute.BaseRouteId;
			_routeRow.Name = pBaseRoute.Name;
			_routeRow.RouteStatus = pBaseRoute.BaseStatus;
			_routeRow.Calling_plan_id = pBaseRoute.CallingPlanId;
			_routeRow.Country_id = pBaseRoute.CountryId;
			_routeRow.Routing_number = pBaseRoute.RoutingNumber;
			_routeRow.Version = pBaseRoute.Version;

			return _routeRow;
		}

		static TerminationChoiceRow mapToTerminationChoiceRow(TerminationChoiceDto pTerminationChoice) {
			if (pTerminationChoice == null) {
				return null;
			}

			var _terminationChoiceRow = new TerminationChoiceRow();
			_terminationChoiceRow.Termination_choice_id = pTerminationChoice.TerminationChoiceId;
			_terminationChoiceRow.Routing_plan_id = pTerminationChoice.RoutingPlanId;
			_terminationChoiceRow.Route_id = pTerminationChoice.RouteId;
			_terminationChoiceRow.Priority = pTerminationChoice.Priority;

			_terminationChoiceRow.Carrier_route_id = pTerminationChoice.CarrierRouteId;
			_terminationChoiceRow.Version = pTerminationChoice.Version;

			return _terminationChoiceRow;
		}

		static void addDialCodeForProperBaseRoute(Rbr_Db pDb, BaseRouteDto pBaseRoute) {
			if (pBaseRoute.IsProper) {
				if (pBaseRoute.Country.CountryCode == 1) {
					//no default code for countries with country code 1 (Canada, USA, ???)
				}
				else {
					//TODO: should we try to find existing, and if found reassign to Proper Route
					var _properDialCode = CallingPlanManager.GetDialCode(pDb, pBaseRoute.CallingPlanId, pBaseRoute.Country.CountryCode);
					if (_properDialCode == null) {
						_properDialCode = new DialCodeDto();
					}
					else {
						CallingPlanManager.DeleteDialCode(pDb, _properDialCode);
					}
					_properDialCode.CallingPlanId = pBaseRoute.CallingPlanId;
					_properDialCode.Code = pBaseRoute.Country.CountryCode;
					_properDialCode.BaseRouteId = pBaseRoute.BaseRouteId;

					CallingPlanManager.AddDialCode(pDb, _properDialCode);
				}
			}
		}

		static List<BaseRouteDto> getBaseRouteList(Rbr_Db pDb, CallingPlanDto pCallingPlan, IEnumerable<RouteRow> pRouteRows) {
			var _routeList = new List<BaseRouteDto>();
			foreach (var _routeRow in pRouteRows) {
				var _country = CallingPlanManager.GetCountry(pDb, _routeRow.Country_id);

				var _dialCodeCount = CallingPlanManager.GetDialCodeCount(pDb, _routeRow.Route_id);
				var _routeState = ( _dialCodeCount > 0 ) ? RouteState.Valid : RouteState.NoDialCodes;

				var _route = mapToBaseRoute(_routeRow, _country, pCallingPlan, _routeState);
				_routeList.Add(_route);
			}
			return _routeList;
		}

		static void updatePrioritiesManual(Rbr_Db_Base pDb, int pRoutingPlanId, int pRouteId) {
			var _terminationChoiceRows = pDb.TerminationChoiceCollection.GetByRoutingPlanIdRouteId_OrderByPriority(pRoutingPlanId, pRouteId);
			var _priority = byte.MaxValue;
			foreach (var _termChoice in _terminationChoiceRows) {
				_termChoice.Priority = _priority--;
				pDb.TerminationChoiceCollection.UpdateInternal(_termChoice);
			}

			_priority = 0;
			foreach (var _termChoice in _terminationChoiceRows) {
				_termChoice.Priority = ++_priority;
				pDb.TerminationChoiceCollection.UpdateInternal(_termChoice);
			}
		}

		static TerminationChoiceDto mapToTerminationChoice(TerminationChoiceRow_Base pTerminationChoiceRow, RatedRouteDto pCarrierRoute) {
			if (pTerminationChoiceRow == null) {
				return null;
			}

			var _terminationChoice = new TerminationChoiceDto();
			_terminationChoice.TerminationChoiceId = pTerminationChoiceRow.Termination_choice_id;
			_terminationChoice.RoutingPlanId = pTerminationChoiceRow.Routing_plan_id;
			_terminationChoice.RouteId = pTerminationChoiceRow.Route_id;
			_terminationChoice.Priority = pTerminationChoiceRow.Priority;

			_terminationChoice.CarrierBaseRouteStatus = pCarrierRoute.BaseRoute.BaseStatus;
			_terminationChoice.CarrierRouteId = pCarrierRoute.RatedRouteId;
			_terminationChoice.CarrierRouteName = pCarrierRoute.Name;
			_terminationChoice.CarrierRouteStatus = pCarrierRoute.Status;
			_terminationChoice.CarrierRouteState = pCarrierRoute.RouteState;
			_terminationChoice.CarrierAcctName = pCarrierRoute.AccountName;
			_terminationChoice.CarrierAcctStatus = pCarrierRoute.AccountStatus;

			_terminationChoice.Version = pTerminationChoiceRow.Version;

			return _terminationChoice;
		}

		#endregion privates

	}
}
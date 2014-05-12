using System.Collections;
using System.Collections.Generic;
using Timok.Core;
using Timok.Rbr.Core.Config;
using Timok.Rbr.DAL.RbrDatabase;
using Timok.Rbr.DAL.RbrDatabase.Base;
using Timok.Rbr.DTO;

namespace Timok.Rbr.BLL.Managers {
	internal static class CallingPlanManager {
		#region Getters 

		internal static CountryDto[] GetCountries(Rbr_Db pDb) { return mapToCountries(pDb.CountryCollection.GetAll()); }

		internal static CountryDto[] GetCountries(Rbr_Db pDb, short pAccountId, int pCallingPlanId, ViewContext pContext) {
			return mapToCountries(pDb.CountryCollection.Get(pAccountId, pCallingPlanId, pContext));
		}

		internal static CountryDto GetCountry(Rbr_Db pDb, int pCountryId) {
			return mapToCountry(pDb.CountryCollection.GetByPrimaryKey(pCountryId));
		}

		internal static CountryDto GetCountry(Rbr_Db pDb, string pName) {
			return mapToCountry(pDb.CountryCollection.GetByName(pName));
		}

		internal static CountryDto GetByCountryCode(Rbr_Db pDb, int pCountryCode) {
			return mapToCountry(pDb.CountryCollection.GetByCountryCode(pCountryCode));
		}

		internal static CountryDto[] GetCountriesByCallingPlan(Rbr_Db pDb, int pCallingPlanId) {
			return mapToCountries(pDb.CountryCollection.GetByCallingPlanId(pCallingPlanId));
		}

		internal static CountryDto[] GetCountriesByRoutingPlan(Rbr_Db pDb, int pRoutingPlanId) {
			return mapToCountries(pDb.CountryCollection.GetByRoutingPlanId(pRoutingPlanId));
		}

		//internal static CountryDto[] GetCountriesByRoutingPlan(Rbr_Db pDb, int pRoutingPlanId) {
		//  var _routingPlanDto = RoutingManager.GetRoutingPlan(pDb, pRoutingPlanId);
		//  //var _routes = RoutingManager.GetBaseRoutesByRoutingPlanId(pDb, _routingPlanDto.CallingPlanId, pRoutingPlanId);
		//  return mapToCountries(pDb.CountryCollection.GetByCallingPlanId(_routingPlanDto.CallingPlanId));
		//  //var _countryDtoDictionary = new Dictionary<int, CountryDto>();
		//  //foreach (var _route in _routes) {
		//  //  _countryDtoDictionary.Add(_route.CountryId, null);
		//  //}
		//  //var _countryDtoList = mapToCountries(pDb.CountryCollection.GetByCallingPlanId(_routingPlanDto.CallingPlanId));

		//  //foreach (var _country in _countryDtoList) {
		//  //  if (_countryDtoDictionary.ContainsKey(_country.CountryId)) {
		//  //    _countryDtoDictionary[_country.CountryId] = _country;
		//  //  }
		//  //}

		//  //var _countryDtoArray = new CountryDto[_countryDtoDictionary.Count];
		//  //_countryDtoDictionary.Values.CopyTo(_countryDtoArray, 0);
		//  //return _countryDtoArray;
		//}

		//internal static CountryDto[] GetCountriesByRoutingPlanOld(Rbr_Db pDb, int pRoutingPlanId)
		//{
		//  var _routingPlanDetails = RoutingManager.GetRoutingPlanDetails(pDb, pRoutingPlanId);
		//  var _countryDtoList = new List<CountryDto>();
		//  var _countryDtoDictionary = new Dictionary<int, CountryDto>();

		//  foreach (var _routingPlanDetail in _routingPlanDetails)
		//  {
		//    if (_countryDtoDictionary.ContainsKey(_routingPlanDetail.CountryId))
		//    {
		//      continue;
		//    }
		//    _countryDtoDictionary.Add(_routingPlanDetail.CountryId, null);
		//    _countryDtoList.Add(mapToCountry(pDb.CountryCollection.GetByCountryId(_routingPlanDetail.CountryId)));
		//  }

		//  return _countryDtoList.ToArray();
		//}

		internal static CountryDto[] GetUnusedCountries(Rbr_Db pDb, int pCallingPlanId) { return mapToCountries(pDb.CountryCollection.GetUnused(pCallingPlanId)); }

		internal static CallingPlanDto[] GetCallingPlans(Rbr_Db pDb) { return mapToCallingPlans(pDb.CallingPlanCollection.GetAll()); }

		internal static CallingPlanRow GetCallingPlanRow(Rbr_Db pDb, int pCallingPlanId) { return pDb.CallingPlanCollection.GetByPrimaryKey(pCallingPlanId); }

		internal static CallingPlanDto GetCallingPlan(Rbr_Db pDb, int pCallingPlanId) { return mapToCallingPlan(pDb.CallingPlanCollection.GetByPrimaryKey(pCallingPlanId)); }

		internal static int GetCallingPlanUsageCount(Rbr_Db pDb, int pCallingPlanId) {
			int _count = pDb.RoutingPlanCollection.GetCountByCallingPlanId(pCallingPlanId);
			if (_count <= 0) {
				_count = pDb.ServiceCollection.GetCountByCallingPlanId(pCallingPlanId);
			}
			if (_count <= 0) {
				_count = pDb.CustomerAcctCollection.GetCountByRetailCallingPlanId(pCallingPlanId);
			}
			if (_count <= 0) {
				_count = pDb.CarrierAcctCollection.GetCountByCallingPlanId(pCallingPlanId);
			}
			return _count;
		}

		internal static int GetDialCodeCount(Rbr_Db pDb, int pRouteId) { return pDb.DialCodeCollection.GetCount(pRouteId); }

		internal static DialCodeDto GetDialCode(Rbr_Db pDb, int pCallingPlanId, long pDialCode) { return mapToDialCode(pDb.DialCodeCollection.GetByPrimaryKey(pCallingPlanId, pDialCode)); }

		internal static DialCodeDto[] GetByDialedNumber(Rbr_Db pDb, string pDialCode) {
			var _dialCodeRows = pDb.DialCodeCollection.GetByDialedNumberFromCarriers(pDialCode);

			var _dialCodes = new List<DialCodeDto>();
			foreach (var _dialCodeRow in _dialCodeRows) {
				_dialCodes.Add(mapToDialCode(_dialCodeRow));
			}
			return _dialCodes.ToArray();
		}

		public static DialCodeDto[] GetDialCodes(Rbr_Db pDb, int pCallingPlanId, int pRouteId) {
			var _list = new ArrayList();
			_list.AddRange(pDb.DialCodeCollection.GetByCallingPlanIdRouteId(pCallingPlanId, pRouteId));
			_list.Sort(new ObjectComparer(DialCodeRow.DialCodeString_PropName, false));
			return mapToDialCodes((DialCodeRow[]) _list.ToArray(typeof (DialCodeRow)));
		}

		internal static DialCodeDto GetFirstByCallingPlanIdDialedNumber(Rbr_Db pDb, int pCallingPlanId, long pDialedNumber) { return mapToDialCode(pDb.DialCodeCollection.GetFirstByCallingPlanIdDialedNumber(pCallingPlanId, pDialedNumber.ToString())); }

		internal static DialCodeDto GetFirstByCallingPlanIdRouteId(Rbr_Db pDb, int pCallingPlanId, int pRouteId) { return mapToDialCode(pDb.DialCodeCollection.GetFirstByCallingPlanIdRouteId(pCallingPlanId, pRouteId)); }

		internal static DialCodeDto[] GetByRouteIdPaged(Rbr_Db pDb, int pRouteId, int pPageNumber, int pPageSize, out int pTotalCount) {
			var _list = new ArrayList();
			_list.AddRange(pDb.DialCodeCollection.GetByRouteIdPaged(pRouteId, pPageNumber, pPageSize, out pTotalCount));
			_list.Sort(new ObjectComparer(DialCodeRow.DialCodeString_PropName, false));
			return mapToDialCodes((DialCodeRow[]) _list.ToArray(typeof (DialCodeRow)));
		}

		internal static DialCodeDto[] GetByCallingPlanIdCountryIdPaged(Rbr_Db pDb, int pCallingPlanId, int pCountryId, int pPageNumber, int pPageSize, out int pTotalCount) {
			var _list = new ArrayList();
			_list.AddRange(pDb.DialCodeCollection.GetByCallingPlanIdCountryIdPaged(pCallingPlanId, pCountryId, pPageNumber, pPageSize, out pTotalCount));
			_list.Sort(new ObjectComparer(DialCodeRow.DialCodeString_PropName, false));
			return mapToDialCodes((DialCodeRow[]) _list.ToArray(typeof (DialCodeRow)));
		}

		internal static int GetRowNumber(Rbr_Db pDb, int pCallingPlanId, int pRouteId, long pDialCode) { return pDb.DialCodeCollection.GetRowNumber(pCallingPlanId, pRouteId, pDialCode); }

		#endregion Getters

		#region Validators

		internal static bool Validate(Rbr_Db pDb, int pCountryCode, int pExcludingCountryId, out string pMsg) {
			pMsg = string.Empty;
			var _existing = pDb.CountryCollection.GetIsCovered(pCountryCode, pExcludingCountryId);

			if (_existing != null && _existing.Length > 0) {
				pMsg = "Country Code already covered by existing Country [" + _existing[0].Name + "]";
				return false;
			}

			_existing = pDb.CountryCollection.GetWillCover(pCountryCode, pExcludingCountryId);
			if (_existing != null && _existing.Length > 0) {
				pMsg = "Country Code covers at least one existing Country [" + _existing[0].Name + "]";
				return false;
			}
			return true;
		}

		internal static bool Validate(Rbr_Db pDb, string pCountryName, out string pMsg) {
			pMsg = string.Empty;
			var _existing = pDb.CountryCollection.GetByName(pCountryName);

			if (_existing != null) {
				pMsg = "Country with the same Name already exists.";
				return false;
			}
			return true;
		}

		#endregion Validators

		#region Actions

		internal static int AddCountry(Rbr_Db pDb, CountryDto pCountry) {
			if (pCountry.CountryCode.ToString().StartsWith("1") && pCountry.CountryCode > 1) {
				pCountry.CountryCode = 1;
			}

			var _countryRow = mapToCountryRow(pCountry);
			pDb.CountryCollection.Insert(_countryRow);
			return _countryRow.Country_id;
		}

		internal static void UpdateCountry(Rbr_Db pDb, CountryDto pCountry) {
			var _original = pDb.CountryCollection.GetByPrimaryKey(pCountry.CountryId);
			var _countryRow = mapToCountryRow(pCountry);
			pDb.CountryCollection.Update(_countryRow);

			if (_countryRow.IsNameChanged(_original)) {
				//update names for All Country Routes (for ALL Services)
				var _routeRows = RoutingManager.GetByCountryId(pDb, _countryRow.Country_id);
				foreach (var _routeRow in _routeRows) {
					_routeRow.Name = _countryRow.Name + AppConstants.SubRouteSeparator + _routeRow.BreakoutName;
					RoutingManager.UpdateBaseRoute(pDb, RoutingManager.MapToBaseRoute(pDb, _routeRow));
				}
			}
			//pDb.AddChangedObject(new BaseRouteKey(TxType.Delete, 0, _countryRow.Country_id, 0));
		}

		internal static void DeleteCountry(Rbr_Db pDb, CountryDto pCountry) {
			//NOTE: no delete, just archive for Country and Routes
			pCountry.Status = Status.Archived;

			var _countryRow = mapToCountryRow(pCountry);
			var _routeRows = pDb.RouteCollection.GetByCountry_id(_countryRow.Country_id);
			foreach (var _routeRow in _routeRows) {
				_routeRow.RouteStatus = Status.Archived;
				RoutingManager.UpdateBaseRoute(pDb, RoutingManager.MapToBaseRoute(pDb, _routeRow));
			}
			pDb.CountryCollection.Update(_countryRow);
			//pDb.AddChangedObject(new BaseRouteKey(TxType.Delete, 0, _countryRow.Country_id, 0));
		}

		internal static void AddCallingPlan(Rbr_Db pDb, CallingPlanDto pCallingPlan) {
			pCallingPlan.VirtualSwitchId = AppConstants.DefaultVirtualSwitchId;

			var _callingPlanRow = mapToCallingPlanRow(pCallingPlan);

			pDb.CallingPlanCollection.Insert(_callingPlanRow);
			pCallingPlan.CallingPlanId = _callingPlanRow.Calling_plan_id;
			pCallingPlan.Version = _callingPlanRow.Version;
		}

		internal static void UpdateCallingPlan(Rbr_Db pDb, CallingPlanDto pCallingPlan) {
			var _callingPlanRow = mapToCallingPlanRow(pCallingPlan);
			pDb.CallingPlanCollection.Update(_callingPlanRow);
			//pDb.AddChangedObject(new CallingPlanKey(TxType.Delete, _callingPlanRow.Calling_plan_id));
			pCallingPlan.Version = _callingPlanRow.Version;
		}

		internal static void DeleteCallingPlan(Rbr_Db pDb, CallingPlanDto pCallingPlan) {
			var _callingPlanRow = mapToCallingPlanRow(pCallingPlan);
			pDb.DialCodeCollection.DeleteByCalling_plan_id(_callingPlanRow.Calling_plan_id);
			pDb.RouteCollection.DeleteByCalling_plan_id(_callingPlanRow.Calling_plan_id);
			pDb.CallingPlanCollection.Delete(_callingPlanRow);
			//pDb.AddChangedObject(new CallingPlanKey(TxType.Delete, _callingPlanRow.Calling_plan_id));
		}

		internal static void AddCountriesToCallingPlan(Rbr_Db pDb, CallingPlanDto pCallingPlan, CountryDto[] pSelectedCountries) {
			foreach (var _country in pSelectedCountries) {
				var _properBaseRoute = new BaseRouteDto
				                       	{
				                       		BaseStatus = Status.Active,
				                       		CallingPlan = pCallingPlan,
				                       		Country = _country,
				                       		Name = string.Concat(_country.Name, AppConstants.SubRouteSeparator, AppConstants.ProperNameSuffix),
				                       		Version = 0
				                       	};
				RoutingManager.AddBaseRoute(pDb, _properBaseRoute);
			}
		}

		internal static void AddDialCode(Rbr_Db pDb, DialCodeDto pDialCode) { pDb.DialCodeCollection.Insert(mapToDialCodeRow(pDialCode)); }

		internal static void UpdateDialCode(Rbr_Db pDb, DialCodeRow pDialCodeRow) {
			pDb.DialCodeCollection.Update(pDialCodeRow);
			//pDb.AddChangedObject(new BaseRouteKey(TxType.Delete, 0, 0, pDialCodeRow.Route_id));
		}

		internal static void DeleteDialCode(Rbr_Db pDb, DialCodeDto pDialCode) {
			pDb.DialCodeCollection.Delete(mapToDialCodeRow(pDialCode));
			//pDb.AddChangedObject(new BaseRouteKey(TxType.Delete, 0, 0, pDialCode.BaseRouteId));
		}

		internal static void MoveDialCode(Rbr_Db pDb, DialCodeDto pDialCode, RouteRow pToRouteRow) {
			//NOTE: no more TermChoices deletion on DialCode change/delete, 
			//we will run BG process to show/mark PartialCovarage and NoCovarage TermChoces in the GUI
			var _dialCodeRow = mapToDialCodeRow(pDialCode);
			_dialCodeRow.Route_id = pToRouteRow.Route_id;
			UpdateDialCode(pDb, _dialCodeRow);
		}

		#endregion Actions

		#region Mappers

		static CallingPlanDto[] mapToCallingPlans(IEnumerable<CallingPlanRow> pCallingPlans) {
			var _list = new List<CallingPlanDto>();
			if (pCallingPlans != null) {
				foreach (var _callingPlanRow in pCallingPlans) {
					var _callingPlan = mapToCallingPlan(_callingPlanRow);
					//TODO: !!! HARDCODED VALUE 
					if (_callingPlan.CallingPlanId == 1) {
						//DEFAULT CallingPlan
						_list.Insert(0, _callingPlan);
					}
					else {
						_list.Add(_callingPlan);
					}
				}
			}
			return _list.ToArray();
		}

		internal static CallingPlanDto mapToCallingPlan(CallingPlanRow pCallingPlanRow) {
			var _callingPlan = new CallingPlanDto();
			_callingPlan.CallingPlanId = pCallingPlanRow.Calling_plan_id;
			_callingPlan.Name = pCallingPlanRow.Name;
			_callingPlan.VirtualSwitchId = pCallingPlanRow.Virtual_switch_id;
			_callingPlan.Version = pCallingPlanRow.Version;
			return _callingPlan;
		}

		static CallingPlanRow mapToCallingPlanRow(CallingPlanDto pCallingPlan) {
			var _callingPlanRow = new CallingPlanRow();
			_callingPlanRow.Calling_plan_id = pCallingPlan.CallingPlanId;
			_callingPlanRow.Name = pCallingPlan.Name;
			_callingPlanRow.Virtual_switch_id = pCallingPlan.VirtualSwitchId;
			_callingPlanRow.Version = pCallingPlan.Version;
			return _callingPlanRow;
		}

		static CountryRow mapToCountryRow(CountryDto pCountry) {
			if (pCountry == null) {
				return null;
			}

			var _countryRow = new CountryRow();
			_countryRow.Country_id = pCountry.CountryId;
			_countryRow.Name = pCountry.Name;
			_countryRow.Country_code = pCountry.CountryCode;
			_countryRow.Status = (byte) pCountry.Status;
			_countryRow.Version = pCountry.Version;
			return _countryRow;
		}

		static CountryDto[] mapToCountries(CountryRow[] pCountryRows) {
			var _list = new SortedList<string, CountryDto>();
			if (pCountryRows != null) {
				foreach (var _countryRow in pCountryRows) {
					var _country = mapToCountry(_countryRow);
					_list.Add(_country.Name, _country);
				}
			}

			var _countries = new CountryDto[_list.Count];
			_list.Values.CopyTo(_countries, 0);
			return _countries;
		}

		static CountryDto mapToCountry(CountryRow pCountryRow) {
			if (pCountryRow == null) {
				return null;
			}

			var _country = new CountryDto();
			_country.CountryId = pCountryRow.Country_id;
			_country.Name = pCountryRow.Name;
			_country.CountryCode = pCountryRow.Country_code;
			_country.Status = (Status) pCountryRow.Status;
			_country.Version = pCountryRow.Version;
			return _country;
		}

		static DialCodeRow mapToDialCodeRow(DialCodeDto pDialCode) {
			if (pDialCode == null) {
				return null;
			}

			var _dialCodeRow = new DialCodeRow();
			_dialCodeRow.Calling_plan_id = pDialCode.CallingPlanId;
			_dialCodeRow.Dial_code = pDialCode.Code;
			_dialCodeRow.Route_id = pDialCode.BaseRouteId;
			_dialCodeRow.Version = pDialCode.Version;
			return _dialCodeRow;
		}

		static DialCodeDto[] mapToDialCodes(IEnumerable<DialCodeRow> pDialCodeRows) {
			var _list = new List<DialCodeDto>();
			if (pDialCodeRows != null) {
				foreach (var _dialCodeRow in pDialCodeRows) {
					var _dialCode = mapToDialCode(_dialCodeRow);
					_list.Add(_dialCode);
				}
			}
			return _list.ToArray();
		}

		static DialCodeDto mapToDialCode(DialCodeRow_Base pDialCodeRow) {
			if (pDialCodeRow == null) {
				return null;
			}
			var _dialCode = new DialCodeDto();
			_dialCode.CallingPlanId = pDialCodeRow.Calling_plan_id;
			_dialCode.Code = pDialCodeRow.Dial_code;
			_dialCode.BaseRouteId = pDialCodeRow.Route_id;
			_dialCode.Version = pDialCodeRow.Version;
			return _dialCode;
		}

		#endregion
	}
}
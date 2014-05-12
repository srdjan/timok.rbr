using System;
using Timok.Rbr.BLL.Managers;
using Timok.Rbr.Core;
using Timok.Rbr.Core.Config;
using Timok.Rbr.DAL.RbrDatabase;
using Timok.Rbr.DTO;

namespace Timok.Rbr.BLL.Controllers {
	public class CallingPlanController {
		CallingPlanController() { }

		#region Public Getters

		public static CallingPlanDto[] GetAllCallingPlans() {
			using (var _db = new Rbr_Db()) {
				return CallingPlanManager.GetCallingPlans(_db);
			}
		}

		public static CallingPlanDto GetCallingPlan(int pCallingPlanId) {
			using (var _db = new Rbr_Db()) {
				return CallingPlanManager.GetCallingPlan(_db, pCallingPlanId);
			}
		}

		//public static int GetUsageCount(int pCallingPlanId) {
		//  using (var _db = new Rbr_Db()) {
		//    return CallingPlanManager.GetCallingPlanUsageCount(_db, pCallingPlanId);
		//  }
		//}

		public static BaseRouteDto[] GetUnusedBaseRoutes(short pAccountId, int pCallingPlanId, int pRoutingPlanId, ViewContext pContext) {
			using (var _db = new Rbr_Db()) {
				return RoutingManager.GetUnusedBaseRoutes(_db, pAccountId, pCallingPlanId, pContext, pRoutingPlanId);
			}
		}

		public static BaseRouteDto[] GetUnusedBaseRoutes(RoutingPlanDto pRoutingPlan) {
			using (var _db = new Rbr_Db()) {
				return RoutingManager.GetUnusedByCallingPlanIdRoutingPlanId(_db, pRoutingPlan.CallingPlanId, pRoutingPlan.RoutingPlanId);
			}
		}

		public static CountryDto[] GetCountries() {
			using (var _db = new Rbr_Db()) {
				return CallingPlanManager.GetCountries(_db);
			}
		}

		public static CountryDto[] GetCountriesByCallingPlan(int pCallingPlanId) {
		  using (var _db = new Rbr_Db()) {
		    return CallingPlanManager.GetCountriesByCallingPlan(_db, pCallingPlanId);
		  }
		}

		public static CountryDto[] GetCountriesByRoutingPlan(int pRoutingPlanId) {
		  using (var _db = new Rbr_Db()) {
		    return CallingPlanManager.GetCountriesByRoutingPlan(_db, pRoutingPlanId);
		  }
		}

		//public static CountryDto[] GetCountries(short pAccountId, ViewContext pContext) {
		//  using (var _db = new Rbr_Db()) {
		//    CustomerAcctDto _customerAcct = CustomerAcctManager.GetAcct(_db, pAccountId);
		//    if (_customerAcct == null) {
		//      return new CountryDto[0];
		//    }
		//    return CallingPlanManager.GetCountries(_db, pAccountId, _customerAcct.ServiceDto.CallingPlanId, pContext);
		//  }
		//}

		public static CountryDto[] GetCountries(short pAccountId, int pCallingPlanId, ViewContext pContext) {
			using (var _db = new Rbr_Db()) {
				return CallingPlanManager.GetCountries(_db, pAccountId, pCallingPlanId, pContext);
			}
		}

		//public static CountryDto GetCountryByCountryId(int pCountryId) {
		//  using (var _db = new Rbr_Db()) {
		//    return CallingPlanManager.GetCountry(_db, pCountryId);
		//  }
		//}

		public static CountryDto GetCountryByName(string pName) {
			using (var _db = new Rbr_Db()) {
				return CallingPlanManager.GetCountry(_db, pName);
			}
		}

		public static CountryDto GetCountryByCountryCode(int pCountryCode) {
			using (var _db = new Rbr_Db()) {
				return CallingPlanManager.GetByCountryCode(_db, pCountryCode);
			}
		}

		public static CountryDto[] GetUnusedCountries(int pCallingPlanId) {
			using (var _db = new Rbr_Db()) {
				return CallingPlanManager.GetUnusedCountries(_db, pCallingPlanId);
			}
		}

		public static bool ValidateCountry(int pCountryCode, int pExcludingCountryId, out string pMsg) {
			using (var _db = new Rbr_Db()) {
				return CallingPlanManager.Validate(_db, pCountryCode, pExcludingCountryId, out pMsg);
			}
		}

		public static bool ValidateCountry(string pCountryName, out string pMsg) {
			using (var _db = new Rbr_Db()) {
				return CallingPlanManager.Validate(_db, pCountryName, out pMsg);
			}
		}

		//public static DialCodeDto[] GetDialCodes(int pCallingPlanId, int pBaseRouteId) {
		//  using (var _db = new Rbr_Db()) {
		//    return CallingPlanManager.GetDialCodes(_db, pCallingPlanId, pBaseRouteId);
		//  }
		//}

		//public static DialCodeDto GetDialCode(int pCallingPlanId, long pDialCode) {
		//  using (var _db = new Rbr_Db()) {
		//    return CallingPlanManager.GetDialCode(_db, pCallingPlanId, pDialCode);
		//  }
		//}

		//public static DialCodeDto[] GetByDialedNumbers(string pDialCode) {
		//  using (var _db = new Rbr_Db()) {
		//    return CallingPlanManager.GetByDialedNumber(_db, pDialCode);
		//  }
		//}

		public static int GetDialCodeRowNumber(int pCallingPlanId, int pRouteId, long pDialCode) {
			using (var _db = new Rbr_Db()) {
				return CallingPlanManager.GetRowNumber(_db, pCallingPlanId, pRouteId, pDialCode);
			}
		}

		public static DialCodeDto GetFirstDialCodeByCallingPlanIdDialedNumber(int pCallingPlanId, long pDialedNumber) {
			using (var _db = new Rbr_Db()) {
				return CallingPlanManager.GetFirstByCallingPlanIdDialedNumber(_db, pCallingPlanId, pDialedNumber);
			}
		}

		public static DialCodeDto GetFirstDialCodeByCallingPlanIdRouteId(int pCallingPlanId, int pRouteId) {
			using (var _db = new Rbr_Db()) {
				return CallingPlanManager.GetFirstByCallingPlanIdRouteId(_db, pCallingPlanId, pRouteId);
			}
		}

		public static DialCodeDto[] GetDialCodesByRouteIdPaged(int pRouteId, int pPageNumber, int pPageSize, out int pTotalCount) {
			using (var _db = new Rbr_Db()) {
				return CallingPlanManager.GetByRouteIdPaged(_db, pRouteId, pPageNumber, pPageSize, out pTotalCount);
			}
		}

		public static DialCodeDto[] GetDialCodesByCallingPlanIdCountryIdPaged(int pCallingPlanId, int pCountryId, int pPageNumber, int pPageSize, out int pTotalCount) {
			using (var _db = new Rbr_Db()) {
				return CallingPlanManager.GetByCallingPlanIdCountryIdPaged(_db, pCallingPlanId, pCountryId, pPageNumber, pPageSize, out pTotalCount);
			}
		}

		public static bool ValidateDialCode(BaseRouteDto pRoute, int pCountryCode, long pDialCode, out string pMsg) {
			pMsg = string.Empty;
			if (pCountryCode == 1) {
				// Canada, USA, ???
				//not true anymore
				//				//has to be 3 digits fixed
				//				if (pDialCode.ToString().Length != 4) {
				//					pMsg = "Please enter a 3 digits code.";
				//					return false;
				//				}
			}
			else {
				//other countries
				if (pRoute.IsProper) {
					//allow any code
				}
				else {
					//min 1 digit
					if (pDialCode.ToString().Length - pCountryCode.ToString().Length < 1) {
						pMsg = "Please enter a minimum 1 digit code.";
						return false;
					}
				}
			}

			using (var _db = new Rbr_Db()) {
				if (_db.DialCodeCollection.Exists(pRoute.CallingPlanId, pDialCode)) {
					pMsg = "Dial Code already exists";
					return false;
				}
			}

			DialCodeRow[] _existing;
			using (var _db = new Rbr_Db()) {
				_existing = _db.DialCodeCollection.GetIsCovered(pRoute.CallingPlanId, pRoute.BaseRouteId, pCountryCode, pDialCode);
			}
			if (_existing != null && _existing.Length > 0) {
				pMsg = "Dial Code already covered by existing Dial Code [" + _existing[0].Dial_code + "]";
				return false;
			}

			if (pRoute.IsProper && pCountryCode == pDialCode) {
				//allow to add CountryCode as DialCode for Proper
			}
			else {
				using (var _db = new Rbr_Db()) {
					_existing = _db.DialCodeCollection.GetWillCover(pRoute.CallingPlanId, pRoute.BaseRouteId, pDialCode);
				}
				if (_existing != null && _existing.Length > 0) {
					pMsg = "Dial Code covers at least one existing Dial Code [" + _existing[0].Dial_code + "]";
					return false;
				}
			}
			return true;
		}

		#endregion Public Getters

		#region Public Actions

		public static void AddCallingPlan(CallingPlanDto pCallingPlan) {
			using (var _db = new Rbr_Db()) {
				using (var _tx = new Transaction(_db, pCallingPlan)) {
					CallingPlanManager.AddCallingPlan(_db, pCallingPlan);
					_tx.Commit();
				}
			}
		}

		public static void UpdateCallingPlan(CallingPlanDto pCallingPlan) {
			using (var _db = new Rbr_Db()) {
				using (var _tx = new Transaction(_db, pCallingPlan)) {
					CallingPlanManager.UpdateCallingPlan(_db, pCallingPlan);
					_tx.Commit();
				}
			}
		}

		public static void DeleteCallingPlan(CallingPlanDto pCallingPlan) {
			using (var _db = new Rbr_Db()) {
				using (var _tx = new Transaction(_db, pCallingPlan)) {
					CallingPlanManager.DeleteCallingPlan(_db, pCallingPlan);
					_tx.Commit();
				}
			}
		}

		public static void AddCountry(CountryDto pCountry) {
			using (var _db = new Rbr_Db()) {
				using (var _tx = new Transaction(_db, pCountry)) {
					CallingPlanManager.AddCountry(_db, pCountry);
					_tx.Commit();
				}
			}
		}

		public static void AddCountriesToCallingPlan(CallingPlanDto pCallingPlan, CountryDto[] pSelectedCountries) {
			using (var _db = new Rbr_Db()) {
				using (var _tx = new Transaction(_db, pCallingPlan, pSelectedCountries)) {
					CallingPlanManager.AddCountriesToCallingPlan(_db, pCallingPlan, pSelectedCountries);
					_tx.Commit();
				}
			}
		}

		public static void UpdateCountry(CountryDto pCountry) {
			//NOTE: !!! NOT status change on Country !!!
			using (var _db = new Rbr_Db()) {
				using (var _tx = new Transaction(_db, pCountry)) {
					CallingPlanManager.UpdateCountry(_db, pCountry);
					_tx.Commit();
				}
			}
		}

		public static void DeleteCountry(CountryDto pCountry) {
			using (var _db = new Rbr_Db()) {
				using (var _tx = new Transaction(_db, pCountry)) {
					CallingPlanManager.DeleteCountry(_db, pCountry);
					_tx.Commit();
				}
			}
		}

		public static void AddDialCode(DialCodeDto pDialCode) {
			using (var _db = new Rbr_Db()) {
				using (var _tx = new Transaction(_db, pDialCode)) {
					CallingPlanManager.AddDialCode(_db, pDialCode);
					_tx.Commit();
				}
			}
		}

		//public static void AddDialCodes(DialCodeDto[] pDialCodes) {
		//  using (var _db = new Rbr_Db()) {
		//    using (var _tx = new Transaction(_db, pDialCodes)) {
		//      foreach (DialCodeDto _dialCode in pDialCodes) {
		//        CallingPlanManager.AddDialCode(_db, _dialCode);
		//      }
		//      _tx.Commit();
		//    }
		//  }
		//}

		public static void MoveDialCodes(DialCodeDto[] pDialCodes, BaseRouteDto pFromRoute, BaseRouteDto pToRoute) {
			using (var _db = new Rbr_Db()) {
				using (var _tx = new Transaction(_db, pDialCodes, pFromRoute, pToRoute)) {
					//NOTE: not used?					RouteRow _fromRouteRow = RoutingManager.MapToRouteRow(pFromRoute);
					RouteRow _toRouteRow = RoutingManager.MapToRouteRow(pToRoute);
					foreach (DialCodeDto _dialCode in pDialCodes) {
						CallingPlanManager.MoveDialCode(_db, _dialCode, _toRouteRow);
					}
					_tx.Commit();
				}
			}
		}

		public static void DeleteDialCode(DialCodeDto pDialCode) {
			using (var _db = new Rbr_Db()) {
				using (var _tx = new Transaction(_db, pDialCode)) {
					CallingPlanManager.DeleteDialCode(_db, pDialCode);
					_tx.Commit();
				}
			}
		}

		public static void DeleteDialCodes(DialCodeDto[] pDialCodes) {
			using (var _db = new Rbr_Db()) {
				using (var _tx = new Transaction(_db, pDialCodes)) {
					foreach (DialCodeDto _dialCode in pDialCodes) {
						CallingPlanManager.DeleteDialCode(_db, _dialCode);
					}
					_tx.Commit();
				}
			}
		}

		#endregion Public Actions
	}
}
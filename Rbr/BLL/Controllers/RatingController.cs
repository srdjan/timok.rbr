using System;
using Timok.Core;
using Timok.Logger;
using Timok.Rbr.BLL.Managers;
using Timok.Rbr.Core;
using Timok.Rbr.Core.Config;
using Timok.Rbr.DAL.RbrDatabase;
using Timok.Rbr.DTO;
using Timok.Rbr.DTO.Interfaces;

namespace Timok.Rbr.BLL.Controllers {
	public class RatingController {
		RatingController() {}

		#region Public Getters

		//public static RatingInfoDto[] GetRates(short pAcctId, int pCountryId, RouteType pRouteType, DateTime pDate) {
		//  using (var _db = new Rbr_Db()) {
		//    if (pRouteType == RouteType.Carrier) {
		//      throw new NotImplementedException();
		//    }
		//    if (pRouteType == RouteType.Wholesale) {
		//      return getWholesaleRates(_db, pAcctId, pRouteType, pCountryId, pDate);
		//    }
		//    //else if (pRouteType == RouteType.Retail) {
		//    //  //TODO:
		//    //}
		//    throw new ArgumentException(string.Format("UNSUPPORTED Type : {0}", pRouteType), "pRouteType");
		//  }
		//}

		public static RatingHistoryEntry GetNewRatingHistoryEntry(int pRatedRouteId, RouteType pRouteType) {
			using (var _db = new Rbr_Db()) {
				RatedRouteDto _route = null;
				if (pRouteType == RouteType.Carrier) {
					_route = CarrierRouteManager.Get(_db, pRatedRouteId);
				}
				else if (pRouteType == RouteType.Wholesale) {
					_route = CustomerRouteManager.Get(_db, pRatedRouteId);
				}
				else if (pRouteType == RouteType.Retail) {
					
				}
				if (_route != null) {
					var _ratingInfo = _route.DefaultRatingInfo.Clone();
					if (_ratingInfo != null) {
						var _ratingHistoryEntry = new RatingHistoryEntry(pRouteType, pRatedRouteId, DateTime.Today, Configuration.Instance.Db.SqlSmallDateTimeMaxValue, _ratingInfo.Clone());
						_ratingHistoryEntry.HasChanged = true;
						return _ratingHistoryEntry;
					}
				}
				return null;
			}
		}

		public static RatingHistoryEntry[] GetRatingHistoryEntries(int pRatedRouteId, RouteType pRouteType) {
			using (var _db = new Rbr_Db()) {
				return RatingManager.GetRatingHistoryEntries(_db, pRatedRouteId, pRouteType);
			}
		}

		#endregion Public Getters

		#region Public Actions

		public static Result UpdateRatingHistoryEntry(IRatingHistoryEntry pRatingHistoryEntry, RouteType pRouteType) {
			Result _result = new Result(true, null);
			try {
				using (Rbr_Db _db = new Rbr_Db()) {
					using (Transaction _tx = new Transaction(_db, pRatingHistoryEntry)) {
						RatingHistoryEntry[] _ratingHistoryEntries = RatingManager.GetRatingHistoryEntries(_db, pRatingHistoryEntry.RatedRouteId, pRouteType);
						if (_ratingHistoryEntries == null || _ratingHistoryEntries.Length == 0) {
							return new Result(false, "Error saving Rates, no RatingHistoryEntries found.");
						}

						RatingHistoryEntry _ratingHistoryEntry = null;
						foreach (RatingHistoryEntry _rate in _ratingHistoryEntries) {
							if (_rate.RateInfoId == pRatingHistoryEntry.RateInfoId) {
								_ratingHistoryEntry = _rate;
								break;
							}
						}
						if (_ratingHistoryEntry == null) {
							return new Result(false, "Error saving Rates, matching RatingHistoryEntry not found.");
						}

						if (pRatingHistoryEntry.RatingInfo.RegularDayRateEntry != null) {
							_ratingHistoryEntry.RatingInfo.RegularDayRateEntry = pRatingHistoryEntry.RatingInfo.RegularDayRateEntry;
							_ratingHistoryEntry.FirstDate = pRatingHistoryEntry.FirstDate;
							_ratingHistoryEntry.LastDate = pRatingHistoryEntry.LastDate;
						}
						if (pRatingHistoryEntry.RatingInfo.WeekendRateEntry != null) {
							_ratingHistoryEntry.RatingInfo.WeekendRateEntry = pRatingHistoryEntry.RatingInfo.WeekendRateEntry;
							_ratingHistoryEntry.FirstDate = pRatingHistoryEntry.FirstDate;
							_ratingHistoryEntry.LastDate = pRatingHistoryEntry.LastDate;
						}
						if (pRatingHistoryEntry.RatingInfo.HolidayRateEntry != null) {
							_ratingHistoryEntry.RatingInfo.HolidayRateEntry = pRatingHistoryEntry.RatingInfo.HolidayRateEntry;
							_ratingHistoryEntry.FirstDate = pRatingHistoryEntry.FirstDate;
							_ratingHistoryEntry.LastDate = pRatingHistoryEntry.LastDate;
						}

						_ratingHistoryEntry.RateInfoId = 0;
						RatingManager.SaveRatingHistoryEntry(_db, _ratingHistoryEntry);
						_tx.Commit();
					}
				}
			}
			catch (Exception _ex) {
				TimokLogger.Instance.LogRbr(LogSeverity.Error, "RateController.Save(1)", string.Format("Exception:\r\n{0}", _ex));
				_result = new Result(false, _ex.Message);
			}
			return _result;
		}

		public static Result SaveRatingHistoryEntries(IRatingHistoryEntry pRatingHistoryEntry, int[] pRatedRouteIds) {
			var _result = new Result(true, null);
			try {
				using (var _db = new Rbr_Db()) {
					using (var _tx = new Transaction(_db, pRatingHistoryEntry, pRatedRouteIds)) {
						foreach (int _routeId in pRatedRouteIds) {
							var _ratingHistoryEntry = (IRatingHistoryEntry)Cloner.Clone(pRatingHistoryEntry);
							if (_routeId != pRatingHistoryEntry.RatedRouteId) {//NOTE: not a leading route, set RouteId=one of the selected, and set RateInfoId=0
								_ratingHistoryEntry.RatedRouteId = _routeId;
								_ratingHistoryEntry.RateInfoId = 0;
							}

							RatingManager.SaveRatingHistoryEntry(_db, _ratingHistoryEntry);

							if (_routeId == pRatingHistoryEntry.RatedRouteId) {//NOTE: this is the leading route, update sent ref on it
								pRatingHistoryEntry = _ratingHistoryEntry;
							}
						}
						_tx.Commit();
					}
				}
			}
			catch (Exception _ex) {
				TimokLogger.Instance.LogRbr(LogSeverity.Error, "RateController.Save(2)", string.Format("Exception:\r\n{0}", _ex));
				_result = new Result(false, _ex.Message);
			}
			return _result;
		}

		public static Result DeleteRatingHistoryEntry(IRatingHistoryEntry pRatingHistoryEntry) {
			Result _result = new Result(true, null);
			try {
				using (Rbr_Db _db = new Rbr_Db()) {
					using (Transaction _tx = new Transaction(_db, pRatingHistoryEntry)) {
						//NOTE: NO physical DELETE, just set Rates to 0
						pRatingHistoryEntry.RatingInfo.ClearRates();
						RatingManager.UpdateRatingInfo(_db, pRatingHistoryEntry.RatingInfo);
						_tx.Commit();
					}
				}
			}
			catch (Exception _ex) {
				TimokLogger.Instance.LogRbr(LogSeverity.Critical, "RatingController.Delete", string.Format("Exception:\r\n{0}", _ex));
				_result = new Result(false, _ex.Message);
			}
			return _result;
		}

		#endregion Public Actions

		#region Private 
		
		//static RatingInfoDto[] getWholesaleRates(Rbr_Db pDb, short pAcctId, RouteType pRouteType, int pCountryId, DateTime pDate) {
		//  List<RatingInfoDto> _ratingInfoDtos = new List<RatingInfoDto>();
		//  CustomerAcctDto _customerAcct = CustomerAcctManager.GetAcct(pDb, pAcctId);
		//  if (_customerAcct != null) {
		//    CountryDto _country = CallingPlanManager.GetCountry(pDb, pCountryId);
		//    RatedRouteDto[] _ratedRoutes = CustomerRouteManager.Get(pDb, _customerAcct.ServiceDto.ServiceId, _customerAcct.RoutingPlanId, _country.CountryId);
		//    if (_ratedRoutes != null && _ratedRoutes.Length > 0) {
		//      foreach (RatedRouteDto _ratedRoute in _ratedRoutes) {
		//        RatingHistoryEntry[] _ratingHistoryEntries = RatingManager.GetRatingHistoryEntries(pDb, _ratedRoute.RatedRouteId, pRouteType);
		//        if (_ratingHistoryEntries != null && _ratingHistoryEntries.Length > 0) {
		//          foreach (RatingHistoryEntry _ratingHistoryEntry in _ratingHistoryEntries) {
		//            if (_ratingHistoryEntry.FirstDate >= pDate || _ratingHistoryEntry.LastDate <= pDate) {
		//              continue;
		//            }
		//            _ratingHistoryEntry.RatingInfo.RatedRouteId = _ratedRoute.RatedRouteId;
		//            _ratingHistoryEntry.RatingInfo.RouteName = _ratedRoute.Name;
		//            _ratingInfoDtos.Add(_ratingHistoryEntry.RatingInfo);
		//          }
		//        }
		//      }
		//    }
		//  }
		//  return _ratingInfoDtos.ToArray();
		//}

		#endregion
	}
}
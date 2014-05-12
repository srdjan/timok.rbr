using System;
using System.Collections;
using System.Collections.Generic;
using Timok.Logger;
using Timok.Rbr.BLL.DTO;
using Timok.Rbr.Core.Config;
using Timok.Rbr.DAL.RbrDatabase;
using Timok.Rbr.DAL.RbrDatabase.Base;
using Timok.Rbr.DTO;
using Timok.Rbr.DTO.Interfaces;

namespace Timok.Rbr.BLL.Managers {
	internal class RatingManager {
		RatingManager() { }

		#region Getters

		internal static RatingInfoDto GetRatingInfo(Rbr_Db pDb, int pRateInfoId, bool pShouldGetNew) {
			if (pRateInfoId == 0 && ! pShouldGetNew) {
				return null;
			}

			var _ratingInfo = new RatingInfoDto();
			var _holidayCalendarRows = pDb.HolidayCalendarCollection.GetByRate_info_id(pRateInfoId);
			var _rateInfoRow = pDb.RateInfoCollection.GetByPrimaryKey(pRateInfoId);
			if (_rateInfoRow != null) {
				_ratingInfo.RateInfoId = pRateInfoId;
				TypeOfDayRow[] _typeOfDayRows = pDb.TypeOfDayCollection.GetByRate_info_id(pRateInfoId);

				if (_typeOfDayRows != null) {
					foreach (var _typeOfDayRow in _typeOfDayRows) {
						var _timeOfDayPeriodRows = pDb.TimeOfDayPeriodCollection.GetByRate_info_id_Type_of_day_choice(_typeOfDayRow.Rate_info_id, _typeOfDayRow.Type_of_day_choice);
						switch (_typeOfDayRow.TypeOfDayChoice) {
							case TypeOfDayChoice.RegularDay:
								_ratingInfo.RegularDay = mapToDayType(_typeOfDayRow, _timeOfDayPeriodRows);
								break;
							case TypeOfDayChoice.Weekend:
								_ratingInfo.Weekend = mapToDayType(_typeOfDayRow, _timeOfDayPeriodRows);
								break;
							case TypeOfDayChoice.Holiday:
								_ratingInfo.Holiday = mapToDayType(_typeOfDayRow, _timeOfDayPeriodRows);
								break;
						}
					}
				}
			}
			_ratingInfo.HolidayList = new HolidayListDto(pRateInfoId, mapToHolidays(_holidayCalendarRows));

			TypeOfDayRateEntryDto _regularDayRateEntry;
			TypeOfDayRateEntryDto _weekendRateEntry;
			TypeOfDayRateEntryDto _holidayRateEntry;
			getTypeOfDayRateEntries(pDb, pRateInfoId, out _regularDayRateEntry, out _weekendRateEntry, out _holidayRateEntry);

			_ratingInfo.RegularDayRateEntry = _regularDayRateEntry;
			_ratingInfo.WeekendRateEntry = _weekendRateEntry;
			_ratingInfo.HolidayRateEntry = _holidayRateEntry;

			return _ratingInfo;
		}

		internal static RatingHistoryEntry[] GetRatingHistoryEntries(Rbr_Db pDb, int pRatedRouteId, RouteType pRouteType) {
			var _list = new List<RatingHistoryEntry>();
			if (pRouteType == RouteType.Carrier) {
				var _carrierRateHistoryRows = pDb.CarrierRateHistoryCollection.GetByCarrier_route_id(pRatedRouteId);
				foreach (var _carrierRateHistoryRow in _carrierRateHistoryRows) {
					var _ratingInfo = GetRatingInfo(pDb, _carrierRateHistoryRow.Rate_info_id, false);
					_list.Add(MapToCarrierRatingHistoryEntry(_carrierRateHistoryRow, _ratingInfo));
				}
			}
			else if (pRouteType == RouteType.Wholesale) {
				var _wholesaleRateHistoryRows = pDb.WholesaleRateHistoryCollection.GetByWholesale_route_id(pRatedRouteId);
				foreach (var _wholesaleRateHistoryRow in _wholesaleRateHistoryRows) {
					var _ratingInfo = GetRatingInfo(pDb, _wholesaleRateHistoryRow.Rate_info_id, false);
					_list.Add(MapToWholesaleRatingHistoryEntry(_wholesaleRateHistoryRow, _ratingInfo));
				}
			}
			else if (pRouteType == RouteType.Retail) {
				//TODO:
			}
			else {
				throw new ArgumentException(string.Format("UNSUPPORTED Type : {0}", pRouteType), "pRouteType");
			}
			return _list.ToArray();
		}

		#endregion Getters

		#region Actions

		internal static void SaveRatingHistoryEntry(Rbr_Db pDb, IRatingHistoryEntry pRatingHistoryEntry) {
			if (pRatingHistoryEntry.RatedRouteId == 0) {
				throw new ArgumentException("UNEXPECTED: RatedRouteId == 0", "pRatingHistoryEntry");
			}

			validateRatingHistoryEntry(pRatingHistoryEntry);

			if (pRatingHistoryEntry.RateInfoId == 0) {
				//NOTE: fill the gap (if there is one)...
				saveRanges(pDb, pRatingHistoryEntry);
				UpdateRatingInfo(pDb, pRatingHistoryEntry.RatingInfo);
				addRatingHistoryEntry(pDb, pRatingHistoryEntry);
			}
			else {
				deleteRatingHistoryEntry(pDb, pRatingHistoryEntry); //NOTE: first delete it, so it will not interfear with save ranges
				saveRanges(pDb, pRatingHistoryEntry);
				addRatingHistoryEntry(pDb, pRatingHistoryEntry);
				UpdateRatingInfo(pDb, pRatingHistoryEntry.RatingInfo);
			}

			IRatingHistoryEntry[] _ratingHistoryEntryGaps = findGaps(pDb, pRatingHistoryEntry.RatedRouteId, pRatingHistoryEntry.RouteType);
			fillRangeGaps(pDb, pRatingHistoryEntry, _ratingHistoryEntryGaps);
		}

		internal static void AddDefaultRatingInfo(Rbr_Db pDb, int pRatedRouteId, RatingInfoDto pRatingInfo, RouteType pRouteType) {
			var _ratingInfo = pRatingInfo.Clone();
			UpdateRatingInfo(pDb, _ratingInfo);

			if (pRouteType == RouteType.Wholesale) {
				var _wholesaleRateHistoryRow = new WholesaleRateHistoryRow
				                               	{
				                               		Rate_info_id = _ratingInfo.RateInfoId, 
																					Wholesale_route_id = pRatedRouteId, 
																					Date_on = DateTime.Today, 
																					Date_off = Configuration.Instance.Db.SqlSmallDateTimeMaxValue
				                               	};
				pDb.WholesaleRateHistoryCollection.Insert(_wholesaleRateHistoryRow);
			}
			else if (pRouteType == RouteType.Carrier) {
				var _carrierRateHistoryRow = new CarrierRateHistoryRow
				                             	{
				                             		Rate_info_id = _ratingInfo.RateInfoId, 
																				Carrier_route_id = pRatedRouteId, 
																				Date_on = DateTime.Today, 
																				Date_off = Configuration.Instance.Db.SqlSmallDateTimeMaxValue
				                             	};
				pDb.CarrierRateHistoryCollection.Insert(_carrierRateHistoryRow);
			}
			else if (pRouteType == RouteType.Retail) {
				//TODO:
			}
			else {
				throw new ArgumentException(string.Format("UNSUPPORTED Type : {0}", pRouteType), "pRouteType");
			}
		}

		internal static void UpdateRatingInfo(Rbr_Db pDb, RatingInfoDto pRatingInfo) {
			try {
				if (pRatingInfo.RateInfoId <= 0) {
					var _rateInfoRow = new RateInfoRow();
					pDb.RateInfoCollection.Insert(_rateInfoRow);
					pRatingInfo.RateInfoId = _rateInfoRow.Rate_info_id;
					pRatingInfo.HolidayList.RateInfoId = _rateInfoRow.Rate_info_id;
				}
				else {
					RateInfoRow _rateInfoRow = pDb.RateInfoCollection.GetByPrimaryKey(pRatingInfo.RateInfoId);
					if (_rateInfoRow == null) {
						_rateInfoRow = new RateInfoRow();
						pDb.RateInfoCollection.Insert(_rateInfoRow);
						pRatingInfo.RateInfoId = _rateInfoRow.Rate_info_id;
						pRatingInfo.HolidayList.RateInfoId = _rateInfoRow.Rate_info_id;
					}
				}

				saveTypeOfDayRow(pDb, pRatingInfo.RateInfoId, pRatingInfo.RegularDay, TypeOfDayChoice.RegularDay, pRatingInfo.RegularDayRateEntry);
				saveTypeOfDayRow(pDb, pRatingInfo.RateInfoId, pRatingInfo.Weekend, TypeOfDayChoice.Weekend, pRatingInfo.WeekendRateEntry);
				saveTypeOfDayRow(pDb, pRatingInfo.RateInfoId, pRatingInfo.Holiday, TypeOfDayChoice.Holiday, pRatingInfo.HolidayRateEntry);

				SaveHolidayList(pDb, pRatingInfo.HolidayList);
			}
			catch {
				if (pRatingInfo != null) {
					pRatingInfo.RateInfoId = 0;
				}
				throw;
			}
		}

		internal static void DeleteRatingInfo(Rbr_Db pDb, int pRateInfoId) {
			pDb.RateCollection.DeleteByRateInfoID(pRateInfoId);
			pDb.TimeOfDayPeriodCollection.DeleteByRateInfoID(pRateInfoId);
			pDb.TypeOfDayCollection.DeleteByRate_info_id(pRateInfoId);
			pDb.HolidayCalendarCollection.DeleteByRate_info_id(pRateInfoId);
			pDb.RateInfoCollection.DeleteByPrimaryKey(pRateInfoId);
		}

		internal static void SaveHolidayList(Rbr_Db pDb, HolidayListDto pHolidayList) {
			pDb.HolidayCalendarCollection.DeleteByRate_info_id(pHolidayList.RateInfoId);
			HolidayCalendarRow[] _holidayCalendarRows = mapToHolidayCalendarRows(pHolidayList.RateInfoId, pHolidayList.Holidays);
			foreach (HolidayCalendarRow _holidayCalendarRow in _holidayCalendarRows) {
				pDb.HolidayCalendarCollection.Insert(_holidayCalendarRow);
			}
		}

		#endregion Actions

		#region privates

		static void getTypeOfDayRateEntries(Rbr_Db_Base pDb, int pRateInfoID, out TypeOfDayRateEntryDto pRegularDay, out TypeOfDayRateEntryDto pWeekend, out TypeOfDayRateEntryDto pHoliday) {
			pRegularDay = null;
			pWeekend = null;
			pHoliday = null;

			TypeOfDayRow[] _typeOfDayRows = pDb.TypeOfDayCollection.GetByRate_info_id(pRateInfoID);
			foreach (TypeOfDayRow _typeOfDayRow in _typeOfDayRows) {
				RateRow[] _rateRows = pDb.RateCollection.GetByRate_info_id_Type_of_day_choice(_typeOfDayRow.Rate_info_id, _typeOfDayRow.Type_of_day_choice);
				switch (_typeOfDayRow.TypeOfDayChoice) {
					case TypeOfDayChoice.RegularDay:
						pRegularDay = mapToTypeOfDayRateEntry(_typeOfDayRow, _rateRows);
						break;
					case TypeOfDayChoice.Weekend:
						pWeekend = mapToTypeOfDayRateEntry(_typeOfDayRow, _rateRows);
						break;
					case TypeOfDayChoice.Holiday:
						pHoliday = mapToTypeOfDayRateEntry(_typeOfDayRow, _rateRows);
						break;
					default:
						TimokLogger.Instance.LogRbr(LogSeverity.Critical, "RatingManager.getTypeOfDayRateEntries", string.Format("UNKNOWN TypeOfDayChoice{0}", _typeOfDayRow.Type_of_day_choice));
						break;
				}
			}
		}

		static void saveTypeOfDayRow(Rbr_Db pDb, int pRateInfoID, DayTypeDto pDayType, TypeOfDayChoice pTypeOfDayChoice, TypeOfDayRateEntryDto pTypeOfDayRateEntry) {
			TypeOfDayRow _typeOfDayRow = pDb.TypeOfDayCollection.GetByRateInfoIDTypeOfDayChoice(pRateInfoID, pTypeOfDayChoice);
			if (pDayType.IsSelected) {
				if (_typeOfDayRow == null) {
					//doesn't exist yet - create
					createTypeOfDayRow(pDb, pRateInfoID, pTypeOfDayChoice, pDayType, pTypeOfDayRateEntry);
				}
				else {
					updateTypeOfDayRow(pDb, pRateInfoID, pDayType, _typeOfDayRow, pTypeOfDayRateEntry);
				}
			}
			else {
				deleteTypeOfDayRow(pDb, pRateInfoID, _typeOfDayRow);
			}
		}

		static void deleteTypeOfDayRow(Rbr_Db_Base pDb, int pRateInfoID, TypeOfDayRow_Base pTypeOfDayRow) {
			if (pTypeOfDayRow != null) {
				pDb.RateCollection.DeleteByRate_info_id_Type_of_day_choice(pRateInfoID, pTypeOfDayRow.Type_of_day_choice);
				pDb.TimeOfDayPeriodCollection.DeleteByRate_info_id_Type_of_day_choice(pRateInfoID, pTypeOfDayRow.Type_of_day_choice);
				pDb.TypeOfDayCollection.DeleteByPrimaryKey(pRateInfoID, pTypeOfDayRow.Type_of_day_choice);
			}
		}

		static void updateTypeOfDayRow(Rbr_Db pDb, int pRateInfoID, DayTypeDto pDayType, TypeOfDayRow pTypeOfDayRow, TypeOfDayRateEntryDto pTypeOfDayRateEntry) {
			if (pDayType.IsPotentiallyUnknownDayPolicy) {
				fixPotentiallyUnknownPolicy(pDayType, pTypeOfDayRow, pTypeOfDayRateEntry);
			}
			else {
				pTypeOfDayRow.TimeOfDayPolicy = pDayType.TODPolicy;
			}

			pDb.TypeOfDayCollection.Update(pTypeOfDayRow);
			pDb.RateCollection.DeleteByRate_info_id_Type_of_day_choice(pTypeOfDayRow.Rate_info_id, pTypeOfDayRow.Type_of_day_choice);
			pDb.TimeOfDayPeriodCollection.DeleteByRate_info_id_Type_of_day_choice(pTypeOfDayRow.Rate_info_id, pTypeOfDayRow.Type_of_day_choice);
			addTimePeriodRates(pDb, pRateInfoID, pTypeOfDayRow, pDayType, pTypeOfDayRateEntry);
		}

		static void createTypeOfDayRow(Rbr_Db pDb, int pRateInfoID, TypeOfDayChoice pTypeOfDayChoice, DayTypeDto pDayType, TypeOfDayRateEntryDto pTypeOfDayRateEntry) {
			TypeOfDayRow _typeOfDayRow = new TypeOfDayRow();
			_typeOfDayRow.Rate_info_id = pRateInfoID;
			_typeOfDayRow.TypeOfDayChoice = pTypeOfDayChoice;
			_typeOfDayRow.TimeOfDayPolicy = pDayType.TODPolicy;

			pDb.TypeOfDayCollection.Insert(_typeOfDayRow);
			addTimePeriodRates(pDb, pRateInfoID, _typeOfDayRow, pDayType, pTypeOfDayRateEntry);
		}

		static void fixPotentiallyUnknownPolicy(DayTypeDto pDayType, TypeOfDayRow pTypeOfDayRow, TypeOfDayRateEntryDto pTypeOfDayRateEntry) {
			#region NOTE

			/* Handle case when Only one DayTimePeriod AND it's for Blocked AND it covers the whole day (0-23)
              in this case we want to preserve:
              - Original TimeOfDayPolicy for TypeOfDayRow and 
              - Original TimeOfDay for DayType.DayTimePeriods and 
              - Original TimeOfDay for TypeOfDayRateEntry.Rates
              
		          single blocked line  GOOD: 
		          Brazil|55|ROC_11|Regular|Blocked|0am-11pm|0/0|0.0|0.0
						  */

			#endregion

			pDayType.TODPolicy = pTypeOfDayRow.TimeOfDayPolicy;
			if (pDayType.TODPolicy == TimeOfDayPolicy.Flat) {
				pDayType.DayTimePeriods[0].TimeOfDay = TimeOfDay.BlockedFlat;
				pTypeOfDayRateEntry.Rates[0].TimeOfDay = TimeOfDay.BlockedFlat;
			}
			else if (pDayType.TODPolicy == TimeOfDayPolicy.PeakOffPeak) {
				pDayType.DayTimePeriods[0].TimeOfDay = TimeOfDay.BlockedPeakOffPeak;
				pTypeOfDayRateEntry.Rates[0].TimeOfDay = TimeOfDay.BlockedPeakOffPeak;
			}
			else if (pDayType.TODPolicy == TimeOfDayPolicy.NightDayEve) {
				pDayType.DayTimePeriods[0].TimeOfDay = TimeOfDay.BlockedNightDayEve;
				pTypeOfDayRateEntry.Rates[0].TimeOfDay = TimeOfDay.BlockedNightDayEve;
			}
		}

		static void addTimePeriodRates(Rbr_Db_Base pDb, int pRateInfoID, TypeOfDayRow pTypeOfDayRow, DayTypeDto pDayType, TypeOfDayRateEntryDto pTypeOfDayRateEntry) {
			var _sortedRateRows = new SortedList();

			foreach (var _dayTime in pDayType.DayTimePeriods) {
				var _timeOfDayPeriodRow = mapToTimeOfDayPeriodRow(pTypeOfDayRow, _dayTime);
				pDb.TimeOfDayPeriodCollection.Insert(_timeOfDayPeriodRow);
			}

			foreach (var _rate in pTypeOfDayRateEntry.Rates) {
				var _rateRow = new RateRow
				                   	{
				                   		Rate_info_id = pRateInfoID, 
															TypeOfDayChoice = _rate.TypeOfDayChoice, 
															TimeOfDay = _rate.TimeOfDay
				                   	};

				if (_rate.TimeOfDay == TimeOfDay.BlockedFlat || _rate.TimeOfDay == TimeOfDay.BlockedPeakOffPeak || _rate.TimeOfDay == TimeOfDay.BlockedNightDayEve) {
					_rateRow.First_incr_length = 0;
					_rateRow.Add_incr_length = 0;
					_rateRow.First_incr_cost = decimal.Zero;
					_rateRow.Add_incr_cost = decimal.Zero;
				}
				else {
					_rateRow.First_incr_length = _rate.FirstIncrLen;
					_rateRow.Add_incr_length = _rate.AddIncrLen;
					_rateRow.First_incr_cost = _rate.FirstIncrCost;
					_rateRow.Add_incr_cost = _rate.AddIncrCost;
				}

				if (!_sortedRateRows.ContainsKey(_rateRow.TimeOfDay)) {
					_sortedRateRows.Add(_rateRow.TimeOfDay, _rateRow);
				}
			}

			//!!! sort before insert
			foreach (RateRow _rateRow in _sortedRateRows.Values) {
				pDb.RateCollection.Insert(_rateRow);
			}
		}

		static IRatingHistoryEntry getRatingHistoryEntryPart2(IRatingHistoryEntry pRatingHistoryEntry, DateTime pLastDate) {
			if (pRatingHistoryEntry.RouteType == RouteType.Carrier) {
				return new RatingHistoryEntry(RouteType.Carrier, pRatingHistoryEntry.RatedRouteId, pRatingHistoryEntry.LastDate.AddDays(1), pLastDate, null);
			}
			if (pRatingHistoryEntry.RouteType == RouteType.Wholesale) {
				return new RatingHistoryEntry(RouteType.Wholesale, pRatingHistoryEntry.RatedRouteId, pRatingHistoryEntry.LastDate.AddDays(1), pLastDate, null);
			}
				//else if (pRatingHistoryEntry.RouteType == RouteType.Retail) {
				//  return RetailRouteManager.GetNewRetailRateHistory(pRatingHistoryEntry as RetailRatingHistoryEntry, pLastDate);
				//}
			throw new ArgumentException("UNSUPPORTED Type : " + pRatingHistoryEntry.GetType(), "pRatingHistoryEntry");
		}

		static void addRatingHistoryEntry(Rbr_Db_Base pDb, IRatingHistoryEntry pRatingHistoryEntry) {
			if (pRatingHistoryEntry.RouteType == RouteType.Carrier) {
				var _existingCarrierRateHistoryRow = pDb.CarrierRateHistoryCollection.GetByRateInfoId(pRatingHistoryEntry.RateInfoId);
				if (_existingCarrierRateHistoryRow != null) {
					throw new Exception("UNEXPECTED: CarrierRateHistory already exists for RateInfoId:" + pRatingHistoryEntry.RateInfoId);
				}
				var _carrierRateHistoryRow = mapToCarrierRateHistoryRow(pRatingHistoryEntry);
				pDb.CarrierRateHistoryCollection.Insert(_carrierRateHistoryRow);
			}
			else if (pRatingHistoryEntry.RouteType == RouteType.Wholesale) {
				var _existingWholesaleRateHistoryRow = pDb.WholesaleRateHistoryCollection.GetByRateInfoId(pRatingHistoryEntry.RateInfoId);
				if (_existingWholesaleRateHistoryRow != null) {
					throw new Exception("UNEXPECTED: WholesaleRateHistory already exists for RateInfoId:" + pRatingHistoryEntry.RateInfoId);
				}
				var _wholesaleRateHistoryRow = MapToWholesaleRateHistoryRow(pRatingHistoryEntry);
				pDb.WholesaleRateHistoryCollection.Insert(_wholesaleRateHistoryRow);
			}
			else if (pRatingHistoryEntry.RouteType == RouteType.Retail) {
				//x.UpdateRetailRateHistory(pDb, pRatingHistoryEntry as RetailRatingHistoryEntry);
			}
			else {
				throw new ArgumentException("UNSUPPORTED Type : " + pRatingHistoryEntry.GetType(), "pRatingHistoryEntry");
			}
		}

		static void updateRatingHistoryEntry(Rbr_Db pDb, IRatingHistoryEntry pRatingHistoryEntry) {
			if (pRatingHistoryEntry.RouteType == RouteType.Carrier) {
				var _carrierRateHistoryRow = mapToCarrierRateHistoryRow(pRatingHistoryEntry);
				pDb.CarrierRateHistoryCollection.Update(_carrierRateHistoryRow);
			}
			else if (pRatingHistoryEntry.RouteType == RouteType.Wholesale) {
				var _wholesaleRateHistoryRow = MapToWholesaleRateHistoryRow(pRatingHistoryEntry);
				pDb.WholesaleRateHistoryCollection.Update(_wholesaleRateHistoryRow);
				//pDb.AddChangedObject(new CustomerRouteKey(TxType.Delete, pRatingHistoryEntry.RatedRouteId));
			}
			else if (pRatingHistoryEntry.RouteType == RouteType.Retail) {
				//x.UpdateRetailRateHistory(pDb, pRatingHistoryEntry as RetailRatingHistoryEntry);
			}
			else {
				throw new ArgumentException("UNSUPPORTED Type : " + pRatingHistoryEntry.GetType(), "pRatingHistoryEntry");
			}
		}

		static void deleteRatingHistoryEntry(Rbr_Db pDb, IRatingHistoryEntry pRatingHistoryEntry) {
			if (pRatingHistoryEntry.RouteType == RouteType.Carrier) {
				pDb.CarrierRateHistoryCollection.DeleteByRateInfoId(pRatingHistoryEntry.RateInfoId);
			}
			else if (pRatingHistoryEntry.RouteType == RouteType.Wholesale) {
				pDb.WholesaleRateHistoryCollection.DeleteByRateInfoId(pRatingHistoryEntry.RateInfoId);
				//pDb.AddChangedObject(new CustomerRouteKey(TxType.Delete, pRatingHistoryEntry.RatedRouteId));
			}
			else if (pRatingHistoryEntry.RouteType == RouteType.Retail) {
				//x.DeleteRetailRateHistory(pDb, pRatingHistoryEntry as RetailRatingHistoryEntry);
			}
			else {
				throw new ArgumentException("UNSUPPORTED Type : " + pRatingHistoryEntry.GetType(), "pRatingHistoryEntry");
			}
		}

		#region Handle Ranges

		static RatingHistoryEntry[] findGaps(Rbr_Db_Base pDb, int pRatedRouteId, RouteType pRouteType) {
			if (pRouteType == RouteType.Carrier) {
				var _list = new List<RatingHistoryEntry>();
				var _carrierRateHistoryRows = pDb.CarrierRateHistoryCollection.FindGaps(pRatedRouteId);
				foreach (var _carrierRateHistoryRow in _carrierRateHistoryRows) {
					_list.Add(MapToCarrierRatingHistoryEntry(_carrierRateHistoryRow, null));
				}
				return _list.ToArray();
			}
			if (pRouteType == RouteType.Wholesale) {
				var _list = new List<RatingHistoryEntry>();
				var _wholesaleRateHistoryRows = pDb.WholesaleRateHistoryCollection.FindGaps(pRatedRouteId);
				foreach (var _wholesaleRateHistoryRow in _wholesaleRateHistoryRows) {
					_list.Add(MapToWholesaleRatingHistoryEntry(_wholesaleRateHistoryRow, null));
				}
				return _list.ToArray();
			}
			return null;
		}

		static void saveRanges(Rbr_Db pDb, IRatingHistoryEntry pRatingHistoryEntry) {
			List<IRatingHistoryEntry> _toDeleteList;
			List<IRatingHistoryEntry> _toUpdateList;
			IRatingHistoryEntry _coveringRatingHistoryEntry;
			getRanges(pDb, pRatingHistoryEntry, out _toDeleteList, out _toUpdateList, out _coveringRatingHistoryEntry);
			processRanges(pDb, pRatingHistoryEntry, _toDeleteList, _toUpdateList, _coveringRatingHistoryEntry);
		}

		static void getRanges(Rbr_Db pDb, IRatingHistoryEntry pRatingHistoryEntry, out List<IRatingHistoryEntry> pToDeleteList, out List<IRatingHistoryEntry> pToUpdateList, out IRatingHistoryEntry pCoveringRatingHistoryEntry) {
			pToDeleteList = new List<IRatingHistoryEntry>();
			pToUpdateList = new List<IRatingHistoryEntry>();

			//1.1 ********************************************************************* 
			//get covered ranges
			IRatingHistoryEntry[] _coveredRatingHistoryEntries = getCoveredBy(pDb, pRatingHistoryEntry as RatingHistoryEntry);
			foreach (IRatingHistoryEntry _coveredRatingHistoryEntry in _coveredRatingHistoryEntries) {
				//covered in full - delete it
				//existing     |------------------|
				//     new     |------------------|
				//     new   |--------------------|
				//     new     |----------------------|
				//     new |----------------------------|
				//
				//result
				//existing     deleted
				//     new     inserted
				if (_coveredRatingHistoryEntry.FirstDate >= pRatingHistoryEntry.FirstDate && _coveredRatingHistoryEntry.LastDate <= pRatingHistoryEntry.LastDate) {
					pToDeleteList.Add(_coveredRatingHistoryEntry);
					continue;
				}

				//date on covered - set it's date_on to new.LastDate + 1
				//existing      |------------------|
				//     new      |-------------|
				//     new   |----------------|
				//
				//result
				//existing                     |---|  
				//     new     |--------------|
				if (_coveredRatingHistoryEntry.FirstDate >= pRatingHistoryEntry.FirstDate && _coveredRatingHistoryEntry.LastDate > pRatingHistoryEntry.LastDate) {
					_coveredRatingHistoryEntry.FirstDate = pRatingHistoryEntry.LastDate.AddDays(1);
					pToUpdateList.Add(_coveredRatingHistoryEntry);
					continue;
				}

				//date off covered - set it's date_off to new.FirstDate - 1
				//existing      |------------------|
				//     new           |---------------|
				//     new           |------------------|
				//
				//result
				//existing      |---|  
				//     new           |--------------|
				if (_coveredRatingHistoryEntry.FirstDate < pRatingHistoryEntry.FirstDate && _coveredRatingHistoryEntry.LastDate <= pRatingHistoryEntry.LastDate) {
					_coveredRatingHistoryEntry.LastDate = pRatingHistoryEntry.FirstDate.AddDays(-1);
					pToUpdateList.Add(_coveredRatingHistoryEntry);
					continue;
				}
			}

			//1.2 ********************************************************************* 
			//get covering ranges - split it in 2 ranges, clonning RatingInfo for the second part with >>> new Rate_info_id <<<
			//existing      |-----------------------------|
			//     new            |---------------|
			//
			//result
			//existing      |----|                 |------|  
			//     new            |---------------|
			pCoveringRatingHistoryEntry = getCoveringFor(pDb, pRatingHistoryEntry as RatingHistoryEntry);

			//Update list cannot be more then 2
			if (pToUpdateList.Count > 2) {
				throw new Exception("Update list cannot be more then 2");
			}

			if (_coveredRatingHistoryEntries.Length > 0 && pCoveringRatingHistoryEntry != null) {
				throw new Exception("UNEXPECTED: covered and covering exist at the same time.");
			}
		}

		static void processRanges(Rbr_Db pDb, IRatingHistoryEntry pRatingHistoryEntry, IEnumerable<IRatingHistoryEntry> pToDeleteList, ICollection<IRatingHistoryEntry> pToUpdateList, IRatingHistoryEntry pCoveringRatingHistoryEntryPart1) {
			if (pCoveringRatingHistoryEntryPart1 != null) {
				//1.2.1 ******************************************************************
				//get original covering RatingInfo
				RatingInfoDto _coveringPart1RatingInfo = GetRatingInfo(pDb, pCoveringRatingHistoryEntryPart1.RateInfoId, false);

				//create second part out of covering
				IRatingHistoryEntry _coveringRatingHistoryEntryPart2 = getRatingHistoryEntryPart2(pRatingHistoryEntry, pCoveringRatingHistoryEntryPart1.LastDate);

				//clone RatingInfo for part from original
				RatingInfoDto _coveringPart2RatingInfo = _coveringPart1RatingInfo.Clone();
				_coveringPart2RatingInfo.RateInfoId = 0;

				//Insert new RatingInfo for part 2
				UpdateRatingInfo(pDb, _coveringPart2RatingInfo);
				_coveringRatingHistoryEntryPart2.RatingInfo = _coveringPart2RatingInfo;

				//Insert new part 2
				addRatingHistoryEntry(pDb, _coveringRatingHistoryEntryPart2);

				//1.2.2 *********************************************************************
				//change original's date off to new.FirstDate - 1
				pCoveringRatingHistoryEntryPart1.LastDate = pRatingHistoryEntry.FirstDate.AddDays(-1);
				pToUpdateList.Add(pCoveringRatingHistoryEntryPart1);
			}

			//2. delete  *******************************************************************
			foreach (IRatingHistoryEntry _ratingHistoryEntry in pToDeleteList) {
				deleteRatingHistoryEntry(pDb, _ratingHistoryEntry);
				DeleteRatingInfo(pDb, _ratingHistoryEntry.RateInfoId);
			}
			//3. update ********************************************************************
			foreach (IRatingHistoryEntry _ratingHistoryEntry in pToUpdateList) {
				//NOTE: update by rate_info_id
				updateRatingHistoryEntry(pDb, _ratingHistoryEntry);
			}
		}

		static void fillRangeGaps(Rbr_Db pDb, IRatingHistoryEntry pRatingHistoryEntry, ICollection<IRatingHistoryEntry> pRatingHistoryEntryGaps) {
			if (pRatingHistoryEntryGaps != null) {
				if (pRatingHistoryEntryGaps.Count > 2) {
					throw new ArgumentOutOfRangeException("pRatingHistoryEntryGaps", "Cannot have more then 2 gaps; found:" + pRatingHistoryEntryGaps.Count);
				}

				foreach (var _ratingHistoryEntryGap in pRatingHistoryEntryGaps) {
					fillRatingHistoryEntryGap(pDb, pRatingHistoryEntry, _ratingHistoryEntryGap);
				}
			}
		}

		static void fillRatingHistoryEntryGap(Rbr_Db pDb, IRatingHistoryEntry pRatingHistoryEntry, IRatingHistoryEntry pRatingHistoryEntryGap) {
			if (pRatingHistoryEntry.RouteType == RouteType.Carrier) {
				var _route = CarrierRouteManager.Get(pDb, pRatingHistoryEntry.RatedRouteId);
				pRatingHistoryEntryGap.RatingInfo = _route.DefaultRatingInfo.CloneWithNoRates();
				UpdateRatingInfo(pDb, pRatingHistoryEntryGap.RatingInfo);
				addRatingHistoryEntry(pDb, pRatingHistoryEntryGap as RatingHistoryEntry);
			}
			else if (pRatingHistoryEntry.RouteType == RouteType.Wholesale) {
				RatedRouteDto _route = CustomerRouteManager.Get(pDb, pRatingHistoryEntry.RatedRouteId);
				pRatingHistoryEntryGap.RatingInfo = _route.DefaultRatingInfo.CloneWithNoRates();
				UpdateRatingInfo(pDb, pRatingHistoryEntryGap.RatingInfo);
				addRatingHistoryEntry(pDb, pRatingHistoryEntryGap as RatingHistoryEntry);
			}
			else if (pRatingHistoryEntry.RouteType == RouteType.Retail) {
				//x.AddRetailRateHistory(pDb, pRatingHistoryEntry as RetailRatingHistoryEntry);
			}
			else {
				throw new ArgumentException("UNSUPPORTED Type : " + pRatingHistoryEntry.GetType(), "pRatingHistoryEntry");
			}
		}

		static IRatingHistoryEntry[] getCoveredBy(Rbr_Db pDb, IRatingHistoryEntry pRatingHistoryEntry) {
			var _list = new List<RatingHistoryEntry>();
			if (pRatingHistoryEntry.RouteType == RouteType.Carrier) {
				var _carrierRateHistoryRows = pDb.CarrierRateHistoryCollection.GetCoveredBy(pRatingHistoryEntry.RatedRouteId, pRatingHistoryEntry.FirstDate, pRatingHistoryEntry.LastDate);
				foreach (CarrierRateHistoryRow _carrierRateHistoryRow in _carrierRateHistoryRows) {
					var _ratingInfo = GetRatingInfo(pDb, _carrierRateHistoryRow.Rate_info_id, false);
					_list.Add(MapToCarrierRatingHistoryEntry(_carrierRateHistoryRow, _ratingInfo));
				}
				return _list.ToArray();
			}
			if (pRatingHistoryEntry.RouteType == RouteType.Wholesale) {
				var _wholesaleRateHistoryRows = pDb.WholesaleRateHistoryCollection.GetCoveredBy(pRatingHistoryEntry.RatedRouteId, pRatingHistoryEntry.FirstDate, pRatingHistoryEntry.LastDate);
				foreach (var _wholesaleRateHistoryRow in _wholesaleRateHistoryRows) {
					var _ratingInfo = GetRatingInfo(pDb, _wholesaleRateHistoryRow.Rate_info_id, false);
					_list.Add(MapToWholesaleRatingHistoryEntry(_wholesaleRateHistoryRow, _ratingInfo));
				}
				return _list.ToArray();
			}
			return null;
		}

		static IRatingHistoryEntry getCoveringFor(Rbr_Db pDb, IRatingHistoryEntry pRatingHistoryEntry) {
			if (pRatingHistoryEntry.RouteType == RouteType.Carrier) {
				var _carrierRateHistoryRow = pDb.CarrierRateHistoryCollection.GetCoveringFor(pRatingHistoryEntry.RatedRouteId, pRatingHistoryEntry.FirstDate, pRatingHistoryEntry.LastDate);
				if (_carrierRateHistoryRow != null) {
					var _ratingInfo = GetRatingInfo(pDb, _carrierRateHistoryRow.Rate_info_id, false);
					return MapToCarrierRatingHistoryEntry(_carrierRateHistoryRow, _ratingInfo);
				}
			}
			else if (pRatingHistoryEntry.RouteType == RouteType.Wholesale) {
				WholesaleRateHistoryRow _wholesaleRateHistoryRow = pDb.WholesaleRateHistoryCollection.GetCoveringFor(pRatingHistoryEntry.RatedRouteId, pRatingHistoryEntry.FirstDate, pRatingHistoryEntry.LastDate);
				if (_wholesaleRateHistoryRow != null) {
					RatingInfoDto _ratingInfo = GetRatingInfo(pDb, _wholesaleRateHistoryRow.Rate_info_id, false);
					return MapToWholesaleRatingHistoryEntry(_wholesaleRateHistoryRow, _ratingInfo);
				}
			}
			return null;
		}

		#endregion Handle Ranges

		#region Validation

		static void validateRatingHistoryEntry(IRatingHistoryEntry pRatingHistoryEntry) {
			if (pRatingHistoryEntry.FirstDate > pRatingHistoryEntry.LastDate) {
				throw new ArgumentOutOfRangeException("pRatingHistoryEntry", "RatingHistoryEntry.FirstDate, 'First Date' cannot be grater then 'Last Date'");
			}
			if (pRatingHistoryEntry.RatingInfo.RegularDay.IsSelected) {
				if (pRatingHistoryEntry.RatingInfo.RegularDayRateEntry.TypeOfDayChoice != TypeOfDayChoice.RegularDay) {
					throw new ArgumentException("Invalid RatingInfo.RegularDayRateEntry.TypeOfDayChoice: " + pRatingHistoryEntry.RatingInfo.RegularDayRateEntry.TypeOfDayChoice);
				}
				validateTimePeriods(pRatingHistoryEntry.RatingInfo.RegularDay.DayTimePeriods, pRatingHistoryEntry.RatingInfo.RegularDay.TODPolicy);
				validateRates(pRatingHistoryEntry.RatingInfo.RegularDayRateEntry.Rates, pRatingHistoryEntry.RatingInfo.RegularDayRateEntry.TypeOfDayChoice, pRatingHistoryEntry.RatingInfo.RegularDay.TODPolicy);
			}
			if (pRatingHistoryEntry.RatingInfo.Weekend.IsSelected) {
				if (pRatingHistoryEntry.RatingInfo.WeekendRateEntry.TypeOfDayChoice != TypeOfDayChoice.Weekend) {
					throw new ArgumentException("Invalid RatingInfo.WeekendRateEntry.TypeOfDayChoice: " + pRatingHistoryEntry.RatingInfo.WeekendRateEntry.TypeOfDayChoice);
				}
				validateTimePeriods(pRatingHistoryEntry.RatingInfo.Weekend.DayTimePeriods, pRatingHistoryEntry.RatingInfo.Weekend.TODPolicy);
				validateRates(pRatingHistoryEntry.RatingInfo.WeekendRateEntry.Rates, pRatingHistoryEntry.RatingInfo.WeekendRateEntry.TypeOfDayChoice, pRatingHistoryEntry.RatingInfo.Weekend.TODPolicy);
			}
			if (pRatingHistoryEntry.RatingInfo.Holiday.IsSelected) {
				if (pRatingHistoryEntry.RatingInfo.HolidayRateEntry.TypeOfDayChoice != TypeOfDayChoice.Holiday) {
					throw new ArgumentException("Invalid RatingInfo.HolidayRateEntry.TypeOfDayChoice: " + pRatingHistoryEntry.RatingInfo.HolidayRateEntry.TypeOfDayChoice);
				}
				validateTimePeriods(pRatingHistoryEntry.RatingInfo.Holiday.DayTimePeriods, pRatingHistoryEntry.RatingInfo.Holiday.TODPolicy);
				validateRates(pRatingHistoryEntry.RatingInfo.HolidayRateEntry.Rates, pRatingHistoryEntry.RatingInfo.HolidayRateEntry.TypeOfDayChoice, pRatingHistoryEntry.RatingInfo.Holiday.TODPolicy);
			}
		}

		static void validateRates(IEnumerable<RateDto> pRates, TypeOfDayChoice pTypeOfDayChoice, TimeOfDayPolicy pTimeOfDayPolicy) {
			foreach (RateDto _rate in pRates) {
				//-- validateTypeOfDayChoice
				if (_rate.TypeOfDayChoice != pTypeOfDayChoice) {
					throw new ArgumentException("Invalid Rate.TypeOfDayChoice: [" + _rate.TypeOfDayChoice + "] for RateInfo.TypeOfDayChoice: [" + pTypeOfDayChoice + "]");
				}

				if (pTimeOfDayPolicy == TimeOfDayPolicy.Flat) {
					if (_rate.TimeOfDay != TimeOfDay.BlockedFlat && _rate.TimeOfDay != TimeOfDay.Flat) {
						throw new ArgumentException("Invalid Rate.TimeOfDay: [" + _rate.TimeOfDay + "] for TimeOfDayPolicy: [" + pTimeOfDayPolicy + "]");
					}
				}
				else if (pTimeOfDayPolicy == TimeOfDayPolicy.PeakOffPeak) {
					if (_rate.TimeOfDay != TimeOfDay.BlockedPeakOffPeak && _rate.TimeOfDay != TimeOfDay.Peak && _rate.TimeOfDay != TimeOfDay.OffPeak) {
						throw new ArgumentException("Invalid Rate.TimeOfDay: [" + _rate.TimeOfDay + "] for TimeOfDayPolicy: [" + pTimeOfDayPolicy + "]");
					}
				}
				else if (pTimeOfDayPolicy == TimeOfDayPolicy.NightDayEve) {
					if (_rate.TimeOfDay != TimeOfDay.BlockedNightDayEve && _rate.TimeOfDay != TimeOfDay.Night && _rate.TimeOfDay != TimeOfDay.Day && _rate.TimeOfDay != TimeOfDay.Eve) {
						throw new ArgumentException("Invalid Rate.TimeOfDay: [" + _rate.TimeOfDay + "] for TimeOfDayPolicy: [" + pTimeOfDayPolicy + "]");
					}
				}

				//-- validate increments
				if (_rate.TimeOfDay != TimeOfDay.BlockedFlat && _rate.TimeOfDay != TimeOfDay.BlockedPeakOffPeak && _rate.TimeOfDay != TimeOfDay.BlockedNightDayEve) {
					foreach (string _incr in AppConstants.Increments) {
						if (_incr == _rate.Increment) {
							return;
						}
					}
					throw new NotSupportedException(string.Format("Unsupported Increment value: [{0}]", _rate.Increment));
				}
			}
		}

		static void validateTimePeriods(DayTimeDto[] pDayTimePeriods, TimeOfDayPolicy pTimeOfDayPolicy) {
			if (pDayTimePeriods == null || pDayTimePeriods.Length == 0) {
				throw new ArgumentException("No Time periods provided");
			}
			if (pDayTimePeriods.Length == 1 && (pDayTimePeriods[0].TimeOfDay == TimeOfDay.BlockedFlat || pDayTimePeriods[0].TimeOfDay == TimeOfDay.BlockedPeakOffPeak || pDayTimePeriods[0].TimeOfDay == TimeOfDay.BlockedNightDayEve)) {
				if (pDayTimePeriods[0].Start != 0 && pDayTimePeriods[0].Stop != 23) {
					//NOTE: if only one TimePeriod and if this TimePeriod is for Blocked* - it has to cover whole Day 0-23 hours
					throw new ArgumentException("Invalid Time Period Range for TimeOfDayPolicy: [" + pTimeOfDayPolicy + "]  TimeOfDay: [" + pDayTimePeriods[0].TimeOfDay + "]");
				}
			}

			var _sorted = new SortedList<int, DayTimeDto>();
			foreach (DayTimeDto _dayTime in pDayTimePeriods) {
				validateTimePeriod(_dayTime, pTimeOfDayPolicy);

				if (_sorted.ContainsKey(_dayTime.Start)) {
					throw new ArgumentException("Duplicates in Time Start. Start: [" + _dayTime.Start + "] ; Stop: [" + _dayTime.Stop + "]");
				}
				_sorted.Add(_dayTime.Start, _dayTime);
			}

			int _lastStop = -1;
			foreach (DayTimeDto _dayTime in _sorted.Values) {
				if (_lastStop + 1 != _dayTime.Start) {
					throw new ArgumentException("Gaps are not allowed in Time ranges. Start: [" + _dayTime.Start + "] ; Stop: [" + _dayTime.Stop + "]");
				}
				_lastStop = _dayTime.Stop;
			}
			if (_lastStop != 23) {
				throw new ArgumentException("Gaps are not allowed in Time ranges. Stop: [" + _lastStop + "]");
			}
		}

		static void validateTimePeriod(DayTimeDto pDayTimePeriod, TimeOfDayPolicy pTimeOfDayPolicy) {
			if (pDayTimePeriod.Start > pDayTimePeriod.Stop) {
				throw new ArgumentException("Time Start cannot be grater then Time Stop. Start: [" + pDayTimePeriod.Start + "] ; Stop: [" + pDayTimePeriod.Stop + "]");
			}
			if (pTimeOfDayPolicy == TimeOfDayPolicy.Flat) {
				if (pDayTimePeriod.TimeOfDay != TimeOfDay.BlockedFlat && pDayTimePeriod.TimeOfDay != TimeOfDay.Flat) {
					throw new ArgumentException("Invalid DayTimePeriod.TimeOfDay: [" + pDayTimePeriod.TimeOfDay + "] for TimeOfDayPolicy: [" + pTimeOfDayPolicy + "]");
				}
			}
			else if (pTimeOfDayPolicy == TimeOfDayPolicy.PeakOffPeak) {
				if (pDayTimePeriod.TimeOfDay != TimeOfDay.BlockedPeakOffPeak && pDayTimePeriod.TimeOfDay != TimeOfDay.Peak && pDayTimePeriod.TimeOfDay != TimeOfDay.OffPeak) {
					throw new ArgumentException("Invalid DayTimePeriod.TimeOfDay: [" + pDayTimePeriod.TimeOfDay + "] for TimeOfDayPolicy: [" + pTimeOfDayPolicy + "]");
				}
			}
			else if (pTimeOfDayPolicy == TimeOfDayPolicy.NightDayEve) {
				if (pDayTimePeriod.TimeOfDay != TimeOfDay.BlockedNightDayEve && pDayTimePeriod.TimeOfDay != TimeOfDay.Night && pDayTimePeriod.TimeOfDay != TimeOfDay.Day && pDayTimePeriod.TimeOfDay != TimeOfDay.Eve) {
					throw new ArgumentException("Invalid DayTimePeriod.TimeOfDay: [" + pDayTimePeriod.TimeOfDay + "] for TimeOfDayPolicy: [" + pTimeOfDayPolicy + "]");
				}
			}
		}

		#endregion validation

		#endregion privates

		#region mappings

		internal static WholesaleRateHistoryRow MapToWholesaleRateHistoryRow(IRatingHistoryEntry pWholesaleRatingHistoryEntry) {
			if (pWholesaleRatingHistoryEntry == null) {
				return null;
			}

			var _wholesaleRateHistoryRow = new WholesaleRateHistoryRow
			                               	{
			                               		Wholesale_route_id = pWholesaleRatingHistoryEntry.RatedRouteId, 
																				Date_on = pWholesaleRatingHistoryEntry.FirstDate, 
																				Date_off = pWholesaleRatingHistoryEntry.LastDate, 
																				Rate_info_id = pWholesaleRatingHistoryEntry.RateInfoId
			                               	};

			return _wholesaleRateHistoryRow;
		}

		static DayTypeDto mapToDayType(TypeOfDayRow pTypeOfDayRow, IEnumerable<TimeOfDayPeriodRow> pTimeOfDayPeriodRows) {
			var _list = new ArrayList();

			foreach (var _timeOfDayPeriodRow in pTimeOfDayPeriodRows) {
				var _dayTime = new DayTimeDto(_timeOfDayPeriodRow.TimeOfDay, _timeOfDayPeriodRow.Start_hour, _timeOfDayPeriodRow.Stop_hour);
				_list.Add(_dayTime);
			}
			var _dayTimes = (DayTimeDto[]) _list.ToArray(typeof (DayTimeDto));
			var _dayType = new DayTypeDto(_dayTimes, pTypeOfDayRow.TimeOfDayPolicy, true, false);
			return _dayType;
		}

		static TimeOfDayPeriodRow[] mapToTimeOfDayPeriodRows(TypeOfDayRow pTypeOfDayRow, IEnumerable<DayTimeDto> pDayTimes) {
			var _list = new ArrayList();
			foreach (var _dayTime in pDayTimes) {
				var _timeOfDayPeriodRow = mapToTimeOfDayPeriodRow(pTypeOfDayRow, _dayTime);
				_list.Add(_timeOfDayPeriodRow);
			}
			return (TimeOfDayPeriodRow[]) _list.ToArray(typeof (TimeOfDayPeriodRow));
		}

		static TimeOfDayPeriodRow mapToTimeOfDayPeriodRow(TypeOfDayRow pTypeOfDayRow, DayTimeDto pDayTime) {
			var _timeOfDayPeriodRow = new TimeOfDayPeriodRow();
			_timeOfDayPeriodRow.Rate_info_id = pTypeOfDayRow.Rate_info_id;
			_timeOfDayPeriodRow.TypeOfDayChoice = pTypeOfDayRow.TypeOfDayChoice;
			_timeOfDayPeriodRow.TimeOfDay = pDayTime.TimeOfDay;
			_timeOfDayPeriodRow.Start_hour = (short) pDayTime.Start;
			_timeOfDayPeriodRow.Stop_hour = (short) pDayTime.Stop;
			return _timeOfDayPeriodRow;
		}

		static TypeOfDayRateEntryDto mapToTypeOfDayRateEntry(TypeOfDayRow pTypeOfDayRow, IEnumerable<RateRow> pRateRows) {
			var _rateEntry = new TypeOfDayRateEntryDto();
			var _list = new ArrayList();
			foreach (var _rateRow in pRateRows) {
				_list.Add(mapToRate(_rateRow));
			}
			_rateEntry.RateInfoId = pTypeOfDayRow.Rate_info_id;
			_rateEntry.TypeOfDayChoice = pTypeOfDayRow.TypeOfDayChoice;
			_rateEntry.Rates = (RateDto[]) _list.ToArray(typeof (RateDto));
			return _rateEntry;
		}

		static RateDto mapToRate(RateRow pRateRow) {
			var _rate = new RateDto();
			_rate.RateInfoId = pRateRow.Rate_info_id;
			_rate.TimeOfDay = pRateRow.TimeOfDay;
			_rate.TypeOfDayChoice = pRateRow.TypeOfDayChoice;
			_rate.AddIncrCost = pRateRow.Add_incr_cost;
			_rate.AddIncrLen = pRateRow.Add_incr_length;
			_rate.FirstIncrCost = pRateRow.First_incr_cost;
			_rate.FirstIncrLen = pRateRow.First_incr_length;
			return _rate;
		}

		static RateRow[] mapToRateRows(IEnumerable<RateDto> pRates, TypeOfDayChoice pTypeOfDayChoice) {
			var _list = new ArrayList();
			if (pRates != null) {
				foreach (var _rate in pRates) {
					var _rateRow = mapToRateRow(_rate, pTypeOfDayChoice);
					_list.Add(_rateRow);
				}
			}
			return (RateRow[]) _list.ToArray(typeof (RateRow));
		}

		static RateRow mapToRateRow(RateDto pRate, TypeOfDayChoice pTypeOfDayChoice) {
			var _rateRow = new RateRow();
			_rateRow.Add_incr_cost = pRate.AddIncrCost;
			_rateRow.Add_incr_length = pRate.AddIncrLen;
			_rateRow.First_incr_cost = pRate.FirstIncrCost;
			_rateRow.First_incr_length = pRate.FirstIncrLen;
			_rateRow.Rate_info_id = pRate.RateInfoId;
			_rateRow.TimeOfDay = pRate.TimeOfDay;
			_rateRow.TypeOfDayChoice = pTypeOfDayChoice;

			return _rateRow;
		}

		static HolidayDto[] mapToHolidays(IEnumerable<HolidayCalendarRow> pHolidayCalendarRows) {
			var _list = new ArrayList();
			if (pHolidayCalendarRows != null) {
				foreach (var _holidayCalendarRow in pHolidayCalendarRows) {
					_list.Add(mapToHoliday(_holidayCalendarRow));
				}
			}

			return (HolidayDto[]) _list.ToArray(typeof (HolidayDto));
		}

		static HolidayDto mapToHoliday(HolidayCalendarRow_Base pHolidayCalendarRow) {
			return new HolidayDto(pHolidayCalendarRow.Holiday_day, pHolidayCalendarRow.Name);
		}

		static HolidayCalendarRow[] mapToHolidayCalendarRows(int pRateInfoID, IEnumerable<HolidayDto> pHolidays) {
			var _list = new ArrayList();
			if (pHolidays != null) {
				foreach (var _holiday in pHolidays) {
					_list.Add(mapToHolidayCalendarRow(pRateInfoID, _holiday));
				}
			}
			return (HolidayCalendarRow[]) _list.ToArray(typeof (HolidayCalendarRow));
		}

		static HolidayCalendarRow mapToHolidayCalendarRow(int pRateInfoID, HolidayDto pHoliday) {
			var _holidayCalendarRow = new HolidayCalendarRow();
			_holidayCalendarRow.Rate_info_id = pRateInfoID;
			_holidayCalendarRow.Holiday_day = pHoliday.Date;
			_holidayCalendarRow.Name = pHoliday.Name;
			return _holidayCalendarRow;
		}

		internal static RatingHistoryEntry MapToCarrierRatingHistoryEntry(CarrierRateHistoryRow pCarrierRateHistoryRow, RatingInfoDto pRatingInfo) {
			if (pCarrierRateHistoryRow == null) {
				return null;
			}

			var _ratingHistoryEntry = new RatingHistoryEntry(RouteType.Carrier, pCarrierRateHistoryRow.Carrier_route_id, pCarrierRateHistoryRow.Date_on, pCarrierRateHistoryRow.Date_off, pRatingInfo);
			_ratingHistoryEntry.HasChanged = false;
			return _ratingHistoryEntry;
		}

		internal static RatingHistoryEntry MapToWholesaleRatingHistoryEntry(WholesaleRateHistoryRow pWholesaleRateHistoryRow, RatingInfoDto pRatingInfo) {
			if (pWholesaleRateHistoryRow == null) {
				return null;
			}

			var _ratingHistoryEntry = new RatingHistoryEntry(RouteType.Wholesale, pWholesaleRateHistoryRow.Wholesale_route_id, pWholesaleRateHistoryRow.Date_on, pWholesaleRateHistoryRow.Date_off, pRatingInfo);
			_ratingHistoryEntry.HasChanged = false;
			return _ratingHistoryEntry;
		}

		static CarrierRateHistoryRow mapToCarrierRateHistoryRow(IRatingHistoryEntry pCarrierRatingHistoryEntry) {
			if (pCarrierRatingHistoryEntry == null) {
				return null;
			}

			var _carrierRateHistoryRow = new CarrierRateHistoryRow();
			_carrierRateHistoryRow.Carrier_route_id = pCarrierRatingHistoryEntry.RatedRouteId;
			_carrierRateHistoryRow.Date_on = pCarrierRatingHistoryEntry.FirstDate;
			_carrierRateHistoryRow.Date_off = pCarrierRatingHistoryEntry.LastDate;
			_carrierRateHistoryRow.Rate_info_id = pCarrierRatingHistoryEntry.RateInfoId;

			return _carrierRateHistoryRow;
		}

		#endregion mappings
	}
}
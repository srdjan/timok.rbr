using System;
using Timok.Logger;
using Timok.Rbr.Core.Config;
using Timok.Rbr.DAL.RbrDatabase;
using Timok.Rbr.DOM;

namespace Timok.Rbr.BLL.DOM {
	[Serializable]
	public sealed class RateInfo {
		public int Id;

		TypeOfDayEntity currentRates;
		public TypeOfDayChoice CurrentTypeOfDayChoice {
			set {
				if (value == TypeOfDayChoice.RegularDay) {
					currentRates = regularDayRates;
				}
				else if (value == TypeOfDayChoice.Weekend) {
					currentRates = weekendRates;
				}
				if (value == TypeOfDayChoice.Holiday) {
					currentRates = holidayRates;
				}
			}
		}

		readonly DateTime dateTime;
		readonly HolidayCalendarRow[] holidayCalendarRows;
		readonly TypeOfDayEntity holidayRates;

		readonly TypeOfDayEntity regularDayRates;
		readonly TypeOfDayEntity weekendRates;

		//----------------------- Private Ctor ------------------------------------------------------------
		RateInfo(int pRateInfoId, DateTime pDateTime) {
			Id = pRateInfoId;
			dateTime = pDateTime;

			regularDayRates = null;
			weekendRates = null;
			holidayRates = null;

			using (var _db = new Rbr_Db()) {
				var _typeOfDayRows = _db.TypeOfDayCollection.GetByRate_info_id(pRateInfoId);
				if (_typeOfDayRows == null || _typeOfDayRows.Length == 0) {
					throw new Exception("Rates.Ctor | NO TypeOfDay RATES: RateInfoId: " + pRateInfoId);
				}

				foreach (var _typeOfDayRow in _typeOfDayRows) {
					switch (_typeOfDayRow.TypeOfDayChoice) {
					case TypeOfDayChoice.RegularDay:
						regularDayRates = new TypeOfDayEntity(_typeOfDayRow.Rate_info_id, _typeOfDayRow.TypeOfDayChoice);
						break;
					case TypeOfDayChoice.Weekend:
						weekendRates = new TypeOfDayEntity(_typeOfDayRow.Rate_info_id, _typeOfDayRow.TypeOfDayChoice);
						break;
					case TypeOfDayChoice.Holiday:
						holidayCalendarRows = _db.HolidayCalendarCollection.GetByRate_info_id(pRateInfoId);
						holidayRates = new TypeOfDayEntity(_typeOfDayRow.Rate_info_id, _typeOfDayRow.TypeOfDayChoice);
						break;
					}
				}
			}

			if (regularDayRates == null) {
				throw new Exception(string.Format("Error: No RegularDay Rates, RateInfoId={0}, Datetime={1}", pRateInfoId, pDateTime));
			}

			//-- set currentTypeOf Day
			currentRates = null;
			if (holidayRates != null && isHoliday(dateTime)) {
				currentRates = holidayRates;
			}
			else if (weekendRates != null && (dateTime.DayOfWeek == DayOfWeek.Saturday || dateTime.DayOfWeek == DayOfWeek.Sunday)) {
				currentRates = weekendRates;
			}
			else {
				currentRates = regularDayRates;
			}
		}

		//-------------------------------------- Public static ----------------------------------------------
		public static RateInfo GetWholesaleRateInfo(int pRouteId, DateTime pDateHour) {
			RateInfo _rateInfo = null;

			WholesaleRateHistoryRow _wholesaleRateHistory;
			try {
				using (var _db = new Rbr_Db()) {
					_wholesaleRateHistory = _db.WholesaleRateHistoryCollection.GetByWholesaleRouteIdDate(pRouteId, pDateHour.Date);
				}
			}
			catch (Exception _ex) {
				TimokLogger.Instance.LogRbr(LogSeverity.Critical, "RateInfo.GetWholesaleRateInfo", string.Format("Get Wholesale RateHistory: {0}, {1}, Exception:\r\n{2}", pRouteId, pDateHour.Date, _ex));
				return _rateInfo;
			}
			if (_wholesaleRateHistory == null) {
				TimokLogger.Instance.LogRbr(LogSeverity.Critical, "RateInfo.GetWholesaleRateInfo", string.Format("Missing Wholesale RateHistory: {0}, {1}", pRouteId, pDateHour.Date));
				return _rateInfo;
			}

			try {
				_rateInfo = new RateInfo(_wholesaleRateHistory.Rate_info_id, pDateHour);
			}
			catch (Exception _ex) {
				TimokLogger.Instance.LogRbr(LogSeverity.Critical,
				                            "RateInfo.GetWholesaleRateInfo",
				                            string.Format("Missing Wholesale RateInfo: {0}, Exception:\r\n{1}", _wholesaleRateHistory.Rate_info_id, _ex));
			}

			return _rateInfo;
		}

		public static RateInfo GetCarrierRateInfo(int pRouteId, DateTime pDateHour) {
			RateInfo _rateInfo = null;

			CarrierRateHistoryRow _carrierRateHistory;
			try {
				using (var _db = new Rbr_Db()) {
					_carrierRateHistory = _db.CarrierRateHistoryCollection.GetByCarrierRouteIdDate(pRouteId, pDateHour.Date);
				}
			}
			catch (Exception _ex) {
				TimokLogger.Instance.LogRbr(LogSeverity.Critical, "RateInfo.GetCarrierRateInfo", string.Format("Get Carrier RateHistory: {0}, {1}, Exception:\r\n{2}", pRouteId, pDateHour.Date, _ex));
				return _rateInfo;
			}
			if (_carrierRateHistory == null) {
				TimokLogger.Instance.LogRbr(LogSeverity.Critical, "RateInfo.GetCarrierRateInfo", string.Format("Missing Carrier RateHistory: {0}, {1}", pRouteId, pDateHour.Date));
				return _rateInfo;
			}

			try {
				_rateInfo = new RateInfo(_carrierRateHistory.Rate_info_id, pDateHour);
			}
			catch (Exception _ex) {
				TimokLogger.Instance.LogRbr(LogSeverity.Critical, "RateInfo.GetCarrierRateInfo", string.Format("Missing Carrier RateInfo: {0}, Exception:\r\n{1}", _carrierRateHistory.Rate_info_id, _ex));
			}

			return _rateInfo;
		}

		//----------------------------------------- Public -------------------------------------------------------------
		public int GetNormalizedCost() {
			return currentRates.GetNormalizedCost(dateTime);
		}

		public int GetTimeLimit(decimal pCurrentBalance, SurchargeInfo pAccessNumberSurcharge, SurchargeInfo pPayphoneSurcharge) {
			return currentRates.GetTimeLimit(dateTime, pCurrentBalance, pAccessNumberSurcharge, pPayphoneSurcharge);
		}

		public decimal GetPerCallCost() {
			return regularDayRates.GetCost();
		}

		public decimal GetCost(int pDuration, SurchargeInfo pAccessNumberSurcharge, SurchargeInfo pPayphoneSurcharge, out short pRoundedSeconds) {
			return currentRates.GetCost(dateTime, pDuration, pAccessNumberSurcharge, pPayphoneSurcharge, out pRoundedSeconds);
		}

		//-------------------------------- Private --------------------------------------------------------------
		bool isHoliday(DateTime pDate) {
			if (holidayCalendarRows == null || holidayCalendarRows.Length == 0) {
				return false;
			}
			foreach (HolidayCalendarRow _holidayCalendarRow in holidayCalendarRows) {
				if (_holidayCalendarRow.Holiday_day.Year == pDate.Year && _holidayCalendarRow.Holiday_day.Month == pDate.Month && _holidayCalendarRow.Holiday_day.Day == pDate.Day) {
					return true;
				}
			}
			return false;
		}
	}
}
using System;
using System.Collections.Generic;
using Timok.Logger;
using Timok.Rbr.Core.Config;
using Timok.Rbr.DAL.RbrDatabase;
using Timok.Rbr.DOM;

namespace Timok.Rbr.BLL.DOM {
	[Serializable]
	internal sealed class TypeOfDayEntity {
		readonly int rateInfoId;
		Rate[] rates;

		internal TypeOfDayEntity(int pRateInfoId, TypeOfDayChoice pTypeOfDayChoice) {
			rateInfoId = pRateInfoId;
			loadRates(pRateInfoId, pTypeOfDayChoice);
		}

		public int GetNormalizedCost(DateTime pDateTime) {
			var _rate = getRate(pDateTime.Hour);
			return _rate != null ? _rate.GetNormalizedCost() : int.MaxValue;
		}

		public int GetTimeLimit(DateTime pDateTime, decimal pCurrentBalance, SurchargeInfo pAccessNumberSurcharge, SurchargeInfo pPayphoneSurcharge) {
			var _rate = getRate(pDateTime.Hour);
			if (_rate.IsBlocked) {
				TimokLogger.Instance.LogRbr(LogSeverity.Error, "TypeOfDayEntity.GetTimeLimit", string.Format("Rate is BLOCKED, rateInfoId: {0}", rateInfoId));
				return 0;
			}
			return _rate.GetTimeLimit(pCurrentBalance, pAccessNumberSurcharge, pPayphoneSurcharge);
		}

		public decimal GetCost() {
			var _rate = getRate(DateTime.Now.Hour);
			if (_rate.IsBlocked) {
				TimokLogger.Instance.LogRbr(LogSeverity.Error, "TypeOfDayEntity.GetWholesaleCost", string.Format("Rate is BLOCKED, rateInfoId: {0}", rateInfoId));
				return decimal.Zero;
			}
			return _rate.GetCost();
		}

		public decimal GetCost(DateTime pDateTime, int pDuration, SurchargeInfo pAccessNumberSurcharge, SurchargeInfo pPayphoneSurcharge, out short pRoundedSeconds) {
			pRoundedSeconds = 0;

			var _rate = getRate(pDateTime.Hour);
			if (_rate.IsBlocked) {
				TimokLogger.Instance.LogRbr(LogSeverity.Error, "TypeOfDayEntity.GetCost", string.Format("Rate is BLOCKED, rateInfoId: {0}", rateInfoId));
				return decimal.Zero;
			}
			return _rate.GetCost(pDuration, pAccessNumberSurcharge, pPayphoneSurcharge, out pRoundedSeconds);
		}

		//----------------------------------- Privates ---------------------------------------------------------
		Rate getRate(int pHour) {
			Rate _validRate = null;
			foreach (var _rate in rates) {
				if (_rate.HourIsCovered(pHour)) {
					_validRate = _rate;
					break;
				}
			}
			if (_validRate == null) {
				throw new Exception("TimeOfDayEntity.getRate | Exception: Hour not covered? RateInfoId: " + rateInfoId + " Hour: " + pHour);
			}
			return _validRate;
		}

		//TODO: Whay is pTimeOfDayPolicy NOT used?
		void loadRates(int pRateInfoId, TypeOfDayChoice pTypeOfDayChoice) {
			var _rates = new List<Rate>();
			using (var _db = new Rbr_Db()) {
				var _timeOfDayPeriodRows = _db.TimeOfDayPeriodCollection.GetByRate_info_id_Type_of_day_choice(pRateInfoId, (byte) pTypeOfDayChoice);
				foreach (var _timeOfDayPeriodRow in _timeOfDayPeriodRows) {
					var _rateRow = _db.RateCollection.GetByPrimaryKey(_timeOfDayPeriodRow.Rate_info_id, _timeOfDayPeriodRow.Type_of_day_choice, _timeOfDayPeriodRow.Time_of_day);
					var _rate =
						new Rate(_timeOfDayPeriodRow.Start_hour,
						         _timeOfDayPeriodRow.Stop_hour,
						         _timeOfDayPeriodRow.TimeOfDay,
						         _rateRow.First_incr_length,
						         _rateRow.Add_incr_length,
						         _rateRow.First_incr_cost,
						         _rateRow.Add_incr_cost);

					_rates.Add(_rate);
				}
			}

			if (_rates.Count == 0) {
				throw new Exception(string.Format("NO RATES, RateInfoId={0}", pRateInfoId));
			}
			rates = _rates.ToArray();
		}
	}
}
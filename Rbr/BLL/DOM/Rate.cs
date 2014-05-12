using System;
using Timok.Logger;
using Timok.Rbr.Core.Config;
using Timok.Rbr.DOM;

namespace Timok.Rbr.BLL.DOM {
	[Serializable]
	internal sealed class Rate {
		readonly int startHour;
		readonly int stopHour;
		readonly TimeOfDay timeOfDay;
		readonly byte firstIncrementLength;
		readonly byte addIncrementLength;
		readonly decimal firstIncrementCost;
		readonly decimal addIncrementCost;
		
		readonly bool isNotBlocked;
		public bool IsNotBlocked { get { return isNotBlocked; } }
		public bool IsBlocked { get { return ! isNotBlocked; } }

		//---------------- Ctor group
		public Rate(int pStartHour, int pStopHour, TimeOfDay pTimeOfDay, byte pFirstIncrLength, byte pAddIncrLength, decimal pFirstIncrCost, decimal pAddIncrCost) {
			startHour = pStartHour;
			stopHour = pStopHour;
			timeOfDay = pTimeOfDay;
			firstIncrementLength = pFirstIncrLength;
			addIncrementLength = pAddIncrLength;
			firstIncrementCost = pFirstIncrCost;
			addIncrementCost = pAddIncrCost;
			if (firstIncrementCost < decimal.Zero || addIncrementCost < decimal.Zero) {
				throw new Exception("Exception: firstIncrementCost < decimal.Zero || addIncrementCost < decimal.Zero");
			}

			isNotBlocked = (timeOfDay != TimeOfDay.BlockedFlat && timeOfDay != TimeOfDay.BlockedPeakOffPeak && timeOfDay != TimeOfDay.BlockedNightDayEve);
		}

		public override string ToString() {
			return string.Format("{0}-{1}-{2}-{3}", firstIncrementLength, addIncrementLength, firstIncrementCost, addIncrementCost);
		}

		public bool HourIsCovered(int pHour) {
			return (startHour <= pHour && pHour <= stopHour);
		}

		public int GetTimeLimit(decimal pBalance, SurchargeInfo pAccessNumberSurcharge, SurchargeInfo pPayphoneSurcharge) {
			if (pBalance <= decimal.Zero) {
				return 0;
			}

			decimal _firstIncrementCost = firstIncrementCost;
			decimal _addIncrementCost = addIncrementCost;
			add(pAccessNumberSurcharge, ref _firstIncrementCost, ref _addIncrementCost);
			add(pPayphoneSurcharge, ref _firstIncrementCost, ref _addIncrementCost);

			if (_firstIncrementCost == decimal.Zero || _addIncrementCost == decimal.Zero) {
				return 0;
			}

			if (pBalance < _firstIncrementCost) {
				return 0;
			}

			if (pBalance == _firstIncrementCost) {
				return firstIncrementLength;
			}

			//else: pBalance > First_increment_cost
			var _numberOfAddIncr = (int) (((pBalance - _firstIncrementCost) / _addIncrementCost));
			var _timeLimitInSeconds = firstIncrementLength + _numberOfAddIncr * addIncrementLength;
			return _timeLimitInSeconds;
		}

		public decimal GetCost() {
			return firstIncrementCost;
		}

		public decimal GetCost(int pDurationInSeconds, SurchargeInfo pAccessNumberSurcharge, SurchargeInfo pPayphoneSurcharge, out short pRoundedSeconds) {
			pRoundedSeconds = 0;

			if (firstIncrementLength <= 0 || addIncrementLength <= 0) {
				return decimal.Zero;
			}

			var _numberOfAddIncr = 0;
			var _firstIncrementCost = decimal.Zero;
			var _addIncrementCost = decimal.Zero;
			add(pAccessNumberSurcharge, ref _firstIncrementCost, ref _addIncrementCost);
			add(pPayphoneSurcharge, ref _firstIncrementCost, ref _addIncrementCost);

			if (pDurationInSeconds > 0) {
				_firstIncrementCost += firstIncrementCost;
				_addIncrementCost += addIncrementCost;

				if (pDurationInSeconds <= firstIncrementLength) {
					_numberOfAddIncr = 0;
				}
				else {
					_numberOfAddIncr = (pDurationInSeconds - firstIncrementLength) / addIncrementLength;
					_numberOfAddIncr += (pDurationInSeconds - firstIncrementLength) % addIncrementLength == 0 ? 0 : 1;
				}

				var _roundedSeconds = firstIncrementLength + _numberOfAddIncr * addIncrementLength;
				if (_roundedSeconds > short.MaxValue) {
					TimokLogger.Instance.LogRbr(LogSeverity.Critical, "Rate.GetCost", "_roundedSeconds > short.MaxValue");
					pRoundedSeconds = short.MaxValue;
				}
				else {
					pRoundedSeconds = (short) _roundedSeconds;
				}
			}

			var _cost = _firstIncrementCost + _numberOfAddIncr * _addIncrementCost;
			return _cost;
		}

		public int GetNormalizedCost() {
			var _minutesLenghtList = new[] {1, 2, 3, 4, 5, 7, 9, 11, 14, 17, 20, 25, 30, 40, 50, 60, 75, 90, 120};
			decimal _totalCost = 0;

			var _surcharge = new SurchargeInfo(decimal.Zero, SurchargeType.PerCall);
			for (var _i = 0; _i < _minutesLenghtList.Length; _i++) {
				short _roundedSeconds;
				var _cost = GetCost(_minutesLenghtList[_i] * 60, _surcharge, _surcharge, out _roundedSeconds);
				//decimal _mw = _cost * 10000000;
				_totalCost = decimal.Add(_totalCost, _cost);
			}

			var _res = normalize(_totalCost);
			return _res;
		}

		//----------------------------------------- Privates ------------------------------------------------
		void add(SurchargeInfo pSurchargeInfo, ref decimal pFirstIncrementCost, ref decimal pAddIncrementCost) {
			if (pSurchargeInfo == null || pSurchargeInfo.Cost <= decimal.Zero) {
				return;
			}

			if (pSurchargeInfo.SurchargeType == SurchargeType.PerCall) {
				pFirstIncrementCost += pSurchargeInfo.Cost;
				return;
			}

			var _firstIncrSurchargePercentage = (100 * firstIncrementLength) / 60;
			var _addIncrSurchargePercentage = (100 * addIncrementLength) / 60;

			pFirstIncrementCost += pSurchargeInfo.Cost * (_firstIncrSurchargePercentage / 100);
			pAddIncrementCost += pSurchargeInfo.Cost * (_addIncrSurchargePercentage / 100);
		}

		static int normalize(decimal pWeight) {
			var _hundredPercent = new Decimal(100);
			var _max = new Decimal(580000);
			var _percent = (pWeight * _hundredPercent) / _max;

			var _result = decimal.Round(_percent, 8); //, MidpointRounding.AwayFromZero);
			if (_result == decimal.Zero && _percent > decimal.Zero) {
				_result = new decimal(0.00000001);
			}

			var _intResult = int.MaxValue;
			if (_result * 100000000 <= int.MaxValue) {
				_intResult = decimal.ToInt32(_result * 100000000);
			}
			return (_intResult == 0) ? int.MaxValue : _intResult;
		}
	}
}
using System;
using Timok.Rbr.Core.Config;

namespace Timok.Rbr.BLL.DTO {
	[Serializable]
	public class RateDto {
		public int RateInfoId { get; set; }

		TimeOfDay timeOfDay;
		public TimeOfDay TimeOfDay { get { return timeOfDay; } set { timeOfDay = value; } }

		TypeOfDayChoice typeOfDayChoice;
		public TypeOfDayChoice TypeOfDayChoice { get { return typeOfDayChoice; } set { typeOfDayChoice = value; } }

		byte firstIncrLen;
		public byte FirstIncrLen { get { return firstIncrLen; } set { firstIncrLen = value; } }

		byte addIncrLen;
		public byte AddIncrLen { get { return addIncrLen; } set { addIncrLen = value; } }

		decimal firstIncrCost;
		public decimal FirstIncrCost { get { return firstIncrCost; } set { firstIncrCost = value; } }

		decimal addIncrCost;
		public decimal AddIncrCost { get { return addIncrCost; } set { addIncrCost = value; } }

		public string Increment { get { return firstIncrLen + " / " + addIncrLen; } }

		public RateDto() {
			timeOfDay = TimeOfDay.Flat;
			typeOfDayChoice = TypeOfDayChoice.RegularDay;
			firstIncrLen = 60;
			addIncrLen = 60;
		}

		public decimal GetPerMinuteCost() {
			if (FirstIncrLen == 60 && AddIncrLen == 180) {
				return FirstIncrCost;
			}
			if (FirstIncrLen == 60 && AddIncrLen == 60) {
				return FirstIncrCost;
			}
			if (FirstIncrLen == 60 && AddIncrLen == 6) {
				return FirstIncrCost;
			}
			if (FirstIncrLen == 30 && AddIncrLen == 6) {
				return FirstIncrCost + AddIncrCost * 5;
			}
			if (FirstIncrLen == 6 && AddIncrLen == 6) {
				return FirstIncrCost * 10;
			}
			if (FirstIncrLen == 1 && AddIncrLen == 1) {
				return FirstIncrCost * 60;
			}

			throw new NotImplementedException("RateRateDto.GetPerMinuteCost, Not Implemented!");
		}

		public decimal GetFirstIncrAmount(decimal pPerMinuteRateAmount, byte pFirstIncrementLength, byte pAddIncrementLength) {
			if (pFirstIncrementLength == 60 && pAddIncrementLength == 180) {
				return pPerMinuteRateAmount;
			}
			if (pFirstIncrementLength == 60 && pAddIncrementLength == 60) {
				return pPerMinuteRateAmount;
			}
			if (pFirstIncrementLength == 60 && pAddIncrementLength == 6) {
				return pPerMinuteRateAmount;
			}
			if (pFirstIncrementLength == 30 && pAddIncrementLength == 6) {
				return (pPerMinuteRateAmount / 10) * 5;
			}
			if (pFirstIncrementLength == 6 && pAddIncrementLength == 6) {
				return pPerMinuteRateAmount / 10;
			}
			if (pFirstIncrementLength == 1 && pAddIncrementLength == 1) {
				return pPerMinuteRateAmount / 60;
			}
			throw new NotImplementedException("RateRateDto.GetPerMinuteCost, Not Implemented!");
		}

		public decimal GetAddIncrAmount(decimal pPerMinuteRateAmount, byte pFirstIncrementLength, byte pAddIncrementLength) {
			if (pFirstIncrementLength == 60 && pAddIncrementLength == 180) {
				return pPerMinuteRateAmount * 3;
			}
			if (pFirstIncrementLength == 60 && pAddIncrementLength == 60) {
				return pPerMinuteRateAmount;
			}
			if (pFirstIncrementLength == 60 && pAddIncrementLength == 6) {
				return pPerMinuteRateAmount / 10;
			}
			if (pFirstIncrementLength == 30 && pAddIncrementLength == 6) {
				return pPerMinuteRateAmount / 10;
			}
			if (pFirstIncrementLength == 6 && pAddIncrementLength == 6) {
				return pPerMinuteRateAmount / 10;
			}
			if (pFirstIncrementLength == 1 && pAddIncrementLength == 1) {
				return pPerMinuteRateAmount / 60;
			}
			throw new NotImplementedException();
		}

		public RateDto MakeClone() {
			RateDto _clone = new RateDto();
			_clone.RateInfoId = 0;
			_clone.AddIncrCost = addIncrCost;
			_clone.AddIncrLen = addIncrLen;
			_clone.FirstIncrCost = firstIncrCost;
			_clone.FirstIncrLen = firstIncrLen;
			_clone.TimeOfDay = timeOfDay;
			_clone.TypeOfDayChoice = typeOfDayChoice;

			return _clone;
		}

		public RateDto MakeCloneWithNoRates() {
			RateDto _clone = new RateDto();
			_clone.RateInfoId = 0;
			_clone.AddIncrCost = decimal.Zero;
			_clone.FirstIncrCost = decimal.Zero;

			_clone.AddIncrLen = addIncrLen;
			_clone.FirstIncrLen = firstIncrLen;
			_clone.TimeOfDay = timeOfDay;
			_clone.TypeOfDayChoice = typeOfDayChoice;

			return _clone;
		}
	}
}
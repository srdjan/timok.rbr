using System;
using Timok.Rbr.Core.Config;
using System.Collections.Generic;

namespace Timok.Rbr.BLL.DTO {
	[Serializable]
	public class TypeOfDayRateEntryDto {
		
		private int rateInfoId;
		public int RateInfoId {
			get { return rateInfoId; }
			set {
				rateInfoId = value;
				if (Rates != null) {
					foreach (var _rate in Rates) {
						_rate.RateInfoId = rateInfoId;
					}
				}
			}
		}

		public TypeOfDayChoice TypeOfDayChoice { get; set; }

		public RateDto[] Rates { get; set; }

		public TypeOfDayRateEntryDto() {
			TypeOfDayChoice = TypeOfDayChoice.RegularDay;
			Rates = new[] { new RateDto() };
		}

		public TypeOfDayRateEntryDto MakeClone() {
			var _clone = new TypeOfDayRateEntryDto
			             {
			             	RateInfoId = 0, 
			             	TypeOfDayChoice = TypeOfDayChoice
			             };

			var _list = new List<RateDto>();
			foreach (var _rate in Rates) {
				_list.Add(_rate.MakeClone());
			}
			_clone.Rates = _list.ToArray();

			return _clone;
		}

		public TypeOfDayRateEntryDto MakeCloneWithNoRates() {
			var _clone = new TypeOfDayRateEntryDto
			             {
			             	RateInfoId = 0, 
			             	TypeOfDayChoice = TypeOfDayChoice
			             };

			var _list = new List<RateDto>();
			foreach (var _rate in Rates) {
				_list.Add(_rate.MakeCloneWithNoRates());
			}
			_clone.Rates = _list.ToArray();

			return _clone;
		}
	}
}
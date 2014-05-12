using System;

namespace Timok.Rbr.DTO {
	[Serializable]
	public class HolidayDto {
		DateTime date;
		string name = string.Empty;

		public DateTime Date { get { return date; } }
		public string Name { get { return name; } }

		public HolidayDto(DateTime pDate, string pName) {
			date = pDate;
			if (pName != null) {
				name = pName;
			}
		}

		public override string ToString() {
			return date.ToString("MM/dd/yyyy") + "  " + name;
		}
	}

	[Serializable]
	public class HolidayListDto {
		int rateInfoId;
		public int RateInfoId { get { return rateInfoId; } set { rateInfoId = value; } }

		HolidayDto[] holidays;
		public HolidayDto[] Holidays { get { return holidays; } set { holidays = value; } }

		public HolidayListDto() {}

		public HolidayListDto(int pRateInfoId, HolidayDto[] pHolidays) {
			rateInfoId = pRateInfoId;
			holidays = pHolidays;
		}

		public HolidayListDto MakeClone() {
			HolidayListDto _clone = new HolidayListDto(0, holidays);
			return _clone;
		}
	}
}
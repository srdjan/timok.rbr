using System;

namespace RbrData.Helpers {
	public static class TimokDate {
		public const int MaxHour = 23;
		public const int MaxJDay = 366;
		public const int MaxShortTimokDate = 9999366;
		public const int MaxTimokDate = 999936623;
		public const int MaxYear = 9999;
		public const int MinHour = 0;
		public const int MinJDay = 1;
		public const int MinShortTimokDate = 1000001;
		public const int MinTimokDate = 100000100;
		public const int MinYear = 1000;

		/// <summary>
		/// Parses DateTime into TimokDate as Int32 (format: year + julian day + hour  (YYYYJJJHH))
		/// </summary>
		/// <param name="pDate">DateTime</param>
		/// <returns>Int32; format: year + julian day + hour (YYYYJJJHH).</returns>
		public static int Parse(DateTime pDate) {
			//return pDate.Year * 1000 + pDate.DayOfYear;
			var ydh = Parse(pDate, pDate.Hour);
			return ydh;
		}

		/// <summary>
		/// Parses DateTime and Hour[0 - 23] into TimokDate as Int32 (format: year + julian day + hour  (YYYYJJJHH))
		/// </summary>
		/// <param name="pDate">DateTime</param>
		/// <param name="pHour"></param>
		/// <returns>Int32; format: year + julian day + hour (YYYYJJJHH).</returns>
		public static int Parse(DateTime pDate, int pHour) {
			if (pHour < MinHour || pHour > MaxHour) {
				throw new ArgumentOutOfRangeException("pHour", pHour, "Bad Hour value; should be from 0 to 23.");
			}
			var ydh = ParseToShortTimokDate(pDate)*100 + pHour;
			return ydh;
		}

		/// <summary>
		/// Parses DateTime into Short TimokDate as Int32 (format: year + julian day  (YYYYJJJ))
		/// </summary>
		/// <param name="pDate">DateTime</param>
		/// <returns>Int32; format: year + julian day (YYYYJJJ).</returns>
		public static int ParseToShortTimokDate(DateTime pDate) {
			var yj = pDate.Year*1000 + pDate.DayOfYear;
			return yj;
		}

		/// <summary>
		/// Converts TimokDate to regular DateTime
		/// </summary>
		/// <param name="pTimokDate">Int32; format: year + julian day + hour (YYYYJJJHH).</param>
		/// <returns>DateTime</returns>
		public static DateTime ToDateTime(int pTimokDate) {
			int _year;
			int _jday;
			DateTime _date;

			//yyyyjjjhh	100000100 - 999936623
			if (pTimokDate >= MinTimokDate && pTimokDate <= MaxTimokDate) {
				_year = pTimokDate/100000;
				var _jdayHour = pTimokDate%100000;
				_jday = _jdayHour/100;
				var _hour = _jdayHour%100;
				validateYear(_year);
				validateJDay(_jday);
				validateHour(_hour);

				_date = new DateTime(_year, 1, 1).AddDays(_jday - 1).AddHours(_hour);
				return _date;
			}
				//yyyyjjj	1000001 - 9999366
			if (pTimokDate >= MinShortTimokDate && pTimokDate <= MaxShortTimokDate) {
				_year = pTimokDate/1000;
				_jday = pTimokDate%1000;
				validateYear(_year);
				validateJDay(_jday);

				_date = new DateTime(_year, 1, 1).AddDays(_jday - 1);
				return _date;
			}
			
			throw new ArgumentOutOfRangeException("pTimokDate",pTimokDate, string.Format("Invalid value of pTimokDate.\r\nExpected range for full TimokDate : {0} - {1} [YYYYJJJHH]\r\nor\r\nfor Short TimokDate Day : {2} - {3} [YYYYJJJ] .", MinTimokDate, MaxTimokDate, MinShortTimokDate, MaxShortTimokDate));
		}

		public static bool IsFullTimokDate(int pTimokDate) {
			return (pTimokDate >= MinTimokDate && pTimokDate <= MaxTimokDate);
		}

		public static bool IsShortTimokDate(int pTimokDate) {
			return (pTimokDate >= MinShortTimokDate && pTimokDate <= MaxShortTimokDate);
		}

		//Privates
		static void validateYear(int year) {
			if (year < MinYear || year > MaxYear) {
				throw new ArgumentOutOfRangeException("year", year, string.Format("Invalid value of year.\r\nExpected range : {0} - {1} [YYYY] .", MinYear, MaxYear));
			}
		}

		static void validateJDay(int jday) {
			if (jday < MinJDay || jday > MaxJDay) {
				throw new ArgumentOutOfRangeException("jday",jday,string.Format("Invalid value of jday.\r\nExpected range : {0} - {1} .", MinJDay, MaxJDay));
			}
		}

		static void validateHour(int hour) {
			if (hour < MinHour || hour > MaxHour) {
				throw new ArgumentOutOfRangeException("hour",hour,string.Format("Invalid value of hour.\r\nExpected range : {0} - {1} [24 hour format].", MinHour, MaxHour));
			}
		}
	}
}
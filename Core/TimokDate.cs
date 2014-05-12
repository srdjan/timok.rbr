using System;

namespace Timok.Core {
	/// <summary>
	/// Summary description for TimokDate.
	/// </summary>
	public sealed class TimokDate {

		public const int MinYear = 1000;
		public const int MaxYear = 9999;
		public const int MinJDay = 1;
		public const int MaxJDay = 366;
		public const int MinShortTimokDate = 1000001;
		public const int MaxShortTimokDate = 9999366;
		public const int MinTimokDate = 100000100;
		public const int MaxTimokDate = 999936623;
		public const int MinHour = 0;
		public const int MaxHour = 23;

		private TimokDate() {}
		/// <summary>
		/// Parses DateTime into TimokDate as Int32 (format: year + julian day + hour  (YYYYJJJHH))
		/// </summary>
		/// <param name="pDate">DateTime</param>
		/// <returns>Int32; format: year + julian day + hour (YYYYJJJHH).</returns>
		public static int Parse(DateTime pDate){
			//return pDate.Year * 1000 + pDate.DayOfYear;
			int ydh = Parse(pDate, pDate.Hour);
			return ydh;
		}

		/// <summary>
		/// Parses DateTime and Hour[0 - 23] into TimokDate as Int32 (format: year + julian day + hour  (YYYYJJJHH))
		/// </summary>
		/// <param name="pDate">DateTime</param>
		/// <returns>Int32; format: year + julian day + hour (YYYYJJJHH).</returns>
		public static int Parse(DateTime pDate, int pHour){
			if(pHour < MinHour || pHour > MaxHour){
				throw new ArgumentOutOfRangeException("pHour", pHour, "Bad Hour value; should be from 0 to 23.");
			}
			int ydh = ParseToShortTimokDate(pDate) * 100 + pHour;
			return ydh;
		}

		/// <summary>
		/// Parses DateTime into Short TimokDate as Int32 (format: year + julian day  (YYYYJJJ))
		/// </summary>
		/// <param name="pDate">DateTime</param>
		/// <returns>Int32; format: year + julian day (YYYYJJJ).</returns>
		public static int ParseToShortTimokDate(DateTime pDate){
			int yj = pDate.Year * 1000 + pDate.DayOfYear;
			return yj;
		}

		/// <summary>
		/// Converts TimokDate to regular DateTime
		/// </summary>
		/// <param name="pTimokDate">Int32; format: year + julian day + hour (YYYYJJJHH).</param>
		/// <returns>DateTime</returns>
		public static DateTime ToDateTime(int pTimokDate){
			int _year = MinYear;
			int _jdayHour = 0;
			int _jday = MinJDay;
			int _hour = MinHour;
			DateTime _date = DateTime.MinValue;

			//yyyyjjjhh	100000100 - 999936623
			if(pTimokDate >= MinTimokDate && pTimokDate <= MaxTimokDate){
				_year = pTimokDate/100000;
				_jdayHour = pTimokDate%100000;
				_jday = _jdayHour/100;
				_hour = _jdayHour%100;
				validateYear(_year);
				validateJDay(_jday);
				validateHour(_hour);

				_date = new DateTime(_year, 1, 1).AddDays(_jday - 1).AddHours(_hour);
				return _date;
			}
			//yyyyjjj	1000001 - 9999366
			else if(pTimokDate >= MinShortTimokDate && pTimokDate <= MaxShortTimokDate){
				_year = pTimokDate/1000;
				_jday = pTimokDate%1000;
				validateYear(_year);
				validateJDay(_jday);

				_date = new DateTime(_year, 1, 1).AddDays(_jday - 1);
				return _date;
			}
			else{
				throw new ArgumentOutOfRangeException(
					"pTimokDate",
					pTimokDate, 
					"Invalid value of pTimokDate.\r\n" + 
					"Expected range for full TimokDate : " + MinTimokDate + " - " + MaxTimokDate + " [YYYYJJJHH]\r\n" + 
					"or\r\nfor Short TimokDate Day : " + MinShortTimokDate + " - " + MaxShortTimokDate + " [YYYYJJJ] .");
			}
		}

		public static bool IsFullTimokDate(int pTimokDate){
			return (pTimokDate >= MinTimokDate && pTimokDate <= MaxTimokDate);
		}

		public static bool IsShortTimokDate(int pTimokDate){
			return (pTimokDate >= MinShortTimokDate && pTimokDate <= MaxShortTimokDate);
		}
		//*****************************************************
		//Privates
		private static void validateYear(int year){
			if(year < MinYear || year > MaxYear){
				throw new ArgumentOutOfRangeException(
					"year",
					year, 
					"Invalid value of year.\r\n" + 
					"Expected range : " + MinYear + " - " + MaxYear + " [YYYY] .");
			}
		}

		private static void validateJDay(int jday){
			if(jday < MinJDay || jday > MaxJDay){
				throw new ArgumentOutOfRangeException(
					"jday",
					jday, 
					"Invalid value of jday.\r\n" + 
					"Expected range : " + MinJDay + " - " + MaxJDay + " .");
			}
		}

		private static void validateHour(int hour){
			if(hour < MinHour || hour > MaxHour){
				throw new ArgumentOutOfRangeException(
					"hour",
					hour, 
					"Invalid value of hour.\r\n" + 
					"Expected range : " + MinHour + " - " + MaxHour + " [24 hour format].");
			}
		}
	}
}

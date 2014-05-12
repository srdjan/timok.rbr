using System;
using Microsoft.VisualBasic;

namespace Timok.Core 
{
	public enum DateInterval {
		Day,
		Month,
		Year, 
    Hour
	}

	public class DateTimeUtils {
		private DateTimeUtils() {}

		public static DateTime DateHourNow {
			get {
				DateTime _dt = DateTime.Now;
				return new DateTime(_dt.Year, _dt.Month, _dt.Day, _dt.Hour, 0, 0);
			}
		}

		public static int DateDiffInMonths(DateTime start_date, DateTime end_date) {
			long _monthsDiff = 0;
			_monthsDiff = DateAndTime.DateDiff(Microsoft.VisualBasic.DateInterval.Month, 
				start_date, end_date, FirstDayOfWeek.System, FirstWeekOfYear.System);
			if(_monthsDiff > int.MaxValue)
				throw new ApplicationException("Cannot process date diff more then " + int.MaxValue + " months");
			else
				return (int)_monthsDiff;
		}

    public static int DateDiff(DateInterval date_interval, DateTime start_date, DateTime end_date) {
      long _diff = 0;
      switch (date_interval) {
        case DateInterval.Day:
          _diff = DateAndTime.DateDiff(
            Microsoft.VisualBasic.DateInterval.Day,
            start_date, end_date, FirstDayOfWeek.System, FirstWeekOfYear.System);
          break;
        case DateInterval.Month:
          _diff = DateAndTime.DateDiff(
            Microsoft.VisualBasic.DateInterval.Month,
            start_date, end_date, FirstDayOfWeek.System, FirstWeekOfYear.System);
          break;
        case DateInterval.Year:
          _diff = DateAndTime.DateDiff(
            Microsoft.VisualBasic.DateInterval.Year,
            start_date, end_date, FirstDayOfWeek.System, FirstWeekOfYear.System);
          break;
        case DateInterval.Hour:
          _diff = DateAndTime.DateDiff(
            Microsoft.VisualBasic.DateInterval.Hour,
            start_date, end_date, FirstDayOfWeek.System, FirstWeekOfYear.System);
          break;
      }

      //if(_diff > int.MaxValue){
      //  throw new ApplicationException("Date range more then 365 days is not supported.");
      //}

      return (int) _diff;
    }
	}
}

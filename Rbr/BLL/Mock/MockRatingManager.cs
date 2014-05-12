using System;
using System.Diagnostics;

using Timok.Rbr.Core;
//using Timok.Rbr.DOM;
//using Timok.Rbr.BLL;
using Timok.Rbr.DTO;

namespace Timok.Rbr.BLL.Mock {

//	public class MockRatingManager : IRatingManager {
//		static private DayType regularDay;
//		static private DayType weekend;
//		static private DayType holiday;
//		
//		static private RatingInfo ratingInfo;
//
//
//		static private HolidayList dbHolidayList;
//
//		public static readonly MockRatingManager Instance = new MockRatingManager();
//		private MockRatingManager() { }
//
//		public HolidayList GetHolidayList(int pRateInfoID) {
//			if (dbHolidayList == null) {
//				Holiday[] _dbHolidays = new Holiday[] {	
//																						 new Holiday(DateTime.Parse("1/1/2006"), string.Empty), 
//																						 new Holiday(DateTime.Parse("2/14/2006"), string.Empty), 
//																						 new Holiday(DateTime.Parse("2/15/2006"), string.Empty), 
//																						 new Holiday(DateTime.Parse("3/11/2006"), string.Empty), 
//																						 new Holiday(DateTime.Parse("2/17/2006"), string.Empty)
//																					 };
//				dbHolidayList = new HolidayList(pRateInfoID, _dbHolidays, DateTime.Today, DateTime.Today);
//			}
//			return (HolidayList) Timok.Core.Cloner.Clone(dbHolidayList);
//		}
//		
//		public void SaveHolidayList(HolidayList pHolidayList) {
//			dbHolidayList = (HolidayList) Timok.Core.Cloner.Clone(pHolidayList);
//		}
//
//		public RatingInfo GetRatingInfo(int pRateInfoId) {
//			if (ratingInfo == null) {
//				ratingInfo = new RatingInfo();
//			
//				ratingInfo.RateInfoId = pRateInfoId;
//				ratingInfo.RegularDay = new DayType(null);
//				ratingInfo.Weekend = new DayType(null);
//				ratingInfo.Holiday = new DayType(null);
//				ratingInfo.DefaultFirstIncrLen = 30;
//				ratingInfo.DefaultAddIncrLen = 6;
//			}
//			return ratingInfo;
//		}
//		
//		public void SaveRatingInfo(RatingInfo pRatingInfo) {
//			ratingInfo = pRatingInfo;
//		}
//
//		public void GetRateInfo(int pRouteId, out DayType pRegularDay, out DayType pWeekend, out DayType pHoliday) {
//			pRegularDay = regularDay;
//			pWeekend = weekend;
//			pHoliday = holiday;
//		}
//		
//		public void SaveRateInfo(int pRouteId, DayType pRegularDay, DayType pWeekend, DayType pHoliday) {
//			regularDay = pRegularDay;
//			weekend = pWeekend;
//			holiday = pHoliday;
//		}
//
//		//------------------------------- Private -----------------------------------------------------
//		static private void debugShow() {
//			Debug.WriteLine("TypeOfDayChoice: " + TypeOfDayChoice.RegularDay);
//			foreach( DayTime _dayTimePeriod in regularDay.DayTimePeriods ) {
//				Debug.WriteLine( "\t" + _dayTimePeriod.TimeOfDay + " Start: " + _dayTimePeriod.Start + " Stop: " + _dayTimePeriod.Stop );
//			}
//			Debug.WriteLine( "-----------------" );
//
//			Debug.WriteLine("TypeOfDayChoice: " + TypeOfDayChoice.Weekend);
//			foreach( DayTime _dayTimePeriod in weekend.DayTimePeriods ) {
//				Debug.WriteLine( "\t" + _dayTimePeriod.TimeOfDay + " Start: " + _dayTimePeriod.Start + " Stop: " + _dayTimePeriod.Stop );
//			}
//			Debug.WriteLine( "-----------------" );
//
//			Debug.WriteLine("TypeOfDayChoice: " + TypeOfDayChoice.Holiday);
//			foreach( DayTime _dayTimePeriod in holiday.DayTimePeriods ) {
//				Debug.WriteLine( "\t" + _dayTimePeriod.TimeOfDay + " Start: " + _dayTimePeriod.Start + " Stop: " + _dayTimePeriod.Stop );
//			}
//			Debug.WriteLine( "-----------------" );
//		}		
//	}
}

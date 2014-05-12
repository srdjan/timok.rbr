// <fileinfo name="HolidayCalendarCollection.cs">
//		<copyright>
//			Copyright © 2002-2007 Timok ES LLC. All rights reserved.
//		</copyright>
//		<remarks>
//			You can update this source code manually. If the file
//			already exists it will not be rewritten by the generator.
//		</remarks>
//		<generator rewritefile="False" infourl="http://www.SharpPower.com">RapTier</generator>
// </fileinfo>

using System;
using System.Data;
using Timok.Rbr.DAL.RbrDatabase.Base;

namespace Timok.Rbr.DAL.RbrDatabase {
	/// <summary>
	/// Represents the <c>HolidayCalendar</c> table.
	/// </summary>
	public class HolidayCalendarCollection : HolidayCalendarCollection_Base {
		/// <summary>
		/// Initializes a new instance of the <see cref="HolidayCalendarCollection"/> class.
		/// </summary>
		/// <param name="db">The database object.</param>
		internal HolidayCalendarCollection(Rbr_Db db)
			: base(db) {
			// EMPTY
		}

		public static HolidayCalendarRow Parse(System.Data.DataRow row){
			return new HolidayCalendarCollection(null).MapRow(row);
		}

		public bool Exists(int pRateInfoId, DateTime pDate) {
			string _sqlStr = "SELECT COUNT(*) FROM HolidayCalendar " + 
				"WHERE [" + HolidayCalendarRow.rate_info_id_DbName + "] = " + 
				Database.CreateSqlParameterName(HolidayCalendarRow.rate_info_id_PropName) + 
				" AND " +
				" [" + HolidayCalendarRow.holiday_day_DbName + "] = " + 
				Database.CreateSqlParameterName(HolidayCalendarRow.holiday_day_PropName);

			IDbCommand _cmd = base.Database.CreateCommand(_sqlStr);
			AddParameter(_cmd, HolidayCalendarRow.rate_info_id_PropName, pRateInfoId);
			//ensure a date only, no time
			AddParameter(_cmd, HolidayCalendarRow.holiday_day_PropName, new DateTime(pDate.Year, pDate.Month, pDate.Day));

			int _res = (int) _cmd.ExecuteScalar();
			return _res > 0;
		}
	} // End of HolidayCalendarCollection class
} // End of namespace

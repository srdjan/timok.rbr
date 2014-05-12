// <fileinfo name="TimeOfDayPeriodCollection.cs">
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
using Timok.Rbr.Core.Config;

namespace Timok.Rbr.DAL.RbrDatabase {
	/// <summary>
	/// Represents the <c>TimeOfDayPeriod</c> table.
	/// </summary>
	public class TimeOfDayPeriodCollection : TimeOfDayPeriodCollection_Base {
		/// <summary>
		/// Initializes a new instance of the <see cref="TimeOfDayPeriodCollection"/> class.
		/// </summary>
		/// <param name="db">The database object.</param>
		internal TimeOfDayPeriodCollection(Rbr_Db db)
			: base(db) {
			// EMPTY
		}

		public static TimeOfDayPeriodRow Parse(System.Data.DataRow row){
			return new TimeOfDayPeriodCollection(null).MapRow(row);
		}

		public bool DeleteByRateInfoID(int pRateInfoID) {
			string _whereSql = "[" + TimeOfDayPeriodRow.rate_info_id_DbName + "]=" + Database.CreateSqlParameterName(TimeOfDayPeriodRow.rate_info_id_PropName);
			IDbCommand _cmd = CreateDeleteCommand(_whereSql);
			AddParameter(_cmd, TimeOfDayPeriodRow.rate_info_id_PropName, pRateInfoID);
			return _cmd.ExecuteNonQuery() > 0;
		}

		public TimeOfDayPeriodRow GetByRateInfoIdTypeOfDayChoiceHour(int pRateInfoId, TypeOfDayChoice pTypeOfDayChoice, int pHour) {
			string _whereSql = 
				"[" + TimeOfDayPeriodRow.rate_info_id_DbName + "]=" + 
				Database.CreateSqlParameterName(TimeOfDayPeriodRow.rate_info_id_PropName) + 
				" AND " + 
				"[" + TimeOfDayPeriodRow.type_of_day_choice_DbName + "]=" + 
				Database.CreateSqlParameterName(TimeOfDayPeriodRow.type_of_day_choice_PropName) + 
				" AND " + 
				"(" + Database.CreateSqlParameterName("pHour") + 
				" BETWEEN [" + TimeOfDayPeriodRow.start_hour_DbName + "] AND [" + TimeOfDayPeriodRow.stop_hour_DbName + "]" + 
				")";

			IDbCommand _cmd = CreateGetCommand(_whereSql, null);
			AddParameter(_cmd, TimeOfDayPeriodRow.rate_info_id_PropName, pRateInfoId);
			AddParameter(_cmd, TimeOfDayPeriodRow.type_of_day_choice_PropName, (byte) pTypeOfDayChoice);
			Database.AddParameter(_cmd, "pHour", DbType.Int16, pHour);
			
			using (IDataReader _reader = _cmd.ExecuteReader()) {
				TimeOfDayPeriodRow[] _temp = MapRecords(_reader);
				return 0 == _temp.Length ? null : _temp[0];
			}
		}
	} // End of TimeOfDayPeriodCollection class
} // End of namespace

// <fileinfo name="ScheduleCollection.cs">
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

namespace Timok.Rbr.DAL.RbrDatabase
{
	/// <summary>
	/// Represents the <c>Schedule</c> table.
	/// </summary>
	public class ScheduleCollection : ScheduleCollection_Base
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="ScheduleCollection"/> class.
		/// </summary>
		/// <param name="db">The database object.</param>
		internal ScheduleCollection(Rbr_Db db)
				: base(db)
		{
			// EMPTY
		}

		public static ScheduleRow Parse(System.Data.DataRow row){
			return new ScheduleCollection(null).MapRow(row);
		}

    public override void Insert(ScheduleRow value) {
      string _sqlStr = "DECLARE " +
        base.Database.CreateSqlParameterName(ScheduleRow.schedule_id_PropName) + " int " +
        "SET " +
        base.Database.CreateSqlParameterName(ScheduleRow.schedule_id_PropName) +
        " = COALESCE((SELECT MAX(" + ScheduleRow.schedule_id_DbName + ") FROM Schedule) + 1, 1) " +

        "INSERT INTO [dbo].[Schedule] (" +
        "[" + ScheduleRow.schedule_id_DbName + "], " +
        "[" + ScheduleRow.type_DbName + "], " +
        "[" + ScheduleRow.day_of_week_DbName + "], " +
        "[" + ScheduleRow.day_of_the_month_1_DbName + "], " +
        "[" + ScheduleRow.day_of_the_month_2_DbName + "]" +
        ") VALUES (" +
        Database.CreateSqlParameterName(ScheduleRow.schedule_id_PropName) + ", " +
        Database.CreateSqlParameterName(ScheduleRow.type_PropName) + ", " +
        Database.CreateSqlParameterName(ScheduleRow.day_of_week_PropName) + ", " +
        Database.CreateSqlParameterName(ScheduleRow.day_of_the_month_1_PropName) + ", " +
        Database.CreateSqlParameterName(ScheduleRow.day_of_the_month_2_PropName) + ") " +

        "SELECT " + base.Database.CreateSqlParameterName(ScheduleRow.schedule_id_PropName) + " ";

      IDbCommand _cmd = Database.CreateCommand(_sqlStr);
      //AddParameter(_cmd, "Schedule_id", value.schedule_id);
      AddParameter(_cmd, ScheduleRow.type_PropName, value.Type);
      AddParameter(_cmd, ScheduleRow.day_of_week_PropName, value.Day_of_week);
      AddParameter(_cmd, ScheduleRow.day_of_the_month_1_PropName, value.Day_of_the_month_1);
      AddParameter(_cmd, ScheduleRow.day_of_the_month_2_PropName, value.Day_of_the_month_2);

      value.Schedule_id = (int) _cmd.ExecuteScalar();
    }

	} // End of ScheduleCollection class
} // End of namespace

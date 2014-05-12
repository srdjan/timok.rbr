// <fileinfo name="RateCollection.cs">
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
	/// Represents the <c>Rate</c> table.
	/// </summary>
	public class RateCollection : RateCollection_Base {
		/// <summary>
		/// Initializes a new instance of the <see cref="RateCollection"/> class.
		/// </summary>
		/// <param name="db">The database object.</param>
		internal RateCollection(Rbr_Db db)
			: base(db) {
			// EMPTY
		}

		public static RateRow Parse(System.Data.DataRow row){
			return new RateCollection(null).MapRow(row);
		}

		public bool DeleteByRateInfoID(int pRateInfoID) {
			string _whereSql = "[" + RateRow.rate_info_id_DbName + "]=" + Database.CreateSqlParameterName(RateRow.rate_info_id_PropName);
			IDbCommand _cmd = CreateDeleteCommand(_whereSql);
			AddParameter(_cmd, RateRow.rate_info_id_PropName, pRateInfoID);
			return _cmd.ExecuteNonQuery() > 0;
		}

    public bool HasAllValidRates(int pRateInfoId) {
      string _sqlStr = "SELECT COUNT(*) FROM  Rate WHERE " +
        RateRow.rate_info_id_DbName + " = " + Database.CreateSqlParameterName(RateRow.rate_info_id_PropName) + " " + 
        " AND " +
        RateRow.time_of_day_DbName + " NOT IN (" + (byte) TimeOfDay.BlockedFlat + "," + (byte) TimeOfDay.BlockedPeakOffPeak + "," + (byte) TimeOfDay.BlockedNightDayEve + ") " + 
        " AND " + 
        "(" + 
        RateRow.first_incr_cost_DbName + " = " + decimal.Zero + 
        " OR " + 
        RateRow.add_incr_cost_DbName + " = " + decimal.Zero + 
        ")";

      IDbCommand _cmd = base.Database.CreateCommand(_sqlStr);
      AddParameter(_cmd, RateRow.rate_info_id_PropName, pRateInfoId);

      int _res = (int) _cmd.ExecuteScalar();
      return _res == 0;
    }
  } // End of RateCollection class
} // End of namespace

// <fileinfo name="WholesaleRateHistoryCollection.cs">
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
	/// Represents the <c>WholesaleRateHistory</c> table.
	/// </summary>
	public class WholesaleRateHistoryCollection : WholesaleRateHistoryCollection_Base
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="WholesaleRateHistoryCollection"/> class.
		/// </summary>
		/// <param name="db">The database object.</param>
		internal WholesaleRateHistoryCollection(Rbr_Db db)
				: base(db)
		{
			// EMPTY
		}

		public static WholesaleRateHistoryRow Parse(System.Data.DataRow row){
			return new WholesaleRateHistoryCollection(null).MapRow(row);
		}

    public override bool Update(WholesaleRateHistoryRow value) {
      string _sqlStr = "UPDATE [dbo].[WholesaleRateHistory] SET " +
        WholesaleRateHistoryRow.wholesale_route_id_DbName + "=" + Database.CreateSqlParameterName(WholesaleRateHistoryRow.wholesale_route_id_PropName) + ", " +
				WholesaleRateHistoryRow.date_on_DbName + "=" + Database.CreateSqlParameterName(WholesaleRateHistoryRow.date_on_PropName) + ", " +
				WholesaleRateHistoryRow.date_off_DbName + "=" + Database.CreateSqlParameterName(WholesaleRateHistoryRow.date_off_PropName) +
        " WHERE " +
        WholesaleRateHistoryRow.rate_info_id_DbName + "=" + Database.CreateSqlParameterName(WholesaleRateHistoryRow.rate_info_id_PropName);
      IDbCommand _cmd = Database.CreateCommand(_sqlStr);
      AddParameter(_cmd, WholesaleRateHistoryRow.wholesale_route_id_PropName, value.Wholesale_route_id);
      AddParameter(_cmd, WholesaleRateHistoryRow.date_on_PropName, value.Date_on);
      AddParameter(_cmd, WholesaleRateHistoryRow.date_off_PropName, value.Date_off);
			AddParameter(_cmd, WholesaleRateHistoryRow.rate_info_id_PropName, value.Rate_info_id);
			return 0 != _cmd.ExecuteNonQuery();
    }

    public WholesaleRateHistoryRow GetByRateInfoId(int pRateInfoId) {
      string _where = WholesaleRateHistoryRow.rate_info_id_DbName + " = " + Database.CreateSqlParameterName(WholesaleRateHistoryRow.rate_info_id_PropName);

      IDbCommand _cmd = CreateGetCommand(_where, null);
      AddParameter(_cmd, WholesaleRateHistoryRow.rate_info_id_PropName, pRateInfoId);
      using (IDataReader _reader = _cmd.ExecuteReader()) {
        WholesaleRateHistoryRow[] _tempArray = MapRecords(_reader);
        if (_tempArray.Length > 1) {
          throw new Exception("UNEXPECTED: more then 1 record for RateInfoId:" + pRateInfoId);
        }
        return 0 == _tempArray.Length ? null : _tempArray[0];
      }
    }

    public int DeleteByRateInfoId(int pRateInfoId) {
      string _where = WholesaleRateHistoryRow.rate_info_id_DbName + " = " + Database.CreateSqlParameterName(WholesaleRateHistoryRow.rate_info_id_PropName);

      IDbCommand _cmd = CreateDeleteCommand(_where);
      AddParameter(_cmd, WholesaleRateHistoryRow.rate_info_id_PropName, pRateInfoId);
      return _cmd.ExecuteNonQuery();
    }

    public WholesaleRateHistoryRow GetByWholesaleRouteIdDate(int pWholesaleRouteId, DateTime pDateTime) {
      string _where = WholesaleRateHistoryRow.wholesale_route_id_DbName + " = " + Database.CreateSqlParameterName(WholesaleRateHistoryRow.wholesale_route_id_PropName) +
        " AND " +
        "( " +
        Database.CreateSqlParameterName("SelectedDate") + " BETWEEN " +
        WholesaleRateHistoryRow.date_on_DbName + " AND " + WholesaleRateHistoryRow.date_off_DbName +
        ")";

      IDbCommand _cmd = CreateGetCommand(_where, null);
      AddParameter(_cmd, WholesaleRateHistoryRow.wholesale_route_id_PropName, pWholesaleRouteId);
      Database.AddParameter(_cmd, "SelectedDate", DbType.DateTime, pDateTime);
      using (IDataReader _reader = _cmd.ExecuteReader()) {
        WholesaleRateHistoryRow[] _tempArray = MapRecords(_reader);
        return 0 == _tempArray.Length ? null : _tempArray[0];
      }
    }

    protected override IDbCommand CreateGetByWholesale_route_idCommand(int pWholesaleRouteId) {
      string _whereSql = "";
      _whereSql += WholesaleRateHistoryRow.wholesale_route_id_DbName + "=" + Database.CreateSqlParameterName(WholesaleRateHistoryRow.wholesale_route_id_PropName);

      IDbCommand _cmd = CreateGetCommand(_whereSql, WholesaleRateHistoryRow.date_on_DbName + " DESC ");
      AddParameter(_cmd, WholesaleRateHistoryRow.wholesale_route_id_PropName, pWholesaleRouteId);
      return _cmd;
    }

    public WholesaleRateHistoryRow[] GetCoveredBy(int pWholesaleRouteId, DateTime pDateOn, DateTime pDateOff) {

      //covered in full - delete it
      //existing     |------------------|
      //     new     |------------------|
      //     new   |--------------------|
      //     new     |----------------------|
      //     new |----------------------------|

      //OR *********************************************

      //date on covered
      //existing      |------------------|
      //     new      |-------------|
      //     new   |----------------|

      //OR

      //date off covered *******************************
      //existing      |------------------|
      //     new           |---------------|
      //     new           |------------------|

      string _where = WholesaleRateHistoryRow.wholesale_route_id_DbName + " = " + Database.CreateSqlParameterName(WholesaleRateHistoryRow.wholesale_route_id_PropName) +
        " AND " +
        "( " +
        "   (" +
        WholesaleRateHistoryRow.date_on_DbName + " BETWEEN " +
        Database.CreateSqlParameterName(WholesaleRateHistoryRow.date_on_PropName) +
        "     AND " +
        Database.CreateSqlParameterName(WholesaleRateHistoryRow.date_off_PropName) +
        "   )" +
        " OR " +
        "   (" +
        WholesaleRateHistoryRow.date_off_DbName + " BETWEEN " +
        Database.CreateSqlParameterName(WholesaleRateHistoryRow.date_on_PropName) +
        "     AND " +
        Database.CreateSqlParameterName(WholesaleRateHistoryRow.date_off_PropName) +
        "   )" +
        ")";

      IDbCommand _cmd = CreateGetCommand(_where, WholesaleRateHistoryRow.date_on_DbName);
      AddParameter(_cmd, WholesaleRateHistoryRow.wholesale_route_id_PropName, pWholesaleRouteId);
      AddParameter(_cmd, WholesaleRateHistoryRow.date_on_PropName, pDateOn);
      AddParameter(_cmd, WholesaleRateHistoryRow.date_off_PropName, pDateOff);

      return MapRecords(_cmd);
    }

    public WholesaleRateHistoryRow GetCoveringFor(int pWholesaleRouteId, DateTime pDateOn, DateTime pDateOff) {
      //existing      |-----------------------------|
      //     new            |---------------|
      string _where = WholesaleRateHistoryRow.wholesale_route_id_DbName + " = " + Database.CreateSqlParameterName(WholesaleRateHistoryRow.wholesale_route_id_PropName) +
        " AND " +
        "( " +
        "   (" +
        Database.CreateSqlParameterName(WholesaleRateHistoryRow.date_on_PropName) + " > " +
        WholesaleRateHistoryRow.date_on_DbName +
        "     AND " +
        Database.CreateSqlParameterName(WholesaleRateHistoryRow.date_on_PropName) + " < " +
        WholesaleRateHistoryRow.date_off_DbName +
        "   )" +
        " AND " +
        "   (" +
        Database.CreateSqlParameterName(WholesaleRateHistoryRow.date_off_PropName) + " > " +
        WholesaleRateHistoryRow.date_on_DbName +
        "     AND " +
        Database.CreateSqlParameterName(WholesaleRateHistoryRow.date_off_PropName) + " < " +
        WholesaleRateHistoryRow.date_off_DbName +
        "   )" +
        ")";

      IDbCommand _cmd = CreateGetCommand(_where, WholesaleRateHistoryRow.date_on_DbName);
      AddParameter(_cmd, WholesaleRateHistoryRow.wholesale_route_id_PropName, pWholesaleRouteId);
      AddParameter(_cmd, WholesaleRateHistoryRow.date_on_PropName, pDateOn);
      AddParameter(_cmd, WholesaleRateHistoryRow.date_off_PropName, pDateOff);

      using (IDataReader _reader = _cmd.ExecuteReader()) {
        WholesaleRateHistoryRow[] _tempArray = MapRecords(_reader);
        if (_tempArray.Length > 1) {
          throw new Exception("UNEXPECTED: more then 1 covering: " + _tempArray.Length);
        }
        return 0 == _tempArray.Length ? null : _tempArray[0];
      }
    }

    public WholesaleRateHistoryRow[] FindGaps(int pWholesaleRouteId) {
      //existing      |----|     |------|      |-------------------|
      //    gaps            |---|        |----|
      #region TEST SQL
      /*
        DECLARE @Wholesale_route_id INT
        SET @Wholesale_route_id = 10001
        --NOTE: date fileds are smalldatetime, cast them to datetime when doing DATEADD to avoid overflow
        SELECT @Wholesale_route_id AS Wholesale_route_id,
        DATEADD(day, 1, CAST(A.date_off AS DATETIME)) AS date_on, 
        DATEADD(day, -1, Min(CAST(B.date_on AS DATETIME)))  AS date_off,
        0 AS rate_info_id

        FROM WholesaleRateHistory AS A, WholesaleRateHistory AS B 
        WHERE 
        A.Wholesale_route_id = @Wholesale_route_id AND 
        B.Wholesale_route_id = @Wholesale_route_id AND 
        NOT EXISTS 	(
	        SELECT C.date_on FROM WholesaleRateHistory AS C 
	        WHERE C.Wholesale_route_id = @Wholesale_route_id AND 
		        DATEADD(day, 1, CAST(A.date_off AS DATETIME)) = C.date_on
	        ) 
        AND B.date_on > A.date_on 

        GROUP BY A.date_on, A.date_off
        ORDER BY A.date_on DESC
       */

      #endregion
      string _sql = "SELECT " + Database.CreateSqlParameterName(WholesaleRateHistoryRow.wholesale_route_id_PropName) + " " +
        " AS " + WholesaleRateHistoryRow.wholesale_route_id_DbName + ", " +
        " DATEADD(day, 1, CAST(A." + WholesaleRateHistoryRow.date_off_DbName + " AS DATETIME)) AS " + WholesaleRateHistoryRow.date_on_DbName + ",  " +
        " DATEADD(day, -1, Min(CAST(B." + WholesaleRateHistoryRow.date_on_DbName + " AS DATETIME)))  AS " + WholesaleRateHistoryRow.date_off_DbName + ", " +
        " 0 AS " + WholesaleRateHistoryRow.rate_info_id_DbName + " " +

        " FROM WholesaleRateHistory AS A, WholesaleRateHistory AS B  " +
        " WHERE  " +
        " A." + WholesaleRateHistoryRow.wholesale_route_id_DbName + " = " + Database.CreateSqlParameterName(WholesaleRateHistoryRow.wholesale_route_id_PropName) + " AND  " +
        " B." + WholesaleRateHistoryRow.wholesale_route_id_DbName + " = " + Database.CreateSqlParameterName(WholesaleRateHistoryRow.wholesale_route_id_PropName) + " AND  " +
        " NOT EXISTS 	( " +
        "   SELECT C." + WholesaleRateHistoryRow.date_on_DbName + " FROM WholesaleRateHistory AS C  " +
        "   WHERE C." + WholesaleRateHistoryRow.wholesale_route_id_DbName + " = " + Database.CreateSqlParameterName(WholesaleRateHistoryRow.wholesale_route_id_PropName) + " AND  " +
        "     DATEADD(day, 1, CAST(A." + WholesaleRateHistoryRow.date_off_DbName + " AS DATETIME)) = C." + WholesaleRateHistoryRow.date_on_DbName + " " +
        "   )  " +
        " AND B." + WholesaleRateHistoryRow.date_on_DbName + " > A." + WholesaleRateHistoryRow.date_on_DbName + "  " +

        " GROUP BY A." + WholesaleRateHistoryRow.date_on_DbName + ", A." + WholesaleRateHistoryRow.date_off_DbName + " " +
        " ORDER BY A." + WholesaleRateHistoryRow.date_on_DbName + " DESC";

      IDbCommand _cmd = Database.CreateCommand(_sql);
      AddParameter(_cmd, WholesaleRateHistoryRow.wholesale_route_id_PropName, pWholesaleRouteId);

      return MapRecords(_cmd);
    }

  } // End of WholesaleRateHistoryCollection class
} // End of namespace

// <fileinfo name="CarrierRateHistoryCollection.cs">
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
	/// Represents the <c>CarrierRateHistory</c> table.
	/// </summary>
	public class CarrierRateHistoryCollection : CarrierRateHistoryCollection_Base
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="CarrierRateHistoryCollection"/> class.
		/// </summary>
		/// <param name="db">The database object.</param>
		internal CarrierRateHistoryCollection(Rbr_Db db)
				: base(db)
		{
			// EMPTY
		}

		public static CarrierRateHistoryRow Parse(System.Data.DataRow row){
			return new CarrierRateHistoryCollection(null).MapRow(row);
		}

    public override bool Update(CarrierRateHistoryRow value) {
      string _sqlStr = "UPDATE [dbo].[CarrierRateHistory] SET " +
        CarrierRateHistoryRow.carrier_route_id_DbName + "=" + Database.CreateSqlParameterName(CarrierRateHistoryRow.carrier_route_id_PropName) + ", " +
        CarrierRateHistoryRow.date_on_DbName + "=" + Database.CreateSqlParameterName(CarrierRateHistoryRow.date_on_PropName) + ", " +
        CarrierRateHistoryRow.date_off_DbName + "=" + Database.CreateSqlParameterName(CarrierRateHistoryRow.date_off_PropName) +
        " WHERE " +
        CarrierRateHistoryRow.rate_info_id_DbName + "=" + Database.CreateSqlParameterName(CarrierRateHistoryRow.rate_info_id_PropName);
      IDbCommand _cmd = Database.CreateCommand(_sqlStr);
      AddParameter(_cmd, CarrierRateHistoryRow.carrier_route_id_PropName, value.Carrier_route_id);
      AddParameter(_cmd, CarrierRateHistoryRow.date_on_PropName, value.Date_on);
      AddParameter(_cmd, CarrierRateHistoryRow.date_off_PropName, value.Date_off);
      AddParameter(_cmd, CarrierRateHistoryRow.rate_info_id_PropName, value.Rate_info_id);
      return 0 != _cmd.ExecuteNonQuery();
    }

    public CarrierRateHistoryRow GetByRateInfoId(int pRateInfoId) {
      string _where = CarrierRateHistoryRow.rate_info_id_DbName + " = " + Database.CreateSqlParameterName(CarrierRateHistoryRow.rate_info_id_PropName);

      IDbCommand _cmd = CreateGetCommand(_where, null);
      AddParameter(_cmd, CarrierRateHistoryRow.rate_info_id_PropName, pRateInfoId);
      using (IDataReader _reader = _cmd.ExecuteReader()) {
        CarrierRateHistoryRow[] _tempArray = MapRecords(_reader);
        if (_tempArray.Length > 1) {
          throw new Exception("UNEXPECTED: more then 1 record for RateInfoId:" + pRateInfoId);
        }
        return 0 == _tempArray.Length ? null : _tempArray[0];
      }
    }

    public int DeleteByRateInfoId(int pRateInfoId) {
      string _where = CarrierRateHistoryRow.rate_info_id_DbName + " = " + Database.CreateSqlParameterName(CarrierRateHistoryRow.rate_info_id_PropName);

      IDbCommand _cmd = CreateDeleteCommand(_where);
      AddParameter(_cmd, CarrierRateHistoryRow.rate_info_id_PropName, pRateInfoId);
      return _cmd.ExecuteNonQuery();
    }

    public CarrierRateHistoryRow GetByCarrierRouteIdDate(int pCarrierRouteId, DateTime pDateTime) {
      string _where = CarrierRateHistoryRow.carrier_route_id_DbName + " = " + Database.CreateSqlParameterName(CarrierRateHistoryRow.carrier_route_id_PropName) +
        " AND " +
        "( " +
        Database.CreateSqlParameterName("SelectedDate") + " BETWEEN " +
        CarrierRateHistoryRow.date_on_DbName + " AND " + CarrierRateHistoryRow.date_off_DbName +
        ")";

      IDbCommand _cmd = CreateGetCommand(_where, null);
      AddParameter(_cmd, CarrierRateHistoryRow.carrier_route_id_PropName, pCarrierRouteId);
      Database.AddParameter(_cmd, "SelectedDate", DbType.DateTime, pDateTime);
      using (IDataReader _reader = _cmd.ExecuteReader()) {
        CarrierRateHistoryRow[] _tempArray = MapRecords(_reader);
        return 0 == _tempArray.Length ? null : _tempArray[0];
      }
    }

    protected override IDbCommand CreateGetByCarrier_route_idCommand(int pCarrierRouteId) {
      string _whereSql = "";
      _whereSql += CarrierRateHistoryRow.carrier_route_id_DbName + "=" + Database.CreateSqlParameterName(CarrierRateHistoryRow.carrier_route_id_PropName);

      IDbCommand _cmd = CreateGetCommand(_whereSql, CarrierRateHistoryRow.date_on_DbName + " DESC ");
      AddParameter(_cmd, CarrierRateHistoryRow.carrier_route_id_PropName, pCarrierRouteId);
      return _cmd;
    }

    public CarrierRateHistoryRow[] GetCoveredBy(int pCarrierRouteId, DateTime pDateOn, DateTime pDateOff) {

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

      string _where = CarrierRateHistoryRow.carrier_route_id_DbName + " = " + Database.CreateSqlParameterName(CarrierRateHistoryRow.carrier_route_id_PropName) +
        " AND " +
        "( " +
        "   (" + 
        CarrierRateHistoryRow.date_on_DbName + " BETWEEN " +
        Database.CreateSqlParameterName(CarrierRateHistoryRow.date_on_PropName) + 
        "     AND " +
        Database.CreateSqlParameterName(CarrierRateHistoryRow.date_off_PropName) + 
        "   )" + 
        " OR " +
        "   (" +
        CarrierRateHistoryRow.date_off_DbName + " BETWEEN " +
        Database.CreateSqlParameterName(CarrierRateHistoryRow.date_on_PropName) +
        "     AND " +
        Database.CreateSqlParameterName(CarrierRateHistoryRow.date_off_PropName) +
        "   )" +
        ")";

      IDbCommand _cmd = CreateGetCommand(_where, CarrierRateHistoryRow.date_on_DbName);
      AddParameter(_cmd, CarrierRateHistoryRow.carrier_route_id_PropName, pCarrierRouteId);
      AddParameter(_cmd, CarrierRateHistoryRow.date_on_PropName, pDateOn);
      AddParameter(_cmd, CarrierRateHistoryRow.date_off_PropName, pDateOff);

      return MapRecords(_cmd);
    }

    public CarrierRateHistoryRow GetCoveringFor(int pCarrierRouteId, DateTime pDateOn, DateTime pDateOff) {
      //existing      |-----------------------------|
      //     new            |---------------|
      string _where = CarrierRateHistoryRow.carrier_route_id_DbName + " = " + Database.CreateSqlParameterName(CarrierRateHistoryRow.carrier_route_id_PropName) +
        " AND " +
        "( " +
        "   (" + 
        Database.CreateSqlParameterName(CarrierRateHistoryRow.date_on_PropName) + " > " +
        CarrierRateHistoryRow.date_on_DbName + 
        "     AND " +
        Database.CreateSqlParameterName(CarrierRateHistoryRow.date_on_PropName) + " < " +
        CarrierRateHistoryRow.date_off_DbName + 
        "   )" + 
        " AND " +
        "   (" +
        Database.CreateSqlParameterName(CarrierRateHistoryRow.date_off_PropName) + " > " +
        CarrierRateHistoryRow.date_on_DbName + 
        "     AND " +
        Database.CreateSqlParameterName(CarrierRateHistoryRow.date_off_PropName) + " < " +
        CarrierRateHistoryRow.date_off_DbName +
        "   )" +
        ")";

      IDbCommand _cmd = CreateGetCommand(_where, CarrierRateHistoryRow.date_on_DbName);
      AddParameter(_cmd, CarrierRateHistoryRow.carrier_route_id_PropName, pCarrierRouteId);
      AddParameter(_cmd, CarrierRateHistoryRow.date_on_PropName, pDateOn);
      AddParameter(_cmd, CarrierRateHistoryRow.date_off_PropName, pDateOff);

      using (IDataReader _reader = _cmd.ExecuteReader()) {
        CarrierRateHistoryRow[] _tempArray = MapRecords(_reader);
        if (_tempArray.Length > 1) {
          throw new Exception("UNEXPECTED: more then 1 covering: " + _tempArray.Length);
        }
        return 0 == _tempArray.Length ? null : _tempArray[0];
      }
    }

    public CarrierRateHistoryRow[] FindGaps(int pCarrierRouteId) {
      //existing      |----|     |------|      |-------------------|
      //    gaps            |---|        |----|
      #region TEST SQL
      /*
        DECLARE @carrier_route_id INT
        SET @carrier_route_id = 10001
        --NOTE: date fileds are smalldatetime, cast them to datetime when doing DATEADD to avoid overflow
        SELECT @carrier_route_id AS carrier_route_id,
        DATEADD(day, 1, CAST(A.date_off AS DATETIME)) AS date_on, 
        DATEADD(day, -1, Min(CAST(B.date_on AS DATETIME)))  AS date_off,
        0 AS rate_info_id

        FROM CarrierRateHistory AS A, CarrierRateHistory AS B 
        WHERE 
        A.carrier_route_id = @carrier_route_id AND 
        B.carrier_route_id = @carrier_route_id AND 
        NOT EXISTS 	(
	        SELECT C.date_on FROM CarrierRateHistory AS C 
	        WHERE C.carrier_route_id = @carrier_route_id AND 
		        DATEADD(day, 1, CAST(A.date_off AS DATETIME)) = C.date_on
	        ) 
        AND B.date_on > A.date_on 

        GROUP BY A.date_on, A.date_off
        ORDER BY A.date_on DESC
       */
      
      #endregion
      string _sql = "SELECT " + Database.CreateSqlParameterName(CarrierRateHistoryRow.carrier_route_id_PropName) + " " +
        " AS " + CarrierRateHistoryRow.carrier_route_id_DbName + ", " +
        " DATEADD(day, 1, CAST(A." + CarrierRateHistoryRow.date_off_DbName + " AS DATETIME)) AS " + CarrierRateHistoryRow.date_on_DbName + ",  " +
        " DATEADD(day, -1, Min(CAST(B." + CarrierRateHistoryRow.date_on_DbName + " AS DATETIME)))  AS " + CarrierRateHistoryRow.date_off_DbName + ", " +
        " 0 AS " + CarrierRateHistoryRow.rate_info_id_DbName + " " +

        " FROM CarrierRateHistory AS A, CarrierRateHistory AS B  " +
        " WHERE  " +
        " A." + CarrierRateHistoryRow.carrier_route_id_DbName + " = " + Database.CreateSqlParameterName(CarrierRateHistoryRow.carrier_route_id_PropName) + " AND  " +
        " B." + CarrierRateHistoryRow.carrier_route_id_DbName + " = " + Database.CreateSqlParameterName(CarrierRateHistoryRow.carrier_route_id_PropName) + " AND  " +
        " NOT EXISTS 	( " +
        "   SELECT C." + CarrierRateHistoryRow.date_on_DbName + " FROM CarrierRateHistory AS C  " +
        "   WHERE C." + CarrierRateHistoryRow.carrier_route_id_DbName + " = " + Database.CreateSqlParameterName(CarrierRateHistoryRow.carrier_route_id_PropName) + " AND  " +
        "     DATEADD(day, 1, CAST(A." + CarrierRateHistoryRow.date_off_DbName + " AS DATETIME)) = C." + CarrierRateHistoryRow.date_on_DbName + " " +
        "   )  " +
        " AND B." + CarrierRateHistoryRow.date_on_DbName + " > A." + CarrierRateHistoryRow.date_on_DbName + "  " +

        " GROUP BY A." + CarrierRateHistoryRow.date_on_DbName + ", A." + CarrierRateHistoryRow.date_off_DbName + " " +
        " ORDER BY A." + CarrierRateHistoryRow.date_on_DbName + " DESC";

      IDbCommand _cmd = Database.CreateCommand(_sql);
      AddParameter(_cmd, CarrierRateHistoryRow.carrier_route_id_PropName, pCarrierRouteId);

      return MapRecords(_cmd);
    }

  } // End of CarrierRateHistoryCollection class
} // End of namespace

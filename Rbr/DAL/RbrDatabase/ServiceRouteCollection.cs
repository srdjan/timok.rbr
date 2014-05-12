// <fileinfo name="ServiceRouteCollection.cs">
//		<copyright>
//			Copyright © 2002-2006 Timok ES LLC. All rights reserved.
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
using Timok.Rbr.Core;

namespace Timok.Rbr.DAL.RbrDatabase {
  /// <summary>
  /// Represents the <c>ServiceRoute</c> table.
  /// </summary>
  public class ServiceRouteCollection : ServiceRouteCollection_Base {
    /// <summary>
    /// Initializes a new instance of the <see cref="ServiceRouteCollection"/> class.
    /// </summary>
    /// <param name="db">The database object.</param>
    internal ServiceRouteCollection(Rbr_Db db)
      : base(db) {
      // EMPTY
    }

    public static ServiceRouteRow Parse(System.Data.DataRow row) {
      return new ServiceRouteCollection(null).MapRow(row);
    }

    public ServiceRouteRow GetByServiceIdRouteId(short pServiceId, int pRouteId) {
      string _where =
        ServiceRouteRow.service_id_DbName + " = " +
        Database.CreateSqlParameterName(ServiceRouteRow.service_id_PropName) +
        " AND " +
        ServiceRouteRow.route_id_DbName + "=" +
        Database.CreateSqlParameterName(ServiceRouteRow.route_id_PropName);

      IDbCommand _cmd = CreateGetCommand(_where, null);
      AddParameter(_cmd, ServiceRouteRow.service_id_PropName, pServiceId);
      AddParameter(_cmd, ServiceRouteRow.route_id_PropName, pRouteId);

      using (IDataReader reader = Database.ExecuteReader(_cmd)) {
        ServiceRouteRow[] tempArray = MapRecords(reader);
        return 0 == tempArray.Length ? null : tempArray[0];
      }
    }

    public override void Insert(ServiceRouteRow value) {
      string _sqlStr =
        "DECLARE " + base.Database.CreateSqlParameterName(ServiceRouteRow.service_route_id_PropName) + " int " +
        "SET " + base.Database.CreateSqlParameterName(ServiceRouteRow.service_route_id_PropName);

      if (value.Service_route_id == -(value.Service_id)) {
        //DEFAULT Service's Route 
        _sqlStr += " = " + value.Service_route_id;
      }
      else {
        //Regular ServiceRoute
        _sqlStr += " = COALESCE((SELECT MAX(" + ServiceRouteRow.service_route_id_DbName + ") FROM ServiceRoute WHERE " + ServiceRouteRow.service_route_id_DbName + " > 0) + 1, 70001) ";
      }

      _sqlStr += "INSERT INTO [dbo].[ServiceRoute] (" +
        "[" + ServiceRouteRow.service_route_id_DbName + "], " +
        "[" + ServiceRouteRow.service_id_DbName + "], " +
        "[" + ServiceRouteRow.route_id_DbName + "], " +
        "[" + ServiceRouteRow.status_DbName + "], " +
        "[" + ServiceRouteRow.bonus_minutes_type_DbName + "], " +
        "[" + ServiceRouteRow.multiplier_DbName + "] " +
        ") VALUES (" +

      Database.CreateSqlParameterName(ServiceRouteRow.service_route_id_PropName) + ", " +
      Database.CreateSqlParameterName(ServiceRouteRow.service_id_PropName) + ", " +
      Database.CreateSqlParameterName(ServiceRouteRow.route_id_PropName) + ", " +
      Database.CreateSqlParameterName(ServiceRouteRow.status_PropName) + ", " +
      Database.CreateSqlParameterName(ServiceRouteRow.bonus_minutes_type_PropName) + ", " +
      Database.CreateSqlParameterName(ServiceRouteRow.multiplier_PropName) + ") " +
      "SELECT " + base.Database.CreateSqlParameterName(ServiceRouteRow.service_route_id_PropName);

      IDbCommand _cmd = Database.CreateCommand(_sqlStr);
      //AddParameter(_cmd, ServiceRouteRow.service_route_id_PropName, value.Service_route_id);
      AddParameter(_cmd, ServiceRouteRow.service_id_PropName, value.Service_id);

      AddParameter(_cmd, ServiceRouteRow.route_id_PropName,
        value.IsRoute_idNull ? DBNull.Value : (object) value.Route_id);

      AddParameter(_cmd, ServiceRouteRow.status_PropName, value.Status);
      AddParameter(_cmd, ServiceRouteRow.bonus_minutes_type_PropName, value.Bonus_minutes_type);
      AddParameter(_cmd, ServiceRouteRow.multiplier_PropName, value.Multiplier);

      value.Service_route_id = (int) _cmd.ExecuteScalar();
    }

    public ServiceRouteRow[] GetByServiceIdCountryId(short pServiceId, int pCountryId) {
      /*
      SELECT * 
      FROM [RbrDb_265].[dbo].[ServiceRoute]
      WHERE service_id = 700 
      AND route_id IN (
        SELECT route_id FROM Route 
        WHERE country_id = 202
      ) 
       */
      string _where =
        ServiceRouteRow.service_id_DbName + " = " +
        Database.CreateSqlParameterName(ServiceRouteRow.service_id_PropName) +
        " AND " +
        ServiceRouteRow.route_id_DbName + " IN (" +
        "     SELECT " + RouteRow.route_id_DbName + " FROM Route " +
        "     WHERE " + RouteRow.country_id_DbName + "=" + pCountryId +
        ")";

      IDbCommand _cmd = CreateGetCommand(_where, null);
      AddParameter(_cmd, ServiceRouteRow.service_id_PropName, pServiceId);

      using (IDataReader reader = Database.ExecuteReader(_cmd)) {
        return MapRecords(reader);
      }
    }

    //NOTE: this sql needs to return all carrier routes that suppor given cust dial code and all subcodes down to ccode
    //example: cust Dial Code 5234 >> look for all carrier routes that cover: 5234, 523, 52 (asuming ccode is 52)
    public ServiceRouteRow[] GetActiveByServiceTypeDialedNumber(Timok.Rbr.Core.ServiceType pServiceType, string pDialedNumber) {
      if (pDialedNumber == null || pDialedNumber.Length == 0) {//dial code should never be less than 7
        throw new ArgumentException("Invalid Dialed Number: " + pDialedNumber);
      }
      #region sql
      /*
      SELECT * FROM ServiceRoute 
      WHERE 
        status = 1
      AND 
        service_id IN (
          SELECT service_id FROM Service 
          WHERE type = 0 --Carrier ServiceType
        )
      AND 
        route_id IN (
          SELECT route_id FROM  DialCode
          WHERE '544573476758' 
          LIKE CAST(dial_code AS varchar) + '%'
        )
      */
      #endregion sql

      string _where =
        ServiceRouteRow.status_DbName + " = " + (byte) Status.Active + " " +
        "AND  " +
          ServiceRouteRow.service_id_DbName + " IN ( " +
          "	 SELECT " + ServiceRow.service_id_DbName + " " +
          "	 FROM Service WHERE " +
          "	 " + ServiceRow.type_DbName + " = " + (byte) pServiceType + " " +
          ") " +
        "AND " +
          ServiceRouteRow.route_id_DbName + " IN ( " +
          "	SELECT " + DialCodeRow.route_id_DbName + " FROM  DialCode " +
          "	WHERE " + Database.CreateSqlParameterName("DialedNumber") +
          " LIKE CAST(" + DialCodeRow.dial_code_DbName + " AS varchar) + '%' " +
          ") ";

      IDbCommand _cmd = CreateGetCommand(_where, null);
      Database.AddParameter(_cmd, "DialedNumber", DbType.AnsiString, pDialedNumber);
      using (IDataReader _reader = base.Database.ExecuteReader(_cmd)) {
        return MapRecords(_reader);
      }
    }

    //public RouteRow[] GetActiveByOwnershipType(OwnershipType pOwnershipType) {
    //  #region sql
    //  /*
    //  SELECT Route.* FROM Route 
    //  WHERE 
    //  service_id 
    //  IN (
    //    SELECT 
    //    service_id 
    //    FROM Service WHERE 
    //    ownership_type = 2
    //  )
    //  AND 
    //  status = 1
    //  */
    //  #endregion sql

    //  string _sql = "SELECT Route.* FROM Route " +
    //  "WHERE " + RouteRow.service_id_DbName + " " +
    //  " IN ( " +
    //  "	 SELECT " + ServiceRow.service_id_DbName + " " +
    //  "	 FROM Service WHERE " +
    //  "	 " + ServiceRow.ownership_type_DbName + " = " + (byte) pOwnershipType + " " +
    //  ") " +
    //  "AND " + RouteRow.status_DbName + " = " + (byte) Status.Active;

    //  IDbCommand _cmd = Database.CreateCommand(_sql);
    //  using (IDataReader _reader = base.Database.ExecuteReader(_cmd)) {
    //    return MapRecords(_reader);
    //  }
    //}
  } // End of ServiceRouteCollection class
} // End of namespace

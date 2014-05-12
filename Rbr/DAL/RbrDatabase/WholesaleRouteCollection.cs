// <fileinfo name="WholesaleRouteCollection.cs">
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
using System.Collections;
using System.Data;
using Timok.Rbr.DAL.RbrDatabase.Base;

namespace Timok.Rbr.DAL.RbrDatabase {
  /// <summary>
  /// Represents the <c>WholesaleRoute</c> table.
  /// </summary>
  public class WholesaleRouteCollection : WholesaleRouteCollection_Base {
    /// <summary>
    /// Initializes a new instance of the <see cref="WholesaleRouteCollection"/> class.
    /// </summary>
    /// <param name="db">The database object.</param>
    internal WholesaleRouteCollection(Rbr_Db db)
      : base(db) {
      // EMPTY
    }

    public static WholesaleRouteRow Parse(System.Data.DataRow row) {
      return new WholesaleRouteCollection(null).MapRow(row);
    }

    public override void Insert(WholesaleRouteRow value) {
      string _sqlStr =
        "DECLARE " + Database.CreateSqlParameterName(WholesaleRouteRow.wholesale_route_id_PropName) + " int " +
        "SET " + Database.CreateSqlParameterName(WholesaleRouteRow.wholesale_route_id_PropName);

      if (value.Wholesale_route_id == -(value.Service_id)) {
        //DEFAULT Service's Route 
        _sqlStr += " = " + value.Wholesale_route_id;
      }
      else {
        //Regular WholesaleRoute
        _sqlStr += " = COALESCE((SELECT MAX(" + WholesaleRouteRow.wholesale_route_id_DbName + ") FROM WholesaleRoute WHERE " + WholesaleRouteRow.wholesale_route_id_DbName + " > 0) + 1, 10000) ";
      }
      
      _sqlStr += " INSERT INTO [dbo].[WholesaleRoute] (" +
        "[" + WholesaleRouteRow.wholesale_route_id_DbName + "], " +
        "[" + WholesaleRouteRow.service_id_DbName + "], " +
        "[" + WholesaleRouteRow.route_id_DbName + "], " +
				"[" + WholesaleRouteRow.status_DbName + "]" +
				") VALUES (" +
        Database.CreateSqlParameterName(WholesaleRouteRow.wholesale_route_id_PropName) + ", " +
        Database.CreateSqlParameterName(WholesaleRouteRow.service_id_PropName) + ", " +
        Database.CreateSqlParameterName(WholesaleRouteRow.route_id_PropName) + ", " +
				Database.CreateSqlParameterName(WholesaleRouteRow.status_PropName) + ") " +
				" SELECT " + Database.CreateSqlParameterName(WholesaleRouteRow.wholesale_route_id_PropName);

      IDbCommand _cmd = Database.CreateCommand(_sqlStr);
      //AddParameter(cmd, "Wholesale_route_id", value.Wholesale_route_id);
      AddParameter(_cmd, WholesaleRouteRow.service_id_PropName, value.Service_id);
      AddParameter(_cmd, WholesaleRouteRow.route_id_PropName,
        value.IsRoute_idNull ? DBNull.Value : (object) value.Route_id);
			AddParameter(_cmd, WholesaleRouteRow.status_PropName, value.Status);

      value.Wholesale_route_id = (int) _cmd.ExecuteScalar();
    }

    public WholesaleRouteRow GetByServiceIdBaseRouteId(short pServiceId, int pBaseRouteId) {
      string _where = WholesaleRouteRow.service_id_DbName + " = " +
        Database.CreateSqlParameterName(WholesaleRouteRow.service_id_PropName) +
        " AND " +
        WholesaleRouteRow.route_id_DbName + "=" +
        Database.CreateSqlParameterName(WholesaleRouteRow.route_id_PropName);

      IDbCommand _cmd = CreateGetCommand(_where, null);
      AddParameter(_cmd, WholesaleRouteRow.service_id_PropName, pServiceId);
      AddParameter(_cmd, WholesaleRouteRow.route_id_PropName, pBaseRouteId);

      using (IDataReader reader = Database.ExecuteReader(_cmd)) {
        WholesaleRouteRow[] tempArray = MapRecords(reader);
        return 0 == tempArray.Length ? null : tempArray[0];
      }
    }

    public int GetCount(int pBaseRouteId) {
      string _sqlStr = "SELECT COUNT(*) FROM WholesaleRoute WHERE " +
        "[" + WholesaleRouteRow.route_id_DbName + "]=" + base.Database.CreateSqlParameterName(WholesaleRouteRow.route_id_PropName);

      IDbCommand _cmd = base.Database.CreateCommand(_sqlStr);
      AddParameter(_cmd, WholesaleRouteRow.route_id_PropName, pBaseRouteId);
      return ((int) _cmd.ExecuteScalar());
    }

  	public WholesaleRouteRow[] GetByService_idCountry_id(short pServiceId, int pCountryId) {
			/*
				SELECT WholesaleRoute.*
				FROM Route INNER JOIN WholesaleRoute ON Route.route_id = WholesaleRoute.route_id
				where
				WholesaleRoute.service_id = 7084
				AND 
				Route.country_id = 123 
			 */
			var _sqlStr = "SELECT * FROM [dbo].[Route]  INNER JOIN WholesaleRoute ON " +
				"Route." + RouteRow.route_id_DbName + " = WholesaleRoute." + WholesaleRouteRow.route_id_DbName + " " +
				" WHERE " +
				"WholesaleRoute.[" + WholesaleRouteRow.service_id_DbName + "]=" + Database.CreateSqlParameterName(WholesaleRouteRow.service_id_PropName) + 
				" AND " +
				"Route.[" + RouteRow.country_id_DbName + "]= " + Database.CreateSqlParameterName(RouteRow.country_id_PropName);

			var _cmd = Database.CreateCommand(_sqlStr);
			Database.AddParameter(_cmd, WholesaleRouteRow.service_id_PropName, DbType.Int16, pServiceId);
			Database.AddParameter(_cmd, RouteRow.country_id_PropName, DbType.Int32, pCountryId);
			using (var _reader = _cmd.ExecuteReader()) {
				return MapRecords(_reader);
			}

			//string _where = WholesaleRouteRow.service_id_DbName + " = " +
			//  Database.CreateSqlParameterName(WholesaleRouteRow.service_id_PropName) +
			//  " AND " +
			//  WholesaleRouteRow.route_id_DbName + "=" +
			//  Database.CreateSqlParameterName(WholesaleRouteRow.route_id_PropName);

			//IDbCommand cmd = CreateGetCommand(_whereSql, null);
			//AddParameter(cmd, "Service_id", service_id);
			//return MapRecords(cmd);
		}
  } // End of WholesaleRouteCollection class
} // End of namespace

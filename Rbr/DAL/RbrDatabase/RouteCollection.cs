// <fileinfo name="RouteCollection.cs">
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
  public class RouteCollection : RouteCollection_Base {
    internal RouteCollection(Rbr_Db db) : base(db) {
      // EMPTY
    }

    public static RouteRow Parse(DataRow row) {
      return new RouteCollection(null).MapRow(row);
    }

    public bool Deactivate(int route_id) {
      var sqlStr = "UPDATE dbo.Route " +
        "SET dbo.Route.[" + RouteRow_Base.status_DbName + "]=" +
        Database.CreateSqlParameterName(RouteRow_Base.status_PropName) +
        " WHERE dbo.Route.[" + RouteRow_Base.route_id_DbName + "]=" +
        Database.CreateSqlParameterName(RouteRow_Base.route_id_PropName) + " " +
        " SELECT @@ROWCOUNT ";

      var cmd = Database.CreateCommand(sqlStr);
      AddParameter(cmd, RouteRow_Base.status_PropName, (byte) Status.Pending);
      AddParameter(cmd, RouteRow_Base.route_id_PropName, route_id);
      var _res = cmd.ExecuteNonQuery();
      return (_res == 1) ? true : false;
    }

    public bool Activate(int route_id) {
      var sqlStr = "UPDATE dbo.Route " +
        "SET dbo.Route.[" + RouteRow_Base.status_DbName + "]=" +
        Database.CreateSqlParameterName(RouteRow_Base.status_PropName) +
        " WHERE dbo.Route.[" + RouteRow_Base.route_id_DbName + "]=" +
        Database.CreateSqlParameterName(RouteRow_Base.route_id_PropName) + " " +
        " SELECT @@ROWCOUNT ";

      var cmd = Database.CreateCommand(sqlStr);
      AddParameter(cmd, RouteRow_Base.status_PropName, (byte) Status.Active);
      AddParameter(cmd, RouteRow_Base.route_id_PropName, route_id);
      var _res = cmd.ExecuteNonQuery();
      return (_res == 1) ? true : false;
    }

    public bool Archive(int route_id) {
      var sqlStr = "UPDATE dbo.Route " +
        "SET dbo.Route.[" + RouteRow_Base.status_DbName + "]=" +
        Database.CreateSqlParameterName(RouteRow_Base.status_PropName) +
        " WHERE dbo.Route.[" + RouteRow_Base.route_id_DbName + "]=" +
        Database.CreateSqlParameterName(RouteRow_Base.route_id_PropName) + " " +
        " SELECT @@ROWCOUNT ";

      var cmd = Database.CreateCommand(sqlStr);
      AddParameter(cmd, RouteRow_Base.status_PropName, (byte) Status.Archived);
      AddParameter(cmd, RouteRow_Base.route_id_PropName, route_id);
      var _res = cmd.ExecuteNonQuery();
      return (_res == 1) ? true : false;
    }

    protected override IDbCommand CreateGetAllCommand() {
      return CreateGetCommand(null, RouteRow_Base.name_DbName);
    }

    protected override IDbCommand CreateGetByCountry_idCommand(int country_id) {
      var _whereSql = "[" + RouteRow_Base.country_id_DbName + "]=" + Database.CreateSqlParameterName(RouteRow_Base.country_id_PropName);

      var _cmd = CreateGetCommand(_whereSql, RouteRow_Base.name_DbName);
      AddParameter(_cmd, RouteRow_Base.country_id_PropName, country_id);
      return _cmd;
    }

    public RouteRow GetProperByCallingPlanIdCountryId(int pCallingPlanId, int pCountryId) {
      var _where =
        RouteRow_Base.calling_plan_id_DbName + "=" + Database.CreateSqlParameterName(RouteRow_Base.calling_plan_id_PropName) +
        " AND " +
        RouteRow_Base.country_id_DbName + "=" + Database.CreateSqlParameterName(RouteRow_Base.country_id_PropName) +
        " AND " + RouteRow_Base.name_DbName + " LIKE " +
        Database.CreateSqlParameterName("ProperParam");

      var _cmd = CreateGetCommand(_where, null);
      AddParameter(_cmd, RouteRow_Base.calling_plan_id_PropName, pCallingPlanId);
      AddParameter(_cmd, RouteRow_Base.country_id_PropName, pCountryId);
      Database.AddParameter(_cmd, "ProperParam", DbType.AnsiString, "%" + AppConstants.SubRouteSeparator + AppConstants.ProperNameSuffix);
      using (var reader = Database.ExecuteReader(_cmd)) {
        var tempArray = MapRecords(reader);
        return 0 == tempArray.Length ? null : tempArray[0];
      }
    }

    public RouteRow[] GetByCallingPlanIdCountryId(int pCallingPlanId, int pCountryId) {
      var _where =
        RouteRow_Base.calling_plan_id_DbName + "=" + Database.CreateSqlParameterName(RouteRow_Base.calling_plan_id_PropName) +
        " AND " +
        RouteRow_Base.country_id_DbName + "=" + Database.CreateSqlParameterName(RouteRow_Base.country_id_PropName);

      var _cmd = CreateGetCommand(_where, RouteRow_Base.name_DbName);
      AddParameter(_cmd, RouteRow_Base.calling_plan_id_PropName, pCallingPlanId);
      AddParameter(_cmd, RouteRow_Base.country_id_PropName, pCountryId);
      using (var _reader = Database.ExecuteReader(_cmd)) {
        return MapRecords(_reader);
      }
    }

    public RouteRow[] GetByCallingPlanIdRoutingPlanId(int pCallingPlanId, int pRoutingPlanId) {
      /*
        SELECT * FROM  Route WHERE 
        calling_plan_id = 1
        AND 
        route_id IN (
	        SELECT route_id FROM RoutingPlanDetail
	        WHERE routing_plan_id = 8000
        )
      */
      var _where =
        RouteRow_Base.calling_plan_id_DbName + "=" + Database.CreateSqlParameterName(RouteRow_Base.calling_plan_id_PropName) +
        " AND " +
        RouteRow_Base.route_id_DbName + " IN (" +
        "  SELECT " + RoutingPlanDetailRow_Base.route_id_DbName + " FROM RoutingPlanDetail " +
        "  WHERE " + RoutingPlanDetailRow_Base.routing_plan_id_DbName + " = " + pRoutingPlanId + 
        ")";

      var _cmd = CreateGetCommand(_where, RouteRow_Base.name_DbName);
      AddParameter(_cmd, RouteRow_Base.calling_plan_id_PropName, pCallingPlanId);
      using (var _reader = Database.ExecuteReader(_cmd)) {
        return MapRecords(_reader);
      }
    }

    public RouteRow[] GetByCallingPlanIdCarrierAcctId(int pCallingPlanId, short pCarrierAcctId) {
      /*
        SELECT * FROM  Route WHERE 
        calling_plan_id = 1
        AND 
        route_id IN (
	        SELECT route_id FROM CarrierRoute
	        WHERE carrier_acct_id = 8000
        )
      */
      var _where =
        RouteRow_Base.calling_plan_id_DbName + "=" + Database.CreateSqlParameterName(RouteRow_Base.calling_plan_id_PropName) +
        " AND " +
        RouteRow_Base.route_id_DbName + " IN (" +
        "  SELECT " + CarrierRouteRow_Base.route_id_DbName + " FROM CarrierRoute " +
        "  WHERE " + CarrierRouteRow_Base.carrier_acct_id_DbName + " = " + pCarrierAcctId + 
        ")";

      var _cmd = CreateGetCommand(_where, RouteRow_Base.name_DbName);
      AddParameter(_cmd, RouteRow_Base.calling_plan_id_PropName, pCallingPlanId);
      using (var _reader = Database.ExecuteReader(_cmd)) {
        return MapRecords(_reader);
      }
    }

    public override void Insert(RouteRow value) {
      string _sqlStr = "DECLARE " +
        Database.CreateSqlParameterName(RouteRow_Base.route_id_PropName) + " int " +
        "SET " + Database.CreateSqlParameterName(RouteRow_Base.route_id_PropName) +
        " = COALESCE((SELECT MAX(" + RouteRow_Base.route_id_DbName + ") FROM Route) + 1, 1) " +

        "INSERT INTO [dbo].[Route] (" +
        "[" + RouteRow_Base.route_id_DbName + "], " +
        "[" + RouteRow_Base.name_DbName + "], " +
        "[" + RouteRow_Base.status_DbName + "], " +
        "[" + RouteRow_Base.calling_plan_id_DbName + "], " +
				"[" + RouteRow_Base.country_id_DbName + "], " +
				"[" + RouteRow_Base.routing_number_PropName + "], " +
				"[" + RouteRow_Base.version_DbName + "] " +
        ") VALUES (" +
        Database.CreateSqlParameterName(RouteRow_Base.route_id_PropName) + ", " +
        Database.CreateSqlParameterName(RouteRow_Base.name_PropName) + ", " +
        Database.CreateSqlParameterName(RouteRow_Base.status_PropName) + ", " +
        Database.CreateSqlParameterName(RouteRow_Base.calling_plan_id_PropName) + ", " +
				Database.CreateSqlParameterName(RouteRow_Base.country_id_PropName) + ", " +
				Database.CreateSqlParameterName(RouteRow_Base.routing_number_PropName) + ", " +
				Database.CreateSqlParameterName(RouteRow_Base.version_PropName) + ") " +
        "SELECT " + Database.CreateSqlParameterName(RouteRow_Base.route_id_PropName);

      var _cmd = Database.CreateCommand(_sqlStr);
      AddParameter(_cmd, RouteRow_Base.name_PropName, value.Name);
      AddParameter(_cmd, RouteRow_Base.status_PropName, value.Status);
      AddParameter(_cmd, RouteRow_Base.calling_plan_id_PropName, value.Calling_plan_id);
			AddParameter(_cmd, RouteRow_Base.country_id_PropName, value.Country_id);
			AddParameter(_cmd, RouteRow_Base.routing_number_PropName, value.Routing_number);
			AddParameter(_cmd, RouteRow_Base.version_PropName, value.Version);

      try {
        var _res = _cmd.ExecuteScalar();
        value.Route_id = (int) _res;
      }
      catch (System.Data.SqlClient.SqlException _sqlEx) {
        if (_sqlEx.Message.IndexOf("Cannot insert duplicate key row in object") > -1) {
          throw new AlternateKeyException("Route Name already in use!", _sqlEx);
        }
        throw;// any other ex
      }
    }

    public RouteRow GetByName(int pCallingPlanId, string pName) {
      var whereSql =
        "[" + RouteRow_Base.calling_plan_id_DbName + "]=" +
        Database.CreateSqlParameterName(RouteRow_Base.calling_plan_id_PropName) +
        " AND " +
        "[" + RouteRow_Base.name_DbName + "]=" +
        Database.CreateSqlParameterName(RouteRow_Base.name_PropName);

      var cmd = CreateGetCommand(whereSql, null);
      AddParameter(cmd, RouteRow_Base.calling_plan_id_PropName, pCallingPlanId);
      AddParameter(cmd, RouteRow_Base.name_PropName, pName);
      using (var reader = Database.ExecuteReader(cmd)) {
        var tempArray = MapRecords(reader);
        return 0 == tempArray.Length ? null : tempArray[0];
      }
    }

		public RouteRow[] GetAllCarrierRoutes(string pDialedNumber) {
			/*
				SELECT Route.* FROM  Route 
				INNER JOIN DialCode ON 
							Route.route_id = DialCode.route_id 
				where DialCode.calling_plan_id in (select distinct calling_plan_id FROM CarrierAcct)
							and (@DialedNumber LIKE CAST(dial_code AS varchar) + '%')
			*/

			var _sqlStr = "SELECT Route.* FROM [dbo].[Route] " +
			              " INNER JOIN [dbo].DialCode ON " +
			              " Route." + RouteRow_Base.route_id_DbName + " = DialCode." + DialCodeRow_Base.route_id_DbName +
			              " WHERE DialCode." + DialCodeRow_Base.calling_plan_id_DbName + " IN (" +
			              " SELECT DISTINCT CarrierAcct." + CarrierAcctRow_Base.calling_plan_id_DbName + " FROM CarrierAcct )" +
										" AND " + "( " + Database.CreateSqlParameterName("DialedNumber") +
			              " LIKE CAST([" + DialCodeRow_Base.dial_code_DbName + "] AS varchar) + '%') ";

			var _cmd = Database.CreateCommand(_sqlStr);
			Database.AddParameter(_cmd, "DialedNumber", DbType.AnsiString, pDialedNumber);

			using (var _reader = Database.ExecuteReader(_cmd)) {
				return MapRecords(_reader);
			}
		}

  	public RouteRow[] GetAllCarrierRoutes() {
			/*
			SELECT * FROM  Route WHERE 
			route_id IN (
				SELECT DISTINCT route_id FROM CarrierRoute
			)
			*/

			var _sqlStr = "SELECT Route.* FROM [dbo].[Route] " +
			              " WHERE " + RouteRow_Base.route_id_DbName + " IN (" +
			              " SELECT DISTINCT " + CarrierRouteRow_Base.route_id_DbName + " FROM CarrierRoute )";

			var _cmd = Database.CreateCommand(_sqlStr);
			using (var _reader = Database.ExecuteReader(_cmd)) {
				return MapRecords(_reader);
			}
		}

    public RouteRow[] GetUnusedByCallingPlanIdCarrierAcctId(int pCallingPlanId, short pCarrierAcctId) {
      /*
      SELECT * FROM Route 
      WHERE calling_plan_id = 1 
      AND 
      route_id NOT IN (
        SELECT route_id FROM CarrierRoute 
        WHERE route_id IS NOT NULL AND carrier_acct_id = 701
      )
       */
      var _where = RouteRow_Base.calling_plan_id_DbName + "=" + Database.CreateSqlParameterName(RouteRow_Base.calling_plan_id_PropName) +
        " AND " + RouteRow_Base.route_id_DbName + " NOT IN ( " +
        "    SELECT " + CarrierRouteRow_Base.route_id_DbName + " FROM CarrierRoute " +
        "    WHERE " + CarrierRouteRow_Base.route_id_DbName + " IS NOT NULL " +
        "    AND " + CarrierRouteRow_Base.carrier_acct_id_DbName + "=" + pCarrierAcctId +
        " )";

      var _cmd = CreateGetCommand(_where, RouteRow_Base.name_DbName);
      AddParameter(_cmd, RouteRow_Base.calling_plan_id_PropName, pCallingPlanId);
      using (var _reader = Database.ExecuteReader(_cmd)) {
        return MapRecords(_reader);
      }
    }

    //TODO: NEW DAL - NOT FINISHED !!!
    public RouteRow[] GetUnusedByCallingPlanIdRoutingPlanIdServiceId(int pCallingPlanId, int pRoutingPlanId, short pServiceId) {
      /*
      SELECT * FROM Route 
      WHERE 
      calling_plan_id = 2
      AND route_id IN ( 
	      SELECT route_id FROM RoutingPlanDetail 
		      WHERE routing_plan_id = 8001
		      AND route_id NOT IN ( 
			      SELECT route_id FROM WholesaleRoute 
				      WHERE route_id IS NOT NULL --skip Service's default WholesaleRoutes
				      AND service_id = 700
		      )
      )
       */
      var _where = RouteRow_Base.calling_plan_id_DbName + "=" + Database.CreateSqlParameterName(RouteRow_Base.calling_plan_id_PropName) +
        " AND " + RouteRow_Base.route_id_DbName + " IN ( " +
        "   SELECT " + RoutingPlanDetailRow_Base.route_id_DbName + " FROM RoutingPlanDetail " +
        "   WHERE " + RoutingPlanDetailRow_Base.routing_plan_id_DbName + " = " + pRoutingPlanId +
        "   AND " + RoutingPlanDetailRow_Base.route_id_DbName + " NOT IN ( " +
        "       SELECT " + WholesaleRouteRow_Base.route_id_DbName + " FROM WholesaleRoute " +
        "       WHERE " + WholesaleRouteRow_Base.route_id_DbName + " IS NOT NULL " + //skip Service's default WholesaleRoutes
        "       AND " + WholesaleRouteRow_Base.service_id_DbName + " = " + pServiceId +
        "   )" +
        " )";

      var _cmd = CreateGetCommand(_where, RouteRow_Base.name_DbName);
      AddParameter(_cmd, RouteRow_Base.calling_plan_id_PropName, pCallingPlanId);
      using (var _reader = Database.ExecuteReader(_cmd)) {
        return MapRecords(_reader);
      }
    }

    public RouteRow[] GetUnusedByCallingPlanIdRoutingPlanId(int pCallingPlanId, int pRoutingPlanId) {
      /*
      SELECT * FROM Route 
      WHERE calling_plan_id = 1 
      AND 
      route_id NOT IN (
        SELECT route_id FROM CarrierRoute 
        WHERE route_id IS NOT NULL AND carrier_acct_id = 701
      )
       */
      var _where = RouteRow_Base.calling_plan_id_DbName + "=" + Database.CreateSqlParameterName(RouteRow_Base.calling_plan_id_PropName) +
        " AND " + RouteRow_Base.route_id_DbName + " NOT IN ( " +
        "    SELECT " + RoutingPlanDetailRow_Base.route_id_DbName + " FROM RoutingPlanDetail " +
        "    WHERE " + RoutingPlanDetailRow_Base.route_id_DbName + " IS NOT NULL " +
        "    AND " + RoutingPlanDetailRow_Base.routing_plan_id_DbName + "=" + pRoutingPlanId +
        " )";

      var _cmd = CreateGetCommand(_where, RouteRow_Base.name_DbName);
      AddParameter(_cmd, RouteRow_Base.calling_plan_id_PropName, pCallingPlanId);
      using (var _reader = Database.ExecuteReader(_cmd)) {
        return MapRecords(_reader);
      }
    }
  } // End of RouteCollection class
} // End of namespace

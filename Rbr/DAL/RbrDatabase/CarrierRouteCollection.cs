// <fileinfo name="CarrierRouteCollection.cs">
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
  /// Represents the <c>CarrierRoute</c> table.
  /// </summary>
	public class CarrierRouteCollection : CarrierRouteCollection_Base {
		/// <summary>
		/// Initializes a new instance of the <see cref="CarrierRouteCollection"/> class.
		/// </summary>
		/// <param name="db">The database object.</param>
		internal CarrierRouteCollection(Rbr_Db db)
			: base(db) {
			// EMPTY
		}

		public static CarrierRouteRow Parse(System.Data.DataRow row) {
			return new CarrierRouteCollection(null).MapRow(row);
		}

		public override void Insert(CarrierRouteRow value) {
			string _sqlStr =
				"DECLARE " + base.Database.CreateSqlParameterName(CarrierRouteRow.carrier_route_id_PropName) + " int " +
				"SET " + base.Database.CreateSqlParameterName(CarrierRouteRow.carrier_route_id_PropName);

			if (value.Carrier_route_id == -( value.Carrier_acct_id )) {
				//DEFAULT Carrier's Route 
				_sqlStr += " = " + value.Carrier_route_id;
			}
			else {
				//Regular CarrierRoute
				_sqlStr += " = COALESCE((SELECT MAX(" + CarrierRouteRow.carrier_route_id_DbName + ") FROM CarrierRoute WHERE " + CarrierRouteRow.carrier_route_id_DbName + " > 0) + 1, 10000) ";
			}

			_sqlStr += "INSERT INTO [dbo].[CarrierRoute] (" +
				"[" + CarrierRouteRow.carrier_route_id_DbName + "], " +
				"[" + CarrierRouteRow.carrier_acct_id_DbName + "], " +
				"[" + CarrierRouteRow.route_id_DbName + "], " +
				"[" + CarrierRouteRow.status_DbName + "], " +
				"[" + CarrierRouteRow.asr_time_window_DbName + "], " +
				"[" + CarrierRouteRow.asr_target_DbName + "], " +
				"[" + CarrierRouteRow.acd_time_window_DbName + "], " +
				"[" + CarrierRouteRow.acd_target_DbName + "], " +
				"[" + CarrierRouteRow.next_ep_DbName + "] " +
				") VALUES (" +

			Database.CreateSqlParameterName(CarrierRouteRow.carrier_route_id_PropName) + ", " +
			Database.CreateSqlParameterName(CarrierRouteRow.carrier_acct_id_PropName) + ", " +
			Database.CreateSqlParameterName(CarrierRouteRow.route_id_PropName) + ", " +
			Database.CreateSqlParameterName(CarrierRouteRow.status_PropName) + ", " +
			Database.CreateSqlParameterName(CarrierRouteRow.asr_time_window_PropName) + ", " +
			Database.CreateSqlParameterName(CarrierRouteRow.asr_target_PropName) + ", " +
			Database.CreateSqlParameterName(CarrierRouteRow.acd_time_window_PropName) + ", " +
			Database.CreateSqlParameterName(CarrierRouteRow.acd_target_PropName) + ", " +
			Database.CreateSqlParameterName(CarrierRouteRow.next_ep_PropName) + ") " +
			"SELECT " + base.Database.CreateSqlParameterName(CarrierRouteRow.carrier_route_id_PropName);

			var _cmd = Database.CreateCommand(_sqlStr);
			//AddParameter(_cmd, CarrierRouteRow.carrier_route_id_PropName, value.Carrier_route_id);
			AddParameter(_cmd, CarrierRouteRow.carrier_acct_id_PropName, value.Carrier_acct_id);

			AddParameter(_cmd, CarrierRouteRow.route_id_PropName,
				value.IsRoute_idNull ? DBNull.Value : (object)value.Route_id);

			AddParameter(_cmd, CarrierRouteRow.status_PropName, value.Status);
			AddParameter(_cmd, CarrierRouteRow.asr_time_window_PropName, value.Asr_time_window);
			AddParameter(_cmd, CarrierRouteRow.asr_target_PropName, value.Asr_target);
			AddParameter(_cmd, CarrierRouteRow.acd_time_window_PropName, value.Acd_time_window);
			AddParameter(_cmd, CarrierRouteRow.acd_target_PropName, value.Acd_target);
			AddParameter(_cmd, CarrierRouteRow.next_ep_PropName, value.Next_ep);

			value.Carrier_route_id = (int)_cmd.ExecuteScalar();
		}

		public CarrierRouteRow GetByCarrierAcctIdRouteId(short pCarrierAcctId, int pBaseRouteId) {
			var _where =
				CarrierRouteRow.carrier_acct_id_DbName + " = " +
				Database.CreateSqlParameterName(CarrierRouteRow.carrier_acct_id_PropName) +
				" AND " +
				CarrierRouteRow.route_id_DbName + "=" +
				Database.CreateSqlParameterName(CarrierRouteRow.route_id_PropName);

			IDbCommand _cmd = CreateGetCommand(_where, null);
			AddParameter(_cmd, CarrierRouteRow.carrier_acct_id_PropName, pCarrierAcctId);
			AddParameter(_cmd, CarrierRouteRow.route_id_PropName, pBaseRouteId);

			using (IDataReader reader = Database.ExecuteReader(_cmd)) {
				CarrierRouteRow[] tempArray = MapRecords(reader);
				return 0 == tempArray.Length ? null : tempArray[0];
			}
		}

		public CarrierRouteRow[] GetByCarrierAcctIdCountryId(short pCarrierAcctId, int pCountryId) {
			/*
			SELECT * 
			FROM dbo.CarrierRoute
			WHERE carrier_acct_id = 700 
			AND route_id IN (
				SELECT route_id FROM Route 
				WHERE country_id = 202
			) 
			 */
			string _where = "[CarrierRoute].[" + CarrierRouteRow.carrier_acct_id_DbName + "] = " +
				Database.CreateSqlParameterName(CarrierRouteRow.carrier_acct_id_PropName) +
				" AND " +
				"[CarrierRoute].[" + CarrierRouteRow.route_id_DbName + "] IN (" +
				"     SELECT [Route].[" + RouteRow.route_id_DbName + "] FROM [Route] " +
				"     WHERE " + "[Route].[" + RouteRow.country_id_DbName + "] = " + pCountryId +
				")";

			IDbCommand _cmd = CreateGetCommand(_where, null);
			AddParameter(_cmd, CarrierRouteRow.carrier_acct_id_PropName, pCarrierAcctId);

			using (IDataReader reader = Database.ExecuteReader(_cmd)) {
				return MapRecords(reader);
			}
		}

		////NOTE: this sql needs to return all carrier routes that support given dial code and all subcodes down to ccode
		////example: cust Dial Code 5234 >> look for all carrier routes that cover: 5234, 523, 52 (asuming ccode is 52)
		//public CarrierRouteRow[] GetActive(int pBaseRouteId, string pDialedNumber) {
		//  if (string.IsNullOrEmpty(pDialedNumber)) {//dial code should never be less than 7
		//    throw new ArgumentException("Invalid Dialed Number: " + pDialedNumber);
		//  }
		//  #region sql
		//  /*
		//    SELECT * FROM CarrierRoute 
		//    WHERE 
		//    status = 1
		//    AND 
		//    route_id IN (
		//      SELECT route_id FROM  DialCode
		//      WHERE '544573476758' 
		//      LIKE CAST(dial_code AS varchar) + '%'
		//    )
		//    AND
		//    route_id = pRouteId
		//  */
		//  #endregion sql

		//  var _where =
		//    CarrierRouteRow_Base.status_DbName + " = " + (byte) Status.Active + " " +
		//    "AND " +
		//    CarrierRouteRow_Base.route_id_DbName + " IN ( " +
		//    "	SELECT " + DialCodeRow_Base.route_id_DbName + " FROM  DialCode " +
		//    "	WHERE " + Database.CreateSqlParameterName("DialedNumber") +
		//    " LIKE CAST(" + DialCodeRow_Base.dial_code_DbName + " AS varchar) + '%' " +
		//    ") " +
		//    "AND " +
		//    CarrierRouteRow_Base.route_id_DbName + " = " + Database.CreateSqlParameterName("RouteId") + " ";

		//  var _cmd = CreateGetCommand(_where, null);
		//  Database.AddParameter(_cmd, "DialedNumber", DbType.AnsiString, pDialedNumber);
		//  Database.AddParameter(_cmd, "RouteId", DbType.Int32, pBaseRouteId);
		//  using (var _reader = Database.ExecuteReader(_cmd)) {
		//    return MapRecords(_reader);
		//  }
		//}

		//NOTE: this sql needs to return all carrier routes that support given dial code and all subcodes down to ccode
		//example: cust Dial Code 5234 >> look for all carrier routes that cover: 5234, 523, 52 (asuming ccode is 52)
		public CarrierRouteRow[] GetActive(string pDialedNumber) {
			if (string.IsNullOrEmpty(pDialedNumber)) {//dial code should never be less than 7
				throw new ArgumentException("Invalid Dialed Number: " + pDialedNumber);
			}
			#region sql
			/*
			SELECT DISTINCT CarrierRoute.* FROM CarrierAcctEPMap 
					INNER JOIN CarrierRoute ON CarrierAcctEPMap.carrier_route_id = CarrierRoute.carrier_route_id 
						INNER JOIN EndPoint ON CarrierAcctEPMap.end_point_id = EndPoint.end_point_id
						INNER JOIN CarrierAcct ON CarrierAcctEPMap.carrier_acct_id = CarrierRoute.carrier_acct_id 
			WHERE   EndPoint.status = 1
			AND 	CarrierRoute.status = 1
			AND		CarrierAcct.status = 1
			AND 	route_id IN 
					(
						SELECT route_id FROM  DialCode
						WHERE '5535520312389' LIKE CAST(dial_code AS varchar) + '%'
					)
      */
			#endregion sql

			var _sqlStr = "SELECT CarrierRoute.* FROM CarrierRoute " +
				"INNER JOIN CarrierAcctEPMap ON " +
				"CarrierAcctEPMap." + CarrierAcctEPMapRow_Base.carrier_route_id_DbName + " = " + "CarrierRoute." + CarrierRouteRow_Base.carrier_route_id_DbName + " " +
				"INNER JOIN EndPoint ON " +
				"CarrierAcctEPMap." + CarrierAcctEPMapRow_Base.end_point_id_DbName + " = " + "EndPoint." + EndPointRow_Base.end_point_id_DbName + " " +
				"INNER JOIN CarrierAcct ON " +
				"CarrierAcct." + CarrierAcctRow_Base.carrier_acct_id_DbName + " = " + "CarrierRoute." + CarrierRouteRow_Base.carrier_acct_id_DbName + " " +
				"WHERE " +
				"EndPoint." + EndPointRow_Base.status_DbName + " = " + (byte) Status.Active + " " +
				"AND " +
				"CarrierRoute." + CarrierRouteRow_Base.status_DbName + " = " + (byte) Status.Active + " " +
				"AND " +
				"CarrierAcct." + CarrierAcctRow_Base.status_DbName + " = " + (byte) Status.Active + " " +
				"AND " +
				CarrierRouteRow_Base.route_id_DbName + " IN " + 
				"( " +
				"	SELECT " + DialCodeRow_Base.route_id_DbName + " FROM  DialCode " +
				"	WHERE " + Database.CreateSqlParameterName("DialedNumber") +
				" LIKE CAST(" + DialCodeRow_Base.dial_code_DbName + " AS varchar) + '%' " +
				") ";

			var _cmd = Database.CreateCommand(_sqlStr);
			Database.AddParameter(_cmd, "DialedNumber", DbType.AnsiString, pDialedNumber);
			using (var _reader = Database.ExecuteReader(_cmd)) {
				return MapRecords(_reader);
			}
		}

		public CarrierRouteRow GetByRoutingPlanIdRouteIdPriority(int pRoutingPlanId, int pRouteId, byte pPriority) {
			/*
			SELECT CarrierRoute.*
			FROM CarrierRoute INNER JOIN TerminationChoice ON 
					CarrierRoute.carrier_route_id = TerminationChoice.carrier_route_id
			WHERE (TerminationChoice.routing_plan_id = 1) AND 
					(TerminationChoice.route_id = 1) AND 
					(TerminationChoice.priority = 1)
			*/
			var _sqlStr = "SELECT CarrierRoute.* FROM CarrierRoute INNER JOIN TerminationChoice ON " +
				" CarrierRoute." + CarrierRouteRow.carrier_route_id_DbName + " = TerminationChoice." + TerminationChoiceRow.carrier_route_id_DbName + 
				" WHERE " +
				" TerminationChoice." + TerminationChoiceRow.routing_plan_id_DbName + " = " + Database.CreateSqlParameterName(TerminationChoiceRow.routing_plan_id_PropName) + " AND " +
				" TerminationChoice." + TerminationChoiceRow.route_id_DbName + " = " + Database.CreateSqlParameterName(TerminationChoiceRow.route_id_PropName) + " AND " +
				" TerminationChoice." + TerminationChoiceRow.priority_DbName + " = " + Database.CreateSqlParameterName(TerminationChoiceRow.priority_PropName);

			var _cmd = Database.CreateCommand(_sqlStr);
			Database.AddParameter(_cmd, TerminationChoiceRow.routing_plan_id_PropName, DbType.Int32, pRoutingPlanId);
			Database.AddParameter(_cmd, TerminationChoiceRow.route_id_PropName, DbType.Int32, pRouteId);
			Database.AddParameter(_cmd, TerminationChoiceRow.priority_PropName, DbType.Int16, pPriority);

			using (var _reader = Database.ExecuteReader(_cmd)) {
				var _tempArray = MapRecords(_reader);
				return 0 == _tempArray.Length ? null : _tempArray[0];
			}
		}

  	public CarrierRouteRow[] GetByRoutingPlanIdRouteId(int pRoutingPlanId, int pRouteId) {
			/*
			SELECT CarrierRoute.*
			FROM CarrierRoute INNER JOIN TerminationChoice ON 
					CarrierRoute.carrier_route_id = TerminationChoice.carrier_route_id
			WHERE (TerminationChoice.routing_plan_id = 1) AND 
					(TerminationChoice.route_id = 1)
			*/
  		var _sqlStr = "SELECT CarrierRoute.* FROM CarrierRoute INNER JOIN TerminationChoice ON " +
  		                 " CarrierRoute." + CarrierRouteRow.carrier_route_id_DbName + " = TerminationChoice." + TerminationChoiceRow.carrier_route_id_DbName +
  		                 " WHERE " +
  		                 " TerminationChoice." + TerminationChoiceRow.routing_plan_id_DbName + " = " + Database.CreateSqlParameterName(TerminationChoiceRow.routing_plan_id_PropName) + " AND " +
  		                 " TerminationChoice." + TerminationChoiceRow.route_id_DbName + " = " + Database.CreateSqlParameterName(TerminationChoiceRow.route_id_PropName);
				
			var _cmd = Database.CreateCommand(_sqlStr);
			Database.AddParameter(_cmd, TerminationChoiceRow.routing_plan_id_PropName, DbType.Int32, pRoutingPlanId);
			Database.AddParameter(_cmd, TerminationChoiceRow.route_id_PropName, DbType.Int32, pRouteId);

			using (var _reader = Database.ExecuteReader(_cmd)) {
				return MapRecords(_reader);
			}
		}
	}// End of CarrierRouteCollection class
} // End of namespace

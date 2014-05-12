// <fileinfo name="CarrierAcctEPMapCollection.cs">
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
	/// Represents the <c>CarrierAcctEPMap</c> table.
	/// </summary>
	public class CarrierAcctEPMapCollection : CarrierAcctEPMapCollection_Base {
		/// <summary>
		/// Initializes a new instance of the <see cref="CarrierAcctEPMapCollection"/> class.
		/// </summary>
		/// <param name="db">The database object.</param>
		internal CarrierAcctEPMapCollection(Rbr_Db db)
			: base(db) {
			// EMPTY
		}

		public static CarrierAcctEPMapRow Parse(System.Data.DataRow row){
			return new CarrierAcctEPMapCollection(null).MapRow(row);
		}

		public override void Insert(CarrierAcctEPMapRow value) {
//			string _id_param = base.Database.CreateSqlParameterName(CarrierAcctEPMapRow.carrier_acct_EP_map_id_PropName);
//			string _priority_param = base.Database.CreateSqlParameterName(CarrierAcctEPMapRow.priority_PropName);
			string _sqlStr = 
//				"DECLARE " + _id_param + " int " + 
//				"DECLARE " + _priority_param + " tinyint " + 

				"SET " + Database.CreateSqlParameterName(CarrierAcctEPMapRow.carrier_acct_EP_map_id_PropName) + 
				" = COALESCE((SELECT MAX(" + CarrierAcctEPMapRow.carrier_acct_EP_map_id_DbName + ") FROM [dbo].[CarrierAcctEPMap]) + 1, 1) " + 
				
				"SET " + Database.CreateSqlParameterName(CarrierAcctEPMapRow.priority_PropName) + 
				" = COALESCE((SELECT MAX(" + CarrierAcctEPMapRow.priority_DbName + ") FROM [dbo].[CarrierAcctEPMap] " + 
				"WHERE [" + CarrierAcctEPMapRow.carrier_route_id_DbName + "]=" + 
				base.Database.CreateSqlParameterName(CarrierAcctEPMapRow.carrier_route_id_PropName) + 
				" AND " + 
				" [" + CarrierAcctEPMapRow.end_point_id_DbName + "]=" + 
				base.Database.CreateSqlParameterName(CarrierAcctEPMapRow.end_point_id_PropName) + 
				") + 1, 1) " + 

				"INSERT INTO [dbo].[CarrierAcctEPMap] (" +
				"[" + CarrierAcctEPMapRow.carrier_acct_EP_map_id_DbName + "], " +
				"[" + CarrierAcctEPMapRow.carrier_acct_id_DbName + "], " +
				"[" + CarrierAcctEPMapRow.end_point_id_DbName + "], " +
				"[" + CarrierAcctEPMapRow.priority_DbName + "], " +
				"[" + CarrierAcctEPMapRow.carrier_route_id_DbName + "]" +
				") VALUES (" +
				Database.CreateSqlParameterName(CarrierAcctEPMapRow.carrier_acct_EP_map_id_PropName) + ", " +
				Database.CreateSqlParameterName(CarrierAcctEPMapRow.carrier_acct_id_PropName) + ", " +
				Database.CreateSqlParameterName(CarrierAcctEPMapRow.end_point_id_PropName) + ", " +
				Database.CreateSqlParameterName(CarrierAcctEPMapRow.priority_PropName) + ", " +
				Database.CreateSqlParameterName(CarrierAcctEPMapRow.carrier_route_id_PropName) + ")";


			IDbCommand _cmd = Database.CreateCommand(_sqlStr);
//			AddParameter(_cmd, CarrierAcctEPMapRow.carrier_acct_EP_map_id_PropName, value.Carrier_acct_EP_map_id);
			IDbDataParameter _id_param = _cmd.CreateParameter();
			_id_param.ParameterName = Database.CreateSqlParameterName(CarrierAcctEPMapRow.carrier_acct_EP_map_id_PropName);
			_id_param.DbType = DbType.Int32;
			_id_param.Direction = ParameterDirection.Output;
			_cmd.Parameters.Add(_id_param);

//			AddParameter(_cmd, CarrierAcctEPMapRow.priority_PropName, value.Priority);
			IDbDataParameter _priority_param = _cmd.CreateParameter();
			_priority_param.ParameterName = Database.CreateSqlParameterName(CarrierAcctEPMapRow.priority_PropName);
			_priority_param.DbType = DbType.Byte;
			_priority_param.Direction = ParameterDirection.Output;
			_cmd.Parameters.Add(_priority_param);
			
			AddParameter(_cmd, CarrierAcctEPMapRow.carrier_acct_id_PropName, value.Carrier_acct_id);
			AddParameter(_cmd, CarrierAcctEPMapRow.end_point_id_PropName, value.End_point_id);
			AddParameter(_cmd, CarrierAcctEPMapRow.carrier_route_id_PropName, value.Carrier_route_id);

			_cmd.ExecuteNonQuery();
			value.Carrier_acct_EP_map_id = (int) _id_param.Value;
			value.Priority = (byte) _priority_param.Value;

			value.Carrier_acct_EP_map_id = (int) ((System.Data.SqlClient.SqlParameter) _cmd.Parameters[_id_param.ParameterName]).Value;
			value.Priority = (byte) ((System.Data.SqlClient.SqlParameter) _cmd.Parameters[_priority_param.ParameterName]).Value;
		}

		public CarrierAcctEPMapRow[] GetByCarrierAcctIDEndPointID(short pCarrierAcctID, short pEndPointID){
			string sqlStr = "SELECT * FROM CarrierAcctEPMap WHERE " + 
				"[" + CarrierAcctEPMapRow.carrier_acct_id_DbName + "]=" + base.Database.CreateSqlParameterName(CarrierAcctEPMapRow.carrier_acct_id_PropName) + 
				" AND " + 
				"[" + CarrierAcctEPMapRow.end_point_id_DbName + "]=" + base.Database.CreateSqlParameterName(CarrierAcctEPMapRow.end_point_id_PropName);

			IDbCommand _cmd = base.Database.CreateCommand(sqlStr);
			AddParameter(_cmd, CarrierAcctEPMapRow.carrier_acct_id_PropName, pCarrierAcctID);
			AddParameter(_cmd, CarrierAcctEPMapRow.end_point_id_PropName, pEndPointID);
			using (IDataReader _reader = _cmd.ExecuteReader()) {
				return MapRecords(_reader);
			}
		}

		//TODO: !!! this potentially could return > 1 !!!
		public CarrierAcctEPMapRow GetByCarrierAcctIDEndPointIDCarrierRouteID(short pCarrierAcctID, short pEndPointID, int pCarrierRouteID){
			string sqlStr = "SELECT * FROM CarrierAcctEPMap WHERE " + 
				"[" + CarrierAcctEPMapRow.carrier_acct_id_DbName + "]=" + base.Database.CreateSqlParameterName(CarrierAcctEPMapRow.carrier_acct_id_PropName) + 
				" AND " + 
				"[" + CarrierAcctEPMapRow.end_point_id_DbName + "]=" + base.Database.CreateSqlParameterName(CarrierAcctEPMapRow.end_point_id_PropName) + 
				" AND " + 
				"[" + CarrierAcctEPMapRow.carrier_route_id_DbName + "]=" + base.Database.CreateSqlParameterName(CarrierAcctEPMapRow.carrier_route_id_PropName);

			IDbCommand _cmd = base.Database.CreateCommand(sqlStr);
			AddParameter(_cmd, CarrierAcctEPMapRow.carrier_acct_id_PropName, pCarrierAcctID);
			AddParameter(_cmd, CarrierAcctEPMapRow.end_point_id_PropName, pEndPointID);
			AddParameter(_cmd, CarrierAcctEPMapRow.carrier_route_id_PropName, pCarrierRouteID);
			using (IDataReader _reader = _cmd.ExecuteReader()) {
				CarrierAcctEPMapRow[] _tempArray = MapRecords(_reader);
				return 0 == _tempArray.Length ? null : _tempArray[0];
			}
		}

		public int GetCountByCarrierAcctIdEndPointId(short pCarrierAcctId, short pEndPointId){
			string _sqlStr = "SELECT COUNT(*) FROM CarrierAcctEPMap WHERE " + 
				"[" + CarrierAcctEPMapRow.carrier_acct_id_DbName + "]=" + base.Database.CreateSqlParameterName(CarrierAcctEPMapRow.carrier_acct_id_PropName) + 
				" AND " + 
				"[" + CarrierAcctEPMapRow.end_point_id_DbName + "]=" + base.Database.CreateSqlParameterName(CarrierAcctEPMapRow.end_point_id_PropName);

			IDbCommand _cmd = base.Database.CreateCommand(_sqlStr);
			AddParameter(_cmd, CarrierAcctEPMapRow.carrier_acct_id_PropName, pCarrierAcctId);
			AddParameter(_cmd, CarrierAcctEPMapRow.end_point_id_PropName, pEndPointId);
			int _res = (int) _cmd.ExecuteScalar();
			return _res;
		}

    //TODO: NEW DAL
    ///// <summary>
    ///// Will filter by Status.Active on [Carrier]Route, Endpoint and Carrier
    ///// </summary>
    ///// <param name="pCarrierAcctId"></param>
    ///// <param name="pCarrierRouteId"></param>
    ///// <returns></returns>
    //public int GetActiveCountByCarrierAcctIdCarrierRouteId(short pCarrierAcctId, int pCarrierRouteId) {
    //  /*
    //  SELECT COUNT(*)
    //  FROM  CarrierAcctEPMap INNER JOIN Route ON 
    //  CarrierAcctEPMap.carrier_route_id = Route.route_id 
    //  INNER JOIN EndPoint ON 
    //  CarrierAcctEPMap.end_point_id = EndPoint.end_point_id 
    //  INNER JOIN CarrierAcct ON 
    //  CarrierAcctEPMap.carrier_acct_id = CarrierAcct.carrier_acct_id
    //  WHERE 
    //  (Route.status = 1) 
    //  AND 
    //  (EndPoint.status = 1) 
    //  AND 
    //  (CarrierAcct.status = 1)
    //  AND 
    //  (CarrierAcctEPMap.carrier_acct_id = 1000) 
    //  AND 
    //  (CarrierAcctEPMap.carrier_route_id = 2000) 
    //  */

    //  string _sqlStr = "SELECT COUNT(*) " + 
    //  "FROM  CarrierAcctEPMap INNER JOIN ServiceRoute ON " +
    //  "CarrierAcctEPMap.carrier_route_id = ServiceRoute.service_route_id " + 
    //  "INNER JOIN EndPoint ON " + 
    //  "CarrierAcctEPMap.end_point_id = EndPoint.end_point_id " + 
    //  "INNER JOIN CarrierAcct ON " + 
    //  "CarrierAcctEPMap.carrier_acct_id = CarrierAcct.carrier_acct_id " + 
    //  "WHERE " +
    //  "(ServiceRoute." + ServiceRouteRow.status_DbName + " = " + (byte) Status.Active + ") " + 
    //  "AND " + 
    //  "(EndPoint." + EndPointRow.status_DbName + " = " + (byte) Status.Active + ") " + 
    //  "AND " + 
    //  "(CarrierAcct." + CarrierAcctRow.status_DbName + " = " + (byte) Status.Active + ") " + 
    //  "AND " + 
    //  "(CarrierAcctEPMap." + CarrierAcctEPMapRow.carrier_acct_id_DbName + " = " + Database.CreateSqlParameterName(CarrierAcctEPMapRow.carrier_acct_id_PropName) + ") " + 
    //  "AND " + 
    //  "(CarrierAcctEPMap." + CarrierAcctEPMapRow.carrier_route_id_DbName + " = " + Database.CreateSqlParameterName(CarrierAcctEPMapRow.carrier_route_id_PropName) + ") ";


    //  IDbCommand _cmd = base.Database.CreateCommand(_sqlStr);
    //  AddParameter(_cmd, CarrierAcctEPMapRow.carrier_acct_id_PropName, pCarrierAcctId);
    //  AddParameter(_cmd, CarrierAcctEPMapRow.carrier_route_id_PropName, pCarrierRouteId);
    //  int _res = (int) _cmd.ExecuteScalar();
    //  return _res;
    //}

    public int GetActiveCountByCarrierRouteId(int pCarrierRouteId) {
      /*
      SELECT COUNT(CarrierAcctEPMap.carrier_acct_EP_map_id) 
      FROM  CarrierAcctEPMap INNER JOIN CarrierRoute ON 
      CarrierAcctEPMap.carrier_route_id = CarrierRoute.carrier_route_id
      INNER JOIN EndPoint ON 
      CarrierAcctEPMap.end_point_id = EndPoint.end_point_id
      INNER JOIN CarrierAcct ON 
      CarrierAcctEPMap.carrier_acct_id = CarrierAcct.carrier_acct_id
      WHERE 
      (CarrierRoute.status = 1) --Active
      AND 
      (EndPoint.status = 1) --Active
      AND 
      (CarrierAcct.status = 1) --Active
      AND 
      (CarrierAcctEPMap.carrier_route_id = 1000)
      */

      string _sqlStr = "SELECT COUNT(CarrierAcctEPMap." + CarrierAcctEPMapRow.carrier_acct_EP_map_id_DbName + ") " +
      "FROM  CarrierAcctEPMap INNER JOIN CarrierRoute ON " +
      "CarrierAcctEPMap." + CarrierAcctEPMapRow.carrier_route_id_DbName + " = CarrierRoute." + CarrierRouteRow.carrier_route_id_DbName + " " +
      "INNER JOIN EndPoint ON " +
      "CarrierAcctEPMap." + CarrierAcctEPMapRow.end_point_id_DbName + " = EndPoint." + EndPointRow.end_point_id_DbName + " " +
      "INNER JOIN CarrierAcct ON " +
      "CarrierAcctEPMap." + CarrierAcctEPMapRow.carrier_acct_id_DbName + " = CarrierAcct." + CarrierAcctRow.carrier_acct_id_DbName + " " +
      "WHERE " +
      "(CarrierRoute." + CarrierRouteRow.status_DbName + " = " + (byte) Status.Active + ") " +
      "AND " +
      "(EndPoint." + EndPointRow.status_DbName + " = " + (byte) Status.Active + ") " +
      "AND " +
      "(CarrierAcct." + CarrierAcctRow.status_DbName + " = " + (byte) Status.Active + ") " +
      "AND " +
      "(CarrierAcctEPMap." + CarrierAcctEPMapRow.carrier_route_id_DbName + " = " + Database.CreateSqlParameterName(CarrierAcctEPMapRow.carrier_route_id_PropName) + ") ";


      IDbCommand _cmd = base.Database.CreateCommand(_sqlStr);
      AddParameter(_cmd, CarrierAcctEPMapRow.carrier_route_id_PropName, pCarrierRouteId);
      int _res = (int) _cmd.ExecuteScalar();
      return _res;
    }

		public int GetCountByEndPointId(short pEndPointId){
			string _sqlStr = "SELECT COUNT(*) FROM CarrierAcctEPMap WHERE " + 
				"[" + CarrierAcctEPMapRow.end_point_id_DbName + "]=" + base.Database.CreateSqlParameterName(CarrierAcctEPMapRow.end_point_id_PropName);

			IDbCommand _cmd = base.Database.CreateCommand(_sqlStr);
			AddParameter(_cmd, CarrierAcctEPMapRow.end_point_id_PropName, pEndPointId);
			int _res = (int) _cmd.ExecuteScalar();
			return _res;
		}

		public CarrierAcctEPMapRow[] GetWhereEndpointIsUsedLast(short pEndpointId) {
			/*
			SELECT CarrierAcctEPMap.* FROM CarrierAcctEPMap 
			WHERE carrier_route_id 
			NOT IN (
					SELECT CarrierAcctEPMap.carrier_route_id 
					FROM CarrierAcctEPMap 
					WHERE end_point_id <> 5061
			    AND end_point_id 
					IN (SELECT 
					    end_point_id 
							FROM EndPoint WHERE 
							status = 1
							)
			)
			AND 
			end_point_id = 5061
			*/
			string _sqlStr = "SELECT CarrierAcctEPMap.* FROM CarrierAcctEPMap " + 
			"WHERE " + CarrierAcctEPMapRow.carrier_route_id_DbName + " " + 
			"NOT IN ( " + 
			"    SELECT CarrierAcctEPMap." + CarrierAcctEPMapRow.carrier_route_id_DbName + " " + 
			"    FROM CarrierAcctEPMap " + 
			"    WHERE " + CarrierAcctEPMapRow.end_point_id_DbName + " <> " + Database.CreateSqlParameterName(CarrierAcctEPMapRow.end_point_id_PropName) + 
			"    AND " + CarrierAcctEPMapRow.end_point_id_DbName + " " + 
			"    IN (SELECT " + 
			"        " + EndPointRow.end_point_id_DbName + " " + 
			"        FROM EndPoint WHERE " + 
			"        " + EndPointRow.status_DbName + " = " + (byte) Status.Active + " " + 
			"       ) " + 
			") " + 
			"AND " + 
			CarrierAcctEPMapRow.end_point_id_DbName + " = " + Database.CreateSqlParameterName(CarrierAcctEPMapRow.end_point_id_PropName);

			IDbCommand _cmd = base.Database.CreateCommand(_sqlStr);
			AddParameter(_cmd, CarrierAcctEPMapRow.end_point_id_PropName, pEndpointId);
			using (IDataReader _reader = _cmd.ExecuteReader()) {
				return MapRecords(_reader);
			}
		}
  } // End of CarrierAcctEPMapCollection class
} // End of namespace

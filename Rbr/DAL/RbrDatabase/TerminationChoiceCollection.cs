// <fileinfo name="TerminationChoiceCollection.cs">
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
using System.Text;
using Timok.Rbr.DAL.RbrDatabase.Base;
using Timok.Rbr.Core.Config;

namespace Timok.Rbr.DAL.RbrDatabase {
	/// <summary>
	/// Represents the <c>TerminationChoice</c> table.
	/// </summary>
	public class TerminationChoiceCollection : TerminationChoiceCollection_Base {
		/// <summary>
		/// Initializes a new instance of the <see cref="TerminationChoiceCollection"/> class.
		/// </summary>
		/// <param name="db">The database object.</param>
		internal TerminationChoiceCollection(Rbr_Db db)
			: base(db) {
			// EMPTY
		}

		public static TerminationChoiceRow Parse(System.Data.DataRow row) {
			return new TerminationChoiceCollection(null).MapRow(row);
		}

    public TerminationChoiceRow[] GetByCarrierAcctId(short pCarrierAcctId) {
      #region sql
      /*
      SELECT TerminationChoice.*
      FROM TerminationChoice 
      WHERE carrier_route_id IN 
      (
        SELECT carrier_route_id FROM CarrierRoute 
        WHERE carrier_acct_id = 7000
      ) 
      */
      #endregion sql

      string _sql = "SELECT TerminationChoice.* FROM TerminationChoice " +
      "WHERE " + TerminationChoiceRow.carrier_route_id_DbName + " IN " +
      "( " +
      "	SELECT " + CarrierRouteRow.carrier_route_id_DbName + " FROM CarrierRoute " +
      "	WHERE " + CarrierRouteRow.carrier_acct_id_DbName + " = " + Database.CreateSqlParameterName(CarrierRouteRow.carrier_acct_id_PropName) +
      ")";

      IDbCommand _cmd = Database.CreateCommand(_sql);
      Database.AddParameter(_cmd, CarrierRouteRow.carrier_acct_id_PropName, DbType.Int16, pCarrierAcctId);
      using (IDataReader _reader = base.Database.ExecuteReader(_cmd)) {
        return MapRecords(_reader);
      }
    }

		public override bool Update(TerminationChoiceRow value) {
			if (value != null)
				throw new NotSupportedException();
			return false;
		}

		public bool UpdateInternal(TerminationChoiceRow value) {
			return base.Update(value);
		}

    public override void Insert(TerminationChoiceRow value) {
      //			string _id_param = base.Database.CreateSqlParameterName(TerminationChoiceRow.termination_choice_id_PropName);
      //			string _priority_param = base.Database.CreateSqlParameterName(TerminationChoiceRow.priority_PropName);
      string _sqlStr =
        //				"DECLARE " + _id_param + " int " + 
        //				"DECLARE " + _priority_param + " tinyint " + 

        "SET " + Database.CreateSqlParameterName(TerminationChoiceRow.termination_choice_id_PropName) +
        " = COALESCE((SELECT MAX(" + TerminationChoiceRow.termination_choice_id_DbName + ") FROM [dbo].[TerminationChoice]) + 1, 1) " +

        "SET " + Database.CreateSqlParameterName(TerminationChoiceRow.priority_PropName) +
        " = COALESCE((SELECT MAX(" + TerminationChoiceRow.priority_DbName + ") FROM [dbo].[TerminationChoice] " +
        "WHERE [" + TerminationChoiceRow.routing_plan_id_DbName + "]=" +
        base.Database.CreateSqlParameterName(TerminationChoiceRow.routing_plan_id_PropName) +
        " AND [" + TerminationChoiceRow.route_id_DbName + "]=" +
        base.Database.CreateSqlParameterName(TerminationChoiceRow.route_id_PropName) +
        ") + 1, 1) " +

        "INSERT INTO [dbo].[TerminationChoice] (" +
        "[" + TerminationChoiceRow.termination_choice_id_DbName + "], " +
        "[" + TerminationChoiceRow.routing_plan_id_DbName + "], " +
        "[" + TerminationChoiceRow.route_id_DbName + "], " +
        "[" + TerminationChoiceRow.priority_DbName + "], " +
        "[" + TerminationChoiceRow.carrier_route_id_DbName + "], " +
        "[" + TerminationChoiceRow.version_DbName + "] " +
        ") VALUES (" +
        Database.CreateSqlParameterName(TerminationChoiceRow.termination_choice_id_PropName) + ", " +
        Database.CreateSqlParameterName(TerminationChoiceRow.routing_plan_id_PropName) + ", " +
        Database.CreateSqlParameterName(TerminationChoiceRow.route_id_PropName) + ", " +
        Database.CreateSqlParameterName(TerminationChoiceRow.priority_PropName) + ", " +
        Database.CreateSqlParameterName(TerminationChoiceRow.carrier_route_id_PropName) + ", " +
        Database.CreateSqlParameterName(TerminationChoiceRow.version_PropName) + ") ";

      IDbCommand _cmd = Database.CreateCommand(_sqlStr);
      //AddParameter(cmd, "Termination_choice_id", value.Termination_choice_id);
      IDbDataParameter _id_param = _cmd.CreateParameter();
      _id_param.ParameterName = Database.CreateSqlParameterName(TerminationChoiceRow.termination_choice_id_PropName);
      _id_param.DbType = DbType.Int32;
      _id_param.Direction = ParameterDirection.Output;
      _cmd.Parameters.Add(_id_param);

      //AddParameter(cmd, "Priority", value.Priority);
      IDbDataParameter _priority_param = _cmd.CreateParameter();
      _priority_param.ParameterName = Database.CreateSqlParameterName(TerminationChoiceRow.priority_PropName);
      _priority_param.DbType = DbType.Byte;
      _priority_param.Direction = ParameterDirection.Output;
      _cmd.Parameters.Add(_priority_param);

      AddParameter(_cmd, TerminationChoiceRow.routing_plan_id_PropName, value.Routing_plan_id);
      AddParameter(_cmd, TerminationChoiceRow.route_id_PropName, value.Route_id);
      AddParameter(_cmd, TerminationChoiceRow.carrier_route_id_PropName, value.Carrier_route_id);
      AddParameter(_cmd, TerminationChoiceRow.version_PropName, value.Version);


      _cmd.ExecuteNonQuery();

      //object _res = _cmd.ExecuteScalar();
      value.Termination_choice_id = (int) _id_param.Value;
      value.Priority = (byte) _priority_param.Value;

      value.Termination_choice_id = (int) ((System.Data.SqlClient.SqlParameter) _cmd.Parameters[_id_param.ParameterName]).Value;
      value.Priority = (byte) ((System.Data.SqlClient.SqlParameter) _cmd.Parameters[_priority_param.ParameterName]).Value;

    }

    public TerminationChoiceRow GetByRoutingPlanIdRouteIdPriority(int pRoutingPlanId, int pRouteId, byte pPriority) {
      var _whereSql = "";
      _whereSql += "[" + TerminationChoiceRow_Base.routing_plan_id_DbName + "]=" +
        Database.CreateSqlParameterName(TerminationChoiceRow_Base.routing_plan_id_PropName) + " AND ";
      _whereSql += "[" + TerminationChoiceRow_Base.route_id_DbName + "]=" +
        Database.CreateSqlParameterName(TerminationChoiceRow_Base.route_id_PropName) + " AND ";
      _whereSql += "[" + TerminationChoiceRow_Base.priority_DbName + "]=" +
        Database.CreateSqlParameterName(TerminationChoiceRow_Base.priority_PropName);

      var _cmd = CreateGetCommand(_whereSql, null);
      AddParameter(_cmd, TerminationChoiceRow_Base.routing_plan_id_PropName, pRoutingPlanId);
      AddParameter(_cmd, TerminationChoiceRow_Base.route_id_PropName, pRouteId);
      AddParameter(_cmd, TerminationChoiceRow_Base.priority_PropName, pPriority);

      using (var _reader = Database.ExecuteReader(_cmd)) {
        var tempArray = MapRecords(_reader);
        return 0 == tempArray.Length ? null : tempArray[0];
      }
    }

    public TerminationChoiceRow[] GetByRoutingPlanIdRouteId_OrderByPriority(int pRoutingPlanId, int pRouteId) {
      var _where =
        TerminationChoiceRow_Base.routing_plan_id_DbName + " = " + 
					Database.CreateSqlParameterName(TerminationChoiceRow_Base.routing_plan_id_PropName) +
        " AND " +
        TerminationChoiceRow_Base.route_id_DbName + " = " + 
					Database.CreateSqlParameterName(TerminationChoiceRow_Base.route_id_PropName);

      var _cmd = CreateGetCommand(_where, TerminationChoiceRow_Base.priority_DbName);
      AddParameter(_cmd, TerminationChoiceRow_Base.routing_plan_id_PropName, pRoutingPlanId);
      AddParameter(_cmd, TerminationChoiceRow_Base.route_id_PropName, pRouteId);
      
      using (var _reader = Database.ExecuteReader(_cmd)) {
        return MapRecords(_reader);
      }
    }

    public int GetCountByRoutingPlanIdRouteId(int pRoutingPlanId, int pRouteId) {
      var _sql =
        "SELECT COUNT(*) FROM TerminationChoice WHERE " +
        TerminationChoiceRow_Base.routing_plan_id_DbName + " = " + Database.CreateSqlParameterName(TerminationChoiceRow_Base.routing_plan_id_PropName) +
        " AND " +
        TerminationChoiceRow_Base.route_id_DbName + " = " + Database.CreateSqlParameterName(TerminationChoiceRow_Base.route_id_PropName);

      var cmd = Database.CreateCommand(_sql);
      AddParameter(cmd, TerminationChoiceRow_Base.routing_plan_id_PropName, pRoutingPlanId);
      AddParameter(cmd, TerminationChoiceRow_Base.route_id_PropName, pRouteId);
      return (int) cmd.ExecuteScalar();
    }

    public int GetCountByCarrierRouteId(int pCarrierRouteId) {
      var _sqlStr = "SELECT COUNT(*) FROM TerminationChoice WHERE " +
        TerminationChoiceRow_Base.carrier_route_id_DbName + "=" +
        Database.CreateSqlParameterName(TerminationChoiceRow_Base.carrier_route_id_PropName);

      var cmd = Database.CreateCommand(_sqlStr);
      AddParameter(cmd, TerminationChoiceRow_Base.carrier_route_id_PropName, pCarrierRouteId);

      return (int) cmd.ExecuteScalar();
    }

    //TODO: NEW DAL
    //public TerminationChoiceRow[] GetByCustomer_route_id_Carrier_route_id(int customer_route_id, int carrier_route_id) {
    //  using(IDataReader reader = base.Database.ExecuteReader(CreateGetByCustomer_route_id_Carrier_route_idCommand(customer_route_id, carrier_route_id))) {
    //    return MapRecords(reader);
    //  }
    //}

    //TODO: NEW DAL
    //protected IDbCommand CreateGetByCustomer_route_id_Carrier_route_idCommand(int customer_route_id, int carrier_route_id) {
    //  string whereSql = "";
    //  whereSql += "[" + TerminationChoiceRow.customer_route_id_DbName + "]=" + 
    //    base.Database.CreateSqlParameterName(TerminationChoiceRow.customer_route_id_PropName) + " AND ";
    //  whereSql += "[" + TerminationChoiceRow.carrier_route_id_DbName + "]=" + 
    //    base.Database.CreateSqlParameterName(TerminationChoiceRow.carrier_route_id_PropName);

    //  IDbCommand cmd = CreateGetCommand(whereSql, TerminationChoiceRow.priority_DbName);
    //  AddParameter(cmd, TerminationChoiceRow.customer_route_id_PropName, customer_route_id);
    //  AddParameter(cmd, TerminationChoiceRow.carrier_route_id_PropName, carrier_route_id);
    //  return cmd;
    //}

	} // End of TerminationChoiceCollection class
} // End of namespace

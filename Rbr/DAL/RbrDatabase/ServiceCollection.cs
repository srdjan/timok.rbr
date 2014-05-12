// <fileinfo name="ServiceCollection.cs">
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
  /// Represents the <c>Service</c> table.
  /// </summary>
  public class ServiceCollection : ServiceCollection_Base {
    /// <summary>
    /// Initializes a new instance of the <see cref="ServiceCollection"/> class.
    /// </summary>
    /// <param name="db">The database object.</param>
    internal ServiceCollection(Rbr_Db db)
      : base(db) {
      // EMPTY
    }

    public static ServiceRow Parse(System.Data.DataRow row) {
      return new ServiceCollection(null).MapRow(row);
    }

    protected override IDbCommand CreateGetAllCommand() {
      return base.CreateGetCommand(null, ServiceRow.type_DbName + "," + ServiceRow.name_DbName);
    }

    public ServiceRow[] GetByType(ServiceType pServiceType) {
      string _wherSql =
        ServiceRow.type_DbName + "=" + Database.CreateSqlParameterName(ServiceRow.type_PropName);
      IDbCommand _cmd = base.CreateGetCommand(_wherSql, ServiceRow.name_DbName); ;
      AddParameter(_cmd, ServiceRow.type_PropName, (byte) pServiceType);
      using (IDataReader _reader = _cmd.ExecuteReader()) {
        return MapRecords(_reader);
      }
    }

    public ServiceRow[] GetCustomerServices() {
      string _wherSql =
        ServiceRow.type_DbName + " IN (" + 
        (byte) ServiceType.Wholesale + "," + (byte) ServiceType.Retail + 
        ")";
      IDbCommand _cmd = base.CreateGetCommand(_wherSql, ServiceRow.name_DbName); ;
      using (IDataReader _reader = _cmd.ExecuteReader()) {
        return MapRecords(_reader);
      }
    }

    public ServiceRow[] GetAllShared() {
      string _wherSql =
        ServiceRow.is_shared_DbName + "=" + Database.CreateSqlParameterName(ServiceRow.is_shared_PropName);
      IDbCommand _cmd = base.CreateGetCommand(_wherSql, ServiceRow.name_DbName);
      AddParameter(_cmd, ServiceRow.is_shared_PropName, 1);
      using (IDataReader _reader = _cmd.ExecuteReader()) {
        return MapRecords(_reader);
      }
    }

    public ServiceRow[] GetSharedByRoutingPlanId(int pRoutingPlanId) {
      string _wherSql =
        ServiceRow.default_routing_plan_id_DbName + " = " + Database.CreateSqlParameterName(ServiceRow.default_routing_plan_id_PropName) +
        " AND " +
        ServiceRow.is_shared_DbName + " = " + Database.CreateSqlParameterName(ServiceRow.is_shared_PropName);

      IDbCommand _cmd = base.CreateGetCommand(_wherSql, ServiceRow.name_DbName);
      AddParameter(_cmd, ServiceRow.default_routing_plan_id_PropName, pRoutingPlanId);
      AddParameter(_cmd, ServiceRow.is_shared_PropName, 1);
      using (IDataReader _reader = _cmd.ExecuteReader()) {
        return MapRecords(_reader);
      }
    }

    public ServiceRow[] GetSharedByRetailType(RetailType pRetailType) {
      string _wherSql =
        ServiceRow.type_DbName + " = " + Database.CreateSqlParameterName(ServiceRow.type_PropName) +
        " AND " +
        ServiceRow.is_shared_DbName + " = " + Database.CreateSqlParameterName(ServiceRow.is_shared_PropName) +
        " AND " +
        ServiceRow.retail_type_DbName + " = " + Database.CreateSqlParameterName(ServiceRow.retail_type_PropName);
      IDbCommand _cmd = base.CreateGetCommand(_wherSql, ServiceRow.name_DbName);
      AddParameter(_cmd, ServiceRow.type_PropName, (byte) ServiceType.Retail);
      AddParameter(_cmd, ServiceRow.is_shared_PropName, 1);
      AddParameter(_cmd, ServiceRow.retail_type_PropName, (int) pRetailType);
      using (IDataReader _reader = _cmd.ExecuteReader()) {
        return MapRecords(_reader);
      }
    }

    public override void Insert(ServiceRow value) {
      string _sqlStr = "DECLARE " +
        base.Database.CreateSqlParameterName(ServiceRow.service_id_PropName) + " smallint " +
        "SET " + base.Database.CreateSqlParameterName(ServiceRow.service_id_PropName) +
        " = COALESCE((SELECT MAX(" + ServiceRow.service_id_DbName + ") FROM Service) + 1, 10000) " +

        "INSERT INTO [dbo].[Service] (" +
        "[" + ServiceRow.service_id_DbName + "], " +
        "[" + ServiceRow.name_DbName + "], " +
        "[" + ServiceRow.virtual_switch_id_DbName + "], " +
        "[" + ServiceRow.calling_plan_id_DbName + "], " +
        "[" + ServiceRow.default_routing_plan_id_DbName + "], " +
        "[" + ServiceRow.status_DbName + "], " +
        "[" + ServiceRow.type_DbName + "], " +
        "[" + ServiceRow.retail_type_DbName + "], " +
        "[" + ServiceRow.is_shared_DbName + "]," +
        "[" + ServiceRow.rating_type_DbName + "], " +
        "[" + ServiceRow.pin_length_DbName + "], " +
        "[" + ServiceRow.payphone_surcharge_id_DbName + "], " +
        "[" + ServiceRow.sweep_schedule_id_DbName + "], " +
        "[" + ServiceRow.sweep_fee_DbName + "], " +
        "[" + ServiceRow.sweep_rule_DbName + "], " +
        "[" + ServiceRow.balance_prompt_type_DbName + "], " +
        "[" + ServiceRow.balance_prompt_per_unit_DbName + "] " +
      ") VALUES (" +
        Database.CreateSqlParameterName(ServiceRow.service_id_PropName) + ", " +
        Database.CreateSqlParameterName(ServiceRow.name_PropName) + ", " +
        Database.CreateSqlParameterName(ServiceRow.virtual_switch_id_PropName) + ", " +
        Database.CreateSqlParameterName(ServiceRow.calling_plan_id_PropName) + ", " +
        Database.CreateSqlParameterName(ServiceRow.default_routing_plan_id_PropName) + ", " +
        Database.CreateSqlParameterName(ServiceRow.status_PropName) + ", " +
        Database.CreateSqlParameterName(ServiceRow.type_PropName) + ", " +
        Database.CreateSqlParameterName(ServiceRow.retail_type_PropName) + ", " +
        Database.CreateSqlParameterName(ServiceRow.is_shared_PropName) + ", " +
        Database.CreateSqlParameterName(ServiceRow.rating_type_PropName) + ", " +
        Database.CreateSqlParameterName(ServiceRow.pin_length_PropName) + ", " +
        Database.CreateSqlParameterName(ServiceRow.payphone_surcharge_id_PropName) + ", " +
        Database.CreateSqlParameterName(ServiceRow.sweep_schedule_id_PropName) + ", " +
        Database.CreateSqlParameterName(ServiceRow.sweep_fee_PropName) + ", " +
        Database.CreateSqlParameterName(ServiceRow.sweep_rule_PropName) + ", " +
        Database.CreateSqlParameterName(ServiceRow.balance_prompt_type_PropName) + ", " +
        Database.CreateSqlParameterName(ServiceRow.balance_prompt_per_unit_PropName) + ") " +
        " SELECT " + Database.CreateSqlParameterName(ServiceRow.service_id_PropName);

      IDbCommand _cmd = Database.CreateCommand(_sqlStr);
      AddParameter(_cmd, ServiceRow.name_PropName, value.Name);
      AddParameter(_cmd, ServiceRow.virtual_switch_id_PropName, value.Virtual_switch_id);
      AddParameter(_cmd, ServiceRow.calling_plan_id_PropName, value.Calling_plan_id);
      AddParameter(_cmd, ServiceRow.default_routing_plan_id_PropName, value.Default_routing_plan_id);
      AddParameter(_cmd, ServiceRow.status_PropName, value.Status);
      AddParameter(_cmd, ServiceRow.type_PropName, value.Type);
      AddParameter(_cmd, ServiceRow.retail_type_PropName, value.Retail_type);
      AddParameter(_cmd, ServiceRow.is_shared_PropName, value.Is_shared);
      AddParameter(_cmd, ServiceRow.rating_type_PropName, value.Rating_type);
      AddParameter(_cmd, ServiceRow.pin_length_PropName, value.Pin_length);

      AddParameter(_cmd, ServiceRow.payphone_surcharge_id_PropName,
        value.IsPayphone_surcharge_idNull ? DBNull.Value : (object) value.Payphone_surcharge_id);
      AddParameter(_cmd, ServiceRow.sweep_schedule_id_PropName,
        value.IsSweep_schedule_idNull ? DBNull.Value : (object) value.Sweep_schedule_id);
      
      AddParameter(_cmd, ServiceRow.sweep_fee_PropName, value.Sweep_fee);
      AddParameter(_cmd, ServiceRow.sweep_rule_PropName, value.Sweep_rule);
      AddParameter(_cmd, ServiceRow.balance_prompt_type_PropName, value.Balance_prompt_type);
      AddParameter(_cmd, ServiceRow.balance_prompt_per_unit_PropName, value.Balance_prompt_per_unit);


      try {
        object _res = _cmd.ExecuteScalar();
        value.Service_id = (short) _res;
      }
      catch (System.Data.SqlClient.SqlException _sqlEx) {
        if (_sqlEx.Message.IndexOf("Cannot insert duplicate key row in object") > -1) {
          throw new AlternateKeyException("Service Name already in use!", _sqlEx);
        }
        throw;// any other ex
      }
    }

    public int GetCountByDefaultRoutingPlanId(int pDefaultRoutingPlanId) {
      string _sqlStr = "SELECT COUNT(*) FROM Service WHERE " +
        "[" + ServiceRow.default_routing_plan_id_DbName + "]=" + base.Database.CreateSqlParameterName(ServiceRow.default_routing_plan_id_PropName);

      IDbCommand _cmd = base.Database.CreateCommand(_sqlStr);
      AddParameter(_cmd, ServiceRow.default_routing_plan_id_PropName, pDefaultRoutingPlanId);
      int _res = (int) _cmd.ExecuteScalar();
      return _res;
    }

    public int GetCountByCallingPlanId(int pCallingPlanId) {
      string _sqlStr = "SELECT COUNT(*) FROM Service WHERE " +
        "[" + ServiceRow.calling_plan_id_DbName + "]=" + base.Database.CreateSqlParameterName(ServiceRow.calling_plan_id_PropName);

      IDbCommand _cmd = base.Database.CreateCommand(_sqlStr);
      AddParameter(_cmd, ServiceRow.calling_plan_id_PropName, pCallingPlanId);
      int _res = (int) _cmd.ExecuteScalar();
      return _res;
    }

  } // End of ServiceCollection class
} // End of namespace

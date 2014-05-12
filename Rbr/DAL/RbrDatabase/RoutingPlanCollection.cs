// <fileinfo name="RoutingPlanCollection.cs">
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

namespace Timok.Rbr.DAL.RbrDatabase {
  /// <summary>
  /// Represents the <c>RoutingPlan</c> table.
  /// </summary>
  public class RoutingPlanCollection : RoutingPlanCollection_Base {
    /// <summary>
    /// Initializes a new instance of the <see cref="RoutingPlanCollection"/> class.
    /// </summary>
    /// <param name="db">The database object.</param>
    internal RoutingPlanCollection(Rbr_Db db)
      : base(db) {
      // EMPTY
    }

    public static RoutingPlanRow Parse(System.Data.DataRow row) {
      return new RoutingPlanCollection(null).MapRow(row);
    }

    protected override IDbCommand CreateGetAllCommand() {
      return CreateGetCommand(null, RoutingPlanRow.name_DbName);
    }

    protected override IDbCommand CreateGetByCalling_plan_idCommand(int pCallingPlanId) {
      string _whereSql = RoutingPlanRow.calling_plan_id_DbName + "=" + Database.CreateSqlParameterName(RoutingPlanRow.calling_plan_id_PropName);

      IDbCommand _cmd = CreateGetCommand(_whereSql, RoutingPlanRow.name_DbName);
      AddParameter(_cmd, RoutingPlanRow.calling_plan_id_PropName, pCallingPlanId);
      return _cmd;
    }

    protected override IDbCommand CreateDeleteByVirtual_switch_idCommand(int pVirtualSwitchId) {
      string _whereSql = RoutingPlanRow.virtual_switch_id_DbName + "=" + Database.CreateSqlParameterName(RoutingPlanRow.virtual_switch_id_PropName);

      IDbCommand _cmd = CreateGetCommand(_whereSql, RoutingPlanRow.name_DbName);
      AddParameter(_cmd, RoutingPlanRow.virtual_switch_id_PropName, pVirtualSwitchId);
      return _cmd;
    }

    public override void Insert(RoutingPlanRow value) {
      string _sqlStr = "DECLARE " +
        base.Database.CreateSqlParameterName(RoutingPlanRow.routing_plan_id_PropName) + " int " +
        "SET " + base.Database.CreateSqlParameterName(RoutingPlanRow.routing_plan_id_PropName) +
        " = COALESCE((SELECT MAX(" + RoutingPlanRow.routing_plan_id_DbName + ") FROM RoutingPlan) + 1, 1) " +

        "INSERT INTO [dbo].[RoutingPlan] (" +
        "[" + RoutingPlanRow.routing_plan_id_DbName + "], " +
        "[" + RoutingPlanRow.name_DbName + "], " +
        "[" + RoutingPlanRow.calling_plan_id_DbName + "], " +
        "[" + RoutingPlanRow.virtual_switch_id_DbName + "]" +
        ") VALUES (" +
        Database.CreateSqlParameterName(RoutingPlanRow.routing_plan_id_PropName) + ", " +
        Database.CreateSqlParameterName(RoutingPlanRow.name_PropName) + ", " +
        Database.CreateSqlParameterName(RoutingPlanRow.calling_plan_id_PropName) + ", " +
        Database.CreateSqlParameterName(RoutingPlanRow.virtual_switch_id_PropName) + ") " +
        " SELECT " + Database.CreateSqlParameterName(RoutingPlanRow.routing_plan_id_PropName);

      IDbCommand _cmd = Database.CreateCommand(_sqlStr);
      //AddParameter(_cmd, RoutingPlanRow.routing_plan_id_PropName, value.Routing_plan_id);
      AddParameter(_cmd, RoutingPlanRow.name_PropName, value.Name);
      AddParameter(_cmd, RoutingPlanRow.calling_plan_id_PropName, value.Calling_plan_id);
      AddParameter(_cmd, RoutingPlanRow.virtual_switch_id_PropName, value.Virtual_switch_id);

      try {
        object _res = _cmd.ExecuteScalar();
        value.Routing_plan_id = (int) _res;
      }
      catch (System.Data.SqlClient.SqlException _sqlEx) {
        if (_sqlEx.Message.IndexOf("Cannot insert duplicate key row in object") > -1) {
          throw new AlternateKeyException("Routing Plan Name already in use!", _sqlEx);
        }
        throw;// any other ex
      }
    }

    public int GetCountByCallingPlanId(int pCallingPlanId) {
      string _sqlStr = "SELECT COUNT(*) FROM RoutingPlan WHERE " +
        "[" + RoutingPlanRow.calling_plan_id_DbName + "]=" + base.Database.CreateSqlParameterName(RoutingPlanRow.calling_plan_id_PropName);

      IDbCommand _cmd = base.Database.CreateCommand(_sqlStr);
      AddParameter(_cmd, RoutingPlanRow.calling_plan_id_PropName, pCallingPlanId);
      int _res = (int) _cmd.ExecuteScalar();
      return _res;
    }
  } // End of RoutingPlanCollection class
} // End of namespace

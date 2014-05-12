// <fileinfo name="CallingPlanCollection.cs">
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
	/// Represents the <c>CallingPlan</c> table.
	/// </summary>
	public class CallingPlanCollection : CallingPlanCollection_Base
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="CallingPlanCollection"/> class.
		/// </summary>
		/// <param name="db">The database object.</param>
		internal CallingPlanCollection(Rbr_Db db)
				: base(db)
		{
			// EMPTY
		}

		public static CallingPlanRow Parse(System.Data.DataRow row){
			return new CallingPlanCollection(null).MapRow(row);
		}

    protected override IDbCommand CreateGetAllCommand() {
      return CreateGetCommand(null, CallingPlanRow.name_DbName);
    }

    public override void Insert(CallingPlanRow value) {
      string _sqlStr = "DECLARE " +
        base.Database.CreateSqlParameterName(CallingPlanRow.calling_plan_id_DbName) + " int " +
        "SET " + base.Database.CreateSqlParameterName(CallingPlanRow.calling_plan_id_PropName) +
        " = COALESCE((SELECT MAX(" + CallingPlanRow.calling_plan_id_DbName + ") FROM CallingPlan) + 1, 1) " +

        "INSERT INTO [dbo].[CallingPlan] (" +
        "[" + CallingPlanRow.calling_plan_id_DbName + "], " +
        "[" + CallingPlanRow.name_DbName + "], " +
        "[" + CallingPlanRow.virtual_switch_id_DbName + "], " +
        "[" + CallingPlanRow.version_DbName + "]" +
        ") VALUES (" +
        Database.CreateSqlParameterName(CallingPlanRow.calling_plan_id_PropName) + ", " +
        Database.CreateSqlParameterName(CallingPlanRow.name_PropName) + ", " +
        Database.CreateSqlParameterName(CallingPlanRow.virtual_switch_id_PropName) + ", " +
        Database.CreateSqlParameterName(CallingPlanRow.version_PropName) + ")" +
        " SELECT " + Database.CreateSqlParameterName(CallingPlanRow.calling_plan_id_PropName);

      IDbCommand _cmd = Database.CreateCommand(_sqlStr);
      //AddParameter(_cmd, CallingPlanRow.calling_plan_id_PropName, value.Calling_plan_id);
      AddParameter(_cmd, CallingPlanRow.name_PropName, value.Name);
      AddParameter(_cmd, CallingPlanRow.virtual_switch_id_PropName, value.Virtual_switch_id);
      AddParameter(_cmd, CallingPlanRow.version_PropName, value.Version);

      try {
        value.Calling_plan_id = (int) _cmd.ExecuteScalar();
      }
      catch (System.Data.SqlClient.SqlException _sqlEx) {
        if (_sqlEx.Message.IndexOf("Cannot insert duplicate key row in object") > -1) {
          throw new AlternateKeyException("Calling Plan Name already in use!", _sqlEx);
        }
        throw;// any other ex
      }

    }
	} // End of CallingPlanCollection class
} // End of namespace

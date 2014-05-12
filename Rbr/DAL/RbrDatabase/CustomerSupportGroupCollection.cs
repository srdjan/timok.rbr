// <fileinfo name="CustomerSupportGroupCollection.cs">
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
  /// Represents the <c>CustomerSupportGroup</c> table.
  /// </summary>
  public class CustomerSupportGroupCollection : CustomerSupportGroupCollection_Base {
    /// <summary>
    /// Initializes a new instance of the <see cref="CustomerSupportGroupCollection"/> class.
    /// </summary>
    /// <param name="db">The database object.</param>
    internal CustomerSupportGroupCollection(Rbr_Db db)
      : base(db) {
      // EMPTY
    }

    public static CustomerSupportGroupRow Parse(System.Data.DataRow row) {
      return new CustomerSupportGroupCollection(null).MapRow(row);
    }

    public override void Insert(CustomerSupportGroupRow value) {
      try {
        string _sqlStr = "DECLARE " + base.Database.CreateSqlParameterName(CustomerSupportGroupRow.group_id_PropName) + " int " +
          "SET " + base.Database.CreateSqlParameterName(CustomerSupportGroupRow.group_id_PropName) +
          " = COALESCE((SELECT MAX(" + CustomerSupportGroupRow.group_id_DbName + ") FROM CustomerSupportGroup) + 1, " + CustomerSupportGroupRow.DefaultId + ") " +

          "INSERT INTO [dbo].[CustomerSupportGroup] (" +
          "[" + CustomerSupportGroupRow.group_id_DbName + "], " +
          "[" + CustomerSupportGroupRow.description_DbName + "]," +
          "[" + CustomerSupportGroupRow.role_DbName + "]," +
          "[" + CustomerSupportGroupRow.max_amount_DbName + "]," +
          "[" + CustomerSupportGroupRow.allow_status_change_DbName + "]," +
          "[" + CustomerSupportGroupRow.vendor_id_DbName + "] " +
          ") VALUES (" +
          Database.CreateSqlParameterName(CustomerSupportGroupRow.group_id_PropName) + ", " +
          Database.CreateSqlParameterName(CustomerSupportGroupRow.description_PropName) + ", " +
          Database.CreateSqlParameterName(CustomerSupportGroupRow.role_PropName) + ", " +
          Database.CreateSqlParameterName(CustomerSupportGroupRow.max_amount_PropName) + ", " +
          Database.CreateSqlParameterName(CustomerSupportGroupRow.allow_status_change_PropName) + ", " +
          Database.CreateSqlParameterName(CustomerSupportGroupRow.vendor_id_PropName) + ")" +
          " SELECT " + Database.CreateSqlParameterName(CustomerSupportGroupRow.group_id_PropName);

        IDbCommand _cmd = Database.CreateCommand(_sqlStr);
        //AUTO-GEN above// AddParameter(cmd, CustomerSupportGroupRow.customer_support_group_id_PropName, value.Customer_support_group_id);
        AddParameter(_cmd, CustomerSupportGroupRow.description_PropName, value.Description);
        AddParameter(_cmd, CustomerSupportGroupRow.role_PropName, value.Role);
        AddParameter(_cmd, CustomerSupportGroupRow.max_amount_PropName, value.Max_amount);
        AddParameter(_cmd, CustomerSupportGroupRow.allow_status_change_PropName, value.Allow_status_change);
        AddParameter(_cmd, CustomerSupportGroupRow.vendor_id_PropName, value.Vendor_id);
        value.Group_id = (int) _cmd.ExecuteNonQuery();
      }
      catch (System.Data.SqlClient.SqlException _sqlEx) {
        if (_sqlEx.Message.IndexOf("Cannot insert duplicate key row in object") > -1) {
          throw new AlternateKeyException("Customer Support Group Description already in use! [value=" + value.Description + "]", _sqlEx);
        }
        throw;// any other ex
      }
    }

    public override bool Update(CustomerSupportGroupRow value) {
      try {
        return base.Update(value);
      }
      catch (System.Data.SqlClient.SqlException _sqlEx) {
        if (_sqlEx.Message.IndexOf("Cannot insert duplicate key row in object") > -1) {
          throw new AlternateKeyException("Customer Support Group Description already in use! [value=" + value.Description + "]", _sqlEx);
        }
        throw;// any other ex
      }
    }

    protected override IDbCommand CreateGetCommand(string whereSql, string orderBySql) {
      return base.CreateGetCommand(whereSql, CustomerSupportGroupRow.description_DbName);
    }

  } // End of CustomerSupportGroupCollection class
} // End of namespace

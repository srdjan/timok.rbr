// <fileinfo name="CustomerSupportVendorCollection.cs">
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
  /// Represents the <c>CustomerSupportVendor</c> table.
  /// </summary>
  public class CustomerSupportVendorCollection : CustomerSupportVendorCollection_Base {
    /// <summary>
    /// Initializes a new instance of the <see cref="CustomerSupportVendorCollection"/> class.
    /// </summary>
    /// <param name="db">The database object.</param>
    internal CustomerSupportVendorCollection(Rbr_Db db)
      : base(db) {
      // EMPTY
    }

    public static CustomerSupportVendorRow Parse(System.Data.DataRow row) {
      return new CustomerSupportVendorCollection(null).MapRow(row);
    }

    public override void Insert(CustomerSupportVendorRow value) {
      try {
        string _sqlStr = "DECLARE " + Database.CreateSqlParameterName(CustomerSupportVendorRow.vendor_id_PropName) + " int " +
          "SET " + Database.CreateSqlParameterName(CustomerSupportVendorRow.vendor_id_PropName) +
          " = COALESCE((SELECT MAX(" + CustomerSupportVendorRow.vendor_id_DbName + ") FROM CustomerSupportVendor) + 1, " + CustomerSupportVendorRow.DefaultId + ") " +

          "INSERT INTO [dbo].[CustomerSupportVendor] (" +
          "[" + CustomerSupportVendorRow.vendor_id_DbName + "], " +
          "[" + CustomerSupportVendorRow.name_DbName + "], " +
          "[" + CustomerSupportVendorRow.contact_info_id_DbName + "]" +
          ") VALUES (" +
          Database.CreateSqlParameterName(CustomerSupportVendorRow.vendor_id_PropName) + ", " +
          Database.CreateSqlParameterName(CustomerSupportVendorRow.name_PropName) + ", " +
          Database.CreateSqlParameterName(CustomerSupportVendorRow.contact_info_id_PropName) + ")" +
          " SELECT " + Database.CreateSqlParameterName(CustomerSupportVendorRow.vendor_id_PropName);

        IDbCommand _cmd = Database.CreateCommand(_sqlStr);
        //AddParameter(_cmd, CustomerSupportVendorRow.vendor_id_PropName, value.Vendor_id);
        AddParameter(_cmd, CustomerSupportVendorRow.name_PropName, value.Name);
        AddParameter(_cmd, CustomerSupportVendorRow.contact_info_id_PropName, value.Contact_info_id);
        
        value.Vendor_id = (int) _cmd.ExecuteNonQuery();
      }
      catch (System.Data.SqlClient.SqlException _sqlEx) {
        if (_sqlEx.Message.IndexOf("Cannot insert duplicate key row in object") > -1) {
          throw new AlternateKeyException("Customer Support Vendor Name already in use! [value=" + value.Name + "]", _sqlEx);
        }
        throw;// any other ex
      }
    }
  } // End of CustomerSupportVendorCollection class
} // End of namespace

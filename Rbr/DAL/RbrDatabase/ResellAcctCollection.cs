// <fileinfo name="ResellAcctCollection.cs">
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
  /// Represents the <c>ResellAcct</c> table.
  /// </summary>
  public class ResellAcctCollection : ResellAcctCollection_Base {
    /// <summary>
    /// Initializes a new instance of the <see cref="ResellAcctCollection"/> class.
    /// </summary>
    /// <param name="db">The database object.</param>
    internal ResellAcctCollection(Rbr_Db db)
      : base(db) {
      // EMPTY
    }

    public static ResellAcctRow Parse(System.Data.DataRow row) {
      return new ResellAcctCollection(null).MapRow(row);
    }

    public override void Insert(ResellAcctRow value) {
      string _sqlStr = "DECLARE " +
        base.Database.CreateSqlParameterName(ResellAcctRow.resell_acct_id_PropName) + " smallint " +
        "SET " + base.Database.CreateSqlParameterName(ResellAcctRow.resell_acct_id_PropName) +
        " = COALESCE((SELECT MAX(" + ResellAcctRow.resell_acct_id_DbName + ") FROM ResellAcct) + 1, 10000) " +

        "INSERT INTO [dbo].[ResellAcct] (" +
        "[" + ResellAcctRow.resell_acct_id_DbName + "], " +
        "[" + ResellAcctRow.partner_id_DbName + "], " +
        "[" + ResellAcctRow.customer_acct_id_DbName + "], " +
        "[" + ResellAcctRow.person_id_DbName + "], " +
        "[" + ResellAcctRow.per_route_DbName + "], " +
        "[" + ResellAcctRow.commision_type_DbName + "], " +
        "[" + ResellAcctRow.markup_dollar_DbName + "], " +
        "[" + ResellAcctRow.markup_percent_DbName + "], " +
        "[" + ResellAcctRow.fee_per_call_DbName + "], " +
        "[" + ResellAcctRow.fee_per_minute_DbName + "]" +
        ") VALUES (" +
        Database.CreateSqlParameterName(ResellAcctRow.resell_acct_id_PropName) + ", " +
        Database.CreateSqlParameterName(ResellAcctRow.partner_id_PropName) + ", " +
        Database.CreateSqlParameterName(ResellAcctRow.customer_acct_id_PropName) + ", " +
        Database.CreateSqlParameterName(ResellAcctRow.person_id_PropName) + ", " +
        Database.CreateSqlParameterName(ResellAcctRow.per_route_PropName) + ", " +
        Database.CreateSqlParameterName(ResellAcctRow.commision_type_PropName) + ", " +
        Database.CreateSqlParameterName(ResellAcctRow.markup_dollar_PropName) + ", " +
        Database.CreateSqlParameterName(ResellAcctRow.markup_percent_PropName) + ", " +
        Database.CreateSqlParameterName(ResellAcctRow.fee_per_call_PropName) + ", " +
        Database.CreateSqlParameterName(ResellAcctRow.fee_per_minute_PropName) + ") " +
        " SELECT " + base.Database.CreateSqlParameterName(ResellAcctRow.resell_acct_id_PropName);

      IDbCommand _cmd = Database.CreateCommand(_sqlStr);
      //AddParameter(_cmd, "Resell_acct_id", value.Resell_acct_id);
      AddParameter(_cmd, ResellAcctRow.partner_id_PropName, value.Partner_id);
      AddParameter(_cmd, ResellAcctRow.customer_acct_id_PropName, value.Customer_acct_id);
      AddParameter(_cmd, ResellAcctRow.person_id_PropName, value.Person_id);
      AddParameter(_cmd, ResellAcctRow.per_route_PropName, value.Per_route);
      AddParameter(_cmd, ResellAcctRow.commision_type_PropName, value.Commision_type);
      AddParameter(_cmd, ResellAcctRow.markup_dollar_PropName, value.Markup_dollar);
      AddParameter(_cmd, ResellAcctRow.markup_percent_PropName, value.Markup_percent);
      AddParameter(_cmd, ResellAcctRow.fee_per_call_PropName, value.Fee_per_call);
      AddParameter(_cmd, ResellAcctRow.fee_per_minute_PropName, value.Fee_per_minute);

      object _res = _cmd.ExecuteScalar();

      value.Resell_acct_id = (short) _res;
    }
  } // End of ResellAcctCollection class
} // End of namespace

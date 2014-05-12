// <fileinfo name="PayphoneSurchargeCollection.cs">
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
  /// Represents the <c>PayphoneSurcharge</c> table.
  /// </summary>
  public class PayphoneSurchargeCollection : PayphoneSurchargeCollection_Base {
    /// <summary>
    /// Initializes a new instance of the <see cref="PayphoneSurchargeCollection"/> class.
    /// </summary>
    /// <param name="db">The database object.</param>
    internal PayphoneSurchargeCollection(Rbr_Db db)
      : base(db) {
      // EMPTY
    }

    public static PayphoneSurchargeRow Parse(System.Data.DataRow row) {
      return new PayphoneSurchargeCollection(null).MapRow(row);
    }

    public override void Insert(PayphoneSurchargeRow value) {
      string _sqlStr = "DECLARE " +
        base.Database.CreateSqlParameterName(PayphoneSurchargeRow.payphone_surcharge_id_DbName) + " int " +
        "SET " +
        base.Database.CreateSqlParameterName(PayphoneSurchargeRow.payphone_surcharge_id_PropName) +
        " = COALESCE((SELECT MAX(" + PayphoneSurchargeRow.payphone_surcharge_id_DbName + ") FROM PayphoneSurcharge) + 1, 1) " +

        "INSERT INTO [dbo].[PayphoneSurcharge] (" +
        "[" + PayphoneSurchargeRow.payphone_surcharge_id_DbName + "], " +
        "[" + PayphoneSurchargeRow.surcharge_DbName + "], " +
        "[" + PayphoneSurchargeRow.surcharge_type_DbName + "]" +
        ") VALUES (" +
        Database.CreateSqlParameterName(PayphoneSurchargeRow.payphone_surcharge_id_PropName) + ", " +
        Database.CreateSqlParameterName(PayphoneSurchargeRow.surcharge_PropName) + ", " +
        Database.CreateSqlParameterName(PayphoneSurchargeRow.surcharge_type_PropName) + ") " +
        " SELECT " + base.Database.CreateSqlParameterName(PayphoneSurchargeRow.payphone_surcharge_id_PropName) + " ";

      IDbCommand _cmd = Database.CreateCommand(_sqlStr);
      //AddParameter(cmd, "Payphone_surcharge_id", value.Payphone_surcharge_id);
      AddParameter(_cmd, PayphoneSurchargeRow.surcharge_PropName, value.Surcharge);
      AddParameter(_cmd, PayphoneSurchargeRow.surcharge_type_PropName, value.Surcharge_type);

      value.Payphone_surcharge_id = (int) _cmd.ExecuteScalar();
    }
  } // End of PayphoneSurchargeCollection class
} // End of namespace

// <fileinfo name="RateInfoCollection.cs">
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
  /// Represents the <c>RateInfo</c> table.
  /// </summary>
  public class RateInfoCollection : RateInfoCollection_Base {
    /// <summary>
    /// Initializes a new instance of the <see cref="RateInfoCollection"/> class.
    /// </summary>
    /// <param name="db">The database object.</param>
    internal RateInfoCollection(Rbr_Db db)
      : base(db) {
      // EMPTY
    }

    public static RateInfoRow Parse(System.Data.DataRow row) {
      return new RateInfoCollection(null).MapRow(row);
    }

    public override void Insert(RateInfoRow value) {
      string _sqlStr = "DECLARE " +
        base.Database.CreateSqlParameterName(RateInfoRow.rate_info_id_PropName) + " int " +
        "SET " + base.Database.CreateSqlParameterName(RateInfoRow.rate_info_id_PropName) +
        " = COALESCE((SELECT MAX(" + RateInfoRow.rate_info_id_DbName + ") FROM RateInfo) + 1, 700) " +

        "INSERT INTO [dbo].[RateInfo] (" +
        "[" + RateInfoRow.rate_info_id_DbName + "] " +
        ") VALUES (" +
        Database.CreateSqlParameterName(RateInfoRow.rate_info_id_PropName) + ") " +
        " SELECT " + base.Database.CreateSqlParameterName(RateInfoRow.rate_info_id_PropName);

      IDbCommand _cmd = Database.CreateCommand(_sqlStr);
      //AddParameter(_cmd, "Rate_info_id", value.Rate_info_id);

      object _res = _cmd.ExecuteScalar();

      value.Rate_info_id = (int) _res;
    }

  } // End of RateInfoCollection class
} // End of namespace

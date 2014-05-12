// <fileinfo name="LCRBlackListCollection.cs">
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
  /// Represents the <c>LCRBlackList</c> table.
  /// </summary>
  public class LCRBlackListCollection : LCRBlackListCollection_Base {
    /// <summary>
    /// Initializes a new instance of the <see cref="LCRBlackListCollection"/> class.
    /// </summary>
    /// <param name="db">The database object.</param>
    internal LCRBlackListCollection(Rbr_Db db)
      : base(db) {
      // EMPTY
    }

    public static LCRBlackListRow Parse(System.Data.DataRow row) {
      return new LCRBlackListCollection(null).MapRow(row);
    }

    public int GetCountByPrimaryKey(int pRoutingPlanId, int pRouteId, short pCarrierAcctId) {
      string _sqlStr = "SELECT COUNT(*) FROM LCRBlackList WHERE " +
        "[" + LCRBlackListRow.routing_plan_id_DbName + "]=" + Database.CreateSqlParameterName(LCRBlackListRow.routing_plan_id_PropName) +
        " AND " +
        "[" + LCRBlackListRow.route_id_DbName + "]=" + Database.CreateSqlParameterName(LCRBlackListRow.route_id_PropName) +
        " AND " +
        "[" + LCRBlackListRow.carrier_acct_id_DbName + "]=" + Database.CreateSqlParameterName(LCRBlackListRow.carrier_acct_id_PropName);

      IDbCommand _cmd = base.Database.CreateCommand(_sqlStr);
      AddParameter(_cmd, LCRBlackListRow.routing_plan_id_PropName, pRoutingPlanId);
      AddParameter(_cmd, LCRBlackListRow.route_id_PropName, pRouteId);
      AddParameter(_cmd, LCRBlackListRow.carrier_acct_id_PropName, pCarrierAcctId);
      return (int) _cmd.ExecuteScalar();
    }
  } // End of LCRBlackListCollection class
} // End of namespace

// <fileinfo name="BalanceAdjustmentReasonCollection.cs">
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

namespace Timok.Rbr.DAL.RbrDatabase
{
	/// <summary>
	/// Represents the <c>BalanceAdjustmentReason</c> table.
	/// </summary>
	public class BalanceAdjustmentReasonCollection : BalanceAdjustmentReasonCollection_Base
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="BalanceAdjustmentReasonCollection"/> class.
		/// </summary>
		/// <param name="db">The database object.</param>
		internal BalanceAdjustmentReasonCollection(Rbr_Db db)
				: base(db)
		{
			// EMPTY
		}

		public static BalanceAdjustmentReasonRow Parse(System.Data.DataRow row){
			return new BalanceAdjustmentReasonCollection(null).MapRow(row);
		}

    public override void Insert(BalanceAdjustmentReasonRow value) {
      string _sqlStr = "DECLARE " + base.Database.CreateSqlParameterName(BalanceAdjustmentReasonRow.balance_adjustment_reason_id_PropName) + " int " +
        "SET " + base.Database.CreateSqlParameterName(BalanceAdjustmentReasonRow.balance_adjustment_reason_id_PropName) +
        " = COALESCE((SELECT MAX(" + BalanceAdjustmentReasonRow.balance_adjustment_reason_id_DbName + ") FROM BalanceAdjustmentReason) + 1, 1) " +

        "INSERT INTO [dbo].[BalanceAdjustmentReason] (" +
        "[" + BalanceAdjustmentReasonRow.balance_adjustment_reason_id_DbName + "], " +
        "[" + BalanceAdjustmentReasonRow.description_DbName + "], " +
        "[" + BalanceAdjustmentReasonRow.type_DbName + "]" +
        ") VALUES (" +
        Database.CreateSqlParameterName(BalanceAdjustmentReasonRow.balance_adjustment_reason_id_PropName) + ", " +
        Database.CreateSqlParameterName(BalanceAdjustmentReasonRow.description_PropName) + ", " +
        Database.CreateSqlParameterName(BalanceAdjustmentReasonRow.type_PropName) + ")" +
        " SELECT " + Database.CreateSqlParameterName(BalanceAdjustmentReasonRow.balance_adjustment_reason_id_PropName);

      IDbCommand _cmd = Database.CreateCommand(_sqlStr);
      //AddParameter(cmd, "Balance_adjustment_reason_id", value.Balance_adjustment_reason_id);
      AddParameter(_cmd, BalanceAdjustmentReasonRow.description_PropName, value.Description);
      AddParameter(_cmd, BalanceAdjustmentReasonRow.type_PropName, value.Type);

      value.Balance_adjustment_reason_id = (int) _cmd.ExecuteNonQuery();
    }

    public BalanceAdjustmentReasonRow[] GetByType(BalanceAdjustmentReasonType pBalanceAdjustmentReasonType) {
      #region sql
      /*
			SELECT * 
			FROM  BalanceAdjustmentReasonType 
			WHERE 
			balance_adjustment_reason_id = 1
			*/
      #endregion sql

      string _where = BalanceAdjustmentReasonRow.type_DbName + " = " + (byte) pBalanceAdjustmentReasonType;

      IDbCommand _cmd = CreateGetCommand(_where, BalanceAdjustmentReasonRow.description_DbName);
      using (IDataReader _reader = _cmd.ExecuteReader()) {
        return MapRecords(_reader);
      }
    }


	} // End of BalanceAdjustmentReasonCollection class
} // End of namespace

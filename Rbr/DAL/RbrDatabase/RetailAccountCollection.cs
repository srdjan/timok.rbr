// <fileinfo name="RetailAccountCollection.cs">
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
	/// Represents the <c>RetailAccount</c> table.
	/// </summary>
	public class RetailAccountCollection : RetailAccountCollection_Base {
		/// <summary>
		/// Initializes a new instance of the <see cref="RetailAccountCollection"/> class.
		/// </summary>
		/// <param name="db">The database object.</param>
		internal RetailAccountCollection(Rbr_Db db)
			: base(db) {
			// EMPTY
		}

		public static RetailAccountRow Parse(System.Data.DataRow row){
			return new RetailAccountCollection(null).MapRow(row);
		}

    public override void Insert(RetailAccountRow value) {
      string _sqlStr =
        "DECLARE " +
        base.Database.CreateSqlParameterName(RetailAccountRow.retail_acct_id_DbName) + " int " +
        "SET " +
        base.Database.CreateSqlParameterName(RetailAccountRow.retail_acct_id_PropName) +
        " = COALESCE((SELECT MAX(" + RetailAccountRow.retail_acct_id_DbName + ") FROM RetailAccount) + 1, " + RetailAccountRow.DefaultId + ") " +

        " INSERT INTO [dbo].[RetailAccount] (" +
        "[" + RetailAccountRow.retail_acct_id_DbName + "], " +
        "[" + RetailAccountRow.customer_acct_id_DbName + "], " +
        "[" + RetailAccountRow.date_created_DbName + "], " +
        "[" + RetailAccountRow.date_active_DbName + "], " +
        "[" + RetailAccountRow.date_to_expire_DbName + "], " +
        "[" + RetailAccountRow.date_expired_DbName + "], " +
        "[" + RetailAccountRow.status_DbName + "], " +
        "[" + RetailAccountRow.start_balance_DbName + "], " +
        "[" + RetailAccountRow.current_balance_DbName + "], " +
        "[" + RetailAccountRow.start_bonus_minutes_DbName + "], " +
        "[" + RetailAccountRow.current_bonus_minutes_DbName + "] " +
        ") VALUES (" +
        Database.CreateSqlParameterName(RetailAccountRow.retail_acct_id_PropName) + ", " +
        Database.CreateSqlParameterName(RetailAccountRow.customer_acct_id_PropName) + ", " +
        Database.CreateSqlParameterName(RetailAccountRow.date_created_PropName) + ", " +
        Database.CreateSqlParameterName(RetailAccountRow.date_active_PropName) + ", " +
        Database.CreateSqlParameterName(RetailAccountRow.date_to_expire_PropName) + ", " +
        Database.CreateSqlParameterName(RetailAccountRow.date_expired_PropName) + ", " +
        Database.CreateSqlParameterName(RetailAccountRow.status_PropName) + ", " +
        Database.CreateSqlParameterName(RetailAccountRow.start_balance_PropName) + ", " +
        Database.CreateSqlParameterName(RetailAccountRow.current_balance_PropName) + ", " +
        Database.CreateSqlParameterName(RetailAccountRow.start_bonus_minutes_PropName) + ", " +
        Database.CreateSqlParameterName(RetailAccountRow.current_bonus_minutes_PropName) + ") " +

        " SELECT " + base.Database.CreateSqlParameterName(RetailAccountRow.retail_acct_id_PropName) + " ";

      IDbCommand _cmd = Database.CreateCommand(_sqlStr);
      //AddParameter(_cmd, RetailAccountRow.retail_acct_id_PropName, value.Retail_acct_id);
      AddParameter(_cmd, RetailAccountRow.customer_acct_id_PropName, value.Customer_acct_id);
      AddParameter(_cmd, RetailAccountRow.date_created_PropName, value.Date_created);
      AddParameter(_cmd, RetailAccountRow.date_active_PropName, value.Date_active);
      AddParameter(_cmd, RetailAccountRow.date_to_expire_PropName, value.Date_to_expire);
      AddParameter(_cmd, RetailAccountRow.date_expired_PropName, value.Date_expired);
      AddParameter(_cmd, RetailAccountRow.status_PropName, value.Status);
      AddParameter(_cmd, RetailAccountRow.start_balance_PropName, value.Start_balance);
      AddParameter(_cmd, RetailAccountRow.current_balance_PropName, value.Current_balance);
      AddParameter(_cmd, RetailAccountRow.start_bonus_minutes_PropName, value.Start_bonus_minutes);
      AddParameter(_cmd, RetailAccountRow.current_bonus_minutes_PropName, value.Current_bonus_minutes);

      value.Retail_acct_id = (int) _cmd.ExecuteScalar();
    }

		
		public bool Credit(int pRetailAcctId, decimal pAmount, short pMinutes) {
			string _sqlStr = "UPDATE RetailAccount " + 
				"SET [" + RetailAccountRow.current_balance_DbName + "] = [" + RetailAccountRow.current_balance_DbName + "] + @CreditAmount " + 
				", [" + RetailAccountRow.current_bonus_minutes_DbName + "] = [" + RetailAccountRow.current_bonus_minutes_DbName + "] + @CreditMinutes " + 
				"WHERE [" + RetailAccountRow.retail_acct_id_DbName + "] = " + 
				base.Database.CreateSqlParameterName(RetailAccountRow.retail_acct_id_PropName) + " " + 
				"SELECT @@ROWCOUNT ";
			IDbCommand _cmd = base.Database.CreateCommand(_sqlStr);
			AddParameter(_cmd, RetailAccountRow.retail_acct_id_PropName, pRetailAcctId);
			base.Database.AddParameter(_cmd, "CreditAmount", DbType.Decimal, pAmount);
			base.Database.AddParameter(_cmd, "CreditMinutes", DbType.Int16, pMinutes);
			int _count = (int) _cmd.ExecuteScalar();
			return _count == 1;
		}

    public bool Debit(int pRetailAcctId, decimal pAmount, short pMinutes) {
			string _sqlStr = "UPDATE RetailAccount " + 
				"SET [" + RetailAccountRow.current_balance_DbName + "] = [" + RetailAccountRow.current_balance_DbName + "] - @DebitAmount " + 
				", [" + RetailAccountRow.current_bonus_minutes_DbName + "] = [" + RetailAccountRow.current_bonus_minutes_DbName + "] - @DebitMinutes " + 
				"WHERE [" + RetailAccountRow.retail_acct_id_DbName + "] = " + 
				base.Database.CreateSqlParameterName(RetailAccountRow.retail_acct_id_PropName) + " " + 
				"SELECT @@ROWCOUNT ";
			IDbCommand _cmd = base.Database.CreateCommand(_sqlStr);
			AddParameter(_cmd, RetailAccountRow.retail_acct_id_PropName, pRetailAcctId);
			base.Database.AddParameter(_cmd, "DebitAmount", DbType.Decimal, pAmount);
			base.Database.AddParameter(_cmd, "DebitMinutes", DbType.Int16, pMinutes);
			int _count = (int) _cmd.ExecuteScalar();
			return _count == 1;
		}

    public bool UpdateStatus(int pRetailAcctId, Status pStatus) {
      string _sqlStr = "UPDATE RetailAccount " +
        " SET [" + RetailAccountRow.status_DbName + "] = " + (byte) pStatus + " " +
        " WHERE [" + RetailAccountRow.retail_acct_id_DbName + "] = " +
        Database.CreateSqlParameterName(RetailAccountRow.retail_acct_id_PropName) + " " +
        " SELECT @@ROWCOUNT ";
      IDbCommand _cmd = Database.CreateCommand(_sqlStr);
      AddParameter(_cmd, RetailAccountRow.retail_acct_id_PropName, pRetailAcctId);
      int _count = (int) _cmd.ExecuteScalar();
      return _count == 1;
    }

		public int ActivatePhoneCard(DateTime pActiveDate, long pFirstSerial, long pLastSerial) {
      string _sqlStr = "UPDATE RetailAccount " +
        " SET [" + RetailAccountRow.status_DbName + "] = " + (byte) Status.Active + ", " +
        " [" + RetailAccountRow.date_active_DbName + "] = " + Database.CreateSqlParameterName(RetailAccountRow.date_active_PropName) + " " +
        " WHERE " + RetailAccountRow.retail_acct_id_DbName + " IN " + 
        "( " + 
        "     SELECT " + PhoneCardRow.retail_acct_id_DbName + " FROM PhoneCard " + 
        "     WHERE [" + PhoneCardRow.serial_number_DbName + "] " +
        "     BETWEEN " + pFirstSerial + " AND " + pLastSerial + " " +
        ")" + 
        " SELECT @@ROWCOUNT ";
      IDbCommand _cmd = base.Database.CreateCommand(_sqlStr);
      AddParameter(_cmd, RetailAccountRow.date_active_PropName, pActiveDate);
      int _count = (int) _cmd.ExecuteScalar();
      return _count;
    }

		public int GetCountByCustomer_acct_id(short pCustomerAcctId){
			string _sqlStr = "SELECT COUNT([" + RetailAccountRow.customer_acct_id_DbName + "]) FROM RetailAccount WHERE " + 
				"[" + RetailAccountRow.customer_acct_id_DbName + "]=" + base.Database.CreateSqlParameterName(RetailAccountRow.customer_acct_id_PropName);

			IDbCommand _cmd = base.Database.CreateCommand(_sqlStr);
			AddParameter(_cmd, RetailAccountRow.customer_acct_id_PropName, pCustomerAcctId);
			return ((int) _cmd.ExecuteScalar());
		}
	}// End of RetailAccountCollection class
} // End of namespace

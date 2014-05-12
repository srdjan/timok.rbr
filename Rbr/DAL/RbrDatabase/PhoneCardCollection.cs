// <fileinfo name="PhoneCardCollection.cs">
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
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using Timok.Rbr.Core.Config;
using Timok.Rbr.DAL.RbrDatabase.Base;

namespace Timok.Rbr.DAL.RbrDatabase {
	/// <summary>
	/// Represents the <c>PhoneCard</c> table.
	/// </summary>
	public class PhoneCardCollection : PhoneCardCollection_Base {
		/// <summary>
		/// Initializes a new instance of the <see cref="PhoneCardCollection"/> class.
		/// </summary>
		/// <param name="db">The database object.</param>
		internal PhoneCardCollection(Rbr_Db db) : base(db) {
			// EMPTY
		}

		public static PhoneCardRow Parse(DataRow row) {
			return new PhoneCardCollection(null).MapRow(row);
		}

		public override void Insert(PhoneCardRow value) {
			try {
				base.Insert(value);
			}
			catch (SqlException _sqlex) {
				if (_sqlex.Message.IndexOf("Violation of PRIMARY KEY constraint") > -1 || _sqlex.Message.IndexOf("Cannot insert duplicate key row in object") > -1) {
					return; //return and continue, PIN already loaded
				}
				throw new Exception("Unexpected Exception:\r\n", _sqlex);
			}
		}

		public bool UpdateStatus(short pServiceId, long pPIN, Status pStatus) {
			string _sqlStr = "UPDATE PhoneCard " + "SET [" + PhoneCardRow.status_DbName + "] = " + (byte) pStatus + " " + "WHERE [" + PhoneCardRow.service_id_DbName + "] = " + Database.CreateSqlParameterName(PhoneCardRow.service_id_PropName) + " " + "AND [" + PhoneCardRow.pin_DbName + "] = " + Database.CreateSqlParameterName(PhoneCardRow.pin_PropName) + " " + "SELECT @@ROWCOUNT ";
			IDbCommand _cmd = Database.CreateCommand(_sqlStr);
			AddParameter(_cmd, PhoneCardRow.service_id_PropName, pServiceId);
			AddParameter(_cmd, PhoneCardRow.pin_PropName, pPIN);
			int _count = (int) _cmd.ExecuteScalar();
			return _count == 1;
		}

		public bool UpdateLastUsed(short pServiceId, long pPIN) {
			return UpdateLastUsed(pServiceId, pPIN, DateTime.Today);
		}

		public bool UpdateFirstUsed(short pServiceId, long pPIN, DateTime pDate) {
			string _sqlStr = "UPDATE PhoneCard " + " SET [" + PhoneCardRow.date_first_used_DbName + "] = " + Database.CreateSqlParameterName(PhoneCardRow.date_first_used_PropName) + " " + " WHERE [" + PhoneCardRow.service_id_DbName + "] = " + Database.CreateSqlParameterName(PhoneCardRow.service_id_PropName) + " " + " AND [" + PhoneCardRow.pin_DbName + "] = " + Database.CreateSqlParameterName(PhoneCardRow.pin_PropName) + " " + " SELECT @@ROWCOUNT ";
			IDbCommand _cmd = Database.CreateCommand(_sqlStr);
			AddParameter(_cmd, PhoneCardRow.service_id_PropName, pServiceId);
			AddParameter(_cmd, PhoneCardRow.pin_PropName, pPIN);
			AddParameter(_cmd, PhoneCardRow.date_first_used_PropName, pDate);
			int _count = (int) _cmd.ExecuteScalar();
			return _count == 1;
		}

		public bool UpdateLastUsed(short pServiceId, long pPIN, DateTime pDate) {
			string _sqlStr = "UPDATE PhoneCard " + " SET [" + PhoneCardRow.date_last_used_DbName + "] = " + Database.CreateSqlParameterName(PhoneCardRow.date_last_used_PropName) + " " + " WHERE [" + PhoneCardRow.service_id_DbName + "] = " + Database.CreateSqlParameterName(PhoneCardRow.service_id_PropName) + " " + " AND [" + PhoneCardRow.pin_DbName + "] = " + Database.CreateSqlParameterName(PhoneCardRow.pin_PropName) + " " + " SELECT @@ROWCOUNT ";
			IDbCommand _cmd = Database.CreateCommand(_sqlStr);
			AddParameter(_cmd, PhoneCardRow.service_id_PropName, pServiceId);
			AddParameter(_cmd, PhoneCardRow.pin_PropName, pPIN);
			AddParameter(_cmd, PhoneCardRow.date_last_used_PropName, pDate);
			int _count = (int) _cmd.ExecuteScalar();
			return _count == 1;
		}

		public bool UpdateFirstUsedAndLastUsed(short pServiceId, long pPIN) {
			return UpdateFirstUsedAndLastUsed(pServiceId, pPIN, DateTime.Today, DateTime.Today);
		}

		public bool UpdateFirstUsedAndLastUsed(short pServiceId, long pPIN, DateTime pFirstUsed, DateTime pLastUsed) {
			string _sqlStr = "UPDATE PhoneCard " + " SET [" + PhoneCardRow.date_first_used_DbName + "] = " + Database.CreateSqlParameterName(PhoneCardRow.date_first_used_PropName) + " " + " , " + " [" + PhoneCardRow.date_last_used_DbName + "] = " + Database.CreateSqlParameterName(PhoneCardRow.date_last_used_PropName) + " " + " WHERE [" + PhoneCardRow.service_id_DbName + "] = " + Database.CreateSqlParameterName(PhoneCardRow.service_id_PropName) + " " + " AND [" + PhoneCardRow.pin_DbName + "] = " + Database.CreateSqlParameterName(PhoneCardRow.pin_PropName) + " " + " SELECT @@ROWCOUNT ";
			IDbCommand _cmd = Database.CreateCommand(_sqlStr);
			AddParameter(_cmd, PhoneCardRow.service_id_PropName, pServiceId);
			AddParameter(_cmd, PhoneCardRow.pin_PropName, pPIN);
			AddParameter(_cmd, PhoneCardRow.date_first_used_PropName, pFirstUsed);
			AddParameter(_cmd, PhoneCardRow.date_last_used_PropName, pLastUsed);
			int _count = (int) _cmd.ExecuteScalar();
			return _count == 1;
		}

		public PhoneCardRow GetBySerialNumber(short pServiceId, long pSerialNumber) {
			string _where = "[" + PhoneCardRow.service_id_DbName + "]=" + Database.CreateSqlParameterName("Service_id") + " AND " +
								"[" + PhoneCardRow.serial_number_DbName + "]=" + Database.CreateSqlParameterName(PhoneCardRow.serial_number_PropName);

			IDbCommand _cmd = base.CreateGetCommand(_where, null);
			AddParameter(_cmd, PhoneCardRow.service_id_PropName, pServiceId);
			AddParameter(_cmd, PhoneCardRow.serial_number_PropName, pSerialNumber);
			using (IDataReader _reader = _cmd.ExecuteReader()) {
				PhoneCardRow[] _tempArray = MapRecords(_reader);
				return (_tempArray.Length > 0) ? _tempArray[0] : null;
			}
		}

		public int GetCountByServiceID(short pServiceId) {
			string _sqlStr = "SELECT COUNT([" + PhoneCardRow.service_id_DbName + "]) FROM PhoneCard WHERE " + "[" + PhoneCardRow.service_id_DbName + "]=" + base.Database.CreateSqlParameterName(PhoneCardRow.service_id_PropName);

			IDbCommand _cmd = Database.CreateCommand(_sqlStr);
			AddParameter(_cmd, PhoneCardRow.service_id_PropName, pServiceId);
			return ((int) _cmd.ExecuteScalar());
		}

		public bool ExistsByCardNumber(short pServiceId, long pPIN) {
			string _sqlStr = "SELECT COUNT([" + PhoneCardRow.pin_DbName + "]) FROM PhoneCard WHERE " + "[" + PhoneCardRow.service_id_DbName + "]=" + base.Database.CreateSqlParameterName(PhoneCardRow.service_id_PropName) + " AND " + "[" + PhoneCardRow.pin_DbName + "]=" + base.Database.CreateSqlParameterName(PhoneCardRow.pin_PropName);

			IDbCommand _cmd = Database.CreateCommand(_sqlStr);
			AddParameter(_cmd, PhoneCardRow.service_id_PropName, pServiceId);
			AddParameter(_cmd, PhoneCardRow.pin_PropName, pPIN);
			int _res = (int) _cmd.ExecuteScalar();
			return _res > 0;
		}

		public bool ExistsBySerialNumber(short pServiceId, long pSerialNumber) {
			string _sqlStr = "SELECT COUNT([" + PhoneCardRow.serial_number_DbName + "]) FROM PhoneCard WHERE " + "[" + PhoneCardRow.service_id_DbName + "]=" + Database.CreateSqlParameterName("Service_id") + " AND " +
								"[" + PhoneCardRow.serial_number_DbName + "]=" + Database.CreateSqlParameterName(PhoneCardRow.serial_number_PropName);

			IDbCommand _cmd = Database.CreateCommand(_sqlStr);
			AddParameter(_cmd, PhoneCardRow.service_id_PropName, pServiceId);
			AddParameter(_cmd, PhoneCardRow.serial_number_PropName, pSerialNumber);
			int _res = (int) _cmd.ExecuteScalar();
			return _res > 0;
		}

		public int GetCountByFirstUsed(DateTime pDateTime) {
			string _sqlStr = "SELECT COUNT([" + PhoneCardRow.serial_number_DbName + "]) FROM PhoneCard WHERE " + "[" + PhoneCardRow.date_first_used_DbName + "]=" + base.Database.CreateSqlParameterName(PhoneCardRow.date_first_used_PropName);

			IDbCommand _cmd = base.Database.CreateCommand(_sqlStr);
			AddParameter(_cmd, PhoneCardRow.date_first_used_PropName, new DateTime(pDateTime.Year, pDateTime.Month, pDateTime.Day));
			return (int) _cmd.ExecuteScalar();
		}

		public int GetCountByExpired(DateTime pDateTime) {
			string _sqlStr = "SELECT COUNT(" + PhoneCardRow.serial_number_DbName + ") FROM PhoneCard WHERE " + PhoneCardRow.date_to_expire_DbName + "=" + base.Database.CreateSqlParameterName(PhoneCardRow.date_to_expire_PropName);

			IDbCommand _cmd = base.Database.CreateCommand(_sqlStr);
			AddParameter(_cmd, PhoneCardRow.date_to_expire_PropName, new DateTime(pDateTime.Year, pDateTime.Month, pDateTime.Day));
			return (int) _cmd.ExecuteScalar();
		}

		public int GetCountByTotalUsed(DateTime pDateTime) {
			string _sqlStr = "SELECT COUNT([" + PhoneCardRow.serial_number_DbName + "]) FROM PhoneCard WHERE " + "[" + PhoneCardRow.date_last_used_DbName + "]=" + base.Database.CreateSqlParameterName(PhoneCardRow.date_last_used_PropName);

			IDbCommand _cmd = base.Database.CreateCommand(_sqlStr);
			AddParameter(_cmd, PhoneCardRow.date_last_used_PropName, new DateTime(pDateTime.Year, pDateTime.Month, pDateTime.Day));
			return (int) _cmd.ExecuteScalar();
		}

		public int GetCountByDepleted(DateTime pDateTime) {
			string _sqlStr = "SELECT COUNT(" + PhoneCardRow.serial_number_DbName + ") FROM PhoneCard WHERE " + PhoneCardRow.date_last_used_DbName + "=" + base.Database.CreateSqlParameterName(PhoneCardRow.date_last_used_PropName) + " AND " + PhoneCardRow.retail_acct_id_DbName + " in " + " (SELECT " + RetailAccountRow.retail_acct_id_DbName + " FROM RetailAccount WHERE " + RetailAccountRow.current_balance_DbName + "<=0 " + " AND " + RetailAccountRow.current_bonus_minutes_DbName + "<=0)";

			IDbCommand _cmd = base.Database.CreateCommand(_sqlStr);
			AddParameter(_cmd, PhoneCardRow.date_last_used_PropName, new DateTime(pDateTime.Year, pDateTime.Month, pDateTime.Day));
			return (int) _cmd.ExecuteScalar();
		}

		public int[] Export(InventoryStatus pStatus, long pFirstSerial, long pLastSerial) {
			string _sqlStr = "UPDATE PhoneCard " + " SET [" + PhoneCardRow.inventory_status_DbName + "] = " + (byte)InventoryStatus.Archived + ", " + " [" + PhoneCardRow.status_DbName + "] = " + (byte)Status.Archived + ", " + " [" + PhoneCardRow.date_archived_DbName + "] = " + Database.CreateSqlParameterName(PhoneCardRow.date_archived_PropName) + " " + " WHERE [" + PhoneCardRow.serial_number_DbName + "] " + " BETWEEN " + pFirstSerial + " AND " + pLastSerial + " " + " SELECT @@ROWCOUNT ";
			IDbCommand _cmd = base.Database.CreateCommand(_sqlStr);
			AddParameter(_cmd, PhoneCardRow.date_archived_PropName, DateTime.Today);

			PhoneCardRow[] _tempArray = null;
			using (IDataReader _reader = _cmd.ExecuteReader()) {
				_tempArray = MapRecords(_reader);
			}

			var _ids = new List<int>();
			foreach (var _phoneCardRow in _tempArray) {
				_ids.Add(_phoneCardRow.Retail_acct_id);
			}
			return _ids.ToArray();
		}

		public int ActivateInventory(DateTime pDateTime, long pFirstSerial, long pLastSerial) {
			string _sqlStr = "UPDATE PhoneCard " + " SET [" + PhoneCardRow.inventory_status_DbName + "] = " + (byte) InventoryStatus.Activated + ", " + " [" + PhoneCardRow.status_DbName + "] = " + (byte) Status.Active + ", " + " [" + PhoneCardRow.date_active_DbName + "] = " + Database.CreateSqlParameterName(PhoneCardRow.date_active_PropName) + " " + " WHERE [" + PhoneCardRow.serial_number_DbName + "] " + " BETWEEN " + pFirstSerial + " AND " + pLastSerial + " " + " SELECT @@ROWCOUNT ";
			IDbCommand _cmd = base.Database.CreateCommand(_sqlStr);
			AddParameter(_cmd, PhoneCardRow.date_active_PropName, new DateTime(pDateTime.Year, pDateTime.Month, pDateTime.Day));
			int _count = (int) _cmd.ExecuteScalar();
			return _count;
		}

		public int DeactivateInventory(long pFirstSerial, long pLastSerial) {
			string _sqlStr = "UPDATE PhoneCard " + " SET [" + PhoneCardRow.inventory_status_DbName + "] = " + (byte) InventoryStatus.Deactivated + ", " + " [" + PhoneCardRow.status_DbName + "] = " + (byte) Status.Pending + ", " + " [" + PhoneCardRow.date_deactivated_DbName + "] = " + Database.CreateSqlParameterName(PhoneCardRow.date_deactivated_PropName) + " " + " WHERE [" + PhoneCardRow.serial_number_DbName + "] " + " BETWEEN " + pFirstSerial + " AND " + pLastSerial + " " + " SELECT @@ROWCOUNT ";
			IDbCommand _cmd = base.Database.CreateCommand(_sqlStr);
			AddParameter(_cmd, PhoneCardRow.date_deactivated_PropName, DateTime.Today);
			int _count = (int) _cmd.ExecuteScalar();
			return _count;
		}

		public int ArchiveInventory(long pFirstSerial, long pLastSerial) {
			string _sqlStr = "UPDATE PhoneCard " + " SET [" + PhoneCardRow.inventory_status_DbName + "] = " + (byte) InventoryStatus.Archived + ", " + " [" + PhoneCardRow.status_DbName + "] = " + (byte) Status.Archived + ", " + " [" + PhoneCardRow.date_archived_DbName + "] = " + Database.CreateSqlParameterName(PhoneCardRow.date_archived_PropName) + " " + " WHERE [" + PhoneCardRow.serial_number_DbName + "] " + " BETWEEN " + pFirstSerial + " AND " + pLastSerial + " " + " SELECT @@ROWCOUNT ";
			IDbCommand _cmd = base.Database.CreateCommand(_sqlStr);
			AddParameter(_cmd, PhoneCardRow.date_archived_PropName, DateTime.Today);
			int _count = (int) _cmd.ExecuteScalar();
			return _count;
		}

		public PhoneCardRow[] GetFirstUnusedActiveByDenomination(short pCustomerAcctId, decimal pDenomination, int pCount) {
			/*
      SELECT TOP 1 * FROM PhoneCard INNER JOIN 
      RetailAccount ON 
      PhoneCard.retail_acct_id = RetailAccount.retail_acct_id
      WHERE 
      RetailAccount.customer_acct_id = 10000
      AND 
      RetailAccount.start_balance = 5 
      AND 
      PhoneCard.status = 1 
      AND 
      PhoneCard.date_first_used IS NULL 
       */
			string _sqlStr = "SELECT TOP " + pCount + " * FROM PhoneCard INNER JOIN " + " RetailAccount ON " + " PhoneCard." + PhoneCardRow.retail_acct_id_DbName + " = RetailAccount." + RetailAccountRow.retail_acct_id_DbName + "" + " WHERE " + "RetailAccount." + RetailAccountRow.customer_acct_id_DbName + " = " + Database.CreateSqlParameterName(RetailAccountRow.customer_acct_id_PropName) + " AND " + "RetailAccount." + RetailAccountRow.status_DbName + " = " + (byte) Status.Active + " AND " + "RetailAccount." + RetailAccountRow.start_balance_DbName + " = " + Database.CreateSqlParameterName(RetailAccountRow.start_balance_PropName) + " AND " + "PhoneCard." + PhoneCardRow.status_DbName + " = " + (byte) Status.Active + " AND " + "PhoneCard." + PhoneCardRow.date_first_used_DbName + " IS NULL";

			IDbCommand _cmd = base.Database.CreateCommand(_sqlStr);
			Database.AddParameter(_cmd, RetailAccountRow.customer_acct_id_PropName, DbType.Int16, pCustomerAcctId);
			Database.AddParameter(_cmd, RetailAccountRow.start_balance_PropName, DbType.Decimal, pDenomination);
			PhoneCardRow[] _tempArray = null;
			using (IDataReader _reader = _cmd.ExecuteReader()) {
				_tempArray = MapRecords(_reader);
			}
			return _tempArray;
		}
	} // End of PhoneCardCollection class
} // End of namespace
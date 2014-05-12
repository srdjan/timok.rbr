// <fileinfo name="CustomerAcctCollection.cs">
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
	/// Represents the <c>CustomerAcct</c> table.
	/// </summary>
	public class CustomerAcctCollection : CustomerAcctCollection_Base {
		/// <summary>
		/// Initializes a new instance of the <see cref="CustomerAcctCollection"/> class.
		/// </summary>
		/// <param name="db">The database object.</param>
		internal CustomerAcctCollection(Rbr_Db db)
			: base(db) {
			// EMPTY
		}

		public static CustomerAcctRow Parse(System.Data.DataRow row) {
			return new CustomerAcctCollection(null).MapRow(row);
		}

		public CustomerAcctRow[] GetAllResellAcctsByPartnerId(int pPartnerId) {
			/*
			SELECT *
			FROM  CustomerAcct 
			INNER JOIN ResellAcct ON CustomerAcct.customer_acct_id = ResellAcct.customer_acct_id
			INNER JOIN Partner ON ResellAcct.partner_id = Partner.partner_id
			WHERE (Partner.partner_id = 1245)
			 */
			string _sqlStr = "SELECT * FROM CustomerAcct " +
				"INNER JOIN ResellAcct ON CustomerAcct." + CustomerAcctRow.customer_acct_id_DbName + " = ResellAcct." + ResellAcctRow.customer_acct_id_DbName + " " +
				"INNER JOIN Partner ON ResellAcct." + ResellAcctRow.partner_id_DbName + " = Partner." + PartnerRow.partner_id_DbName + " " +
				"WHERE Partner." + PartnerRow.partner_id_DbName + "=" + Database.CreateSqlParameterName(ResellAcctRow.partner_id_PropName);

			IDbCommand _cmd = Database.CreateCommand(_sqlStr);
			Database.AddParameter(_cmd, ResellAcctRow.partner_id_PropName, DbType.Int32, pPartnerId);
			using (IDataReader _reader = _cmd.ExecuteReader()) {
				return MapRecords(_reader);
			}
		}


		public CustomerAcctRow[] GetReselled() {
			/*
			SELECT *
			FROM  CustomerAcct 
			WHERE customer_acct_id IN (
				SELECT customer_acct_id FROM ResellAcct 
			)
			 */
			string _where = "[" + CustomerAcctRow.customer_acct_id_DbName + "] IN " +
				" ( " +
				" SELECT " + ResellAcctRow.customer_acct_id_DbName + " FROM ResellAcct " +
				" )";

			IDbCommand _cmd = CreateGetCommand(_where, CustomerAcctRow.name_DbName);
			using (IDataReader _reader = _cmd.ExecuteReader()) {
				return MapRecords(_reader);
			}
		}

		public CustomerAcctRow[] GetAllAcctsByPartnerId(int pPartnerId) {
			/*
			SELECT *
			FROM  CustomerAcct 
			INNER JOIN ResellAcct ON CustomerAcct.customer_acct_id = ResellAcct.customer_acct_id
			INNER JOIN Partner ON ResellAcct.partner_id = Partner.partner_id
			WHERE (Partner.partner_id = 1245)
			 */
			string _sqlStr = "SELECT * FROM CustomerAcct " +
				"INNER JOIN ResellAcct ON CustomerAcct." + CustomerAcctRow.customer_acct_id_DbName + " = ResellAcct." + ResellAcctRow.customer_acct_id_DbName + " " +
				"INNER JOIN Partner ON ResellAcct." + ResellAcctRow.partner_id_DbName + " = Partner." + PartnerRow.partner_id_DbName + " " +
				"WHERE Partner." + PartnerRow.partner_id_DbName + "=" + Database.CreateSqlParameterName(ResellAcctRow.partner_id_PropName);

			IDbCommand _cmd = Database.CreateCommand(_sqlStr);
			Database.AddParameter(_cmd, ResellAcctRow.partner_id_PropName, DbType.Int32, pPartnerId);
			using (IDataReader _reader = _cmd.ExecuteReader()) {
				return MapRecords(_reader);
			}
		}

		public CustomerAcctRow[] GetResellAcctsByPartnerId(int pPartnerId) {
			/*
			SELECT *
			FROM  CustomerAcct 
			WHERE customer_acct_id IN (
				SELECT customer_acct_id FROM ResellAcct 
				WHERE partner_id = 1000
			)
			 */
			string _where = "[" + CustomerAcctRow.customer_acct_id_DbName + "] IN " +
				" ( " +
				" SELECT " + ResellAcctRow.customer_acct_id_DbName + " FROM ResellAcct " +
				" WHERE " + ResellAcctRow.partner_id_DbName + "=" + Database.CreateSqlParameterName(ResellAcctRow.partner_id_PropName) +
				" )";

			IDbCommand _cmd = CreateGetCommand(_where, CustomerAcctRow.name_DbName);
			Database.AddParameter(_cmd, ResellAcctRow.partner_id_PropName, DbType.Int32, pPartnerId);
			using (IDataReader _reader = _cmd.ExecuteReader()) {
				return MapRecords(_reader);
			}
		}

		public CustomerAcctRow[] GetReselledByPersonId(int pPersonId) {
			/*
			SELECT *
			FROM  CustomerAcct 
			WHERE customer_acct_id IN (
				SELECT customer_acct_id FROM ResellAcct 
				WHERE person_id = 1000
			)
			 */
			string _where = "[" + CustomerAcctRow.customer_acct_id_DbName + "] IN " +
				" ( " +
				" SELECT " + ResellAcctRow.customer_acct_id_DbName + " FROM ResellAcct " +
				" WHERE " + ResellAcctRow.person_id_DbName + "=" + Database.CreateSqlParameterName(ResellAcctRow.person_id_PropName) +
				" )";

			IDbCommand _cmd = CreateGetCommand(_where, CustomerAcctRow.name_DbName);
			Database.AddParameter(_cmd, ResellAcctRow.person_id_PropName, DbType.Int32, pPersonId);
			using (IDataReader _reader = _cmd.ExecuteReader()) {
				return MapRecords(_reader);
			}
		}

		public bool UpdateStatus(short Customer_acct_id, byte status) {
			string sqlStr = "UPDATE CustomerAcct " +
				"SET [" + CustomerAcctRow.status_DbName + "] = " + status + " " +
				"WHERE [" + CustomerAcctRow.customer_acct_id_DbName + "] = " +
				base.Database.CreateSqlParameterName(CustomerAcctRow.customer_acct_id_PropName) + " " +
				"SELECT @@ROWCOUNT ";
			IDbCommand cmd = base.Database.CreateCommand(sqlStr);
			AddParameter(cmd, CustomerAcctRow.customer_acct_id_PropName, Customer_acct_id);
			int _count = (int)cmd.ExecuteScalar();
			return _count == 1;
		}


		public CustomerAcctRow[] GetUnmappedByVendorId(int pVendorId) {
			/*
			SELECT CustomerAcct.*
			FROM  CustomerAcct INNER JOIN
			Service ON CustomerAcct.service_id = Service.service_id
			WHERE (CustomerAcct.customer_acct_id NOT IN
						 (SELECT customer_acct_id FROM CustomerAcctSupportMap
							WHERE vendor_id = 1058
							)
						) 
			AND Service.type = 2 -- Retail
			 */

			string _sqlStr = "SELECT CustomerAcct.* FROM  CustomerAcct INNER JOIN Service ON " +
				" CustomerAcct." + CustomerAcctRow.service_id_DbName + " = Service." + ServiceRow.service_id_DbName + " " +
				" WHERE (" + CustomerAcctRow.customer_acct_id_DbName + " NOT IN " +
				" (SELECT " + CustomerAcctSupportMapRow.customer_acct_id_DbName + " " +
				" FROM CustomerAcctSupportMap  WHERE " +
				" " + CustomerAcctSupportMapRow.vendor_id_DbName + " = " + Database.CreateSqlParameterName(CustomerAcctSupportMapRow.vendor_id_PropName) +
				")) AND " +
				" Service." + ServiceRow.type_DbName + " = " + (byte)ServiceType.Retail + " " +
				" ORDER BY " + CustomerAcctRow.name_DbName;

			IDbCommand _cmd = Database.CreateCommand(_sqlStr);
			Database.AddParameter(_cmd, CustomerAcctSupportMapRow.vendor_id_PropName, DbType.Int32, pVendorId);
			using (IDataReader _reader = _cmd.ExecuteReader()) {
				return MapRecords(_reader);
			}
		}

		public CustomerAcctRow[] GetByRetailType(RetailType[] pRetailTypes) {
			if (pRetailTypes == null || pRetailTypes.Length == 0) {
				throw new ArgumentException("pRetailTypes is empty, at least one value is required");
			}

			string _retailTypeFilter = Database.CreateEnumFilter("Service",
				ServiceRow.retail_type_DbName, pRetailTypes, typeof(RetailType));

			/*
					 SELECT *
					 FROM  CustomerAcct 
					 WHERE service_id IN (
							SELECT service_id
							FROM   Service
							WHERE Service.retail_type IN (1,2)
						)
			*/

			string _sqlStr = "SELECT * FROM  CustomerAcct " +
				" WHERE " + CustomerAcctRow.service_id_DbName + " IN " +
				" ( " +
				"    SELECT " + ServiceRow.service_id_DbName + " " +
				"    FROM Service  WHERE " + _retailTypeFilter + " " +
				" ) " +
				" ORDER BY " + CustomerAcctRow.name_DbName;

			IDbCommand _cmd = Database.CreateCommand(_sqlStr);
			using (IDataReader _reader = _cmd.ExecuteReader()) {
				return MapRecords(_reader);
			}
		}

		public int GetCountByServiceId(short pServiceId) {
			string _sqlStr = "SELECT COUNT(*) FROM [CustomerAcct] WHERE " +
				"[" + CustomerAcctRow.service_id_DbName + "]=" + Database.CreateSqlParameterName(CustomerAcctRow.service_id_PropName);

			IDbCommand _cmd = Database.CreateCommand(_sqlStr);
			AddParameter(_cmd, CustomerAcctRow.service_id_PropName, pServiceId);
			return (int)_cmd.ExecuteScalar();
		}

		public int GetCountByRoutingPlanId(int pRoutingPlanId) {
			string _sqlStr = "SELECT COUNT(*) FROM CustomerAcct WHERE " +
				"[" + CustomerAcctRow.routing_plan_id_DbName + "]=" + Database.CreateSqlParameterName(CustomerAcctRow.routing_plan_id_PropName);

			IDbCommand _cmd = Database.CreateCommand(_sqlStr);
			AddParameter(_cmd, CustomerAcctRow.routing_plan_id_PropName, pRoutingPlanId);
			int _res = (int)_cmd.ExecuteScalar();
			return _res;
		}

		public int GetCountByRetailCallingPlanId(int pRetailCallingPlanId) {
			string _sqlStr = "SELECT COUNT(*) FROM CustomerAcct WHERE " +
				"[" + CustomerAcctRow.retail_calling_plan_id_DbName + "]=" + Database.CreateSqlParameterName(CustomerAcctRow.retail_calling_plan_id_PropName);

			IDbCommand _cmd = base.Database.CreateCommand(_sqlStr);
			AddParameter(_cmd, CustomerAcctRow.retail_calling_plan_id_PropName, pRetailCallingPlanId);
			int _res = (int)_cmd.ExecuteScalar();
			return _res;
		}

		//		public CustomerAcctRow[] GetByAccount_type_And_Prefix_in_type_id(AccountType pAccountType, short prefix_in_type_id) {
		//			string _where = "[" + CustomerAcctRow.account_type_DbName + "]=" + base.Database.CreateSqlParameterName(CustomerAcctRow.account_type_PropName);
		//			_where += " AND ";
		//			_where += "[" + CustomerAcctRow.prefix_in_type_id_DbName + "]=" + base.Database.CreateSqlParameterName(CustomerAcctRow.prefix_in_type_id_PropName);
		//			IDbCommand cmd = CreateGetCommand(_where, CustomerAcctRow.name_DbName);
		//			AddParameter(cmd, CustomerAcctRow.account_type_PropName, (byte) pAccountType);
		//			AddParameter(cmd, CustomerAcctRow.prefix_in_type_id_PropName, prefix_in_type_id);
		//			using(IDataReader reader = cmd.ExecuteReader()) {
		//				return MapRecords(reader);
		//			}
		//		}

		public CustomerAcctRow[] GetByPartner_id_Prefix_in_type_id(int partner_id, short prefix_in_type_id) {
			string _where = "[" + CustomerAcctRow.partner_id_DbName + "]=" + Database.CreateSqlParameterName(CustomerAcctRow.partner_id_PropName);
			_where += " AND ";
			_where += "[" + CustomerAcctRow.prefix_in_type_id_DbName + "]=" + Database.CreateSqlParameterName(CustomerAcctRow.prefix_in_type_id_PropName);

			IDbCommand _cmd = CreateGetCommand(_where, CustomerAcctRow.name_DbName);
			AddParameter(_cmd, CustomerAcctRow.partner_id_PropName, partner_id);
			AddParameter(_cmd, CustomerAcctRow.prefix_in_type_id_PropName, prefix_in_type_id);
			using (IDataReader reader = _cmd.ExecuteReader()) {
				return MapRecords(reader);
			}
		}

		protected override IDbCommand CreateGetAllCommand() {
			return CreateGetCommand(null, CustomerAcctRow.name_DbName);
		}

		protected override IDbCommand CreateGetByRouting_plan_idCommand(int routing_plan_id) {
			string _whereSql = "[" + CustomerAcctRow.routing_plan_id_DbName + "]=" + Database.CreateSqlParameterName(CustomerAcctRow.routing_plan_id_PropName);

			IDbCommand _cmd = CreateGetCommand(_whereSql, CustomerAcctRow.name_DbName);
			AddParameter(_cmd, CustomerAcctRow.routing_plan_id_PropName, routing_plan_id);
			return _cmd;
		}

		protected override IDbCommand CreateGetByPartner_idCommand(int partner_id) {
			string _whereSql = "[" + CustomerAcctRow.partner_id_DbName + "]=" + base.Database.CreateSqlParameterName(CustomerAcctRow.partner_id_PropName);

			IDbCommand _cmd = CreateGetCommand(_whereSql, CustomerAcctRow.name_DbName);
			AddParameter(_cmd, CustomerAcctRow.partner_id_PropName, partner_id);
			return _cmd;
		}

		protected override IDbCommand CreateGetByService_idCommand(short pServiceId) {
			string _whereSql = "[" + CustomerAcctRow.service_id_DbName + "]=" + base.Database.CreateSqlParameterName(CustomerAcctRow.service_id_PropName);

			IDbCommand _cmd = CreateGetCommand(_whereSql, CustomerAcctRow.name_DbName);
			AddParameter(_cmd, CustomerAcctRow.service_id_PropName, pServiceId);
			return _cmd;
		}

		public bool IsNameInUseByOtherCustomerAcct(string pName, short pCustomerAcctID) {
			string sqlStr = "SELECT COUNT(*) FROM [dbo].[CustomerAcct] WHERE " +
				"[" + CustomerAcctRow.name_DbName + "]=" + base.Database.CreateSqlParameterName(CustomerAcctRow.name_PropName) +
				" AND " +
				"[" + CustomerAcctRow.customer_acct_id_DbName + "]<>" + base.Database.CreateSqlParameterName(CustomerAcctRow.customer_acct_id_PropName);

			IDbCommand _cmd = base.Database.CreateCommand(sqlStr);
			AddParameter(_cmd, CustomerAcctRow.name_PropName, pName);
			AddParameter(_cmd, CustomerAcctRow.customer_acct_id_PropName, pCustomerAcctID);

			int _count = (int)_cmd.ExecuteScalar();
			return _count > 0 ? true : false;
		}

		public bool IsPrefixInInUseByOtherCustomerAcct(string pPrefixIn, short pCustomerAcctID) {
			string sqlStr = "SELECT COUNT(*) FROM [dbo].[CustomerAcct] WHERE " +
				"[" + CustomerAcctRow.prefix_in_DbName + "]=" + base.Database.CreateSqlParameterName(CustomerAcctRow.prefix_in_PropName) +
				" AND " +
				"[" + CustomerAcctRow.customer_acct_id_DbName + "]<>" + base.Database.CreateSqlParameterName(CustomerAcctRow.customer_acct_id_PropName);

			IDbCommand _cmd = base.Database.CreateCommand(sqlStr);
			AddParameter(_cmd, CustomerAcctRow.prefix_in_PropName, pPrefixIn);
			AddParameter(_cmd, CustomerAcctRow.customer_acct_id_PropName, pCustomerAcctID);

			int _count = (int)_cmd.ExecuteScalar();
			return _count > 0 ? true : false;
		}

		public bool AdjustWarningAmount(short pCustomerAcctId, decimal pWarningLevel) {
			string _sqlStr = " UPDATE CustomerAcct " +
				" SET [" + CustomerAcctRow.warning_amount_DbName + "] = @WarningLevel" +
				" WHERE [" + CustomerAcctRow.customer_acct_id_DbName + "] = " + Database.CreateSqlParameterName(CustomerAcctRow.customer_acct_id_PropName) + " " +
				" SELECT @@ROWCOUNT ";
			IDbCommand _cmd = Database.CreateCommand(_sqlStr);
			AddParameter(_cmd, CustomerAcctRow.customer_acct_id_PropName, pCustomerAcctId);
			Database.AddParameter(_cmd, "WarningLevel", DbType.Decimal, pWarningLevel);
			int _count = (int)_cmd.ExecuteScalar();
			return _count > 0;
		}

		public bool Credit(short pCustomerAcctId, decimal pAmount) {
			string _sqlStr = "UPDATE CustomerAcct " +
				" SET [" + CustomerAcctRow.current_amount_DbName + "] = [" + CustomerAcctRow.current_amount_DbName + "] + @CreditAmount " +
				" WHERE [" + CustomerAcctRow.customer_acct_id_DbName + "] = " +
				base.Database.CreateSqlParameterName(CustomerAcctRow.customer_acct_id_PropName) + " " +
				" SELECT @@ROWCOUNT ";
			IDbCommand _cmd = base.Database.CreateCommand(_sqlStr);
			AddParameter(_cmd, CustomerAcctRow.customer_acct_id_PropName, pCustomerAcctId);
			base.Database.AddParameter(_cmd, "CreditAmount", DbType.Decimal, pAmount);
			int _count = (int)_cmd.ExecuteScalar();
			return _count == 1;
		}

		public bool Debit(short pCustomerAcctId, decimal pAmount) {
			string _sqlStr = "UPDATE CustomerAcct " +
				" SET [" + CustomerAcctRow.current_amount_DbName + "] = [" + CustomerAcctRow.current_amount_DbName + "] - @DebitAmount " +
				" WHERE [" + CustomerAcctRow.customer_acct_id_DbName + "] = " +
				base.Database.CreateSqlParameterName(CustomerAcctRow.customer_acct_id_PropName) + " " +
				" SELECT @@ROWCOUNT ";
			IDbCommand _cmd = base.Database.CreateCommand(_sqlStr);
			AddParameter(_cmd, CustomerAcctRow.customer_acct_id_PropName, pCustomerAcctId);
			base.Database.AddParameter(_cmd, "DebitAmount", DbType.Decimal, pAmount);
			int _count = (int)_cmd.ExecuteScalar();
			return _count == 1;
		}

		public override void Insert(CustomerAcctRow value) {
			string sqlStr = "DECLARE " +
				base.Database.CreateSqlParameterName(CustomerAcctRow.customer_acct_id_PropName) + " smallint " +
				" SET " +
				base.Database.CreateSqlParameterName(CustomerAcctRow.customer_acct_id_PropName) +
				" = COALESCE((SELECT MAX(" + CustomerAcctRow.customer_acct_id_DbName + ") FROM CustomerAcct) + 1, 10000) " +


				"INSERT INTO [dbo].[CustomerAcct] (" +
				"[" + CustomerAcctRow.customer_acct_id_DbName + "], " +
				"[" + CustomerAcctRow.name_DbName + "], " +
				"[" + CustomerAcctRow.status_DbName + "], " +
				"[" + CustomerAcctRow.default_bonus_minutes_type_DbName + "], " +
				"[" + CustomerAcctRow.default_start_bonus_minutes_DbName + "], " +
				"[" + CustomerAcctRow.is_prepaid_DbName + "], " +
				"[" + CustomerAcctRow.current_amount_DbName + "], " +
				"[" + CustomerAcctRow.limit_amount_DbName + "], " +
				"[" + CustomerAcctRow.warning_amount_DbName + "], " +
				"[" + CustomerAcctRow.allow_rerouting_DbName + "], " +
				"[" + CustomerAcctRow.concurrent_use_DbName + "], " +
				"[" + CustomerAcctRow.prefix_in_type_id_DbName + "], " +
				"[" + CustomerAcctRow.prefix_in_DbName + "], " +
				"[" + CustomerAcctRow.prefix_out_DbName + "], " +
				"[" + CustomerAcctRow.partner_id_DbName + "], " +
				"[" + CustomerAcctRow.service_id_DbName + "], " +
				"[" + CustomerAcctRow.retail_calling_plan_id_DbName + "], " +
				"[" + CustomerAcctRow.retail_markup_type_DbName + "], " +
				"[" + CustomerAcctRow.retail_markup_dollar_DbName + "], " +
				"[" + CustomerAcctRow.retail_markup_percent_DbName + "], " +
				"[" + CustomerAcctRow.max_call_length_DbName + "], " +
				"[" + CustomerAcctRow.routing_plan_id_DbName + "] " +
				") VALUES (" +
				Database.CreateSqlParameterName(CustomerAcctRow.customer_acct_id_PropName) + ", " +
				Database.CreateSqlParameterName(CustomerAcctRow.name_PropName) + ", " +
				Database.CreateSqlParameterName(CustomerAcctRow.status_PropName) + ", " +
				Database.CreateSqlParameterName(CustomerAcctRow.default_bonus_minutes_type_PropName) + ", " +
				Database.CreateSqlParameterName(CustomerAcctRow.default_start_bonus_minutes_PropName) + ", " +
				Database.CreateSqlParameterName(CustomerAcctRow.is_prepaid_PropName) + ", " +
				Database.CreateSqlParameterName(CustomerAcctRow.current_amount_PropName) + ", " +
				Database.CreateSqlParameterName(CustomerAcctRow.limit_amount_PropName) + ", " +
				Database.CreateSqlParameterName(CustomerAcctRow.warning_amount_PropName) + ", " +
				Database.CreateSqlParameterName(CustomerAcctRow.allow_rerouting_PropName) + ", " +
				Database.CreateSqlParameterName(CustomerAcctRow.concurrent_use_PropName) + ", " +
				Database.CreateSqlParameterName(CustomerAcctRow.prefix_in_type_id_PropName) + ", " +
				Database.CreateSqlParameterName(CustomerAcctRow.prefix_in_PropName) + ", " +
				Database.CreateSqlParameterName(CustomerAcctRow.prefix_out_PropName) + ", " +
				Database.CreateSqlParameterName(CustomerAcctRow.partner_id_PropName) + ", " +
				Database.CreateSqlParameterName(CustomerAcctRow.service_id_PropName) + ", " +
				Database.CreateSqlParameterName(CustomerAcctRow.retail_calling_plan_id_PropName) + ", " +
				Database.CreateSqlParameterName(CustomerAcctRow.retail_markup_type_PropName) + ", " +
				Database.CreateSqlParameterName(CustomerAcctRow.retail_markup_dollar_PropName) + ", " +
				Database.CreateSqlParameterName(CustomerAcctRow.retail_markup_percent_PropName) + ", " +
				Database.CreateSqlParameterName(CustomerAcctRow.max_call_length_PropName) + ", " +
				Database.CreateSqlParameterName(CustomerAcctRow.routing_plan_id_PropName) + ") " +
				" SELECT " + base.Database.CreateSqlParameterName(CustomerAcctRow.customer_acct_id_PropName) + " ";

			IDbCommand cmd = base.Database.CreateCommand(sqlStr);
			//AddParameter(cmd, CustomerAcctRow.customer_id_PropName, value.Customer_id);
			AddParameter(cmd, CustomerAcctRow.name_PropName, value.Name);
			AddParameter(cmd, CustomerAcctRow.status_PropName, value.Status);
			AddParameter(cmd, CustomerAcctRow.default_bonus_minutes_type_PropName, value.Default_bonus_minutes_type);
			AddParameter(cmd, CustomerAcctRow.default_start_bonus_minutes_PropName, value.Default_start_bonus_minutes);
			AddParameter(cmd, CustomerAcctRow.is_prepaid_PropName, value.Is_prepaid);
			AddParameter(cmd, CustomerAcctRow.current_amount_PropName, value.Current_amount);
			AddParameter(cmd, CustomerAcctRow.limit_amount_PropName, value.Limit_amount);
			AddParameter(cmd, CustomerAcctRow.warning_amount_PropName, value.Warning_amount);
			AddParameter(cmd, CustomerAcctRow.allow_rerouting_PropName, value.Allow_rerouting);
			AddParameter(cmd, CustomerAcctRow.concurrent_use_PropName, value.Concurrent_use);
			AddParameter(cmd, CustomerAcctRow.prefix_in_type_id_PropName, value.Prefix_in_type_id);
			AddParameter(cmd, CustomerAcctRow.prefix_in_PropName, value.Prefix_in);
			AddParameter(cmd, CustomerAcctRow.prefix_out_PropName, value.Prefix_out);
			AddParameter(cmd, CustomerAcctRow.partner_id_PropName, value.Partner_id);
			AddParameter(cmd, CustomerAcctRow.service_id_PropName, value.Service_id);
			AddParameter(cmd, CustomerAcctRow.retail_calling_plan_id_PropName,
				value.IsRetail_calling_plan_idNull ? DBNull.Value : (object)value.Retail_calling_plan_id);
			AddParameter(cmd, CustomerAcctRow.retail_markup_type_PropName, value.Retail_markup_type);
			AddParameter(cmd, CustomerAcctRow.retail_markup_dollar_PropName, value.Retail_markup_dollar);
			AddParameter(cmd, CustomerAcctRow.retail_markup_percent_PropName, value.Retail_markup_percent);
			AddParameter(cmd, CustomerAcctRow.max_call_length_PropName, value.Max_call_length);
			AddParameter(cmd, CustomerAcctRow.routing_plan_id_PropName, value.Routing_plan_id);

			value.Customer_acct_id = (short)cmd.ExecuteScalar();
		}

		public void DeleteByCustomerAcctId(short pCustomerAcctId) {
			DeleteByPrimaryKey(pCustomerAcctId);
		}
	}
	// End of CustomerAcctCollection class
} // End of namespace

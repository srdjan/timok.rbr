// <fileinfo name="Base\CustomerAcctCollection_Base.cs">
//		<copyright>
//			Copyright © 2002-2007 Timok ES LLC. All rights reserved.
//		</copyright>
//		<remarks>
//			Do not change this source code manually. Changes to this file may 
//			cause incorrect behavior and will be lost if the code is regenerated.
//		</remarks>
//		<generator rewritefile="True" infourl="http://www.SharpPower.com">RapTier</generator>
// </fileinfo>

using System;
using System.Data;
using Timok.Rbr.DAL.RbrDatabase;

namespace Timok.Rbr.DAL.RbrDatabase.Base
{
	/// <summary>
	/// The base class for <see cref="CustomerAcctCollection"/>. Provides methods 
	/// for common database table operations. 
	/// </summary>
	/// <remarks>
	/// Do not change this source code. Update the <see cref="CustomerAcctCollection"/>
	/// class if you need to add or change some functionality.
	/// </remarks>
	public abstract class CustomerAcctCollection_Base
	{
		// Constants
		public const string Customer_acct_idColumnName = "customer_acct_id";
		public const string NameColumnName = "name";
		public const string StatusColumnName = "status";
		public const string Default_bonus_minutes_typeColumnName = "default_bonus_minutes_type";
		public const string Default_start_bonus_minutesColumnName = "default_start_bonus_minutes";
		public const string Is_prepaidColumnName = "is_prepaid";
		public const string Current_amountColumnName = "current_amount";
		public const string Limit_amountColumnName = "limit_amount";
		public const string Warning_amountColumnName = "warning_amount";
		public const string Allow_reroutingColumnName = "allow_rerouting";
		public const string Concurrent_useColumnName = "concurrent_use";
		public const string Prefix_in_type_idColumnName = "prefix_in_type_id";
		public const string Prefix_inColumnName = "prefix_in";
		public const string Prefix_outColumnName = "prefix_out";
		public const string Partner_idColumnName = "partner_id";
		public const string Service_idColumnName = "service_id";
		public const string Retail_calling_plan_idColumnName = "retail_calling_plan_id";
		public const string Retail_markup_typeColumnName = "retail_markup_type";
		public const string Retail_markup_dollarColumnName = "retail_markup_dollar";
		public const string Retail_markup_percentColumnName = "retail_markup_percent";
		public const string Max_call_lengthColumnName = "max_call_length";
		public const string Routing_plan_idColumnName = "routing_plan_id";

		// Instance fields
		private Rbr_Db _db;

		/// <summary>
		/// Initializes a new instance of the <see cref="CustomerAcctCollection_Base"/> 
		/// class with the specified <see cref="Rbr_Db"/>.
		/// </summary>
		/// <param name="db">The <see cref="Rbr_Db"/> object.</param>
		public CustomerAcctCollection_Base(Rbr_Db db)
		{
			_db = db;
		}

		/// <summary>
		/// Gets the database object that this table belongs to.
		///	</summary>
		///	<value>The <see cref="Rbr_Db"/> object.</value>
		protected Rbr_Db Database
		{
			get { return _db; }
		}

		/// <summary>
		/// Gets an array of all records from the <c>CustomerAcct</c> table.
		/// </summary>
		/// <returns>An array of <see cref="CustomerAcctRow"/> objects.</returns>
		public virtual CustomerAcctRow[] GetAll()
		{
			return MapRecords(CreateGetAllCommand());
		}

		/// <summary>
		/// Gets a <see cref="System.Data.DataTable"/> object that 
		/// includes all records from the <c>CustomerAcct</c> table.
		/// </summary>
		/// <returns>A reference to the <see cref="System.Data.DataTable"/> object.</returns>
		public virtual DataTable GetAllAsDataTable()
		{
			return MapRecordsToDataTable(CreateGetAllCommand());
		}

		/// <summary>
		/// Creates and returns an <see cref="System.Data.IDbCommand"/> object that is used
		/// to retrieve all records from the <c>CustomerAcct</c> table.
		/// </summary>
		/// <returns>A reference to the <see cref="System.Data.IDbCommand"/> object.</returns>
		protected virtual IDbCommand CreateGetAllCommand()
		{
			return CreateGetCommand(null, null);
		}

		/// <summary>
		/// Gets the first <see cref="CustomerAcctRow"/> objects that 
		/// match the search condition.
		/// </summary>
		/// <param name="whereSql">The SQL search condition. For example: 
		/// <c>"FirstName='Smith' AND Zip=75038"</c>.</param>
		/// <returns>An instance of <see cref="CustomerAcctRow"/> or null reference 
		/// (Nothing in Visual Basic) if the object was not found.</returns>
		public CustomerAcctRow GetRow(string whereSql)
		{
			int totalRecordCount = -1;
			CustomerAcctRow[] rows = GetAsArray(whereSql, null, 0, 1, ref totalRecordCount);
			return 0 == rows.Length ? null : rows[0];
		}

		/// <summary>
		/// Gets an array of <see cref="CustomerAcctRow"/> objects that 
		/// match the search condition, in the the specified sort order.
		/// </summary>
		/// <param name="whereSql">The SQL search condition. For example: 
		/// <c>"FirstName='Smith' AND Zip=75038"</c>.</param>
		/// <param name="orderBySql">The column name(s) followed by "ASC" (ascending) or "DESC" (descending).
		/// Columns are sorted in ascending order by default. For example: <c>"LastName ASC, FirstName ASC"</c>.</param>
		/// <returns>An array of <see cref="CustomerAcctRow"/> objects.</returns>
		public CustomerAcctRow[] GetAsArray(string whereSql, string orderBySql)
		{
			int totalRecordCount = -1;
			return GetAsArray(whereSql, orderBySql, 0, int.MaxValue, ref totalRecordCount);
		}

		/// <summary>
		/// Gets an array of <see cref="CustomerAcctRow"/> objects that 
		/// match the search condition, in the the specified sort order.
		/// </summary>
		/// <param name="whereSql">The SQL search condition. For example:
		/// <c>"FirstName='Smith' AND Zip=75038"</c>.</param>
		/// <param name="orderBySql">The column name(s) followed by "ASC" (ascending) or "DESC" (descending).
		/// Columns are sorted in ascending order by default. For example: <c>"LastName ASC, FirstName ASC"</c>.</param>
		/// <param name="startIndex">The index of the first record to return.</param>
		/// <param name="length">The number of records to return.</param>
		/// <param name="totalRecordCount">A reference parameter that returns the total number 
		/// of records in the reader object if 0 was passed into the method; otherwise it returns -1.</param>
		/// <returns>An array of <see cref="CustomerAcctRow"/> objects.</returns>
		public virtual CustomerAcctRow[] GetAsArray(string whereSql, string orderBySql,
							int startIndex, int length, ref int totalRecordCount)
		{
			using(IDataReader reader = _db.ExecuteReader(CreateGetCommand(whereSql, orderBySql)))
			{
				return MapRecords(reader, startIndex, length, ref totalRecordCount);
			}
		}

		/// <summary>
		/// Gets a <see cref="System.Data.DataTable"/> object filled with data that 
		/// match the search condition, in the the specified sort order.
		/// </summary>
		/// <param name="whereSql">The SQL search condition. For example: "FirstName='Smith' AND Zip=75038".</param>
		/// <param name="orderBySql">The column name(s) followed by "ASC" (ascending) or "DESC" (descending).
		/// Columns are sorted in ascending order by default. For example: "LastName ASC, FirstName ASC".</param>
		/// <returns>A reference to the <see cref="System.Data.DataTable"/> object.</returns>
		public DataTable GetAsDataTable(string whereSql, string orderBySql)
		{
			int totalRecordCount = -1;
			return GetAsDataTable(whereSql, orderBySql, 0, int.MaxValue, ref totalRecordCount);
		}

		/// <summary>
		/// Gets a <see cref="System.Data.DataTable"/> object filled with data that 
		/// match the search condition, in the the specified sort order.
		/// </summary>
		/// <param name="whereSql">The SQL search condition. For example: "FirstName='Smith' AND Zip=75038".</param>
		/// <param name="orderBySql">The column name(s) followed by "ASC" (ascending) or "DESC" (descending).
		/// Columns are sorted in ascending order by default. For example: "LastName ASC, FirstName ASC".</param>
		/// <param name="startIndex">The index of the first record to return.</param>
		/// <param name="length">The number of records to return.</param>
		/// <param name="totalRecordCount">A reference parameter that returns the total number 
		/// of records in the reader object if 0 was passed into the method; otherwise it returns -1.</param>
		/// <returns>A reference to the <see cref="System.Data.DataTable"/> object.</returns>
		public virtual DataTable GetAsDataTable(string whereSql, string orderBySql,
							int startIndex, int length, ref int totalRecordCount)
		{
			using(IDataReader reader = _db.ExecuteReader(CreateGetCommand(whereSql, orderBySql)))
			{
				return MapRecordsToDataTable(reader, startIndex, length, ref totalRecordCount);
			}
		}

		/// <summary>
		/// Creates an <see cref="System.Data.IDbCommand"/> object for the specified search criteria.
		/// </summary>
		/// <param name="whereSql">The SQL search condition. For example: "FirstName='Smith' AND Zip=75038".</param>
		/// <param name="orderBySql">The column name(s) followed by "ASC" (ascending) or "DESC" (descending).
		/// Columns are sorted in ascending order by default. For example: "LastName ASC, FirstName ASC".</param>
		/// <returns>A reference to the <see cref="System.Data.IDbCommand"/> object.</returns>
		protected virtual IDbCommand CreateGetCommand(string whereSql, string orderBySql)
		{
			string sql = "SELECT * FROM [dbo].[CustomerAcct]";
			if(null != whereSql && 0 < whereSql.Length)
				sql += " WHERE " + whereSql;
			if(null != orderBySql && 0 < orderBySql.Length)
				sql += " ORDER BY " + orderBySql;
			return _db.CreateCommand(sql);
		}

		/// <summary>
		/// Gets <see cref="CustomerAcctRow"/> by the primary key.
		/// </summary>
		/// <param name="customer_acct_id">The <c>customer_acct_id</c> column value.</param>
		/// <returns>An instance of <see cref="CustomerAcctRow"/> or null reference 
		/// (Nothing in Visual Basic) if the object was not found.</returns>
		public virtual CustomerAcctRow GetByPrimaryKey(short customer_acct_id)
		{
			string whereSql = "[customer_acct_id]=" + _db.CreateSqlParameterName("Customer_acct_id");
			IDbCommand cmd = CreateGetCommand(whereSql, null);
			AddParameter(cmd, "Customer_acct_id", customer_acct_id);
			CustomerAcctRow[] tempArray = MapRecords(cmd);
			return 0 == tempArray.Length ? null : tempArray[0];
		}

		/// <summary>
		/// Gets an array of <see cref="CustomerAcctRow"/> objects 
		/// by the <c>R_164</c> foreign key.
		/// </summary>
		/// <param name="prefix_in_type_id">The <c>prefix_in_type_id</c> column value.</param>
		/// <returns>An array of <see cref="CustomerAcctRow"/> objects.</returns>
		public virtual CustomerAcctRow[] GetByPrefix_in_type_id(short prefix_in_type_id)
		{
			return MapRecords(CreateGetByPrefix_in_type_idCommand(prefix_in_type_id));
		}

		/// <summary>
		/// Gets a <see cref="System.Data.DataTable"/> object 
		/// by the <c>R_164</c> foreign key.
		/// </summary>
		/// <param name="prefix_in_type_id">The <c>prefix_in_type_id</c> column value.</param>
		/// <returns>A reference to the <see cref="System.Data.DataTable"/> object.</returns>
		public virtual DataTable GetByPrefix_in_type_idAsDataTable(short prefix_in_type_id)
		{
			return MapRecordsToDataTable(CreateGetByPrefix_in_type_idCommand(prefix_in_type_id));
		}

		/// <summary>
		/// Creates an <see cref="System.Data.IDbCommand"/> object that can be used to 
		/// return records by the <c>R_164</c> foreign key.
		/// </summary>
		/// <param name="prefix_in_type_id">The <c>prefix_in_type_id</c> column value.</param>
		/// <returns>A reference to the <see cref="System.Data.IDbCommand"/> object.</returns>
		protected virtual IDbCommand CreateGetByPrefix_in_type_idCommand(short prefix_in_type_id)
		{
			string whereSql = "";
			whereSql += "[prefix_in_type_id]=" + _db.CreateSqlParameterName("Prefix_in_type_id");

			IDbCommand cmd = CreateGetCommand(whereSql, null);
			AddParameter(cmd, "Prefix_in_type_id", prefix_in_type_id);
			return cmd;
		}

		/// <summary>
		/// Gets an array of <see cref="CustomerAcctRow"/> objects 
		/// by the <c>R_195</c> foreign key.
		/// </summary>
		/// <param name="service_id">The <c>service_id</c> column value.</param>
		/// <returns>An array of <see cref="CustomerAcctRow"/> objects.</returns>
		public virtual CustomerAcctRow[] GetByService_id(short service_id)
		{
			return MapRecords(CreateGetByService_idCommand(service_id));
		}

		/// <summary>
		/// Gets a <see cref="System.Data.DataTable"/> object 
		/// by the <c>R_195</c> foreign key.
		/// </summary>
		/// <param name="service_id">The <c>service_id</c> column value.</param>
		/// <returns>A reference to the <see cref="System.Data.DataTable"/> object.</returns>
		public virtual DataTable GetByService_idAsDataTable(short service_id)
		{
			return MapRecordsToDataTable(CreateGetByService_idCommand(service_id));
		}

		/// <summary>
		/// Creates an <see cref="System.Data.IDbCommand"/> object that can be used to 
		/// return records by the <c>R_195</c> foreign key.
		/// </summary>
		/// <param name="service_id">The <c>service_id</c> column value.</param>
		/// <returns>A reference to the <see cref="System.Data.IDbCommand"/> object.</returns>
		protected virtual IDbCommand CreateGetByService_idCommand(short service_id)
		{
			string whereSql = "";
			whereSql += "[service_id]=" + _db.CreateSqlParameterName("Service_id");

			IDbCommand cmd = CreateGetCommand(whereSql, null);
			AddParameter(cmd, "Service_id", service_id);
			return cmd;
		}

		/// <summary>
		/// Gets an array of <see cref="CustomerAcctRow"/> objects 
		/// by the <c>R_341</c> foreign key.
		/// </summary>
		/// <param name="routing_plan_id">The <c>routing_plan_id</c> column value.</param>
		/// <returns>An array of <see cref="CustomerAcctRow"/> objects.</returns>
		public virtual CustomerAcctRow[] GetByRouting_plan_id(int routing_plan_id)
		{
			return MapRecords(CreateGetByRouting_plan_idCommand(routing_plan_id));
		}

		/// <summary>
		/// Gets a <see cref="System.Data.DataTable"/> object 
		/// by the <c>R_341</c> foreign key.
		/// </summary>
		/// <param name="routing_plan_id">The <c>routing_plan_id</c> column value.</param>
		/// <returns>A reference to the <see cref="System.Data.DataTable"/> object.</returns>
		public virtual DataTable GetByRouting_plan_idAsDataTable(int routing_plan_id)
		{
			return MapRecordsToDataTable(CreateGetByRouting_plan_idCommand(routing_plan_id));
		}

		/// <summary>
		/// Creates an <see cref="System.Data.IDbCommand"/> object that can be used to 
		/// return records by the <c>R_341</c> foreign key.
		/// </summary>
		/// <param name="routing_plan_id">The <c>routing_plan_id</c> column value.</param>
		/// <returns>A reference to the <see cref="System.Data.IDbCommand"/> object.</returns>
		protected virtual IDbCommand CreateGetByRouting_plan_idCommand(int routing_plan_id)
		{
			string whereSql = "";
			whereSql += "[routing_plan_id]=" + _db.CreateSqlParameterName("Routing_plan_id");

			IDbCommand cmd = CreateGetCommand(whereSql, null);
			AddParameter(cmd, "Routing_plan_id", routing_plan_id);
			return cmd;
		}

		/// <summary>
		/// Gets an array of <see cref="CustomerAcctRow"/> objects 
		/// by the <c>R_358</c> foreign key.
		/// </summary>
		/// <param name="retail_calling_plan_id">The <c>retail_calling_plan_id</c> column value.</param>
		/// <returns>An array of <see cref="CustomerAcctRow"/> objects.</returns>
		public CustomerAcctRow[] GetByRetail_calling_plan_id(int retail_calling_plan_id)
		{
			return GetByRetail_calling_plan_id(retail_calling_plan_id, false);
		}

		/// <summary>
		/// Gets an array of <see cref="CustomerAcctRow"/> objects 
		/// by the <c>R_358</c> foreign key.
		/// </summary>
		/// <param name="retail_calling_plan_id">The <c>retail_calling_plan_id</c> column value.</param>
		/// <param name="retail_calling_plan_idNull">true if the method ignores the retail_calling_plan_id
		/// parameter value and uses DbNull instead of it; otherwise, false.</param>
		/// <returns>An array of <see cref="CustomerAcctRow"/> objects.</returns>
		public virtual CustomerAcctRow[] GetByRetail_calling_plan_id(int retail_calling_plan_id, bool retail_calling_plan_idNull)
		{
			return MapRecords(CreateGetByRetail_calling_plan_idCommand(retail_calling_plan_id, retail_calling_plan_idNull));
		}

		/// <summary>
		/// Gets a <see cref="System.Data.DataTable"/> object 
		/// by the <c>R_358</c> foreign key.
		/// </summary>
		/// <param name="retail_calling_plan_id">The <c>retail_calling_plan_id</c> column value.</param>
		/// <returns>A reference to the <see cref="System.Data.DataTable"/> object.</returns>
		public DataTable GetByRetail_calling_plan_idAsDataTable(int retail_calling_plan_id)
		{
			return GetByRetail_calling_plan_idAsDataTable(retail_calling_plan_id, false);
		}

		/// <summary>
		/// Gets a <see cref="System.Data.DataTable"/> object 
		/// by the <c>R_358</c> foreign key.
		/// </summary>
		/// <param name="retail_calling_plan_id">The <c>retail_calling_plan_id</c> column value.</param>
		/// <param name="retail_calling_plan_idNull">true if the method ignores the retail_calling_plan_id
		/// parameter value and uses DbNull instead of it; otherwise, false.</param>
		/// <returns>A reference to the <see cref="System.Data.DataTable"/> object.</returns>
		public virtual DataTable GetByRetail_calling_plan_idAsDataTable(int retail_calling_plan_id, bool retail_calling_plan_idNull)
		{
			return MapRecordsToDataTable(CreateGetByRetail_calling_plan_idCommand(retail_calling_plan_id, retail_calling_plan_idNull));
		}

		/// <summary>
		/// Creates an <see cref="System.Data.IDbCommand"/> object that can be used to 
		/// return records by the <c>R_358</c> foreign key.
		/// </summary>
		/// <param name="retail_calling_plan_id">The <c>retail_calling_plan_id</c> column value.</param>
		/// <param name="retail_calling_plan_idNull">true if the method ignores the retail_calling_plan_id
		/// parameter value and uses DbNull instead of it; otherwise, false.</param>
		/// <returns>A reference to the <see cref="System.Data.IDbCommand"/> object.</returns>
		protected virtual IDbCommand CreateGetByRetail_calling_plan_idCommand(int retail_calling_plan_id, bool retail_calling_plan_idNull)
		{
			string whereSql = "";
			if(retail_calling_plan_idNull)
				whereSql += "[retail_calling_plan_id] IS NULL";
			else
				whereSql += "[retail_calling_plan_id]=" + _db.CreateSqlParameterName("Retail_calling_plan_id");

			IDbCommand cmd = CreateGetCommand(whereSql, null);
			if(!retail_calling_plan_idNull)
				AddParameter(cmd, "Retail_calling_plan_id", retail_calling_plan_id);
			return cmd;
		}

		/// <summary>
		/// Gets an array of <see cref="CustomerAcctRow"/> objects 
		/// by the <c>R_42</c> foreign key.
		/// </summary>
		/// <param name="partner_id">The <c>partner_id</c> column value.</param>
		/// <returns>An array of <see cref="CustomerAcctRow"/> objects.</returns>
		public virtual CustomerAcctRow[] GetByPartner_id(int partner_id)
		{
			return MapRecords(CreateGetByPartner_idCommand(partner_id));
		}

		/// <summary>
		/// Gets a <see cref="System.Data.DataTable"/> object 
		/// by the <c>R_42</c> foreign key.
		/// </summary>
		/// <param name="partner_id">The <c>partner_id</c> column value.</param>
		/// <returns>A reference to the <see cref="System.Data.DataTable"/> object.</returns>
		public virtual DataTable GetByPartner_idAsDataTable(int partner_id)
		{
			return MapRecordsToDataTable(CreateGetByPartner_idCommand(partner_id));
		}

		/// <summary>
		/// Creates an <see cref="System.Data.IDbCommand"/> object that can be used to 
		/// return records by the <c>R_42</c> foreign key.
		/// </summary>
		/// <param name="partner_id">The <c>partner_id</c> column value.</param>
		/// <returns>A reference to the <see cref="System.Data.IDbCommand"/> object.</returns>
		protected virtual IDbCommand CreateGetByPartner_idCommand(int partner_id)
		{
			string whereSql = "";
			whereSql += "[partner_id]=" + _db.CreateSqlParameterName("Partner_id");

			IDbCommand cmd = CreateGetCommand(whereSql, null);
			AddParameter(cmd, "Partner_id", partner_id);
			return cmd;
		}

		/// <summary>
		/// Adds a new record into the <c>CustomerAcct</c> table.
		/// </summary>
		/// <param name="value">The <see cref="CustomerAcctRow"/> object to be inserted.</param>
		public virtual void Insert(CustomerAcctRow value)
		{
			string sqlStr = "INSERT INTO [dbo].[CustomerAcct] (" +
				"[customer_acct_id], " +
				"[name], " +
				"[status], " +
				"[default_bonus_minutes_type], " +
				"[default_start_bonus_minutes], " +
				"[is_prepaid], " +
				"[limit_amount], " +
				"[warning_amount], " +
				"[allow_rerouting], " +
				"[concurrent_use], " +
				"[prefix_in_type_id], " +
				"[prefix_in], " +
				"[prefix_out], " +
				"[partner_id], " +
				"[service_id], " +
				"[retail_calling_plan_id], " +
				"[retail_markup_type], " +
				"[retail_markup_dollar], " +
				"[retail_markup_percent], " +
				"[max_call_length], " +
				"[routing_plan_id]" +
				") VALUES (" +
				_db.CreateSqlParameterName("Customer_acct_id") + ", " +
				_db.CreateSqlParameterName("Name") + ", " +
				_db.CreateSqlParameterName("Status") + ", " +
				_db.CreateSqlParameterName("Default_bonus_minutes_type") + ", " +
				_db.CreateSqlParameterName("Default_start_bonus_minutes") + ", " +
				_db.CreateSqlParameterName("Is_prepaid") + ", " +
				_db.CreateSqlParameterName("Limit_amount") + ", " +
				_db.CreateSqlParameterName("Warning_amount") + ", " +
				_db.CreateSqlParameterName("Allow_rerouting") + ", " +
				_db.CreateSqlParameterName("Concurrent_use") + ", " +
				_db.CreateSqlParameterName("Prefix_in_type_id") + ", " +
				_db.CreateSqlParameterName("Prefix_in") + ", " +
				_db.CreateSqlParameterName("Prefix_out") + ", " +
				_db.CreateSqlParameterName("Partner_id") + ", " +
				_db.CreateSqlParameterName("Service_id") + ", " +
				_db.CreateSqlParameterName("Retail_calling_plan_id") + ", " +
				_db.CreateSqlParameterName("Retail_markup_type") + ", " +
				_db.CreateSqlParameterName("Retail_markup_dollar") + ", " +
				_db.CreateSqlParameterName("Retail_markup_percent") + ", " +
				_db.CreateSqlParameterName("Max_call_length") + ", " +
				_db.CreateSqlParameterName("Routing_plan_id") + ")";
			IDbCommand cmd = _db.CreateCommand(sqlStr);
			AddParameter(cmd, "Customer_acct_id", value.Customer_acct_id);
			AddParameter(cmd, "Name", value.Name);
			AddParameter(cmd, "Status", value.Status);
			AddParameter(cmd, "Default_bonus_minutes_type", value.Default_bonus_minutes_type);
			AddParameter(cmd, "Default_start_bonus_minutes", value.Default_start_bonus_minutes);
			AddParameter(cmd, "Is_prepaid", value.Is_prepaid);
			AddParameter(cmd, "Limit_amount", value.Limit_amount);
			AddParameter(cmd, "Warning_amount", value.Warning_amount);
			AddParameter(cmd, "Allow_rerouting", value.Allow_rerouting);
			AddParameter(cmd, "Concurrent_use", value.Concurrent_use);
			AddParameter(cmd, "Prefix_in_type_id", value.Prefix_in_type_id);
			AddParameter(cmd, "Prefix_in", value.Prefix_in);
			AddParameter(cmd, "Prefix_out", value.Prefix_out);
			AddParameter(cmd, "Partner_id", value.Partner_id);
			AddParameter(cmd, "Service_id", value.Service_id);
			AddParameter(cmd, "Retail_calling_plan_id",
				value.IsRetail_calling_plan_idNull ? DBNull.Value : (object)value.Retail_calling_plan_id);
			AddParameter(cmd, "Retail_markup_type", value.Retail_markup_type);
			AddParameter(cmd, "Retail_markup_dollar", value.Retail_markup_dollar);
			AddParameter(cmd, "Retail_markup_percent", value.Retail_markup_percent);
			AddParameter(cmd, "Max_call_length", value.Max_call_length);
			AddParameter(cmd, "Routing_plan_id", value.Routing_plan_id);
			cmd.ExecuteNonQuery();
		}

		/// <summary>
		/// Updates a record in the <c>CustomerAcct</c> table.
		/// </summary>
		/// <param name="value">The <see cref="CustomerAcctRow"/>
		/// object used to update the table record.</param>
		/// <returns>true if the record was updated; otherwise, false.</returns>
		public virtual bool Update(CustomerAcctRow value)
		{
			string sqlStr = "UPDATE [dbo].[CustomerAcct] SET " +
				"[name]=" + _db.CreateSqlParameterName("Name") + ", " +
				"[status]=" + _db.CreateSqlParameterName("Status") + ", " +
				"[default_bonus_minutes_type]=" + _db.CreateSqlParameterName("Default_bonus_minutes_type") + ", " +
				"[default_start_bonus_minutes]=" + _db.CreateSqlParameterName("Default_start_bonus_minutes") + ", " +
				"[is_prepaid]=" + _db.CreateSqlParameterName("Is_prepaid") + ", " +
				"[limit_amount]=" + _db.CreateSqlParameterName("Limit_amount") + ", " +
				"[warning_amount]=" + _db.CreateSqlParameterName("Warning_amount") + ", " +
				"[allow_rerouting]=" + _db.CreateSqlParameterName("Allow_rerouting") + ", " +
				"[concurrent_use]=" + _db.CreateSqlParameterName("Concurrent_use") + ", " +
				"[prefix_in_type_id]=" + _db.CreateSqlParameterName("Prefix_in_type_id") + ", " +
				"[prefix_in]=" + _db.CreateSqlParameterName("Prefix_in") + ", " +
				"[prefix_out]=" + _db.CreateSqlParameterName("Prefix_out") + ", " +
				"[partner_id]=" + _db.CreateSqlParameterName("Partner_id") + ", " +
				"[service_id]=" + _db.CreateSqlParameterName("Service_id") + ", " +
				"[retail_calling_plan_id]=" + _db.CreateSqlParameterName("Retail_calling_plan_id") + ", " +
				"[retail_markup_type]=" + _db.CreateSqlParameterName("Retail_markup_type") + ", " +
				"[retail_markup_dollar]=" + _db.CreateSqlParameterName("Retail_markup_dollar") + ", " +
				"[retail_markup_percent]=" + _db.CreateSqlParameterName("Retail_markup_percent") + ", " +
				"[max_call_length]=" + _db.CreateSqlParameterName("Max_call_length") + ", " +
				"[routing_plan_id]=" + _db.CreateSqlParameterName("Routing_plan_id") +
				" WHERE " +
				"[customer_acct_id]=" + _db.CreateSqlParameterName("Customer_acct_id");
			IDbCommand cmd = _db.CreateCommand(sqlStr);
			AddParameter(cmd, "Name", value.Name);
			AddParameter(cmd, "Status", value.Status);
			AddParameter(cmd, "Default_bonus_minutes_type", value.Default_bonus_minutes_type);
			AddParameter(cmd, "Default_start_bonus_minutes", value.Default_start_bonus_minutes);
			AddParameter(cmd, "Is_prepaid", value.Is_prepaid);
			AddParameter(cmd, "Limit_amount", value.Limit_amount);
			AddParameter(cmd, "Warning_amount", value.Warning_amount);
			AddParameter(cmd, "Allow_rerouting", value.Allow_rerouting);
			AddParameter(cmd, "Concurrent_use", value.Concurrent_use);
			AddParameter(cmd, "Prefix_in_type_id", value.Prefix_in_type_id);
			AddParameter(cmd, "Prefix_in", value.Prefix_in);
			AddParameter(cmd, "Prefix_out", value.Prefix_out);
			AddParameter(cmd, "Partner_id", value.Partner_id);
			AddParameter(cmd, "Service_id", value.Service_id);
			AddParameter(cmd, "Retail_calling_plan_id",
				value.IsRetail_calling_plan_idNull ? DBNull.Value : (object)value.Retail_calling_plan_id);
			AddParameter(cmd, "Retail_markup_type", value.Retail_markup_type);
			AddParameter(cmd, "Retail_markup_dollar", value.Retail_markup_dollar);
			AddParameter(cmd, "Retail_markup_percent", value.Retail_markup_percent);
			AddParameter(cmd, "Max_call_length", value.Max_call_length);
			AddParameter(cmd, "Routing_plan_id", value.Routing_plan_id);
			AddParameter(cmd, "Customer_acct_id", value.Customer_acct_id);
			return 0 != cmd.ExecuteNonQuery();
		}

		/// <summary>
		/// Updates the <c>CustomerAcct</c> table and calls the <c>AcceptChanges</c> method
		/// on the changed DataRow objects.
		/// </summary>
		/// <param name="table">The <see cref="System.Data.DataTable"/> used to update the data source.</param>
		public void Update(DataTable table)
		{
			Update(table, true);
		}

		/// <summary>
		/// Updates the <c>CustomerAcct</c> table. Pass <c>false</c> as the <c>acceptChanges</c> 
		/// argument when your code calls this method in an ADO.NET transaction context. Note that in 
		/// this case, after you call the Update method you need call either <c>AcceptChanges</c> 
		/// or <c>RejectChanges</c> method on the DataTable object.
		/// <code>
		/// MyDb db = new MyDb();
		/// try
		/// {
		///		db.BeginTransaction();
		///		db.MyCollection.Update(myDataTable, false);
		///		db.CommitTransaction();
		///		myDataTable.AcceptChanges();
		/// }
		/// catch(Exception)
		/// {
		///		db.RollbackTransaction();
		///		myDataTable.RejectChanges();
		/// }
		/// </code>
		/// </summary>
		/// <param name="table">The <see cref="System.Data.DataTable"/> used to update the data source.</param>
		/// <param name="acceptChanges">Specifies whether this method calls the <c>AcceptChanges</c>
		/// method on the changed DataRow objects.</param>
		public virtual void Update(DataTable table, bool acceptChanges)
		{
			DataRowCollection rows = table.Rows;
			for(int i = rows.Count - 1; i >= 0; i--)
			{
				DataRow row = rows[i];
				switch(row.RowState)
				{
					case DataRowState.Added:
						Insert(MapRow(row));
						if(acceptChanges)
							row.AcceptChanges();
						break;

					case DataRowState.Deleted:
						// Temporary reject changes to be able to access to the PK column(s)
						row.RejectChanges();
						try
						{
							DeleteByPrimaryKey((short)row["Customer_acct_id"]);
						}
						finally
						{
							row.Delete();
						}
						if(acceptChanges)
							row.AcceptChanges();
						break;
						
					case DataRowState.Modified:
						Update(MapRow(row));
						if(acceptChanges)
							row.AcceptChanges();
						break;
				}
			}
		}

		/// <summary>
		/// Deletes the specified object from the <c>CustomerAcct</c> table.
		/// </summary>
		/// <param name="value">The <see cref="CustomerAcctRow"/> object to delete.</param>
		/// <returns>true if the record was deleted; otherwise, false.</returns>
		public bool Delete(CustomerAcctRow value)
		{
			return DeleteByPrimaryKey(value.Customer_acct_id);
		}

		/// <summary>
		/// Deletes a record from the <c>CustomerAcct</c> table using
		/// the specified primary key.
		/// </summary>
		/// <param name="customer_acct_id">The <c>customer_acct_id</c> column value.</param>
		/// <returns>true if the record was deleted; otherwise, false.</returns>
		public virtual bool DeleteByPrimaryKey(short customer_acct_id)
		{
			string whereSql = "[customer_acct_id]=" + _db.CreateSqlParameterName("Customer_acct_id");
			IDbCommand cmd = CreateDeleteCommand(whereSql);
			AddParameter(cmd, "Customer_acct_id", customer_acct_id);
			return 0 < cmd.ExecuteNonQuery();
		}

		/// <summary>
		/// Deletes records from the <c>CustomerAcct</c> table using the 
		/// <c>R_164</c> foreign key.
		/// </summary>
		/// <param name="prefix_in_type_id">The <c>prefix_in_type_id</c> column value.</param>
		/// <returns>The number of records deleted from the table.</returns>
		public int DeleteByPrefix_in_type_id(short prefix_in_type_id)
		{
			return CreateDeleteByPrefix_in_type_idCommand(prefix_in_type_id).ExecuteNonQuery();
		}

		/// <summary>
		/// Creates an <see cref="System.Data.IDbCommand"/> object that can be used to
		/// delete records using the <c>R_164</c> foreign key.
		/// </summary>
		/// <param name="prefix_in_type_id">The <c>prefix_in_type_id</c> column value.</param>
		/// <returns>A reference to the <see cref="System.Data.IDbCommand"/> object.</returns>
		protected virtual IDbCommand CreateDeleteByPrefix_in_type_idCommand(short prefix_in_type_id)
		{
			string whereSql = "";
			whereSql += "[prefix_in_type_id]=" + _db.CreateSqlParameterName("Prefix_in_type_id");

			IDbCommand cmd = CreateDeleteCommand(whereSql);
			AddParameter(cmd, "Prefix_in_type_id", prefix_in_type_id);
			return cmd;
		}

		/// <summary>
		/// Deletes records from the <c>CustomerAcct</c> table using the 
		/// <c>R_195</c> foreign key.
		/// </summary>
		/// <param name="service_id">The <c>service_id</c> column value.</param>
		/// <returns>The number of records deleted from the table.</returns>
		public int DeleteByService_id(short service_id)
		{
			return CreateDeleteByService_idCommand(service_id).ExecuteNonQuery();
		}

		/// <summary>
		/// Creates an <see cref="System.Data.IDbCommand"/> object that can be used to
		/// delete records using the <c>R_195</c> foreign key.
		/// </summary>
		/// <param name="service_id">The <c>service_id</c> column value.</param>
		/// <returns>A reference to the <see cref="System.Data.IDbCommand"/> object.</returns>
		protected virtual IDbCommand CreateDeleteByService_idCommand(short service_id)
		{
			string whereSql = "";
			whereSql += "[service_id]=" + _db.CreateSqlParameterName("Service_id");

			IDbCommand cmd = CreateDeleteCommand(whereSql);
			AddParameter(cmd, "Service_id", service_id);
			return cmd;
		}

		/// <summary>
		/// Deletes records from the <c>CustomerAcct</c> table using the 
		/// <c>R_341</c> foreign key.
		/// </summary>
		/// <param name="routing_plan_id">The <c>routing_plan_id</c> column value.</param>
		/// <returns>The number of records deleted from the table.</returns>
		public int DeleteByRouting_plan_id(int routing_plan_id)
		{
			return CreateDeleteByRouting_plan_idCommand(routing_plan_id).ExecuteNonQuery();
		}

		/// <summary>
		/// Creates an <see cref="System.Data.IDbCommand"/> object that can be used to
		/// delete records using the <c>R_341</c> foreign key.
		/// </summary>
		/// <param name="routing_plan_id">The <c>routing_plan_id</c> column value.</param>
		/// <returns>A reference to the <see cref="System.Data.IDbCommand"/> object.</returns>
		protected virtual IDbCommand CreateDeleteByRouting_plan_idCommand(int routing_plan_id)
		{
			string whereSql = "";
			whereSql += "[routing_plan_id]=" + _db.CreateSqlParameterName("Routing_plan_id");

			IDbCommand cmd = CreateDeleteCommand(whereSql);
			AddParameter(cmd, "Routing_plan_id", routing_plan_id);
			return cmd;
		}

		/// <summary>
		/// Deletes records from the <c>CustomerAcct</c> table using the 
		/// <c>R_358</c> foreign key.
		/// </summary>
		/// <param name="retail_calling_plan_id">The <c>retail_calling_plan_id</c> column value.</param>
		/// <returns>The number of records deleted from the table.</returns>
		public int DeleteByRetail_calling_plan_id(int retail_calling_plan_id)
		{
			return DeleteByRetail_calling_plan_id(retail_calling_plan_id, false);
		}

		/// <summary>
		/// Deletes records from the <c>CustomerAcct</c> table using the 
		/// <c>R_358</c> foreign key.
		/// </summary>
		/// <param name="retail_calling_plan_id">The <c>retail_calling_plan_id</c> column value.</param>
		/// <param name="retail_calling_plan_idNull">true if the method ignores the retail_calling_plan_id
		/// parameter value and uses DbNull instead of it; otherwise, false.</param>
		/// <returns>The number of records deleted from the table.</returns>
		public int DeleteByRetail_calling_plan_id(int retail_calling_plan_id, bool retail_calling_plan_idNull)
		{
			return CreateDeleteByRetail_calling_plan_idCommand(retail_calling_plan_id, retail_calling_plan_idNull).ExecuteNonQuery();
		}

		/// <summary>
		/// Creates an <see cref="System.Data.IDbCommand"/> object that can be used to
		/// delete records using the <c>R_358</c> foreign key.
		/// </summary>
		/// <param name="retail_calling_plan_id">The <c>retail_calling_plan_id</c> column value.</param>
		/// <param name="retail_calling_plan_idNull">true if the method ignores the retail_calling_plan_id
		/// parameter value and uses DbNull instead of it; otherwise, false.</param>
		/// <returns>A reference to the <see cref="System.Data.IDbCommand"/> object.</returns>
		protected virtual IDbCommand CreateDeleteByRetail_calling_plan_idCommand(int retail_calling_plan_id, bool retail_calling_plan_idNull)
		{
			string whereSql = "";
			if(retail_calling_plan_idNull)
				whereSql += "[retail_calling_plan_id] IS NULL";
			else
				whereSql += "[retail_calling_plan_id]=" + _db.CreateSqlParameterName("Retail_calling_plan_id");

			IDbCommand cmd = CreateDeleteCommand(whereSql);
			if(!retail_calling_plan_idNull)
				AddParameter(cmd, "Retail_calling_plan_id", retail_calling_plan_id);
			return cmd;
		}

		/// <summary>
		/// Deletes records from the <c>CustomerAcct</c> table using the 
		/// <c>R_42</c> foreign key.
		/// </summary>
		/// <param name="partner_id">The <c>partner_id</c> column value.</param>
		/// <returns>The number of records deleted from the table.</returns>
		public int DeleteByPartner_id(int partner_id)
		{
			return CreateDeleteByPartner_idCommand(partner_id).ExecuteNonQuery();
		}

		/// <summary>
		/// Creates an <see cref="System.Data.IDbCommand"/> object that can be used to
		/// delete records using the <c>R_42</c> foreign key.
		/// </summary>
		/// <param name="partner_id">The <c>partner_id</c> column value.</param>
		/// <returns>A reference to the <see cref="System.Data.IDbCommand"/> object.</returns>
		protected virtual IDbCommand CreateDeleteByPartner_idCommand(int partner_id)
		{
			string whereSql = "";
			whereSql += "[partner_id]=" + _db.CreateSqlParameterName("Partner_id");

			IDbCommand cmd = CreateDeleteCommand(whereSql);
			AddParameter(cmd, "Partner_id", partner_id);
			return cmd;
		}

		/// <summary>
		/// Deletes <c>CustomerAcct</c> records that match the specified criteria.
		/// </summary>
		/// <param name="whereSql">The SQL search condition. 
		/// For example: <c>"FirstName='Smith' AND Zip=75038"</c>.</param>
		/// <returns>The number of deleted records.</returns>
		public int Delete(string whereSql)
		{
			return CreateDeleteCommand(whereSql).ExecuteNonQuery();
		}

		/// <summary>
		/// Creates an <see cref="System.Data.IDbCommand"/> object that can be used 
		/// to delete <c>CustomerAcct</c> records that match the specified criteria.
		/// </summary>
		/// <param name="whereSql">The SQL search condition. 
		/// For example: <c>"FirstName='Smith' AND Zip=75038"</c>.</param>
		/// <returns>A reference to the <see cref="System.Data.IDbCommand"/> object.</returns>
		protected virtual IDbCommand CreateDeleteCommand(string whereSql)
		{
			string sql = "DELETE FROM [dbo].[CustomerAcct]";
			if(null != whereSql && 0 < whereSql.Length)
				sql += " WHERE " + whereSql;
			return _db.CreateCommand(sql);
		}

		/// <summary>
		/// Deletes all records from the <c>CustomerAcct</c> table.
		/// </summary>
		/// <returns>The number of deleted records.</returns>
		public int DeleteAll()
		{
			return Delete("");
		}

		/// <summary>
		/// Reads data using the specified command and returns 
		/// an array of mapped objects.
		/// </summary>
		/// <param name="command">The <see cref="System.Data.IDbCommand"/> object.</param>
		/// <returns>An array of <see cref="CustomerAcctRow"/> objects.</returns>
		protected CustomerAcctRow[] MapRecords(IDbCommand command)
		{
			using(IDataReader reader = _db.ExecuteReader(command))
			{
				return MapRecords(reader);
			}
		}

		/// <summary>
		/// Reads data from the provided data reader and returns 
		/// an array of mapped objects.
		/// </summary>
		/// <param name="reader">The <see cref="System.Data.IDataReader"/> object to read data from the table.</param>
		/// <returns>An array of <see cref="CustomerAcctRow"/> objects.</returns>
		protected CustomerAcctRow[] MapRecords(IDataReader reader)
		{
			int totalRecordCount = -1;
			return MapRecords(reader, 0, int.MaxValue, ref totalRecordCount);
		}

		/// <summary>
		/// Reads data from the provided data reader and returns 
		/// an array of mapped objects.
		/// </summary>
		/// <param name="reader">The <see cref="System.Data.IDataReader"/> object to read data from the table.</param>
		/// <param name="startIndex">The index of the first record to map.</param>
		/// <param name="length">The number of records to map.</param>
		/// <param name="totalRecordCount">A reference parameter that returns the total number 
		/// of records in the reader object if 0 was passed into the method; otherwise it returns -1.</param>
		/// <returns>An array of <see cref="CustomerAcctRow"/> objects.</returns>
		protected virtual CustomerAcctRow[] MapRecords(IDataReader reader, 
										int startIndex, int length, ref int totalRecordCount)
		{
			if(0 > startIndex)
				throw new ArgumentOutOfRangeException("startIndex", startIndex, "StartIndex cannot be less than zero.");
			if(0 > length)
				throw new ArgumentOutOfRangeException("length", length, "Length cannot be less than zero.");

			int customer_acct_idColumnIndex = reader.GetOrdinal("customer_acct_id");
			int nameColumnIndex = reader.GetOrdinal("name");
			int statusColumnIndex = reader.GetOrdinal("status");
			int default_bonus_minutes_typeColumnIndex = reader.GetOrdinal("default_bonus_minutes_type");
			int default_start_bonus_minutesColumnIndex = reader.GetOrdinal("default_start_bonus_minutes");
			int is_prepaidColumnIndex = reader.GetOrdinal("is_prepaid");
			int current_amountColumnIndex = reader.GetOrdinal("current_amount");
			int limit_amountColumnIndex = reader.GetOrdinal("limit_amount");
			int warning_amountColumnIndex = reader.GetOrdinal("warning_amount");
			int allow_reroutingColumnIndex = reader.GetOrdinal("allow_rerouting");
			int concurrent_useColumnIndex = reader.GetOrdinal("concurrent_use");
			int prefix_in_type_idColumnIndex = reader.GetOrdinal("prefix_in_type_id");
			int prefix_inColumnIndex = reader.GetOrdinal("prefix_in");
			int prefix_outColumnIndex = reader.GetOrdinal("prefix_out");
			int partner_idColumnIndex = reader.GetOrdinal("partner_id");
			int service_idColumnIndex = reader.GetOrdinal("service_id");
			int retail_calling_plan_idColumnIndex = reader.GetOrdinal("retail_calling_plan_id");
			int retail_markup_typeColumnIndex = reader.GetOrdinal("retail_markup_type");
			int retail_markup_dollarColumnIndex = reader.GetOrdinal("retail_markup_dollar");
			int retail_markup_percentColumnIndex = reader.GetOrdinal("retail_markup_percent");
			int max_call_lengthColumnIndex = reader.GetOrdinal("max_call_length");
			int routing_plan_idColumnIndex = reader.GetOrdinal("routing_plan_id");

			System.Collections.ArrayList recordList = new System.Collections.ArrayList();
			int ri = -startIndex;
			while(reader.Read())
			{
				ri++;
				if(ri > 0 && ri <= length)
				{
					CustomerAcctRow record = new CustomerAcctRow();
					recordList.Add(record);

					record.Customer_acct_id = Convert.ToInt16(reader.GetValue(customer_acct_idColumnIndex));
					record.Name = Convert.ToString(reader.GetValue(nameColumnIndex));
					record.Status = Convert.ToByte(reader.GetValue(statusColumnIndex));
					record.Default_bonus_minutes_type = Convert.ToByte(reader.GetValue(default_bonus_minutes_typeColumnIndex));
					record.Default_start_bonus_minutes = Convert.ToInt16(reader.GetValue(default_start_bonus_minutesColumnIndex));
					record.Is_prepaid = Convert.ToByte(reader.GetValue(is_prepaidColumnIndex));
					record.Current_amount = Convert.ToDecimal(reader.GetValue(current_amountColumnIndex));
					record.Limit_amount = Convert.ToDecimal(reader.GetValue(limit_amountColumnIndex));
					record.Warning_amount = Convert.ToDecimal(reader.GetValue(warning_amountColumnIndex));
					record.Allow_rerouting = Convert.ToByte(reader.GetValue(allow_reroutingColumnIndex));
					record.Concurrent_use = Convert.ToByte(reader.GetValue(concurrent_useColumnIndex));
					record.Prefix_in_type_id = Convert.ToInt16(reader.GetValue(prefix_in_type_idColumnIndex));
					record.Prefix_in = Convert.ToString(reader.GetValue(prefix_inColumnIndex));
					record.Prefix_out = Convert.ToString(reader.GetValue(prefix_outColumnIndex));
					record.Partner_id = Convert.ToInt32(reader.GetValue(partner_idColumnIndex));
					record.Service_id = Convert.ToInt16(reader.GetValue(service_idColumnIndex));
					if(!reader.IsDBNull(retail_calling_plan_idColumnIndex))
						record.Retail_calling_plan_id = Convert.ToInt32(reader.GetValue(retail_calling_plan_idColumnIndex));
					record.Retail_markup_type = Convert.ToByte(reader.GetValue(retail_markup_typeColumnIndex));
					record.Retail_markup_dollar = Convert.ToDecimal(reader.GetValue(retail_markup_dollarColumnIndex));
					record.Retail_markup_percent = Convert.ToDecimal(reader.GetValue(retail_markup_percentColumnIndex));
					record.Max_call_length = Convert.ToInt16(reader.GetValue(max_call_lengthColumnIndex));
					record.Routing_plan_id = Convert.ToInt32(reader.GetValue(routing_plan_idColumnIndex));

					if(ri == length && 0 != totalRecordCount)
						break;
				}
			}

			totalRecordCount = 0 == totalRecordCount ? ri + startIndex : -1;
			return (CustomerAcctRow[])(recordList.ToArray(typeof(CustomerAcctRow)));
		}

		/// <summary>
		/// Reads data using the specified command and returns 
		/// a filled <see cref="System.Data.DataTable"/> object.
		/// </summary>
		/// <param name="command">The <see cref="System.Data.IDbCommand"/> object.</param>
		/// <returns>A reference to the <see cref="System.Data.DataTable"/> object.</returns>
		protected DataTable MapRecordsToDataTable(IDbCommand command)
		{
			using(IDataReader reader = _db.ExecuteReader(command))
			{
				return MapRecordsToDataTable(reader);
			}
		}

		/// <summary>
		/// Reads data from the provided data reader and returns 
		/// a filled <see cref="System.Data.DataTable"/> object.
		/// </summary>
		/// <param name="reader">The <see cref="System.Data.IDataReader"/> object to read data from the table.</param>
		/// <returns>A reference to the <see cref="System.Data.DataTable"/> object.</returns>
		protected DataTable MapRecordsToDataTable(IDataReader reader)
		{
			int totalRecordCount = 0;
			return MapRecordsToDataTable(reader, 0, int.MaxValue, ref totalRecordCount);
		}
		
		/// <summary>
		/// Reads data from the provided data reader and returns 
		/// a filled <see cref="System.Data.DataTable"/> object.
		/// </summary>
		/// <param name="reader">The <see cref="System.Data.IDataReader"/> object to read data from the table.</param>
		/// <param name="startIndex">The index of the first record to read.</param>
		/// <param name="length">The number of records to read.</param>
		/// <param name="totalRecordCount">A reference parameter that returns the total number 
		/// of records in the reader object if 0 was passed into the method; otherwise it returns -1.</param>
		/// <returns>A reference to the <see cref="System.Data.DataTable"/> object.</returns>
		protected virtual DataTable MapRecordsToDataTable(IDataReader reader, 
										int startIndex, int length, ref int totalRecordCount)
		{
			if(0 > startIndex)
				throw new ArgumentOutOfRangeException("startIndex", startIndex, "StartIndex cannot be less than zero.");
			if(0 > length)
				throw new ArgumentOutOfRangeException("length", length, "Length cannot be less than zero.");

			int columnCount = reader.FieldCount;
			int ri = -startIndex;
			
			DataTable dataTable = CreateDataTable();
			dataTable.BeginLoadData();
			object[] values = new object[columnCount];

			while(reader.Read())
			{
				ri++;
				if(ri > 0 && ri <= length)
				{
					reader.GetValues(values);
					dataTable.LoadDataRow(values, true);

					if(ri == length && 0 != totalRecordCount)
						break;
				}
			}
			dataTable.EndLoadData();

			totalRecordCount = 0 == totalRecordCount ? ri + startIndex : -1;
			return dataTable;
		}

		/// <summary>
		/// Converts <see cref="System.Data.DataRow"/> to <see cref="CustomerAcctRow"/>.
		/// </summary>
		/// <param name="row">The <see cref="System.Data.DataRow"/> object to be mapped.</param>
		/// <returns>A reference to the <see cref="CustomerAcctRow"/> object.</returns>
		protected virtual CustomerAcctRow MapRow(DataRow row)
		{
			CustomerAcctRow mappedObject = new CustomerAcctRow();
			DataTable dataTable = row.Table;
			DataColumn dataColumn;
			// Column "Customer_acct_id"
			dataColumn = dataTable.Columns["Customer_acct_id"];
			if(!row.IsNull(dataColumn))
				mappedObject.Customer_acct_id = (short)row[dataColumn];
			// Column "Name"
			dataColumn = dataTable.Columns["Name"];
			if(!row.IsNull(dataColumn))
				mappedObject.Name = (string)row[dataColumn];
			// Column "Status"
			dataColumn = dataTable.Columns["Status"];
			if(!row.IsNull(dataColumn))
				mappedObject.Status = (byte)row[dataColumn];
			// Column "Default_bonus_minutes_type"
			dataColumn = dataTable.Columns["Default_bonus_minutes_type"];
			if(!row.IsNull(dataColumn))
				mappedObject.Default_bonus_minutes_type = (byte)row[dataColumn];
			// Column "Default_start_bonus_minutes"
			dataColumn = dataTable.Columns["Default_start_bonus_minutes"];
			if(!row.IsNull(dataColumn))
				mappedObject.Default_start_bonus_minutes = (short)row[dataColumn];
			// Column "Is_prepaid"
			dataColumn = dataTable.Columns["Is_prepaid"];
			if(!row.IsNull(dataColumn))
				mappedObject.Is_prepaid = (byte)row[dataColumn];
			// Column "Current_amount"
			dataColumn = dataTable.Columns["Current_amount"];
			if(!row.IsNull(dataColumn))
				mappedObject.Current_amount = (decimal)row[dataColumn];
			// Column "Limit_amount"
			dataColumn = dataTable.Columns["Limit_amount"];
			if(!row.IsNull(dataColumn))
				mappedObject.Limit_amount = (decimal)row[dataColumn];
			// Column "Warning_amount"
			dataColumn = dataTable.Columns["Warning_amount"];
			if(!row.IsNull(dataColumn))
				mappedObject.Warning_amount = (decimal)row[dataColumn];
			// Column "Allow_rerouting"
			dataColumn = dataTable.Columns["Allow_rerouting"];
			if(!row.IsNull(dataColumn))
				mappedObject.Allow_rerouting = (byte)row[dataColumn];
			// Column "Concurrent_use"
			dataColumn = dataTable.Columns["Concurrent_use"];
			if(!row.IsNull(dataColumn))
				mappedObject.Concurrent_use = (byte)row[dataColumn];
			// Column "Prefix_in_type_id"
			dataColumn = dataTable.Columns["Prefix_in_type_id"];
			if(!row.IsNull(dataColumn))
				mappedObject.Prefix_in_type_id = (short)row[dataColumn];
			// Column "Prefix_in"
			dataColumn = dataTable.Columns["Prefix_in"];
			if(!row.IsNull(dataColumn))
				mappedObject.Prefix_in = (string)row[dataColumn];
			// Column "Prefix_out"
			dataColumn = dataTable.Columns["Prefix_out"];
			if(!row.IsNull(dataColumn))
				mappedObject.Prefix_out = (string)row[dataColumn];
			// Column "Partner_id"
			dataColumn = dataTable.Columns["Partner_id"];
			if(!row.IsNull(dataColumn))
				mappedObject.Partner_id = (int)row[dataColumn];
			// Column "Service_id"
			dataColumn = dataTable.Columns["Service_id"];
			if(!row.IsNull(dataColumn))
				mappedObject.Service_id = (short)row[dataColumn];
			// Column "Retail_calling_plan_id"
			dataColumn = dataTable.Columns["Retail_calling_plan_id"];
			if(!row.IsNull(dataColumn))
				mappedObject.Retail_calling_plan_id = (int)row[dataColumn];
			// Column "Retail_markup_type"
			dataColumn = dataTable.Columns["Retail_markup_type"];
			if(!row.IsNull(dataColumn))
				mappedObject.Retail_markup_type = (byte)row[dataColumn];
			// Column "Retail_markup_dollar"
			dataColumn = dataTable.Columns["Retail_markup_dollar"];
			if(!row.IsNull(dataColumn))
				mappedObject.Retail_markup_dollar = (decimal)row[dataColumn];
			// Column "Retail_markup_percent"
			dataColumn = dataTable.Columns["Retail_markup_percent"];
			if(!row.IsNull(dataColumn))
				mappedObject.Retail_markup_percent = (decimal)row[dataColumn];
			// Column "Max_call_length"
			dataColumn = dataTable.Columns["Max_call_length"];
			if(!row.IsNull(dataColumn))
				mappedObject.Max_call_length = (short)row[dataColumn];
			// Column "Routing_plan_id"
			dataColumn = dataTable.Columns["Routing_plan_id"];
			if(!row.IsNull(dataColumn))
				mappedObject.Routing_plan_id = (int)row[dataColumn];
			return mappedObject;
		}

		/// <summary>
		/// Creates a <see cref="System.Data.DataTable"/> object for 
		/// the <c>CustomerAcct</c> table.
		/// </summary>
		/// <returns>A reference to the <see cref="System.Data.DataTable"/> object.</returns>
		protected virtual DataTable CreateDataTable()
		{
			DataTable dataTable = new DataTable();
			dataTable.TableName = "CustomerAcct";
			DataColumn dataColumn;
			dataColumn = dataTable.Columns.Add("Customer_acct_id", typeof(short));
			dataColumn.Caption = "customer_acct_id";
			dataColumn.AllowDBNull = false;
			dataColumn = dataTable.Columns.Add("Name", typeof(string));
			dataColumn.Caption = "name";
			dataColumn.MaxLength = 50;
			dataColumn.AllowDBNull = false;
			dataColumn = dataTable.Columns.Add("Status", typeof(byte));
			dataColumn.Caption = "status";
			dataColumn.AllowDBNull = false;
			dataColumn = dataTable.Columns.Add("Default_bonus_minutes_type", typeof(byte));
			dataColumn.Caption = "default_bonus_minutes_type";
			dataColumn.AllowDBNull = false;
			dataColumn = dataTable.Columns.Add("Default_start_bonus_minutes", typeof(short));
			dataColumn.Caption = "default_start_bonus_minutes";
			dataColumn.AllowDBNull = false;
			dataColumn = dataTable.Columns.Add("Is_prepaid", typeof(byte));
			dataColumn.Caption = "is_prepaid";
			dataColumn.AllowDBNull = false;
			dataColumn = dataTable.Columns.Add("Current_amount", typeof(decimal));
			dataColumn.Caption = "current_amount";
			dataColumn.AllowDBNull = false;
			dataColumn.ReadOnly = true;
			dataColumn = dataTable.Columns.Add("Limit_amount", typeof(decimal));
			dataColumn.Caption = "limit_amount";
			dataColumn.AllowDBNull = false;
			dataColumn = dataTable.Columns.Add("Warning_amount", typeof(decimal));
			dataColumn.Caption = "warning_amount";
			dataColumn.AllowDBNull = false;
			dataColumn = dataTable.Columns.Add("Allow_rerouting", typeof(byte));
			dataColumn.Caption = "allow_rerouting";
			dataColumn.AllowDBNull = false;
			dataColumn = dataTable.Columns.Add("Concurrent_use", typeof(byte));
			dataColumn.Caption = "concurrent_use";
			dataColumn.AllowDBNull = false;
			dataColumn = dataTable.Columns.Add("Prefix_in_type_id", typeof(short));
			dataColumn.Caption = "prefix_in_type_id";
			dataColumn.AllowDBNull = false;
			dataColumn = dataTable.Columns.Add("Prefix_in", typeof(string));
			dataColumn.Caption = "prefix_in";
			dataColumn.MaxLength = 10;
			dataColumn.AllowDBNull = false;
			dataColumn = dataTable.Columns.Add("Prefix_out", typeof(string));
			dataColumn.Caption = "prefix_out";
			dataColumn.MaxLength = 10;
			dataColumn.AllowDBNull = false;
			dataColumn = dataTable.Columns.Add("Partner_id", typeof(int));
			dataColumn.Caption = "partner_id";
			dataColumn.AllowDBNull = false;
			dataColumn = dataTable.Columns.Add("Service_id", typeof(short));
			dataColumn.Caption = "service_id";
			dataColumn.AllowDBNull = false;
			dataColumn = dataTable.Columns.Add("Retail_calling_plan_id", typeof(int));
			dataColumn.Caption = "retail_calling_plan_id";
			dataColumn = dataTable.Columns.Add("Retail_markup_type", typeof(byte));
			dataColumn.Caption = "retail_markup_type";
			dataColumn.AllowDBNull = false;
			dataColumn = dataTable.Columns.Add("Retail_markup_dollar", typeof(decimal));
			dataColumn.Caption = "retail_markup_dollar";
			dataColumn.AllowDBNull = false;
			dataColumn = dataTable.Columns.Add("Retail_markup_percent", typeof(decimal));
			dataColumn.Caption = "retail_markup_percent";
			dataColumn.AllowDBNull = false;
			dataColumn = dataTable.Columns.Add("Max_call_length", typeof(short));
			dataColumn.Caption = "max_call_length";
			dataColumn.AllowDBNull = false;
			dataColumn = dataTable.Columns.Add("Routing_plan_id", typeof(int));
			dataColumn.Caption = "routing_plan_id";
			dataColumn.AllowDBNull = false;
			return dataTable;
		}
		
		/// <summary>
		/// Adds a new parameter to the specified command.
		/// </summary>
		/// <param name="cmd">The <see cref="System.Data.IDbCommand"/> object to add the parameter to.</param>
		/// <param name="paramName">The name of the parameter.</param>
		/// <param name="value">The value of the parameter.</param>
		/// <returns>A reference to the added parameter.</returns>
		protected virtual IDbDataParameter AddParameter(IDbCommand cmd, string paramName, object value)
		{
			IDbDataParameter parameter;
			switch(paramName)
			{
				case "Customer_acct_id":
					parameter = _db.AddParameter(cmd, paramName, DbType.Int16, value);
					break;

				case "Name":
					parameter = _db.AddParameter(cmd, paramName, DbType.AnsiString, value);
					break;

				case "Status":
					parameter = _db.AddParameter(cmd, paramName, DbType.Byte, value);
					break;

				case "Default_bonus_minutes_type":
					parameter = _db.AddParameter(cmd, paramName, DbType.Byte, value);
					break;

				case "Default_start_bonus_minutes":
					parameter = _db.AddParameter(cmd, paramName, DbType.Int16, value);
					break;

				case "Is_prepaid":
					parameter = _db.AddParameter(cmd, paramName, DbType.Byte, value);
					break;

				case "Current_amount":
					parameter = _db.AddParameter(cmd, paramName, DbType.Decimal, value);
					break;

				case "Limit_amount":
					parameter = _db.AddParameter(cmd, paramName, DbType.Decimal, value);
					break;

				case "Warning_amount":
					parameter = _db.AddParameter(cmd, paramName, DbType.Decimal, value);
					break;

				case "Allow_rerouting":
					parameter = _db.AddParameter(cmd, paramName, DbType.Byte, value);
					break;

				case "Concurrent_use":
					parameter = _db.AddParameter(cmd, paramName, DbType.Byte, value);
					break;

				case "Prefix_in_type_id":
					parameter = _db.AddParameter(cmd, paramName, DbType.Int16, value);
					break;

				case "Prefix_in":
					parameter = _db.AddParameter(cmd, paramName, DbType.AnsiString, value);
					break;

				case "Prefix_out":
					parameter = _db.AddParameter(cmd, paramName, DbType.AnsiString, value);
					break;

				case "Partner_id":
					parameter = _db.AddParameter(cmd, paramName, DbType.Int32, value);
					break;

				case "Service_id":
					parameter = _db.AddParameter(cmd, paramName, DbType.Int16, value);
					break;

				case "Retail_calling_plan_id":
					parameter = _db.AddParameter(cmd, paramName, DbType.Int32, value);
					break;

				case "Retail_markup_type":
					parameter = _db.AddParameter(cmd, paramName, DbType.Byte, value);
					break;

				case "Retail_markup_dollar":
					parameter = _db.AddParameter(cmd, paramName, DbType.Decimal, value);
					break;

				case "Retail_markup_percent":
					parameter = _db.AddParameter(cmd, paramName, DbType.Decimal, value);
					break;

				case "Max_call_length":
					parameter = _db.AddParameter(cmd, paramName, DbType.Int16, value);
					break;

				case "Routing_plan_id":
					parameter = _db.AddParameter(cmd, paramName, DbType.Int32, value);
					break;

				default:
					throw new ArgumentException("Unknown parameter name (" + paramName + ").");
			}
			return parameter;
		}


		/// <summary>
		/// Gets records for the 'CustomerAcct-CustomerAcctSupportMap-CustomerSupportVendor' relationship.
		/// </summary>
		/// <param name="vendor_id"></param>
		public virtual CustomerAcctRow[] GetByCustomerSupportVendor_Vendor_id(int vendor_id)
		{
			string whereSql = "customer_acct_id IN" + 
				"(SELECT customer_acct_id FROM [dbo].[CustomerAcctSupportMap] WHERE " +
				"vendor_id = " + _db.CreateSqlParameterName("Vendor_id") + ")";
			IDbCommand cmd = CreateGetCommand(whereSql, null);
			_db.AddParameter(cmd, "Vendor_id", DbType.Int32, vendor_id);
			return MapRecords(cmd);
		}
	} // End of CustomerAcctCollection_Base class
}  // End of namespace

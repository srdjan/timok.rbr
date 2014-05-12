// <fileinfo name="Base\ServiceCollection_Base.cs">
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
	/// The base class for <see cref="ServiceCollection"/>. Provides methods 
	/// for common database table operations. 
	/// </summary>
	/// <remarks>
	/// Do not change this source code. Update the <see cref="ServiceCollection"/>
	/// class if you need to add or change some functionality.
	/// </remarks>
	public abstract class ServiceCollection_Base
	{
		// Constants
		public const string Service_idColumnName = "service_id";
		public const string NameColumnName = "name";
		public const string Virtual_switch_idColumnName = "virtual_switch_id";
		public const string Calling_plan_idColumnName = "calling_plan_id";
		public const string Default_routing_plan_idColumnName = "default_routing_plan_id";
		public const string StatusColumnName = "status";
		public const string TypeColumnName = "type";
		public const string Retail_typeColumnName = "retail_type";
		public const string Is_sharedColumnName = "is_shared";
		public const string Rating_typeColumnName = "rating_type";
		public const string Pin_lengthColumnName = "pin_length";
		public const string Payphone_surcharge_idColumnName = "payphone_surcharge_id";
		public const string Sweep_schedule_idColumnName = "sweep_schedule_id";
		public const string Sweep_feeColumnName = "sweep_fee";
		public const string Sweep_ruleColumnName = "sweep_rule";
		public const string Balance_prompt_typeColumnName = "balance_prompt_type";
		public const string Balance_prompt_per_unitColumnName = "balance_prompt_per_unit";

		// Instance fields
		private Rbr_Db _db;

		/// <summary>
		/// Initializes a new instance of the <see cref="ServiceCollection_Base"/> 
		/// class with the specified <see cref="Rbr_Db"/>.
		/// </summary>
		/// <param name="db">The <see cref="Rbr_Db"/> object.</param>
		public ServiceCollection_Base(Rbr_Db db)
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
		/// Gets an array of all records from the <c>Service</c> table.
		/// </summary>
		/// <returns>An array of <see cref="ServiceRow"/> objects.</returns>
		public virtual ServiceRow[] GetAll()
		{
			return MapRecords(CreateGetAllCommand());
		}

		/// <summary>
		/// Gets a <see cref="System.Data.DataTable"/> object that 
		/// includes all records from the <c>Service</c> table.
		/// </summary>
		/// <returns>A reference to the <see cref="System.Data.DataTable"/> object.</returns>
		public virtual DataTable GetAllAsDataTable()
		{
			return MapRecordsToDataTable(CreateGetAllCommand());
		}

		/// <summary>
		/// Creates and returns an <see cref="System.Data.IDbCommand"/> object that is used
		/// to retrieve all records from the <c>Service</c> table.
		/// </summary>
		/// <returns>A reference to the <see cref="System.Data.IDbCommand"/> object.</returns>
		protected virtual IDbCommand CreateGetAllCommand()
		{
			return CreateGetCommand(null, null);
		}

		/// <summary>
		/// Gets the first <see cref="ServiceRow"/> objects that 
		/// match the search condition.
		/// </summary>
		/// <param name="whereSql">The SQL search condition. For example: 
		/// <c>"FirstName='Smith' AND Zip=75038"</c>.</param>
		/// <returns>An instance of <see cref="ServiceRow"/> or null reference 
		/// (Nothing in Visual Basic) if the object was not found.</returns>
		public ServiceRow GetRow(string whereSql)
		{
			int totalRecordCount = -1;
			ServiceRow[] rows = GetAsArray(whereSql, null, 0, 1, ref totalRecordCount);
			return 0 == rows.Length ? null : rows[0];
		}

		/// <summary>
		/// Gets an array of <see cref="ServiceRow"/> objects that 
		/// match the search condition, in the the specified sort order.
		/// </summary>
		/// <param name="whereSql">The SQL search condition. For example: 
		/// <c>"FirstName='Smith' AND Zip=75038"</c>.</param>
		/// <param name="orderBySql">The column name(s) followed by "ASC" (ascending) or "DESC" (descending).
		/// Columns are sorted in ascending order by default. For example: <c>"LastName ASC, FirstName ASC"</c>.</param>
		/// <returns>An array of <see cref="ServiceRow"/> objects.</returns>
		public ServiceRow[] GetAsArray(string whereSql, string orderBySql)
		{
			int totalRecordCount = -1;
			return GetAsArray(whereSql, orderBySql, 0, int.MaxValue, ref totalRecordCount);
		}

		/// <summary>
		/// Gets an array of <see cref="ServiceRow"/> objects that 
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
		/// <returns>An array of <see cref="ServiceRow"/> objects.</returns>
		public virtual ServiceRow[] GetAsArray(string whereSql, string orderBySql,
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
			string sql = "SELECT * FROM [dbo].[Service]";
			if(null != whereSql && 0 < whereSql.Length)
				sql += " WHERE " + whereSql;
			if(null != orderBySql && 0 < orderBySql.Length)
				sql += " ORDER BY " + orderBySql;
			return _db.CreateCommand(sql);
		}

		/// <summary>
		/// Gets <see cref="ServiceRow"/> by the primary key.
		/// </summary>
		/// <param name="service_id">The <c>service_id</c> column value.</param>
		/// <returns>An instance of <see cref="ServiceRow"/> or null reference 
		/// (Nothing in Visual Basic) if the object was not found.</returns>
		public virtual ServiceRow GetByPrimaryKey(short service_id)
		{
			string whereSql = "[service_id]=" + _db.CreateSqlParameterName("Service_id");
			IDbCommand cmd = CreateGetCommand(whereSql, null);
			AddParameter(cmd, "Service_id", service_id);
			ServiceRow[] tempArray = MapRecords(cmd);
			return 0 == tempArray.Length ? null : tempArray[0];
		}

		/// <summary>
		/// Gets an array of <see cref="ServiceRow"/> objects 
		/// by the <c>FK_Service_CallingPlan</c> foreign key.
		/// </summary>
		/// <param name="calling_plan_id">The <c>calling_plan_id</c> column value.</param>
		/// <returns>An array of <see cref="ServiceRow"/> objects.</returns>
		public virtual ServiceRow[] GetByCalling_plan_id(int calling_plan_id)
		{
			return MapRecords(CreateGetByCalling_plan_idCommand(calling_plan_id));
		}

		/// <summary>
		/// Gets a <see cref="System.Data.DataTable"/> object 
		/// by the <c>FK_Service_CallingPlan</c> foreign key.
		/// </summary>
		/// <param name="calling_plan_id">The <c>calling_plan_id</c> column value.</param>
		/// <returns>A reference to the <see cref="System.Data.DataTable"/> object.</returns>
		public virtual DataTable GetByCalling_plan_idAsDataTable(int calling_plan_id)
		{
			return MapRecordsToDataTable(CreateGetByCalling_plan_idCommand(calling_plan_id));
		}

		/// <summary>
		/// Creates an <see cref="System.Data.IDbCommand"/> object that can be used to 
		/// return records by the <c>FK_Service_CallingPlan</c> foreign key.
		/// </summary>
		/// <param name="calling_plan_id">The <c>calling_plan_id</c> column value.</param>
		/// <returns>A reference to the <see cref="System.Data.IDbCommand"/> object.</returns>
		protected virtual IDbCommand CreateGetByCalling_plan_idCommand(int calling_plan_id)
		{
			string whereSql = "";
			whereSql += "[calling_plan_id]=" + _db.CreateSqlParameterName("Calling_plan_id");

			IDbCommand cmd = CreateGetCommand(whereSql, null);
			AddParameter(cmd, "Calling_plan_id", calling_plan_id);
			return cmd;
		}

		/// <summary>
		/// Gets an array of <see cref="ServiceRow"/> objects 
		/// by the <c>FK_Service_PayphoneSurcharge</c> foreign key.
		/// </summary>
		/// <param name="payphone_surcharge_id">The <c>payphone_surcharge_id</c> column value.</param>
		/// <returns>An array of <see cref="ServiceRow"/> objects.</returns>
		public ServiceRow[] GetByPayphone_surcharge_id(int payphone_surcharge_id)
		{
			return GetByPayphone_surcharge_id(payphone_surcharge_id, false);
		}

		/// <summary>
		/// Gets an array of <see cref="ServiceRow"/> objects 
		/// by the <c>FK_Service_PayphoneSurcharge</c> foreign key.
		/// </summary>
		/// <param name="payphone_surcharge_id">The <c>payphone_surcharge_id</c> column value.</param>
		/// <param name="payphone_surcharge_idNull">true if the method ignores the payphone_surcharge_id
		/// parameter value and uses DbNull instead of it; otherwise, false.</param>
		/// <returns>An array of <see cref="ServiceRow"/> objects.</returns>
		public virtual ServiceRow[] GetByPayphone_surcharge_id(int payphone_surcharge_id, bool payphone_surcharge_idNull)
		{
			return MapRecords(CreateGetByPayphone_surcharge_idCommand(payphone_surcharge_id, payphone_surcharge_idNull));
		}

		/// <summary>
		/// Gets a <see cref="System.Data.DataTable"/> object 
		/// by the <c>FK_Service_PayphoneSurcharge</c> foreign key.
		/// </summary>
		/// <param name="payphone_surcharge_id">The <c>payphone_surcharge_id</c> column value.</param>
		/// <returns>A reference to the <see cref="System.Data.DataTable"/> object.</returns>
		public DataTable GetByPayphone_surcharge_idAsDataTable(int payphone_surcharge_id)
		{
			return GetByPayphone_surcharge_idAsDataTable(payphone_surcharge_id, false);
		}

		/// <summary>
		/// Gets a <see cref="System.Data.DataTable"/> object 
		/// by the <c>FK_Service_PayphoneSurcharge</c> foreign key.
		/// </summary>
		/// <param name="payphone_surcharge_id">The <c>payphone_surcharge_id</c> column value.</param>
		/// <param name="payphone_surcharge_idNull">true if the method ignores the payphone_surcharge_id
		/// parameter value and uses DbNull instead of it; otherwise, false.</param>
		/// <returns>A reference to the <see cref="System.Data.DataTable"/> object.</returns>
		public virtual DataTable GetByPayphone_surcharge_idAsDataTable(int payphone_surcharge_id, bool payphone_surcharge_idNull)
		{
			return MapRecordsToDataTable(CreateGetByPayphone_surcharge_idCommand(payphone_surcharge_id, payphone_surcharge_idNull));
		}

		/// <summary>
		/// Creates an <see cref="System.Data.IDbCommand"/> object that can be used to 
		/// return records by the <c>FK_Service_PayphoneSurcharge</c> foreign key.
		/// </summary>
		/// <param name="payphone_surcharge_id">The <c>payphone_surcharge_id</c> column value.</param>
		/// <param name="payphone_surcharge_idNull">true if the method ignores the payphone_surcharge_id
		/// parameter value and uses DbNull instead of it; otherwise, false.</param>
		/// <returns>A reference to the <see cref="System.Data.IDbCommand"/> object.</returns>
		protected virtual IDbCommand CreateGetByPayphone_surcharge_idCommand(int payphone_surcharge_id, bool payphone_surcharge_idNull)
		{
			string whereSql = "";
			if(payphone_surcharge_idNull)
				whereSql += "[payphone_surcharge_id] IS NULL";
			else
				whereSql += "[payphone_surcharge_id]=" + _db.CreateSqlParameterName("Payphone_surcharge_id");

			IDbCommand cmd = CreateGetCommand(whereSql, null);
			if(!payphone_surcharge_idNull)
				AddParameter(cmd, "Payphone_surcharge_id", payphone_surcharge_id);
			return cmd;
		}

		/// <summary>
		/// Gets an array of <see cref="ServiceRow"/> objects 
		/// by the <c>R_269</c> foreign key.
		/// </summary>
		/// <param name="sweep_schedule_id">The <c>sweep_schedule_id</c> column value.</param>
		/// <returns>An array of <see cref="ServiceRow"/> objects.</returns>
		public ServiceRow[] GetBySweep_schedule_id(int sweep_schedule_id)
		{
			return GetBySweep_schedule_id(sweep_schedule_id, false);
		}

		/// <summary>
		/// Gets an array of <see cref="ServiceRow"/> objects 
		/// by the <c>R_269</c> foreign key.
		/// </summary>
		/// <param name="sweep_schedule_id">The <c>sweep_schedule_id</c> column value.</param>
		/// <param name="sweep_schedule_idNull">true if the method ignores the sweep_schedule_id
		/// parameter value and uses DbNull instead of it; otherwise, false.</param>
		/// <returns>An array of <see cref="ServiceRow"/> objects.</returns>
		public virtual ServiceRow[] GetBySweep_schedule_id(int sweep_schedule_id, bool sweep_schedule_idNull)
		{
			return MapRecords(CreateGetBySweep_schedule_idCommand(sweep_schedule_id, sweep_schedule_idNull));
		}

		/// <summary>
		/// Gets a <see cref="System.Data.DataTable"/> object 
		/// by the <c>R_269</c> foreign key.
		/// </summary>
		/// <param name="sweep_schedule_id">The <c>sweep_schedule_id</c> column value.</param>
		/// <returns>A reference to the <see cref="System.Data.DataTable"/> object.</returns>
		public DataTable GetBySweep_schedule_idAsDataTable(int sweep_schedule_id)
		{
			return GetBySweep_schedule_idAsDataTable(sweep_schedule_id, false);
		}

		/// <summary>
		/// Gets a <see cref="System.Data.DataTable"/> object 
		/// by the <c>R_269</c> foreign key.
		/// </summary>
		/// <param name="sweep_schedule_id">The <c>sweep_schedule_id</c> column value.</param>
		/// <param name="sweep_schedule_idNull">true if the method ignores the sweep_schedule_id
		/// parameter value and uses DbNull instead of it; otherwise, false.</param>
		/// <returns>A reference to the <see cref="System.Data.DataTable"/> object.</returns>
		public virtual DataTable GetBySweep_schedule_idAsDataTable(int sweep_schedule_id, bool sweep_schedule_idNull)
		{
			return MapRecordsToDataTable(CreateGetBySweep_schedule_idCommand(sweep_schedule_id, sweep_schedule_idNull));
		}

		/// <summary>
		/// Creates an <see cref="System.Data.IDbCommand"/> object that can be used to 
		/// return records by the <c>R_269</c> foreign key.
		/// </summary>
		/// <param name="sweep_schedule_id">The <c>sweep_schedule_id</c> column value.</param>
		/// <param name="sweep_schedule_idNull">true if the method ignores the sweep_schedule_id
		/// parameter value and uses DbNull instead of it; otherwise, false.</param>
		/// <returns>A reference to the <see cref="System.Data.IDbCommand"/> object.</returns>
		protected virtual IDbCommand CreateGetBySweep_schedule_idCommand(int sweep_schedule_id, bool sweep_schedule_idNull)
		{
			string whereSql = "";
			if(sweep_schedule_idNull)
				whereSql += "[sweep_schedule_id] IS NULL";
			else
				whereSql += "[sweep_schedule_id]=" + _db.CreateSqlParameterName("Sweep_schedule_id");

			IDbCommand cmd = CreateGetCommand(whereSql, null);
			if(!sweep_schedule_idNull)
				AddParameter(cmd, "Sweep_schedule_id", sweep_schedule_id);
			return cmd;
		}

		/// <summary>
		/// Gets an array of <see cref="ServiceRow"/> objects 
		/// by the <c>R_299</c> foreign key.
		/// </summary>
		/// <param name="virtual_switch_id">The <c>virtual_switch_id</c> column value.</param>
		/// <returns>An array of <see cref="ServiceRow"/> objects.</returns>
		public virtual ServiceRow[] GetByVirtual_switch_id(int virtual_switch_id)
		{
			return MapRecords(CreateGetByVirtual_switch_idCommand(virtual_switch_id));
		}

		/// <summary>
		/// Gets a <see cref="System.Data.DataTable"/> object 
		/// by the <c>R_299</c> foreign key.
		/// </summary>
		/// <param name="virtual_switch_id">The <c>virtual_switch_id</c> column value.</param>
		/// <returns>A reference to the <see cref="System.Data.DataTable"/> object.</returns>
		public virtual DataTable GetByVirtual_switch_idAsDataTable(int virtual_switch_id)
		{
			return MapRecordsToDataTable(CreateGetByVirtual_switch_idCommand(virtual_switch_id));
		}

		/// <summary>
		/// Creates an <see cref="System.Data.IDbCommand"/> object that can be used to 
		/// return records by the <c>R_299</c> foreign key.
		/// </summary>
		/// <param name="virtual_switch_id">The <c>virtual_switch_id</c> column value.</param>
		/// <returns>A reference to the <see cref="System.Data.IDbCommand"/> object.</returns>
		protected virtual IDbCommand CreateGetByVirtual_switch_idCommand(int virtual_switch_id)
		{
			string whereSql = "";
			whereSql += "[virtual_switch_id]=" + _db.CreateSqlParameterName("Virtual_switch_id");

			IDbCommand cmd = CreateGetCommand(whereSql, null);
			AddParameter(cmd, "Virtual_switch_id", virtual_switch_id);
			return cmd;
		}

		/// <summary>
		/// Gets an array of <see cref="ServiceRow"/> objects 
		/// by the <c>R_346</c> foreign key.
		/// </summary>
		/// <param name="default_routing_plan_id">The <c>default_routing_plan_id</c> column value.</param>
		/// <returns>An array of <see cref="ServiceRow"/> objects.</returns>
		public virtual ServiceRow[] GetByDefault_routing_plan_id(int default_routing_plan_id)
		{
			return MapRecords(CreateGetByDefault_routing_plan_idCommand(default_routing_plan_id));
		}

		/// <summary>
		/// Gets a <see cref="System.Data.DataTable"/> object 
		/// by the <c>R_346</c> foreign key.
		/// </summary>
		/// <param name="default_routing_plan_id">The <c>default_routing_plan_id</c> column value.</param>
		/// <returns>A reference to the <see cref="System.Data.DataTable"/> object.</returns>
		public virtual DataTable GetByDefault_routing_plan_idAsDataTable(int default_routing_plan_id)
		{
			return MapRecordsToDataTable(CreateGetByDefault_routing_plan_idCommand(default_routing_plan_id));
		}

		/// <summary>
		/// Creates an <see cref="System.Data.IDbCommand"/> object that can be used to 
		/// return records by the <c>R_346</c> foreign key.
		/// </summary>
		/// <param name="default_routing_plan_id">The <c>default_routing_plan_id</c> column value.</param>
		/// <returns>A reference to the <see cref="System.Data.IDbCommand"/> object.</returns>
		protected virtual IDbCommand CreateGetByDefault_routing_plan_idCommand(int default_routing_plan_id)
		{
			string whereSql = "";
			whereSql += "[default_routing_plan_id]=" + _db.CreateSqlParameterName("Default_routing_plan_id");

			IDbCommand cmd = CreateGetCommand(whereSql, null);
			AddParameter(cmd, "Default_routing_plan_id", default_routing_plan_id);
			return cmd;
		}

		/// <summary>
		/// Adds a new record into the <c>Service</c> table.
		/// </summary>
		/// <param name="value">The <see cref="ServiceRow"/> object to be inserted.</param>
		public virtual void Insert(ServiceRow value)
		{
			string sqlStr = "INSERT INTO [dbo].[Service] (" +
				"[service_id], " +
				"[name], " +
				"[virtual_switch_id], " +
				"[calling_plan_id], " +
				"[default_routing_plan_id], " +
				"[status], " +
				"[type], " +
				"[retail_type], " +
				"[is_shared], " +
				"[rating_type], " +
				"[pin_length], " +
				"[payphone_surcharge_id], " +
				"[sweep_schedule_id], " +
				"[sweep_fee], " +
				"[sweep_rule], " +
				"[balance_prompt_type], " +
				"[balance_prompt_per_unit]" +
				") VALUES (" +
				_db.CreateSqlParameterName("Service_id") + ", " +
				_db.CreateSqlParameterName("Name") + ", " +
				_db.CreateSqlParameterName("Virtual_switch_id") + ", " +
				_db.CreateSqlParameterName("Calling_plan_id") + ", " +
				_db.CreateSqlParameterName("Default_routing_plan_id") + ", " +
				_db.CreateSqlParameterName("Status") + ", " +
				_db.CreateSqlParameterName("Type") + ", " +
				_db.CreateSqlParameterName("Retail_type") + ", " +
				_db.CreateSqlParameterName("Is_shared") + ", " +
				_db.CreateSqlParameterName("Rating_type") + ", " +
				_db.CreateSqlParameterName("Pin_length") + ", " +
				_db.CreateSqlParameterName("Payphone_surcharge_id") + ", " +
				_db.CreateSqlParameterName("Sweep_schedule_id") + ", " +
				_db.CreateSqlParameterName("Sweep_fee") + ", " +
				_db.CreateSqlParameterName("Sweep_rule") + ", " +
				_db.CreateSqlParameterName("Balance_prompt_type") + ", " +
				_db.CreateSqlParameterName("Balance_prompt_per_unit") + ")";
			IDbCommand cmd = _db.CreateCommand(sqlStr);
			AddParameter(cmd, "Service_id", value.Service_id);
			AddParameter(cmd, "Name", value.Name);
			AddParameter(cmd, "Virtual_switch_id", value.Virtual_switch_id);
			AddParameter(cmd, "Calling_plan_id", value.Calling_plan_id);
			AddParameter(cmd, "Default_routing_plan_id", value.Default_routing_plan_id);
			AddParameter(cmd, "Status", value.Status);
			AddParameter(cmd, "Type", value.Type);
			AddParameter(cmd, "Retail_type", value.Retail_type);
			AddParameter(cmd, "Is_shared", value.Is_shared);
			AddParameter(cmd, "Rating_type", value.Rating_type);
			AddParameter(cmd, "Pin_length", value.Pin_length);
			AddParameter(cmd, "Payphone_surcharge_id",
				value.IsPayphone_surcharge_idNull ? DBNull.Value : (object)value.Payphone_surcharge_id);
			AddParameter(cmd, "Sweep_schedule_id",
				value.IsSweep_schedule_idNull ? DBNull.Value : (object)value.Sweep_schedule_id);
			AddParameter(cmd, "Sweep_fee", value.Sweep_fee);
			AddParameter(cmd, "Sweep_rule", value.Sweep_rule);
			AddParameter(cmd, "Balance_prompt_type", value.Balance_prompt_type);
			AddParameter(cmd, "Balance_prompt_per_unit", value.Balance_prompt_per_unit);
			cmd.ExecuteNonQuery();
		}

		/// <summary>
		/// Updates a record in the <c>Service</c> table.
		/// </summary>
		/// <param name="value">The <see cref="ServiceRow"/>
		/// object used to update the table record.</param>
		/// <returns>true if the record was updated; otherwise, false.</returns>
		public virtual bool Update(ServiceRow value)
		{
			string sqlStr = "UPDATE [dbo].[Service] SET " +
				"[name]=" + _db.CreateSqlParameterName("Name") + ", " +
				"[virtual_switch_id]=" + _db.CreateSqlParameterName("Virtual_switch_id") + ", " +
				"[calling_plan_id]=" + _db.CreateSqlParameterName("Calling_plan_id") + ", " +
				"[default_routing_plan_id]=" + _db.CreateSqlParameterName("Default_routing_plan_id") + ", " +
				"[status]=" + _db.CreateSqlParameterName("Status") + ", " +
				"[type]=" + _db.CreateSqlParameterName("Type") + ", " +
				"[retail_type]=" + _db.CreateSqlParameterName("Retail_type") + ", " +
				"[is_shared]=" + _db.CreateSqlParameterName("Is_shared") + ", " +
				"[rating_type]=" + _db.CreateSqlParameterName("Rating_type") + ", " +
				"[pin_length]=" + _db.CreateSqlParameterName("Pin_length") + ", " +
				"[payphone_surcharge_id]=" + _db.CreateSqlParameterName("Payphone_surcharge_id") + ", " +
				"[sweep_schedule_id]=" + _db.CreateSqlParameterName("Sweep_schedule_id") + ", " +
				"[sweep_fee]=" + _db.CreateSqlParameterName("Sweep_fee") + ", " +
				"[sweep_rule]=" + _db.CreateSqlParameterName("Sweep_rule") + ", " +
				"[balance_prompt_type]=" + _db.CreateSqlParameterName("Balance_prompt_type") + ", " +
				"[balance_prompt_per_unit]=" + _db.CreateSqlParameterName("Balance_prompt_per_unit") +
				" WHERE " +
				"[service_id]=" + _db.CreateSqlParameterName("Service_id");
			IDbCommand cmd = _db.CreateCommand(sqlStr);
			AddParameter(cmd, "Name", value.Name);
			AddParameter(cmd, "Virtual_switch_id", value.Virtual_switch_id);
			AddParameter(cmd, "Calling_plan_id", value.Calling_plan_id);
			AddParameter(cmd, "Default_routing_plan_id", value.Default_routing_plan_id);
			AddParameter(cmd, "Status", value.Status);
			AddParameter(cmd, "Type", value.Type);
			AddParameter(cmd, "Retail_type", value.Retail_type);
			AddParameter(cmd, "Is_shared", value.Is_shared);
			AddParameter(cmd, "Rating_type", value.Rating_type);
			AddParameter(cmd, "Pin_length", value.Pin_length);
			AddParameter(cmd, "Payphone_surcharge_id",
				value.IsPayphone_surcharge_idNull ? DBNull.Value : (object)value.Payphone_surcharge_id);
			AddParameter(cmd, "Sweep_schedule_id",
				value.IsSweep_schedule_idNull ? DBNull.Value : (object)value.Sweep_schedule_id);
			AddParameter(cmd, "Sweep_fee", value.Sweep_fee);
			AddParameter(cmd, "Sweep_rule", value.Sweep_rule);
			AddParameter(cmd, "Balance_prompt_type", value.Balance_prompt_type);
			AddParameter(cmd, "Balance_prompt_per_unit", value.Balance_prompt_per_unit);
			AddParameter(cmd, "Service_id", value.Service_id);
			return 0 != cmd.ExecuteNonQuery();
		}

		/// <summary>
		/// Updates the <c>Service</c> table and calls the <c>AcceptChanges</c> method
		/// on the changed DataRow objects.
		/// </summary>
		/// <param name="table">The <see cref="System.Data.DataTable"/> used to update the data source.</param>
		public void Update(DataTable table)
		{
			Update(table, true);
		}

		/// <summary>
		/// Updates the <c>Service</c> table. Pass <c>false</c> as the <c>acceptChanges</c> 
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
							DeleteByPrimaryKey((short)row["Service_id"]);
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
		/// Deletes the specified object from the <c>Service</c> table.
		/// </summary>
		/// <param name="value">The <see cref="ServiceRow"/> object to delete.</param>
		/// <returns>true if the record was deleted; otherwise, false.</returns>
		public bool Delete(ServiceRow value)
		{
			return DeleteByPrimaryKey(value.Service_id);
		}

		/// <summary>
		/// Deletes a record from the <c>Service</c> table using
		/// the specified primary key.
		/// </summary>
		/// <param name="service_id">The <c>service_id</c> column value.</param>
		/// <returns>true if the record was deleted; otherwise, false.</returns>
		public virtual bool DeleteByPrimaryKey(short service_id)
		{
			string whereSql = "[service_id]=" + _db.CreateSqlParameterName("Service_id");
			IDbCommand cmd = CreateDeleteCommand(whereSql);
			AddParameter(cmd, "Service_id", service_id);
			return 0 < cmd.ExecuteNonQuery();
		}

		/// <summary>
		/// Deletes records from the <c>Service</c> table using the 
		/// <c>FK_Service_CallingPlan</c> foreign key.
		/// </summary>
		/// <param name="calling_plan_id">The <c>calling_plan_id</c> column value.</param>
		/// <returns>The number of records deleted from the table.</returns>
		public int DeleteByCalling_plan_id(int calling_plan_id)
		{
			return CreateDeleteByCalling_plan_idCommand(calling_plan_id).ExecuteNonQuery();
		}

		/// <summary>
		/// Creates an <see cref="System.Data.IDbCommand"/> object that can be used to
		/// delete records using the <c>FK_Service_CallingPlan</c> foreign key.
		/// </summary>
		/// <param name="calling_plan_id">The <c>calling_plan_id</c> column value.</param>
		/// <returns>A reference to the <see cref="System.Data.IDbCommand"/> object.</returns>
		protected virtual IDbCommand CreateDeleteByCalling_plan_idCommand(int calling_plan_id)
		{
			string whereSql = "";
			whereSql += "[calling_plan_id]=" + _db.CreateSqlParameterName("Calling_plan_id");

			IDbCommand cmd = CreateDeleteCommand(whereSql);
			AddParameter(cmd, "Calling_plan_id", calling_plan_id);
			return cmd;
		}

		/// <summary>
		/// Deletes records from the <c>Service</c> table using the 
		/// <c>FK_Service_PayphoneSurcharge</c> foreign key.
		/// </summary>
		/// <param name="payphone_surcharge_id">The <c>payphone_surcharge_id</c> column value.</param>
		/// <returns>The number of records deleted from the table.</returns>
		public int DeleteByPayphone_surcharge_id(int payphone_surcharge_id)
		{
			return DeleteByPayphone_surcharge_id(payphone_surcharge_id, false);
		}

		/// <summary>
		/// Deletes records from the <c>Service</c> table using the 
		/// <c>FK_Service_PayphoneSurcharge</c> foreign key.
		/// </summary>
		/// <param name="payphone_surcharge_id">The <c>payphone_surcharge_id</c> column value.</param>
		/// <param name="payphone_surcharge_idNull">true if the method ignores the payphone_surcharge_id
		/// parameter value and uses DbNull instead of it; otherwise, false.</param>
		/// <returns>The number of records deleted from the table.</returns>
		public int DeleteByPayphone_surcharge_id(int payphone_surcharge_id, bool payphone_surcharge_idNull)
		{
			return CreateDeleteByPayphone_surcharge_idCommand(payphone_surcharge_id, payphone_surcharge_idNull).ExecuteNonQuery();
		}

		/// <summary>
		/// Creates an <see cref="System.Data.IDbCommand"/> object that can be used to
		/// delete records using the <c>FK_Service_PayphoneSurcharge</c> foreign key.
		/// </summary>
		/// <param name="payphone_surcharge_id">The <c>payphone_surcharge_id</c> column value.</param>
		/// <param name="payphone_surcharge_idNull">true if the method ignores the payphone_surcharge_id
		/// parameter value and uses DbNull instead of it; otherwise, false.</param>
		/// <returns>A reference to the <see cref="System.Data.IDbCommand"/> object.</returns>
		protected virtual IDbCommand CreateDeleteByPayphone_surcharge_idCommand(int payphone_surcharge_id, bool payphone_surcharge_idNull)
		{
			string whereSql = "";
			if(payphone_surcharge_idNull)
				whereSql += "[payphone_surcharge_id] IS NULL";
			else
				whereSql += "[payphone_surcharge_id]=" + _db.CreateSqlParameterName("Payphone_surcharge_id");

			IDbCommand cmd = CreateDeleteCommand(whereSql);
			if(!payphone_surcharge_idNull)
				AddParameter(cmd, "Payphone_surcharge_id", payphone_surcharge_id);
			return cmd;
		}

		/// <summary>
		/// Deletes records from the <c>Service</c> table using the 
		/// <c>R_269</c> foreign key.
		/// </summary>
		/// <param name="sweep_schedule_id">The <c>sweep_schedule_id</c> column value.</param>
		/// <returns>The number of records deleted from the table.</returns>
		public int DeleteBySweep_schedule_id(int sweep_schedule_id)
		{
			return DeleteBySweep_schedule_id(sweep_schedule_id, false);
		}

		/// <summary>
		/// Deletes records from the <c>Service</c> table using the 
		/// <c>R_269</c> foreign key.
		/// </summary>
		/// <param name="sweep_schedule_id">The <c>sweep_schedule_id</c> column value.</param>
		/// <param name="sweep_schedule_idNull">true if the method ignores the sweep_schedule_id
		/// parameter value and uses DbNull instead of it; otherwise, false.</param>
		/// <returns>The number of records deleted from the table.</returns>
		public int DeleteBySweep_schedule_id(int sweep_schedule_id, bool sweep_schedule_idNull)
		{
			return CreateDeleteBySweep_schedule_idCommand(sweep_schedule_id, sweep_schedule_idNull).ExecuteNonQuery();
		}

		/// <summary>
		/// Creates an <see cref="System.Data.IDbCommand"/> object that can be used to
		/// delete records using the <c>R_269</c> foreign key.
		/// </summary>
		/// <param name="sweep_schedule_id">The <c>sweep_schedule_id</c> column value.</param>
		/// <param name="sweep_schedule_idNull">true if the method ignores the sweep_schedule_id
		/// parameter value and uses DbNull instead of it; otherwise, false.</param>
		/// <returns>A reference to the <see cref="System.Data.IDbCommand"/> object.</returns>
		protected virtual IDbCommand CreateDeleteBySweep_schedule_idCommand(int sweep_schedule_id, bool sweep_schedule_idNull)
		{
			string whereSql = "";
			if(sweep_schedule_idNull)
				whereSql += "[sweep_schedule_id] IS NULL";
			else
				whereSql += "[sweep_schedule_id]=" + _db.CreateSqlParameterName("Sweep_schedule_id");

			IDbCommand cmd = CreateDeleteCommand(whereSql);
			if(!sweep_schedule_idNull)
				AddParameter(cmd, "Sweep_schedule_id", sweep_schedule_id);
			return cmd;
		}

		/// <summary>
		/// Deletes records from the <c>Service</c> table using the 
		/// <c>R_299</c> foreign key.
		/// </summary>
		/// <param name="virtual_switch_id">The <c>virtual_switch_id</c> column value.</param>
		/// <returns>The number of records deleted from the table.</returns>
		public int DeleteByVirtual_switch_id(int virtual_switch_id)
		{
			return CreateDeleteByVirtual_switch_idCommand(virtual_switch_id).ExecuteNonQuery();
		}

		/// <summary>
		/// Creates an <see cref="System.Data.IDbCommand"/> object that can be used to
		/// delete records using the <c>R_299</c> foreign key.
		/// </summary>
		/// <param name="virtual_switch_id">The <c>virtual_switch_id</c> column value.</param>
		/// <returns>A reference to the <see cref="System.Data.IDbCommand"/> object.</returns>
		protected virtual IDbCommand CreateDeleteByVirtual_switch_idCommand(int virtual_switch_id)
		{
			string whereSql = "";
			whereSql += "[virtual_switch_id]=" + _db.CreateSqlParameterName("Virtual_switch_id");

			IDbCommand cmd = CreateDeleteCommand(whereSql);
			AddParameter(cmd, "Virtual_switch_id", virtual_switch_id);
			return cmd;
		}

		/// <summary>
		/// Deletes records from the <c>Service</c> table using the 
		/// <c>R_346</c> foreign key.
		/// </summary>
		/// <param name="default_routing_plan_id">The <c>default_routing_plan_id</c> column value.</param>
		/// <returns>The number of records deleted from the table.</returns>
		public int DeleteByDefault_routing_plan_id(int default_routing_plan_id)
		{
			return CreateDeleteByDefault_routing_plan_idCommand(default_routing_plan_id).ExecuteNonQuery();
		}

		/// <summary>
		/// Creates an <see cref="System.Data.IDbCommand"/> object that can be used to
		/// delete records using the <c>R_346</c> foreign key.
		/// </summary>
		/// <param name="default_routing_plan_id">The <c>default_routing_plan_id</c> column value.</param>
		/// <returns>A reference to the <see cref="System.Data.IDbCommand"/> object.</returns>
		protected virtual IDbCommand CreateDeleteByDefault_routing_plan_idCommand(int default_routing_plan_id)
		{
			string whereSql = "";
			whereSql += "[default_routing_plan_id]=" + _db.CreateSqlParameterName("Default_routing_plan_id");

			IDbCommand cmd = CreateDeleteCommand(whereSql);
			AddParameter(cmd, "Default_routing_plan_id", default_routing_plan_id);
			return cmd;
		}

		/// <summary>
		/// Deletes <c>Service</c> records that match the specified criteria.
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
		/// to delete <c>Service</c> records that match the specified criteria.
		/// </summary>
		/// <param name="whereSql">The SQL search condition. 
		/// For example: <c>"FirstName='Smith' AND Zip=75038"</c>.</param>
		/// <returns>A reference to the <see cref="System.Data.IDbCommand"/> object.</returns>
		protected virtual IDbCommand CreateDeleteCommand(string whereSql)
		{
			string sql = "DELETE FROM [dbo].[Service]";
			if(null != whereSql && 0 < whereSql.Length)
				sql += " WHERE " + whereSql;
			return _db.CreateCommand(sql);
		}

		/// <summary>
		/// Deletes all records from the <c>Service</c> table.
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
		/// <returns>An array of <see cref="ServiceRow"/> objects.</returns>
		protected ServiceRow[] MapRecords(IDbCommand command)
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
		/// <returns>An array of <see cref="ServiceRow"/> objects.</returns>
		protected ServiceRow[] MapRecords(IDataReader reader)
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
		/// <returns>An array of <see cref="ServiceRow"/> objects.</returns>
		protected virtual ServiceRow[] MapRecords(IDataReader reader, 
										int startIndex, int length, ref int totalRecordCount)
		{
			if(0 > startIndex)
				throw new ArgumentOutOfRangeException("startIndex", startIndex, "StartIndex cannot be less than zero.");
			if(0 > length)
				throw new ArgumentOutOfRangeException("length", length, "Length cannot be less than zero.");

			int service_idColumnIndex = reader.GetOrdinal("service_id");
			int nameColumnIndex = reader.GetOrdinal("name");
			int virtual_switch_idColumnIndex = reader.GetOrdinal("virtual_switch_id");
			int calling_plan_idColumnIndex = reader.GetOrdinal("calling_plan_id");
			int default_routing_plan_idColumnIndex = reader.GetOrdinal("default_routing_plan_id");
			int statusColumnIndex = reader.GetOrdinal("status");
			int typeColumnIndex = reader.GetOrdinal("type");
			int retail_typeColumnIndex = reader.GetOrdinal("retail_type");
			int is_sharedColumnIndex = reader.GetOrdinal("is_shared");
			int rating_typeColumnIndex = reader.GetOrdinal("rating_type");
			int pin_lengthColumnIndex = reader.GetOrdinal("pin_length");
			int payphone_surcharge_idColumnIndex = reader.GetOrdinal("payphone_surcharge_id");
			int sweep_schedule_idColumnIndex = reader.GetOrdinal("sweep_schedule_id");
			int sweep_feeColumnIndex = reader.GetOrdinal("sweep_fee");
			int sweep_ruleColumnIndex = reader.GetOrdinal("sweep_rule");
			int balance_prompt_typeColumnIndex = reader.GetOrdinal("balance_prompt_type");
			int balance_prompt_per_unitColumnIndex = reader.GetOrdinal("balance_prompt_per_unit");

			System.Collections.ArrayList recordList = new System.Collections.ArrayList();
			int ri = -startIndex;
			while(reader.Read())
			{
				ri++;
				if(ri > 0 && ri <= length)
				{
					ServiceRow record = new ServiceRow();
					recordList.Add(record);

					record.Service_id = Convert.ToInt16(reader.GetValue(service_idColumnIndex));
					record.Name = Convert.ToString(reader.GetValue(nameColumnIndex));
					record.Virtual_switch_id = Convert.ToInt32(reader.GetValue(virtual_switch_idColumnIndex));
					record.Calling_plan_id = Convert.ToInt32(reader.GetValue(calling_plan_idColumnIndex));
					record.Default_routing_plan_id = Convert.ToInt32(reader.GetValue(default_routing_plan_idColumnIndex));
					record.Status = Convert.ToByte(reader.GetValue(statusColumnIndex));
					record.Type = Convert.ToByte(reader.GetValue(typeColumnIndex));
					record.Retail_type = Convert.ToInt32(reader.GetValue(retail_typeColumnIndex));
					record.Is_shared = Convert.ToByte(reader.GetValue(is_sharedColumnIndex));
					record.Rating_type = Convert.ToByte(reader.GetValue(rating_typeColumnIndex));
					record.Pin_length = Convert.ToInt32(reader.GetValue(pin_lengthColumnIndex));
					if(!reader.IsDBNull(payphone_surcharge_idColumnIndex))
						record.Payphone_surcharge_id = Convert.ToInt32(reader.GetValue(payphone_surcharge_idColumnIndex));
					if(!reader.IsDBNull(sweep_schedule_idColumnIndex))
						record.Sweep_schedule_id = Convert.ToInt32(reader.GetValue(sweep_schedule_idColumnIndex));
					record.Sweep_fee = Convert.ToDecimal(reader.GetValue(sweep_feeColumnIndex));
					record.Sweep_rule = Convert.ToInt32(reader.GetValue(sweep_ruleColumnIndex));
					record.Balance_prompt_type = Convert.ToByte(reader.GetValue(balance_prompt_typeColumnIndex));
					record.Balance_prompt_per_unit = Convert.ToDecimal(reader.GetValue(balance_prompt_per_unitColumnIndex));

					if(ri == length && 0 != totalRecordCount)
						break;
				}
			}

			totalRecordCount = 0 == totalRecordCount ? ri + startIndex : -1;
			return (ServiceRow[])(recordList.ToArray(typeof(ServiceRow)));
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
		/// Converts <see cref="System.Data.DataRow"/> to <see cref="ServiceRow"/>.
		/// </summary>
		/// <param name="row">The <see cref="System.Data.DataRow"/> object to be mapped.</param>
		/// <returns>A reference to the <see cref="ServiceRow"/> object.</returns>
		protected virtual ServiceRow MapRow(DataRow row)
		{
			ServiceRow mappedObject = new ServiceRow();
			DataTable dataTable = row.Table;
			DataColumn dataColumn;
			// Column "Service_id"
			dataColumn = dataTable.Columns["Service_id"];
			if(!row.IsNull(dataColumn))
				mappedObject.Service_id = (short)row[dataColumn];
			// Column "Name"
			dataColumn = dataTable.Columns["Name"];
			if(!row.IsNull(dataColumn))
				mappedObject.Name = (string)row[dataColumn];
			// Column "Virtual_switch_id"
			dataColumn = dataTable.Columns["Virtual_switch_id"];
			if(!row.IsNull(dataColumn))
				mappedObject.Virtual_switch_id = (int)row[dataColumn];
			// Column "Calling_plan_id"
			dataColumn = dataTable.Columns["Calling_plan_id"];
			if(!row.IsNull(dataColumn))
				mappedObject.Calling_plan_id = (int)row[dataColumn];
			// Column "Default_routing_plan_id"
			dataColumn = dataTable.Columns["Default_routing_plan_id"];
			if(!row.IsNull(dataColumn))
				mappedObject.Default_routing_plan_id = (int)row[dataColumn];
			// Column "Status"
			dataColumn = dataTable.Columns["Status"];
			if(!row.IsNull(dataColumn))
				mappedObject.Status = (byte)row[dataColumn];
			// Column "Type"
			dataColumn = dataTable.Columns["Type"];
			if(!row.IsNull(dataColumn))
				mappedObject.Type = (byte)row[dataColumn];
			// Column "Retail_type"
			dataColumn = dataTable.Columns["Retail_type"];
			if(!row.IsNull(dataColumn))
				mappedObject.Retail_type = (int)row[dataColumn];
			// Column "Is_shared"
			dataColumn = dataTable.Columns["Is_shared"];
			if(!row.IsNull(dataColumn))
				mappedObject.Is_shared = (byte)row[dataColumn];
			// Column "Rating_type"
			dataColumn = dataTable.Columns["Rating_type"];
			if(!row.IsNull(dataColumn))
				mappedObject.Rating_type = (byte)row[dataColumn];
			// Column "Pin_length"
			dataColumn = dataTable.Columns["Pin_length"];
			if(!row.IsNull(dataColumn))
				mappedObject.Pin_length = (int)row[dataColumn];
			// Column "Payphone_surcharge_id"
			dataColumn = dataTable.Columns["Payphone_surcharge_id"];
			if(!row.IsNull(dataColumn))
				mappedObject.Payphone_surcharge_id = (int)row[dataColumn];
			// Column "Sweep_schedule_id"
			dataColumn = dataTable.Columns["Sweep_schedule_id"];
			if(!row.IsNull(dataColumn))
				mappedObject.Sweep_schedule_id = (int)row[dataColumn];
			// Column "Sweep_fee"
			dataColumn = dataTable.Columns["Sweep_fee"];
			if(!row.IsNull(dataColumn))
				mappedObject.Sweep_fee = (decimal)row[dataColumn];
			// Column "Sweep_rule"
			dataColumn = dataTable.Columns["Sweep_rule"];
			if(!row.IsNull(dataColumn))
				mappedObject.Sweep_rule = (int)row[dataColumn];
			// Column "Balance_prompt_type"
			dataColumn = dataTable.Columns["Balance_prompt_type"];
			if(!row.IsNull(dataColumn))
				mappedObject.Balance_prompt_type = (byte)row[dataColumn];
			// Column "Balance_prompt_per_unit"
			dataColumn = dataTable.Columns["Balance_prompt_per_unit"];
			if(!row.IsNull(dataColumn))
				mappedObject.Balance_prompt_per_unit = (decimal)row[dataColumn];
			return mappedObject;
		}

		/// <summary>
		/// Creates a <see cref="System.Data.DataTable"/> object for 
		/// the <c>Service</c> table.
		/// </summary>
		/// <returns>A reference to the <see cref="System.Data.DataTable"/> object.</returns>
		protected virtual DataTable CreateDataTable()
		{
			DataTable dataTable = new DataTable();
			dataTable.TableName = "Service";
			DataColumn dataColumn;
			dataColumn = dataTable.Columns.Add("Service_id", typeof(short));
			dataColumn.Caption = "service_id";
			dataColumn.AllowDBNull = false;
			dataColumn = dataTable.Columns.Add("Name", typeof(string));
			dataColumn.Caption = "name";
			dataColumn.MaxLength = 64;
			dataColumn.AllowDBNull = false;
			dataColumn = dataTable.Columns.Add("Virtual_switch_id", typeof(int));
			dataColumn.Caption = "virtual_switch_id";
			dataColumn.AllowDBNull = false;
			dataColumn = dataTable.Columns.Add("Calling_plan_id", typeof(int));
			dataColumn.Caption = "calling_plan_id";
			dataColumn.AllowDBNull = false;
			dataColumn = dataTable.Columns.Add("Default_routing_plan_id", typeof(int));
			dataColumn.Caption = "default_routing_plan_id";
			dataColumn.AllowDBNull = false;
			dataColumn = dataTable.Columns.Add("Status", typeof(byte));
			dataColumn.Caption = "status";
			dataColumn.AllowDBNull = false;
			dataColumn = dataTable.Columns.Add("Type", typeof(byte));
			dataColumn.Caption = "type";
			dataColumn.AllowDBNull = false;
			dataColumn = dataTable.Columns.Add("Retail_type", typeof(int));
			dataColumn.Caption = "retail_type";
			dataColumn.AllowDBNull = false;
			dataColumn = dataTable.Columns.Add("Is_shared", typeof(byte));
			dataColumn.Caption = "is_shared";
			dataColumn.AllowDBNull = false;
			dataColumn = dataTable.Columns.Add("Rating_type", typeof(byte));
			dataColumn.Caption = "rating_type";
			dataColumn.AllowDBNull = false;
			dataColumn = dataTable.Columns.Add("Pin_length", typeof(int));
			dataColumn.Caption = "pin_length";
			dataColumn.AllowDBNull = false;
			dataColumn = dataTable.Columns.Add("Payphone_surcharge_id", typeof(int));
			dataColumn.Caption = "payphone_surcharge_id";
			dataColumn = dataTable.Columns.Add("Sweep_schedule_id", typeof(int));
			dataColumn.Caption = "sweep_schedule_id";
			dataColumn = dataTable.Columns.Add("Sweep_fee", typeof(decimal));
			dataColumn.Caption = "sweep_fee";
			dataColumn.AllowDBNull = false;
			dataColumn = dataTable.Columns.Add("Sweep_rule", typeof(int));
			dataColumn.Caption = "sweep_rule";
			dataColumn.AllowDBNull = false;
			dataColumn = dataTable.Columns.Add("Balance_prompt_type", typeof(byte));
			dataColumn.Caption = "balance_prompt_type";
			dataColumn.AllowDBNull = false;
			dataColumn = dataTable.Columns.Add("Balance_prompt_per_unit", typeof(decimal));
			dataColumn.Caption = "balance_prompt_per_unit";
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
				case "Service_id":
					parameter = _db.AddParameter(cmd, paramName, DbType.Int16, value);
					break;

				case "Name":
					parameter = _db.AddParameter(cmd, paramName, DbType.AnsiString, value);
					break;

				case "Virtual_switch_id":
					parameter = _db.AddParameter(cmd, paramName, DbType.Int32, value);
					break;

				case "Calling_plan_id":
					parameter = _db.AddParameter(cmd, paramName, DbType.Int32, value);
					break;

				case "Default_routing_plan_id":
					parameter = _db.AddParameter(cmd, paramName, DbType.Int32, value);
					break;

				case "Status":
					parameter = _db.AddParameter(cmd, paramName, DbType.Byte, value);
					break;

				case "Type":
					parameter = _db.AddParameter(cmd, paramName, DbType.Byte, value);
					break;

				case "Retail_type":
					parameter = _db.AddParameter(cmd, paramName, DbType.Int32, value);
					break;

				case "Is_shared":
					parameter = _db.AddParameter(cmd, paramName, DbType.Byte, value);
					break;

				case "Rating_type":
					parameter = _db.AddParameter(cmd, paramName, DbType.Byte, value);
					break;

				case "Pin_length":
					parameter = _db.AddParameter(cmd, paramName, DbType.Int32, value);
					break;

				case "Payphone_surcharge_id":
					parameter = _db.AddParameter(cmd, paramName, DbType.Int32, value);
					break;

				case "Sweep_schedule_id":
					parameter = _db.AddParameter(cmd, paramName, DbType.Int32, value);
					break;

				case "Sweep_fee":
					parameter = _db.AddParameter(cmd, paramName, DbType.Decimal, value);
					break;

				case "Sweep_rule":
					parameter = _db.AddParameter(cmd, paramName, DbType.Int32, value);
					break;

				case "Balance_prompt_type":
					parameter = _db.AddParameter(cmd, paramName, DbType.Byte, value);
					break;

				case "Balance_prompt_per_unit":
					parameter = _db.AddParameter(cmd, paramName, DbType.Decimal, value);
					break;

				default:
					throw new ArgumentException("Unknown parameter name (" + paramName + ").");
			}
			return parameter;
		}

	} // End of ServiceCollection_Base class
}  // End of namespace

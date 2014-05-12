// <fileinfo name="Base\AccessNumberListCollection_Base.cs">
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
	/// The base class for <see cref="AccessNumberListCollection"/>. Provides methods 
	/// for common database table operations. 
	/// </summary>
	/// <remarks>
	/// Do not change this source code. Update the <see cref="AccessNumberListCollection"/>
	/// class if you need to add or change some functionality.
	/// </remarks>
	public abstract class AccessNumberListCollection_Base
	{
		// Constants
		public const string Access_numberColumnName = "access_number";
		public const string Service_idColumnName = "service_id";
		public const string LanguageColumnName = "language";
		public const string SurchargeColumnName = "surcharge";
		public const string Surcharge_typeColumnName = "surcharge_type";
		public const string Customer_acct_idColumnName = "customer_acct_id";
		public const string Script_typeColumnName = "script_type";

		// Instance fields
		private Rbr_Db _db;

		/// <summary>
		/// Initializes a new instance of the <see cref="AccessNumberListCollection_Base"/> 
		/// class with the specified <see cref="Rbr_Db"/>.
		/// </summary>
		/// <param name="db">The <see cref="Rbr_Db"/> object.</param>
		public AccessNumberListCollection_Base(Rbr_Db db)
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
		/// Gets an array of all records from the <c>AccessNumberList</c> table.
		/// </summary>
		/// <returns>An array of <see cref="AccessNumberListRow"/> objects.</returns>
		public virtual AccessNumberListRow[] GetAll()
		{
			return MapRecords(CreateGetAllCommand());
		}

		/// <summary>
		/// Gets a <see cref="System.Data.DataTable"/> object that 
		/// includes all records from the <c>AccessNumberList</c> table.
		/// </summary>
		/// <returns>A reference to the <see cref="System.Data.DataTable"/> object.</returns>
		public virtual DataTable GetAllAsDataTable()
		{
			return MapRecordsToDataTable(CreateGetAllCommand());
		}

		/// <summary>
		/// Creates and returns an <see cref="System.Data.IDbCommand"/> object that is used
		/// to retrieve all records from the <c>AccessNumberList</c> table.
		/// </summary>
		/// <returns>A reference to the <see cref="System.Data.IDbCommand"/> object.</returns>
		protected virtual IDbCommand CreateGetAllCommand()
		{
			return CreateGetCommand(null, null);
		}

		/// <summary>
		/// Gets the first <see cref="AccessNumberListRow"/> objects that 
		/// match the search condition.
		/// </summary>
		/// <param name="whereSql">The SQL search condition. For example: 
		/// <c>"FirstName='Smith' AND Zip=75038"</c>.</param>
		/// <returns>An instance of <see cref="AccessNumberListRow"/> or null reference 
		/// (Nothing in Visual Basic) if the object was not found.</returns>
		public AccessNumberListRow GetRow(string whereSql)
		{
			int totalRecordCount = -1;
			AccessNumberListRow[] rows = GetAsArray(whereSql, null, 0, 1, ref totalRecordCount);
			return 0 == rows.Length ? null : rows[0];
		}

		/// <summary>
		/// Gets an array of <see cref="AccessNumberListRow"/> objects that 
		/// match the search condition, in the the specified sort order.
		/// </summary>
		/// <param name="whereSql">The SQL search condition. For example: 
		/// <c>"FirstName='Smith' AND Zip=75038"</c>.</param>
		/// <param name="orderBySql">The column name(s) followed by "ASC" (ascending) or "DESC" (descending).
		/// Columns are sorted in ascending order by default. For example: <c>"LastName ASC, FirstName ASC"</c>.</param>
		/// <returns>An array of <see cref="AccessNumberListRow"/> objects.</returns>
		public AccessNumberListRow[] GetAsArray(string whereSql, string orderBySql)
		{
			int totalRecordCount = -1;
			return GetAsArray(whereSql, orderBySql, 0, int.MaxValue, ref totalRecordCount);
		}

		/// <summary>
		/// Gets an array of <see cref="AccessNumberListRow"/> objects that 
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
		/// <returns>An array of <see cref="AccessNumberListRow"/> objects.</returns>
		public virtual AccessNumberListRow[] GetAsArray(string whereSql, string orderBySql,
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
			string sql = "SELECT * FROM [dbo].[AccessNumberList]";
			if(null != whereSql && 0 < whereSql.Length)
				sql += " WHERE " + whereSql;
			if(null != orderBySql && 0 < orderBySql.Length)
				sql += " ORDER BY " + orderBySql;
			return _db.CreateCommand(sql);
		}

		/// <summary>
		/// Gets <see cref="AccessNumberListRow"/> by the primary key.
		/// </summary>
		/// <param name="access_number">The <c>access_number</c> column value.</param>
		/// <returns>An instance of <see cref="AccessNumberListRow"/> or null reference 
		/// (Nothing in Visual Basic) if the object was not found.</returns>
		public virtual AccessNumberListRow GetByPrimaryKey(long access_number)
		{
			string whereSql = "[access_number]=" + _db.CreateSqlParameterName("Access_number");
			IDbCommand cmd = CreateGetCommand(whereSql, null);
			AddParameter(cmd, "Access_number", access_number);
			AccessNumberListRow[] tempArray = MapRecords(cmd);
			return 0 == tempArray.Length ? null : tempArray[0];
		}

		/// <summary>
		/// Gets an array of <see cref="AccessNumberListRow"/> objects 
		/// by the <c>FK_AccessNumberList_Service</c> foreign key.
		/// </summary>
		/// <param name="service_id">The <c>service_id</c> column value.</param>
		/// <returns>An array of <see cref="AccessNumberListRow"/> objects.</returns>
		public AccessNumberListRow[] GetByService_id(short service_id)
		{
			return GetByService_id(service_id, false);
		}

		/// <summary>
		/// Gets an array of <see cref="AccessNumberListRow"/> objects 
		/// by the <c>FK_AccessNumberList_Service</c> foreign key.
		/// </summary>
		/// <param name="service_id">The <c>service_id</c> column value.</param>
		/// <param name="service_idNull">true if the method ignores the service_id
		/// parameter value and uses DbNull instead of it; otherwise, false.</param>
		/// <returns>An array of <see cref="AccessNumberListRow"/> objects.</returns>
		public virtual AccessNumberListRow[] GetByService_id(short service_id, bool service_idNull)
		{
			return MapRecords(CreateGetByService_idCommand(service_id, service_idNull));
		}

		/// <summary>
		/// Gets a <see cref="System.Data.DataTable"/> object 
		/// by the <c>FK_AccessNumberList_Service</c> foreign key.
		/// </summary>
		/// <param name="service_id">The <c>service_id</c> column value.</param>
		/// <returns>A reference to the <see cref="System.Data.DataTable"/> object.</returns>
		public DataTable GetByService_idAsDataTable(short service_id)
		{
			return GetByService_idAsDataTable(service_id, false);
		}

		/// <summary>
		/// Gets a <see cref="System.Data.DataTable"/> object 
		/// by the <c>FK_AccessNumberList_Service</c> foreign key.
		/// </summary>
		/// <param name="service_id">The <c>service_id</c> column value.</param>
		/// <param name="service_idNull">true if the method ignores the service_id
		/// parameter value and uses DbNull instead of it; otherwise, false.</param>
		/// <returns>A reference to the <see cref="System.Data.DataTable"/> object.</returns>
		public virtual DataTable GetByService_idAsDataTable(short service_id, bool service_idNull)
		{
			return MapRecordsToDataTable(CreateGetByService_idCommand(service_id, service_idNull));
		}

		/// <summary>
		/// Creates an <see cref="System.Data.IDbCommand"/> object that can be used to 
		/// return records by the <c>FK_AccessNumberList_Service</c> foreign key.
		/// </summary>
		/// <param name="service_id">The <c>service_id</c> column value.</param>
		/// <param name="service_idNull">true if the method ignores the service_id
		/// parameter value and uses DbNull instead of it; otherwise, false.</param>
		/// <returns>A reference to the <see cref="System.Data.IDbCommand"/> object.</returns>
		protected virtual IDbCommand CreateGetByService_idCommand(short service_id, bool service_idNull)
		{
			string whereSql = "";
			if(service_idNull)
				whereSql += "[service_id] IS NULL";
			else
				whereSql += "[service_id]=" + _db.CreateSqlParameterName("Service_id");

			IDbCommand cmd = CreateGetCommand(whereSql, null);
			if(!service_idNull)
				AddParameter(cmd, "Service_id", service_id);
			return cmd;
		}

		/// <summary>
		/// Gets an array of <see cref="AccessNumberListRow"/> objects 
		/// by the <c>R_254</c> foreign key.
		/// </summary>
		/// <param name="customer_acct_id">The <c>customer_acct_id</c> column value.</param>
		/// <returns>An array of <see cref="AccessNumberListRow"/> objects.</returns>
		public AccessNumberListRow[] GetByCustomer_acct_id(short customer_acct_id)
		{
			return GetByCustomer_acct_id(customer_acct_id, false);
		}

		/// <summary>
		/// Gets an array of <see cref="AccessNumberListRow"/> objects 
		/// by the <c>R_254</c> foreign key.
		/// </summary>
		/// <param name="customer_acct_id">The <c>customer_acct_id</c> column value.</param>
		/// <param name="customer_acct_idNull">true if the method ignores the customer_acct_id
		/// parameter value and uses DbNull instead of it; otherwise, false.</param>
		/// <returns>An array of <see cref="AccessNumberListRow"/> objects.</returns>
		public virtual AccessNumberListRow[] GetByCustomer_acct_id(short customer_acct_id, bool customer_acct_idNull)
		{
			return MapRecords(CreateGetByCustomer_acct_idCommand(customer_acct_id, customer_acct_idNull));
		}

		/// <summary>
		/// Gets a <see cref="System.Data.DataTable"/> object 
		/// by the <c>R_254</c> foreign key.
		/// </summary>
		/// <param name="customer_acct_id">The <c>customer_acct_id</c> column value.</param>
		/// <returns>A reference to the <see cref="System.Data.DataTable"/> object.</returns>
		public DataTable GetByCustomer_acct_idAsDataTable(short customer_acct_id)
		{
			return GetByCustomer_acct_idAsDataTable(customer_acct_id, false);
		}

		/// <summary>
		/// Gets a <see cref="System.Data.DataTable"/> object 
		/// by the <c>R_254</c> foreign key.
		/// </summary>
		/// <param name="customer_acct_id">The <c>customer_acct_id</c> column value.</param>
		/// <param name="customer_acct_idNull">true if the method ignores the customer_acct_id
		/// parameter value and uses DbNull instead of it; otherwise, false.</param>
		/// <returns>A reference to the <see cref="System.Data.DataTable"/> object.</returns>
		public virtual DataTable GetByCustomer_acct_idAsDataTable(short customer_acct_id, bool customer_acct_idNull)
		{
			return MapRecordsToDataTable(CreateGetByCustomer_acct_idCommand(customer_acct_id, customer_acct_idNull));
		}

		/// <summary>
		/// Creates an <see cref="System.Data.IDbCommand"/> object that can be used to 
		/// return records by the <c>R_254</c> foreign key.
		/// </summary>
		/// <param name="customer_acct_id">The <c>customer_acct_id</c> column value.</param>
		/// <param name="customer_acct_idNull">true if the method ignores the customer_acct_id
		/// parameter value and uses DbNull instead of it; otherwise, false.</param>
		/// <returns>A reference to the <see cref="System.Data.IDbCommand"/> object.</returns>
		protected virtual IDbCommand CreateGetByCustomer_acct_idCommand(short customer_acct_id, bool customer_acct_idNull)
		{
			string whereSql = "";
			if(customer_acct_idNull)
				whereSql += "[customer_acct_id] IS NULL";
			else
				whereSql += "[customer_acct_id]=" + _db.CreateSqlParameterName("Customer_acct_id");

			IDbCommand cmd = CreateGetCommand(whereSql, null);
			if(!customer_acct_idNull)
				AddParameter(cmd, "Customer_acct_id", customer_acct_id);
			return cmd;
		}

		/// <summary>
		/// Adds a new record into the <c>AccessNumberList</c> table.
		/// </summary>
		/// <param name="value">The <see cref="AccessNumberListRow"/> object to be inserted.</param>
		public virtual void Insert(AccessNumberListRow value)
		{
			string sqlStr = "INSERT INTO [dbo].[AccessNumberList] (" +
				"[access_number], " +
				"[service_id], " +
				"[language], " +
				"[surcharge], " +
				"[surcharge_type], " +
				"[customer_acct_id], " +
				"[script_type]" +
				") VALUES (" +
				_db.CreateSqlParameterName("Access_number") + ", " +
				_db.CreateSqlParameterName("Service_id") + ", " +
				_db.CreateSqlParameterName("Language") + ", " +
				_db.CreateSqlParameterName("Surcharge") + ", " +
				_db.CreateSqlParameterName("Surcharge_type") + ", " +
				_db.CreateSqlParameterName("Customer_acct_id") + ", " +
				_db.CreateSqlParameterName("Script_type") + ")";
			IDbCommand cmd = _db.CreateCommand(sqlStr);
			AddParameter(cmd, "Access_number", value.Access_number);
			AddParameter(cmd, "Service_id",
				value.IsService_idNull ? DBNull.Value : (object)value.Service_id);
			AddParameter(cmd, "Language", value.Language);
			AddParameter(cmd, "Surcharge", value.Surcharge);
			AddParameter(cmd, "Surcharge_type", value.Surcharge_type);
			AddParameter(cmd, "Customer_acct_id",
				value.IsCustomer_acct_idNull ? DBNull.Value : (object)value.Customer_acct_id);
			AddParameter(cmd, "Script_type", value.Script_type);
			cmd.ExecuteNonQuery();
		}

		/// <summary>
		/// Updates a record in the <c>AccessNumberList</c> table.
		/// </summary>
		/// <param name="value">The <see cref="AccessNumberListRow"/>
		/// object used to update the table record.</param>
		/// <returns>true if the record was updated; otherwise, false.</returns>
		public virtual bool Update(AccessNumberListRow value)
		{
			string sqlStr = "UPDATE [dbo].[AccessNumberList] SET " +
				"[service_id]=" + _db.CreateSqlParameterName("Service_id") + ", " +
				"[language]=" + _db.CreateSqlParameterName("Language") + ", " +
				"[surcharge]=" + _db.CreateSqlParameterName("Surcharge") + ", " +
				"[surcharge_type]=" + _db.CreateSqlParameterName("Surcharge_type") + ", " +
				"[customer_acct_id]=" + _db.CreateSqlParameterName("Customer_acct_id") + ", " +
				"[script_type]=" + _db.CreateSqlParameterName("Script_type") +
				" WHERE " +
				"[access_number]=" + _db.CreateSqlParameterName("Access_number");
			IDbCommand cmd = _db.CreateCommand(sqlStr);
			AddParameter(cmd, "Service_id",
				value.IsService_idNull ? DBNull.Value : (object)value.Service_id);
			AddParameter(cmd, "Language", value.Language);
			AddParameter(cmd, "Surcharge", value.Surcharge);
			AddParameter(cmd, "Surcharge_type", value.Surcharge_type);
			AddParameter(cmd, "Customer_acct_id",
				value.IsCustomer_acct_idNull ? DBNull.Value : (object)value.Customer_acct_id);
			AddParameter(cmd, "Script_type", value.Script_type);
			AddParameter(cmd, "Access_number", value.Access_number);
			return 0 != cmd.ExecuteNonQuery();
		}

		/// <summary>
		/// Updates the <c>AccessNumberList</c> table and calls the <c>AcceptChanges</c> method
		/// on the changed DataRow objects.
		/// </summary>
		/// <param name="table">The <see cref="System.Data.DataTable"/> used to update the data source.</param>
		public void Update(DataTable table)
		{
			Update(table, true);
		}

		/// <summary>
		/// Updates the <c>AccessNumberList</c> table. Pass <c>false</c> as the <c>acceptChanges</c> 
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
							DeleteByPrimaryKey((long)row["Access_number"]);
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
		/// Deletes the specified object from the <c>AccessNumberList</c> table.
		/// </summary>
		/// <param name="value">The <see cref="AccessNumberListRow"/> object to delete.</param>
		/// <returns>true if the record was deleted; otherwise, false.</returns>
		public bool Delete(AccessNumberListRow value)
		{
			return DeleteByPrimaryKey(value.Access_number);
		}

		/// <summary>
		/// Deletes a record from the <c>AccessNumberList</c> table using
		/// the specified primary key.
		/// </summary>
		/// <param name="access_number">The <c>access_number</c> column value.</param>
		/// <returns>true if the record was deleted; otherwise, false.</returns>
		public virtual bool DeleteByPrimaryKey(long access_number)
		{
			string whereSql = "[access_number]=" + _db.CreateSqlParameterName("Access_number");
			IDbCommand cmd = CreateDeleteCommand(whereSql);
			AddParameter(cmd, "Access_number", access_number);
			return 0 < cmd.ExecuteNonQuery();
		}

		/// <summary>
		/// Deletes records from the <c>AccessNumberList</c> table using the 
		/// <c>FK_AccessNumberList_Service</c> foreign key.
		/// </summary>
		/// <param name="service_id">The <c>service_id</c> column value.</param>
		/// <returns>The number of records deleted from the table.</returns>
		public int DeleteByService_id(short service_id)
		{
			return DeleteByService_id(service_id, false);
		}

		/// <summary>
		/// Deletes records from the <c>AccessNumberList</c> table using the 
		/// <c>FK_AccessNumberList_Service</c> foreign key.
		/// </summary>
		/// <param name="service_id">The <c>service_id</c> column value.</param>
		/// <param name="service_idNull">true if the method ignores the service_id
		/// parameter value and uses DbNull instead of it; otherwise, false.</param>
		/// <returns>The number of records deleted from the table.</returns>
		public int DeleteByService_id(short service_id, bool service_idNull)
		{
			return CreateDeleteByService_idCommand(service_id, service_idNull).ExecuteNonQuery();
		}

		/// <summary>
		/// Creates an <see cref="System.Data.IDbCommand"/> object that can be used to
		/// delete records using the <c>FK_AccessNumberList_Service</c> foreign key.
		/// </summary>
		/// <param name="service_id">The <c>service_id</c> column value.</param>
		/// <param name="service_idNull">true if the method ignores the service_id
		/// parameter value and uses DbNull instead of it; otherwise, false.</param>
		/// <returns>A reference to the <see cref="System.Data.IDbCommand"/> object.</returns>
		protected virtual IDbCommand CreateDeleteByService_idCommand(short service_id, bool service_idNull)
		{
			string whereSql = "";
			if(service_idNull)
				whereSql += "[service_id] IS NULL";
			else
				whereSql += "[service_id]=" + _db.CreateSqlParameterName("Service_id");

			IDbCommand cmd = CreateDeleteCommand(whereSql);
			if(!service_idNull)
				AddParameter(cmd, "Service_id", service_id);
			return cmd;
		}

		/// <summary>
		/// Deletes records from the <c>AccessNumberList</c> table using the 
		/// <c>R_254</c> foreign key.
		/// </summary>
		/// <param name="customer_acct_id">The <c>customer_acct_id</c> column value.</param>
		/// <returns>The number of records deleted from the table.</returns>
		public int DeleteByCustomer_acct_id(short customer_acct_id)
		{
			return DeleteByCustomer_acct_id(customer_acct_id, false);
		}

		/// <summary>
		/// Deletes records from the <c>AccessNumberList</c> table using the 
		/// <c>R_254</c> foreign key.
		/// </summary>
		/// <param name="customer_acct_id">The <c>customer_acct_id</c> column value.</param>
		/// <param name="customer_acct_idNull">true if the method ignores the customer_acct_id
		/// parameter value and uses DbNull instead of it; otherwise, false.</param>
		/// <returns>The number of records deleted from the table.</returns>
		public int DeleteByCustomer_acct_id(short customer_acct_id, bool customer_acct_idNull)
		{
			return CreateDeleteByCustomer_acct_idCommand(customer_acct_id, customer_acct_idNull).ExecuteNonQuery();
		}

		/// <summary>
		/// Creates an <see cref="System.Data.IDbCommand"/> object that can be used to
		/// delete records using the <c>R_254</c> foreign key.
		/// </summary>
		/// <param name="customer_acct_id">The <c>customer_acct_id</c> column value.</param>
		/// <param name="customer_acct_idNull">true if the method ignores the customer_acct_id
		/// parameter value and uses DbNull instead of it; otherwise, false.</param>
		/// <returns>A reference to the <see cref="System.Data.IDbCommand"/> object.</returns>
		protected virtual IDbCommand CreateDeleteByCustomer_acct_idCommand(short customer_acct_id, bool customer_acct_idNull)
		{
			string whereSql = "";
			if(customer_acct_idNull)
				whereSql += "[customer_acct_id] IS NULL";
			else
				whereSql += "[customer_acct_id]=" + _db.CreateSqlParameterName("Customer_acct_id");

			IDbCommand cmd = CreateDeleteCommand(whereSql);
			if(!customer_acct_idNull)
				AddParameter(cmd, "Customer_acct_id", customer_acct_id);
			return cmd;
		}

		/// <summary>
		/// Deletes <c>AccessNumberList</c> records that match the specified criteria.
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
		/// to delete <c>AccessNumberList</c> records that match the specified criteria.
		/// </summary>
		/// <param name="whereSql">The SQL search condition. 
		/// For example: <c>"FirstName='Smith' AND Zip=75038"</c>.</param>
		/// <returns>A reference to the <see cref="System.Data.IDbCommand"/> object.</returns>
		protected virtual IDbCommand CreateDeleteCommand(string whereSql)
		{
			string sql = "DELETE FROM [dbo].[AccessNumberList]";
			if(null != whereSql && 0 < whereSql.Length)
				sql += " WHERE " + whereSql;
			return _db.CreateCommand(sql);
		}

		/// <summary>
		/// Deletes all records from the <c>AccessNumberList</c> table.
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
		/// <returns>An array of <see cref="AccessNumberListRow"/> objects.</returns>
		protected AccessNumberListRow[] MapRecords(IDbCommand command)
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
		/// <returns>An array of <see cref="AccessNumberListRow"/> objects.</returns>
		protected AccessNumberListRow[] MapRecords(IDataReader reader)
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
		/// <returns>An array of <see cref="AccessNumberListRow"/> objects.</returns>
		protected virtual AccessNumberListRow[] MapRecords(IDataReader reader, 
										int startIndex, int length, ref int totalRecordCount)
		{
			if(0 > startIndex)
				throw new ArgumentOutOfRangeException("startIndex", startIndex, "StartIndex cannot be less than zero.");
			if(0 > length)
				throw new ArgumentOutOfRangeException("length", length, "Length cannot be less than zero.");

			int access_numberColumnIndex = reader.GetOrdinal("access_number");
			int service_idColumnIndex = reader.GetOrdinal("service_id");
			int languageColumnIndex = reader.GetOrdinal("language");
			int surchargeColumnIndex = reader.GetOrdinal("surcharge");
			int surcharge_typeColumnIndex = reader.GetOrdinal("surcharge_type");
			int customer_acct_idColumnIndex = reader.GetOrdinal("customer_acct_id");
			int script_typeColumnIndex = reader.GetOrdinal("script_type");

			System.Collections.ArrayList recordList = new System.Collections.ArrayList();
			int ri = -startIndex;
			while(reader.Read())
			{
				ri++;
				if(ri > 0 && ri <= length)
				{
					AccessNumberListRow record = new AccessNumberListRow();
					recordList.Add(record);

					record.Access_number = Convert.ToInt64(reader.GetValue(access_numberColumnIndex));
					if(!reader.IsDBNull(service_idColumnIndex))
						record.Service_id = Convert.ToInt16(reader.GetValue(service_idColumnIndex));
					record.Language = Convert.ToByte(reader.GetValue(languageColumnIndex));
					record.Surcharge = Convert.ToDecimal(reader.GetValue(surchargeColumnIndex));
					record.Surcharge_type = Convert.ToByte(reader.GetValue(surcharge_typeColumnIndex));
					if(!reader.IsDBNull(customer_acct_idColumnIndex))
						record.Customer_acct_id = Convert.ToInt16(reader.GetValue(customer_acct_idColumnIndex));
					record.Script_type = Convert.ToInt32(reader.GetValue(script_typeColumnIndex));

					if(ri == length && 0 != totalRecordCount)
						break;
				}
			}

			totalRecordCount = 0 == totalRecordCount ? ri + startIndex : -1;
			return (AccessNumberListRow[])(recordList.ToArray(typeof(AccessNumberListRow)));
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
		/// Converts <see cref="System.Data.DataRow"/> to <see cref="AccessNumberListRow"/>.
		/// </summary>
		/// <param name="row">The <see cref="System.Data.DataRow"/> object to be mapped.</param>
		/// <returns>A reference to the <see cref="AccessNumberListRow"/> object.</returns>
		protected virtual AccessNumberListRow MapRow(DataRow row)
		{
			AccessNumberListRow mappedObject = new AccessNumberListRow();
			DataTable dataTable = row.Table;
			DataColumn dataColumn;
			// Column "Access_number"
			dataColumn = dataTable.Columns["Access_number"];
			if(!row.IsNull(dataColumn))
				mappedObject.Access_number = (long)row[dataColumn];
			// Column "Service_id"
			dataColumn = dataTable.Columns["Service_id"];
			if(!row.IsNull(dataColumn))
				mappedObject.Service_id = (short)row[dataColumn];
			// Column "Language"
			dataColumn = dataTable.Columns["Language"];
			if(!row.IsNull(dataColumn))
				mappedObject.Language = (byte)row[dataColumn];
			// Column "Surcharge"
			dataColumn = dataTable.Columns["Surcharge"];
			if(!row.IsNull(dataColumn))
				mappedObject.Surcharge = (decimal)row[dataColumn];
			// Column "Surcharge_type"
			dataColumn = dataTable.Columns["Surcharge_type"];
			if(!row.IsNull(dataColumn))
				mappedObject.Surcharge_type = (byte)row[dataColumn];
			// Column "Customer_acct_id"
			dataColumn = dataTable.Columns["Customer_acct_id"];
			if(!row.IsNull(dataColumn))
				mappedObject.Customer_acct_id = (short)row[dataColumn];
			// Column "Script_type"
			dataColumn = dataTable.Columns["Script_type"];
			if(!row.IsNull(dataColumn))
				mappedObject.Script_type = (int)row[dataColumn];
			return mappedObject;
		}

		/// <summary>
		/// Creates a <see cref="System.Data.DataTable"/> object for 
		/// the <c>AccessNumberList</c> table.
		/// </summary>
		/// <returns>A reference to the <see cref="System.Data.DataTable"/> object.</returns>
		protected virtual DataTable CreateDataTable()
		{
			DataTable dataTable = new DataTable();
			dataTable.TableName = "AccessNumberList";
			DataColumn dataColumn;
			dataColumn = dataTable.Columns.Add("Access_number", typeof(long));
			dataColumn.Caption = "access_number";
			dataColumn.AllowDBNull = false;
			dataColumn = dataTable.Columns.Add("Service_id", typeof(short));
			dataColumn.Caption = "service_id";
			dataColumn = dataTable.Columns.Add("Language", typeof(byte));
			dataColumn.Caption = "language";
			dataColumn.AllowDBNull = false;
			dataColumn = dataTable.Columns.Add("Surcharge", typeof(decimal));
			dataColumn.Caption = "surcharge";
			dataColumn.AllowDBNull = false;
			dataColumn = dataTable.Columns.Add("Surcharge_type", typeof(byte));
			dataColumn.Caption = "surcharge_type";
			dataColumn.AllowDBNull = false;
			dataColumn = dataTable.Columns.Add("Customer_acct_id", typeof(short));
			dataColumn.Caption = "customer_acct_id";
			dataColumn = dataTable.Columns.Add("Script_type", typeof(int));
			dataColumn.Caption = "script_type";
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
				case "Access_number":
					parameter = _db.AddParameter(cmd, paramName, DbType.Int64, value);
					break;

				case "Service_id":
					parameter = _db.AddParameter(cmd, paramName, DbType.Int16, value);
					break;

				case "Language":
					parameter = _db.AddParameter(cmd, paramName, DbType.Byte, value);
					break;

				case "Surcharge":
					parameter = _db.AddParameter(cmd, paramName, DbType.Decimal, value);
					break;

				case "Surcharge_type":
					parameter = _db.AddParameter(cmd, paramName, DbType.Byte, value);
					break;

				case "Customer_acct_id":
					parameter = _db.AddParameter(cmd, paramName, DbType.Int16, value);
					break;

				case "Script_type":
					parameter = _db.AddParameter(cmd, paramName, DbType.Int32, value);
					break;

				default:
					throw new ArgumentException("Unknown parameter name (" + paramName + ").");
			}
			return parameter;
		}

	} // End of AccessNumberListCollection_Base class
}  // End of namespace

// <fileinfo name="Base\RetailRouteCollection_Base.cs">
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
	/// The base class for <see cref="RetailRouteCollection"/>. Provides methods 
	/// for common database table operations. 
	/// </summary>
	/// <remarks>
	/// Do not change this source code. Update the <see cref="RetailRouteCollection"/>
	/// class if you need to add or change some functionality.
	/// </remarks>
	public abstract class RetailRouteCollection_Base
	{
		// Constants
		public const string Retail_route_idColumnName = "retail_route_id";
		public const string Customer_acct_idColumnName = "customer_acct_id";
		public const string Route_idColumnName = "route_id";
		public const string StatusColumnName = "status";
		public const string Start_bonus_minutesColumnName = "start_bonus_minutes";
		public const string Bonus_minutes_typeColumnName = "bonus_minutes_type";
		public const string MultiplierColumnName = "multiplier";

		// Instance fields
		private Rbr_Db _db;

		/// <summary>
		/// Initializes a new instance of the <see cref="RetailRouteCollection_Base"/> 
		/// class with the specified <see cref="Rbr_Db"/>.
		/// </summary>
		/// <param name="db">The <see cref="Rbr_Db"/> object.</param>
		public RetailRouteCollection_Base(Rbr_Db db)
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
		/// Gets an array of all records from the <c>RetailRoute</c> table.
		/// </summary>
		/// <returns>An array of <see cref="RetailRouteRow"/> objects.</returns>
		public virtual RetailRouteRow[] GetAll()
		{
			return MapRecords(CreateGetAllCommand());
		}

		/// <summary>
		/// Gets a <see cref="System.Data.DataTable"/> object that 
		/// includes all records from the <c>RetailRoute</c> table.
		/// </summary>
		/// <returns>A reference to the <see cref="System.Data.DataTable"/> object.</returns>
		public virtual DataTable GetAllAsDataTable()
		{
			return MapRecordsToDataTable(CreateGetAllCommand());
		}

		/// <summary>
		/// Creates and returns an <see cref="System.Data.IDbCommand"/> object that is used
		/// to retrieve all records from the <c>RetailRoute</c> table.
		/// </summary>
		/// <returns>A reference to the <see cref="System.Data.IDbCommand"/> object.</returns>
		protected virtual IDbCommand CreateGetAllCommand()
		{
			return CreateGetCommand(null, null);
		}

		/// <summary>
		/// Gets the first <see cref="RetailRouteRow"/> objects that 
		/// match the search condition.
		/// </summary>
		/// <param name="whereSql">The SQL search condition. For example: 
		/// <c>"FirstName='Smith' AND Zip=75038"</c>.</param>
		/// <returns>An instance of <see cref="RetailRouteRow"/> or null reference 
		/// (Nothing in Visual Basic) if the object was not found.</returns>
		public RetailRouteRow GetRow(string whereSql)
		{
			int totalRecordCount = -1;
			RetailRouteRow[] rows = GetAsArray(whereSql, null, 0, 1, ref totalRecordCount);
			return 0 == rows.Length ? null : rows[0];
		}

		/// <summary>
		/// Gets an array of <see cref="RetailRouteRow"/> objects that 
		/// match the search condition, in the the specified sort order.
		/// </summary>
		/// <param name="whereSql">The SQL search condition. For example: 
		/// <c>"FirstName='Smith' AND Zip=75038"</c>.</param>
		/// <param name="orderBySql">The column name(s) followed by "ASC" (ascending) or "DESC" (descending).
		/// Columns are sorted in ascending order by default. For example: <c>"LastName ASC, FirstName ASC"</c>.</param>
		/// <returns>An array of <see cref="RetailRouteRow"/> objects.</returns>
		public RetailRouteRow[] GetAsArray(string whereSql, string orderBySql)
		{
			int totalRecordCount = -1;
			return GetAsArray(whereSql, orderBySql, 0, int.MaxValue, ref totalRecordCount);
		}

		/// <summary>
		/// Gets an array of <see cref="RetailRouteRow"/> objects that 
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
		/// <returns>An array of <see cref="RetailRouteRow"/> objects.</returns>
		public virtual RetailRouteRow[] GetAsArray(string whereSql, string orderBySql,
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
			string sql = "SELECT * FROM [dbo].[RetailRoute]";
			if(null != whereSql && 0 < whereSql.Length)
				sql += " WHERE " + whereSql;
			if(null != orderBySql && 0 < orderBySql.Length)
				sql += " ORDER BY " + orderBySql;
			return _db.CreateCommand(sql);
		}

		/// <summary>
		/// Gets <see cref="RetailRouteRow"/> by the primary key.
		/// </summary>
		/// <param name="retail_route_id">The <c>retail_route_id</c> column value.</param>
		/// <returns>An instance of <see cref="RetailRouteRow"/> or null reference 
		/// (Nothing in Visual Basic) if the object was not found.</returns>
		public virtual RetailRouteRow GetByPrimaryKey(int retail_route_id)
		{
			string whereSql = "[retail_route_id]=" + _db.CreateSqlParameterName("Retail_route_id");
			IDbCommand cmd = CreateGetCommand(whereSql, null);
			AddParameter(cmd, "Retail_route_id", retail_route_id);
			RetailRouteRow[] tempArray = MapRecords(cmd);
			return 0 == tempArray.Length ? null : tempArray[0];
		}

		/// <summary>
		/// Gets an array of <see cref="RetailRouteRow"/> objects 
		/// by the <c>R_355</c> foreign key.
		/// </summary>
		/// <param name="route_id">The <c>route_id</c> column value.</param>
		/// <returns>An array of <see cref="RetailRouteRow"/> objects.</returns>
		public RetailRouteRow[] GetByRoute_id(int route_id)
		{
			return GetByRoute_id(route_id, false);
		}

		/// <summary>
		/// Gets an array of <see cref="RetailRouteRow"/> objects 
		/// by the <c>R_355</c> foreign key.
		/// </summary>
		/// <param name="route_id">The <c>route_id</c> column value.</param>
		/// <param name="route_idNull">true if the method ignores the route_id
		/// parameter value and uses DbNull instead of it; otherwise, false.</param>
		/// <returns>An array of <see cref="RetailRouteRow"/> objects.</returns>
		public virtual RetailRouteRow[] GetByRoute_id(int route_id, bool route_idNull)
		{
			return MapRecords(CreateGetByRoute_idCommand(route_id, route_idNull));
		}

		/// <summary>
		/// Gets a <see cref="System.Data.DataTable"/> object 
		/// by the <c>R_355</c> foreign key.
		/// </summary>
		/// <param name="route_id">The <c>route_id</c> column value.</param>
		/// <returns>A reference to the <see cref="System.Data.DataTable"/> object.</returns>
		public DataTable GetByRoute_idAsDataTable(int route_id)
		{
			return GetByRoute_idAsDataTable(route_id, false);
		}

		/// <summary>
		/// Gets a <see cref="System.Data.DataTable"/> object 
		/// by the <c>R_355</c> foreign key.
		/// </summary>
		/// <param name="route_id">The <c>route_id</c> column value.</param>
		/// <param name="route_idNull">true if the method ignores the route_id
		/// parameter value and uses DbNull instead of it; otherwise, false.</param>
		/// <returns>A reference to the <see cref="System.Data.DataTable"/> object.</returns>
		public virtual DataTable GetByRoute_idAsDataTable(int route_id, bool route_idNull)
		{
			return MapRecordsToDataTable(CreateGetByRoute_idCommand(route_id, route_idNull));
		}

		/// <summary>
		/// Creates an <see cref="System.Data.IDbCommand"/> object that can be used to 
		/// return records by the <c>R_355</c> foreign key.
		/// </summary>
		/// <param name="route_id">The <c>route_id</c> column value.</param>
		/// <param name="route_idNull">true if the method ignores the route_id
		/// parameter value and uses DbNull instead of it; otherwise, false.</param>
		/// <returns>A reference to the <see cref="System.Data.IDbCommand"/> object.</returns>
		protected virtual IDbCommand CreateGetByRoute_idCommand(int route_id, bool route_idNull)
		{
			string whereSql = "";
			if(route_idNull)
				whereSql += "[route_id] IS NULL";
			else
				whereSql += "[route_id]=" + _db.CreateSqlParameterName("Route_id");

			IDbCommand cmd = CreateGetCommand(whereSql, null);
			if(!route_idNull)
				AddParameter(cmd, "Route_id", route_id);
			return cmd;
		}

		/// <summary>
		/// Gets an array of <see cref="RetailRouteRow"/> objects 
		/// by the <c>R_357</c> foreign key.
		/// </summary>
		/// <param name="customer_acct_id">The <c>customer_acct_id</c> column value.</param>
		/// <returns>An array of <see cref="RetailRouteRow"/> objects.</returns>
		public virtual RetailRouteRow[] GetByCustomer_acct_id(short customer_acct_id)
		{
			return MapRecords(CreateGetByCustomer_acct_idCommand(customer_acct_id));
		}

		/// <summary>
		/// Gets a <see cref="System.Data.DataTable"/> object 
		/// by the <c>R_357</c> foreign key.
		/// </summary>
		/// <param name="customer_acct_id">The <c>customer_acct_id</c> column value.</param>
		/// <returns>A reference to the <see cref="System.Data.DataTable"/> object.</returns>
		public virtual DataTable GetByCustomer_acct_idAsDataTable(short customer_acct_id)
		{
			return MapRecordsToDataTable(CreateGetByCustomer_acct_idCommand(customer_acct_id));
		}

		/// <summary>
		/// Creates an <see cref="System.Data.IDbCommand"/> object that can be used to 
		/// return records by the <c>R_357</c> foreign key.
		/// </summary>
		/// <param name="customer_acct_id">The <c>customer_acct_id</c> column value.</param>
		/// <returns>A reference to the <see cref="System.Data.IDbCommand"/> object.</returns>
		protected virtual IDbCommand CreateGetByCustomer_acct_idCommand(short customer_acct_id)
		{
			string whereSql = "";
			whereSql += "[customer_acct_id]=" + _db.CreateSqlParameterName("Customer_acct_id");

			IDbCommand cmd = CreateGetCommand(whereSql, null);
			AddParameter(cmd, "Customer_acct_id", customer_acct_id);
			return cmd;
		}

		/// <summary>
		/// Adds a new record into the <c>RetailRoute</c> table.
		/// </summary>
		/// <param name="value">The <see cref="RetailRouteRow"/> object to be inserted.</param>
		public virtual void Insert(RetailRouteRow value)
		{
			string sqlStr = "INSERT INTO [dbo].[RetailRoute] (" +
				"[retail_route_id], " +
				"[customer_acct_id], " +
				"[route_id], " +
				"[status], " +
				"[start_bonus_minutes], " +
				"[bonus_minutes_type], " +
				"[multiplier]" +
				") VALUES (" +
				_db.CreateSqlParameterName("Retail_route_id") + ", " +
				_db.CreateSqlParameterName("Customer_acct_id") + ", " +
				_db.CreateSqlParameterName("Route_id") + ", " +
				_db.CreateSqlParameterName("Status") + ", " +
				_db.CreateSqlParameterName("Start_bonus_minutes") + ", " +
				_db.CreateSqlParameterName("Bonus_minutes_type") + ", " +
				_db.CreateSqlParameterName("Multiplier") + ")";
			IDbCommand cmd = _db.CreateCommand(sqlStr);
			AddParameter(cmd, "Retail_route_id", value.Retail_route_id);
			AddParameter(cmd, "Customer_acct_id", value.Customer_acct_id);
			AddParameter(cmd, "Route_id",
				value.IsRoute_idNull ? DBNull.Value : (object)value.Route_id);
			AddParameter(cmd, "Status", value.Status);
			AddParameter(cmd, "Start_bonus_minutes", value.Start_bonus_minutes);
			AddParameter(cmd, "Bonus_minutes_type", value.Bonus_minutes_type);
			AddParameter(cmd, "Multiplier", value.Multiplier);
			cmd.ExecuteNonQuery();
		}

		/// <summary>
		/// Updates a record in the <c>RetailRoute</c> table.
		/// </summary>
		/// <param name="value">The <see cref="RetailRouteRow"/>
		/// object used to update the table record.</param>
		/// <returns>true if the record was updated; otherwise, false.</returns>
		public virtual bool Update(RetailRouteRow value)
		{
			string sqlStr = "UPDATE [dbo].[RetailRoute] SET " +
				"[customer_acct_id]=" + _db.CreateSqlParameterName("Customer_acct_id") + ", " +
				"[route_id]=" + _db.CreateSqlParameterName("Route_id") + ", " +
				"[status]=" + _db.CreateSqlParameterName("Status") + ", " +
				"[start_bonus_minutes]=" + _db.CreateSqlParameterName("Start_bonus_minutes") + ", " +
				"[bonus_minutes_type]=" + _db.CreateSqlParameterName("Bonus_minutes_type") + ", " +
				"[multiplier]=" + _db.CreateSqlParameterName("Multiplier") +
				" WHERE " +
				"[retail_route_id]=" + _db.CreateSqlParameterName("Retail_route_id");
			IDbCommand cmd = _db.CreateCommand(sqlStr);
			AddParameter(cmd, "Customer_acct_id", value.Customer_acct_id);
			AddParameter(cmd, "Route_id",
				value.IsRoute_idNull ? DBNull.Value : (object)value.Route_id);
			AddParameter(cmd, "Status", value.Status);
			AddParameter(cmd, "Start_bonus_minutes", value.Start_bonus_minutes);
			AddParameter(cmd, "Bonus_minutes_type", value.Bonus_minutes_type);
			AddParameter(cmd, "Multiplier", value.Multiplier);
			AddParameter(cmd, "Retail_route_id", value.Retail_route_id);
			return 0 != cmd.ExecuteNonQuery();
		}

		/// <summary>
		/// Updates the <c>RetailRoute</c> table and calls the <c>AcceptChanges</c> method
		/// on the changed DataRow objects.
		/// </summary>
		/// <param name="table">The <see cref="System.Data.DataTable"/> used to update the data source.</param>
		public void Update(DataTable table)
		{
			Update(table, true);
		}

		/// <summary>
		/// Updates the <c>RetailRoute</c> table. Pass <c>false</c> as the <c>acceptChanges</c> 
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
							DeleteByPrimaryKey((int)row["Retail_route_id"]);
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
		/// Deletes the specified object from the <c>RetailRoute</c> table.
		/// </summary>
		/// <param name="value">The <see cref="RetailRouteRow"/> object to delete.</param>
		/// <returns>true if the record was deleted; otherwise, false.</returns>
		public bool Delete(RetailRouteRow value)
		{
			return DeleteByPrimaryKey(value.Retail_route_id);
		}

		/// <summary>
		/// Deletes a record from the <c>RetailRoute</c> table using
		/// the specified primary key.
		/// </summary>
		/// <param name="retail_route_id">The <c>retail_route_id</c> column value.</param>
		/// <returns>true if the record was deleted; otherwise, false.</returns>
		public virtual bool DeleteByPrimaryKey(int retail_route_id)
		{
			string whereSql = "[retail_route_id]=" + _db.CreateSqlParameterName("Retail_route_id");
			IDbCommand cmd = CreateDeleteCommand(whereSql);
			AddParameter(cmd, "Retail_route_id", retail_route_id);
			return 0 < cmd.ExecuteNonQuery();
		}

		/// <summary>
		/// Deletes records from the <c>RetailRoute</c> table using the 
		/// <c>R_355</c> foreign key.
		/// </summary>
		/// <param name="route_id">The <c>route_id</c> column value.</param>
		/// <returns>The number of records deleted from the table.</returns>
		public int DeleteByRoute_id(int route_id)
		{
			return DeleteByRoute_id(route_id, false);
		}

		/// <summary>
		/// Deletes records from the <c>RetailRoute</c> table using the 
		/// <c>R_355</c> foreign key.
		/// </summary>
		/// <param name="route_id">The <c>route_id</c> column value.</param>
		/// <param name="route_idNull">true if the method ignores the route_id
		/// parameter value and uses DbNull instead of it; otherwise, false.</param>
		/// <returns>The number of records deleted from the table.</returns>
		public int DeleteByRoute_id(int route_id, bool route_idNull)
		{
			return CreateDeleteByRoute_idCommand(route_id, route_idNull).ExecuteNonQuery();
		}

		/// <summary>
		/// Creates an <see cref="System.Data.IDbCommand"/> object that can be used to
		/// delete records using the <c>R_355</c> foreign key.
		/// </summary>
		/// <param name="route_id">The <c>route_id</c> column value.</param>
		/// <param name="route_idNull">true if the method ignores the route_id
		/// parameter value and uses DbNull instead of it; otherwise, false.</param>
		/// <returns>A reference to the <see cref="System.Data.IDbCommand"/> object.</returns>
		protected virtual IDbCommand CreateDeleteByRoute_idCommand(int route_id, bool route_idNull)
		{
			string whereSql = "";
			if(route_idNull)
				whereSql += "[route_id] IS NULL";
			else
				whereSql += "[route_id]=" + _db.CreateSqlParameterName("Route_id");

			IDbCommand cmd = CreateDeleteCommand(whereSql);
			if(!route_idNull)
				AddParameter(cmd, "Route_id", route_id);
			return cmd;
		}

		/// <summary>
		/// Deletes records from the <c>RetailRoute</c> table using the 
		/// <c>R_357</c> foreign key.
		/// </summary>
		/// <param name="customer_acct_id">The <c>customer_acct_id</c> column value.</param>
		/// <returns>The number of records deleted from the table.</returns>
		public int DeleteByCustomer_acct_id(short customer_acct_id)
		{
			return CreateDeleteByCustomer_acct_idCommand(customer_acct_id).ExecuteNonQuery();
		}

		/// <summary>
		/// Creates an <see cref="System.Data.IDbCommand"/> object that can be used to
		/// delete records using the <c>R_357</c> foreign key.
		/// </summary>
		/// <param name="customer_acct_id">The <c>customer_acct_id</c> column value.</param>
		/// <returns>A reference to the <see cref="System.Data.IDbCommand"/> object.</returns>
		protected virtual IDbCommand CreateDeleteByCustomer_acct_idCommand(short customer_acct_id)
		{
			string whereSql = "";
			whereSql += "[customer_acct_id]=" + _db.CreateSqlParameterName("Customer_acct_id");

			IDbCommand cmd = CreateDeleteCommand(whereSql);
			AddParameter(cmd, "Customer_acct_id", customer_acct_id);
			return cmd;
		}

		/// <summary>
		/// Deletes <c>RetailRoute</c> records that match the specified criteria.
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
		/// to delete <c>RetailRoute</c> records that match the specified criteria.
		/// </summary>
		/// <param name="whereSql">The SQL search condition. 
		/// For example: <c>"FirstName='Smith' AND Zip=75038"</c>.</param>
		/// <returns>A reference to the <see cref="System.Data.IDbCommand"/> object.</returns>
		protected virtual IDbCommand CreateDeleteCommand(string whereSql)
		{
			string sql = "DELETE FROM [dbo].[RetailRoute]";
			if(null != whereSql && 0 < whereSql.Length)
				sql += " WHERE " + whereSql;
			return _db.CreateCommand(sql);
		}

		/// <summary>
		/// Deletes all records from the <c>RetailRoute</c> table.
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
		/// <returns>An array of <see cref="RetailRouteRow"/> objects.</returns>
		protected RetailRouteRow[] MapRecords(IDbCommand command)
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
		/// <returns>An array of <see cref="RetailRouteRow"/> objects.</returns>
		protected RetailRouteRow[] MapRecords(IDataReader reader)
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
		/// <returns>An array of <see cref="RetailRouteRow"/> objects.</returns>
		protected virtual RetailRouteRow[] MapRecords(IDataReader reader, 
										int startIndex, int length, ref int totalRecordCount)
		{
			if(0 > startIndex)
				throw new ArgumentOutOfRangeException("startIndex", startIndex, "StartIndex cannot be less than zero.");
			if(0 > length)
				throw new ArgumentOutOfRangeException("length", length, "Length cannot be less than zero.");

			int retail_route_idColumnIndex = reader.GetOrdinal("retail_route_id");
			int customer_acct_idColumnIndex = reader.GetOrdinal("customer_acct_id");
			int route_idColumnIndex = reader.GetOrdinal("route_id");
			int statusColumnIndex = reader.GetOrdinal("status");
			int start_bonus_minutesColumnIndex = reader.GetOrdinal("start_bonus_minutes");
			int bonus_minutes_typeColumnIndex = reader.GetOrdinal("bonus_minutes_type");
			int multiplierColumnIndex = reader.GetOrdinal("multiplier");

			System.Collections.ArrayList recordList = new System.Collections.ArrayList();
			int ri = -startIndex;
			while(reader.Read())
			{
				ri++;
				if(ri > 0 && ri <= length)
				{
					RetailRouteRow record = new RetailRouteRow();
					recordList.Add(record);

					record.Retail_route_id = Convert.ToInt32(reader.GetValue(retail_route_idColumnIndex));
					record.Customer_acct_id = Convert.ToInt16(reader.GetValue(customer_acct_idColumnIndex));
					if(!reader.IsDBNull(route_idColumnIndex))
						record.Route_id = Convert.ToInt32(reader.GetValue(route_idColumnIndex));
					record.Status = Convert.ToByte(reader.GetValue(statusColumnIndex));
					record.Start_bonus_minutes = Convert.ToInt16(reader.GetValue(start_bonus_minutesColumnIndex));
					record.Bonus_minutes_type = Convert.ToByte(reader.GetValue(bonus_minutes_typeColumnIndex));
					record.Multiplier = Convert.ToInt16(reader.GetValue(multiplierColumnIndex));

					if(ri == length && 0 != totalRecordCount)
						break;
				}
			}

			totalRecordCount = 0 == totalRecordCount ? ri + startIndex : -1;
			return (RetailRouteRow[])(recordList.ToArray(typeof(RetailRouteRow)));
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
		/// Converts <see cref="System.Data.DataRow"/> to <see cref="RetailRouteRow"/>.
		/// </summary>
		/// <param name="row">The <see cref="System.Data.DataRow"/> object to be mapped.</param>
		/// <returns>A reference to the <see cref="RetailRouteRow"/> object.</returns>
		protected virtual RetailRouteRow MapRow(DataRow row)
		{
			RetailRouteRow mappedObject = new RetailRouteRow();
			DataTable dataTable = row.Table;
			DataColumn dataColumn;
			// Column "Retail_route_id"
			dataColumn = dataTable.Columns["Retail_route_id"];
			if(!row.IsNull(dataColumn))
				mappedObject.Retail_route_id = (int)row[dataColumn];
			// Column "Customer_acct_id"
			dataColumn = dataTable.Columns["Customer_acct_id"];
			if(!row.IsNull(dataColumn))
				mappedObject.Customer_acct_id = (short)row[dataColumn];
			// Column "Route_id"
			dataColumn = dataTable.Columns["Route_id"];
			if(!row.IsNull(dataColumn))
				mappedObject.Route_id = (int)row[dataColumn];
			// Column "Status"
			dataColumn = dataTable.Columns["Status"];
			if(!row.IsNull(dataColumn))
				mappedObject.Status = (byte)row[dataColumn];
			// Column "Start_bonus_minutes"
			dataColumn = dataTable.Columns["Start_bonus_minutes"];
			if(!row.IsNull(dataColumn))
				mappedObject.Start_bonus_minutes = (short)row[dataColumn];
			// Column "Bonus_minutes_type"
			dataColumn = dataTable.Columns["Bonus_minutes_type"];
			if(!row.IsNull(dataColumn))
				mappedObject.Bonus_minutes_type = (byte)row[dataColumn];
			// Column "Multiplier"
			dataColumn = dataTable.Columns["Multiplier"];
			if(!row.IsNull(dataColumn))
				mappedObject.Multiplier = (short)row[dataColumn];
			return mappedObject;
		}

		/// <summary>
		/// Creates a <see cref="System.Data.DataTable"/> object for 
		/// the <c>RetailRoute</c> table.
		/// </summary>
		/// <returns>A reference to the <see cref="System.Data.DataTable"/> object.</returns>
		protected virtual DataTable CreateDataTable()
		{
			DataTable dataTable = new DataTable();
			dataTable.TableName = "RetailRoute";
			DataColumn dataColumn;
			dataColumn = dataTable.Columns.Add("Retail_route_id", typeof(int));
			dataColumn.Caption = "retail_route_id";
			dataColumn.AllowDBNull = false;
			dataColumn = dataTable.Columns.Add("Customer_acct_id", typeof(short));
			dataColumn.Caption = "customer_acct_id";
			dataColumn.AllowDBNull = false;
			dataColumn = dataTable.Columns.Add("Route_id", typeof(int));
			dataColumn.Caption = "route_id";
			dataColumn = dataTable.Columns.Add("Status", typeof(byte));
			dataColumn.Caption = "status";
			dataColumn.AllowDBNull = false;
			dataColumn = dataTable.Columns.Add("Start_bonus_minutes", typeof(short));
			dataColumn.Caption = "start_bonus_minutes";
			dataColumn.AllowDBNull = false;
			dataColumn = dataTable.Columns.Add("Bonus_minutes_type", typeof(byte));
			dataColumn.Caption = "bonus_minutes_type";
			dataColumn.AllowDBNull = false;
			dataColumn = dataTable.Columns.Add("Multiplier", typeof(short));
			dataColumn.Caption = "multiplier";
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
				case "Retail_route_id":
					parameter = _db.AddParameter(cmd, paramName, DbType.Int32, value);
					break;

				case "Customer_acct_id":
					parameter = _db.AddParameter(cmd, paramName, DbType.Int16, value);
					break;

				case "Route_id":
					parameter = _db.AddParameter(cmd, paramName, DbType.Int32, value);
					break;

				case "Status":
					parameter = _db.AddParameter(cmd, paramName, DbType.Byte, value);
					break;

				case "Start_bonus_minutes":
					parameter = _db.AddParameter(cmd, paramName, DbType.Int16, value);
					break;

				case "Bonus_minutes_type":
					parameter = _db.AddParameter(cmd, paramName, DbType.Byte, value);
					break;

				case "Multiplier":
					parameter = _db.AddParameter(cmd, paramName, DbType.Int16, value);
					break;

				default:
					throw new ArgumentException("Unknown parameter name (" + paramName + ").");
			}
			return parameter;
		}

	} // End of RetailRouteCollection_Base class
}  // End of namespace

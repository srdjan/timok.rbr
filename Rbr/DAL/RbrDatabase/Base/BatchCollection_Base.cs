// <fileinfo name="Base\BatchCollection_Base.cs">
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
	/// The base class for <see cref="BatchCollection"/>. Provides methods 
	/// for common database table operations. 
	/// </summary>
	/// <remarks>
	/// Do not change this source code. Update the <see cref="BatchCollection"/>
	/// class if you need to add or change some functionality.
	/// </remarks>
	public abstract class BatchCollection_Base
	{
		// Constants
		public const string Batch_idColumnName = "batch_id";
		public const string StatusColumnName = "status";
		public const string First_serialColumnName = "first_serial";
		public const string Last_serialColumnName = "last_serial";
		public const string Request_idColumnName = "request_id";
		public const string Box_idColumnName = "box_id";
		public const string Customer_acct_idColumnName = "customer_acct_id";

		// Instance fields
		private Rbr_Db _db;

		/// <summary>
		/// Initializes a new instance of the <see cref="BatchCollection_Base"/> 
		/// class with the specified <see cref="Rbr_Db"/>.
		/// </summary>
		/// <param name="db">The <see cref="Rbr_Db"/> object.</param>
		public BatchCollection_Base(Rbr_Db db)
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
		/// Gets an array of all records from the <c>Batch</c> table.
		/// </summary>
		/// <returns>An array of <see cref="BatchRow"/> objects.</returns>
		public virtual BatchRow[] GetAll()
		{
			return MapRecords(CreateGetAllCommand());
		}

		/// <summary>
		/// Gets a <see cref="System.Data.DataTable"/> object that 
		/// includes all records from the <c>Batch</c> table.
		/// </summary>
		/// <returns>A reference to the <see cref="System.Data.DataTable"/> object.</returns>
		public virtual DataTable GetAllAsDataTable()
		{
			return MapRecordsToDataTable(CreateGetAllCommand());
		}

		/// <summary>
		/// Creates and returns an <see cref="System.Data.IDbCommand"/> object that is used
		/// to retrieve all records from the <c>Batch</c> table.
		/// </summary>
		/// <returns>A reference to the <see cref="System.Data.IDbCommand"/> object.</returns>
		protected virtual IDbCommand CreateGetAllCommand()
		{
			return CreateGetCommand(null, null);
		}

		/// <summary>
		/// Gets the first <see cref="BatchRow"/> objects that 
		/// match the search condition.
		/// </summary>
		/// <param name="whereSql">The SQL search condition. For example: 
		/// <c>"FirstName='Smith' AND Zip=75038"</c>.</param>
		/// <returns>An instance of <see cref="BatchRow"/> or null reference 
		/// (Nothing in Visual Basic) if the object was not found.</returns>
		public BatchRow GetRow(string whereSql)
		{
			int totalRecordCount = -1;
			BatchRow[] rows = GetAsArray(whereSql, null, 0, 1, ref totalRecordCount);
			return 0 == rows.Length ? null : rows[0];
		}

		/// <summary>
		/// Gets an array of <see cref="BatchRow"/> objects that 
		/// match the search condition, in the the specified sort order.
		/// </summary>
		/// <param name="whereSql">The SQL search condition. For example: 
		/// <c>"FirstName='Smith' AND Zip=75038"</c>.</param>
		/// <param name="orderBySql">The column name(s) followed by "ASC" (ascending) or "DESC" (descending).
		/// Columns are sorted in ascending order by default. For example: <c>"LastName ASC, FirstName ASC"</c>.</param>
		/// <returns>An array of <see cref="BatchRow"/> objects.</returns>
		public BatchRow[] GetAsArray(string whereSql, string orderBySql)
		{
			int totalRecordCount = -1;
			return GetAsArray(whereSql, orderBySql, 0, int.MaxValue, ref totalRecordCount);
		}

		/// <summary>
		/// Gets an array of <see cref="BatchRow"/> objects that 
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
		/// <returns>An array of <see cref="BatchRow"/> objects.</returns>
		public virtual BatchRow[] GetAsArray(string whereSql, string orderBySql,
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
			string sql = "SELECT * FROM [dbo].[Batch]";
			if(null != whereSql && 0 < whereSql.Length)
				sql += " WHERE " + whereSql;
			if(null != orderBySql && 0 < orderBySql.Length)
				sql += " ORDER BY " + orderBySql;
			return _db.CreateCommand(sql);
		}

		/// <summary>
		/// Gets <see cref="BatchRow"/> by the primary key.
		/// </summary>
		/// <param name="batch_id">The <c>batch_id</c> column value.</param>
		/// <returns>An instance of <see cref="BatchRow"/> or null reference 
		/// (Nothing in Visual Basic) if the object was not found.</returns>
		public virtual BatchRow GetByPrimaryKey(int batch_id)
		{
			string whereSql = "[batch_id]=" + _db.CreateSqlParameterName("Batch_id");
			IDbCommand cmd = CreateGetCommand(whereSql, null);
			AddParameter(cmd, "Batch_id", batch_id);
			BatchRow[] tempArray = MapRecords(cmd);
			return 0 == tempArray.Length ? null : tempArray[0];
		}

		/// <summary>
		/// Gets an array of <see cref="BatchRow"/> objects 
		/// by the <c>R_282</c> foreign key.
		/// </summary>
		/// <param name="box_id">The <c>box_id</c> column value.</param>
		/// <returns>An array of <see cref="BatchRow"/> objects.</returns>
		public BatchRow[] GetByBox_id(int box_id)
		{
			return GetByBox_id(box_id, false);
		}

		/// <summary>
		/// Gets an array of <see cref="BatchRow"/> objects 
		/// by the <c>R_282</c> foreign key.
		/// </summary>
		/// <param name="box_id">The <c>box_id</c> column value.</param>
		/// <param name="box_idNull">true if the method ignores the box_id
		/// parameter value and uses DbNull instead of it; otherwise, false.</param>
		/// <returns>An array of <see cref="BatchRow"/> objects.</returns>
		public virtual BatchRow[] GetByBox_id(int box_id, bool box_idNull)
		{
			return MapRecords(CreateGetByBox_idCommand(box_id, box_idNull));
		}

		/// <summary>
		/// Gets a <see cref="System.Data.DataTable"/> object 
		/// by the <c>R_282</c> foreign key.
		/// </summary>
		/// <param name="box_id">The <c>box_id</c> column value.</param>
		/// <returns>A reference to the <see cref="System.Data.DataTable"/> object.</returns>
		public DataTable GetByBox_idAsDataTable(int box_id)
		{
			return GetByBox_idAsDataTable(box_id, false);
		}

		/// <summary>
		/// Gets a <see cref="System.Data.DataTable"/> object 
		/// by the <c>R_282</c> foreign key.
		/// </summary>
		/// <param name="box_id">The <c>box_id</c> column value.</param>
		/// <param name="box_idNull">true if the method ignores the box_id
		/// parameter value and uses DbNull instead of it; otherwise, false.</param>
		/// <returns>A reference to the <see cref="System.Data.DataTable"/> object.</returns>
		public virtual DataTable GetByBox_idAsDataTable(int box_id, bool box_idNull)
		{
			return MapRecordsToDataTable(CreateGetByBox_idCommand(box_id, box_idNull));
		}

		/// <summary>
		/// Creates an <see cref="System.Data.IDbCommand"/> object that can be used to 
		/// return records by the <c>R_282</c> foreign key.
		/// </summary>
		/// <param name="box_id">The <c>box_id</c> column value.</param>
		/// <param name="box_idNull">true if the method ignores the box_id
		/// parameter value and uses DbNull instead of it; otherwise, false.</param>
		/// <returns>A reference to the <see cref="System.Data.IDbCommand"/> object.</returns>
		protected virtual IDbCommand CreateGetByBox_idCommand(int box_id, bool box_idNull)
		{
			string whereSql = "";
			if(box_idNull)
				whereSql += "[box_id] IS NULL";
			else
				whereSql += "[box_id]=" + _db.CreateSqlParameterName("Box_id");

			IDbCommand cmd = CreateGetCommand(whereSql, null);
			if(!box_idNull)
				AddParameter(cmd, "Box_id", box_id);
			return cmd;
		}

		/// <summary>
		/// Gets an array of <see cref="BatchRow"/> objects 
		/// by the <c>R_290</c> foreign key.
		/// </summary>
		/// <param name="request_id">The <c>request_id</c> column value.</param>
		/// <returns>An array of <see cref="BatchRow"/> objects.</returns>
		public virtual BatchRow[] GetByRequest_id(int request_id)
		{
			return MapRecords(CreateGetByRequest_idCommand(request_id));
		}

		/// <summary>
		/// Gets a <see cref="System.Data.DataTable"/> object 
		/// by the <c>R_290</c> foreign key.
		/// </summary>
		/// <param name="request_id">The <c>request_id</c> column value.</param>
		/// <returns>A reference to the <see cref="System.Data.DataTable"/> object.</returns>
		public virtual DataTable GetByRequest_idAsDataTable(int request_id)
		{
			return MapRecordsToDataTable(CreateGetByRequest_idCommand(request_id));
		}

		/// <summary>
		/// Creates an <see cref="System.Data.IDbCommand"/> object that can be used to 
		/// return records by the <c>R_290</c> foreign key.
		/// </summary>
		/// <param name="request_id">The <c>request_id</c> column value.</param>
		/// <returns>A reference to the <see cref="System.Data.IDbCommand"/> object.</returns>
		protected virtual IDbCommand CreateGetByRequest_idCommand(int request_id)
		{
			string whereSql = "";
			whereSql += "[request_id]=" + _db.CreateSqlParameterName("Request_id");

			IDbCommand cmd = CreateGetCommand(whereSql, null);
			AddParameter(cmd, "Request_id", request_id);
			return cmd;
		}

		/// <summary>
		/// Adds a new record into the <c>Batch</c> table.
		/// </summary>
		/// <param name="value">The <see cref="BatchRow"/> object to be inserted.</param>
		public virtual void Insert(BatchRow value)
		{
			string sqlStr = "INSERT INTO [dbo].[Batch] (" +
				"[batch_id], " +
				"[status], " +
				"[first_serial], " +
				"[last_serial], " +
				"[request_id], " +
				"[box_id], " +
				"[customer_acct_id]" +
				") VALUES (" +
				_db.CreateSqlParameterName("Batch_id") + ", " +
				_db.CreateSqlParameterName("Status") + ", " +
				_db.CreateSqlParameterName("First_serial") + ", " +
				_db.CreateSqlParameterName("Last_serial") + ", " +
				_db.CreateSqlParameterName("Request_id") + ", " +
				_db.CreateSqlParameterName("Box_id") + ", " +
				_db.CreateSqlParameterName("Customer_acct_id") + ")";
			IDbCommand cmd = _db.CreateCommand(sqlStr);
			AddParameter(cmd, "Batch_id", value.Batch_id);
			AddParameter(cmd, "Status", value.Status);
			AddParameter(cmd, "First_serial", value.First_serial);
			AddParameter(cmd, "Last_serial", value.Last_serial);
			AddParameter(cmd, "Request_id", value.Request_id);
			AddParameter(cmd, "Box_id",
				value.IsBox_idNull ? DBNull.Value : (object)value.Box_id);
			AddParameter(cmd, "Customer_acct_id",
				value.IsCustomer_acct_idNull ? DBNull.Value : (object)value.Customer_acct_id);
			cmd.ExecuteNonQuery();
		}

		/// <summary>
		/// Updates a record in the <c>Batch</c> table.
		/// </summary>
		/// <param name="value">The <see cref="BatchRow"/>
		/// object used to update the table record.</param>
		/// <returns>true if the record was updated; otherwise, false.</returns>
		public virtual bool Update(BatchRow value)
		{
			string sqlStr = "UPDATE [dbo].[Batch] SET " +
				"[status]=" + _db.CreateSqlParameterName("Status") + ", " +
				"[first_serial]=" + _db.CreateSqlParameterName("First_serial") + ", " +
				"[last_serial]=" + _db.CreateSqlParameterName("Last_serial") + ", " +
				"[request_id]=" + _db.CreateSqlParameterName("Request_id") + ", " +
				"[box_id]=" + _db.CreateSqlParameterName("Box_id") + ", " +
				"[customer_acct_id]=" + _db.CreateSqlParameterName("Customer_acct_id") +
				" WHERE " +
				"[batch_id]=" + _db.CreateSqlParameterName("Batch_id");
			IDbCommand cmd = _db.CreateCommand(sqlStr);
			AddParameter(cmd, "Status", value.Status);
			AddParameter(cmd, "First_serial", value.First_serial);
			AddParameter(cmd, "Last_serial", value.Last_serial);
			AddParameter(cmd, "Request_id", value.Request_id);
			AddParameter(cmd, "Box_id",
				value.IsBox_idNull ? DBNull.Value : (object)value.Box_id);
			AddParameter(cmd, "Customer_acct_id",
				value.IsCustomer_acct_idNull ? DBNull.Value : (object)value.Customer_acct_id);
			AddParameter(cmd, "Batch_id", value.Batch_id);
			return 0 != cmd.ExecuteNonQuery();
		}

		/// <summary>
		/// Updates the <c>Batch</c> table and calls the <c>AcceptChanges</c> method
		/// on the changed DataRow objects.
		/// </summary>
		/// <param name="table">The <see cref="System.Data.DataTable"/> used to update the data source.</param>
		public void Update(DataTable table)
		{
			Update(table, true);
		}

		/// <summary>
		/// Updates the <c>Batch</c> table. Pass <c>false</c> as the <c>acceptChanges</c> 
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
							DeleteByPrimaryKey((int)row["Batch_id"]);
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
		/// Deletes the specified object from the <c>Batch</c> table.
		/// </summary>
		/// <param name="value">The <see cref="BatchRow"/> object to delete.</param>
		/// <returns>true if the record was deleted; otherwise, false.</returns>
		public bool Delete(BatchRow value)
		{
			return DeleteByPrimaryKey(value.Batch_id);
		}

		/// <summary>
		/// Deletes a record from the <c>Batch</c> table using
		/// the specified primary key.
		/// </summary>
		/// <param name="batch_id">The <c>batch_id</c> column value.</param>
		/// <returns>true if the record was deleted; otherwise, false.</returns>
		public virtual bool DeleteByPrimaryKey(int batch_id)
		{
			string whereSql = "[batch_id]=" + _db.CreateSqlParameterName("Batch_id");
			IDbCommand cmd = CreateDeleteCommand(whereSql);
			AddParameter(cmd, "Batch_id", batch_id);
			return 0 < cmd.ExecuteNonQuery();
		}

		/// <summary>
		/// Deletes records from the <c>Batch</c> table using the 
		/// <c>R_282</c> foreign key.
		/// </summary>
		/// <param name="box_id">The <c>box_id</c> column value.</param>
		/// <returns>The number of records deleted from the table.</returns>
		public int DeleteByBox_id(int box_id)
		{
			return DeleteByBox_id(box_id, false);
		}

		/// <summary>
		/// Deletes records from the <c>Batch</c> table using the 
		/// <c>R_282</c> foreign key.
		/// </summary>
		/// <param name="box_id">The <c>box_id</c> column value.</param>
		/// <param name="box_idNull">true if the method ignores the box_id
		/// parameter value and uses DbNull instead of it; otherwise, false.</param>
		/// <returns>The number of records deleted from the table.</returns>
		public int DeleteByBox_id(int box_id, bool box_idNull)
		{
			return CreateDeleteByBox_idCommand(box_id, box_idNull).ExecuteNonQuery();
		}

		/// <summary>
		/// Creates an <see cref="System.Data.IDbCommand"/> object that can be used to
		/// delete records using the <c>R_282</c> foreign key.
		/// </summary>
		/// <param name="box_id">The <c>box_id</c> column value.</param>
		/// <param name="box_idNull">true if the method ignores the box_id
		/// parameter value and uses DbNull instead of it; otherwise, false.</param>
		/// <returns>A reference to the <see cref="System.Data.IDbCommand"/> object.</returns>
		protected virtual IDbCommand CreateDeleteByBox_idCommand(int box_id, bool box_idNull)
		{
			string whereSql = "";
			if(box_idNull)
				whereSql += "[box_id] IS NULL";
			else
				whereSql += "[box_id]=" + _db.CreateSqlParameterName("Box_id");

			IDbCommand cmd = CreateDeleteCommand(whereSql);
			if(!box_idNull)
				AddParameter(cmd, "Box_id", box_id);
			return cmd;
		}

		/// <summary>
		/// Deletes records from the <c>Batch</c> table using the 
		/// <c>R_290</c> foreign key.
		/// </summary>
		/// <param name="request_id">The <c>request_id</c> column value.</param>
		/// <returns>The number of records deleted from the table.</returns>
		public int DeleteByRequest_id(int request_id)
		{
			return CreateDeleteByRequest_idCommand(request_id).ExecuteNonQuery();
		}

		/// <summary>
		/// Creates an <see cref="System.Data.IDbCommand"/> object that can be used to
		/// delete records using the <c>R_290</c> foreign key.
		/// </summary>
		/// <param name="request_id">The <c>request_id</c> column value.</param>
		/// <returns>A reference to the <see cref="System.Data.IDbCommand"/> object.</returns>
		protected virtual IDbCommand CreateDeleteByRequest_idCommand(int request_id)
		{
			string whereSql = "";
			whereSql += "[request_id]=" + _db.CreateSqlParameterName("Request_id");

			IDbCommand cmd = CreateDeleteCommand(whereSql);
			AddParameter(cmd, "Request_id", request_id);
			return cmd;
		}

		/// <summary>
		/// Deletes <c>Batch</c> records that match the specified criteria.
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
		/// to delete <c>Batch</c> records that match the specified criteria.
		/// </summary>
		/// <param name="whereSql">The SQL search condition. 
		/// For example: <c>"FirstName='Smith' AND Zip=75038"</c>.</param>
		/// <returns>A reference to the <see cref="System.Data.IDbCommand"/> object.</returns>
		protected virtual IDbCommand CreateDeleteCommand(string whereSql)
		{
			string sql = "DELETE FROM [dbo].[Batch]";
			if(null != whereSql && 0 < whereSql.Length)
				sql += " WHERE " + whereSql;
			return _db.CreateCommand(sql);
		}

		/// <summary>
		/// Deletes all records from the <c>Batch</c> table.
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
		/// <returns>An array of <see cref="BatchRow"/> objects.</returns>
		protected BatchRow[] MapRecords(IDbCommand command)
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
		/// <returns>An array of <see cref="BatchRow"/> objects.</returns>
		protected BatchRow[] MapRecords(IDataReader reader)
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
		/// <returns>An array of <see cref="BatchRow"/> objects.</returns>
		protected virtual BatchRow[] MapRecords(IDataReader reader, 
										int startIndex, int length, ref int totalRecordCount)
		{
			if(0 > startIndex)
				throw new ArgumentOutOfRangeException("startIndex", startIndex, "StartIndex cannot be less than zero.");
			if(0 > length)
				throw new ArgumentOutOfRangeException("length", length, "Length cannot be less than zero.");

			int batch_idColumnIndex = reader.GetOrdinal("batch_id");
			int statusColumnIndex = reader.GetOrdinal("status");
			int first_serialColumnIndex = reader.GetOrdinal("first_serial");
			int last_serialColumnIndex = reader.GetOrdinal("last_serial");
			int request_idColumnIndex = reader.GetOrdinal("request_id");
			int box_idColumnIndex = reader.GetOrdinal("box_id");
			int customer_acct_idColumnIndex = reader.GetOrdinal("customer_acct_id");

			System.Collections.ArrayList recordList = new System.Collections.ArrayList();
			int ri = -startIndex;
			while(reader.Read())
			{
				ri++;
				if(ri > 0 && ri <= length)
				{
					BatchRow record = new BatchRow();
					recordList.Add(record);

					record.Batch_id = Convert.ToInt32(reader.GetValue(batch_idColumnIndex));
					record.Status = Convert.ToByte(reader.GetValue(statusColumnIndex));
					record.First_serial = Convert.ToInt64(reader.GetValue(first_serialColumnIndex));
					record.Last_serial = Convert.ToInt64(reader.GetValue(last_serialColumnIndex));
					record.Request_id = Convert.ToInt32(reader.GetValue(request_idColumnIndex));
					if(!reader.IsDBNull(box_idColumnIndex))
						record.Box_id = Convert.ToInt32(reader.GetValue(box_idColumnIndex));
					if(!reader.IsDBNull(customer_acct_idColumnIndex))
						record.Customer_acct_id = Convert.ToInt16(reader.GetValue(customer_acct_idColumnIndex));

					if(ri == length && 0 != totalRecordCount)
						break;
				}
			}

			totalRecordCount = 0 == totalRecordCount ? ri + startIndex : -1;
			return (BatchRow[])(recordList.ToArray(typeof(BatchRow)));
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
		/// Converts <see cref="System.Data.DataRow"/> to <see cref="BatchRow"/>.
		/// </summary>
		/// <param name="row">The <see cref="System.Data.DataRow"/> object to be mapped.</param>
		/// <returns>A reference to the <see cref="BatchRow"/> object.</returns>
		protected virtual BatchRow MapRow(DataRow row)
		{
			BatchRow mappedObject = new BatchRow();
			DataTable dataTable = row.Table;
			DataColumn dataColumn;
			// Column "Batch_id"
			dataColumn = dataTable.Columns["Batch_id"];
			if(!row.IsNull(dataColumn))
				mappedObject.Batch_id = (int)row[dataColumn];
			// Column "Status"
			dataColumn = dataTable.Columns["Status"];
			if(!row.IsNull(dataColumn))
				mappedObject.Status = (byte)row[dataColumn];
			// Column "First_serial"
			dataColumn = dataTable.Columns["First_serial"];
			if(!row.IsNull(dataColumn))
				mappedObject.First_serial = (long)row[dataColumn];
			// Column "Last_serial"
			dataColumn = dataTable.Columns["Last_serial"];
			if(!row.IsNull(dataColumn))
				mappedObject.Last_serial = (long)row[dataColumn];
			// Column "Request_id"
			dataColumn = dataTable.Columns["Request_id"];
			if(!row.IsNull(dataColumn))
				mappedObject.Request_id = (int)row[dataColumn];
			// Column "Box_id"
			dataColumn = dataTable.Columns["Box_id"];
			if(!row.IsNull(dataColumn))
				mappedObject.Box_id = (int)row[dataColumn];
			// Column "Customer_acct_id"
			dataColumn = dataTable.Columns["Customer_acct_id"];
			if(!row.IsNull(dataColumn))
				mappedObject.Customer_acct_id = (short)row[dataColumn];
			return mappedObject;
		}

		/// <summary>
		/// Creates a <see cref="System.Data.DataTable"/> object for 
		/// the <c>Batch</c> table.
		/// </summary>
		/// <returns>A reference to the <see cref="System.Data.DataTable"/> object.</returns>
		protected virtual DataTable CreateDataTable()
		{
			DataTable dataTable = new DataTable();
			dataTable.TableName = "Batch";
			DataColumn dataColumn;
			dataColumn = dataTable.Columns.Add("Batch_id", typeof(int));
			dataColumn.Caption = "batch_id";
			dataColumn.AllowDBNull = false;
			dataColumn = dataTable.Columns.Add("Status", typeof(byte));
			dataColumn.Caption = "status";
			dataColumn.AllowDBNull = false;
			dataColumn = dataTable.Columns.Add("First_serial", typeof(long));
			dataColumn.Caption = "first_serial";
			dataColumn.AllowDBNull = false;
			dataColumn = dataTable.Columns.Add("Last_serial", typeof(long));
			dataColumn.Caption = "last_serial";
			dataColumn.AllowDBNull = false;
			dataColumn = dataTable.Columns.Add("Request_id", typeof(int));
			dataColumn.Caption = "request_id";
			dataColumn.AllowDBNull = false;
			dataColumn = dataTable.Columns.Add("Box_id", typeof(int));
			dataColumn.Caption = "box_id";
			dataColumn = dataTable.Columns.Add("Customer_acct_id", typeof(short));
			dataColumn.Caption = "customer_acct_id";
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
				case "Batch_id":
					parameter = _db.AddParameter(cmd, paramName, DbType.Int32, value);
					break;

				case "Status":
					parameter = _db.AddParameter(cmd, paramName, DbType.Byte, value);
					break;

				case "First_serial":
					parameter = _db.AddParameter(cmd, paramName, DbType.Int64, value);
					break;

				case "Last_serial":
					parameter = _db.AddParameter(cmd, paramName, DbType.Int64, value);
					break;

				case "Request_id":
					parameter = _db.AddParameter(cmd, paramName, DbType.Int32, value);
					break;

				case "Box_id":
					parameter = _db.AddParameter(cmd, paramName, DbType.Int32, value);
					break;

				case "Customer_acct_id":
					parameter = _db.AddParameter(cmd, paramName, DbType.Int16, value);
					break;

				default:
					throw new ArgumentException("Unknown parameter name (" + paramName + ").");
			}
			return parameter;
		}

	} // End of BatchCollection_Base class
}  // End of namespace

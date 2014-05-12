// <fileinfo name="Base\GenerationRequestCollection_Base.cs">
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
	/// The base class for <see cref="GenerationRequestCollection"/>. Provides methods 
	/// for common database table operations. 
	/// </summary>
	/// <remarks>
	/// Do not change this source code. Update the <see cref="GenerationRequestCollection"/>
	/// class if you need to add or change some functionality.
	/// </remarks>
	public abstract class GenerationRequestCollection_Base
	{
		// Constants
		public const string Request_idColumnName = "request_id";
		public const string Date_requestedColumnName = "date_requested";
		public const string Date_to_processColumnName = "date_to_process";
		public const string Date_completedColumnName = "date_completed";
		public const string Number_of_batchesColumnName = "number_of_batches";
		public const string Batch_sizeColumnName = "batch_size";
		public const string Lot_idColumnName = "lot_id";

		// Instance fields
		private Rbr_Db _db;

		/// <summary>
		/// Initializes a new instance of the <see cref="GenerationRequestCollection_Base"/> 
		/// class with the specified <see cref="Rbr_Db"/>.
		/// </summary>
		/// <param name="db">The <see cref="Rbr_Db"/> object.</param>
		public GenerationRequestCollection_Base(Rbr_Db db)
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
		/// Gets an array of all records from the <c>GenerationRequest</c> table.
		/// </summary>
		/// <returns>An array of <see cref="GenerationRequestRow"/> objects.</returns>
		public virtual GenerationRequestRow[] GetAll()
		{
			return MapRecords(CreateGetAllCommand());
		}

		/// <summary>
		/// Gets a <see cref="System.Data.DataTable"/> object that 
		/// includes all records from the <c>GenerationRequest</c> table.
		/// </summary>
		/// <returns>A reference to the <see cref="System.Data.DataTable"/> object.</returns>
		public virtual DataTable GetAllAsDataTable()
		{
			return MapRecordsToDataTable(CreateGetAllCommand());
		}

		/// <summary>
		/// Creates and returns an <see cref="System.Data.IDbCommand"/> object that is used
		/// to retrieve all records from the <c>GenerationRequest</c> table.
		/// </summary>
		/// <returns>A reference to the <see cref="System.Data.IDbCommand"/> object.</returns>
		protected virtual IDbCommand CreateGetAllCommand()
		{
			return CreateGetCommand(null, null);
		}

		/// <summary>
		/// Gets the first <see cref="GenerationRequestRow"/> objects that 
		/// match the search condition.
		/// </summary>
		/// <param name="whereSql">The SQL search condition. For example: 
		/// <c>"FirstName='Smith' AND Zip=75038"</c>.</param>
		/// <returns>An instance of <see cref="GenerationRequestRow"/> or null reference 
		/// (Nothing in Visual Basic) if the object was not found.</returns>
		public GenerationRequestRow GetRow(string whereSql)
		{
			int totalRecordCount = -1;
			GenerationRequestRow[] rows = GetAsArray(whereSql, null, 0, 1, ref totalRecordCount);
			return 0 == rows.Length ? null : rows[0];
		}

		/// <summary>
		/// Gets an array of <see cref="GenerationRequestRow"/> objects that 
		/// match the search condition, in the the specified sort order.
		/// </summary>
		/// <param name="whereSql">The SQL search condition. For example: 
		/// <c>"FirstName='Smith' AND Zip=75038"</c>.</param>
		/// <param name="orderBySql">The column name(s) followed by "ASC" (ascending) or "DESC" (descending).
		/// Columns are sorted in ascending order by default. For example: <c>"LastName ASC, FirstName ASC"</c>.</param>
		/// <returns>An array of <see cref="GenerationRequestRow"/> objects.</returns>
		public GenerationRequestRow[] GetAsArray(string whereSql, string orderBySql)
		{
			int totalRecordCount = -1;
			return GetAsArray(whereSql, orderBySql, 0, int.MaxValue, ref totalRecordCount);
		}

		/// <summary>
		/// Gets an array of <see cref="GenerationRequestRow"/> objects that 
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
		/// <returns>An array of <see cref="GenerationRequestRow"/> objects.</returns>
		public virtual GenerationRequestRow[] GetAsArray(string whereSql, string orderBySql,
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
			string sql = "SELECT * FROM [dbo].[GenerationRequest]";
			if(null != whereSql && 0 < whereSql.Length)
				sql += " WHERE " + whereSql;
			if(null != orderBySql && 0 < orderBySql.Length)
				sql += " ORDER BY " + orderBySql;
			return _db.CreateCommand(sql);
		}

		/// <summary>
		/// Gets <see cref="GenerationRequestRow"/> by the primary key.
		/// </summary>
		/// <param name="request_id">The <c>request_id</c> column value.</param>
		/// <returns>An instance of <see cref="GenerationRequestRow"/> or null reference 
		/// (Nothing in Visual Basic) if the object was not found.</returns>
		public virtual GenerationRequestRow GetByPrimaryKey(int request_id)
		{
			string whereSql = "[request_id]=" + _db.CreateSqlParameterName("Request_id");
			IDbCommand cmd = CreateGetCommand(whereSql, null);
			AddParameter(cmd, "Request_id", request_id);
			GenerationRequestRow[] tempArray = MapRecords(cmd);
			return 0 == tempArray.Length ? null : tempArray[0];
		}

		/// <summary>
		/// Gets an array of <see cref="GenerationRequestRow"/> objects 
		/// by the <c>R_289</c> foreign key.
		/// </summary>
		/// <param name="lot_id">The <c>lot_id</c> column value.</param>
		/// <returns>An array of <see cref="GenerationRequestRow"/> objects.</returns>
		public virtual GenerationRequestRow[] GetByLot_id(int lot_id)
		{
			return MapRecords(CreateGetByLot_idCommand(lot_id));
		}

		/// <summary>
		/// Gets a <see cref="System.Data.DataTable"/> object 
		/// by the <c>R_289</c> foreign key.
		/// </summary>
		/// <param name="lot_id">The <c>lot_id</c> column value.</param>
		/// <returns>A reference to the <see cref="System.Data.DataTable"/> object.</returns>
		public virtual DataTable GetByLot_idAsDataTable(int lot_id)
		{
			return MapRecordsToDataTable(CreateGetByLot_idCommand(lot_id));
		}

		/// <summary>
		/// Creates an <see cref="System.Data.IDbCommand"/> object that can be used to 
		/// return records by the <c>R_289</c> foreign key.
		/// </summary>
		/// <param name="lot_id">The <c>lot_id</c> column value.</param>
		/// <returns>A reference to the <see cref="System.Data.IDbCommand"/> object.</returns>
		protected virtual IDbCommand CreateGetByLot_idCommand(int lot_id)
		{
			string whereSql = "";
			whereSql += "[lot_id]=" + _db.CreateSqlParameterName("Lot_id");

			IDbCommand cmd = CreateGetCommand(whereSql, null);
			AddParameter(cmd, "Lot_id", lot_id);
			return cmd;
		}

		/// <summary>
		/// Adds a new record into the <c>GenerationRequest</c> table.
		/// </summary>
		/// <param name="value">The <see cref="GenerationRequestRow"/> object to be inserted.</param>
		public virtual void Insert(GenerationRequestRow value)
		{
			string sqlStr = "INSERT INTO [dbo].[GenerationRequest] (" +
				"[request_id], " +
				"[date_requested], " +
				"[date_to_process], " +
				"[number_of_batches], " +
				"[batch_size], " +
				"[lot_id]" +
				") VALUES (" +
				_db.CreateSqlParameterName("Request_id") + ", " +
				_db.CreateSqlParameterName("Date_requested") + ", " +
				_db.CreateSqlParameterName("Date_to_process") + ", " +
				_db.CreateSqlParameterName("Number_of_batches") + ", " +
				_db.CreateSqlParameterName("Batch_size") + ", " +
				_db.CreateSqlParameterName("Lot_id") + ")";
			IDbCommand cmd = _db.CreateCommand(sqlStr);
			AddParameter(cmd, "Request_id", value.Request_id);
			AddParameter(cmd, "Date_requested", value.Date_requested);
			AddParameter(cmd, "Date_to_process", value.Date_to_process);
			AddParameter(cmd, "Number_of_batches", value.Number_of_batches);
			AddParameter(cmd, "Batch_size", value.Batch_size);
			AddParameter(cmd, "Lot_id", value.Lot_id);
			cmd.ExecuteNonQuery();
		}

		/// <summary>
		/// Updates a record in the <c>GenerationRequest</c> table.
		/// </summary>
		/// <param name="value">The <see cref="GenerationRequestRow"/>
		/// object used to update the table record.</param>
		/// <returns>true if the record was updated; otherwise, false.</returns>
		public virtual bool Update(GenerationRequestRow value)
		{
			string sqlStr = "UPDATE [dbo].[GenerationRequest] SET " +
				"[date_requested]=" + _db.CreateSqlParameterName("Date_requested") + ", " +
				"[date_to_process]=" + _db.CreateSqlParameterName("Date_to_process") + ", " +
				"[number_of_batches]=" + _db.CreateSqlParameterName("Number_of_batches") + ", " +
				"[batch_size]=" + _db.CreateSqlParameterName("Batch_size") + ", " +
				"[lot_id]=" + _db.CreateSqlParameterName("Lot_id") +
				" WHERE " +
				"[request_id]=" + _db.CreateSqlParameterName("Request_id");
			IDbCommand cmd = _db.CreateCommand(sqlStr);
			AddParameter(cmd, "Date_requested", value.Date_requested);
			AddParameter(cmd, "Date_to_process", value.Date_to_process);
			AddParameter(cmd, "Number_of_batches", value.Number_of_batches);
			AddParameter(cmd, "Batch_size", value.Batch_size);
			AddParameter(cmd, "Lot_id", value.Lot_id);
			AddParameter(cmd, "Request_id", value.Request_id);
			return 0 != cmd.ExecuteNonQuery();
		}

		/// <summary>
		/// Updates the <c>GenerationRequest</c> table and calls the <c>AcceptChanges</c> method
		/// on the changed DataRow objects.
		/// </summary>
		/// <param name="table">The <see cref="System.Data.DataTable"/> used to update the data source.</param>
		public void Update(DataTable table)
		{
			Update(table, true);
		}

		/// <summary>
		/// Updates the <c>GenerationRequest</c> table. Pass <c>false</c> as the <c>acceptChanges</c> 
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
							DeleteByPrimaryKey((int)row["Request_id"]);
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
		/// Deletes the specified object from the <c>GenerationRequest</c> table.
		/// </summary>
		/// <param name="value">The <see cref="GenerationRequestRow"/> object to delete.</param>
		/// <returns>true if the record was deleted; otherwise, false.</returns>
		public bool Delete(GenerationRequestRow value)
		{
			return DeleteByPrimaryKey(value.Request_id);
		}

		/// <summary>
		/// Deletes a record from the <c>GenerationRequest</c> table using
		/// the specified primary key.
		/// </summary>
		/// <param name="request_id">The <c>request_id</c> column value.</param>
		/// <returns>true if the record was deleted; otherwise, false.</returns>
		public virtual bool DeleteByPrimaryKey(int request_id)
		{
			string whereSql = "[request_id]=" + _db.CreateSqlParameterName("Request_id");
			IDbCommand cmd = CreateDeleteCommand(whereSql);
			AddParameter(cmd, "Request_id", request_id);
			return 0 < cmd.ExecuteNonQuery();
		}

		/// <summary>
		/// Deletes records from the <c>GenerationRequest</c> table using the 
		/// <c>R_289</c> foreign key.
		/// </summary>
		/// <param name="lot_id">The <c>lot_id</c> column value.</param>
		/// <returns>The number of records deleted from the table.</returns>
		public int DeleteByLot_id(int lot_id)
		{
			return CreateDeleteByLot_idCommand(lot_id).ExecuteNonQuery();
		}

		/// <summary>
		/// Creates an <see cref="System.Data.IDbCommand"/> object that can be used to
		/// delete records using the <c>R_289</c> foreign key.
		/// </summary>
		/// <param name="lot_id">The <c>lot_id</c> column value.</param>
		/// <returns>A reference to the <see cref="System.Data.IDbCommand"/> object.</returns>
		protected virtual IDbCommand CreateDeleteByLot_idCommand(int lot_id)
		{
			string whereSql = "";
			whereSql += "[lot_id]=" + _db.CreateSqlParameterName("Lot_id");

			IDbCommand cmd = CreateDeleteCommand(whereSql);
			AddParameter(cmd, "Lot_id", lot_id);
			return cmd;
		}

		/// <summary>
		/// Deletes <c>GenerationRequest</c> records that match the specified criteria.
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
		/// to delete <c>GenerationRequest</c> records that match the specified criteria.
		/// </summary>
		/// <param name="whereSql">The SQL search condition. 
		/// For example: <c>"FirstName='Smith' AND Zip=75038"</c>.</param>
		/// <returns>A reference to the <see cref="System.Data.IDbCommand"/> object.</returns>
		protected virtual IDbCommand CreateDeleteCommand(string whereSql)
		{
			string sql = "DELETE FROM [dbo].[GenerationRequest]";
			if(null != whereSql && 0 < whereSql.Length)
				sql += " WHERE " + whereSql;
			return _db.CreateCommand(sql);
		}

		/// <summary>
		/// Deletes all records from the <c>GenerationRequest</c> table.
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
		/// <returns>An array of <see cref="GenerationRequestRow"/> objects.</returns>
		protected GenerationRequestRow[] MapRecords(IDbCommand command)
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
		/// <returns>An array of <see cref="GenerationRequestRow"/> objects.</returns>
		protected GenerationRequestRow[] MapRecords(IDataReader reader)
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
		/// <returns>An array of <see cref="GenerationRequestRow"/> objects.</returns>
		protected virtual GenerationRequestRow[] MapRecords(IDataReader reader, 
										int startIndex, int length, ref int totalRecordCount)
		{
			if(0 > startIndex)
				throw new ArgumentOutOfRangeException("startIndex", startIndex, "StartIndex cannot be less than zero.");
			if(0 > length)
				throw new ArgumentOutOfRangeException("length", length, "Length cannot be less than zero.");

			int request_idColumnIndex = reader.GetOrdinal("request_id");
			int date_requestedColumnIndex = reader.GetOrdinal("date_requested");
			int date_to_processColumnIndex = reader.GetOrdinal("date_to_process");
			int date_completedColumnIndex = reader.GetOrdinal("date_completed");
			int number_of_batchesColumnIndex = reader.GetOrdinal("number_of_batches");
			int batch_sizeColumnIndex = reader.GetOrdinal("batch_size");
			int lot_idColumnIndex = reader.GetOrdinal("lot_id");

			System.Collections.ArrayList recordList = new System.Collections.ArrayList();
			int ri = -startIndex;
			while(reader.Read())
			{
				ri++;
				if(ri > 0 && ri <= length)
				{
					GenerationRequestRow record = new GenerationRequestRow();
					recordList.Add(record);

					record.Request_id = Convert.ToInt32(reader.GetValue(request_idColumnIndex));
					record.Date_requested = Convert.ToDateTime(reader.GetValue(date_requestedColumnIndex));
					record.Date_to_process = Convert.ToDateTime(reader.GetValue(date_to_processColumnIndex));
					if(!reader.IsDBNull(date_completedColumnIndex))
						record.Date_completed = Convert.ToDateTime(reader.GetValue(date_completedColumnIndex));
					record.Number_of_batches = Convert.ToInt32(reader.GetValue(number_of_batchesColumnIndex));
					record.Batch_size = Convert.ToInt32(reader.GetValue(batch_sizeColumnIndex));
					record.Lot_id = Convert.ToInt32(reader.GetValue(lot_idColumnIndex));

					if(ri == length && 0 != totalRecordCount)
						break;
				}
			}

			totalRecordCount = 0 == totalRecordCount ? ri + startIndex : -1;
			return (GenerationRequestRow[])(recordList.ToArray(typeof(GenerationRequestRow)));
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
		/// Converts <see cref="System.Data.DataRow"/> to <see cref="GenerationRequestRow"/>.
		/// </summary>
		/// <param name="row">The <see cref="System.Data.DataRow"/> object to be mapped.</param>
		/// <returns>A reference to the <see cref="GenerationRequestRow"/> object.</returns>
		protected virtual GenerationRequestRow MapRow(DataRow row)
		{
			GenerationRequestRow mappedObject = new GenerationRequestRow();
			DataTable dataTable = row.Table;
			DataColumn dataColumn;
			// Column "Request_id"
			dataColumn = dataTable.Columns["Request_id"];
			if(!row.IsNull(dataColumn))
				mappedObject.Request_id = (int)row[dataColumn];
			// Column "Date_requested"
			dataColumn = dataTable.Columns["Date_requested"];
			if(!row.IsNull(dataColumn))
				mappedObject.Date_requested = (System.DateTime)row[dataColumn];
			// Column "Date_to_process"
			dataColumn = dataTable.Columns["Date_to_process"];
			if(!row.IsNull(dataColumn))
				mappedObject.Date_to_process = (System.DateTime)row[dataColumn];
			// Column "Date_completed"
			dataColumn = dataTable.Columns["Date_completed"];
			if(!row.IsNull(dataColumn))
				mappedObject.Date_completed = (System.DateTime)row[dataColumn];
			// Column "Number_of_batches"
			dataColumn = dataTable.Columns["Number_of_batches"];
			if(!row.IsNull(dataColumn))
				mappedObject.Number_of_batches = (int)row[dataColumn];
			// Column "Batch_size"
			dataColumn = dataTable.Columns["Batch_size"];
			if(!row.IsNull(dataColumn))
				mappedObject.Batch_size = (int)row[dataColumn];
			// Column "Lot_id"
			dataColumn = dataTable.Columns["Lot_id"];
			if(!row.IsNull(dataColumn))
				mappedObject.Lot_id = (int)row[dataColumn];
			return mappedObject;
		}

		/// <summary>
		/// Creates a <see cref="System.Data.DataTable"/> object for 
		/// the <c>GenerationRequest</c> table.
		/// </summary>
		/// <returns>A reference to the <see cref="System.Data.DataTable"/> object.</returns>
		protected virtual DataTable CreateDataTable()
		{
			DataTable dataTable = new DataTable();
			dataTable.TableName = "GenerationRequest";
			DataColumn dataColumn;
			dataColumn = dataTable.Columns.Add("Request_id", typeof(int));
			dataColumn.Caption = "request_id";
			dataColumn.AllowDBNull = false;
			dataColumn = dataTable.Columns.Add("Date_requested", typeof(System.DateTime));
			dataColumn.Caption = "date_requested";
			dataColumn.AllowDBNull = false;
			dataColumn = dataTable.Columns.Add("Date_to_process", typeof(System.DateTime));
			dataColumn.Caption = "date_to_process";
			dataColumn.AllowDBNull = false;
			dataColumn = dataTable.Columns.Add("Date_completed", typeof(System.DateTime));
			dataColumn.Caption = "date_completed";
			dataColumn.ReadOnly = true;
			dataColumn = dataTable.Columns.Add("Number_of_batches", typeof(int));
			dataColumn.Caption = "number_of_batches";
			dataColumn.AllowDBNull = false;
			dataColumn = dataTable.Columns.Add("Batch_size", typeof(int));
			dataColumn.Caption = "batch_size";
			dataColumn.AllowDBNull = false;
			dataColumn = dataTable.Columns.Add("Lot_id", typeof(int));
			dataColumn.Caption = "lot_id";
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
				case "Request_id":
					parameter = _db.AddParameter(cmd, paramName, DbType.Int32, value);
					break;

				case "Date_requested":
					parameter = _db.AddParameter(cmd, paramName, DbType.DateTime, value);
					break;

				case "Date_to_process":
					parameter = _db.AddParameter(cmd, paramName, DbType.DateTime, value);
					break;

				case "Date_completed":
					parameter = _db.AddParameter(cmd, paramName, DbType.DateTime, value);
					break;

				case "Number_of_batches":
					parameter = _db.AddParameter(cmd, paramName, DbType.Int32, value);
					break;

				case "Batch_size":
					parameter = _db.AddParameter(cmd, paramName, DbType.Int32, value);
					break;

				case "Lot_id":
					parameter = _db.AddParameter(cmd, paramName, DbType.Int32, value);
					break;

				default:
					throw new ArgumentException("Unknown parameter name (" + paramName + ").");
			}
			return parameter;
		}

	} // End of GenerationRequestCollection_Base class
}  // End of namespace

// <fileinfo name="Base\CallCenterCabinaCollection_Base.cs">
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
	/// The base class for <see cref="CallCenterCabinaCollection"/>. Provides methods 
	/// for common database table operations. 
	/// </summary>
	/// <remarks>
	/// Do not change this source code. Update the <see cref="CallCenterCabinaCollection"/>
	/// class if you need to add or change some functionality.
	/// </remarks>
	public abstract class CallCenterCabinaCollection_Base
	{
		// Constants
		public const string Service_idColumnName = "service_id";
		public const string Serial_numberColumnName = "serial_number";
		public const string Cabina_labelColumnName = "cabina_label";
		public const string StatusColumnName = "status";
		public const string Date_first_usedColumnName = "date_first_used";
		public const string Date_last_usedColumnName = "date_last_used";
		public const string Retail_acct_idColumnName = "retail_acct_id";

		// Instance fields
		private Rbr_Db _db;

		/// <summary>
		/// Initializes a new instance of the <see cref="CallCenterCabinaCollection_Base"/> 
		/// class with the specified <see cref="Rbr_Db"/>.
		/// </summary>
		/// <param name="db">The <see cref="Rbr_Db"/> object.</param>
		public CallCenterCabinaCollection_Base(Rbr_Db db)
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
		/// Gets an array of all records from the <c>CallCenterCabina</c> table.
		/// </summary>
		/// <returns>An array of <see cref="CallCenterCabinaRow"/> objects.</returns>
		public virtual CallCenterCabinaRow[] GetAll()
		{
			return MapRecords(CreateGetAllCommand());
		}

		/// <summary>
		/// Gets a <see cref="System.Data.DataTable"/> object that 
		/// includes all records from the <c>CallCenterCabina</c> table.
		/// </summary>
		/// <returns>A reference to the <see cref="System.Data.DataTable"/> object.</returns>
		public virtual DataTable GetAllAsDataTable()
		{
			return MapRecordsToDataTable(CreateGetAllCommand());
		}

		/// <summary>
		/// Creates and returns an <see cref="System.Data.IDbCommand"/> object that is used
		/// to retrieve all records from the <c>CallCenterCabina</c> table.
		/// </summary>
		/// <returns>A reference to the <see cref="System.Data.IDbCommand"/> object.</returns>
		protected virtual IDbCommand CreateGetAllCommand()
		{
			return CreateGetCommand(null, null);
		}

		/// <summary>
		/// Gets the first <see cref="CallCenterCabinaRow"/> objects that 
		/// match the search condition.
		/// </summary>
		/// <param name="whereSql">The SQL search condition. For example: 
		/// <c>"FirstName='Smith' AND Zip=75038"</c>.</param>
		/// <returns>An instance of <see cref="CallCenterCabinaRow"/> or null reference 
		/// (Nothing in Visual Basic) if the object was not found.</returns>
		public CallCenterCabinaRow GetRow(string whereSql)
		{
			int totalRecordCount = -1;
			CallCenterCabinaRow[] rows = GetAsArray(whereSql, null, 0, 1, ref totalRecordCount);
			return 0 == rows.Length ? null : rows[0];
		}

		/// <summary>
		/// Gets an array of <see cref="CallCenterCabinaRow"/> objects that 
		/// match the search condition, in the the specified sort order.
		/// </summary>
		/// <param name="whereSql">The SQL search condition. For example: 
		/// <c>"FirstName='Smith' AND Zip=75038"</c>.</param>
		/// <param name="orderBySql">The column name(s) followed by "ASC" (ascending) or "DESC" (descending).
		/// Columns are sorted in ascending order by default. For example: <c>"LastName ASC, FirstName ASC"</c>.</param>
		/// <returns>An array of <see cref="CallCenterCabinaRow"/> objects.</returns>
		public CallCenterCabinaRow[] GetAsArray(string whereSql, string orderBySql)
		{
			int totalRecordCount = -1;
			return GetAsArray(whereSql, orderBySql, 0, int.MaxValue, ref totalRecordCount);
		}

		/// <summary>
		/// Gets an array of <see cref="CallCenterCabinaRow"/> objects that 
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
		/// <returns>An array of <see cref="CallCenterCabinaRow"/> objects.</returns>
		public virtual CallCenterCabinaRow[] GetAsArray(string whereSql, string orderBySql,
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
			string sql = "SELECT * FROM [dbo].[CallCenterCabina]";
			if(null != whereSql && 0 < whereSql.Length)
				sql += " WHERE " + whereSql;
			if(null != orderBySql && 0 < orderBySql.Length)
				sql += " ORDER BY " + orderBySql;
			return _db.CreateCommand(sql);
		}

		/// <summary>
		/// Gets <see cref="CallCenterCabinaRow"/> by the primary key.
		/// </summary>
		/// <param name="service_id">The <c>service_id</c> column value.</param>
		/// <param name="serial_number">The <c>serial_number</c> column value.</param>
		/// <returns>An instance of <see cref="CallCenterCabinaRow"/> or null reference 
		/// (Nothing in Visual Basic) if the object was not found.</returns>
		public virtual CallCenterCabinaRow GetByPrimaryKey(short service_id, long serial_number)
		{
			string whereSql = "[service_id]=" + _db.CreateSqlParameterName("Service_id") + " AND " +
							  "[serial_number]=" + _db.CreateSqlParameterName("Serial_number");
			IDbCommand cmd = CreateGetCommand(whereSql, null);
			AddParameter(cmd, "Service_id", service_id);
			AddParameter(cmd, "Serial_number", serial_number);
			CallCenterCabinaRow[] tempArray = MapRecords(cmd);
			return 0 == tempArray.Length ? null : tempArray[0];
		}

		/// <summary>
		/// Gets an array of <see cref="CallCenterCabinaRow"/> objects 
		/// by the <c>R_316</c> foreign key.
		/// </summary>
		/// <param name="retail_acct_id">The <c>retail_acct_id</c> column value.</param>
		/// <returns>An array of <see cref="CallCenterCabinaRow"/> objects.</returns>
		public virtual CallCenterCabinaRow[] GetByRetail_acct_id(int retail_acct_id)
		{
			return MapRecords(CreateGetByRetail_acct_idCommand(retail_acct_id));
		}

		/// <summary>
		/// Gets a <see cref="System.Data.DataTable"/> object 
		/// by the <c>R_316</c> foreign key.
		/// </summary>
		/// <param name="retail_acct_id">The <c>retail_acct_id</c> column value.</param>
		/// <returns>A reference to the <see cref="System.Data.DataTable"/> object.</returns>
		public virtual DataTable GetByRetail_acct_idAsDataTable(int retail_acct_id)
		{
			return MapRecordsToDataTable(CreateGetByRetail_acct_idCommand(retail_acct_id));
		}

		/// <summary>
		/// Creates an <see cref="System.Data.IDbCommand"/> object that can be used to 
		/// return records by the <c>R_316</c> foreign key.
		/// </summary>
		/// <param name="retail_acct_id">The <c>retail_acct_id</c> column value.</param>
		/// <returns>A reference to the <see cref="System.Data.IDbCommand"/> object.</returns>
		protected virtual IDbCommand CreateGetByRetail_acct_idCommand(int retail_acct_id)
		{
			string whereSql = "";
			whereSql += "[retail_acct_id]=" + _db.CreateSqlParameterName("Retail_acct_id");

			IDbCommand cmd = CreateGetCommand(whereSql, null);
			AddParameter(cmd, "Retail_acct_id", retail_acct_id);
			return cmd;
		}

		/// <summary>
		/// Gets an array of <see cref="CallCenterCabinaRow"/> objects 
		/// by the <c>R_363</c> foreign key.
		/// </summary>
		/// <param name="service_id">The <c>service_id</c> column value.</param>
		/// <returns>An array of <see cref="CallCenterCabinaRow"/> objects.</returns>
		public virtual CallCenterCabinaRow[] GetByService_id(short service_id)
		{
			return MapRecords(CreateGetByService_idCommand(service_id));
		}

		/// <summary>
		/// Gets a <see cref="System.Data.DataTable"/> object 
		/// by the <c>R_363</c> foreign key.
		/// </summary>
		/// <param name="service_id">The <c>service_id</c> column value.</param>
		/// <returns>A reference to the <see cref="System.Data.DataTable"/> object.</returns>
		public virtual DataTable GetByService_idAsDataTable(short service_id)
		{
			return MapRecordsToDataTable(CreateGetByService_idCommand(service_id));
		}

		/// <summary>
		/// Creates an <see cref="System.Data.IDbCommand"/> object that can be used to 
		/// return records by the <c>R_363</c> foreign key.
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
		/// Adds a new record into the <c>CallCenterCabina</c> table.
		/// </summary>
		/// <param name="value">The <see cref="CallCenterCabinaRow"/> object to be inserted.</param>
		public virtual void Insert(CallCenterCabinaRow value)
		{
			string sqlStr = "INSERT INTO [dbo].[CallCenterCabina] (" +
				"[service_id], " +
				"[serial_number], " +
				"[cabina_label], " +
				"[status], " +
				"[date_first_used], " +
				"[date_last_used], " +
				"[retail_acct_id]" +
				") VALUES (" +
				_db.CreateSqlParameterName("Service_id") + ", " +
				_db.CreateSqlParameterName("Serial_number") + ", " +
				_db.CreateSqlParameterName("Cabina_label") + ", " +
				_db.CreateSqlParameterName("Status") + ", " +
				_db.CreateSqlParameterName("Date_first_used") + ", " +
				_db.CreateSqlParameterName("Date_last_used") + ", " +
				_db.CreateSqlParameterName("Retail_acct_id") + ")";
			IDbCommand cmd = _db.CreateCommand(sqlStr);
			AddParameter(cmd, "Service_id", value.Service_id);
			AddParameter(cmd, "Serial_number", value.Serial_number);
			AddParameter(cmd, "Cabina_label", value.Cabina_label);
			AddParameter(cmd, "Status", value.Status);
			AddParameter(cmd, "Date_first_used",
				value.IsDate_first_usedNull ? DBNull.Value : (object)value.Date_first_used);
			AddParameter(cmd, "Date_last_used",
				value.IsDate_last_usedNull ? DBNull.Value : (object)value.Date_last_used);
			AddParameter(cmd, "Retail_acct_id", value.Retail_acct_id);
			cmd.ExecuteNonQuery();
		}

		/// <summary>
		/// Updates a record in the <c>CallCenterCabina</c> table.
		/// </summary>
		/// <param name="value">The <see cref="CallCenterCabinaRow"/>
		/// object used to update the table record.</param>
		/// <returns>true if the record was updated; otherwise, false.</returns>
		public virtual bool Update(CallCenterCabinaRow value)
		{
			string sqlStr = "UPDATE [dbo].[CallCenterCabina] SET " +
				"[cabina_label]=" + _db.CreateSqlParameterName("Cabina_label") + ", " +
				"[status]=" + _db.CreateSqlParameterName("Status") + ", " +
				"[date_first_used]=" + _db.CreateSqlParameterName("Date_first_used") + ", " +
				"[date_last_used]=" + _db.CreateSqlParameterName("Date_last_used") + ", " +
				"[retail_acct_id]=" + _db.CreateSqlParameterName("Retail_acct_id") +
				" WHERE " +
				"[service_id]=" + _db.CreateSqlParameterName("Service_id") + " AND " +
				"[serial_number]=" + _db.CreateSqlParameterName("Serial_number");
			IDbCommand cmd = _db.CreateCommand(sqlStr);
			AddParameter(cmd, "Cabina_label", value.Cabina_label);
			AddParameter(cmd, "Status", value.Status);
			AddParameter(cmd, "Date_first_used",
				value.IsDate_first_usedNull ? DBNull.Value : (object)value.Date_first_used);
			AddParameter(cmd, "Date_last_used",
				value.IsDate_last_usedNull ? DBNull.Value : (object)value.Date_last_used);
			AddParameter(cmd, "Retail_acct_id", value.Retail_acct_id);
			AddParameter(cmd, "Service_id", value.Service_id);
			AddParameter(cmd, "Serial_number", value.Serial_number);
			return 0 != cmd.ExecuteNonQuery();
		}

		/// <summary>
		/// Updates the <c>CallCenterCabina</c> table and calls the <c>AcceptChanges</c> method
		/// on the changed DataRow objects.
		/// </summary>
		/// <param name="table">The <see cref="System.Data.DataTable"/> used to update the data source.</param>
		public void Update(DataTable table)
		{
			Update(table, true);
		}

		/// <summary>
		/// Updates the <c>CallCenterCabina</c> table. Pass <c>false</c> as the <c>acceptChanges</c> 
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
							DeleteByPrimaryKey((short)row["Service_id"], (long)row["Serial_number"]);
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
		/// Deletes the specified object from the <c>CallCenterCabina</c> table.
		/// </summary>
		/// <param name="value">The <see cref="CallCenterCabinaRow"/> object to delete.</param>
		/// <returns>true if the record was deleted; otherwise, false.</returns>
		public bool Delete(CallCenterCabinaRow value)
		{
			return DeleteByPrimaryKey(value.Service_id, value.Serial_number);
		}

		/// <summary>
		/// Deletes a record from the <c>CallCenterCabina</c> table using
		/// the specified primary key.
		/// </summary>
		/// <param name="service_id">The <c>service_id</c> column value.</param>
		/// <param name="serial_number">The <c>serial_number</c> column value.</param>
		/// <returns>true if the record was deleted; otherwise, false.</returns>
		public virtual bool DeleteByPrimaryKey(short service_id, long serial_number)
		{
			string whereSql = "[service_id]=" + _db.CreateSqlParameterName("Service_id") + " AND " +
							  "[serial_number]=" + _db.CreateSqlParameterName("Serial_number");
			IDbCommand cmd = CreateDeleteCommand(whereSql);
			AddParameter(cmd, "Service_id", service_id);
			AddParameter(cmd, "Serial_number", serial_number);
			return 0 < cmd.ExecuteNonQuery();
		}

		/// <summary>
		/// Deletes records from the <c>CallCenterCabina</c> table using the 
		/// <c>R_316</c> foreign key.
		/// </summary>
		/// <param name="retail_acct_id">The <c>retail_acct_id</c> column value.</param>
		/// <returns>The number of records deleted from the table.</returns>
		public int DeleteByRetail_acct_id(int retail_acct_id)
		{
			return CreateDeleteByRetail_acct_idCommand(retail_acct_id).ExecuteNonQuery();
		}

		/// <summary>
		/// Creates an <see cref="System.Data.IDbCommand"/> object that can be used to
		/// delete records using the <c>R_316</c> foreign key.
		/// </summary>
		/// <param name="retail_acct_id">The <c>retail_acct_id</c> column value.</param>
		/// <returns>A reference to the <see cref="System.Data.IDbCommand"/> object.</returns>
		protected virtual IDbCommand CreateDeleteByRetail_acct_idCommand(int retail_acct_id)
		{
			string whereSql = "";
			whereSql += "[retail_acct_id]=" + _db.CreateSqlParameterName("Retail_acct_id");

			IDbCommand cmd = CreateDeleteCommand(whereSql);
			AddParameter(cmd, "Retail_acct_id", retail_acct_id);
			return cmd;
		}

		/// <summary>
		/// Deletes records from the <c>CallCenterCabina</c> table using the 
		/// <c>R_363</c> foreign key.
		/// </summary>
		/// <param name="service_id">The <c>service_id</c> column value.</param>
		/// <returns>The number of records deleted from the table.</returns>
		public int DeleteByService_id(short service_id)
		{
			return CreateDeleteByService_idCommand(service_id).ExecuteNonQuery();
		}

		/// <summary>
		/// Creates an <see cref="System.Data.IDbCommand"/> object that can be used to
		/// delete records using the <c>R_363</c> foreign key.
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
		/// Deletes <c>CallCenterCabina</c> records that match the specified criteria.
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
		/// to delete <c>CallCenterCabina</c> records that match the specified criteria.
		/// </summary>
		/// <param name="whereSql">The SQL search condition. 
		/// For example: <c>"FirstName='Smith' AND Zip=75038"</c>.</param>
		/// <returns>A reference to the <see cref="System.Data.IDbCommand"/> object.</returns>
		protected virtual IDbCommand CreateDeleteCommand(string whereSql)
		{
			string sql = "DELETE FROM [dbo].[CallCenterCabina]";
			if(null != whereSql && 0 < whereSql.Length)
				sql += " WHERE " + whereSql;
			return _db.CreateCommand(sql);
		}

		/// <summary>
		/// Deletes all records from the <c>CallCenterCabina</c> table.
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
		/// <returns>An array of <see cref="CallCenterCabinaRow"/> objects.</returns>
		protected CallCenterCabinaRow[] MapRecords(IDbCommand command)
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
		/// <returns>An array of <see cref="CallCenterCabinaRow"/> objects.</returns>
		protected CallCenterCabinaRow[] MapRecords(IDataReader reader)
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
		/// <returns>An array of <see cref="CallCenterCabinaRow"/> objects.</returns>
		protected virtual CallCenterCabinaRow[] MapRecords(IDataReader reader, 
										int startIndex, int length, ref int totalRecordCount)
		{
			if(0 > startIndex)
				throw new ArgumentOutOfRangeException("startIndex", startIndex, "StartIndex cannot be less than zero.");
			if(0 > length)
				throw new ArgumentOutOfRangeException("length", length, "Length cannot be less than zero.");

			int service_idColumnIndex = reader.GetOrdinal("service_id");
			int serial_numberColumnIndex = reader.GetOrdinal("serial_number");
			int cabina_labelColumnIndex = reader.GetOrdinal("cabina_label");
			int statusColumnIndex = reader.GetOrdinal("status");
			int date_first_usedColumnIndex = reader.GetOrdinal("date_first_used");
			int date_last_usedColumnIndex = reader.GetOrdinal("date_last_used");
			int retail_acct_idColumnIndex = reader.GetOrdinal("retail_acct_id");

			System.Collections.ArrayList recordList = new System.Collections.ArrayList();
			int ri = -startIndex;
			while(reader.Read())
			{
				ri++;
				if(ri > 0 && ri <= length)
				{
					CallCenterCabinaRow record = new CallCenterCabinaRow();
					recordList.Add(record);

					record.Service_id = Convert.ToInt16(reader.GetValue(service_idColumnIndex));
					record.Serial_number = Convert.ToInt64(reader.GetValue(serial_numberColumnIndex));
					record.Cabina_label = Convert.ToString(reader.GetValue(cabina_labelColumnIndex));
					record.Status = Convert.ToByte(reader.GetValue(statusColumnIndex));
					if(!reader.IsDBNull(date_first_usedColumnIndex))
						record.Date_first_used = Convert.ToDateTime(reader.GetValue(date_first_usedColumnIndex));
					if(!reader.IsDBNull(date_last_usedColumnIndex))
						record.Date_last_used = Convert.ToDateTime(reader.GetValue(date_last_usedColumnIndex));
					record.Retail_acct_id = Convert.ToInt32(reader.GetValue(retail_acct_idColumnIndex));

					if(ri == length && 0 != totalRecordCount)
						break;
				}
			}

			totalRecordCount = 0 == totalRecordCount ? ri + startIndex : -1;
			return (CallCenterCabinaRow[])(recordList.ToArray(typeof(CallCenterCabinaRow)));
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
		/// Converts <see cref="System.Data.DataRow"/> to <see cref="CallCenterCabinaRow"/>.
		/// </summary>
		/// <param name="row">The <see cref="System.Data.DataRow"/> object to be mapped.</param>
		/// <returns>A reference to the <see cref="CallCenterCabinaRow"/> object.</returns>
		protected virtual CallCenterCabinaRow MapRow(DataRow row)
		{
			CallCenterCabinaRow mappedObject = new CallCenterCabinaRow();
			DataTable dataTable = row.Table;
			DataColumn dataColumn;
			// Column "Service_id"
			dataColumn = dataTable.Columns["Service_id"];
			if(!row.IsNull(dataColumn))
				mappedObject.Service_id = (short)row[dataColumn];
			// Column "Serial_number"
			dataColumn = dataTable.Columns["Serial_number"];
			if(!row.IsNull(dataColumn))
				mappedObject.Serial_number = (long)row[dataColumn];
			// Column "Cabina_label"
			dataColumn = dataTable.Columns["Cabina_label"];
			if(!row.IsNull(dataColumn))
				mappedObject.Cabina_label = (string)row[dataColumn];
			// Column "Status"
			dataColumn = dataTable.Columns["Status"];
			if(!row.IsNull(dataColumn))
				mappedObject.Status = (byte)row[dataColumn];
			// Column "Date_first_used"
			dataColumn = dataTable.Columns["Date_first_used"];
			if(!row.IsNull(dataColumn))
				mappedObject.Date_first_used = (System.DateTime)row[dataColumn];
			// Column "Date_last_used"
			dataColumn = dataTable.Columns["Date_last_used"];
			if(!row.IsNull(dataColumn))
				mappedObject.Date_last_used = (System.DateTime)row[dataColumn];
			// Column "Retail_acct_id"
			dataColumn = dataTable.Columns["Retail_acct_id"];
			if(!row.IsNull(dataColumn))
				mappedObject.Retail_acct_id = (int)row[dataColumn];
			return mappedObject;
		}

		/// <summary>
		/// Creates a <see cref="System.Data.DataTable"/> object for 
		/// the <c>CallCenterCabina</c> table.
		/// </summary>
		/// <returns>A reference to the <see cref="System.Data.DataTable"/> object.</returns>
		protected virtual DataTable CreateDataTable()
		{
			DataTable dataTable = new DataTable();
			dataTable.TableName = "CallCenterCabina";
			DataColumn dataColumn;
			dataColumn = dataTable.Columns.Add("Service_id", typeof(short));
			dataColumn.Caption = "service_id";
			dataColumn.AllowDBNull = false;
			dataColumn = dataTable.Columns.Add("Serial_number", typeof(long));
			dataColumn.Caption = "serial_number";
			dataColumn.AllowDBNull = false;
			dataColumn = dataTable.Columns.Add("Cabina_label", typeof(string));
			dataColumn.Caption = "cabina_label";
			dataColumn.MaxLength = 16;
			dataColumn.AllowDBNull = false;
			dataColumn = dataTable.Columns.Add("Status", typeof(byte));
			dataColumn.Caption = "status";
			dataColumn.AllowDBNull = false;
			dataColumn = dataTable.Columns.Add("Date_first_used", typeof(System.DateTime));
			dataColumn.Caption = "date_first_used";
			dataColumn = dataTable.Columns.Add("Date_last_used", typeof(System.DateTime));
			dataColumn.Caption = "date_last_used";
			dataColumn = dataTable.Columns.Add("Retail_acct_id", typeof(int));
			dataColumn.Caption = "retail_acct_id";
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

				case "Serial_number":
					parameter = _db.AddParameter(cmd, paramName, DbType.Int64, value);
					break;

				case "Cabina_label":
					parameter = _db.AddParameter(cmd, paramName, DbType.AnsiString, value);
					break;

				case "Status":
					parameter = _db.AddParameter(cmd, paramName, DbType.Byte, value);
					break;

				case "Date_first_used":
					parameter = _db.AddParameter(cmd, paramName, DbType.DateTime, value);
					break;

				case "Date_last_used":
					parameter = _db.AddParameter(cmd, paramName, DbType.DateTime, value);
					break;

				case "Retail_acct_id":
					parameter = _db.AddParameter(cmd, paramName, DbType.Int32, value);
					break;

				default:
					throw new ArgumentException("Unknown parameter name (" + paramName + ").");
			}
			return parameter;
		}

	} // End of CallCenterCabinaCollection_Base class
}  // End of namespace

// <fileinfo name="Base\PhoneCardCollection_Base.cs">
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
	/// The base class for <see cref="PhoneCardCollection"/>. Provides methods 
	/// for common database table operations. 
	/// </summary>
	/// <remarks>
	/// Do not change this source code. Update the <see cref="PhoneCardCollection"/>
	/// class if you need to add or change some functionality.
	/// </remarks>
	public abstract class PhoneCardCollection_Base
	{
		// Constants
		public const string Service_idColumnName = "service_id";
		public const string PinColumnName = "pin";
		public const string Retail_acct_idColumnName = "retail_acct_id";
		public const string Serial_numberColumnName = "serial_number";
		public const string StatusColumnName = "status";
		public const string Inventory_statusColumnName = "inventory_status";
		public const string Date_loadedColumnName = "date_loaded";
		public const string Date_to_expireColumnName = "date_to_expire";
		public const string Date_activeColumnName = "date_active";
		public const string Date_first_usedColumnName = "date_first_used";
		public const string Date_last_usedColumnName = "date_last_used";
		public const string Date_deactivatedColumnName = "date_deactivated";
		public const string Date_archivedColumnName = "date_archived";

		// Instance fields
		private Rbr_Db _db;

		/// <summary>
		/// Initializes a new instance of the <see cref="PhoneCardCollection_Base"/> 
		/// class with the specified <see cref="Rbr_Db"/>.
		/// </summary>
		/// <param name="db">The <see cref="Rbr_Db"/> object.</param>
		public PhoneCardCollection_Base(Rbr_Db db)
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
		/// Gets an array of all records from the <c>PhoneCard</c> table.
		/// </summary>
		/// <returns>An array of <see cref="PhoneCardRow"/> objects.</returns>
		public virtual PhoneCardRow[] GetAll()
		{
			return MapRecords(CreateGetAllCommand());
		}

		/// <summary>
		/// Gets a <see cref="System.Data.DataTable"/> object that 
		/// includes all records from the <c>PhoneCard</c> table.
		/// </summary>
		/// <returns>A reference to the <see cref="System.Data.DataTable"/> object.</returns>
		public virtual DataTable GetAllAsDataTable()
		{
			return MapRecordsToDataTable(CreateGetAllCommand());
		}

		/// <summary>
		/// Creates and returns an <see cref="System.Data.IDbCommand"/> object that is used
		/// to retrieve all records from the <c>PhoneCard</c> table.
		/// </summary>
		/// <returns>A reference to the <see cref="System.Data.IDbCommand"/> object.</returns>
		protected virtual IDbCommand CreateGetAllCommand()
		{
			return CreateGetCommand(null, null);
		}

		/// <summary>
		/// Gets the first <see cref="PhoneCardRow"/> objects that 
		/// match the search condition.
		/// </summary>
		/// <param name="whereSql">The SQL search condition. For example: 
		/// <c>"FirstName='Smith' AND Zip=75038"</c>.</param>
		/// <returns>An instance of <see cref="PhoneCardRow"/> or null reference 
		/// (Nothing in Visual Basic) if the object was not found.</returns>
		public PhoneCardRow GetRow(string whereSql)
		{
			int totalRecordCount = -1;
			PhoneCardRow[] rows = GetAsArray(whereSql, null, 0, 1, ref totalRecordCount);
			return 0 == rows.Length ? null : rows[0];
		}

		/// <summary>
		/// Gets an array of <see cref="PhoneCardRow"/> objects that 
		/// match the search condition, in the the specified sort order.
		/// </summary>
		/// <param name="whereSql">The SQL search condition. For example: 
		/// <c>"FirstName='Smith' AND Zip=75038"</c>.</param>
		/// <param name="orderBySql">The column name(s) followed by "ASC" (ascending) or "DESC" (descending).
		/// Columns are sorted in ascending order by default. For example: <c>"LastName ASC, FirstName ASC"</c>.</param>
		/// <returns>An array of <see cref="PhoneCardRow"/> objects.</returns>
		public PhoneCardRow[] GetAsArray(string whereSql, string orderBySql)
		{
			int totalRecordCount = -1;
			return GetAsArray(whereSql, orderBySql, 0, int.MaxValue, ref totalRecordCount);
		}

		/// <summary>
		/// Gets an array of <see cref="PhoneCardRow"/> objects that 
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
		/// <returns>An array of <see cref="PhoneCardRow"/> objects.</returns>
		public virtual PhoneCardRow[] GetAsArray(string whereSql, string orderBySql,
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
			string sql = "SELECT * FROM [dbo].[PhoneCard]";
			if(null != whereSql && 0 < whereSql.Length)
				sql += " WHERE " + whereSql;
			if(null != orderBySql && 0 < orderBySql.Length)
				sql += " ORDER BY " + orderBySql;
			return _db.CreateCommand(sql);
		}

		/// <summary>
		/// Gets <see cref="PhoneCardRow"/> by the primary key.
		/// </summary>
		/// <param name="service_id">The <c>service_id</c> column value.</param>
		/// <param name="pin">The <c>pin</c> column value.</param>
		/// <returns>An instance of <see cref="PhoneCardRow"/> or null reference 
		/// (Nothing in Visual Basic) if the object was not found.</returns>
		public virtual PhoneCardRow GetByPrimaryKey(short service_id, long pin)
		{
			string whereSql = "[service_id]=" + _db.CreateSqlParameterName("Service_id") + " AND " +
							  "[pin]=" + _db.CreateSqlParameterName("Pin");
			IDbCommand cmd = CreateGetCommand(whereSql, null);
			AddParameter(cmd, "Service_id", service_id);
			AddParameter(cmd, "Pin", pin);
			PhoneCardRow[] tempArray = MapRecords(cmd);
			return 0 == tempArray.Length ? null : tempArray[0];
		}

		/// <summary>
		/// Gets an array of <see cref="PhoneCardRow"/> objects 
		/// by the <c>R_221</c> foreign key.
		/// </summary>
		/// <param name="retail_acct_id">The <c>retail_acct_id</c> column value.</param>
		/// <returns>An array of <see cref="PhoneCardRow"/> objects.</returns>
		public virtual PhoneCardRow[] GetByRetail_acct_id(int retail_acct_id)
		{
			return MapRecords(CreateGetByRetail_acct_idCommand(retail_acct_id));
		}

		/// <summary>
		/// Gets a <see cref="System.Data.DataTable"/> object 
		/// by the <c>R_221</c> foreign key.
		/// </summary>
		/// <param name="retail_acct_id">The <c>retail_acct_id</c> column value.</param>
		/// <returns>A reference to the <see cref="System.Data.DataTable"/> object.</returns>
		public virtual DataTable GetByRetail_acct_idAsDataTable(int retail_acct_id)
		{
			return MapRecordsToDataTable(CreateGetByRetail_acct_idCommand(retail_acct_id));
		}

		/// <summary>
		/// Creates an <see cref="System.Data.IDbCommand"/> object that can be used to 
		/// return records by the <c>R_221</c> foreign key.
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
		/// Gets an array of <see cref="PhoneCardRow"/> objects 
		/// by the <c>R_365</c> foreign key.
		/// </summary>
		/// <param name="service_id">The <c>service_id</c> column value.</param>
		/// <returns>An array of <see cref="PhoneCardRow"/> objects.</returns>
		public virtual PhoneCardRow[] GetByService_id(short service_id)
		{
			return MapRecords(CreateGetByService_idCommand(service_id));
		}

		/// <summary>
		/// Gets a <see cref="System.Data.DataTable"/> object 
		/// by the <c>R_365</c> foreign key.
		/// </summary>
		/// <param name="service_id">The <c>service_id</c> column value.</param>
		/// <returns>A reference to the <see cref="System.Data.DataTable"/> object.</returns>
		public virtual DataTable GetByService_idAsDataTable(short service_id)
		{
			return MapRecordsToDataTable(CreateGetByService_idCommand(service_id));
		}

		/// <summary>
		/// Creates an <see cref="System.Data.IDbCommand"/> object that can be used to 
		/// return records by the <c>R_365</c> foreign key.
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
		/// Adds a new record into the <c>PhoneCard</c> table.
		/// </summary>
		/// <param name="value">The <see cref="PhoneCardRow"/> object to be inserted.</param>
		public virtual void Insert(PhoneCardRow value)
		{
			string sqlStr = "INSERT INTO [dbo].[PhoneCard] (" +
				"[service_id], " +
				"[pin], " +
				"[retail_acct_id], " +
				"[serial_number], " +
				"[status], " +
				"[inventory_status], " +
				"[date_loaded], " +
				"[date_to_expire], " +
				"[date_active], " +
				"[date_deactivated], " +
				"[date_archived]" +
				") VALUES (" +
				_db.CreateSqlParameterName("Service_id") + ", " +
				_db.CreateSqlParameterName("Pin") + ", " +
				_db.CreateSqlParameterName("Retail_acct_id") + ", " +
				_db.CreateSqlParameterName("Serial_number") + ", " +
				_db.CreateSqlParameterName("Status") + ", " +
				_db.CreateSqlParameterName("Inventory_status") + ", " +
				_db.CreateSqlParameterName("Date_loaded") + ", " +
				_db.CreateSqlParameterName("Date_to_expire") + ", " +
				_db.CreateSqlParameterName("Date_active") + ", " +
				_db.CreateSqlParameterName("Date_deactivated") + ", " +
				_db.CreateSqlParameterName("Date_archived") + ")";
			IDbCommand cmd = _db.CreateCommand(sqlStr);
			AddParameter(cmd, "Service_id", value.Service_id);
			AddParameter(cmd, "Pin", value.Pin);
			AddParameter(cmd, "Retail_acct_id", value.Retail_acct_id);
			AddParameter(cmd, "Serial_number", value.Serial_number);
			AddParameter(cmd, "Status", value.Status);
			AddParameter(cmd, "Inventory_status", value.Inventory_status);
			AddParameter(cmd, "Date_loaded", value.Date_loaded);
			AddParameter(cmd, "Date_to_expire", value.Date_to_expire);
			AddParameter(cmd, "Date_active",
				value.IsDate_activeNull ? DBNull.Value : (object)value.Date_active);
			AddParameter(cmd, "Date_deactivated",
				value.IsDate_deactivatedNull ? DBNull.Value : (object)value.Date_deactivated);
			AddParameter(cmd, "Date_archived",
				value.IsDate_archivedNull ? DBNull.Value : (object)value.Date_archived);
			cmd.ExecuteNonQuery();
		}

		/// <summary>
		/// Updates a record in the <c>PhoneCard</c> table.
		/// </summary>
		/// <param name="value">The <see cref="PhoneCardRow"/>
		/// object used to update the table record.</param>
		/// <returns>true if the record was updated; otherwise, false.</returns>
		public virtual bool Update(PhoneCardRow value)
		{
			string sqlStr = "UPDATE [dbo].[PhoneCard] SET " +
				"[retail_acct_id]=" + _db.CreateSqlParameterName("Retail_acct_id") + ", " +
				"[serial_number]=" + _db.CreateSqlParameterName("Serial_number") + ", " +
				"[status]=" + _db.CreateSqlParameterName("Status") + ", " +
				"[inventory_status]=" + _db.CreateSqlParameterName("Inventory_status") + ", " +
				"[date_loaded]=" + _db.CreateSqlParameterName("Date_loaded") + ", " +
				"[date_to_expire]=" + _db.CreateSqlParameterName("Date_to_expire") + ", " +
				"[date_active]=" + _db.CreateSqlParameterName("Date_active") + ", " +
				"[date_deactivated]=" + _db.CreateSqlParameterName("Date_deactivated") + ", " +
				"[date_archived]=" + _db.CreateSqlParameterName("Date_archived") +
				" WHERE " +
				"[service_id]=" + _db.CreateSqlParameterName("Service_id") + " AND " +
				"[pin]=" + _db.CreateSqlParameterName("Pin");
			IDbCommand cmd = _db.CreateCommand(sqlStr);
			AddParameter(cmd, "Retail_acct_id", value.Retail_acct_id);
			AddParameter(cmd, "Serial_number", value.Serial_number);
			AddParameter(cmd, "Status", value.Status);
			AddParameter(cmd, "Inventory_status", value.Inventory_status);
			AddParameter(cmd, "Date_loaded", value.Date_loaded);
			AddParameter(cmd, "Date_to_expire", value.Date_to_expire);
			AddParameter(cmd, "Date_active",
				value.IsDate_activeNull ? DBNull.Value : (object)value.Date_active);
			AddParameter(cmd, "Date_deactivated",
				value.IsDate_deactivatedNull ? DBNull.Value : (object)value.Date_deactivated);
			AddParameter(cmd, "Date_archived",
				value.IsDate_archivedNull ? DBNull.Value : (object)value.Date_archived);
			AddParameter(cmd, "Service_id", value.Service_id);
			AddParameter(cmd, "Pin", value.Pin);
			return 0 != cmd.ExecuteNonQuery();
		}

		/// <summary>
		/// Updates the <c>PhoneCard</c> table and calls the <c>AcceptChanges</c> method
		/// on the changed DataRow objects.
		/// </summary>
		/// <param name="table">The <see cref="System.Data.DataTable"/> used to update the data source.</param>
		public void Update(DataTable table)
		{
			Update(table, true);
		}

		/// <summary>
		/// Updates the <c>PhoneCard</c> table. Pass <c>false</c> as the <c>acceptChanges</c> 
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
							DeleteByPrimaryKey((short)row["Service_id"], (long)row["Pin"]);
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
		/// Deletes the specified object from the <c>PhoneCard</c> table.
		/// </summary>
		/// <param name="value">The <see cref="PhoneCardRow"/> object to delete.</param>
		/// <returns>true if the record was deleted; otherwise, false.</returns>
		public bool Delete(PhoneCardRow value)
		{
			return DeleteByPrimaryKey(value.Service_id, value.Pin);
		}

		/// <summary>
		/// Deletes a record from the <c>PhoneCard</c> table using
		/// the specified primary key.
		/// </summary>
		/// <param name="service_id">The <c>service_id</c> column value.</param>
		/// <param name="pin">The <c>pin</c> column value.</param>
		/// <returns>true if the record was deleted; otherwise, false.</returns>
		public virtual bool DeleteByPrimaryKey(short service_id, long pin)
		{
			string whereSql = "[service_id]=" + _db.CreateSqlParameterName("Service_id") + " AND " +
							  "[pin]=" + _db.CreateSqlParameterName("Pin");
			IDbCommand cmd = CreateDeleteCommand(whereSql);
			AddParameter(cmd, "Service_id", service_id);
			AddParameter(cmd, "Pin", pin);
			return 0 < cmd.ExecuteNonQuery();
		}

		/// <summary>
		/// Deletes records from the <c>PhoneCard</c> table using the 
		/// <c>R_221</c> foreign key.
		/// </summary>
		/// <param name="retail_acct_id">The <c>retail_acct_id</c> column value.</param>
		/// <returns>The number of records deleted from the table.</returns>
		public int DeleteByRetail_acct_id(int retail_acct_id)
		{
			return CreateDeleteByRetail_acct_idCommand(retail_acct_id).ExecuteNonQuery();
		}

		/// <summary>
		/// Creates an <see cref="System.Data.IDbCommand"/> object that can be used to
		/// delete records using the <c>R_221</c> foreign key.
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
		/// Deletes records from the <c>PhoneCard</c> table using the 
		/// <c>R_365</c> foreign key.
		/// </summary>
		/// <param name="service_id">The <c>service_id</c> column value.</param>
		/// <returns>The number of records deleted from the table.</returns>
		public int DeleteByService_id(short service_id)
		{
			return CreateDeleteByService_idCommand(service_id).ExecuteNonQuery();
		}

		/// <summary>
		/// Creates an <see cref="System.Data.IDbCommand"/> object that can be used to
		/// delete records using the <c>R_365</c> foreign key.
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
		/// Deletes <c>PhoneCard</c> records that match the specified criteria.
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
		/// to delete <c>PhoneCard</c> records that match the specified criteria.
		/// </summary>
		/// <param name="whereSql">The SQL search condition. 
		/// For example: <c>"FirstName='Smith' AND Zip=75038"</c>.</param>
		/// <returns>A reference to the <see cref="System.Data.IDbCommand"/> object.</returns>
		protected virtual IDbCommand CreateDeleteCommand(string whereSql)
		{
			string sql = "DELETE FROM [dbo].[PhoneCard]";
			if(null != whereSql && 0 < whereSql.Length)
				sql += " WHERE " + whereSql;
			return _db.CreateCommand(sql);
		}

		/// <summary>
		/// Deletes all records from the <c>PhoneCard</c> table.
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
		/// <returns>An array of <see cref="PhoneCardRow"/> objects.</returns>
		protected PhoneCardRow[] MapRecords(IDbCommand command)
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
		/// <returns>An array of <see cref="PhoneCardRow"/> objects.</returns>
		protected PhoneCardRow[] MapRecords(IDataReader reader)
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
		/// <returns>An array of <see cref="PhoneCardRow"/> objects.</returns>
		protected virtual PhoneCardRow[] MapRecords(IDataReader reader, 
										int startIndex, int length, ref int totalRecordCount)
		{
			if(0 > startIndex)
				throw new ArgumentOutOfRangeException("startIndex", startIndex, "StartIndex cannot be less than zero.");
			if(0 > length)
				throw new ArgumentOutOfRangeException("length", length, "Length cannot be less than zero.");

			int service_idColumnIndex = reader.GetOrdinal("service_id");
			int pinColumnIndex = reader.GetOrdinal("pin");
			int retail_acct_idColumnIndex = reader.GetOrdinal("retail_acct_id");
			int serial_numberColumnIndex = reader.GetOrdinal("serial_number");
			int statusColumnIndex = reader.GetOrdinal("status");
			int inventory_statusColumnIndex = reader.GetOrdinal("inventory_status");
			int date_loadedColumnIndex = reader.GetOrdinal("date_loaded");
			int date_to_expireColumnIndex = reader.GetOrdinal("date_to_expire");
			int date_activeColumnIndex = reader.GetOrdinal("date_active");
			int date_first_usedColumnIndex = reader.GetOrdinal("date_first_used");
			int date_last_usedColumnIndex = reader.GetOrdinal("date_last_used");
			int date_deactivatedColumnIndex = reader.GetOrdinal("date_deactivated");
			int date_archivedColumnIndex = reader.GetOrdinal("date_archived");

			System.Collections.ArrayList recordList = new System.Collections.ArrayList();
			int ri = -startIndex;
			while(reader.Read())
			{
				ri++;
				if(ri > 0 && ri <= length)
				{
					PhoneCardRow record = new PhoneCardRow();
					recordList.Add(record);

					record.Service_id = Convert.ToInt16(reader.GetValue(service_idColumnIndex));
					record.Pin = Convert.ToInt64(reader.GetValue(pinColumnIndex));
					record.Retail_acct_id = Convert.ToInt32(reader.GetValue(retail_acct_idColumnIndex));
					record.Serial_number = Convert.ToInt64(reader.GetValue(serial_numberColumnIndex));
					record.Status = Convert.ToByte(reader.GetValue(statusColumnIndex));
					record.Inventory_status = Convert.ToByte(reader.GetValue(inventory_statusColumnIndex));
					record.Date_loaded = Convert.ToDateTime(reader.GetValue(date_loadedColumnIndex));
					record.Date_to_expire = Convert.ToDateTime(reader.GetValue(date_to_expireColumnIndex));
					if(!reader.IsDBNull(date_activeColumnIndex))
						record.Date_active = Convert.ToDateTime(reader.GetValue(date_activeColumnIndex));
					if(!reader.IsDBNull(date_first_usedColumnIndex))
						record.Date_first_used = Convert.ToDateTime(reader.GetValue(date_first_usedColumnIndex));
					if(!reader.IsDBNull(date_last_usedColumnIndex))
						record.Date_last_used = Convert.ToDateTime(reader.GetValue(date_last_usedColumnIndex));
					if(!reader.IsDBNull(date_deactivatedColumnIndex))
						record.Date_deactivated = Convert.ToDateTime(reader.GetValue(date_deactivatedColumnIndex));
					if(!reader.IsDBNull(date_archivedColumnIndex))
						record.Date_archived = Convert.ToDateTime(reader.GetValue(date_archivedColumnIndex));

					if(ri == length && 0 != totalRecordCount)
						break;
				}
			}

			totalRecordCount = 0 == totalRecordCount ? ri + startIndex : -1;
			return (PhoneCardRow[])(recordList.ToArray(typeof(PhoneCardRow)));
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
		/// Converts <see cref="System.Data.DataRow"/> to <see cref="PhoneCardRow"/>.
		/// </summary>
		/// <param name="row">The <see cref="System.Data.DataRow"/> object to be mapped.</param>
		/// <returns>A reference to the <see cref="PhoneCardRow"/> object.</returns>
		protected virtual PhoneCardRow MapRow(DataRow row)
		{
			PhoneCardRow mappedObject = new PhoneCardRow();
			DataTable dataTable = row.Table;
			DataColumn dataColumn;
			// Column "Service_id"
			dataColumn = dataTable.Columns["Service_id"];
			if(!row.IsNull(dataColumn))
				mappedObject.Service_id = (short)row[dataColumn];
			// Column "Pin"
			dataColumn = dataTable.Columns["Pin"];
			if(!row.IsNull(dataColumn))
				mappedObject.Pin = (long)row[dataColumn];
			// Column "Retail_acct_id"
			dataColumn = dataTable.Columns["Retail_acct_id"];
			if(!row.IsNull(dataColumn))
				mappedObject.Retail_acct_id = (int)row[dataColumn];
			// Column "Serial_number"
			dataColumn = dataTable.Columns["Serial_number"];
			if(!row.IsNull(dataColumn))
				mappedObject.Serial_number = (long)row[dataColumn];
			// Column "Status"
			dataColumn = dataTable.Columns["Status"];
			if(!row.IsNull(dataColumn))
				mappedObject.Status = (byte)row[dataColumn];
			// Column "Inventory_status"
			dataColumn = dataTable.Columns["Inventory_status"];
			if(!row.IsNull(dataColumn))
				mappedObject.Inventory_status = (byte)row[dataColumn];
			// Column "Date_loaded"
			dataColumn = dataTable.Columns["Date_loaded"];
			if(!row.IsNull(dataColumn))
				mappedObject.Date_loaded = (System.DateTime)row[dataColumn];
			// Column "Date_to_expire"
			dataColumn = dataTable.Columns["Date_to_expire"];
			if(!row.IsNull(dataColumn))
				mappedObject.Date_to_expire = (System.DateTime)row[dataColumn];
			// Column "Date_active"
			dataColumn = dataTable.Columns["Date_active"];
			if(!row.IsNull(dataColumn))
				mappedObject.Date_active = (System.DateTime)row[dataColumn];
			// Column "Date_first_used"
			dataColumn = dataTable.Columns["Date_first_used"];
			if(!row.IsNull(dataColumn))
				mappedObject.Date_first_used = (System.DateTime)row[dataColumn];
			// Column "Date_last_used"
			dataColumn = dataTable.Columns["Date_last_used"];
			if(!row.IsNull(dataColumn))
				mappedObject.Date_last_used = (System.DateTime)row[dataColumn];
			// Column "Date_deactivated"
			dataColumn = dataTable.Columns["Date_deactivated"];
			if(!row.IsNull(dataColumn))
				mappedObject.Date_deactivated = (System.DateTime)row[dataColumn];
			// Column "Date_archived"
			dataColumn = dataTable.Columns["Date_archived"];
			if(!row.IsNull(dataColumn))
				mappedObject.Date_archived = (System.DateTime)row[dataColumn];
			return mappedObject;
		}

		/// <summary>
		/// Creates a <see cref="System.Data.DataTable"/> object for 
		/// the <c>PhoneCard</c> table.
		/// </summary>
		/// <returns>A reference to the <see cref="System.Data.DataTable"/> object.</returns>
		protected virtual DataTable CreateDataTable()
		{
			DataTable dataTable = new DataTable();
			dataTable.TableName = "PhoneCard";
			DataColumn dataColumn;
			dataColumn = dataTable.Columns.Add("Service_id", typeof(short));
			dataColumn.Caption = "service_id";
			dataColumn.AllowDBNull = false;
			dataColumn = dataTable.Columns.Add("Pin", typeof(long));
			dataColumn.Caption = "pin";
			dataColumn.AllowDBNull = false;
			dataColumn = dataTable.Columns.Add("Retail_acct_id", typeof(int));
			dataColumn.Caption = "retail_acct_id";
			dataColumn.AllowDBNull = false;
			dataColumn = dataTable.Columns.Add("Serial_number", typeof(long));
			dataColumn.Caption = "serial_number";
			dataColumn.AllowDBNull = false;
			dataColumn = dataTable.Columns.Add("Status", typeof(byte));
			dataColumn.Caption = "status";
			dataColumn.AllowDBNull = false;
			dataColumn = dataTable.Columns.Add("Inventory_status", typeof(byte));
			dataColumn.Caption = "inventory_status";
			dataColumn.AllowDBNull = false;
			dataColumn = dataTable.Columns.Add("Date_loaded", typeof(System.DateTime));
			dataColumn.Caption = "date_loaded";
			dataColumn.AllowDBNull = false;
			dataColumn = dataTable.Columns.Add("Date_to_expire", typeof(System.DateTime));
			dataColumn.Caption = "date_to_expire";
			dataColumn.AllowDBNull = false;
			dataColumn = dataTable.Columns.Add("Date_active", typeof(System.DateTime));
			dataColumn.Caption = "date_active";
			dataColumn = dataTable.Columns.Add("Date_first_used", typeof(System.DateTime));
			dataColumn.Caption = "date_first_used";
			dataColumn.ReadOnly = true;
			dataColumn = dataTable.Columns.Add("Date_last_used", typeof(System.DateTime));
			dataColumn.Caption = "date_last_used";
			dataColumn.ReadOnly = true;
			dataColumn = dataTable.Columns.Add("Date_deactivated", typeof(System.DateTime));
			dataColumn.Caption = "date_deactivated";
			dataColumn = dataTable.Columns.Add("Date_archived", typeof(System.DateTime));
			dataColumn.Caption = "date_archived";
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

				case "Pin":
					parameter = _db.AddParameter(cmd, paramName, DbType.Int64, value);
					break;

				case "Retail_acct_id":
					parameter = _db.AddParameter(cmd, paramName, DbType.Int32, value);
					break;

				case "Serial_number":
					parameter = _db.AddParameter(cmd, paramName, DbType.Int64, value);
					break;

				case "Status":
					parameter = _db.AddParameter(cmd, paramName, DbType.Byte, value);
					break;

				case "Inventory_status":
					parameter = _db.AddParameter(cmd, paramName, DbType.Byte, value);
					break;

				case "Date_loaded":
					parameter = _db.AddParameter(cmd, paramName, DbType.DateTime, value);
					break;

				case "Date_to_expire":
					parameter = _db.AddParameter(cmd, paramName, DbType.DateTime, value);
					break;

				case "Date_active":
					parameter = _db.AddParameter(cmd, paramName, DbType.DateTime, value);
					break;

				case "Date_first_used":
					parameter = _db.AddParameter(cmd, paramName, DbType.DateTime, value);
					break;

				case "Date_last_used":
					parameter = _db.AddParameter(cmd, paramName, DbType.DateTime, value);
					break;

				case "Date_deactivated":
					parameter = _db.AddParameter(cmd, paramName, DbType.DateTime, value);
					break;

				case "Date_archived":
					parameter = _db.AddParameter(cmd, paramName, DbType.DateTime, value);
					break;

				default:
					throw new ArgumentException("Unknown parameter name (" + paramName + ").");
			}
			return parameter;
		}

	} // End of PhoneCardCollection_Base class
}  // End of namespace

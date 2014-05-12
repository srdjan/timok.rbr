// <fileinfo name="Base\InventoryHistoryCollection_Base.cs">
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
	/// The base class for <see cref="InventoryHistoryCollection"/>. Provides methods 
	/// for common database table operations. 
	/// </summary>
	/// <remarks>
	/// Do not change this source code. Update the <see cref="InventoryHistoryCollection"/>
	/// class if you need to add or change some functionality.
	/// </remarks>
	public abstract class InventoryHistoryCollection_Base
	{
		// Constants
		public const string Service_idColumnName = "service_id";
		public const string Batch_idColumnName = "batch_id";
		public const string TimestampColumnName = "timestamp";
		public const string CommandColumnName = "command";
		public const string Number_of_cardsColumnName = "number_of_cards";
		public const string DenominationColumnName = "denomination";
		public const string Customer_acct_idColumnName = "customer_acct_id";
		public const string Reseller_partner_idColumnName = "reseller_partner_id";
		public const string Reseller_agent_idColumnName = "reseller_agent_id";
		public const string Person_idColumnName = "person_id";

		// Instance fields
		private Rbr_Db _db;

		/// <summary>
		/// Initializes a new instance of the <see cref="InventoryHistoryCollection_Base"/> 
		/// class with the specified <see cref="Rbr_Db"/>.
		/// </summary>
		/// <param name="db">The <see cref="Rbr_Db"/> object.</param>
		public InventoryHistoryCollection_Base(Rbr_Db db)
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
		/// Gets an array of all records from the <c>InventoryHistory</c> table.
		/// </summary>
		/// <returns>An array of <see cref="InventoryHistoryRow"/> objects.</returns>
		public virtual InventoryHistoryRow[] GetAll()
		{
			return MapRecords(CreateGetAllCommand());
		}

		/// <summary>
		/// Gets a <see cref="System.Data.DataTable"/> object that 
		/// includes all records from the <c>InventoryHistory</c> table.
		/// </summary>
		/// <returns>A reference to the <see cref="System.Data.DataTable"/> object.</returns>
		public virtual DataTable GetAllAsDataTable()
		{
			return MapRecordsToDataTable(CreateGetAllCommand());
		}

		/// <summary>
		/// Creates and returns an <see cref="System.Data.IDbCommand"/> object that is used
		/// to retrieve all records from the <c>InventoryHistory</c> table.
		/// </summary>
		/// <returns>A reference to the <see cref="System.Data.IDbCommand"/> object.</returns>
		protected virtual IDbCommand CreateGetAllCommand()
		{
			return CreateGetCommand(null, null);
		}

		/// <summary>
		/// Gets the first <see cref="InventoryHistoryRow"/> objects that 
		/// match the search condition.
		/// </summary>
		/// <param name="whereSql">The SQL search condition. For example: 
		/// <c>"FirstName='Smith' AND Zip=75038"</c>.</param>
		/// <returns>An instance of <see cref="InventoryHistoryRow"/> or null reference 
		/// (Nothing in Visual Basic) if the object was not found.</returns>
		public InventoryHistoryRow GetRow(string whereSql)
		{
			int totalRecordCount = -1;
			InventoryHistoryRow[] rows = GetAsArray(whereSql, null, 0, 1, ref totalRecordCount);
			return 0 == rows.Length ? null : rows[0];
		}

		/// <summary>
		/// Gets an array of <see cref="InventoryHistoryRow"/> objects that 
		/// match the search condition, in the the specified sort order.
		/// </summary>
		/// <param name="whereSql">The SQL search condition. For example: 
		/// <c>"FirstName='Smith' AND Zip=75038"</c>.</param>
		/// <param name="orderBySql">The column name(s) followed by "ASC" (ascending) or "DESC" (descending).
		/// Columns are sorted in ascending order by default. For example: <c>"LastName ASC, FirstName ASC"</c>.</param>
		/// <returns>An array of <see cref="InventoryHistoryRow"/> objects.</returns>
		public InventoryHistoryRow[] GetAsArray(string whereSql, string orderBySql)
		{
			int totalRecordCount = -1;
			return GetAsArray(whereSql, orderBySql, 0, int.MaxValue, ref totalRecordCount);
		}

		/// <summary>
		/// Gets an array of <see cref="InventoryHistoryRow"/> objects that 
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
		/// <returns>An array of <see cref="InventoryHistoryRow"/> objects.</returns>
		public virtual InventoryHistoryRow[] GetAsArray(string whereSql, string orderBySql,
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
			string sql = "SELECT * FROM [dbo].[InventoryHistory]";
			if(null != whereSql && 0 < whereSql.Length)
				sql += " WHERE " + whereSql;
			if(null != orderBySql && 0 < orderBySql.Length)
				sql += " ORDER BY " + orderBySql;
			return _db.CreateCommand(sql);
		}

		/// <summary>
		/// Gets <see cref="InventoryHistoryRow"/> by the primary key.
		/// </summary>
		/// <param name="service_id">The <c>service_id</c> column value.</param>
		/// <param name="batch_id">The <c>batch_id</c> column value.</param>
		/// <param name="timestamp">The <c>timestamp</c> column value.</param>
		/// <returns>An instance of <see cref="InventoryHistoryRow"/> or null reference 
		/// (Nothing in Visual Basic) if the object was not found.</returns>
		public virtual InventoryHistoryRow GetByPrimaryKey(short service_id, int batch_id, System.DateTime timestamp)
		{
			string whereSql = "[service_id]=" + _db.CreateSqlParameterName("Service_id") + " AND " +
							  "[batch_id]=" + _db.CreateSqlParameterName("Batch_id") + " AND " +
							  "[timestamp]=" + _db.CreateSqlParameterName("Timestamp");
			IDbCommand cmd = CreateGetCommand(whereSql, null);
			AddParameter(cmd, "Service_id", service_id);
			AddParameter(cmd, "Batch_id", batch_id);
			AddParameter(cmd, "Timestamp", timestamp);
			InventoryHistoryRow[] tempArray = MapRecords(cmd);
			return 0 == tempArray.Length ? null : tempArray[0];
		}

		/// <summary>
		/// Adds a new record into the <c>InventoryHistory</c> table.
		/// </summary>
		/// <param name="value">The <see cref="InventoryHistoryRow"/> object to be inserted.</param>
		public virtual void Insert(InventoryHistoryRow value)
		{
			string sqlStr = "INSERT INTO [dbo].[InventoryHistory] (" +
				"[service_id], " +
				"[batch_id], " +
				"[timestamp], " +
				"[command], " +
				"[number_of_cards], " +
				"[denomination], " +
				"[customer_acct_id], " +
				"[reseller_partner_id], " +
				"[reseller_agent_id], " +
				"[person_id]" +
				") VALUES (" +
				_db.CreateSqlParameterName("Service_id") + ", " +
				_db.CreateSqlParameterName("Batch_id") + ", " +
				_db.CreateSqlParameterName("Timestamp") + ", " +
				_db.CreateSqlParameterName("Command") + ", " +
				_db.CreateSqlParameterName("Number_of_cards") + ", " +
				_db.CreateSqlParameterName("Denomination") + ", " +
				_db.CreateSqlParameterName("Customer_acct_id") + ", " +
				_db.CreateSqlParameterName("Reseller_partner_id") + ", " +
				_db.CreateSqlParameterName("Reseller_agent_id") + ", " +
				_db.CreateSqlParameterName("Person_id") + ")";
			IDbCommand cmd = _db.CreateCommand(sqlStr);
			AddParameter(cmd, "Service_id", value.Service_id);
			AddParameter(cmd, "Batch_id", value.Batch_id);
			AddParameter(cmd, "Timestamp", value.Timestamp);
			AddParameter(cmd, "Command", value.Command);
			AddParameter(cmd, "Number_of_cards", value.Number_of_cards);
			AddParameter(cmd, "Denomination", value.Denomination);
			AddParameter(cmd, "Customer_acct_id",
				value.IsCustomer_acct_idNull ? DBNull.Value : (object)value.Customer_acct_id);
			AddParameter(cmd, "Reseller_partner_id",
				value.IsReseller_partner_idNull ? DBNull.Value : (object)value.Reseller_partner_id);
			AddParameter(cmd, "Reseller_agent_id",
				value.IsReseller_agent_idNull ? DBNull.Value : (object)value.Reseller_agent_id);
			AddParameter(cmd, "Person_id", value.Person_id);
			cmd.ExecuteNonQuery();
		}

		/// <summary>
		/// Updates a record in the <c>InventoryHistory</c> table.
		/// </summary>
		/// <param name="value">The <see cref="InventoryHistoryRow"/>
		/// object used to update the table record.</param>
		/// <returns>true if the record was updated; otherwise, false.</returns>
		public virtual bool Update(InventoryHistoryRow value)
		{
			string sqlStr = "UPDATE [dbo].[InventoryHistory] SET " +
				"[command]=" + _db.CreateSqlParameterName("Command") + ", " +
				"[number_of_cards]=" + _db.CreateSqlParameterName("Number_of_cards") + ", " +
				"[denomination]=" + _db.CreateSqlParameterName("Denomination") + ", " +
				"[customer_acct_id]=" + _db.CreateSqlParameterName("Customer_acct_id") + ", " +
				"[reseller_partner_id]=" + _db.CreateSqlParameterName("Reseller_partner_id") + ", " +
				"[reseller_agent_id]=" + _db.CreateSqlParameterName("Reseller_agent_id") + ", " +
				"[person_id]=" + _db.CreateSqlParameterName("Person_id") +
				" WHERE " +
				"[service_id]=" + _db.CreateSqlParameterName("Service_id") + " AND " +
				"[batch_id]=" + _db.CreateSqlParameterName("Batch_id") + " AND " +
				"[timestamp]=" + _db.CreateSqlParameterName("Timestamp");
			IDbCommand cmd = _db.CreateCommand(sqlStr);
			AddParameter(cmd, "Command", value.Command);
			AddParameter(cmd, "Number_of_cards", value.Number_of_cards);
			AddParameter(cmd, "Denomination", value.Denomination);
			AddParameter(cmd, "Customer_acct_id",
				value.IsCustomer_acct_idNull ? DBNull.Value : (object)value.Customer_acct_id);
			AddParameter(cmd, "Reseller_partner_id",
				value.IsReseller_partner_idNull ? DBNull.Value : (object)value.Reseller_partner_id);
			AddParameter(cmd, "Reseller_agent_id",
				value.IsReseller_agent_idNull ? DBNull.Value : (object)value.Reseller_agent_id);
			AddParameter(cmd, "Person_id", value.Person_id);
			AddParameter(cmd, "Service_id", value.Service_id);
			AddParameter(cmd, "Batch_id", value.Batch_id);
			AddParameter(cmd, "Timestamp", value.Timestamp);
			return 0 != cmd.ExecuteNonQuery();
		}

		/// <summary>
		/// Updates the <c>InventoryHistory</c> table and calls the <c>AcceptChanges</c> method
		/// on the changed DataRow objects.
		/// </summary>
		/// <param name="table">The <see cref="System.Data.DataTable"/> used to update the data source.</param>
		public void Update(DataTable table)
		{
			Update(table, true);
		}

		/// <summary>
		/// Updates the <c>InventoryHistory</c> table. Pass <c>false</c> as the <c>acceptChanges</c> 
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
							DeleteByPrimaryKey((short)row["Service_id"], (int)row["Batch_id"], (System.DateTime)row["Timestamp"]);
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
		/// Deletes the specified object from the <c>InventoryHistory</c> table.
		/// </summary>
		/// <param name="value">The <see cref="InventoryHistoryRow"/> object to delete.</param>
		/// <returns>true if the record was deleted; otherwise, false.</returns>
		public bool Delete(InventoryHistoryRow value)
		{
			return DeleteByPrimaryKey(value.Service_id, value.Batch_id, value.Timestamp);
		}

		/// <summary>
		/// Deletes a record from the <c>InventoryHistory</c> table using
		/// the specified primary key.
		/// </summary>
		/// <param name="service_id">The <c>service_id</c> column value.</param>
		/// <param name="batch_id">The <c>batch_id</c> column value.</param>
		/// <param name="timestamp">The <c>timestamp</c> column value.</param>
		/// <returns>true if the record was deleted; otherwise, false.</returns>
		public virtual bool DeleteByPrimaryKey(short service_id, int batch_id, System.DateTime timestamp)
		{
			string whereSql = "[service_id]=" + _db.CreateSqlParameterName("Service_id") + " AND " +
							  "[batch_id]=" + _db.CreateSqlParameterName("Batch_id") + " AND " +
							  "[timestamp]=" + _db.CreateSqlParameterName("Timestamp");
			IDbCommand cmd = CreateDeleteCommand(whereSql);
			AddParameter(cmd, "Service_id", service_id);
			AddParameter(cmd, "Batch_id", batch_id);
			AddParameter(cmd, "Timestamp", timestamp);
			return 0 < cmd.ExecuteNonQuery();
		}

		/// <summary>
		/// Deletes <c>InventoryHistory</c> records that match the specified criteria.
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
		/// to delete <c>InventoryHistory</c> records that match the specified criteria.
		/// </summary>
		/// <param name="whereSql">The SQL search condition. 
		/// For example: <c>"FirstName='Smith' AND Zip=75038"</c>.</param>
		/// <returns>A reference to the <see cref="System.Data.IDbCommand"/> object.</returns>
		protected virtual IDbCommand CreateDeleteCommand(string whereSql)
		{
			string sql = "DELETE FROM [dbo].[InventoryHistory]";
			if(null != whereSql && 0 < whereSql.Length)
				sql += " WHERE " + whereSql;
			return _db.CreateCommand(sql);
		}

		/// <summary>
		/// Deletes all records from the <c>InventoryHistory</c> table.
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
		/// <returns>An array of <see cref="InventoryHistoryRow"/> objects.</returns>
		protected InventoryHistoryRow[] MapRecords(IDbCommand command)
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
		/// <returns>An array of <see cref="InventoryHistoryRow"/> objects.</returns>
		protected InventoryHistoryRow[] MapRecords(IDataReader reader)
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
		/// <returns>An array of <see cref="InventoryHistoryRow"/> objects.</returns>
		protected virtual InventoryHistoryRow[] MapRecords(IDataReader reader, 
										int startIndex, int length, ref int totalRecordCount)
		{
			if(0 > startIndex)
				throw new ArgumentOutOfRangeException("startIndex", startIndex, "StartIndex cannot be less than zero.");
			if(0 > length)
				throw new ArgumentOutOfRangeException("length", length, "Length cannot be less than zero.");

			int service_idColumnIndex = reader.GetOrdinal("service_id");
			int batch_idColumnIndex = reader.GetOrdinal("batch_id");
			int timestampColumnIndex = reader.GetOrdinal("timestamp");
			int commandColumnIndex = reader.GetOrdinal("command");
			int number_of_cardsColumnIndex = reader.GetOrdinal("number_of_cards");
			int denominationColumnIndex = reader.GetOrdinal("denomination");
			int customer_acct_idColumnIndex = reader.GetOrdinal("customer_acct_id");
			int reseller_partner_idColumnIndex = reader.GetOrdinal("reseller_partner_id");
			int reseller_agent_idColumnIndex = reader.GetOrdinal("reseller_agent_id");
			int person_idColumnIndex = reader.GetOrdinal("person_id");

			System.Collections.ArrayList recordList = new System.Collections.ArrayList();
			int ri = -startIndex;
			while(reader.Read())
			{
				ri++;
				if(ri > 0 && ri <= length)
				{
					InventoryHistoryRow record = new InventoryHistoryRow();
					recordList.Add(record);

					record.Service_id = Convert.ToInt16(reader.GetValue(service_idColumnIndex));
					record.Batch_id = Convert.ToInt32(reader.GetValue(batch_idColumnIndex));
					record.Timestamp = Convert.ToDateTime(reader.GetValue(timestampColumnIndex));
					record.Command = Convert.ToByte(reader.GetValue(commandColumnIndex));
					record.Number_of_cards = Convert.ToInt32(reader.GetValue(number_of_cardsColumnIndex));
					record.Denomination = Convert.ToDecimal(reader.GetValue(denominationColumnIndex));
					if(!reader.IsDBNull(customer_acct_idColumnIndex))
						record.Customer_acct_id = Convert.ToInt16(reader.GetValue(customer_acct_idColumnIndex));
					if(!reader.IsDBNull(reseller_partner_idColumnIndex))
						record.Reseller_partner_id = Convert.ToInt32(reader.GetValue(reseller_partner_idColumnIndex));
					if(!reader.IsDBNull(reseller_agent_idColumnIndex))
						record.Reseller_agent_id = Convert.ToInt32(reader.GetValue(reseller_agent_idColumnIndex));
					record.Person_id = Convert.ToInt32(reader.GetValue(person_idColumnIndex));

					if(ri == length && 0 != totalRecordCount)
						break;
				}
			}

			totalRecordCount = 0 == totalRecordCount ? ri + startIndex : -1;
			return (InventoryHistoryRow[])(recordList.ToArray(typeof(InventoryHistoryRow)));
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
		/// Converts <see cref="System.Data.DataRow"/> to <see cref="InventoryHistoryRow"/>.
		/// </summary>
		/// <param name="row">The <see cref="System.Data.DataRow"/> object to be mapped.</param>
		/// <returns>A reference to the <see cref="InventoryHistoryRow"/> object.</returns>
		protected virtual InventoryHistoryRow MapRow(DataRow row)
		{
			InventoryHistoryRow mappedObject = new InventoryHistoryRow();
			DataTable dataTable = row.Table;
			DataColumn dataColumn;
			// Column "Service_id"
			dataColumn = dataTable.Columns["Service_id"];
			if(!row.IsNull(dataColumn))
				mappedObject.Service_id = (short)row[dataColumn];
			// Column "Batch_id"
			dataColumn = dataTable.Columns["Batch_id"];
			if(!row.IsNull(dataColumn))
				mappedObject.Batch_id = (int)row[dataColumn];
			// Column "Timestamp"
			dataColumn = dataTable.Columns["Timestamp"];
			if(!row.IsNull(dataColumn))
				mappedObject.Timestamp = (System.DateTime)row[dataColumn];
			// Column "Command"
			dataColumn = dataTable.Columns["Command"];
			if(!row.IsNull(dataColumn))
				mappedObject.Command = (byte)row[dataColumn];
			// Column "Number_of_cards"
			dataColumn = dataTable.Columns["Number_of_cards"];
			if(!row.IsNull(dataColumn))
				mappedObject.Number_of_cards = (int)row[dataColumn];
			// Column "Denomination"
			dataColumn = dataTable.Columns["Denomination"];
			if(!row.IsNull(dataColumn))
				mappedObject.Denomination = (decimal)row[dataColumn];
			// Column "Customer_acct_id"
			dataColumn = dataTable.Columns["Customer_acct_id"];
			if(!row.IsNull(dataColumn))
				mappedObject.Customer_acct_id = (short)row[dataColumn];
			// Column "Reseller_partner_id"
			dataColumn = dataTable.Columns["Reseller_partner_id"];
			if(!row.IsNull(dataColumn))
				mappedObject.Reseller_partner_id = (int)row[dataColumn];
			// Column "Reseller_agent_id"
			dataColumn = dataTable.Columns["Reseller_agent_id"];
			if(!row.IsNull(dataColumn))
				mappedObject.Reseller_agent_id = (int)row[dataColumn];
			// Column "Person_id"
			dataColumn = dataTable.Columns["Person_id"];
			if(!row.IsNull(dataColumn))
				mappedObject.Person_id = (int)row[dataColumn];
			return mappedObject;
		}

		/// <summary>
		/// Creates a <see cref="System.Data.DataTable"/> object for 
		/// the <c>InventoryHistory</c> table.
		/// </summary>
		/// <returns>A reference to the <see cref="System.Data.DataTable"/> object.</returns>
		protected virtual DataTable CreateDataTable()
		{
			DataTable dataTable = new DataTable();
			dataTable.TableName = "InventoryHistory";
			DataColumn dataColumn;
			dataColumn = dataTable.Columns.Add("Service_id", typeof(short));
			dataColumn.Caption = "service_id";
			dataColumn.AllowDBNull = false;
			dataColumn = dataTable.Columns.Add("Batch_id", typeof(int));
			dataColumn.Caption = "batch_id";
			dataColumn.AllowDBNull = false;
			dataColumn = dataTable.Columns.Add("Timestamp", typeof(System.DateTime));
			dataColumn.Caption = "timestamp";
			dataColumn.AllowDBNull = false;
			dataColumn = dataTable.Columns.Add("Command", typeof(byte));
			dataColumn.Caption = "command";
			dataColumn.AllowDBNull = false;
			dataColumn = dataTable.Columns.Add("Number_of_cards", typeof(int));
			dataColumn.Caption = "number_of_cards";
			dataColumn.AllowDBNull = false;
			dataColumn = dataTable.Columns.Add("Denomination", typeof(decimal));
			dataColumn.Caption = "denomination";
			dataColumn.AllowDBNull = false;
			dataColumn = dataTable.Columns.Add("Customer_acct_id", typeof(short));
			dataColumn.Caption = "customer_acct_id";
			dataColumn = dataTable.Columns.Add("Reseller_partner_id", typeof(int));
			dataColumn.Caption = "reseller_partner_id";
			dataColumn = dataTable.Columns.Add("Reseller_agent_id", typeof(int));
			dataColumn.Caption = "reseller_agent_id";
			dataColumn = dataTable.Columns.Add("Person_id", typeof(int));
			dataColumn.Caption = "person_id";
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

				case "Batch_id":
					parameter = _db.AddParameter(cmd, paramName, DbType.Int32, value);
					break;

				case "Timestamp":
					parameter = _db.AddParameter(cmd, paramName, DbType.DateTime, value);
					break;

				case "Command":
					parameter = _db.AddParameter(cmd, paramName, DbType.Byte, value);
					break;

				case "Number_of_cards":
					parameter = _db.AddParameter(cmd, paramName, DbType.Int32, value);
					break;

				case "Denomination":
					parameter = _db.AddParameter(cmd, paramName, DbType.Decimal, value);
					break;

				case "Customer_acct_id":
					parameter = _db.AddParameter(cmd, paramName, DbType.Int16, value);
					break;

				case "Reseller_partner_id":
					parameter = _db.AddParameter(cmd, paramName, DbType.Int32, value);
					break;

				case "Reseller_agent_id":
					parameter = _db.AddParameter(cmd, paramName, DbType.Int32, value);
					break;

				case "Person_id":
					parameter = _db.AddParameter(cmd, paramName, DbType.Int32, value);
					break;

				default:
					throw new ArgumentException("Unknown parameter name (" + paramName + ").");
			}
			return parameter;
		}

	} // End of InventoryHistoryCollection_Base class
}  // End of namespace

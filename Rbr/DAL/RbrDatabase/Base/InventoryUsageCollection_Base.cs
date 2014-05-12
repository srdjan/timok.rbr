// <fileinfo name="Base\InventoryUsageCollection_Base.cs">
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
	/// The base class for <see cref="InventoryUsageCollection"/>. Provides methods 
	/// for common database table operations. 
	/// </summary>
	/// <remarks>
	/// Do not change this source code. Update the <see cref="InventoryUsageCollection"/>
	/// class if you need to add or change some functionality.
	/// </remarks>
	public abstract class InventoryUsageCollection_Base
	{
		// Constants
		public const string Service_idColumnName = "service_id";
		public const string Customer_acct_idColumnName = "customer_acct_id";
		public const string TimestampColumnName = "timestamp";
		public const string First_usedColumnName = "first_used";
		public const string Total_usedColumnName = "total_used";
		public const string Balance_depletedColumnName = "balance_depleted";
		public const string ExpiredColumnName = "expired";

		// Instance fields
		private Rbr_Db _db;

		/// <summary>
		/// Initializes a new instance of the <see cref="InventoryUsageCollection_Base"/> 
		/// class with the specified <see cref="Rbr_Db"/>.
		/// </summary>
		/// <param name="db">The <see cref="Rbr_Db"/> object.</param>
		public InventoryUsageCollection_Base(Rbr_Db db)
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
		/// Gets an array of all records from the <c>InventoryUsage</c> table.
		/// </summary>
		/// <returns>An array of <see cref="InventoryUsageRow"/> objects.</returns>
		public virtual InventoryUsageRow[] GetAll()
		{
			return MapRecords(CreateGetAllCommand());
		}

		/// <summary>
		/// Gets a <see cref="System.Data.DataTable"/> object that 
		/// includes all records from the <c>InventoryUsage</c> table.
		/// </summary>
		/// <returns>A reference to the <see cref="System.Data.DataTable"/> object.</returns>
		public virtual DataTable GetAllAsDataTable()
		{
			return MapRecordsToDataTable(CreateGetAllCommand());
		}

		/// <summary>
		/// Creates and returns an <see cref="System.Data.IDbCommand"/> object that is used
		/// to retrieve all records from the <c>InventoryUsage</c> table.
		/// </summary>
		/// <returns>A reference to the <see cref="System.Data.IDbCommand"/> object.</returns>
		protected virtual IDbCommand CreateGetAllCommand()
		{
			return CreateGetCommand(null, null);
		}

		/// <summary>
		/// Gets the first <see cref="InventoryUsageRow"/> objects that 
		/// match the search condition.
		/// </summary>
		/// <param name="whereSql">The SQL search condition. For example: 
		/// <c>"FirstName='Smith' AND Zip=75038"</c>.</param>
		/// <returns>An instance of <see cref="InventoryUsageRow"/> or null reference 
		/// (Nothing in Visual Basic) if the object was not found.</returns>
		public InventoryUsageRow GetRow(string whereSql)
		{
			int totalRecordCount = -1;
			InventoryUsageRow[] rows = GetAsArray(whereSql, null, 0, 1, ref totalRecordCount);
			return 0 == rows.Length ? null : rows[0];
		}

		/// <summary>
		/// Gets an array of <see cref="InventoryUsageRow"/> objects that 
		/// match the search condition, in the the specified sort order.
		/// </summary>
		/// <param name="whereSql">The SQL search condition. For example: 
		/// <c>"FirstName='Smith' AND Zip=75038"</c>.</param>
		/// <param name="orderBySql">The column name(s) followed by "ASC" (ascending) or "DESC" (descending).
		/// Columns are sorted in ascending order by default. For example: <c>"LastName ASC, FirstName ASC"</c>.</param>
		/// <returns>An array of <see cref="InventoryUsageRow"/> objects.</returns>
		public InventoryUsageRow[] GetAsArray(string whereSql, string orderBySql)
		{
			int totalRecordCount = -1;
			return GetAsArray(whereSql, orderBySql, 0, int.MaxValue, ref totalRecordCount);
		}

		/// <summary>
		/// Gets an array of <see cref="InventoryUsageRow"/> objects that 
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
		/// <returns>An array of <see cref="InventoryUsageRow"/> objects.</returns>
		public virtual InventoryUsageRow[] GetAsArray(string whereSql, string orderBySql,
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
			string sql = "SELECT * FROM [dbo].[InventoryUsage]";
			if(null != whereSql && 0 < whereSql.Length)
				sql += " WHERE " + whereSql;
			if(null != orderBySql && 0 < orderBySql.Length)
				sql += " ORDER BY " + orderBySql;
			return _db.CreateCommand(sql);
		}

		/// <summary>
		/// Gets <see cref="InventoryUsageRow"/> by the primary key.
		/// </summary>
		/// <param name="service_id">The <c>service_id</c> column value.</param>
		/// <param name="customer_acct_id">The <c>customer_acct_id</c> column value.</param>
		/// <param name="timestamp">The <c>timestamp</c> column value.</param>
		/// <returns>An instance of <see cref="InventoryUsageRow"/> or null reference 
		/// (Nothing in Visual Basic) if the object was not found.</returns>
		public virtual InventoryUsageRow GetByPrimaryKey(short service_id, short customer_acct_id, System.DateTime timestamp)
		{
			string whereSql = "[service_id]=" + _db.CreateSqlParameterName("Service_id") + " AND " +
							  "[customer_acct_id]=" + _db.CreateSqlParameterName("Customer_acct_id") + " AND " +
							  "[timestamp]=" + _db.CreateSqlParameterName("Timestamp");
			IDbCommand cmd = CreateGetCommand(whereSql, null);
			AddParameter(cmd, "Service_id", service_id);
			AddParameter(cmd, "Customer_acct_id", customer_acct_id);
			AddParameter(cmd, "Timestamp", timestamp);
			InventoryUsageRow[] tempArray = MapRecords(cmd);
			return 0 == tempArray.Length ? null : tempArray[0];
		}

		/// <summary>
		/// Adds a new record into the <c>InventoryUsage</c> table.
		/// </summary>
		/// <param name="value">The <see cref="InventoryUsageRow"/> object to be inserted.</param>
		public virtual void Insert(InventoryUsageRow value)
		{
			string sqlStr = "INSERT INTO [dbo].[InventoryUsage] (" +
				"[service_id], " +
				"[customer_acct_id], " +
				"[timestamp], " +
				"[first_used], " +
				"[total_used], " +
				"[balance_depleted], " +
				"[expired]" +
				") VALUES (" +
				_db.CreateSqlParameterName("Service_id") + ", " +
				_db.CreateSqlParameterName("Customer_acct_id") + ", " +
				_db.CreateSqlParameterName("Timestamp") + ", " +
				_db.CreateSqlParameterName("First_used") + ", " +
				_db.CreateSqlParameterName("Total_used") + ", " +
				_db.CreateSqlParameterName("Balance_depleted") + ", " +
				_db.CreateSqlParameterName("Expired") + ")";
			IDbCommand cmd = _db.CreateCommand(sqlStr);
			AddParameter(cmd, "Service_id", value.Service_id);
			AddParameter(cmd, "Customer_acct_id", value.Customer_acct_id);
			AddParameter(cmd, "Timestamp", value.Timestamp);
			AddParameter(cmd, "First_used", value.First_used);
			AddParameter(cmd, "Total_used", value.Total_used);
			AddParameter(cmd, "Balance_depleted", value.Balance_depleted);
			AddParameter(cmd, "Expired", value.Expired);
			cmd.ExecuteNonQuery();
		}

		/// <summary>
		/// Updates a record in the <c>InventoryUsage</c> table.
		/// </summary>
		/// <param name="value">The <see cref="InventoryUsageRow"/>
		/// object used to update the table record.</param>
		/// <returns>true if the record was updated; otherwise, false.</returns>
		public virtual bool Update(InventoryUsageRow value)
		{
			string sqlStr = "UPDATE [dbo].[InventoryUsage] SET " +
				"[first_used]=" + _db.CreateSqlParameterName("First_used") + ", " +
				"[total_used]=" + _db.CreateSqlParameterName("Total_used") + ", " +
				"[balance_depleted]=" + _db.CreateSqlParameterName("Balance_depleted") + ", " +
				"[expired]=" + _db.CreateSqlParameterName("Expired") +
				" WHERE " +
				"[service_id]=" + _db.CreateSqlParameterName("Service_id") + " AND " +
				"[customer_acct_id]=" + _db.CreateSqlParameterName("Customer_acct_id") + " AND " +
				"[timestamp]=" + _db.CreateSqlParameterName("Timestamp");
			IDbCommand cmd = _db.CreateCommand(sqlStr);
			AddParameter(cmd, "First_used", value.First_used);
			AddParameter(cmd, "Total_used", value.Total_used);
			AddParameter(cmd, "Balance_depleted", value.Balance_depleted);
			AddParameter(cmd, "Expired", value.Expired);
			AddParameter(cmd, "Service_id", value.Service_id);
			AddParameter(cmd, "Customer_acct_id", value.Customer_acct_id);
			AddParameter(cmd, "Timestamp", value.Timestamp);
			return 0 != cmd.ExecuteNonQuery();
		}

		/// <summary>
		/// Updates the <c>InventoryUsage</c> table and calls the <c>AcceptChanges</c> method
		/// on the changed DataRow objects.
		/// </summary>
		/// <param name="table">The <see cref="System.Data.DataTable"/> used to update the data source.</param>
		public void Update(DataTable table)
		{
			Update(table, true);
		}

		/// <summary>
		/// Updates the <c>InventoryUsage</c> table. Pass <c>false</c> as the <c>acceptChanges</c> 
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
							DeleteByPrimaryKey((short)row["Service_id"], (short)row["Customer_acct_id"], (System.DateTime)row["Timestamp"]);
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
		/// Deletes the specified object from the <c>InventoryUsage</c> table.
		/// </summary>
		/// <param name="value">The <see cref="InventoryUsageRow"/> object to delete.</param>
		/// <returns>true if the record was deleted; otherwise, false.</returns>
		public bool Delete(InventoryUsageRow value)
		{
			return DeleteByPrimaryKey(value.Service_id, value.Customer_acct_id, value.Timestamp);
		}

		/// <summary>
		/// Deletes a record from the <c>InventoryUsage</c> table using
		/// the specified primary key.
		/// </summary>
		/// <param name="service_id">The <c>service_id</c> column value.</param>
		/// <param name="customer_acct_id">The <c>customer_acct_id</c> column value.</param>
		/// <param name="timestamp">The <c>timestamp</c> column value.</param>
		/// <returns>true if the record was deleted; otherwise, false.</returns>
		public virtual bool DeleteByPrimaryKey(short service_id, short customer_acct_id, System.DateTime timestamp)
		{
			string whereSql = "[service_id]=" + _db.CreateSqlParameterName("Service_id") + " AND " +
							  "[customer_acct_id]=" + _db.CreateSqlParameterName("Customer_acct_id") + " AND " +
							  "[timestamp]=" + _db.CreateSqlParameterName("Timestamp");
			IDbCommand cmd = CreateDeleteCommand(whereSql);
			AddParameter(cmd, "Service_id", service_id);
			AddParameter(cmd, "Customer_acct_id", customer_acct_id);
			AddParameter(cmd, "Timestamp", timestamp);
			return 0 < cmd.ExecuteNonQuery();
		}

		/// <summary>
		/// Deletes <c>InventoryUsage</c> records that match the specified criteria.
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
		/// to delete <c>InventoryUsage</c> records that match the specified criteria.
		/// </summary>
		/// <param name="whereSql">The SQL search condition. 
		/// For example: <c>"FirstName='Smith' AND Zip=75038"</c>.</param>
		/// <returns>A reference to the <see cref="System.Data.IDbCommand"/> object.</returns>
		protected virtual IDbCommand CreateDeleteCommand(string whereSql)
		{
			string sql = "DELETE FROM [dbo].[InventoryUsage]";
			if(null != whereSql && 0 < whereSql.Length)
				sql += " WHERE " + whereSql;
			return _db.CreateCommand(sql);
		}

		/// <summary>
		/// Deletes all records from the <c>InventoryUsage</c> table.
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
		/// <returns>An array of <see cref="InventoryUsageRow"/> objects.</returns>
		protected InventoryUsageRow[] MapRecords(IDbCommand command)
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
		/// <returns>An array of <see cref="InventoryUsageRow"/> objects.</returns>
		protected InventoryUsageRow[] MapRecords(IDataReader reader)
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
		/// <returns>An array of <see cref="InventoryUsageRow"/> objects.</returns>
		protected virtual InventoryUsageRow[] MapRecords(IDataReader reader, 
										int startIndex, int length, ref int totalRecordCount)
		{
			if(0 > startIndex)
				throw new ArgumentOutOfRangeException("startIndex", startIndex, "StartIndex cannot be less than zero.");
			if(0 > length)
				throw new ArgumentOutOfRangeException("length", length, "Length cannot be less than zero.");

			int service_idColumnIndex = reader.GetOrdinal("service_id");
			int customer_acct_idColumnIndex = reader.GetOrdinal("customer_acct_id");
			int timestampColumnIndex = reader.GetOrdinal("timestamp");
			int first_usedColumnIndex = reader.GetOrdinal("first_used");
			int total_usedColumnIndex = reader.GetOrdinal("total_used");
			int balance_depletedColumnIndex = reader.GetOrdinal("balance_depleted");
			int expiredColumnIndex = reader.GetOrdinal("expired");

			System.Collections.ArrayList recordList = new System.Collections.ArrayList();
			int ri = -startIndex;
			while(reader.Read())
			{
				ri++;
				if(ri > 0 && ri <= length)
				{
					InventoryUsageRow record = new InventoryUsageRow();
					recordList.Add(record);

					record.Service_id = Convert.ToInt16(reader.GetValue(service_idColumnIndex));
					record.Customer_acct_id = Convert.ToInt16(reader.GetValue(customer_acct_idColumnIndex));
					record.Timestamp = Convert.ToDateTime(reader.GetValue(timestampColumnIndex));
					record.First_used = Convert.ToInt32(reader.GetValue(first_usedColumnIndex));
					record.Total_used = Convert.ToInt32(reader.GetValue(total_usedColumnIndex));
					record.Balance_depleted = Convert.ToInt32(reader.GetValue(balance_depletedColumnIndex));
					record.Expired = Convert.ToInt32(reader.GetValue(expiredColumnIndex));

					if(ri == length && 0 != totalRecordCount)
						break;
				}
			}

			totalRecordCount = 0 == totalRecordCount ? ri + startIndex : -1;
			return (InventoryUsageRow[])(recordList.ToArray(typeof(InventoryUsageRow)));
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
		/// Converts <see cref="System.Data.DataRow"/> to <see cref="InventoryUsageRow"/>.
		/// </summary>
		/// <param name="row">The <see cref="System.Data.DataRow"/> object to be mapped.</param>
		/// <returns>A reference to the <see cref="InventoryUsageRow"/> object.</returns>
		protected virtual InventoryUsageRow MapRow(DataRow row)
		{
			InventoryUsageRow mappedObject = new InventoryUsageRow();
			DataTable dataTable = row.Table;
			DataColumn dataColumn;
			// Column "Service_id"
			dataColumn = dataTable.Columns["Service_id"];
			if(!row.IsNull(dataColumn))
				mappedObject.Service_id = (short)row[dataColumn];
			// Column "Customer_acct_id"
			dataColumn = dataTable.Columns["Customer_acct_id"];
			if(!row.IsNull(dataColumn))
				mappedObject.Customer_acct_id = (short)row[dataColumn];
			// Column "Timestamp"
			dataColumn = dataTable.Columns["Timestamp"];
			if(!row.IsNull(dataColumn))
				mappedObject.Timestamp = (System.DateTime)row[dataColumn];
			// Column "First_used"
			dataColumn = dataTable.Columns["First_used"];
			if(!row.IsNull(dataColumn))
				mappedObject.First_used = (int)row[dataColumn];
			// Column "Total_used"
			dataColumn = dataTable.Columns["Total_used"];
			if(!row.IsNull(dataColumn))
				mappedObject.Total_used = (int)row[dataColumn];
			// Column "Balance_depleted"
			dataColumn = dataTable.Columns["Balance_depleted"];
			if(!row.IsNull(dataColumn))
				mappedObject.Balance_depleted = (int)row[dataColumn];
			// Column "Expired"
			dataColumn = dataTable.Columns["Expired"];
			if(!row.IsNull(dataColumn))
				mappedObject.Expired = (int)row[dataColumn];
			return mappedObject;
		}

		/// <summary>
		/// Creates a <see cref="System.Data.DataTable"/> object for 
		/// the <c>InventoryUsage</c> table.
		/// </summary>
		/// <returns>A reference to the <see cref="System.Data.DataTable"/> object.</returns>
		protected virtual DataTable CreateDataTable()
		{
			DataTable dataTable = new DataTable();
			dataTable.TableName = "InventoryUsage";
			DataColumn dataColumn;
			dataColumn = dataTable.Columns.Add("Service_id", typeof(short));
			dataColumn.Caption = "service_id";
			dataColumn.AllowDBNull = false;
			dataColumn = dataTable.Columns.Add("Customer_acct_id", typeof(short));
			dataColumn.Caption = "customer_acct_id";
			dataColumn.AllowDBNull = false;
			dataColumn = dataTable.Columns.Add("Timestamp", typeof(System.DateTime));
			dataColumn.Caption = "timestamp";
			dataColumn.AllowDBNull = false;
			dataColumn = dataTable.Columns.Add("First_used", typeof(int));
			dataColumn.Caption = "first_used";
			dataColumn.AllowDBNull = false;
			dataColumn = dataTable.Columns.Add("Total_used", typeof(int));
			dataColumn.Caption = "total_used";
			dataColumn.AllowDBNull = false;
			dataColumn = dataTable.Columns.Add("Balance_depleted", typeof(int));
			dataColumn.Caption = "balance_depleted";
			dataColumn.AllowDBNull = false;
			dataColumn = dataTable.Columns.Add("Expired", typeof(int));
			dataColumn.Caption = "expired";
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

				case "Customer_acct_id":
					parameter = _db.AddParameter(cmd, paramName, DbType.Int16, value);
					break;

				case "Timestamp":
					parameter = _db.AddParameter(cmd, paramName, DbType.DateTime, value);
					break;

				case "First_used":
					parameter = _db.AddParameter(cmd, paramName, DbType.Int32, value);
					break;

				case "Total_used":
					parameter = _db.AddParameter(cmd, paramName, DbType.Int32, value);
					break;

				case "Balance_depleted":
					parameter = _db.AddParameter(cmd, paramName, DbType.Int32, value);
					break;

				case "Expired":
					parameter = _db.AddParameter(cmd, paramName, DbType.Int32, value);
					break;

				default:
					throw new ArgumentException("Unknown parameter name (" + paramName + ").");
			}
			return parameter;
		}

	} // End of InventoryUsageCollection_Base class
}  // End of namespace

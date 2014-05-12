// <fileinfo name="Base\CdrAggregateCollection_Base.cs">
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
	/// The base class for <see cref="CdrAggregateCollection"/>. Provides methods 
	/// for common database table operations. 
	/// </summary>
	/// <remarks>
	/// Do not change this source code. Update the <see cref="CdrAggregateCollection"/>
	/// class if you need to add or change some functionality.
	/// </remarks>
	public abstract class CdrAggregateCollection_Base
	{
		// Constants
		public const string Date_hourColumnName = "date_hour";
		public const string Node_idColumnName = "node_id";
		public const string Orig_end_point_IPColumnName = "orig_end_point_IP";
		public const string Orig_end_point_idColumnName = "orig_end_point_id";
		public const string Customer_acct_idColumnName = "customer_acct_id";
		public const string Customer_route_idColumnName = "customer_route_id";
		public const string Term_end_point_IPColumnName = "term_end_point_IP";
		public const string Term_end_point_idColumnName = "term_end_point_id";
		public const string Calls_attemptedColumnName = "calls_attempted";
		public const string Calls_completedColumnName = "calls_completed";
		public const string Setup_secondsColumnName = "setup_seconds";
		public const string Alert_secondsColumnName = "alert_seconds";
		public const string Connected_secondsColumnName = "connected_seconds";
		public const string Connected_minutesColumnName = "connected_minutes";
		public const string Carrier_costColumnName = "carrier_cost";
		public const string Carrier_rounded_minutesColumnName = "carrier_rounded_minutes";
		public const string Wholesale_priceColumnName = "wholesale_price";
		public const string Wholesale_rounded_minutesColumnName = "wholesale_rounded_minutes";
		public const string End_user_priceColumnName = "end_user_price";
		public const string End_user_rounded_minutesColumnName = "end_user_rounded_minutes";
		public const string Carrier_acct_idColumnName = "carrier_acct_id";
		public const string Carrier_route_idColumnName = "carrier_route_id";
		public const string Access_numberColumnName = "access_number";

		// Instance fields
		private Rbr_Db _db;

		/// <summary>
		/// Initializes a new instance of the <see cref="CdrAggregateCollection_Base"/> 
		/// class with the specified <see cref="Rbr_Db"/>.
		/// </summary>
		/// <param name="db">The <see cref="Rbr_Db"/> object.</param>
		public CdrAggregateCollection_Base(Rbr_Db db)
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
		/// Gets an array of all records from the <c>CdrAggregate</c> table.
		/// </summary>
		/// <returns>An array of <see cref="CdrAggregateRow"/> objects.</returns>
		public virtual CdrAggregateRow[] GetAll()
		{
			return MapRecords(CreateGetAllCommand());
		}

		/// <summary>
		/// Gets a <see cref="System.Data.DataTable"/> object that 
		/// includes all records from the <c>CdrAggregate</c> table.
		/// </summary>
		/// <returns>A reference to the <see cref="System.Data.DataTable"/> object.</returns>
		public virtual DataTable GetAllAsDataTable()
		{
			return MapRecordsToDataTable(CreateGetAllCommand());
		}

		/// <summary>
		/// Creates and returns an <see cref="System.Data.IDbCommand"/> object that is used
		/// to retrieve all records from the <c>CdrAggregate</c> table.
		/// </summary>
		/// <returns>A reference to the <see cref="System.Data.IDbCommand"/> object.</returns>
		protected virtual IDbCommand CreateGetAllCommand()
		{
			return CreateGetCommand(null, null);
		}

		/// <summary>
		/// Gets the first <see cref="CdrAggregateRow"/> objects that 
		/// match the search condition.
		/// </summary>
		/// <param name="whereSql">The SQL search condition. For example: 
		/// <c>"FirstName='Smith' AND Zip=75038"</c>.</param>
		/// <returns>An instance of <see cref="CdrAggregateRow"/> or null reference 
		/// (Nothing in Visual Basic) if the object was not found.</returns>
		public CdrAggregateRow GetRow(string whereSql)
		{
			int totalRecordCount = -1;
			CdrAggregateRow[] rows = GetAsArray(whereSql, null, 0, 1, ref totalRecordCount);
			return 0 == rows.Length ? null : rows[0];
		}

		/// <summary>
		/// Gets an array of <see cref="CdrAggregateRow"/> objects that 
		/// match the search condition, in the the specified sort order.
		/// </summary>
		/// <param name="whereSql">The SQL search condition. For example: 
		/// <c>"FirstName='Smith' AND Zip=75038"</c>.</param>
		/// <param name="orderBySql">The column name(s) followed by "ASC" (ascending) or "DESC" (descending).
		/// Columns are sorted in ascending order by default. For example: <c>"LastName ASC, FirstName ASC"</c>.</param>
		/// <returns>An array of <see cref="CdrAggregateRow"/> objects.</returns>
		public CdrAggregateRow[] GetAsArray(string whereSql, string orderBySql)
		{
			int totalRecordCount = -1;
			return GetAsArray(whereSql, orderBySql, 0, int.MaxValue, ref totalRecordCount);
		}

		/// <summary>
		/// Gets an array of <see cref="CdrAggregateRow"/> objects that 
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
		/// <returns>An array of <see cref="CdrAggregateRow"/> objects.</returns>
		public virtual CdrAggregateRow[] GetAsArray(string whereSql, string orderBySql,
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
			string sql = "SELECT * FROM [dbo].[CdrAggregate]";
			if(null != whereSql && 0 < whereSql.Length)
				sql += " WHERE " + whereSql;
			if(null != orderBySql && 0 < orderBySql.Length)
				sql += " ORDER BY " + orderBySql;
			return _db.CreateCommand(sql);
		}

		/// <summary>
		/// Adds a new record into the <c>CdrAggregate</c> table.
		/// </summary>
		/// <param name="value">The <see cref="CdrAggregateRow"/> object to be inserted.</param>
		public virtual void Insert(CdrAggregateRow value)
		{
			string sqlStr = "INSERT INTO [dbo].[CdrAggregate] (" +
				"[date_hour], " +
				"[node_id], " +
				"[orig_end_point_IP], " +
				"[orig_end_point_id], " +
				"[customer_acct_id], " +
				"[customer_route_id], " +
				"[term_end_point_IP], " +
				"[term_end_point_id], " +
				"[calls_attempted], " +
				"[calls_completed], " +
				"[setup_seconds], " +
				"[alert_seconds], " +
				"[connected_seconds], " +
				"[connected_minutes], " +
				"[carrier_cost], " +
				"[carrier_rounded_minutes], " +
				"[wholesale_price], " +
				"[wholesale_rounded_minutes], " +
				"[end_user_price], " +
				"[end_user_rounded_minutes], " +
				"[carrier_acct_id], " +
				"[carrier_route_id], " +
				"[access_number]" +
				") VALUES (" +
				_db.CreateSqlParameterName("Date_hour") + ", " +
				_db.CreateSqlParameterName("Node_id") + ", " +
				_db.CreateSqlParameterName("Orig_end_point_IP") + ", " +
				_db.CreateSqlParameterName("Orig_end_point_id") + ", " +
				_db.CreateSqlParameterName("Customer_acct_id") + ", " +
				_db.CreateSqlParameterName("Customer_route_id") + ", " +
				_db.CreateSqlParameterName("Term_end_point_IP") + ", " +
				_db.CreateSqlParameterName("Term_end_point_id") + ", " +
				_db.CreateSqlParameterName("Calls_attempted") + ", " +
				_db.CreateSqlParameterName("Calls_completed") + ", " +
				_db.CreateSqlParameterName("Setup_seconds") + ", " +
				_db.CreateSqlParameterName("Alert_seconds") + ", " +
				_db.CreateSqlParameterName("Connected_seconds") + ", " +
				_db.CreateSqlParameterName("Connected_minutes") + ", " +
				_db.CreateSqlParameterName("Carrier_cost") + ", " +
				_db.CreateSqlParameterName("Carrier_rounded_minutes") + ", " +
				_db.CreateSqlParameterName("Wholesale_price") + ", " +
				_db.CreateSqlParameterName("Wholesale_rounded_minutes") + ", " +
				_db.CreateSqlParameterName("End_user_price") + ", " +
				_db.CreateSqlParameterName("End_user_rounded_minutes") + ", " +
				_db.CreateSqlParameterName("Carrier_acct_id") + ", " +
				_db.CreateSqlParameterName("Carrier_route_id") + ", " +
				_db.CreateSqlParameterName("Access_number") + ")";
			IDbCommand cmd = _db.CreateCommand(sqlStr);
			AddParameter(cmd, "Date_hour", value.Date_hour);
			AddParameter(cmd, "Node_id", value.Node_id);
			AddParameter(cmd, "Orig_end_point_IP", value.Orig_end_point_IP);
			AddParameter(cmd, "Orig_end_point_id", value.Orig_end_point_id);
			AddParameter(cmd, "Customer_acct_id", value.Customer_acct_id);
			AddParameter(cmd, "Customer_route_id", value.Customer_route_id);
			AddParameter(cmd, "Term_end_point_IP", value.Term_end_point_IP);
			AddParameter(cmd, "Term_end_point_id", value.Term_end_point_id);
			AddParameter(cmd, "Calls_attempted", value.Calls_attempted);
			AddParameter(cmd, "Calls_completed", value.Calls_completed);
			AddParameter(cmd, "Setup_seconds", value.Setup_seconds);
			AddParameter(cmd, "Alert_seconds", value.Alert_seconds);
			AddParameter(cmd, "Connected_seconds", value.Connected_seconds);
			AddParameter(cmd, "Connected_minutes", value.Connected_minutes);
			AddParameter(cmd, "Carrier_cost", value.Carrier_cost);
			AddParameter(cmd, "Carrier_rounded_minutes", value.Carrier_rounded_minutes);
			AddParameter(cmd, "Wholesale_price", value.Wholesale_price);
			AddParameter(cmd, "Wholesale_rounded_minutes", value.Wholesale_rounded_minutes);
			AddParameter(cmd, "End_user_price", value.End_user_price);
			AddParameter(cmd, "End_user_rounded_minutes", value.End_user_rounded_minutes);
			AddParameter(cmd, "Carrier_acct_id", value.Carrier_acct_id);
			AddParameter(cmd, "Carrier_route_id", value.Carrier_route_id);
			AddParameter(cmd, "Access_number", value.Access_number);
			cmd.ExecuteNonQuery();
		}

		/// <summary>
		/// Deletes <c>CdrAggregate</c> records that match the specified criteria.
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
		/// to delete <c>CdrAggregate</c> records that match the specified criteria.
		/// </summary>
		/// <param name="whereSql">The SQL search condition. 
		/// For example: <c>"FirstName='Smith' AND Zip=75038"</c>.</param>
		/// <returns>A reference to the <see cref="System.Data.IDbCommand"/> object.</returns>
		protected virtual IDbCommand CreateDeleteCommand(string whereSql)
		{
			string sql = "DELETE FROM [dbo].[CdrAggregate]";
			if(null != whereSql && 0 < whereSql.Length)
				sql += " WHERE " + whereSql;
			return _db.CreateCommand(sql);
		}

		/// <summary>
		/// Deletes all records from the <c>CdrAggregate</c> table.
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
		/// <returns>An array of <see cref="CdrAggregateRow"/> objects.</returns>
		protected CdrAggregateRow[] MapRecords(IDbCommand command)
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
		/// <returns>An array of <see cref="CdrAggregateRow"/> objects.</returns>
		protected CdrAggregateRow[] MapRecords(IDataReader reader)
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
		/// <returns>An array of <see cref="CdrAggregateRow"/> objects.</returns>
		protected virtual CdrAggregateRow[] MapRecords(IDataReader reader, 
										int startIndex, int length, ref int totalRecordCount)
		{
			if(0 > startIndex)
				throw new ArgumentOutOfRangeException("startIndex", startIndex, "StartIndex cannot be less than zero.");
			if(0 > length)
				throw new ArgumentOutOfRangeException("length", length, "Length cannot be less than zero.");

			int date_hourColumnIndex = reader.GetOrdinal("date_hour");
			int node_idColumnIndex = reader.GetOrdinal("node_id");
			int orig_end_point_IPColumnIndex = reader.GetOrdinal("orig_end_point_IP");
			int orig_end_point_idColumnIndex = reader.GetOrdinal("orig_end_point_id");
			int customer_acct_idColumnIndex = reader.GetOrdinal("customer_acct_id");
			int customer_route_idColumnIndex = reader.GetOrdinal("customer_route_id");
			int term_end_point_IPColumnIndex = reader.GetOrdinal("term_end_point_IP");
			int term_end_point_idColumnIndex = reader.GetOrdinal("term_end_point_id");
			int calls_attemptedColumnIndex = reader.GetOrdinal("calls_attempted");
			int calls_completedColumnIndex = reader.GetOrdinal("calls_completed");
			int setup_secondsColumnIndex = reader.GetOrdinal("setup_seconds");
			int alert_secondsColumnIndex = reader.GetOrdinal("alert_seconds");
			int connected_secondsColumnIndex = reader.GetOrdinal("connected_seconds");
			int connected_minutesColumnIndex = reader.GetOrdinal("connected_minutes");
			int carrier_costColumnIndex = reader.GetOrdinal("carrier_cost");
			int carrier_rounded_minutesColumnIndex = reader.GetOrdinal("carrier_rounded_minutes");
			int wholesale_priceColumnIndex = reader.GetOrdinal("wholesale_price");
			int wholesale_rounded_minutesColumnIndex = reader.GetOrdinal("wholesale_rounded_minutes");
			int end_user_priceColumnIndex = reader.GetOrdinal("end_user_price");
			int end_user_rounded_minutesColumnIndex = reader.GetOrdinal("end_user_rounded_minutes");
			int carrier_acct_idColumnIndex = reader.GetOrdinal("carrier_acct_id");
			int carrier_route_idColumnIndex = reader.GetOrdinal("carrier_route_id");
			int access_numberColumnIndex = reader.GetOrdinal("access_number");

			System.Collections.ArrayList recordList = new System.Collections.ArrayList();
			int ri = -startIndex;
			while(reader.Read())
			{
				ri++;
				if(ri > 0 && ri <= length)
				{
					CdrAggregateRow record = new CdrAggregateRow();
					recordList.Add(record);

					record.Date_hour = Convert.ToInt32(reader.GetValue(date_hourColumnIndex));
					record.Node_id = Convert.ToInt16(reader.GetValue(node_idColumnIndex));
					record.Orig_end_point_IP = Convert.ToInt32(reader.GetValue(orig_end_point_IPColumnIndex));
					record.Orig_end_point_id = Convert.ToInt16(reader.GetValue(orig_end_point_idColumnIndex));
					record.Customer_acct_id = Convert.ToInt16(reader.GetValue(customer_acct_idColumnIndex));
					record.Customer_route_id = Convert.ToInt32(reader.GetValue(customer_route_idColumnIndex));
					record.Term_end_point_IP = Convert.ToInt32(reader.GetValue(term_end_point_IPColumnIndex));
					record.Term_end_point_id = Convert.ToInt16(reader.GetValue(term_end_point_idColumnIndex));
					record.Calls_attempted = Convert.ToInt32(reader.GetValue(calls_attemptedColumnIndex));
					record.Calls_completed = Convert.ToInt32(reader.GetValue(calls_completedColumnIndex));
					record.Setup_seconds = Convert.ToInt32(reader.GetValue(setup_secondsColumnIndex));
					record.Alert_seconds = Convert.ToInt32(reader.GetValue(alert_secondsColumnIndex));
					record.Connected_seconds = Convert.ToInt32(reader.GetValue(connected_secondsColumnIndex));
					record.Connected_minutes = Convert.ToDecimal(reader.GetValue(connected_minutesColumnIndex));
					record.Carrier_cost = Convert.ToDecimal(reader.GetValue(carrier_costColumnIndex));
					record.Carrier_rounded_minutes = Convert.ToDecimal(reader.GetValue(carrier_rounded_minutesColumnIndex));
					record.Wholesale_price = Convert.ToDecimal(reader.GetValue(wholesale_priceColumnIndex));
					record.Wholesale_rounded_minutes = Convert.ToDecimal(reader.GetValue(wholesale_rounded_minutesColumnIndex));
					record.End_user_price = Convert.ToDecimal(reader.GetValue(end_user_priceColumnIndex));
					record.End_user_rounded_minutes = Convert.ToDecimal(reader.GetValue(end_user_rounded_minutesColumnIndex));
					record.Carrier_acct_id = Convert.ToInt16(reader.GetValue(carrier_acct_idColumnIndex));
					record.Carrier_route_id = Convert.ToInt32(reader.GetValue(carrier_route_idColumnIndex));
					record.Access_number = Convert.ToInt64(reader.GetValue(access_numberColumnIndex));

					if(ri == length && 0 != totalRecordCount)
						break;
				}
			}

			totalRecordCount = 0 == totalRecordCount ? ri + startIndex : -1;
			return (CdrAggregateRow[])(recordList.ToArray(typeof(CdrAggregateRow)));
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
		/// Converts <see cref="System.Data.DataRow"/> to <see cref="CdrAggregateRow"/>.
		/// </summary>
		/// <param name="row">The <see cref="System.Data.DataRow"/> object to be mapped.</param>
		/// <returns>A reference to the <see cref="CdrAggregateRow"/> object.</returns>
		protected virtual CdrAggregateRow MapRow(DataRow row)
		{
			CdrAggregateRow mappedObject = new CdrAggregateRow();
			DataTable dataTable = row.Table;
			DataColumn dataColumn;
			// Column "Date_hour"
			dataColumn = dataTable.Columns["Date_hour"];
			if(!row.IsNull(dataColumn))
				mappedObject.Date_hour = (int)row[dataColumn];
			// Column "Node_id"
			dataColumn = dataTable.Columns["Node_id"];
			if(!row.IsNull(dataColumn))
				mappedObject.Node_id = (short)row[dataColumn];
			// Column "Orig_end_point_IP"
			dataColumn = dataTable.Columns["Orig_end_point_IP"];
			if(!row.IsNull(dataColumn))
				mappedObject.Orig_end_point_IP = (int)row[dataColumn];
			// Column "Orig_end_point_id"
			dataColumn = dataTable.Columns["Orig_end_point_id"];
			if(!row.IsNull(dataColumn))
				mappedObject.Orig_end_point_id = (short)row[dataColumn];
			// Column "Customer_acct_id"
			dataColumn = dataTable.Columns["Customer_acct_id"];
			if(!row.IsNull(dataColumn))
				mappedObject.Customer_acct_id = (short)row[dataColumn];
			// Column "Customer_route_id"
			dataColumn = dataTable.Columns["Customer_route_id"];
			if(!row.IsNull(dataColumn))
				mappedObject.Customer_route_id = (int)row[dataColumn];
			// Column "Term_end_point_IP"
			dataColumn = dataTable.Columns["Term_end_point_IP"];
			if(!row.IsNull(dataColumn))
				mappedObject.Term_end_point_IP = (int)row[dataColumn];
			// Column "Term_end_point_id"
			dataColumn = dataTable.Columns["Term_end_point_id"];
			if(!row.IsNull(dataColumn))
				mappedObject.Term_end_point_id = (short)row[dataColumn];
			// Column "Calls_attempted"
			dataColumn = dataTable.Columns["Calls_attempted"];
			if(!row.IsNull(dataColumn))
				mappedObject.Calls_attempted = (int)row[dataColumn];
			// Column "Calls_completed"
			dataColumn = dataTable.Columns["Calls_completed"];
			if(!row.IsNull(dataColumn))
				mappedObject.Calls_completed = (int)row[dataColumn];
			// Column "Setup_seconds"
			dataColumn = dataTable.Columns["Setup_seconds"];
			if(!row.IsNull(dataColumn))
				mappedObject.Setup_seconds = (int)row[dataColumn];
			// Column "Alert_seconds"
			dataColumn = dataTable.Columns["Alert_seconds"];
			if(!row.IsNull(dataColumn))
				mappedObject.Alert_seconds = (int)row[dataColumn];
			// Column "Connected_seconds"
			dataColumn = dataTable.Columns["Connected_seconds"];
			if(!row.IsNull(dataColumn))
				mappedObject.Connected_seconds = (int)row[dataColumn];
			// Column "Connected_minutes"
			dataColumn = dataTable.Columns["Connected_minutes"];
			if(!row.IsNull(dataColumn))
				mappedObject.Connected_minutes = (decimal)row[dataColumn];
			// Column "Carrier_cost"
			dataColumn = dataTable.Columns["Carrier_cost"];
			if(!row.IsNull(dataColumn))
				mappedObject.Carrier_cost = (decimal)row[dataColumn];
			// Column "Carrier_rounded_minutes"
			dataColumn = dataTable.Columns["Carrier_rounded_minutes"];
			if(!row.IsNull(dataColumn))
				mappedObject.Carrier_rounded_minutes = (decimal)row[dataColumn];
			// Column "Wholesale_price"
			dataColumn = dataTable.Columns["Wholesale_price"];
			if(!row.IsNull(dataColumn))
				mappedObject.Wholesale_price = (decimal)row[dataColumn];
			// Column "Wholesale_rounded_minutes"
			dataColumn = dataTable.Columns["Wholesale_rounded_minutes"];
			if(!row.IsNull(dataColumn))
				mappedObject.Wholesale_rounded_minutes = (decimal)row[dataColumn];
			// Column "End_user_price"
			dataColumn = dataTable.Columns["End_user_price"];
			if(!row.IsNull(dataColumn))
				mappedObject.End_user_price = (decimal)row[dataColumn];
			// Column "End_user_rounded_minutes"
			dataColumn = dataTable.Columns["End_user_rounded_minutes"];
			if(!row.IsNull(dataColumn))
				mappedObject.End_user_rounded_minutes = (decimal)row[dataColumn];
			// Column "Carrier_acct_id"
			dataColumn = dataTable.Columns["Carrier_acct_id"];
			if(!row.IsNull(dataColumn))
				mappedObject.Carrier_acct_id = (short)row[dataColumn];
			// Column "Carrier_route_id"
			dataColumn = dataTable.Columns["Carrier_route_id"];
			if(!row.IsNull(dataColumn))
				mappedObject.Carrier_route_id = (int)row[dataColumn];
			// Column "Access_number"
			dataColumn = dataTable.Columns["Access_number"];
			if(!row.IsNull(dataColumn))
				mappedObject.Access_number = (long)row[dataColumn];
			return mappedObject;
		}

		/// <summary>
		/// Creates a <see cref="System.Data.DataTable"/> object for 
		/// the <c>CdrAggregate</c> table.
		/// </summary>
		/// <returns>A reference to the <see cref="System.Data.DataTable"/> object.</returns>
		protected virtual DataTable CreateDataTable()
		{
			DataTable dataTable = new DataTable();
			dataTable.TableName = "CdrAggregate";
			DataColumn dataColumn;
			dataColumn = dataTable.Columns.Add("Date_hour", typeof(int));
			dataColumn.Caption = "date_hour";
			dataColumn.AllowDBNull = false;
			dataColumn = dataTable.Columns.Add("Node_id", typeof(short));
			dataColumn.Caption = "node_id";
			dataColumn.AllowDBNull = false;
			dataColumn = dataTable.Columns.Add("Orig_end_point_IP", typeof(int));
			dataColumn.Caption = "orig_end_point_IP";
			dataColumn.AllowDBNull = false;
			dataColumn = dataTable.Columns.Add("Orig_end_point_id", typeof(short));
			dataColumn.Caption = "orig_end_point_id";
			dataColumn.AllowDBNull = false;
			dataColumn = dataTable.Columns.Add("Customer_acct_id", typeof(short));
			dataColumn.Caption = "customer_acct_id";
			dataColumn.AllowDBNull = false;
			dataColumn = dataTable.Columns.Add("Customer_route_id", typeof(int));
			dataColumn.Caption = "customer_route_id";
			dataColumn.AllowDBNull = false;
			dataColumn = dataTable.Columns.Add("Term_end_point_IP", typeof(int));
			dataColumn.Caption = "term_end_point_IP";
			dataColumn.AllowDBNull = false;
			dataColumn = dataTable.Columns.Add("Term_end_point_id", typeof(short));
			dataColumn.Caption = "term_end_point_id";
			dataColumn.AllowDBNull = false;
			dataColumn = dataTable.Columns.Add("Calls_attempted", typeof(int));
			dataColumn.Caption = "calls_attempted";
			dataColumn.AllowDBNull = false;
			dataColumn = dataTable.Columns.Add("Calls_completed", typeof(int));
			dataColumn.Caption = "calls_completed";
			dataColumn.AllowDBNull = false;
			dataColumn = dataTable.Columns.Add("Setup_seconds", typeof(int));
			dataColumn.Caption = "setup_seconds";
			dataColumn.AllowDBNull = false;
			dataColumn = dataTable.Columns.Add("Alert_seconds", typeof(int));
			dataColumn.Caption = "alert_seconds";
			dataColumn.AllowDBNull = false;
			dataColumn = dataTable.Columns.Add("Connected_seconds", typeof(int));
			dataColumn.Caption = "connected_seconds";
			dataColumn.AllowDBNull = false;
			dataColumn = dataTable.Columns.Add("Connected_minutes", typeof(decimal));
			dataColumn.Caption = "connected_minutes";
			dataColumn.AllowDBNull = false;
			dataColumn = dataTable.Columns.Add("Carrier_cost", typeof(decimal));
			dataColumn.Caption = "carrier_cost";
			dataColumn.AllowDBNull = false;
			dataColumn = dataTable.Columns.Add("Carrier_rounded_minutes", typeof(decimal));
			dataColumn.Caption = "carrier_rounded_minutes";
			dataColumn.AllowDBNull = false;
			dataColumn = dataTable.Columns.Add("Wholesale_price", typeof(decimal));
			dataColumn.Caption = "wholesale_price";
			dataColumn.AllowDBNull = false;
			dataColumn = dataTable.Columns.Add("Wholesale_rounded_minutes", typeof(decimal));
			dataColumn.Caption = "wholesale_rounded_minutes";
			dataColumn.AllowDBNull = false;
			dataColumn = dataTable.Columns.Add("End_user_price", typeof(decimal));
			dataColumn.Caption = "end_user_price";
			dataColumn.AllowDBNull = false;
			dataColumn = dataTable.Columns.Add("End_user_rounded_minutes", typeof(decimal));
			dataColumn.Caption = "end_user_rounded_minutes";
			dataColumn.AllowDBNull = false;
			dataColumn = dataTable.Columns.Add("Carrier_acct_id", typeof(short));
			dataColumn.Caption = "carrier_acct_id";
			dataColumn.AllowDBNull = false;
			dataColumn = dataTable.Columns.Add("Carrier_route_id", typeof(int));
			dataColumn.Caption = "carrier_route_id";
			dataColumn.AllowDBNull = false;
			dataColumn = dataTable.Columns.Add("Access_number", typeof(long));
			dataColumn.Caption = "access_number";
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
				case "Date_hour":
					parameter = _db.AddParameter(cmd, paramName, DbType.Int32, value);
					break;

				case "Node_id":
					parameter = _db.AddParameter(cmd, paramName, DbType.Int16, value);
					break;

				case "Orig_end_point_IP":
					parameter = _db.AddParameter(cmd, paramName, DbType.Int32, value);
					break;

				case "Orig_end_point_id":
					parameter = _db.AddParameter(cmd, paramName, DbType.Int16, value);
					break;

				case "Customer_acct_id":
					parameter = _db.AddParameter(cmd, paramName, DbType.Int16, value);
					break;

				case "Customer_route_id":
					parameter = _db.AddParameter(cmd, paramName, DbType.Int32, value);
					break;

				case "Term_end_point_IP":
					parameter = _db.AddParameter(cmd, paramName, DbType.Int32, value);
					break;

				case "Term_end_point_id":
					parameter = _db.AddParameter(cmd, paramName, DbType.Int16, value);
					break;

				case "Calls_attempted":
					parameter = _db.AddParameter(cmd, paramName, DbType.Int32, value);
					break;

				case "Calls_completed":
					parameter = _db.AddParameter(cmd, paramName, DbType.Int32, value);
					break;

				case "Setup_seconds":
					parameter = _db.AddParameter(cmd, paramName, DbType.Int32, value);
					break;

				case "Alert_seconds":
					parameter = _db.AddParameter(cmd, paramName, DbType.Int32, value);
					break;

				case "Connected_seconds":
					parameter = _db.AddParameter(cmd, paramName, DbType.Int32, value);
					break;

				case "Connected_minutes":
					parameter = _db.AddParameter(cmd, paramName, DbType.Decimal, value);
					break;

				case "Carrier_cost":
					parameter = _db.AddParameter(cmd, paramName, DbType.Decimal, value);
					break;

				case "Carrier_rounded_minutes":
					parameter = _db.AddParameter(cmd, paramName, DbType.Decimal, value);
					break;

				case "Wholesale_price":
					parameter = _db.AddParameter(cmd, paramName, DbType.Decimal, value);
					break;

				case "Wholesale_rounded_minutes":
					parameter = _db.AddParameter(cmd, paramName, DbType.Decimal, value);
					break;

				case "End_user_price":
					parameter = _db.AddParameter(cmd, paramName, DbType.Decimal, value);
					break;

				case "End_user_rounded_minutes":
					parameter = _db.AddParameter(cmd, paramName, DbType.Decimal, value);
					break;

				case "Carrier_acct_id":
					parameter = _db.AddParameter(cmd, paramName, DbType.Int16, value);
					break;

				case "Carrier_route_id":
					parameter = _db.AddParameter(cmd, paramName, DbType.Int32, value);
					break;

				case "Access_number":
					parameter = _db.AddParameter(cmd, paramName, DbType.Int64, value);
					break;

				default:
					throw new ArgumentException("Unknown parameter name (" + paramName + ").");
			}
			return parameter;
		}

	} // End of CdrAggregateCollection_Base class
}  // End of namespace

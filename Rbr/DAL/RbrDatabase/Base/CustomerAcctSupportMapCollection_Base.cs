// <fileinfo name="Base\CustomerAcctSupportMapCollection_Base.cs">
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
	/// The base class for <see cref="CustomerAcctSupportMapCollection"/>. Provides methods 
	/// for common database table operations. 
	/// </summary>
	/// <remarks>
	/// Do not change this source code. Update the <see cref="CustomerAcctSupportMapCollection"/>
	/// class if you need to add or change some functionality.
	/// </remarks>
	public abstract class CustomerAcctSupportMapCollection_Base
	{
		// Constants
		public const string Customer_acct_idColumnName = "customer_acct_id";
		public const string Vendor_idColumnName = "vendor_id";

		// Instance fields
		private Rbr_Db _db;

		/// <summary>
		/// Initializes a new instance of the <see cref="CustomerAcctSupportMapCollection_Base"/> 
		/// class with the specified <see cref="Rbr_Db"/>.
		/// </summary>
		/// <param name="db">The <see cref="Rbr_Db"/> object.</param>
		public CustomerAcctSupportMapCollection_Base(Rbr_Db db)
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
		/// Gets an array of all records from the <c>CustomerAcctSupportMap</c> table.
		/// </summary>
		/// <returns>An array of <see cref="CustomerAcctSupportMapRow"/> objects.</returns>
		public virtual CustomerAcctSupportMapRow[] GetAll()
		{
			return MapRecords(CreateGetAllCommand());
		}

		/// <summary>
		/// Gets a <see cref="System.Data.DataTable"/> object that 
		/// includes all records from the <c>CustomerAcctSupportMap</c> table.
		/// </summary>
		/// <returns>A reference to the <see cref="System.Data.DataTable"/> object.</returns>
		public virtual DataTable GetAllAsDataTable()
		{
			return MapRecordsToDataTable(CreateGetAllCommand());
		}

		/// <summary>
		/// Creates and returns an <see cref="System.Data.IDbCommand"/> object that is used
		/// to retrieve all records from the <c>CustomerAcctSupportMap</c> table.
		/// </summary>
		/// <returns>A reference to the <see cref="System.Data.IDbCommand"/> object.</returns>
		protected virtual IDbCommand CreateGetAllCommand()
		{
			return CreateGetCommand(null, null);
		}

		/// <summary>
		/// Gets the first <see cref="CustomerAcctSupportMapRow"/> objects that 
		/// match the search condition.
		/// </summary>
		/// <param name="whereSql">The SQL search condition. For example: 
		/// <c>"FirstName='Smith' AND Zip=75038"</c>.</param>
		/// <returns>An instance of <see cref="CustomerAcctSupportMapRow"/> or null reference 
		/// (Nothing in Visual Basic) if the object was not found.</returns>
		public CustomerAcctSupportMapRow GetRow(string whereSql)
		{
			int totalRecordCount = -1;
			CustomerAcctSupportMapRow[] rows = GetAsArray(whereSql, null, 0, 1, ref totalRecordCount);
			return 0 == rows.Length ? null : rows[0];
		}

		/// <summary>
		/// Gets an array of <see cref="CustomerAcctSupportMapRow"/> objects that 
		/// match the search condition, in the the specified sort order.
		/// </summary>
		/// <param name="whereSql">The SQL search condition. For example: 
		/// <c>"FirstName='Smith' AND Zip=75038"</c>.</param>
		/// <param name="orderBySql">The column name(s) followed by "ASC" (ascending) or "DESC" (descending).
		/// Columns are sorted in ascending order by default. For example: <c>"LastName ASC, FirstName ASC"</c>.</param>
		/// <returns>An array of <see cref="CustomerAcctSupportMapRow"/> objects.</returns>
		public CustomerAcctSupportMapRow[] GetAsArray(string whereSql, string orderBySql)
		{
			int totalRecordCount = -1;
			return GetAsArray(whereSql, orderBySql, 0, int.MaxValue, ref totalRecordCount);
		}

		/// <summary>
		/// Gets an array of <see cref="CustomerAcctSupportMapRow"/> objects that 
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
		/// <returns>An array of <see cref="CustomerAcctSupportMapRow"/> objects.</returns>
		public virtual CustomerAcctSupportMapRow[] GetAsArray(string whereSql, string orderBySql,
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
			string sql = "SELECT * FROM [dbo].[CustomerAcctSupportMap]";
			if(null != whereSql && 0 < whereSql.Length)
				sql += " WHERE " + whereSql;
			if(null != orderBySql && 0 < orderBySql.Length)
				sql += " ORDER BY " + orderBySql;
			return _db.CreateCommand(sql);
		}

		/// <summary>
		/// Gets <see cref="CustomerAcctSupportMapRow"/> by the primary key.
		/// </summary>
		/// <param name="customer_acct_id">The <c>customer_acct_id</c> column value.</param>
		/// <param name="vendor_id">The <c>vendor_id</c> column value.</param>
		/// <returns>An instance of <see cref="CustomerAcctSupportMapRow"/> or null reference 
		/// (Nothing in Visual Basic) if the object was not found.</returns>
		public virtual CustomerAcctSupportMapRow GetByPrimaryKey(short customer_acct_id, int vendor_id)
		{
			string whereSql = "[customer_acct_id]=" + _db.CreateSqlParameterName("Customer_acct_id") + " AND " +
							  "[vendor_id]=" + _db.CreateSqlParameterName("Vendor_id");
			IDbCommand cmd = CreateGetCommand(whereSql, null);
			AddParameter(cmd, "Customer_acct_id", customer_acct_id);
			AddParameter(cmd, "Vendor_id", vendor_id);
			CustomerAcctSupportMapRow[] tempArray = MapRecords(cmd);
			return 0 == tempArray.Length ? null : tempArray[0];
		}

		/// <summary>
		/// Gets an array of <see cref="CustomerAcctSupportMapRow"/> objects 
		/// by the <c>R_257</c> foreign key.
		/// </summary>
		/// <param name="customer_acct_id">The <c>customer_acct_id</c> column value.</param>
		/// <returns>An array of <see cref="CustomerAcctSupportMapRow"/> objects.</returns>
		public virtual CustomerAcctSupportMapRow[] GetByCustomer_acct_id(short customer_acct_id)
		{
			return MapRecords(CreateGetByCustomer_acct_idCommand(customer_acct_id));
		}

		/// <summary>
		/// Gets a <see cref="System.Data.DataTable"/> object 
		/// by the <c>R_257</c> foreign key.
		/// </summary>
		/// <param name="customer_acct_id">The <c>customer_acct_id</c> column value.</param>
		/// <returns>A reference to the <see cref="System.Data.DataTable"/> object.</returns>
		public virtual DataTable GetByCustomer_acct_idAsDataTable(short customer_acct_id)
		{
			return MapRecordsToDataTable(CreateGetByCustomer_acct_idCommand(customer_acct_id));
		}

		/// <summary>
		/// Creates an <see cref="System.Data.IDbCommand"/> object that can be used to 
		/// return records by the <c>R_257</c> foreign key.
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
		/// Gets an array of <see cref="CustomerAcctSupportMapRow"/> objects 
		/// by the <c>R_315</c> foreign key.
		/// </summary>
		/// <param name="vendor_id">The <c>vendor_id</c> column value.</param>
		/// <returns>An array of <see cref="CustomerAcctSupportMapRow"/> objects.</returns>
		public virtual CustomerAcctSupportMapRow[] GetByVendor_id(int vendor_id)
		{
			return MapRecords(CreateGetByVendor_idCommand(vendor_id));
		}

		/// <summary>
		/// Gets a <see cref="System.Data.DataTable"/> object 
		/// by the <c>R_315</c> foreign key.
		/// </summary>
		/// <param name="vendor_id">The <c>vendor_id</c> column value.</param>
		/// <returns>A reference to the <see cref="System.Data.DataTable"/> object.</returns>
		public virtual DataTable GetByVendor_idAsDataTable(int vendor_id)
		{
			return MapRecordsToDataTable(CreateGetByVendor_idCommand(vendor_id));
		}

		/// <summary>
		/// Creates an <see cref="System.Data.IDbCommand"/> object that can be used to 
		/// return records by the <c>R_315</c> foreign key.
		/// </summary>
		/// <param name="vendor_id">The <c>vendor_id</c> column value.</param>
		/// <returns>A reference to the <see cref="System.Data.IDbCommand"/> object.</returns>
		protected virtual IDbCommand CreateGetByVendor_idCommand(int vendor_id)
		{
			string whereSql = "";
			whereSql += "[vendor_id]=" + _db.CreateSqlParameterName("Vendor_id");

			IDbCommand cmd = CreateGetCommand(whereSql, null);
			AddParameter(cmd, "Vendor_id", vendor_id);
			return cmd;
		}

		/// <summary>
		/// Adds a new record into the <c>CustomerAcctSupportMap</c> table.
		/// </summary>
		/// <param name="value">The <see cref="CustomerAcctSupportMapRow"/> object to be inserted.</param>
		public virtual void Insert(CustomerAcctSupportMapRow value)
		{
			string sqlStr = "INSERT INTO [dbo].[CustomerAcctSupportMap] (" +
				"[customer_acct_id], " +
				"[vendor_id]" +
				") VALUES (" +
				_db.CreateSqlParameterName("Customer_acct_id") + ", " +
				_db.CreateSqlParameterName("Vendor_id") + ")";
			IDbCommand cmd = _db.CreateCommand(sqlStr);
			AddParameter(cmd, "Customer_acct_id", value.Customer_acct_id);
			AddParameter(cmd, "Vendor_id", value.Vendor_id);
			cmd.ExecuteNonQuery();
		}

		/// <summary>
		/// Deletes the specified object from the <c>CustomerAcctSupportMap</c> table.
		/// </summary>
		/// <param name="value">The <see cref="CustomerAcctSupportMapRow"/> object to delete.</param>
		/// <returns>true if the record was deleted; otherwise, false.</returns>
		public bool Delete(CustomerAcctSupportMapRow value)
		{
			return DeleteByPrimaryKey(value.Customer_acct_id, value.Vendor_id);
		}

		/// <summary>
		/// Deletes a record from the <c>CustomerAcctSupportMap</c> table using
		/// the specified primary key.
		/// </summary>
		/// <param name="customer_acct_id">The <c>customer_acct_id</c> column value.</param>
		/// <param name="vendor_id">The <c>vendor_id</c> column value.</param>
		/// <returns>true if the record was deleted; otherwise, false.</returns>
		public virtual bool DeleteByPrimaryKey(short customer_acct_id, int vendor_id)
		{
			string whereSql = "[customer_acct_id]=" + _db.CreateSqlParameterName("Customer_acct_id") + " AND " +
							  "[vendor_id]=" + _db.CreateSqlParameterName("Vendor_id");
			IDbCommand cmd = CreateDeleteCommand(whereSql);
			AddParameter(cmd, "Customer_acct_id", customer_acct_id);
			AddParameter(cmd, "Vendor_id", vendor_id);
			return 0 < cmd.ExecuteNonQuery();
		}

		/// <summary>
		/// Deletes records from the <c>CustomerAcctSupportMap</c> table using the 
		/// <c>R_257</c> foreign key.
		/// </summary>
		/// <param name="customer_acct_id">The <c>customer_acct_id</c> column value.</param>
		/// <returns>The number of records deleted from the table.</returns>
		public int DeleteByCustomer_acct_id(short customer_acct_id)
		{
			return CreateDeleteByCustomer_acct_idCommand(customer_acct_id).ExecuteNonQuery();
		}

		/// <summary>
		/// Creates an <see cref="System.Data.IDbCommand"/> object that can be used to
		/// delete records using the <c>R_257</c> foreign key.
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
		/// Deletes records from the <c>CustomerAcctSupportMap</c> table using the 
		/// <c>R_315</c> foreign key.
		/// </summary>
		/// <param name="vendor_id">The <c>vendor_id</c> column value.</param>
		/// <returns>The number of records deleted from the table.</returns>
		public int DeleteByVendor_id(int vendor_id)
		{
			return CreateDeleteByVendor_idCommand(vendor_id).ExecuteNonQuery();
		}

		/// <summary>
		/// Creates an <see cref="System.Data.IDbCommand"/> object that can be used to
		/// delete records using the <c>R_315</c> foreign key.
		/// </summary>
		/// <param name="vendor_id">The <c>vendor_id</c> column value.</param>
		/// <returns>A reference to the <see cref="System.Data.IDbCommand"/> object.</returns>
		protected virtual IDbCommand CreateDeleteByVendor_idCommand(int vendor_id)
		{
			string whereSql = "";
			whereSql += "[vendor_id]=" + _db.CreateSqlParameterName("Vendor_id");

			IDbCommand cmd = CreateDeleteCommand(whereSql);
			AddParameter(cmd, "Vendor_id", vendor_id);
			return cmd;
		}

		/// <summary>
		/// Deletes <c>CustomerAcctSupportMap</c> records that match the specified criteria.
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
		/// to delete <c>CustomerAcctSupportMap</c> records that match the specified criteria.
		/// </summary>
		/// <param name="whereSql">The SQL search condition. 
		/// For example: <c>"FirstName='Smith' AND Zip=75038"</c>.</param>
		/// <returns>A reference to the <see cref="System.Data.IDbCommand"/> object.</returns>
		protected virtual IDbCommand CreateDeleteCommand(string whereSql)
		{
			string sql = "DELETE FROM [dbo].[CustomerAcctSupportMap]";
			if(null != whereSql && 0 < whereSql.Length)
				sql += " WHERE " + whereSql;
			return _db.CreateCommand(sql);
		}

		/// <summary>
		/// Deletes all records from the <c>CustomerAcctSupportMap</c> table.
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
		/// <returns>An array of <see cref="CustomerAcctSupportMapRow"/> objects.</returns>
		protected CustomerAcctSupportMapRow[] MapRecords(IDbCommand command)
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
		/// <returns>An array of <see cref="CustomerAcctSupportMapRow"/> objects.</returns>
		protected CustomerAcctSupportMapRow[] MapRecords(IDataReader reader)
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
		/// <returns>An array of <see cref="CustomerAcctSupportMapRow"/> objects.</returns>
		protected virtual CustomerAcctSupportMapRow[] MapRecords(IDataReader reader, 
										int startIndex, int length, ref int totalRecordCount)
		{
			if(0 > startIndex)
				throw new ArgumentOutOfRangeException("startIndex", startIndex, "StartIndex cannot be less than zero.");
			if(0 > length)
				throw new ArgumentOutOfRangeException("length", length, "Length cannot be less than zero.");

			int customer_acct_idColumnIndex = reader.GetOrdinal("customer_acct_id");
			int vendor_idColumnIndex = reader.GetOrdinal("vendor_id");

			System.Collections.ArrayList recordList = new System.Collections.ArrayList();
			int ri = -startIndex;
			while(reader.Read())
			{
				ri++;
				if(ri > 0 && ri <= length)
				{
					CustomerAcctSupportMapRow record = new CustomerAcctSupportMapRow();
					recordList.Add(record);

					record.Customer_acct_id = Convert.ToInt16(reader.GetValue(customer_acct_idColumnIndex));
					record.Vendor_id = Convert.ToInt32(reader.GetValue(vendor_idColumnIndex));

					if(ri == length && 0 != totalRecordCount)
						break;
				}
			}

			totalRecordCount = 0 == totalRecordCount ? ri + startIndex : -1;
			return (CustomerAcctSupportMapRow[])(recordList.ToArray(typeof(CustomerAcctSupportMapRow)));
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
		/// Converts <see cref="System.Data.DataRow"/> to <see cref="CustomerAcctSupportMapRow"/>.
		/// </summary>
		/// <param name="row">The <see cref="System.Data.DataRow"/> object to be mapped.</param>
		/// <returns>A reference to the <see cref="CustomerAcctSupportMapRow"/> object.</returns>
		protected virtual CustomerAcctSupportMapRow MapRow(DataRow row)
		{
			CustomerAcctSupportMapRow mappedObject = new CustomerAcctSupportMapRow();
			DataTable dataTable = row.Table;
			DataColumn dataColumn;
			// Column "Customer_acct_id"
			dataColumn = dataTable.Columns["Customer_acct_id"];
			if(!row.IsNull(dataColumn))
				mappedObject.Customer_acct_id = (short)row[dataColumn];
			// Column "Vendor_id"
			dataColumn = dataTable.Columns["Vendor_id"];
			if(!row.IsNull(dataColumn))
				mappedObject.Vendor_id = (int)row[dataColumn];
			return mappedObject;
		}

		/// <summary>
		/// Creates a <see cref="System.Data.DataTable"/> object for 
		/// the <c>CustomerAcctSupportMap</c> table.
		/// </summary>
		/// <returns>A reference to the <see cref="System.Data.DataTable"/> object.</returns>
		protected virtual DataTable CreateDataTable()
		{
			DataTable dataTable = new DataTable();
			dataTable.TableName = "CustomerAcctSupportMap";
			DataColumn dataColumn;
			dataColumn = dataTable.Columns.Add("Customer_acct_id", typeof(short));
			dataColumn.Caption = "customer_acct_id";
			dataColumn.AllowDBNull = false;
			dataColumn = dataTable.Columns.Add("Vendor_id", typeof(int));
			dataColumn.Caption = "vendor_id";
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
				case "Customer_acct_id":
					parameter = _db.AddParameter(cmd, paramName, DbType.Int16, value);
					break;

				case "Vendor_id":
					parameter = _db.AddParameter(cmd, paramName, DbType.Int32, value);
					break;

				default:
					throw new ArgumentException("Unknown parameter name (" + paramName + ").");
			}
			return parameter;
		}

	} // End of CustomerAcctSupportMapCollection_Base class
}  // End of namespace

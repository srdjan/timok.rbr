// <fileinfo name="Base\RetailAccountCollection_Base.cs">
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
	/// The base class for <see cref="RetailAccountCollection"/>. Provides methods 
	/// for common database table operations. 
	/// </summary>
	/// <remarks>
	/// Do not change this source code. Update the <see cref="RetailAccountCollection"/>
	/// class if you need to add or change some functionality.
	/// </remarks>
	public abstract class RetailAccountCollection_Base
	{
		// Constants
		public const string Retail_acct_idColumnName = "retail_acct_id";
		public const string Customer_acct_idColumnName = "customer_acct_id";
		public const string Date_createdColumnName = "date_created";
		public const string Date_activeColumnName = "date_active";
		public const string Date_to_expireColumnName = "date_to_expire";
		public const string Date_expiredColumnName = "date_expired";
		public const string StatusColumnName = "status";
		public const string Start_balanceColumnName = "start_balance";
		public const string Current_balanceColumnName = "current_balance";
		public const string Start_bonus_minutesColumnName = "start_bonus_minutes";
		public const string Current_bonus_minutesColumnName = "current_bonus_minutes";

		// Instance fields
		private Rbr_Db _db;

		/// <summary>
		/// Initializes a new instance of the <see cref="RetailAccountCollection_Base"/> 
		/// class with the specified <see cref="Rbr_Db"/>.
		/// </summary>
		/// <param name="db">The <see cref="Rbr_Db"/> object.</param>
		public RetailAccountCollection_Base(Rbr_Db db)
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
		/// Gets an array of all records from the <c>RetailAccount</c> table.
		/// </summary>
		/// <returns>An array of <see cref="RetailAccountRow"/> objects.</returns>
		public virtual RetailAccountRow[] GetAll()
		{
			return MapRecords(CreateGetAllCommand());
		}

		/// <summary>
		/// Gets a <see cref="System.Data.DataTable"/> object that 
		/// includes all records from the <c>RetailAccount</c> table.
		/// </summary>
		/// <returns>A reference to the <see cref="System.Data.DataTable"/> object.</returns>
		public virtual DataTable GetAllAsDataTable()
		{
			return MapRecordsToDataTable(CreateGetAllCommand());
		}

		/// <summary>
		/// Creates and returns an <see cref="System.Data.IDbCommand"/> object that is used
		/// to retrieve all records from the <c>RetailAccount</c> table.
		/// </summary>
		/// <returns>A reference to the <see cref="System.Data.IDbCommand"/> object.</returns>
		protected virtual IDbCommand CreateGetAllCommand()
		{
			return CreateGetCommand(null, null);
		}

		/// <summary>
		/// Gets the first <see cref="RetailAccountRow"/> objects that 
		/// match the search condition.
		/// </summary>
		/// <param name="whereSql">The SQL search condition. For example: 
		/// <c>"FirstName='Smith' AND Zip=75038"</c>.</param>
		/// <returns>An instance of <see cref="RetailAccountRow"/> or null reference 
		/// (Nothing in Visual Basic) if the object was not found.</returns>
		public RetailAccountRow GetRow(string whereSql)
		{
			int totalRecordCount = -1;
			RetailAccountRow[] rows = GetAsArray(whereSql, null, 0, 1, ref totalRecordCount);
			return 0 == rows.Length ? null : rows[0];
		}

		/// <summary>
		/// Gets an array of <see cref="RetailAccountRow"/> objects that 
		/// match the search condition, in the the specified sort order.
		/// </summary>
		/// <param name="whereSql">The SQL search condition. For example: 
		/// <c>"FirstName='Smith' AND Zip=75038"</c>.</param>
		/// <param name="orderBySql">The column name(s) followed by "ASC" (ascending) or "DESC" (descending).
		/// Columns are sorted in ascending order by default. For example: <c>"LastName ASC, FirstName ASC"</c>.</param>
		/// <returns>An array of <see cref="RetailAccountRow"/> objects.</returns>
		public RetailAccountRow[] GetAsArray(string whereSql, string orderBySql)
		{
			int totalRecordCount = -1;
			return GetAsArray(whereSql, orderBySql, 0, int.MaxValue, ref totalRecordCount);
		}

		/// <summary>
		/// Gets an array of <see cref="RetailAccountRow"/> objects that 
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
		/// <returns>An array of <see cref="RetailAccountRow"/> objects.</returns>
		public virtual RetailAccountRow[] GetAsArray(string whereSql, string orderBySql,
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
			string sql = "SELECT * FROM [dbo].[RetailAccount]";
			if(null != whereSql && 0 < whereSql.Length)
				sql += " WHERE " + whereSql;
			if(null != orderBySql && 0 < orderBySql.Length)
				sql += " ORDER BY " + orderBySql;
			return _db.CreateCommand(sql);
		}

		/// <summary>
		/// Gets <see cref="RetailAccountRow"/> by the primary key.
		/// </summary>
		/// <param name="retail_acct_id">The <c>retail_acct_id</c> column value.</param>
		/// <returns>An instance of <see cref="RetailAccountRow"/> or null reference 
		/// (Nothing in Visual Basic) if the object was not found.</returns>
		public virtual RetailAccountRow GetByPrimaryKey(int retail_acct_id)
		{
			string whereSql = "[retail_acct_id]=" + _db.CreateSqlParameterName("Retail_acct_id");
			IDbCommand cmd = CreateGetCommand(whereSql, null);
			AddParameter(cmd, "Retail_acct_id", retail_acct_id);
			RetailAccountRow[] tempArray = MapRecords(cmd);
			return 0 == tempArray.Length ? null : tempArray[0];
		}

		/// <summary>
		/// Gets an array of <see cref="RetailAccountRow"/> objects 
		/// by the <c>R_218</c> foreign key.
		/// </summary>
		/// <param name="customer_acct_id">The <c>customer_acct_id</c> column value.</param>
		/// <returns>An array of <see cref="RetailAccountRow"/> objects.</returns>
		public virtual RetailAccountRow[] GetByCustomer_acct_id(short customer_acct_id)
		{
			return MapRecords(CreateGetByCustomer_acct_idCommand(customer_acct_id));
		}

		/// <summary>
		/// Gets a <see cref="System.Data.DataTable"/> object 
		/// by the <c>R_218</c> foreign key.
		/// </summary>
		/// <param name="customer_acct_id">The <c>customer_acct_id</c> column value.</param>
		/// <returns>A reference to the <see cref="System.Data.DataTable"/> object.</returns>
		public virtual DataTable GetByCustomer_acct_idAsDataTable(short customer_acct_id)
		{
			return MapRecordsToDataTable(CreateGetByCustomer_acct_idCommand(customer_acct_id));
		}

		/// <summary>
		/// Creates an <see cref="System.Data.IDbCommand"/> object that can be used to 
		/// return records by the <c>R_218</c> foreign key.
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
		/// Adds a new record into the <c>RetailAccount</c> table.
		/// </summary>
		/// <param name="value">The <see cref="RetailAccountRow"/> object to be inserted.</param>
		public virtual void Insert(RetailAccountRow value)
		{
			string sqlStr = "INSERT INTO [dbo].[RetailAccount] (" +
				"[retail_acct_id], " +
				"[customer_acct_id], " +
				"[date_to_expire], " +
				"[status], " +
				"[start_balance], " +
				"[start_bonus_minutes]" +
				") VALUES (" +
				_db.CreateSqlParameterName("Retail_acct_id") + ", " +
				_db.CreateSqlParameterName("Customer_acct_id") + ", " +
				_db.CreateSqlParameterName("Date_to_expire") + ", " +
				_db.CreateSqlParameterName("Status") + ", " +
				_db.CreateSqlParameterName("Start_balance") + ", " +
				_db.CreateSqlParameterName("Start_bonus_minutes") + ")";
			IDbCommand cmd = _db.CreateCommand(sqlStr);
			AddParameter(cmd, "Retail_acct_id", value.Retail_acct_id);
			AddParameter(cmd, "Customer_acct_id", value.Customer_acct_id);
			AddParameter(cmd, "Date_to_expire", value.Date_to_expire);
			AddParameter(cmd, "Status", value.Status);
			AddParameter(cmd, "Start_balance", value.Start_balance);
			AddParameter(cmd, "Start_bonus_minutes", value.Start_bonus_minutes);
			cmd.ExecuteNonQuery();
		}

		/// <summary>
		/// Updates a record in the <c>RetailAccount</c> table.
		/// </summary>
		/// <param name="value">The <see cref="RetailAccountRow"/>
		/// object used to update the table record.</param>
		/// <returns>true if the record was updated; otherwise, false.</returns>
		public virtual bool Update(RetailAccountRow value)
		{
			string sqlStr = "UPDATE [dbo].[RetailAccount] SET " +
				"[customer_acct_id]=" + _db.CreateSqlParameterName("Customer_acct_id") + ", " +
				"[date_to_expire]=" + _db.CreateSqlParameterName("Date_to_expire") + ", " +
				"[status]=" + _db.CreateSqlParameterName("Status") + ", " +
				"[start_balance]=" + _db.CreateSqlParameterName("Start_balance") + ", " +
				"[start_bonus_minutes]=" + _db.CreateSqlParameterName("Start_bonus_minutes") +
				" WHERE " +
				"[retail_acct_id]=" + _db.CreateSqlParameterName("Retail_acct_id");
			IDbCommand cmd = _db.CreateCommand(sqlStr);
			AddParameter(cmd, "Customer_acct_id", value.Customer_acct_id);
			AddParameter(cmd, "Date_to_expire", value.Date_to_expire);
			AddParameter(cmd, "Status", value.Status);
			AddParameter(cmd, "Start_balance", value.Start_balance);
			AddParameter(cmd, "Start_bonus_minutes", value.Start_bonus_minutes);
			AddParameter(cmd, "Retail_acct_id", value.Retail_acct_id);
			return 0 != cmd.ExecuteNonQuery();
		}

		/// <summary>
		/// Updates the <c>RetailAccount</c> table and calls the <c>AcceptChanges</c> method
		/// on the changed DataRow objects.
		/// </summary>
		/// <param name="table">The <see cref="System.Data.DataTable"/> used to update the data source.</param>
		public void Update(DataTable table)
		{
			Update(table, true);
		}

		/// <summary>
		/// Updates the <c>RetailAccount</c> table. Pass <c>false</c> as the <c>acceptChanges</c> 
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
							DeleteByPrimaryKey((int)row["Retail_acct_id"]);
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
		/// Deletes the specified object from the <c>RetailAccount</c> table.
		/// </summary>
		/// <param name="value">The <see cref="RetailAccountRow"/> object to delete.</param>
		/// <returns>true if the record was deleted; otherwise, false.</returns>
		public bool Delete(RetailAccountRow value)
		{
			return DeleteByPrimaryKey(value.Retail_acct_id);
		}

		/// <summary>
		/// Deletes a record from the <c>RetailAccount</c> table using
		/// the specified primary key.
		/// </summary>
		/// <param name="retail_acct_id">The <c>retail_acct_id</c> column value.</param>
		/// <returns>true if the record was deleted; otherwise, false.</returns>
		public virtual bool DeleteByPrimaryKey(int retail_acct_id)
		{
			string whereSql = "[retail_acct_id]=" + _db.CreateSqlParameterName("Retail_acct_id");
			IDbCommand cmd = CreateDeleteCommand(whereSql);
			AddParameter(cmd, "Retail_acct_id", retail_acct_id);
			return 0 < cmd.ExecuteNonQuery();
		}

		/// <summary>
		/// Deletes records from the <c>RetailAccount</c> table using the 
		/// <c>R_218</c> foreign key.
		/// </summary>
		/// <param name="customer_acct_id">The <c>customer_acct_id</c> column value.</param>
		/// <returns>The number of records deleted from the table.</returns>
		public int DeleteByCustomer_acct_id(short customer_acct_id)
		{
			return CreateDeleteByCustomer_acct_idCommand(customer_acct_id).ExecuteNonQuery();
		}

		/// <summary>
		/// Creates an <see cref="System.Data.IDbCommand"/> object that can be used to
		/// delete records using the <c>R_218</c> foreign key.
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
		/// Deletes <c>RetailAccount</c> records that match the specified criteria.
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
		/// to delete <c>RetailAccount</c> records that match the specified criteria.
		/// </summary>
		/// <param name="whereSql">The SQL search condition. 
		/// For example: <c>"FirstName='Smith' AND Zip=75038"</c>.</param>
		/// <returns>A reference to the <see cref="System.Data.IDbCommand"/> object.</returns>
		protected virtual IDbCommand CreateDeleteCommand(string whereSql)
		{
			string sql = "DELETE FROM [dbo].[RetailAccount]";
			if(null != whereSql && 0 < whereSql.Length)
				sql += " WHERE " + whereSql;
			return _db.CreateCommand(sql);
		}

		/// <summary>
		/// Deletes all records from the <c>RetailAccount</c> table.
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
		/// <returns>An array of <see cref="RetailAccountRow"/> objects.</returns>
		protected RetailAccountRow[] MapRecords(IDbCommand command)
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
		/// <returns>An array of <see cref="RetailAccountRow"/> objects.</returns>
		protected RetailAccountRow[] MapRecords(IDataReader reader)
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
		/// <returns>An array of <see cref="RetailAccountRow"/> objects.</returns>
		protected virtual RetailAccountRow[] MapRecords(IDataReader reader, 
										int startIndex, int length, ref int totalRecordCount)
		{
			if(0 > startIndex)
				throw new ArgumentOutOfRangeException("startIndex", startIndex, "StartIndex cannot be less than zero.");
			if(0 > length)
				throw new ArgumentOutOfRangeException("length", length, "Length cannot be less than zero.");

			int retail_acct_idColumnIndex = reader.GetOrdinal("retail_acct_id");
			int customer_acct_idColumnIndex = reader.GetOrdinal("customer_acct_id");
			int date_createdColumnIndex = reader.GetOrdinal("date_created");
			int date_activeColumnIndex = reader.GetOrdinal("date_active");
			int date_to_expireColumnIndex = reader.GetOrdinal("date_to_expire");
			int date_expiredColumnIndex = reader.GetOrdinal("date_expired");
			int statusColumnIndex = reader.GetOrdinal("status");
			int start_balanceColumnIndex = reader.GetOrdinal("start_balance");
			int current_balanceColumnIndex = reader.GetOrdinal("current_balance");
			int start_bonus_minutesColumnIndex = reader.GetOrdinal("start_bonus_minutes");
			int current_bonus_minutesColumnIndex = reader.GetOrdinal("current_bonus_minutes");

			System.Collections.ArrayList recordList = new System.Collections.ArrayList();
			int ri = -startIndex;
			while(reader.Read())
			{
				ri++;
				if(ri > 0 && ri <= length)
				{
					RetailAccountRow record = new RetailAccountRow();
					recordList.Add(record);

					record.Retail_acct_id = Convert.ToInt32(reader.GetValue(retail_acct_idColumnIndex));
					record.Customer_acct_id = Convert.ToInt16(reader.GetValue(customer_acct_idColumnIndex));
					record.Date_created = Convert.ToDateTime(reader.GetValue(date_createdColumnIndex));
					record.Date_active = Convert.ToDateTime(reader.GetValue(date_activeColumnIndex));
					record.Date_to_expire = Convert.ToDateTime(reader.GetValue(date_to_expireColumnIndex));
					record.Date_expired = Convert.ToDateTime(reader.GetValue(date_expiredColumnIndex));
					record.Status = Convert.ToByte(reader.GetValue(statusColumnIndex));
					record.Start_balance = Convert.ToDecimal(reader.GetValue(start_balanceColumnIndex));
					record.Current_balance = Convert.ToDecimal(reader.GetValue(current_balanceColumnIndex));
					record.Start_bonus_minutes = Convert.ToInt16(reader.GetValue(start_bonus_minutesColumnIndex));
					record.Current_bonus_minutes = Convert.ToInt16(reader.GetValue(current_bonus_minutesColumnIndex));

					if(ri == length && 0 != totalRecordCount)
						break;
				}
			}

			totalRecordCount = 0 == totalRecordCount ? ri + startIndex : -1;
			return (RetailAccountRow[])(recordList.ToArray(typeof(RetailAccountRow)));
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
		/// Converts <see cref="System.Data.DataRow"/> to <see cref="RetailAccountRow"/>.
		/// </summary>
		/// <param name="row">The <see cref="System.Data.DataRow"/> object to be mapped.</param>
		/// <returns>A reference to the <see cref="RetailAccountRow"/> object.</returns>
		protected virtual RetailAccountRow MapRow(DataRow row)
		{
			RetailAccountRow mappedObject = new RetailAccountRow();
			DataTable dataTable = row.Table;
			DataColumn dataColumn;
			// Column "Retail_acct_id"
			dataColumn = dataTable.Columns["Retail_acct_id"];
			if(!row.IsNull(dataColumn))
				mappedObject.Retail_acct_id = (int)row[dataColumn];
			// Column "Customer_acct_id"
			dataColumn = dataTable.Columns["Customer_acct_id"];
			if(!row.IsNull(dataColumn))
				mappedObject.Customer_acct_id = (short)row[dataColumn];
			// Column "Date_created"
			dataColumn = dataTable.Columns["Date_created"];
			if(!row.IsNull(dataColumn))
				mappedObject.Date_created = (System.DateTime)row[dataColumn];
			// Column "Date_active"
			dataColumn = dataTable.Columns["Date_active"];
			if(!row.IsNull(dataColumn))
				mappedObject.Date_active = (System.DateTime)row[dataColumn];
			// Column "Date_to_expire"
			dataColumn = dataTable.Columns["Date_to_expire"];
			if(!row.IsNull(dataColumn))
				mappedObject.Date_to_expire = (System.DateTime)row[dataColumn];
			// Column "Date_expired"
			dataColumn = dataTable.Columns["Date_expired"];
			if(!row.IsNull(dataColumn))
				mappedObject.Date_expired = (System.DateTime)row[dataColumn];
			// Column "Status"
			dataColumn = dataTable.Columns["Status"];
			if(!row.IsNull(dataColumn))
				mappedObject.Status = (byte)row[dataColumn];
			// Column "Start_balance"
			dataColumn = dataTable.Columns["Start_balance"];
			if(!row.IsNull(dataColumn))
				mappedObject.Start_balance = (decimal)row[dataColumn];
			// Column "Current_balance"
			dataColumn = dataTable.Columns["Current_balance"];
			if(!row.IsNull(dataColumn))
				mappedObject.Current_balance = (decimal)row[dataColumn];
			// Column "Start_bonus_minutes"
			dataColumn = dataTable.Columns["Start_bonus_minutes"];
			if(!row.IsNull(dataColumn))
				mappedObject.Start_bonus_minutes = (short)row[dataColumn];
			// Column "Current_bonus_minutes"
			dataColumn = dataTable.Columns["Current_bonus_minutes"];
			if(!row.IsNull(dataColumn))
				mappedObject.Current_bonus_minutes = (short)row[dataColumn];
			return mappedObject;
		}

		/// <summary>
		/// Creates a <see cref="System.Data.DataTable"/> object for 
		/// the <c>RetailAccount</c> table.
		/// </summary>
		/// <returns>A reference to the <see cref="System.Data.DataTable"/> object.</returns>
		protected virtual DataTable CreateDataTable()
		{
			DataTable dataTable = new DataTable();
			dataTable.TableName = "RetailAccount";
			DataColumn dataColumn;
			dataColumn = dataTable.Columns.Add("Retail_acct_id", typeof(int));
			dataColumn.Caption = "retail_acct_id";
			dataColumn.AllowDBNull = false;
			dataColumn = dataTable.Columns.Add("Customer_acct_id", typeof(short));
			dataColumn.Caption = "customer_acct_id";
			dataColumn.AllowDBNull = false;
			dataColumn = dataTable.Columns.Add("Date_created", typeof(System.DateTime));
			dataColumn.Caption = "date_created";
			dataColumn.AllowDBNull = false;
			dataColumn.ReadOnly = true;
			dataColumn = dataTable.Columns.Add("Date_active", typeof(System.DateTime));
			dataColumn.Caption = "date_active";
			dataColumn.AllowDBNull = false;
			dataColumn.ReadOnly = true;
			dataColumn = dataTable.Columns.Add("Date_to_expire", typeof(System.DateTime));
			dataColumn.Caption = "date_to_expire";
			dataColumn.AllowDBNull = false;
			dataColumn = dataTable.Columns.Add("Date_expired", typeof(System.DateTime));
			dataColumn.Caption = "date_expired";
			dataColumn.AllowDBNull = false;
			dataColumn.ReadOnly = true;
			dataColumn = dataTable.Columns.Add("Status", typeof(byte));
			dataColumn.Caption = "status";
			dataColumn.AllowDBNull = false;
			dataColumn = dataTable.Columns.Add("Start_balance", typeof(decimal));
			dataColumn.Caption = "start_balance";
			dataColumn.AllowDBNull = false;
			dataColumn = dataTable.Columns.Add("Current_balance", typeof(decimal));
			dataColumn.Caption = "current_balance";
			dataColumn.AllowDBNull = false;
			dataColumn.ReadOnly = true;
			dataColumn = dataTable.Columns.Add("Start_bonus_minutes", typeof(short));
			dataColumn.Caption = "start_bonus_minutes";
			dataColumn.AllowDBNull = false;
			dataColumn = dataTable.Columns.Add("Current_bonus_minutes", typeof(short));
			dataColumn.Caption = "current_bonus_minutes";
			dataColumn.AllowDBNull = false;
			dataColumn.ReadOnly = true;
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
				case "Retail_acct_id":
					parameter = _db.AddParameter(cmd, paramName, DbType.Int32, value);
					break;

				case "Customer_acct_id":
					parameter = _db.AddParameter(cmd, paramName, DbType.Int16, value);
					break;

				case "Date_created":
					parameter = _db.AddParameter(cmd, paramName, DbType.DateTime, value);
					break;

				case "Date_active":
					parameter = _db.AddParameter(cmd, paramName, DbType.DateTime, value);
					break;

				case "Date_to_expire":
					parameter = _db.AddParameter(cmd, paramName, DbType.DateTime, value);
					break;

				case "Date_expired":
					parameter = _db.AddParameter(cmd, paramName, DbType.DateTime, value);
					break;

				case "Status":
					parameter = _db.AddParameter(cmd, paramName, DbType.Byte, value);
					break;

				case "Start_balance":
					parameter = _db.AddParameter(cmd, paramName, DbType.Decimal, value);
					break;

				case "Current_balance":
					parameter = _db.AddParameter(cmd, paramName, DbType.Decimal, value);
					break;

				case "Start_bonus_minutes":
					parameter = _db.AddParameter(cmd, paramName, DbType.Int16, value);
					break;

				case "Current_bonus_minutes":
					parameter = _db.AddParameter(cmd, paramName, DbType.Int16, value);
					break;

				default:
					throw new ArgumentException("Unknown parameter name (" + paramName + ").");
			}
			return parameter;
		}

	} // End of RetailAccountCollection_Base class
}  // End of namespace

// <fileinfo name="Base\RetailAccountPaymentCollection_Base.cs">
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
	/// The base class for <see cref="RetailAccountPaymentCollection"/>. Provides methods 
	/// for common database table operations. 
	/// </summary>
	/// <remarks>
	/// Do not change this source code. Update the <see cref="RetailAccountPaymentCollection"/>
	/// class if you need to add or change some functionality.
	/// </remarks>
	public abstract class RetailAccountPaymentCollection_Base
	{
		// Constants
		public const string Retail_acct_idColumnName = "retail_acct_id";
		public const string Date_timeColumnName = "date_time";
		public const string Previous_amountColumnName = "previous_amount";
		public const string Payment_amountColumnName = "payment_amount";
		public const string Previous_bonus_minutesColumnName = "previous_bonus_minutes";
		public const string Added_bonus_minutesColumnName = "added_bonus_minutes";
		public const string CommentsColumnName = "comments";
		public const string Person_idColumnName = "person_id";
		public const string Balance_adjustment_reason_idColumnName = "balance_adjustment_reason_id";
		public const string Cdr_keyColumnName = "cdr_key";

		// Instance fields
		private Rbr_Db _db;

		/// <summary>
		/// Initializes a new instance of the <see cref="RetailAccountPaymentCollection_Base"/> 
		/// class with the specified <see cref="Rbr_Db"/>.
		/// </summary>
		/// <param name="db">The <see cref="Rbr_Db"/> object.</param>
		public RetailAccountPaymentCollection_Base(Rbr_Db db)
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
		/// Gets an array of all records from the <c>RetailAccountPayment</c> table.
		/// </summary>
		/// <returns>An array of <see cref="RetailAccountPaymentRow"/> objects.</returns>
		public virtual RetailAccountPaymentRow[] GetAll()
		{
			return MapRecords(CreateGetAllCommand());
		}

		/// <summary>
		/// Gets a <see cref="System.Data.DataTable"/> object that 
		/// includes all records from the <c>RetailAccountPayment</c> table.
		/// </summary>
		/// <returns>A reference to the <see cref="System.Data.DataTable"/> object.</returns>
		public virtual DataTable GetAllAsDataTable()
		{
			return MapRecordsToDataTable(CreateGetAllCommand());
		}

		/// <summary>
		/// Creates and returns an <see cref="System.Data.IDbCommand"/> object that is used
		/// to retrieve all records from the <c>RetailAccountPayment</c> table.
		/// </summary>
		/// <returns>A reference to the <see cref="System.Data.IDbCommand"/> object.</returns>
		protected virtual IDbCommand CreateGetAllCommand()
		{
			return CreateGetCommand(null, null);
		}

		/// <summary>
		/// Gets the first <see cref="RetailAccountPaymentRow"/> objects that 
		/// match the search condition.
		/// </summary>
		/// <param name="whereSql">The SQL search condition. For example: 
		/// <c>"FirstName='Smith' AND Zip=75038"</c>.</param>
		/// <returns>An instance of <see cref="RetailAccountPaymentRow"/> or null reference 
		/// (Nothing in Visual Basic) if the object was not found.</returns>
		public RetailAccountPaymentRow GetRow(string whereSql)
		{
			int totalRecordCount = -1;
			RetailAccountPaymentRow[] rows = GetAsArray(whereSql, null, 0, 1, ref totalRecordCount);
			return 0 == rows.Length ? null : rows[0];
		}

		/// <summary>
		/// Gets an array of <see cref="RetailAccountPaymentRow"/> objects that 
		/// match the search condition, in the the specified sort order.
		/// </summary>
		/// <param name="whereSql">The SQL search condition. For example: 
		/// <c>"FirstName='Smith' AND Zip=75038"</c>.</param>
		/// <param name="orderBySql">The column name(s) followed by "ASC" (ascending) or "DESC" (descending).
		/// Columns are sorted in ascending order by default. For example: <c>"LastName ASC, FirstName ASC"</c>.</param>
		/// <returns>An array of <see cref="RetailAccountPaymentRow"/> objects.</returns>
		public RetailAccountPaymentRow[] GetAsArray(string whereSql, string orderBySql)
		{
			int totalRecordCount = -1;
			return GetAsArray(whereSql, orderBySql, 0, int.MaxValue, ref totalRecordCount);
		}

		/// <summary>
		/// Gets an array of <see cref="RetailAccountPaymentRow"/> objects that 
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
		/// <returns>An array of <see cref="RetailAccountPaymentRow"/> objects.</returns>
		public virtual RetailAccountPaymentRow[] GetAsArray(string whereSql, string orderBySql,
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
			string sql = "SELECT * FROM [dbo].[RetailAccountPayment]";
			if(null != whereSql && 0 < whereSql.Length)
				sql += " WHERE " + whereSql;
			if(null != orderBySql && 0 < orderBySql.Length)
				sql += " ORDER BY " + orderBySql;
			return _db.CreateCommand(sql);
		}

		/// <summary>
		/// Gets <see cref="RetailAccountPaymentRow"/> by the primary key.
		/// </summary>
		/// <param name="retail_acct_id">The <c>retail_acct_id</c> column value.</param>
		/// <param name="date_time">The <c>date_time</c> column value.</param>
		/// <returns>An instance of <see cref="RetailAccountPaymentRow"/> or null reference 
		/// (Nothing in Visual Basic) if the object was not found.</returns>
		public virtual RetailAccountPaymentRow GetByPrimaryKey(int retail_acct_id, System.DateTime date_time)
		{
			string whereSql = "[retail_acct_id]=" + _db.CreateSqlParameterName("Retail_acct_id") + " AND " +
							  "[date_time]=" + _db.CreateSqlParameterName("Date_time");
			IDbCommand cmd = CreateGetCommand(whereSql, null);
			AddParameter(cmd, "Retail_acct_id", retail_acct_id);
			AddParameter(cmd, "Date_time", date_time);
			RetailAccountPaymentRow[] tempArray = MapRecords(cmd);
			return 0 == tempArray.Length ? null : tempArray[0];
		}

		/// <summary>
		/// Gets an array of <see cref="RetailAccountPaymentRow"/> objects 
		/// by the <c>R_249</c> foreign key.
		/// </summary>
		/// <param name="retail_acct_id">The <c>retail_acct_id</c> column value.</param>
		/// <returns>An array of <see cref="RetailAccountPaymentRow"/> objects.</returns>
		public virtual RetailAccountPaymentRow[] GetByRetail_acct_id(int retail_acct_id)
		{
			return MapRecords(CreateGetByRetail_acct_idCommand(retail_acct_id));
		}

		/// <summary>
		/// Gets a <see cref="System.Data.DataTable"/> object 
		/// by the <c>R_249</c> foreign key.
		/// </summary>
		/// <param name="retail_acct_id">The <c>retail_acct_id</c> column value.</param>
		/// <returns>A reference to the <see cref="System.Data.DataTable"/> object.</returns>
		public virtual DataTable GetByRetail_acct_idAsDataTable(int retail_acct_id)
		{
			return MapRecordsToDataTable(CreateGetByRetail_acct_idCommand(retail_acct_id));
		}

		/// <summary>
		/// Creates an <see cref="System.Data.IDbCommand"/> object that can be used to 
		/// return records by the <c>R_249</c> foreign key.
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
		/// Gets an array of <see cref="RetailAccountPaymentRow"/> objects 
		/// by the <c>R_253</c> foreign key.
		/// </summary>
		/// <param name="person_id">The <c>person_id</c> column value.</param>
		/// <returns>An array of <see cref="RetailAccountPaymentRow"/> objects.</returns>
		public virtual RetailAccountPaymentRow[] GetByPerson_id(int person_id)
		{
			return MapRecords(CreateGetByPerson_idCommand(person_id));
		}

		/// <summary>
		/// Gets a <see cref="System.Data.DataTable"/> object 
		/// by the <c>R_253</c> foreign key.
		/// </summary>
		/// <param name="person_id">The <c>person_id</c> column value.</param>
		/// <returns>A reference to the <see cref="System.Data.DataTable"/> object.</returns>
		public virtual DataTable GetByPerson_idAsDataTable(int person_id)
		{
			return MapRecordsToDataTable(CreateGetByPerson_idCommand(person_id));
		}

		/// <summary>
		/// Creates an <see cref="System.Data.IDbCommand"/> object that can be used to 
		/// return records by the <c>R_253</c> foreign key.
		/// </summary>
		/// <param name="person_id">The <c>person_id</c> column value.</param>
		/// <returns>A reference to the <see cref="System.Data.IDbCommand"/> object.</returns>
		protected virtual IDbCommand CreateGetByPerson_idCommand(int person_id)
		{
			string whereSql = "";
			whereSql += "[person_id]=" + _db.CreateSqlParameterName("Person_id");

			IDbCommand cmd = CreateGetCommand(whereSql, null);
			AddParameter(cmd, "Person_id", person_id);
			return cmd;
		}

		/// <summary>
		/// Gets an array of <see cref="RetailAccountPaymentRow"/> objects 
		/// by the <c>R_280</c> foreign key.
		/// </summary>
		/// <param name="balance_adjustment_reason_id">The <c>balance_adjustment_reason_id</c> column value.</param>
		/// <returns>An array of <see cref="RetailAccountPaymentRow"/> objects.</returns>
		public virtual RetailAccountPaymentRow[] GetByBalance_adjustment_reason_id(int balance_adjustment_reason_id)
		{
			return MapRecords(CreateGetByBalance_adjustment_reason_idCommand(balance_adjustment_reason_id));
		}

		/// <summary>
		/// Gets a <see cref="System.Data.DataTable"/> object 
		/// by the <c>R_280</c> foreign key.
		/// </summary>
		/// <param name="balance_adjustment_reason_id">The <c>balance_adjustment_reason_id</c> column value.</param>
		/// <returns>A reference to the <see cref="System.Data.DataTable"/> object.</returns>
		public virtual DataTable GetByBalance_adjustment_reason_idAsDataTable(int balance_adjustment_reason_id)
		{
			return MapRecordsToDataTable(CreateGetByBalance_adjustment_reason_idCommand(balance_adjustment_reason_id));
		}

		/// <summary>
		/// Creates an <see cref="System.Data.IDbCommand"/> object that can be used to 
		/// return records by the <c>R_280</c> foreign key.
		/// </summary>
		/// <param name="balance_adjustment_reason_id">The <c>balance_adjustment_reason_id</c> column value.</param>
		/// <returns>A reference to the <see cref="System.Data.IDbCommand"/> object.</returns>
		protected virtual IDbCommand CreateGetByBalance_adjustment_reason_idCommand(int balance_adjustment_reason_id)
		{
			string whereSql = "";
			whereSql += "[balance_adjustment_reason_id]=" + _db.CreateSqlParameterName("Balance_adjustment_reason_id");

			IDbCommand cmd = CreateGetCommand(whereSql, null);
			AddParameter(cmd, "Balance_adjustment_reason_id", balance_adjustment_reason_id);
			return cmd;
		}

		/// <summary>
		/// Adds a new record into the <c>RetailAccountPayment</c> table.
		/// </summary>
		/// <param name="value">The <see cref="RetailAccountPaymentRow"/> object to be inserted.</param>
		public virtual void Insert(RetailAccountPaymentRow value)
		{
			string sqlStr = "INSERT INTO [dbo].[RetailAccountPayment] (" +
				"[retail_acct_id], " +
				"[date_time], " +
				"[previous_amount], " +
				"[payment_amount], " +
				"[previous_bonus_minutes], " +
				"[added_bonus_minutes], " +
				"[comments], " +
				"[person_id], " +
				"[balance_adjustment_reason_id], " +
				"[cdr_key]" +
				") VALUES (" +
				_db.CreateSqlParameterName("Retail_acct_id") + ", " +
				_db.CreateSqlParameterName("Date_time") + ", " +
				_db.CreateSqlParameterName("Previous_amount") + ", " +
				_db.CreateSqlParameterName("Payment_amount") + ", " +
				_db.CreateSqlParameterName("Previous_bonus_minutes") + ", " +
				_db.CreateSqlParameterName("Added_bonus_minutes") + ", " +
				_db.CreateSqlParameterName("Comments") + ", " +
				_db.CreateSqlParameterName("Person_id") + ", " +
				_db.CreateSqlParameterName("Balance_adjustment_reason_id") + ", " +
				_db.CreateSqlParameterName("Cdr_key") + ")";
			IDbCommand cmd = _db.CreateCommand(sqlStr);
			AddParameter(cmd, "Retail_acct_id", value.Retail_acct_id);
			AddParameter(cmd, "Date_time", value.Date_time);
			AddParameter(cmd, "Previous_amount", value.Previous_amount);
			AddParameter(cmd, "Payment_amount", value.Payment_amount);
			AddParameter(cmd, "Previous_bonus_minutes", value.Previous_bonus_minutes);
			AddParameter(cmd, "Added_bonus_minutes", value.Added_bonus_minutes);
			AddParameter(cmd, "Comments", value.Comments);
			AddParameter(cmd, "Person_id", value.Person_id);
			AddParameter(cmd, "Balance_adjustment_reason_id", value.Balance_adjustment_reason_id);
			AddParameter(cmd, "Cdr_key", value.Cdr_key);
			cmd.ExecuteNonQuery();
		}

		/// <summary>
		/// Updates a record in the <c>RetailAccountPayment</c> table.
		/// </summary>
		/// <param name="value">The <see cref="RetailAccountPaymentRow"/>
		/// object used to update the table record.</param>
		/// <returns>true if the record was updated; otherwise, false.</returns>
		public virtual bool Update(RetailAccountPaymentRow value)
		{
			string sqlStr = "UPDATE [dbo].[RetailAccountPayment] SET " +
				"[previous_amount]=" + _db.CreateSqlParameterName("Previous_amount") + ", " +
				"[payment_amount]=" + _db.CreateSqlParameterName("Payment_amount") + ", " +
				"[previous_bonus_minutes]=" + _db.CreateSqlParameterName("Previous_bonus_minutes") + ", " +
				"[added_bonus_minutes]=" + _db.CreateSqlParameterName("Added_bonus_minutes") + ", " +
				"[comments]=" + _db.CreateSqlParameterName("Comments") + ", " +
				"[person_id]=" + _db.CreateSqlParameterName("Person_id") + ", " +
				"[balance_adjustment_reason_id]=" + _db.CreateSqlParameterName("Balance_adjustment_reason_id") + ", " +
				"[cdr_key]=" + _db.CreateSqlParameterName("Cdr_key") +
				" WHERE " +
				"[retail_acct_id]=" + _db.CreateSqlParameterName("Retail_acct_id") + " AND " +
				"[date_time]=" + _db.CreateSqlParameterName("Date_time");
			IDbCommand cmd = _db.CreateCommand(sqlStr);
			AddParameter(cmd, "Previous_amount", value.Previous_amount);
			AddParameter(cmd, "Payment_amount", value.Payment_amount);
			AddParameter(cmd, "Previous_bonus_minutes", value.Previous_bonus_minutes);
			AddParameter(cmd, "Added_bonus_minutes", value.Added_bonus_minutes);
			AddParameter(cmd, "Comments", value.Comments);
			AddParameter(cmd, "Person_id", value.Person_id);
			AddParameter(cmd, "Balance_adjustment_reason_id", value.Balance_adjustment_reason_id);
			AddParameter(cmd, "Cdr_key", value.Cdr_key);
			AddParameter(cmd, "Retail_acct_id", value.Retail_acct_id);
			AddParameter(cmd, "Date_time", value.Date_time);
			return 0 != cmd.ExecuteNonQuery();
		}

		/// <summary>
		/// Updates the <c>RetailAccountPayment</c> table and calls the <c>AcceptChanges</c> method
		/// on the changed DataRow objects.
		/// </summary>
		/// <param name="table">The <see cref="System.Data.DataTable"/> used to update the data source.</param>
		public void Update(DataTable table)
		{
			Update(table, true);
		}

		/// <summary>
		/// Updates the <c>RetailAccountPayment</c> table. Pass <c>false</c> as the <c>acceptChanges</c> 
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
							DeleteByPrimaryKey((int)row["Retail_acct_id"], (System.DateTime)row["Date_time"]);
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
		/// Deletes the specified object from the <c>RetailAccountPayment</c> table.
		/// </summary>
		/// <param name="value">The <see cref="RetailAccountPaymentRow"/> object to delete.</param>
		/// <returns>true if the record was deleted; otherwise, false.</returns>
		public bool Delete(RetailAccountPaymentRow value)
		{
			return DeleteByPrimaryKey(value.Retail_acct_id, value.Date_time);
		}

		/// <summary>
		/// Deletes a record from the <c>RetailAccountPayment</c> table using
		/// the specified primary key.
		/// </summary>
		/// <param name="retail_acct_id">The <c>retail_acct_id</c> column value.</param>
		/// <param name="date_time">The <c>date_time</c> column value.</param>
		/// <returns>true if the record was deleted; otherwise, false.</returns>
		public virtual bool DeleteByPrimaryKey(int retail_acct_id, System.DateTime date_time)
		{
			string whereSql = "[retail_acct_id]=" + _db.CreateSqlParameterName("Retail_acct_id") + " AND " +
							  "[date_time]=" + _db.CreateSqlParameterName("Date_time");
			IDbCommand cmd = CreateDeleteCommand(whereSql);
			AddParameter(cmd, "Retail_acct_id", retail_acct_id);
			AddParameter(cmd, "Date_time", date_time);
			return 0 < cmd.ExecuteNonQuery();
		}

		/// <summary>
		/// Deletes records from the <c>RetailAccountPayment</c> table using the 
		/// <c>R_249</c> foreign key.
		/// </summary>
		/// <param name="retail_acct_id">The <c>retail_acct_id</c> column value.</param>
		/// <returns>The number of records deleted from the table.</returns>
		public int DeleteByRetail_acct_id(int retail_acct_id)
		{
			return CreateDeleteByRetail_acct_idCommand(retail_acct_id).ExecuteNonQuery();
		}

		/// <summary>
		/// Creates an <see cref="System.Data.IDbCommand"/> object that can be used to
		/// delete records using the <c>R_249</c> foreign key.
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
		/// Deletes records from the <c>RetailAccountPayment</c> table using the 
		/// <c>R_253</c> foreign key.
		/// </summary>
		/// <param name="person_id">The <c>person_id</c> column value.</param>
		/// <returns>The number of records deleted from the table.</returns>
		public int DeleteByPerson_id(int person_id)
		{
			return CreateDeleteByPerson_idCommand(person_id).ExecuteNonQuery();
		}

		/// <summary>
		/// Creates an <see cref="System.Data.IDbCommand"/> object that can be used to
		/// delete records using the <c>R_253</c> foreign key.
		/// </summary>
		/// <param name="person_id">The <c>person_id</c> column value.</param>
		/// <returns>A reference to the <see cref="System.Data.IDbCommand"/> object.</returns>
		protected virtual IDbCommand CreateDeleteByPerson_idCommand(int person_id)
		{
			string whereSql = "";
			whereSql += "[person_id]=" + _db.CreateSqlParameterName("Person_id");

			IDbCommand cmd = CreateDeleteCommand(whereSql);
			AddParameter(cmd, "Person_id", person_id);
			return cmd;
		}

		/// <summary>
		/// Deletes records from the <c>RetailAccountPayment</c> table using the 
		/// <c>R_280</c> foreign key.
		/// </summary>
		/// <param name="balance_adjustment_reason_id">The <c>balance_adjustment_reason_id</c> column value.</param>
		/// <returns>The number of records deleted from the table.</returns>
		public int DeleteByBalance_adjustment_reason_id(int balance_adjustment_reason_id)
		{
			return CreateDeleteByBalance_adjustment_reason_idCommand(balance_adjustment_reason_id).ExecuteNonQuery();
		}

		/// <summary>
		/// Creates an <see cref="System.Data.IDbCommand"/> object that can be used to
		/// delete records using the <c>R_280</c> foreign key.
		/// </summary>
		/// <param name="balance_adjustment_reason_id">The <c>balance_adjustment_reason_id</c> column value.</param>
		/// <returns>A reference to the <see cref="System.Data.IDbCommand"/> object.</returns>
		protected virtual IDbCommand CreateDeleteByBalance_adjustment_reason_idCommand(int balance_adjustment_reason_id)
		{
			string whereSql = "";
			whereSql += "[balance_adjustment_reason_id]=" + _db.CreateSqlParameterName("Balance_adjustment_reason_id");

			IDbCommand cmd = CreateDeleteCommand(whereSql);
			AddParameter(cmd, "Balance_adjustment_reason_id", balance_adjustment_reason_id);
			return cmd;
		}

		/// <summary>
		/// Deletes <c>RetailAccountPayment</c> records that match the specified criteria.
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
		/// to delete <c>RetailAccountPayment</c> records that match the specified criteria.
		/// </summary>
		/// <param name="whereSql">The SQL search condition. 
		/// For example: <c>"FirstName='Smith' AND Zip=75038"</c>.</param>
		/// <returns>A reference to the <see cref="System.Data.IDbCommand"/> object.</returns>
		protected virtual IDbCommand CreateDeleteCommand(string whereSql)
		{
			string sql = "DELETE FROM [dbo].[RetailAccountPayment]";
			if(null != whereSql && 0 < whereSql.Length)
				sql += " WHERE " + whereSql;
			return _db.CreateCommand(sql);
		}

		/// <summary>
		/// Deletes all records from the <c>RetailAccountPayment</c> table.
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
		/// <returns>An array of <see cref="RetailAccountPaymentRow"/> objects.</returns>
		protected RetailAccountPaymentRow[] MapRecords(IDbCommand command)
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
		/// <returns>An array of <see cref="RetailAccountPaymentRow"/> objects.</returns>
		protected RetailAccountPaymentRow[] MapRecords(IDataReader reader)
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
		/// <returns>An array of <see cref="RetailAccountPaymentRow"/> objects.</returns>
		protected virtual RetailAccountPaymentRow[] MapRecords(IDataReader reader, 
										int startIndex, int length, ref int totalRecordCount)
		{
			if(0 > startIndex)
				throw new ArgumentOutOfRangeException("startIndex", startIndex, "StartIndex cannot be less than zero.");
			if(0 > length)
				throw new ArgumentOutOfRangeException("length", length, "Length cannot be less than zero.");

			int retail_acct_idColumnIndex = reader.GetOrdinal("retail_acct_id");
			int date_timeColumnIndex = reader.GetOrdinal("date_time");
			int previous_amountColumnIndex = reader.GetOrdinal("previous_amount");
			int payment_amountColumnIndex = reader.GetOrdinal("payment_amount");
			int previous_bonus_minutesColumnIndex = reader.GetOrdinal("previous_bonus_minutes");
			int added_bonus_minutesColumnIndex = reader.GetOrdinal("added_bonus_minutes");
			int commentsColumnIndex = reader.GetOrdinal("comments");
			int person_idColumnIndex = reader.GetOrdinal("person_id");
			int balance_adjustment_reason_idColumnIndex = reader.GetOrdinal("balance_adjustment_reason_id");
			int cdr_keyColumnIndex = reader.GetOrdinal("cdr_key");

			System.Collections.ArrayList recordList = new System.Collections.ArrayList();
			int ri = -startIndex;
			while(reader.Read())
			{
				ri++;
				if(ri > 0 && ri <= length)
				{
					RetailAccountPaymentRow record = new RetailAccountPaymentRow();
					recordList.Add(record);

					record.Retail_acct_id = Convert.ToInt32(reader.GetValue(retail_acct_idColumnIndex));
					record.Date_time = Convert.ToDateTime(reader.GetValue(date_timeColumnIndex));
					record.Previous_amount = Convert.ToDecimal(reader.GetValue(previous_amountColumnIndex));
					record.Payment_amount = Convert.ToDecimal(reader.GetValue(payment_amountColumnIndex));
					record.Previous_bonus_minutes = Convert.ToInt16(reader.GetValue(previous_bonus_minutesColumnIndex));
					record.Added_bonus_minutes = Convert.ToInt16(reader.GetValue(added_bonus_minutesColumnIndex));
					record.Comments = Convert.ToString(reader.GetValue(commentsColumnIndex));
					record.Person_id = Convert.ToInt32(reader.GetValue(person_idColumnIndex));
					record.Balance_adjustment_reason_id = Convert.ToInt32(reader.GetValue(balance_adjustment_reason_idColumnIndex));
					record.Cdr_key = Convert.ToString(reader.GetValue(cdr_keyColumnIndex));

					if(ri == length && 0 != totalRecordCount)
						break;
				}
			}

			totalRecordCount = 0 == totalRecordCount ? ri + startIndex : -1;
			return (RetailAccountPaymentRow[])(recordList.ToArray(typeof(RetailAccountPaymentRow)));
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
		/// Converts <see cref="System.Data.DataRow"/> to <see cref="RetailAccountPaymentRow"/>.
		/// </summary>
		/// <param name="row">The <see cref="System.Data.DataRow"/> object to be mapped.</param>
		/// <returns>A reference to the <see cref="RetailAccountPaymentRow"/> object.</returns>
		protected virtual RetailAccountPaymentRow MapRow(DataRow row)
		{
			RetailAccountPaymentRow mappedObject = new RetailAccountPaymentRow();
			DataTable dataTable = row.Table;
			DataColumn dataColumn;
			// Column "Retail_acct_id"
			dataColumn = dataTable.Columns["Retail_acct_id"];
			if(!row.IsNull(dataColumn))
				mappedObject.Retail_acct_id = (int)row[dataColumn];
			// Column "Date_time"
			dataColumn = dataTable.Columns["Date_time"];
			if(!row.IsNull(dataColumn))
				mappedObject.Date_time = (System.DateTime)row[dataColumn];
			// Column "Previous_amount"
			dataColumn = dataTable.Columns["Previous_amount"];
			if(!row.IsNull(dataColumn))
				mappedObject.Previous_amount = (decimal)row[dataColumn];
			// Column "Payment_amount"
			dataColumn = dataTable.Columns["Payment_amount"];
			if(!row.IsNull(dataColumn))
				mappedObject.Payment_amount = (decimal)row[dataColumn];
			// Column "Previous_bonus_minutes"
			dataColumn = dataTable.Columns["Previous_bonus_minutes"];
			if(!row.IsNull(dataColumn))
				mappedObject.Previous_bonus_minutes = (short)row[dataColumn];
			// Column "Added_bonus_minutes"
			dataColumn = dataTable.Columns["Added_bonus_minutes"];
			if(!row.IsNull(dataColumn))
				mappedObject.Added_bonus_minutes = (short)row[dataColumn];
			// Column "Comments"
			dataColumn = dataTable.Columns["Comments"];
			if(!row.IsNull(dataColumn))
				mappedObject.Comments = (string)row[dataColumn];
			// Column "Person_id"
			dataColumn = dataTable.Columns["Person_id"];
			if(!row.IsNull(dataColumn))
				mappedObject.Person_id = (int)row[dataColumn];
			// Column "Balance_adjustment_reason_id"
			dataColumn = dataTable.Columns["Balance_adjustment_reason_id"];
			if(!row.IsNull(dataColumn))
				mappedObject.Balance_adjustment_reason_id = (int)row[dataColumn];
			// Column "Cdr_key"
			dataColumn = dataTable.Columns["Cdr_key"];
			if(!row.IsNull(dataColumn))
				mappedObject.Cdr_key = (string)row[dataColumn];
			return mappedObject;
		}

		/// <summary>
		/// Creates a <see cref="System.Data.DataTable"/> object for 
		/// the <c>RetailAccountPayment</c> table.
		/// </summary>
		/// <returns>A reference to the <see cref="System.Data.DataTable"/> object.</returns>
		protected virtual DataTable CreateDataTable()
		{
			DataTable dataTable = new DataTable();
			dataTable.TableName = "RetailAccountPayment";
			DataColumn dataColumn;
			dataColumn = dataTable.Columns.Add("Retail_acct_id", typeof(int));
			dataColumn.Caption = "retail_acct_id";
			dataColumn.AllowDBNull = false;
			dataColumn = dataTable.Columns.Add("Date_time", typeof(System.DateTime));
			dataColumn.Caption = "date_time";
			dataColumn.AllowDBNull = false;
			dataColumn = dataTable.Columns.Add("Previous_amount", typeof(decimal));
			dataColumn.Caption = "previous_amount";
			dataColumn.AllowDBNull = false;
			dataColumn = dataTable.Columns.Add("Payment_amount", typeof(decimal));
			dataColumn.Caption = "payment_amount";
			dataColumn.AllowDBNull = false;
			dataColumn = dataTable.Columns.Add("Previous_bonus_minutes", typeof(short));
			dataColumn.Caption = "previous_bonus_minutes";
			dataColumn.AllowDBNull = false;
			dataColumn = dataTable.Columns.Add("Added_bonus_minutes", typeof(short));
			dataColumn.Caption = "added_bonus_minutes";
			dataColumn.AllowDBNull = false;
			dataColumn = dataTable.Columns.Add("Comments", typeof(string));
			dataColumn.Caption = "comments";
			dataColumn.AllowDBNull = false;
			dataColumn = dataTable.Columns.Add("Person_id", typeof(int));
			dataColumn.Caption = "person_id";
			dataColumn.AllowDBNull = false;
			dataColumn = dataTable.Columns.Add("Balance_adjustment_reason_id", typeof(int));
			dataColumn.Caption = "balance_adjustment_reason_id";
			dataColumn.AllowDBNull = false;
			dataColumn = dataTable.Columns.Add("Cdr_key", typeof(string));
			dataColumn.Caption = "cdr_key";
			dataColumn.MaxLength = 128;
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
				case "Retail_acct_id":
					parameter = _db.AddParameter(cmd, paramName, DbType.Int32, value);
					break;

				case "Date_time":
					parameter = _db.AddParameter(cmd, paramName, DbType.DateTime, value);
					break;

				case "Previous_amount":
					parameter = _db.AddParameter(cmd, paramName, DbType.Decimal, value);
					break;

				case "Payment_amount":
					parameter = _db.AddParameter(cmd, paramName, DbType.Decimal, value);
					break;

				case "Previous_bonus_minutes":
					parameter = _db.AddParameter(cmd, paramName, DbType.Int16, value);
					break;

				case "Added_bonus_minutes":
					parameter = _db.AddParameter(cmd, paramName, DbType.Int16, value);
					break;

				case "Comments":
					parameter = _db.AddParameter(cmd, paramName, DbType.AnsiString, value);
					break;

				case "Person_id":
					parameter = _db.AddParameter(cmd, paramName, DbType.Int32, value);
					break;

				case "Balance_adjustment_reason_id":
					parameter = _db.AddParameter(cmd, paramName, DbType.Int32, value);
					break;

				case "Cdr_key":
					parameter = _db.AddParameter(cmd, paramName, DbType.AnsiString, value);
					break;

				default:
					throw new ArgumentException("Unknown parameter name (" + paramName + ").");
			}
			return parameter;
		}

	} // End of RetailAccountPaymentCollection_Base class
}  // End of namespace

// <fileinfo name="Base\RetailRouteBonusMinutesCollection_Base.cs">
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
	/// The base class for <see cref="RetailRouteBonusMinutesCollection"/>. Provides methods 
	/// for common database table operations. 
	/// </summary>
	/// <remarks>
	/// Do not change this source code. Update the <see cref="RetailRouteBonusMinutesCollection"/>
	/// class if you need to add or change some functionality.
	/// </remarks>
	public abstract class RetailRouteBonusMinutesCollection_Base
	{
		// Constants
		public const string Retail_acct_idColumnName = "retail_acct_id";
		public const string Retail_route_idColumnName = "retail_route_id";
		public const string Start_bonus_minutesColumnName = "start_bonus_minutes";
		public const string Current_bonus_minutesColumnName = "current_bonus_minutes";

		// Instance fields
		private Rbr_Db _db;

		/// <summary>
		/// Initializes a new instance of the <see cref="RetailRouteBonusMinutesCollection_Base"/> 
		/// class with the specified <see cref="Rbr_Db"/>.
		/// </summary>
		/// <param name="db">The <see cref="Rbr_Db"/> object.</param>
		public RetailRouteBonusMinutesCollection_Base(Rbr_Db db)
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
		/// Gets an array of all records from the <c>RetailRouteBonusMinutes</c> table.
		/// </summary>
		/// <returns>An array of <see cref="RetailRouteBonusMinutesRow"/> objects.</returns>
		public virtual RetailRouteBonusMinutesRow[] GetAll()
		{
			return MapRecords(CreateGetAllCommand());
		}

		/// <summary>
		/// Gets a <see cref="System.Data.DataTable"/> object that 
		/// includes all records from the <c>RetailRouteBonusMinutes</c> table.
		/// </summary>
		/// <returns>A reference to the <see cref="System.Data.DataTable"/> object.</returns>
		public virtual DataTable GetAllAsDataTable()
		{
			return MapRecordsToDataTable(CreateGetAllCommand());
		}

		/// <summary>
		/// Creates and returns an <see cref="System.Data.IDbCommand"/> object that is used
		/// to retrieve all records from the <c>RetailRouteBonusMinutes</c> table.
		/// </summary>
		/// <returns>A reference to the <see cref="System.Data.IDbCommand"/> object.</returns>
		protected virtual IDbCommand CreateGetAllCommand()
		{
			return CreateGetCommand(null, null);
		}

		/// <summary>
		/// Gets the first <see cref="RetailRouteBonusMinutesRow"/> objects that 
		/// match the search condition.
		/// </summary>
		/// <param name="whereSql">The SQL search condition. For example: 
		/// <c>"FirstName='Smith' AND Zip=75038"</c>.</param>
		/// <returns>An instance of <see cref="RetailRouteBonusMinutesRow"/> or null reference 
		/// (Nothing in Visual Basic) if the object was not found.</returns>
		public RetailRouteBonusMinutesRow GetRow(string whereSql)
		{
			int totalRecordCount = -1;
			RetailRouteBonusMinutesRow[] rows = GetAsArray(whereSql, null, 0, 1, ref totalRecordCount);
			return 0 == rows.Length ? null : rows[0];
		}

		/// <summary>
		/// Gets an array of <see cref="RetailRouteBonusMinutesRow"/> objects that 
		/// match the search condition, in the the specified sort order.
		/// </summary>
		/// <param name="whereSql">The SQL search condition. For example: 
		/// <c>"FirstName='Smith' AND Zip=75038"</c>.</param>
		/// <param name="orderBySql">The column name(s) followed by "ASC" (ascending) or "DESC" (descending).
		/// Columns are sorted in ascending order by default. For example: <c>"LastName ASC, FirstName ASC"</c>.</param>
		/// <returns>An array of <see cref="RetailRouteBonusMinutesRow"/> objects.</returns>
		public RetailRouteBonusMinutesRow[] GetAsArray(string whereSql, string orderBySql)
		{
			int totalRecordCount = -1;
			return GetAsArray(whereSql, orderBySql, 0, int.MaxValue, ref totalRecordCount);
		}

		/// <summary>
		/// Gets an array of <see cref="RetailRouteBonusMinutesRow"/> objects that 
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
		/// <returns>An array of <see cref="RetailRouteBonusMinutesRow"/> objects.</returns>
		public virtual RetailRouteBonusMinutesRow[] GetAsArray(string whereSql, string orderBySql,
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
			string sql = "SELECT * FROM [dbo].[RetailRouteBonusMinutes]";
			if(null != whereSql && 0 < whereSql.Length)
				sql += " WHERE " + whereSql;
			if(null != orderBySql && 0 < orderBySql.Length)
				sql += " ORDER BY " + orderBySql;
			return _db.CreateCommand(sql);
		}

		/// <summary>
		/// Gets <see cref="RetailRouteBonusMinutesRow"/> by the primary key.
		/// </summary>
		/// <param name="retail_acct_id">The <c>retail_acct_id</c> column value.</param>
		/// <param name="retail_route_id">The <c>retail_route_id</c> column value.</param>
		/// <returns>An instance of <see cref="RetailRouteBonusMinutesRow"/> or null reference 
		/// (Nothing in Visual Basic) if the object was not found.</returns>
		public virtual RetailRouteBonusMinutesRow GetByPrimaryKey(int retail_acct_id, int retail_route_id)
		{
			string whereSql = "[retail_acct_id]=" + _db.CreateSqlParameterName("Retail_acct_id") + " AND " +
							  "[retail_route_id]=" + _db.CreateSqlParameterName("Retail_route_id");
			IDbCommand cmd = CreateGetCommand(whereSql, null);
			AddParameter(cmd, "Retail_acct_id", retail_acct_id);
			AddParameter(cmd, "Retail_route_id", retail_route_id);
			RetailRouteBonusMinutesRow[] tempArray = MapRecords(cmd);
			return 0 == tempArray.Length ? null : tempArray[0];
		}

		/// <summary>
		/// Gets an array of <see cref="RetailRouteBonusMinutesRow"/> objects 
		/// by the <c>R_367</c> foreign key.
		/// </summary>
		/// <param name="retail_route_id">The <c>retail_route_id</c> column value.</param>
		/// <returns>An array of <see cref="RetailRouteBonusMinutesRow"/> objects.</returns>
		public virtual RetailRouteBonusMinutesRow[] GetByRetail_route_id(int retail_route_id)
		{
			return MapRecords(CreateGetByRetail_route_idCommand(retail_route_id));
		}

		/// <summary>
		/// Gets a <see cref="System.Data.DataTable"/> object 
		/// by the <c>R_367</c> foreign key.
		/// </summary>
		/// <param name="retail_route_id">The <c>retail_route_id</c> column value.</param>
		/// <returns>A reference to the <see cref="System.Data.DataTable"/> object.</returns>
		public virtual DataTable GetByRetail_route_idAsDataTable(int retail_route_id)
		{
			return MapRecordsToDataTable(CreateGetByRetail_route_idCommand(retail_route_id));
		}

		/// <summary>
		/// Creates an <see cref="System.Data.IDbCommand"/> object that can be used to 
		/// return records by the <c>R_367</c> foreign key.
		/// </summary>
		/// <param name="retail_route_id">The <c>retail_route_id</c> column value.</param>
		/// <returns>A reference to the <see cref="System.Data.IDbCommand"/> object.</returns>
		protected virtual IDbCommand CreateGetByRetail_route_idCommand(int retail_route_id)
		{
			string whereSql = "";
			whereSql += "[retail_route_id]=" + _db.CreateSqlParameterName("Retail_route_id");

			IDbCommand cmd = CreateGetCommand(whereSql, null);
			AddParameter(cmd, "Retail_route_id", retail_route_id);
			return cmd;
		}

		/// <summary>
		/// Gets an array of <see cref="RetailRouteBonusMinutesRow"/> objects 
		/// by the <c>R_368</c> foreign key.
		/// </summary>
		/// <param name="retail_acct_id">The <c>retail_acct_id</c> column value.</param>
		/// <returns>An array of <see cref="RetailRouteBonusMinutesRow"/> objects.</returns>
		public virtual RetailRouteBonusMinutesRow[] GetByRetail_acct_id(int retail_acct_id)
		{
			return MapRecords(CreateGetByRetail_acct_idCommand(retail_acct_id));
		}

		/// <summary>
		/// Gets a <see cref="System.Data.DataTable"/> object 
		/// by the <c>R_368</c> foreign key.
		/// </summary>
		/// <param name="retail_acct_id">The <c>retail_acct_id</c> column value.</param>
		/// <returns>A reference to the <see cref="System.Data.DataTable"/> object.</returns>
		public virtual DataTable GetByRetail_acct_idAsDataTable(int retail_acct_id)
		{
			return MapRecordsToDataTable(CreateGetByRetail_acct_idCommand(retail_acct_id));
		}

		/// <summary>
		/// Creates an <see cref="System.Data.IDbCommand"/> object that can be used to 
		/// return records by the <c>R_368</c> foreign key.
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
		/// Adds a new record into the <c>RetailRouteBonusMinutes</c> table.
		/// </summary>
		/// <param name="value">The <see cref="RetailRouteBonusMinutesRow"/> object to be inserted.</param>
		public virtual void Insert(RetailRouteBonusMinutesRow value)
		{
			string sqlStr = "INSERT INTO [dbo].[RetailRouteBonusMinutes] (" +
				"[retail_acct_id], " +
				"[retail_route_id], " +
				"[start_bonus_minutes], " +
				"[current_bonus_minutes]" +
				") VALUES (" +
				_db.CreateSqlParameterName("Retail_acct_id") + ", " +
				_db.CreateSqlParameterName("Retail_route_id") + ", " +
				_db.CreateSqlParameterName("Start_bonus_minutes") + ", " +
				_db.CreateSqlParameterName("Current_bonus_minutes") + ")";
			IDbCommand cmd = _db.CreateCommand(sqlStr);
			AddParameter(cmd, "Retail_acct_id", value.Retail_acct_id);
			AddParameter(cmd, "Retail_route_id", value.Retail_route_id);
			AddParameter(cmd, "Start_bonus_minutes", value.Start_bonus_minutes);
			AddParameter(cmd, "Current_bonus_minutes", value.Current_bonus_minutes);
			cmd.ExecuteNonQuery();
		}

		/// <summary>
		/// Updates a record in the <c>RetailRouteBonusMinutes</c> table.
		/// </summary>
		/// <param name="value">The <see cref="RetailRouteBonusMinutesRow"/>
		/// object used to update the table record.</param>
		/// <returns>true if the record was updated; otherwise, false.</returns>
		public virtual bool Update(RetailRouteBonusMinutesRow value)
		{
			string sqlStr = "UPDATE [dbo].[RetailRouteBonusMinutes] SET " +
				"[start_bonus_minutes]=" + _db.CreateSqlParameterName("Start_bonus_minutes") + ", " +
				"[current_bonus_minutes]=" + _db.CreateSqlParameterName("Current_bonus_minutes") +
				" WHERE " +
				"[retail_acct_id]=" + _db.CreateSqlParameterName("Retail_acct_id") + " AND " +
				"[retail_route_id]=" + _db.CreateSqlParameterName("Retail_route_id");
			IDbCommand cmd = _db.CreateCommand(sqlStr);
			AddParameter(cmd, "Start_bonus_minutes", value.Start_bonus_minutes);
			AddParameter(cmd, "Current_bonus_minutes", value.Current_bonus_minutes);
			AddParameter(cmd, "Retail_acct_id", value.Retail_acct_id);
			AddParameter(cmd, "Retail_route_id", value.Retail_route_id);
			return 0 != cmd.ExecuteNonQuery();
		}

		/// <summary>
		/// Updates the <c>RetailRouteBonusMinutes</c> table and calls the <c>AcceptChanges</c> method
		/// on the changed DataRow objects.
		/// </summary>
		/// <param name="table">The <see cref="System.Data.DataTable"/> used to update the data source.</param>
		public void Update(DataTable table)
		{
			Update(table, true);
		}

		/// <summary>
		/// Updates the <c>RetailRouteBonusMinutes</c> table. Pass <c>false</c> as the <c>acceptChanges</c> 
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
							DeleteByPrimaryKey((int)row["Retail_acct_id"], (int)row["Retail_route_id"]);
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
		/// Deletes the specified object from the <c>RetailRouteBonusMinutes</c> table.
		/// </summary>
		/// <param name="value">The <see cref="RetailRouteBonusMinutesRow"/> object to delete.</param>
		/// <returns>true if the record was deleted; otherwise, false.</returns>
		public bool Delete(RetailRouteBonusMinutesRow value)
		{
			return DeleteByPrimaryKey(value.Retail_acct_id, value.Retail_route_id);
		}

		/// <summary>
		/// Deletes a record from the <c>RetailRouteBonusMinutes</c> table using
		/// the specified primary key.
		/// </summary>
		/// <param name="retail_acct_id">The <c>retail_acct_id</c> column value.</param>
		/// <param name="retail_route_id">The <c>retail_route_id</c> column value.</param>
		/// <returns>true if the record was deleted; otherwise, false.</returns>
		public virtual bool DeleteByPrimaryKey(int retail_acct_id, int retail_route_id)
		{
			string whereSql = "[retail_acct_id]=" + _db.CreateSqlParameterName("Retail_acct_id") + " AND " +
							  "[retail_route_id]=" + _db.CreateSqlParameterName("Retail_route_id");
			IDbCommand cmd = CreateDeleteCommand(whereSql);
			AddParameter(cmd, "Retail_acct_id", retail_acct_id);
			AddParameter(cmd, "Retail_route_id", retail_route_id);
			return 0 < cmd.ExecuteNonQuery();
		}

		/// <summary>
		/// Deletes records from the <c>RetailRouteBonusMinutes</c> table using the 
		/// <c>R_367</c> foreign key.
		/// </summary>
		/// <param name="retail_route_id">The <c>retail_route_id</c> column value.</param>
		/// <returns>The number of records deleted from the table.</returns>
		public int DeleteByRetail_route_id(int retail_route_id)
		{
			return CreateDeleteByRetail_route_idCommand(retail_route_id).ExecuteNonQuery();
		}

		/// <summary>
		/// Creates an <see cref="System.Data.IDbCommand"/> object that can be used to
		/// delete records using the <c>R_367</c> foreign key.
		/// </summary>
		/// <param name="retail_route_id">The <c>retail_route_id</c> column value.</param>
		/// <returns>A reference to the <see cref="System.Data.IDbCommand"/> object.</returns>
		protected virtual IDbCommand CreateDeleteByRetail_route_idCommand(int retail_route_id)
		{
			string whereSql = "";
			whereSql += "[retail_route_id]=" + _db.CreateSqlParameterName("Retail_route_id");

			IDbCommand cmd = CreateDeleteCommand(whereSql);
			AddParameter(cmd, "Retail_route_id", retail_route_id);
			return cmd;
		}

		/// <summary>
		/// Deletes records from the <c>RetailRouteBonusMinutes</c> table using the 
		/// <c>R_368</c> foreign key.
		/// </summary>
		/// <param name="retail_acct_id">The <c>retail_acct_id</c> column value.</param>
		/// <returns>The number of records deleted from the table.</returns>
		public int DeleteByRetail_acct_id(int retail_acct_id)
		{
			return CreateDeleteByRetail_acct_idCommand(retail_acct_id).ExecuteNonQuery();
		}

		/// <summary>
		/// Creates an <see cref="System.Data.IDbCommand"/> object that can be used to
		/// delete records using the <c>R_368</c> foreign key.
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
		/// Deletes <c>RetailRouteBonusMinutes</c> records that match the specified criteria.
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
		/// to delete <c>RetailRouteBonusMinutes</c> records that match the specified criteria.
		/// </summary>
		/// <param name="whereSql">The SQL search condition. 
		/// For example: <c>"FirstName='Smith' AND Zip=75038"</c>.</param>
		/// <returns>A reference to the <see cref="System.Data.IDbCommand"/> object.</returns>
		protected virtual IDbCommand CreateDeleteCommand(string whereSql)
		{
			string sql = "DELETE FROM [dbo].[RetailRouteBonusMinutes]";
			if(null != whereSql && 0 < whereSql.Length)
				sql += " WHERE " + whereSql;
			return _db.CreateCommand(sql);
		}

		/// <summary>
		/// Deletes all records from the <c>RetailRouteBonusMinutes</c> table.
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
		/// <returns>An array of <see cref="RetailRouteBonusMinutesRow"/> objects.</returns>
		protected RetailRouteBonusMinutesRow[] MapRecords(IDbCommand command)
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
		/// <returns>An array of <see cref="RetailRouteBonusMinutesRow"/> objects.</returns>
		protected RetailRouteBonusMinutesRow[] MapRecords(IDataReader reader)
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
		/// <returns>An array of <see cref="RetailRouteBonusMinutesRow"/> objects.</returns>
		protected virtual RetailRouteBonusMinutesRow[] MapRecords(IDataReader reader, 
										int startIndex, int length, ref int totalRecordCount)
		{
			if(0 > startIndex)
				throw new ArgumentOutOfRangeException("startIndex", startIndex, "StartIndex cannot be less than zero.");
			if(0 > length)
				throw new ArgumentOutOfRangeException("length", length, "Length cannot be less than zero.");

			int retail_acct_idColumnIndex = reader.GetOrdinal("retail_acct_id");
			int retail_route_idColumnIndex = reader.GetOrdinal("retail_route_id");
			int start_bonus_minutesColumnIndex = reader.GetOrdinal("start_bonus_minutes");
			int current_bonus_minutesColumnIndex = reader.GetOrdinal("current_bonus_minutes");

			System.Collections.ArrayList recordList = new System.Collections.ArrayList();
			int ri = -startIndex;
			while(reader.Read())
			{
				ri++;
				if(ri > 0 && ri <= length)
				{
					RetailRouteBonusMinutesRow record = new RetailRouteBonusMinutesRow();
					recordList.Add(record);

					record.Retail_acct_id = Convert.ToInt32(reader.GetValue(retail_acct_idColumnIndex));
					record.Retail_route_id = Convert.ToInt32(reader.GetValue(retail_route_idColumnIndex));
					record.Start_bonus_minutes = Convert.ToInt16(reader.GetValue(start_bonus_minutesColumnIndex));
					record.Current_bonus_minutes = Convert.ToInt16(reader.GetValue(current_bonus_minutesColumnIndex));

					if(ri == length && 0 != totalRecordCount)
						break;
				}
			}

			totalRecordCount = 0 == totalRecordCount ? ri + startIndex : -1;
			return (RetailRouteBonusMinutesRow[])(recordList.ToArray(typeof(RetailRouteBonusMinutesRow)));
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
		/// Converts <see cref="System.Data.DataRow"/> to <see cref="RetailRouteBonusMinutesRow"/>.
		/// </summary>
		/// <param name="row">The <see cref="System.Data.DataRow"/> object to be mapped.</param>
		/// <returns>A reference to the <see cref="RetailRouteBonusMinutesRow"/> object.</returns>
		protected virtual RetailRouteBonusMinutesRow MapRow(DataRow row)
		{
			RetailRouteBonusMinutesRow mappedObject = new RetailRouteBonusMinutesRow();
			DataTable dataTable = row.Table;
			DataColumn dataColumn;
			// Column "Retail_acct_id"
			dataColumn = dataTable.Columns["Retail_acct_id"];
			if(!row.IsNull(dataColumn))
				mappedObject.Retail_acct_id = (int)row[dataColumn];
			// Column "Retail_route_id"
			dataColumn = dataTable.Columns["Retail_route_id"];
			if(!row.IsNull(dataColumn))
				mappedObject.Retail_route_id = (int)row[dataColumn];
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
		/// the <c>RetailRouteBonusMinutes</c> table.
		/// </summary>
		/// <returns>A reference to the <see cref="System.Data.DataTable"/> object.</returns>
		protected virtual DataTable CreateDataTable()
		{
			DataTable dataTable = new DataTable();
			dataTable.TableName = "RetailRouteBonusMinutes";
			DataColumn dataColumn;
			dataColumn = dataTable.Columns.Add("Retail_acct_id", typeof(int));
			dataColumn.Caption = "retail_acct_id";
			dataColumn.AllowDBNull = false;
			dataColumn = dataTable.Columns.Add("Retail_route_id", typeof(int));
			dataColumn.Caption = "retail_route_id";
			dataColumn.AllowDBNull = false;
			dataColumn = dataTable.Columns.Add("Start_bonus_minutes", typeof(short));
			dataColumn.Caption = "start_bonus_minutes";
			dataColumn.AllowDBNull = false;
			dataColumn = dataTable.Columns.Add("Current_bonus_minutes", typeof(short));
			dataColumn.Caption = "current_bonus_minutes";
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

				case "Retail_route_id":
					parameter = _db.AddParameter(cmd, paramName, DbType.Int32, value);
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

	} // End of RetailRouteBonusMinutesCollection_Base class
}  // End of namespace

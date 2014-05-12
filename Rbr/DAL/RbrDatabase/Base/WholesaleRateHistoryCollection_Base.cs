// <fileinfo name="Base\WholesaleRateHistoryCollection_Base.cs">
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
	/// The base class for <see cref="WholesaleRateHistoryCollection"/>. Provides methods 
	/// for common database table operations. 
	/// </summary>
	/// <remarks>
	/// Do not change this source code. Update the <see cref="WholesaleRateHistoryCollection"/>
	/// class if you need to add or change some functionality.
	/// </remarks>
	public abstract class WholesaleRateHistoryCollection_Base
	{
		// Constants
		public const string Wholesale_route_idColumnName = "wholesale_route_id";
		public const string Date_onColumnName = "date_on";
		public const string Date_offColumnName = "date_off";
		public const string Rate_info_idColumnName = "rate_info_id";

		// Instance fields
		private Rbr_Db _db;

		/// <summary>
		/// Initializes a new instance of the <see cref="WholesaleRateHistoryCollection_Base"/> 
		/// class with the specified <see cref="Rbr_Db"/>.
		/// </summary>
		/// <param name="db">The <see cref="Rbr_Db"/> object.</param>
		public WholesaleRateHistoryCollection_Base(Rbr_Db db)
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
		/// Gets an array of all records from the <c>WholesaleRateHistory</c> table.
		/// </summary>
		/// <returns>An array of <see cref="WholesaleRateHistoryRow"/> objects.</returns>
		public virtual WholesaleRateHistoryRow[] GetAll()
		{
			return MapRecords(CreateGetAllCommand());
		}

		/// <summary>
		/// Gets a <see cref="System.Data.DataTable"/> object that 
		/// includes all records from the <c>WholesaleRateHistory</c> table.
		/// </summary>
		/// <returns>A reference to the <see cref="System.Data.DataTable"/> object.</returns>
		public virtual DataTable GetAllAsDataTable()
		{
			return MapRecordsToDataTable(CreateGetAllCommand());
		}

		/// <summary>
		/// Creates and returns an <see cref="System.Data.IDbCommand"/> object that is used
		/// to retrieve all records from the <c>WholesaleRateHistory</c> table.
		/// </summary>
		/// <returns>A reference to the <see cref="System.Data.IDbCommand"/> object.</returns>
		protected virtual IDbCommand CreateGetAllCommand()
		{
			return CreateGetCommand(null, null);
		}

		/// <summary>
		/// Gets the first <see cref="WholesaleRateHistoryRow"/> objects that 
		/// match the search condition.
		/// </summary>
		/// <param name="whereSql">The SQL search condition. For example: 
		/// <c>"FirstName='Smith' AND Zip=75038"</c>.</param>
		/// <returns>An instance of <see cref="WholesaleRateHistoryRow"/> or null reference 
		/// (Nothing in Visual Basic) if the object was not found.</returns>
		public WholesaleRateHistoryRow GetRow(string whereSql)
		{
			int totalRecordCount = -1;
			WholesaleRateHistoryRow[] rows = GetAsArray(whereSql, null, 0, 1, ref totalRecordCount);
			return 0 == rows.Length ? null : rows[0];
		}

		/// <summary>
		/// Gets an array of <see cref="WholesaleRateHistoryRow"/> objects that 
		/// match the search condition, in the the specified sort order.
		/// </summary>
		/// <param name="whereSql">The SQL search condition. For example: 
		/// <c>"FirstName='Smith' AND Zip=75038"</c>.</param>
		/// <param name="orderBySql">The column name(s) followed by "ASC" (ascending) or "DESC" (descending).
		/// Columns are sorted in ascending order by default. For example: <c>"LastName ASC, FirstName ASC"</c>.</param>
		/// <returns>An array of <see cref="WholesaleRateHistoryRow"/> objects.</returns>
		public WholesaleRateHistoryRow[] GetAsArray(string whereSql, string orderBySql)
		{
			int totalRecordCount = -1;
			return GetAsArray(whereSql, orderBySql, 0, int.MaxValue, ref totalRecordCount);
		}

		/// <summary>
		/// Gets an array of <see cref="WholesaleRateHistoryRow"/> objects that 
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
		/// <returns>An array of <see cref="WholesaleRateHistoryRow"/> objects.</returns>
		public virtual WholesaleRateHistoryRow[] GetAsArray(string whereSql, string orderBySql,
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
			string sql = "SELECT * FROM [dbo].[WholesaleRateHistory]";
			if(null != whereSql && 0 < whereSql.Length)
				sql += " WHERE " + whereSql;
			if(null != orderBySql && 0 < orderBySql.Length)
				sql += " ORDER BY " + orderBySql;
			return _db.CreateCommand(sql);
		}

		/// <summary>
		/// Gets <see cref="WholesaleRateHistoryRow"/> by the primary key.
		/// </summary>
		/// <param name="wholesale_route_id">The <c>wholesale_route_id</c> column value.</param>
		/// <param name="date_on">The <c>date_on</c> column value.</param>
		/// <returns>An instance of <see cref="WholesaleRateHistoryRow"/> or null reference 
		/// (Nothing in Visual Basic) if the object was not found.</returns>
		public virtual WholesaleRateHistoryRow GetByPrimaryKey(int wholesale_route_id, System.DateTime date_on)
		{
			string whereSql = "[wholesale_route_id]=" + _db.CreateSqlParameterName("Wholesale_route_id") + " AND " +
							  "[date_on]=" + _db.CreateSqlParameterName("Date_on");
			IDbCommand cmd = CreateGetCommand(whereSql, null);
			AddParameter(cmd, "Wholesale_route_id", wholesale_route_id);
			AddParameter(cmd, "Date_on", date_on);
			WholesaleRateHistoryRow[] tempArray = MapRecords(cmd);
			return 0 == tempArray.Length ? null : tempArray[0];
		}

		/// <summary>
		/// Gets an array of <see cref="WholesaleRateHistoryRow"/> objects 
		/// by the <c>R_334</c> foreign key.
		/// </summary>
		/// <param name="wholesale_route_id">The <c>wholesale_route_id</c> column value.</param>
		/// <returns>An array of <see cref="WholesaleRateHistoryRow"/> objects.</returns>
		public virtual WholesaleRateHistoryRow[] GetByWholesale_route_id(int wholesale_route_id)
		{
			return MapRecords(CreateGetByWholesale_route_idCommand(wholesale_route_id));
		}

		/// <summary>
		/// Gets a <see cref="System.Data.DataTable"/> object 
		/// by the <c>R_334</c> foreign key.
		/// </summary>
		/// <param name="wholesale_route_id">The <c>wholesale_route_id</c> column value.</param>
		/// <returns>A reference to the <see cref="System.Data.DataTable"/> object.</returns>
		public virtual DataTable GetByWholesale_route_idAsDataTable(int wholesale_route_id)
		{
			return MapRecordsToDataTable(CreateGetByWholesale_route_idCommand(wholesale_route_id));
		}

		/// <summary>
		/// Creates an <see cref="System.Data.IDbCommand"/> object that can be used to 
		/// return records by the <c>R_334</c> foreign key.
		/// </summary>
		/// <param name="wholesale_route_id">The <c>wholesale_route_id</c> column value.</param>
		/// <returns>A reference to the <see cref="System.Data.IDbCommand"/> object.</returns>
		protected virtual IDbCommand CreateGetByWholesale_route_idCommand(int wholesale_route_id)
		{
			string whereSql = "";
			whereSql += "[wholesale_route_id]=" + _db.CreateSqlParameterName("Wholesale_route_id");

			IDbCommand cmd = CreateGetCommand(whereSql, null);
			AddParameter(cmd, "Wholesale_route_id", wholesale_route_id);
			return cmd;
		}

		/// <summary>
		/// Gets an array of <see cref="WholesaleRateHistoryRow"/> objects 
		/// by the <c>R_335</c> foreign key.
		/// </summary>
		/// <param name="rate_info_id">The <c>rate_info_id</c> column value.</param>
		/// <returns>An array of <see cref="WholesaleRateHistoryRow"/> objects.</returns>
		public virtual WholesaleRateHistoryRow[] GetByRate_info_id(int rate_info_id)
		{
			return MapRecords(CreateGetByRate_info_idCommand(rate_info_id));
		}

		/// <summary>
		/// Gets a <see cref="System.Data.DataTable"/> object 
		/// by the <c>R_335</c> foreign key.
		/// </summary>
		/// <param name="rate_info_id">The <c>rate_info_id</c> column value.</param>
		/// <returns>A reference to the <see cref="System.Data.DataTable"/> object.</returns>
		public virtual DataTable GetByRate_info_idAsDataTable(int rate_info_id)
		{
			return MapRecordsToDataTable(CreateGetByRate_info_idCommand(rate_info_id));
		}

		/// <summary>
		/// Creates an <see cref="System.Data.IDbCommand"/> object that can be used to 
		/// return records by the <c>R_335</c> foreign key.
		/// </summary>
		/// <param name="rate_info_id">The <c>rate_info_id</c> column value.</param>
		/// <returns>A reference to the <see cref="System.Data.IDbCommand"/> object.</returns>
		protected virtual IDbCommand CreateGetByRate_info_idCommand(int rate_info_id)
		{
			string whereSql = "";
			whereSql += "[rate_info_id]=" + _db.CreateSqlParameterName("Rate_info_id");

			IDbCommand cmd = CreateGetCommand(whereSql, null);
			AddParameter(cmd, "Rate_info_id", rate_info_id);
			return cmd;
		}

		/// <summary>
		/// Adds a new record into the <c>WholesaleRateHistory</c> table.
		/// </summary>
		/// <param name="value">The <see cref="WholesaleRateHistoryRow"/> object to be inserted.</param>
		public virtual void Insert(WholesaleRateHistoryRow value)
		{
			string sqlStr = "INSERT INTO [dbo].[WholesaleRateHistory] (" +
				"[wholesale_route_id], " +
				"[date_on], " +
				"[date_off], " +
				"[rate_info_id]" +
				") VALUES (" +
				_db.CreateSqlParameterName("Wholesale_route_id") + ", " +
				_db.CreateSqlParameterName("Date_on") + ", " +
				_db.CreateSqlParameterName("Date_off") + ", " +
				_db.CreateSqlParameterName("Rate_info_id") + ")";
			IDbCommand cmd = _db.CreateCommand(sqlStr);
			AddParameter(cmd, "Wholesale_route_id", value.Wholesale_route_id);
			AddParameter(cmd, "Date_on", value.Date_on);
			AddParameter(cmd, "Date_off", value.Date_off);
			AddParameter(cmd, "Rate_info_id", value.Rate_info_id);
			cmd.ExecuteNonQuery();
		}

		/// <summary>
		/// Updates a record in the <c>WholesaleRateHistory</c> table.
		/// </summary>
		/// <param name="value">The <see cref="WholesaleRateHistoryRow"/>
		/// object used to update the table record.</param>
		/// <returns>true if the record was updated; otherwise, false.</returns>
		public virtual bool Update(WholesaleRateHistoryRow value)
		{
			string sqlStr = "UPDATE [dbo].[WholesaleRateHistory] SET " +
				"[date_off]=" + _db.CreateSqlParameterName("Date_off") + ", " +
				"[rate_info_id]=" + _db.CreateSqlParameterName("Rate_info_id") +
				" WHERE " +
				"[wholesale_route_id]=" + _db.CreateSqlParameterName("Wholesale_route_id") + " AND " +
				"[date_on]=" + _db.CreateSqlParameterName("Date_on");
			IDbCommand cmd = _db.CreateCommand(sqlStr);
			AddParameter(cmd, "Date_off", value.Date_off);
			AddParameter(cmd, "Rate_info_id", value.Rate_info_id);
			AddParameter(cmd, "Wholesale_route_id", value.Wholesale_route_id);
			AddParameter(cmd, "Date_on", value.Date_on);
			return 0 != cmd.ExecuteNonQuery();
		}

		/// <summary>
		/// Updates the <c>WholesaleRateHistory</c> table and calls the <c>AcceptChanges</c> method
		/// on the changed DataRow objects.
		/// </summary>
		/// <param name="table">The <see cref="System.Data.DataTable"/> used to update the data source.</param>
		public void Update(DataTable table)
		{
			Update(table, true);
		}

		/// <summary>
		/// Updates the <c>WholesaleRateHistory</c> table. Pass <c>false</c> as the <c>acceptChanges</c> 
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
							DeleteByPrimaryKey((int)row["Wholesale_route_id"], (System.DateTime)row["Date_on"]);
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
		/// Deletes the specified object from the <c>WholesaleRateHistory</c> table.
		/// </summary>
		/// <param name="value">The <see cref="WholesaleRateHistoryRow"/> object to delete.</param>
		/// <returns>true if the record was deleted; otherwise, false.</returns>
		public bool Delete(WholesaleRateHistoryRow value)
		{
			return DeleteByPrimaryKey(value.Wholesale_route_id, value.Date_on);
		}

		/// <summary>
		/// Deletes a record from the <c>WholesaleRateHistory</c> table using
		/// the specified primary key.
		/// </summary>
		/// <param name="wholesale_route_id">The <c>wholesale_route_id</c> column value.</param>
		/// <param name="date_on">The <c>date_on</c> column value.</param>
		/// <returns>true if the record was deleted; otherwise, false.</returns>
		public virtual bool DeleteByPrimaryKey(int wholesale_route_id, System.DateTime date_on)
		{
			string whereSql = "[wholesale_route_id]=" + _db.CreateSqlParameterName("Wholesale_route_id") + " AND " +
							  "[date_on]=" + _db.CreateSqlParameterName("Date_on");
			IDbCommand cmd = CreateDeleteCommand(whereSql);
			AddParameter(cmd, "Wholesale_route_id", wholesale_route_id);
			AddParameter(cmd, "Date_on", date_on);
			return 0 < cmd.ExecuteNonQuery();
		}

		/// <summary>
		/// Deletes records from the <c>WholesaleRateHistory</c> table using the 
		/// <c>R_334</c> foreign key.
		/// </summary>
		/// <param name="wholesale_route_id">The <c>wholesale_route_id</c> column value.</param>
		/// <returns>The number of records deleted from the table.</returns>
		public int DeleteByWholesale_route_id(int wholesale_route_id)
		{
			return CreateDeleteByWholesale_route_idCommand(wholesale_route_id).ExecuteNonQuery();
		}

		/// <summary>
		/// Creates an <see cref="System.Data.IDbCommand"/> object that can be used to
		/// delete records using the <c>R_334</c> foreign key.
		/// </summary>
		/// <param name="wholesale_route_id">The <c>wholesale_route_id</c> column value.</param>
		/// <returns>A reference to the <see cref="System.Data.IDbCommand"/> object.</returns>
		protected virtual IDbCommand CreateDeleteByWholesale_route_idCommand(int wholesale_route_id)
		{
			string whereSql = "";
			whereSql += "[wholesale_route_id]=" + _db.CreateSqlParameterName("Wholesale_route_id");

			IDbCommand cmd = CreateDeleteCommand(whereSql);
			AddParameter(cmd, "Wholesale_route_id", wholesale_route_id);
			return cmd;
		}

		/// <summary>
		/// Deletes records from the <c>WholesaleRateHistory</c> table using the 
		/// <c>R_335</c> foreign key.
		/// </summary>
		/// <param name="rate_info_id">The <c>rate_info_id</c> column value.</param>
		/// <returns>The number of records deleted from the table.</returns>
		public int DeleteByRate_info_id(int rate_info_id)
		{
			return CreateDeleteByRate_info_idCommand(rate_info_id).ExecuteNonQuery();
		}

		/// <summary>
		/// Creates an <see cref="System.Data.IDbCommand"/> object that can be used to
		/// delete records using the <c>R_335</c> foreign key.
		/// </summary>
		/// <param name="rate_info_id">The <c>rate_info_id</c> column value.</param>
		/// <returns>A reference to the <see cref="System.Data.IDbCommand"/> object.</returns>
		protected virtual IDbCommand CreateDeleteByRate_info_idCommand(int rate_info_id)
		{
			string whereSql = "";
			whereSql += "[rate_info_id]=" + _db.CreateSqlParameterName("Rate_info_id");

			IDbCommand cmd = CreateDeleteCommand(whereSql);
			AddParameter(cmd, "Rate_info_id", rate_info_id);
			return cmd;
		}

		/// <summary>
		/// Deletes <c>WholesaleRateHistory</c> records that match the specified criteria.
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
		/// to delete <c>WholesaleRateHistory</c> records that match the specified criteria.
		/// </summary>
		/// <param name="whereSql">The SQL search condition. 
		/// For example: <c>"FirstName='Smith' AND Zip=75038"</c>.</param>
		/// <returns>A reference to the <see cref="System.Data.IDbCommand"/> object.</returns>
		protected virtual IDbCommand CreateDeleteCommand(string whereSql)
		{
			string sql = "DELETE FROM [dbo].[WholesaleRateHistory]";
			if(null != whereSql && 0 < whereSql.Length)
				sql += " WHERE " + whereSql;
			return _db.CreateCommand(sql);
		}

		/// <summary>
		/// Deletes all records from the <c>WholesaleRateHistory</c> table.
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
		/// <returns>An array of <see cref="WholesaleRateHistoryRow"/> objects.</returns>
		protected WholesaleRateHistoryRow[] MapRecords(IDbCommand command)
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
		/// <returns>An array of <see cref="WholesaleRateHistoryRow"/> objects.</returns>
		protected WholesaleRateHistoryRow[] MapRecords(IDataReader reader)
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
		/// <returns>An array of <see cref="WholesaleRateHistoryRow"/> objects.</returns>
		protected virtual WholesaleRateHistoryRow[] MapRecords(IDataReader reader, 
										int startIndex, int length, ref int totalRecordCount)
		{
			if(0 > startIndex)
				throw new ArgumentOutOfRangeException("startIndex", startIndex, "StartIndex cannot be less than zero.");
			if(0 > length)
				throw new ArgumentOutOfRangeException("length", length, "Length cannot be less than zero.");

			int wholesale_route_idColumnIndex = reader.GetOrdinal("wholesale_route_id");
			int date_onColumnIndex = reader.GetOrdinal("date_on");
			int date_offColumnIndex = reader.GetOrdinal("date_off");
			int rate_info_idColumnIndex = reader.GetOrdinal("rate_info_id");

			System.Collections.ArrayList recordList = new System.Collections.ArrayList();
			int ri = -startIndex;
			while(reader.Read())
			{
				ri++;
				if(ri > 0 && ri <= length)
				{
					WholesaleRateHistoryRow record = new WholesaleRateHistoryRow();
					recordList.Add(record);

					record.Wholesale_route_id = Convert.ToInt32(reader.GetValue(wholesale_route_idColumnIndex));
					record.Date_on = Convert.ToDateTime(reader.GetValue(date_onColumnIndex));
					record.Date_off = Convert.ToDateTime(reader.GetValue(date_offColumnIndex));
					record.Rate_info_id = Convert.ToInt32(reader.GetValue(rate_info_idColumnIndex));

					if(ri == length && 0 != totalRecordCount)
						break;
				}
			}

			totalRecordCount = 0 == totalRecordCount ? ri + startIndex : -1;
			return (WholesaleRateHistoryRow[])(recordList.ToArray(typeof(WholesaleRateHistoryRow)));
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
		/// Converts <see cref="System.Data.DataRow"/> to <see cref="WholesaleRateHistoryRow"/>.
		/// </summary>
		/// <param name="row">The <see cref="System.Data.DataRow"/> object to be mapped.</param>
		/// <returns>A reference to the <see cref="WholesaleRateHistoryRow"/> object.</returns>
		protected virtual WholesaleRateHistoryRow MapRow(DataRow row)
		{
			WholesaleRateHistoryRow mappedObject = new WholesaleRateHistoryRow();
			DataTable dataTable = row.Table;
			DataColumn dataColumn;
			// Column "Wholesale_route_id"
			dataColumn = dataTable.Columns["Wholesale_route_id"];
			if(!row.IsNull(dataColumn))
				mappedObject.Wholesale_route_id = (int)row[dataColumn];
			// Column "Date_on"
			dataColumn = dataTable.Columns["Date_on"];
			if(!row.IsNull(dataColumn))
				mappedObject.Date_on = (System.DateTime)row[dataColumn];
			// Column "Date_off"
			dataColumn = dataTable.Columns["Date_off"];
			if(!row.IsNull(dataColumn))
				mappedObject.Date_off = (System.DateTime)row[dataColumn];
			// Column "Rate_info_id"
			dataColumn = dataTable.Columns["Rate_info_id"];
			if(!row.IsNull(dataColumn))
				mappedObject.Rate_info_id = (int)row[dataColumn];
			return mappedObject;
		}

		/// <summary>
		/// Creates a <see cref="System.Data.DataTable"/> object for 
		/// the <c>WholesaleRateHistory</c> table.
		/// </summary>
		/// <returns>A reference to the <see cref="System.Data.DataTable"/> object.</returns>
		protected virtual DataTable CreateDataTable()
		{
			DataTable dataTable = new DataTable();
			dataTable.TableName = "WholesaleRateHistory";
			DataColumn dataColumn;
			dataColumn = dataTable.Columns.Add("Wholesale_route_id", typeof(int));
			dataColumn.Caption = "wholesale_route_id";
			dataColumn.AllowDBNull = false;
			dataColumn = dataTable.Columns.Add("Date_on", typeof(System.DateTime));
			dataColumn.Caption = "date_on";
			dataColumn.AllowDBNull = false;
			dataColumn = dataTable.Columns.Add("Date_off", typeof(System.DateTime));
			dataColumn.Caption = "date_off";
			dataColumn.AllowDBNull = false;
			dataColumn = dataTable.Columns.Add("Rate_info_id", typeof(int));
			dataColumn.Caption = "rate_info_id";
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
				case "Wholesale_route_id":
					parameter = _db.AddParameter(cmd, paramName, DbType.Int32, value);
					break;

				case "Date_on":
					parameter = _db.AddParameter(cmd, paramName, DbType.DateTime, value);
					break;

				case "Date_off":
					parameter = _db.AddParameter(cmd, paramName, DbType.DateTime, value);
					break;

				case "Rate_info_id":
					parameter = _db.AddParameter(cmd, paramName, DbType.Int32, value);
					break;

				default:
					throw new ArgumentException("Unknown parameter name (" + paramName + ").");
			}
			return parameter;
		}

	} // End of WholesaleRateHistoryCollection_Base class
}  // End of namespace

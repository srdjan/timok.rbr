// <fileinfo name="Base\ResellRateHistoryCollection_Base.cs">
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
	/// The base class for <see cref="ResellRateHistoryCollection"/>. Provides methods 
	/// for common database table operations. 
	/// </summary>
	/// <remarks>
	/// Do not change this source code. Update the <see cref="ResellRateHistoryCollection"/>
	/// class if you need to add or change some functionality.
	/// </remarks>
	public abstract class ResellRateHistoryCollection_Base
	{
		// Constants
		public const string Resell_acct_idColumnName = "resell_acct_id";
		public const string Wholesale_route_idColumnName = "wholesale_route_id";
		public const string Date_onColumnName = "date_on";
		public const string Date_offColumnName = "date_off";
		public const string Rate_info_idColumnName = "rate_info_id";
		public const string Commision_typeColumnName = "commision_type";
		public const string Markup_dollarColumnName = "markup_dollar";
		public const string Markup_percentColumnName = "markup_percent";
		public const string Markup_per_callColumnName = "markup_per_call";
		public const string Markup_per_minuteColumnName = "markup_per_minute";

		// Instance fields
		private Rbr_Db _db;

		/// <summary>
		/// Initializes a new instance of the <see cref="ResellRateHistoryCollection_Base"/> 
		/// class with the specified <see cref="Rbr_Db"/>.
		/// </summary>
		/// <param name="db">The <see cref="Rbr_Db"/> object.</param>
		public ResellRateHistoryCollection_Base(Rbr_Db db)
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
		/// Gets an array of all records from the <c>ResellRateHistory</c> table.
		/// </summary>
		/// <returns>An array of <see cref="ResellRateHistoryRow"/> objects.</returns>
		public virtual ResellRateHistoryRow[] GetAll()
		{
			return MapRecords(CreateGetAllCommand());
		}

		/// <summary>
		/// Gets a <see cref="System.Data.DataTable"/> object that 
		/// includes all records from the <c>ResellRateHistory</c> table.
		/// </summary>
		/// <returns>A reference to the <see cref="System.Data.DataTable"/> object.</returns>
		public virtual DataTable GetAllAsDataTable()
		{
			return MapRecordsToDataTable(CreateGetAllCommand());
		}

		/// <summary>
		/// Creates and returns an <see cref="System.Data.IDbCommand"/> object that is used
		/// to retrieve all records from the <c>ResellRateHistory</c> table.
		/// </summary>
		/// <returns>A reference to the <see cref="System.Data.IDbCommand"/> object.</returns>
		protected virtual IDbCommand CreateGetAllCommand()
		{
			return CreateGetCommand(null, null);
		}

		/// <summary>
		/// Gets the first <see cref="ResellRateHistoryRow"/> objects that 
		/// match the search condition.
		/// </summary>
		/// <param name="whereSql">The SQL search condition. For example: 
		/// <c>"FirstName='Smith' AND Zip=75038"</c>.</param>
		/// <returns>An instance of <see cref="ResellRateHistoryRow"/> or null reference 
		/// (Nothing in Visual Basic) if the object was not found.</returns>
		public ResellRateHistoryRow GetRow(string whereSql)
		{
			int totalRecordCount = -1;
			ResellRateHistoryRow[] rows = GetAsArray(whereSql, null, 0, 1, ref totalRecordCount);
			return 0 == rows.Length ? null : rows[0];
		}

		/// <summary>
		/// Gets an array of <see cref="ResellRateHistoryRow"/> objects that 
		/// match the search condition, in the the specified sort order.
		/// </summary>
		/// <param name="whereSql">The SQL search condition. For example: 
		/// <c>"FirstName='Smith' AND Zip=75038"</c>.</param>
		/// <param name="orderBySql">The column name(s) followed by "ASC" (ascending) or "DESC" (descending).
		/// Columns are sorted in ascending order by default. For example: <c>"LastName ASC, FirstName ASC"</c>.</param>
		/// <returns>An array of <see cref="ResellRateHistoryRow"/> objects.</returns>
		public ResellRateHistoryRow[] GetAsArray(string whereSql, string orderBySql)
		{
			int totalRecordCount = -1;
			return GetAsArray(whereSql, orderBySql, 0, int.MaxValue, ref totalRecordCount);
		}

		/// <summary>
		/// Gets an array of <see cref="ResellRateHistoryRow"/> objects that 
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
		/// <returns>An array of <see cref="ResellRateHistoryRow"/> objects.</returns>
		public virtual ResellRateHistoryRow[] GetAsArray(string whereSql, string orderBySql,
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
			string sql = "SELECT * FROM [dbo].[ResellRateHistory]";
			if(null != whereSql && 0 < whereSql.Length)
				sql += " WHERE " + whereSql;
			if(null != orderBySql && 0 < orderBySql.Length)
				sql += " ORDER BY " + orderBySql;
			return _db.CreateCommand(sql);
		}

		/// <summary>
		/// Gets <see cref="ResellRateHistoryRow"/> by the primary key.
		/// </summary>
		/// <param name="resell_acct_id">The <c>resell_acct_id</c> column value.</param>
		/// <param name="wholesale_route_id">The <c>wholesale_route_id</c> column value.</param>
		/// <param name="date_on">The <c>date_on</c> column value.</param>
		/// <returns>An instance of <see cref="ResellRateHistoryRow"/> or null reference 
		/// (Nothing in Visual Basic) if the object was not found.</returns>
		public virtual ResellRateHistoryRow GetByPrimaryKey(short resell_acct_id, int wholesale_route_id, System.DateTime date_on)
		{
			string whereSql = "[resell_acct_id]=" + _db.CreateSqlParameterName("Resell_acct_id") + " AND " +
							  "[wholesale_route_id]=" + _db.CreateSqlParameterName("Wholesale_route_id") + " AND " +
							  "[date_on]=" + _db.CreateSqlParameterName("Date_on");
			IDbCommand cmd = CreateGetCommand(whereSql, null);
			AddParameter(cmd, "Resell_acct_id", resell_acct_id);
			AddParameter(cmd, "Wholesale_route_id", wholesale_route_id);
			AddParameter(cmd, "Date_on", date_on);
			ResellRateHistoryRow[] tempArray = MapRecords(cmd);
			return 0 == tempArray.Length ? null : tempArray[0];
		}

		/// <summary>
		/// Gets an array of <see cref="ResellRateHistoryRow"/> objects 
		/// by the <c>R_339</c> foreign key.
		/// </summary>
		/// <param name="rate_info_id">The <c>rate_info_id</c> column value.</param>
		/// <returns>An array of <see cref="ResellRateHistoryRow"/> objects.</returns>
		public virtual ResellRateHistoryRow[] GetByRate_info_id(int rate_info_id)
		{
			return MapRecords(CreateGetByRate_info_idCommand(rate_info_id));
		}

		/// <summary>
		/// Gets a <see cref="System.Data.DataTable"/> object 
		/// by the <c>R_339</c> foreign key.
		/// </summary>
		/// <param name="rate_info_id">The <c>rate_info_id</c> column value.</param>
		/// <returns>A reference to the <see cref="System.Data.DataTable"/> object.</returns>
		public virtual DataTable GetByRate_info_idAsDataTable(int rate_info_id)
		{
			return MapRecordsToDataTable(CreateGetByRate_info_idCommand(rate_info_id));
		}

		/// <summary>
		/// Creates an <see cref="System.Data.IDbCommand"/> object that can be used to 
		/// return records by the <c>R_339</c> foreign key.
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
		/// Gets an array of <see cref="ResellRateHistoryRow"/> objects 
		/// by the <c>R_360</c> foreign key.
		/// </summary>
		/// <param name="wholesale_route_id">The <c>wholesale_route_id</c> column value.</param>
		/// <returns>An array of <see cref="ResellRateHistoryRow"/> objects.</returns>
		public virtual ResellRateHistoryRow[] GetByWholesale_route_id(int wholesale_route_id)
		{
			return MapRecords(CreateGetByWholesale_route_idCommand(wholesale_route_id));
		}

		/// <summary>
		/// Gets a <see cref="System.Data.DataTable"/> object 
		/// by the <c>R_360</c> foreign key.
		/// </summary>
		/// <param name="wholesale_route_id">The <c>wholesale_route_id</c> column value.</param>
		/// <returns>A reference to the <see cref="System.Data.DataTable"/> object.</returns>
		public virtual DataTable GetByWholesale_route_idAsDataTable(int wholesale_route_id)
		{
			return MapRecordsToDataTable(CreateGetByWholesale_route_idCommand(wholesale_route_id));
		}

		/// <summary>
		/// Creates an <see cref="System.Data.IDbCommand"/> object that can be used to 
		/// return records by the <c>R_360</c> foreign key.
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
		/// Gets an array of <see cref="ResellRateHistoryRow"/> objects 
		/// by the <c>R_362</c> foreign key.
		/// </summary>
		/// <param name="resell_acct_id">The <c>resell_acct_id</c> column value.</param>
		/// <returns>An array of <see cref="ResellRateHistoryRow"/> objects.</returns>
		public virtual ResellRateHistoryRow[] GetByResell_acct_id(short resell_acct_id)
		{
			return MapRecords(CreateGetByResell_acct_idCommand(resell_acct_id));
		}

		/// <summary>
		/// Gets a <see cref="System.Data.DataTable"/> object 
		/// by the <c>R_362</c> foreign key.
		/// </summary>
		/// <param name="resell_acct_id">The <c>resell_acct_id</c> column value.</param>
		/// <returns>A reference to the <see cref="System.Data.DataTable"/> object.</returns>
		public virtual DataTable GetByResell_acct_idAsDataTable(short resell_acct_id)
		{
			return MapRecordsToDataTable(CreateGetByResell_acct_idCommand(resell_acct_id));
		}

		/// <summary>
		/// Creates an <see cref="System.Data.IDbCommand"/> object that can be used to 
		/// return records by the <c>R_362</c> foreign key.
		/// </summary>
		/// <param name="resell_acct_id">The <c>resell_acct_id</c> column value.</param>
		/// <returns>A reference to the <see cref="System.Data.IDbCommand"/> object.</returns>
		protected virtual IDbCommand CreateGetByResell_acct_idCommand(short resell_acct_id)
		{
			string whereSql = "";
			whereSql += "[resell_acct_id]=" + _db.CreateSqlParameterName("Resell_acct_id");

			IDbCommand cmd = CreateGetCommand(whereSql, null);
			AddParameter(cmd, "Resell_acct_id", resell_acct_id);
			return cmd;
		}

		/// <summary>
		/// Adds a new record into the <c>ResellRateHistory</c> table.
		/// </summary>
		/// <param name="value">The <see cref="ResellRateHistoryRow"/> object to be inserted.</param>
		public virtual void Insert(ResellRateHistoryRow value)
		{
			string sqlStr = "INSERT INTO [dbo].[ResellRateHistory] (" +
				"[resell_acct_id], " +
				"[wholesale_route_id], " +
				"[date_on], " +
				"[date_off], " +
				"[rate_info_id], " +
				"[commision_type], " +
				"[markup_dollar], " +
				"[markup_percent], " +
				"[markup_per_call], " +
				"[markup_per_minute]" +
				") VALUES (" +
				_db.CreateSqlParameterName("Resell_acct_id") + ", " +
				_db.CreateSqlParameterName("Wholesale_route_id") + ", " +
				_db.CreateSqlParameterName("Date_on") + ", " +
				_db.CreateSqlParameterName("Date_off") + ", " +
				_db.CreateSqlParameterName("Rate_info_id") + ", " +
				_db.CreateSqlParameterName("Commision_type") + ", " +
				_db.CreateSqlParameterName("Markup_dollar") + ", " +
				_db.CreateSqlParameterName("Markup_percent") + ", " +
				_db.CreateSqlParameterName("Markup_per_call") + ", " +
				_db.CreateSqlParameterName("Markup_per_minute") + ")";
			IDbCommand cmd = _db.CreateCommand(sqlStr);
			AddParameter(cmd, "Resell_acct_id", value.Resell_acct_id);
			AddParameter(cmd, "Wholesale_route_id", value.Wholesale_route_id);
			AddParameter(cmd, "Date_on", value.Date_on);
			AddParameter(cmd, "Date_off", value.Date_off);
			AddParameter(cmd, "Rate_info_id", value.Rate_info_id);
			AddParameter(cmd, "Commision_type", value.Commision_type);
			AddParameter(cmd, "Markup_dollar", value.Markup_dollar);
			AddParameter(cmd, "Markup_percent", value.Markup_percent);
			AddParameter(cmd, "Markup_per_call", value.Markup_per_call);
			AddParameter(cmd, "Markup_per_minute", value.Markup_per_minute);
			cmd.ExecuteNonQuery();
		}

		/// <summary>
		/// Updates a record in the <c>ResellRateHistory</c> table.
		/// </summary>
		/// <param name="value">The <see cref="ResellRateHistoryRow"/>
		/// object used to update the table record.</param>
		/// <returns>true if the record was updated; otherwise, false.</returns>
		public virtual bool Update(ResellRateHistoryRow value)
		{
			string sqlStr = "UPDATE [dbo].[ResellRateHistory] SET " +
				"[date_off]=" + _db.CreateSqlParameterName("Date_off") + ", " +
				"[rate_info_id]=" + _db.CreateSqlParameterName("Rate_info_id") + ", " +
				"[commision_type]=" + _db.CreateSqlParameterName("Commision_type") + ", " +
				"[markup_dollar]=" + _db.CreateSqlParameterName("Markup_dollar") + ", " +
				"[markup_percent]=" + _db.CreateSqlParameterName("Markup_percent") + ", " +
				"[markup_per_call]=" + _db.CreateSqlParameterName("Markup_per_call") + ", " +
				"[markup_per_minute]=" + _db.CreateSqlParameterName("Markup_per_minute") +
				" WHERE " +
				"[resell_acct_id]=" + _db.CreateSqlParameterName("Resell_acct_id") + " AND " +
				"[wholesale_route_id]=" + _db.CreateSqlParameterName("Wholesale_route_id") + " AND " +
				"[date_on]=" + _db.CreateSqlParameterName("Date_on");
			IDbCommand cmd = _db.CreateCommand(sqlStr);
			AddParameter(cmd, "Date_off", value.Date_off);
			AddParameter(cmd, "Rate_info_id", value.Rate_info_id);
			AddParameter(cmd, "Commision_type", value.Commision_type);
			AddParameter(cmd, "Markup_dollar", value.Markup_dollar);
			AddParameter(cmd, "Markup_percent", value.Markup_percent);
			AddParameter(cmd, "Markup_per_call", value.Markup_per_call);
			AddParameter(cmd, "Markup_per_minute", value.Markup_per_minute);
			AddParameter(cmd, "Resell_acct_id", value.Resell_acct_id);
			AddParameter(cmd, "Wholesale_route_id", value.Wholesale_route_id);
			AddParameter(cmd, "Date_on", value.Date_on);
			return 0 != cmd.ExecuteNonQuery();
		}

		/// <summary>
		/// Updates the <c>ResellRateHistory</c> table and calls the <c>AcceptChanges</c> method
		/// on the changed DataRow objects.
		/// </summary>
		/// <param name="table">The <see cref="System.Data.DataTable"/> used to update the data source.</param>
		public void Update(DataTable table)
		{
			Update(table, true);
		}

		/// <summary>
		/// Updates the <c>ResellRateHistory</c> table. Pass <c>false</c> as the <c>acceptChanges</c> 
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
							DeleteByPrimaryKey((short)row["Resell_acct_id"], (int)row["Wholesale_route_id"], (System.DateTime)row["Date_on"]);
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
		/// Deletes the specified object from the <c>ResellRateHistory</c> table.
		/// </summary>
		/// <param name="value">The <see cref="ResellRateHistoryRow"/> object to delete.</param>
		/// <returns>true if the record was deleted; otherwise, false.</returns>
		public bool Delete(ResellRateHistoryRow value)
		{
			return DeleteByPrimaryKey(value.Resell_acct_id, value.Wholesale_route_id, value.Date_on);
		}

		/// <summary>
		/// Deletes a record from the <c>ResellRateHistory</c> table using
		/// the specified primary key.
		/// </summary>
		/// <param name="resell_acct_id">The <c>resell_acct_id</c> column value.</param>
		/// <param name="wholesale_route_id">The <c>wholesale_route_id</c> column value.</param>
		/// <param name="date_on">The <c>date_on</c> column value.</param>
		/// <returns>true if the record was deleted; otherwise, false.</returns>
		public virtual bool DeleteByPrimaryKey(short resell_acct_id, int wholesale_route_id, System.DateTime date_on)
		{
			string whereSql = "[resell_acct_id]=" + _db.CreateSqlParameterName("Resell_acct_id") + " AND " +
							  "[wholesale_route_id]=" + _db.CreateSqlParameterName("Wholesale_route_id") + " AND " +
							  "[date_on]=" + _db.CreateSqlParameterName("Date_on");
			IDbCommand cmd = CreateDeleteCommand(whereSql);
			AddParameter(cmd, "Resell_acct_id", resell_acct_id);
			AddParameter(cmd, "Wholesale_route_id", wholesale_route_id);
			AddParameter(cmd, "Date_on", date_on);
			return 0 < cmd.ExecuteNonQuery();
		}

		/// <summary>
		/// Deletes records from the <c>ResellRateHistory</c> table using the 
		/// <c>R_339</c> foreign key.
		/// </summary>
		/// <param name="rate_info_id">The <c>rate_info_id</c> column value.</param>
		/// <returns>The number of records deleted from the table.</returns>
		public int DeleteByRate_info_id(int rate_info_id)
		{
			return CreateDeleteByRate_info_idCommand(rate_info_id).ExecuteNonQuery();
		}

		/// <summary>
		/// Creates an <see cref="System.Data.IDbCommand"/> object that can be used to
		/// delete records using the <c>R_339</c> foreign key.
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
		/// Deletes records from the <c>ResellRateHistory</c> table using the 
		/// <c>R_360</c> foreign key.
		/// </summary>
		/// <param name="wholesale_route_id">The <c>wholesale_route_id</c> column value.</param>
		/// <returns>The number of records deleted from the table.</returns>
		public int DeleteByWholesale_route_id(int wholesale_route_id)
		{
			return CreateDeleteByWholesale_route_idCommand(wholesale_route_id).ExecuteNonQuery();
		}

		/// <summary>
		/// Creates an <see cref="System.Data.IDbCommand"/> object that can be used to
		/// delete records using the <c>R_360</c> foreign key.
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
		/// Deletes records from the <c>ResellRateHistory</c> table using the 
		/// <c>R_362</c> foreign key.
		/// </summary>
		/// <param name="resell_acct_id">The <c>resell_acct_id</c> column value.</param>
		/// <returns>The number of records deleted from the table.</returns>
		public int DeleteByResell_acct_id(short resell_acct_id)
		{
			return CreateDeleteByResell_acct_idCommand(resell_acct_id).ExecuteNonQuery();
		}

		/// <summary>
		/// Creates an <see cref="System.Data.IDbCommand"/> object that can be used to
		/// delete records using the <c>R_362</c> foreign key.
		/// </summary>
		/// <param name="resell_acct_id">The <c>resell_acct_id</c> column value.</param>
		/// <returns>A reference to the <see cref="System.Data.IDbCommand"/> object.</returns>
		protected virtual IDbCommand CreateDeleteByResell_acct_idCommand(short resell_acct_id)
		{
			string whereSql = "";
			whereSql += "[resell_acct_id]=" + _db.CreateSqlParameterName("Resell_acct_id");

			IDbCommand cmd = CreateDeleteCommand(whereSql);
			AddParameter(cmd, "Resell_acct_id", resell_acct_id);
			return cmd;
		}

		/// <summary>
		/// Deletes <c>ResellRateHistory</c> records that match the specified criteria.
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
		/// to delete <c>ResellRateHistory</c> records that match the specified criteria.
		/// </summary>
		/// <param name="whereSql">The SQL search condition. 
		/// For example: <c>"FirstName='Smith' AND Zip=75038"</c>.</param>
		/// <returns>A reference to the <see cref="System.Data.IDbCommand"/> object.</returns>
		protected virtual IDbCommand CreateDeleteCommand(string whereSql)
		{
			string sql = "DELETE FROM [dbo].[ResellRateHistory]";
			if(null != whereSql && 0 < whereSql.Length)
				sql += " WHERE " + whereSql;
			return _db.CreateCommand(sql);
		}

		/// <summary>
		/// Deletes all records from the <c>ResellRateHistory</c> table.
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
		/// <returns>An array of <see cref="ResellRateHistoryRow"/> objects.</returns>
		protected ResellRateHistoryRow[] MapRecords(IDbCommand command)
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
		/// <returns>An array of <see cref="ResellRateHistoryRow"/> objects.</returns>
		protected ResellRateHistoryRow[] MapRecords(IDataReader reader)
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
		/// <returns>An array of <see cref="ResellRateHistoryRow"/> objects.</returns>
		protected virtual ResellRateHistoryRow[] MapRecords(IDataReader reader, 
										int startIndex, int length, ref int totalRecordCount)
		{
			if(0 > startIndex)
				throw new ArgumentOutOfRangeException("startIndex", startIndex, "StartIndex cannot be less than zero.");
			if(0 > length)
				throw new ArgumentOutOfRangeException("length", length, "Length cannot be less than zero.");

			int resell_acct_idColumnIndex = reader.GetOrdinal("resell_acct_id");
			int wholesale_route_idColumnIndex = reader.GetOrdinal("wholesale_route_id");
			int date_onColumnIndex = reader.GetOrdinal("date_on");
			int date_offColumnIndex = reader.GetOrdinal("date_off");
			int rate_info_idColumnIndex = reader.GetOrdinal("rate_info_id");
			int commision_typeColumnIndex = reader.GetOrdinal("commision_type");
			int markup_dollarColumnIndex = reader.GetOrdinal("markup_dollar");
			int markup_percentColumnIndex = reader.GetOrdinal("markup_percent");
			int markup_per_callColumnIndex = reader.GetOrdinal("markup_per_call");
			int markup_per_minuteColumnIndex = reader.GetOrdinal("markup_per_minute");

			System.Collections.ArrayList recordList = new System.Collections.ArrayList();
			int ri = -startIndex;
			while(reader.Read())
			{
				ri++;
				if(ri > 0 && ri <= length)
				{
					ResellRateHistoryRow record = new ResellRateHistoryRow();
					recordList.Add(record);

					record.Resell_acct_id = Convert.ToInt16(reader.GetValue(resell_acct_idColumnIndex));
					record.Wholesale_route_id = Convert.ToInt32(reader.GetValue(wholesale_route_idColumnIndex));
					record.Date_on = Convert.ToDateTime(reader.GetValue(date_onColumnIndex));
					record.Date_off = Convert.ToDateTime(reader.GetValue(date_offColumnIndex));
					record.Rate_info_id = Convert.ToInt32(reader.GetValue(rate_info_idColumnIndex));
					record.Commision_type = Convert.ToByte(reader.GetValue(commision_typeColumnIndex));
					record.Markup_dollar = Convert.ToDecimal(reader.GetValue(markup_dollarColumnIndex));
					record.Markup_percent = Convert.ToDecimal(reader.GetValue(markup_percentColumnIndex));
					record.Markup_per_call = Convert.ToDecimal(reader.GetValue(markup_per_callColumnIndex));
					record.Markup_per_minute = Convert.ToDecimal(reader.GetValue(markup_per_minuteColumnIndex));

					if(ri == length && 0 != totalRecordCount)
						break;
				}
			}

			totalRecordCount = 0 == totalRecordCount ? ri + startIndex : -1;
			return (ResellRateHistoryRow[])(recordList.ToArray(typeof(ResellRateHistoryRow)));
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
		/// Converts <see cref="System.Data.DataRow"/> to <see cref="ResellRateHistoryRow"/>.
		/// </summary>
		/// <param name="row">The <see cref="System.Data.DataRow"/> object to be mapped.</param>
		/// <returns>A reference to the <see cref="ResellRateHistoryRow"/> object.</returns>
		protected virtual ResellRateHistoryRow MapRow(DataRow row)
		{
			ResellRateHistoryRow mappedObject = new ResellRateHistoryRow();
			DataTable dataTable = row.Table;
			DataColumn dataColumn;
			// Column "Resell_acct_id"
			dataColumn = dataTable.Columns["Resell_acct_id"];
			if(!row.IsNull(dataColumn))
				mappedObject.Resell_acct_id = (short)row[dataColumn];
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
			// Column "Commision_type"
			dataColumn = dataTable.Columns["Commision_type"];
			if(!row.IsNull(dataColumn))
				mappedObject.Commision_type = (byte)row[dataColumn];
			// Column "Markup_dollar"
			dataColumn = dataTable.Columns["Markup_dollar"];
			if(!row.IsNull(dataColumn))
				mappedObject.Markup_dollar = (decimal)row[dataColumn];
			// Column "Markup_percent"
			dataColumn = dataTable.Columns["Markup_percent"];
			if(!row.IsNull(dataColumn))
				mappedObject.Markup_percent = (decimal)row[dataColumn];
			// Column "Markup_per_call"
			dataColumn = dataTable.Columns["Markup_per_call"];
			if(!row.IsNull(dataColumn))
				mappedObject.Markup_per_call = (decimal)row[dataColumn];
			// Column "Markup_per_minute"
			dataColumn = dataTable.Columns["Markup_per_minute"];
			if(!row.IsNull(dataColumn))
				mappedObject.Markup_per_minute = (decimal)row[dataColumn];
			return mappedObject;
		}

		/// <summary>
		/// Creates a <see cref="System.Data.DataTable"/> object for 
		/// the <c>ResellRateHistory</c> table.
		/// </summary>
		/// <returns>A reference to the <see cref="System.Data.DataTable"/> object.</returns>
		protected virtual DataTable CreateDataTable()
		{
			DataTable dataTable = new DataTable();
			dataTable.TableName = "ResellRateHistory";
			DataColumn dataColumn;
			dataColumn = dataTable.Columns.Add("Resell_acct_id", typeof(short));
			dataColumn.Caption = "resell_acct_id";
			dataColumn.AllowDBNull = false;
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
			dataColumn = dataTable.Columns.Add("Commision_type", typeof(byte));
			dataColumn.Caption = "commision_type";
			dataColumn.AllowDBNull = false;
			dataColumn = dataTable.Columns.Add("Markup_dollar", typeof(decimal));
			dataColumn.Caption = "markup_dollar";
			dataColumn.AllowDBNull = false;
			dataColumn = dataTable.Columns.Add("Markup_percent", typeof(decimal));
			dataColumn.Caption = "markup_percent";
			dataColumn.AllowDBNull = false;
			dataColumn = dataTable.Columns.Add("Markup_per_call", typeof(decimal));
			dataColumn.Caption = "markup_per_call";
			dataColumn.AllowDBNull = false;
			dataColumn = dataTable.Columns.Add("Markup_per_minute", typeof(decimal));
			dataColumn.Caption = "markup_per_minute";
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
				case "Resell_acct_id":
					parameter = _db.AddParameter(cmd, paramName, DbType.Int16, value);
					break;

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

				case "Commision_type":
					parameter = _db.AddParameter(cmd, paramName, DbType.Byte, value);
					break;

				case "Markup_dollar":
					parameter = _db.AddParameter(cmd, paramName, DbType.Decimal, value);
					break;

				case "Markup_percent":
					parameter = _db.AddParameter(cmd, paramName, DbType.Decimal, value);
					break;

				case "Markup_per_call":
					parameter = _db.AddParameter(cmd, paramName, DbType.Decimal, value);
					break;

				case "Markup_per_minute":
					parameter = _db.AddParameter(cmd, paramName, DbType.Decimal, value);
					break;

				default:
					throw new ArgumentException("Unknown parameter name (" + paramName + ").");
			}
			return parameter;
		}

	} // End of ResellRateHistoryCollection_Base class
}  // End of namespace

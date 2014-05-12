// <fileinfo name="Base\TimeOfDayPeriodCollection_Base.cs">
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
	/// The base class for <see cref="TimeOfDayPeriodCollection"/>. Provides methods 
	/// for common database table operations. 
	/// </summary>
	/// <remarks>
	/// Do not change this source code. Update the <see cref="TimeOfDayPeriodCollection"/>
	/// class if you need to add or change some functionality.
	/// </remarks>
	public abstract class TimeOfDayPeriodCollection_Base
	{
		// Constants
		public const string Rate_info_idColumnName = "rate_info_id";
		public const string Type_of_day_choiceColumnName = "type_of_day_choice";
		public const string Start_hourColumnName = "start_hour";
		public const string Stop_hourColumnName = "stop_hour";
		public const string Time_of_dayColumnName = "time_of_day";

		// Instance fields
		private Rbr_Db _db;

		/// <summary>
		/// Initializes a new instance of the <see cref="TimeOfDayPeriodCollection_Base"/> 
		/// class with the specified <see cref="Rbr_Db"/>.
		/// </summary>
		/// <param name="db">The <see cref="Rbr_Db"/> object.</param>
		public TimeOfDayPeriodCollection_Base(Rbr_Db db)
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
		/// Gets an array of all records from the <c>TimeOfDayPeriod</c> table.
		/// </summary>
		/// <returns>An array of <see cref="TimeOfDayPeriodRow"/> objects.</returns>
		public virtual TimeOfDayPeriodRow[] GetAll()
		{
			return MapRecords(CreateGetAllCommand());
		}

		/// <summary>
		/// Gets a <see cref="System.Data.DataTable"/> object that 
		/// includes all records from the <c>TimeOfDayPeriod</c> table.
		/// </summary>
		/// <returns>A reference to the <see cref="System.Data.DataTable"/> object.</returns>
		public virtual DataTable GetAllAsDataTable()
		{
			return MapRecordsToDataTable(CreateGetAllCommand());
		}

		/// <summary>
		/// Creates and returns an <see cref="System.Data.IDbCommand"/> object that is used
		/// to retrieve all records from the <c>TimeOfDayPeriod</c> table.
		/// </summary>
		/// <returns>A reference to the <see cref="System.Data.IDbCommand"/> object.</returns>
		protected virtual IDbCommand CreateGetAllCommand()
		{
			return CreateGetCommand(null, null);
		}

		/// <summary>
		/// Gets the first <see cref="TimeOfDayPeriodRow"/> objects that 
		/// match the search condition.
		/// </summary>
		/// <param name="whereSql">The SQL search condition. For example: 
		/// <c>"FirstName='Smith' AND Zip=75038"</c>.</param>
		/// <returns>An instance of <see cref="TimeOfDayPeriodRow"/> or null reference 
		/// (Nothing in Visual Basic) if the object was not found.</returns>
		public TimeOfDayPeriodRow GetRow(string whereSql)
		{
			int totalRecordCount = -1;
			TimeOfDayPeriodRow[] rows = GetAsArray(whereSql, null, 0, 1, ref totalRecordCount);
			return 0 == rows.Length ? null : rows[0];
		}

		/// <summary>
		/// Gets an array of <see cref="TimeOfDayPeriodRow"/> objects that 
		/// match the search condition, in the the specified sort order.
		/// </summary>
		/// <param name="whereSql">The SQL search condition. For example: 
		/// <c>"FirstName='Smith' AND Zip=75038"</c>.</param>
		/// <param name="orderBySql">The column name(s) followed by "ASC" (ascending) or "DESC" (descending).
		/// Columns are sorted in ascending order by default. For example: <c>"LastName ASC, FirstName ASC"</c>.</param>
		/// <returns>An array of <see cref="TimeOfDayPeriodRow"/> objects.</returns>
		public TimeOfDayPeriodRow[] GetAsArray(string whereSql, string orderBySql)
		{
			int totalRecordCount = -1;
			return GetAsArray(whereSql, orderBySql, 0, int.MaxValue, ref totalRecordCount);
		}

		/// <summary>
		/// Gets an array of <see cref="TimeOfDayPeriodRow"/> objects that 
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
		/// <returns>An array of <see cref="TimeOfDayPeriodRow"/> objects.</returns>
		public virtual TimeOfDayPeriodRow[] GetAsArray(string whereSql, string orderBySql,
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
			string sql = "SELECT * FROM [dbo].[TimeOfDayPeriod]";
			if(null != whereSql && 0 < whereSql.Length)
				sql += " WHERE " + whereSql;
			if(null != orderBySql && 0 < orderBySql.Length)
				sql += " ORDER BY " + orderBySql;
			return _db.CreateCommand(sql);
		}

		/// <summary>
		/// Gets <see cref="TimeOfDayPeriodRow"/> by the primary key.
		/// </summary>
		/// <param name="rate_info_id">The <c>rate_info_id</c> column value.</param>
		/// <param name="type_of_day_choice">The <c>type_of_day_choice</c> column value.</param>
		/// <param name="start_hour">The <c>start_hour</c> column value.</param>
		/// <returns>An instance of <see cref="TimeOfDayPeriodRow"/> or null reference 
		/// (Nothing in Visual Basic) if the object was not found.</returns>
		public virtual TimeOfDayPeriodRow GetByPrimaryKey(int rate_info_id, byte type_of_day_choice, short start_hour)
		{
			string whereSql = "[rate_info_id]=" + _db.CreateSqlParameterName("Rate_info_id") + " AND " +
							  "[type_of_day_choice]=" + _db.CreateSqlParameterName("Type_of_day_choice") + " AND " +
							  "[start_hour]=" + _db.CreateSqlParameterName("Start_hour");
			IDbCommand cmd = CreateGetCommand(whereSql, null);
			AddParameter(cmd, "Rate_info_id", rate_info_id);
			AddParameter(cmd, "Type_of_day_choice", type_of_day_choice);
			AddParameter(cmd, "Start_hour", start_hour);
			TimeOfDayPeriodRow[] tempArray = MapRecords(cmd);
			return 0 == tempArray.Length ? null : tempArray[0];
		}

		/// <summary>
		/// Gets an array of <see cref="TimeOfDayPeriodRow"/> objects 
		/// by the <c>R_207</c> foreign key.
		/// </summary>
		/// <param name="rate_info_id">The <c>rate_info_id</c> column value.</param>
		/// <param name="type_of_day_choice">The <c>type_of_day_choice</c> column value.</param>
		/// <returns>An array of <see cref="TimeOfDayPeriodRow"/> objects.</returns>
		public virtual TimeOfDayPeriodRow[] GetByRate_info_id_Type_of_day_choice(int rate_info_id, byte type_of_day_choice)
		{
			return MapRecords(CreateGetByRate_info_id_Type_of_day_choiceCommand(rate_info_id, type_of_day_choice));
		}

		/// <summary>
		/// Gets a <see cref="System.Data.DataTable"/> object 
		/// by the <c>R_207</c> foreign key.
		/// </summary>
		/// <param name="rate_info_id">The <c>rate_info_id</c> column value.</param>
		/// <param name="type_of_day_choice">The <c>type_of_day_choice</c> column value.</param>
		/// <returns>A reference to the <see cref="System.Data.DataTable"/> object.</returns>
		public virtual DataTable GetByRate_info_id_Type_of_day_choiceAsDataTable(int rate_info_id, byte type_of_day_choice)
		{
			return MapRecordsToDataTable(CreateGetByRate_info_id_Type_of_day_choiceCommand(rate_info_id, type_of_day_choice));
		}

		/// <summary>
		/// Creates an <see cref="System.Data.IDbCommand"/> object that can be used to 
		/// return records by the <c>R_207</c> foreign key.
		/// </summary>
		/// <param name="rate_info_id">The <c>rate_info_id</c> column value.</param>
		/// <param name="type_of_day_choice">The <c>type_of_day_choice</c> column value.</param>
		/// <returns>A reference to the <see cref="System.Data.IDbCommand"/> object.</returns>
		protected virtual IDbCommand CreateGetByRate_info_id_Type_of_day_choiceCommand(int rate_info_id, byte type_of_day_choice)
		{
			string whereSql = "";
			whereSql += "[rate_info_id]=" + _db.CreateSqlParameterName("Rate_info_id");
			whereSql += " AND [type_of_day_choice]=" + _db.CreateSqlParameterName("Type_of_day_choice");

			IDbCommand cmd = CreateGetCommand(whereSql, null);
			AddParameter(cmd, "Rate_info_id", rate_info_id);
			AddParameter(cmd, "Type_of_day_choice", type_of_day_choice);
			return cmd;
		}

		/// <summary>
		/// Gets an array of <see cref="TimeOfDayPeriodRow"/> objects 
		/// by the <c>R_210</c> foreign key.
		/// </summary>
		/// <param name="time_of_day">The <c>time_of_day</c> column value.</param>
		/// <returns>An array of <see cref="TimeOfDayPeriodRow"/> objects.</returns>
		public virtual TimeOfDayPeriodRow[] GetByTime_of_day(byte time_of_day)
		{
			return MapRecords(CreateGetByTime_of_dayCommand(time_of_day));
		}

		/// <summary>
		/// Gets a <see cref="System.Data.DataTable"/> object 
		/// by the <c>R_210</c> foreign key.
		/// </summary>
		/// <param name="time_of_day">The <c>time_of_day</c> column value.</param>
		/// <returns>A reference to the <see cref="System.Data.DataTable"/> object.</returns>
		public virtual DataTable GetByTime_of_dayAsDataTable(byte time_of_day)
		{
			return MapRecordsToDataTable(CreateGetByTime_of_dayCommand(time_of_day));
		}

		/// <summary>
		/// Creates an <see cref="System.Data.IDbCommand"/> object that can be used to 
		/// return records by the <c>R_210</c> foreign key.
		/// </summary>
		/// <param name="time_of_day">The <c>time_of_day</c> column value.</param>
		/// <returns>A reference to the <see cref="System.Data.IDbCommand"/> object.</returns>
		protected virtual IDbCommand CreateGetByTime_of_dayCommand(byte time_of_day)
		{
			string whereSql = "";
			whereSql += "[time_of_day]=" + _db.CreateSqlParameterName("Time_of_day");

			IDbCommand cmd = CreateGetCommand(whereSql, null);
			AddParameter(cmd, "Time_of_day", time_of_day);
			return cmd;
		}

		/// <summary>
		/// Adds a new record into the <c>TimeOfDayPeriod</c> table.
		/// </summary>
		/// <param name="value">The <see cref="TimeOfDayPeriodRow"/> object to be inserted.</param>
		public virtual void Insert(TimeOfDayPeriodRow value)
		{
			string sqlStr = "INSERT INTO [dbo].[TimeOfDayPeriod] (" +
				"[rate_info_id], " +
				"[type_of_day_choice], " +
				"[start_hour], " +
				"[stop_hour], " +
				"[time_of_day]" +
				") VALUES (" +
				_db.CreateSqlParameterName("Rate_info_id") + ", " +
				_db.CreateSqlParameterName("Type_of_day_choice") + ", " +
				_db.CreateSqlParameterName("Start_hour") + ", " +
				_db.CreateSqlParameterName("Stop_hour") + ", " +
				_db.CreateSqlParameterName("Time_of_day") + ")";
			IDbCommand cmd = _db.CreateCommand(sqlStr);
			AddParameter(cmd, "Rate_info_id", value.Rate_info_id);
			AddParameter(cmd, "Type_of_day_choice", value.Type_of_day_choice);
			AddParameter(cmd, "Start_hour", value.Start_hour);
			AddParameter(cmd, "Stop_hour", value.Stop_hour);
			AddParameter(cmd, "Time_of_day", value.Time_of_day);
			cmd.ExecuteNonQuery();
		}

		/// <summary>
		/// Updates a record in the <c>TimeOfDayPeriod</c> table.
		/// </summary>
		/// <param name="value">The <see cref="TimeOfDayPeriodRow"/>
		/// object used to update the table record.</param>
		/// <returns>true if the record was updated; otherwise, false.</returns>
		public virtual bool Update(TimeOfDayPeriodRow value)
		{
			string sqlStr = "UPDATE [dbo].[TimeOfDayPeriod] SET " +
				"[stop_hour]=" + _db.CreateSqlParameterName("Stop_hour") + ", " +
				"[time_of_day]=" + _db.CreateSqlParameterName("Time_of_day") +
				" WHERE " +
				"[rate_info_id]=" + _db.CreateSqlParameterName("Rate_info_id") + " AND " +
				"[type_of_day_choice]=" + _db.CreateSqlParameterName("Type_of_day_choice") + " AND " +
				"[start_hour]=" + _db.CreateSqlParameterName("Start_hour");
			IDbCommand cmd = _db.CreateCommand(sqlStr);
			AddParameter(cmd, "Stop_hour", value.Stop_hour);
			AddParameter(cmd, "Time_of_day", value.Time_of_day);
			AddParameter(cmd, "Rate_info_id", value.Rate_info_id);
			AddParameter(cmd, "Type_of_day_choice", value.Type_of_day_choice);
			AddParameter(cmd, "Start_hour", value.Start_hour);
			return 0 != cmd.ExecuteNonQuery();
		}

		/// <summary>
		/// Updates the <c>TimeOfDayPeriod</c> table and calls the <c>AcceptChanges</c> method
		/// on the changed DataRow objects.
		/// </summary>
		/// <param name="table">The <see cref="System.Data.DataTable"/> used to update the data source.</param>
		public void Update(DataTable table)
		{
			Update(table, true);
		}

		/// <summary>
		/// Updates the <c>TimeOfDayPeriod</c> table. Pass <c>false</c> as the <c>acceptChanges</c> 
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
							DeleteByPrimaryKey((int)row["Rate_info_id"], (byte)row["Type_of_day_choice"], (short)row["Start_hour"]);
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
		/// Deletes the specified object from the <c>TimeOfDayPeriod</c> table.
		/// </summary>
		/// <param name="value">The <see cref="TimeOfDayPeriodRow"/> object to delete.</param>
		/// <returns>true if the record was deleted; otherwise, false.</returns>
		public bool Delete(TimeOfDayPeriodRow value)
		{
			return DeleteByPrimaryKey(value.Rate_info_id, value.Type_of_day_choice, value.Start_hour);
		}

		/// <summary>
		/// Deletes a record from the <c>TimeOfDayPeriod</c> table using
		/// the specified primary key.
		/// </summary>
		/// <param name="rate_info_id">The <c>rate_info_id</c> column value.</param>
		/// <param name="type_of_day_choice">The <c>type_of_day_choice</c> column value.</param>
		/// <param name="start_hour">The <c>start_hour</c> column value.</param>
		/// <returns>true if the record was deleted; otherwise, false.</returns>
		public virtual bool DeleteByPrimaryKey(int rate_info_id, byte type_of_day_choice, short start_hour)
		{
			string whereSql = "[rate_info_id]=" + _db.CreateSqlParameterName("Rate_info_id") + " AND " +
							  "[type_of_day_choice]=" + _db.CreateSqlParameterName("Type_of_day_choice") + " AND " +
							  "[start_hour]=" + _db.CreateSqlParameterName("Start_hour");
			IDbCommand cmd = CreateDeleteCommand(whereSql);
			AddParameter(cmd, "Rate_info_id", rate_info_id);
			AddParameter(cmd, "Type_of_day_choice", type_of_day_choice);
			AddParameter(cmd, "Start_hour", start_hour);
			return 0 < cmd.ExecuteNonQuery();
		}

		/// <summary>
		/// Deletes records from the <c>TimeOfDayPeriod</c> table using the 
		/// <c>R_207</c> foreign key.
		/// </summary>
		/// <param name="rate_info_id">The <c>rate_info_id</c> column value.</param>
		/// <param name="type_of_day_choice">The <c>type_of_day_choice</c> column value.</param>
		/// <returns>The number of records deleted from the table.</returns>
		public int DeleteByRate_info_id_Type_of_day_choice(int rate_info_id, byte type_of_day_choice)
		{
			return CreateDeleteByRate_info_id_Type_of_day_choiceCommand(rate_info_id, type_of_day_choice).ExecuteNonQuery();
		}

		/// <summary>
		/// Creates an <see cref="System.Data.IDbCommand"/> object that can be used to
		/// delete records using the <c>R_207</c> foreign key.
		/// </summary>
		/// <param name="rate_info_id">The <c>rate_info_id</c> column value.</param>
		/// <param name="type_of_day_choice">The <c>type_of_day_choice</c> column value.</param>
		/// <returns>A reference to the <see cref="System.Data.IDbCommand"/> object.</returns>
		protected virtual IDbCommand CreateDeleteByRate_info_id_Type_of_day_choiceCommand(int rate_info_id, byte type_of_day_choice)
		{
			string whereSql = "";
			whereSql += "[rate_info_id]=" + _db.CreateSqlParameterName("Rate_info_id");
			whereSql += " AND [type_of_day_choice]=" + _db.CreateSqlParameterName("Type_of_day_choice");

			IDbCommand cmd = CreateDeleteCommand(whereSql);
			AddParameter(cmd, "Rate_info_id", rate_info_id);
			AddParameter(cmd, "Type_of_day_choice", type_of_day_choice);
			return cmd;
		}

		/// <summary>
		/// Deletes records from the <c>TimeOfDayPeriod</c> table using the 
		/// <c>R_210</c> foreign key.
		/// </summary>
		/// <param name="time_of_day">The <c>time_of_day</c> column value.</param>
		/// <returns>The number of records deleted from the table.</returns>
		public int DeleteByTime_of_day(byte time_of_day)
		{
			return CreateDeleteByTime_of_dayCommand(time_of_day).ExecuteNonQuery();
		}

		/// <summary>
		/// Creates an <see cref="System.Data.IDbCommand"/> object that can be used to
		/// delete records using the <c>R_210</c> foreign key.
		/// </summary>
		/// <param name="time_of_day">The <c>time_of_day</c> column value.</param>
		/// <returns>A reference to the <see cref="System.Data.IDbCommand"/> object.</returns>
		protected virtual IDbCommand CreateDeleteByTime_of_dayCommand(byte time_of_day)
		{
			string whereSql = "";
			whereSql += "[time_of_day]=" + _db.CreateSqlParameterName("Time_of_day");

			IDbCommand cmd = CreateDeleteCommand(whereSql);
			AddParameter(cmd, "Time_of_day", time_of_day);
			return cmd;
		}

		/// <summary>
		/// Deletes <c>TimeOfDayPeriod</c> records that match the specified criteria.
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
		/// to delete <c>TimeOfDayPeriod</c> records that match the specified criteria.
		/// </summary>
		/// <param name="whereSql">The SQL search condition. 
		/// For example: <c>"FirstName='Smith' AND Zip=75038"</c>.</param>
		/// <returns>A reference to the <see cref="System.Data.IDbCommand"/> object.</returns>
		protected virtual IDbCommand CreateDeleteCommand(string whereSql)
		{
			string sql = "DELETE FROM [dbo].[TimeOfDayPeriod]";
			if(null != whereSql && 0 < whereSql.Length)
				sql += " WHERE " + whereSql;
			return _db.CreateCommand(sql);
		}

		/// <summary>
		/// Deletes all records from the <c>TimeOfDayPeriod</c> table.
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
		/// <returns>An array of <see cref="TimeOfDayPeriodRow"/> objects.</returns>
		protected TimeOfDayPeriodRow[] MapRecords(IDbCommand command)
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
		/// <returns>An array of <see cref="TimeOfDayPeriodRow"/> objects.</returns>
		protected TimeOfDayPeriodRow[] MapRecords(IDataReader reader)
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
		/// <returns>An array of <see cref="TimeOfDayPeriodRow"/> objects.</returns>
		protected virtual TimeOfDayPeriodRow[] MapRecords(IDataReader reader, 
										int startIndex, int length, ref int totalRecordCount)
		{
			if(0 > startIndex)
				throw new ArgumentOutOfRangeException("startIndex", startIndex, "StartIndex cannot be less than zero.");
			if(0 > length)
				throw new ArgumentOutOfRangeException("length", length, "Length cannot be less than zero.");

			int rate_info_idColumnIndex = reader.GetOrdinal("rate_info_id");
			int type_of_day_choiceColumnIndex = reader.GetOrdinal("type_of_day_choice");
			int start_hourColumnIndex = reader.GetOrdinal("start_hour");
			int stop_hourColumnIndex = reader.GetOrdinal("stop_hour");
			int time_of_dayColumnIndex = reader.GetOrdinal("time_of_day");

			System.Collections.ArrayList recordList = new System.Collections.ArrayList();
			int ri = -startIndex;
			while(reader.Read())
			{
				ri++;
				if(ri > 0 && ri <= length)
				{
					TimeOfDayPeriodRow record = new TimeOfDayPeriodRow();
					recordList.Add(record);

					record.Rate_info_id = Convert.ToInt32(reader.GetValue(rate_info_idColumnIndex));
					record.Type_of_day_choice = Convert.ToByte(reader.GetValue(type_of_day_choiceColumnIndex));
					record.Start_hour = Convert.ToInt16(reader.GetValue(start_hourColumnIndex));
					record.Stop_hour = Convert.ToInt16(reader.GetValue(stop_hourColumnIndex));
					record.Time_of_day = Convert.ToByte(reader.GetValue(time_of_dayColumnIndex));

					if(ri == length && 0 != totalRecordCount)
						break;
				}
			}

			totalRecordCount = 0 == totalRecordCount ? ri + startIndex : -1;
			return (TimeOfDayPeriodRow[])(recordList.ToArray(typeof(TimeOfDayPeriodRow)));
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
		/// Converts <see cref="System.Data.DataRow"/> to <see cref="TimeOfDayPeriodRow"/>.
		/// </summary>
		/// <param name="row">The <see cref="System.Data.DataRow"/> object to be mapped.</param>
		/// <returns>A reference to the <see cref="TimeOfDayPeriodRow"/> object.</returns>
		protected virtual TimeOfDayPeriodRow MapRow(DataRow row)
		{
			TimeOfDayPeriodRow mappedObject = new TimeOfDayPeriodRow();
			DataTable dataTable = row.Table;
			DataColumn dataColumn;
			// Column "Rate_info_id"
			dataColumn = dataTable.Columns["Rate_info_id"];
			if(!row.IsNull(dataColumn))
				mappedObject.Rate_info_id = (int)row[dataColumn];
			// Column "Type_of_day_choice"
			dataColumn = dataTable.Columns["Type_of_day_choice"];
			if(!row.IsNull(dataColumn))
				mappedObject.Type_of_day_choice = (byte)row[dataColumn];
			// Column "Start_hour"
			dataColumn = dataTable.Columns["Start_hour"];
			if(!row.IsNull(dataColumn))
				mappedObject.Start_hour = (short)row[dataColumn];
			// Column "Stop_hour"
			dataColumn = dataTable.Columns["Stop_hour"];
			if(!row.IsNull(dataColumn))
				mappedObject.Stop_hour = (short)row[dataColumn];
			// Column "Time_of_day"
			dataColumn = dataTable.Columns["Time_of_day"];
			if(!row.IsNull(dataColumn))
				mappedObject.Time_of_day = (byte)row[dataColumn];
			return mappedObject;
		}

		/// <summary>
		/// Creates a <see cref="System.Data.DataTable"/> object for 
		/// the <c>TimeOfDayPeriod</c> table.
		/// </summary>
		/// <returns>A reference to the <see cref="System.Data.DataTable"/> object.</returns>
		protected virtual DataTable CreateDataTable()
		{
			DataTable dataTable = new DataTable();
			dataTable.TableName = "TimeOfDayPeriod";
			DataColumn dataColumn;
			dataColumn = dataTable.Columns.Add("Rate_info_id", typeof(int));
			dataColumn.Caption = "rate_info_id";
			dataColumn.AllowDBNull = false;
			dataColumn = dataTable.Columns.Add("Type_of_day_choice", typeof(byte));
			dataColumn.Caption = "type_of_day_choice";
			dataColumn.AllowDBNull = false;
			dataColumn = dataTable.Columns.Add("Start_hour", typeof(short));
			dataColumn.Caption = "start_hour";
			dataColumn.AllowDBNull = false;
			dataColumn = dataTable.Columns.Add("Stop_hour", typeof(short));
			dataColumn.Caption = "stop_hour";
			dataColumn.AllowDBNull = false;
			dataColumn = dataTable.Columns.Add("Time_of_day", typeof(byte));
			dataColumn.Caption = "time_of_day";
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
				case "Rate_info_id":
					parameter = _db.AddParameter(cmd, paramName, DbType.Int32, value);
					break;

				case "Type_of_day_choice":
					parameter = _db.AddParameter(cmd, paramName, DbType.Byte, value);
					break;

				case "Start_hour":
					parameter = _db.AddParameter(cmd, paramName, DbType.Int16, value);
					break;

				case "Stop_hour":
					parameter = _db.AddParameter(cmd, paramName, DbType.Int16, value);
					break;

				case "Time_of_day":
					parameter = _db.AddParameter(cmd, paramName, DbType.Byte, value);
					break;

				default:
					throw new ArgumentException("Unknown parameter name (" + paramName + ").");
			}
			return parameter;
		}

	} // End of TimeOfDayPeriodCollection_Base class
}  // End of namespace

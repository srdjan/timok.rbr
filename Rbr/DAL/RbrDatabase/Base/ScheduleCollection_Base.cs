// <fileinfo name="Base\ScheduleCollection_Base.cs">
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
	/// The base class for <see cref="ScheduleCollection"/>. Provides methods 
	/// for common database table operations. 
	/// </summary>
	/// <remarks>
	/// Do not change this source code. Update the <see cref="ScheduleCollection"/>
	/// class if you need to add or change some functionality.
	/// </remarks>
	public abstract class ScheduleCollection_Base
	{
		// Constants
		public const string Schedule_idColumnName = "schedule_id";
		public const string TypeColumnName = "type";
		public const string Day_of_weekColumnName = "day_of_week";
		public const string Day_of_the_month_1ColumnName = "day_of_the_month_1";
		public const string Day_of_the_month_2ColumnName = "day_of_the_month_2";

		// Instance fields
		private Rbr_Db _db;

		/// <summary>
		/// Initializes a new instance of the <see cref="ScheduleCollection_Base"/> 
		/// class with the specified <see cref="Rbr_Db"/>.
		/// </summary>
		/// <param name="db">The <see cref="Rbr_Db"/> object.</param>
		public ScheduleCollection_Base(Rbr_Db db)
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
		/// Gets an array of all records from the <c>Schedule</c> table.
		/// </summary>
		/// <returns>An array of <see cref="ScheduleRow"/> objects.</returns>
		public virtual ScheduleRow[] GetAll()
		{
			return MapRecords(CreateGetAllCommand());
		}

		/// <summary>
		/// Gets a <see cref="System.Data.DataTable"/> object that 
		/// includes all records from the <c>Schedule</c> table.
		/// </summary>
		/// <returns>A reference to the <see cref="System.Data.DataTable"/> object.</returns>
		public virtual DataTable GetAllAsDataTable()
		{
			return MapRecordsToDataTable(CreateGetAllCommand());
		}

		/// <summary>
		/// Creates and returns an <see cref="System.Data.IDbCommand"/> object that is used
		/// to retrieve all records from the <c>Schedule</c> table.
		/// </summary>
		/// <returns>A reference to the <see cref="System.Data.IDbCommand"/> object.</returns>
		protected virtual IDbCommand CreateGetAllCommand()
		{
			return CreateGetCommand(null, null);
		}

		/// <summary>
		/// Gets the first <see cref="ScheduleRow"/> objects that 
		/// match the search condition.
		/// </summary>
		/// <param name="whereSql">The SQL search condition. For example: 
		/// <c>"FirstName='Smith' AND Zip=75038"</c>.</param>
		/// <returns>An instance of <see cref="ScheduleRow"/> or null reference 
		/// (Nothing in Visual Basic) if the object was not found.</returns>
		public ScheduleRow GetRow(string whereSql)
		{
			int totalRecordCount = -1;
			ScheduleRow[] rows = GetAsArray(whereSql, null, 0, 1, ref totalRecordCount);
			return 0 == rows.Length ? null : rows[0];
		}

		/// <summary>
		/// Gets an array of <see cref="ScheduleRow"/> objects that 
		/// match the search condition, in the the specified sort order.
		/// </summary>
		/// <param name="whereSql">The SQL search condition. For example: 
		/// <c>"FirstName='Smith' AND Zip=75038"</c>.</param>
		/// <param name="orderBySql">The column name(s) followed by "ASC" (ascending) or "DESC" (descending).
		/// Columns are sorted in ascending order by default. For example: <c>"LastName ASC, FirstName ASC"</c>.</param>
		/// <returns>An array of <see cref="ScheduleRow"/> objects.</returns>
		public ScheduleRow[] GetAsArray(string whereSql, string orderBySql)
		{
			int totalRecordCount = -1;
			return GetAsArray(whereSql, orderBySql, 0, int.MaxValue, ref totalRecordCount);
		}

		/// <summary>
		/// Gets an array of <see cref="ScheduleRow"/> objects that 
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
		/// <returns>An array of <see cref="ScheduleRow"/> objects.</returns>
		public virtual ScheduleRow[] GetAsArray(string whereSql, string orderBySql,
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
			string sql = "SELECT * FROM [dbo].[Schedule]";
			if(null != whereSql && 0 < whereSql.Length)
				sql += " WHERE " + whereSql;
			if(null != orderBySql && 0 < orderBySql.Length)
				sql += " ORDER BY " + orderBySql;
			return _db.CreateCommand(sql);
		}

		/// <summary>
		/// Gets <see cref="ScheduleRow"/> by the primary key.
		/// </summary>
		/// <param name="schedule_id">The <c>schedule_id</c> column value.</param>
		/// <returns>An instance of <see cref="ScheduleRow"/> or null reference 
		/// (Nothing in Visual Basic) if the object was not found.</returns>
		public virtual ScheduleRow GetByPrimaryKey(int schedule_id)
		{
			string whereSql = "[schedule_id]=" + _db.CreateSqlParameterName("Schedule_id");
			IDbCommand cmd = CreateGetCommand(whereSql, null);
			AddParameter(cmd, "Schedule_id", schedule_id);
			ScheduleRow[] tempArray = MapRecords(cmd);
			return 0 == tempArray.Length ? null : tempArray[0];
		}

		/// <summary>
		/// Adds a new record into the <c>Schedule</c> table.
		/// </summary>
		/// <param name="value">The <see cref="ScheduleRow"/> object to be inserted.</param>
		public virtual void Insert(ScheduleRow value)
		{
			string sqlStr = "INSERT INTO [dbo].[Schedule] (" +
				"[schedule_id], " +
				"[type], " +
				"[day_of_week], " +
				"[day_of_the_month_1], " +
				"[day_of_the_month_2]" +
				") VALUES (" +
				_db.CreateSqlParameterName("Schedule_id") + ", " +
				_db.CreateSqlParameterName("Type") + ", " +
				_db.CreateSqlParameterName("Day_of_week") + ", " +
				_db.CreateSqlParameterName("Day_of_the_month_1") + ", " +
				_db.CreateSqlParameterName("Day_of_the_month_2") + ")";
			IDbCommand cmd = _db.CreateCommand(sqlStr);
			AddParameter(cmd, "Schedule_id", value.Schedule_id);
			AddParameter(cmd, "Type", value.Type);
			AddParameter(cmd, "Day_of_week", value.Day_of_week);
			AddParameter(cmd, "Day_of_the_month_1", value.Day_of_the_month_1);
			AddParameter(cmd, "Day_of_the_month_2", value.Day_of_the_month_2);
			cmd.ExecuteNonQuery();
		}

		/// <summary>
		/// Updates a record in the <c>Schedule</c> table.
		/// </summary>
		/// <param name="value">The <see cref="ScheduleRow"/>
		/// object used to update the table record.</param>
		/// <returns>true if the record was updated; otherwise, false.</returns>
		public virtual bool Update(ScheduleRow value)
		{
			string sqlStr = "UPDATE [dbo].[Schedule] SET " +
				"[type]=" + _db.CreateSqlParameterName("Type") + ", " +
				"[day_of_week]=" + _db.CreateSqlParameterName("Day_of_week") + ", " +
				"[day_of_the_month_1]=" + _db.CreateSqlParameterName("Day_of_the_month_1") + ", " +
				"[day_of_the_month_2]=" + _db.CreateSqlParameterName("Day_of_the_month_2") +
				" WHERE " +
				"[schedule_id]=" + _db.CreateSqlParameterName("Schedule_id");
			IDbCommand cmd = _db.CreateCommand(sqlStr);
			AddParameter(cmd, "Type", value.Type);
			AddParameter(cmd, "Day_of_week", value.Day_of_week);
			AddParameter(cmd, "Day_of_the_month_1", value.Day_of_the_month_1);
			AddParameter(cmd, "Day_of_the_month_2", value.Day_of_the_month_2);
			AddParameter(cmd, "Schedule_id", value.Schedule_id);
			return 0 != cmd.ExecuteNonQuery();
		}

		/// <summary>
		/// Updates the <c>Schedule</c> table and calls the <c>AcceptChanges</c> method
		/// on the changed DataRow objects.
		/// </summary>
		/// <param name="table">The <see cref="System.Data.DataTable"/> used to update the data source.</param>
		public void Update(DataTable table)
		{
			Update(table, true);
		}

		/// <summary>
		/// Updates the <c>Schedule</c> table. Pass <c>false</c> as the <c>acceptChanges</c> 
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
							DeleteByPrimaryKey((int)row["Schedule_id"]);
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
		/// Deletes the specified object from the <c>Schedule</c> table.
		/// </summary>
		/// <param name="value">The <see cref="ScheduleRow"/> object to delete.</param>
		/// <returns>true if the record was deleted; otherwise, false.</returns>
		public bool Delete(ScheduleRow value)
		{
			return DeleteByPrimaryKey(value.Schedule_id);
		}

		/// <summary>
		/// Deletes a record from the <c>Schedule</c> table using
		/// the specified primary key.
		/// </summary>
		/// <param name="schedule_id">The <c>schedule_id</c> column value.</param>
		/// <returns>true if the record was deleted; otherwise, false.</returns>
		public virtual bool DeleteByPrimaryKey(int schedule_id)
		{
			string whereSql = "[schedule_id]=" + _db.CreateSqlParameterName("Schedule_id");
			IDbCommand cmd = CreateDeleteCommand(whereSql);
			AddParameter(cmd, "Schedule_id", schedule_id);
			return 0 < cmd.ExecuteNonQuery();
		}

		/// <summary>
		/// Deletes <c>Schedule</c> records that match the specified criteria.
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
		/// to delete <c>Schedule</c> records that match the specified criteria.
		/// </summary>
		/// <param name="whereSql">The SQL search condition. 
		/// For example: <c>"FirstName='Smith' AND Zip=75038"</c>.</param>
		/// <returns>A reference to the <see cref="System.Data.IDbCommand"/> object.</returns>
		protected virtual IDbCommand CreateDeleteCommand(string whereSql)
		{
			string sql = "DELETE FROM [dbo].[Schedule]";
			if(null != whereSql && 0 < whereSql.Length)
				sql += " WHERE " + whereSql;
			return _db.CreateCommand(sql);
		}

		/// <summary>
		/// Deletes all records from the <c>Schedule</c> table.
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
		/// <returns>An array of <see cref="ScheduleRow"/> objects.</returns>
		protected ScheduleRow[] MapRecords(IDbCommand command)
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
		/// <returns>An array of <see cref="ScheduleRow"/> objects.</returns>
		protected ScheduleRow[] MapRecords(IDataReader reader)
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
		/// <returns>An array of <see cref="ScheduleRow"/> objects.</returns>
		protected virtual ScheduleRow[] MapRecords(IDataReader reader, 
										int startIndex, int length, ref int totalRecordCount)
		{
			if(0 > startIndex)
				throw new ArgumentOutOfRangeException("startIndex", startIndex, "StartIndex cannot be less than zero.");
			if(0 > length)
				throw new ArgumentOutOfRangeException("length", length, "Length cannot be less than zero.");

			int schedule_idColumnIndex = reader.GetOrdinal("schedule_id");
			int typeColumnIndex = reader.GetOrdinal("type");
			int day_of_weekColumnIndex = reader.GetOrdinal("day_of_week");
			int day_of_the_month_1ColumnIndex = reader.GetOrdinal("day_of_the_month_1");
			int day_of_the_month_2ColumnIndex = reader.GetOrdinal("day_of_the_month_2");

			System.Collections.ArrayList recordList = new System.Collections.ArrayList();
			int ri = -startIndex;
			while(reader.Read())
			{
				ri++;
				if(ri > 0 && ri <= length)
				{
					ScheduleRow record = new ScheduleRow();
					recordList.Add(record);

					record.Schedule_id = Convert.ToInt32(reader.GetValue(schedule_idColumnIndex));
					record.Type = Convert.ToByte(reader.GetValue(typeColumnIndex));
					record.Day_of_week = Convert.ToInt16(reader.GetValue(day_of_weekColumnIndex));
					record.Day_of_the_month_1 = Convert.ToInt32(reader.GetValue(day_of_the_month_1ColumnIndex));
					record.Day_of_the_month_2 = Convert.ToInt32(reader.GetValue(day_of_the_month_2ColumnIndex));

					if(ri == length && 0 != totalRecordCount)
						break;
				}
			}

			totalRecordCount = 0 == totalRecordCount ? ri + startIndex : -1;
			return (ScheduleRow[])(recordList.ToArray(typeof(ScheduleRow)));
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
		/// Converts <see cref="System.Data.DataRow"/> to <see cref="ScheduleRow"/>.
		/// </summary>
		/// <param name="row">The <see cref="System.Data.DataRow"/> object to be mapped.</param>
		/// <returns>A reference to the <see cref="ScheduleRow"/> object.</returns>
		protected virtual ScheduleRow MapRow(DataRow row)
		{
			ScheduleRow mappedObject = new ScheduleRow();
			DataTable dataTable = row.Table;
			DataColumn dataColumn;
			// Column "Schedule_id"
			dataColumn = dataTable.Columns["Schedule_id"];
			if(!row.IsNull(dataColumn))
				mappedObject.Schedule_id = (int)row[dataColumn];
			// Column "Type"
			dataColumn = dataTable.Columns["Type"];
			if(!row.IsNull(dataColumn))
				mappedObject.Type = (byte)row[dataColumn];
			// Column "Day_of_week"
			dataColumn = dataTable.Columns["Day_of_week"];
			if(!row.IsNull(dataColumn))
				mappedObject.Day_of_week = (short)row[dataColumn];
			// Column "Day_of_the_month_1"
			dataColumn = dataTable.Columns["Day_of_the_month_1"];
			if(!row.IsNull(dataColumn))
				mappedObject.Day_of_the_month_1 = (int)row[dataColumn];
			// Column "Day_of_the_month_2"
			dataColumn = dataTable.Columns["Day_of_the_month_2"];
			if(!row.IsNull(dataColumn))
				mappedObject.Day_of_the_month_2 = (int)row[dataColumn];
			return mappedObject;
		}

		/// <summary>
		/// Creates a <see cref="System.Data.DataTable"/> object for 
		/// the <c>Schedule</c> table.
		/// </summary>
		/// <returns>A reference to the <see cref="System.Data.DataTable"/> object.</returns>
		protected virtual DataTable CreateDataTable()
		{
			DataTable dataTable = new DataTable();
			dataTable.TableName = "Schedule";
			DataColumn dataColumn;
			dataColumn = dataTable.Columns.Add("Schedule_id", typeof(int));
			dataColumn.Caption = "schedule_id";
			dataColumn.AllowDBNull = false;
			dataColumn = dataTable.Columns.Add("Type", typeof(byte));
			dataColumn.Caption = "type";
			dataColumn.AllowDBNull = false;
			dataColumn = dataTable.Columns.Add("Day_of_week", typeof(short));
			dataColumn.Caption = "day_of_week";
			dataColumn.AllowDBNull = false;
			dataColumn = dataTable.Columns.Add("Day_of_the_month_1", typeof(int));
			dataColumn.Caption = "day_of_the_month_1";
			dataColumn.AllowDBNull = false;
			dataColumn = dataTable.Columns.Add("Day_of_the_month_2", typeof(int));
			dataColumn.Caption = "day_of_the_month_2";
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
				case "Schedule_id":
					parameter = _db.AddParameter(cmd, paramName, DbType.Int32, value);
					break;

				case "Type":
					parameter = _db.AddParameter(cmd, paramName, DbType.Byte, value);
					break;

				case "Day_of_week":
					parameter = _db.AddParameter(cmd, paramName, DbType.Int16, value);
					break;

				case "Day_of_the_month_1":
					parameter = _db.AddParameter(cmd, paramName, DbType.Int32, value);
					break;

				case "Day_of_the_month_2":
					parameter = _db.AddParameter(cmd, paramName, DbType.Int32, value);
					break;

				default:
					throw new ArgumentException("Unknown parameter name (" + paramName + ").");
			}
			return parameter;
		}

	} // End of ScheduleCollection_Base class
}  // End of namespace

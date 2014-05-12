// <fileinfo name="Base\TimeOfDayCollection_Base.cs">
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
	/// The base class for <see cref="TimeOfDayCollection"/>. Provides methods 
	/// for common database table operations. 
	/// </summary>
	/// <remarks>
	/// Do not change this source code. Update the <see cref="TimeOfDayCollection"/>
	/// class if you need to add or change some functionality.
	/// </remarks>
	public abstract class TimeOfDayCollection_Base
	{
		// Constants
		public const string Time_of_dayColumnName = "time_of_day";
		public const string NameColumnName = "name";
		public const string Time_of_day_policyColumnName = "time_of_day_policy";

		// Instance fields
		private Rbr_Db _db;

		/// <summary>
		/// Initializes a new instance of the <see cref="TimeOfDayCollection_Base"/> 
		/// class with the specified <see cref="Rbr_Db"/>.
		/// </summary>
		/// <param name="db">The <see cref="Rbr_Db"/> object.</param>
		public TimeOfDayCollection_Base(Rbr_Db db)
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
		/// Gets an array of all records from the <c>TimeOfDay</c> table.
		/// </summary>
		/// <returns>An array of <see cref="TimeOfDayRow"/> objects.</returns>
		public virtual TimeOfDayRow[] GetAll()
		{
			return MapRecords(CreateGetAllCommand());
		}

		/// <summary>
		/// Gets a <see cref="System.Data.DataTable"/> object that 
		/// includes all records from the <c>TimeOfDay</c> table.
		/// </summary>
		/// <returns>A reference to the <see cref="System.Data.DataTable"/> object.</returns>
		public virtual DataTable GetAllAsDataTable()
		{
			return MapRecordsToDataTable(CreateGetAllCommand());
		}

		/// <summary>
		/// Creates and returns an <see cref="System.Data.IDbCommand"/> object that is used
		/// to retrieve all records from the <c>TimeOfDay</c> table.
		/// </summary>
		/// <returns>A reference to the <see cref="System.Data.IDbCommand"/> object.</returns>
		protected virtual IDbCommand CreateGetAllCommand()
		{
			return CreateGetCommand(null, null);
		}

		/// <summary>
		/// Gets the first <see cref="TimeOfDayRow"/> objects that 
		/// match the search condition.
		/// </summary>
		/// <param name="whereSql">The SQL search condition. For example: 
		/// <c>"FirstName='Smith' AND Zip=75038"</c>.</param>
		/// <returns>An instance of <see cref="TimeOfDayRow"/> or null reference 
		/// (Nothing in Visual Basic) if the object was not found.</returns>
		public TimeOfDayRow GetRow(string whereSql)
		{
			int totalRecordCount = -1;
			TimeOfDayRow[] rows = GetAsArray(whereSql, null, 0, 1, ref totalRecordCount);
			return 0 == rows.Length ? null : rows[0];
		}

		/// <summary>
		/// Gets an array of <see cref="TimeOfDayRow"/> objects that 
		/// match the search condition, in the the specified sort order.
		/// </summary>
		/// <param name="whereSql">The SQL search condition. For example: 
		/// <c>"FirstName='Smith' AND Zip=75038"</c>.</param>
		/// <param name="orderBySql">The column name(s) followed by "ASC" (ascending) or "DESC" (descending).
		/// Columns are sorted in ascending order by default. For example: <c>"LastName ASC, FirstName ASC"</c>.</param>
		/// <returns>An array of <see cref="TimeOfDayRow"/> objects.</returns>
		public TimeOfDayRow[] GetAsArray(string whereSql, string orderBySql)
		{
			int totalRecordCount = -1;
			return GetAsArray(whereSql, orderBySql, 0, int.MaxValue, ref totalRecordCount);
		}

		/// <summary>
		/// Gets an array of <see cref="TimeOfDayRow"/> objects that 
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
		/// <returns>An array of <see cref="TimeOfDayRow"/> objects.</returns>
		public virtual TimeOfDayRow[] GetAsArray(string whereSql, string orderBySql,
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
			string sql = "SELECT * FROM [dbo].[TimeOfDay]";
			if(null != whereSql && 0 < whereSql.Length)
				sql += " WHERE " + whereSql;
			if(null != orderBySql && 0 < orderBySql.Length)
				sql += " ORDER BY " + orderBySql;
			return _db.CreateCommand(sql);
		}

		/// <summary>
		/// Gets <see cref="TimeOfDayRow"/> by the primary key.
		/// </summary>
		/// <param name="time_of_day">The <c>time_of_day</c> column value.</param>
		/// <returns>An instance of <see cref="TimeOfDayRow"/> or null reference 
		/// (Nothing in Visual Basic) if the object was not found.</returns>
		public virtual TimeOfDayRow GetByPrimaryKey(byte time_of_day)
		{
			string whereSql = "[time_of_day]=" + _db.CreateSqlParameterName("Time_of_day");
			IDbCommand cmd = CreateGetCommand(whereSql, null);
			AddParameter(cmd, "Time_of_day", time_of_day);
			TimeOfDayRow[] tempArray = MapRecords(cmd);
			return 0 == tempArray.Length ? null : tempArray[0];
		}

		/// <summary>
		/// Gets an array of <see cref="TimeOfDayRow"/> objects 
		/// by the <c>R_173</c> foreign key.
		/// </summary>
		/// <param name="time_of_day_policy">The <c>time_of_day_policy</c> column value.</param>
		/// <returns>An array of <see cref="TimeOfDayRow"/> objects.</returns>
		public virtual TimeOfDayRow[] GetByTime_of_day_policy(byte time_of_day_policy)
		{
			return MapRecords(CreateGetByTime_of_day_policyCommand(time_of_day_policy));
		}

		/// <summary>
		/// Gets a <see cref="System.Data.DataTable"/> object 
		/// by the <c>R_173</c> foreign key.
		/// </summary>
		/// <param name="time_of_day_policy">The <c>time_of_day_policy</c> column value.</param>
		/// <returns>A reference to the <see cref="System.Data.DataTable"/> object.</returns>
		public virtual DataTable GetByTime_of_day_policyAsDataTable(byte time_of_day_policy)
		{
			return MapRecordsToDataTable(CreateGetByTime_of_day_policyCommand(time_of_day_policy));
		}

		/// <summary>
		/// Creates an <see cref="System.Data.IDbCommand"/> object that can be used to 
		/// return records by the <c>R_173</c> foreign key.
		/// </summary>
		/// <param name="time_of_day_policy">The <c>time_of_day_policy</c> column value.</param>
		/// <returns>A reference to the <see cref="System.Data.IDbCommand"/> object.</returns>
		protected virtual IDbCommand CreateGetByTime_of_day_policyCommand(byte time_of_day_policy)
		{
			string whereSql = "";
			whereSql += "[time_of_day_policy]=" + _db.CreateSqlParameterName("Time_of_day_policy");

			IDbCommand cmd = CreateGetCommand(whereSql, null);
			AddParameter(cmd, "Time_of_day_policy", time_of_day_policy);
			return cmd;
		}

		/// <summary>
		/// Adds a new record into the <c>TimeOfDay</c> table.
		/// </summary>
		/// <param name="value">The <see cref="TimeOfDayRow"/> object to be inserted.</param>
		public virtual void Insert(TimeOfDayRow value)
		{
			string sqlStr = "INSERT INTO [dbo].[TimeOfDay] (" +
				"[time_of_day], " +
				"[name], " +
				"[time_of_day_policy]" +
				") VALUES (" +
				_db.CreateSqlParameterName("Time_of_day") + ", " +
				_db.CreateSqlParameterName("Name") + ", " +
				_db.CreateSqlParameterName("Time_of_day_policy") + ")";
			IDbCommand cmd = _db.CreateCommand(sqlStr);
			AddParameter(cmd, "Time_of_day", value.Time_of_day);
			AddParameter(cmd, "Name", value.Name);
			AddParameter(cmd, "Time_of_day_policy", value.Time_of_day_policy);
			cmd.ExecuteNonQuery();
		}

		/// <summary>
		/// Updates a record in the <c>TimeOfDay</c> table.
		/// </summary>
		/// <param name="value">The <see cref="TimeOfDayRow"/>
		/// object used to update the table record.</param>
		/// <returns>true if the record was updated; otherwise, false.</returns>
		public virtual bool Update(TimeOfDayRow value)
		{
			string sqlStr = "UPDATE [dbo].[TimeOfDay] SET " +
				"[name]=" + _db.CreateSqlParameterName("Name") + ", " +
				"[time_of_day_policy]=" + _db.CreateSqlParameterName("Time_of_day_policy") +
				" WHERE " +
				"[time_of_day]=" + _db.CreateSqlParameterName("Time_of_day");
			IDbCommand cmd = _db.CreateCommand(sqlStr);
			AddParameter(cmd, "Name", value.Name);
			AddParameter(cmd, "Time_of_day_policy", value.Time_of_day_policy);
			AddParameter(cmd, "Time_of_day", value.Time_of_day);
			return 0 != cmd.ExecuteNonQuery();
		}

		/// <summary>
		/// Updates the <c>TimeOfDay</c> table and calls the <c>AcceptChanges</c> method
		/// on the changed DataRow objects.
		/// </summary>
		/// <param name="table">The <see cref="System.Data.DataTable"/> used to update the data source.</param>
		public void Update(DataTable table)
		{
			Update(table, true);
		}

		/// <summary>
		/// Updates the <c>TimeOfDay</c> table. Pass <c>false</c> as the <c>acceptChanges</c> 
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
							DeleteByPrimaryKey((byte)row["Time_of_day"]);
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
		/// Deletes the specified object from the <c>TimeOfDay</c> table.
		/// </summary>
		/// <param name="value">The <see cref="TimeOfDayRow"/> object to delete.</param>
		/// <returns>true if the record was deleted; otherwise, false.</returns>
		public bool Delete(TimeOfDayRow value)
		{
			return DeleteByPrimaryKey(value.Time_of_day);
		}

		/// <summary>
		/// Deletes a record from the <c>TimeOfDay</c> table using
		/// the specified primary key.
		/// </summary>
		/// <param name="time_of_day">The <c>time_of_day</c> column value.</param>
		/// <returns>true if the record was deleted; otherwise, false.</returns>
		public virtual bool DeleteByPrimaryKey(byte time_of_day)
		{
			string whereSql = "[time_of_day]=" + _db.CreateSqlParameterName("Time_of_day");
			IDbCommand cmd = CreateDeleteCommand(whereSql);
			AddParameter(cmd, "Time_of_day", time_of_day);
			return 0 < cmd.ExecuteNonQuery();
		}

		/// <summary>
		/// Deletes records from the <c>TimeOfDay</c> table using the 
		/// <c>R_173</c> foreign key.
		/// </summary>
		/// <param name="time_of_day_policy">The <c>time_of_day_policy</c> column value.</param>
		/// <returns>The number of records deleted from the table.</returns>
		public int DeleteByTime_of_day_policy(byte time_of_day_policy)
		{
			return CreateDeleteByTime_of_day_policyCommand(time_of_day_policy).ExecuteNonQuery();
		}

		/// <summary>
		/// Creates an <see cref="System.Data.IDbCommand"/> object that can be used to
		/// delete records using the <c>R_173</c> foreign key.
		/// </summary>
		/// <param name="time_of_day_policy">The <c>time_of_day_policy</c> column value.</param>
		/// <returns>A reference to the <see cref="System.Data.IDbCommand"/> object.</returns>
		protected virtual IDbCommand CreateDeleteByTime_of_day_policyCommand(byte time_of_day_policy)
		{
			string whereSql = "";
			whereSql += "[time_of_day_policy]=" + _db.CreateSqlParameterName("Time_of_day_policy");

			IDbCommand cmd = CreateDeleteCommand(whereSql);
			AddParameter(cmd, "Time_of_day_policy", time_of_day_policy);
			return cmd;
		}

		/// <summary>
		/// Deletes <c>TimeOfDay</c> records that match the specified criteria.
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
		/// to delete <c>TimeOfDay</c> records that match the specified criteria.
		/// </summary>
		/// <param name="whereSql">The SQL search condition. 
		/// For example: <c>"FirstName='Smith' AND Zip=75038"</c>.</param>
		/// <returns>A reference to the <see cref="System.Data.IDbCommand"/> object.</returns>
		protected virtual IDbCommand CreateDeleteCommand(string whereSql)
		{
			string sql = "DELETE FROM [dbo].[TimeOfDay]";
			if(null != whereSql && 0 < whereSql.Length)
				sql += " WHERE " + whereSql;
			return _db.CreateCommand(sql);
		}

		/// <summary>
		/// Deletes all records from the <c>TimeOfDay</c> table.
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
		/// <returns>An array of <see cref="TimeOfDayRow"/> objects.</returns>
		protected TimeOfDayRow[] MapRecords(IDbCommand command)
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
		/// <returns>An array of <see cref="TimeOfDayRow"/> objects.</returns>
		protected TimeOfDayRow[] MapRecords(IDataReader reader)
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
		/// <returns>An array of <see cref="TimeOfDayRow"/> objects.</returns>
		protected virtual TimeOfDayRow[] MapRecords(IDataReader reader, 
										int startIndex, int length, ref int totalRecordCount)
		{
			if(0 > startIndex)
				throw new ArgumentOutOfRangeException("startIndex", startIndex, "StartIndex cannot be less than zero.");
			if(0 > length)
				throw new ArgumentOutOfRangeException("length", length, "Length cannot be less than zero.");

			int time_of_dayColumnIndex = reader.GetOrdinal("time_of_day");
			int nameColumnIndex = reader.GetOrdinal("name");
			int time_of_day_policyColumnIndex = reader.GetOrdinal("time_of_day_policy");

			System.Collections.ArrayList recordList = new System.Collections.ArrayList();
			int ri = -startIndex;
			while(reader.Read())
			{
				ri++;
				if(ri > 0 && ri <= length)
				{
					TimeOfDayRow record = new TimeOfDayRow();
					recordList.Add(record);

					record.Time_of_day = Convert.ToByte(reader.GetValue(time_of_dayColumnIndex));
					record.Name = Convert.ToString(reader.GetValue(nameColumnIndex));
					record.Time_of_day_policy = Convert.ToByte(reader.GetValue(time_of_day_policyColumnIndex));

					if(ri == length && 0 != totalRecordCount)
						break;
				}
			}

			totalRecordCount = 0 == totalRecordCount ? ri + startIndex : -1;
			return (TimeOfDayRow[])(recordList.ToArray(typeof(TimeOfDayRow)));
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
		/// Converts <see cref="System.Data.DataRow"/> to <see cref="TimeOfDayRow"/>.
		/// </summary>
		/// <param name="row">The <see cref="System.Data.DataRow"/> object to be mapped.</param>
		/// <returns>A reference to the <see cref="TimeOfDayRow"/> object.</returns>
		protected virtual TimeOfDayRow MapRow(DataRow row)
		{
			TimeOfDayRow mappedObject = new TimeOfDayRow();
			DataTable dataTable = row.Table;
			DataColumn dataColumn;
			// Column "Time_of_day"
			dataColumn = dataTable.Columns["Time_of_day"];
			if(!row.IsNull(dataColumn))
				mappedObject.Time_of_day = (byte)row[dataColumn];
			// Column "Name"
			dataColumn = dataTable.Columns["Name"];
			if(!row.IsNull(dataColumn))
				mappedObject.Name = (string)row[dataColumn];
			// Column "Time_of_day_policy"
			dataColumn = dataTable.Columns["Time_of_day_policy"];
			if(!row.IsNull(dataColumn))
				mappedObject.Time_of_day_policy = (byte)row[dataColumn];
			return mappedObject;
		}

		/// <summary>
		/// Creates a <see cref="System.Data.DataTable"/> object for 
		/// the <c>TimeOfDay</c> table.
		/// </summary>
		/// <returns>A reference to the <see cref="System.Data.DataTable"/> object.</returns>
		protected virtual DataTable CreateDataTable()
		{
			DataTable dataTable = new DataTable();
			dataTable.TableName = "TimeOfDay";
			DataColumn dataColumn;
			dataColumn = dataTable.Columns.Add("Time_of_day", typeof(byte));
			dataColumn.Caption = "time_of_day";
			dataColumn.AllowDBNull = false;
			dataColumn = dataTable.Columns.Add("Name", typeof(string));
			dataColumn.Caption = "name";
			dataColumn.MaxLength = 32;
			dataColumn.AllowDBNull = false;
			dataColumn = dataTable.Columns.Add("Time_of_day_policy", typeof(byte));
			dataColumn.Caption = "time_of_day_policy";
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
				case "Time_of_day":
					parameter = _db.AddParameter(cmd, paramName, DbType.Byte, value);
					break;

				case "Name":
					parameter = _db.AddParameter(cmd, paramName, DbType.AnsiString, value);
					break;

				case "Time_of_day_policy":
					parameter = _db.AddParameter(cmd, paramName, DbType.Byte, value);
					break;

				default:
					throw new ArgumentException("Unknown parameter name (" + paramName + ").");
			}
			return parameter;
		}

	} // End of TimeOfDayCollection_Base class
}  // End of namespace

// <fileinfo name="Base\CdrExportMapDetailCollection_Base.cs">
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
	/// The base class for <see cref="CdrExportMapDetailCollection"/>. Provides methods 
	/// for common database table operations. 
	/// </summary>
	/// <remarks>
	/// Do not change this source code. Update the <see cref="CdrExportMapDetailCollection"/>
	/// class if you need to add or change some functionality.
	/// </remarks>
	public abstract class CdrExportMapDetailCollection_Base
	{
		// Constants
		public const string Map_detail_idColumnName = "map_detail_id";
		public const string Map_idColumnName = "map_id";
		public const string SequenceColumnName = "sequence";
		public const string Field_nameColumnName = "field_name";
		public const string Format_typeColumnName = "format_type";

		// Instance fields
		private Rbr_Db _db;

		/// <summary>
		/// Initializes a new instance of the <see cref="CdrExportMapDetailCollection_Base"/> 
		/// class with the specified <see cref="Rbr_Db"/>.
		/// </summary>
		/// <param name="db">The <see cref="Rbr_Db"/> object.</param>
		public CdrExportMapDetailCollection_Base(Rbr_Db db)
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
		/// Gets an array of all records from the <c>CdrExportMapDetail</c> table.
		/// </summary>
		/// <returns>An array of <see cref="CdrExportMapDetailRow"/> objects.</returns>
		public virtual CdrExportMapDetailRow[] GetAll()
		{
			return MapRecords(CreateGetAllCommand());
		}

		/// <summary>
		/// Gets a <see cref="System.Data.DataTable"/> object that 
		/// includes all records from the <c>CdrExportMapDetail</c> table.
		/// </summary>
		/// <returns>A reference to the <see cref="System.Data.DataTable"/> object.</returns>
		public virtual DataTable GetAllAsDataTable()
		{
			return MapRecordsToDataTable(CreateGetAllCommand());
		}

		/// <summary>
		/// Creates and returns an <see cref="System.Data.IDbCommand"/> object that is used
		/// to retrieve all records from the <c>CdrExportMapDetail</c> table.
		/// </summary>
		/// <returns>A reference to the <see cref="System.Data.IDbCommand"/> object.</returns>
		protected virtual IDbCommand CreateGetAllCommand()
		{
			return CreateGetCommand(null, null);
		}

		/// <summary>
		/// Gets the first <see cref="CdrExportMapDetailRow"/> objects that 
		/// match the search condition.
		/// </summary>
		/// <param name="whereSql">The SQL search condition. For example: 
		/// <c>"FirstName='Smith' AND Zip=75038"</c>.</param>
		/// <returns>An instance of <see cref="CdrExportMapDetailRow"/> or null reference 
		/// (Nothing in Visual Basic) if the object was not found.</returns>
		public CdrExportMapDetailRow GetRow(string whereSql)
		{
			int totalRecordCount = -1;
			CdrExportMapDetailRow[] rows = GetAsArray(whereSql, null, 0, 1, ref totalRecordCount);
			return 0 == rows.Length ? null : rows[0];
		}

		/// <summary>
		/// Gets an array of <see cref="CdrExportMapDetailRow"/> objects that 
		/// match the search condition, in the the specified sort order.
		/// </summary>
		/// <param name="whereSql">The SQL search condition. For example: 
		/// <c>"FirstName='Smith' AND Zip=75038"</c>.</param>
		/// <param name="orderBySql">The column name(s) followed by "ASC" (ascending) or "DESC" (descending).
		/// Columns are sorted in ascending order by default. For example: <c>"LastName ASC, FirstName ASC"</c>.</param>
		/// <returns>An array of <see cref="CdrExportMapDetailRow"/> objects.</returns>
		public CdrExportMapDetailRow[] GetAsArray(string whereSql, string orderBySql)
		{
			int totalRecordCount = -1;
			return GetAsArray(whereSql, orderBySql, 0, int.MaxValue, ref totalRecordCount);
		}

		/// <summary>
		/// Gets an array of <see cref="CdrExportMapDetailRow"/> objects that 
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
		/// <returns>An array of <see cref="CdrExportMapDetailRow"/> objects.</returns>
		public virtual CdrExportMapDetailRow[] GetAsArray(string whereSql, string orderBySql,
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
			string sql = "SELECT * FROM [dbo].[CdrExportMapDetail]";
			if(null != whereSql && 0 < whereSql.Length)
				sql += " WHERE " + whereSql;
			if(null != orderBySql && 0 < orderBySql.Length)
				sql += " ORDER BY " + orderBySql;
			return _db.CreateCommand(sql);
		}

		/// <summary>
		/// Gets <see cref="CdrExportMapDetailRow"/> by the primary key.
		/// </summary>
		/// <param name="map_detail_id">The <c>map_detail_id</c> column value.</param>
		/// <returns>An instance of <see cref="CdrExportMapDetailRow"/> or null reference 
		/// (Nothing in Visual Basic) if the object was not found.</returns>
		public virtual CdrExportMapDetailRow GetByPrimaryKey(int map_detail_id)
		{
			string whereSql = "[map_detail_id]=" + _db.CreateSqlParameterName("Map_detail_id");
			IDbCommand cmd = CreateGetCommand(whereSql, null);
			AddParameter(cmd, "Map_detail_id", map_detail_id);
			CdrExportMapDetailRow[] tempArray = MapRecords(cmd);
			return 0 == tempArray.Length ? null : tempArray[0];
		}

		/// <summary>
		/// Gets an array of <see cref="CdrExportMapDetailRow"/> objects 
		/// by the <c>R_69</c> foreign key.
		/// </summary>
		/// <param name="map_id">The <c>map_id</c> column value.</param>
		/// <returns>An array of <see cref="CdrExportMapDetailRow"/> objects.</returns>
		public virtual CdrExportMapDetailRow[] GetByMap_id(int map_id)
		{
			return MapRecords(CreateGetByMap_idCommand(map_id));
		}

		/// <summary>
		/// Gets a <see cref="System.Data.DataTable"/> object 
		/// by the <c>R_69</c> foreign key.
		/// </summary>
		/// <param name="map_id">The <c>map_id</c> column value.</param>
		/// <returns>A reference to the <see cref="System.Data.DataTable"/> object.</returns>
		public virtual DataTable GetByMap_idAsDataTable(int map_id)
		{
			return MapRecordsToDataTable(CreateGetByMap_idCommand(map_id));
		}

		/// <summary>
		/// Creates an <see cref="System.Data.IDbCommand"/> object that can be used to 
		/// return records by the <c>R_69</c> foreign key.
		/// </summary>
		/// <param name="map_id">The <c>map_id</c> column value.</param>
		/// <returns>A reference to the <see cref="System.Data.IDbCommand"/> object.</returns>
		protected virtual IDbCommand CreateGetByMap_idCommand(int map_id)
		{
			string whereSql = "";
			whereSql += "[map_id]=" + _db.CreateSqlParameterName("Map_id");

			IDbCommand cmd = CreateGetCommand(whereSql, null);
			AddParameter(cmd, "Map_id", map_id);
			return cmd;
		}

		/// <summary>
		/// Adds a new record into the <c>CdrExportMapDetail</c> table.
		/// </summary>
		/// <param name="value">The <see cref="CdrExportMapDetailRow"/> object to be inserted.</param>
		public virtual void Insert(CdrExportMapDetailRow value)
		{
			string sqlStr = "INSERT INTO [dbo].[CdrExportMapDetail] (" +
				"[map_detail_id], " +
				"[map_id], " +
				"[sequence], " +
				"[field_name], " +
				"[format_type]" +
				") VALUES (" +
				_db.CreateSqlParameterName("Map_detail_id") + ", " +
				_db.CreateSqlParameterName("Map_id") + ", " +
				_db.CreateSqlParameterName("Sequence") + ", " +
				_db.CreateSqlParameterName("Field_name") + ", " +
				_db.CreateSqlParameterName("Format_type") + ")";
			IDbCommand cmd = _db.CreateCommand(sqlStr);
			AddParameter(cmd, "Map_detail_id", value.Map_detail_id);
			AddParameter(cmd, "Map_id", value.Map_id);
			AddParameter(cmd, "Sequence", value.Sequence);
			AddParameter(cmd, "Field_name", value.Field_name);
			AddParameter(cmd, "Format_type", value.Format_type);
			cmd.ExecuteNonQuery();
		}

		/// <summary>
		/// Updates a record in the <c>CdrExportMapDetail</c> table.
		/// </summary>
		/// <param name="value">The <see cref="CdrExportMapDetailRow"/>
		/// object used to update the table record.</param>
		/// <returns>true if the record was updated; otherwise, false.</returns>
		public virtual bool Update(CdrExportMapDetailRow value)
		{
			string sqlStr = "UPDATE [dbo].[CdrExportMapDetail] SET " +
				"[map_id]=" + _db.CreateSqlParameterName("Map_id") + ", " +
				"[sequence]=" + _db.CreateSqlParameterName("Sequence") + ", " +
				"[field_name]=" + _db.CreateSqlParameterName("Field_name") + ", " +
				"[format_type]=" + _db.CreateSqlParameterName("Format_type") +
				" WHERE " +
				"[map_detail_id]=" + _db.CreateSqlParameterName("Map_detail_id");
			IDbCommand cmd = _db.CreateCommand(sqlStr);
			AddParameter(cmd, "Map_id", value.Map_id);
			AddParameter(cmd, "Sequence", value.Sequence);
			AddParameter(cmd, "Field_name", value.Field_name);
			AddParameter(cmd, "Format_type", value.Format_type);
			AddParameter(cmd, "Map_detail_id", value.Map_detail_id);
			return 0 != cmd.ExecuteNonQuery();
		}

		/// <summary>
		/// Updates the <c>CdrExportMapDetail</c> table and calls the <c>AcceptChanges</c> method
		/// on the changed DataRow objects.
		/// </summary>
		/// <param name="table">The <see cref="System.Data.DataTable"/> used to update the data source.</param>
		public void Update(DataTable table)
		{
			Update(table, true);
		}

		/// <summary>
		/// Updates the <c>CdrExportMapDetail</c> table. Pass <c>false</c> as the <c>acceptChanges</c> 
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
							DeleteByPrimaryKey((int)row["Map_detail_id"]);
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
		/// Deletes the specified object from the <c>CdrExportMapDetail</c> table.
		/// </summary>
		/// <param name="value">The <see cref="CdrExportMapDetailRow"/> object to delete.</param>
		/// <returns>true if the record was deleted; otherwise, false.</returns>
		public bool Delete(CdrExportMapDetailRow value)
		{
			return DeleteByPrimaryKey(value.Map_detail_id);
		}

		/// <summary>
		/// Deletes a record from the <c>CdrExportMapDetail</c> table using
		/// the specified primary key.
		/// </summary>
		/// <param name="map_detail_id">The <c>map_detail_id</c> column value.</param>
		/// <returns>true if the record was deleted; otherwise, false.</returns>
		public virtual bool DeleteByPrimaryKey(int map_detail_id)
		{
			string whereSql = "[map_detail_id]=" + _db.CreateSqlParameterName("Map_detail_id");
			IDbCommand cmd = CreateDeleteCommand(whereSql);
			AddParameter(cmd, "Map_detail_id", map_detail_id);
			return 0 < cmd.ExecuteNonQuery();
		}

		/// <summary>
		/// Deletes records from the <c>CdrExportMapDetail</c> table using the 
		/// <c>R_69</c> foreign key.
		/// </summary>
		/// <param name="map_id">The <c>map_id</c> column value.</param>
		/// <returns>The number of records deleted from the table.</returns>
		public int DeleteByMap_id(int map_id)
		{
			return CreateDeleteByMap_idCommand(map_id).ExecuteNonQuery();
		}

		/// <summary>
		/// Creates an <see cref="System.Data.IDbCommand"/> object that can be used to
		/// delete records using the <c>R_69</c> foreign key.
		/// </summary>
		/// <param name="map_id">The <c>map_id</c> column value.</param>
		/// <returns>A reference to the <see cref="System.Data.IDbCommand"/> object.</returns>
		protected virtual IDbCommand CreateDeleteByMap_idCommand(int map_id)
		{
			string whereSql = "";
			whereSql += "[map_id]=" + _db.CreateSqlParameterName("Map_id");

			IDbCommand cmd = CreateDeleteCommand(whereSql);
			AddParameter(cmd, "Map_id", map_id);
			return cmd;
		}

		/// <summary>
		/// Deletes <c>CdrExportMapDetail</c> records that match the specified criteria.
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
		/// to delete <c>CdrExportMapDetail</c> records that match the specified criteria.
		/// </summary>
		/// <param name="whereSql">The SQL search condition. 
		/// For example: <c>"FirstName='Smith' AND Zip=75038"</c>.</param>
		/// <returns>A reference to the <see cref="System.Data.IDbCommand"/> object.</returns>
		protected virtual IDbCommand CreateDeleteCommand(string whereSql)
		{
			string sql = "DELETE FROM [dbo].[CdrExportMapDetail]";
			if(null != whereSql && 0 < whereSql.Length)
				sql += " WHERE " + whereSql;
			return _db.CreateCommand(sql);
		}

		/// <summary>
		/// Deletes all records from the <c>CdrExportMapDetail</c> table.
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
		/// <returns>An array of <see cref="CdrExportMapDetailRow"/> objects.</returns>
		protected CdrExportMapDetailRow[] MapRecords(IDbCommand command)
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
		/// <returns>An array of <see cref="CdrExportMapDetailRow"/> objects.</returns>
		protected CdrExportMapDetailRow[] MapRecords(IDataReader reader)
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
		/// <returns>An array of <see cref="CdrExportMapDetailRow"/> objects.</returns>
		protected virtual CdrExportMapDetailRow[] MapRecords(IDataReader reader, 
										int startIndex, int length, ref int totalRecordCount)
		{
			if(0 > startIndex)
				throw new ArgumentOutOfRangeException("startIndex", startIndex, "StartIndex cannot be less than zero.");
			if(0 > length)
				throw new ArgumentOutOfRangeException("length", length, "Length cannot be less than zero.");

			int map_detail_idColumnIndex = reader.GetOrdinal("map_detail_id");
			int map_idColumnIndex = reader.GetOrdinal("map_id");
			int sequenceColumnIndex = reader.GetOrdinal("sequence");
			int field_nameColumnIndex = reader.GetOrdinal("field_name");
			int format_typeColumnIndex = reader.GetOrdinal("format_type");

			System.Collections.ArrayList recordList = new System.Collections.ArrayList();
			int ri = -startIndex;
			while(reader.Read())
			{
				ri++;
				if(ri > 0 && ri <= length)
				{
					CdrExportMapDetailRow record = new CdrExportMapDetailRow();
					recordList.Add(record);

					record.Map_detail_id = Convert.ToInt32(reader.GetValue(map_detail_idColumnIndex));
					record.Map_id = Convert.ToInt32(reader.GetValue(map_idColumnIndex));
					record.Sequence = Convert.ToInt32(reader.GetValue(sequenceColumnIndex));
					record.Field_name = Convert.ToString(reader.GetValue(field_nameColumnIndex));
					if(!reader.IsDBNull(format_typeColumnIndex))
						record.Format_type = Convert.ToString(reader.GetValue(format_typeColumnIndex));

					if(ri == length && 0 != totalRecordCount)
						break;
				}
			}

			totalRecordCount = 0 == totalRecordCount ? ri + startIndex : -1;
			return (CdrExportMapDetailRow[])(recordList.ToArray(typeof(CdrExportMapDetailRow)));
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
		/// Converts <see cref="System.Data.DataRow"/> to <see cref="CdrExportMapDetailRow"/>.
		/// </summary>
		/// <param name="row">The <see cref="System.Data.DataRow"/> object to be mapped.</param>
		/// <returns>A reference to the <see cref="CdrExportMapDetailRow"/> object.</returns>
		protected virtual CdrExportMapDetailRow MapRow(DataRow row)
		{
			CdrExportMapDetailRow mappedObject = new CdrExportMapDetailRow();
			DataTable dataTable = row.Table;
			DataColumn dataColumn;
			// Column "Map_detail_id"
			dataColumn = dataTable.Columns["Map_detail_id"];
			if(!row.IsNull(dataColumn))
				mappedObject.Map_detail_id = (int)row[dataColumn];
			// Column "Map_id"
			dataColumn = dataTable.Columns["Map_id"];
			if(!row.IsNull(dataColumn))
				mappedObject.Map_id = (int)row[dataColumn];
			// Column "Sequence"
			dataColumn = dataTable.Columns["Sequence"];
			if(!row.IsNull(dataColumn))
				mappedObject.Sequence = (int)row[dataColumn];
			// Column "Field_name"
			dataColumn = dataTable.Columns["Field_name"];
			if(!row.IsNull(dataColumn))
				mappedObject.Field_name = (string)row[dataColumn];
			// Column "Format_type"
			dataColumn = dataTable.Columns["Format_type"];
			if(!row.IsNull(dataColumn))
				mappedObject.Format_type = (string)row[dataColumn];
			return mappedObject;
		}

		/// <summary>
		/// Creates a <see cref="System.Data.DataTable"/> object for 
		/// the <c>CdrExportMapDetail</c> table.
		/// </summary>
		/// <returns>A reference to the <see cref="System.Data.DataTable"/> object.</returns>
		protected virtual DataTable CreateDataTable()
		{
			DataTable dataTable = new DataTable();
			dataTable.TableName = "CdrExportMapDetail";
			DataColumn dataColumn;
			dataColumn = dataTable.Columns.Add("Map_detail_id", typeof(int));
			dataColumn.Caption = "map_detail_id";
			dataColumn.AllowDBNull = false;
			dataColumn = dataTable.Columns.Add("Map_id", typeof(int));
			dataColumn.Caption = "map_id";
			dataColumn.AllowDBNull = false;
			dataColumn = dataTable.Columns.Add("Sequence", typeof(int));
			dataColumn.Caption = "sequence";
			dataColumn.AllowDBNull = false;
			dataColumn = dataTable.Columns.Add("Field_name", typeof(string));
			dataColumn.Caption = "field_name";
			dataColumn.MaxLength = 500;
			dataColumn.AllowDBNull = false;
			dataColumn = dataTable.Columns.Add("Format_type", typeof(string));
			dataColumn.Caption = "format_type";
			dataColumn.MaxLength = 500;
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
				case "Map_detail_id":
					parameter = _db.AddParameter(cmd, paramName, DbType.Int32, value);
					break;

				case "Map_id":
					parameter = _db.AddParameter(cmd, paramName, DbType.Int32, value);
					break;

				case "Sequence":
					parameter = _db.AddParameter(cmd, paramName, DbType.Int32, value);
					break;

				case "Field_name":
					parameter = _db.AddParameter(cmd, paramName, DbType.AnsiString, value);
					break;

				case "Format_type":
					parameter = _db.AddParameter(cmd, paramName, DbType.AnsiString, value);
					break;

				default:
					throw new ArgumentException("Unknown parameter name (" + paramName + ").");
			}
			return parameter;
		}

	} // End of CdrExportMapDetailCollection_Base class
}  // End of namespace

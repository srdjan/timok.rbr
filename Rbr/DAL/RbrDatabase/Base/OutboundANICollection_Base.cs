// <fileinfo name="Base\OutboundANICollection_Base.cs">
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
	/// The base class for <see cref="OutboundANICollection"/>. Provides methods 
	/// for common database table operations. 
	/// </summary>
	/// <remarks>
	/// Do not change this source code. Update the <see cref="OutboundANICollection"/>
	/// class if you need to add or change some functionality.
	/// </remarks>
	public abstract class OutboundANICollection_Base
	{
		// Constants
		public const string Outbound_ani_idColumnName = "outbound_ani_id";
		public const string ANIColumnName = "ANI";
		public const string Carrier_route_idColumnName = "carrier_route_id";
		public const string VersionColumnName = "version";

		// Instance fields
		private Rbr_Db _db;

		/// <summary>
		/// Initializes a new instance of the <see cref="OutboundANICollection_Base"/> 
		/// class with the specified <see cref="Rbr_Db"/>.
		/// </summary>
		/// <param name="db">The <see cref="Rbr_Db"/> object.</param>
		public OutboundANICollection_Base(Rbr_Db db)
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
		/// Gets an array of all records from the <c>OutboundANI</c> table.
		/// </summary>
		/// <returns>An array of <see cref="OutboundANIRow"/> objects.</returns>
		public virtual OutboundANIRow[] GetAll()
		{
			return MapRecords(CreateGetAllCommand());
		}

		/// <summary>
		/// Gets a <see cref="System.Data.DataTable"/> object that 
		/// includes all records from the <c>OutboundANI</c> table.
		/// </summary>
		/// <returns>A reference to the <see cref="System.Data.DataTable"/> object.</returns>
		public virtual DataTable GetAllAsDataTable()
		{
			return MapRecordsToDataTable(CreateGetAllCommand());
		}

		/// <summary>
		/// Creates and returns an <see cref="System.Data.IDbCommand"/> object that is used
		/// to retrieve all records from the <c>OutboundANI</c> table.
		/// </summary>
		/// <returns>A reference to the <see cref="System.Data.IDbCommand"/> object.</returns>
		protected virtual IDbCommand CreateGetAllCommand()
		{
			return CreateGetCommand(null, null);
		}

		/// <summary>
		/// Gets the first <see cref="OutboundANIRow"/> objects that 
		/// match the search condition.
		/// </summary>
		/// <param name="whereSql">The SQL search condition. For example: 
		/// <c>"FirstName='Smith' AND Zip=75038"</c>.</param>
		/// <returns>An instance of <see cref="OutboundANIRow"/> or null reference 
		/// (Nothing in Visual Basic) if the object was not found.</returns>
		public OutboundANIRow GetRow(string whereSql)
		{
			int totalRecordCount = -1;
			OutboundANIRow[] rows = GetAsArray(whereSql, null, 0, 1, ref totalRecordCount);
			return 0 == rows.Length ? null : rows[0];
		}

		/// <summary>
		/// Gets an array of <see cref="OutboundANIRow"/> objects that 
		/// match the search condition, in the the specified sort order.
		/// </summary>
		/// <param name="whereSql">The SQL search condition. For example: 
		/// <c>"FirstName='Smith' AND Zip=75038"</c>.</param>
		/// <param name="orderBySql">The column name(s) followed by "ASC" (ascending) or "DESC" (descending).
		/// Columns are sorted in ascending order by default. For example: <c>"LastName ASC, FirstName ASC"</c>.</param>
		/// <returns>An array of <see cref="OutboundANIRow"/> objects.</returns>
		public OutboundANIRow[] GetAsArray(string whereSql, string orderBySql)
		{
			int totalRecordCount = -1;
			return GetAsArray(whereSql, orderBySql, 0, int.MaxValue, ref totalRecordCount);
		}

		/// <summary>
		/// Gets an array of <see cref="OutboundANIRow"/> objects that 
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
		/// <returns>An array of <see cref="OutboundANIRow"/> objects.</returns>
		public virtual OutboundANIRow[] GetAsArray(string whereSql, string orderBySql,
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
			string sql = "SELECT * FROM [dbo].[OutboundANI]";
			if(null != whereSql && 0 < whereSql.Length)
				sql += " WHERE " + whereSql;
			if(null != orderBySql && 0 < orderBySql.Length)
				sql += " ORDER BY " + orderBySql;
			return _db.CreateCommand(sql);
		}

		/// <summary>
		/// Gets <see cref="OutboundANIRow"/> by the primary key.
		/// </summary>
		/// <param name="outbound_ani_id">The <c>outbound_ani_id</c> column value.</param>
		/// <returns>An instance of <see cref="OutboundANIRow"/> or null reference 
		/// (Nothing in Visual Basic) if the object was not found.</returns>
		public virtual OutboundANIRow GetByPrimaryKey(int outbound_ani_id)
		{
			string whereSql = "[outbound_ani_id]=" + _db.CreateSqlParameterName("Outbound_ani_id");
			IDbCommand cmd = CreateGetCommand(whereSql, null);
			AddParameter(cmd, "Outbound_ani_id", outbound_ani_id);
			OutboundANIRow[] tempArray = MapRecords(cmd);
			return 0 == tempArray.Length ? null : tempArray[0];
		}

		/// <summary>
		/// Gets an array of <see cref="OutboundANIRow"/> objects 
		/// by the <c>R_369</c> foreign key.
		/// </summary>
		/// <param name="carrier_route_id">The <c>carrier_route_id</c> column value.</param>
		/// <returns>An array of <see cref="OutboundANIRow"/> objects.</returns>
		public virtual OutboundANIRow[] GetByCarrier_route_id(int carrier_route_id)
		{
			return MapRecords(CreateGetByCarrier_route_idCommand(carrier_route_id));
		}

		/// <summary>
		/// Gets a <see cref="System.Data.DataTable"/> object 
		/// by the <c>R_369</c> foreign key.
		/// </summary>
		/// <param name="carrier_route_id">The <c>carrier_route_id</c> column value.</param>
		/// <returns>A reference to the <see cref="System.Data.DataTable"/> object.</returns>
		public virtual DataTable GetByCarrier_route_idAsDataTable(int carrier_route_id)
		{
			return MapRecordsToDataTable(CreateGetByCarrier_route_idCommand(carrier_route_id));
		}

		/// <summary>
		/// Creates an <see cref="System.Data.IDbCommand"/> object that can be used to 
		/// return records by the <c>R_369</c> foreign key.
		/// </summary>
		/// <param name="carrier_route_id">The <c>carrier_route_id</c> column value.</param>
		/// <returns>A reference to the <see cref="System.Data.IDbCommand"/> object.</returns>
		protected virtual IDbCommand CreateGetByCarrier_route_idCommand(int carrier_route_id)
		{
			string whereSql = "";
			whereSql += "[carrier_route_id]=" + _db.CreateSqlParameterName("Carrier_route_id");

			IDbCommand cmd = CreateGetCommand(whereSql, null);
			AddParameter(cmd, "Carrier_route_id", carrier_route_id);
			return cmd;
		}

		/// <summary>
		/// Adds a new record into the <c>OutboundANI</c> table.
		/// </summary>
		/// <param name="value">The <see cref="OutboundANIRow"/> object to be inserted.</param>
		public virtual void Insert(OutboundANIRow value)
		{
			string sqlStr = "INSERT INTO [dbo].[OutboundANI] (" +
				"[outbound_ani_id], " +
				"[ANI], " +
				"[carrier_route_id]" +
				") VALUES (" +
				_db.CreateSqlParameterName("Outbound_ani_id") + ", " +
				_db.CreateSqlParameterName("ANI") + ", " +
				_db.CreateSqlParameterName("Carrier_route_id") + ")";
			IDbCommand cmd = _db.CreateCommand(sqlStr);
			AddParameter(cmd, "Outbound_ani_id", value.Outbound_ani_id);
			AddParameter(cmd, "ANI", value.ANI);
			AddParameter(cmd, "Carrier_route_id", value.Carrier_route_id);
			cmd.ExecuteNonQuery();
		}

		/// <summary>
		/// Updates a record in the <c>OutboundANI</c> table.
		/// </summary>
		/// <param name="value">The <see cref="OutboundANIRow"/>
		/// object used to update the table record.</param>
		/// <returns>true if the record was updated; otherwise, false.</returns>
		public virtual bool Update(OutboundANIRow value)
		{
			string sqlStr = "UPDATE [dbo].[OutboundANI] SET " +
				"[ANI]=" + _db.CreateSqlParameterName("ANI") + ", " +
				"[carrier_route_id]=" + _db.CreateSqlParameterName("Carrier_route_id") +
				" WHERE " +
				"[outbound_ani_id]=" + _db.CreateSqlParameterName("Outbound_ani_id");
			IDbCommand cmd = _db.CreateCommand(sqlStr);
			AddParameter(cmd, "ANI", value.ANI);
			AddParameter(cmd, "Carrier_route_id", value.Carrier_route_id);
			AddParameter(cmd, "Outbound_ani_id", value.Outbound_ani_id);
			return 0 != cmd.ExecuteNonQuery();
		}

		/// <summary>
		/// Updates the <c>OutboundANI</c> table and calls the <c>AcceptChanges</c> method
		/// on the changed DataRow objects.
		/// </summary>
		/// <param name="table">The <see cref="System.Data.DataTable"/> used to update the data source.</param>
		public void Update(DataTable table)
		{
			Update(table, true);
		}

		/// <summary>
		/// Updates the <c>OutboundANI</c> table. Pass <c>false</c> as the <c>acceptChanges</c> 
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
							DeleteByPrimaryKey((int)row["Outbound_ani_id"]);
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
		/// Deletes the specified object from the <c>OutboundANI</c> table.
		/// </summary>
		/// <param name="value">The <see cref="OutboundANIRow"/> object to delete.</param>
		/// <returns>true if the record was deleted; otherwise, false.</returns>
		public bool Delete(OutboundANIRow value)
		{
			return DeleteByPrimaryKey(value.Outbound_ani_id);
		}

		/// <summary>
		/// Deletes a record from the <c>OutboundANI</c> table using
		/// the specified primary key.
		/// </summary>
		/// <param name="outbound_ani_id">The <c>outbound_ani_id</c> column value.</param>
		/// <returns>true if the record was deleted; otherwise, false.</returns>
		public virtual bool DeleteByPrimaryKey(int outbound_ani_id)
		{
			string whereSql = "[outbound_ani_id]=" + _db.CreateSqlParameterName("Outbound_ani_id");
			IDbCommand cmd = CreateDeleteCommand(whereSql);
			AddParameter(cmd, "Outbound_ani_id", outbound_ani_id);
			return 0 < cmd.ExecuteNonQuery();
		}

		/// <summary>
		/// Deletes records from the <c>OutboundANI</c> table using the 
		/// <c>R_369</c> foreign key.
		/// </summary>
		/// <param name="carrier_route_id">The <c>carrier_route_id</c> column value.</param>
		/// <returns>The number of records deleted from the table.</returns>
		public int DeleteByCarrier_route_id(int carrier_route_id)
		{
			return CreateDeleteByCarrier_route_idCommand(carrier_route_id).ExecuteNonQuery();
		}

		/// <summary>
		/// Creates an <see cref="System.Data.IDbCommand"/> object that can be used to
		/// delete records using the <c>R_369</c> foreign key.
		/// </summary>
		/// <param name="carrier_route_id">The <c>carrier_route_id</c> column value.</param>
		/// <returns>A reference to the <see cref="System.Data.IDbCommand"/> object.</returns>
		protected virtual IDbCommand CreateDeleteByCarrier_route_idCommand(int carrier_route_id)
		{
			string whereSql = "";
			whereSql += "[carrier_route_id]=" + _db.CreateSqlParameterName("Carrier_route_id");

			IDbCommand cmd = CreateDeleteCommand(whereSql);
			AddParameter(cmd, "Carrier_route_id", carrier_route_id);
			return cmd;
		}

		/// <summary>
		/// Deletes <c>OutboundANI</c> records that match the specified criteria.
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
		/// to delete <c>OutboundANI</c> records that match the specified criteria.
		/// </summary>
		/// <param name="whereSql">The SQL search condition. 
		/// For example: <c>"FirstName='Smith' AND Zip=75038"</c>.</param>
		/// <returns>A reference to the <see cref="System.Data.IDbCommand"/> object.</returns>
		protected virtual IDbCommand CreateDeleteCommand(string whereSql)
		{
			string sql = "DELETE FROM [dbo].[OutboundANI]";
			if(null != whereSql && 0 < whereSql.Length)
				sql += " WHERE " + whereSql;
			return _db.CreateCommand(sql);
		}

		/// <summary>
		/// Deletes all records from the <c>OutboundANI</c> table.
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
		/// <returns>An array of <see cref="OutboundANIRow"/> objects.</returns>
		protected OutboundANIRow[] MapRecords(IDbCommand command)
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
		/// <returns>An array of <see cref="OutboundANIRow"/> objects.</returns>
		protected OutboundANIRow[] MapRecords(IDataReader reader)
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
		/// <returns>An array of <see cref="OutboundANIRow"/> objects.</returns>
		protected virtual OutboundANIRow[] MapRecords(IDataReader reader, 
										int startIndex, int length, ref int totalRecordCount)
		{
			if(0 > startIndex)
				throw new ArgumentOutOfRangeException("startIndex", startIndex, "StartIndex cannot be less than zero.");
			if(0 > length)
				throw new ArgumentOutOfRangeException("length", length, "Length cannot be less than zero.");

			int outbound_ani_idColumnIndex = reader.GetOrdinal("outbound_ani_id");
			int aniColumnIndex = reader.GetOrdinal("ANI");
			int carrier_route_idColumnIndex = reader.GetOrdinal("carrier_route_id");
			int versionColumnIndex = reader.GetOrdinal("version");

			System.Collections.ArrayList recordList = new System.Collections.ArrayList();
			int ri = -startIndex;
			while(reader.Read())
			{
				ri++;
				if(ri > 0 && ri <= length)
				{
					OutboundANIRow record = new OutboundANIRow();
					recordList.Add(record);

					record.Outbound_ani_id = Convert.ToInt32(reader.GetValue(outbound_ani_idColumnIndex));
					record.ANI = Convert.ToInt64(reader.GetValue(aniColumnIndex));
					record.Carrier_route_id = Convert.ToInt32(reader.GetValue(carrier_route_idColumnIndex));
					record.Version = Convert.ToInt32(reader.GetValue(versionColumnIndex));

					if(ri == length && 0 != totalRecordCount)
						break;
				}
			}

			totalRecordCount = 0 == totalRecordCount ? ri + startIndex : -1;
			return (OutboundANIRow[])(recordList.ToArray(typeof(OutboundANIRow)));
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
		/// Converts <see cref="System.Data.DataRow"/> to <see cref="OutboundANIRow"/>.
		/// </summary>
		/// <param name="row">The <see cref="System.Data.DataRow"/> object to be mapped.</param>
		/// <returns>A reference to the <see cref="OutboundANIRow"/> object.</returns>
		protected virtual OutboundANIRow MapRow(DataRow row)
		{
			OutboundANIRow mappedObject = new OutboundANIRow();
			DataTable dataTable = row.Table;
			DataColumn dataColumn;
			// Column "Outbound_ani_id"
			dataColumn = dataTable.Columns["Outbound_ani_id"];
			if(!row.IsNull(dataColumn))
				mappedObject.Outbound_ani_id = (int)row[dataColumn];
			// Column "ANI"
			dataColumn = dataTable.Columns["ANI"];
			if(!row.IsNull(dataColumn))
				mappedObject.ANI = (long)row[dataColumn];
			// Column "Carrier_route_id"
			dataColumn = dataTable.Columns["Carrier_route_id"];
			if(!row.IsNull(dataColumn))
				mappedObject.Carrier_route_id = (int)row[dataColumn];
			// Column "Version"
			dataColumn = dataTable.Columns["Version"];
			if(!row.IsNull(dataColumn))
				mappedObject.Version = (int)row[dataColumn];
			return mappedObject;
		}

		/// <summary>
		/// Creates a <see cref="System.Data.DataTable"/> object for 
		/// the <c>OutboundANI</c> table.
		/// </summary>
		/// <returns>A reference to the <see cref="System.Data.DataTable"/> object.</returns>
		protected virtual DataTable CreateDataTable()
		{
			DataTable dataTable = new DataTable();
			dataTable.TableName = "OutboundANI";
			DataColumn dataColumn;
			dataColumn = dataTable.Columns.Add("Outbound_ani_id", typeof(int));
			dataColumn.Caption = "outbound_ani_id";
			dataColumn.AllowDBNull = false;
			dataColumn = dataTable.Columns.Add("ANI", typeof(long));
			dataColumn.AllowDBNull = false;
			dataColumn = dataTable.Columns.Add("Carrier_route_id", typeof(int));
			dataColumn.Caption = "carrier_route_id";
			dataColumn.AllowDBNull = false;
			dataColumn = dataTable.Columns.Add("Version", typeof(int));
			dataColumn.Caption = "version";
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
				case "Outbound_ani_id":
					parameter = _db.AddParameter(cmd, paramName, DbType.Int32, value);
					break;

				case "ANI":
					parameter = _db.AddParameter(cmd, paramName, DbType.Int64, value);
					break;

				case "Carrier_route_id":
					parameter = _db.AddParameter(cmd, paramName, DbType.Int32, value);
					break;

				case "Version":
					parameter = _db.AddParameter(cmd, paramName, DbType.Int32, value);
					break;

				default:
					throw new ArgumentException("Unknown parameter name (" + paramName + ").");
			}
			return parameter;
		}

	} // End of OutboundANICollection_Base class
}  // End of namespace

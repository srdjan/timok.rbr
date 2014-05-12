// <fileinfo name="Base\LCRBlackListCollection_Base.cs">
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
	/// The base class for <see cref="LCRBlackListCollection"/>. Provides methods 
	/// for common database table operations. 
	/// </summary>
	/// <remarks>
	/// Do not change this source code. Update the <see cref="LCRBlackListCollection"/>
	/// class if you need to add or change some functionality.
	/// </remarks>
	public abstract class LCRBlackListCollection_Base
	{
		// Constants
		public const string Routing_plan_idColumnName = "routing_plan_id";
		public const string Route_idColumnName = "route_id";
		public const string Carrier_acct_idColumnName = "carrier_acct_id";
		public const string VersionColumnName = "version";

		// Instance fields
		private Rbr_Db _db;

		/// <summary>
		/// Initializes a new instance of the <see cref="LCRBlackListCollection_Base"/> 
		/// class with the specified <see cref="Rbr_Db"/>.
		/// </summary>
		/// <param name="db">The <see cref="Rbr_Db"/> object.</param>
		public LCRBlackListCollection_Base(Rbr_Db db)
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
		/// Gets an array of all records from the <c>LCRBlackList</c> table.
		/// </summary>
		/// <returns>An array of <see cref="LCRBlackListRow"/> objects.</returns>
		public virtual LCRBlackListRow[] GetAll()
		{
			return MapRecords(CreateGetAllCommand());
		}

		/// <summary>
		/// Gets a <see cref="System.Data.DataTable"/> object that 
		/// includes all records from the <c>LCRBlackList</c> table.
		/// </summary>
		/// <returns>A reference to the <see cref="System.Data.DataTable"/> object.</returns>
		public virtual DataTable GetAllAsDataTable()
		{
			return MapRecordsToDataTable(CreateGetAllCommand());
		}

		/// <summary>
		/// Creates and returns an <see cref="System.Data.IDbCommand"/> object that is used
		/// to retrieve all records from the <c>LCRBlackList</c> table.
		/// </summary>
		/// <returns>A reference to the <see cref="System.Data.IDbCommand"/> object.</returns>
		protected virtual IDbCommand CreateGetAllCommand()
		{
			return CreateGetCommand(null, null);
		}

		/// <summary>
		/// Gets the first <see cref="LCRBlackListRow"/> objects that 
		/// match the search condition.
		/// </summary>
		/// <param name="whereSql">The SQL search condition. For example: 
		/// <c>"FirstName='Smith' AND Zip=75038"</c>.</param>
		/// <returns>An instance of <see cref="LCRBlackListRow"/> or null reference 
		/// (Nothing in Visual Basic) if the object was not found.</returns>
		public LCRBlackListRow GetRow(string whereSql)
		{
			int totalRecordCount = -1;
			LCRBlackListRow[] rows = GetAsArray(whereSql, null, 0, 1, ref totalRecordCount);
			return 0 == rows.Length ? null : rows[0];
		}

		/// <summary>
		/// Gets an array of <see cref="LCRBlackListRow"/> objects that 
		/// match the search condition, in the the specified sort order.
		/// </summary>
		/// <param name="whereSql">The SQL search condition. For example: 
		/// <c>"FirstName='Smith' AND Zip=75038"</c>.</param>
		/// <param name="orderBySql">The column name(s) followed by "ASC" (ascending) or "DESC" (descending).
		/// Columns are sorted in ascending order by default. For example: <c>"LastName ASC, FirstName ASC"</c>.</param>
		/// <returns>An array of <see cref="LCRBlackListRow"/> objects.</returns>
		public LCRBlackListRow[] GetAsArray(string whereSql, string orderBySql)
		{
			int totalRecordCount = -1;
			return GetAsArray(whereSql, orderBySql, 0, int.MaxValue, ref totalRecordCount);
		}

		/// <summary>
		/// Gets an array of <see cref="LCRBlackListRow"/> objects that 
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
		/// <returns>An array of <see cref="LCRBlackListRow"/> objects.</returns>
		public virtual LCRBlackListRow[] GetAsArray(string whereSql, string orderBySql,
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
			string sql = "SELECT * FROM [dbo].[LCRBlackList]";
			if(null != whereSql && 0 < whereSql.Length)
				sql += " WHERE " + whereSql;
			if(null != orderBySql && 0 < orderBySql.Length)
				sql += " ORDER BY " + orderBySql;
			return _db.CreateCommand(sql);
		}

		/// <summary>
		/// Gets <see cref="LCRBlackListRow"/> by the primary key.
		/// </summary>
		/// <param name="routing_plan_id">The <c>routing_plan_id</c> column value.</param>
		/// <param name="route_id">The <c>route_id</c> column value.</param>
		/// <param name="carrier_acct_id">The <c>carrier_acct_id</c> column value.</param>
		/// <returns>An instance of <see cref="LCRBlackListRow"/> or null reference 
		/// (Nothing in Visual Basic) if the object was not found.</returns>
		public virtual LCRBlackListRow GetByPrimaryKey(int routing_plan_id, int route_id, short carrier_acct_id)
		{
			string whereSql = "[routing_plan_id]=" + _db.CreateSqlParameterName("Routing_plan_id") + " AND " +
							  "[route_id]=" + _db.CreateSqlParameterName("Route_id") + " AND " +
							  "[carrier_acct_id]=" + _db.CreateSqlParameterName("Carrier_acct_id");
			IDbCommand cmd = CreateGetCommand(whereSql, null);
			AddParameter(cmd, "Routing_plan_id", routing_plan_id);
			AddParameter(cmd, "Route_id", route_id);
			AddParameter(cmd, "Carrier_acct_id", carrier_acct_id);
			LCRBlackListRow[] tempArray = MapRecords(cmd);
			return 0 == tempArray.Length ? null : tempArray[0];
		}

		/// <summary>
		/// Gets an array of <see cref="LCRBlackListRow"/> objects 
		/// by the <c>R_272</c> foreign key.
		/// </summary>
		/// <param name="carrier_acct_id">The <c>carrier_acct_id</c> column value.</param>
		/// <returns>An array of <see cref="LCRBlackListRow"/> objects.</returns>
		public virtual LCRBlackListRow[] GetByCarrier_acct_id(short carrier_acct_id)
		{
			return MapRecords(CreateGetByCarrier_acct_idCommand(carrier_acct_id));
		}

		/// <summary>
		/// Gets a <see cref="System.Data.DataTable"/> object 
		/// by the <c>R_272</c> foreign key.
		/// </summary>
		/// <param name="carrier_acct_id">The <c>carrier_acct_id</c> column value.</param>
		/// <returns>A reference to the <see cref="System.Data.DataTable"/> object.</returns>
		public virtual DataTable GetByCarrier_acct_idAsDataTable(short carrier_acct_id)
		{
			return MapRecordsToDataTable(CreateGetByCarrier_acct_idCommand(carrier_acct_id));
		}

		/// <summary>
		/// Creates an <see cref="System.Data.IDbCommand"/> object that can be used to 
		/// return records by the <c>R_272</c> foreign key.
		/// </summary>
		/// <param name="carrier_acct_id">The <c>carrier_acct_id</c> column value.</param>
		/// <returns>A reference to the <see cref="System.Data.IDbCommand"/> object.</returns>
		protected virtual IDbCommand CreateGetByCarrier_acct_idCommand(short carrier_acct_id)
		{
			string whereSql = "";
			whereSql += "[carrier_acct_id]=" + _db.CreateSqlParameterName("Carrier_acct_id");

			IDbCommand cmd = CreateGetCommand(whereSql, null);
			AddParameter(cmd, "Carrier_acct_id", carrier_acct_id);
			return cmd;
		}

		/// <summary>
		/// Gets an array of <see cref="LCRBlackListRow"/> objects 
		/// by the <c>R_344</c> foreign key.
		/// </summary>
		/// <param name="routing_plan_id">The <c>routing_plan_id</c> column value.</param>
		/// <param name="route_id">The <c>route_id</c> column value.</param>
		/// <returns>An array of <see cref="LCRBlackListRow"/> objects.</returns>
		public virtual LCRBlackListRow[] GetByRouting_plan_id_Route_id(int routing_plan_id, int route_id)
		{
			return MapRecords(CreateGetByRouting_plan_id_Route_idCommand(routing_plan_id, route_id));
		}

		/// <summary>
		/// Gets a <see cref="System.Data.DataTable"/> object 
		/// by the <c>R_344</c> foreign key.
		/// </summary>
		/// <param name="routing_plan_id">The <c>routing_plan_id</c> column value.</param>
		/// <param name="route_id">The <c>route_id</c> column value.</param>
		/// <returns>A reference to the <see cref="System.Data.DataTable"/> object.</returns>
		public virtual DataTable GetByRouting_plan_id_Route_idAsDataTable(int routing_plan_id, int route_id)
		{
			return MapRecordsToDataTable(CreateGetByRouting_plan_id_Route_idCommand(routing_plan_id, route_id));
		}

		/// <summary>
		/// Creates an <see cref="System.Data.IDbCommand"/> object that can be used to 
		/// return records by the <c>R_344</c> foreign key.
		/// </summary>
		/// <param name="routing_plan_id">The <c>routing_plan_id</c> column value.</param>
		/// <param name="route_id">The <c>route_id</c> column value.</param>
		/// <returns>A reference to the <see cref="System.Data.IDbCommand"/> object.</returns>
		protected virtual IDbCommand CreateGetByRouting_plan_id_Route_idCommand(int routing_plan_id, int route_id)
		{
			string whereSql = "";
			whereSql += "[routing_plan_id]=" + _db.CreateSqlParameterName("Routing_plan_id");
			whereSql += " AND [route_id]=" + _db.CreateSqlParameterName("Route_id");

			IDbCommand cmd = CreateGetCommand(whereSql, null);
			AddParameter(cmd, "Routing_plan_id", routing_plan_id);
			AddParameter(cmd, "Route_id", route_id);
			return cmd;
		}

		/// <summary>
		/// Adds a new record into the <c>LCRBlackList</c> table.
		/// </summary>
		/// <param name="value">The <see cref="LCRBlackListRow"/> object to be inserted.</param>
		public virtual void Insert(LCRBlackListRow value)
		{
			string sqlStr = "INSERT INTO [dbo].[LCRBlackList] (" +
				"[routing_plan_id], " +
				"[route_id], " +
				"[carrier_acct_id]" +
				") VALUES (" +
				_db.CreateSqlParameterName("Routing_plan_id") + ", " +
				_db.CreateSqlParameterName("Route_id") + ", " +
				_db.CreateSqlParameterName("Carrier_acct_id") + ")";
			IDbCommand cmd = _db.CreateCommand(sqlStr);
			AddParameter(cmd, "Routing_plan_id", value.Routing_plan_id);
			AddParameter(cmd, "Route_id", value.Route_id);
			AddParameter(cmd, "Carrier_acct_id", value.Carrier_acct_id);
			cmd.ExecuteNonQuery();
		}

		/// <summary>
		/// Deletes the specified object from the <c>LCRBlackList</c> table.
		/// </summary>
		/// <param name="value">The <see cref="LCRBlackListRow"/> object to delete.</param>
		/// <returns>true if the record was deleted; otherwise, false.</returns>
		public bool Delete(LCRBlackListRow value)
		{
			return DeleteByPrimaryKey(value.Routing_plan_id, value.Route_id, value.Carrier_acct_id);
		}

		/// <summary>
		/// Deletes a record from the <c>LCRBlackList</c> table using
		/// the specified primary key.
		/// </summary>
		/// <param name="routing_plan_id">The <c>routing_plan_id</c> column value.</param>
		/// <param name="route_id">The <c>route_id</c> column value.</param>
		/// <param name="carrier_acct_id">The <c>carrier_acct_id</c> column value.</param>
		/// <returns>true if the record was deleted; otherwise, false.</returns>
		public virtual bool DeleteByPrimaryKey(int routing_plan_id, int route_id, short carrier_acct_id)
		{
			string whereSql = "[routing_plan_id]=" + _db.CreateSqlParameterName("Routing_plan_id") + " AND " +
							  "[route_id]=" + _db.CreateSqlParameterName("Route_id") + " AND " +
							  "[carrier_acct_id]=" + _db.CreateSqlParameterName("Carrier_acct_id");
			IDbCommand cmd = CreateDeleteCommand(whereSql);
			AddParameter(cmd, "Routing_plan_id", routing_plan_id);
			AddParameter(cmd, "Route_id", route_id);
			AddParameter(cmd, "Carrier_acct_id", carrier_acct_id);
			return 0 < cmd.ExecuteNonQuery();
		}

		/// <summary>
		/// Deletes records from the <c>LCRBlackList</c> table using the 
		/// <c>R_272</c> foreign key.
		/// </summary>
		/// <param name="carrier_acct_id">The <c>carrier_acct_id</c> column value.</param>
		/// <returns>The number of records deleted from the table.</returns>
		public int DeleteByCarrier_acct_id(short carrier_acct_id)
		{
			return CreateDeleteByCarrier_acct_idCommand(carrier_acct_id).ExecuteNonQuery();
		}

		/// <summary>
		/// Creates an <see cref="System.Data.IDbCommand"/> object that can be used to
		/// delete records using the <c>R_272</c> foreign key.
		/// </summary>
		/// <param name="carrier_acct_id">The <c>carrier_acct_id</c> column value.</param>
		/// <returns>A reference to the <see cref="System.Data.IDbCommand"/> object.</returns>
		protected virtual IDbCommand CreateDeleteByCarrier_acct_idCommand(short carrier_acct_id)
		{
			string whereSql = "";
			whereSql += "[carrier_acct_id]=" + _db.CreateSqlParameterName("Carrier_acct_id");

			IDbCommand cmd = CreateDeleteCommand(whereSql);
			AddParameter(cmd, "Carrier_acct_id", carrier_acct_id);
			return cmd;
		}

		/// <summary>
		/// Deletes records from the <c>LCRBlackList</c> table using the 
		/// <c>R_344</c> foreign key.
		/// </summary>
		/// <param name="routing_plan_id">The <c>routing_plan_id</c> column value.</param>
		/// <param name="route_id">The <c>route_id</c> column value.</param>
		/// <returns>The number of records deleted from the table.</returns>
		public int DeleteByRouting_plan_id_Route_id(int routing_plan_id, int route_id)
		{
			return CreateDeleteByRouting_plan_id_Route_idCommand(routing_plan_id, route_id).ExecuteNonQuery();
		}

		/// <summary>
		/// Creates an <see cref="System.Data.IDbCommand"/> object that can be used to
		/// delete records using the <c>R_344</c> foreign key.
		/// </summary>
		/// <param name="routing_plan_id">The <c>routing_plan_id</c> column value.</param>
		/// <param name="route_id">The <c>route_id</c> column value.</param>
		/// <returns>A reference to the <see cref="System.Data.IDbCommand"/> object.</returns>
		protected virtual IDbCommand CreateDeleteByRouting_plan_id_Route_idCommand(int routing_plan_id, int route_id)
		{
			string whereSql = "";
			whereSql += "[routing_plan_id]=" + _db.CreateSqlParameterName("Routing_plan_id");
			whereSql += " AND [route_id]=" + _db.CreateSqlParameterName("Route_id");

			IDbCommand cmd = CreateDeleteCommand(whereSql);
			AddParameter(cmd, "Routing_plan_id", routing_plan_id);
			AddParameter(cmd, "Route_id", route_id);
			return cmd;
		}

		/// <summary>
		/// Deletes <c>LCRBlackList</c> records that match the specified criteria.
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
		/// to delete <c>LCRBlackList</c> records that match the specified criteria.
		/// </summary>
		/// <param name="whereSql">The SQL search condition. 
		/// For example: <c>"FirstName='Smith' AND Zip=75038"</c>.</param>
		/// <returns>A reference to the <see cref="System.Data.IDbCommand"/> object.</returns>
		protected virtual IDbCommand CreateDeleteCommand(string whereSql)
		{
			string sql = "DELETE FROM [dbo].[LCRBlackList]";
			if(null != whereSql && 0 < whereSql.Length)
				sql += " WHERE " + whereSql;
			return _db.CreateCommand(sql);
		}

		/// <summary>
		/// Deletes all records from the <c>LCRBlackList</c> table.
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
		/// <returns>An array of <see cref="LCRBlackListRow"/> objects.</returns>
		protected LCRBlackListRow[] MapRecords(IDbCommand command)
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
		/// <returns>An array of <see cref="LCRBlackListRow"/> objects.</returns>
		protected LCRBlackListRow[] MapRecords(IDataReader reader)
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
		/// <returns>An array of <see cref="LCRBlackListRow"/> objects.</returns>
		protected virtual LCRBlackListRow[] MapRecords(IDataReader reader, 
										int startIndex, int length, ref int totalRecordCount)
		{
			if(0 > startIndex)
				throw new ArgumentOutOfRangeException("startIndex", startIndex, "StartIndex cannot be less than zero.");
			if(0 > length)
				throw new ArgumentOutOfRangeException("length", length, "Length cannot be less than zero.");

			int routing_plan_idColumnIndex = reader.GetOrdinal("routing_plan_id");
			int route_idColumnIndex = reader.GetOrdinal("route_id");
			int carrier_acct_idColumnIndex = reader.GetOrdinal("carrier_acct_id");
			int versionColumnIndex = reader.GetOrdinal("version");

			System.Collections.ArrayList recordList = new System.Collections.ArrayList();
			int ri = -startIndex;
			while(reader.Read())
			{
				ri++;
				if(ri > 0 && ri <= length)
				{
					LCRBlackListRow record = new LCRBlackListRow();
					recordList.Add(record);

					record.Routing_plan_id = Convert.ToInt32(reader.GetValue(routing_plan_idColumnIndex));
					record.Route_id = Convert.ToInt32(reader.GetValue(route_idColumnIndex));
					record.Carrier_acct_id = Convert.ToInt16(reader.GetValue(carrier_acct_idColumnIndex));
					record.Version = Convert.ToInt32(reader.GetValue(versionColumnIndex));

					if(ri == length && 0 != totalRecordCount)
						break;
				}
			}

			totalRecordCount = 0 == totalRecordCount ? ri + startIndex : -1;
			return (LCRBlackListRow[])(recordList.ToArray(typeof(LCRBlackListRow)));
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
		/// Converts <see cref="System.Data.DataRow"/> to <see cref="LCRBlackListRow"/>.
		/// </summary>
		/// <param name="row">The <see cref="System.Data.DataRow"/> object to be mapped.</param>
		/// <returns>A reference to the <see cref="LCRBlackListRow"/> object.</returns>
		protected virtual LCRBlackListRow MapRow(DataRow row)
		{
			LCRBlackListRow mappedObject = new LCRBlackListRow();
			DataTable dataTable = row.Table;
			DataColumn dataColumn;
			// Column "Routing_plan_id"
			dataColumn = dataTable.Columns["Routing_plan_id"];
			if(!row.IsNull(dataColumn))
				mappedObject.Routing_plan_id = (int)row[dataColumn];
			// Column "Route_id"
			dataColumn = dataTable.Columns["Route_id"];
			if(!row.IsNull(dataColumn))
				mappedObject.Route_id = (int)row[dataColumn];
			// Column "Carrier_acct_id"
			dataColumn = dataTable.Columns["Carrier_acct_id"];
			if(!row.IsNull(dataColumn))
				mappedObject.Carrier_acct_id = (short)row[dataColumn];
			// Column "Version"
			dataColumn = dataTable.Columns["Version"];
			if(!row.IsNull(dataColumn))
				mappedObject.Version = (int)row[dataColumn];
			return mappedObject;
		}

		/// <summary>
		/// Creates a <see cref="System.Data.DataTable"/> object for 
		/// the <c>LCRBlackList</c> table.
		/// </summary>
		/// <returns>A reference to the <see cref="System.Data.DataTable"/> object.</returns>
		protected virtual DataTable CreateDataTable()
		{
			DataTable dataTable = new DataTable();
			dataTable.TableName = "LCRBlackList";
			DataColumn dataColumn;
			dataColumn = dataTable.Columns.Add("Routing_plan_id", typeof(int));
			dataColumn.Caption = "routing_plan_id";
			dataColumn.AllowDBNull = false;
			dataColumn = dataTable.Columns.Add("Route_id", typeof(int));
			dataColumn.Caption = "route_id";
			dataColumn.AllowDBNull = false;
			dataColumn = dataTable.Columns.Add("Carrier_acct_id", typeof(short));
			dataColumn.Caption = "carrier_acct_id";
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
				case "Routing_plan_id":
					parameter = _db.AddParameter(cmd, paramName, DbType.Int32, value);
					break;

				case "Route_id":
					parameter = _db.AddParameter(cmd, paramName, DbType.Int32, value);
					break;

				case "Carrier_acct_id":
					parameter = _db.AddParameter(cmd, paramName, DbType.Int16, value);
					break;

				case "Version":
					parameter = _db.AddParameter(cmd, paramName, DbType.Int32, value);
					break;

				default:
					throw new ArgumentException("Unknown parameter name (" + paramName + ").");
			}
			return parameter;
		}

	} // End of LCRBlackListCollection_Base class
}  // End of namespace

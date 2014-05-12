// <fileinfo name="Base\RouteCollection_Base.cs">
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

namespace Timok.Rbr.DAL.RbrDatabase.Base {
	/// <summary>
	/// The base class for <see cref="RouteCollection"/>. Provides methods 
	/// for common database table operations. 
	/// </summary>
	/// <remarks>
	/// Do not change this source code. Update the <see cref="RouteCollection"/>
	/// class if you need to add or change some functionality.
	/// </remarks>
	public abstract class RouteCollection_Base {
		// Constants
		public const string Route_idColumnName = "route_id";
		public const string NameColumnName = "name";
		public const string StatusColumnName = "status";
		public const string Calling_plan_idColumnName = "calling_plan_id";
		public const string Country_idColumnName = "country_id";
		public const string VersionColumnName = "version";
		public const string Routing_numberColumnName = "routing_number";

		// Instance fields
		readonly Rbr_Db _db;

		/// <summary>
		/// Initializes a new instance of the <see cref="RouteCollection_Base"/> 
		/// class with the specified <see cref="Rbr_Db"/>.
		/// </summary>
		/// <param name="db">The <see cref="Rbr_Db"/> object.</param>
		protected RouteCollection_Base(Rbr_Db db)
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
		/// Gets an array of all records from the <c>Route</c> table.
		/// </summary>
		/// <returns>An array of <see cref="RouteRow"/> objects.</returns>
		public virtual RouteRow[] GetAll()
		{
			return MapRecords(CreateGetAllCommand());
		}

		/// <summary>
		/// Gets a <see cref="System.Data.DataTable"/> object that 
		/// includes all records from the <c>Route</c> table.
		/// </summary>
		/// <returns>A reference to the <see cref="System.Data.DataTable"/> object.</returns>
		public virtual DataTable GetAllAsDataTable()
		{
			return MapRecordsToDataTable(CreateGetAllCommand());
		}

		/// <summary>
		/// Creates and returns an <see cref="System.Data.IDbCommand"/> object that is used
		/// to retrieve all records from the <c>Route</c> table.
		/// </summary>
		/// <returns>A reference to the <see cref="System.Data.IDbCommand"/> object.</returns>
		protected virtual IDbCommand CreateGetAllCommand()
		{
			return CreateGetCommand(null, null);
		}

		/// <summary>
		/// Gets the first <see cref="RouteRow"/> objects that 
		/// match the search condition.
		/// </summary>
		/// <param name="whereSql">The SQL search condition. For example: 
		/// <c>"FirstName='Smith' AND Zip=75038"</c>.</param>
		/// <returns>An instance of <see cref="RouteRow"/> or null reference 
		/// (Nothing in Visual Basic) if the object was not found.</returns>
		public RouteRow GetRow(string whereSql)
		{
			int totalRecordCount = -1;
			RouteRow[] rows = GetAsArray(whereSql, null, 0, 1, ref totalRecordCount);
			return 0 == rows.Length ? null : rows[0];
		}

		/// <summary>
		/// Gets an array of <see cref="RouteRow"/> objects that 
		/// match the search condition, in the the specified sort order.
		/// </summary>
		/// <param name="whereSql">The SQL search condition. For example: 
		/// <c>"FirstName='Smith' AND Zip=75038"</c>.</param>
		/// <param name="orderBySql">The column name(s) followed by "ASC" (ascending) or "DESC" (descending).
		/// Columns are sorted in ascending order by default. For example: <c>"LastName ASC, FirstName ASC"</c>.</param>
		/// <returns>An array of <see cref="RouteRow"/> objects.</returns>
		public RouteRow[] GetAsArray(string whereSql, string orderBySql)
		{
			int totalRecordCount = -1;
			return GetAsArray(whereSql, orderBySql, 0, int.MaxValue, ref totalRecordCount);
		}

		/// <summary>
		/// Gets an array of <see cref="RouteRow"/> objects that 
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
		/// <returns>An array of <see cref="RouteRow"/> objects.</returns>
		public virtual RouteRow[] GetAsArray(string whereSql, string orderBySql,
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
			using(var reader = _db.ExecuteReader(CreateGetCommand(whereSql, orderBySql)))
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
			var sql = "SELECT * FROM [dbo].[Route]";
			if(!string.IsNullOrEmpty(whereSql))
				sql += " WHERE " + whereSql;
			if(!string.IsNullOrEmpty(orderBySql))
				sql += " ORDER BY " + orderBySql;
			return _db.CreateCommand(sql);
		}

		/// <summary>
		/// Gets <see cref="RouteRow"/> by the primary key.
		/// </summary>
		/// <param name="route_id">The <c>route_id</c> column value.</param>
		/// <returns>An instance of <see cref="RouteRow"/> or null reference 
		/// (Nothing in Visual Basic) if the object was not found.</returns>
		public virtual RouteRow GetByPrimaryKey(int route_id)
		{
			string whereSql = "[route_id]=" + _db.CreateSqlParameterName("Route_id");
			IDbCommand cmd = CreateGetCommand(whereSql, null);
			AddParameter(cmd, "Route_id", route_id);
			RouteRow[] tempArray = MapRecords(cmd);
			return 0 == tempArray.Length ? null : tempArray[0];
		}

		/// <summary>
		/// Gets an array of <see cref="RouteRow"/> objects 
		/// by the <c>FK_Route_CallingPlan</c> foreign key.
		/// </summary>
		/// <param name="calling_plan_id">The <c>calling_plan_id</c> column value.</param>
		/// <returns>An array of <see cref="RouteRow"/> objects.</returns>
		public virtual RouteRow[] GetByCalling_plan_id(int calling_plan_id)
		{
			return MapRecords(CreateGetByCalling_plan_idCommand(calling_plan_id));
		}

		/// <summary>
		/// Gets a <see cref="System.Data.DataTable"/> object 
		/// by the <c>FK_Route_CallingPlan</c> foreign key.
		/// </summary>
		/// <param name="calling_plan_id">The <c>calling_plan_id</c> column value.</param>
		/// <returns>A reference to the <see cref="System.Data.DataTable"/> object.</returns>
		public virtual DataTable GetByCalling_plan_idAsDataTable(int calling_plan_id)
		{
			return MapRecordsToDataTable(CreateGetByCalling_plan_idCommand(calling_plan_id));
		}

		/// <summary>
		/// Creates an <see cref="System.Data.IDbCommand"/> object that can be used to 
		/// return records by the <c>FK_Route_CallingPlan</c> foreign key.
		/// </summary>
		/// <param name="calling_plan_id">The <c>calling_plan_id</c> column value.</param>
		/// <returns>A reference to the <see cref="System.Data.IDbCommand"/> object.</returns>
		protected virtual IDbCommand CreateGetByCalling_plan_idCommand(int calling_plan_id)
		{
			string whereSql = "";
			whereSql += "[calling_plan_id]=" + _db.CreateSqlParameterName("Calling_plan_id");

			IDbCommand cmd = CreateGetCommand(whereSql, null);
			AddParameter(cmd, "Calling_plan_id", calling_plan_id);
			return cmd;
		}

		/// <summary>
		/// Gets an array of <see cref="RouteRow"/> objects 
		/// by the <c>FK_Route_Country</c> foreign key.
		/// </summary>
		/// <param name="country_id">The <c>country_id</c> column value.</param>
		/// <returns>An array of <see cref="RouteRow"/> objects.</returns>
		public virtual RouteRow[] GetByCountry_id(int country_id)
		{
			return MapRecords(CreateGetByCountry_idCommand(country_id));
		}

		/// <summary>
		/// Gets a <see cref="System.Data.DataTable"/> object 
		/// by the <c>FK_Route_Country</c> foreign key.
		/// </summary>
		/// <param name="country_id">The <c>country_id</c> column value.</param>
		/// <returns>A reference to the <see cref="System.Data.DataTable"/> object.</returns>
		public virtual DataTable GetByCountry_idAsDataTable(int country_id)
		{
			return MapRecordsToDataTable(CreateGetByCountry_idCommand(country_id));
		}

		/// <summary>
		/// Creates an <see cref="System.Data.IDbCommand"/> object that can be used to 
		/// return records by the <c>FK_Route_Country</c> foreign key.
		/// </summary>
		/// <param name="country_id">The <c>country_id</c> column value.</param>
		/// <returns>A reference to the <see cref="System.Data.IDbCommand"/> object.</returns>
		protected virtual IDbCommand CreateGetByCountry_idCommand(int country_id)
		{
			string whereSql = "";
			whereSql += "[country_id]=" + _db.CreateSqlParameterName("Country_id");

			IDbCommand cmd = CreateGetCommand(whereSql, null);
			AddParameter(cmd, "Country_id", country_id);
			return cmd;
		}

		/// <summary>
		/// Adds a new record into the <c>Route</c> table.
		/// </summary>
		/// <param name="value">The <see cref="RouteRow"/> object to be inserted.</param>
		public virtual void Insert(RouteRow value) {
			var sqlStr = "INSERT INTO [dbo].[Route] (" +
			                "[name], " +
			                "[status], " +
			                "[calling_plan_id], " +
			                "[country_id]" +
			                "[routing_number]" +
			                ") VALUES (" +
			                _db.CreateSqlParameterName("Name") + ", " +
			                _db.CreateSqlParameterName("Status") + ", " +
			                _db.CreateSqlParameterName("Calling_plan_id") + ", " +
			                _db.CreateSqlParameterName("Country_id") + ")" + ", " +
			                _db.CreateSqlParameterName("Routing_number_id");

			IDbCommand cmd = _db.CreateCommand(sqlStr);
			AddParameter(cmd, "Name", value.Name);
			AddParameter(cmd, "Status", value.Status);
			AddParameter(cmd, "Calling_plan_id", value.Calling_plan_id);
			AddParameter(cmd, "Country_id", value.Country_id);
			AddParameter(cmd, "Routing_number", value.Routing_number);
			cmd.ExecuteNonQuery();
		}

		/// <summary>
		/// Updates a record in the <c>Route</c> table.
		/// </summary>
		/// <param name="value">The <see cref="RouteRow"/>
		/// object used to update the table record.</param>
		/// <returns>true if the record was updated; otherwise, false.</returns>
		public virtual bool Update(RouteRow value) {
			var _sqlStr = "UPDATE [dbo].[Route] SET " +
				"[name]=" + _db.CreateSqlParameterName("Name") + ", " +
				"[status]=" + _db.CreateSqlParameterName("Status") + ", " +
				"[calling_plan_id]=" + _db.CreateSqlParameterName("Calling_plan_id") + ", " +
				"[country_id]=" + _db.CreateSqlParameterName("Country_id") + ", " +
				"[routing_number]=" + _db.CreateSqlParameterName("Routing_number") +
				" WHERE " +
				"[route_id]=" + _db.CreateSqlParameterName("Route_id");

			var cmd = _db.CreateCommand(_sqlStr);
			AddParameter(cmd, "Name", value.Name);
			AddParameter(cmd, "Status", value.Status);
			AddParameter(cmd, "Calling_plan_id", value.Calling_plan_id);
			AddParameter(cmd, "Country_id", value.Country_id);
			AddParameter(cmd, "Routing_number", value.Routing_number);
			AddParameter(cmd, "Route_id", value.Route_id);
			return 0 != cmd.ExecuteNonQuery();
		}

		/// <summary>
		/// Updates the <c>Route</c> table and calls the <c>AcceptChanges</c> method
		/// on the changed DataRow objects.
		/// </summary>
		/// <param name="table">The <see cref="System.Data.DataTable"/> used to update the data source.</param>
		public void Update(DataTable table)
		{
			Update(table, true);
		}

		/// <summary>
		/// Updates the <c>Route</c> table. Pass <c>false</c> as the <c>acceptChanges</c> 
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
							DeleteByPrimaryKey((int)row["Route_id"]);
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
		/// Deletes the specified object from the <c>Route</c> table.
		/// </summary>
		/// <param name="value">The <see cref="RouteRow"/> object to delete.</param>
		/// <returns>true if the record was deleted; otherwise, false.</returns>
		public bool Delete(RouteRow value)
		{
			return DeleteByPrimaryKey(value.Route_id);
		}

		/// <summary>
		/// Deletes a record from the <c>Route</c> table using
		/// the specified primary key.
		/// </summary>
		/// <param name="route_id">The <c>route_id</c> column value.</param>
		/// <returns>true if the record was deleted; otherwise, false.</returns>
		public virtual bool DeleteByPrimaryKey(int route_id)
		{
			string whereSql = "[route_id]=" + _db.CreateSqlParameterName("Route_id");
			IDbCommand cmd = CreateDeleteCommand(whereSql);
			AddParameter(cmd, "Route_id", route_id);
			return 0 < cmd.ExecuteNonQuery();
		}

		/// <summary>
		/// Deletes records from the <c>Route</c> table using the 
		/// <c>FK_Route_CallingPlan</c> foreign key.
		/// </summary>
		/// <param name="calling_plan_id">The <c>calling_plan_id</c> column value.</param>
		/// <returns>The number of records deleted from the table.</returns>
		public int DeleteByCalling_plan_id(int calling_plan_id)
		{
			return CreateDeleteByCalling_plan_idCommand(calling_plan_id).ExecuteNonQuery();
		}

		/// <summary>
		/// Creates an <see cref="System.Data.IDbCommand"/> object that can be used to
		/// delete records using the <c>FK_Route_CallingPlan</c> foreign key.
		/// </summary>
		/// <param name="calling_plan_id">The <c>calling_plan_id</c> column value.</param>
		/// <returns>A reference to the <see cref="System.Data.IDbCommand"/> object.</returns>
		protected virtual IDbCommand CreateDeleteByCalling_plan_idCommand(int calling_plan_id)
		{
			string whereSql = "";
			whereSql += "[calling_plan_id]=" + _db.CreateSqlParameterName("Calling_plan_id");

			IDbCommand cmd = CreateDeleteCommand(whereSql);
			AddParameter(cmd, "Calling_plan_id", calling_plan_id);
			return cmd;
		}

		/// <summary>
		/// Deletes records from the <c>Route</c> table using the 
		/// <c>FK_Route_Country</c> foreign key.
		/// </summary>
		/// <param name="country_id">The <c>country_id</c> column value.</param>
		/// <returns>The number of records deleted from the table.</returns>
		public int DeleteByCountry_id(int country_id)
		{
			return CreateDeleteByCountry_idCommand(country_id).ExecuteNonQuery();
		}

		/// <summary>
		/// Creates an <see cref="System.Data.IDbCommand"/> object that can be used to
		/// delete records using the <c>FK_Route_Country</c> foreign key.
		/// </summary>
		/// <param name="country_id">The <c>country_id</c> column value.</param>
		/// <returns>A reference to the <see cref="System.Data.IDbCommand"/> object.</returns>
		protected virtual IDbCommand CreateDeleteByCountry_idCommand(int country_id)
		{
			string whereSql = "";
			whereSql += "[country_id]=" + _db.CreateSqlParameterName("Country_id");

			IDbCommand cmd = CreateDeleteCommand(whereSql);
			AddParameter(cmd, "Country_id", country_id);
			return cmd;
		}

		/// <summary>
		/// Deletes <c>Route</c> records that match the specified criteria.
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
		/// to delete <c>Route</c> records that match the specified criteria.
		/// </summary>
		/// <param name="whereSql">The SQL search condition. 
		/// For example: <c>"FirstName='Smith' AND Zip=75038"</c>.</param>
		/// <returns>A reference to the <see cref="System.Data.IDbCommand"/> object.</returns>
		protected virtual IDbCommand CreateDeleteCommand(string whereSql)
		{
			string sql = "DELETE FROM [dbo].[Route]";
			if(!string.IsNullOrEmpty(whereSql))
				sql += " WHERE " + whereSql;
			return _db.CreateCommand(sql);
		}

		/// <summary>
		/// Deletes all records from the <c>Route</c> table.
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
		/// <returns>An array of <see cref="RouteRow"/> objects.</returns>
		protected RouteRow[] MapRecords(IDbCommand command)
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
		/// <returns>An array of <see cref="RouteRow"/> objects.</returns>
		protected RouteRow[] MapRecords(IDataReader reader)
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
		/// <returns>An array of <see cref="RouteRow"/> objects.</returns>
		protected virtual RouteRow[] MapRecords(IDataReader reader, int startIndex, int length, ref int totalRecordCount) {
			if(0 > startIndex)
				throw new ArgumentOutOfRangeException("startIndex", startIndex, "StartIndex cannot be less than zero.");
			if(0 > length)
				throw new ArgumentOutOfRangeException("length", length, "Length cannot be less than zero.");

			var route_idColumnIndex = reader.GetOrdinal("route_id");
			var nameColumnIndex = reader.GetOrdinal("name");
			var statusColumnIndex = reader.GetOrdinal("status");
			var calling_plan_idColumnIndex = reader.GetOrdinal("calling_plan_id");
			var country_idColumnIndex = reader.GetOrdinal("country_id");
			var routing_numberColumnIndex = reader.GetOrdinal("routing_number");
			var versionColumnIndex = reader.GetOrdinal("version");

			var recordList = new System.Collections.ArrayList();
			var ri = -startIndex;
			while(reader.Read()) {
				ri++;
				if(ri > 0 && ri <= length) {
					var record = new RouteRow();
					recordList.Add(record);

					record.Route_id = Convert.ToInt32(reader.GetValue(route_idColumnIndex));
					record.Name = Convert.ToString(reader.GetValue(nameColumnIndex));
					record.Status = Convert.ToByte(reader.GetValue(statusColumnIndex));
					record.Calling_plan_id = Convert.ToInt32(reader.GetValue(calling_plan_idColumnIndex));
					record.Country_id = Convert.ToInt32(reader.GetValue(country_idColumnIndex));
					var _routing_number = reader.GetValue(routing_numberColumnIndex).ToString();
					record.Routing_number = string.IsNullOrEmpty(_routing_number) ? 0 : Convert.ToInt32(_routing_number);
					record.Version = Convert.ToInt32(reader.GetValue(versionColumnIndex));

					if(ri == length && 0 != totalRecordCount)
						break;
				}
			}

			totalRecordCount = 0 == totalRecordCount ? ri + startIndex : -1;
			return (RouteRow[])(recordList.ToArray(typeof(RouteRow)));
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
		/// Converts <see cref="System.Data.DataRow"/> to <see cref="RouteRow"/>.
		/// </summary>
		/// <param name="row">The <see cref="System.Data.DataRow"/> object to be mapped.</param>
		/// <returns>A reference to the <see cref="RouteRow"/> object.</returns>
		protected virtual RouteRow MapRow(DataRow row)
		{
			RouteRow mappedObject = new RouteRow();
			DataTable dataTable = row.Table;
			DataColumn dataColumn;
			// Column "Route_id"
			dataColumn = dataTable.Columns["Route_id"];
			if(!row.IsNull(dataColumn))
				mappedObject.Route_id = (int)row[dataColumn];
			// Column "Name"
			dataColumn = dataTable.Columns["Name"];
			if(!row.IsNull(dataColumn))
				mappedObject.Name = (string)row[dataColumn];
			// Column "Status"
			dataColumn = dataTable.Columns["Status"];
			if(!row.IsNull(dataColumn))
				mappedObject.Status = (byte)row[dataColumn];
			// Column "Calling_plan_id"
			dataColumn = dataTable.Columns["Calling_plan_id"];
			if(!row.IsNull(dataColumn))
				mappedObject.Calling_plan_id = (int)row[dataColumn];
			// Column "Country_id"
			dataColumn = dataTable.Columns["Country_id"];
			if (!row.IsNull(dataColumn))
				mappedObject.Country_id = (int)row[dataColumn];
			// Column "Routing_number"
			dataColumn = dataTable.Columns["Routing_number"];
			if (!row.IsNull(dataColumn))
				mappedObject.Routing_number = (int)row[dataColumn];
			// Column "Version"
			dataColumn = dataTable.Columns["Version"];
			if(!row.IsNull(dataColumn))
				mappedObject.Version = (int)row[dataColumn];
			return mappedObject;
		}

		/// <summary>
		/// Creates a <see cref="System.Data.DataTable"/> object for 
		/// the <c>Route</c> table.
		/// </summary>
		/// <returns>A reference to the <see cref="System.Data.DataTable"/> object.</returns>
		protected virtual DataTable CreateDataTable()
		{
			DataTable dataTable = new DataTable();
			dataTable.TableName = "Route";
			DataColumn dataColumn;
			dataColumn = dataTable.Columns.Add("Route_id", typeof(int));
			dataColumn.Caption = "route_id";
			dataColumn.AllowDBNull = false;
			dataColumn.ReadOnly = true;
			dataColumn = dataTable.Columns.Add("Name", typeof(string));
			dataColumn.Caption = "name";
			dataColumn.MaxLength = 50;
			dataColumn.AllowDBNull = false;
			dataColumn = dataTable.Columns.Add("Status", typeof(byte));
			dataColumn.Caption = "status";
			dataColumn.AllowDBNull = false;
			dataColumn = dataTable.Columns.Add("Calling_plan_id", typeof(int));
			dataColumn.Caption = "calling_plan_id";
			dataColumn.AllowDBNull = false;
			dataColumn = dataTable.Columns.Add("Country_id", typeof(int));
			dataColumn.Caption = "country_id";
			dataColumn.AllowDBNull = false;
			dataColumn = dataTable.Columns.Add("Routing_number", typeof(int));
			dataColumn.Caption = "routing_number";
			dataColumn.AllowDBNull = true;
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
				case "Route_id":
					parameter = _db.AddParameter(cmd, paramName, DbType.Int32, value);
					break;

				case "Name":
					parameter = _db.AddParameter(cmd, paramName, DbType.AnsiString, value);
					break;

				case "Status":
					parameter = _db.AddParameter(cmd, paramName, DbType.Byte, value);
					break;

				case "Calling_plan_id":
					parameter = _db.AddParameter(cmd, paramName, DbType.Int32, value);
					break;

				case "Country_id":
					parameter = _db.AddParameter(cmd, paramName, DbType.Int32, value);
					break;

				case "Routing_number":
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

	} // End of RouteCollection_Base class
}  // End of namespace

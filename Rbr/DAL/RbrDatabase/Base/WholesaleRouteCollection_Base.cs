// <fileinfo name="Base\WholesaleRouteCollection_Base.cs">
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
	/// The base class for <see cref="WholesaleRouteCollection"/>. Provides methods 
	/// for common database table operations. 
	/// </summary>
	/// <remarks>
	/// Do not change this source code. Update the <see cref="WholesaleRouteCollection"/>
	/// class if you need to add or change some functionality.
	/// </remarks>
	public abstract class WholesaleRouteCollection_Base
	{
		// Constants
		public const string Wholesale_route_idColumnName = "wholesale_route_id";
		public const string Service_idColumnName = "service_id";
		public const string Route_idColumnName = "route_id";
		public const string StatusColumnName = "status";

		// Instance fields
		private Rbr_Db _db;

		/// <summary>
		/// Initializes a new instance of the <see cref="WholesaleRouteCollection_Base"/> 
		/// class with the specified <see cref="Rbr_Db"/>.
		/// </summary>
		/// <param name="db">The <see cref="Rbr_Db"/> object.</param>
		public WholesaleRouteCollection_Base(Rbr_Db db)
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
		/// Gets an array of all records from the <c>WholesaleRoute</c> table.
		/// </summary>
		/// <returns>An array of <see cref="WholesaleRouteRow"/> objects.</returns>
		public virtual WholesaleRouteRow[] GetAll()
		{
			return MapRecords(CreateGetAllCommand());
		}

		/// <summary>
		/// Gets a <see cref="System.Data.DataTable"/> object that 
		/// includes all records from the <c>WholesaleRoute</c> table.
		/// </summary>
		/// <returns>A reference to the <see cref="System.Data.DataTable"/> object.</returns>
		public virtual DataTable GetAllAsDataTable()
		{
			return MapRecordsToDataTable(CreateGetAllCommand());
		}

		/// <summary>
		/// Creates and returns an <see cref="System.Data.IDbCommand"/> object that is used
		/// to retrieve all records from the <c>WholesaleRoute</c> table.
		/// </summary>
		/// <returns>A reference to the <see cref="System.Data.IDbCommand"/> object.</returns>
		protected virtual IDbCommand CreateGetAllCommand()
		{
			return CreateGetCommand(null, null);
		}

		/// <summary>
		/// Gets the first <see cref="WholesaleRouteRow"/> objects that 
		/// match the search condition.
		/// </summary>
		/// <param name="whereSql">The SQL search condition. For example: 
		/// <c>"FirstName='Smith' AND Zip=75038"</c>.</param>
		/// <returns>An instance of <see cref="WholesaleRouteRow"/> or null reference 
		/// (Nothing in Visual Basic) if the object was not found.</returns>
		public WholesaleRouteRow GetRow(string whereSql)
		{
			int totalRecordCount = -1;
			WholesaleRouteRow[] rows = GetAsArray(whereSql, null, 0, 1, ref totalRecordCount);
			return 0 == rows.Length ? null : rows[0];
		}

		/// <summary>
		/// Gets an array of <see cref="WholesaleRouteRow"/> objects that 
		/// match the search condition, in the the specified sort order.
		/// </summary>
		/// <param name="whereSql">The SQL search condition. For example: 
		/// <c>"FirstName='Smith' AND Zip=75038"</c>.</param>
		/// <param name="orderBySql">The column name(s) followed by "ASC" (ascending) or "DESC" (descending).
		/// Columns are sorted in ascending order by default. For example: <c>"LastName ASC, FirstName ASC"</c>.</param>
		/// <returns>An array of <see cref="WholesaleRouteRow"/> objects.</returns>
		public WholesaleRouteRow[] GetAsArray(string whereSql, string orderBySql)
		{
			int totalRecordCount = -1;
			return GetAsArray(whereSql, orderBySql, 0, int.MaxValue, ref totalRecordCount);
		}

		/// <summary>
		/// Gets an array of <see cref="WholesaleRouteRow"/> objects that 
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
		/// <returns>An array of <see cref="WholesaleRouteRow"/> objects.</returns>
		public virtual WholesaleRouteRow[] GetAsArray(string whereSql, string orderBySql,
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
			string sql = "SELECT * FROM [dbo].[WholesaleRoute]";
			if(null != whereSql && 0 < whereSql.Length)
				sql += " WHERE " + whereSql;
			if(null != orderBySql && 0 < orderBySql.Length)
				sql += " ORDER BY " + orderBySql;
			return _db.CreateCommand(sql);
		}

		/// <summary>
		/// Gets <see cref="WholesaleRouteRow"/> by the primary key.
		/// </summary>
		/// <param name="wholesale_route_id">The <c>wholesale_route_id</c> column value.</param>
		/// <returns>An instance of <see cref="WholesaleRouteRow"/> or null reference 
		/// (Nothing in Visual Basic) if the object was not found.</returns>
		public virtual WholesaleRouteRow GetByPrimaryKey(int wholesale_route_id)
		{
			string whereSql = "[wholesale_route_id]=" + _db.CreateSqlParameterName("Wholesale_route_id");
			IDbCommand cmd = CreateGetCommand(whereSql, null);
			AddParameter(cmd, "Wholesale_route_id", wholesale_route_id);
			WholesaleRouteRow[] tempArray = MapRecords(cmd);
			return 0 == tempArray.Length ? null : tempArray[0];
		}

		/// <summary>
		/// Gets an array of <see cref="WholesaleRouteRow"/> objects 
		/// by the <c>FK_ServiceRoute_Route</c> foreign key.
		/// </summary>
		/// <param name="route_id">The <c>route_id</c> column value.</param>
		/// <returns>An array of <see cref="WholesaleRouteRow"/> objects.</returns>
		public WholesaleRouteRow[] GetByRoute_id(int route_id)
		{
			return GetByRoute_id(route_id, false);
		}

		/// <summary>
		/// Gets an array of <see cref="WholesaleRouteRow"/> objects 
		/// by the <c>FK_ServiceRoute_Route</c> foreign key.
		/// </summary>
		/// <param name="route_id">The <c>route_id</c> column value.</param>
		/// <param name="route_idNull">true if the method ignores the route_id
		/// parameter value and uses DbNull instead of it; otherwise, false.</param>
		/// <returns>An array of <see cref="WholesaleRouteRow"/> objects.</returns>
		public virtual WholesaleRouteRow[] GetByRoute_id(int route_id, bool route_idNull)
		{
			return MapRecords(CreateGetByRoute_idCommand(route_id, route_idNull));
		}

		/// <summary>
		/// Gets a <see cref="System.Data.DataTable"/> object 
		/// by the <c>FK_ServiceRoute_Route</c> foreign key.
		/// </summary>
		/// <param name="route_id">The <c>route_id</c> column value.</param>
		/// <returns>A reference to the <see cref="System.Data.DataTable"/> object.</returns>
		public DataTable GetByRoute_idAsDataTable(int route_id)
		{
			return GetByRoute_idAsDataTable(route_id, false);
		}

		/// <summary>
		/// Gets a <see cref="System.Data.DataTable"/> object 
		/// by the <c>FK_ServiceRoute_Route</c> foreign key.
		/// </summary>
		/// <param name="route_id">The <c>route_id</c> column value.</param>
		/// <param name="route_idNull">true if the method ignores the route_id
		/// parameter value and uses DbNull instead of it; otherwise, false.</param>
		/// <returns>A reference to the <see cref="System.Data.DataTable"/> object.</returns>
		public virtual DataTable GetByRoute_idAsDataTable(int route_id, bool route_idNull)
		{
			return MapRecordsToDataTable(CreateGetByRoute_idCommand(route_id, route_idNull));
		}

		/// <summary>
		/// Creates an <see cref="System.Data.IDbCommand"/> object that can be used to 
		/// return records by the <c>FK_ServiceRoute_Route</c> foreign key.
		/// </summary>
		/// <param name="route_id">The <c>route_id</c> column value.</param>
		/// <param name="route_idNull">true if the method ignores the route_id
		/// parameter value and uses DbNull instead of it; otherwise, false.</param>
		/// <returns>A reference to the <see cref="System.Data.IDbCommand"/> object.</returns>
		protected virtual IDbCommand CreateGetByRoute_idCommand(int route_id, bool route_idNull)
		{
			string whereSql = "";
			if(route_idNull)
				whereSql += "[route_id] IS NULL";
			else
				whereSql += "[route_id]=" + _db.CreateSqlParameterName("Route_id");

			IDbCommand cmd = CreateGetCommand(whereSql, null);
			if(!route_idNull)
				AddParameter(cmd, "Route_id", route_id);
			return cmd;
		}

		/// <summary>
		/// Gets an array of <see cref="WholesaleRouteRow"/> objects 
		/// by the <c>R_328</c> foreign key.
		/// </summary>
		/// <param name="service_id">The <c>service_id</c> column value.</param>
		/// <returns>An array of <see cref="WholesaleRouteRow"/> objects.</returns>
		public virtual WholesaleRouteRow[] GetByService_id(short service_id)
		{
			return MapRecords(CreateGetByService_idCommand(service_id));
		}

		/// <summary>
		/// Gets a <see cref="System.Data.DataTable"/> object 
		/// by the <c>R_328</c> foreign key.
		/// </summary>
		/// <param name="service_id">The <c>service_id</c> column value.</param>
		/// <returns>A reference to the <see cref="System.Data.DataTable"/> object.</returns>
		public virtual DataTable GetByService_idAsDataTable(short service_id)
		{
			return MapRecordsToDataTable(CreateGetByService_idCommand(service_id));
		}

		/// <summary>
		/// Creates an <see cref="System.Data.IDbCommand"/> object that can be used to 
		/// return records by the <c>R_328</c> foreign key.
		/// </summary>
		/// <param name="service_id">The <c>service_id</c> column value.</param>
		/// <returns>A reference to the <see cref="System.Data.IDbCommand"/> object.</returns>
		protected virtual IDbCommand CreateGetByService_idCommand(short service_id)
		{
			string whereSql = "";
			whereSql += "[service_id]=" + _db.CreateSqlParameterName("Service_id");

			IDbCommand cmd = CreateGetCommand(whereSql, null);
			AddParameter(cmd, "Service_id", service_id);
			return cmd;
		}

		/// <summary>
		/// Adds a new record into the <c>WholesaleRoute</c> table.
		/// </summary>
		/// <param name="value">The <see cref="WholesaleRouteRow"/> object to be inserted.</param>
		public virtual void Insert(WholesaleRouteRow value)
		{
			string sqlStr = "INSERT INTO [dbo].[WholesaleRoute] (" +
				"[wholesale_route_id], " +
				"[service_id], " +
				"[route_id], " +
				"[status]" +
				") VALUES (" +
				_db.CreateSqlParameterName("Wholesale_route_id") + ", " +
				_db.CreateSqlParameterName("Service_id") + ", " +
				_db.CreateSqlParameterName("Route_id") + ", " +
				_db.CreateSqlParameterName("Status") + ")";
			IDbCommand cmd = _db.CreateCommand(sqlStr);
			AddParameter(cmd, "Wholesale_route_id", value.Wholesale_route_id);
			AddParameter(cmd, "Service_id", value.Service_id);
			AddParameter(cmd, "Route_id",
				value.IsRoute_idNull ? DBNull.Value : (object)value.Route_id);
			AddParameter(cmd, "Status", value.Status);
			cmd.ExecuteNonQuery();
		}

		/// <summary>
		/// Updates a record in the <c>WholesaleRoute</c> table.
		/// </summary>
		/// <param name="value">The <see cref="WholesaleRouteRow"/>
		/// object used to update the table record.</param>
		/// <returns>true if the record was updated; otherwise, false.</returns>
		public virtual bool Update(WholesaleRouteRow value)
		{
			string sqlStr = "UPDATE [dbo].[WholesaleRoute] SET " +
				"[service_id]=" + _db.CreateSqlParameterName("Service_id") + ", " +
				"[route_id]=" + _db.CreateSqlParameterName("Route_id") + ", " +
				"[status]=" + _db.CreateSqlParameterName("Status") +
				" WHERE " +
				"[wholesale_route_id]=" + _db.CreateSqlParameterName("Wholesale_route_id");
			IDbCommand cmd = _db.CreateCommand(sqlStr);
			AddParameter(cmd, "Service_id", value.Service_id);
			AddParameter(cmd, "Route_id",
				value.IsRoute_idNull ? DBNull.Value : (object)value.Route_id);
			AddParameter(cmd, "Status", value.Status);
			AddParameter(cmd, "Wholesale_route_id", value.Wholesale_route_id);
			return 0 != cmd.ExecuteNonQuery();
		}

		/// <summary>
		/// Updates the <c>WholesaleRoute</c> table and calls the <c>AcceptChanges</c> method
		/// on the changed DataRow objects.
		/// </summary>
		/// <param name="table">The <see cref="System.Data.DataTable"/> used to update the data source.</param>
		public void Update(DataTable table)
		{
			Update(table, true);
		}

		/// <summary>
		/// Updates the <c>WholesaleRoute</c> table. Pass <c>false</c> as the <c>acceptChanges</c> 
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
							DeleteByPrimaryKey((int)row["Wholesale_route_id"]);
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
		/// Deletes the specified object from the <c>WholesaleRoute</c> table.
		/// </summary>
		/// <param name="value">The <see cref="WholesaleRouteRow"/> object to delete.</param>
		/// <returns>true if the record was deleted; otherwise, false.</returns>
		public bool Delete(WholesaleRouteRow value)
		{
			return DeleteByPrimaryKey(value.Wholesale_route_id);
		}

		/// <summary>
		/// Deletes a record from the <c>WholesaleRoute</c> table using
		/// the specified primary key.
		/// </summary>
		/// <param name="wholesale_route_id">The <c>wholesale_route_id</c> column value.</param>
		/// <returns>true if the record was deleted; otherwise, false.</returns>
		public virtual bool DeleteByPrimaryKey(int wholesale_route_id)
		{
			string whereSql = "[wholesale_route_id]=" + _db.CreateSqlParameterName("Wholesale_route_id");
			IDbCommand cmd = CreateDeleteCommand(whereSql);
			AddParameter(cmd, "Wholesale_route_id", wholesale_route_id);
			return 0 < cmd.ExecuteNonQuery();
		}

		/// <summary>
		/// Deletes records from the <c>WholesaleRoute</c> table using the 
		/// <c>FK_ServiceRoute_Route</c> foreign key.
		/// </summary>
		/// <param name="route_id">The <c>route_id</c> column value.</param>
		/// <returns>The number of records deleted from the table.</returns>
		public int DeleteByRoute_id(int route_id)
		{
			return DeleteByRoute_id(route_id, false);
		}

		/// <summary>
		/// Deletes records from the <c>WholesaleRoute</c> table using the 
		/// <c>FK_ServiceRoute_Route</c> foreign key.
		/// </summary>
		/// <param name="route_id">The <c>route_id</c> column value.</param>
		/// <param name="route_idNull">true if the method ignores the route_id
		/// parameter value and uses DbNull instead of it; otherwise, false.</param>
		/// <returns>The number of records deleted from the table.</returns>
		public int DeleteByRoute_id(int route_id, bool route_idNull)
		{
			return CreateDeleteByRoute_idCommand(route_id, route_idNull).ExecuteNonQuery();
		}

		/// <summary>
		/// Creates an <see cref="System.Data.IDbCommand"/> object that can be used to
		/// delete records using the <c>FK_ServiceRoute_Route</c> foreign key.
		/// </summary>
		/// <param name="route_id">The <c>route_id</c> column value.</param>
		/// <param name="route_idNull">true if the method ignores the route_id
		/// parameter value and uses DbNull instead of it; otherwise, false.</param>
		/// <returns>A reference to the <see cref="System.Data.IDbCommand"/> object.</returns>
		protected virtual IDbCommand CreateDeleteByRoute_idCommand(int route_id, bool route_idNull)
		{
			string whereSql = "";
			if(route_idNull)
				whereSql += "[route_id] IS NULL";
			else
				whereSql += "[route_id]=" + _db.CreateSqlParameterName("Route_id");

			IDbCommand cmd = CreateDeleteCommand(whereSql);
			if(!route_idNull)
				AddParameter(cmd, "Route_id", route_id);
			return cmd;
		}

		/// <summary>
		/// Deletes records from the <c>WholesaleRoute</c> table using the 
		/// <c>R_328</c> foreign key.
		/// </summary>
		/// <param name="service_id">The <c>service_id</c> column value.</param>
		/// <returns>The number of records deleted from the table.</returns>
		public int DeleteByService_id(short service_id)
		{
			return CreateDeleteByService_idCommand(service_id).ExecuteNonQuery();
		}

		/// <summary>
		/// Creates an <see cref="System.Data.IDbCommand"/> object that can be used to
		/// delete records using the <c>R_328</c> foreign key.
		/// </summary>
		/// <param name="service_id">The <c>service_id</c> column value.</param>
		/// <returns>A reference to the <see cref="System.Data.IDbCommand"/> object.</returns>
		protected virtual IDbCommand CreateDeleteByService_idCommand(short service_id)
		{
			string whereSql = "";
			whereSql += "[service_id]=" + _db.CreateSqlParameterName("Service_id");

			IDbCommand cmd = CreateDeleteCommand(whereSql);
			AddParameter(cmd, "Service_id", service_id);
			return cmd;
		}

		/// <summary>
		/// Deletes <c>WholesaleRoute</c> records that match the specified criteria.
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
		/// to delete <c>WholesaleRoute</c> records that match the specified criteria.
		/// </summary>
		/// <param name="whereSql">The SQL search condition. 
		/// For example: <c>"FirstName='Smith' AND Zip=75038"</c>.</param>
		/// <returns>A reference to the <see cref="System.Data.IDbCommand"/> object.</returns>
		protected virtual IDbCommand CreateDeleteCommand(string whereSql)
		{
			string sql = "DELETE FROM [dbo].[WholesaleRoute]";
			if(null != whereSql && 0 < whereSql.Length)
				sql += " WHERE " + whereSql;
			return _db.CreateCommand(sql);
		}

		/// <summary>
		/// Deletes all records from the <c>WholesaleRoute</c> table.
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
		/// <returns>An array of <see cref="WholesaleRouteRow"/> objects.</returns>
		protected WholesaleRouteRow[] MapRecords(IDbCommand command)
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
		/// <returns>An array of <see cref="WholesaleRouteRow"/> objects.</returns>
		protected WholesaleRouteRow[] MapRecords(IDataReader reader)
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
		/// <returns>An array of <see cref="WholesaleRouteRow"/> objects.</returns>
		protected virtual WholesaleRouteRow[] MapRecords(IDataReader reader, 
										int startIndex, int length, ref int totalRecordCount)
		{
			if(0 > startIndex)
				throw new ArgumentOutOfRangeException("startIndex", startIndex, "StartIndex cannot be less than zero.");
			if(0 > length)
				throw new ArgumentOutOfRangeException("length", length, "Length cannot be less than zero.");

			int wholesale_route_idColumnIndex = reader.GetOrdinal("wholesale_route_id");
			int service_idColumnIndex = reader.GetOrdinal("service_id");
			int route_idColumnIndex = reader.GetOrdinal("route_id");
			int statusColumnIndex = reader.GetOrdinal("status");

			System.Collections.ArrayList recordList = new System.Collections.ArrayList();
			int ri = -startIndex;
			while(reader.Read())
			{
				ri++;
				if(ri > 0 && ri <= length)
				{
					WholesaleRouteRow record = new WholesaleRouteRow();
					recordList.Add(record);

					record.Wholesale_route_id = Convert.ToInt32(reader.GetValue(wholesale_route_idColumnIndex));
					record.Service_id = Convert.ToInt16(reader.GetValue(service_idColumnIndex));
					if(!reader.IsDBNull(route_idColumnIndex))
						record.Route_id = Convert.ToInt32(reader.GetValue(route_idColumnIndex));
					record.Status = Convert.ToByte(reader.GetValue(statusColumnIndex));

					if(ri == length && 0 != totalRecordCount)
						break;
				}
			}

			totalRecordCount = 0 == totalRecordCount ? ri + startIndex : -1;
			return (WholesaleRouteRow[])(recordList.ToArray(typeof(WholesaleRouteRow)));
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
		/// Converts <see cref="System.Data.DataRow"/> to <see cref="WholesaleRouteRow"/>.
		/// </summary>
		/// <param name="row">The <see cref="System.Data.DataRow"/> object to be mapped.</param>
		/// <returns>A reference to the <see cref="WholesaleRouteRow"/> object.</returns>
		protected virtual WholesaleRouteRow MapRow(DataRow row)
		{
			WholesaleRouteRow mappedObject = new WholesaleRouteRow();
			DataTable dataTable = row.Table;
			DataColumn dataColumn;
			// Column "Wholesale_route_id"
			dataColumn = dataTable.Columns["Wholesale_route_id"];
			if(!row.IsNull(dataColumn))
				mappedObject.Wholesale_route_id = (int)row[dataColumn];
			// Column "Service_id"
			dataColumn = dataTable.Columns["Service_id"];
			if(!row.IsNull(dataColumn))
				mappedObject.Service_id = (short)row[dataColumn];
			// Column "Route_id"
			dataColumn = dataTable.Columns["Route_id"];
			if(!row.IsNull(dataColumn))
				mappedObject.Route_id = (int)row[dataColumn];
			// Column "Status"
			dataColumn = dataTable.Columns["Status"];
			if(!row.IsNull(dataColumn))
				mappedObject.Status = (byte)row[dataColumn];
			return mappedObject;
		}

		/// <summary>
		/// Creates a <see cref="System.Data.DataTable"/> object for 
		/// the <c>WholesaleRoute</c> table.
		/// </summary>
		/// <returns>A reference to the <see cref="System.Data.DataTable"/> object.</returns>
		protected virtual DataTable CreateDataTable()
		{
			DataTable dataTable = new DataTable();
			dataTable.TableName = "WholesaleRoute";
			DataColumn dataColumn;
			dataColumn = dataTable.Columns.Add("Wholesale_route_id", typeof(int));
			dataColumn.Caption = "wholesale_route_id";
			dataColumn.AllowDBNull = false;
			dataColumn = dataTable.Columns.Add("Service_id", typeof(short));
			dataColumn.Caption = "service_id";
			dataColumn.AllowDBNull = false;
			dataColumn = dataTable.Columns.Add("Route_id", typeof(int));
			dataColumn.Caption = "route_id";
			dataColumn = dataTable.Columns.Add("Status", typeof(byte));
			dataColumn.Caption = "status";
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

				case "Service_id":
					parameter = _db.AddParameter(cmd, paramName, DbType.Int16, value);
					break;

				case "Route_id":
					parameter = _db.AddParameter(cmd, paramName, DbType.Int32, value);
					break;

				case "Status":
					parameter = _db.AddParameter(cmd, paramName, DbType.Byte, value);
					break;

				default:
					throw new ArgumentException("Unknown parameter name (" + paramName + ").");
			}
			return parameter;
		}

	} // End of WholesaleRouteCollection_Base class
}  // End of namespace

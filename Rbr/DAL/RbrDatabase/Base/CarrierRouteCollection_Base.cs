// <fileinfo name="Base\CarrierRouteCollection_Base.cs">
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
	/// The base class for <see cref="CarrierRouteCollection"/>. Provides methods 
	/// for common database table operations. 
	/// </summary>
	/// <remarks>
	/// Do not change this source code. Update the <see cref="CarrierRouteCollection"/>
	/// class if you need to add or change some functionality.
	/// </remarks>
	public abstract class CarrierRouteCollection_Base
	{
		// Constants
		public const string Carrier_route_idColumnName = "carrier_route_id";
		public const string Carrier_acct_idColumnName = "carrier_acct_id";
		public const string Route_idColumnName = "route_id";
		public const string StatusColumnName = "status";
		public const string Asr_time_windowColumnName = "asr_time_window";
		public const string Asr_targetColumnName = "asr_target";
		public const string Acd_time_windowColumnName = "acd_time_window";
		public const string Acd_targetColumnName = "acd_target";
		public const string Next_epColumnName = "next_ep";

		// Instance fields
		private Rbr_Db _db;

		/// <summary>
		/// Initializes a new instance of the <see cref="CarrierRouteCollection_Base"/> 
		/// class with the specified <see cref="Rbr_Db"/>.
		/// </summary>
		/// <param name="db">The <see cref="Rbr_Db"/> object.</param>
		public CarrierRouteCollection_Base(Rbr_Db db)
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
		/// Gets an array of all records from the <c>CarrierRoute</c> table.
		/// </summary>
		/// <returns>An array of <see cref="CarrierRouteRow"/> objects.</returns>
		public virtual CarrierRouteRow[] GetAll()
		{
			return MapRecords(CreateGetAllCommand());
		}

		/// <summary>
		/// Gets a <see cref="System.Data.DataTable"/> object that 
		/// includes all records from the <c>CarrierRoute</c> table.
		/// </summary>
		/// <returns>A reference to the <see cref="System.Data.DataTable"/> object.</returns>
		public virtual DataTable GetAllAsDataTable()
		{
			return MapRecordsToDataTable(CreateGetAllCommand());
		}

		/// <summary>
		/// Creates and returns an <see cref="System.Data.IDbCommand"/> object that is used
		/// to retrieve all records from the <c>CarrierRoute</c> table.
		/// </summary>
		/// <returns>A reference to the <see cref="System.Data.IDbCommand"/> object.</returns>
		protected virtual IDbCommand CreateGetAllCommand()
		{
			return CreateGetCommand(null, null);
		}

		/// <summary>
		/// Gets the first <see cref="CarrierRouteRow"/> objects that 
		/// match the search condition.
		/// </summary>
		/// <param name="whereSql">The SQL search condition. For example: 
		/// <c>"FirstName='Smith' AND Zip=75038"</c>.</param>
		/// <returns>An instance of <see cref="CarrierRouteRow"/> or null reference 
		/// (Nothing in Visual Basic) if the object was not found.</returns>
		public CarrierRouteRow GetRow(string whereSql)
		{
			int totalRecordCount = -1;
			CarrierRouteRow[] rows = GetAsArray(whereSql, null, 0, 1, ref totalRecordCount);
			return 0 == rows.Length ? null : rows[0];
		}

		/// <summary>
		/// Gets an array of <see cref="CarrierRouteRow"/> objects that 
		/// match the search condition, in the the specified sort order.
		/// </summary>
		/// <param name="whereSql">The SQL search condition. For example: 
		/// <c>"FirstName='Smith' AND Zip=75038"</c>.</param>
		/// <param name="orderBySql">The column name(s) followed by "ASC" (ascending) or "DESC" (descending).
		/// Columns are sorted in ascending order by default. For example: <c>"LastName ASC, FirstName ASC"</c>.</param>
		/// <returns>An array of <see cref="CarrierRouteRow"/> objects.</returns>
		public CarrierRouteRow[] GetAsArray(string whereSql, string orderBySql)
		{
			int totalRecordCount = -1;
			return GetAsArray(whereSql, orderBySql, 0, int.MaxValue, ref totalRecordCount);
		}

		/// <summary>
		/// Gets an array of <see cref="CarrierRouteRow"/> objects that 
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
		/// <returns>An array of <see cref="CarrierRouteRow"/> objects.</returns>
		public virtual CarrierRouteRow[] GetAsArray(string whereSql, string orderBySql,
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
			string sql = "SELECT * FROM [dbo].[CarrierRoute]";
			if(null != whereSql && 0 < whereSql.Length)
				sql += " WHERE " + whereSql;
			if(null != orderBySql && 0 < orderBySql.Length)
				sql += " ORDER BY " + orderBySql;
			return _db.CreateCommand(sql);
		}

		/// <summary>
		/// Gets <see cref="CarrierRouteRow"/> by the primary key.
		/// </summary>
		/// <param name="carrier_route_id">The <c>carrier_route_id</c> column value.</param>
		/// <returns>An instance of <see cref="CarrierRouteRow"/> or null reference 
		/// (Nothing in Visual Basic) if the object was not found.</returns>
		public virtual CarrierRouteRow GetByPrimaryKey(int carrier_route_id)
		{
			string whereSql = "[carrier_route_id]=" + _db.CreateSqlParameterName("Carrier_route_id");
			IDbCommand cmd = CreateGetCommand(whereSql, null);
			AddParameter(cmd, "Carrier_route_id", carrier_route_id);
			CarrierRouteRow[] tempArray = MapRecords(cmd);
			return 0 == tempArray.Length ? null : tempArray[0];
		}

		/// <summary>
		/// Gets an array of <see cref="CarrierRouteRow"/> objects 
		/// by the <c>R_329</c> foreign key.
		/// </summary>
		/// <param name="route_id">The <c>route_id</c> column value.</param>
		/// <returns>An array of <see cref="CarrierRouteRow"/> objects.</returns>
		public CarrierRouteRow[] GetByRoute_id(int route_id)
		{
			return GetByRoute_id(route_id, false);
		}

		/// <summary>
		/// Gets an array of <see cref="CarrierRouteRow"/> objects 
		/// by the <c>R_329</c> foreign key.
		/// </summary>
		/// <param name="route_id">The <c>route_id</c> column value.</param>
		/// <param name="route_idNull">true if the method ignores the route_id
		/// parameter value and uses DbNull instead of it; otherwise, false.</param>
		/// <returns>An array of <see cref="CarrierRouteRow"/> objects.</returns>
		public virtual CarrierRouteRow[] GetByRoute_id(int route_id, bool route_idNull)
		{
			return MapRecords(CreateGetByRoute_idCommand(route_id, route_idNull));
		}

		/// <summary>
		/// Gets a <see cref="System.Data.DataTable"/> object 
		/// by the <c>R_329</c> foreign key.
		/// </summary>
		/// <param name="route_id">The <c>route_id</c> column value.</param>
		/// <returns>A reference to the <see cref="System.Data.DataTable"/> object.</returns>
		public DataTable GetByRoute_idAsDataTable(int route_id)
		{
			return GetByRoute_idAsDataTable(route_id, false);
		}

		/// <summary>
		/// Gets a <see cref="System.Data.DataTable"/> object 
		/// by the <c>R_329</c> foreign key.
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
		/// return records by the <c>R_329</c> foreign key.
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
		/// Gets an array of <see cref="CarrierRouteRow"/> objects 
		/// by the <c>R_333</c> foreign key.
		/// </summary>
		/// <param name="carrier_acct_id">The <c>carrier_acct_id</c> column value.</param>
		/// <returns>An array of <see cref="CarrierRouteRow"/> objects.</returns>
		public virtual CarrierRouteRow[] GetByCarrier_acct_id(short carrier_acct_id)
		{
			return MapRecords(CreateGetByCarrier_acct_idCommand(carrier_acct_id));
		}

		/// <summary>
		/// Gets a <see cref="System.Data.DataTable"/> object 
		/// by the <c>R_333</c> foreign key.
		/// </summary>
		/// <param name="carrier_acct_id">The <c>carrier_acct_id</c> column value.</param>
		/// <returns>A reference to the <see cref="System.Data.DataTable"/> object.</returns>
		public virtual DataTable GetByCarrier_acct_idAsDataTable(short carrier_acct_id)
		{
			return MapRecordsToDataTable(CreateGetByCarrier_acct_idCommand(carrier_acct_id));
		}

		/// <summary>
		/// Creates an <see cref="System.Data.IDbCommand"/> object that can be used to 
		/// return records by the <c>R_333</c> foreign key.
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
		/// Adds a new record into the <c>CarrierRoute</c> table.
		/// </summary>
		/// <param name="value">The <see cref="CarrierRouteRow"/> object to be inserted.</param>
		public virtual void Insert(CarrierRouteRow value)
		{
			string sqlStr = "INSERT INTO [dbo].[CarrierRoute] (" +
				"[carrier_route_id], " +
				"[carrier_acct_id], " +
				"[route_id], " +
				"[status], " +
				"[asr_time_window], " +
				"[asr_target], " +
				"[acd_time_window], " +
				"[acd_target]" +
				") VALUES (" +
				_db.CreateSqlParameterName("Carrier_route_id") + ", " +
				_db.CreateSqlParameterName("Carrier_acct_id") + ", " +
				_db.CreateSqlParameterName("Route_id") + ", " +
				_db.CreateSqlParameterName("Status") + ", " +
				_db.CreateSqlParameterName("Asr_time_window") + ", " +
				_db.CreateSqlParameterName("Asr_target") + ", " +
				_db.CreateSqlParameterName("Acd_time_window") + ", " +
				_db.CreateSqlParameterName("Acd_target") + ")";
			IDbCommand cmd = _db.CreateCommand(sqlStr);
			AddParameter(cmd, "Carrier_route_id", value.Carrier_route_id);
			AddParameter(cmd, "Carrier_acct_id", value.Carrier_acct_id);
			AddParameter(cmd, "Route_id",
				value.IsRoute_idNull ? DBNull.Value : (object)value.Route_id);
			AddParameter(cmd, "Status", value.Status);
			AddParameter(cmd, "Asr_time_window", value.Asr_time_window);
			AddParameter(cmd, "Asr_target", value.Asr_target);
			AddParameter(cmd, "Acd_time_window", value.Acd_time_window);
			AddParameter(cmd, "Acd_target", value.Acd_target);
			cmd.ExecuteNonQuery();
		}

		/// <summary>
		/// Updates a record in the <c>CarrierRoute</c> table.
		/// </summary>
		/// <param name="value">The <see cref="CarrierRouteRow"/>
		/// object used to update the table record.</param>
		/// <returns>true if the record was updated; otherwise, false.</returns>
		public virtual bool Update(CarrierRouteRow value)
		{
			string sqlStr = "UPDATE [dbo].[CarrierRoute] SET " +
				"[carrier_acct_id]=" + _db.CreateSqlParameterName("Carrier_acct_id") + ", " +
				"[route_id]=" + _db.CreateSqlParameterName("Route_id") + ", " +
				"[status]=" + _db.CreateSqlParameterName("Status") + ", " +
				"[asr_time_window]=" + _db.CreateSqlParameterName("Asr_time_window") + ", " +
				"[asr_target]=" + _db.CreateSqlParameterName("Asr_target") + ", " +
				"[acd_time_window]=" + _db.CreateSqlParameterName("Acd_time_window") + ", " +
				"[acd_target]=" + _db.CreateSqlParameterName("Acd_target") +
				" WHERE " +
				"[carrier_route_id]=" + _db.CreateSqlParameterName("Carrier_route_id");
			IDbCommand cmd = _db.CreateCommand(sqlStr);
			AddParameter(cmd, "Carrier_acct_id", value.Carrier_acct_id);
			AddParameter(cmd, "Route_id",
				value.IsRoute_idNull ? DBNull.Value : (object)value.Route_id);
			AddParameter(cmd, "Status", value.Status);
			AddParameter(cmd, "Asr_time_window", value.Asr_time_window);
			AddParameter(cmd, "Asr_target", value.Asr_target);
			AddParameter(cmd, "Acd_time_window", value.Acd_time_window);
			AddParameter(cmd, "Acd_target", value.Acd_target);
			AddParameter(cmd, "Carrier_route_id", value.Carrier_route_id);
			return 0 != cmd.ExecuteNonQuery();
		}

		/// <summary>
		/// Updates the <c>CarrierRoute</c> table and calls the <c>AcceptChanges</c> method
		/// on the changed DataRow objects.
		/// </summary>
		/// <param name="table">The <see cref="System.Data.DataTable"/> used to update the data source.</param>
		public void Update(DataTable table)
		{
			Update(table, true);
		}

		/// <summary>
		/// Updates the <c>CarrierRoute</c> table. Pass <c>false</c> as the <c>acceptChanges</c> 
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
							DeleteByPrimaryKey((int)row["Carrier_route_id"]);
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
		/// Deletes the specified object from the <c>CarrierRoute</c> table.
		/// </summary>
		/// <param name="value">The <see cref="CarrierRouteRow"/> object to delete.</param>
		/// <returns>true if the record was deleted; otherwise, false.</returns>
		public bool Delete(CarrierRouteRow value)
		{
			return DeleteByPrimaryKey(value.Carrier_route_id);
		}

		/// <summary>
		/// Deletes a record from the <c>CarrierRoute</c> table using
		/// the specified primary key.
		/// </summary>
		/// <param name="carrier_route_id">The <c>carrier_route_id</c> column value.</param>
		/// <returns>true if the record was deleted; otherwise, false.</returns>
		public virtual bool DeleteByPrimaryKey(int carrier_route_id)
		{
			string whereSql = "[carrier_route_id]=" + _db.CreateSqlParameterName("Carrier_route_id");
			IDbCommand cmd = CreateDeleteCommand(whereSql);
			AddParameter(cmd, "Carrier_route_id", carrier_route_id);
			return 0 < cmd.ExecuteNonQuery();
		}

		/// <summary>
		/// Deletes records from the <c>CarrierRoute</c> table using the 
		/// <c>R_329</c> foreign key.
		/// </summary>
		/// <param name="route_id">The <c>route_id</c> column value.</param>
		/// <returns>The number of records deleted from the table.</returns>
		public int DeleteByRoute_id(int route_id)
		{
			return DeleteByRoute_id(route_id, false);
		}

		/// <summary>
		/// Deletes records from the <c>CarrierRoute</c> table using the 
		/// <c>R_329</c> foreign key.
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
		/// delete records using the <c>R_329</c> foreign key.
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
		/// Deletes records from the <c>CarrierRoute</c> table using the 
		/// <c>R_333</c> foreign key.
		/// </summary>
		/// <param name="carrier_acct_id">The <c>carrier_acct_id</c> column value.</param>
		/// <returns>The number of records deleted from the table.</returns>
		public int DeleteByCarrier_acct_id(short carrier_acct_id)
		{
			return CreateDeleteByCarrier_acct_idCommand(carrier_acct_id).ExecuteNonQuery();
		}

		/// <summary>
		/// Creates an <see cref="System.Data.IDbCommand"/> object that can be used to
		/// delete records using the <c>R_333</c> foreign key.
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
		/// Deletes <c>CarrierRoute</c> records that match the specified criteria.
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
		/// to delete <c>CarrierRoute</c> records that match the specified criteria.
		/// </summary>
		/// <param name="whereSql">The SQL search condition. 
		/// For example: <c>"FirstName='Smith' AND Zip=75038"</c>.</param>
		/// <returns>A reference to the <see cref="System.Data.IDbCommand"/> object.</returns>
		protected virtual IDbCommand CreateDeleteCommand(string whereSql)
		{
			string sql = "DELETE FROM [dbo].[CarrierRoute]";
			if(null != whereSql && 0 < whereSql.Length)
				sql += " WHERE " + whereSql;
			return _db.CreateCommand(sql);
		}

		/// <summary>
		/// Deletes all records from the <c>CarrierRoute</c> table.
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
		/// <returns>An array of <see cref="CarrierRouteRow"/> objects.</returns>
		protected CarrierRouteRow[] MapRecords(IDbCommand command)
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
		/// <returns>An array of <see cref="CarrierRouteRow"/> objects.</returns>
		protected CarrierRouteRow[] MapRecords(IDataReader reader)
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
		/// <returns>An array of <see cref="CarrierRouteRow"/> objects.</returns>
		protected virtual CarrierRouteRow[] MapRecords(IDataReader reader, 
										int startIndex, int length, ref int totalRecordCount)
		{
			if(0 > startIndex)
				throw new ArgumentOutOfRangeException("startIndex", startIndex, "StartIndex cannot be less than zero.");
			if(0 > length)
				throw new ArgumentOutOfRangeException("length", length, "Length cannot be less than zero.");

			int carrier_route_idColumnIndex = reader.GetOrdinal("carrier_route_id");
			int carrier_acct_idColumnIndex = reader.GetOrdinal("carrier_acct_id");
			int route_idColumnIndex = reader.GetOrdinal("route_id");
			int statusColumnIndex = reader.GetOrdinal("status");
			int asr_time_windowColumnIndex = reader.GetOrdinal("asr_time_window");
			int asr_targetColumnIndex = reader.GetOrdinal("asr_target");
			int acd_time_windowColumnIndex = reader.GetOrdinal("acd_time_window");
			int acd_targetColumnIndex = reader.GetOrdinal("acd_target");
			int next_epColumnIndex = reader.GetOrdinal("next_ep");

			System.Collections.ArrayList recordList = new System.Collections.ArrayList();
			int ri = -startIndex;
			while(reader.Read())
			{
				ri++;
				if(ri > 0 && ri <= length)
				{
					CarrierRouteRow record = new CarrierRouteRow();
					recordList.Add(record);

					record.Carrier_route_id = Convert.ToInt32(reader.GetValue(carrier_route_idColumnIndex));
					record.Carrier_acct_id = Convert.ToInt16(reader.GetValue(carrier_acct_idColumnIndex));
					if(!reader.IsDBNull(route_idColumnIndex))
						record.Route_id = Convert.ToInt32(reader.GetValue(route_idColumnIndex));
					record.Status = Convert.ToByte(reader.GetValue(statusColumnIndex));
					record.Asr_time_window = Convert.ToInt32(reader.GetValue(asr_time_windowColumnIndex));
					record.Asr_target = Convert.ToInt16(reader.GetValue(asr_targetColumnIndex));
					record.Acd_time_window = Convert.ToInt32(reader.GetValue(acd_time_windowColumnIndex));
					record.Acd_target = Convert.ToInt16(reader.GetValue(acd_targetColumnIndex));
					record.Next_ep = Convert.ToByte(reader.GetValue(next_epColumnIndex));

					if(ri == length && 0 != totalRecordCount)
						break;
				}
			}

			totalRecordCount = 0 == totalRecordCount ? ri + startIndex : -1;
			return (CarrierRouteRow[])(recordList.ToArray(typeof(CarrierRouteRow)));
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
		/// Converts <see cref="System.Data.DataRow"/> to <see cref="CarrierRouteRow"/>.
		/// </summary>
		/// <param name="row">The <see cref="System.Data.DataRow"/> object to be mapped.</param>
		/// <returns>A reference to the <see cref="CarrierRouteRow"/> object.</returns>
		protected virtual CarrierRouteRow MapRow(DataRow row)
		{
			CarrierRouteRow mappedObject = new CarrierRouteRow();
			DataTable dataTable = row.Table;
			DataColumn dataColumn;
			// Column "Carrier_route_id"
			dataColumn = dataTable.Columns["Carrier_route_id"];
			if(!row.IsNull(dataColumn))
				mappedObject.Carrier_route_id = (int)row[dataColumn];
			// Column "Carrier_acct_id"
			dataColumn = dataTable.Columns["Carrier_acct_id"];
			if(!row.IsNull(dataColumn))
				mappedObject.Carrier_acct_id = (short)row[dataColumn];
			// Column "Route_id"
			dataColumn = dataTable.Columns["Route_id"];
			if(!row.IsNull(dataColumn))
				mappedObject.Route_id = (int)row[dataColumn];
			// Column "Status"
			dataColumn = dataTable.Columns["Status"];
			if(!row.IsNull(dataColumn))
				mappedObject.Status = (byte)row[dataColumn];
			// Column "Asr_time_window"
			dataColumn = dataTable.Columns["Asr_time_window"];
			if(!row.IsNull(dataColumn))
				mappedObject.Asr_time_window = (int)row[dataColumn];
			// Column "Asr_target"
			dataColumn = dataTable.Columns["Asr_target"];
			if(!row.IsNull(dataColumn))
				mappedObject.Asr_target = (short)row[dataColumn];
			// Column "Acd_time_window"
			dataColumn = dataTable.Columns["Acd_time_window"];
			if(!row.IsNull(dataColumn))
				mappedObject.Acd_time_window = (int)row[dataColumn];
			// Column "Acd_target"
			dataColumn = dataTable.Columns["Acd_target"];
			if(!row.IsNull(dataColumn))
				mappedObject.Acd_target = (short)row[dataColumn];
			// Column "Next_ep"
			dataColumn = dataTable.Columns["Next_ep"];
			if(!row.IsNull(dataColumn))
				mappedObject.Next_ep = (byte)row[dataColumn];
			return mappedObject;
		}

		/// <summary>
		/// Creates a <see cref="System.Data.DataTable"/> object for 
		/// the <c>CarrierRoute</c> table.
		/// </summary>
		/// <returns>A reference to the <see cref="System.Data.DataTable"/> object.</returns>
		protected virtual DataTable CreateDataTable()
		{
			DataTable dataTable = new DataTable();
			dataTable.TableName = "CarrierRoute";
			DataColumn dataColumn;
			dataColumn = dataTable.Columns.Add("Carrier_route_id", typeof(int));
			dataColumn.Caption = "carrier_route_id";
			dataColumn.AllowDBNull = false;
			dataColumn = dataTable.Columns.Add("Carrier_acct_id", typeof(short));
			dataColumn.Caption = "carrier_acct_id";
			dataColumn.AllowDBNull = false;
			dataColumn = dataTable.Columns.Add("Route_id", typeof(int));
			dataColumn.Caption = "route_id";
			dataColumn = dataTable.Columns.Add("Status", typeof(byte));
			dataColumn.Caption = "status";
			dataColumn.AllowDBNull = false;
			dataColumn = dataTable.Columns.Add("Asr_time_window", typeof(int));
			dataColumn.Caption = "asr_time_window";
			dataColumn.AllowDBNull = false;
			dataColumn = dataTable.Columns.Add("Asr_target", typeof(short));
			dataColumn.Caption = "asr_target";
			dataColumn.AllowDBNull = false;
			dataColumn = dataTable.Columns.Add("Acd_time_window", typeof(int));
			dataColumn.Caption = "acd_time_window";
			dataColumn.AllowDBNull = false;
			dataColumn = dataTable.Columns.Add("Acd_target", typeof(short));
			dataColumn.Caption = "acd_target";
			dataColumn.AllowDBNull = false;
			dataColumn = dataTable.Columns.Add("Next_ep", typeof(byte));
			dataColumn.Caption = "next_ep";
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
				case "Carrier_route_id":
					parameter = _db.AddParameter(cmd, paramName, DbType.Int32, value);
					break;

				case "Carrier_acct_id":
					parameter = _db.AddParameter(cmd, paramName, DbType.Int16, value);
					break;

				case "Route_id":
					parameter = _db.AddParameter(cmd, paramName, DbType.Int32, value);
					break;

				case "Status":
					parameter = _db.AddParameter(cmd, paramName, DbType.Byte, value);
					break;

				case "Asr_time_window":
					parameter = _db.AddParameter(cmd, paramName, DbType.Int32, value);
					break;

				case "Asr_target":
					parameter = _db.AddParameter(cmd, paramName, DbType.Int16, value);
					break;

				case "Acd_time_window":
					parameter = _db.AddParameter(cmd, paramName, DbType.Int32, value);
					break;

				case "Acd_target":
					parameter = _db.AddParameter(cmd, paramName, DbType.Int16, value);
					break;

				case "Next_ep":
					parameter = _db.AddParameter(cmd, paramName, DbType.Byte, value);
					break;

				default:
					throw new ArgumentException("Unknown parameter name (" + paramName + ").");
			}
			return parameter;
		}

	} // End of CarrierRouteCollection_Base class
}  // End of namespace

// <fileinfo name="Base\RoutingPlanDetailCollection_Base.cs">
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
	/// The base class for <see cref="RoutingPlanDetailCollection"/>. Provides methods 
	/// for common database table operations. 
	/// </summary>
	/// <remarks>
	/// Do not change this source code. Update the <see cref="RoutingPlanDetailCollection"/>
	/// class if you need to add or change some functionality.
	/// </remarks>
	public abstract class RoutingPlanDetailCollection_Base
	{
		// Constants
		public const string Routing_plan_idColumnName = "routing_plan_id";
		public const string Route_idColumnName = "route_id";
		public const string Routing_algorithmColumnName = "routing_algorithm";

		// Instance fields
		private Rbr_Db _db;

		/// <summary>
		/// Initializes a new instance of the <see cref="RoutingPlanDetailCollection_Base"/> 
		/// class with the specified <see cref="Rbr_Db"/>.
		/// </summary>
		/// <param name="db">The <see cref="Rbr_Db"/> object.</param>
		public RoutingPlanDetailCollection_Base(Rbr_Db db)
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
		/// Gets an array of all records from the <c>RoutingPlanDetail</c> table.
		/// </summary>
		/// <returns>An array of <see cref="RoutingPlanDetailRow"/> objects.</returns>
		public virtual RoutingPlanDetailRow[] GetAll()
		{
			return MapRecords(CreateGetAllCommand());
		}

		/// <summary>
		/// Gets a <see cref="System.Data.DataTable"/> object that 
		/// includes all records from the <c>RoutingPlanDetail</c> table.
		/// </summary>
		/// <returns>A reference to the <see cref="System.Data.DataTable"/> object.</returns>
		public virtual DataTable GetAllAsDataTable()
		{
			return MapRecordsToDataTable(CreateGetAllCommand());
		}

		/// <summary>
		/// Creates and returns an <see cref="System.Data.IDbCommand"/> object that is used
		/// to retrieve all records from the <c>RoutingPlanDetail</c> table.
		/// </summary>
		/// <returns>A reference to the <see cref="System.Data.IDbCommand"/> object.</returns>
		protected virtual IDbCommand CreateGetAllCommand()
		{
			return CreateGetCommand(null, null);
		}

		/// <summary>
		/// Gets the first <see cref="RoutingPlanDetailRow"/> objects that 
		/// match the search condition.
		/// </summary>
		/// <param name="whereSql">The SQL search condition. For example: 
		/// <c>"FirstName='Smith' AND Zip=75038"</c>.</param>
		/// <returns>An instance of <see cref="RoutingPlanDetailRow"/> or null reference 
		/// (Nothing in Visual Basic) if the object was not found.</returns>
		public RoutingPlanDetailRow GetRow(string whereSql)
		{
			int totalRecordCount = -1;
			RoutingPlanDetailRow[] rows = GetAsArray(whereSql, null, 0, 1, ref totalRecordCount);
			return 0 == rows.Length ? null : rows[0];
		}

		/// <summary>
		/// Gets an array of <see cref="RoutingPlanDetailRow"/> objects that 
		/// match the search condition, in the the specified sort order.
		/// </summary>
		/// <param name="whereSql">The SQL search condition. For example: 
		/// <c>"FirstName='Smith' AND Zip=75038"</c>.</param>
		/// <param name="orderBySql">The column name(s) followed by "ASC" (ascending) or "DESC" (descending).
		/// Columns are sorted in ascending order by default. For example: <c>"LastName ASC, FirstName ASC"</c>.</param>
		/// <returns>An array of <see cref="RoutingPlanDetailRow"/> objects.</returns>
		public RoutingPlanDetailRow[] GetAsArray(string whereSql, string orderBySql)
		{
			int totalRecordCount = -1;
			return GetAsArray(whereSql, orderBySql, 0, int.MaxValue, ref totalRecordCount);
		}

		/// <summary>
		/// Gets an array of <see cref="RoutingPlanDetailRow"/> objects that 
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
		/// <returns>An array of <see cref="RoutingPlanDetailRow"/> objects.</returns>
		public virtual RoutingPlanDetailRow[] GetAsArray(string whereSql, string orderBySql,
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
			string sql = "SELECT * FROM [dbo].[RoutingPlanDetail]";
			if(null != whereSql && 0 < whereSql.Length)
				sql += " WHERE " + whereSql;
			if(null != orderBySql && 0 < orderBySql.Length)
				sql += " ORDER BY " + orderBySql;
			return _db.CreateCommand(sql);
		}

		/// <summary>
		/// Gets <see cref="RoutingPlanDetailRow"/> by the primary key.
		/// </summary>
		/// <param name="routing_plan_id">The <c>routing_plan_id</c> column value.</param>
		/// <param name="route_id">The <c>route_id</c> column value.</param>
		/// <returns>An instance of <see cref="RoutingPlanDetailRow"/> or null reference 
		/// (Nothing in Visual Basic) if the object was not found.</returns>
		public virtual RoutingPlanDetailRow GetByPrimaryKey(int routing_plan_id, int route_id)
		{
			string whereSql = "[routing_plan_id]=" + _db.CreateSqlParameterName("Routing_plan_id") + " AND " +
							  "[route_id]=" + _db.CreateSqlParameterName("Route_id");
			IDbCommand cmd = CreateGetCommand(whereSql, null);
			AddParameter(cmd, "Routing_plan_id", routing_plan_id);
			AddParameter(cmd, "Route_id", route_id);
			RoutingPlanDetailRow[] tempArray = MapRecords(cmd);
			return 0 == tempArray.Length ? null : tempArray[0];
		}

		/// <summary>
		/// Gets an array of <see cref="RoutingPlanDetailRow"/> objects 
		/// by the <c>R_342</c> foreign key.
		/// </summary>
		/// <param name="routing_plan_id">The <c>routing_plan_id</c> column value.</param>
		/// <returns>An array of <see cref="RoutingPlanDetailRow"/> objects.</returns>
		public virtual RoutingPlanDetailRow[] GetByRouting_plan_id(int routing_plan_id)
		{
			return MapRecords(CreateGetByRouting_plan_idCommand(routing_plan_id));
		}

		/// <summary>
		/// Gets a <see cref="System.Data.DataTable"/> object 
		/// by the <c>R_342</c> foreign key.
		/// </summary>
		/// <param name="routing_plan_id">The <c>routing_plan_id</c> column value.</param>
		/// <returns>A reference to the <see cref="System.Data.DataTable"/> object.</returns>
		public virtual DataTable GetByRouting_plan_idAsDataTable(int routing_plan_id)
		{
			return MapRecordsToDataTable(CreateGetByRouting_plan_idCommand(routing_plan_id));
		}

		/// <summary>
		/// Creates an <see cref="System.Data.IDbCommand"/> object that can be used to 
		/// return records by the <c>R_342</c> foreign key.
		/// </summary>
		/// <param name="routing_plan_id">The <c>routing_plan_id</c> column value.</param>
		/// <returns>A reference to the <see cref="System.Data.IDbCommand"/> object.</returns>
		protected virtual IDbCommand CreateGetByRouting_plan_idCommand(int routing_plan_id)
		{
			string whereSql = "";
			whereSql += "[routing_plan_id]=" + _db.CreateSqlParameterName("Routing_plan_id");

			IDbCommand cmd = CreateGetCommand(whereSql, null);
			AddParameter(cmd, "Routing_plan_id", routing_plan_id);
			return cmd;
		}

		/// <summary>
		/// Gets an array of <see cref="RoutingPlanDetailRow"/> objects 
		/// by the <c>R_350</c> foreign key.
		/// </summary>
		/// <param name="route_id">The <c>route_id</c> column value.</param>
		/// <returns>An array of <see cref="RoutingPlanDetailRow"/> objects.</returns>
		public virtual RoutingPlanDetailRow[] GetByRoute_id(int route_id)
		{
			return MapRecords(CreateGetByRoute_idCommand(route_id));
		}

		/// <summary>
		/// Gets a <see cref="System.Data.DataTable"/> object 
		/// by the <c>R_350</c> foreign key.
		/// </summary>
		/// <param name="route_id">The <c>route_id</c> column value.</param>
		/// <returns>A reference to the <see cref="System.Data.DataTable"/> object.</returns>
		public virtual DataTable GetByRoute_idAsDataTable(int route_id)
		{
			return MapRecordsToDataTable(CreateGetByRoute_idCommand(route_id));
		}

		/// <summary>
		/// Creates an <see cref="System.Data.IDbCommand"/> object that can be used to 
		/// return records by the <c>R_350</c> foreign key.
		/// </summary>
		/// <param name="route_id">The <c>route_id</c> column value.</param>
		/// <returns>A reference to the <see cref="System.Data.IDbCommand"/> object.</returns>
		protected virtual IDbCommand CreateGetByRoute_idCommand(int route_id)
		{
			string whereSql = "";
			whereSql += "[route_id]=" + _db.CreateSqlParameterName("Route_id");

			IDbCommand cmd = CreateGetCommand(whereSql, null);
			AddParameter(cmd, "Route_id", route_id);
			return cmd;
		}

		/// <summary>
		/// Adds a new record into the <c>RoutingPlanDetail</c> table.
		/// </summary>
		/// <param name="value">The <see cref="RoutingPlanDetailRow"/> object to be inserted.</param>
		public virtual void Insert(RoutingPlanDetailRow value)
		{
			string sqlStr = "INSERT INTO [dbo].[RoutingPlanDetail] (" +
				"[routing_plan_id], " +
				"[route_id], " +
				"[routing_algorithm]" +
				") VALUES (" +
				_db.CreateSqlParameterName("Routing_plan_id") + ", " +
				_db.CreateSqlParameterName("Route_id") + ", " +
				_db.CreateSqlParameterName("Routing_algorithm") + ")";
			IDbCommand cmd = _db.CreateCommand(sqlStr);
			AddParameter(cmd, "Routing_plan_id", value.Routing_plan_id);
			AddParameter(cmd, "Route_id", value.Route_id);
			AddParameter(cmd, "Routing_algorithm", value.Routing_algorithm);
			cmd.ExecuteNonQuery();
		}

		/// <summary>
		/// Updates a record in the <c>RoutingPlanDetail</c> table.
		/// </summary>
		/// <param name="value">The <see cref="RoutingPlanDetailRow"/>
		/// object used to update the table record.</param>
		/// <returns>true if the record was updated; otherwise, false.</returns>
		public virtual bool Update(RoutingPlanDetailRow value)
		{
			string sqlStr = "UPDATE [dbo].[RoutingPlanDetail] SET " +
				"[routing_algorithm]=" + _db.CreateSqlParameterName("Routing_algorithm") +
				" WHERE " +
				"[routing_plan_id]=" + _db.CreateSqlParameterName("Routing_plan_id") + " AND " +
				"[route_id]=" + _db.CreateSqlParameterName("Route_id");
			IDbCommand cmd = _db.CreateCommand(sqlStr);
			AddParameter(cmd, "Routing_algorithm", value.Routing_algorithm);
			AddParameter(cmd, "Routing_plan_id", value.Routing_plan_id);
			AddParameter(cmd, "Route_id", value.Route_id);
			return 0 != cmd.ExecuteNonQuery();
		}

		/// <summary>
		/// Updates the <c>RoutingPlanDetail</c> table and calls the <c>AcceptChanges</c> method
		/// on the changed DataRow objects.
		/// </summary>
		/// <param name="table">The <see cref="System.Data.DataTable"/> used to update the data source.</param>
		public void Update(DataTable table)
		{
			Update(table, true);
		}

		/// <summary>
		/// Updates the <c>RoutingPlanDetail</c> table. Pass <c>false</c> as the <c>acceptChanges</c> 
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
							DeleteByPrimaryKey((int)row["Routing_plan_id"], (int)row["Route_id"]);
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
		/// Deletes the specified object from the <c>RoutingPlanDetail</c> table.
		/// </summary>
		/// <param name="value">The <see cref="RoutingPlanDetailRow"/> object to delete.</param>
		/// <returns>true if the record was deleted; otherwise, false.</returns>
		public bool Delete(RoutingPlanDetailRow value)
		{
			return DeleteByPrimaryKey(value.Routing_plan_id, value.Route_id);
		}

		/// <summary>
		/// Deletes a record from the <c>RoutingPlanDetail</c> table using
		/// the specified primary key.
		/// </summary>
		/// <param name="routing_plan_id">The <c>routing_plan_id</c> column value.</param>
		/// <param name="route_id">The <c>route_id</c> column value.</param>
		/// <returns>true if the record was deleted; otherwise, false.</returns>
		public virtual bool DeleteByPrimaryKey(int routing_plan_id, int route_id)
		{
			string whereSql = "[routing_plan_id]=" + _db.CreateSqlParameterName("Routing_plan_id") + " AND " +
							  "[route_id]=" + _db.CreateSqlParameterName("Route_id");
			IDbCommand cmd = CreateDeleteCommand(whereSql);
			AddParameter(cmd, "Routing_plan_id", routing_plan_id);
			AddParameter(cmd, "Route_id", route_id);
			return 0 < cmd.ExecuteNonQuery();
		}

		/// <summary>
		/// Deletes records from the <c>RoutingPlanDetail</c> table using the 
		/// <c>R_342</c> foreign key.
		/// </summary>
		/// <param name="routing_plan_id">The <c>routing_plan_id</c> column value.</param>
		/// <returns>The number of records deleted from the table.</returns>
		public int DeleteByRouting_plan_id(int routing_plan_id)
		{
			return CreateDeleteByRouting_plan_idCommand(routing_plan_id).ExecuteNonQuery();
		}

		/// <summary>
		/// Creates an <see cref="System.Data.IDbCommand"/> object that can be used to
		/// delete records using the <c>R_342</c> foreign key.
		/// </summary>
		/// <param name="routing_plan_id">The <c>routing_plan_id</c> column value.</param>
		/// <returns>A reference to the <see cref="System.Data.IDbCommand"/> object.</returns>
		protected virtual IDbCommand CreateDeleteByRouting_plan_idCommand(int routing_plan_id)
		{
			string whereSql = "";
			whereSql += "[routing_plan_id]=" + _db.CreateSqlParameterName("Routing_plan_id");

			IDbCommand cmd = CreateDeleteCommand(whereSql);
			AddParameter(cmd, "Routing_plan_id", routing_plan_id);
			return cmd;
		}

		/// <summary>
		/// Deletes records from the <c>RoutingPlanDetail</c> table using the 
		/// <c>R_350</c> foreign key.
		/// </summary>
		/// <param name="route_id">The <c>route_id</c> column value.</param>
		/// <returns>The number of records deleted from the table.</returns>
		public int DeleteByRoute_id(int route_id)
		{
			return CreateDeleteByRoute_idCommand(route_id).ExecuteNonQuery();
		}

		/// <summary>
		/// Creates an <see cref="System.Data.IDbCommand"/> object that can be used to
		/// delete records using the <c>R_350</c> foreign key.
		/// </summary>
		/// <param name="route_id">The <c>route_id</c> column value.</param>
		/// <returns>A reference to the <see cref="System.Data.IDbCommand"/> object.</returns>
		protected virtual IDbCommand CreateDeleteByRoute_idCommand(int route_id)
		{
			string whereSql = "";
			whereSql += "[route_id]=" + _db.CreateSqlParameterName("Route_id");

			IDbCommand cmd = CreateDeleteCommand(whereSql);
			AddParameter(cmd, "Route_id", route_id);
			return cmd;
		}

		/// <summary>
		/// Deletes <c>RoutingPlanDetail</c> records that match the specified criteria.
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
		/// to delete <c>RoutingPlanDetail</c> records that match the specified criteria.
		/// </summary>
		/// <param name="whereSql">The SQL search condition. 
		/// For example: <c>"FirstName='Smith' AND Zip=75038"</c>.</param>
		/// <returns>A reference to the <see cref="System.Data.IDbCommand"/> object.</returns>
		protected virtual IDbCommand CreateDeleteCommand(string whereSql)
		{
			string sql = "DELETE FROM [dbo].[RoutingPlanDetail]";
			if(null != whereSql && 0 < whereSql.Length)
				sql += " WHERE " + whereSql;
			return _db.CreateCommand(sql);
		}

		/// <summary>
		/// Deletes all records from the <c>RoutingPlanDetail</c> table.
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
		/// <returns>An array of <see cref="RoutingPlanDetailRow"/> objects.</returns>
		protected RoutingPlanDetailRow[] MapRecords(IDbCommand command)
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
		/// <returns>An array of <see cref="RoutingPlanDetailRow"/> objects.</returns>
		protected RoutingPlanDetailRow[] MapRecords(IDataReader reader)
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
		/// <returns>An array of <see cref="RoutingPlanDetailRow"/> objects.</returns>
		protected virtual RoutingPlanDetailRow[] MapRecords(IDataReader reader, 
										int startIndex, int length, ref int totalRecordCount)
		{
			if(0 > startIndex)
				throw new ArgumentOutOfRangeException("startIndex", startIndex, "StartIndex cannot be less than zero.");
			if(0 > length)
				throw new ArgumentOutOfRangeException("length", length, "Length cannot be less than zero.");

			int routing_plan_idColumnIndex = reader.GetOrdinal("routing_plan_id");
			int route_idColumnIndex = reader.GetOrdinal("route_id");
			int routing_algorithmColumnIndex = reader.GetOrdinal("routing_algorithm");

			System.Collections.ArrayList recordList = new System.Collections.ArrayList();
			int ri = -startIndex;
			while(reader.Read())
			{
				ri++;
				if(ri > 0 && ri <= length)
				{
					RoutingPlanDetailRow record = new RoutingPlanDetailRow();
					recordList.Add(record);

					record.Routing_plan_id = Convert.ToInt32(reader.GetValue(routing_plan_idColumnIndex));
					record.Route_id = Convert.ToInt32(reader.GetValue(route_idColumnIndex));
					record.Routing_algorithm = Convert.ToByte(reader.GetValue(routing_algorithmColumnIndex));

					if(ri == length && 0 != totalRecordCount)
						break;
				}
			}

			totalRecordCount = 0 == totalRecordCount ? ri + startIndex : -1;
			return (RoutingPlanDetailRow[])(recordList.ToArray(typeof(RoutingPlanDetailRow)));
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
		/// Converts <see cref="System.Data.DataRow"/> to <see cref="RoutingPlanDetailRow"/>.
		/// </summary>
		/// <param name="row">The <see cref="System.Data.DataRow"/> object to be mapped.</param>
		/// <returns>A reference to the <see cref="RoutingPlanDetailRow"/> object.</returns>
		protected virtual RoutingPlanDetailRow MapRow(DataRow row)
		{
			RoutingPlanDetailRow mappedObject = new RoutingPlanDetailRow();
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
			// Column "Routing_algorithm"
			dataColumn = dataTable.Columns["Routing_algorithm"];
			if(!row.IsNull(dataColumn))
				mappedObject.Routing_algorithm = (byte)row[dataColumn];
			return mappedObject;
		}

		/// <summary>
		/// Creates a <see cref="System.Data.DataTable"/> object for 
		/// the <c>RoutingPlanDetail</c> table.
		/// </summary>
		/// <returns>A reference to the <see cref="System.Data.DataTable"/> object.</returns>
		protected virtual DataTable CreateDataTable()
		{
			DataTable dataTable = new DataTable();
			dataTable.TableName = "RoutingPlanDetail";
			DataColumn dataColumn;
			dataColumn = dataTable.Columns.Add("Routing_plan_id", typeof(int));
			dataColumn.Caption = "routing_plan_id";
			dataColumn.AllowDBNull = false;
			dataColumn = dataTable.Columns.Add("Route_id", typeof(int));
			dataColumn.Caption = "route_id";
			dataColumn.AllowDBNull = false;
			dataColumn = dataTable.Columns.Add("Routing_algorithm", typeof(byte));
			dataColumn.Caption = "routing_algorithm";
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
				case "Routing_plan_id":
					parameter = _db.AddParameter(cmd, paramName, DbType.Int32, value);
					break;

				case "Route_id":
					parameter = _db.AddParameter(cmd, paramName, DbType.Int32, value);
					break;

				case "Routing_algorithm":
					parameter = _db.AddParameter(cmd, paramName, DbType.Byte, value);
					break;

				default:
					throw new ArgumentException("Unknown parameter name (" + paramName + ").");
			}
			return parameter;
		}

	} // End of RoutingPlanDetailCollection_Base class
}  // End of namespace

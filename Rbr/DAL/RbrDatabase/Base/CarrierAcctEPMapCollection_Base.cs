// <fileinfo name="Base\CarrierAcctEPMapCollection_Base.cs">
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
	/// The base class for <see cref="CarrierAcctEPMapCollection"/>. Provides methods 
	/// for common database table operations. 
	/// </summary>
	/// <remarks>
	/// Do not change this source code. Update the <see cref="CarrierAcctEPMapCollection"/>
	/// class if you need to add or change some functionality.
	/// </remarks>
	public abstract class CarrierAcctEPMapCollection_Base
	{
		// Constants
		public const string Carrier_acct_EP_map_idColumnName = "carrier_acct_EP_map_id";
		public const string Carrier_route_idColumnName = "carrier_route_id";
		public const string End_point_idColumnName = "end_point_id";
		public const string PriorityColumnName = "priority";
		public const string Carrier_acct_idColumnName = "carrier_acct_id";

		// Instance fields
		private Rbr_Db _db;

		/// <summary>
		/// Initializes a new instance of the <see cref="CarrierAcctEPMapCollection_Base"/> 
		/// class with the specified <see cref="Rbr_Db"/>.
		/// </summary>
		/// <param name="db">The <see cref="Rbr_Db"/> object.</param>
		public CarrierAcctEPMapCollection_Base(Rbr_Db db)
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
		/// Gets an array of all records from the <c>CarrierAcctEPMap</c> table.
		/// </summary>
		/// <returns>An array of <see cref="CarrierAcctEPMapRow"/> objects.</returns>
		public virtual CarrierAcctEPMapRow[] GetAll()
		{
			return MapRecords(CreateGetAllCommand());
		}

		/// <summary>
		/// Gets a <see cref="System.Data.DataTable"/> object that 
		/// includes all records from the <c>CarrierAcctEPMap</c> table.
		/// </summary>
		/// <returns>A reference to the <see cref="System.Data.DataTable"/> object.</returns>
		public virtual DataTable GetAllAsDataTable()
		{
			return MapRecordsToDataTable(CreateGetAllCommand());
		}

		/// <summary>
		/// Creates and returns an <see cref="System.Data.IDbCommand"/> object that is used
		/// to retrieve all records from the <c>CarrierAcctEPMap</c> table.
		/// </summary>
		/// <returns>A reference to the <see cref="System.Data.IDbCommand"/> object.</returns>
		protected virtual IDbCommand CreateGetAllCommand()
		{
			return CreateGetCommand(null, null);
		}

		/// <summary>
		/// Gets the first <see cref="CarrierAcctEPMapRow"/> objects that 
		/// match the search condition.
		/// </summary>
		/// <param name="whereSql">The SQL search condition. For example: 
		/// <c>"FirstName='Smith' AND Zip=75038"</c>.</param>
		/// <returns>An instance of <see cref="CarrierAcctEPMapRow"/> or null reference 
		/// (Nothing in Visual Basic) if the object was not found.</returns>
		public CarrierAcctEPMapRow GetRow(string whereSql)
		{
			int totalRecordCount = -1;
			CarrierAcctEPMapRow[] rows = GetAsArray(whereSql, null, 0, 1, ref totalRecordCount);
			return 0 == rows.Length ? null : rows[0];
		}

		/// <summary>
		/// Gets an array of <see cref="CarrierAcctEPMapRow"/> objects that 
		/// match the search condition, in the the specified sort order.
		/// </summary>
		/// <param name="whereSql">The SQL search condition. For example: 
		/// <c>"FirstName='Smith' AND Zip=75038"</c>.</param>
		/// <param name="orderBySql">The column name(s) followed by "ASC" (ascending) or "DESC" (descending).
		/// Columns are sorted in ascending order by default. For example: <c>"LastName ASC, FirstName ASC"</c>.</param>
		/// <returns>An array of <see cref="CarrierAcctEPMapRow"/> objects.</returns>
		public CarrierAcctEPMapRow[] GetAsArray(string whereSql, string orderBySql)
		{
			int totalRecordCount = -1;
			return GetAsArray(whereSql, orderBySql, 0, int.MaxValue, ref totalRecordCount);
		}

		/// <summary>
		/// Gets an array of <see cref="CarrierAcctEPMapRow"/> objects that 
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
		/// <returns>An array of <see cref="CarrierAcctEPMapRow"/> objects.</returns>
		public virtual CarrierAcctEPMapRow[] GetAsArray(string whereSql, string orderBySql,
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
			string sql = "SELECT * FROM [dbo].[CarrierAcctEPMap]";
			if(null != whereSql && 0 < whereSql.Length)
				sql += " WHERE " + whereSql;
			if(null != orderBySql && 0 < orderBySql.Length)
				sql += " ORDER BY " + orderBySql;
			return _db.CreateCommand(sql);
		}

		/// <summary>
		/// Gets <see cref="CarrierAcctEPMapRow"/> by the primary key.
		/// </summary>
		/// <param name="carrier_acct_EP_map_id">The <c>carrier_acct_EP_map_id</c> column value.</param>
		/// <returns>An instance of <see cref="CarrierAcctEPMapRow"/> or null reference 
		/// (Nothing in Visual Basic) if the object was not found.</returns>
		public virtual CarrierAcctEPMapRow GetByPrimaryKey(int carrier_acct_EP_map_id)
		{
			string whereSql = "[carrier_acct_EP_map_id]=" + _db.CreateSqlParameterName("Carrier_acct_EP_map_id");
			IDbCommand cmd = CreateGetCommand(whereSql, null);
			AddParameter(cmd, "Carrier_acct_EP_map_id", carrier_acct_EP_map_id);
			CarrierAcctEPMapRow[] tempArray = MapRecords(cmd);
			return 0 == tempArray.Length ? null : tempArray[0];
		}

		/// <summary>
		/// Gets an array of <see cref="CarrierAcctEPMapRow"/> objects 
		/// by the <c>R_347</c> foreign key.
		/// </summary>
		/// <param name="carrier_acct_id">The <c>carrier_acct_id</c> column value.</param>
		/// <returns>An array of <see cref="CarrierAcctEPMapRow"/> objects.</returns>
		public virtual CarrierAcctEPMapRow[] GetByCarrier_acct_id(short carrier_acct_id)
		{
			return MapRecords(CreateGetByCarrier_acct_idCommand(carrier_acct_id));
		}

		/// <summary>
		/// Gets a <see cref="System.Data.DataTable"/> object 
		/// by the <c>R_347</c> foreign key.
		/// </summary>
		/// <param name="carrier_acct_id">The <c>carrier_acct_id</c> column value.</param>
		/// <returns>A reference to the <see cref="System.Data.DataTable"/> object.</returns>
		public virtual DataTable GetByCarrier_acct_idAsDataTable(short carrier_acct_id)
		{
			return MapRecordsToDataTable(CreateGetByCarrier_acct_idCommand(carrier_acct_id));
		}

		/// <summary>
		/// Creates an <see cref="System.Data.IDbCommand"/> object that can be used to 
		/// return records by the <c>R_347</c> foreign key.
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
		/// Gets an array of <see cref="CarrierAcctEPMapRow"/> objects 
		/// by the <c>R_348</c> foreign key.
		/// </summary>
		/// <param name="carrier_route_id">The <c>carrier_route_id</c> column value.</param>
		/// <returns>An array of <see cref="CarrierAcctEPMapRow"/> objects.</returns>
		public virtual CarrierAcctEPMapRow[] GetByCarrier_route_id(int carrier_route_id)
		{
			return MapRecords(CreateGetByCarrier_route_idCommand(carrier_route_id));
		}

		/// <summary>
		/// Gets a <see cref="System.Data.DataTable"/> object 
		/// by the <c>R_348</c> foreign key.
		/// </summary>
		/// <param name="carrier_route_id">The <c>carrier_route_id</c> column value.</param>
		/// <returns>A reference to the <see cref="System.Data.DataTable"/> object.</returns>
		public virtual DataTable GetByCarrier_route_idAsDataTable(int carrier_route_id)
		{
			return MapRecordsToDataTable(CreateGetByCarrier_route_idCommand(carrier_route_id));
		}

		/// <summary>
		/// Creates an <see cref="System.Data.IDbCommand"/> object that can be used to 
		/// return records by the <c>R_348</c> foreign key.
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
		/// Gets an array of <see cref="CarrierAcctEPMapRow"/> objects 
		/// by the <c>R_349</c> foreign key.
		/// </summary>
		/// <param name="end_point_id">The <c>end_point_id</c> column value.</param>
		/// <returns>An array of <see cref="CarrierAcctEPMapRow"/> objects.</returns>
		public virtual CarrierAcctEPMapRow[] GetByEnd_point_id(short end_point_id)
		{
			return MapRecords(CreateGetByEnd_point_idCommand(end_point_id));
		}

		/// <summary>
		/// Gets a <see cref="System.Data.DataTable"/> object 
		/// by the <c>R_349</c> foreign key.
		/// </summary>
		/// <param name="end_point_id">The <c>end_point_id</c> column value.</param>
		/// <returns>A reference to the <see cref="System.Data.DataTable"/> object.</returns>
		public virtual DataTable GetByEnd_point_idAsDataTable(short end_point_id)
		{
			return MapRecordsToDataTable(CreateGetByEnd_point_idCommand(end_point_id));
		}

		/// <summary>
		/// Creates an <see cref="System.Data.IDbCommand"/> object that can be used to 
		/// return records by the <c>R_349</c> foreign key.
		/// </summary>
		/// <param name="end_point_id">The <c>end_point_id</c> column value.</param>
		/// <returns>A reference to the <see cref="System.Data.IDbCommand"/> object.</returns>
		protected virtual IDbCommand CreateGetByEnd_point_idCommand(short end_point_id)
		{
			string whereSql = "";
			whereSql += "[end_point_id]=" + _db.CreateSqlParameterName("End_point_id");

			IDbCommand cmd = CreateGetCommand(whereSql, null);
			AddParameter(cmd, "End_point_id", end_point_id);
			return cmd;
		}

		/// <summary>
		/// Adds a new record into the <c>CarrierAcctEPMap</c> table.
		/// </summary>
		/// <param name="value">The <see cref="CarrierAcctEPMapRow"/> object to be inserted.</param>
		public virtual void Insert(CarrierAcctEPMapRow value)
		{
			string sqlStr = "INSERT INTO [dbo].[CarrierAcctEPMap] (" +
				"[carrier_acct_EP_map_id], " +
				"[carrier_route_id], " +
				"[end_point_id], " +
				"[priority], " +
				"[carrier_acct_id]" +
				") VALUES (" +
				_db.CreateSqlParameterName("Carrier_acct_EP_map_id") + ", " +
				_db.CreateSqlParameterName("Carrier_route_id") + ", " +
				_db.CreateSqlParameterName("End_point_id") + ", " +
				_db.CreateSqlParameterName("Priority") + ", " +
				_db.CreateSqlParameterName("Carrier_acct_id") + ")";
			IDbCommand cmd = _db.CreateCommand(sqlStr);
			AddParameter(cmd, "Carrier_acct_EP_map_id", value.Carrier_acct_EP_map_id);
			AddParameter(cmd, "Carrier_route_id", value.Carrier_route_id);
			AddParameter(cmd, "End_point_id", value.End_point_id);
			AddParameter(cmd, "Priority", value.Priority);
			AddParameter(cmd, "Carrier_acct_id", value.Carrier_acct_id);
			cmd.ExecuteNonQuery();
		}

		/// <summary>
		/// Updates a record in the <c>CarrierAcctEPMap</c> table.
		/// </summary>
		/// <param name="value">The <see cref="CarrierAcctEPMapRow"/>
		/// object used to update the table record.</param>
		/// <returns>true if the record was updated; otherwise, false.</returns>
		public virtual bool Update(CarrierAcctEPMapRow value)
		{
			string sqlStr = "UPDATE [dbo].[CarrierAcctEPMap] SET " +
				"[carrier_route_id]=" + _db.CreateSqlParameterName("Carrier_route_id") + ", " +
				"[end_point_id]=" + _db.CreateSqlParameterName("End_point_id") + ", " +
				"[priority]=" + _db.CreateSqlParameterName("Priority") + ", " +
				"[carrier_acct_id]=" + _db.CreateSqlParameterName("Carrier_acct_id") +
				" WHERE " +
				"[carrier_acct_EP_map_id]=" + _db.CreateSqlParameterName("Carrier_acct_EP_map_id");
			IDbCommand cmd = _db.CreateCommand(sqlStr);
			AddParameter(cmd, "Carrier_route_id", value.Carrier_route_id);
			AddParameter(cmd, "End_point_id", value.End_point_id);
			AddParameter(cmd, "Priority", value.Priority);
			AddParameter(cmd, "Carrier_acct_id", value.Carrier_acct_id);
			AddParameter(cmd, "Carrier_acct_EP_map_id", value.Carrier_acct_EP_map_id);
			return 0 != cmd.ExecuteNonQuery();
		}

		/// <summary>
		/// Updates the <c>CarrierAcctEPMap</c> table and calls the <c>AcceptChanges</c> method
		/// on the changed DataRow objects.
		/// </summary>
		/// <param name="table">The <see cref="System.Data.DataTable"/> used to update the data source.</param>
		public void Update(DataTable table)
		{
			Update(table, true);
		}

		/// <summary>
		/// Updates the <c>CarrierAcctEPMap</c> table. Pass <c>false</c> as the <c>acceptChanges</c> 
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
							DeleteByPrimaryKey((int)row["Carrier_acct_EP_map_id"]);
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
		/// Deletes the specified object from the <c>CarrierAcctEPMap</c> table.
		/// </summary>
		/// <param name="value">The <see cref="CarrierAcctEPMapRow"/> object to delete.</param>
		/// <returns>true if the record was deleted; otherwise, false.</returns>
		public bool Delete(CarrierAcctEPMapRow value)
		{
			return DeleteByPrimaryKey(value.Carrier_acct_EP_map_id);
		}

		/// <summary>
		/// Deletes a record from the <c>CarrierAcctEPMap</c> table using
		/// the specified primary key.
		/// </summary>
		/// <param name="carrier_acct_EP_map_id">The <c>carrier_acct_EP_map_id</c> column value.</param>
		/// <returns>true if the record was deleted; otherwise, false.</returns>
		public virtual bool DeleteByPrimaryKey(int carrier_acct_EP_map_id)
		{
			string whereSql = "[carrier_acct_EP_map_id]=" + _db.CreateSqlParameterName("Carrier_acct_EP_map_id");
			IDbCommand cmd = CreateDeleteCommand(whereSql);
			AddParameter(cmd, "Carrier_acct_EP_map_id", carrier_acct_EP_map_id);
			return 0 < cmd.ExecuteNonQuery();
		}

		/// <summary>
		/// Deletes records from the <c>CarrierAcctEPMap</c> table using the 
		/// <c>R_347</c> foreign key.
		/// </summary>
		/// <param name="carrier_acct_id">The <c>carrier_acct_id</c> column value.</param>
		/// <returns>The number of records deleted from the table.</returns>
		public int DeleteByCarrier_acct_id(short carrier_acct_id)
		{
			return CreateDeleteByCarrier_acct_idCommand(carrier_acct_id).ExecuteNonQuery();
		}

		/// <summary>
		/// Creates an <see cref="System.Data.IDbCommand"/> object that can be used to
		/// delete records using the <c>R_347</c> foreign key.
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
		/// Deletes records from the <c>CarrierAcctEPMap</c> table using the 
		/// <c>R_348</c> foreign key.
		/// </summary>
		/// <param name="carrier_route_id">The <c>carrier_route_id</c> column value.</param>
		/// <returns>The number of records deleted from the table.</returns>
		public int DeleteByCarrier_route_id(int carrier_route_id)
		{
			return CreateDeleteByCarrier_route_idCommand(carrier_route_id).ExecuteNonQuery();
		}

		/// <summary>
		/// Creates an <see cref="System.Data.IDbCommand"/> object that can be used to
		/// delete records using the <c>R_348</c> foreign key.
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
		/// Deletes records from the <c>CarrierAcctEPMap</c> table using the 
		/// <c>R_349</c> foreign key.
		/// </summary>
		/// <param name="end_point_id">The <c>end_point_id</c> column value.</param>
		/// <returns>The number of records deleted from the table.</returns>
		public int DeleteByEnd_point_id(short end_point_id)
		{
			return CreateDeleteByEnd_point_idCommand(end_point_id).ExecuteNonQuery();
		}

		/// <summary>
		/// Creates an <see cref="System.Data.IDbCommand"/> object that can be used to
		/// delete records using the <c>R_349</c> foreign key.
		/// </summary>
		/// <param name="end_point_id">The <c>end_point_id</c> column value.</param>
		/// <returns>A reference to the <see cref="System.Data.IDbCommand"/> object.</returns>
		protected virtual IDbCommand CreateDeleteByEnd_point_idCommand(short end_point_id)
		{
			string whereSql = "";
			whereSql += "[end_point_id]=" + _db.CreateSqlParameterName("End_point_id");

			IDbCommand cmd = CreateDeleteCommand(whereSql);
			AddParameter(cmd, "End_point_id", end_point_id);
			return cmd;
		}

		/// <summary>
		/// Deletes <c>CarrierAcctEPMap</c> records that match the specified criteria.
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
		/// to delete <c>CarrierAcctEPMap</c> records that match the specified criteria.
		/// </summary>
		/// <param name="whereSql">The SQL search condition. 
		/// For example: <c>"FirstName='Smith' AND Zip=75038"</c>.</param>
		/// <returns>A reference to the <see cref="System.Data.IDbCommand"/> object.</returns>
		protected virtual IDbCommand CreateDeleteCommand(string whereSql)
		{
			string sql = "DELETE FROM [dbo].[CarrierAcctEPMap]";
			if(null != whereSql && 0 < whereSql.Length)
				sql += " WHERE " + whereSql;
			return _db.CreateCommand(sql);
		}

		/// <summary>
		/// Deletes all records from the <c>CarrierAcctEPMap</c> table.
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
		/// <returns>An array of <see cref="CarrierAcctEPMapRow"/> objects.</returns>
		protected CarrierAcctEPMapRow[] MapRecords(IDbCommand command)
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
		/// <returns>An array of <see cref="CarrierAcctEPMapRow"/> objects.</returns>
		protected CarrierAcctEPMapRow[] MapRecords(IDataReader reader)
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
		/// <returns>An array of <see cref="CarrierAcctEPMapRow"/> objects.</returns>
		protected virtual CarrierAcctEPMapRow[] MapRecords(IDataReader reader, 
										int startIndex, int length, ref int totalRecordCount)
		{
			if(0 > startIndex)
				throw new ArgumentOutOfRangeException("startIndex", startIndex, "StartIndex cannot be less than zero.");
			if(0 > length)
				throw new ArgumentOutOfRangeException("length", length, "Length cannot be less than zero.");

			int carrier_acct_EP_map_idColumnIndex = reader.GetOrdinal("carrier_acct_EP_map_id");
			int carrier_route_idColumnIndex = reader.GetOrdinal("carrier_route_id");
			int end_point_idColumnIndex = reader.GetOrdinal("end_point_id");
			int priorityColumnIndex = reader.GetOrdinal("priority");
			int carrier_acct_idColumnIndex = reader.GetOrdinal("carrier_acct_id");

			System.Collections.ArrayList recordList = new System.Collections.ArrayList();
			int ri = -startIndex;
			while(reader.Read())
			{
				ri++;
				if(ri > 0 && ri <= length)
				{
					CarrierAcctEPMapRow record = new CarrierAcctEPMapRow();
					recordList.Add(record);

					record.Carrier_acct_EP_map_id = Convert.ToInt32(reader.GetValue(carrier_acct_EP_map_idColumnIndex));
					record.Carrier_route_id = Convert.ToInt32(reader.GetValue(carrier_route_idColumnIndex));
					record.End_point_id = Convert.ToInt16(reader.GetValue(end_point_idColumnIndex));
					record.Priority = Convert.ToByte(reader.GetValue(priorityColumnIndex));
					record.Carrier_acct_id = Convert.ToInt16(reader.GetValue(carrier_acct_idColumnIndex));

					if(ri == length && 0 != totalRecordCount)
						break;
				}
			}

			totalRecordCount = 0 == totalRecordCount ? ri + startIndex : -1;
			return (CarrierAcctEPMapRow[])(recordList.ToArray(typeof(CarrierAcctEPMapRow)));
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
		/// Converts <see cref="System.Data.DataRow"/> to <see cref="CarrierAcctEPMapRow"/>.
		/// </summary>
		/// <param name="row">The <see cref="System.Data.DataRow"/> object to be mapped.</param>
		/// <returns>A reference to the <see cref="CarrierAcctEPMapRow"/> object.</returns>
		protected virtual CarrierAcctEPMapRow MapRow(DataRow row)
		{
			CarrierAcctEPMapRow mappedObject = new CarrierAcctEPMapRow();
			DataTable dataTable = row.Table;
			DataColumn dataColumn;
			// Column "Carrier_acct_EP_map_id"
			dataColumn = dataTable.Columns["Carrier_acct_EP_map_id"];
			if(!row.IsNull(dataColumn))
				mappedObject.Carrier_acct_EP_map_id = (int)row[dataColumn];
			// Column "Carrier_route_id"
			dataColumn = dataTable.Columns["Carrier_route_id"];
			if(!row.IsNull(dataColumn))
				mappedObject.Carrier_route_id = (int)row[dataColumn];
			// Column "End_point_id"
			dataColumn = dataTable.Columns["End_point_id"];
			if(!row.IsNull(dataColumn))
				mappedObject.End_point_id = (short)row[dataColumn];
			// Column "Priority"
			dataColumn = dataTable.Columns["Priority"];
			if(!row.IsNull(dataColumn))
				mappedObject.Priority = (byte)row[dataColumn];
			// Column "Carrier_acct_id"
			dataColumn = dataTable.Columns["Carrier_acct_id"];
			if(!row.IsNull(dataColumn))
				mappedObject.Carrier_acct_id = (short)row[dataColumn];
			return mappedObject;
		}

		/// <summary>
		/// Creates a <see cref="System.Data.DataTable"/> object for 
		/// the <c>CarrierAcctEPMap</c> table.
		/// </summary>
		/// <returns>A reference to the <see cref="System.Data.DataTable"/> object.</returns>
		protected virtual DataTable CreateDataTable()
		{
			DataTable dataTable = new DataTable();
			dataTable.TableName = "CarrierAcctEPMap";
			DataColumn dataColumn;
			dataColumn = dataTable.Columns.Add("Carrier_acct_EP_map_id", typeof(int));
			dataColumn.Caption = "carrier_acct_EP_map_id";
			dataColumn.AllowDBNull = false;
			dataColumn = dataTable.Columns.Add("Carrier_route_id", typeof(int));
			dataColumn.Caption = "carrier_route_id";
			dataColumn.AllowDBNull = false;
			dataColumn = dataTable.Columns.Add("End_point_id", typeof(short));
			dataColumn.Caption = "end_point_id";
			dataColumn.AllowDBNull = false;
			dataColumn = dataTable.Columns.Add("Priority", typeof(byte));
			dataColumn.Caption = "priority";
			dataColumn.AllowDBNull = false;
			dataColumn = dataTable.Columns.Add("Carrier_acct_id", typeof(short));
			dataColumn.Caption = "carrier_acct_id";
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
				case "Carrier_acct_EP_map_id":
					parameter = _db.AddParameter(cmd, paramName, DbType.Int32, value);
					break;

				case "Carrier_route_id":
					parameter = _db.AddParameter(cmd, paramName, DbType.Int32, value);
					break;

				case "End_point_id":
					parameter = _db.AddParameter(cmd, paramName, DbType.Int16, value);
					break;

				case "Priority":
					parameter = _db.AddParameter(cmd, paramName, DbType.Byte, value);
					break;

				case "Carrier_acct_id":
					parameter = _db.AddParameter(cmd, paramName, DbType.Int16, value);
					break;

				default:
					throw new ArgumentException("Unknown parameter name (" + paramName + ").");
			}
			return parameter;
		}

	} // End of CarrierAcctEPMapCollection_Base class
}  // End of namespace

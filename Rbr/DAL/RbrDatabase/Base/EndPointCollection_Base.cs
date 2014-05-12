// <fileinfo name="Base\EndPointCollection_Base.cs">
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
	/// The base class for <see cref="EndPointCollection"/>. Provides methods 
	/// for common database table operations. 
	/// </summary>
	/// <remarks>
	/// Do not change this source code. Update the <see cref="EndPointCollection"/>
	/// class if you need to add or change some functionality.
	/// </remarks>
	public abstract class EndPointCollection_Base
	{
		// Constants
		public const string End_point_idColumnName = "end_point_id";
		public const string AliasColumnName = "alias";
		public const string With_alias_authenticationColumnName = "with_alias_authentication";
		public const string StatusColumnName = "status";
		public const string TypeColumnName = "type";
		public const string ProtocolColumnName = "protocol";
		public const string PortColumnName = "port";
		public const string RegistrationColumnName = "registration";
		public const string Is_registeredColumnName = "is_registered";
		public const string Ip_address_rangeColumnName = "ip_address_range";
		public const string Max_callsColumnName = "max_calls";
		public const string PasswordColumnName = "password";
		public const string Prefix_in_type_idColumnName = "prefix_in_type_id";
		public const string Virtual_switch_idColumnName = "virtual_switch_id";

		// Instance fields
		private Rbr_Db _db;

		/// <summary>
		/// Initializes a new instance of the <see cref="EndPointCollection_Base"/> 
		/// class with the specified <see cref="Rbr_Db"/>.
		/// </summary>
		/// <param name="db">The <see cref="Rbr_Db"/> object.</param>
		public EndPointCollection_Base(Rbr_Db db)
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
		/// Gets an array of all records from the <c>EndPoint</c> table.
		/// </summary>
		/// <returns>An array of <see cref="EndPointRow"/> objects.</returns>
		public virtual EndPointRow[] GetAll()
		{
			return MapRecords(CreateGetAllCommand());
		}

		/// <summary>
		/// Gets a <see cref="System.Data.DataTable"/> object that 
		/// includes all records from the <c>EndPoint</c> table.
		/// </summary>
		/// <returns>A reference to the <see cref="System.Data.DataTable"/> object.</returns>
		public virtual DataTable GetAllAsDataTable()
		{
			return MapRecordsToDataTable(CreateGetAllCommand());
		}

		/// <summary>
		/// Creates and returns an <see cref="System.Data.IDbCommand"/> object that is used
		/// to retrieve all records from the <c>EndPoint</c> table.
		/// </summary>
		/// <returns>A reference to the <see cref="System.Data.IDbCommand"/> object.</returns>
		protected virtual IDbCommand CreateGetAllCommand()
		{
			return CreateGetCommand(null, null);
		}

		/// <summary>
		/// Gets the first <see cref="EndPointRow"/> objects that 
		/// match the search condition.
		/// </summary>
		/// <param name="whereSql">The SQL search condition. For example: 
		/// <c>"FirstName='Smith' AND Zip=75038"</c>.</param>
		/// <returns>An instance of <see cref="EndPointRow"/> or null reference 
		/// (Nothing in Visual Basic) if the object was not found.</returns>
		public EndPointRow GetRow(string whereSql)
		{
			int totalRecordCount = -1;
			EndPointRow[] rows = GetAsArray(whereSql, null, 0, 1, ref totalRecordCount);
			return 0 == rows.Length ? null : rows[0];
		}

		/// <summary>
		/// Gets an array of <see cref="EndPointRow"/> objects that 
		/// match the search condition, in the the specified sort order.
		/// </summary>
		/// <param name="whereSql">The SQL search condition. For example: 
		/// <c>"FirstName='Smith' AND Zip=75038"</c>.</param>
		/// <param name="orderBySql">The column name(s) followed by "ASC" (ascending) or "DESC" (descending).
		/// Columns are sorted in ascending order by default. For example: <c>"LastName ASC, FirstName ASC"</c>.</param>
		/// <returns>An array of <see cref="EndPointRow"/> objects.</returns>
		public EndPointRow[] GetAsArray(string whereSql, string orderBySql)
		{
			int totalRecordCount = -1;
			return GetAsArray(whereSql, orderBySql, 0, int.MaxValue, ref totalRecordCount);
		}

		/// <summary>
		/// Gets an array of <see cref="EndPointRow"/> objects that 
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
		/// <returns>An array of <see cref="EndPointRow"/> objects.</returns>
		public virtual EndPointRow[] GetAsArray(string whereSql, string orderBySql,
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
			var sql = "SELECT * FROM [dbo].[EndPoint]";
			if(!string.IsNullOrEmpty(whereSql))
				sql += " WHERE " + whereSql;
			if(!string.IsNullOrEmpty(orderBySql))
				sql += " ORDER BY " + orderBySql;
			return _db.CreateCommand(sql);
		}

		/// <summary>
		/// Gets <see cref="EndPointRow"/> by the primary key.
		/// </summary>
		/// <param name="end_point_id">The <c>end_point_id</c> column value.</param>
		/// <returns>An instance of <see cref="EndPointRow"/> or null reference 
		/// (Nothing in Visual Basic) if the object was not found.</returns>
		public virtual EndPointRow GetByPrimaryKey(short end_point_id)
		{
			var whereSql = "[end_point_id]=" + _db.CreateSqlParameterName("End_point_id");
			var cmd = CreateGetCommand(whereSql, null);
			AddParameter(cmd, "End_point_id", end_point_id);
			var tempArray = MapRecords(cmd);
			return 0 == tempArray.Length ? null : tempArray[0];
		}

		/// <summary>
		/// Gets an array of <see cref="EndPointRow"/> objects 
		/// by the <c>R_165</c> foreign key.
		/// </summary>
		/// <param name="prefix_in_type_id">The <c>prefix_in_type_id</c> column value.</param>
		/// <returns>An array of <see cref="EndPointRow"/> objects.</returns>
		public virtual EndPointRow[] GetByPrefix_in_type_id(short prefix_in_type_id)
		{
			return MapRecords(CreateGetByPrefix_in_type_idCommand(prefix_in_type_id));
		}

		/// <summary>
		/// Gets a <see cref="System.Data.DataTable"/> object 
		/// by the <c>R_165</c> foreign key.
		/// </summary>
		/// <param name="prefix_in_type_id">The <c>prefix_in_type_id</c> column value.</param>
		/// <returns>A reference to the <see cref="System.Data.DataTable"/> object.</returns>
		public virtual DataTable GetByPrefix_in_type_idAsDataTable(short prefix_in_type_id)
		{
			return MapRecordsToDataTable(CreateGetByPrefix_in_type_idCommand(prefix_in_type_id));
		}

		/// <summary>
		/// Creates an <see cref="System.Data.IDbCommand"/> object that can be used to 
		/// return records by the <c>R_165</c> foreign key.
		/// </summary>
		/// <param name="prefix_in_type_id">The <c>prefix_in_type_id</c> column value.</param>
		/// <returns>A reference to the <see cref="System.Data.IDbCommand"/> object.</returns>
		protected virtual IDbCommand CreateGetByPrefix_in_type_idCommand(short prefix_in_type_id)
		{
			string whereSql = "";
			whereSql += "[prefix_in_type_id]=" + _db.CreateSqlParameterName("Prefix_in_type_id");

			IDbCommand cmd = CreateGetCommand(whereSql, null);
			AddParameter(cmd, "Prefix_in_type_id", prefix_in_type_id);
			return cmd;
		}

		/// <summary>
		/// Gets an array of <see cref="EndPointRow"/> objects 
		/// by the <c>R_300</c> foreign key.
		/// </summary>
		/// <param name="virtual_switch_id">The <c>virtual_switch_id</c> column value.</param>
		/// <returns>An array of <see cref="EndPointRow"/> objects.</returns>
		public virtual EndPointRow[] GetByVirtual_switch_id(int virtual_switch_id)
		{
			return MapRecords(CreateGetByVirtual_switch_idCommand(virtual_switch_id));
		}

		/// <summary>
		/// Gets a <see cref="System.Data.DataTable"/> object 
		/// by the <c>R_300</c> foreign key.
		/// </summary>
		/// <param name="virtual_switch_id">The <c>virtual_switch_id</c> column value.</param>
		/// <returns>A reference to the <see cref="System.Data.DataTable"/> object.</returns>
		public virtual DataTable GetByVirtual_switch_idAsDataTable(int virtual_switch_id)
		{
			return MapRecordsToDataTable(CreateGetByVirtual_switch_idCommand(virtual_switch_id));
		}

		/// <summary>
		/// Creates an <see cref="System.Data.IDbCommand"/> object that can be used to 
		/// return records by the <c>R_300</c> foreign key.
		/// </summary>
		/// <param name="virtual_switch_id">The <c>virtual_switch_id</c> column value.</param>
		/// <returns>A reference to the <see cref="System.Data.IDbCommand"/> object.</returns>
		protected virtual IDbCommand CreateGetByVirtual_switch_idCommand(int virtual_switch_id)
		{
			string whereSql = "";
			whereSql += "[virtual_switch_id]=" + _db.CreateSqlParameterName("Virtual_switch_id");

			IDbCommand cmd = CreateGetCommand(whereSql, null);
			AddParameter(cmd, "Virtual_switch_id", virtual_switch_id);
			return cmd;
		}

		/// <summary>
		/// Adds a new record into the <c>EndPoint</c> table.
		/// </summary>
		/// <param name="value">The <see cref="EndPointRow"/> object to be inserted.</param>
		public virtual void Insert(EndPointRow value)
		{
			string sqlStr = "INSERT INTO [dbo].[EndPoint] (" +
				"[end_point_id], " +
				"[alias], " +
				"[with_alias_authentication], " +
				"[status], " +
				"[type], " +
				"[protocol], " +
				"[port], " +
				"[registration], " +
				"[is_registered], " +
				"[ip_address_range], " +
				"[max_calls], " +
				"[password], " +
				"[prefix_in_type_id], " +
				"[virtual_switch_id]" +
				") VALUES (" +
				_db.CreateSqlParameterName("End_point_id") + ", " +
				_db.CreateSqlParameterName("Alias") + ", " +
				_db.CreateSqlParameterName("With_alias_authentication") + ", " +
				_db.CreateSqlParameterName("Status") + ", " +
				_db.CreateSqlParameterName("Type") + ", " +
				_db.CreateSqlParameterName("Protocol") + ", " +
				_db.CreateSqlParameterName("Port") + ", " +
				_db.CreateSqlParameterName("Registration") + ", " +
				_db.CreateSqlParameterName("Is_registered") + ", " +
				_db.CreateSqlParameterName("Ip_address_range") + ", " +
				_db.CreateSqlParameterName("Max_calls") + ", " +
				_db.CreateSqlParameterName("Password") + ", " +
				_db.CreateSqlParameterName("Prefix_in_type_id") + ", " +
				_db.CreateSqlParameterName("Virtual_switch_id") + ")";
			IDbCommand cmd = _db.CreateCommand(sqlStr);
			AddParameter(cmd, "End_point_id", value.End_point_id);
			AddParameter(cmd, "Alias", value.Alias);
			AddParameter(cmd, "With_alias_authentication", value.With_alias_authentication);
			AddParameter(cmd, "Status", value.Status);
			AddParameter(cmd, "Type", value.Type);
			AddParameter(cmd, "Protocol", value.Protocol);
			AddParameter(cmd, "Port", value.Port);
			AddParameter(cmd, "Registration", value.Registration);
			AddParameter(cmd, "Is_registered", value.Is_registered);
			AddParameter(cmd, "Ip_address_range", value.Ip_address_range);
			AddParameter(cmd, "Max_calls", value.Max_calls);
			AddParameter(cmd, "Password", value.Password);
			AddParameter(cmd, "Prefix_in_type_id", value.Prefix_in_type_id);
			AddParameter(cmd, "Virtual_switch_id", value.Virtual_switch_id);
			cmd.ExecuteNonQuery();
		}

		/// <summary>
		/// Updates a record in the <c>EndPoint</c> table.
		/// </summary>
		/// <param name="value">The <see cref="EndPointRow"/>
		/// object used to update the table record.</param>
		/// <returns>true if the record was updated; otherwise, false.</returns>
		public virtual bool Update(EndPointRow value)
		{
			string sqlStr = "UPDATE [dbo].[EndPoint] SET " +
				"[alias]=" + _db.CreateSqlParameterName("Alias") + ", " +
				"[with_alias_authentication]=" + _db.CreateSqlParameterName("With_alias_authentication") + ", " +
				"[status]=" + _db.CreateSqlParameterName("Status") + ", " +
				"[type]=" + _db.CreateSqlParameterName("Type") + ", " +
				"[protocol]=" + _db.CreateSqlParameterName("Protocol") + ", " +
				"[port]=" + _db.CreateSqlParameterName("Port") + ", " +
				"[registration]=" + _db.CreateSqlParameterName("Registration") + ", " +
				"[is_registered]=" + _db.CreateSqlParameterName("Is_registered") + ", " +
				"[ip_address_range]=" + _db.CreateSqlParameterName("Ip_address_range") + ", " +
				"[max_calls]=" + _db.CreateSqlParameterName("Max_calls") + ", " +
				"[password]=" + _db.CreateSqlParameterName("Password") + ", " +
				"[prefix_in_type_id]=" + _db.CreateSqlParameterName("Prefix_in_type_id") + ", " +
				"[virtual_switch_id]=" + _db.CreateSqlParameterName("Virtual_switch_id") +
				" WHERE " +
				"[end_point_id]=" + _db.CreateSqlParameterName("End_point_id");
			IDbCommand cmd = _db.CreateCommand(sqlStr);
			AddParameter(cmd, "Alias", value.Alias);
			AddParameter(cmd, "With_alias_authentication", value.With_alias_authentication);
			AddParameter(cmd, "Status", value.Status);
			AddParameter(cmd, "Type", value.Type);
			AddParameter(cmd, "Protocol", value.Protocol);
			AddParameter(cmd, "Port", value.Port);
			AddParameter(cmd, "Registration", value.Registration);
			AddParameter(cmd, "Is_registered", value.Is_registered);
			AddParameter(cmd, "Ip_address_range", value.Ip_address_range);
			AddParameter(cmd, "Max_calls", value.Max_calls);
			AddParameter(cmd, "Password", value.Password);
			AddParameter(cmd, "Prefix_in_type_id", value.Prefix_in_type_id);
			AddParameter(cmd, "Virtual_switch_id", value.Virtual_switch_id);
			AddParameter(cmd, "End_point_id", value.End_point_id);
			return 0 != cmd.ExecuteNonQuery();
		}

		/// <summary>
		/// Updates the <c>EndPoint</c> table and calls the <c>AcceptChanges</c> method
		/// on the changed DataRow objects.
		/// </summary>
		/// <param name="table">The <see cref="System.Data.DataTable"/> used to update the data source.</param>
		public void Update(DataTable table)
		{
			Update(table, true);
		}

		/// <summary>
		/// Updates the <c>EndPoint</c> table. Pass <c>false</c> as the <c>acceptChanges</c> 
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
							DeleteByPrimaryKey((short)row["End_point_id"]);
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
		/// Deletes the specified object from the <c>EndPoint</c> table.
		/// </summary>
		/// <param name="value">The <see cref="EndPointRow"/> object to delete.</param>
		/// <returns>true if the record was deleted; otherwise, false.</returns>
		public bool Delete(EndPointRow value)
		{
			return DeleteByPrimaryKey(value.End_point_id);
		}

		/// <summary>
		/// Deletes a record from the <c>EndPoint</c> table using
		/// the specified primary key.
		/// </summary>
		/// <param name="end_point_id">The <c>end_point_id</c> column value.</param>
		/// <returns>true if the record was deleted; otherwise, false.</returns>
		public virtual bool DeleteByPrimaryKey(short end_point_id)
		{
			string whereSql = "[end_point_id]=" + _db.CreateSqlParameterName("End_point_id");
			IDbCommand cmd = CreateDeleteCommand(whereSql);
			AddParameter(cmd, "End_point_id", end_point_id);
			return 0 < cmd.ExecuteNonQuery();
		}

		/// <summary>
		/// Deletes records from the <c>EndPoint</c> table using the 
		/// <c>R_165</c> foreign key.
		/// </summary>
		/// <param name="prefix_in_type_id">The <c>prefix_in_type_id</c> column value.</param>
		/// <returns>The number of records deleted from the table.</returns>
		public int DeleteByPrefix_in_type_id(short prefix_in_type_id)
		{
			return CreateDeleteByPrefix_in_type_idCommand(prefix_in_type_id).ExecuteNonQuery();
		}

		/// <summary>
		/// Creates an <see cref="System.Data.IDbCommand"/> object that can be used to
		/// delete records using the <c>R_165</c> foreign key.
		/// </summary>
		/// <param name="prefix_in_type_id">The <c>prefix_in_type_id</c> column value.</param>
		/// <returns>A reference to the <see cref="System.Data.IDbCommand"/> object.</returns>
		protected virtual IDbCommand CreateDeleteByPrefix_in_type_idCommand(short prefix_in_type_id)
		{
			string whereSql = "";
			whereSql += "[prefix_in_type_id]=" + _db.CreateSqlParameterName("Prefix_in_type_id");

			IDbCommand cmd = CreateDeleteCommand(whereSql);
			AddParameter(cmd, "Prefix_in_type_id", prefix_in_type_id);
			return cmd;
		}

		/// <summary>
		/// Deletes records from the <c>EndPoint</c> table using the 
		/// <c>R_300</c> foreign key.
		/// </summary>
		/// <param name="virtual_switch_id">The <c>virtual_switch_id</c> column value.</param>
		/// <returns>The number of records deleted from the table.</returns>
		public int DeleteByVirtual_switch_id(int virtual_switch_id)
		{
			return CreateDeleteByVirtual_switch_idCommand(virtual_switch_id).ExecuteNonQuery();
		}

		/// <summary>
		/// Creates an <see cref="System.Data.IDbCommand"/> object that can be used to
		/// delete records using the <c>R_300</c> foreign key.
		/// </summary>
		/// <param name="virtual_switch_id">The <c>virtual_switch_id</c> column value.</param>
		/// <returns>A reference to the <see cref="System.Data.IDbCommand"/> object.</returns>
		protected virtual IDbCommand CreateDeleteByVirtual_switch_idCommand(int virtual_switch_id)
		{
			string whereSql = "";
			whereSql += "[virtual_switch_id]=" + _db.CreateSqlParameterName("Virtual_switch_id");

			IDbCommand cmd = CreateDeleteCommand(whereSql);
			AddParameter(cmd, "Virtual_switch_id", virtual_switch_id);
			return cmd;
		}

		/// <summary>
		/// Deletes <c>EndPoint</c> records that match the specified criteria.
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
		/// to delete <c>EndPoint</c> records that match the specified criteria.
		/// </summary>
		/// <param name="whereSql">The SQL search condition. 
		/// For example: <c>"FirstName='Smith' AND Zip=75038"</c>.</param>
		/// <returns>A reference to the <see cref="System.Data.IDbCommand"/> object.</returns>
		protected virtual IDbCommand CreateDeleteCommand(string whereSql)
		{
			string sql = "DELETE FROM [dbo].[EndPoint]";
			if(null != whereSql && 0 < whereSql.Length)
				sql += " WHERE " + whereSql;
			return _db.CreateCommand(sql);
		}

		/// <summary>
		/// Deletes all records from the <c>EndPoint</c> table.
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
		/// <returns>An array of <see cref="EndPointRow"/> objects.</returns>
		protected EndPointRow[] MapRecords(IDbCommand command)
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
		/// <returns>An array of <see cref="EndPointRow"/> objects.</returns>
		protected EndPointRow[] MapRecords(IDataReader reader)
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
		/// <returns>An array of <see cref="EndPointRow"/> objects.</returns>
		protected virtual EndPointRow[] MapRecords(IDataReader reader, 
										int startIndex, int length, ref int totalRecordCount)
		{
			if(0 > startIndex)
				throw new ArgumentOutOfRangeException("startIndex", startIndex, "StartIndex cannot be less than zero.");
			if(0 > length)
				throw new ArgumentOutOfRangeException("length", length, "Length cannot be less than zero.");

			int end_point_idColumnIndex = reader.GetOrdinal("end_point_id");
			int aliasColumnIndex = reader.GetOrdinal("alias");
			int with_alias_authenticationColumnIndex = reader.GetOrdinal("with_alias_authentication");
			int statusColumnIndex = reader.GetOrdinal("status");
			int typeColumnIndex = reader.GetOrdinal("type");
			int protocolColumnIndex = reader.GetOrdinal("protocol");
			int portColumnIndex = reader.GetOrdinal("port");
			int registrationColumnIndex = reader.GetOrdinal("registration");
			int is_registeredColumnIndex = reader.GetOrdinal("is_registered");
			int ip_address_rangeColumnIndex = reader.GetOrdinal("ip_address_range");
			int max_callsColumnIndex = reader.GetOrdinal("max_calls");
			int passwordColumnIndex = reader.GetOrdinal("password");
			int prefix_in_type_idColumnIndex = reader.GetOrdinal("prefix_in_type_id");
			int virtual_switch_idColumnIndex = reader.GetOrdinal("virtual_switch_id");

			System.Collections.ArrayList recordList = new System.Collections.ArrayList();
			int ri = -startIndex;
			while(reader.Read())
			{
				ri++;
				if(ri > 0 && ri <= length)
				{
					EndPointRow record = new EndPointRow();
					recordList.Add(record);

					record.End_point_id = Convert.ToInt16(reader.GetValue(end_point_idColumnIndex));
					record.Alias = Convert.ToString(reader.GetValue(aliasColumnIndex));
					record.With_alias_authentication = Convert.ToByte(reader.GetValue(with_alias_authenticationColumnIndex));
					record.Status = Convert.ToByte(reader.GetValue(statusColumnIndex));
					record.Type = Convert.ToByte(reader.GetValue(typeColumnIndex));
					record.Protocol = Convert.ToByte(reader.GetValue(protocolColumnIndex));
					record.Port = Convert.ToInt32(reader.GetValue(portColumnIndex));
					record.Registration = Convert.ToByte(reader.GetValue(registrationColumnIndex));
					record.Is_registered = Convert.ToByte(reader.GetValue(is_registeredColumnIndex));
					record.Ip_address_range = Convert.ToString(reader.GetValue(ip_address_rangeColumnIndex));
					record.Max_calls = Convert.ToInt32(reader.GetValue(max_callsColumnIndex));
					record.Password = Convert.ToString(reader.GetValue(passwordColumnIndex));
					record.Prefix_in_type_id = Convert.ToInt16(reader.GetValue(prefix_in_type_idColumnIndex));
					record.Virtual_switch_id = Convert.ToInt32(reader.GetValue(virtual_switch_idColumnIndex));

					if(ri == length && 0 != totalRecordCount)
						break;
				}
			}

			totalRecordCount = 0 == totalRecordCount ? ri + startIndex : -1;
			return (EndPointRow[])(recordList.ToArray(typeof(EndPointRow)));
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
		/// Converts <see cref="System.Data.DataRow"/> to <see cref="EndPointRow"/>.
		/// </summary>
		/// <param name="row">The <see cref="System.Data.DataRow"/> object to be mapped.</param>
		/// <returns>A reference to the <see cref="EndPointRow"/> object.</returns>
		protected virtual EndPointRow MapRow(DataRow row)
		{
			EndPointRow mappedObject = new EndPointRow();
			DataTable dataTable = row.Table;
			DataColumn dataColumn;
			// Column "End_point_id"
			dataColumn = dataTable.Columns["End_point_id"];
			if(!row.IsNull(dataColumn))
				mappedObject.End_point_id = (short)row[dataColumn];
			// Column "Alias"
			dataColumn = dataTable.Columns["Alias"];
			if(!row.IsNull(dataColumn))
				mappedObject.Alias = (string)row[dataColumn];
			// Column "With_alias_authentication"
			dataColumn = dataTable.Columns["With_alias_authentication"];
			if(!row.IsNull(dataColumn))
				mappedObject.With_alias_authentication = (byte)row[dataColumn];
			// Column "Status"
			dataColumn = dataTable.Columns["Status"];
			if(!row.IsNull(dataColumn))
				mappedObject.Status = (byte)row[dataColumn];
			// Column "Type"
			dataColumn = dataTable.Columns["Type"];
			if(!row.IsNull(dataColumn))
				mappedObject.Type = (byte)row[dataColumn];
			// Column "Protocol"
			dataColumn = dataTable.Columns["Protocol"];
			if(!row.IsNull(dataColumn))
				mappedObject.Protocol = (byte)row[dataColumn];
			// Column "Port"
			dataColumn = dataTable.Columns["Port"];
			if(!row.IsNull(dataColumn))
				mappedObject.Port = (int)row[dataColumn];
			// Column "Registration"
			dataColumn = dataTable.Columns["Registration"];
			if(!row.IsNull(dataColumn))
				mappedObject.Registration = (byte)row[dataColumn];
			// Column "Is_registered"
			dataColumn = dataTable.Columns["Is_registered"];
			if(!row.IsNull(dataColumn))
				mappedObject.Is_registered = (byte)row[dataColumn];
			// Column "Ip_address_range"
			dataColumn = dataTable.Columns["Ip_address_range"];
			if(!row.IsNull(dataColumn))
				mappedObject.Ip_address_range = (string)row[dataColumn];
			// Column "Max_calls"
			dataColumn = dataTable.Columns["Max_calls"];
			if(!row.IsNull(dataColumn))
				mappedObject.Max_calls = (int)row[dataColumn];
			// Column "Password"
			dataColumn = dataTable.Columns["Password"];
			if(!row.IsNull(dataColumn))
				mappedObject.Password = (string)row[dataColumn];
			// Column "Prefix_in_type_id"
			dataColumn = dataTable.Columns["Prefix_in_type_id"];
			if(!row.IsNull(dataColumn))
				mappedObject.Prefix_in_type_id = (short)row[dataColumn];
			// Column "Virtual_switch_id"
			dataColumn = dataTable.Columns["Virtual_switch_id"];
			if(!row.IsNull(dataColumn))
				mappedObject.Virtual_switch_id = (int)row[dataColumn];
			return mappedObject;
		}

		/// <summary>
		/// Creates a <see cref="System.Data.DataTable"/> object for 
		/// the <c>EndPoint</c> table.
		/// </summary>
		/// <returns>A reference to the <see cref="System.Data.DataTable"/> object.</returns>
		protected virtual DataTable CreateDataTable()
		{
			DataTable dataTable = new DataTable();
			dataTable.TableName = "EndPoint";
			DataColumn dataColumn;
			dataColumn = dataTable.Columns.Add("End_point_id", typeof(short));
			dataColumn.Caption = "end_point_id";
			dataColumn.AllowDBNull = false;
			dataColumn = dataTable.Columns.Add("Alias", typeof(string));
			dataColumn.Caption = "alias";
			dataColumn.MaxLength = 50;
			dataColumn.AllowDBNull = false;
			dataColumn = dataTable.Columns.Add("With_alias_authentication", typeof(byte));
			dataColumn.Caption = "with_alias_authentication";
			dataColumn.AllowDBNull = false;
			dataColumn = dataTable.Columns.Add("Status", typeof(byte));
			dataColumn.Caption = "status";
			dataColumn.AllowDBNull = false;
			dataColumn = dataTable.Columns.Add("Type", typeof(byte));
			dataColumn.Caption = "type";
			dataColumn.AllowDBNull = false;
			dataColumn = dataTable.Columns.Add("Protocol", typeof(byte));
			dataColumn.Caption = "protocol";
			dataColumn.AllowDBNull = false;
			dataColumn = dataTable.Columns.Add("Port", typeof(int));
			dataColumn.Caption = "port";
			dataColumn.AllowDBNull = false;
			dataColumn = dataTable.Columns.Add("Registration", typeof(byte));
			dataColumn.Caption = "registration";
			dataColumn.AllowDBNull = false;
			dataColumn = dataTable.Columns.Add("Is_registered", typeof(byte));
			dataColumn.Caption = "is_registered";
			dataColumn.AllowDBNull = false;
			dataColumn = dataTable.Columns.Add("Ip_address_range", typeof(string));
			dataColumn.Caption = "ip_address_range";
			dataColumn.MaxLength = 19;
			dataColumn.AllowDBNull = false;
			dataColumn = dataTable.Columns.Add("Max_calls", typeof(int));
			dataColumn.Caption = "max_calls";
			dataColumn.AllowDBNull = false;
			dataColumn = dataTable.Columns.Add("Password", typeof(string));
			dataColumn.Caption = "password";
			dataColumn.MaxLength = 16;
			dataColumn.AllowDBNull = false;
			dataColumn = dataTable.Columns.Add("Prefix_in_type_id", typeof(short));
			dataColumn.Caption = "prefix_in_type_id";
			dataColumn.AllowDBNull = false;
			dataColumn = dataTable.Columns.Add("Virtual_switch_id", typeof(int));
			dataColumn.Caption = "virtual_switch_id";
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
				case "End_point_id":
					parameter = _db.AddParameter(cmd, paramName, DbType.Int16, value);
					break;

				case "Alias":
					parameter = _db.AddParameter(cmd, paramName, DbType.AnsiString, value);
					break;

				case "With_alias_authentication":
					parameter = _db.AddParameter(cmd, paramName, DbType.Byte, value);
					break;

				case "Status":
					parameter = _db.AddParameter(cmd, paramName, DbType.Byte, value);
					break;

				case "Type":
					parameter = _db.AddParameter(cmd, paramName, DbType.Byte, value);
					break;

				case "Protocol":
					parameter = _db.AddParameter(cmd, paramName, DbType.Byte, value);
					break;

				case "Port":
					parameter = _db.AddParameter(cmd, paramName, DbType.Int32, value);
					break;

				case "Registration":
					parameter = _db.AddParameter(cmd, paramName, DbType.Byte, value);
					break;

				case "Is_registered":
					parameter = _db.AddParameter(cmd, paramName, DbType.Byte, value);
					break;

				case "Ip_address_range":
					parameter = _db.AddParameter(cmd, paramName, DbType.AnsiString, value);
					break;

				case "Max_calls":
					parameter = _db.AddParameter(cmd, paramName, DbType.Int32, value);
					break;

				case "Password":
					parameter = _db.AddParameter(cmd, paramName, DbType.AnsiString, value);
					break;

				case "Prefix_in_type_id":
					parameter = _db.AddParameter(cmd, paramName, DbType.Int16, value);
					break;

				case "Virtual_switch_id":
					parameter = _db.AddParameter(cmd, paramName, DbType.Int32, value);
					break;

				default:
					throw new ArgumentException("Unknown parameter name (" + paramName + ").");
			}
			return parameter;
		}

	} // End of EndPointCollection_Base class
}  // End of namespace

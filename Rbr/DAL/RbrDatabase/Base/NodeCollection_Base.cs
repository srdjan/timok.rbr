// <fileinfo name="Base\NodeCollection_Base.cs">
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
	/// The base class for <see cref="NodeCollection"/>. Provides methods 
	/// for common database table operations. 
	/// </summary>
	/// <remarks>
	/// Do not change this source code. Update the <see cref="NodeCollection"/>
	/// class if you need to add or change some functionality.
	/// </remarks>
	public abstract class NodeCollection_Base
	{
		// Constants
		public const string Node_idColumnName = "node_id";
		public const string Platform_idColumnName = "platform_id";
		public const string DescriptionColumnName = "description";
		public const string Node_configColumnName = "node_config";
		public const string Transport_typeColumnName = "transport_type";
		public const string User_nameColumnName = "user_name";
		public const string PasswordColumnName = "password";
		public const string Ip_addressColumnName = "ip_address";
		public const string PortColumnName = "port";
		public const string StatusColumnName = "status";
		public const string Billing_export_frequencyColumnName = "billing_export_frequency";
		public const string Cdr_publishing_frequencyColumnName = "cdr_publishing_frequency";

		// Instance fields
		private Rbr_Db _db;

		/// <summary>
		/// Initializes a new instance of the <see cref="NodeCollection_Base"/> 
		/// class with the specified <see cref="Rbr_Db"/>.
		/// </summary>
		/// <param name="db">The <see cref="Rbr_Db"/> object.</param>
		public NodeCollection_Base(Rbr_Db db)
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
		/// Gets an array of all records from the <c>Node</c> table.
		/// </summary>
		/// <returns>An array of <see cref="NodeRow"/> objects.</returns>
		public virtual NodeRow[] GetAll()
		{
			return MapRecords(CreateGetAllCommand());
		}

		/// <summary>
		/// Gets a <see cref="System.Data.DataTable"/> object that 
		/// includes all records from the <c>Node</c> table.
		/// </summary>
		/// <returns>A reference to the <see cref="System.Data.DataTable"/> object.</returns>
		public virtual DataTable GetAllAsDataTable()
		{
			return MapRecordsToDataTable(CreateGetAllCommand());
		}

		/// <summary>
		/// Creates and returns an <see cref="System.Data.IDbCommand"/> object that is used
		/// to retrieve all records from the <c>Node</c> table.
		/// </summary>
		/// <returns>A reference to the <see cref="System.Data.IDbCommand"/> object.</returns>
		protected virtual IDbCommand CreateGetAllCommand()
		{
			return CreateGetCommand(null, null);
		}

		/// <summary>
		/// Gets the first <see cref="NodeRow"/> objects that 
		/// match the search condition.
		/// </summary>
		/// <param name="whereSql">The SQL search condition. For example: 
		/// <c>"FirstName='Smith' AND Zip=75038"</c>.</param>
		/// <returns>An instance of <see cref="NodeRow"/> or null reference 
		/// (Nothing in Visual Basic) if the object was not found.</returns>
		public NodeRow GetRow(string whereSql)
		{
			int totalRecordCount = -1;
			NodeRow[] rows = GetAsArray(whereSql, null, 0, 1, ref totalRecordCount);
			return 0 == rows.Length ? null : rows[0];
		}

		/// <summary>
		/// Gets an array of <see cref="NodeRow"/> objects that 
		/// match the search condition, in the the specified sort order.
		/// </summary>
		/// <param name="whereSql">The SQL search condition. For example: 
		/// <c>"FirstName='Smith' AND Zip=75038"</c>.</param>
		/// <param name="orderBySql">The column name(s) followed by "ASC" (ascending) or "DESC" (descending).
		/// Columns are sorted in ascending order by default. For example: <c>"LastName ASC, FirstName ASC"</c>.</param>
		/// <returns>An array of <see cref="NodeRow"/> objects.</returns>
		public NodeRow[] GetAsArray(string whereSql, string orderBySql)
		{
			int totalRecordCount = -1;
			return GetAsArray(whereSql, orderBySql, 0, int.MaxValue, ref totalRecordCount);
		}

		/// <summary>
		/// Gets an array of <see cref="NodeRow"/> objects that 
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
		/// <returns>An array of <see cref="NodeRow"/> objects.</returns>
		public virtual NodeRow[] GetAsArray(string whereSql, string orderBySql,
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
			string sql = "SELECT * FROM [dbo].[Node]";
			if(null != whereSql && 0 < whereSql.Length)
				sql += " WHERE " + whereSql;
			if(null != orderBySql && 0 < orderBySql.Length)
				sql += " ORDER BY " + orderBySql;
			return _db.CreateCommand(sql);
		}

		/// <summary>
		/// Gets <see cref="NodeRow"/> by the primary key.
		/// </summary>
		/// <param name="node_id">The <c>node_id</c> column value.</param>
		/// <returns>An instance of <see cref="NodeRow"/> or null reference 
		/// (Nothing in Visual Basic) if the object was not found.</returns>
		public virtual NodeRow GetByPrimaryKey(short node_id)
		{
			string whereSql = "[node_id]=" + _db.CreateSqlParameterName("Node_id");
			IDbCommand cmd = CreateGetCommand(whereSql, null);
			AddParameter(cmd, "Node_id", node_id);
			NodeRow[] tempArray = MapRecords(cmd);
			return 0 == tempArray.Length ? null : tempArray[0];
		}

		/// <summary>
		/// Gets an array of <see cref="NodeRow"/> objects 
		/// by the <c>R_10</c> foreign key.
		/// </summary>
		/// <param name="platform_id">The <c>platform_id</c> column value.</param>
		/// <returns>An array of <see cref="NodeRow"/> objects.</returns>
		public virtual NodeRow[] GetByPlatform_id(short platform_id)
		{
			return MapRecords(CreateGetByPlatform_idCommand(platform_id));
		}

		/// <summary>
		/// Gets a <see cref="System.Data.DataTable"/> object 
		/// by the <c>R_10</c> foreign key.
		/// </summary>
		/// <param name="platform_id">The <c>platform_id</c> column value.</param>
		/// <returns>A reference to the <see cref="System.Data.DataTable"/> object.</returns>
		public virtual DataTable GetByPlatform_idAsDataTable(short platform_id)
		{
			return MapRecordsToDataTable(CreateGetByPlatform_idCommand(platform_id));
		}

		/// <summary>
		/// Creates an <see cref="System.Data.IDbCommand"/> object that can be used to 
		/// return records by the <c>R_10</c> foreign key.
		/// </summary>
		/// <param name="platform_id">The <c>platform_id</c> column value.</param>
		/// <returns>A reference to the <see cref="System.Data.IDbCommand"/> object.</returns>
		protected virtual IDbCommand CreateGetByPlatform_idCommand(short platform_id)
		{
			string whereSql = "";
			whereSql += "[platform_id]=" + _db.CreateSqlParameterName("Platform_id");

			IDbCommand cmd = CreateGetCommand(whereSql, null);
			AddParameter(cmd, "Platform_id", platform_id);
			return cmd;
		}

		/// <summary>
		/// Adds a new record into the <c>Node</c> table.
		/// </summary>
		/// <param name="value">The <see cref="NodeRow"/> object to be inserted.</param>
		public virtual void Insert(NodeRow value)
		{
			string sqlStr = "INSERT INTO [dbo].[Node] (" +
				"[node_id], " +
				"[platform_id], " +
				"[description], " +
				"[node_config], " +
				"[transport_type], " +
				"[user_name], " +
				"[password], " +
				"[ip_address], " +
				"[port], " +
				"[status], " +
				"[billing_export_frequency], " +
				"[cdr_publishing_frequency]" +
				") VALUES (" +
				_db.CreateSqlParameterName("Node_id") + ", " +
				_db.CreateSqlParameterName("Platform_id") + ", " +
				_db.CreateSqlParameterName("Description") + ", " +
				_db.CreateSqlParameterName("Node_config") + ", " +
				_db.CreateSqlParameterName("Transport_type") + ", " +
				_db.CreateSqlParameterName("User_name") + ", " +
				_db.CreateSqlParameterName("Password") + ", " +
				_db.CreateSqlParameterName("Ip_address") + ", " +
				_db.CreateSqlParameterName("Port") + ", " +
				_db.CreateSqlParameterName("Status") + ", " +
				_db.CreateSqlParameterName("Billing_export_frequency") + ", " +
				_db.CreateSqlParameterName("Cdr_publishing_frequency") + ")";
			IDbCommand cmd = _db.CreateCommand(sqlStr);
			AddParameter(cmd, "Node_id", value.Node_id);
			AddParameter(cmd, "Platform_id", value.Platform_id);
			AddParameter(cmd, "Description", value.Description);
			AddParameter(cmd, "Node_config", value.Node_config);
			AddParameter(cmd, "Transport_type", value.Transport_type);
			AddParameter(cmd, "User_name", value.User_name);
			AddParameter(cmd, "Password", value.Password);
			AddParameter(cmd, "Ip_address", value.Ip_address);
			AddParameter(cmd, "Port", value.Port);
			AddParameter(cmd, "Status", value.Status);
			AddParameter(cmd, "Billing_export_frequency", value.Billing_export_frequency);
			AddParameter(cmd, "Cdr_publishing_frequency", value.Cdr_publishing_frequency);
			cmd.ExecuteNonQuery();
		}

		/// <summary>
		/// Updates a record in the <c>Node</c> table.
		/// </summary>
		/// <param name="value">The <see cref="NodeRow"/>
		/// object used to update the table record.</param>
		/// <returns>true if the record was updated; otherwise, false.</returns>
		public virtual bool Update(NodeRow value)
		{
			string sqlStr = "UPDATE [dbo].[Node] SET " +
				"[platform_id]=" + _db.CreateSqlParameterName("Platform_id") + ", " +
				"[description]=" + _db.CreateSqlParameterName("Description") + ", " +
				"[node_config]=" + _db.CreateSqlParameterName("Node_config") + ", " +
				"[transport_type]=" + _db.CreateSqlParameterName("Transport_type") + ", " +
				"[user_name]=" + _db.CreateSqlParameterName("User_name") + ", " +
				"[password]=" + _db.CreateSqlParameterName("Password") + ", " +
				"[ip_address]=" + _db.CreateSqlParameterName("Ip_address") + ", " +
				"[port]=" + _db.CreateSqlParameterName("Port") + ", " +
				"[status]=" + _db.CreateSqlParameterName("Status") + ", " +
				"[billing_export_frequency]=" + _db.CreateSqlParameterName("Billing_export_frequency") + ", " +
				"[cdr_publishing_frequency]=" + _db.CreateSqlParameterName("Cdr_publishing_frequency") +
				" WHERE " +
				"[node_id]=" + _db.CreateSqlParameterName("Node_id");
			IDbCommand cmd = _db.CreateCommand(sqlStr);
			AddParameter(cmd, "Platform_id", value.Platform_id);
			AddParameter(cmd, "Description", value.Description);
			AddParameter(cmd, "Node_config", value.Node_config);
			AddParameter(cmd, "Transport_type", value.Transport_type);
			AddParameter(cmd, "User_name", value.User_name);
			AddParameter(cmd, "Password", value.Password);
			AddParameter(cmd, "Ip_address", value.Ip_address);
			AddParameter(cmd, "Port", value.Port);
			AddParameter(cmd, "Status", value.Status);
			AddParameter(cmd, "Billing_export_frequency", value.Billing_export_frequency);
			AddParameter(cmd, "Cdr_publishing_frequency", value.Cdr_publishing_frequency);
			AddParameter(cmd, "Node_id", value.Node_id);
			return 0 != cmd.ExecuteNonQuery();
		}

		/// <summary>
		/// Updates the <c>Node</c> table and calls the <c>AcceptChanges</c> method
		/// on the changed DataRow objects.
		/// </summary>
		/// <param name="table">The <see cref="System.Data.DataTable"/> used to update the data source.</param>
		public void Update(DataTable table)
		{
			Update(table, true);
		}

		/// <summary>
		/// Updates the <c>Node</c> table. Pass <c>false</c> as the <c>acceptChanges</c> 
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
							DeleteByPrimaryKey((short)row["Node_id"]);
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
		/// Deletes the specified object from the <c>Node</c> table.
		/// </summary>
		/// <param name="value">The <see cref="NodeRow"/> object to delete.</param>
		/// <returns>true if the record was deleted; otherwise, false.</returns>
		public bool Delete(NodeRow value)
		{
			return DeleteByPrimaryKey(value.Node_id);
		}

		/// <summary>
		/// Deletes a record from the <c>Node</c> table using
		/// the specified primary key.
		/// </summary>
		/// <param name="node_id">The <c>node_id</c> column value.</param>
		/// <returns>true if the record was deleted; otherwise, false.</returns>
		public virtual bool DeleteByPrimaryKey(short node_id)
		{
			string whereSql = "[node_id]=" + _db.CreateSqlParameterName("Node_id");
			IDbCommand cmd = CreateDeleteCommand(whereSql);
			AddParameter(cmd, "Node_id", node_id);
			return 0 < cmd.ExecuteNonQuery();
		}

		/// <summary>
		/// Deletes records from the <c>Node</c> table using the 
		/// <c>R_10</c> foreign key.
		/// </summary>
		/// <param name="platform_id">The <c>platform_id</c> column value.</param>
		/// <returns>The number of records deleted from the table.</returns>
		public int DeleteByPlatform_id(short platform_id)
		{
			return CreateDeleteByPlatform_idCommand(platform_id).ExecuteNonQuery();
		}

		/// <summary>
		/// Creates an <see cref="System.Data.IDbCommand"/> object that can be used to
		/// delete records using the <c>R_10</c> foreign key.
		/// </summary>
		/// <param name="platform_id">The <c>platform_id</c> column value.</param>
		/// <returns>A reference to the <see cref="System.Data.IDbCommand"/> object.</returns>
		protected virtual IDbCommand CreateDeleteByPlatform_idCommand(short platform_id)
		{
			string whereSql = "";
			whereSql += "[platform_id]=" + _db.CreateSqlParameterName("Platform_id");

			IDbCommand cmd = CreateDeleteCommand(whereSql);
			AddParameter(cmd, "Platform_id", platform_id);
			return cmd;
		}

		/// <summary>
		/// Deletes <c>Node</c> records that match the specified criteria.
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
		/// to delete <c>Node</c> records that match the specified criteria.
		/// </summary>
		/// <param name="whereSql">The SQL search condition. 
		/// For example: <c>"FirstName='Smith' AND Zip=75038"</c>.</param>
		/// <returns>A reference to the <see cref="System.Data.IDbCommand"/> object.</returns>
		protected virtual IDbCommand CreateDeleteCommand(string whereSql)
		{
			string sql = "DELETE FROM [dbo].[Node]";
			if(null != whereSql && 0 < whereSql.Length)
				sql += " WHERE " + whereSql;
			return _db.CreateCommand(sql);
		}

		/// <summary>
		/// Deletes all records from the <c>Node</c> table.
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
		/// <returns>An array of <see cref="NodeRow"/> objects.</returns>
		protected NodeRow[] MapRecords(IDbCommand command)
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
		/// <returns>An array of <see cref="NodeRow"/> objects.</returns>
		protected NodeRow[] MapRecords(IDataReader reader)
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
		/// <returns>An array of <see cref="NodeRow"/> objects.</returns>
		protected virtual NodeRow[] MapRecords(IDataReader reader, 
										int startIndex, int length, ref int totalRecordCount)
		{
			if(0 > startIndex)
				throw new ArgumentOutOfRangeException("startIndex", startIndex, "StartIndex cannot be less than zero.");
			if(0 > length)
				throw new ArgumentOutOfRangeException("length", length, "Length cannot be less than zero.");

			int node_idColumnIndex = reader.GetOrdinal("node_id");
			int platform_idColumnIndex = reader.GetOrdinal("platform_id");
			int descriptionColumnIndex = reader.GetOrdinal("description");
			int node_configColumnIndex = reader.GetOrdinal("node_config");
			int transport_typeColumnIndex = reader.GetOrdinal("transport_type");
			int user_nameColumnIndex = reader.GetOrdinal("user_name");
			int passwordColumnIndex = reader.GetOrdinal("password");
			int ip_addressColumnIndex = reader.GetOrdinal("ip_address");
			int portColumnIndex = reader.GetOrdinal("port");
			int statusColumnIndex = reader.GetOrdinal("status");
			int billing_export_frequencyColumnIndex = reader.GetOrdinal("billing_export_frequency");
			int cdr_publishing_frequencyColumnIndex = reader.GetOrdinal("cdr_publishing_frequency");

			System.Collections.ArrayList recordList = new System.Collections.ArrayList();
			int ri = -startIndex;
			while(reader.Read())
			{
				ri++;
				if(ri > 0 && ri <= length)
				{
					NodeRow record = new NodeRow();
					recordList.Add(record);

					record.Node_id = Convert.ToInt16(reader.GetValue(node_idColumnIndex));
					record.Platform_id = Convert.ToInt16(reader.GetValue(platform_idColumnIndex));
					record.Description = Convert.ToString(reader.GetValue(descriptionColumnIndex));
					record.Node_config = Convert.ToInt32(reader.GetValue(node_configColumnIndex));
					record.Transport_type = Convert.ToByte(reader.GetValue(transport_typeColumnIndex));
					record.User_name = Convert.ToString(reader.GetValue(user_nameColumnIndex));
					record.Password = Convert.ToString(reader.GetValue(passwordColumnIndex));
					record.Ip_address = Convert.ToInt32(reader.GetValue(ip_addressColumnIndex));
					record.Port = Convert.ToInt32(reader.GetValue(portColumnIndex));
					record.Status = Convert.ToByte(reader.GetValue(statusColumnIndex));
					record.Billing_export_frequency = Convert.ToInt32(reader.GetValue(billing_export_frequencyColumnIndex));
					record.Cdr_publishing_frequency = Convert.ToInt32(reader.GetValue(cdr_publishing_frequencyColumnIndex));

					if(ri == length && 0 != totalRecordCount)
						break;
				}
			}

			totalRecordCount = 0 == totalRecordCount ? ri + startIndex : -1;
			return (NodeRow[])(recordList.ToArray(typeof(NodeRow)));
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
		/// Converts <see cref="System.Data.DataRow"/> to <see cref="NodeRow"/>.
		/// </summary>
		/// <param name="row">The <see cref="System.Data.DataRow"/> object to be mapped.</param>
		/// <returns>A reference to the <see cref="NodeRow"/> object.</returns>
		protected virtual NodeRow MapRow(DataRow row)
		{
			NodeRow mappedObject = new NodeRow();
			DataTable dataTable = row.Table;
			DataColumn dataColumn;
			// Column "Node_id"
			dataColumn = dataTable.Columns["Node_id"];
			if(!row.IsNull(dataColumn))
				mappedObject.Node_id = (short)row[dataColumn];
			// Column "Platform_id"
			dataColumn = dataTable.Columns["Platform_id"];
			if(!row.IsNull(dataColumn))
				mappedObject.Platform_id = (short)row[dataColumn];
			// Column "Description"
			dataColumn = dataTable.Columns["Description"];
			if(!row.IsNull(dataColumn))
				mappedObject.Description = (string)row[dataColumn];
			// Column "Node_config"
			dataColumn = dataTable.Columns["Node_config"];
			if(!row.IsNull(dataColumn))
				mappedObject.Node_config = (int)row[dataColumn];
			// Column "Transport_type"
			dataColumn = dataTable.Columns["Transport_type"];
			if(!row.IsNull(dataColumn))
				mappedObject.Transport_type = (byte)row[dataColumn];
			// Column "User_name"
			dataColumn = dataTable.Columns["User_name"];
			if(!row.IsNull(dataColumn))
				mappedObject.User_name = (string)row[dataColumn];
			// Column "Password"
			dataColumn = dataTable.Columns["Password"];
			if(!row.IsNull(dataColumn))
				mappedObject.Password = (string)row[dataColumn];
			// Column "Ip_address"
			dataColumn = dataTable.Columns["Ip_address"];
			if(!row.IsNull(dataColumn))
				mappedObject.Ip_address = (int)row[dataColumn];
			// Column "Port"
			dataColumn = dataTable.Columns["Port"];
			if(!row.IsNull(dataColumn))
				mappedObject.Port = (int)row[dataColumn];
			// Column "Status"
			dataColumn = dataTable.Columns["Status"];
			if(!row.IsNull(dataColumn))
				mappedObject.Status = (byte)row[dataColumn];
			// Column "Billing_export_frequency"
			dataColumn = dataTable.Columns["Billing_export_frequency"];
			if(!row.IsNull(dataColumn))
				mappedObject.Billing_export_frequency = (int)row[dataColumn];
			// Column "Cdr_publishing_frequency"
			dataColumn = dataTable.Columns["Cdr_publishing_frequency"];
			if(!row.IsNull(dataColumn))
				mappedObject.Cdr_publishing_frequency = (int)row[dataColumn];
			return mappedObject;
		}

		/// <summary>
		/// Creates a <see cref="System.Data.DataTable"/> object for 
		/// the <c>Node</c> table.
		/// </summary>
		/// <returns>A reference to the <see cref="System.Data.DataTable"/> object.</returns>
		protected virtual DataTable CreateDataTable()
		{
			DataTable dataTable = new DataTable();
			dataTable.TableName = "Node";
			DataColumn dataColumn;
			dataColumn = dataTable.Columns.Add("Node_id", typeof(short));
			dataColumn.Caption = "node_id";
			dataColumn.AllowDBNull = false;
			dataColumn = dataTable.Columns.Add("Platform_id", typeof(short));
			dataColumn.Caption = "platform_id";
			dataColumn.AllowDBNull = false;
			dataColumn = dataTable.Columns.Add("Description", typeof(string));
			dataColumn.Caption = "description";
			dataColumn.MaxLength = 50;
			dataColumn.AllowDBNull = false;
			dataColumn = dataTable.Columns.Add("Node_config", typeof(int));
			dataColumn.Caption = "node_config";
			dataColumn.AllowDBNull = false;
			dataColumn = dataTable.Columns.Add("Transport_type", typeof(byte));
			dataColumn.Caption = "transport_type";
			dataColumn.AllowDBNull = false;
			dataColumn = dataTable.Columns.Add("User_name", typeof(string));
			dataColumn.Caption = "user_name";
			dataColumn.MaxLength = 50;
			dataColumn.AllowDBNull = false;
			dataColumn = dataTable.Columns.Add("Password", typeof(string));
			dataColumn.Caption = "password";
			dataColumn.MaxLength = 50;
			dataColumn.AllowDBNull = false;
			dataColumn = dataTable.Columns.Add("Ip_address", typeof(int));
			dataColumn.Caption = "ip_address";
			dataColumn.AllowDBNull = false;
			dataColumn = dataTable.Columns.Add("Port", typeof(int));
			dataColumn.Caption = "port";
			dataColumn.AllowDBNull = false;
			dataColumn = dataTable.Columns.Add("Status", typeof(byte));
			dataColumn.Caption = "status";
			dataColumn.AllowDBNull = false;
			dataColumn = dataTable.Columns.Add("Billing_export_frequency", typeof(int));
			dataColumn.Caption = "billing_export_frequency";
			dataColumn.AllowDBNull = false;
			dataColumn = dataTable.Columns.Add("Cdr_publishing_frequency", typeof(int));
			dataColumn.Caption = "cdr_publishing_frequency";
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
				case "Node_id":
					parameter = _db.AddParameter(cmd, paramName, DbType.Int16, value);
					break;

				case "Platform_id":
					parameter = _db.AddParameter(cmd, paramName, DbType.Int16, value);
					break;

				case "Description":
					parameter = _db.AddParameter(cmd, paramName, DbType.AnsiString, value);
					break;

				case "Node_config":
					parameter = _db.AddParameter(cmd, paramName, DbType.Int32, value);
					break;

				case "Transport_type":
					parameter = _db.AddParameter(cmd, paramName, DbType.Byte, value);
					break;

				case "User_name":
					parameter = _db.AddParameter(cmd, paramName, DbType.AnsiString, value);
					break;

				case "Password":
					parameter = _db.AddParameter(cmd, paramName, DbType.AnsiString, value);
					break;

				case "Ip_address":
					parameter = _db.AddParameter(cmd, paramName, DbType.Int32, value);
					break;

				case "Port":
					parameter = _db.AddParameter(cmd, paramName, DbType.Int32, value);
					break;

				case "Status":
					parameter = _db.AddParameter(cmd, paramName, DbType.Byte, value);
					break;

				case "Billing_export_frequency":
					parameter = _db.AddParameter(cmd, paramName, DbType.Int32, value);
					break;

				case "Cdr_publishing_frequency":
					parameter = _db.AddParameter(cmd, paramName, DbType.Int32, value);
					break;

				default:
					throw new ArgumentException("Unknown parameter name (" + paramName + ").");
			}
			return parameter;
		}

	} // End of NodeCollection_Base class
}  // End of namespace

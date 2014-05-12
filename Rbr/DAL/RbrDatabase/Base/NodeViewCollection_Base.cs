// <fileinfo name="Base\NodeViewCollection_Base.cs">
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
	/// The base class for <see cref="NodeViewCollection"/>. Provides methods 
	/// for common database view operations. 
	/// </summary>
	/// <remarks>
	/// Do not change this source code. Update the <see cref="NodeViewCollection"/>
	/// class if you need to add or change some functionality.
	/// </remarks>
	public abstract class NodeViewCollection_Base
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
		public const string Dot_ip_addressColumnName = "dot_ip_address";
		public const string Node_statusColumnName = "node_status";
		public const string Billing_export_frequencyColumnName = "billing_export_frequency";
		public const string Cdr_publishing_frequencyColumnName = "cdr_publishing_frequency";
		public const string Platform_locationColumnName = "platform_location";
		public const string Platform_statusColumnName = "platform_status";
		public const string Platform_configColumnName = "platform_config";

		// Instance fields
		private Rbr_Db _db;

		/// <summary>
		/// Initializes a new instance of the <see cref="NodeViewCollection_Base"/> 
		/// class with the specified <see cref="Rbr_Db"/>.
		/// </summary>
		/// <param name="db">The <see cref="Rbr_Db"/> object.</param>
		public NodeViewCollection_Base(Rbr_Db db)
		{
			_db = db;
		}

		/// <summary>
		/// Gets the database object that this view belongs to.
		///	</summary>
		///	<value>The <see cref="Rbr_Db"/> object.</value>
		protected Rbr_Db Database
		{
			get { return _db; }
		}

		/// <summary>
		/// Gets an array of all records from the <c>NodeView</c> view.
		/// </summary>
		/// <returns>An array of <see cref="NodeViewRow"/> objects.</returns>
		public virtual NodeViewRow[] GetAll()
		{
			return MapRecords(CreateGetAllCommand());
		}

		/// <summary>
		/// Gets a <see cref="System.Data.DataTable"/> object that 
		/// includes all records from the <c>NodeView</c> view.
		/// </summary>
		/// <returns>A reference to the <see cref="System.Data.DataTable"/> object.</returns>
		public virtual DataTable GetAllAsDataTable()
		{
			return MapRecordsToDataTable(CreateGetAllCommand());
		}

		/// <summary>
		/// Creates and returns an <see cref="System.Data.IDbCommand"/> object that is used
		/// to retrieve all records from the <c>NodeView</c> view.
		/// </summary>
		/// <returns>A reference to the <see cref="System.Data.IDbCommand"/> object.</returns>
		protected virtual IDbCommand CreateGetAllCommand()
		{
			return CreateGetCommand(null, null);
		}

		/// <summary>
		/// Gets the first <see cref="NodeViewRow"/> objects that 
		/// match the search condition.
		/// </summary>
		/// <param name="whereSql">The SQL search condition. For example: 
		/// <c>"FirstName='Smith' AND Zip=75038"</c>.</param>
		/// <returns>An instance of <see cref="NodeViewRow"/> or null reference 
		/// (Nothing in Visual Basic) if the object was not found.</returns>
		public NodeViewRow GetRow(string whereSql)
		{
			int totalRecordCount = -1;
			NodeViewRow[] rows = GetAsArray(whereSql, null, 0, 1, ref totalRecordCount);
			return 0 == rows.Length ? null : rows[0];
		}

		/// <summary>
		/// Gets an array of <see cref="NodeViewRow"/> objects that 
		/// match the search condition, in the the specified sort order.
		/// </summary>
		/// <param name="whereSql">The SQL search condition. For example: 
		/// <c>"FirstName='Smith' AND Zip=75038"</c>.</param>
		/// <param name="orderBySql">The column name(s) followed by "ASC" (ascending) or "DESC" (descending).
		/// Columns are sorted in ascending order by default. For example: <c>"LastName ASC, FirstName ASC"</c>.</param>
		/// <returns>An array of <see cref="NodeViewRow"/> objects.</returns>
		public NodeViewRow[] GetAsArray(string whereSql, string orderBySql)
		{
			int totalRecordCount = -1;
			return GetAsArray(whereSql, orderBySql, 0, int.MaxValue, ref totalRecordCount);
		}

		/// <summary>
		/// Gets an array of <see cref="NodeViewRow"/> objects that 
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
		/// <returns>An array of <see cref="NodeViewRow"/> objects.</returns>
		public virtual NodeViewRow[] GetAsArray(string whereSql, string orderBySql,
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
			string sql = "SELECT * FROM [dbo].[NodeView]";
			if(null != whereSql && 0 < whereSql.Length)
				sql += " WHERE " + whereSql;
			if(null != orderBySql && 0 < orderBySql.Length)
				sql += " ORDER BY " + orderBySql;
			return _db.CreateCommand(sql);
		}

		/// <summary>
		/// Reads data using the specified command and returns 
		/// an array of mapped objects.
		/// </summary>
		/// <param name="command">The <see cref="System.Data.IDbCommand"/> object.</param>
		/// <returns>An array of <see cref="NodeViewRow"/> objects.</returns>
		protected NodeViewRow[] MapRecords(IDbCommand command)
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
		/// <param name="reader">The <see cref="System.Data.IDataReader"/> object to read data from the view.</param>
		/// <returns>An array of <see cref="NodeViewRow"/> objects.</returns>
		protected NodeViewRow[] MapRecords(IDataReader reader)
		{
			int totalRecordCount = -1;
			return MapRecords(reader, 0, int.MaxValue, ref totalRecordCount);
		}

		/// <summary>
		/// Reads data from the provided data reader and returns 
		/// an array of mapped objects.
		/// </summary>
		/// <param name="reader">The <see cref="System.Data.IDataReader"/> object to read data from the view.</param>
		/// <param name="startIndex">The index of the first record to map.</param>
		/// <param name="length">The number of records to map.</param>
		/// <param name="totalRecordCount">A reference parameter that returns the total number 
		/// of records in the reader object if 0 was passed into the method; otherwise it returns -1.</param>
		/// <returns>An array of <see cref="NodeViewRow"/> objects.</returns>
		protected virtual NodeViewRow[] MapRecords(IDataReader reader, 
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
			int dot_ip_addressColumnIndex = reader.GetOrdinal("dot_ip_address");
			int node_statusColumnIndex = reader.GetOrdinal("node_status");
			int billing_export_frequencyColumnIndex = reader.GetOrdinal("billing_export_frequency");
			int cdr_publishing_frequencyColumnIndex = reader.GetOrdinal("cdr_publishing_frequency");
			int platform_locationColumnIndex = reader.GetOrdinal("platform_location");
			int platform_statusColumnIndex = reader.GetOrdinal("platform_status");
			int platform_configColumnIndex = reader.GetOrdinal("platform_config");

			System.Collections.ArrayList recordList = new System.Collections.ArrayList();
			int ri = -startIndex;
			while(reader.Read())
			{
				ri++;
				if(ri > 0 && ri <= length)
				{
					NodeViewRow record = new NodeViewRow();
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
					if(!reader.IsDBNull(dot_ip_addressColumnIndex))
						record.Dot_ip_address = Convert.ToString(reader.GetValue(dot_ip_addressColumnIndex));
					record.Node_status = Convert.ToByte(reader.GetValue(node_statusColumnIndex));
					record.Billing_export_frequency = Convert.ToInt32(reader.GetValue(billing_export_frequencyColumnIndex));
					record.Cdr_publishing_frequency = Convert.ToInt32(reader.GetValue(cdr_publishing_frequencyColumnIndex));
					record.Platform_location = Convert.ToString(reader.GetValue(platform_locationColumnIndex));
					record.Platform_status = Convert.ToByte(reader.GetValue(platform_statusColumnIndex));
					record.Platform_config = Convert.ToInt32(reader.GetValue(platform_configColumnIndex));

					if(ri == length && 0 != totalRecordCount)
						break;
				}
			}

			totalRecordCount = 0 == totalRecordCount ? ri + startIndex : -1;
			return (NodeViewRow[])(recordList.ToArray(typeof(NodeViewRow)));
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
		/// <param name="reader">The <see cref="System.Data.IDataReader"/> object to read data from the view.</param>
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
		/// <param name="reader">The <see cref="System.Data.IDataReader"/> object to read data from the view.</param>
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
		/// Converts <see cref="System.Data.DataRow"/> to <see cref="NodeViewRow"/>.
		/// </summary>
		/// <param name="row">The <see cref="System.Data.DataRow"/> object to be mapped.</param>
		/// <returns>A reference to the <see cref="NodeViewRow"/> object.</returns>
		protected virtual NodeViewRow MapRow(DataRow row)
		{
			NodeViewRow mappedObject = new NodeViewRow();
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
			// Column "Dot_ip_address"
			dataColumn = dataTable.Columns["Dot_ip_address"];
			if(!row.IsNull(dataColumn))
				mappedObject.Dot_ip_address = (string)row[dataColumn];
			// Column "Node_status"
			dataColumn = dataTable.Columns["Node_status"];
			if(!row.IsNull(dataColumn))
				mappedObject.Node_status = (byte)row[dataColumn];
			// Column "Billing_export_frequency"
			dataColumn = dataTable.Columns["Billing_export_frequency"];
			if(!row.IsNull(dataColumn))
				mappedObject.Billing_export_frequency = (int)row[dataColumn];
			// Column "Cdr_publishing_frequency"
			dataColumn = dataTable.Columns["Cdr_publishing_frequency"];
			if(!row.IsNull(dataColumn))
				mappedObject.Cdr_publishing_frequency = (int)row[dataColumn];
			// Column "Platform_location"
			dataColumn = dataTable.Columns["Platform_location"];
			if(!row.IsNull(dataColumn))
				mappedObject.Platform_location = (string)row[dataColumn];
			// Column "Platform_status"
			dataColumn = dataTable.Columns["Platform_status"];
			if(!row.IsNull(dataColumn))
				mappedObject.Platform_status = (byte)row[dataColumn];
			// Column "Platform_config"
			dataColumn = dataTable.Columns["Platform_config"];
			if(!row.IsNull(dataColumn))
				mappedObject.Platform_config = (int)row[dataColumn];
			return mappedObject;
		}

		/// <summary>
		/// Creates a <see cref="System.Data.DataTable"/> object for 
		/// the <c>NodeView</c> view.
		/// </summary>
		/// <returns>A reference to the <see cref="System.Data.DataTable"/> object.</returns>
		protected virtual DataTable CreateDataTable()
		{
			DataTable dataTable = new DataTable();
			dataTable.TableName = "NodeView";
			DataColumn dataColumn;
			dataColumn = dataTable.Columns.Add("Node_id", typeof(short));
			dataColumn.Caption = "node_id";
			dataColumn.AllowDBNull = false;
			dataColumn.ReadOnly = true;
			dataColumn = dataTable.Columns.Add("Platform_id", typeof(short));
			dataColumn.Caption = "platform_id";
			dataColumn.AllowDBNull = false;
			dataColumn.ReadOnly = true;
			dataColumn = dataTable.Columns.Add("Description", typeof(string));
			dataColumn.Caption = "description";
			dataColumn.MaxLength = 50;
			dataColumn.AllowDBNull = false;
			dataColumn.ReadOnly = true;
			dataColumn = dataTable.Columns.Add("Node_config", typeof(int));
			dataColumn.Caption = "node_config";
			dataColumn.AllowDBNull = false;
			dataColumn.ReadOnly = true;
			dataColumn = dataTable.Columns.Add("Transport_type", typeof(byte));
			dataColumn.Caption = "transport_type";
			dataColumn.AllowDBNull = false;
			dataColumn.ReadOnly = true;
			dataColumn = dataTable.Columns.Add("User_name", typeof(string));
			dataColumn.Caption = "user_name";
			dataColumn.MaxLength = 50;
			dataColumn.AllowDBNull = false;
			dataColumn.ReadOnly = true;
			dataColumn = dataTable.Columns.Add("Password", typeof(string));
			dataColumn.Caption = "password";
			dataColumn.MaxLength = 50;
			dataColumn.AllowDBNull = false;
			dataColumn.ReadOnly = true;
			dataColumn = dataTable.Columns.Add("Ip_address", typeof(int));
			dataColumn.Caption = "ip_address";
			dataColumn.AllowDBNull = false;
			dataColumn.ReadOnly = true;
			dataColumn = dataTable.Columns.Add("Port", typeof(int));
			dataColumn.Caption = "port";
			dataColumn.AllowDBNull = false;
			dataColumn.ReadOnly = true;
			dataColumn = dataTable.Columns.Add("Dot_ip_address", typeof(string));
			dataColumn.Caption = "dot_ip_address";
			dataColumn.MaxLength = 20;
			dataColumn.ReadOnly = true;
			dataColumn = dataTable.Columns.Add("Node_status", typeof(byte));
			dataColumn.Caption = "node_status";
			dataColumn.AllowDBNull = false;
			dataColumn.ReadOnly = true;
			dataColumn = dataTable.Columns.Add("Billing_export_frequency", typeof(int));
			dataColumn.Caption = "billing_export_frequency";
			dataColumn.AllowDBNull = false;
			dataColumn.ReadOnly = true;
			dataColumn = dataTable.Columns.Add("Cdr_publishing_frequency", typeof(int));
			dataColumn.Caption = "cdr_publishing_frequency";
			dataColumn.AllowDBNull = false;
			dataColumn.ReadOnly = true;
			dataColumn = dataTable.Columns.Add("Platform_location", typeof(string));
			dataColumn.Caption = "platform_location";
			dataColumn.MaxLength = 50;
			dataColumn.AllowDBNull = false;
			dataColumn.ReadOnly = true;
			dataColumn = dataTable.Columns.Add("Platform_status", typeof(byte));
			dataColumn.Caption = "platform_status";
			dataColumn.AllowDBNull = false;
			dataColumn.ReadOnly = true;
			dataColumn = dataTable.Columns.Add("Platform_config", typeof(int));
			dataColumn.Caption = "platform_config";
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

				case "Dot_ip_address":
					parameter = _db.AddParameter(cmd, paramName, DbType.AnsiString, value);
					break;

				case "Node_status":
					parameter = _db.AddParameter(cmd, paramName, DbType.Byte, value);
					break;

				case "Billing_export_frequency":
					parameter = _db.AddParameter(cmd, paramName, DbType.Int32, value);
					break;

				case "Cdr_publishing_frequency":
					parameter = _db.AddParameter(cmd, paramName, DbType.Int32, value);
					break;

				case "Platform_location":
					parameter = _db.AddParameter(cmd, paramName, DbType.AnsiString, value);
					break;

				case "Platform_status":
					parameter = _db.AddParameter(cmd, paramName, DbType.Byte, value);
					break;

				case "Platform_config":
					parameter = _db.AddParameter(cmd, paramName, DbType.Int32, value);
					break;

				default:
					throw new ArgumentException("Unknown parameter name (" + paramName + ").");
			}
			return parameter;
		}

	} // End of NodeViewCollection_Base class
}  // End of namespace

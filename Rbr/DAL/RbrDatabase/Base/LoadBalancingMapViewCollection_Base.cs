// <fileinfo name="Base\LoadBalancingMapViewCollection_Base.cs">
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
	/// The base class for <see cref="LoadBalancingMapViewCollection"/>. Provides methods 
	/// for common database view operations. 
	/// </summary>
	/// <remarks>
	/// Do not change this source code. Update the <see cref="LoadBalancingMapViewCollection"/>
	/// class if you need to add or change some functionality.
	/// </remarks>
	public abstract class LoadBalancingMapViewCollection_Base
	{
		// Constants
		public const string Node_idColumnName = "node_id";
		public const string Customer_acct_idColumnName = "customer_acct_id";
		public const string Max_callsColumnName = "max_calls";
		public const string Current_callsColumnName = "current_calls";
		public const string Platform_idColumnName = "platform_id";
		public const string Node_descriptionColumnName = "node_description";
		public const string Node_configColumnName = "node_config";
		public const string Transport_typeColumnName = "transport_type";
		public const string Node_statusColumnName = "node_status";
		public const string Ip_addressColumnName = "ip_address";
		public const string Dot_ip_addressColumnName = "dot_ip_address";
		public const string Platform_locationColumnName = "platform_location";
		public const string Platform_statusColumnName = "platform_status";
		public const string Platform_configColumnName = "platform_config";
		public const string Customer_acct_nameColumnName = "customer_acct_name";
		public const string Customer_acct_statusColumnName = "customer_acct_status";
		public const string Prefix_inColumnName = "prefix_in";
		public const string Prefix_outColumnName = "prefix_out";
		public const string Prefix_in_type_idColumnName = "prefix_in_type_id";
		public const string Partner_idColumnName = "partner_id";
		public const string Partner_nameColumnName = "partner_name";
		public const string Partner_statusColumnName = "partner_status";
		public const string Service_typeColumnName = "service_type";
		public const string Service_retail_typeColumnName = "service_retail_type";
		public const string Is_prepaidColumnName = "is_prepaid";
		public const string Is_shared_serviceColumnName = "is_shared_service";

		// Instance fields
		private Rbr_Db _db;

		/// <summary>
		/// Initializes a new instance of the <see cref="LoadBalancingMapViewCollection_Base"/> 
		/// class with the specified <see cref="Rbr_Db"/>.
		/// </summary>
		/// <param name="db">The <see cref="Rbr_Db"/> object.</param>
		public LoadBalancingMapViewCollection_Base(Rbr_Db db)
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
		/// Gets an array of all records from the <c>LoadBalancingMapView</c> view.
		/// </summary>
		/// <returns>An array of <see cref="LoadBalancingMapViewRow"/> objects.</returns>
		public virtual LoadBalancingMapViewRow[] GetAll()
		{
			return MapRecords(CreateGetAllCommand());
		}

		/// <summary>
		/// Gets a <see cref="System.Data.DataTable"/> object that 
		/// includes all records from the <c>LoadBalancingMapView</c> view.
		/// </summary>
		/// <returns>A reference to the <see cref="System.Data.DataTable"/> object.</returns>
		public virtual DataTable GetAllAsDataTable()
		{
			return MapRecordsToDataTable(CreateGetAllCommand());
		}

		/// <summary>
		/// Creates and returns an <see cref="System.Data.IDbCommand"/> object that is used
		/// to retrieve all records from the <c>LoadBalancingMapView</c> view.
		/// </summary>
		/// <returns>A reference to the <see cref="System.Data.IDbCommand"/> object.</returns>
		protected virtual IDbCommand CreateGetAllCommand()
		{
			return CreateGetCommand(null, null);
		}

		/// <summary>
		/// Gets the first <see cref="LoadBalancingMapViewRow"/> objects that 
		/// match the search condition.
		/// </summary>
		/// <param name="whereSql">The SQL search condition. For example: 
		/// <c>"FirstName='Smith' AND Zip=75038"</c>.</param>
		/// <returns>An instance of <see cref="LoadBalancingMapViewRow"/> or null reference 
		/// (Nothing in Visual Basic) if the object was not found.</returns>
		public LoadBalancingMapViewRow GetRow(string whereSql)
		{
			int totalRecordCount = -1;
			LoadBalancingMapViewRow[] rows = GetAsArray(whereSql, null, 0, 1, ref totalRecordCount);
			return 0 == rows.Length ? null : rows[0];
		}

		/// <summary>
		/// Gets an array of <see cref="LoadBalancingMapViewRow"/> objects that 
		/// match the search condition, in the the specified sort order.
		/// </summary>
		/// <param name="whereSql">The SQL search condition. For example: 
		/// <c>"FirstName='Smith' AND Zip=75038"</c>.</param>
		/// <param name="orderBySql">The column name(s) followed by "ASC" (ascending) or "DESC" (descending).
		/// Columns are sorted in ascending order by default. For example: <c>"LastName ASC, FirstName ASC"</c>.</param>
		/// <returns>An array of <see cref="LoadBalancingMapViewRow"/> objects.</returns>
		public LoadBalancingMapViewRow[] GetAsArray(string whereSql, string orderBySql)
		{
			int totalRecordCount = -1;
			return GetAsArray(whereSql, orderBySql, 0, int.MaxValue, ref totalRecordCount);
		}

		/// <summary>
		/// Gets an array of <see cref="LoadBalancingMapViewRow"/> objects that 
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
		/// <returns>An array of <see cref="LoadBalancingMapViewRow"/> objects.</returns>
		public virtual LoadBalancingMapViewRow[] GetAsArray(string whereSql, string orderBySql,
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
			string sql = "SELECT * FROM [dbo].[LoadBalancingMapView]";
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
		/// <returns>An array of <see cref="LoadBalancingMapViewRow"/> objects.</returns>
		protected LoadBalancingMapViewRow[] MapRecords(IDbCommand command)
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
		/// <returns>An array of <see cref="LoadBalancingMapViewRow"/> objects.</returns>
		protected LoadBalancingMapViewRow[] MapRecords(IDataReader reader)
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
		/// <returns>An array of <see cref="LoadBalancingMapViewRow"/> objects.</returns>
		protected virtual LoadBalancingMapViewRow[] MapRecords(IDataReader reader, 
										int startIndex, int length, ref int totalRecordCount)
		{
			if(0 > startIndex)
				throw new ArgumentOutOfRangeException("startIndex", startIndex, "StartIndex cannot be less than zero.");
			if(0 > length)
				throw new ArgumentOutOfRangeException("length", length, "Length cannot be less than zero.");

			int node_idColumnIndex = reader.GetOrdinal("node_id");
			int customer_acct_idColumnIndex = reader.GetOrdinal("customer_acct_id");
			int max_callsColumnIndex = reader.GetOrdinal("max_calls");
			int current_callsColumnIndex = reader.GetOrdinal("current_calls");
			int platform_idColumnIndex = reader.GetOrdinal("platform_id");
			int node_descriptionColumnIndex = reader.GetOrdinal("node_description");
			int node_configColumnIndex = reader.GetOrdinal("node_config");
			int transport_typeColumnIndex = reader.GetOrdinal("transport_type");
			int node_statusColumnIndex = reader.GetOrdinal("node_status");
			int ip_addressColumnIndex = reader.GetOrdinal("ip_address");
			int dot_ip_addressColumnIndex = reader.GetOrdinal("dot_ip_address");
			int platform_locationColumnIndex = reader.GetOrdinal("platform_location");
			int platform_statusColumnIndex = reader.GetOrdinal("platform_status");
			int platform_configColumnIndex = reader.GetOrdinal("platform_config");
			int customer_acct_nameColumnIndex = reader.GetOrdinal("customer_acct_name");
			int customer_acct_statusColumnIndex = reader.GetOrdinal("customer_acct_status");
			int prefix_inColumnIndex = reader.GetOrdinal("prefix_in");
			int prefix_outColumnIndex = reader.GetOrdinal("prefix_out");
			int prefix_in_type_idColumnIndex = reader.GetOrdinal("prefix_in_type_id");
			int partner_idColumnIndex = reader.GetOrdinal("partner_id");
			int partner_nameColumnIndex = reader.GetOrdinal("partner_name");
			int partner_statusColumnIndex = reader.GetOrdinal("partner_status");
			int service_typeColumnIndex = reader.GetOrdinal("service_type");
			int service_retail_typeColumnIndex = reader.GetOrdinal("service_retail_type");
			int is_prepaidColumnIndex = reader.GetOrdinal("is_prepaid");
			int is_shared_serviceColumnIndex = reader.GetOrdinal("is_shared_service");

			System.Collections.ArrayList recordList = new System.Collections.ArrayList();
			int ri = -startIndex;
			while(reader.Read())
			{
				ri++;
				if(ri > 0 && ri <= length)
				{
					LoadBalancingMapViewRow record = new LoadBalancingMapViewRow();
					recordList.Add(record);

					record.Node_id = Convert.ToInt16(reader.GetValue(node_idColumnIndex));
					record.Customer_acct_id = Convert.ToInt16(reader.GetValue(customer_acct_idColumnIndex));
					record.Max_calls = Convert.ToInt32(reader.GetValue(max_callsColumnIndex));
					record.Current_calls = Convert.ToInt32(reader.GetValue(current_callsColumnIndex));
					if(!reader.IsDBNull(platform_idColumnIndex))
						record.Platform_id = Convert.ToInt16(reader.GetValue(platform_idColumnIndex));
					if(!reader.IsDBNull(node_descriptionColumnIndex))
						record.Node_description = Convert.ToString(reader.GetValue(node_descriptionColumnIndex));
					if(!reader.IsDBNull(node_configColumnIndex))
						record.Node_config = Convert.ToInt32(reader.GetValue(node_configColumnIndex));
					if(!reader.IsDBNull(transport_typeColumnIndex))
						record.Transport_type = Convert.ToByte(reader.GetValue(transport_typeColumnIndex));
					if(!reader.IsDBNull(node_statusColumnIndex))
						record.Node_status = Convert.ToByte(reader.GetValue(node_statusColumnIndex));
					if(!reader.IsDBNull(ip_addressColumnIndex))
						record.Ip_address = Convert.ToInt32(reader.GetValue(ip_addressColumnIndex));
					if(!reader.IsDBNull(dot_ip_addressColumnIndex))
						record.Dot_ip_address = Convert.ToString(reader.GetValue(dot_ip_addressColumnIndex));
					if(!reader.IsDBNull(platform_locationColumnIndex))
						record.Platform_location = Convert.ToString(reader.GetValue(platform_locationColumnIndex));
					if(!reader.IsDBNull(platform_statusColumnIndex))
						record.Platform_status = Convert.ToByte(reader.GetValue(platform_statusColumnIndex));
					if(!reader.IsDBNull(platform_configColumnIndex))
						record.Platform_config = Convert.ToInt32(reader.GetValue(platform_configColumnIndex));
					if(!reader.IsDBNull(customer_acct_nameColumnIndex))
						record.Customer_acct_name = Convert.ToString(reader.GetValue(customer_acct_nameColumnIndex));
					if(!reader.IsDBNull(customer_acct_statusColumnIndex))
						record.Customer_acct_status = Convert.ToByte(reader.GetValue(customer_acct_statusColumnIndex));
					if(!reader.IsDBNull(prefix_inColumnIndex))
						record.Prefix_in = Convert.ToString(reader.GetValue(prefix_inColumnIndex));
					if(!reader.IsDBNull(prefix_outColumnIndex))
						record.Prefix_out = Convert.ToString(reader.GetValue(prefix_outColumnIndex));
					if(!reader.IsDBNull(prefix_in_type_idColumnIndex))
						record.Prefix_in_type_id = Convert.ToInt16(reader.GetValue(prefix_in_type_idColumnIndex));
					if(!reader.IsDBNull(partner_idColumnIndex))
						record.Partner_id = Convert.ToInt32(reader.GetValue(partner_idColumnIndex));
					if(!reader.IsDBNull(partner_nameColumnIndex))
						record.Partner_name = Convert.ToString(reader.GetValue(partner_nameColumnIndex));
					if(!reader.IsDBNull(partner_statusColumnIndex))
						record.Partner_status = Convert.ToByte(reader.GetValue(partner_statusColumnIndex));
					if(!reader.IsDBNull(service_typeColumnIndex))
						record.Service_type = Convert.ToByte(reader.GetValue(service_typeColumnIndex));
					if(!reader.IsDBNull(service_retail_typeColumnIndex))
						record.Service_retail_type = Convert.ToInt32(reader.GetValue(service_retail_typeColumnIndex));
					if(!reader.IsDBNull(is_prepaidColumnIndex))
						record.Is_prepaid = Convert.ToByte(reader.GetValue(is_prepaidColumnIndex));
					if(!reader.IsDBNull(is_shared_serviceColumnIndex))
						record.Is_shared_service = Convert.ToByte(reader.GetValue(is_shared_serviceColumnIndex));

					if(ri == length && 0 != totalRecordCount)
						break;
				}
			}

			totalRecordCount = 0 == totalRecordCount ? ri + startIndex : -1;
			return (LoadBalancingMapViewRow[])(recordList.ToArray(typeof(LoadBalancingMapViewRow)));
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
		/// Converts <see cref="System.Data.DataRow"/> to <see cref="LoadBalancingMapViewRow"/>.
		/// </summary>
		/// <param name="row">The <see cref="System.Data.DataRow"/> object to be mapped.</param>
		/// <returns>A reference to the <see cref="LoadBalancingMapViewRow"/> object.</returns>
		protected virtual LoadBalancingMapViewRow MapRow(DataRow row)
		{
			LoadBalancingMapViewRow mappedObject = new LoadBalancingMapViewRow();
			DataTable dataTable = row.Table;
			DataColumn dataColumn;
			// Column "Node_id"
			dataColumn = dataTable.Columns["Node_id"];
			if(!row.IsNull(dataColumn))
				mappedObject.Node_id = (short)row[dataColumn];
			// Column "Customer_acct_id"
			dataColumn = dataTable.Columns["Customer_acct_id"];
			if(!row.IsNull(dataColumn))
				mappedObject.Customer_acct_id = (short)row[dataColumn];
			// Column "Max_calls"
			dataColumn = dataTable.Columns["Max_calls"];
			if(!row.IsNull(dataColumn))
				mappedObject.Max_calls = (int)row[dataColumn];
			// Column "Current_calls"
			dataColumn = dataTable.Columns["Current_calls"];
			if(!row.IsNull(dataColumn))
				mappedObject.Current_calls = (int)row[dataColumn];
			// Column "Platform_id"
			dataColumn = dataTable.Columns["Platform_id"];
			if(!row.IsNull(dataColumn))
				mappedObject.Platform_id = (short)row[dataColumn];
			// Column "Node_description"
			dataColumn = dataTable.Columns["Node_description"];
			if(!row.IsNull(dataColumn))
				mappedObject.Node_description = (string)row[dataColumn];
			// Column "Node_config"
			dataColumn = dataTable.Columns["Node_config"];
			if(!row.IsNull(dataColumn))
				mappedObject.Node_config = (int)row[dataColumn];
			// Column "Transport_type"
			dataColumn = dataTable.Columns["Transport_type"];
			if(!row.IsNull(dataColumn))
				mappedObject.Transport_type = (byte)row[dataColumn];
			// Column "Node_status"
			dataColumn = dataTable.Columns["Node_status"];
			if(!row.IsNull(dataColumn))
				mappedObject.Node_status = (byte)row[dataColumn];
			// Column "Ip_address"
			dataColumn = dataTable.Columns["Ip_address"];
			if(!row.IsNull(dataColumn))
				mappedObject.Ip_address = (int)row[dataColumn];
			// Column "Dot_ip_address"
			dataColumn = dataTable.Columns["Dot_ip_address"];
			if(!row.IsNull(dataColumn))
				mappedObject.Dot_ip_address = (string)row[dataColumn];
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
			// Column "Customer_acct_name"
			dataColumn = dataTable.Columns["Customer_acct_name"];
			if(!row.IsNull(dataColumn))
				mappedObject.Customer_acct_name = (string)row[dataColumn];
			// Column "Customer_acct_status"
			dataColumn = dataTable.Columns["Customer_acct_status"];
			if(!row.IsNull(dataColumn))
				mappedObject.Customer_acct_status = (byte)row[dataColumn];
			// Column "Prefix_in"
			dataColumn = dataTable.Columns["Prefix_in"];
			if(!row.IsNull(dataColumn))
				mappedObject.Prefix_in = (string)row[dataColumn];
			// Column "Prefix_out"
			dataColumn = dataTable.Columns["Prefix_out"];
			if(!row.IsNull(dataColumn))
				mappedObject.Prefix_out = (string)row[dataColumn];
			// Column "Prefix_in_type_id"
			dataColumn = dataTable.Columns["Prefix_in_type_id"];
			if(!row.IsNull(dataColumn))
				mappedObject.Prefix_in_type_id = (short)row[dataColumn];
			// Column "Partner_id"
			dataColumn = dataTable.Columns["Partner_id"];
			if(!row.IsNull(dataColumn))
				mappedObject.Partner_id = (int)row[dataColumn];
			// Column "Partner_name"
			dataColumn = dataTable.Columns["Partner_name"];
			if(!row.IsNull(dataColumn))
				mappedObject.Partner_name = (string)row[dataColumn];
			// Column "Partner_status"
			dataColumn = dataTable.Columns["Partner_status"];
			if(!row.IsNull(dataColumn))
				mappedObject.Partner_status = (byte)row[dataColumn];
			// Column "Service_type"
			dataColumn = dataTable.Columns["Service_type"];
			if(!row.IsNull(dataColumn))
				mappedObject.Service_type = (byte)row[dataColumn];
			// Column "Service_retail_type"
			dataColumn = dataTable.Columns["Service_retail_type"];
			if(!row.IsNull(dataColumn))
				mappedObject.Service_retail_type = (int)row[dataColumn];
			// Column "Is_prepaid"
			dataColumn = dataTable.Columns["Is_prepaid"];
			if(!row.IsNull(dataColumn))
				mappedObject.Is_prepaid = (byte)row[dataColumn];
			// Column "Is_shared_service"
			dataColumn = dataTable.Columns["Is_shared_service"];
			if(!row.IsNull(dataColumn))
				mappedObject.Is_shared_service = (byte)row[dataColumn];
			return mappedObject;
		}

		/// <summary>
		/// Creates a <see cref="System.Data.DataTable"/> object for 
		/// the <c>LoadBalancingMapView</c> view.
		/// </summary>
		/// <returns>A reference to the <see cref="System.Data.DataTable"/> object.</returns>
		protected virtual DataTable CreateDataTable()
		{
			DataTable dataTable = new DataTable();
			dataTable.TableName = "LoadBalancingMapView";
			DataColumn dataColumn;
			dataColumn = dataTable.Columns.Add("Node_id", typeof(short));
			dataColumn.Caption = "node_id";
			dataColumn.AllowDBNull = false;
			dataColumn.ReadOnly = true;
			dataColumn = dataTable.Columns.Add("Customer_acct_id", typeof(short));
			dataColumn.Caption = "customer_acct_id";
			dataColumn.AllowDBNull = false;
			dataColumn.ReadOnly = true;
			dataColumn = dataTable.Columns.Add("Max_calls", typeof(int));
			dataColumn.Caption = "max_calls";
			dataColumn.AllowDBNull = false;
			dataColumn.ReadOnly = true;
			dataColumn = dataTable.Columns.Add("Current_calls", typeof(int));
			dataColumn.Caption = "current_calls";
			dataColumn.AllowDBNull = false;
			dataColumn.ReadOnly = true;
			dataColumn = dataTable.Columns.Add("Platform_id", typeof(short));
			dataColumn.Caption = "platform_id";
			dataColumn.ReadOnly = true;
			dataColumn = dataTable.Columns.Add("Node_description", typeof(string));
			dataColumn.Caption = "node_description";
			dataColumn.MaxLength = 50;
			dataColumn.ReadOnly = true;
			dataColumn = dataTable.Columns.Add("Node_config", typeof(int));
			dataColumn.Caption = "node_config";
			dataColumn.ReadOnly = true;
			dataColumn = dataTable.Columns.Add("Transport_type", typeof(byte));
			dataColumn.Caption = "transport_type";
			dataColumn.ReadOnly = true;
			dataColumn = dataTable.Columns.Add("Node_status", typeof(byte));
			dataColumn.Caption = "node_status";
			dataColumn.ReadOnly = true;
			dataColumn = dataTable.Columns.Add("Ip_address", typeof(int));
			dataColumn.Caption = "ip_address";
			dataColumn.ReadOnly = true;
			dataColumn = dataTable.Columns.Add("Dot_ip_address", typeof(string));
			dataColumn.Caption = "dot_ip_address";
			dataColumn.MaxLength = 20;
			dataColumn.ReadOnly = true;
			dataColumn = dataTable.Columns.Add("Platform_location", typeof(string));
			dataColumn.Caption = "platform_location";
			dataColumn.MaxLength = 50;
			dataColumn.ReadOnly = true;
			dataColumn = dataTable.Columns.Add("Platform_status", typeof(byte));
			dataColumn.Caption = "platform_status";
			dataColumn.ReadOnly = true;
			dataColumn = dataTable.Columns.Add("Platform_config", typeof(int));
			dataColumn.Caption = "platform_config";
			dataColumn.ReadOnly = true;
			dataColumn = dataTable.Columns.Add("Customer_acct_name", typeof(string));
			dataColumn.Caption = "customer_acct_name";
			dataColumn.MaxLength = 50;
			dataColumn.ReadOnly = true;
			dataColumn = dataTable.Columns.Add("Customer_acct_status", typeof(byte));
			dataColumn.Caption = "customer_acct_status";
			dataColumn.ReadOnly = true;
			dataColumn = dataTable.Columns.Add("Prefix_in", typeof(string));
			dataColumn.Caption = "prefix_in";
			dataColumn.MaxLength = 10;
			dataColumn.ReadOnly = true;
			dataColumn = dataTable.Columns.Add("Prefix_out", typeof(string));
			dataColumn.Caption = "prefix_out";
			dataColumn.MaxLength = 10;
			dataColumn.ReadOnly = true;
			dataColumn = dataTable.Columns.Add("Prefix_in_type_id", typeof(short));
			dataColumn.Caption = "prefix_in_type_id";
			dataColumn.ReadOnly = true;
			dataColumn = dataTable.Columns.Add("Partner_id", typeof(int));
			dataColumn.Caption = "partner_id";
			dataColumn.ReadOnly = true;
			dataColumn = dataTable.Columns.Add("Partner_name", typeof(string));
			dataColumn.Caption = "partner_name";
			dataColumn.MaxLength = 50;
			dataColumn.ReadOnly = true;
			dataColumn = dataTable.Columns.Add("Partner_status", typeof(byte));
			dataColumn.Caption = "partner_status";
			dataColumn.ReadOnly = true;
			dataColumn = dataTable.Columns.Add("Service_type", typeof(byte));
			dataColumn.Caption = "service_type";
			dataColumn.ReadOnly = true;
			dataColumn = dataTable.Columns.Add("Service_retail_type", typeof(int));
			dataColumn.Caption = "service_retail_type";
			dataColumn.ReadOnly = true;
			dataColumn = dataTable.Columns.Add("Is_prepaid", typeof(byte));
			dataColumn.Caption = "is_prepaid";
			dataColumn.ReadOnly = true;
			dataColumn = dataTable.Columns.Add("Is_shared_service", typeof(byte));
			dataColumn.Caption = "is_shared_service";
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

				case "Customer_acct_id":
					parameter = _db.AddParameter(cmd, paramName, DbType.Int16, value);
					break;

				case "Max_calls":
					parameter = _db.AddParameter(cmd, paramName, DbType.Int32, value);
					break;

				case "Current_calls":
					parameter = _db.AddParameter(cmd, paramName, DbType.Int32, value);
					break;

				case "Platform_id":
					parameter = _db.AddParameter(cmd, paramName, DbType.Int16, value);
					break;

				case "Node_description":
					parameter = _db.AddParameter(cmd, paramName, DbType.AnsiString, value);
					break;

				case "Node_config":
					parameter = _db.AddParameter(cmd, paramName, DbType.Int32, value);
					break;

				case "Transport_type":
					parameter = _db.AddParameter(cmd, paramName, DbType.Byte, value);
					break;

				case "Node_status":
					parameter = _db.AddParameter(cmd, paramName, DbType.Byte, value);
					break;

				case "Ip_address":
					parameter = _db.AddParameter(cmd, paramName, DbType.Int32, value);
					break;

				case "Dot_ip_address":
					parameter = _db.AddParameter(cmd, paramName, DbType.AnsiString, value);
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

				case "Customer_acct_name":
					parameter = _db.AddParameter(cmd, paramName, DbType.AnsiString, value);
					break;

				case "Customer_acct_status":
					parameter = _db.AddParameter(cmd, paramName, DbType.Byte, value);
					break;

				case "Prefix_in":
					parameter = _db.AddParameter(cmd, paramName, DbType.AnsiString, value);
					break;

				case "Prefix_out":
					parameter = _db.AddParameter(cmd, paramName, DbType.AnsiString, value);
					break;

				case "Prefix_in_type_id":
					parameter = _db.AddParameter(cmd, paramName, DbType.Int16, value);
					break;

				case "Partner_id":
					parameter = _db.AddParameter(cmd, paramName, DbType.Int32, value);
					break;

				case "Partner_name":
					parameter = _db.AddParameter(cmd, paramName, DbType.AnsiString, value);
					break;

				case "Partner_status":
					parameter = _db.AddParameter(cmd, paramName, DbType.Byte, value);
					break;

				case "Service_type":
					parameter = _db.AddParameter(cmd, paramName, DbType.Byte, value);
					break;

				case "Service_retail_type":
					parameter = _db.AddParameter(cmd, paramName, DbType.Int32, value);
					break;

				case "Is_prepaid":
					parameter = _db.AddParameter(cmd, paramName, DbType.Byte, value);
					break;

				case "Is_shared_service":
					parameter = _db.AddParameter(cmd, paramName, DbType.Byte, value);
					break;

				default:
					throw new ArgumentException("Unknown parameter name (" + paramName + ").");
			}
			return parameter;
		}

	} // End of LoadBalancingMapViewCollection_Base class
}  // End of namespace

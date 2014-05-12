// <fileinfo name="Base\DialPeerViewCollection_Base.cs">
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
	/// The base class for <see cref="DialPeerViewCollection"/>. Provides methods 
	/// for common database view operations. 
	/// </summary>
	/// <remarks>
	/// Do not change this source code. Update the <see cref="DialPeerViewCollection"/>
	/// class if you need to add or change some functionality.
	/// </remarks>
	public abstract class DialPeerViewCollection_Base
	{
		// Constants
		public const string End_point_idColumnName = "end_point_id";
		public const string Prefix_inColumnName = "prefix_in";
		public const string Customer_acct_idColumnName = "customer_acct_id";
		public const string AliasColumnName = "alias";
		public const string Prefix_type_descrColumnName = "prefix_type_descr";
		public const string Prefix_type_lengthColumnName = "prefix_type_length";
		public const string Prefix_type_delimiterColumnName = "prefix_type_delimiter";
		public const string Customer_acct_nameColumnName = "customer_acct_name";
		public const string Service_idColumnName = "service_id";
		public const string Customer_acct_statusColumnName = "customer_acct_status";
		public const string Prefix_outColumnName = "prefix_out";
		public const string Partner_idColumnName = "partner_id";
		public const string Allow_reroutingColumnName = "allow_rerouting";
		public const string Service_nameColumnName = "service_name";
		public const string Service_typeColumnName = "service_type";
		public const string Service_retail_typeColumnName = "service_retail_type";
		public const string Partner_nameColumnName = "partner_name";
		public const string Partner_statusColumnName = "partner_status";

		// Instance fields
		private Rbr_Db _db;

		/// <summary>
		/// Initializes a new instance of the <see cref="DialPeerViewCollection_Base"/> 
		/// class with the specified <see cref="Rbr_Db"/>.
		/// </summary>
		/// <param name="db">The <see cref="Rbr_Db"/> object.</param>
		public DialPeerViewCollection_Base(Rbr_Db db)
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
		/// Gets an array of all records from the <c>DialPeerView</c> view.
		/// </summary>
		/// <returns>An array of <see cref="DialPeerViewRow"/> objects.</returns>
		public virtual DialPeerViewRow[] GetAll()
		{
			return MapRecords(CreateGetAllCommand());
		}

		/// <summary>
		/// Gets a <see cref="System.Data.DataTable"/> object that 
		/// includes all records from the <c>DialPeerView</c> view.
		/// </summary>
		/// <returns>A reference to the <see cref="System.Data.DataTable"/> object.</returns>
		public virtual DataTable GetAllAsDataTable()
		{
			return MapRecordsToDataTable(CreateGetAllCommand());
		}

		/// <summary>
		/// Creates and returns an <see cref="System.Data.IDbCommand"/> object that is used
		/// to retrieve all records from the <c>DialPeerView</c> view.
		/// </summary>
		/// <returns>A reference to the <see cref="System.Data.IDbCommand"/> object.</returns>
		protected virtual IDbCommand CreateGetAllCommand()
		{
			return CreateGetCommand(null, null);
		}

		/// <summary>
		/// Gets the first <see cref="DialPeerViewRow"/> objects that 
		/// match the search condition.
		/// </summary>
		/// <param name="whereSql">The SQL search condition. For example: 
		/// <c>"FirstName='Smith' AND Zip=75038"</c>.</param>
		/// <returns>An instance of <see cref="DialPeerViewRow"/> or null reference 
		/// (Nothing in Visual Basic) if the object was not found.</returns>
		public DialPeerViewRow GetRow(string whereSql)
		{
			int totalRecordCount = -1;
			DialPeerViewRow[] rows = GetAsArray(whereSql, null, 0, 1, ref totalRecordCount);
			return 0 == rows.Length ? null : rows[0];
		}

		/// <summary>
		/// Gets an array of <see cref="DialPeerViewRow"/> objects that 
		/// match the search condition, in the the specified sort order.
		/// </summary>
		/// <param name="whereSql">The SQL search condition. For example: 
		/// <c>"FirstName='Smith' AND Zip=75038"</c>.</param>
		/// <param name="orderBySql">The column name(s) followed by "ASC" (ascending) or "DESC" (descending).
		/// Columns are sorted in ascending order by default. For example: <c>"LastName ASC, FirstName ASC"</c>.</param>
		/// <returns>An array of <see cref="DialPeerViewRow"/> objects.</returns>
		public DialPeerViewRow[] GetAsArray(string whereSql, string orderBySql)
		{
			int totalRecordCount = -1;
			return GetAsArray(whereSql, orderBySql, 0, int.MaxValue, ref totalRecordCount);
		}

		/// <summary>
		/// Gets an array of <see cref="DialPeerViewRow"/> objects that 
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
		/// <returns>An array of <see cref="DialPeerViewRow"/> objects.</returns>
		public virtual DialPeerViewRow[] GetAsArray(string whereSql, string orderBySql,
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
			string sql = "SELECT * FROM [dbo].[DialPeerView]";
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
		/// <returns>An array of <see cref="DialPeerViewRow"/> objects.</returns>
		protected DialPeerViewRow[] MapRecords(IDbCommand command)
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
		/// <returns>An array of <see cref="DialPeerViewRow"/> objects.</returns>
		protected DialPeerViewRow[] MapRecords(IDataReader reader)
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
		/// <returns>An array of <see cref="DialPeerViewRow"/> objects.</returns>
		protected virtual DialPeerViewRow[] MapRecords(IDataReader reader, 
										int startIndex, int length, ref int totalRecordCount)
		{
			if(0 > startIndex)
				throw new ArgumentOutOfRangeException("startIndex", startIndex, "StartIndex cannot be less than zero.");
			if(0 > length)
				throw new ArgumentOutOfRangeException("length", length, "Length cannot be less than zero.");

			int end_point_idColumnIndex = reader.GetOrdinal("end_point_id");
			int prefix_inColumnIndex = reader.GetOrdinal("prefix_in");
			int customer_acct_idColumnIndex = reader.GetOrdinal("customer_acct_id");
			int aliasColumnIndex = reader.GetOrdinal("alias");
			int prefix_type_descrColumnIndex = reader.GetOrdinal("prefix_type_descr");
			int prefix_type_lengthColumnIndex = reader.GetOrdinal("prefix_type_length");
			int prefix_type_delimiterColumnIndex = reader.GetOrdinal("prefix_type_delimiter");
			int customer_acct_nameColumnIndex = reader.GetOrdinal("customer_acct_name");
			int service_idColumnIndex = reader.GetOrdinal("service_id");
			int customer_acct_statusColumnIndex = reader.GetOrdinal("customer_acct_status");
			int prefix_outColumnIndex = reader.GetOrdinal("prefix_out");
			int partner_idColumnIndex = reader.GetOrdinal("partner_id");
			int allow_reroutingColumnIndex = reader.GetOrdinal("allow_rerouting");
			int service_nameColumnIndex = reader.GetOrdinal("service_name");
			int service_typeColumnIndex = reader.GetOrdinal("service_type");
			int service_retail_typeColumnIndex = reader.GetOrdinal("service_retail_type");
			int partner_nameColumnIndex = reader.GetOrdinal("partner_name");
			int partner_statusColumnIndex = reader.GetOrdinal("partner_status");

			System.Collections.ArrayList recordList = new System.Collections.ArrayList();
			int ri = -startIndex;
			while(reader.Read())
			{
				ri++;
				if(ri > 0 && ri <= length)
				{
					DialPeerViewRow record = new DialPeerViewRow();
					recordList.Add(record);

					record.End_point_id = Convert.ToInt16(reader.GetValue(end_point_idColumnIndex));
					record.Prefix_in = Convert.ToString(reader.GetValue(prefix_inColumnIndex));
					record.Customer_acct_id = Convert.ToInt16(reader.GetValue(customer_acct_idColumnIndex));
					if(!reader.IsDBNull(aliasColumnIndex))
						record.Alias = Convert.ToString(reader.GetValue(aliasColumnIndex));
					if(!reader.IsDBNull(prefix_type_descrColumnIndex))
						record.Prefix_type_descr = Convert.ToString(reader.GetValue(prefix_type_descrColumnIndex));
					if(!reader.IsDBNull(prefix_type_lengthColumnIndex))
						record.Prefix_type_length = Convert.ToByte(reader.GetValue(prefix_type_lengthColumnIndex));
					if(!reader.IsDBNull(prefix_type_delimiterColumnIndex))
						record.Prefix_type_delimiter = Convert.ToByte(reader.GetValue(prefix_type_delimiterColumnIndex));
					if(!reader.IsDBNull(customer_acct_nameColumnIndex))
						record.Customer_acct_name = Convert.ToString(reader.GetValue(customer_acct_nameColumnIndex));
					if(!reader.IsDBNull(service_idColumnIndex))
						record.Service_id = Convert.ToInt16(reader.GetValue(service_idColumnIndex));
					if(!reader.IsDBNull(customer_acct_statusColumnIndex))
						record.Customer_acct_status = Convert.ToByte(reader.GetValue(customer_acct_statusColumnIndex));
					if(!reader.IsDBNull(prefix_outColumnIndex))
						record.Prefix_out = Convert.ToString(reader.GetValue(prefix_outColumnIndex));
					if(!reader.IsDBNull(partner_idColumnIndex))
						record.Partner_id = Convert.ToInt32(reader.GetValue(partner_idColumnIndex));
					if(!reader.IsDBNull(allow_reroutingColumnIndex))
						record.Allow_rerouting = Convert.ToByte(reader.GetValue(allow_reroutingColumnIndex));
					if(!reader.IsDBNull(service_nameColumnIndex))
						record.Service_name = Convert.ToString(reader.GetValue(service_nameColumnIndex));
					if(!reader.IsDBNull(service_typeColumnIndex))
						record.Service_type = Convert.ToByte(reader.GetValue(service_typeColumnIndex));
					if(!reader.IsDBNull(service_retail_typeColumnIndex))
						record.Service_retail_type = Convert.ToInt32(reader.GetValue(service_retail_typeColumnIndex));
					if(!reader.IsDBNull(partner_nameColumnIndex))
						record.Partner_name = Convert.ToString(reader.GetValue(partner_nameColumnIndex));
					if(!reader.IsDBNull(partner_statusColumnIndex))
						record.Partner_status = Convert.ToByte(reader.GetValue(partner_statusColumnIndex));

					if(ri == length && 0 != totalRecordCount)
						break;
				}
			}

			totalRecordCount = 0 == totalRecordCount ? ri + startIndex : -1;
			return (DialPeerViewRow[])(recordList.ToArray(typeof(DialPeerViewRow)));
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
		/// Converts <see cref="System.Data.DataRow"/> to <see cref="DialPeerViewRow"/>.
		/// </summary>
		/// <param name="row">The <see cref="System.Data.DataRow"/> object to be mapped.</param>
		/// <returns>A reference to the <see cref="DialPeerViewRow"/> object.</returns>
		protected virtual DialPeerViewRow MapRow(DataRow row)
		{
			DialPeerViewRow mappedObject = new DialPeerViewRow();
			DataTable dataTable = row.Table;
			DataColumn dataColumn;
			// Column "End_point_id"
			dataColumn = dataTable.Columns["End_point_id"];
			if(!row.IsNull(dataColumn))
				mappedObject.End_point_id = (short)row[dataColumn];
			// Column "Prefix_in"
			dataColumn = dataTable.Columns["Prefix_in"];
			if(!row.IsNull(dataColumn))
				mappedObject.Prefix_in = (string)row[dataColumn];
			// Column "Customer_acct_id"
			dataColumn = dataTable.Columns["Customer_acct_id"];
			if(!row.IsNull(dataColumn))
				mappedObject.Customer_acct_id = (short)row[dataColumn];
			// Column "Alias"
			dataColumn = dataTable.Columns["Alias"];
			if(!row.IsNull(dataColumn))
				mappedObject.Alias = (string)row[dataColumn];
			// Column "Prefix_type_descr"
			dataColumn = dataTable.Columns["Prefix_type_descr"];
			if(!row.IsNull(dataColumn))
				mappedObject.Prefix_type_descr = (string)row[dataColumn];
			// Column "Prefix_type_length"
			dataColumn = dataTable.Columns["Prefix_type_length"];
			if(!row.IsNull(dataColumn))
				mappedObject.Prefix_type_length = (byte)row[dataColumn];
			// Column "Prefix_type_delimiter"
			dataColumn = dataTable.Columns["Prefix_type_delimiter"];
			if(!row.IsNull(dataColumn))
				mappedObject.Prefix_type_delimiter = (byte)row[dataColumn];
			// Column "Customer_acct_name"
			dataColumn = dataTable.Columns["Customer_acct_name"];
			if(!row.IsNull(dataColumn))
				mappedObject.Customer_acct_name = (string)row[dataColumn];
			// Column "Service_id"
			dataColumn = dataTable.Columns["Service_id"];
			if(!row.IsNull(dataColumn))
				mappedObject.Service_id = (short)row[dataColumn];
			// Column "Customer_acct_status"
			dataColumn = dataTable.Columns["Customer_acct_status"];
			if(!row.IsNull(dataColumn))
				mappedObject.Customer_acct_status = (byte)row[dataColumn];
			// Column "Prefix_out"
			dataColumn = dataTable.Columns["Prefix_out"];
			if(!row.IsNull(dataColumn))
				mappedObject.Prefix_out = (string)row[dataColumn];
			// Column "Partner_id"
			dataColumn = dataTable.Columns["Partner_id"];
			if(!row.IsNull(dataColumn))
				mappedObject.Partner_id = (int)row[dataColumn];
			// Column "Allow_rerouting"
			dataColumn = dataTable.Columns["Allow_rerouting"];
			if(!row.IsNull(dataColumn))
				mappedObject.Allow_rerouting = (byte)row[dataColumn];
			// Column "Service_name"
			dataColumn = dataTable.Columns["Service_name"];
			if(!row.IsNull(dataColumn))
				mappedObject.Service_name = (string)row[dataColumn];
			// Column "Service_type"
			dataColumn = dataTable.Columns["Service_type"];
			if(!row.IsNull(dataColumn))
				mappedObject.Service_type = (byte)row[dataColumn];
			// Column "Service_retail_type"
			dataColumn = dataTable.Columns["Service_retail_type"];
			if(!row.IsNull(dataColumn))
				mappedObject.Service_retail_type = (int)row[dataColumn];
			// Column "Partner_name"
			dataColumn = dataTable.Columns["Partner_name"];
			if(!row.IsNull(dataColumn))
				mappedObject.Partner_name = (string)row[dataColumn];
			// Column "Partner_status"
			dataColumn = dataTable.Columns["Partner_status"];
			if(!row.IsNull(dataColumn))
				mappedObject.Partner_status = (byte)row[dataColumn];
			return mappedObject;
		}

		/// <summary>
		/// Creates a <see cref="System.Data.DataTable"/> object for 
		/// the <c>DialPeerView</c> view.
		/// </summary>
		/// <returns>A reference to the <see cref="System.Data.DataTable"/> object.</returns>
		protected virtual DataTable CreateDataTable()
		{
			DataTable dataTable = new DataTable();
			dataTable.TableName = "DialPeerView";
			DataColumn dataColumn;
			dataColumn = dataTable.Columns.Add("End_point_id", typeof(short));
			dataColumn.Caption = "end_point_id";
			dataColumn.AllowDBNull = false;
			dataColumn.ReadOnly = true;
			dataColumn = dataTable.Columns.Add("Prefix_in", typeof(string));
			dataColumn.Caption = "prefix_in";
			dataColumn.MaxLength = 10;
			dataColumn.AllowDBNull = false;
			dataColumn.ReadOnly = true;
			dataColumn = dataTable.Columns.Add("Customer_acct_id", typeof(short));
			dataColumn.Caption = "customer_acct_id";
			dataColumn.AllowDBNull = false;
			dataColumn.ReadOnly = true;
			dataColumn = dataTable.Columns.Add("Alias", typeof(string));
			dataColumn.Caption = "alias";
			dataColumn.MaxLength = 50;
			dataColumn.ReadOnly = true;
			dataColumn = dataTable.Columns.Add("Prefix_type_descr", typeof(string));
			dataColumn.Caption = "prefix_type_descr";
			dataColumn.MaxLength = 50;
			dataColumn.ReadOnly = true;
			dataColumn = dataTable.Columns.Add("Prefix_type_length", typeof(byte));
			dataColumn.Caption = "prefix_type_length";
			dataColumn.ReadOnly = true;
			dataColumn = dataTable.Columns.Add("Prefix_type_delimiter", typeof(byte));
			dataColumn.Caption = "prefix_type_delimiter";
			dataColumn.ReadOnly = true;
			dataColumn = dataTable.Columns.Add("Customer_acct_name", typeof(string));
			dataColumn.Caption = "customer_acct_name";
			dataColumn.MaxLength = 50;
			dataColumn.ReadOnly = true;
			dataColumn = dataTable.Columns.Add("Service_id", typeof(short));
			dataColumn.Caption = "service_id";
			dataColumn.ReadOnly = true;
			dataColumn = dataTable.Columns.Add("Customer_acct_status", typeof(byte));
			dataColumn.Caption = "customer_acct_status";
			dataColumn.ReadOnly = true;
			dataColumn = dataTable.Columns.Add("Prefix_out", typeof(string));
			dataColumn.Caption = "prefix_out";
			dataColumn.MaxLength = 10;
			dataColumn.ReadOnly = true;
			dataColumn = dataTable.Columns.Add("Partner_id", typeof(int));
			dataColumn.Caption = "partner_id";
			dataColumn.ReadOnly = true;
			dataColumn = dataTable.Columns.Add("Allow_rerouting", typeof(byte));
			dataColumn.Caption = "allow_rerouting";
			dataColumn.ReadOnly = true;
			dataColumn = dataTable.Columns.Add("Service_name", typeof(string));
			dataColumn.Caption = "service_name";
			dataColumn.MaxLength = 64;
			dataColumn.ReadOnly = true;
			dataColumn = dataTable.Columns.Add("Service_type", typeof(byte));
			dataColumn.Caption = "service_type";
			dataColumn.ReadOnly = true;
			dataColumn = dataTable.Columns.Add("Service_retail_type", typeof(int));
			dataColumn.Caption = "service_retail_type";
			dataColumn.ReadOnly = true;
			dataColumn = dataTable.Columns.Add("Partner_name", typeof(string));
			dataColumn.Caption = "partner_name";
			dataColumn.MaxLength = 50;
			dataColumn.ReadOnly = true;
			dataColumn = dataTable.Columns.Add("Partner_status", typeof(byte));
			dataColumn.Caption = "partner_status";
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
				case "End_point_id":
					parameter = _db.AddParameter(cmd, paramName, DbType.Int16, value);
					break;

				case "Prefix_in":
					parameter = _db.AddParameter(cmd, paramName, DbType.AnsiString, value);
					break;

				case "Customer_acct_id":
					parameter = _db.AddParameter(cmd, paramName, DbType.Int16, value);
					break;

				case "Alias":
					parameter = _db.AddParameter(cmd, paramName, DbType.AnsiString, value);
					break;

				case "Prefix_type_descr":
					parameter = _db.AddParameter(cmd, paramName, DbType.AnsiString, value);
					break;

				case "Prefix_type_length":
					parameter = _db.AddParameter(cmd, paramName, DbType.Byte, value);
					break;

				case "Prefix_type_delimiter":
					parameter = _db.AddParameter(cmd, paramName, DbType.Byte, value);
					break;

				case "Customer_acct_name":
					parameter = _db.AddParameter(cmd, paramName, DbType.AnsiString, value);
					break;

				case "Service_id":
					parameter = _db.AddParameter(cmd, paramName, DbType.Int16, value);
					break;

				case "Customer_acct_status":
					parameter = _db.AddParameter(cmd, paramName, DbType.Byte, value);
					break;

				case "Prefix_out":
					parameter = _db.AddParameter(cmd, paramName, DbType.AnsiString, value);
					break;

				case "Partner_id":
					parameter = _db.AddParameter(cmd, paramName, DbType.Int32, value);
					break;

				case "Allow_rerouting":
					parameter = _db.AddParameter(cmd, paramName, DbType.Byte, value);
					break;

				case "Service_name":
					parameter = _db.AddParameter(cmd, paramName, DbType.AnsiString, value);
					break;

				case "Service_type":
					parameter = _db.AddParameter(cmd, paramName, DbType.Byte, value);
					break;

				case "Service_retail_type":
					parameter = _db.AddParameter(cmd, paramName, DbType.Int32, value);
					break;

				case "Partner_name":
					parameter = _db.AddParameter(cmd, paramName, DbType.AnsiString, value);
					break;

				case "Partner_status":
					parameter = _db.AddParameter(cmd, paramName, DbType.Byte, value);
					break;

				default:
					throw new ArgumentException("Unknown parameter name (" + paramName + ").");
			}
			return parameter;
		}

	} // End of DialPeerViewCollection_Base class
}  // End of namespace

// <fileinfo name="Base\IPAddressViewCollection_Base.cs">
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
	/// The base class for <see cref="IPAddressViewCollection"/>. Provides methods 
	/// for common database view operations. 
	/// </summary>
	/// <remarks>
	/// Do not change this source code. Update the <see cref="IPAddressViewCollection"/>
	/// class if you need to add or change some functionality.
	/// </remarks>
	public abstract class IPAddressViewCollection_Base
	{
		// Constants
		public const string IP_addressColumnName = "IP_address";
		public const string Dot_IP_addressColumnName = "dot_IP_address";
		public const string End_point_idColumnName = "end_point_id";
		public const string AliasColumnName = "alias";
		public const string With_alias_authenticationColumnName = "with_alias_authentication";
		public const string StatusColumnName = "status";
		public const string TypeColumnName = "type";
		public const string ProtocolColumnName = "protocol";
		public const string PortColumnName = "port";
		public const string RegistrationColumnName = "registration";
		public const string Is_registeredColumnName = "is_registered";
		public const string IP_address_rangeColumnName = "IP_address_range";
		public const string Max_callsColumnName = "max_calls";
		public const string PasswordColumnName = "password";
		public const string Prefix_in_type_idColumnName = "prefix_in_type_id";
		public const string Prefix_type_descrColumnName = "prefix_type_descr";
		public const string Prefix_lengthColumnName = "prefix_length";
		public const string Prefix_delimiterColumnName = "prefix_delimiter";

		// Instance fields
		private Rbr_Db _db;

		/// <summary>
		/// Initializes a new instance of the <see cref="IPAddressViewCollection_Base"/> 
		/// class with the specified <see cref="Rbr_Db"/>.
		/// </summary>
		/// <param name="db">The <see cref="Rbr_Db"/> object.</param>
		public IPAddressViewCollection_Base(Rbr_Db db)
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
		/// Gets an array of all records from the <c>IPAddressView</c> view.
		/// </summary>
		/// <returns>An array of <see cref="IPAddressViewRow"/> objects.</returns>
		public virtual IPAddressViewRow[] GetAll()
		{
			return MapRecords(CreateGetAllCommand());
		}

		/// <summary>
		/// Gets a <see cref="System.Data.DataTable"/> object that 
		/// includes all records from the <c>IPAddressView</c> view.
		/// </summary>
		/// <returns>A reference to the <see cref="System.Data.DataTable"/> object.</returns>
		public virtual DataTable GetAllAsDataTable()
		{
			return MapRecordsToDataTable(CreateGetAllCommand());
		}

		/// <summary>
		/// Creates and returns an <see cref="System.Data.IDbCommand"/> object that is used
		/// to retrieve all records from the <c>IPAddressView</c> view.
		/// </summary>
		/// <returns>A reference to the <see cref="System.Data.IDbCommand"/> object.</returns>
		protected virtual IDbCommand CreateGetAllCommand()
		{
			return CreateGetCommand(null, null);
		}

		/// <summary>
		/// Gets the first <see cref="IPAddressViewRow"/> objects that 
		/// match the search condition.
		/// </summary>
		/// <param name="whereSql">The SQL search condition. For example: 
		/// <c>"FirstName='Smith' AND Zip=75038"</c>.</param>
		/// <returns>An instance of <see cref="IPAddressViewRow"/> or null reference 
		/// (Nothing in Visual Basic) if the object was not found.</returns>
		public IPAddressViewRow GetRow(string whereSql)
		{
			int totalRecordCount = -1;
			IPAddressViewRow[] rows = GetAsArray(whereSql, null, 0, 1, ref totalRecordCount);
			return 0 == rows.Length ? null : rows[0];
		}

		/// <summary>
		/// Gets an array of <see cref="IPAddressViewRow"/> objects that 
		/// match the search condition, in the the specified sort order.
		/// </summary>
		/// <param name="whereSql">The SQL search condition. For example: 
		/// <c>"FirstName='Smith' AND Zip=75038"</c>.</param>
		/// <param name="orderBySql">The column name(s) followed by "ASC" (ascending) or "DESC" (descending).
		/// Columns are sorted in ascending order by default. For example: <c>"LastName ASC, FirstName ASC"</c>.</param>
		/// <returns>An array of <see cref="IPAddressViewRow"/> objects.</returns>
		public IPAddressViewRow[] GetAsArray(string whereSql, string orderBySql)
		{
			int totalRecordCount = -1;
			return GetAsArray(whereSql, orderBySql, 0, int.MaxValue, ref totalRecordCount);
		}

		/// <summary>
		/// Gets an array of <see cref="IPAddressViewRow"/> objects that 
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
		/// <returns>An array of <see cref="IPAddressViewRow"/> objects.</returns>
		public virtual IPAddressViewRow[] GetAsArray(string whereSql, string orderBySql,
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
			string sql = "SELECT * FROM [dbo].[IPAddressView]";
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
		/// <returns>An array of <see cref="IPAddressViewRow"/> objects.</returns>
		protected IPAddressViewRow[] MapRecords(IDbCommand command)
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
		/// <returns>An array of <see cref="IPAddressViewRow"/> objects.</returns>
		protected IPAddressViewRow[] MapRecords(IDataReader reader)
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
		/// <returns>An array of <see cref="IPAddressViewRow"/> objects.</returns>
		protected virtual IPAddressViewRow[] MapRecords(IDataReader reader, 
										int startIndex, int length, ref int totalRecordCount)
		{
			if(0 > startIndex)
				throw new ArgumentOutOfRangeException("startIndex", startIndex, "StartIndex cannot be less than zero.");
			if(0 > length)
				throw new ArgumentOutOfRangeException("length", length, "Length cannot be less than zero.");

			int iP_addressColumnIndex = reader.GetOrdinal("IP_address");
			int dot_IP_addressColumnIndex = reader.GetOrdinal("dot_IP_address");
			int end_point_idColumnIndex = reader.GetOrdinal("end_point_id");
			int aliasColumnIndex = reader.GetOrdinal("alias");
			int with_alias_authenticationColumnIndex = reader.GetOrdinal("with_alias_authentication");
			int statusColumnIndex = reader.GetOrdinal("status");
			int typeColumnIndex = reader.GetOrdinal("type");
			int protocolColumnIndex = reader.GetOrdinal("protocol");
			int portColumnIndex = reader.GetOrdinal("port");
			int registrationColumnIndex = reader.GetOrdinal("registration");
			int is_registeredColumnIndex = reader.GetOrdinal("is_registered");
			int iP_address_rangeColumnIndex = reader.GetOrdinal("IP_address_range");
			int max_callsColumnIndex = reader.GetOrdinal("max_calls");
			int passwordColumnIndex = reader.GetOrdinal("password");
			int prefix_in_type_idColumnIndex = reader.GetOrdinal("prefix_in_type_id");
			int prefix_type_descrColumnIndex = reader.GetOrdinal("prefix_type_descr");
			int prefix_lengthColumnIndex = reader.GetOrdinal("prefix_length");
			int prefix_delimiterColumnIndex = reader.GetOrdinal("prefix_delimiter");

			System.Collections.ArrayList recordList = new System.Collections.ArrayList();
			int ri = -startIndex;
			while(reader.Read())
			{
				ri++;
				if(ri > 0 && ri <= length)
				{
					IPAddressViewRow record = new IPAddressViewRow();
					recordList.Add(record);

					record.IP_address = Convert.ToInt32(reader.GetValue(iP_addressColumnIndex));
					if(!reader.IsDBNull(dot_IP_addressColumnIndex))
						record.Dot_IP_address = Convert.ToString(reader.GetValue(dot_IP_addressColumnIndex));
					record.End_point_id = Convert.ToInt16(reader.GetValue(end_point_idColumnIndex));
					record.Alias = Convert.ToString(reader.GetValue(aliasColumnIndex));
					record.With_alias_authentication = Convert.ToByte(reader.GetValue(with_alias_authenticationColumnIndex));
					record.Status = Convert.ToByte(reader.GetValue(statusColumnIndex));
					record.Type = Convert.ToByte(reader.GetValue(typeColumnIndex));
					record.Protocol = Convert.ToByte(reader.GetValue(protocolColumnIndex));
					record.Port = Convert.ToInt32(reader.GetValue(portColumnIndex));
					record.Registration = Convert.ToByte(reader.GetValue(registrationColumnIndex));
					record.Is_registered = Convert.ToByte(reader.GetValue(is_registeredColumnIndex));
					record.IP_address_range = Convert.ToString(reader.GetValue(iP_address_rangeColumnIndex));
					record.Max_calls = Convert.ToInt32(reader.GetValue(max_callsColumnIndex));
					record.Password = Convert.ToString(reader.GetValue(passwordColumnIndex));
					record.Prefix_in_type_id = Convert.ToInt16(reader.GetValue(prefix_in_type_idColumnIndex));
					record.Prefix_type_descr = Convert.ToString(reader.GetValue(prefix_type_descrColumnIndex));
					record.Prefix_length = Convert.ToByte(reader.GetValue(prefix_lengthColumnIndex));
					record.Prefix_delimiter = Convert.ToByte(reader.GetValue(prefix_delimiterColumnIndex));

					if(ri == length && 0 != totalRecordCount)
						break;
				}
			}

			totalRecordCount = 0 == totalRecordCount ? ri + startIndex : -1;
			return (IPAddressViewRow[])(recordList.ToArray(typeof(IPAddressViewRow)));
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
		/// Converts <see cref="System.Data.DataRow"/> to <see cref="IPAddressViewRow"/>.
		/// </summary>
		/// <param name="row">The <see cref="System.Data.DataRow"/> object to be mapped.</param>
		/// <returns>A reference to the <see cref="IPAddressViewRow"/> object.</returns>
		protected virtual IPAddressViewRow MapRow(DataRow row)
		{
			IPAddressViewRow mappedObject = new IPAddressViewRow();
			DataTable dataTable = row.Table;
			DataColumn dataColumn;
			// Column "IP_address"
			dataColumn = dataTable.Columns["IP_address"];
			if(!row.IsNull(dataColumn))
				mappedObject.IP_address = (int)row[dataColumn];
			// Column "Dot_IP_address"
			dataColumn = dataTable.Columns["Dot_IP_address"];
			if(!row.IsNull(dataColumn))
				mappedObject.Dot_IP_address = (string)row[dataColumn];
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
			// Column "IP_address_range"
			dataColumn = dataTable.Columns["IP_address_range"];
			if(!row.IsNull(dataColumn))
				mappedObject.IP_address_range = (string)row[dataColumn];
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
			// Column "Prefix_type_descr"
			dataColumn = dataTable.Columns["Prefix_type_descr"];
			if(!row.IsNull(dataColumn))
				mappedObject.Prefix_type_descr = (string)row[dataColumn];
			// Column "Prefix_length"
			dataColumn = dataTable.Columns["Prefix_length"];
			if(!row.IsNull(dataColumn))
				mappedObject.Prefix_length = (byte)row[dataColumn];
			// Column "Prefix_delimiter"
			dataColumn = dataTable.Columns["Prefix_delimiter"];
			if(!row.IsNull(dataColumn))
				mappedObject.Prefix_delimiter = (byte)row[dataColumn];
			return mappedObject;
		}

		/// <summary>
		/// Creates a <see cref="System.Data.DataTable"/> object for 
		/// the <c>IPAddressView</c> view.
		/// </summary>
		/// <returns>A reference to the <see cref="System.Data.DataTable"/> object.</returns>
		protected virtual DataTable CreateDataTable()
		{
			DataTable dataTable = new DataTable();
			dataTable.TableName = "IPAddressView";
			DataColumn dataColumn;
			dataColumn = dataTable.Columns.Add("IP_address", typeof(int));
			dataColumn.AllowDBNull = false;
			dataColumn.ReadOnly = true;
			dataColumn = dataTable.Columns.Add("Dot_IP_address", typeof(string));
			dataColumn.Caption = "dot_IP_address";
			dataColumn.MaxLength = 20;
			dataColumn.ReadOnly = true;
			dataColumn = dataTable.Columns.Add("End_point_id", typeof(short));
			dataColumn.Caption = "end_point_id";
			dataColumn.AllowDBNull = false;
			dataColumn.ReadOnly = true;
			dataColumn = dataTable.Columns.Add("Alias", typeof(string));
			dataColumn.Caption = "alias";
			dataColumn.MaxLength = 50;
			dataColumn.AllowDBNull = false;
			dataColumn.ReadOnly = true;
			dataColumn = dataTable.Columns.Add("With_alias_authentication", typeof(byte));
			dataColumn.Caption = "with_alias_authentication";
			dataColumn.AllowDBNull = false;
			dataColumn.ReadOnly = true;
			dataColumn = dataTable.Columns.Add("Status", typeof(byte));
			dataColumn.Caption = "status";
			dataColumn.AllowDBNull = false;
			dataColumn.ReadOnly = true;
			dataColumn = dataTable.Columns.Add("Type", typeof(byte));
			dataColumn.Caption = "type";
			dataColumn.AllowDBNull = false;
			dataColumn.ReadOnly = true;
			dataColumn = dataTable.Columns.Add("Protocol", typeof(byte));
			dataColumn.Caption = "protocol";
			dataColumn.AllowDBNull = false;
			dataColumn.ReadOnly = true;
			dataColumn = dataTable.Columns.Add("Port", typeof(int));
			dataColumn.Caption = "port";
			dataColumn.AllowDBNull = false;
			dataColumn.ReadOnly = true;
			dataColumn = dataTable.Columns.Add("Registration", typeof(byte));
			dataColumn.Caption = "registration";
			dataColumn.AllowDBNull = false;
			dataColumn.ReadOnly = true;
			dataColumn = dataTable.Columns.Add("Is_registered", typeof(byte));
			dataColumn.Caption = "is_registered";
			dataColumn.AllowDBNull = false;
			dataColumn.ReadOnly = true;
			dataColumn = dataTable.Columns.Add("IP_address_range", typeof(string));
			dataColumn.MaxLength = 19;
			dataColumn.AllowDBNull = false;
			dataColumn.ReadOnly = true;
			dataColumn = dataTable.Columns.Add("Max_calls", typeof(int));
			dataColumn.Caption = "max_calls";
			dataColumn.AllowDBNull = false;
			dataColumn.ReadOnly = true;
			dataColumn = dataTable.Columns.Add("Password", typeof(string));
			dataColumn.Caption = "password";
			dataColumn.MaxLength = 16;
			dataColumn.AllowDBNull = false;
			dataColumn.ReadOnly = true;
			dataColumn = dataTable.Columns.Add("Prefix_in_type_id", typeof(short));
			dataColumn.Caption = "prefix_in_type_id";
			dataColumn.AllowDBNull = false;
			dataColumn.ReadOnly = true;
			dataColumn = dataTable.Columns.Add("Prefix_type_descr", typeof(string));
			dataColumn.Caption = "prefix_type_descr";
			dataColumn.MaxLength = 50;
			dataColumn.AllowDBNull = false;
			dataColumn.ReadOnly = true;
			dataColumn = dataTable.Columns.Add("Prefix_length", typeof(byte));
			dataColumn.Caption = "prefix_length";
			dataColumn.AllowDBNull = false;
			dataColumn.ReadOnly = true;
			dataColumn = dataTable.Columns.Add("Prefix_delimiter", typeof(byte));
			dataColumn.Caption = "prefix_delimiter";
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
				case "IP_address":
					parameter = _db.AddParameter(cmd, paramName, DbType.Int32, value);
					break;

				case "Dot_IP_address":
					parameter = _db.AddParameter(cmd, paramName, DbType.AnsiString, value);
					break;

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

				case "IP_address_range":
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

				case "Prefix_type_descr":
					parameter = _db.AddParameter(cmd, paramName, DbType.AnsiString, value);
					break;

				case "Prefix_length":
					parameter = _db.AddParameter(cmd, paramName, DbType.Byte, value);
					break;

				case "Prefix_delimiter":
					parameter = _db.AddParameter(cmd, paramName, DbType.Byte, value);
					break;

				default:
					throw new ArgumentException("Unknown parameter name (" + paramName + ").");
			}
			return parameter;
		}

	} // End of IPAddressViewCollection_Base class
}  // End of namespace

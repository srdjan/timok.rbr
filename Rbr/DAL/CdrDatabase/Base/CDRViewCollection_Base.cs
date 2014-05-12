// <fileinfo name="Base\CDRViewCollection_Base.cs">
//		<copyright>
//			Copyright © 2002-2006 Timok ES LLC. All rights reserved.
//		</copyright>
//		<remarks>
//			Do not change this source code manually. Changes to this file may 
//			cause incorrect behavior and will be lost if the code is regenerated.
//		</remarks>
//		<generator rewritefile="True" infourl="http://www.SharpPower.com">RapTier</generator>
// </fileinfo>

using System;
using System.Data;
using Timok.Rbr.DAL.CdrDatabase;

namespace Timok.Rbr.DAL.CdrDatabase.Base
{
	/// <summary>
	/// The base class for <see cref="CDRViewCollection"/>. Provides methods 
	/// for common database view operations. 
	/// </summary>
	/// <remarks>
	/// Do not change this source code. Update the <see cref="CDRViewCollection"/>
	/// class if you need to add or change some functionality.
	/// </remarks>
	public abstract class CDRViewCollection_Base
	{
		// Constants
		public const string IdColumnName = "id";
		public const string Date_loggedColumnName = "date_logged";
		public const string Timok_dateColumnName = "timok_date";
		public const string StartColumnName = "start";
		public const string DurationColumnName = "duration";
		public const string CcodeColumnName = "ccode";
		public const string Local_numberColumnName = "local_number";
		public const string Carrier_route_idColumnName = "carrier_route_id";
		public const string PriceColumnName = "price";
		public const string CostColumnName = "cost";
		public const string Orig_IP_addressColumnName = "orig_IP_address";
		public const string Orig_end_point_idColumnName = "orig_end_point_id";
		public const string Term_end_point_idColumnName = "term_end_point_id";
		public const string Customer_acct_idColumnName = "customer_acct_id";
		public const string Disconnect_causeColumnName = "disconnect_cause";
		public const string Disconnect_sourceColumnName = "disconnect_source";
		public const string Rbr_resultColumnName = "rbr_result";
		public const string Prefix_inColumnName = "prefix_in";
		public const string Prefix_outColumnName = "prefix_out";
		public const string DNISColumnName = "DNIS";
		public const string ANIColumnName = "ANI";
		public const string Info_digitsColumnName = "info_digits";
		public const string Serial_numberColumnName = "serial_number";
		public const string End_user_priceColumnName = "end_user_price";
		public const string Used_bonus_minutesColumnName = "used_bonus_minutes";
		public const string Reseller_priceColumnName = "reseller_price";
		public const string Node_idColumnName = "node_id";
		public const string Customer_route_idColumnName = "customer_route_id";
		public const string Mapped_disconnect_causeColumnName = "mapped_disconnect_cause";
		public const string Carrier_acct_idColumnName = "carrier_acct_id";
		public const string Orig_dot_IP_addressColumnName = "orig_dot_IP_address";
		public const string Dialed_numberColumnName = "dialed_number";
		public const string Retail_acct_idColumnName = "retail_acct_id";
		public const string Customer_durationColumnName = "customer_duration";
		public const string Carrier_durationColumnName = "carrier_duration";
		public const string Retail_durationColumnName = "retail_duration";
		public const string MinutesColumnName = "minutes";
		public const string Carrier_minutesColumnName = "carrier_minutes";
		public const string Retail_minutesColumnName = "retail_minutes";
		public const string Carrier_route_nameColumnName = "carrier_route_name";
		public const string Customer_route_nameColumnName = "customer_route_name";
		public const string Orig_aliasColumnName = "orig_alias";
		public const string Term_aliasColumnName = "term_alias";
		public const string Term_ip_address_rangeColumnName = "term_ip_address_range";
		public const string Customer_acct_nameColumnName = "customer_acct_name";
		public const string Orig_partner_idColumnName = "orig_partner_id";
		public const string Orig_partner_nameColumnName = "orig_partner_name";
		public const string Carrier_acct_nameColumnName = "carrier_acct_name";
		public const string Term_partner_idColumnName = "term_partner_id";
		public const string Term_partner_nameColumnName = "term_partner_name";
		public const string Node_nameColumnName = "node_name";

		// Instance fields
		private Cdr_Db _db;

		/// <summary>
		/// Initializes a new instance of the <see cref="CDRViewCollection_Base"/> 
		/// class with the specified <see cref="Cdr_Db"/>.
		/// </summary>
		/// <param name="db">The <see cref="Cdr_Db"/> object.</param>
		public CDRViewCollection_Base(Cdr_Db db)
		{
			_db = db;
		}

		/// <summary>
		/// Gets the database object that this view belongs to.
		///	</summary>
		///	<value>The <see cref="Cdr_Db"/> object.</value>
		protected Cdr_Db Database
		{
			get { return _db; }
		}

		/// <summary>
		/// Gets an array of all records from the <c>CDRView</c> view.
		/// </summary>
		/// <returns>An array of <see cref="CDRViewRow"/> objects.</returns>
		public virtual CDRViewRow[] GetAll()
		{
			return MapRecords(CreateGetAllCommand());
		}

		/// <summary>
		/// Gets a <see cref="System.Data.DataTable"/> object that 
		/// includes all records from the <c>CDRView</c> view.
		/// </summary>
		/// <returns>A reference to the <see cref="System.Data.DataTable"/> object.</returns>
		public virtual DataTable GetAllAsDataTable()
		{
			return MapRecordsToDataTable(CreateGetAllCommand());
		}

		/// <summary>
		/// Creates and returns an <see cref="System.Data.IDbCommand"/> object that is used
		/// to retrieve all records from the <c>CDRView</c> view.
		/// </summary>
		/// <returns>A reference to the <see cref="System.Data.IDbCommand"/> object.</returns>
		protected virtual IDbCommand CreateGetAllCommand()
		{
			return CreateGetCommand(null, null);
		}

		/// <summary>
		/// Gets the first <see cref="CDRViewRow"/> objects that 
		/// match the search condition.
		/// </summary>
		/// <param name="whereSql">The SQL search condition. For example: 
		/// <c>"FirstName='Smith' AND Zip=75038"</c>.</param>
		/// <returns>An instance of <see cref="CDRViewRow"/> or null reference 
		/// (Nothing in Visual Basic) if the object was not found.</returns>
		public CDRViewRow GetRow(string whereSql)
		{
			int totalRecordCount = -1;
			CDRViewRow[] rows = GetAsArray(whereSql, null, 0, 1, ref totalRecordCount);
			return 0 == rows.Length ? null : rows[0];
		}

		/// <summary>
		/// Gets an array of <see cref="CDRViewRow"/> objects that 
		/// match the search condition, in the the specified sort order.
		/// </summary>
		/// <param name="whereSql">The SQL search condition. For example: 
		/// <c>"FirstName='Smith' AND Zip=75038"</c>.</param>
		/// <param name="orderBySql">The column name(s) followed by "ASC" (ascending) or "DESC" (descending).
		/// Columns are sorted in ascending order by default. For example: <c>"LastName ASC, FirstName ASC"</c>.</param>
		/// <returns>An array of <see cref="CDRViewRow"/> objects.</returns>
		public CDRViewRow[] GetAsArray(string whereSql, string orderBySql)
		{
			int totalRecordCount = -1;
			return GetAsArray(whereSql, orderBySql, 0, int.MaxValue, ref totalRecordCount);
		}

		/// <summary>
		/// Gets an array of <see cref="CDRViewRow"/> objects that 
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
		/// <returns>An array of <see cref="CDRViewRow"/> objects.</returns>
		public virtual CDRViewRow[] GetAsArray(string whereSql, string orderBySql,
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
			string sql = "SELECT * FROM [dbo].[CDRView]";
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
		/// <returns>An array of <see cref="CDRViewRow"/> objects.</returns>
		protected CDRViewRow[] MapRecords(IDbCommand command)
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
		/// <returns>An array of <see cref="CDRViewRow"/> objects.</returns>
		protected CDRViewRow[] MapRecords(IDataReader reader)
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
		/// <returns>An array of <see cref="CDRViewRow"/> objects.</returns>
		protected virtual CDRViewRow[] MapRecords(IDataReader reader, 
										int startIndex, int length, ref int totalRecordCount)
		{
			if(0 > startIndex)
				throw new ArgumentOutOfRangeException("startIndex", startIndex, "StartIndex cannot be less than zero.");
			if(0 > length)
				throw new ArgumentOutOfRangeException("length", length, "Length cannot be less than zero.");

			int idColumnIndex = reader.GetOrdinal("id");
			int date_loggedColumnIndex = reader.GetOrdinal("date_logged");
			int timok_dateColumnIndex = reader.GetOrdinal("timok_date");
			int startColumnIndex = reader.GetOrdinal("start");
			int durationColumnIndex = reader.GetOrdinal("duration");
			int ccodeColumnIndex = reader.GetOrdinal("ccode");
			int local_numberColumnIndex = reader.GetOrdinal("local_number");
			int carrier_route_idColumnIndex = reader.GetOrdinal("carrier_route_id");
			int priceColumnIndex = reader.GetOrdinal("price");
			int costColumnIndex = reader.GetOrdinal("cost");
			int orig_IP_addressColumnIndex = reader.GetOrdinal("orig_IP_address");
			int orig_end_point_idColumnIndex = reader.GetOrdinal("orig_end_point_id");
			int term_end_point_idColumnIndex = reader.GetOrdinal("term_end_point_id");
			int customer_acct_idColumnIndex = reader.GetOrdinal("customer_acct_id");
			int disconnect_causeColumnIndex = reader.GetOrdinal("disconnect_cause");
			int disconnect_sourceColumnIndex = reader.GetOrdinal("disconnect_source");
			int rbr_resultColumnIndex = reader.GetOrdinal("rbr_result");
			int prefix_inColumnIndex = reader.GetOrdinal("prefix_in");
			int prefix_outColumnIndex = reader.GetOrdinal("prefix_out");
			int dnisColumnIndex = reader.GetOrdinal("DNIS");
			int aniColumnIndex = reader.GetOrdinal("ANI");
			int info_digitsColumnIndex = reader.GetOrdinal("info_digits");
			int serial_numberColumnIndex = reader.GetOrdinal("serial_number");
			int end_user_priceColumnIndex = reader.GetOrdinal("end_user_price");
			int used_bonus_minutesColumnIndex = reader.GetOrdinal("used_bonus_minutes");
			int reseller_priceColumnIndex = reader.GetOrdinal("reseller_price");
			int node_idColumnIndex = reader.GetOrdinal("node_id");
			int customer_route_idColumnIndex = reader.GetOrdinal("customer_route_id");
			int mapped_disconnect_causeColumnIndex = reader.GetOrdinal("mapped_disconnect_cause");
			int carrier_acct_idColumnIndex = reader.GetOrdinal("carrier_acct_id");
			int orig_dot_IP_addressColumnIndex = reader.GetOrdinal("orig_dot_IP_address");
			int dialed_numberColumnIndex = reader.GetOrdinal("dialed_number");
			int retail_acct_idColumnIndex = reader.GetOrdinal("retail_acct_id");
			int customer_durationColumnIndex = reader.GetOrdinal("customer_duration");
			int carrier_durationColumnIndex = reader.GetOrdinal("carrier_duration");
			int retail_durationColumnIndex = reader.GetOrdinal("retail_duration");
			int minutesColumnIndex = reader.GetOrdinal("minutes");
			int carrier_minutesColumnIndex = reader.GetOrdinal("carrier_minutes");
			int retail_minutesColumnIndex = reader.GetOrdinal("retail_minutes");
			int carrier_route_nameColumnIndex = reader.GetOrdinal("carrier_route_name");
			int customer_route_nameColumnIndex = reader.GetOrdinal("customer_route_name");
			int orig_aliasColumnIndex = reader.GetOrdinal("orig_alias");
			int term_aliasColumnIndex = reader.GetOrdinal("term_alias");
			int term_ip_address_rangeColumnIndex = reader.GetOrdinal("term_ip_address_range");
			int customer_acct_nameColumnIndex = reader.GetOrdinal("customer_acct_name");
			int orig_partner_idColumnIndex = reader.GetOrdinal("orig_partner_id");
			int orig_partner_nameColumnIndex = reader.GetOrdinal("orig_partner_name");
			int carrier_acct_nameColumnIndex = reader.GetOrdinal("carrier_acct_name");
			int term_partner_idColumnIndex = reader.GetOrdinal("term_partner_id");
			int term_partner_nameColumnIndex = reader.GetOrdinal("term_partner_name");
			int node_nameColumnIndex = reader.GetOrdinal("node_name");

			System.Collections.ArrayList recordList = new System.Collections.ArrayList();
			int ri = -startIndex;
			while(reader.Read())
			{
				ri++;
				if(ri > 0 && ri <= length)
				{
					CDRViewRow record = new CDRViewRow();
					recordList.Add(record);

					if(!reader.IsDBNull(idColumnIndex))
						record.Id = Convert.ToString(reader.GetValue(idColumnIndex));
					record.Date_logged = Convert.ToDateTime(reader.GetValue(date_loggedColumnIndex));
					record.Timok_date = Convert.ToInt32(reader.GetValue(timok_dateColumnIndex));
					record.Start = Convert.ToDateTime(reader.GetValue(startColumnIndex));
					record.Duration = Convert.ToInt16(reader.GetValue(durationColumnIndex));
					record.Ccode = Convert.ToInt32(reader.GetValue(ccodeColumnIndex));
					record.Local_number = Convert.ToString(reader.GetValue(local_numberColumnIndex));
					record.Carrier_route_id = Convert.ToInt32(reader.GetValue(carrier_route_idColumnIndex));
					record.Price = Convert.ToDecimal(reader.GetValue(priceColumnIndex));
					record.Cost = Convert.ToDecimal(reader.GetValue(costColumnIndex));
					record.Orig_IP_address = Convert.ToInt32(reader.GetValue(orig_IP_addressColumnIndex));
					record.Orig_end_point_id = Convert.ToInt16(reader.GetValue(orig_end_point_idColumnIndex));
					record.Term_end_point_id = Convert.ToInt16(reader.GetValue(term_end_point_idColumnIndex));
					record.Customer_acct_id = Convert.ToInt16(reader.GetValue(customer_acct_idColumnIndex));
					record.Disconnect_cause = Convert.ToInt16(reader.GetValue(disconnect_causeColumnIndex));
					record.Disconnect_source = Convert.ToByte(reader.GetValue(disconnect_sourceColumnIndex));
					record.Rbr_result = Convert.ToInt16(reader.GetValue(rbr_resultColumnIndex));
					record.Prefix_in = Convert.ToString(reader.GetValue(prefix_inColumnIndex));
					record.Prefix_out = Convert.ToString(reader.GetValue(prefix_outColumnIndex));
					record.DNIS = Convert.ToInt64(reader.GetValue(dnisColumnIndex));
					record.ANI = Convert.ToInt64(reader.GetValue(aniColumnIndex));
					record.Info_digits = Convert.ToByte(reader.GetValue(info_digitsColumnIndex));
					record.Serial_number = Convert.ToInt64(reader.GetValue(serial_numberColumnIndex));
					record.End_user_price = Convert.ToDecimal(reader.GetValue(end_user_priceColumnIndex));
					record.Used_bonus_minutes = Convert.ToInt16(reader.GetValue(used_bonus_minutesColumnIndex));
					record.Reseller_price = Convert.ToDecimal(reader.GetValue(reseller_priceColumnIndex));
					record.Node_id = Convert.ToInt16(reader.GetValue(node_idColumnIndex));
					record.Customer_route_id = Convert.ToInt32(reader.GetValue(customer_route_idColumnIndex));
					record.Mapped_disconnect_cause = Convert.ToInt16(reader.GetValue(mapped_disconnect_causeColumnIndex));
					record.Carrier_acct_id = Convert.ToInt16(reader.GetValue(carrier_acct_idColumnIndex));
					if(!reader.IsDBNull(orig_dot_IP_addressColumnIndex))
						record.Orig_dot_IP_address = Convert.ToString(reader.GetValue(orig_dot_IP_addressColumnIndex));
					if(!reader.IsDBNull(dialed_numberColumnIndex))
						record.Dialed_number = Convert.ToString(reader.GetValue(dialed_numberColumnIndex));
					record.Retail_acct_id = Convert.ToInt32(reader.GetValue(retail_acct_idColumnIndex));
					record.Customer_duration = Convert.ToInt16(reader.GetValue(customer_durationColumnIndex));
					record.Carrier_duration = Convert.ToInt16(reader.GetValue(carrier_durationColumnIndex));
					record.Retail_duration = Convert.ToInt16(reader.GetValue(retail_durationColumnIndex));
					if(!reader.IsDBNull(minutesColumnIndex))
						record.Minutes = Convert.ToDecimal(reader.GetValue(minutesColumnIndex));
					if(!reader.IsDBNull(carrier_minutesColumnIndex))
						record.Carrier_minutes = Convert.ToDecimal(reader.GetValue(carrier_minutesColumnIndex));
					if(!reader.IsDBNull(retail_minutesColumnIndex))
						record.Retail_minutes = Convert.ToDecimal(reader.GetValue(retail_minutesColumnIndex));
					if(!reader.IsDBNull(carrier_route_nameColumnIndex))
						record.Carrier_route_name = Convert.ToString(reader.GetValue(carrier_route_nameColumnIndex));
					if(!reader.IsDBNull(customer_route_nameColumnIndex))
						record.Customer_route_name = Convert.ToString(reader.GetValue(customer_route_nameColumnIndex));
					if(!reader.IsDBNull(orig_aliasColumnIndex))
						record.Orig_alias = Convert.ToString(reader.GetValue(orig_aliasColumnIndex));
					if(!reader.IsDBNull(term_aliasColumnIndex))
						record.Term_alias = Convert.ToString(reader.GetValue(term_aliasColumnIndex));
					if(!reader.IsDBNull(term_ip_address_rangeColumnIndex))
						record.Term_ip_address_range = Convert.ToString(reader.GetValue(term_ip_address_rangeColumnIndex));
					if(!reader.IsDBNull(customer_acct_nameColumnIndex))
						record.Customer_acct_name = Convert.ToString(reader.GetValue(customer_acct_nameColumnIndex));
					if(!reader.IsDBNull(orig_partner_idColumnIndex))
						record.Orig_partner_id = Convert.ToInt32(reader.GetValue(orig_partner_idColumnIndex));
					if(!reader.IsDBNull(orig_partner_nameColumnIndex))
						record.Orig_partner_name = Convert.ToString(reader.GetValue(orig_partner_nameColumnIndex));
					if(!reader.IsDBNull(carrier_acct_nameColumnIndex))
						record.Carrier_acct_name = Convert.ToString(reader.GetValue(carrier_acct_nameColumnIndex));
					if(!reader.IsDBNull(term_partner_idColumnIndex))
						record.Term_partner_id = Convert.ToInt32(reader.GetValue(term_partner_idColumnIndex));
					if(!reader.IsDBNull(term_partner_nameColumnIndex))
						record.Term_partner_name = Convert.ToString(reader.GetValue(term_partner_nameColumnIndex));
					if(!reader.IsDBNull(node_nameColumnIndex))
						record.Node_name = Convert.ToString(reader.GetValue(node_nameColumnIndex));

					if(ri == length && 0 != totalRecordCount)
						break;
				}
			}

			totalRecordCount = 0 == totalRecordCount ? ri + startIndex : -1;
			return (CDRViewRow[])(recordList.ToArray(typeof(CDRViewRow)));
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
		/// Converts <see cref="System.Data.DataRow"/> to <see cref="CDRViewRow"/>.
		/// </summary>
		/// <param name="row">The <see cref="System.Data.DataRow"/> object to be mapped.</param>
		/// <returns>A reference to the <see cref="CDRViewRow"/> object.</returns>
		protected virtual CDRViewRow MapRow(DataRow row)
		{
			CDRViewRow mappedObject = new CDRViewRow();
			DataTable dataTable = row.Table;
			DataColumn dataColumn;
			// Column "Id"
			dataColumn = dataTable.Columns["Id"];
			if(!row.IsNull(dataColumn))
				mappedObject.Id = (string)row[dataColumn];
			// Column "Date_logged"
			dataColumn = dataTable.Columns["Date_logged"];
			if(!row.IsNull(dataColumn))
				mappedObject.Date_logged = (System.DateTime)row[dataColumn];
			// Column "Timok_date"
			dataColumn = dataTable.Columns["Timok_date"];
			if(!row.IsNull(dataColumn))
				mappedObject.Timok_date = (int)row[dataColumn];
			// Column "Start"
			dataColumn = dataTable.Columns["Start"];
			if(!row.IsNull(dataColumn))
				mappedObject.Start = (System.DateTime)row[dataColumn];
			// Column "Duration"
			dataColumn = dataTable.Columns["Duration"];
			if(!row.IsNull(dataColumn))
				mappedObject.Duration = (short)row[dataColumn];
			// Column "Ccode"
			dataColumn = dataTable.Columns["Ccode"];
			if(!row.IsNull(dataColumn))
				mappedObject.Ccode = (int)row[dataColumn];
			// Column "Local_number"
			dataColumn = dataTable.Columns["Local_number"];
			if(!row.IsNull(dataColumn))
				mappedObject.Local_number = (string)row[dataColumn];
			// Column "Carrier_route_id"
			dataColumn = dataTable.Columns["Carrier_route_id"];
			if(!row.IsNull(dataColumn))
				mappedObject.Carrier_route_id = (int)row[dataColumn];
			// Column "Price"
			dataColumn = dataTable.Columns["Price"];
			if(!row.IsNull(dataColumn))
				mappedObject.Price = (decimal)row[dataColumn];
			// Column "Cost"
			dataColumn = dataTable.Columns["Cost"];
			if(!row.IsNull(dataColumn))
				mappedObject.Cost = (decimal)row[dataColumn];
			// Column "Orig_IP_address"
			dataColumn = dataTable.Columns["Orig_IP_address"];
			if(!row.IsNull(dataColumn))
				mappedObject.Orig_IP_address = (int)row[dataColumn];
			// Column "Orig_end_point_id"
			dataColumn = dataTable.Columns["Orig_end_point_id"];
			if(!row.IsNull(dataColumn))
				mappedObject.Orig_end_point_id = (short)row[dataColumn];
			// Column "Term_end_point_id"
			dataColumn = dataTable.Columns["Term_end_point_id"];
			if(!row.IsNull(dataColumn))
				mappedObject.Term_end_point_id = (short)row[dataColumn];
			// Column "Customer_acct_id"
			dataColumn = dataTable.Columns["Customer_acct_id"];
			if(!row.IsNull(dataColumn))
				mappedObject.Customer_acct_id = (short)row[dataColumn];
			// Column "Disconnect_cause"
			dataColumn = dataTable.Columns["Disconnect_cause"];
			if(!row.IsNull(dataColumn))
				mappedObject.Disconnect_cause = (short)row[dataColumn];
			// Column "Disconnect_source"
			dataColumn = dataTable.Columns["Disconnect_source"];
			if(!row.IsNull(dataColumn))
				mappedObject.Disconnect_source = (byte)row[dataColumn];
			// Column "Rbr_result"
			dataColumn = dataTable.Columns["Rbr_result"];
			if(!row.IsNull(dataColumn))
				mappedObject.Rbr_result = (short)row[dataColumn];
			// Column "Prefix_in"
			dataColumn = dataTable.Columns["Prefix_in"];
			if(!row.IsNull(dataColumn))
				mappedObject.Prefix_in = (string)row[dataColumn];
			// Column "Prefix_out"
			dataColumn = dataTable.Columns["Prefix_out"];
			if(!row.IsNull(dataColumn))
				mappedObject.Prefix_out = (string)row[dataColumn];
			// Column "DNIS"
			dataColumn = dataTable.Columns["DNIS"];
			if(!row.IsNull(dataColumn))
				mappedObject.DNIS = (long)row[dataColumn];
			// Column "ANI"
			dataColumn = dataTable.Columns["ANI"];
			if(!row.IsNull(dataColumn))
				mappedObject.ANI = (long)row[dataColumn];
			// Column "Info_digits"
			dataColumn = dataTable.Columns["Info_digits"];
			if(!row.IsNull(dataColumn))
				mappedObject.Info_digits = (byte)row[dataColumn];
			// Column "Serial_number"
			dataColumn = dataTable.Columns["Serial_number"];
			if(!row.IsNull(dataColumn))
				mappedObject.Serial_number = (long)row[dataColumn];
			// Column "End_user_price"
			dataColumn = dataTable.Columns["End_user_price"];
			if(!row.IsNull(dataColumn))
				mappedObject.End_user_price = (decimal)row[dataColumn];
			// Column "Used_bonus_minutes"
			dataColumn = dataTable.Columns["Used_bonus_minutes"];
			if(!row.IsNull(dataColumn))
				mappedObject.Used_bonus_minutes = (short)row[dataColumn];
			// Column "Reseller_price"
			dataColumn = dataTable.Columns["Reseller_price"];
			if(!row.IsNull(dataColumn))
				mappedObject.Reseller_price = (decimal)row[dataColumn];
			// Column "Node_id"
			dataColumn = dataTable.Columns["Node_id"];
			if(!row.IsNull(dataColumn))
				mappedObject.Node_id = (short)row[dataColumn];
			// Column "Customer_route_id"
			dataColumn = dataTable.Columns["Customer_route_id"];
			if(!row.IsNull(dataColumn))
				mappedObject.Customer_route_id = (int)row[dataColumn];
			// Column "Mapped_disconnect_cause"
			dataColumn = dataTable.Columns["Mapped_disconnect_cause"];
			if(!row.IsNull(dataColumn))
				mappedObject.Mapped_disconnect_cause = (short)row[dataColumn];
			// Column "Carrier_acct_id"
			dataColumn = dataTable.Columns["Carrier_acct_id"];
			if(!row.IsNull(dataColumn))
				mappedObject.Carrier_acct_id = (short)row[dataColumn];
			// Column "Orig_dot_IP_address"
			dataColumn = dataTable.Columns["Orig_dot_IP_address"];
			if(!row.IsNull(dataColumn))
				mappedObject.Orig_dot_IP_address = (string)row[dataColumn];
			// Column "Dialed_number"
			dataColumn = dataTable.Columns["Dialed_number"];
			if(!row.IsNull(dataColumn))
				mappedObject.Dialed_number = (string)row[dataColumn];
			// Column "Retail_acct_id"
			dataColumn = dataTable.Columns["Retail_acct_id"];
			if(!row.IsNull(dataColumn))
				mappedObject.Retail_acct_id = (int)row[dataColumn];
			// Column "Customer_duration"
			dataColumn = dataTable.Columns["Customer_duration"];
			if(!row.IsNull(dataColumn))
				mappedObject.Customer_duration = (short)row[dataColumn];
			// Column "Carrier_duration"
			dataColumn = dataTable.Columns["Carrier_duration"];
			if(!row.IsNull(dataColumn))
				mappedObject.Carrier_duration = (short)row[dataColumn];
			// Column "Retail_duration"
			dataColumn = dataTable.Columns["Retail_duration"];
			if(!row.IsNull(dataColumn))
				mappedObject.Retail_duration = (short)row[dataColumn];
			// Column "Minutes"
			dataColumn = dataTable.Columns["Minutes"];
			if(!row.IsNull(dataColumn))
				mappedObject.Minutes = (decimal)row[dataColumn];
			// Column "Carrier_minutes"
			dataColumn = dataTable.Columns["Carrier_minutes"];
			if(!row.IsNull(dataColumn))
				mappedObject.Carrier_minutes = (decimal)row[dataColumn];
			// Column "Retail_minutes"
			dataColumn = dataTable.Columns["Retail_minutes"];
			if(!row.IsNull(dataColumn))
				mappedObject.Retail_minutes = (decimal)row[dataColumn];
			// Column "Carrier_route_name"
			dataColumn = dataTable.Columns["Carrier_route_name"];
			if(!row.IsNull(dataColumn))
				mappedObject.Carrier_route_name = (string)row[dataColumn];
			// Column "Customer_route_name"
			dataColumn = dataTable.Columns["Customer_route_name"];
			if(!row.IsNull(dataColumn))
				mappedObject.Customer_route_name = (string)row[dataColumn];
			// Column "Orig_alias"
			dataColumn = dataTable.Columns["Orig_alias"];
			if(!row.IsNull(dataColumn))
				mappedObject.Orig_alias = (string)row[dataColumn];
			// Column "Term_alias"
			dataColumn = dataTable.Columns["Term_alias"];
			if(!row.IsNull(dataColumn))
				mappedObject.Term_alias = (string)row[dataColumn];
			// Column "Term_ip_address_range"
			dataColumn = dataTable.Columns["Term_ip_address_range"];
			if(!row.IsNull(dataColumn))
				mappedObject.Term_ip_address_range = (string)row[dataColumn];
			// Column "Customer_acct_name"
			dataColumn = dataTable.Columns["Customer_acct_name"];
			if(!row.IsNull(dataColumn))
				mappedObject.Customer_acct_name = (string)row[dataColumn];
			// Column "Orig_partner_id"
			dataColumn = dataTable.Columns["Orig_partner_id"];
			if(!row.IsNull(dataColumn))
				mappedObject.Orig_partner_id = (int)row[dataColumn];
			// Column "Orig_partner_name"
			dataColumn = dataTable.Columns["Orig_partner_name"];
			if(!row.IsNull(dataColumn))
				mappedObject.Orig_partner_name = (string)row[dataColumn];
			// Column "Carrier_acct_name"
			dataColumn = dataTable.Columns["Carrier_acct_name"];
			if(!row.IsNull(dataColumn))
				mappedObject.Carrier_acct_name = (string)row[dataColumn];
			// Column "Term_partner_id"
			dataColumn = dataTable.Columns["Term_partner_id"];
			if(!row.IsNull(dataColumn))
				mappedObject.Term_partner_id = (int)row[dataColumn];
			// Column "Term_partner_name"
			dataColumn = dataTable.Columns["Term_partner_name"];
			if(!row.IsNull(dataColumn))
				mappedObject.Term_partner_name = (string)row[dataColumn];
			// Column "Node_name"
			dataColumn = dataTable.Columns["Node_name"];
			if(!row.IsNull(dataColumn))
				mappedObject.Node_name = (string)row[dataColumn];
			return mappedObject;
		}

		/// <summary>
		/// Creates a <see cref="System.Data.DataTable"/> object for 
		/// the <c>CDRView</c> view.
		/// </summary>
		/// <returns>A reference to the <see cref="System.Data.DataTable"/> object.</returns>
		protected virtual DataTable CreateDataTable()
		{
			DataTable dataTable = new DataTable();
			dataTable.TableName = "CDRView";
			DataColumn dataColumn;
			dataColumn = dataTable.Columns.Add("Id", typeof(string));
			dataColumn.Caption = "id";
			dataColumn.MaxLength = 32;
			dataColumn.ReadOnly = true;
			dataColumn = dataTable.Columns.Add("Date_logged", typeof(System.DateTime));
			dataColumn.Caption = "date_logged";
			dataColumn.AllowDBNull = false;
			dataColumn.ReadOnly = true;
			dataColumn = dataTable.Columns.Add("Timok_date", typeof(int));
			dataColumn.Caption = "timok_date";
			dataColumn.AllowDBNull = false;
			dataColumn.ReadOnly = true;
			dataColumn = dataTable.Columns.Add("Start", typeof(System.DateTime));
			dataColumn.Caption = "start";
			dataColumn.AllowDBNull = false;
			dataColumn.ReadOnly = true;
			dataColumn = dataTable.Columns.Add("Duration", typeof(short));
			dataColumn.Caption = "duration";
			dataColumn.AllowDBNull = false;
			dataColumn.ReadOnly = true;
			dataColumn = dataTable.Columns.Add("Ccode", typeof(int));
			dataColumn.Caption = "ccode";
			dataColumn.AllowDBNull = false;
			dataColumn.ReadOnly = true;
			dataColumn = dataTable.Columns.Add("Local_number", typeof(string));
			dataColumn.Caption = "local_number";
			dataColumn.MaxLength = 18;
			dataColumn.AllowDBNull = false;
			dataColumn.ReadOnly = true;
			dataColumn = dataTable.Columns.Add("Carrier_route_id", typeof(int));
			dataColumn.Caption = "carrier_route_id";
			dataColumn.AllowDBNull = false;
			dataColumn.ReadOnly = true;
			dataColumn = dataTable.Columns.Add("Price", typeof(decimal));
			dataColumn.Caption = "price";
			dataColumn.AllowDBNull = false;
			dataColumn.ReadOnly = true;
			dataColumn = dataTable.Columns.Add("Cost", typeof(decimal));
			dataColumn.Caption = "cost";
			dataColumn.AllowDBNull = false;
			dataColumn.ReadOnly = true;
			dataColumn = dataTable.Columns.Add("Orig_IP_address", typeof(int));
			dataColumn.Caption = "orig_IP_address";
			dataColumn.AllowDBNull = false;
			dataColumn.ReadOnly = true;
			dataColumn = dataTable.Columns.Add("Orig_end_point_id", typeof(short));
			dataColumn.Caption = "orig_end_point_id";
			dataColumn.AllowDBNull = false;
			dataColumn.ReadOnly = true;
			dataColumn = dataTable.Columns.Add("Term_end_point_id", typeof(short));
			dataColumn.Caption = "term_end_point_id";
			dataColumn.AllowDBNull = false;
			dataColumn.ReadOnly = true;
			dataColumn = dataTable.Columns.Add("Customer_acct_id", typeof(short));
			dataColumn.Caption = "customer_acct_id";
			dataColumn.AllowDBNull = false;
			dataColumn.ReadOnly = true;
			dataColumn = dataTable.Columns.Add("Disconnect_cause", typeof(short));
			dataColumn.Caption = "disconnect_cause";
			dataColumn.AllowDBNull = false;
			dataColumn.ReadOnly = true;
			dataColumn = dataTable.Columns.Add("Disconnect_source", typeof(byte));
			dataColumn.Caption = "disconnect_source";
			dataColumn.AllowDBNull = false;
			dataColumn.ReadOnly = true;
			dataColumn = dataTable.Columns.Add("Rbr_result", typeof(short));
			dataColumn.Caption = "rbr_result";
			dataColumn.AllowDBNull = false;
			dataColumn.ReadOnly = true;
			dataColumn = dataTable.Columns.Add("Prefix_in", typeof(string));
			dataColumn.Caption = "prefix_in";
			dataColumn.MaxLength = 10;
			dataColumn.AllowDBNull = false;
			dataColumn.ReadOnly = true;
			dataColumn = dataTable.Columns.Add("Prefix_out", typeof(string));
			dataColumn.Caption = "prefix_out";
			dataColumn.MaxLength = 10;
			dataColumn.AllowDBNull = false;
			dataColumn.ReadOnly = true;
			dataColumn = dataTable.Columns.Add("DNIS", typeof(long));
			dataColumn.AllowDBNull = false;
			dataColumn.ReadOnly = true;
			dataColumn = dataTable.Columns.Add("ANI", typeof(long));
			dataColumn.AllowDBNull = false;
			dataColumn.ReadOnly = true;
			dataColumn = dataTable.Columns.Add("Info_digits", typeof(byte));
			dataColumn.Caption = "info_digits";
			dataColumn.AllowDBNull = false;
			dataColumn.ReadOnly = true;
			dataColumn = dataTable.Columns.Add("Serial_number", typeof(long));
			dataColumn.Caption = "serial_number";
			dataColumn.AllowDBNull = false;
			dataColumn.ReadOnly = true;
			dataColumn = dataTable.Columns.Add("End_user_price", typeof(decimal));
			dataColumn.Caption = "end_user_price";
			dataColumn.AllowDBNull = false;
			dataColumn.ReadOnly = true;
			dataColumn = dataTable.Columns.Add("Used_bonus_minutes", typeof(short));
			dataColumn.Caption = "used_bonus_minutes";
			dataColumn.AllowDBNull = false;
			dataColumn.ReadOnly = true;
			dataColumn = dataTable.Columns.Add("Reseller_price", typeof(decimal));
			dataColumn.Caption = "reseller_price";
			dataColumn.AllowDBNull = false;
			dataColumn.ReadOnly = true;
			dataColumn = dataTable.Columns.Add("Node_id", typeof(short));
			dataColumn.Caption = "node_id";
			dataColumn.AllowDBNull = false;
			dataColumn.ReadOnly = true;
			dataColumn = dataTable.Columns.Add("Customer_route_id", typeof(int));
			dataColumn.Caption = "customer_route_id";
			dataColumn.AllowDBNull = false;
			dataColumn.ReadOnly = true;
			dataColumn = dataTable.Columns.Add("Mapped_disconnect_cause", typeof(short));
			dataColumn.Caption = "mapped_disconnect_cause";
			dataColumn.AllowDBNull = false;
			dataColumn.ReadOnly = true;
			dataColumn = dataTable.Columns.Add("Carrier_acct_id", typeof(short));
			dataColumn.Caption = "carrier_acct_id";
			dataColumn.AllowDBNull = false;
			dataColumn.ReadOnly = true;
			dataColumn = dataTable.Columns.Add("Orig_dot_IP_address", typeof(string));
			dataColumn.Caption = "orig_dot_IP_address";
			dataColumn.MaxLength = 20;
			dataColumn.ReadOnly = true;
			dataColumn = dataTable.Columns.Add("Dialed_number", typeof(string));
			dataColumn.Caption = "dialed_number";
			dataColumn.MaxLength = 48;
			dataColumn.ReadOnly = true;
			dataColumn = dataTable.Columns.Add("Retail_acct_id", typeof(int));
			dataColumn.Caption = "retail_acct_id";
			dataColumn.AllowDBNull = false;
			dataColumn.ReadOnly = true;
			dataColumn = dataTable.Columns.Add("Customer_duration", typeof(short));
			dataColumn.Caption = "customer_duration";
			dataColumn.AllowDBNull = false;
			dataColumn.ReadOnly = true;
			dataColumn = dataTable.Columns.Add("Carrier_duration", typeof(short));
			dataColumn.Caption = "carrier_duration";
			dataColumn.AllowDBNull = false;
			dataColumn.ReadOnly = true;
			dataColumn = dataTable.Columns.Add("Retail_duration", typeof(short));
			dataColumn.Caption = "retail_duration";
			dataColumn.AllowDBNull = false;
			dataColumn.ReadOnly = true;
			dataColumn = dataTable.Columns.Add("Minutes", typeof(decimal));
			dataColumn.Caption = "minutes";
			dataColumn.ReadOnly = true;
			dataColumn = dataTable.Columns.Add("Carrier_minutes", typeof(decimal));
			dataColumn.Caption = "carrier_minutes";
			dataColumn.ReadOnly = true;
			dataColumn = dataTable.Columns.Add("Retail_minutes", typeof(decimal));
			dataColumn.Caption = "retail_minutes";
			dataColumn.ReadOnly = true;
			dataColumn = dataTable.Columns.Add("Carrier_route_name", typeof(string));
			dataColumn.Caption = "carrier_route_name";
			dataColumn.MaxLength = 50;
			dataColumn.ReadOnly = true;
			dataColumn = dataTable.Columns.Add("Customer_route_name", typeof(string));
			dataColumn.Caption = "customer_route_name";
			dataColumn.MaxLength = 50;
			dataColumn.ReadOnly = true;
			dataColumn = dataTable.Columns.Add("Orig_alias", typeof(string));
			dataColumn.Caption = "orig_alias";
			dataColumn.MaxLength = 50;
			dataColumn.ReadOnly = true;
			dataColumn = dataTable.Columns.Add("Term_alias", typeof(string));
			dataColumn.Caption = "term_alias";
			dataColumn.MaxLength = 50;
			dataColumn.ReadOnly = true;
			dataColumn = dataTable.Columns.Add("Term_ip_address_range", typeof(string));
			dataColumn.Caption = "term_ip_address_range";
			dataColumn.MaxLength = 19;
			dataColumn.ReadOnly = true;
			dataColumn = dataTable.Columns.Add("Customer_acct_name", typeof(string));
			dataColumn.Caption = "customer_acct_name";
			dataColumn.MaxLength = 50;
			dataColumn.ReadOnly = true;
			dataColumn = dataTable.Columns.Add("Orig_partner_id", typeof(int));
			dataColumn.Caption = "orig_partner_id";
			dataColumn.ReadOnly = true;
			dataColumn = dataTable.Columns.Add("Orig_partner_name", typeof(string));
			dataColumn.Caption = "orig_partner_name";
			dataColumn.MaxLength = 50;
			dataColumn.ReadOnly = true;
			dataColumn = dataTable.Columns.Add("Carrier_acct_name", typeof(string));
			dataColumn.Caption = "carrier_acct_name";
			dataColumn.MaxLength = 50;
			dataColumn.ReadOnly = true;
			dataColumn = dataTable.Columns.Add("Term_partner_id", typeof(int));
			dataColumn.Caption = "term_partner_id";
			dataColumn.ReadOnly = true;
			dataColumn = dataTable.Columns.Add("Term_partner_name", typeof(string));
			dataColumn.Caption = "term_partner_name";
			dataColumn.MaxLength = 50;
			dataColumn.ReadOnly = true;
			dataColumn = dataTable.Columns.Add("Node_name", typeof(string));
			dataColumn.Caption = "node_name";
			dataColumn.MaxLength = 50;
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
				case "Id":
					parameter = _db.AddParameter(cmd, paramName, DbType.AnsiStringFixedLength, value);
					break;

				case "Date_logged":
					parameter = _db.AddParameter(cmd, paramName, DbType.DateTime, value);
					break;

				case "Timok_date":
					parameter = _db.AddParameter(cmd, paramName, DbType.Int32, value);
					break;

				case "Start":
					parameter = _db.AddParameter(cmd, paramName, DbType.DateTime, value);
					break;

				case "Duration":
					parameter = _db.AddParameter(cmd, paramName, DbType.Int16, value);
					break;

				case "Ccode":
					parameter = _db.AddParameter(cmd, paramName, DbType.Int32, value);
					break;

				case "Local_number":
					parameter = _db.AddParameter(cmd, paramName, DbType.AnsiString, value);
					break;

				case "Carrier_route_id":
					parameter = _db.AddParameter(cmd, paramName, DbType.Int32, value);
					break;

				case "Price":
					parameter = _db.AddParameter(cmd, paramName, DbType.Decimal, value);
					break;

				case "Cost":
					parameter = _db.AddParameter(cmd, paramName, DbType.Decimal, value);
					break;

				case "Orig_IP_address":
					parameter = _db.AddParameter(cmd, paramName, DbType.Int32, value);
					break;

				case "Orig_end_point_id":
					parameter = _db.AddParameter(cmd, paramName, DbType.Int16, value);
					break;

				case "Term_end_point_id":
					parameter = _db.AddParameter(cmd, paramName, DbType.Int16, value);
					break;

				case "Customer_acct_id":
					parameter = _db.AddParameter(cmd, paramName, DbType.Int16, value);
					break;

				case "Disconnect_cause":
					parameter = _db.AddParameter(cmd, paramName, DbType.Int16, value);
					break;

				case "Disconnect_source":
					parameter = _db.AddParameter(cmd, paramName, DbType.Byte, value);
					break;

				case "Rbr_result":
					parameter = _db.AddParameter(cmd, paramName, DbType.Int16, value);
					break;

				case "Prefix_in":
					parameter = _db.AddParameter(cmd, paramName, DbType.AnsiString, value);
					break;

				case "Prefix_out":
					parameter = _db.AddParameter(cmd, paramName, DbType.AnsiString, value);
					break;

				case "DNIS":
					parameter = _db.AddParameter(cmd, paramName, DbType.Int64, value);
					break;

				case "ANI":
					parameter = _db.AddParameter(cmd, paramName, DbType.Int64, value);
					break;

				case "Info_digits":
					parameter = _db.AddParameter(cmd, paramName, DbType.Byte, value);
					break;

				case "Serial_number":
					parameter = _db.AddParameter(cmd, paramName, DbType.Int64, value);
					break;

				case "End_user_price":
					parameter = _db.AddParameter(cmd, paramName, DbType.Decimal, value);
					break;

				case "Used_bonus_minutes":
					parameter = _db.AddParameter(cmd, paramName, DbType.Int16, value);
					break;

				case "Reseller_price":
					parameter = _db.AddParameter(cmd, paramName, DbType.Decimal, value);
					break;

				case "Node_id":
					parameter = _db.AddParameter(cmd, paramName, DbType.Int16, value);
					break;

				case "Customer_route_id":
					parameter = _db.AddParameter(cmd, paramName, DbType.Int32, value);
					break;

				case "Mapped_disconnect_cause":
					parameter = _db.AddParameter(cmd, paramName, DbType.Int16, value);
					break;

				case "Carrier_acct_id":
					parameter = _db.AddParameter(cmd, paramName, DbType.Int16, value);
					break;

				case "Orig_dot_IP_address":
					parameter = _db.AddParameter(cmd, paramName, DbType.AnsiString, value);
					break;

				case "Dialed_number":
					parameter = _db.AddParameter(cmd, paramName, DbType.AnsiString, value);
					break;

				case "Retail_acct_id":
					parameter = _db.AddParameter(cmd, paramName, DbType.Int32, value);
					break;

				case "Customer_duration":
					parameter = _db.AddParameter(cmd, paramName, DbType.Int16, value);
					break;

				case "Carrier_duration":
					parameter = _db.AddParameter(cmd, paramName, DbType.Int16, value);
					break;

				case "Retail_duration":
					parameter = _db.AddParameter(cmd, paramName, DbType.Int16, value);
					break;

				case "Minutes":
					parameter = _db.AddParameter(cmd, paramName, DbType.Decimal, value);
					break;

				case "Carrier_minutes":
					parameter = _db.AddParameter(cmd, paramName, DbType.Decimal, value);
					break;

				case "Retail_minutes":
					parameter = _db.AddParameter(cmd, paramName, DbType.Decimal, value);
					break;

				case "Carrier_route_name":
					parameter = _db.AddParameter(cmd, paramName, DbType.AnsiString, value);
					break;

				case "Customer_route_name":
					parameter = _db.AddParameter(cmd, paramName, DbType.AnsiString, value);
					break;

				case "Orig_alias":
					parameter = _db.AddParameter(cmd, paramName, DbType.AnsiString, value);
					break;

				case "Term_alias":
					parameter = _db.AddParameter(cmd, paramName, DbType.AnsiString, value);
					break;

				case "Term_ip_address_range":
					parameter = _db.AddParameter(cmd, paramName, DbType.AnsiString, value);
					break;

				case "Customer_acct_name":
					parameter = _db.AddParameter(cmd, paramName, DbType.AnsiString, value);
					break;

				case "Orig_partner_id":
					parameter = _db.AddParameter(cmd, paramName, DbType.Int32, value);
					break;

				case "Orig_partner_name":
					parameter = _db.AddParameter(cmd, paramName, DbType.AnsiString, value);
					break;

				case "Carrier_acct_name":
					parameter = _db.AddParameter(cmd, paramName, DbType.AnsiString, value);
					break;

				case "Term_partner_id":
					parameter = _db.AddParameter(cmd, paramName, DbType.Int32, value);
					break;

				case "Term_partner_name":
					parameter = _db.AddParameter(cmd, paramName, DbType.AnsiString, value);
					break;

				case "Node_name":
					parameter = _db.AddParameter(cmd, paramName, DbType.AnsiString, value);
					break;

				default:
					throw new ArgumentException("Unknown parameter name (" + paramName + ").");
			}
			return parameter;
		}

	} // End of CDRViewCollection_Base class
}  // End of namespace

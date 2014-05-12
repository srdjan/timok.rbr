// <fileinfo name="Base\TerminationRouteViewCollection_Base.cs">
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
	/// The base class for <see cref="TerminationRouteViewCollection"/>. Provides methods 
	/// for common database view operations. 
	/// </summary>
	/// <remarks>
	/// Do not change this source code. Update the <see cref="TerminationRouteViewCollection"/>
	/// class if you need to add or change some functionality.
	/// </remarks>
	public abstract class TerminationRouteViewCollection_Base
	{
		// Constants
		public const string Carrier_route_idColumnName = "carrier_route_id";
		public const string Route_nameColumnName = "route_name";
		public const string Carrier_acct_idColumnName = "carrier_acct_id";
		public const string Carrier_acct_nameColumnName = "carrier_acct_name";
		public const string Calling_plan_idColumnName = "calling_plan_id";
		public const string Rating_typeColumnName = "rating_type";
		public const string Base_route_idColumnName = "base_route_id";
		public const string Partial_coverageColumnName = "partial_coverage";

		// Instance fields
		private Rbr_Db _db;

		/// <summary>
		/// Initializes a new instance of the <see cref="TerminationRouteViewCollection_Base"/> 
		/// class with the specified <see cref="Rbr_Db"/>.
		/// </summary>
		/// <param name="db">The <see cref="Rbr_Db"/> object.</param>
		public TerminationRouteViewCollection_Base(Rbr_Db db)
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
		/// Gets an array of all records from the <c>TerminationRouteView</c> view.
		/// </summary>
		/// <returns>An array of <see cref="TerminationRouteViewRow"/> objects.</returns>
		public virtual TerminationRouteViewRow[] GetAll()
		{
			return MapRecords(CreateGetAllCommand());
		}

		/// <summary>
		/// Gets a <see cref="System.Data.DataTable"/> object that 
		/// includes all records from the <c>TerminationRouteView</c> view.
		/// </summary>
		/// <returns>A reference to the <see cref="System.Data.DataTable"/> object.</returns>
		public virtual DataTable GetAllAsDataTable()
		{
			return MapRecordsToDataTable(CreateGetAllCommand());
		}

		/// <summary>
		/// Creates and returns an <see cref="System.Data.IDbCommand"/> object that is used
		/// to retrieve all records from the <c>TerminationRouteView</c> view.
		/// </summary>
		/// <returns>A reference to the <see cref="System.Data.IDbCommand"/> object.</returns>
		protected virtual IDbCommand CreateGetAllCommand()
		{
			return CreateGetCommand(null, null);
		}

		/// <summary>
		/// Gets the first <see cref="TerminationRouteViewRow"/> objects that 
		/// match the search condition.
		/// </summary>
		/// <param name="whereSql">The SQL search condition. For example: 
		/// <c>"FirstName='Smith' AND Zip=75038"</c>.</param>
		/// <returns>An instance of <see cref="TerminationRouteViewRow"/> or null reference 
		/// (Nothing in Visual Basic) if the object was not found.</returns>
		public TerminationRouteViewRow GetRow(string whereSql)
		{
			int totalRecordCount = -1;
			TerminationRouteViewRow[] rows = GetAsArray(whereSql, null, 0, 1, ref totalRecordCount);
			return 0 == rows.Length ? null : rows[0];
		}

		/// <summary>
		/// Gets an array of <see cref="TerminationRouteViewRow"/> objects that 
		/// match the search condition, in the the specified sort order.
		/// </summary>
		/// <param name="whereSql">The SQL search condition. For example: 
		/// <c>"FirstName='Smith' AND Zip=75038"</c>.</param>
		/// <param name="orderBySql">The column name(s) followed by "ASC" (ascending) or "DESC" (descending).
		/// Columns are sorted in ascending order by default. For example: <c>"LastName ASC, FirstName ASC"</c>.</param>
		/// <returns>An array of <see cref="TerminationRouteViewRow"/> objects.</returns>
		public TerminationRouteViewRow[] GetAsArray(string whereSql, string orderBySql)
		{
			int totalRecordCount = -1;
			return GetAsArray(whereSql, orderBySql, 0, int.MaxValue, ref totalRecordCount);
		}

		/// <summary>
		/// Gets an array of <see cref="TerminationRouteViewRow"/> objects that 
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
		/// <returns>An array of <see cref="TerminationRouteViewRow"/> objects.</returns>
		public virtual TerminationRouteViewRow[] GetAsArray(string whereSql, string orderBySql,
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
			string sql = "SELECT * FROM [dbo].[TerminationRouteView]";
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
		/// <returns>An array of <see cref="TerminationRouteViewRow"/> objects.</returns>
		protected TerminationRouteViewRow[] MapRecords(IDbCommand command)
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
		/// <returns>An array of <see cref="TerminationRouteViewRow"/> objects.</returns>
		protected TerminationRouteViewRow[] MapRecords(IDataReader reader)
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
		/// <returns>An array of <see cref="TerminationRouteViewRow"/> objects.</returns>
		protected virtual TerminationRouteViewRow[] MapRecords(IDataReader reader, 
										int startIndex, int length, ref int totalRecordCount)
		{
			if(0 > startIndex)
				throw new ArgumentOutOfRangeException("startIndex", startIndex, "StartIndex cannot be less than zero.");
			if(0 > length)
				throw new ArgumentOutOfRangeException("length", length, "Length cannot be less than zero.");

			int carrier_route_idColumnIndex = reader.GetOrdinal("carrier_route_id");
			int route_nameColumnIndex = reader.GetOrdinal("route_name");
			int carrier_acct_idColumnIndex = reader.GetOrdinal("carrier_acct_id");
			int carrier_acct_nameColumnIndex = reader.GetOrdinal("carrier_acct_name");
			int calling_plan_idColumnIndex = reader.GetOrdinal("calling_plan_id");
			int rating_typeColumnIndex = reader.GetOrdinal("rating_type");
			int base_route_idColumnIndex = reader.GetOrdinal("base_route_id");
			int partial_coverageColumnIndex = reader.GetOrdinal("partial_coverage");

			System.Collections.ArrayList recordList = new System.Collections.ArrayList();
			int ri = -startIndex;
			while(reader.Read())
			{
				ri++;
				if(ri > 0 && ri <= length)
				{
					TerminationRouteViewRow record = new TerminationRouteViewRow();
					recordList.Add(record);

					record.Carrier_route_id = Convert.ToInt32(reader.GetValue(carrier_route_idColumnIndex));
					record.Route_name = Convert.ToString(reader.GetValue(route_nameColumnIndex));
					if(!reader.IsDBNull(carrier_acct_idColumnIndex))
						record.Carrier_acct_id = Convert.ToInt16(reader.GetValue(carrier_acct_idColumnIndex));
					record.Carrier_acct_name = Convert.ToString(reader.GetValue(carrier_acct_nameColumnIndex));
					record.Calling_plan_id = Convert.ToInt32(reader.GetValue(calling_plan_idColumnIndex));
					if(!reader.IsDBNull(rating_typeColumnIndex))
						record.Rating_type = Convert.ToByte(reader.GetValue(rating_typeColumnIndex));
					record.Base_route_id = Convert.ToInt32(reader.GetValue(base_route_idColumnIndex));
					if(!reader.IsDBNull(partial_coverageColumnIndex))
						record.Partial_coverage = Convert.ToByte(reader.GetValue(partial_coverageColumnIndex));

					if(ri == length && 0 != totalRecordCount)
						break;
				}
			}

			totalRecordCount = 0 == totalRecordCount ? ri + startIndex : -1;
			return (TerminationRouteViewRow[])(recordList.ToArray(typeof(TerminationRouteViewRow)));
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
		/// Converts <see cref="System.Data.DataRow"/> to <see cref="TerminationRouteViewRow"/>.
		/// </summary>
		/// <param name="row">The <see cref="System.Data.DataRow"/> object to be mapped.</param>
		/// <returns>A reference to the <see cref="TerminationRouteViewRow"/> object.</returns>
		protected virtual TerminationRouteViewRow MapRow(DataRow row)
		{
			TerminationRouteViewRow mappedObject = new TerminationRouteViewRow();
			DataTable dataTable = row.Table;
			DataColumn dataColumn;
			// Column "Carrier_route_id"
			dataColumn = dataTable.Columns["Carrier_route_id"];
			if(!row.IsNull(dataColumn))
				mappedObject.Carrier_route_id = (int)row[dataColumn];
			// Column "Route_name"
			dataColumn = dataTable.Columns["Route_name"];
			if(!row.IsNull(dataColumn))
				mappedObject.Route_name = (string)row[dataColumn];
			// Column "Carrier_acct_id"
			dataColumn = dataTable.Columns["Carrier_acct_id"];
			if(!row.IsNull(dataColumn))
				mappedObject.Carrier_acct_id = (short)row[dataColumn];
			// Column "Carrier_acct_name"
			dataColumn = dataTable.Columns["Carrier_acct_name"];
			if(!row.IsNull(dataColumn))
				mappedObject.Carrier_acct_name = (string)row[dataColumn];
			// Column "Calling_plan_id"
			dataColumn = dataTable.Columns["Calling_plan_id"];
			if(!row.IsNull(dataColumn))
				mappedObject.Calling_plan_id = (int)row[dataColumn];
			// Column "Rating_type"
			dataColumn = dataTable.Columns["Rating_type"];
			if(!row.IsNull(dataColumn))
				mappedObject.Rating_type = (byte)row[dataColumn];
			// Column "Base_route_id"
			dataColumn = dataTable.Columns["Base_route_id"];
			if(!row.IsNull(dataColumn))
				mappedObject.Base_route_id = (int)row[dataColumn];
			// Column "Partial_coverage"
			dataColumn = dataTable.Columns["Partial_coverage"];
			if(!row.IsNull(dataColumn))
				mappedObject.Partial_coverage = (byte)row[dataColumn];
			return mappedObject;
		}

		/// <summary>
		/// Creates a <see cref="System.Data.DataTable"/> object for 
		/// the <c>TerminationRouteView</c> view.
		/// </summary>
		/// <returns>A reference to the <see cref="System.Data.DataTable"/> object.</returns>
		protected virtual DataTable CreateDataTable()
		{
			DataTable dataTable = new DataTable();
			dataTable.TableName = "TerminationRouteView";
			DataColumn dataColumn;
			dataColumn = dataTable.Columns.Add("Carrier_route_id", typeof(int));
			dataColumn.Caption = "carrier_route_id";
			dataColumn.AllowDBNull = false;
			dataColumn.ReadOnly = true;
			dataColumn = dataTable.Columns.Add("Route_name", typeof(string));
			dataColumn.Caption = "route_name";
			dataColumn.MaxLength = 1;
			dataColumn.AllowDBNull = false;
			dataColumn.ReadOnly = true;
			dataColumn = dataTable.Columns.Add("Carrier_acct_id", typeof(short));
			dataColumn.Caption = "carrier_acct_id";
			dataColumn.ReadOnly = true;
			dataColumn = dataTable.Columns.Add("Carrier_acct_name", typeof(string));
			dataColumn.Caption = "carrier_acct_name";
			dataColumn.MaxLength = 1;
			dataColumn.AllowDBNull = false;
			dataColumn.ReadOnly = true;
			dataColumn = dataTable.Columns.Add("Calling_plan_id", typeof(int));
			dataColumn.Caption = "calling_plan_id";
			dataColumn.AllowDBNull = false;
			dataColumn.ReadOnly = true;
			dataColumn = dataTable.Columns.Add("Rating_type", typeof(byte));
			dataColumn.Caption = "rating_type";
			dataColumn.ReadOnly = true;
			dataColumn = dataTable.Columns.Add("Base_route_id", typeof(int));
			dataColumn.Caption = "base_route_id";
			dataColumn.AllowDBNull = false;
			dataColumn.ReadOnly = true;
			dataColumn = dataTable.Columns.Add("Partial_coverage", typeof(byte));
			dataColumn.Caption = "partial_coverage";
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

				case "Route_name":
					parameter = _db.AddParameter(cmd, paramName, DbType.AnsiString, value);
					break;

				case "Carrier_acct_id":
					parameter = _db.AddParameter(cmd, paramName, DbType.Int16, value);
					break;

				case "Carrier_acct_name":
					parameter = _db.AddParameter(cmd, paramName, DbType.AnsiString, value);
					break;

				case "Calling_plan_id":
					parameter = _db.AddParameter(cmd, paramName, DbType.Int32, value);
					break;

				case "Rating_type":
					parameter = _db.AddParameter(cmd, paramName, DbType.Byte, value);
					break;

				case "Base_route_id":
					parameter = _db.AddParameter(cmd, paramName, DbType.Int32, value);
					break;

				case "Partial_coverage":
					parameter = _db.AddParameter(cmd, paramName, DbType.Byte, value);
					break;

				default:
					throw new ArgumentException("Unknown parameter name (" + paramName + ").");
			}
			return parameter;
		}

	} // End of TerminationRouteViewCollection_Base class
}  // End of namespace

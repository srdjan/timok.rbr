// <fileinfo name="Base\ContactInfoCollection_Base.cs">
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
	/// The base class for <see cref="ContactInfoCollection"/>. Provides methods 
	/// for common database table operations. 
	/// </summary>
	/// <remarks>
	/// Do not change this source code. Update the <see cref="ContactInfoCollection"/>
	/// class if you need to add or change some functionality.
	/// </remarks>
	public abstract class ContactInfoCollection_Base
	{
		// Constants
		public const string Contact_info_idColumnName = "contact_info_id";
		public const string Address1ColumnName = "address1";
		public const string Address2ColumnName = "address2";
		public const string CityColumnName = "city";
		public const string StateColumnName = "state";
		public const string Zip_codeColumnName = "zip_code";
		public const string EmailColumnName = "email";
		public const string Home_phone_numberColumnName = "home_phone_number";
		public const string Cell_phone_numberColumnName = "cell_phone_number";
		public const string Work_phone_numberColumnName = "work_phone_number";

		// Instance fields
		private Rbr_Db _db;

		/// <summary>
		/// Initializes a new instance of the <see cref="ContactInfoCollection_Base"/> 
		/// class with the specified <see cref="Rbr_Db"/>.
		/// </summary>
		/// <param name="db">The <see cref="Rbr_Db"/> object.</param>
		public ContactInfoCollection_Base(Rbr_Db db)
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
		/// Gets an array of all records from the <c>ContactInfo</c> table.
		/// </summary>
		/// <returns>An array of <see cref="ContactInfoRow"/> objects.</returns>
		public virtual ContactInfoRow[] GetAll()
		{
			return MapRecords(CreateGetAllCommand());
		}

		/// <summary>
		/// Gets a <see cref="System.Data.DataTable"/> object that 
		/// includes all records from the <c>ContactInfo</c> table.
		/// </summary>
		/// <returns>A reference to the <see cref="System.Data.DataTable"/> object.</returns>
		public virtual DataTable GetAllAsDataTable()
		{
			return MapRecordsToDataTable(CreateGetAllCommand());
		}

		/// <summary>
		/// Creates and returns an <see cref="System.Data.IDbCommand"/> object that is used
		/// to retrieve all records from the <c>ContactInfo</c> table.
		/// </summary>
		/// <returns>A reference to the <see cref="System.Data.IDbCommand"/> object.</returns>
		protected virtual IDbCommand CreateGetAllCommand()
		{
			return CreateGetCommand(null, null);
		}

		/// <summary>
		/// Gets the first <see cref="ContactInfoRow"/> objects that 
		/// match the search condition.
		/// </summary>
		/// <param name="whereSql">The SQL search condition. For example: 
		/// <c>"FirstName='Smith' AND Zip=75038"</c>.</param>
		/// <returns>An instance of <see cref="ContactInfoRow"/> or null reference 
		/// (Nothing in Visual Basic) if the object was not found.</returns>
		public ContactInfoRow GetRow(string whereSql)
		{
			int totalRecordCount = -1;
			ContactInfoRow[] rows = GetAsArray(whereSql, null, 0, 1, ref totalRecordCount);
			return 0 == rows.Length ? null : rows[0];
		}

		/// <summary>
		/// Gets an array of <see cref="ContactInfoRow"/> objects that 
		/// match the search condition, in the the specified sort order.
		/// </summary>
		/// <param name="whereSql">The SQL search condition. For example: 
		/// <c>"FirstName='Smith' AND Zip=75038"</c>.</param>
		/// <param name="orderBySql">The column name(s) followed by "ASC" (ascending) or "DESC" (descending).
		/// Columns are sorted in ascending order by default. For example: <c>"LastName ASC, FirstName ASC"</c>.</param>
		/// <returns>An array of <see cref="ContactInfoRow"/> objects.</returns>
		public ContactInfoRow[] GetAsArray(string whereSql, string orderBySql)
		{
			int totalRecordCount = -1;
			return GetAsArray(whereSql, orderBySql, 0, int.MaxValue, ref totalRecordCount);
		}

		/// <summary>
		/// Gets an array of <see cref="ContactInfoRow"/> objects that 
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
		/// <returns>An array of <see cref="ContactInfoRow"/> objects.</returns>
		public virtual ContactInfoRow[] GetAsArray(string whereSql, string orderBySql,
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
			string sql = "SELECT * FROM [dbo].[ContactInfo]";
			if(null != whereSql && 0 < whereSql.Length)
				sql += " WHERE " + whereSql;
			if(null != orderBySql && 0 < orderBySql.Length)
				sql += " ORDER BY " + orderBySql;
			return _db.CreateCommand(sql);
		}

		/// <summary>
		/// Gets <see cref="ContactInfoRow"/> by the primary key.
		/// </summary>
		/// <param name="contact_info_id">The <c>contact_info_id</c> column value.</param>
		/// <returns>An instance of <see cref="ContactInfoRow"/> or null reference 
		/// (Nothing in Visual Basic) if the object was not found.</returns>
		public virtual ContactInfoRow GetByPrimaryKey(int contact_info_id)
		{
			string whereSql = "[contact_info_id]=" + _db.CreateSqlParameterName("Contact_info_id");
			IDbCommand cmd = CreateGetCommand(whereSql, null);
			AddParameter(cmd, "Contact_info_id", contact_info_id);
			ContactInfoRow[] tempArray = MapRecords(cmd);
			return 0 == tempArray.Length ? null : tempArray[0];
		}

		/// <summary>
		/// Adds a new record into the <c>ContactInfo</c> table.
		/// </summary>
		/// <param name="value">The <see cref="ContactInfoRow"/> object to be inserted.</param>
		public virtual void Insert(ContactInfoRow value)
		{
			string sqlStr = "INSERT INTO [dbo].[ContactInfo] (" +
				"[contact_info_id], " +
				"[address1], " +
				"[address2], " +
				"[city], " +
				"[state], " +
				"[zip_code], " +
				"[email], " +
				"[home_phone_number], " +
				"[cell_phone_number], " +
				"[work_phone_number]" +
				") VALUES (" +
				_db.CreateSqlParameterName("Contact_info_id") + ", " +
				_db.CreateSqlParameterName("Address1") + ", " +
				_db.CreateSqlParameterName("Address2") + ", " +
				_db.CreateSqlParameterName("City") + ", " +
				_db.CreateSqlParameterName("State") + ", " +
				_db.CreateSqlParameterName("Zip_code") + ", " +
				_db.CreateSqlParameterName("Email") + ", " +
				_db.CreateSqlParameterName("Home_phone_number") + ", " +
				_db.CreateSqlParameterName("Cell_phone_number") + ", " +
				_db.CreateSqlParameterName("Work_phone_number") + ")";
			IDbCommand cmd = _db.CreateCommand(sqlStr);
			AddParameter(cmd, "Contact_info_id", value.Contact_info_id);
			AddParameter(cmd, "Address1", value.Address1);
			AddParameter(cmd, "Address2", value.Address2);
			AddParameter(cmd, "City", value.City);
			AddParameter(cmd, "State", value.State);
			AddParameter(cmd, "Zip_code", value.Zip_code);
			AddParameter(cmd, "Email", value.Email);
			AddParameter(cmd, "Home_phone_number", value.Home_phone_number);
			AddParameter(cmd, "Cell_phone_number", value.Cell_phone_number);
			AddParameter(cmd, "Work_phone_number", value.Work_phone_number);
			cmd.ExecuteNonQuery();
		}

		/// <summary>
		/// Updates a record in the <c>ContactInfo</c> table.
		/// </summary>
		/// <param name="value">The <see cref="ContactInfoRow"/>
		/// object used to update the table record.</param>
		/// <returns>true if the record was updated; otherwise, false.</returns>
		public virtual bool Update(ContactInfoRow value)
		{
			string sqlStr = "UPDATE [dbo].[ContactInfo] SET " +
				"[address1]=" + _db.CreateSqlParameterName("Address1") + ", " +
				"[address2]=" + _db.CreateSqlParameterName("Address2") + ", " +
				"[city]=" + _db.CreateSqlParameterName("City") + ", " +
				"[state]=" + _db.CreateSqlParameterName("State") + ", " +
				"[zip_code]=" + _db.CreateSqlParameterName("Zip_code") + ", " +
				"[email]=" + _db.CreateSqlParameterName("Email") + ", " +
				"[home_phone_number]=" + _db.CreateSqlParameterName("Home_phone_number") + ", " +
				"[cell_phone_number]=" + _db.CreateSqlParameterName("Cell_phone_number") + ", " +
				"[work_phone_number]=" + _db.CreateSqlParameterName("Work_phone_number") +
				" WHERE " +
				"[contact_info_id]=" + _db.CreateSqlParameterName("Contact_info_id");
			IDbCommand cmd = _db.CreateCommand(sqlStr);
			AddParameter(cmd, "Address1", value.Address1);
			AddParameter(cmd, "Address2", value.Address2);
			AddParameter(cmd, "City", value.City);
			AddParameter(cmd, "State", value.State);
			AddParameter(cmd, "Zip_code", value.Zip_code);
			AddParameter(cmd, "Email", value.Email);
			AddParameter(cmd, "Home_phone_number", value.Home_phone_number);
			AddParameter(cmd, "Cell_phone_number", value.Cell_phone_number);
			AddParameter(cmd, "Work_phone_number", value.Work_phone_number);
			AddParameter(cmd, "Contact_info_id", value.Contact_info_id);
			return 0 != cmd.ExecuteNonQuery();
		}

		/// <summary>
		/// Updates the <c>ContactInfo</c> table and calls the <c>AcceptChanges</c> method
		/// on the changed DataRow objects.
		/// </summary>
		/// <param name="table">The <see cref="System.Data.DataTable"/> used to update the data source.</param>
		public void Update(DataTable table)
		{
			Update(table, true);
		}

		/// <summary>
		/// Updates the <c>ContactInfo</c> table. Pass <c>false</c> as the <c>acceptChanges</c> 
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
							DeleteByPrimaryKey((int)row["Contact_info_id"]);
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
		/// Deletes the specified object from the <c>ContactInfo</c> table.
		/// </summary>
		/// <param name="value">The <see cref="ContactInfoRow"/> object to delete.</param>
		/// <returns>true if the record was deleted; otherwise, false.</returns>
		public bool Delete(ContactInfoRow value)
		{
			return DeleteByPrimaryKey(value.Contact_info_id);
		}

		/// <summary>
		/// Deletes a record from the <c>ContactInfo</c> table using
		/// the specified primary key.
		/// </summary>
		/// <param name="contact_info_id">The <c>contact_info_id</c> column value.</param>
		/// <returns>true if the record was deleted; otherwise, false.</returns>
		public virtual bool DeleteByPrimaryKey(int contact_info_id)
		{
			string whereSql = "[contact_info_id]=" + _db.CreateSqlParameterName("Contact_info_id");
			IDbCommand cmd = CreateDeleteCommand(whereSql);
			AddParameter(cmd, "Contact_info_id", contact_info_id);
			return 0 < cmd.ExecuteNonQuery();
		}

		/// <summary>
		/// Deletes <c>ContactInfo</c> records that match the specified criteria.
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
		/// to delete <c>ContactInfo</c> records that match the specified criteria.
		/// </summary>
		/// <param name="whereSql">The SQL search condition. 
		/// For example: <c>"FirstName='Smith' AND Zip=75038"</c>.</param>
		/// <returns>A reference to the <see cref="System.Data.IDbCommand"/> object.</returns>
		protected virtual IDbCommand CreateDeleteCommand(string whereSql)
		{
			string sql = "DELETE FROM [dbo].[ContactInfo]";
			if(null != whereSql && 0 < whereSql.Length)
				sql += " WHERE " + whereSql;
			return _db.CreateCommand(sql);
		}

		/// <summary>
		/// Deletes all records from the <c>ContactInfo</c> table.
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
		/// <returns>An array of <see cref="ContactInfoRow"/> objects.</returns>
		protected ContactInfoRow[] MapRecords(IDbCommand command)
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
		/// <returns>An array of <see cref="ContactInfoRow"/> objects.</returns>
		protected ContactInfoRow[] MapRecords(IDataReader reader)
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
		/// <returns>An array of <see cref="ContactInfoRow"/> objects.</returns>
		protected virtual ContactInfoRow[] MapRecords(IDataReader reader, 
										int startIndex, int length, ref int totalRecordCount)
		{
			if(0 > startIndex)
				throw new ArgumentOutOfRangeException("startIndex", startIndex, "StartIndex cannot be less than zero.");
			if(0 > length)
				throw new ArgumentOutOfRangeException("length", length, "Length cannot be less than zero.");

			int contact_info_idColumnIndex = reader.GetOrdinal("contact_info_id");
			int address1ColumnIndex = reader.GetOrdinal("address1");
			int address2ColumnIndex = reader.GetOrdinal("address2");
			int cityColumnIndex = reader.GetOrdinal("city");
			int stateColumnIndex = reader.GetOrdinal("state");
			int zip_codeColumnIndex = reader.GetOrdinal("zip_code");
			int emailColumnIndex = reader.GetOrdinal("email");
			int home_phone_numberColumnIndex = reader.GetOrdinal("home_phone_number");
			int cell_phone_numberColumnIndex = reader.GetOrdinal("cell_phone_number");
			int work_phone_numberColumnIndex = reader.GetOrdinal("work_phone_number");

			System.Collections.ArrayList recordList = new System.Collections.ArrayList();
			int ri = -startIndex;
			while(reader.Read())
			{
				ri++;
				if(ri > 0 && ri <= length)
				{
					ContactInfoRow record = new ContactInfoRow();
					recordList.Add(record);

					record.Contact_info_id = Convert.ToInt32(reader.GetValue(contact_info_idColumnIndex));
					record.Address1 = Convert.ToString(reader.GetValue(address1ColumnIndex));
					record.Address2 = Convert.ToString(reader.GetValue(address2ColumnIndex));
					record.City = Convert.ToString(reader.GetValue(cityColumnIndex));
					record.State = Convert.ToString(reader.GetValue(stateColumnIndex));
					record.Zip_code = Convert.ToString(reader.GetValue(zip_codeColumnIndex));
					record.Email = Convert.ToString(reader.GetValue(emailColumnIndex));
					record.Home_phone_number = Convert.ToInt64(reader.GetValue(home_phone_numberColumnIndex));
					record.Cell_phone_number = Convert.ToInt64(reader.GetValue(cell_phone_numberColumnIndex));
					record.Work_phone_number = Convert.ToInt64(reader.GetValue(work_phone_numberColumnIndex));

					if(ri == length && 0 != totalRecordCount)
						break;
				}
			}

			totalRecordCount = 0 == totalRecordCount ? ri + startIndex : -1;
			return (ContactInfoRow[])(recordList.ToArray(typeof(ContactInfoRow)));
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
		/// Converts <see cref="System.Data.DataRow"/> to <see cref="ContactInfoRow"/>.
		/// </summary>
		/// <param name="row">The <see cref="System.Data.DataRow"/> object to be mapped.</param>
		/// <returns>A reference to the <see cref="ContactInfoRow"/> object.</returns>
		protected virtual ContactInfoRow MapRow(DataRow row)
		{
			ContactInfoRow mappedObject = new ContactInfoRow();
			DataTable dataTable = row.Table;
			DataColumn dataColumn;
			// Column "Contact_info_id"
			dataColumn = dataTable.Columns["Contact_info_id"];
			if(!row.IsNull(dataColumn))
				mappedObject.Contact_info_id = (int)row[dataColumn];
			// Column "Address1"
			dataColumn = dataTable.Columns["Address1"];
			if(!row.IsNull(dataColumn))
				mappedObject.Address1 = (string)row[dataColumn];
			// Column "Address2"
			dataColumn = dataTable.Columns["Address2"];
			if(!row.IsNull(dataColumn))
				mappedObject.Address2 = (string)row[dataColumn];
			// Column "City"
			dataColumn = dataTable.Columns["City"];
			if(!row.IsNull(dataColumn))
				mappedObject.City = (string)row[dataColumn];
			// Column "State"
			dataColumn = dataTable.Columns["State"];
			if(!row.IsNull(dataColumn))
				mappedObject.State = (string)row[dataColumn];
			// Column "Zip_code"
			dataColumn = dataTable.Columns["Zip_code"];
			if(!row.IsNull(dataColumn))
				mappedObject.Zip_code = (string)row[dataColumn];
			// Column "Email"
			dataColumn = dataTable.Columns["Email"];
			if(!row.IsNull(dataColumn))
				mappedObject.Email = (string)row[dataColumn];
			// Column "Home_phone_number"
			dataColumn = dataTable.Columns["Home_phone_number"];
			if(!row.IsNull(dataColumn))
				mappedObject.Home_phone_number = (long)row[dataColumn];
			// Column "Cell_phone_number"
			dataColumn = dataTable.Columns["Cell_phone_number"];
			if(!row.IsNull(dataColumn))
				mappedObject.Cell_phone_number = (long)row[dataColumn];
			// Column "Work_phone_number"
			dataColumn = dataTable.Columns["Work_phone_number"];
			if(!row.IsNull(dataColumn))
				mappedObject.Work_phone_number = (long)row[dataColumn];
			return mappedObject;
		}

		/// <summary>
		/// Creates a <see cref="System.Data.DataTable"/> object for 
		/// the <c>ContactInfo</c> table.
		/// </summary>
		/// <returns>A reference to the <see cref="System.Data.DataTable"/> object.</returns>
		protected virtual DataTable CreateDataTable()
		{
			DataTable dataTable = new DataTable();
			dataTable.TableName = "ContactInfo";
			DataColumn dataColumn;
			dataColumn = dataTable.Columns.Add("Contact_info_id", typeof(int));
			dataColumn.Caption = "contact_info_id";
			dataColumn.AllowDBNull = false;
			dataColumn = dataTable.Columns.Add("Address1", typeof(string));
			dataColumn.Caption = "address1";
			dataColumn.MaxLength = 50;
			dataColumn.AllowDBNull = false;
			dataColumn = dataTable.Columns.Add("Address2", typeof(string));
			dataColumn.Caption = "address2";
			dataColumn.MaxLength = 50;
			dataColumn.AllowDBNull = false;
			dataColumn = dataTable.Columns.Add("City", typeof(string));
			dataColumn.Caption = "city";
			dataColumn.MaxLength = 50;
			dataColumn.AllowDBNull = false;
			dataColumn = dataTable.Columns.Add("State", typeof(string));
			dataColumn.Caption = "state";
			dataColumn.MaxLength = 50;
			dataColumn.AllowDBNull = false;
			dataColumn = dataTable.Columns.Add("Zip_code", typeof(string));
			dataColumn.Caption = "zip_code";
			dataColumn.MaxLength = 12;
			dataColumn.AllowDBNull = false;
			dataColumn = dataTable.Columns.Add("Email", typeof(string));
			dataColumn.Caption = "email";
			dataColumn.MaxLength = 256;
			dataColumn.AllowDBNull = false;
			dataColumn = dataTable.Columns.Add("Home_phone_number", typeof(long));
			dataColumn.Caption = "home_phone_number";
			dataColumn.AllowDBNull = false;
			dataColumn = dataTable.Columns.Add("Cell_phone_number", typeof(long));
			dataColumn.Caption = "cell_phone_number";
			dataColumn.AllowDBNull = false;
			dataColumn = dataTable.Columns.Add("Work_phone_number", typeof(long));
			dataColumn.Caption = "work_phone_number";
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
				case "Contact_info_id":
					parameter = _db.AddParameter(cmd, paramName, DbType.Int32, value);
					break;

				case "Address1":
					parameter = _db.AddParameter(cmd, paramName, DbType.AnsiString, value);
					break;

				case "Address2":
					parameter = _db.AddParameter(cmd, paramName, DbType.AnsiString, value);
					break;

				case "City":
					parameter = _db.AddParameter(cmd, paramName, DbType.AnsiString, value);
					break;

				case "State":
					parameter = _db.AddParameter(cmd, paramName, DbType.AnsiString, value);
					break;

				case "Zip_code":
					parameter = _db.AddParameter(cmd, paramName, DbType.AnsiString, value);
					break;

				case "Email":
					parameter = _db.AddParameter(cmd, paramName, DbType.AnsiString, value);
					break;

				case "Home_phone_number":
					parameter = _db.AddParameter(cmd, paramName, DbType.Int64, value);
					break;

				case "Cell_phone_number":
					parameter = _db.AddParameter(cmd, paramName, DbType.Int64, value);
					break;

				case "Work_phone_number":
					parameter = _db.AddParameter(cmd, paramName, DbType.Int64, value);
					break;

				default:
					throw new ArgumentException("Unknown parameter name (" + paramName + ").");
			}
			return parameter;
		}

	} // End of ContactInfoCollection_Base class
}  // End of namespace

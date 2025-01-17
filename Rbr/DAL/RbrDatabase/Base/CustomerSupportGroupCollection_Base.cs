// <fileinfo name="Base\CustomerSupportGroupCollection_Base.cs">
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
	/// The base class for <see cref="CustomerSupportGroupCollection"/>. Provides methods 
	/// for common database table operations. 
	/// </summary>
	/// <remarks>
	/// Do not change this source code. Update the <see cref="CustomerSupportGroupCollection"/>
	/// class if you need to add or change some functionality.
	/// </remarks>
	public abstract class CustomerSupportGroupCollection_Base
	{
		// Constants
		public const string Group_idColumnName = "group_id";
		public const string DescriptionColumnName = "description";
		public const string RoleColumnName = "role";
		public const string Max_amountColumnName = "max_amount";
		public const string Allow_status_changeColumnName = "allow_status_change";
		public const string Vendor_idColumnName = "vendor_id";

		// Instance fields
		private Rbr_Db _db;

		/// <summary>
		/// Initializes a new instance of the <see cref="CustomerSupportGroupCollection_Base"/> 
		/// class with the specified <see cref="Rbr_Db"/>.
		/// </summary>
		/// <param name="db">The <see cref="Rbr_Db"/> object.</param>
		public CustomerSupportGroupCollection_Base(Rbr_Db db)
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
		/// Gets an array of all records from the <c>CustomerSupportGroup</c> table.
		/// </summary>
		/// <returns>An array of <see cref="CustomerSupportGroupRow"/> objects.</returns>
		public virtual CustomerSupportGroupRow[] GetAll()
		{
			return MapRecords(CreateGetAllCommand());
		}

		/// <summary>
		/// Gets a <see cref="System.Data.DataTable"/> object that 
		/// includes all records from the <c>CustomerSupportGroup</c> table.
		/// </summary>
		/// <returns>A reference to the <see cref="System.Data.DataTable"/> object.</returns>
		public virtual DataTable GetAllAsDataTable()
		{
			return MapRecordsToDataTable(CreateGetAllCommand());
		}

		/// <summary>
		/// Creates and returns an <see cref="System.Data.IDbCommand"/> object that is used
		/// to retrieve all records from the <c>CustomerSupportGroup</c> table.
		/// </summary>
		/// <returns>A reference to the <see cref="System.Data.IDbCommand"/> object.</returns>
		protected virtual IDbCommand CreateGetAllCommand()
		{
			return CreateGetCommand(null, null);
		}

		/// <summary>
		/// Gets the first <see cref="CustomerSupportGroupRow"/> objects that 
		/// match the search condition.
		/// </summary>
		/// <param name="whereSql">The SQL search condition. For example: 
		/// <c>"FirstName='Smith' AND Zip=75038"</c>.</param>
		/// <returns>An instance of <see cref="CustomerSupportGroupRow"/> or null reference 
		/// (Nothing in Visual Basic) if the object was not found.</returns>
		public CustomerSupportGroupRow GetRow(string whereSql)
		{
			int totalRecordCount = -1;
			CustomerSupportGroupRow[] rows = GetAsArray(whereSql, null, 0, 1, ref totalRecordCount);
			return 0 == rows.Length ? null : rows[0];
		}

		/// <summary>
		/// Gets an array of <see cref="CustomerSupportGroupRow"/> objects that 
		/// match the search condition, in the the specified sort order.
		/// </summary>
		/// <param name="whereSql">The SQL search condition. For example: 
		/// <c>"FirstName='Smith' AND Zip=75038"</c>.</param>
		/// <param name="orderBySql">The column name(s) followed by "ASC" (ascending) or "DESC" (descending).
		/// Columns are sorted in ascending order by default. For example: <c>"LastName ASC, FirstName ASC"</c>.</param>
		/// <returns>An array of <see cref="CustomerSupportGroupRow"/> objects.</returns>
		public CustomerSupportGroupRow[] GetAsArray(string whereSql, string orderBySql)
		{
			int totalRecordCount = -1;
			return GetAsArray(whereSql, orderBySql, 0, int.MaxValue, ref totalRecordCount);
		}

		/// <summary>
		/// Gets an array of <see cref="CustomerSupportGroupRow"/> objects that 
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
		/// <returns>An array of <see cref="CustomerSupportGroupRow"/> objects.</returns>
		public virtual CustomerSupportGroupRow[] GetAsArray(string whereSql, string orderBySql,
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
			string sql = "SELECT * FROM [dbo].[CustomerSupportGroup]";
			if(null != whereSql && 0 < whereSql.Length)
				sql += " WHERE " + whereSql;
			if(null != orderBySql && 0 < orderBySql.Length)
				sql += " ORDER BY " + orderBySql;
			return _db.CreateCommand(sql);
		}

		/// <summary>
		/// Gets <see cref="CustomerSupportGroupRow"/> by the primary key.
		/// </summary>
		/// <param name="group_id">The <c>group_id</c> column value.</param>
		/// <returns>An instance of <see cref="CustomerSupportGroupRow"/> or null reference 
		/// (Nothing in Visual Basic) if the object was not found.</returns>
		public virtual CustomerSupportGroupRow GetByPrimaryKey(int group_id)
		{
			string whereSql = "[group_id]=" + _db.CreateSqlParameterName("Group_id");
			IDbCommand cmd = CreateGetCommand(whereSql, null);
			AddParameter(cmd, "Group_id", group_id);
			CustomerSupportGroupRow[] tempArray = MapRecords(cmd);
			return 0 == tempArray.Length ? null : tempArray[0];
		}

		/// <summary>
		/// Gets an array of <see cref="CustomerSupportGroupRow"/> objects 
		/// by the <c>R_313</c> foreign key.
		/// </summary>
		/// <param name="vendor_id">The <c>vendor_id</c> column value.</param>
		/// <returns>An array of <see cref="CustomerSupportGroupRow"/> objects.</returns>
		public virtual CustomerSupportGroupRow[] GetByVendor_id(int vendor_id)
		{
			return MapRecords(CreateGetByVendor_idCommand(vendor_id));
		}

		/// <summary>
		/// Gets a <see cref="System.Data.DataTable"/> object 
		/// by the <c>R_313</c> foreign key.
		/// </summary>
		/// <param name="vendor_id">The <c>vendor_id</c> column value.</param>
		/// <returns>A reference to the <see cref="System.Data.DataTable"/> object.</returns>
		public virtual DataTable GetByVendor_idAsDataTable(int vendor_id)
		{
			return MapRecordsToDataTable(CreateGetByVendor_idCommand(vendor_id));
		}

		/// <summary>
		/// Creates an <see cref="System.Data.IDbCommand"/> object that can be used to 
		/// return records by the <c>R_313</c> foreign key.
		/// </summary>
		/// <param name="vendor_id">The <c>vendor_id</c> column value.</param>
		/// <returns>A reference to the <see cref="System.Data.IDbCommand"/> object.</returns>
		protected virtual IDbCommand CreateGetByVendor_idCommand(int vendor_id)
		{
			string whereSql = "";
			whereSql += "[vendor_id]=" + _db.CreateSqlParameterName("Vendor_id");

			IDbCommand cmd = CreateGetCommand(whereSql, null);
			AddParameter(cmd, "Vendor_id", vendor_id);
			return cmd;
		}

		/// <summary>
		/// Adds a new record into the <c>CustomerSupportGroup</c> table.
		/// </summary>
		/// <param name="value">The <see cref="CustomerSupportGroupRow"/> object to be inserted.</param>
		public virtual void Insert(CustomerSupportGroupRow value)
		{
			string sqlStr = "INSERT INTO [dbo].[CustomerSupportGroup] (" +
				"[group_id], " +
				"[description], " +
				"[role], " +
				"[max_amount], " +
				"[allow_status_change], " +
				"[vendor_id]" +
				") VALUES (" +
				_db.CreateSqlParameterName("Group_id") + ", " +
				_db.CreateSqlParameterName("Description") + ", " +
				_db.CreateSqlParameterName("Role") + ", " +
				_db.CreateSqlParameterName("Max_amount") + ", " +
				_db.CreateSqlParameterName("Allow_status_change") + ", " +
				_db.CreateSqlParameterName("Vendor_id") + ")";
			IDbCommand cmd = _db.CreateCommand(sqlStr);
			AddParameter(cmd, "Group_id", value.Group_id);
			AddParameter(cmd, "Description", value.Description);
			AddParameter(cmd, "Role", value.Role);
			AddParameter(cmd, "Max_amount", value.Max_amount);
			AddParameter(cmd, "Allow_status_change", value.Allow_status_change);
			AddParameter(cmd, "Vendor_id", value.Vendor_id);
			cmd.ExecuteNonQuery();
		}

		/// <summary>
		/// Updates a record in the <c>CustomerSupportGroup</c> table.
		/// </summary>
		/// <param name="value">The <see cref="CustomerSupportGroupRow"/>
		/// object used to update the table record.</param>
		/// <returns>true if the record was updated; otherwise, false.</returns>
		public virtual bool Update(CustomerSupportGroupRow value)
		{
			string sqlStr = "UPDATE [dbo].[CustomerSupportGroup] SET " +
				"[description]=" + _db.CreateSqlParameterName("Description") + ", " +
				"[role]=" + _db.CreateSqlParameterName("Role") + ", " +
				"[max_amount]=" + _db.CreateSqlParameterName("Max_amount") + ", " +
				"[allow_status_change]=" + _db.CreateSqlParameterName("Allow_status_change") + ", " +
				"[vendor_id]=" + _db.CreateSqlParameterName("Vendor_id") +
				" WHERE " +
				"[group_id]=" + _db.CreateSqlParameterName("Group_id");
			IDbCommand cmd = _db.CreateCommand(sqlStr);
			AddParameter(cmd, "Description", value.Description);
			AddParameter(cmd, "Role", value.Role);
			AddParameter(cmd, "Max_amount", value.Max_amount);
			AddParameter(cmd, "Allow_status_change", value.Allow_status_change);
			AddParameter(cmd, "Vendor_id", value.Vendor_id);
			AddParameter(cmd, "Group_id", value.Group_id);
			return 0 != cmd.ExecuteNonQuery();
		}

		/// <summary>
		/// Updates the <c>CustomerSupportGroup</c> table and calls the <c>AcceptChanges</c> method
		/// on the changed DataRow objects.
		/// </summary>
		/// <param name="table">The <see cref="System.Data.DataTable"/> used to update the data source.</param>
		public void Update(DataTable table)
		{
			Update(table, true);
		}

		/// <summary>
		/// Updates the <c>CustomerSupportGroup</c> table. Pass <c>false</c> as the <c>acceptChanges</c> 
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
							DeleteByPrimaryKey((int)row["Group_id"]);
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
		/// Deletes the specified object from the <c>CustomerSupportGroup</c> table.
		/// </summary>
		/// <param name="value">The <see cref="CustomerSupportGroupRow"/> object to delete.</param>
		/// <returns>true if the record was deleted; otherwise, false.</returns>
		public bool Delete(CustomerSupportGroupRow value)
		{
			return DeleteByPrimaryKey(value.Group_id);
		}

		/// <summary>
		/// Deletes a record from the <c>CustomerSupportGroup</c> table using
		/// the specified primary key.
		/// </summary>
		/// <param name="group_id">The <c>group_id</c> column value.</param>
		/// <returns>true if the record was deleted; otherwise, false.</returns>
		public virtual bool DeleteByPrimaryKey(int group_id)
		{
			string whereSql = "[group_id]=" + _db.CreateSqlParameterName("Group_id");
			IDbCommand cmd = CreateDeleteCommand(whereSql);
			AddParameter(cmd, "Group_id", group_id);
			return 0 < cmd.ExecuteNonQuery();
		}

		/// <summary>
		/// Deletes records from the <c>CustomerSupportGroup</c> table using the 
		/// <c>R_313</c> foreign key.
		/// </summary>
		/// <param name="vendor_id">The <c>vendor_id</c> column value.</param>
		/// <returns>The number of records deleted from the table.</returns>
		public int DeleteByVendor_id(int vendor_id)
		{
			return CreateDeleteByVendor_idCommand(vendor_id).ExecuteNonQuery();
		}

		/// <summary>
		/// Creates an <see cref="System.Data.IDbCommand"/> object that can be used to
		/// delete records using the <c>R_313</c> foreign key.
		/// </summary>
		/// <param name="vendor_id">The <c>vendor_id</c> column value.</param>
		/// <returns>A reference to the <see cref="System.Data.IDbCommand"/> object.</returns>
		protected virtual IDbCommand CreateDeleteByVendor_idCommand(int vendor_id)
		{
			string whereSql = "";
			whereSql += "[vendor_id]=" + _db.CreateSqlParameterName("Vendor_id");

			IDbCommand cmd = CreateDeleteCommand(whereSql);
			AddParameter(cmd, "Vendor_id", vendor_id);
			return cmd;
		}

		/// <summary>
		/// Deletes <c>CustomerSupportGroup</c> records that match the specified criteria.
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
		/// to delete <c>CustomerSupportGroup</c> records that match the specified criteria.
		/// </summary>
		/// <param name="whereSql">The SQL search condition. 
		/// For example: <c>"FirstName='Smith' AND Zip=75038"</c>.</param>
		/// <returns>A reference to the <see cref="System.Data.IDbCommand"/> object.</returns>
		protected virtual IDbCommand CreateDeleteCommand(string whereSql)
		{
			string sql = "DELETE FROM [dbo].[CustomerSupportGroup]";
			if(null != whereSql && 0 < whereSql.Length)
				sql += " WHERE " + whereSql;
			return _db.CreateCommand(sql);
		}

		/// <summary>
		/// Deletes all records from the <c>CustomerSupportGroup</c> table.
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
		/// <returns>An array of <see cref="CustomerSupportGroupRow"/> objects.</returns>
		protected CustomerSupportGroupRow[] MapRecords(IDbCommand command)
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
		/// <returns>An array of <see cref="CustomerSupportGroupRow"/> objects.</returns>
		protected CustomerSupportGroupRow[] MapRecords(IDataReader reader)
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
		/// <returns>An array of <see cref="CustomerSupportGroupRow"/> objects.</returns>
		protected virtual CustomerSupportGroupRow[] MapRecords(IDataReader reader, 
										int startIndex, int length, ref int totalRecordCount)
		{
			if(0 > startIndex)
				throw new ArgumentOutOfRangeException("startIndex", startIndex, "StartIndex cannot be less than zero.");
			if(0 > length)
				throw new ArgumentOutOfRangeException("length", length, "Length cannot be less than zero.");

			int group_idColumnIndex = reader.GetOrdinal("group_id");
			int descriptionColumnIndex = reader.GetOrdinal("description");
			int roleColumnIndex = reader.GetOrdinal("role");
			int max_amountColumnIndex = reader.GetOrdinal("max_amount");
			int allow_status_changeColumnIndex = reader.GetOrdinal("allow_status_change");
			int vendor_idColumnIndex = reader.GetOrdinal("vendor_id");

			System.Collections.ArrayList recordList = new System.Collections.ArrayList();
			int ri = -startIndex;
			while(reader.Read())
			{
				ri++;
				if(ri > 0 && ri <= length)
				{
					CustomerSupportGroupRow record = new CustomerSupportGroupRow();
					recordList.Add(record);

					record.Group_id = Convert.ToInt32(reader.GetValue(group_idColumnIndex));
					record.Description = Convert.ToString(reader.GetValue(descriptionColumnIndex));
					record.Role = Convert.ToInt32(reader.GetValue(roleColumnIndex));
					record.Max_amount = Convert.ToDecimal(reader.GetValue(max_amountColumnIndex));
					record.Allow_status_change = Convert.ToByte(reader.GetValue(allow_status_changeColumnIndex));
					record.Vendor_id = Convert.ToInt32(reader.GetValue(vendor_idColumnIndex));

					if(ri == length && 0 != totalRecordCount)
						break;
				}
			}

			totalRecordCount = 0 == totalRecordCount ? ri + startIndex : -1;
			return (CustomerSupportGroupRow[])(recordList.ToArray(typeof(CustomerSupportGroupRow)));
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
		/// Converts <see cref="System.Data.DataRow"/> to <see cref="CustomerSupportGroupRow"/>.
		/// </summary>
		/// <param name="row">The <see cref="System.Data.DataRow"/> object to be mapped.</param>
		/// <returns>A reference to the <see cref="CustomerSupportGroupRow"/> object.</returns>
		protected virtual CustomerSupportGroupRow MapRow(DataRow row)
		{
			CustomerSupportGroupRow mappedObject = new CustomerSupportGroupRow();
			DataTable dataTable = row.Table;
			DataColumn dataColumn;
			// Column "Group_id"
			dataColumn = dataTable.Columns["Group_id"];
			if(!row.IsNull(dataColumn))
				mappedObject.Group_id = (int)row[dataColumn];
			// Column "Description"
			dataColumn = dataTable.Columns["Description"];
			if(!row.IsNull(dataColumn))
				mappedObject.Description = (string)row[dataColumn];
			// Column "Role"
			dataColumn = dataTable.Columns["Role"];
			if(!row.IsNull(dataColumn))
				mappedObject.Role = (int)row[dataColumn];
			// Column "Max_amount"
			dataColumn = dataTable.Columns["Max_amount"];
			if(!row.IsNull(dataColumn))
				mappedObject.Max_amount = (decimal)row[dataColumn];
			// Column "Allow_status_change"
			dataColumn = dataTable.Columns["Allow_status_change"];
			if(!row.IsNull(dataColumn))
				mappedObject.Allow_status_change = (byte)row[dataColumn];
			// Column "Vendor_id"
			dataColumn = dataTable.Columns["Vendor_id"];
			if(!row.IsNull(dataColumn))
				mappedObject.Vendor_id = (int)row[dataColumn];
			return mappedObject;
		}

		/// <summary>
		/// Creates a <see cref="System.Data.DataTable"/> object for 
		/// the <c>CustomerSupportGroup</c> table.
		/// </summary>
		/// <returns>A reference to the <see cref="System.Data.DataTable"/> object.</returns>
		protected virtual DataTable CreateDataTable()
		{
			DataTable dataTable = new DataTable();
			dataTable.TableName = "CustomerSupportGroup";
			DataColumn dataColumn;
			dataColumn = dataTable.Columns.Add("Group_id", typeof(int));
			dataColumn.Caption = "group_id";
			dataColumn.AllowDBNull = false;
			dataColumn = dataTable.Columns.Add("Description", typeof(string));
			dataColumn.Caption = "description";
			dataColumn.MaxLength = 128;
			dataColumn.AllowDBNull = false;
			dataColumn = dataTable.Columns.Add("Role", typeof(int));
			dataColumn.Caption = "role";
			dataColumn.AllowDBNull = false;
			dataColumn = dataTable.Columns.Add("Max_amount", typeof(decimal));
			dataColumn.Caption = "max_amount";
			dataColumn.AllowDBNull = false;
			dataColumn = dataTable.Columns.Add("Allow_status_change", typeof(byte));
			dataColumn.Caption = "allow_status_change";
			dataColumn.AllowDBNull = false;
			dataColumn = dataTable.Columns.Add("Vendor_id", typeof(int));
			dataColumn.Caption = "vendor_id";
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
				case "Group_id":
					parameter = _db.AddParameter(cmd, paramName, DbType.Int32, value);
					break;

				case "Description":
					parameter = _db.AddParameter(cmd, paramName, DbType.AnsiString, value);
					break;

				case "Role":
					parameter = _db.AddParameter(cmd, paramName, DbType.Int32, value);
					break;

				case "Max_amount":
					parameter = _db.AddParameter(cmd, paramName, DbType.Decimal, value);
					break;

				case "Allow_status_change":
					parameter = _db.AddParameter(cmd, paramName, DbType.Byte, value);
					break;

				case "Vendor_id":
					parameter = _db.AddParameter(cmd, paramName, DbType.Int32, value);
					break;

				default:
					throw new ArgumentException("Unknown parameter name (" + paramName + ").");
			}
			return parameter;
		}

	} // End of CustomerSupportGroupCollection_Base class
}  // End of namespace

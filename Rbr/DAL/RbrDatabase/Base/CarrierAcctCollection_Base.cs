// <fileinfo name="Base\CarrierAcctCollection_Base.cs">
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
	/// The base class for <see cref="CarrierAcctCollection"/>. Provides methods 
	/// for common database table operations. 
	/// </summary>
	/// <remarks>
	/// Do not change this source code. Update the <see cref="CarrierAcctCollection"/>
	/// class if you need to add or change some functionality.
	/// </remarks>
	public abstract class CarrierAcctCollection_Base
	{
		// Constants
		public const string Carrier_acct_idColumnName = "carrier_acct_id";
		public const string NameColumnName = "name";
		public const string StatusColumnName = "status";
		public const string Rating_typeColumnName = "rating_type";
		public const string Prefix_outColumnName = "prefix_out";
		public const string Strip_1plusColumnName = "strip_1plus";
		public const string Intl_dial_codeColumnName = "intl_dial_code";
		public const string Partner_idColumnName = "partner_id";
		public const string Calling_plan_idColumnName = "calling_plan_id";
		public const string Max_call_lengthColumnName = "max_call_length";

		// Instance fields
		private Rbr_Db _db;

		/// <summary>
		/// Initializes a new instance of the <see cref="CarrierAcctCollection_Base"/> 
		/// class with the specified <see cref="Rbr_Db"/>.
		/// </summary>
		/// <param name="db">The <see cref="Rbr_Db"/> object.</param>
		public CarrierAcctCollection_Base(Rbr_Db db)
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
		/// Gets an array of all records from the <c>CarrierAcct</c> table.
		/// </summary>
		/// <returns>An array of <see cref="CarrierAcctRow"/> objects.</returns>
		public virtual CarrierAcctRow[] GetAll()
		{
			return MapRecords(CreateGetAllCommand());
		}

		/// <summary>
		/// Gets a <see cref="System.Data.DataTable"/> object that 
		/// includes all records from the <c>CarrierAcct</c> table.
		/// </summary>
		/// <returns>A reference to the <see cref="System.Data.DataTable"/> object.</returns>
		public virtual DataTable GetAllAsDataTable()
		{
			return MapRecordsToDataTable(CreateGetAllCommand());
		}

		/// <summary>
		/// Creates and returns an <see cref="System.Data.IDbCommand"/> object that is used
		/// to retrieve all records from the <c>CarrierAcct</c> table.
		/// </summary>
		/// <returns>A reference to the <see cref="System.Data.IDbCommand"/> object.</returns>
		protected virtual IDbCommand CreateGetAllCommand()
		{
			return CreateGetCommand(null, null);
		}

		/// <summary>
		/// Gets the first <see cref="CarrierAcctRow"/> objects that 
		/// match the search condition.
		/// </summary>
		/// <param name="whereSql">The SQL search condition. For example: 
		/// <c>"FirstName='Smith' AND Zip=75038"</c>.</param>
		/// <returns>An instance of <see cref="CarrierAcctRow"/> or null reference 
		/// (Nothing in Visual Basic) if the object was not found.</returns>
		public CarrierAcctRow GetRow(string whereSql)
		{
			int totalRecordCount = -1;
			CarrierAcctRow[] rows = GetAsArray(whereSql, null, 0, 1, ref totalRecordCount);
			return 0 == rows.Length ? null : rows[0];
		}

		/// <summary>
		/// Gets an array of <see cref="CarrierAcctRow"/> objects that 
		/// match the search condition, in the the specified sort order.
		/// </summary>
		/// <param name="whereSql">The SQL search condition. For example: 
		/// <c>"FirstName='Smith' AND Zip=75038"</c>.</param>
		/// <param name="orderBySql">The column name(s) followed by "ASC" (ascending) or "DESC" (descending).
		/// Columns are sorted in ascending order by default. For example: <c>"LastName ASC, FirstName ASC"</c>.</param>
		/// <returns>An array of <see cref="CarrierAcctRow"/> objects.</returns>
		public CarrierAcctRow[] GetAsArray(string whereSql, string orderBySql)
		{
			int totalRecordCount = -1;
			return GetAsArray(whereSql, orderBySql, 0, int.MaxValue, ref totalRecordCount);
		}

		/// <summary>
		/// Gets an array of <see cref="CarrierAcctRow"/> objects that 
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
		/// <returns>An array of <see cref="CarrierAcctRow"/> objects.</returns>
		public virtual CarrierAcctRow[] GetAsArray(string whereSql, string orderBySql,
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
			string sql = "SELECT * FROM [dbo].[CarrierAcct]";
			if(null != whereSql && 0 < whereSql.Length)
				sql += " WHERE " + whereSql;
			if(null != orderBySql && 0 < orderBySql.Length)
				sql += " ORDER BY " + orderBySql;
			return _db.CreateCommand(sql);
		}

		/// <summary>
		/// Gets <see cref="CarrierAcctRow"/> by the primary key.
		/// </summary>
		/// <param name="carrier_acct_id">The <c>carrier_acct_id</c> column value.</param>
		/// <returns>An instance of <see cref="CarrierAcctRow"/> or null reference 
		/// (Nothing in Visual Basic) if the object was not found.</returns>
		public virtual CarrierAcctRow GetByPrimaryKey(short carrier_acct_id)
		{
			string whereSql = "[carrier_acct_id]=" + _db.CreateSqlParameterName("Carrier_acct_id");
			IDbCommand cmd = CreateGetCommand(whereSql, null);
			AddParameter(cmd, "Carrier_acct_id", carrier_acct_id);
			CarrierAcctRow[] tempArray = MapRecords(cmd);
			return 0 == tempArray.Length ? null : tempArray[0];
		}

		/// <summary>
		/// Gets an array of <see cref="CarrierAcctRow"/> objects 
		/// by the <c>R_332</c> foreign key.
		/// </summary>
		/// <param name="calling_plan_id">The <c>calling_plan_id</c> column value.</param>
		/// <returns>An array of <see cref="CarrierAcctRow"/> objects.</returns>
		public virtual CarrierAcctRow[] GetByCalling_plan_id(int calling_plan_id)
		{
			return MapRecords(CreateGetByCalling_plan_idCommand(calling_plan_id));
		}

		/// <summary>
		/// Gets a <see cref="System.Data.DataTable"/> object 
		/// by the <c>R_332</c> foreign key.
		/// </summary>
		/// <param name="calling_plan_id">The <c>calling_plan_id</c> column value.</param>
		/// <returns>A reference to the <see cref="System.Data.DataTable"/> object.</returns>
		public virtual DataTable GetByCalling_plan_idAsDataTable(int calling_plan_id)
		{
			return MapRecordsToDataTable(CreateGetByCalling_plan_idCommand(calling_plan_id));
		}

		/// <summary>
		/// Creates an <see cref="System.Data.IDbCommand"/> object that can be used to 
		/// return records by the <c>R_332</c> foreign key.
		/// </summary>
		/// <param name="calling_plan_id">The <c>calling_plan_id</c> column value.</param>
		/// <returns>A reference to the <see cref="System.Data.IDbCommand"/> object.</returns>
		protected virtual IDbCommand CreateGetByCalling_plan_idCommand(int calling_plan_id)
		{
			string whereSql = "";
			whereSql += "[calling_plan_id]=" + _db.CreateSqlParameterName("Calling_plan_id");

			IDbCommand cmd = CreateGetCommand(whereSql, null);
			AddParameter(cmd, "Calling_plan_id", calling_plan_id);
			return cmd;
		}

		/// <summary>
		/// Gets an array of <see cref="CarrierAcctRow"/> objects 
		/// by the <c>R_5</c> foreign key.
		/// </summary>
		/// <param name="partner_id">The <c>partner_id</c> column value.</param>
		/// <returns>An array of <see cref="CarrierAcctRow"/> objects.</returns>
		public virtual CarrierAcctRow[] GetByPartner_id(int partner_id)
		{
			return MapRecords(CreateGetByPartner_idCommand(partner_id));
		}

		/// <summary>
		/// Gets a <see cref="System.Data.DataTable"/> object 
		/// by the <c>R_5</c> foreign key.
		/// </summary>
		/// <param name="partner_id">The <c>partner_id</c> column value.</param>
		/// <returns>A reference to the <see cref="System.Data.DataTable"/> object.</returns>
		public virtual DataTable GetByPartner_idAsDataTable(int partner_id)
		{
			return MapRecordsToDataTable(CreateGetByPartner_idCommand(partner_id));
		}

		/// <summary>
		/// Creates an <see cref="System.Data.IDbCommand"/> object that can be used to 
		/// return records by the <c>R_5</c> foreign key.
		/// </summary>
		/// <param name="partner_id">The <c>partner_id</c> column value.</param>
		/// <returns>A reference to the <see cref="System.Data.IDbCommand"/> object.</returns>
		protected virtual IDbCommand CreateGetByPartner_idCommand(int partner_id)
		{
			string whereSql = "";
			whereSql += "[partner_id]=" + _db.CreateSqlParameterName("Partner_id");

			IDbCommand cmd = CreateGetCommand(whereSql, null);
			AddParameter(cmd, "Partner_id", partner_id);
			return cmd;
		}

		/// <summary>
		/// Adds a new record into the <c>CarrierAcct</c> table.
		/// </summary>
		/// <param name="value">The <see cref="CarrierAcctRow"/> object to be inserted.</param>
		public virtual void Insert(CarrierAcctRow value)
		{
			string sqlStr = "INSERT INTO [dbo].[CarrierAcct] (" +
				"[carrier_acct_id], " +
				"[name], " +
				"[status], " +
				"[rating_type], " +
				"[prefix_out], " +
				"[strip_1plus], " +
				"[intl_dial_code], " +
				"[partner_id], " +
				"[calling_plan_id], " +
				"[max_call_length]" +
				") VALUES (" +
				_db.CreateSqlParameterName("Carrier_acct_id") + ", " +
				_db.CreateSqlParameterName("Name") + ", " +
				_db.CreateSqlParameterName("Status") + ", " +
				_db.CreateSqlParameterName("Rating_type") + ", " +
				_db.CreateSqlParameterName("Prefix_out") + ", " +
				_db.CreateSqlParameterName("Strip_1plus") + ", " +
				_db.CreateSqlParameterName("Intl_dial_code") + ", " +
				_db.CreateSqlParameterName("Partner_id") + ", " +
				_db.CreateSqlParameterName("Calling_plan_id") + ", " +
				_db.CreateSqlParameterName("Max_call_length") + ")";
			IDbCommand cmd = _db.CreateCommand(sqlStr);
			AddParameter(cmd, "Carrier_acct_id", value.Carrier_acct_id);
			AddParameter(cmd, "Name", value.Name);
			AddParameter(cmd, "Status", value.Status);
			AddParameter(cmd, "Rating_type", value.Rating_type);
			AddParameter(cmd, "Prefix_out", value.Prefix_out);
			AddParameter(cmd, "Strip_1plus", value.Strip_1plus);
			AddParameter(cmd, "Intl_dial_code", value.Intl_dial_code);
			AddParameter(cmd, "Partner_id", value.Partner_id);
			AddParameter(cmd, "Calling_plan_id", value.Calling_plan_id);
			AddParameter(cmd, "Max_call_length", value.Max_call_length);
			cmd.ExecuteNonQuery();
		}

		/// <summary>
		/// Updates a record in the <c>CarrierAcct</c> table.
		/// </summary>
		/// <param name="value">The <see cref="CarrierAcctRow"/>
		/// object used to update the table record.</param>
		/// <returns>true if the record was updated; otherwise, false.</returns>
		public virtual bool Update(CarrierAcctRow value)
		{
			string sqlStr = "UPDATE [dbo].[CarrierAcct] SET " +
				"[name]=" + _db.CreateSqlParameterName("Name") + ", " +
				"[status]=" + _db.CreateSqlParameterName("Status") + ", " +
				"[rating_type]=" + _db.CreateSqlParameterName("Rating_type") + ", " +
				"[prefix_out]=" + _db.CreateSqlParameterName("Prefix_out") + ", " +
				"[strip_1plus]=" + _db.CreateSqlParameterName("Strip_1plus") + ", " +
				"[intl_dial_code]=" + _db.CreateSqlParameterName("Intl_dial_code") + ", " +
				"[partner_id]=" + _db.CreateSqlParameterName("Partner_id") + ", " +
				"[calling_plan_id]=" + _db.CreateSqlParameterName("Calling_plan_id") + ", " +
				"[max_call_length]=" + _db.CreateSqlParameterName("Max_call_length") +
				" WHERE " +
				"[carrier_acct_id]=" + _db.CreateSqlParameterName("Carrier_acct_id");
			IDbCommand cmd = _db.CreateCommand(sqlStr);
			AddParameter(cmd, "Name", value.Name);
			AddParameter(cmd, "Status", value.Status);
			AddParameter(cmd, "Rating_type", value.Rating_type);
			AddParameter(cmd, "Prefix_out", value.Prefix_out);
			AddParameter(cmd, "Strip_1plus", value.Strip_1plus);
			AddParameter(cmd, "Intl_dial_code", value.Intl_dial_code);
			AddParameter(cmd, "Partner_id", value.Partner_id);
			AddParameter(cmd, "Calling_plan_id", value.Calling_plan_id);
			AddParameter(cmd, "Max_call_length", value.Max_call_length);
			AddParameter(cmd, "Carrier_acct_id", value.Carrier_acct_id);
			return 0 != cmd.ExecuteNonQuery();
		}

		/// <summary>
		/// Updates the <c>CarrierAcct</c> table and calls the <c>AcceptChanges</c> method
		/// on the changed DataRow objects.
		/// </summary>
		/// <param name="table">The <see cref="System.Data.DataTable"/> used to update the data source.</param>
		public void Update(DataTable table)
		{
			Update(table, true);
		}

		/// <summary>
		/// Updates the <c>CarrierAcct</c> table. Pass <c>false</c> as the <c>acceptChanges</c> 
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
							DeleteByPrimaryKey((short)row["Carrier_acct_id"]);
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
		/// Deletes the specified object from the <c>CarrierAcct</c> table.
		/// </summary>
		/// <param name="value">The <see cref="CarrierAcctRow"/> object to delete.</param>
		/// <returns>true if the record was deleted; otherwise, false.</returns>
		public bool Delete(CarrierAcctRow value)
		{
			return DeleteByPrimaryKey(value.Carrier_acct_id);
		}

		/// <summary>
		/// Deletes a record from the <c>CarrierAcct</c> table using
		/// the specified primary key.
		/// </summary>
		/// <param name="carrier_acct_id">The <c>carrier_acct_id</c> column value.</param>
		/// <returns>true if the record was deleted; otherwise, false.</returns>
		public virtual bool DeleteByPrimaryKey(short carrier_acct_id)
		{
			string whereSql = "[carrier_acct_id]=" + _db.CreateSqlParameterName("Carrier_acct_id");
			IDbCommand cmd = CreateDeleteCommand(whereSql);
			AddParameter(cmd, "Carrier_acct_id", carrier_acct_id);
			return 0 < cmd.ExecuteNonQuery();
		}

		/// <summary>
		/// Deletes records from the <c>CarrierAcct</c> table using the 
		/// <c>R_332</c> foreign key.
		/// </summary>
		/// <param name="calling_plan_id">The <c>calling_plan_id</c> column value.</param>
		/// <returns>The number of records deleted from the table.</returns>
		public int DeleteByCalling_plan_id(int calling_plan_id)
		{
			return CreateDeleteByCalling_plan_idCommand(calling_plan_id).ExecuteNonQuery();
		}

		/// <summary>
		/// Creates an <see cref="System.Data.IDbCommand"/> object that can be used to
		/// delete records using the <c>R_332</c> foreign key.
		/// </summary>
		/// <param name="calling_plan_id">The <c>calling_plan_id</c> column value.</param>
		/// <returns>A reference to the <see cref="System.Data.IDbCommand"/> object.</returns>
		protected virtual IDbCommand CreateDeleteByCalling_plan_idCommand(int calling_plan_id)
		{
			string whereSql = "";
			whereSql += "[calling_plan_id]=" + _db.CreateSqlParameterName("Calling_plan_id");

			IDbCommand cmd = CreateDeleteCommand(whereSql);
			AddParameter(cmd, "Calling_plan_id", calling_plan_id);
			return cmd;
		}

		/// <summary>
		/// Deletes records from the <c>CarrierAcct</c> table using the 
		/// <c>R_5</c> foreign key.
		/// </summary>
		/// <param name="partner_id">The <c>partner_id</c> column value.</param>
		/// <returns>The number of records deleted from the table.</returns>
		public int DeleteByPartner_id(int partner_id)
		{
			return CreateDeleteByPartner_idCommand(partner_id).ExecuteNonQuery();
		}

		/// <summary>
		/// Creates an <see cref="System.Data.IDbCommand"/> object that can be used to
		/// delete records using the <c>R_5</c> foreign key.
		/// </summary>
		/// <param name="partner_id">The <c>partner_id</c> column value.</param>
		/// <returns>A reference to the <see cref="System.Data.IDbCommand"/> object.</returns>
		protected virtual IDbCommand CreateDeleteByPartner_idCommand(int partner_id)
		{
			string whereSql = "";
			whereSql += "[partner_id]=" + _db.CreateSqlParameterName("Partner_id");

			IDbCommand cmd = CreateDeleteCommand(whereSql);
			AddParameter(cmd, "Partner_id", partner_id);
			return cmd;
		}

		/// <summary>
		/// Deletes <c>CarrierAcct</c> records that match the specified criteria.
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
		/// to delete <c>CarrierAcct</c> records that match the specified criteria.
		/// </summary>
		/// <param name="whereSql">The SQL search condition. 
		/// For example: <c>"FirstName='Smith' AND Zip=75038"</c>.</param>
		/// <returns>A reference to the <see cref="System.Data.IDbCommand"/> object.</returns>
		protected virtual IDbCommand CreateDeleteCommand(string whereSql)
		{
			string sql = "DELETE FROM [dbo].[CarrierAcct]";
			if(null != whereSql && 0 < whereSql.Length)
				sql += " WHERE " + whereSql;
			return _db.CreateCommand(sql);
		}

		/// <summary>
		/// Deletes all records from the <c>CarrierAcct</c> table.
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
		/// <returns>An array of <see cref="CarrierAcctRow"/> objects.</returns>
		protected CarrierAcctRow[] MapRecords(IDbCommand command)
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
		/// <returns>An array of <see cref="CarrierAcctRow"/> objects.</returns>
		protected CarrierAcctRow[] MapRecords(IDataReader reader)
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
		/// <returns>An array of <see cref="CarrierAcctRow"/> objects.</returns>
		protected virtual CarrierAcctRow[] MapRecords(IDataReader reader, 
										int startIndex, int length, ref int totalRecordCount)
		{
			if(0 > startIndex)
				throw new ArgumentOutOfRangeException("startIndex", startIndex, "StartIndex cannot be less than zero.");
			if(0 > length)
				throw new ArgumentOutOfRangeException("length", length, "Length cannot be less than zero.");

			int carrier_acct_idColumnIndex = reader.GetOrdinal("carrier_acct_id");
			int nameColumnIndex = reader.GetOrdinal("name");
			int statusColumnIndex = reader.GetOrdinal("status");
			int rating_typeColumnIndex = reader.GetOrdinal("rating_type");
			int prefix_outColumnIndex = reader.GetOrdinal("prefix_out");
			int strip_1plusColumnIndex = reader.GetOrdinal("strip_1plus");
			int intl_dial_codeColumnIndex = reader.GetOrdinal("intl_dial_code");
			int partner_idColumnIndex = reader.GetOrdinal("partner_id");
			int calling_plan_idColumnIndex = reader.GetOrdinal("calling_plan_id");
			int max_call_lengthColumnIndex = reader.GetOrdinal("max_call_length");

			System.Collections.ArrayList recordList = new System.Collections.ArrayList();
			int ri = -startIndex;
			while(reader.Read())
			{
				ri++;
				if(ri > 0 && ri <= length)
				{
					CarrierAcctRow record = new CarrierAcctRow();
					recordList.Add(record);

					record.Carrier_acct_id = Convert.ToInt16(reader.GetValue(carrier_acct_idColumnIndex));
					record.Name = Convert.ToString(reader.GetValue(nameColumnIndex));
					record.Status = Convert.ToByte(reader.GetValue(statusColumnIndex));
					record.Rating_type = Convert.ToByte(reader.GetValue(rating_typeColumnIndex));
					record.Prefix_out = Convert.ToString(reader.GetValue(prefix_outColumnIndex));
					record.Strip_1plus = Convert.ToInt16(reader.GetValue(strip_1plusColumnIndex));
					record.Intl_dial_code = Convert.ToString(reader.GetValue(intl_dial_codeColumnIndex));
					record.Partner_id = Convert.ToInt32(reader.GetValue(partner_idColumnIndex));
					record.Calling_plan_id = Convert.ToInt32(reader.GetValue(calling_plan_idColumnIndex));
					record.Max_call_length = Convert.ToInt16(reader.GetValue(max_call_lengthColumnIndex));

					if(ri == length && 0 != totalRecordCount)
						break;
				}
			}

			totalRecordCount = 0 == totalRecordCount ? ri + startIndex : -1;
			return (CarrierAcctRow[])(recordList.ToArray(typeof(CarrierAcctRow)));
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
		/// Converts <see cref="System.Data.DataRow"/> to <see cref="CarrierAcctRow"/>.
		/// </summary>
		/// <param name="row">The <see cref="System.Data.DataRow"/> object to be mapped.</param>
		/// <returns>A reference to the <see cref="CarrierAcctRow"/> object.</returns>
		protected virtual CarrierAcctRow MapRow(DataRow row)
		{
			CarrierAcctRow mappedObject = new CarrierAcctRow();
			DataTable dataTable = row.Table;
			DataColumn dataColumn;
			// Column "Carrier_acct_id"
			dataColumn = dataTable.Columns["Carrier_acct_id"];
			if(!row.IsNull(dataColumn))
				mappedObject.Carrier_acct_id = (short)row[dataColumn];
			// Column "Name"
			dataColumn = dataTable.Columns["Name"];
			if(!row.IsNull(dataColumn))
				mappedObject.Name = (string)row[dataColumn];
			// Column "Status"
			dataColumn = dataTable.Columns["Status"];
			if(!row.IsNull(dataColumn))
				mappedObject.Status = (byte)row[dataColumn];
			// Column "Rating_type"
			dataColumn = dataTable.Columns["Rating_type"];
			if(!row.IsNull(dataColumn))
				mappedObject.Rating_type = (byte)row[dataColumn];
			// Column "Prefix_out"
			dataColumn = dataTable.Columns["Prefix_out"];
			if(!row.IsNull(dataColumn))
				mappedObject.Prefix_out = (string)row[dataColumn];
			// Column "Strip_1plus"
			dataColumn = dataTable.Columns["Strip_1plus"];
			if(!row.IsNull(dataColumn))
				mappedObject.Strip_1plus = (short)row[dataColumn];
			// Column "Intl_dial_code"
			dataColumn = dataTable.Columns["Intl_dial_code"];
			if(!row.IsNull(dataColumn))
				mappedObject.Intl_dial_code = (string)row[dataColumn];
			// Column "Partner_id"
			dataColumn = dataTable.Columns["Partner_id"];
			if(!row.IsNull(dataColumn))
				mappedObject.Partner_id = (int)row[dataColumn];
			// Column "Calling_plan_id"
			dataColumn = dataTable.Columns["Calling_plan_id"];
			if(!row.IsNull(dataColumn))
				mappedObject.Calling_plan_id = (int)row[dataColumn];
			// Column "Max_call_length"
			dataColumn = dataTable.Columns["Max_call_length"];
			if(!row.IsNull(dataColumn))
				mappedObject.Max_call_length = (short)row[dataColumn];
			return mappedObject;
		}

		/// <summary>
		/// Creates a <see cref="System.Data.DataTable"/> object for 
		/// the <c>CarrierAcct</c> table.
		/// </summary>
		/// <returns>A reference to the <see cref="System.Data.DataTable"/> object.</returns>
		protected virtual DataTable CreateDataTable()
		{
			DataTable dataTable = new DataTable();
			dataTable.TableName = "CarrierAcct";
			DataColumn dataColumn;
			dataColumn = dataTable.Columns.Add("Carrier_acct_id", typeof(short));
			dataColumn.Caption = "carrier_acct_id";
			dataColumn.AllowDBNull = false;
			dataColumn = dataTable.Columns.Add("Name", typeof(string));
			dataColumn.Caption = "name";
			dataColumn.MaxLength = 50;
			dataColumn.AllowDBNull = false;
			dataColumn = dataTable.Columns.Add("Status", typeof(byte));
			dataColumn.Caption = "status";
			dataColumn.AllowDBNull = false;
			dataColumn = dataTable.Columns.Add("Rating_type", typeof(byte));
			dataColumn.Caption = "rating_type";
			dataColumn.AllowDBNull = false;
			dataColumn = dataTable.Columns.Add("Prefix_out", typeof(string));
			dataColumn.Caption = "prefix_out";
			dataColumn.MaxLength = 10;
			dataColumn.AllowDBNull = false;
			dataColumn = dataTable.Columns.Add("Strip_1plus", typeof(short));
			dataColumn.Caption = "strip_1plus";
			dataColumn.AllowDBNull = false;
			dataColumn = dataTable.Columns.Add("Intl_dial_code", typeof(string));
			dataColumn.Caption = "intl_dial_code";
			dataColumn.MaxLength = 5;
			dataColumn.AllowDBNull = false;
			dataColumn = dataTable.Columns.Add("Partner_id", typeof(int));
			dataColumn.Caption = "partner_id";
			dataColumn.AllowDBNull = false;
			dataColumn = dataTable.Columns.Add("Calling_plan_id", typeof(int));
			dataColumn.Caption = "calling_plan_id";
			dataColumn.AllowDBNull = false;
			dataColumn = dataTable.Columns.Add("Max_call_length", typeof(short));
			dataColumn.Caption = "max_call_length";
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
				case "Carrier_acct_id":
					parameter = _db.AddParameter(cmd, paramName, DbType.Int16, value);
					break;

				case "Name":
					parameter = _db.AddParameter(cmd, paramName, DbType.AnsiString, value);
					break;

				case "Status":
					parameter = _db.AddParameter(cmd, paramName, DbType.Byte, value);
					break;

				case "Rating_type":
					parameter = _db.AddParameter(cmd, paramName, DbType.Byte, value);
					break;

				case "Prefix_out":
					parameter = _db.AddParameter(cmd, paramName, DbType.AnsiString, value);
					break;

				case "Strip_1plus":
					parameter = _db.AddParameter(cmd, paramName, DbType.Int16, value);
					break;

				case "Intl_dial_code":
					parameter = _db.AddParameter(cmd, paramName, DbType.AnsiString, value);
					break;

				case "Partner_id":
					parameter = _db.AddParameter(cmd, paramName, DbType.Int32, value);
					break;

				case "Calling_plan_id":
					parameter = _db.AddParameter(cmd, paramName, DbType.Int32, value);
					break;

				case "Max_call_length":
					parameter = _db.AddParameter(cmd, paramName, DbType.Int16, value);
					break;

				default:
					throw new ArgumentException("Unknown parameter name (" + paramName + ").");
			}
			return parameter;
		}

	} // End of CarrierAcctCollection_Base class
}  // End of namespace

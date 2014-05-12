// <fileinfo name="Base\ResellAcctCollection_Base.cs">
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
	/// The base class for <see cref="ResellAcctCollection"/>. Provides methods 
	/// for common database table operations. 
	/// </summary>
	/// <remarks>
	/// Do not change this source code. Update the <see cref="ResellAcctCollection"/>
	/// class if you need to add or change some functionality.
	/// </remarks>
	public abstract class ResellAcctCollection_Base
	{
		// Constants
		public const string Resell_acct_idColumnName = "resell_acct_id";
		public const string Partner_idColumnName = "partner_id";
		public const string Customer_acct_idColumnName = "customer_acct_id";
		public const string Person_idColumnName = "person_id";
		public const string Per_routeColumnName = "per_route";
		public const string Commision_typeColumnName = "commision_type";
		public const string Markup_dollarColumnName = "markup_dollar";
		public const string Markup_percentColumnName = "markup_percent";
		public const string Fee_per_callColumnName = "fee_per_call";
		public const string Fee_per_minuteColumnName = "fee_per_minute";

		// Instance fields
		private Rbr_Db _db;

		/// <summary>
		/// Initializes a new instance of the <see cref="ResellAcctCollection_Base"/> 
		/// class with the specified <see cref="Rbr_Db"/>.
		/// </summary>
		/// <param name="db">The <see cref="Rbr_Db"/> object.</param>
		public ResellAcctCollection_Base(Rbr_Db db)
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
		/// Gets an array of all records from the <c>ResellAcct</c> table.
		/// </summary>
		/// <returns>An array of <see cref="ResellAcctRow"/> objects.</returns>
		public virtual ResellAcctRow[] GetAll()
		{
			return MapRecords(CreateGetAllCommand());
		}

		/// <summary>
		/// Gets a <see cref="System.Data.DataTable"/> object that 
		/// includes all records from the <c>ResellAcct</c> table.
		/// </summary>
		/// <returns>A reference to the <see cref="System.Data.DataTable"/> object.</returns>
		public virtual DataTable GetAllAsDataTable()
		{
			return MapRecordsToDataTable(CreateGetAllCommand());
		}

		/// <summary>
		/// Creates and returns an <see cref="System.Data.IDbCommand"/> object that is used
		/// to retrieve all records from the <c>ResellAcct</c> table.
		/// </summary>
		/// <returns>A reference to the <see cref="System.Data.IDbCommand"/> object.</returns>
		protected virtual IDbCommand CreateGetAllCommand()
		{
			return CreateGetCommand(null, null);
		}

		/// <summary>
		/// Gets the first <see cref="ResellAcctRow"/> objects that 
		/// match the search condition.
		/// </summary>
		/// <param name="whereSql">The SQL search condition. For example: 
		/// <c>"FirstName='Smith' AND Zip=75038"</c>.</param>
		/// <returns>An instance of <see cref="ResellAcctRow"/> or null reference 
		/// (Nothing in Visual Basic) if the object was not found.</returns>
		public ResellAcctRow GetRow(string whereSql)
		{
			int totalRecordCount = -1;
			ResellAcctRow[] rows = GetAsArray(whereSql, null, 0, 1, ref totalRecordCount);
			return 0 == rows.Length ? null : rows[0];
		}

		/// <summary>
		/// Gets an array of <see cref="ResellAcctRow"/> objects that 
		/// match the search condition, in the the specified sort order.
		/// </summary>
		/// <param name="whereSql">The SQL search condition. For example: 
		/// <c>"FirstName='Smith' AND Zip=75038"</c>.</param>
		/// <param name="orderBySql">The column name(s) followed by "ASC" (ascending) or "DESC" (descending).
		/// Columns are sorted in ascending order by default. For example: <c>"LastName ASC, FirstName ASC"</c>.</param>
		/// <returns>An array of <see cref="ResellAcctRow"/> objects.</returns>
		public ResellAcctRow[] GetAsArray(string whereSql, string orderBySql)
		{
			int totalRecordCount = -1;
			return GetAsArray(whereSql, orderBySql, 0, int.MaxValue, ref totalRecordCount);
		}

		/// <summary>
		/// Gets an array of <see cref="ResellAcctRow"/> objects that 
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
		/// <returns>An array of <see cref="ResellAcctRow"/> objects.</returns>
		public virtual ResellAcctRow[] GetAsArray(string whereSql, string orderBySql,
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
			string sql = "SELECT * FROM [dbo].[ResellAcct]";
			if(null != whereSql && 0 < whereSql.Length)
				sql += " WHERE " + whereSql;
			if(null != orderBySql && 0 < orderBySql.Length)
				sql += " ORDER BY " + orderBySql;
			return _db.CreateCommand(sql);
		}

		/// <summary>
		/// Gets <see cref="ResellAcctRow"/> by the primary key.
		/// </summary>
		/// <param name="resell_acct_id">The <c>resell_acct_id</c> column value.</param>
		/// <returns>An instance of <see cref="ResellAcctRow"/> or null reference 
		/// (Nothing in Visual Basic) if the object was not found.</returns>
		public virtual ResellAcctRow GetByPrimaryKey(short resell_acct_id)
		{
			string whereSql = "[resell_acct_id]=" + _db.CreateSqlParameterName("Resell_acct_id");
			IDbCommand cmd = CreateGetCommand(whereSql, null);
			AddParameter(cmd, "Resell_acct_id", resell_acct_id);
			ResellAcctRow[] tempArray = MapRecords(cmd);
			return 0 == tempArray.Length ? null : tempArray[0];
		}

		/// <summary>
		/// Gets an array of <see cref="ResellAcctRow"/> objects 
		/// by the <c>R_288</c> foreign key.
		/// </summary>
		/// <param name="customer_acct_id">The <c>customer_acct_id</c> column value.</param>
		/// <returns>An array of <see cref="ResellAcctRow"/> objects.</returns>
		public virtual ResellAcctRow[] GetByCustomer_acct_id(short customer_acct_id)
		{
			return MapRecords(CreateGetByCustomer_acct_idCommand(customer_acct_id));
		}

		/// <summary>
		/// Gets a <see cref="System.Data.DataTable"/> object 
		/// by the <c>R_288</c> foreign key.
		/// </summary>
		/// <param name="customer_acct_id">The <c>customer_acct_id</c> column value.</param>
		/// <returns>A reference to the <see cref="System.Data.DataTable"/> object.</returns>
		public virtual DataTable GetByCustomer_acct_idAsDataTable(short customer_acct_id)
		{
			return MapRecordsToDataTable(CreateGetByCustomer_acct_idCommand(customer_acct_id));
		}

		/// <summary>
		/// Creates an <see cref="System.Data.IDbCommand"/> object that can be used to 
		/// return records by the <c>R_288</c> foreign key.
		/// </summary>
		/// <param name="customer_acct_id">The <c>customer_acct_id</c> column value.</param>
		/// <returns>A reference to the <see cref="System.Data.IDbCommand"/> object.</returns>
		protected virtual IDbCommand CreateGetByCustomer_acct_idCommand(short customer_acct_id)
		{
			string whereSql = "";
			whereSql += "[customer_acct_id]=" + _db.CreateSqlParameterName("Customer_acct_id");

			IDbCommand cmd = CreateGetCommand(whereSql, null);
			AddParameter(cmd, "Customer_acct_id", customer_acct_id);
			return cmd;
		}

		/// <summary>
		/// Gets an array of <see cref="ResellAcctRow"/> objects 
		/// by the <c>R_291</c> foreign key.
		/// </summary>
		/// <param name="person_id">The <c>person_id</c> column value.</param>
		/// <returns>An array of <see cref="ResellAcctRow"/> objects.</returns>
		public virtual ResellAcctRow[] GetByPerson_id(int person_id)
		{
			return MapRecords(CreateGetByPerson_idCommand(person_id));
		}

		/// <summary>
		/// Gets a <see cref="System.Data.DataTable"/> object 
		/// by the <c>R_291</c> foreign key.
		/// </summary>
		/// <param name="person_id">The <c>person_id</c> column value.</param>
		/// <returns>A reference to the <see cref="System.Data.DataTable"/> object.</returns>
		public virtual DataTable GetByPerson_idAsDataTable(int person_id)
		{
			return MapRecordsToDataTable(CreateGetByPerson_idCommand(person_id));
		}

		/// <summary>
		/// Creates an <see cref="System.Data.IDbCommand"/> object that can be used to 
		/// return records by the <c>R_291</c> foreign key.
		/// </summary>
		/// <param name="person_id">The <c>person_id</c> column value.</param>
		/// <returns>A reference to the <see cref="System.Data.IDbCommand"/> object.</returns>
		protected virtual IDbCommand CreateGetByPerson_idCommand(int person_id)
		{
			string whereSql = "";
			whereSql += "[person_id]=" + _db.CreateSqlParameterName("Person_id");

			IDbCommand cmd = CreateGetCommand(whereSql, null);
			AddParameter(cmd, "Person_id", person_id);
			return cmd;
		}

		/// <summary>
		/// Gets an array of <see cref="ResellAcctRow"/> objects 
		/// by the <c>R_370</c> foreign key.
		/// </summary>
		/// <param name="partner_id">The <c>partner_id</c> column value.</param>
		/// <returns>An array of <see cref="ResellAcctRow"/> objects.</returns>
		public virtual ResellAcctRow[] GetByPartner_id(int partner_id)
		{
			return MapRecords(CreateGetByPartner_idCommand(partner_id));
		}

		/// <summary>
		/// Gets a <see cref="System.Data.DataTable"/> object 
		/// by the <c>R_370</c> foreign key.
		/// </summary>
		/// <param name="partner_id">The <c>partner_id</c> column value.</param>
		/// <returns>A reference to the <see cref="System.Data.DataTable"/> object.</returns>
		public virtual DataTable GetByPartner_idAsDataTable(int partner_id)
		{
			return MapRecordsToDataTable(CreateGetByPartner_idCommand(partner_id));
		}

		/// <summary>
		/// Creates an <see cref="System.Data.IDbCommand"/> object that can be used to 
		/// return records by the <c>R_370</c> foreign key.
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
		/// Adds a new record into the <c>ResellAcct</c> table.
		/// </summary>
		/// <param name="value">The <see cref="ResellAcctRow"/> object to be inserted.</param>
		public virtual void Insert(ResellAcctRow value)
		{
			string sqlStr = "INSERT INTO [dbo].[ResellAcct] (" +
				"[resell_acct_id], " +
				"[partner_id], " +
				"[customer_acct_id], " +
				"[person_id], " +
				"[per_route], " +
				"[commision_type], " +
				"[markup_dollar], " +
				"[markup_percent], " +
				"[fee_per_call], " +
				"[fee_per_minute]" +
				") VALUES (" +
				_db.CreateSqlParameterName("Resell_acct_id") + ", " +
				_db.CreateSqlParameterName("Partner_id") + ", " +
				_db.CreateSqlParameterName("Customer_acct_id") + ", " +
				_db.CreateSqlParameterName("Person_id") + ", " +
				_db.CreateSqlParameterName("Per_route") + ", " +
				_db.CreateSqlParameterName("Commision_type") + ", " +
				_db.CreateSqlParameterName("Markup_dollar") + ", " +
				_db.CreateSqlParameterName("Markup_percent") + ", " +
				_db.CreateSqlParameterName("Fee_per_call") + ", " +
				_db.CreateSqlParameterName("Fee_per_minute") + ")";
			IDbCommand cmd = _db.CreateCommand(sqlStr);
			AddParameter(cmd, "Resell_acct_id", value.Resell_acct_id);
			AddParameter(cmd, "Partner_id", value.Partner_id);
			AddParameter(cmd, "Customer_acct_id", value.Customer_acct_id);
			AddParameter(cmd, "Person_id", value.Person_id);
			AddParameter(cmd, "Per_route", value.Per_route);
			AddParameter(cmd, "Commision_type", value.Commision_type);
			AddParameter(cmd, "Markup_dollar", value.Markup_dollar);
			AddParameter(cmd, "Markup_percent", value.Markup_percent);
			AddParameter(cmd, "Fee_per_call", value.Fee_per_call);
			AddParameter(cmd, "Fee_per_minute", value.Fee_per_minute);
			cmd.ExecuteNonQuery();
		}

		/// <summary>
		/// Updates a record in the <c>ResellAcct</c> table.
		/// </summary>
		/// <param name="value">The <see cref="ResellAcctRow"/>
		/// object used to update the table record.</param>
		/// <returns>true if the record was updated; otherwise, false.</returns>
		public virtual bool Update(ResellAcctRow value)
		{
			string sqlStr = "UPDATE [dbo].[ResellAcct] SET " +
				"[partner_id]=" + _db.CreateSqlParameterName("Partner_id") + ", " +
				"[customer_acct_id]=" + _db.CreateSqlParameterName("Customer_acct_id") + ", " +
				"[person_id]=" + _db.CreateSqlParameterName("Person_id") + ", " +
				"[per_route]=" + _db.CreateSqlParameterName("Per_route") + ", " +
				"[commision_type]=" + _db.CreateSqlParameterName("Commision_type") + ", " +
				"[markup_dollar]=" + _db.CreateSqlParameterName("Markup_dollar") + ", " +
				"[markup_percent]=" + _db.CreateSqlParameterName("Markup_percent") + ", " +
				"[fee_per_call]=" + _db.CreateSqlParameterName("Fee_per_call") + ", " +
				"[fee_per_minute]=" + _db.CreateSqlParameterName("Fee_per_minute") +
				" WHERE " +
				"[resell_acct_id]=" + _db.CreateSqlParameterName("Resell_acct_id");
			IDbCommand cmd = _db.CreateCommand(sqlStr);
			AddParameter(cmd, "Partner_id", value.Partner_id);
			AddParameter(cmd, "Customer_acct_id", value.Customer_acct_id);
			AddParameter(cmd, "Person_id", value.Person_id);
			AddParameter(cmd, "Per_route", value.Per_route);
			AddParameter(cmd, "Commision_type", value.Commision_type);
			AddParameter(cmd, "Markup_dollar", value.Markup_dollar);
			AddParameter(cmd, "Markup_percent", value.Markup_percent);
			AddParameter(cmd, "Fee_per_call", value.Fee_per_call);
			AddParameter(cmd, "Fee_per_minute", value.Fee_per_minute);
			AddParameter(cmd, "Resell_acct_id", value.Resell_acct_id);
			return 0 != cmd.ExecuteNonQuery();
		}

		/// <summary>
		/// Updates the <c>ResellAcct</c> table and calls the <c>AcceptChanges</c> method
		/// on the changed DataRow objects.
		/// </summary>
		/// <param name="table">The <see cref="System.Data.DataTable"/> used to update the data source.</param>
		public void Update(DataTable table)
		{
			Update(table, true);
		}

		/// <summary>
		/// Updates the <c>ResellAcct</c> table. Pass <c>false</c> as the <c>acceptChanges</c> 
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
							DeleteByPrimaryKey((short)row["Resell_acct_id"]);
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
		/// Deletes the specified object from the <c>ResellAcct</c> table.
		/// </summary>
		/// <param name="value">The <see cref="ResellAcctRow"/> object to delete.</param>
		/// <returns>true if the record was deleted; otherwise, false.</returns>
		public bool Delete(ResellAcctRow value)
		{
			return DeleteByPrimaryKey(value.Resell_acct_id);
		}

		/// <summary>
		/// Deletes a record from the <c>ResellAcct</c> table using
		/// the specified primary key.
		/// </summary>
		/// <param name="resell_acct_id">The <c>resell_acct_id</c> column value.</param>
		/// <returns>true if the record was deleted; otherwise, false.</returns>
		public virtual bool DeleteByPrimaryKey(short resell_acct_id)
		{
			string whereSql = "[resell_acct_id]=" + _db.CreateSqlParameterName("Resell_acct_id");
			IDbCommand cmd = CreateDeleteCommand(whereSql);
			AddParameter(cmd, "Resell_acct_id", resell_acct_id);
			return 0 < cmd.ExecuteNonQuery();
		}

		/// <summary>
		/// Deletes records from the <c>ResellAcct</c> table using the 
		/// <c>R_288</c> foreign key.
		/// </summary>
		/// <param name="customer_acct_id">The <c>customer_acct_id</c> column value.</param>
		/// <returns>The number of records deleted from the table.</returns>
		public int DeleteByCustomer_acct_id(short customer_acct_id)
		{
			return CreateDeleteByCustomer_acct_idCommand(customer_acct_id).ExecuteNonQuery();
		}

		/// <summary>
		/// Creates an <see cref="System.Data.IDbCommand"/> object that can be used to
		/// delete records using the <c>R_288</c> foreign key.
		/// </summary>
		/// <param name="customer_acct_id">The <c>customer_acct_id</c> column value.</param>
		/// <returns>A reference to the <see cref="System.Data.IDbCommand"/> object.</returns>
		protected virtual IDbCommand CreateDeleteByCustomer_acct_idCommand(short customer_acct_id)
		{
			string whereSql = "";
			whereSql += "[customer_acct_id]=" + _db.CreateSqlParameterName("Customer_acct_id");

			IDbCommand cmd = CreateDeleteCommand(whereSql);
			AddParameter(cmd, "Customer_acct_id", customer_acct_id);
			return cmd;
		}

		/// <summary>
		/// Deletes records from the <c>ResellAcct</c> table using the 
		/// <c>R_291</c> foreign key.
		/// </summary>
		/// <param name="person_id">The <c>person_id</c> column value.</param>
		/// <returns>The number of records deleted from the table.</returns>
		public int DeleteByPerson_id(int person_id)
		{
			return CreateDeleteByPerson_idCommand(person_id).ExecuteNonQuery();
		}

		/// <summary>
		/// Creates an <see cref="System.Data.IDbCommand"/> object that can be used to
		/// delete records using the <c>R_291</c> foreign key.
		/// </summary>
		/// <param name="person_id">The <c>person_id</c> column value.</param>
		/// <returns>A reference to the <see cref="System.Data.IDbCommand"/> object.</returns>
		protected virtual IDbCommand CreateDeleteByPerson_idCommand(int person_id)
		{
			string whereSql = "";
			whereSql += "[person_id]=" + _db.CreateSqlParameterName("Person_id");

			IDbCommand cmd = CreateDeleteCommand(whereSql);
			AddParameter(cmd, "Person_id", person_id);
			return cmd;
		}

		/// <summary>
		/// Deletes records from the <c>ResellAcct</c> table using the 
		/// <c>R_370</c> foreign key.
		/// </summary>
		/// <param name="partner_id">The <c>partner_id</c> column value.</param>
		/// <returns>The number of records deleted from the table.</returns>
		public int DeleteByPartner_id(int partner_id)
		{
			return CreateDeleteByPartner_idCommand(partner_id).ExecuteNonQuery();
		}

		/// <summary>
		/// Creates an <see cref="System.Data.IDbCommand"/> object that can be used to
		/// delete records using the <c>R_370</c> foreign key.
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
		/// Deletes <c>ResellAcct</c> records that match the specified criteria.
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
		/// to delete <c>ResellAcct</c> records that match the specified criteria.
		/// </summary>
		/// <param name="whereSql">The SQL search condition. 
		/// For example: <c>"FirstName='Smith' AND Zip=75038"</c>.</param>
		/// <returns>A reference to the <see cref="System.Data.IDbCommand"/> object.</returns>
		protected virtual IDbCommand CreateDeleteCommand(string whereSql)
		{
			string sql = "DELETE FROM [dbo].[ResellAcct]";
			if(null != whereSql && 0 < whereSql.Length)
				sql += " WHERE " + whereSql;
			return _db.CreateCommand(sql);
		}

		/// <summary>
		/// Deletes all records from the <c>ResellAcct</c> table.
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
		/// <returns>An array of <see cref="ResellAcctRow"/> objects.</returns>
		protected ResellAcctRow[] MapRecords(IDbCommand command)
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
		/// <returns>An array of <see cref="ResellAcctRow"/> objects.</returns>
		protected ResellAcctRow[] MapRecords(IDataReader reader)
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
		/// <returns>An array of <see cref="ResellAcctRow"/> objects.</returns>
		protected virtual ResellAcctRow[] MapRecords(IDataReader reader, 
										int startIndex, int length, ref int totalRecordCount)
		{
			if(0 > startIndex)
				throw new ArgumentOutOfRangeException("startIndex", startIndex, "StartIndex cannot be less than zero.");
			if(0 > length)
				throw new ArgumentOutOfRangeException("length", length, "Length cannot be less than zero.");

			int resell_acct_idColumnIndex = reader.GetOrdinal("resell_acct_id");
			int partner_idColumnIndex = reader.GetOrdinal("partner_id");
			int customer_acct_idColumnIndex = reader.GetOrdinal("customer_acct_id");
			int person_idColumnIndex = reader.GetOrdinal("person_id");
			int per_routeColumnIndex = reader.GetOrdinal("per_route");
			int commision_typeColumnIndex = reader.GetOrdinal("commision_type");
			int markup_dollarColumnIndex = reader.GetOrdinal("markup_dollar");
			int markup_percentColumnIndex = reader.GetOrdinal("markup_percent");
			int fee_per_callColumnIndex = reader.GetOrdinal("fee_per_call");
			int fee_per_minuteColumnIndex = reader.GetOrdinal("fee_per_minute");

			System.Collections.ArrayList recordList = new System.Collections.ArrayList();
			int ri = -startIndex;
			while(reader.Read())
			{
				ri++;
				if(ri > 0 && ri <= length)
				{
					ResellAcctRow record = new ResellAcctRow();
					recordList.Add(record);

					record.Resell_acct_id = Convert.ToInt16(reader.GetValue(resell_acct_idColumnIndex));
					record.Partner_id = Convert.ToInt32(reader.GetValue(partner_idColumnIndex));
					record.Customer_acct_id = Convert.ToInt16(reader.GetValue(customer_acct_idColumnIndex));
					record.Person_id = Convert.ToInt32(reader.GetValue(person_idColumnIndex));
					record.Per_route = Convert.ToByte(reader.GetValue(per_routeColumnIndex));
					record.Commision_type = Convert.ToByte(reader.GetValue(commision_typeColumnIndex));
					record.Markup_dollar = Convert.ToDecimal(reader.GetValue(markup_dollarColumnIndex));
					record.Markup_percent = Convert.ToDecimal(reader.GetValue(markup_percentColumnIndex));
					record.Fee_per_call = Convert.ToDecimal(reader.GetValue(fee_per_callColumnIndex));
					record.Fee_per_minute = Convert.ToDecimal(reader.GetValue(fee_per_minuteColumnIndex));

					if(ri == length && 0 != totalRecordCount)
						break;
				}
			}

			totalRecordCount = 0 == totalRecordCount ? ri + startIndex : -1;
			return (ResellAcctRow[])(recordList.ToArray(typeof(ResellAcctRow)));
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
		/// Converts <see cref="System.Data.DataRow"/> to <see cref="ResellAcctRow"/>.
		/// </summary>
		/// <param name="row">The <see cref="System.Data.DataRow"/> object to be mapped.</param>
		/// <returns>A reference to the <see cref="ResellAcctRow"/> object.</returns>
		protected virtual ResellAcctRow MapRow(DataRow row)
		{
			ResellAcctRow mappedObject = new ResellAcctRow();
			DataTable dataTable = row.Table;
			DataColumn dataColumn;
			// Column "Resell_acct_id"
			dataColumn = dataTable.Columns["Resell_acct_id"];
			if(!row.IsNull(dataColumn))
				mappedObject.Resell_acct_id = (short)row[dataColumn];
			// Column "Partner_id"
			dataColumn = dataTable.Columns["Partner_id"];
			if(!row.IsNull(dataColumn))
				mappedObject.Partner_id = (int)row[dataColumn];
			// Column "Customer_acct_id"
			dataColumn = dataTable.Columns["Customer_acct_id"];
			if(!row.IsNull(dataColumn))
				mappedObject.Customer_acct_id = (short)row[dataColumn];
			// Column "Person_id"
			dataColumn = dataTable.Columns["Person_id"];
			if(!row.IsNull(dataColumn))
				mappedObject.Person_id = (int)row[dataColumn];
			// Column "Per_route"
			dataColumn = dataTable.Columns["Per_route"];
			if(!row.IsNull(dataColumn))
				mappedObject.Per_route = (byte)row[dataColumn];
			// Column "Commision_type"
			dataColumn = dataTable.Columns["Commision_type"];
			if(!row.IsNull(dataColumn))
				mappedObject.Commision_type = (byte)row[dataColumn];
			// Column "Markup_dollar"
			dataColumn = dataTable.Columns["Markup_dollar"];
			if(!row.IsNull(dataColumn))
				mappedObject.Markup_dollar = (decimal)row[dataColumn];
			// Column "Markup_percent"
			dataColumn = dataTable.Columns["Markup_percent"];
			if(!row.IsNull(dataColumn))
				mappedObject.Markup_percent = (decimal)row[dataColumn];
			// Column "Fee_per_call"
			dataColumn = dataTable.Columns["Fee_per_call"];
			if(!row.IsNull(dataColumn))
				mappedObject.Fee_per_call = (decimal)row[dataColumn];
			// Column "Fee_per_minute"
			dataColumn = dataTable.Columns["Fee_per_minute"];
			if(!row.IsNull(dataColumn))
				mappedObject.Fee_per_minute = (decimal)row[dataColumn];
			return mappedObject;
		}

		/// <summary>
		/// Creates a <see cref="System.Data.DataTable"/> object for 
		/// the <c>ResellAcct</c> table.
		/// </summary>
		/// <returns>A reference to the <see cref="System.Data.DataTable"/> object.</returns>
		protected virtual DataTable CreateDataTable()
		{
			DataTable dataTable = new DataTable();
			dataTable.TableName = "ResellAcct";
			DataColumn dataColumn;
			dataColumn = dataTable.Columns.Add("Resell_acct_id", typeof(short));
			dataColumn.Caption = "resell_acct_id";
			dataColumn.AllowDBNull = false;
			dataColumn = dataTable.Columns.Add("Partner_id", typeof(int));
			dataColumn.Caption = "partner_id";
			dataColumn.AllowDBNull = false;
			dataColumn = dataTable.Columns.Add("Customer_acct_id", typeof(short));
			dataColumn.Caption = "customer_acct_id";
			dataColumn.AllowDBNull = false;
			dataColumn = dataTable.Columns.Add("Person_id", typeof(int));
			dataColumn.Caption = "person_id";
			dataColumn.AllowDBNull = false;
			dataColumn = dataTable.Columns.Add("Per_route", typeof(byte));
			dataColumn.Caption = "per_route";
			dataColumn.AllowDBNull = false;
			dataColumn = dataTable.Columns.Add("Commision_type", typeof(byte));
			dataColumn.Caption = "commision_type";
			dataColumn.AllowDBNull = false;
			dataColumn = dataTable.Columns.Add("Markup_dollar", typeof(decimal));
			dataColumn.Caption = "markup_dollar";
			dataColumn.AllowDBNull = false;
			dataColumn = dataTable.Columns.Add("Markup_percent", typeof(decimal));
			dataColumn.Caption = "markup_percent";
			dataColumn.AllowDBNull = false;
			dataColumn = dataTable.Columns.Add("Fee_per_call", typeof(decimal));
			dataColumn.Caption = "fee_per_call";
			dataColumn.AllowDBNull = false;
			dataColumn = dataTable.Columns.Add("Fee_per_minute", typeof(decimal));
			dataColumn.Caption = "fee_per_minute";
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
				case "Resell_acct_id":
					parameter = _db.AddParameter(cmd, paramName, DbType.Int16, value);
					break;

				case "Partner_id":
					parameter = _db.AddParameter(cmd, paramName, DbType.Int32, value);
					break;

				case "Customer_acct_id":
					parameter = _db.AddParameter(cmd, paramName, DbType.Int16, value);
					break;

				case "Person_id":
					parameter = _db.AddParameter(cmd, paramName, DbType.Int32, value);
					break;

				case "Per_route":
					parameter = _db.AddParameter(cmd, paramName, DbType.Byte, value);
					break;

				case "Commision_type":
					parameter = _db.AddParameter(cmd, paramName, DbType.Byte, value);
					break;

				case "Markup_dollar":
					parameter = _db.AddParameter(cmd, paramName, DbType.Decimal, value);
					break;

				case "Markup_percent":
					parameter = _db.AddParameter(cmd, paramName, DbType.Decimal, value);
					break;

				case "Fee_per_call":
					parameter = _db.AddParameter(cmd, paramName, DbType.Decimal, value);
					break;

				case "Fee_per_minute":
					parameter = _db.AddParameter(cmd, paramName, DbType.Decimal, value);
					break;

				default:
					throw new ArgumentException("Unknown parameter name (" + paramName + ").");
			}
			return parameter;
		}

	} // End of ResellAcctCollection_Base class
}  // End of namespace

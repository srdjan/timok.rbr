// <fileinfo name="Base\PartnerCollection_Base.cs">
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
	/// The base class for <see cref="PartnerCollection"/>. Provides methods 
	/// for common database table operations. 
	/// </summary>
	/// <remarks>
	/// Do not change this source code. Update the <see cref="PartnerCollection"/>
	/// class if you need to add or change some functionality.
	/// </remarks>
	public abstract class PartnerCollection_Base
	{
		// Constants
		public const string Partner_idColumnName = "partner_id";
		public const string NameColumnName = "name";
		public const string StatusColumnName = "status";
		public const string Contact_info_idColumnName = "contact_info_id";
		public const string Billing_schedule_idColumnName = "billing_schedule_id";
		public const string Virtual_switch_idColumnName = "virtual_switch_id";

		// Instance fields
		private Rbr_Db _db;

		/// <summary>
		/// Initializes a new instance of the <see cref="PartnerCollection_Base"/> 
		/// class with the specified <see cref="Rbr_Db"/>.
		/// </summary>
		/// <param name="db">The <see cref="Rbr_Db"/> object.</param>
		public PartnerCollection_Base(Rbr_Db db)
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
		/// Gets an array of all records from the <c>Partner</c> table.
		/// </summary>
		/// <returns>An array of <see cref="PartnerRow"/> objects.</returns>
		public virtual PartnerRow[] GetAll()
		{
			return MapRecords(CreateGetAllCommand());
		}

		/// <summary>
		/// Gets a <see cref="System.Data.DataTable"/> object that 
		/// includes all records from the <c>Partner</c> table.
		/// </summary>
		/// <returns>A reference to the <see cref="System.Data.DataTable"/> object.</returns>
		public virtual DataTable GetAllAsDataTable()
		{
			return MapRecordsToDataTable(CreateGetAllCommand());
		}

		/// <summary>
		/// Creates and returns an <see cref="System.Data.IDbCommand"/> object that is used
		/// to retrieve all records from the <c>Partner</c> table.
		/// </summary>
		/// <returns>A reference to the <see cref="System.Data.IDbCommand"/> object.</returns>
		protected virtual IDbCommand CreateGetAllCommand()
		{
			return CreateGetCommand(null, null);
		}

		/// <summary>
		/// Gets the first <see cref="PartnerRow"/> objects that 
		/// match the search condition.
		/// </summary>
		/// <param name="whereSql">The SQL search condition. For example: 
		/// <c>"FirstName='Smith' AND Zip=75038"</c>.</param>
		/// <returns>An instance of <see cref="PartnerRow"/> or null reference 
		/// (Nothing in Visual Basic) if the object was not found.</returns>
		public PartnerRow GetRow(string whereSql)
		{
			int totalRecordCount = -1;
			PartnerRow[] rows = GetAsArray(whereSql, null, 0, 1, ref totalRecordCount);
			return 0 == rows.Length ? null : rows[0];
		}

		/// <summary>
		/// Gets an array of <see cref="PartnerRow"/> objects that 
		/// match the search condition, in the the specified sort order.
		/// </summary>
		/// <param name="whereSql">The SQL search condition. For example: 
		/// <c>"FirstName='Smith' AND Zip=75038"</c>.</param>
		/// <param name="orderBySql">The column name(s) followed by "ASC" (ascending) or "DESC" (descending).
		/// Columns are sorted in ascending order by default. For example: <c>"LastName ASC, FirstName ASC"</c>.</param>
		/// <returns>An array of <see cref="PartnerRow"/> objects.</returns>
		public PartnerRow[] GetAsArray(string whereSql, string orderBySql)
		{
			int totalRecordCount = -1;
			return GetAsArray(whereSql, orderBySql, 0, int.MaxValue, ref totalRecordCount);
		}

		/// <summary>
		/// Gets an array of <see cref="PartnerRow"/> objects that 
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
		/// <returns>An array of <see cref="PartnerRow"/> objects.</returns>
		public virtual PartnerRow[] GetAsArray(string whereSql, string orderBySql,
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
			string sql = "SELECT * FROM [dbo].[Partner]";
			if(null != whereSql && 0 < whereSql.Length)
				sql += " WHERE " + whereSql;
			if(null != orderBySql && 0 < orderBySql.Length)
				sql += " ORDER BY " + orderBySql;
			return _db.CreateCommand(sql);
		}

		/// <summary>
		/// Gets <see cref="PartnerRow"/> by the primary key.
		/// </summary>
		/// <param name="partner_id">The <c>partner_id</c> column value.</param>
		/// <returns>An instance of <see cref="PartnerRow"/> or null reference 
		/// (Nothing in Visual Basic) if the object was not found.</returns>
		public virtual PartnerRow GetByPrimaryKey(int partner_id)
		{
			string whereSql = "[partner_id]=" + _db.CreateSqlParameterName("Partner_id");
			IDbCommand cmd = CreateGetCommand(whereSql, null);
			AddParameter(cmd, "Partner_id", partner_id);
			PartnerRow[] tempArray = MapRecords(cmd);
			return 0 == tempArray.Length ? null : tempArray[0];
		}

		/// <summary>
		/// Gets an array of <see cref="PartnerRow"/> objects 
		/// by the <c>R_138</c> foreign key.
		/// </summary>
		/// <param name="billing_schedule_id">The <c>billing_schedule_id</c> column value.</param>
		/// <returns>An array of <see cref="PartnerRow"/> objects.</returns>
		public PartnerRow[] GetByBilling_schedule_id(int billing_schedule_id)
		{
			return GetByBilling_schedule_id(billing_schedule_id, false);
		}

		/// <summary>
		/// Gets an array of <see cref="PartnerRow"/> objects 
		/// by the <c>R_138</c> foreign key.
		/// </summary>
		/// <param name="billing_schedule_id">The <c>billing_schedule_id</c> column value.</param>
		/// <param name="billing_schedule_idNull">true if the method ignores the billing_schedule_id
		/// parameter value and uses DbNull instead of it; otherwise, false.</param>
		/// <returns>An array of <see cref="PartnerRow"/> objects.</returns>
		public virtual PartnerRow[] GetByBilling_schedule_id(int billing_schedule_id, bool billing_schedule_idNull)
		{
			return MapRecords(CreateGetByBilling_schedule_idCommand(billing_schedule_id, billing_schedule_idNull));
		}

		/// <summary>
		/// Gets a <see cref="System.Data.DataTable"/> object 
		/// by the <c>R_138</c> foreign key.
		/// </summary>
		/// <param name="billing_schedule_id">The <c>billing_schedule_id</c> column value.</param>
		/// <returns>A reference to the <see cref="System.Data.DataTable"/> object.</returns>
		public DataTable GetByBilling_schedule_idAsDataTable(int billing_schedule_id)
		{
			return GetByBilling_schedule_idAsDataTable(billing_schedule_id, false);
		}

		/// <summary>
		/// Gets a <see cref="System.Data.DataTable"/> object 
		/// by the <c>R_138</c> foreign key.
		/// </summary>
		/// <param name="billing_schedule_id">The <c>billing_schedule_id</c> column value.</param>
		/// <param name="billing_schedule_idNull">true if the method ignores the billing_schedule_id
		/// parameter value and uses DbNull instead of it; otherwise, false.</param>
		/// <returns>A reference to the <see cref="System.Data.DataTable"/> object.</returns>
		public virtual DataTable GetByBilling_schedule_idAsDataTable(int billing_schedule_id, bool billing_schedule_idNull)
		{
			return MapRecordsToDataTable(CreateGetByBilling_schedule_idCommand(billing_schedule_id, billing_schedule_idNull));
		}

		/// <summary>
		/// Creates an <see cref="System.Data.IDbCommand"/> object that can be used to 
		/// return records by the <c>R_138</c> foreign key.
		/// </summary>
		/// <param name="billing_schedule_id">The <c>billing_schedule_id</c> column value.</param>
		/// <param name="billing_schedule_idNull">true if the method ignores the billing_schedule_id
		/// parameter value and uses DbNull instead of it; otherwise, false.</param>
		/// <returns>A reference to the <see cref="System.Data.IDbCommand"/> object.</returns>
		protected virtual IDbCommand CreateGetByBilling_schedule_idCommand(int billing_schedule_id, bool billing_schedule_idNull)
		{
			string whereSql = "";
			if(billing_schedule_idNull)
				whereSql += "[billing_schedule_id] IS NULL";
			else
				whereSql += "[billing_schedule_id]=" + _db.CreateSqlParameterName("Billing_schedule_id");

			IDbCommand cmd = CreateGetCommand(whereSql, null);
			if(!billing_schedule_idNull)
				AddParameter(cmd, "Billing_schedule_id", billing_schedule_id);
			return cmd;
		}

		/// <summary>
		/// Gets an array of <see cref="PartnerRow"/> objects 
		/// by the <c>R_140</c> foreign key.
		/// </summary>
		/// <param name="contact_info_id">The <c>contact_info_id</c> column value.</param>
		/// <returns>An array of <see cref="PartnerRow"/> objects.</returns>
		public virtual PartnerRow[] GetByContact_info_id(int contact_info_id)
		{
			return MapRecords(CreateGetByContact_info_idCommand(contact_info_id));
		}

		/// <summary>
		/// Gets a <see cref="System.Data.DataTable"/> object 
		/// by the <c>R_140</c> foreign key.
		/// </summary>
		/// <param name="contact_info_id">The <c>contact_info_id</c> column value.</param>
		/// <returns>A reference to the <see cref="System.Data.DataTable"/> object.</returns>
		public virtual DataTable GetByContact_info_idAsDataTable(int contact_info_id)
		{
			return MapRecordsToDataTable(CreateGetByContact_info_idCommand(contact_info_id));
		}

		/// <summary>
		/// Creates an <see cref="System.Data.IDbCommand"/> object that can be used to 
		/// return records by the <c>R_140</c> foreign key.
		/// </summary>
		/// <param name="contact_info_id">The <c>contact_info_id</c> column value.</param>
		/// <returns>A reference to the <see cref="System.Data.IDbCommand"/> object.</returns>
		protected virtual IDbCommand CreateGetByContact_info_idCommand(int contact_info_id)
		{
			string whereSql = "";
			whereSql += "[contact_info_id]=" + _db.CreateSqlParameterName("Contact_info_id");

			IDbCommand cmd = CreateGetCommand(whereSql, null);
			AddParameter(cmd, "Contact_info_id", contact_info_id);
			return cmd;
		}

		/// <summary>
		/// Gets an array of <see cref="PartnerRow"/> objects 
		/// by the <c>R_301</c> foreign key.
		/// </summary>
		/// <param name="virtual_switch_id">The <c>virtual_switch_id</c> column value.</param>
		/// <returns>An array of <see cref="PartnerRow"/> objects.</returns>
		public virtual PartnerRow[] GetByVirtual_switch_id(int virtual_switch_id)
		{
			return MapRecords(CreateGetByVirtual_switch_idCommand(virtual_switch_id));
		}

		/// <summary>
		/// Gets a <see cref="System.Data.DataTable"/> object 
		/// by the <c>R_301</c> foreign key.
		/// </summary>
		/// <param name="virtual_switch_id">The <c>virtual_switch_id</c> column value.</param>
		/// <returns>A reference to the <see cref="System.Data.DataTable"/> object.</returns>
		public virtual DataTable GetByVirtual_switch_idAsDataTable(int virtual_switch_id)
		{
			return MapRecordsToDataTable(CreateGetByVirtual_switch_idCommand(virtual_switch_id));
		}

		/// <summary>
		/// Creates an <see cref="System.Data.IDbCommand"/> object that can be used to 
		/// return records by the <c>R_301</c> foreign key.
		/// </summary>
		/// <param name="virtual_switch_id">The <c>virtual_switch_id</c> column value.</param>
		/// <returns>A reference to the <see cref="System.Data.IDbCommand"/> object.</returns>
		protected virtual IDbCommand CreateGetByVirtual_switch_idCommand(int virtual_switch_id)
		{
			string whereSql = "";
			whereSql += "[virtual_switch_id]=" + _db.CreateSqlParameterName("Virtual_switch_id");

			IDbCommand cmd = CreateGetCommand(whereSql, null);
			AddParameter(cmd, "Virtual_switch_id", virtual_switch_id);
			return cmd;
		}

		/// <summary>
		/// Adds a new record into the <c>Partner</c> table.
		/// </summary>
		/// <param name="value">The <see cref="PartnerRow"/> object to be inserted.</param>
		public virtual void Insert(PartnerRow value)
		{
			string sqlStr = "INSERT INTO [dbo].[Partner] (" +
				"[partner_id], " +
				"[name], " +
				"[status], " +
				"[contact_info_id], " +
				"[billing_schedule_id], " +
				"[virtual_switch_id]" +
				") VALUES (" +
				_db.CreateSqlParameterName("Partner_id") + ", " +
				_db.CreateSqlParameterName("Name") + ", " +
				_db.CreateSqlParameterName("Status") + ", " +
				_db.CreateSqlParameterName("Contact_info_id") + ", " +
				_db.CreateSqlParameterName("Billing_schedule_id") + ", " +
				_db.CreateSqlParameterName("Virtual_switch_id") + ")";
			IDbCommand cmd = _db.CreateCommand(sqlStr);
			AddParameter(cmd, "Partner_id", value.Partner_id);
			AddParameter(cmd, "Name", value.Name);
			AddParameter(cmd, "Status", value.Status);
			AddParameter(cmd, "Contact_info_id", value.Contact_info_id);
			AddParameter(cmd, "Billing_schedule_id",
				value.IsBilling_schedule_idNull ? DBNull.Value : (object)value.Billing_schedule_id);
			AddParameter(cmd, "Virtual_switch_id", value.Virtual_switch_id);
			cmd.ExecuteNonQuery();
		}

		/// <summary>
		/// Updates a record in the <c>Partner</c> table.
		/// </summary>
		/// <param name="value">The <see cref="PartnerRow"/>
		/// object used to update the table record.</param>
		/// <returns>true if the record was updated; otherwise, false.</returns>
		public virtual bool Update(PartnerRow value)
		{
			string sqlStr = "UPDATE [dbo].[Partner] SET " +
				"[name]=" + _db.CreateSqlParameterName("Name") + ", " +
				"[status]=" + _db.CreateSqlParameterName("Status") + ", " +
				"[contact_info_id]=" + _db.CreateSqlParameterName("Contact_info_id") + ", " +
				"[billing_schedule_id]=" + _db.CreateSqlParameterName("Billing_schedule_id") + ", " +
				"[virtual_switch_id]=" + _db.CreateSqlParameterName("Virtual_switch_id") +
				" WHERE " +
				"[partner_id]=" + _db.CreateSqlParameterName("Partner_id");
			IDbCommand cmd = _db.CreateCommand(sqlStr);
			AddParameter(cmd, "Name", value.Name);
			AddParameter(cmd, "Status", value.Status);
			AddParameter(cmd, "Contact_info_id", value.Contact_info_id);
			AddParameter(cmd, "Billing_schedule_id",
				value.IsBilling_schedule_idNull ? DBNull.Value : (object)value.Billing_schedule_id);
			AddParameter(cmd, "Virtual_switch_id", value.Virtual_switch_id);
			AddParameter(cmd, "Partner_id", value.Partner_id);
			return 0 != cmd.ExecuteNonQuery();
		}

		/// <summary>
		/// Updates the <c>Partner</c> table and calls the <c>AcceptChanges</c> method
		/// on the changed DataRow objects.
		/// </summary>
		/// <param name="table">The <see cref="System.Data.DataTable"/> used to update the data source.</param>
		public void Update(DataTable table)
		{
			Update(table, true);
		}

		/// <summary>
		/// Updates the <c>Partner</c> table. Pass <c>false</c> as the <c>acceptChanges</c> 
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
							DeleteByPrimaryKey((int)row["Partner_id"]);
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
		/// Deletes the specified object from the <c>Partner</c> table.
		/// </summary>
		/// <param name="value">The <see cref="PartnerRow"/> object to delete.</param>
		/// <returns>true if the record was deleted; otherwise, false.</returns>
		public bool Delete(PartnerRow value)
		{
			return DeleteByPrimaryKey(value.Partner_id);
		}

		/// <summary>
		/// Deletes a record from the <c>Partner</c> table using
		/// the specified primary key.
		/// </summary>
		/// <param name="partner_id">The <c>partner_id</c> column value.</param>
		/// <returns>true if the record was deleted; otherwise, false.</returns>
		public virtual bool DeleteByPrimaryKey(int partner_id)
		{
			string whereSql = "[partner_id]=" + _db.CreateSqlParameterName("Partner_id");
			IDbCommand cmd = CreateDeleteCommand(whereSql);
			AddParameter(cmd, "Partner_id", partner_id);
			return 0 < cmd.ExecuteNonQuery();
		}

		/// <summary>
		/// Deletes records from the <c>Partner</c> table using the 
		/// <c>R_138</c> foreign key.
		/// </summary>
		/// <param name="billing_schedule_id">The <c>billing_schedule_id</c> column value.</param>
		/// <returns>The number of records deleted from the table.</returns>
		public int DeleteByBilling_schedule_id(int billing_schedule_id)
		{
			return DeleteByBilling_schedule_id(billing_schedule_id, false);
		}

		/// <summary>
		/// Deletes records from the <c>Partner</c> table using the 
		/// <c>R_138</c> foreign key.
		/// </summary>
		/// <param name="billing_schedule_id">The <c>billing_schedule_id</c> column value.</param>
		/// <param name="billing_schedule_idNull">true if the method ignores the billing_schedule_id
		/// parameter value and uses DbNull instead of it; otherwise, false.</param>
		/// <returns>The number of records deleted from the table.</returns>
		public int DeleteByBilling_schedule_id(int billing_schedule_id, bool billing_schedule_idNull)
		{
			return CreateDeleteByBilling_schedule_idCommand(billing_schedule_id, billing_schedule_idNull).ExecuteNonQuery();
		}

		/// <summary>
		/// Creates an <see cref="System.Data.IDbCommand"/> object that can be used to
		/// delete records using the <c>R_138</c> foreign key.
		/// </summary>
		/// <param name="billing_schedule_id">The <c>billing_schedule_id</c> column value.</param>
		/// <param name="billing_schedule_idNull">true if the method ignores the billing_schedule_id
		/// parameter value and uses DbNull instead of it; otherwise, false.</param>
		/// <returns>A reference to the <see cref="System.Data.IDbCommand"/> object.</returns>
		protected virtual IDbCommand CreateDeleteByBilling_schedule_idCommand(int billing_schedule_id, bool billing_schedule_idNull)
		{
			string whereSql = "";
			if(billing_schedule_idNull)
				whereSql += "[billing_schedule_id] IS NULL";
			else
				whereSql += "[billing_schedule_id]=" + _db.CreateSqlParameterName("Billing_schedule_id");

			IDbCommand cmd = CreateDeleteCommand(whereSql);
			if(!billing_schedule_idNull)
				AddParameter(cmd, "Billing_schedule_id", billing_schedule_id);
			return cmd;
		}

		/// <summary>
		/// Deletes records from the <c>Partner</c> table using the 
		/// <c>R_140</c> foreign key.
		/// </summary>
		/// <param name="contact_info_id">The <c>contact_info_id</c> column value.</param>
		/// <returns>The number of records deleted from the table.</returns>
		public int DeleteByContact_info_id(int contact_info_id)
		{
			return CreateDeleteByContact_info_idCommand(contact_info_id).ExecuteNonQuery();
		}

		/// <summary>
		/// Creates an <see cref="System.Data.IDbCommand"/> object that can be used to
		/// delete records using the <c>R_140</c> foreign key.
		/// </summary>
		/// <param name="contact_info_id">The <c>contact_info_id</c> column value.</param>
		/// <returns>A reference to the <see cref="System.Data.IDbCommand"/> object.</returns>
		protected virtual IDbCommand CreateDeleteByContact_info_idCommand(int contact_info_id)
		{
			string whereSql = "";
			whereSql += "[contact_info_id]=" + _db.CreateSqlParameterName("Contact_info_id");

			IDbCommand cmd = CreateDeleteCommand(whereSql);
			AddParameter(cmd, "Contact_info_id", contact_info_id);
			return cmd;
		}

		/// <summary>
		/// Deletes records from the <c>Partner</c> table using the 
		/// <c>R_301</c> foreign key.
		/// </summary>
		/// <param name="virtual_switch_id">The <c>virtual_switch_id</c> column value.</param>
		/// <returns>The number of records deleted from the table.</returns>
		public int DeleteByVirtual_switch_id(int virtual_switch_id)
		{
			return CreateDeleteByVirtual_switch_idCommand(virtual_switch_id).ExecuteNonQuery();
		}

		/// <summary>
		/// Creates an <see cref="System.Data.IDbCommand"/> object that can be used to
		/// delete records using the <c>R_301</c> foreign key.
		/// </summary>
		/// <param name="virtual_switch_id">The <c>virtual_switch_id</c> column value.</param>
		/// <returns>A reference to the <see cref="System.Data.IDbCommand"/> object.</returns>
		protected virtual IDbCommand CreateDeleteByVirtual_switch_idCommand(int virtual_switch_id)
		{
			string whereSql = "";
			whereSql += "[virtual_switch_id]=" + _db.CreateSqlParameterName("Virtual_switch_id");

			IDbCommand cmd = CreateDeleteCommand(whereSql);
			AddParameter(cmd, "Virtual_switch_id", virtual_switch_id);
			return cmd;
		}

		/// <summary>
		/// Deletes <c>Partner</c> records that match the specified criteria.
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
		/// to delete <c>Partner</c> records that match the specified criteria.
		/// </summary>
		/// <param name="whereSql">The SQL search condition. 
		/// For example: <c>"FirstName='Smith' AND Zip=75038"</c>.</param>
		/// <returns>A reference to the <see cref="System.Data.IDbCommand"/> object.</returns>
		protected virtual IDbCommand CreateDeleteCommand(string whereSql)
		{
			string sql = "DELETE FROM [dbo].[Partner]";
			if(null != whereSql && 0 < whereSql.Length)
				sql += " WHERE " + whereSql;
			return _db.CreateCommand(sql);
		}

		/// <summary>
		/// Deletes all records from the <c>Partner</c> table.
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
		/// <returns>An array of <see cref="PartnerRow"/> objects.</returns>
		protected PartnerRow[] MapRecords(IDbCommand command)
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
		/// <returns>An array of <see cref="PartnerRow"/> objects.</returns>
		protected PartnerRow[] MapRecords(IDataReader reader)
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
		/// <returns>An array of <see cref="PartnerRow"/> objects.</returns>
		protected virtual PartnerRow[] MapRecords(IDataReader reader, 
										int startIndex, int length, ref int totalRecordCount)
		{
			if(0 > startIndex)
				throw new ArgumentOutOfRangeException("startIndex", startIndex, "StartIndex cannot be less than zero.");
			if(0 > length)
				throw new ArgumentOutOfRangeException("length", length, "Length cannot be less than zero.");

			int partner_idColumnIndex = reader.GetOrdinal("partner_id");
			int nameColumnIndex = reader.GetOrdinal("name");
			int statusColumnIndex = reader.GetOrdinal("status");
			int contact_info_idColumnIndex = reader.GetOrdinal("contact_info_id");
			int billing_schedule_idColumnIndex = reader.GetOrdinal("billing_schedule_id");
			int virtual_switch_idColumnIndex = reader.GetOrdinal("virtual_switch_id");

			System.Collections.ArrayList recordList = new System.Collections.ArrayList();
			int ri = -startIndex;
			while(reader.Read())
			{
				ri++;
				if(ri > 0 && ri <= length)
				{
					PartnerRow record = new PartnerRow();
					recordList.Add(record);

					record.Partner_id = Convert.ToInt32(reader.GetValue(partner_idColumnIndex));
					record.Name = Convert.ToString(reader.GetValue(nameColumnIndex));
					record.Status = Convert.ToByte(reader.GetValue(statusColumnIndex));
					record.Contact_info_id = Convert.ToInt32(reader.GetValue(contact_info_idColumnIndex));
					if(!reader.IsDBNull(billing_schedule_idColumnIndex))
						record.Billing_schedule_id = Convert.ToInt32(reader.GetValue(billing_schedule_idColumnIndex));
					record.Virtual_switch_id = Convert.ToInt32(reader.GetValue(virtual_switch_idColumnIndex));

					if(ri == length && 0 != totalRecordCount)
						break;
				}
			}

			totalRecordCount = 0 == totalRecordCount ? ri + startIndex : -1;
			return (PartnerRow[])(recordList.ToArray(typeof(PartnerRow)));
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
		/// Converts <see cref="System.Data.DataRow"/> to <see cref="PartnerRow"/>.
		/// </summary>
		/// <param name="row">The <see cref="System.Data.DataRow"/> object to be mapped.</param>
		/// <returns>A reference to the <see cref="PartnerRow"/> object.</returns>
		protected virtual PartnerRow MapRow(DataRow row)
		{
			PartnerRow mappedObject = new PartnerRow();
			DataTable dataTable = row.Table;
			DataColumn dataColumn;
			// Column "Partner_id"
			dataColumn = dataTable.Columns["Partner_id"];
			if(!row.IsNull(dataColumn))
				mappedObject.Partner_id = (int)row[dataColumn];
			// Column "Name"
			dataColumn = dataTable.Columns["Name"];
			if(!row.IsNull(dataColumn))
				mappedObject.Name = (string)row[dataColumn];
			// Column "Status"
			dataColumn = dataTable.Columns["Status"];
			if(!row.IsNull(dataColumn))
				mappedObject.Status = (byte)row[dataColumn];
			// Column "Contact_info_id"
			dataColumn = dataTable.Columns["Contact_info_id"];
			if(!row.IsNull(dataColumn))
				mappedObject.Contact_info_id = (int)row[dataColumn];
			// Column "Billing_schedule_id"
			dataColumn = dataTable.Columns["Billing_schedule_id"];
			if(!row.IsNull(dataColumn))
				mappedObject.Billing_schedule_id = (int)row[dataColumn];
			// Column "Virtual_switch_id"
			dataColumn = dataTable.Columns["Virtual_switch_id"];
			if(!row.IsNull(dataColumn))
				mappedObject.Virtual_switch_id = (int)row[dataColumn];
			return mappedObject;
		}

		/// <summary>
		/// Creates a <see cref="System.Data.DataTable"/> object for 
		/// the <c>Partner</c> table.
		/// </summary>
		/// <returns>A reference to the <see cref="System.Data.DataTable"/> object.</returns>
		protected virtual DataTable CreateDataTable()
		{
			DataTable dataTable = new DataTable();
			dataTable.TableName = "Partner";
			DataColumn dataColumn;
			dataColumn = dataTable.Columns.Add("Partner_id", typeof(int));
			dataColumn.Caption = "partner_id";
			dataColumn.AllowDBNull = false;
			dataColumn = dataTable.Columns.Add("Name", typeof(string));
			dataColumn.Caption = "name";
			dataColumn.MaxLength = 50;
			dataColumn.AllowDBNull = false;
			dataColumn = dataTable.Columns.Add("Status", typeof(byte));
			dataColumn.Caption = "status";
			dataColumn.AllowDBNull = false;
			dataColumn = dataTable.Columns.Add("Contact_info_id", typeof(int));
			dataColumn.Caption = "contact_info_id";
			dataColumn.AllowDBNull = false;
			dataColumn = dataTable.Columns.Add("Billing_schedule_id", typeof(int));
			dataColumn.Caption = "billing_schedule_id";
			dataColumn = dataTable.Columns.Add("Virtual_switch_id", typeof(int));
			dataColumn.Caption = "virtual_switch_id";
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
				case "Partner_id":
					parameter = _db.AddParameter(cmd, paramName, DbType.Int32, value);
					break;

				case "Name":
					parameter = _db.AddParameter(cmd, paramName, DbType.AnsiString, value);
					break;

				case "Status":
					parameter = _db.AddParameter(cmd, paramName, DbType.Byte, value);
					break;

				case "Contact_info_id":
					parameter = _db.AddParameter(cmd, paramName, DbType.Int32, value);
					break;

				case "Billing_schedule_id":
					parameter = _db.AddParameter(cmd, paramName, DbType.Int32, value);
					break;

				case "Virtual_switch_id":
					parameter = _db.AddParameter(cmd, paramName, DbType.Int32, value);
					break;

				default:
					throw new ArgumentException("Unknown parameter name (" + paramName + ").");
			}
			return parameter;
		}

	} // End of PartnerCollection_Base class
}  // End of namespace

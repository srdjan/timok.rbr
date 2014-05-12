// <fileinfo name="Base\PersonCollection_Base.cs">
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
	/// The base class for <see cref="PersonCollection"/>. Provides methods 
	/// for common database table operations. 
	/// </summary>
	/// <remarks>
	/// Do not change this source code. Update the <see cref="PersonCollection"/>
	/// class if you need to add or change some functionality.
	/// </remarks>
	public abstract class PersonCollection_Base
	{
		// Constants
		public const string Person_idColumnName = "person_id";
		public const string NameColumnName = "name";
		public const string LoginColumnName = "login";
		public const string PasswordColumnName = "password";
		public const string PermissionColumnName = "permission";
		public const string Is_resellerColumnName = "is_reseller";
		public const string StatusColumnName = "status";
		public const string Registration_statusColumnName = "registration_status";
		public const string SaltColumnName = "salt";
		public const string Partner_idColumnName = "partner_id";
		public const string Retail_acct_idColumnName = "retail_acct_id";
		public const string Group_idColumnName = "group_id";
		public const string Virtual_switch_idColumnName = "virtual_switch_id";
		public const string Contact_info_idColumnName = "contact_info_id";

		// Instance fields
		private Rbr_Db _db;

		/// <summary>
		/// Initializes a new instance of the <see cref="PersonCollection_Base"/> 
		/// class with the specified <see cref="Rbr_Db"/>.
		/// </summary>
		/// <param name="db">The <see cref="Rbr_Db"/> object.</param>
		public PersonCollection_Base(Rbr_Db db)
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
		/// Gets an array of all records from the <c>Person</c> table.
		/// </summary>
		/// <returns>An array of <see cref="PersonRow"/> objects.</returns>
		public virtual PersonRow[] GetAll()
		{
			return MapRecords(CreateGetAllCommand());
		}

		/// <summary>
		/// Gets a <see cref="System.Data.DataTable"/> object that 
		/// includes all records from the <c>Person</c> table.
		/// </summary>
		/// <returns>A reference to the <see cref="System.Data.DataTable"/> object.</returns>
		public virtual DataTable GetAllAsDataTable()
		{
			return MapRecordsToDataTable(CreateGetAllCommand());
		}

		/// <summary>
		/// Creates and returns an <see cref="System.Data.IDbCommand"/> object that is used
		/// to retrieve all records from the <c>Person</c> table.
		/// </summary>
		/// <returns>A reference to the <see cref="System.Data.IDbCommand"/> object.</returns>
		protected virtual IDbCommand CreateGetAllCommand()
		{
			return CreateGetCommand(null, null);
		}

		/// <summary>
		/// Gets the first <see cref="PersonRow"/> objects that 
		/// match the search condition.
		/// </summary>
		/// <param name="whereSql">The SQL search condition. For example: 
		/// <c>"FirstName='Smith' AND Zip=75038"</c>.</param>
		/// <returns>An instance of <see cref="PersonRow"/> or null reference 
		/// (Nothing in Visual Basic) if the object was not found.</returns>
		public PersonRow GetRow(string whereSql)
		{
			int totalRecordCount = -1;
			PersonRow[] rows = GetAsArray(whereSql, null, 0, 1, ref totalRecordCount);
			return 0 == rows.Length ? null : rows[0];
		}

		/// <summary>
		/// Gets an array of <see cref="PersonRow"/> objects that 
		/// match the search condition, in the the specified sort order.
		/// </summary>
		/// <param name="whereSql">The SQL search condition. For example: 
		/// <c>"FirstName='Smith' AND Zip=75038"</c>.</param>
		/// <param name="orderBySql">The column name(s) followed by "ASC" (ascending) or "DESC" (descending).
		/// Columns are sorted in ascending order by default. For example: <c>"LastName ASC, FirstName ASC"</c>.</param>
		/// <returns>An array of <see cref="PersonRow"/> objects.</returns>
		public PersonRow[] GetAsArray(string whereSql, string orderBySql)
		{
			int totalRecordCount = -1;
			return GetAsArray(whereSql, orderBySql, 0, int.MaxValue, ref totalRecordCount);
		}

		/// <summary>
		/// Gets an array of <see cref="PersonRow"/> objects that 
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
		/// <returns>An array of <see cref="PersonRow"/> objects.</returns>
		public virtual PersonRow[] GetAsArray(string whereSql, string orderBySql,
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
			string sql = "SELECT * FROM [dbo].[Person]";
			if(null != whereSql && 0 < whereSql.Length)
				sql += " WHERE " + whereSql;
			if(null != orderBySql && 0 < orderBySql.Length)
				sql += " ORDER BY " + orderBySql;
			return _db.CreateCommand(sql);
		}

		/// <summary>
		/// Gets <see cref="PersonRow"/> by the primary key.
		/// </summary>
		/// <param name="person_id">The <c>person_id</c> column value.</param>
		/// <returns>An instance of <see cref="PersonRow"/> or null reference 
		/// (Nothing in Visual Basic) if the object was not found.</returns>
		public virtual PersonRow GetByPrimaryKey(int person_id)
		{
			string whereSql = "[person_id]=" + _db.CreateSqlParameterName("Person_id");
			IDbCommand cmd = CreateGetCommand(whereSql, null);
			AddParameter(cmd, "Person_id", person_id);
			PersonRow[] tempArray = MapRecords(cmd);
			return 0 == tempArray.Length ? null : tempArray[0];
		}

		/// <summary>
		/// Gets an array of <see cref="PersonRow"/> objects 
		/// by the <c>R_238</c> foreign key.
		/// </summary>
		/// <param name="retail_acct_id">The <c>retail_acct_id</c> column value.</param>
		/// <returns>An array of <see cref="PersonRow"/> objects.</returns>
		public PersonRow[] GetByRetail_acct_id(int retail_acct_id)
		{
			return GetByRetail_acct_id(retail_acct_id, false);
		}

		/// <summary>
		/// Gets an array of <see cref="PersonRow"/> objects 
		/// by the <c>R_238</c> foreign key.
		/// </summary>
		/// <param name="retail_acct_id">The <c>retail_acct_id</c> column value.</param>
		/// <param name="retail_acct_idNull">true if the method ignores the retail_acct_id
		/// parameter value and uses DbNull instead of it; otherwise, false.</param>
		/// <returns>An array of <see cref="PersonRow"/> objects.</returns>
		public virtual PersonRow[] GetByRetail_acct_id(int retail_acct_id, bool retail_acct_idNull)
		{
			return MapRecords(CreateGetByRetail_acct_idCommand(retail_acct_id, retail_acct_idNull));
		}

		/// <summary>
		/// Gets a <see cref="System.Data.DataTable"/> object 
		/// by the <c>R_238</c> foreign key.
		/// </summary>
		/// <param name="retail_acct_id">The <c>retail_acct_id</c> column value.</param>
		/// <returns>A reference to the <see cref="System.Data.DataTable"/> object.</returns>
		public DataTable GetByRetail_acct_idAsDataTable(int retail_acct_id)
		{
			return GetByRetail_acct_idAsDataTable(retail_acct_id, false);
		}

		/// <summary>
		/// Gets a <see cref="System.Data.DataTable"/> object 
		/// by the <c>R_238</c> foreign key.
		/// </summary>
		/// <param name="retail_acct_id">The <c>retail_acct_id</c> column value.</param>
		/// <param name="retail_acct_idNull">true if the method ignores the retail_acct_id
		/// parameter value and uses DbNull instead of it; otherwise, false.</param>
		/// <returns>A reference to the <see cref="System.Data.DataTable"/> object.</returns>
		public virtual DataTable GetByRetail_acct_idAsDataTable(int retail_acct_id, bool retail_acct_idNull)
		{
			return MapRecordsToDataTable(CreateGetByRetail_acct_idCommand(retail_acct_id, retail_acct_idNull));
		}

		/// <summary>
		/// Creates an <see cref="System.Data.IDbCommand"/> object that can be used to 
		/// return records by the <c>R_238</c> foreign key.
		/// </summary>
		/// <param name="retail_acct_id">The <c>retail_acct_id</c> column value.</param>
		/// <param name="retail_acct_idNull">true if the method ignores the retail_acct_id
		/// parameter value and uses DbNull instead of it; otherwise, false.</param>
		/// <returns>A reference to the <see cref="System.Data.IDbCommand"/> object.</returns>
		protected virtual IDbCommand CreateGetByRetail_acct_idCommand(int retail_acct_id, bool retail_acct_idNull)
		{
			string whereSql = "";
			if(retail_acct_idNull)
				whereSql += "[retail_acct_id] IS NULL";
			else
				whereSql += "[retail_acct_id]=" + _db.CreateSqlParameterName("Retail_acct_id");

			IDbCommand cmd = CreateGetCommand(whereSql, null);
			if(!retail_acct_idNull)
				AddParameter(cmd, "Retail_acct_id", retail_acct_id);
			return cmd;
		}

		/// <summary>
		/// Gets an array of <see cref="PersonRow"/> objects 
		/// by the <c>R_240</c> foreign key.
		/// </summary>
		/// <param name="partner_id">The <c>partner_id</c> column value.</param>
		/// <returns>An array of <see cref="PersonRow"/> objects.</returns>
		public PersonRow[] GetByPartner_id(int partner_id)
		{
			return GetByPartner_id(partner_id, false);
		}

		/// <summary>
		/// Gets an array of <see cref="PersonRow"/> objects 
		/// by the <c>R_240</c> foreign key.
		/// </summary>
		/// <param name="partner_id">The <c>partner_id</c> column value.</param>
		/// <param name="partner_idNull">true if the method ignores the partner_id
		/// parameter value and uses DbNull instead of it; otherwise, false.</param>
		/// <returns>An array of <see cref="PersonRow"/> objects.</returns>
		public virtual PersonRow[] GetByPartner_id(int partner_id, bool partner_idNull)
		{
			return MapRecords(CreateGetByPartner_idCommand(partner_id, partner_idNull));
		}

		/// <summary>
		/// Gets a <see cref="System.Data.DataTable"/> object 
		/// by the <c>R_240</c> foreign key.
		/// </summary>
		/// <param name="partner_id">The <c>partner_id</c> column value.</param>
		/// <returns>A reference to the <see cref="System.Data.DataTable"/> object.</returns>
		public DataTable GetByPartner_idAsDataTable(int partner_id)
		{
			return GetByPartner_idAsDataTable(partner_id, false);
		}

		/// <summary>
		/// Gets a <see cref="System.Data.DataTable"/> object 
		/// by the <c>R_240</c> foreign key.
		/// </summary>
		/// <param name="partner_id">The <c>partner_id</c> column value.</param>
		/// <param name="partner_idNull">true if the method ignores the partner_id
		/// parameter value and uses DbNull instead of it; otherwise, false.</param>
		/// <returns>A reference to the <see cref="System.Data.DataTable"/> object.</returns>
		public virtual DataTable GetByPartner_idAsDataTable(int partner_id, bool partner_idNull)
		{
			return MapRecordsToDataTable(CreateGetByPartner_idCommand(partner_id, partner_idNull));
		}

		/// <summary>
		/// Creates an <see cref="System.Data.IDbCommand"/> object that can be used to 
		/// return records by the <c>R_240</c> foreign key.
		/// </summary>
		/// <param name="partner_id">The <c>partner_id</c> column value.</param>
		/// <param name="partner_idNull">true if the method ignores the partner_id
		/// parameter value and uses DbNull instead of it; otherwise, false.</param>
		/// <returns>A reference to the <see cref="System.Data.IDbCommand"/> object.</returns>
		protected virtual IDbCommand CreateGetByPartner_idCommand(int partner_id, bool partner_idNull)
		{
			string whereSql = "";
			if(partner_idNull)
				whereSql += "[partner_id] IS NULL";
			else
				whereSql += "[partner_id]=" + _db.CreateSqlParameterName("Partner_id");

			IDbCommand cmd = CreateGetCommand(whereSql, null);
			if(!partner_idNull)
				AddParameter(cmd, "Partner_id", partner_id);
			return cmd;
		}

		/// <summary>
		/// Gets an array of <see cref="PersonRow"/> objects 
		/// by the <c>R_312</c> foreign key.
		/// </summary>
		/// <param name="virtual_switch_id">The <c>virtual_switch_id</c> column value.</param>
		/// <returns>An array of <see cref="PersonRow"/> objects.</returns>
		public PersonRow[] GetByVirtual_switch_id(int virtual_switch_id)
		{
			return GetByVirtual_switch_id(virtual_switch_id, false);
		}

		/// <summary>
		/// Gets an array of <see cref="PersonRow"/> objects 
		/// by the <c>R_312</c> foreign key.
		/// </summary>
		/// <param name="virtual_switch_id">The <c>virtual_switch_id</c> column value.</param>
		/// <param name="virtual_switch_idNull">true if the method ignores the virtual_switch_id
		/// parameter value and uses DbNull instead of it; otherwise, false.</param>
		/// <returns>An array of <see cref="PersonRow"/> objects.</returns>
		public virtual PersonRow[] GetByVirtual_switch_id(int virtual_switch_id, bool virtual_switch_idNull)
		{
			return MapRecords(CreateGetByVirtual_switch_idCommand(virtual_switch_id, virtual_switch_idNull));
		}

		/// <summary>
		/// Gets a <see cref="System.Data.DataTable"/> object 
		/// by the <c>R_312</c> foreign key.
		/// </summary>
		/// <param name="virtual_switch_id">The <c>virtual_switch_id</c> column value.</param>
		/// <returns>A reference to the <see cref="System.Data.DataTable"/> object.</returns>
		public DataTable GetByVirtual_switch_idAsDataTable(int virtual_switch_id)
		{
			return GetByVirtual_switch_idAsDataTable(virtual_switch_id, false);
		}

		/// <summary>
		/// Gets a <see cref="System.Data.DataTable"/> object 
		/// by the <c>R_312</c> foreign key.
		/// </summary>
		/// <param name="virtual_switch_id">The <c>virtual_switch_id</c> column value.</param>
		/// <param name="virtual_switch_idNull">true if the method ignores the virtual_switch_id
		/// parameter value and uses DbNull instead of it; otherwise, false.</param>
		/// <returns>A reference to the <see cref="System.Data.DataTable"/> object.</returns>
		public virtual DataTable GetByVirtual_switch_idAsDataTable(int virtual_switch_id, bool virtual_switch_idNull)
		{
			return MapRecordsToDataTable(CreateGetByVirtual_switch_idCommand(virtual_switch_id, virtual_switch_idNull));
		}

		/// <summary>
		/// Creates an <see cref="System.Data.IDbCommand"/> object that can be used to 
		/// return records by the <c>R_312</c> foreign key.
		/// </summary>
		/// <param name="virtual_switch_id">The <c>virtual_switch_id</c> column value.</param>
		/// <param name="virtual_switch_idNull">true if the method ignores the virtual_switch_id
		/// parameter value and uses DbNull instead of it; otherwise, false.</param>
		/// <returns>A reference to the <see cref="System.Data.IDbCommand"/> object.</returns>
		protected virtual IDbCommand CreateGetByVirtual_switch_idCommand(int virtual_switch_id, bool virtual_switch_idNull)
		{
			string whereSql = "";
			if(virtual_switch_idNull)
				whereSql += "[virtual_switch_id] IS NULL";
			else
				whereSql += "[virtual_switch_id]=" + _db.CreateSqlParameterName("Virtual_switch_id");

			IDbCommand cmd = CreateGetCommand(whereSql, null);
			if(!virtual_switch_idNull)
				AddParameter(cmd, "Virtual_switch_id", virtual_switch_id);
			return cmd;
		}

		/// <summary>
		/// Gets an array of <see cref="PersonRow"/> objects 
		/// by the <c>R_382</c> foreign key.
		/// </summary>
		/// <param name="contact_info_id">The <c>contact_info_id</c> column value.</param>
		/// <returns>An array of <see cref="PersonRow"/> objects.</returns>
		public PersonRow[] GetByContact_info_id(int contact_info_id)
		{
			return GetByContact_info_id(contact_info_id, false);
		}

		/// <summary>
		/// Gets an array of <see cref="PersonRow"/> objects 
		/// by the <c>R_382</c> foreign key.
		/// </summary>
		/// <param name="contact_info_id">The <c>contact_info_id</c> column value.</param>
		/// <param name="contact_info_idNull">true if the method ignores the contact_info_id
		/// parameter value and uses DbNull instead of it; otherwise, false.</param>
		/// <returns>An array of <see cref="PersonRow"/> objects.</returns>
		public virtual PersonRow[] GetByContact_info_id(int contact_info_id, bool contact_info_idNull)
		{
			return MapRecords(CreateGetByContact_info_idCommand(contact_info_id, contact_info_idNull));
		}

		/// <summary>
		/// Gets a <see cref="System.Data.DataTable"/> object 
		/// by the <c>R_382</c> foreign key.
		/// </summary>
		/// <param name="contact_info_id">The <c>contact_info_id</c> column value.</param>
		/// <returns>A reference to the <see cref="System.Data.DataTable"/> object.</returns>
		public DataTable GetByContact_info_idAsDataTable(int contact_info_id)
		{
			return GetByContact_info_idAsDataTable(contact_info_id, false);
		}

		/// <summary>
		/// Gets a <see cref="System.Data.DataTable"/> object 
		/// by the <c>R_382</c> foreign key.
		/// </summary>
		/// <param name="contact_info_id">The <c>contact_info_id</c> column value.</param>
		/// <param name="contact_info_idNull">true if the method ignores the contact_info_id
		/// parameter value and uses DbNull instead of it; otherwise, false.</param>
		/// <returns>A reference to the <see cref="System.Data.DataTable"/> object.</returns>
		public virtual DataTable GetByContact_info_idAsDataTable(int contact_info_id, bool contact_info_idNull)
		{
			return MapRecordsToDataTable(CreateGetByContact_info_idCommand(contact_info_id, contact_info_idNull));
		}

		/// <summary>
		/// Creates an <see cref="System.Data.IDbCommand"/> object that can be used to 
		/// return records by the <c>R_382</c> foreign key.
		/// </summary>
		/// <param name="contact_info_id">The <c>contact_info_id</c> column value.</param>
		/// <param name="contact_info_idNull">true if the method ignores the contact_info_id
		/// parameter value and uses DbNull instead of it; otherwise, false.</param>
		/// <returns>A reference to the <see cref="System.Data.IDbCommand"/> object.</returns>
		protected virtual IDbCommand CreateGetByContact_info_idCommand(int contact_info_id, bool contact_info_idNull)
		{
			string whereSql = "";
			if(contact_info_idNull)
				whereSql += "[contact_info_id] IS NULL";
			else
				whereSql += "[contact_info_id]=" + _db.CreateSqlParameterName("Contact_info_id");

			IDbCommand cmd = CreateGetCommand(whereSql, null);
			if(!contact_info_idNull)
				AddParameter(cmd, "Contact_info_id", contact_info_id);
			return cmd;
		}

		/// <summary>
		/// Gets an array of <see cref="PersonRow"/> objects 
		/// by the <c>R_555</c> foreign key.
		/// </summary>
		/// <param name="group_id">The <c>group_id</c> column value.</param>
		/// <returns>An array of <see cref="PersonRow"/> objects.</returns>
		public PersonRow[] GetByGroup_id(int group_id)
		{
			return GetByGroup_id(group_id, false);
		}

		/// <summary>
		/// Gets an array of <see cref="PersonRow"/> objects 
		/// by the <c>R_555</c> foreign key.
		/// </summary>
		/// <param name="group_id">The <c>group_id</c> column value.</param>
		/// <param name="group_idNull">true if the method ignores the group_id
		/// parameter value and uses DbNull instead of it; otherwise, false.</param>
		/// <returns>An array of <see cref="PersonRow"/> objects.</returns>
		public virtual PersonRow[] GetByGroup_id(int group_id, bool group_idNull)
		{
			return MapRecords(CreateGetByGroup_idCommand(group_id, group_idNull));
		}

		/// <summary>
		/// Gets a <see cref="System.Data.DataTable"/> object 
		/// by the <c>R_555</c> foreign key.
		/// </summary>
		/// <param name="group_id">The <c>group_id</c> column value.</param>
		/// <returns>A reference to the <see cref="System.Data.DataTable"/> object.</returns>
		public DataTable GetByGroup_idAsDataTable(int group_id)
		{
			return GetByGroup_idAsDataTable(group_id, false);
		}

		/// <summary>
		/// Gets a <see cref="System.Data.DataTable"/> object 
		/// by the <c>R_555</c> foreign key.
		/// </summary>
		/// <param name="group_id">The <c>group_id</c> column value.</param>
		/// <param name="group_idNull">true if the method ignores the group_id
		/// parameter value and uses DbNull instead of it; otherwise, false.</param>
		/// <returns>A reference to the <see cref="System.Data.DataTable"/> object.</returns>
		public virtual DataTable GetByGroup_idAsDataTable(int group_id, bool group_idNull)
		{
			return MapRecordsToDataTable(CreateGetByGroup_idCommand(group_id, group_idNull));
		}

		/// <summary>
		/// Creates an <see cref="System.Data.IDbCommand"/> object that can be used to 
		/// return records by the <c>R_555</c> foreign key.
		/// </summary>
		/// <param name="group_id">The <c>group_id</c> column value.</param>
		/// <param name="group_idNull">true if the method ignores the group_id
		/// parameter value and uses DbNull instead of it; otherwise, false.</param>
		/// <returns>A reference to the <see cref="System.Data.IDbCommand"/> object.</returns>
		protected virtual IDbCommand CreateGetByGroup_idCommand(int group_id, bool group_idNull)
		{
			string whereSql = "";
			if(group_idNull)
				whereSql += "[group_id] IS NULL";
			else
				whereSql += "[group_id]=" + _db.CreateSqlParameterName("Group_id");

			IDbCommand cmd = CreateGetCommand(whereSql, null);
			if(!group_idNull)
				AddParameter(cmd, "Group_id", group_id);
			return cmd;
		}

		/// <summary>
		/// Adds a new record into the <c>Person</c> table.
		/// </summary>
		/// <param name="value">The <see cref="PersonRow"/> object to be inserted.</param>
		public virtual void Insert(PersonRow value)
		{
			string sqlStr = "INSERT INTO [dbo].[Person] (" +
				"[person_id], " +
				"[name], " +
				"[login], " +
				"[password], " +
				"[permission], " +
				"[is_reseller], " +
				"[status], " +
				"[registration_status], " +
				"[partner_id], " +
				"[retail_acct_id], " +
				"[group_id], " +
				"[virtual_switch_id], " +
				"[contact_info_id]" +
				") VALUES (" +
				_db.CreateSqlParameterName("Person_id") + ", " +
				_db.CreateSqlParameterName("Name") + ", " +
				_db.CreateSqlParameterName("Login") + ", " +
				_db.CreateSqlParameterName("Password") + ", " +
				_db.CreateSqlParameterName("Permission") + ", " +
				_db.CreateSqlParameterName("Is_reseller") + ", " +
				_db.CreateSqlParameterName("Status") + ", " +
				_db.CreateSqlParameterName("Registration_status") + ", " +
				_db.CreateSqlParameterName("Partner_id") + ", " +
				_db.CreateSqlParameterName("Retail_acct_id") + ", " +
				_db.CreateSqlParameterName("Group_id") + ", " +
				_db.CreateSqlParameterName("Virtual_switch_id") + ", " +
				_db.CreateSqlParameterName("Contact_info_id") + ")";
			IDbCommand cmd = _db.CreateCommand(sqlStr);
			AddParameter(cmd, "Person_id", value.Person_id);
			AddParameter(cmd, "Name", value.Name);
			AddParameter(cmd, "Login", value.Login);
			AddParameter(cmd, "Password", value.Password);
			AddParameter(cmd, "Permission", value.Permission);
			AddParameter(cmd, "Is_reseller", value.Is_reseller);
			AddParameter(cmd, "Status", value.Status);
			AddParameter(cmd, "Registration_status", value.Registration_status);
			AddParameter(cmd, "Partner_id",
				value.IsPartner_idNull ? DBNull.Value : (object)value.Partner_id);
			AddParameter(cmd, "Retail_acct_id",
				value.IsRetail_acct_idNull ? DBNull.Value : (object)value.Retail_acct_id);
			AddParameter(cmd, "Group_id",
				value.IsGroup_idNull ? DBNull.Value : (object)value.Group_id);
			AddParameter(cmd, "Virtual_switch_id",
				value.IsVirtual_switch_idNull ? DBNull.Value : (object)value.Virtual_switch_id);
			AddParameter(cmd, "Contact_info_id",
				value.IsContact_info_idNull ? DBNull.Value : (object)value.Contact_info_id);
			cmd.ExecuteNonQuery();
		}

		/// <summary>
		/// Updates a record in the <c>Person</c> table.
		/// </summary>
		/// <param name="value">The <see cref="PersonRow"/>
		/// object used to update the table record.</param>
		/// <returns>true if the record was updated; otherwise, false.</returns>
		public virtual bool Update(PersonRow value)
		{
			string sqlStr = "UPDATE [dbo].[Person] SET " +
				"[name]=" + _db.CreateSqlParameterName("Name") + ", " +
				"[login]=" + _db.CreateSqlParameterName("Login") + ", " +
				"[password]=" + _db.CreateSqlParameterName("Password") + ", " +
				"[permission]=" + _db.CreateSqlParameterName("Permission") + ", " +
				"[is_reseller]=" + _db.CreateSqlParameterName("Is_reseller") + ", " +
				"[status]=" + _db.CreateSqlParameterName("Status") + ", " +
				"[registration_status]=" + _db.CreateSqlParameterName("Registration_status") + ", " +
				"[partner_id]=" + _db.CreateSqlParameterName("Partner_id") + ", " +
				"[retail_acct_id]=" + _db.CreateSqlParameterName("Retail_acct_id") + ", " +
				"[group_id]=" + _db.CreateSqlParameterName("Group_id") + ", " +
				"[virtual_switch_id]=" + _db.CreateSqlParameterName("Virtual_switch_id") + ", " +
				"[contact_info_id]=" + _db.CreateSqlParameterName("Contact_info_id") +
				" WHERE " +
				"[person_id]=" + _db.CreateSqlParameterName("Person_id");
			IDbCommand cmd = _db.CreateCommand(sqlStr);
			AddParameter(cmd, "Name", value.Name);
			AddParameter(cmd, "Login", value.Login);
			AddParameter(cmd, "Password", value.Password);
			AddParameter(cmd, "Permission", value.Permission);
			AddParameter(cmd, "Is_reseller", value.Is_reseller);
			AddParameter(cmd, "Status", value.Status);
			AddParameter(cmd, "Registration_status", value.Registration_status);
			AddParameter(cmd, "Partner_id",
				value.IsPartner_idNull ? DBNull.Value : (object)value.Partner_id);
			AddParameter(cmd, "Retail_acct_id",
				value.IsRetail_acct_idNull ? DBNull.Value : (object)value.Retail_acct_id);
			AddParameter(cmd, "Group_id",
				value.IsGroup_idNull ? DBNull.Value : (object)value.Group_id);
			AddParameter(cmd, "Virtual_switch_id",
				value.IsVirtual_switch_idNull ? DBNull.Value : (object)value.Virtual_switch_id);
			AddParameter(cmd, "Contact_info_id",
				value.IsContact_info_idNull ? DBNull.Value : (object)value.Contact_info_id);
			AddParameter(cmd, "Person_id", value.Person_id);
			return 0 != cmd.ExecuteNonQuery();
		}

		/// <summary>
		/// Updates the <c>Person</c> table and calls the <c>AcceptChanges</c> method
		/// on the changed DataRow objects.
		/// </summary>
		/// <param name="table">The <see cref="System.Data.DataTable"/> used to update the data source.</param>
		public void Update(DataTable table)
		{
			Update(table, true);
		}

		/// <summary>
		/// Updates the <c>Person</c> table. Pass <c>false</c> as the <c>acceptChanges</c> 
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
							DeleteByPrimaryKey((int)row["Person_id"]);
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
		/// Deletes the specified object from the <c>Person</c> table.
		/// </summary>
		/// <param name="value">The <see cref="PersonRow"/> object to delete.</param>
		/// <returns>true if the record was deleted; otherwise, false.</returns>
		public bool Delete(PersonRow value)
		{
			return DeleteByPrimaryKey(value.Person_id);
		}

		/// <summary>
		/// Deletes a record from the <c>Person</c> table using
		/// the specified primary key.
		/// </summary>
		/// <param name="person_id">The <c>person_id</c> column value.</param>
		/// <returns>true if the record was deleted; otherwise, false.</returns>
		public virtual bool DeleteByPrimaryKey(int person_id)
		{
			string whereSql = "[person_id]=" + _db.CreateSqlParameterName("Person_id");
			IDbCommand cmd = CreateDeleteCommand(whereSql);
			AddParameter(cmd, "Person_id", person_id);
			return 0 < cmd.ExecuteNonQuery();
		}

		/// <summary>
		/// Deletes records from the <c>Person</c> table using the 
		/// <c>R_238</c> foreign key.
		/// </summary>
		/// <param name="retail_acct_id">The <c>retail_acct_id</c> column value.</param>
		/// <returns>The number of records deleted from the table.</returns>
		public int DeleteByRetail_acct_id(int retail_acct_id)
		{
			return DeleteByRetail_acct_id(retail_acct_id, false);
		}

		/// <summary>
		/// Deletes records from the <c>Person</c> table using the 
		/// <c>R_238</c> foreign key.
		/// </summary>
		/// <param name="retail_acct_id">The <c>retail_acct_id</c> column value.</param>
		/// <param name="retail_acct_idNull">true if the method ignores the retail_acct_id
		/// parameter value and uses DbNull instead of it; otherwise, false.</param>
		/// <returns>The number of records deleted from the table.</returns>
		public int DeleteByRetail_acct_id(int retail_acct_id, bool retail_acct_idNull)
		{
			return CreateDeleteByRetail_acct_idCommand(retail_acct_id, retail_acct_idNull).ExecuteNonQuery();
		}

		/// <summary>
		/// Creates an <see cref="System.Data.IDbCommand"/> object that can be used to
		/// delete records using the <c>R_238</c> foreign key.
		/// </summary>
		/// <param name="retail_acct_id">The <c>retail_acct_id</c> column value.</param>
		/// <param name="retail_acct_idNull">true if the method ignores the retail_acct_id
		/// parameter value and uses DbNull instead of it; otherwise, false.</param>
		/// <returns>A reference to the <see cref="System.Data.IDbCommand"/> object.</returns>
		protected virtual IDbCommand CreateDeleteByRetail_acct_idCommand(int retail_acct_id, bool retail_acct_idNull)
		{
			string whereSql = "";
			if(retail_acct_idNull)
				whereSql += "[retail_acct_id] IS NULL";
			else
				whereSql += "[retail_acct_id]=" + _db.CreateSqlParameterName("Retail_acct_id");

			IDbCommand cmd = CreateDeleteCommand(whereSql);
			if(!retail_acct_idNull)
				AddParameter(cmd, "Retail_acct_id", retail_acct_id);
			return cmd;
		}

		/// <summary>
		/// Deletes records from the <c>Person</c> table using the 
		/// <c>R_240</c> foreign key.
		/// </summary>
		/// <param name="partner_id">The <c>partner_id</c> column value.</param>
		/// <returns>The number of records deleted from the table.</returns>
		public int DeleteByPartner_id(int partner_id)
		{
			return DeleteByPartner_id(partner_id, false);
		}

		/// <summary>
		/// Deletes records from the <c>Person</c> table using the 
		/// <c>R_240</c> foreign key.
		/// </summary>
		/// <param name="partner_id">The <c>partner_id</c> column value.</param>
		/// <param name="partner_idNull">true if the method ignores the partner_id
		/// parameter value and uses DbNull instead of it; otherwise, false.</param>
		/// <returns>The number of records deleted from the table.</returns>
		public int DeleteByPartner_id(int partner_id, bool partner_idNull)
		{
			return CreateDeleteByPartner_idCommand(partner_id, partner_idNull).ExecuteNonQuery();
		}

		/// <summary>
		/// Creates an <see cref="System.Data.IDbCommand"/> object that can be used to
		/// delete records using the <c>R_240</c> foreign key.
		/// </summary>
		/// <param name="partner_id">The <c>partner_id</c> column value.</param>
		/// <param name="partner_idNull">true if the method ignores the partner_id
		/// parameter value and uses DbNull instead of it; otherwise, false.</param>
		/// <returns>A reference to the <see cref="System.Data.IDbCommand"/> object.</returns>
		protected virtual IDbCommand CreateDeleteByPartner_idCommand(int partner_id, bool partner_idNull)
		{
			string whereSql = "";
			if(partner_idNull)
				whereSql += "[partner_id] IS NULL";
			else
				whereSql += "[partner_id]=" + _db.CreateSqlParameterName("Partner_id");

			IDbCommand cmd = CreateDeleteCommand(whereSql);
			if(!partner_idNull)
				AddParameter(cmd, "Partner_id", partner_id);
			return cmd;
		}

		/// <summary>
		/// Deletes records from the <c>Person</c> table using the 
		/// <c>R_312</c> foreign key.
		/// </summary>
		/// <param name="virtual_switch_id">The <c>virtual_switch_id</c> column value.</param>
		/// <returns>The number of records deleted from the table.</returns>
		public int DeleteByVirtual_switch_id(int virtual_switch_id)
		{
			return DeleteByVirtual_switch_id(virtual_switch_id, false);
		}

		/// <summary>
		/// Deletes records from the <c>Person</c> table using the 
		/// <c>R_312</c> foreign key.
		/// </summary>
		/// <param name="virtual_switch_id">The <c>virtual_switch_id</c> column value.</param>
		/// <param name="virtual_switch_idNull">true if the method ignores the virtual_switch_id
		/// parameter value and uses DbNull instead of it; otherwise, false.</param>
		/// <returns>The number of records deleted from the table.</returns>
		public int DeleteByVirtual_switch_id(int virtual_switch_id, bool virtual_switch_idNull)
		{
			return CreateDeleteByVirtual_switch_idCommand(virtual_switch_id, virtual_switch_idNull).ExecuteNonQuery();
		}

		/// <summary>
		/// Creates an <see cref="System.Data.IDbCommand"/> object that can be used to
		/// delete records using the <c>R_312</c> foreign key.
		/// </summary>
		/// <param name="virtual_switch_id">The <c>virtual_switch_id</c> column value.</param>
		/// <param name="virtual_switch_idNull">true if the method ignores the virtual_switch_id
		/// parameter value and uses DbNull instead of it; otherwise, false.</param>
		/// <returns>A reference to the <see cref="System.Data.IDbCommand"/> object.</returns>
		protected virtual IDbCommand CreateDeleteByVirtual_switch_idCommand(int virtual_switch_id, bool virtual_switch_idNull)
		{
			string whereSql = "";
			if(virtual_switch_idNull)
				whereSql += "[virtual_switch_id] IS NULL";
			else
				whereSql += "[virtual_switch_id]=" + _db.CreateSqlParameterName("Virtual_switch_id");

			IDbCommand cmd = CreateDeleteCommand(whereSql);
			if(!virtual_switch_idNull)
				AddParameter(cmd, "Virtual_switch_id", virtual_switch_id);
			return cmd;
		}

		/// <summary>
		/// Deletes records from the <c>Person</c> table using the 
		/// <c>R_382</c> foreign key.
		/// </summary>
		/// <param name="contact_info_id">The <c>contact_info_id</c> column value.</param>
		/// <returns>The number of records deleted from the table.</returns>
		public int DeleteByContact_info_id(int contact_info_id)
		{
			return DeleteByContact_info_id(contact_info_id, false);
		}

		/// <summary>
		/// Deletes records from the <c>Person</c> table using the 
		/// <c>R_382</c> foreign key.
		/// </summary>
		/// <param name="contact_info_id">The <c>contact_info_id</c> column value.</param>
		/// <param name="contact_info_idNull">true if the method ignores the contact_info_id
		/// parameter value and uses DbNull instead of it; otherwise, false.</param>
		/// <returns>The number of records deleted from the table.</returns>
		public int DeleteByContact_info_id(int contact_info_id, bool contact_info_idNull)
		{
			return CreateDeleteByContact_info_idCommand(contact_info_id, contact_info_idNull).ExecuteNonQuery();
		}

		/// <summary>
		/// Creates an <see cref="System.Data.IDbCommand"/> object that can be used to
		/// delete records using the <c>R_382</c> foreign key.
		/// </summary>
		/// <param name="contact_info_id">The <c>contact_info_id</c> column value.</param>
		/// <param name="contact_info_idNull">true if the method ignores the contact_info_id
		/// parameter value and uses DbNull instead of it; otherwise, false.</param>
		/// <returns>A reference to the <see cref="System.Data.IDbCommand"/> object.</returns>
		protected virtual IDbCommand CreateDeleteByContact_info_idCommand(int contact_info_id, bool contact_info_idNull)
		{
			string whereSql = "";
			if(contact_info_idNull)
				whereSql += "[contact_info_id] IS NULL";
			else
				whereSql += "[contact_info_id]=" + _db.CreateSqlParameterName("Contact_info_id");

			IDbCommand cmd = CreateDeleteCommand(whereSql);
			if(!contact_info_idNull)
				AddParameter(cmd, "Contact_info_id", contact_info_id);
			return cmd;
		}

		/// <summary>
		/// Deletes records from the <c>Person</c> table using the 
		/// <c>R_555</c> foreign key.
		/// </summary>
		/// <param name="group_id">The <c>group_id</c> column value.</param>
		/// <returns>The number of records deleted from the table.</returns>
		public int DeleteByGroup_id(int group_id)
		{
			return DeleteByGroup_id(group_id, false);
		}

		/// <summary>
		/// Deletes records from the <c>Person</c> table using the 
		/// <c>R_555</c> foreign key.
		/// </summary>
		/// <param name="group_id">The <c>group_id</c> column value.</param>
		/// <param name="group_idNull">true if the method ignores the group_id
		/// parameter value and uses DbNull instead of it; otherwise, false.</param>
		/// <returns>The number of records deleted from the table.</returns>
		public int DeleteByGroup_id(int group_id, bool group_idNull)
		{
			return CreateDeleteByGroup_idCommand(group_id, group_idNull).ExecuteNonQuery();
		}

		/// <summary>
		/// Creates an <see cref="System.Data.IDbCommand"/> object that can be used to
		/// delete records using the <c>R_555</c> foreign key.
		/// </summary>
		/// <param name="group_id">The <c>group_id</c> column value.</param>
		/// <param name="group_idNull">true if the method ignores the group_id
		/// parameter value and uses DbNull instead of it; otherwise, false.</param>
		/// <returns>A reference to the <see cref="System.Data.IDbCommand"/> object.</returns>
		protected virtual IDbCommand CreateDeleteByGroup_idCommand(int group_id, bool group_idNull)
		{
			string whereSql = "";
			if(group_idNull)
				whereSql += "[group_id] IS NULL";
			else
				whereSql += "[group_id]=" + _db.CreateSqlParameterName("Group_id");

			IDbCommand cmd = CreateDeleteCommand(whereSql);
			if(!group_idNull)
				AddParameter(cmd, "Group_id", group_id);
			return cmd;
		}

		/// <summary>
		/// Deletes <c>Person</c> records that match the specified criteria.
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
		/// to delete <c>Person</c> records that match the specified criteria.
		/// </summary>
		/// <param name="whereSql">The SQL search condition. 
		/// For example: <c>"FirstName='Smith' AND Zip=75038"</c>.</param>
		/// <returns>A reference to the <see cref="System.Data.IDbCommand"/> object.</returns>
		protected virtual IDbCommand CreateDeleteCommand(string whereSql)
		{
			string sql = "DELETE FROM [dbo].[Person]";
			if(null != whereSql && 0 < whereSql.Length)
				sql += " WHERE " + whereSql;
			return _db.CreateCommand(sql);
		}

		/// <summary>
		/// Deletes all records from the <c>Person</c> table.
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
		/// <returns>An array of <see cref="PersonRow"/> objects.</returns>
		protected PersonRow[] MapRecords(IDbCommand command)
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
		/// <returns>An array of <see cref="PersonRow"/> objects.</returns>
		protected PersonRow[] MapRecords(IDataReader reader)
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
		/// <returns>An array of <see cref="PersonRow"/> objects.</returns>
		protected virtual PersonRow[] MapRecords(IDataReader reader, 
										int startIndex, int length, ref int totalRecordCount)
		{
			if(0 > startIndex)
				throw new ArgumentOutOfRangeException("startIndex", startIndex, "StartIndex cannot be less than zero.");
			if(0 > length)
				throw new ArgumentOutOfRangeException("length", length, "Length cannot be less than zero.");

			int person_idColumnIndex = reader.GetOrdinal("person_id");
			int nameColumnIndex = reader.GetOrdinal("name");
			int loginColumnIndex = reader.GetOrdinal("login");
			int passwordColumnIndex = reader.GetOrdinal("password");
			int permissionColumnIndex = reader.GetOrdinal("permission");
			int is_resellerColumnIndex = reader.GetOrdinal("is_reseller");
			int statusColumnIndex = reader.GetOrdinal("status");
			int registration_statusColumnIndex = reader.GetOrdinal("registration_status");
			int saltColumnIndex = reader.GetOrdinal("salt");
			int partner_idColumnIndex = reader.GetOrdinal("partner_id");
			int retail_acct_idColumnIndex = reader.GetOrdinal("retail_acct_id");
			int group_idColumnIndex = reader.GetOrdinal("group_id");
			int virtual_switch_idColumnIndex = reader.GetOrdinal("virtual_switch_id");
			int contact_info_idColumnIndex = reader.GetOrdinal("contact_info_id");

			System.Collections.ArrayList recordList = new System.Collections.ArrayList();
			int ri = -startIndex;
			while(reader.Read())
			{
				ri++;
				if(ri > 0 && ri <= length)
				{
					PersonRow record = new PersonRow();
					recordList.Add(record);

					record.Person_id = Convert.ToInt32(reader.GetValue(person_idColumnIndex));
					record.Name = Convert.ToString(reader.GetValue(nameColumnIndex));
					record.Login = Convert.ToString(reader.GetValue(loginColumnIndex));
					record.Password = Convert.ToString(reader.GetValue(passwordColumnIndex));
					record.Permission = Convert.ToByte(reader.GetValue(permissionColumnIndex));
					record.Is_reseller = Convert.ToByte(reader.GetValue(is_resellerColumnIndex));
					record.Status = Convert.ToByte(reader.GetValue(statusColumnIndex));
					record.Registration_status = Convert.ToByte(reader.GetValue(registration_statusColumnIndex));
					record.Salt = Convert.ToString(reader.GetValue(saltColumnIndex));
					if(!reader.IsDBNull(partner_idColumnIndex))
						record.Partner_id = Convert.ToInt32(reader.GetValue(partner_idColumnIndex));
					if(!reader.IsDBNull(retail_acct_idColumnIndex))
						record.Retail_acct_id = Convert.ToInt32(reader.GetValue(retail_acct_idColumnIndex));
					if(!reader.IsDBNull(group_idColumnIndex))
						record.Group_id = Convert.ToInt32(reader.GetValue(group_idColumnIndex));
					if(!reader.IsDBNull(virtual_switch_idColumnIndex))
						record.Virtual_switch_id = Convert.ToInt32(reader.GetValue(virtual_switch_idColumnIndex));
					if(!reader.IsDBNull(contact_info_idColumnIndex))
						record.Contact_info_id = Convert.ToInt32(reader.GetValue(contact_info_idColumnIndex));

					if(ri == length && 0 != totalRecordCount)
						break;
				}
			}

			totalRecordCount = 0 == totalRecordCount ? ri + startIndex : -1;
			return (PersonRow[])(recordList.ToArray(typeof(PersonRow)));
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
		/// Converts <see cref="System.Data.DataRow"/> to <see cref="PersonRow"/>.
		/// </summary>
		/// <param name="row">The <see cref="System.Data.DataRow"/> object to be mapped.</param>
		/// <returns>A reference to the <see cref="PersonRow"/> object.</returns>
		protected virtual PersonRow MapRow(DataRow row)
		{
			PersonRow mappedObject = new PersonRow();
			DataTable dataTable = row.Table;
			DataColumn dataColumn;
			// Column "Person_id"
			dataColumn = dataTable.Columns["Person_id"];
			if(!row.IsNull(dataColumn))
				mappedObject.Person_id = (int)row[dataColumn];
			// Column "Name"
			dataColumn = dataTable.Columns["Name"];
			if(!row.IsNull(dataColumn))
				mappedObject.Name = (string)row[dataColumn];
			// Column "Login"
			dataColumn = dataTable.Columns["Login"];
			if(!row.IsNull(dataColumn))
				mappedObject.Login = (string)row[dataColumn];
			// Column "Password"
			dataColumn = dataTable.Columns["Password"];
			if(!row.IsNull(dataColumn))
				mappedObject.Password = (string)row[dataColumn];
			// Column "Permission"
			dataColumn = dataTable.Columns["Permission"];
			if(!row.IsNull(dataColumn))
				mappedObject.Permission = (byte)row[dataColumn];
			// Column "Is_reseller"
			dataColumn = dataTable.Columns["Is_reseller"];
			if(!row.IsNull(dataColumn))
				mappedObject.Is_reseller = (byte)row[dataColumn];
			// Column "Status"
			dataColumn = dataTable.Columns["Status"];
			if(!row.IsNull(dataColumn))
				mappedObject.Status = (byte)row[dataColumn];
			// Column "Registration_status"
			dataColumn = dataTable.Columns["Registration_status"];
			if(!row.IsNull(dataColumn))
				mappedObject.Registration_status = (byte)row[dataColumn];
			// Column "Salt"
			dataColumn = dataTable.Columns["Salt"];
			if(!row.IsNull(dataColumn))
				mappedObject.Salt = (string)row[dataColumn];
			// Column "Partner_id"
			dataColumn = dataTable.Columns["Partner_id"];
			if(!row.IsNull(dataColumn))
				mappedObject.Partner_id = (int)row[dataColumn];
			// Column "Retail_acct_id"
			dataColumn = dataTable.Columns["Retail_acct_id"];
			if(!row.IsNull(dataColumn))
				mappedObject.Retail_acct_id = (int)row[dataColumn];
			// Column "Group_id"
			dataColumn = dataTable.Columns["Group_id"];
			if(!row.IsNull(dataColumn))
				mappedObject.Group_id = (int)row[dataColumn];
			// Column "Virtual_switch_id"
			dataColumn = dataTable.Columns["Virtual_switch_id"];
			if(!row.IsNull(dataColumn))
				mappedObject.Virtual_switch_id = (int)row[dataColumn];
			// Column "Contact_info_id"
			dataColumn = dataTable.Columns["Contact_info_id"];
			if(!row.IsNull(dataColumn))
				mappedObject.Contact_info_id = (int)row[dataColumn];
			return mappedObject;
		}

		/// <summary>
		/// Creates a <see cref="System.Data.DataTable"/> object for 
		/// the <c>Person</c> table.
		/// </summary>
		/// <returns>A reference to the <see cref="System.Data.DataTable"/> object.</returns>
		protected virtual DataTable CreateDataTable()
		{
			DataTable dataTable = new DataTable();
			dataTable.TableName = "Person";
			DataColumn dataColumn;
			dataColumn = dataTable.Columns.Add("Person_id", typeof(int));
			dataColumn.Caption = "person_id";
			dataColumn.AllowDBNull = false;
			dataColumn = dataTable.Columns.Add("Name", typeof(string));
			dataColumn.Caption = "name";
			dataColumn.MaxLength = 50;
			dataColumn.AllowDBNull = false;
			dataColumn = dataTable.Columns.Add("Login", typeof(string));
			dataColumn.Caption = "login";
			dataColumn.MaxLength = 50;
			dataColumn.AllowDBNull = false;
			dataColumn = dataTable.Columns.Add("Password", typeof(string));
			dataColumn.Caption = "password";
			dataColumn.MaxLength = 50;
			dataColumn.AllowDBNull = false;
			dataColumn = dataTable.Columns.Add("Permission", typeof(byte));
			dataColumn.Caption = "permission";
			dataColumn.AllowDBNull = false;
			dataColumn = dataTable.Columns.Add("Is_reseller", typeof(byte));
			dataColumn.Caption = "is_reseller";
			dataColumn.AllowDBNull = false;
			dataColumn = dataTable.Columns.Add("Status", typeof(byte));
			dataColumn.Caption = "status";
			dataColumn.AllowDBNull = false;
			dataColumn = dataTable.Columns.Add("Registration_status", typeof(byte));
			dataColumn.Caption = "registration_status";
			dataColumn.AllowDBNull = false;
			dataColumn = dataTable.Columns.Add("Salt", typeof(string));
			dataColumn.Caption = "salt";
			dataColumn.MaxLength = 50;
			dataColumn.AllowDBNull = false;
			dataColumn.ReadOnly = true;
			dataColumn = dataTable.Columns.Add("Partner_id", typeof(int));
			dataColumn.Caption = "partner_id";
			dataColumn = dataTable.Columns.Add("Retail_acct_id", typeof(int));
			dataColumn.Caption = "retail_acct_id";
			dataColumn = dataTable.Columns.Add("Group_id", typeof(int));
			dataColumn.Caption = "group_id";
			dataColumn = dataTable.Columns.Add("Virtual_switch_id", typeof(int));
			dataColumn.Caption = "virtual_switch_id";
			dataColumn = dataTable.Columns.Add("Contact_info_id", typeof(int));
			dataColumn.Caption = "contact_info_id";
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
				case "Person_id":
					parameter = _db.AddParameter(cmd, paramName, DbType.Int32, value);
					break;

				case "Name":
					parameter = _db.AddParameter(cmd, paramName, DbType.AnsiString, value);
					break;

				case "Login":
					parameter = _db.AddParameter(cmd, paramName, DbType.AnsiString, value);
					break;

				case "Password":
					parameter = _db.AddParameter(cmd, paramName, DbType.AnsiString, value);
					break;

				case "Permission":
					parameter = _db.AddParameter(cmd, paramName, DbType.Byte, value);
					break;

				case "Is_reseller":
					parameter = _db.AddParameter(cmd, paramName, DbType.Byte, value);
					break;

				case "Status":
					parameter = _db.AddParameter(cmd, paramName, DbType.Byte, value);
					break;

				case "Registration_status":
					parameter = _db.AddParameter(cmd, paramName, DbType.Byte, value);
					break;

				case "Salt":
					parameter = _db.AddParameter(cmd, paramName, DbType.AnsiString, value);
					break;

				case "Partner_id":
					parameter = _db.AddParameter(cmd, paramName, DbType.Int32, value);
					break;

				case "Retail_acct_id":
					parameter = _db.AddParameter(cmd, paramName, DbType.Int32, value);
					break;

				case "Group_id":
					parameter = _db.AddParameter(cmd, paramName, DbType.Int32, value);
					break;

				case "Virtual_switch_id":
					parameter = _db.AddParameter(cmd, paramName, DbType.Int32, value);
					break;

				case "Contact_info_id":
					parameter = _db.AddParameter(cmd, paramName, DbType.Int32, value);
					break;

				default:
					throw new ArgumentException("Unknown parameter name (" + paramName + ").");
			}
			return parameter;
		}

	} // End of PersonCollection_Base class
}  // End of namespace

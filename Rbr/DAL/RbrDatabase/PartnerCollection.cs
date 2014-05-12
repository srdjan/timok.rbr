// <fileinfo name="PartnerCollection.cs">
//		<copyright>
//			Copyright © 2002-2007 Timok ES LLC. All rights reserved.
//		</copyright>
//		<remarks>
//			You can update this source code manually. If the file
//			already exists it will not be rewritten by the generator.
//		</remarks>
//		<generator rewritefile="False" infourl="http://www.SharpPower.com">RapTier</generator>
// </fileinfo>

using System;
using System.Data;
using Timok.Rbr.DAL.RbrDatabase.Base;
using Timok.Core;
using Timok.Rbr.Core.Config;

namespace Timok.Rbr.DAL.RbrDatabase {
	/// <summary>
	/// Represents the <c>Partner</c> table.
	/// </summary>
	public class PartnerCollection : PartnerCollection_Base {
		/// <summary>
		/// Initializes a new instance of the <see cref="PartnerCollection"/> class.
		/// </summary>
		/// <param name="db">The database object.</param>
		internal PartnerCollection(Rbr_Db db)
			: base(db) {
			// EMPTY
		}

		public static PartnerRow Parse(System.Data.DataRow row){
			return new PartnerCollection(null).MapRow(row);
		}

    public PartnerRow[] GetActiveResellers() {
      string _whereSql = "[" + PartnerRow.status_DbName + "]=" + (byte) Status.Active +
        " AND " +
        PartnerRow.partner_id_DbName + " IN (" +
        " SELECT " + PersonRow.partner_id_DbName + " FROM Person WHERE " +
        PersonRow.status_DbName + " = " + (byte) Status.Active + 
        " AND " +
        PersonRow.is_reseller_DbName + " = 3 " + 
        " )";

      IDbCommand _cmd = base.CreateGetCommand(_whereSql, PartnerRow.name_DbName);
      using (IDataReader _reader = _cmd.ExecuteReader()) {
        return MapRecords(_reader);
      }
    }

		public bool IsNameInUseByOtherPartner(string name, int partner_id){
			string sqlStr = "SELECT COUNT(*) FROM [dbo].[Partner] WHERE " +
				"[" + PartnerRow.name_DbName + "]=" + base.Database.CreateSqlParameterName(PartnerRow.name_PropName) + 
				" AND " + 
				"[" + PartnerRow.partner_id_DbName + "]<>" + base.Database.CreateSqlParameterName(PartnerRow.partner_id_PropName);

			IDbCommand cmd = base.Database.CreateCommand(sqlStr);
			base.Database.AddParameter(cmd, PartnerRow.name_PropName, DbType.String, name);
			base.Database.AddParameter(cmd, PartnerRow.partner_id_PropName, DbType.Int32, partner_id);

			int _count = (int)cmd.ExecuteScalar();
			return _count > 0 ? true : false;
		}

		public override void Insert(PartnerRow value) {
			string sqlStr = "DECLARE " + 
				base.Database.CreateSqlParameterName(PartnerRow.partner_id_PropName) + " int " + 
				//				"DECLARE @Login_name varchar(50) " + 
				//				"DECLARE @Pwd varchar(50) " + 
				"SET " + 
				base.Database.CreateSqlParameterName(PartnerRow.partner_id_PropName) + 
				" = COALESCE((SELECT MAX(" + PartnerRow.partner_id_DbName + ") FROM Partner) + 1, 10000) " + 
				//				"SET @Login_name = @Name " + 
				//				"SET @Pwd = @Name + '@gw' " + 
			
				"INSERT INTO [dbo].[Partner] (" +
				"[" + PartnerRow.partner_id_DbName + "], " +
				"[" + PartnerRow.name_DbName + "], " +
				"[" + PartnerRow.status_DbName + "], " +
				"[" + PartnerRow.contact_info_id_DbName + "], " +
				"[" + PartnerRow.billing_schedule_id_DbName + "], " +
				"[" + PartnerRow.virtual_switch_id_DbName + "] " +
				") VALUES (" +
				base.Database.CreateSqlParameterName(PartnerRow.partner_id_PropName) + ", " +
				base.Database.CreateSqlParameterName(PartnerRow.name_PropName) + ", " +
				base.Database.CreateSqlParameterName(PartnerRow.status_PropName) + ", " +
				base.Database.CreateSqlParameterName(PartnerRow.contact_info_id_PropName) + ", " +
				base.Database.CreateSqlParameterName(PartnerRow.billing_schedule_id_PropName) + ", " +
        base.Database.CreateSqlParameterName(PartnerRow.virtual_switch_id_PropName) + ") " +

				"SELECT " + base.Database.CreateSqlParameterName(PartnerRow.partner_id_PropName) + " ";
			
			IDbCommand cmd = base.Database.CreateCommand(sqlStr);
			AddParameter(cmd, PartnerRow.name_PropName, value.Name);
			AddParameter(cmd, PartnerRow.status_PropName, value.Status);
			AddParameter(cmd, PartnerRow.contact_info_id_PropName, value.Contact_info_id);
			AddParameter(cmd, PartnerRow.billing_schedule_id_PropName,
				value.IsBilling_schedule_idNull ? DBNull.Value : (object)value.Billing_schedule_id);
      AddParameter(cmd, PartnerRow.virtual_switch_id_PropName, value.Virtual_switch_id);

			value.Partner_id = (int)cmd.ExecuteScalar();
		}

		protected override IDbCommand CreateGetAllCommand() {
			return CreateGetCommand(null, PartnerRow.name_DbName);
		}

  } // End of PartnerCollection class
} // End of namespace

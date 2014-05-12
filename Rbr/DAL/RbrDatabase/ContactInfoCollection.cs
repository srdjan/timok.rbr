// <fileinfo name="ContactInfoCollection.cs">
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

namespace Timok.Rbr.DAL.RbrDatabase {
	/// <summary>
	/// Represents the <c>ContactInfo</c> table.
	/// </summary>
	public class ContactInfoCollection : ContactInfoCollection_Base {
		/// <summary>
		/// Initializes a new instance of the <see cref="ContactInfoCollection"/> class.
		/// </summary>
		/// <param name="db">The database object.</param>
		internal ContactInfoCollection(Rbr_Db db)
			: base(db) {
			// EMPTY
		}

		public static ContactInfoRow Parse(System.Data.DataRow row){
			return new ContactInfoCollection(null).MapRow(row);
		}

		public override void Insert(ContactInfoRow value) {
			string _sqlStr = "DECLARE " + 
				base.Database.CreateSqlParameterName(ContactInfoRow.contact_info_id_PropName) + " int " + 
				"SET " + 
				base.Database.CreateSqlParameterName(ContactInfoRow.contact_info_id_PropName) +
        " = COALESCE((SELECT MAX(" + ContactInfoRow.contact_info_id_DbName + ") FROM ContactInfo WHERE " + ContactInfoRow.contact_info_id_DbName + " > 0) + 1, 1) " + 
			
				"INSERT INTO [dbo].[ContactInfo] (" +
				"[" + ContactInfoRow.contact_info_id_DbName + "], " +
				"[" + ContactInfoRow.address1_DbName + "], " +
				"[" + ContactInfoRow.address2_DbName + "], " +
				"[" + ContactInfoRow.city_DbName + "], " +
				"[" + ContactInfoRow.state_DbName + "], " +
				"[" + ContactInfoRow.zip_code_DbName + "], " +
				"[" + ContactInfoRow.email_DbName + "], " +
				"[" + ContactInfoRow.home_phone_number_DbName + "], " +
				"[" + ContactInfoRow.cell_phone_number_DbName + "], " +
				"[" + ContactInfoRow.work_phone_number_DbName + "]" +
				") VALUES (" +
				Database.CreateSqlParameterName(ContactInfoRow.contact_info_id_PropName) + ", " +
				Database.CreateSqlParameterName(ContactInfoRow.address1_PropName) + ", " +
				Database.CreateSqlParameterName(ContactInfoRow.address2_PropName) + ", " +
				Database.CreateSqlParameterName(ContactInfoRow.city_PropName) + ", " +
				Database.CreateSqlParameterName(ContactInfoRow.state_PropName) + ", " +
				Database.CreateSqlParameterName(ContactInfoRow.zip_code_PropName) + ", " +
				Database.CreateSqlParameterName(ContactInfoRow.email_PropName) + ", " +
				Database.CreateSqlParameterName(ContactInfoRow.home_phone_number_PropName) + ", " +
				Database.CreateSqlParameterName(ContactInfoRow.cell_phone_number_PropName) + ", " +
				Database.CreateSqlParameterName(ContactInfoRow.work_phone_number_PropName) + ") " +

				"SELECT " + base.Database.CreateSqlParameterName(ContactInfoRow.contact_info_id_PropName) + " ";


			IDbCommand _cmd = Database.CreateCommand(_sqlStr);
			//AddParameter(_cmd, "Contact_info_id", value.Contact_info_id);
			AddParameter(_cmd, ContactInfoRow.address1_PropName, value.Address1);
			AddParameter(_cmd, ContactInfoRow.address2_PropName, value.Address2);
			AddParameter(_cmd, ContactInfoRow.city_PropName, value.City);
			AddParameter(_cmd, ContactInfoRow.state_PropName, value.State);
			AddParameter(_cmd, ContactInfoRow.zip_code_PropName, value.Zip_code);
			AddParameter(_cmd, ContactInfoRow.email_PropName, value.Email);
			AddParameter(_cmd, ContactInfoRow.home_phone_number_PropName, value.Home_phone_number);
			AddParameter(_cmd, ContactInfoRow.cell_phone_number_PropName, value.Cell_phone_number);
			AddParameter(_cmd, ContactInfoRow.work_phone_number_PropName, value.Work_phone_number);

			value.Contact_info_id = (int) _cmd.ExecuteScalar();
		}

	} // End of ContactInfoCollection class
} // End of namespace

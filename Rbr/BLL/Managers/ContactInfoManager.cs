using Timok.Rbr.DTO;
using Timok.Rbr.DAL.RbrDatabase;

namespace Timok.Rbr.BLL.Managers {
	internal class ContactInfoManager {
		private ContactInfoManager() { }

		#region Internals
				
		#region Getters

		internal static ContactInfoDto Get(Rbr_Db pDb, int pContactInfoId) {
			ContactInfoRow _contactInfoRow = pDb.ContactInfoCollection.GetByPrimaryKey(pContactInfoId);
			return MapToContactInfo(_contactInfoRow);
		}
		
		#endregion Getters

		#region Actions

		internal static void Add(Rbr_Db pDb, ContactInfoDto pContactInfo) {
			ContactInfoRow _contactInfoRow = MapToContactInfoRow(pContactInfo);
			pDb.ContactInfoCollection.Insert(_contactInfoRow);
			pContactInfo.ContactInfoId = _contactInfoRow.Contact_info_id;
		}

		internal static void Update(Rbr_Db pDb, ContactInfoDto pContactInfo) {
			ContactInfoRow _contactInfoRow = MapToContactInfoRow(pContactInfo);
			pDb.ContactInfoCollection.Update(_contactInfoRow);
		}

		internal static void Delete(Rbr_Db pDb, ContactInfoDto pContactInfo) {
			pDb.ContactInfoCollection.DeleteByPrimaryKey(pContactInfo.ContactInfoId);
		}
		#endregion Actions

		#region mappings

		#region To DAL mappings

		static internal ContactInfoRow MapToContactInfoRow(ContactInfoDto pContactInfo) {
			if (pContactInfo == null) {
				return null;
			}
			ContactInfoRow _contactInfoRow = new ContactInfoRow();
			_contactInfoRow.Contact_info_id = pContactInfo.ContactInfoId;
			_contactInfoRow.Address1 = pContactInfo.Address1;
			_contactInfoRow.Address2 = pContactInfo.Address2;
			_contactInfoRow.City = pContactInfo.City;
			_contactInfoRow.State = pContactInfo.State;
			_contactInfoRow.Zip_code = pContactInfo.Zip;
			_contactInfoRow.Email = pContactInfo.Email;
			_contactInfoRow.Home_phone_number = pContactInfo.HomePhone;
			_contactInfoRow.Cell_phone_number = pContactInfo.CellPhone;
			_contactInfoRow.Work_phone_number = pContactInfo.WorkPhone;

			return _contactInfoRow;
		}
		#endregion To DAL mappings

		#region To BLL mappings

		static internal ContactInfoDto MapToContactInfo(ContactInfoRow pContactInfoRow) {
			if (pContactInfoRow == null) {
				return null;
			}
			ContactInfoDto _contactInfo = new ContactInfoDto();
			_contactInfo.ContactInfoId = pContactInfoRow.Contact_info_id;
			_contactInfo.Address1 = pContactInfoRow.Address1;
			_contactInfo.Address2 = pContactInfoRow.Address2;
			_contactInfo.City = pContactInfoRow.City;
			_contactInfo.State = pContactInfoRow.State;
			_contactInfo.Zip = pContactInfoRow.Zip_code;
			_contactInfo.Email = pContactInfoRow.Email;
			_contactInfo.HomePhone = pContactInfoRow.Home_phone_number;
			_contactInfo.CellPhone = pContactInfoRow.Cell_phone_number;
			_contactInfo.WorkPhone = pContactInfoRow.Work_phone_number;

			return _contactInfo;
		}

		#endregion To BLL mappings
		
		#endregion mappings

		#endregion Internals

		#region privates
		#endregion privates
	}
}
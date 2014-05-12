using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using Timok.Core;
using Timok.Core.DataProtection;
using Timok.Rbr.Core.Config;
using Timok.Rbr.DAL;
using Timok.Rbr.DAL.RbrDatabase;
using Timok.Rbr.DTO;

namespace Timok.Rbr.BLL.Managers {
	internal class PersonManager {
		PersonManager() { }

		#region Getters

		internal static PersonDto Get(Rbr_Db pDb, int pPersonId) {
			PersonRow _personRow = pDb.PersonCollection.GetByPrimaryKey(pPersonId);
			ContactInfoDto _contactInfo = null;
			CustomerSupportGroupDto _customerSupportGroup = null;
			VirtualSwitchDto _virtualSwitch = null;
			if (!_personRow.IsContact_info_idNull) {
				_contactInfo = ContactInfoManager.Get(pDb, _personRow.Contact_info_id);
			}
			if (!_personRow.IsGroup_idNull) {
				_customerSupportGroup = CustomerSupportManager.GetCustomerSupportGroup(pDb, _personRow.Group_id);
			}
			if (!_personRow.IsVirtual_switch_idNull) {
				_virtualSwitch = VirtualSwitchManager.Get(pDb, _personRow.Virtual_switch_id);
			}

			return mapToPerson(_personRow, _contactInfo, _customerSupportGroup, _virtualSwitch);
		}

		internal static PersonDto[] GetByPartnerId(Rbr_Db pDb, int pPartnerId) {
			var _list = new List<PersonDto>();
			PersonRow[] _personRows = pDb.PersonCollection.GetByPartner_id(pPartnerId);
			foreach (PersonRow _personRow in _personRows) {
				ContactInfoDto _contactInfo = ContactInfoManager.Get(pDb, _personRow.Contact_info_id);
				_list.Add(mapToPerson(_personRow, _contactInfo, null, null));
			}
			return _list.ToArray();
		}

		public static PersonDto[] GetResellAgents(Rbr_Db pDb, int pPartnerId) {
			var _list = new List<PersonDto>();
			PersonRow[] _personRows = pDb.PersonCollection.GetResellAgents(pPartnerId);
			foreach (PersonRow _personRow in _personRows) {
				ContactInfoDto _contactInfo = ContactInfoManager.Get(pDb, _personRow.Contact_info_id);
				_list.Add(mapToPerson(_personRow, _contactInfo, null, null));
			}
			return _list.ToArray();
		}

		internal static PersonDto[] GetResellAgents(Rbr_Db pDb) {
			var _list = new List<PersonDto>();
			PersonRow[] _personRows = pDb.PersonCollection.GetResellAgents();
			foreach (PersonRow _personRow in _personRows) {
				ContactInfoDto _contactInfo = ContactInfoManager.Get(pDb, _personRow.Contact_info_id);
				_list.Add(mapToPerson(_personRow, _contactInfo, null, null));
			}
			return _list.ToArray();
		}

		internal static PersonDto[] GetActiveResellAgents(Rbr_Db pDb, int pPartnerId) {
			var _list = new List<PersonDto>();
			PersonRow[] _personRows = pDb.PersonCollection.GetActiveResellAgents(pPartnerId);
			foreach (PersonRow _personRow in _personRows) {
				ContactInfoDto _contactInfo = ContactInfoManager.Get(pDb, _personRow.Contact_info_id);
				_list.Add(mapToPerson(_personRow, _contactInfo, null, null));
			}
			return _list.ToArray();
		}

		internal static PersonDto GetByRetailAcctId(Rbr_Db pDb, int pRetailAcctId) {
			PersonRow[] _personRows = pDb.PersonCollection.GetByRetail_acct_id(pRetailAcctId);
			if (_personRows != null && _personRows.Length > 0) {
				PersonRow _personRow = _personRows[0];
				ContactInfoDto _contactInfo = null;
				if (!_personRow.IsContact_info_idNull) {
					_contactInfo = ContactInfoManager.Get(pDb, _personRow.Contact_info_id);
				}
				return mapToPerson(_personRows[0], _contactInfo, null, null);
			}
			return null;
		}

		internal static PersonDto[] GetByGroupId(Rbr_Db pDb, int pGroupId) {
			var _list = new ArrayList();
			CustomerSupportGroupDto _customerSupportGroup = CustomerSupportManager.GetCustomerSupportGroup(pDb, pGroupId);
			PersonRow[] _personRows = pDb.PersonCollection.GetByGroup_id(pGroupId);
			foreach (PersonRow _personRow in _personRows) {
				ContactInfoDto _contactInfo = null;
				if (!_personRow.IsContact_info_idNull) {
					_contactInfo = ContactInfoManager.Get(pDb, _personRow.Contact_info_id);
				}
				_list.Add(mapToPerson(_personRow, _contactInfo, _customerSupportGroup, null));
			}
			if (_list.Count > 1) {
				_list.Sort(new GenericComparer(PersonDto.Name_PropName, ListSortDirection.Ascending));
			}
			return (PersonDto[]) _list.ToArray(typeof (PersonDto));
		}

		internal static PersonDto[] GetByVirtualSwitchId(Rbr_Db pDb, int pVirtualSwitchId) {
			var _list = new ArrayList();
			VirtualSwitchDto _virtualSwitch = VirtualSwitchManager.Get(pDb, pVirtualSwitchId);
			PersonRow[] _personRows = pDb.PersonCollection.GetByVirtual_switch_id(pVirtualSwitchId);
			foreach (PersonRow _personRow in _personRows) {
				ContactInfoDto _contactInfo = null;
				if (!_personRow.IsContact_info_idNull) {
					_contactInfo = ContactInfoManager.Get(pDb, _personRow.Contact_info_id);
				}
				_list.Add(mapToPerson(_personRow, _contactInfo, null, _virtualSwitch));
			}
			if (_list.Count > 1) {
				_list.Sort(new GenericComparer(PersonDto.Name_PropName, ListSortDirection.Ascending));
			}
			return (PersonDto[]) _list.ToArray(typeof (PersonDto));
		}

		internal static PersonDto GetByLogin(Rbr_Db pDb, string pLogin) {
			PersonRow _personRow = pDb.PersonCollection.GetByLogin(pLogin);
			if (_personRow == null) {
				return null;
			}

			ContactInfoDto _contactInfo = null;
			CustomerSupportGroupDto _customerSupportGroup = null;
			VirtualSwitchDto _virtualSwitch = null;
			if (! _personRow.IsContact_info_idNull) {
				_contactInfo = ContactInfoManager.Get(pDb, _personRow.Contact_info_id);
			}
			if (! _personRow.IsGroup_idNull) {
				_customerSupportGroup = CustomerSupportManager.GetCustomerSupportGroup(pDb, _personRow.Group_id);
			}
			if (! _personRow.IsVirtual_switch_idNull) {
				_virtualSwitch = VirtualSwitchManager.Get(pDb, _personRow.Virtual_switch_id);
			}
			return mapToPerson(_personRow, _contactInfo, _customerSupportGroup, _virtualSwitch);
		}

		#endregion Getters

		#region Actions

		internal static void Save(Rbr_Db pDb, string pSalt, PersonDto pPerson) {
			bool _isNew = pPerson.PersonId == 0;

			try {
				PersonRow _personRow;
				ContactInfoRow _contactInfoRow;
				mapToPersonRow(pPerson, out _personRow, out _contactInfoRow);

				if (_personRow != null) {
					PersonRow _existingPersonRow = pDb.PersonCollection.GetByPrimaryKey(_personRow.Person_id);
					if (_existingPersonRow != null) {
						if (_existingPersonRow.Password != _personRow.Password) {
							//-- At this point the PWD should be in a clear form, rewrite it with Hashed value
							SaltHashedPwd _sh = SaltHashedPwd.FromClearPwd(_personRow.Password, _personRow.Salt);
							_personRow.Password = _sh.Value;
							pPerson.Password = _personRow.Password;
						}
						if (_contactInfoRow != null) {
							if (_contactInfoRow.Contact_info_id == 0) {
								pDb.ContactInfoCollection.Insert(_contactInfoRow);
								_personRow.Contact_info_id = _contactInfoRow.Contact_info_id;
							}
							else {
								pDb.ContactInfoCollection.Update(_contactInfoRow);
							}
						}
						pDb.PersonCollection.Update(_personRow);
					}
					else {
						pDb.ContactInfoCollection.Insert(_contactInfoRow);
						_personRow.Contact_info_id = _contactInfoRow.Contact_info_id;

						_personRow.Salt = pSalt;
						SaltHashedPwd _sh = SaltHashedPwd.FromClearPwd(_personRow.Password, _personRow.Salt);
						_personRow.Password = _sh.Value;
						pPerson.Password = _personRow.Password;
						pDb.PersonCollection.Insert(_personRow);
						pPerson.PersonId = _personRow.Person_id;
					}
				}
			}
			catch (AlternateKeyException) {
				if (_isNew) {
					pPerson.PersonId = 0; //reset it in case of err
				}
				throw new LoginNameAlreadyInUseException();
			}
		}

		internal static void Delete(Rbr_Db pDb, int pPersonId) {
			if (hasPayments(pDb, pPersonId)) {
				//NO DELETE, archive it
				pDb.PersonCollection.UpdateStatus(pPersonId, Status.Archived);
			}
			else {
				pDb.PersonCollection.DeleteByPrimaryKey(pPersonId);
			}
		}

		internal static void DeleteByPartnerId(Rbr_Db pDb, int pPartnerId) { pDb.PersonCollection.DeleteByPartner_id(pPartnerId); }

		internal static void DeleteByRetailAcctId(Rbr_Db pDb, int pRetailAcctId) { pDb.PersonCollection.DeleteByRetail_acct_id(pRetailAcctId); }

		#endregion Actions

		//----------------------------------- Privates ----------------------------------------------------------
		static void mapToPersonRow(PersonDto pPerson, out PersonRow pPersonRow, out ContactInfoRow pContactInfoRow) {
			pPersonRow = null;
			pContactInfoRow = null;
			if (pPerson == null) {
				return;
			}

			pPersonRow = new PersonRow
			             	{
			             		Person_id = pPerson.PersonId, 
											Name = pPerson.Name, 
											Login = pPerson.Login, 
											Password = pPerson.Password, 
											AccessScope = pPerson.AccessScope, 
											PermissionType = pPerson.PermissionType, 
											RegistrationStatus = pPerson.RegistrationStatus, 
											PersonStatus = pPerson.Status, 
											Salt = pPerson.Salt
			             	};

			if (pPerson.VirtualSwitch != null) {
				pPersonRow.Virtual_switch_id = pPerson.VirtualSwitchId;
			}

			if (pPerson.PartnerId != 0) {
				pPersonRow.Partner_id = pPerson.PartnerId;
			}
			if (pPerson.RetailAcctId != 0) {
				pPersonRow.Retail_acct_id = pPerson.RetailAcctId;
			}
			if (pPerson.GroupId != 0) {
				pPersonRow.Group_id = pPerson.GroupId;
			}
			if (pPerson.ContactInfo != null) {
				pPersonRow.Contact_info_id = pPerson.ContactInfoId;
				pContactInfoRow = new ContactInfoRow();
				pContactInfoRow.Contact_info_id = pPerson.ContactInfo.ContactInfoId;
				pContactInfoRow.Address1 = pPerson.ContactInfo.Address1;
				pContactInfoRow.Address2 = pPerson.ContactInfo.Address2;
				pContactInfoRow.City = pPerson.ContactInfo.City;
				pContactInfoRow.State = pPerson.ContactInfo.State;
				pContactInfoRow.Zip_code = pPerson.ContactInfo.Zip;
				pContactInfoRow.Email = pPerson.ContactInfo.Email;
				pContactInfoRow.Work_phone_number = pPerson.ContactInfo.WorkPhone;
				pContactInfoRow.Home_phone_number = pPerson.ContactInfo.HomePhone;
				pContactInfoRow.Cell_phone_number = pPerson.ContactInfo.CellPhone;
			}
		}

		static PersonDto mapToPerson(PersonRow pPersonRow, ContactInfoDto pContactInfo, CustomerSupportGroupDto pCustomerSupportGroup, VirtualSwitchDto pVirtualSwitch) {
			if (pPersonRow == null) {
				return null;
			}
			var _person = new PersonDto();
			_person.PersonId = pPersonRow.Person_id;
			_person.Name = pPersonRow.Name;
			_person.Login = pPersonRow.Login;
			_person.Password = pPersonRow.Password;
			_person.AccessScope = pPersonRow.AccessScope;
			_person.PermissionType = pPersonRow.PermissionType;
			_person.RegistrationStatus = pPersonRow.RegistrationStatus;
			_person.Status = pPersonRow.PersonStatus;
			_person.Salt = pPersonRow.Salt;

			if (! pPersonRow.IsPartner_idNull) {
				_person.PartnerId = pPersonRow.Partner_id;
			}
			if (! pPersonRow.IsRetail_acct_idNull) {
				_person.RetailAcctId = pPersonRow.Retail_acct_id;
			}
			if (pCustomerSupportGroup != null) {
				_person.CustomerSupportGroup = pCustomerSupportGroup;
			}
			if (pVirtualSwitch != null) {
				_person.VirtualSwitch = pVirtualSwitch;
			}
			if (pContactInfo != null) {
				_person.ContactInfo = pContactInfo;
			}

			return _person;
		}

		#region privates

		static bool hasPayments(Rbr_Db pDb, int pPersonId) {
			if (CustomerAcctManager.HasPayments(pDb, pPersonId)) {
				return true;
			}
			if (RetailAccountManager.HasPayments(pDb, pPersonId)) {
				return true;
			}
			return false;
		}

		#endregion privates
	}
}
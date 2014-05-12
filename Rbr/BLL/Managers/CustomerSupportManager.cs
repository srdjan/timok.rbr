using System;
using System.Collections;
using System.Collections.Generic;
using Timok.Core;
using Timok.Rbr.BLL.Managers;
using Timok.Rbr.DAL;
using Timok.Rbr.DAL.RbrDatabase;
using Timok.Rbr.DTO;

namespace Timok.Rbr.BLL.Managers {
	internal class CustomerSupportManager {
		private CustomerSupportManager() { }

		#region Internals

		#region Getters

		internal static CustomerSupportVendorDto GetCustomerSupportVendor(Rbr_Db pDb, int pVendorId) {
			CustomerSupportVendorRow _customerSupportVendorRow = pDb.CustomerSupportVendorCollection.GetByPrimaryKey(pVendorId);
			ContactInfoDto _contactInfo = ContactInfoManager.Get(pDb, _customerSupportVendorRow.Contact_info_id);
			return mapToCustomerSupportVendor(_customerSupportVendorRow, _contactInfo);
		}

		internal static CustomerSupportVendorDto[] GetAllCustomerSupportVendors(Rbr_Db pDb) {
			List<CustomerSupportVendorDto> _list = new List<CustomerSupportVendorDto>();

			CustomerSupportVendorRow[] _customerSupportVendorRows = pDb.CustomerSupportVendorCollection.GetAll();
			foreach (CustomerSupportVendorRow _customerSupportVendorRow in _customerSupportVendorRows) {
				ContactInfoDto _contactInfo = ContactInfoManager.Get(pDb, _customerSupportVendorRow.Contact_info_id);
				_list.Add(mapToCustomerSupportVendor(_customerSupportVendorRow, _contactInfo));
			}
			return _list.ToArray();
		}

		internal static CustomerAcctSupportMapDto GetCustomerAcctSupportMap(Rbr_Db pDb, short pCustomerAcctId, int pVendorId) {
			CustomerAcctSupportMapRow _customerAcctSupportMapRow = pDb.CustomerAcctSupportMapCollection.GetByPrimaryKey(pCustomerAcctId, pVendorId);
			CustomerAcctDto _customerAcct = CustomerAcctManager.GetAcct(pDb, pCustomerAcctId);
			return mapToCustomerAcctSupportMap(_customerAcctSupportMapRow, _customerAcct);
		}

		internal static CustomerAcctSupportMapDto[] GetAllCustomerAcctSupportMaps(Rbr_Db pDb, int pVendorId) {
			ArrayList _list = new ArrayList();

			//get mapped
			CustomerAcctSupportMapRow[] _customerAcctSupportMapRows = pDb.CustomerAcctSupportMapCollection.GetByVendor_id(pVendorId);
			foreach (CustomerAcctSupportMapRow _customerAcctSupportMapRow in _customerAcctSupportMapRows) {
				CustomerAcctDto _customerAcct = CustomerAcctManager.GetAcct(pDb, _customerAcctSupportMapRow.Customer_acct_id);
				_list.Add(mapToCustomerAcctSupportMap(_customerAcctSupportMapRow, _customerAcct));
			}

			//get unmapped too, as not Assigned
			CustomerAcctDto[] _unmappedCustomerAccts = CustomerAcctManager.GetUnmapped(pDb, pVendorId);
			foreach (CustomerAcctDto _unmappedCustomerAcct in _unmappedCustomerAccts) {
				_list.Add(mapToCustomerAcctSupportMap(null, _unmappedCustomerAcct));
			}

			if (_list.Count > 1) {
				//TODO: should we sort it by Assigned as well???
				_list.Sort(new GenericComparer(CustomerAcctSupportMapDto.CustomerAcctName_PropName, System.ComponentModel.ListSortDirection.Ascending));
			}
			return (CustomerAcctSupportMapDto[])_list.ToArray(typeof(CustomerAcctSupportMapDto));
		}

		//public static CustomerAcctSupportMap Get(short pCustomerAcctId, int pVendorId) {
		//  using (Rbr_Db _db = new Rbr_Db()) {
		//    return Get(_db, pCustomerAcctId, pVendorId);
		//  }
		//}

		//public static CustomerAcctSupportMap[] GetAll(int pVendorId) {
		//  using (Rbr_Db _db = new Rbr_Db()) {
		//    return GetAll(_db, pVendorId);
		//  }
		//}

		internal static CustomerSupportGroupDto GetCustomerSupportGroup(Rbr_Db pDb, int pCustomerSupportGroupId) {
			CustomerSupportGroupRow _customerSupportGroupRow = pDb.CustomerSupportGroupCollection.GetByPrimaryKey(pCustomerSupportGroupId);
			return mapToCustomerSupportGroup(_customerSupportGroupRow);
		}

		internal static CustomerSupportGroupDto[] GetByVendorIdCustomerSupportGroups(Rbr_Db pDb, int pVendorId) {
			CustomerSupportGroupRow[] _customerSupportGroupRows = pDb.CustomerSupportGroupCollection.GetByVendor_id(pVendorId);
			return mapToCustomerSupportGroups(_customerSupportGroupRows);
		}

		#endregion Getters

		#region Actions

		internal static void AddCustomerSupportMap(Rbr_Db pDb, CustomerAcctSupportMapDto pCustomerAcctSupportMap) {
			CustomerAcctSupportMapRow _customerAcctSupportMapRow = mapToCustomerAcctSupportMapRow(pCustomerAcctSupportMap);
			if (_customerAcctSupportMapRow != null) {
				pDb.CustomerAcctSupportMapCollection.Insert(_customerAcctSupportMapRow);
			}
		}

		internal static void DeleteCustomerSupporMap(Rbr_Db pDb, short pCustomerAcctId, int pVendorId) {
			pDb.CustomerAcctSupportMapCollection.DeleteByPrimaryKey(pCustomerAcctId, pVendorId);
		}
		
		internal static void SaveCustomerSupportVendor(Rbr_Db pDb, CustomerSupportVendorDto pCustomerSupportVendor) {
			CustomerSupportVendorRow _customerSupportVendorRow = mapToCustomerSupportVendorRow(pCustomerSupportVendor);
			if (_customerSupportVendorRow != null) {
				if (_customerSupportVendorRow.Vendor_id == 0) {
					pDb.CustomerSupportVendorCollection.Insert(_customerSupportVendorRow);
					pCustomerSupportVendor.VendorId = _customerSupportVendorRow.Vendor_id;
				}
				else {
					pDb.CustomerSupportVendorCollection.Update(_customerSupportVendorRow);
				}
			}
		}

		internal static void DeleteCustomerSupportVendor(Rbr_Db pDb, int pVendorId) {
			CustomerSupportVendorRow _customerSupportVendorRow = pDb.CustomerSupportVendorCollection.GetByPrimaryKey(pVendorId);
			pDb.CustomerSupportVendorCollection.DeleteByPrimaryKey(pVendorId);
			pDb.ContactInfoCollection.GetByPrimaryKey(_customerSupportVendorRow.Contact_info_id);
		}

		internal static void AddCustomerSupportVendor(Rbr_Db pDb, CustomerAcctSupportMapDto pCustomerAcctSupportMap) {
			CustomerAcctSupportMapRow _customerAcctSupportMapRow = mapToCustomerAcctSupportMapRow(pCustomerAcctSupportMap);
			if (_customerAcctSupportMapRow != null) {
				pDb.CustomerAcctSupportMapCollection.Insert(_customerAcctSupportMapRow);
			}
		}

		internal static void DeleteCustomerSupportMap(Rbr_Db pDb, short pCustomerAcctId, int pVendorId) {
			pDb.CustomerAcctSupportMapCollection.DeleteByPrimaryKey(pCustomerAcctId, pVendorId);
		}

		internal static void SaveCustomerSupportGroup(Rbr_Db pDb, CustomerSupportGroupDto pCustomerSupportGroup) {
			try {
				CustomerSupportGroupRow _customerSupportGroupRow = mapToCustomerSupportGroupRow(pCustomerSupportGroup);
				if (_customerSupportGroupRow != null) {
					if (_customerSupportGroupRow.Group_id == 0) {
						pDb.CustomerSupportGroupCollection.Insert(_customerSupportGroupRow);
						pCustomerSupportGroup.GroupId = _customerSupportGroupRow.Group_id;
					}
					else {
						pDb.CustomerSupportGroupCollection.Update(_customerSupportGroupRow);
					}
				}
			}
			catch (AlternateKeyException) {
				throw new LoginNameAlreadyInUseException();
			}
		}

		internal static void DeleteCustomerSupportGroup(Rbr_Db pDb, int pGroupId) {
			CustomerSupportGroupRow _customerSupportGroupRow = pDb.CustomerSupportGroupCollection.GetByPrimaryKey(pGroupId);

			PersonRow[] _personRows = pDb.PersonCollection.GetByGroup_id(pGroupId);
			foreach (PersonRow _personRow in _personRows) {
				if (RetailAccountManager.HasPayments(pDb, _personRow.Person_id)) {
					throw new InvalidOperationException("Cannot Delete Group [" + _customerSupportGroupRow.Description + "]; At least one Representative [Login=" + _personRow.Login + "] in this Group has a Payment History.");
				}
			}
			pDb.PersonCollection.DeleteByGroup_id(pGroupId);
			pDb.CustomerSupportGroupCollection.DeleteByPrimaryKey(pGroupId);
		}

		#endregion Actions

		static CustomerSupportVendorRow mapToCustomerSupportVendorRow(CustomerSupportVendorDto pCustomerSupportVendor) {
			if (pCustomerSupportVendor == null) {
				return null;
			}
			CustomerSupportVendorRow _customerSupportVendorRow = new CustomerSupportVendorRow();
			_customerSupportVendorRow.Vendor_id = pCustomerSupportVendor.VendorId;
			_customerSupportVendorRow.Name = pCustomerSupportVendor.Name;
			_customerSupportVendorRow.Contact_info_id = pCustomerSupportVendor.ContactInfo.ContactInfoId;

			return _customerSupportVendorRow;
		}

		static CustomerSupportVendorDto mapToCustomerSupportVendor(CustomerSupportVendorRow pCustomerSupportVendorRow, ContactInfoDto pContactInfo) {
			if (pCustomerSupportVendorRow == null) {
				return null;
			}
			CustomerSupportVendorDto _customerSupportVendor = new CustomerSupportVendorDto();
			_customerSupportVendor.VendorId = pCustomerSupportVendorRow.Vendor_id;
			_customerSupportVendor.Name = pCustomerSupportVendorRow.Name;
			_customerSupportVendor.ContactInfo = pContactInfo;

			return _customerSupportVendor;
		}

		#endregion Internals

		//----------------------------------- Privates ----------------------------------------------------------
		#region privates

		static CustomerAcctSupportMapRow mapToCustomerAcctSupportMapRow(CustomerAcctSupportMapDto pCustomerAcctSupportMap) {
			if (pCustomerAcctSupportMap == null) {
				return null;
			}
			CustomerAcctSupportMapRow _customerAcctSupportMapRow = new CustomerAcctSupportMapRow();
			_customerAcctSupportMapRow.Customer_acct_id = pCustomerAcctSupportMap.CustomerAcctId;
			_customerAcctSupportMapRow.Vendor_id = pCustomerAcctSupportMap.VendorId;

			return _customerAcctSupportMapRow;
		}

		static CustomerAcctSupportMapDto mapToCustomerAcctSupportMap(CustomerAcctSupportMapRow pCustomerAcctSupportMapRow, CustomerAcctDto pCustomerAcct) {
			if (pCustomerAcct == null) {
				return null;
			}
			CustomerAcctSupportMapDto _customerAcctSupportMap = new CustomerAcctSupportMapDto();
			_customerAcctSupportMap.CustomerAcctId = pCustomerAcct.CustomerAcctId;
			_customerAcctSupportMap.CustomerAcct = pCustomerAcct;
			if (pCustomerAcctSupportMapRow != null) {
				_customerAcctSupportMap.VendorId = pCustomerAcctSupportMapRow.Vendor_id;
			}

			_customerAcctSupportMap.Assigned = _customerAcctSupportMap.VendorId != 0;

			return _customerAcctSupportMap;
		}
		static internal CustomerSupportGroupRow mapToCustomerSupportGroupRow(CustomerSupportGroupDto pCustomerSupportGroup) {
			if (pCustomerSupportGroup == null) {
				return null;
			}
			CustomerSupportGroupRow _customerSupportGroupRow = new CustomerSupportGroupRow();
			_customerSupportGroupRow.Group_id = pCustomerSupportGroup.GroupId;
			_customerSupportGroupRow.Description = pCustomerSupportGroup.Description;
			_customerSupportGroupRow.GroupRole = pCustomerSupportGroup.GroupRole;
			_customerSupportGroupRow.Max_amount = pCustomerSupportGroup.MaxAmount;
			_customerSupportGroupRow.AllowStatusChange = pCustomerSupportGroup.AllowStatusChange;
			_customerSupportGroupRow.Vendor_id = pCustomerSupportGroup.VendorId;

			return _customerSupportGroupRow;
		}


		static internal CustomerSupportGroupDto[] mapToCustomerSupportGroups(CustomerSupportGroupRow[] pCustomerSupportGroupRows) {
			List<CustomerSupportGroupDto> _list = new List<CustomerSupportGroupDto>();
			if (pCustomerSupportGroupRows != null && pCustomerSupportGroupRows.Length > 0) {
				foreach (CustomerSupportGroupRow _customerSupportGroupRow in pCustomerSupportGroupRows) {
					_list.Add(mapToCustomerSupportGroup(_customerSupportGroupRow));
				}
			}
			return _list.ToArray();
		}

		static internal CustomerSupportGroupDto mapToCustomerSupportGroup(CustomerSupportGroupRow pCustomerSupportGroupRow) {
			if (pCustomerSupportGroupRow == null) {
				return null;
			}
			CustomerSupportGroupDto _customerSupportGroup = new CustomerSupportGroupDto();
			_customerSupportGroup.GroupId = pCustomerSupportGroupRow.Group_id;
			_customerSupportGroup.Description = pCustomerSupportGroupRow.Description;
			_customerSupportGroup.GroupRole = pCustomerSupportGroupRow.GroupRole;
			_customerSupportGroup.MaxAmount = pCustomerSupportGroupRow.Max_amount;
			_customerSupportGroup.AllowStatusChange = pCustomerSupportGroupRow.AllowStatusChange;
			_customerSupportGroup.VendorId = pCustomerSupportGroupRow.Vendor_id;

			return _customerSupportGroup;
		}
		#endregion privates
	}
}
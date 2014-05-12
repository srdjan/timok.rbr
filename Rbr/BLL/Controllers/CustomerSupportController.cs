using Timok.Rbr.BLL.Managers;
using Timok.Rbr.Core;
using Timok.Rbr.DAL.RbrDatabase;
using Timok.Rbr.DTO;

namespace Timok.Rbr.BLL.Controllers {
	public class CustomerSupportController {
		CustomerSupportController() {}

		public static short DefaultCustomerSupportGroupId {
			get { return CustomerSupportGroupRow.DefaultId; }
		}

		#region Public Getters

		public static CustomerSupportVendorDto GetCustomerSupportVendor(int pVendorId) {
			using (Rbr_Db _db = new Rbr_Db()) {
				return CustomerSupportManager.GetCustomerSupportVendor(_db, pVendorId);
			}
		}

		public static CustomerSupportVendorDto[] GetAllCustomerSupportVendors() {
			using (Rbr_Db _db = new Rbr_Db()) {
				return CustomerSupportManager.GetAllCustomerSupportVendors(_db);
			}
		}

		public static CustomerAcctSupportMapDto GetCustomerSupportMap(short pCustomerAcctId, int pVendorId) {
			using (Rbr_Db _db = new Rbr_Db()) {
				return CustomerSupportManager.GetCustomerAcctSupportMap(_db, pCustomerAcctId, pVendorId);
			}
		}

		public static CustomerAcctSupportMapDto[] GetAllCustomerSupportMaps(int pVendorId) {
			using (Rbr_Db _db = new Rbr_Db()) {
				return CustomerSupportManager.GetAllCustomerAcctSupportMaps(_db, pVendorId);
			}
		}

		public static CustomerSupportGroupDto Get(int pCustomerSupportGroupId) {
			using (Rbr_Db _db = new Rbr_Db()) {
				return CustomerSupportManager.GetCustomerSupportGroup(_db, pCustomerSupportGroupId);
			}
		}

		public static CustomerSupportGroupDto[] GetByVendorId(int pVendorId) {
			using (Rbr_Db _db = new Rbr_Db()) {
				return CustomerSupportManager.GetByVendorIdCustomerSupportGroups(_db, pVendorId);
			}
		}

		#endregion Public Getters

		#region Public Actions

		public static void Save(CustomerSupportVendorDto pCustomerSupportVendor) {
			using (Rbr_Db _db = new Rbr_Db()) {
				using (Transaction _tx = new Transaction(_db, pCustomerSupportVendor)) {
					//1. REQUIRED set Contact Info
					if (pCustomerSupportVendor.ContactInfo.ContactInfoId == 0) {
						ContactInfoManager.Add(_db, pCustomerSupportVendor.ContactInfo);
					}
					else {
						ContactInfoManager.Update(_db, pCustomerSupportVendor.ContactInfo);
					}

					CustomerSupportManager.SaveCustomerSupportVendor(_db, pCustomerSupportVendor);
					_tx.Commit();
				}
			}
		}

		public static void Delete(int pVendorId) {
			using (Rbr_Db _db = new Rbr_Db()) {
				using (Transaction _tx = new Transaction(_db, pVendorId)) {
					CustomerSupportManager.DeleteCustomerSupportVendor(_db, pVendorId);
					_tx.Commit();
				}
			}
		}

		public static void SaveCustomerSupportGroup(CustomerSupportGroupDto pCustomerSupportGroup) {
			using (Rbr_Db _db = new Rbr_Db()) {
				using (Transaction _tx = new Transaction(_db, pCustomerSupportGroup)) {
					CustomerSupportManager.SaveCustomerSupportGroup(_db, pCustomerSupportGroup);
					_tx.Commit();
				}
			}
		}

		public static void DeleteCustomerSupportGroup(int pCustomerSupportGroupId) {
			using (Rbr_Db _db = new Rbr_Db()) {
				using (Transaction _tx = new Transaction(_db, pCustomerSupportGroupId)) {
					CustomerSupportManager.DeleteCustomerSupportGroup(_db, pCustomerSupportGroupId);
					_tx.Commit();
				}
			}
		}

		public static void Add(CustomerAcctSupportMapDto pCustomerAcctSupportMap) {
			using (Rbr_Db _db = new Rbr_Db()) {
				using (Transaction _tx = new Transaction(_db, pCustomerAcctSupportMap)) {
					CustomerSupportManager.AddCustomerSupportMap(_db, pCustomerAcctSupportMap);
					_tx.Commit();
				}
			}
		}

		public static void Delete(short pCustomerAcctId, int pVendorId) {
			using (Rbr_Db _db = new Rbr_Db()) {
				using (Transaction _tx = new Transaction(_db, pCustomerAcctId, pVendorId)) {
					CustomerSupportManager.DeleteCustomerSupportMap(_db, pCustomerAcctId, pVendorId);
					_tx.Commit();
				}
			}
		}

		#endregion Public Actions
	}
}
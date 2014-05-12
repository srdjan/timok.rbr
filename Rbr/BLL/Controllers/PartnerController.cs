using System;
using Timok.Core.DataProtection;
using Timok.Logger;
using Timok.Rbr.BLL.Managers;
using Timok.Rbr.Core;
using Timok.Rbr.DTO;
using Timok.Rbr.DAL.RbrDatabase;

namespace Timok.Rbr.BLL.Controllers {
	public class PartnerController {
		PartnerController() {}

		#region Public Getters

		public static PartnerDto Get(int pPartnerId) {
			using (Rbr_Db _db = new Rbr_Db()) {
				return PartnerManager.Get(_db, pPartnerId);
			}
		}

		public static bool IsNameInUse(string pName, int pPartnerId) {
			using (Rbr_Db _db = new Rbr_Db()) {
				return PartnerManager.IsNameInUse(_db, pName, pPartnerId);
			}
		}

		public static PartnerDto[] GetAll() {
			using (Rbr_Db _db = new Rbr_Db()) {
				return PartnerManager.GetAll(_db);
			}
		}

		public static PartnerDto[] GetActiveResellers() {
			using (Rbr_Db _db = new Rbr_Db()) {
				return PartnerManager.GetActiveResellers(_db);
			}
		}

		#endregion Public Getters

		#region Public Actions

		/// <summary>
		/// IMPORTANT: !!! SALT MUST BE THE SAME ON ALL SERVERS !!!
		/// IMPORTANT: !!! SALT MUST BE SET ONLY ONCE !!!
		/// that's why it's set outside of the transaction, so we can replicate it to other servers
		/// </summary>
		/// <param name="pPartner"></param>
		public static void Save(PartnerDto pPartner) {
			//ControllerHelper.SetSalt(pPartner.Employees);
			string _salt = SaltHashedPwd.CreateRandomSalt();

			if (pPartner.PartnerId == 0) {
				Add(_salt, pPartner);
			}
			else {
				Update(_salt, pPartner);
			}
		}

		//NOTE: Use Save() when calling directly, it will generate correct salt.
		//NOTE: Add and Update should be used only by Replication on the remote side, so that they don't regenerate salt again !!!
		public static void Add(string pSalt, PartnerDto pPartner) {
			using (Rbr_Db _db = new Rbr_Db()) {
				using (Transaction _tx = new Transaction(_db, pSalt, pPartner)) {
					try {
						if (pPartner.ContactInfo.ContactInfoId == 0) {
							ContactInfoManager.Add(_db, pPartner.ContactInfo);
						}

						if (pPartner.BillingSchedule != null) {
							ScheduleManager.Save(_db, pPartner.BillingSchedule);
						}

						PartnerManager.Add(_db, pPartner);

						if (pPartner.Employees != null) {
							foreach (PersonDto _employee in pPartner.Employees) {
								_employee.PartnerId = pPartner.PartnerId;
								PersonManager.Save(_db, pSalt, _employee);
							}
						}
					}
					catch (Exception _ex) {
						TimokLogger.Instance.LogRbr(LogSeverity.Error, "PartnerController.add", string.Format("Exception:\r\n{0}", _ex));
						if (pPartner.ContactInfo != null) {
							pPartner.ContactInfo.ContactInfoId = 0;
						}
						if (pPartner.Employees != null) {
							foreach (PersonDto _employee in pPartner.Employees) {
								_employee.PersonId = 0;
							}
						}
						if (pPartner.BillingSchedule != null) {
							pPartner.BillingSchedule.ScheduleId = 0;
						}
						throw;
					}
					_tx.Commit();
				}
			}
		}

		//NOTE: Use Save() when calling directly, it will generate correct salt.
		//NOTE: Add and Update should be used only by Replication on the remote side, so that they don't regenerate salt again !!!
		public static void Update(string pSalt, PartnerDto pPartner) {
			using (Rbr_Db _db = new Rbr_Db()) {
				using (Transaction _tx = new Transaction(_db, pSalt, pPartner)) {
					PartnerDto _originalPartner = PartnerManager.Get(_db, pPartner.PartnerId);

					ContactInfoManager.Update(_db, pPartner.ContactInfo);

					//TODO: NEW DAL - handle Person deletion...
					if (pPartner.Employees != null) {
						foreach (PersonDto _employee in pPartner.Employees) {
							_employee.PartnerId = pPartner.PartnerId;
							PersonManager.Save(_db, pSalt, _employee);
						}
					}

					if (pPartner.BillingSchedule != null) {
						ScheduleManager.Save(_db, pPartner.BillingSchedule);
					}
					else if (_originalPartner.BillingSchedule != null) {
						ScheduleManager.Delete(_db, _originalPartner.BillingSchedule.ScheduleId);
					}

					PartnerManager.Update(_db, pPartner);

					_tx.Commit();
				}
			}
		}

		public static void Delete(PartnerDto pPartner) {
			using (Rbr_Db _db = new Rbr_Db()) {
				using (Transaction _tx = new Transaction(_db, pPartner)) {
					if (CustomerAcctManager.Exist(_db, pPartner.PartnerId)) {
						throw new ApplicationException("Partner has Customer Account(s).\r\nCannot delete.");
					}

					if (CarrierAcctManager.Exist(_db, pPartner.PartnerId)) {
						throw new ApplicationException("Partner has Carrier Account(s).\r\nCannot delete.");
					}

					//TODO: NEW DAL !!!
					//ResellAcctManager.DeleteByPartnerId(_db, pPartner.PartnerId);
					PersonManager.DeleteByPartnerId(_db, pPartner.PartnerId);
					PartnerManager.Delete(_db, pPartner);

					_tx.Commit();
				}
			}
		}

		#endregion Public Actions
	}
}
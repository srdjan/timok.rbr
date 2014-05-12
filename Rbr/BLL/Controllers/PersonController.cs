using System;
using Timok.Core.DataProtection;
using Timok.Logger;
using Timok.Rbr.BLL.Managers;
using Timok.Rbr.Core;
using Timok.Rbr.Core.Config;
using Timok.Rbr.DAL.RbrDatabase;
using Timok.Rbr.DTO;

namespace Timok.Rbr.BLL.Controllers {
	public class PersonController {
		PersonController() { }

		#region Public Getters

		public static PersonDto Authenticate(string pLogin, string pPassword) {
			using (var _db = new Rbr_Db()) {
				var _person = PersonManager.GetByLogin(_db, pLogin);
				if (_person == null) {
					return null;
				}

				var _shp = SaltHashedPwd.FromSaltHashedPwd(_person.Password, _person.Salt);
				if (! _shp.Verify(pPassword)) {
					TimokLogger.Instance.LogRbr(LogSeverity.Error, "PersonController.Authenticate", string.Format("Person password NOT valid!!! [Login: {0}] [Status: {1}]", _person.Login, _person.Status));
					return null;
				}

				//TODO: ??? is Status.InUse valid for login, or should we restrict it ???
				if (_person.Status == Status.Active || _person.Status == Status.InUse) {
					return _person;
				}
				TimokLogger.Instance.LogRbr(LogSeverity.Error, "PersonController.Authenticate", string.Format("Person Status IS NOT Active!!! [Login: {0}] [Status: {1}]", _person.Login, _person.Status));
				return null;
			}
		}

		public static PersonDto Get(int pPersonId) {
			using (var _db = new Rbr_Db()) {
				return PersonManager.Get(_db, pPersonId);
			}
		}

		public static PersonDto GetRbrSysAdmin() {
			using (var _db = new Rbr_Db()) {
				return PersonManager.Get(_db, PersonRow.DefaultId);
			}
		}

		public static PersonDto[] GetSystemAdmins() {
			using (var _db = new Rbr_Db()) {
				return PersonManager.GetByVirtualSwitchId(_db, AppConstants.DefaultVirtualSwitchId);
			}
		}

		public static PersonDto[] GetVirtualSwitchAdmins(int pVirtualSwitchId) {
			using (var _db = new Rbr_Db()) {
				return PersonManager.GetByVirtualSwitchId(_db, pVirtualSwitchId);
			}
		}

		public static PersonDto[] GetCustomerSupportReps(int pGroupId) {
			using (var _db = new Rbr_Db()) {
				return PersonManager.GetByGroupId(_db, pGroupId);
			}
		}

		public static PersonDto[] GetResellAgents(int pPartnerId) {
			using (var _db = new Rbr_Db()) {
				return PersonManager.GetResellAgents(_db, pPartnerId);
			}
		}

		public static PersonDto[] GetResellAgents() {
			using (var _db = new Rbr_Db()) {
				return PersonManager.GetResellAgents(_db);
			}
		}

		public static PersonDto[] GetActiveResellAgents(int pPartnerId) {
			using (var _db = new Rbr_Db()) {
				return PersonManager.GetActiveResellAgents(_db, pPartnerId);
			}
		}

		#endregion Public Getters

		#region Public Actions

		public static Result Save(PersonDto pPerson) {
			//IMPORTANT: !!! SALT MUST BE THE SAME ON ALL SERVERS, SALT MUST BE SET ONLY ONCE !!!
			//that's why it's set outside of the transaction, so we can replicate it to other servers
			string _salt = SaltHashedPwd.CreateRandomSalt();
			return SafeSave(pPerson, _salt);
		}

		public static Result SafeSave(PersonDto pPerson, string pSalt) {
			var _result = new Result();
			using (var _db = new Rbr_Db()) {
				using (var _tx = new Transaction(_db, pPerson, pSalt)) {
					try {
						if (pPerson.AccessScope == AccessScope.Switch && pPerson.VirtualSwitchId != AppConstants.DefaultVirtualSwitchId) {
							throw new Exception("Invalid state, DefaultVirtualSwitch not set");
						}
						if (pPerson.AccessScope == AccessScope.VirtualSwitch && pPerson.VirtualSwitchId <= 0) {
							throw new Exception("Invalid state, VirtualSwitch not set");
						}
						if (pPerson.AccessScope == AccessScope.Partner && pPerson.PartnerId == 0) {
							throw new Exception("Invalid state, Partner not set");
						}
						if (pPerson.AccessScope == AccessScope.ResellAgent && pPerson.PartnerId == 0) {
							throw new Exception("Invalid state, Partner not set");
						}
						if (pPerson.AccessScope == AccessScope.CustomerSupport && pPerson.GroupId == 0) {
							throw new Exception("Invalid state, Group not set");
						}
						if (pPerson.AccessScope == AccessScope.Consumer && pPerson.RetailAcctId == 0) {
							throw new Exception("Invalid state, RetailAccount not set");
						}
						if (pPerson.AccessScope == AccessScope.None) {
							throw new Exception("Invalid state, AccessScope not set");
						}

						//1. REQUIRED set Contact Info
						if (pPerson.ContactInfo != null) {
							ContactInfoManager.Add(_db, pPerson.ContactInfo);
						}

						PersonManager.Save(_db, pSalt, pPerson);
						_tx.Commit();
					}
					catch (Exception _ex) {
						//_db.RollbackTransaction();
						_result.Success = false;
						_result.ErrorMessage = _ex.Message;
						TimokLogger.Instance.LogRbr(LogSeverity.Error, "PersonController.SafeSave", string.Format("Exception:\r\n{0}", _ex));
					}
				}
			}
			return _result;
		}

		public static Result Delete(PersonDto pPerson) {
			var _result = new Result();
			using (var _db = new Rbr_Db()) {
				using (var _tx = new Transaction(_db, pPerson)) {
					try {
						if (pPerson.PersonId == PersonRow.DefaultId) {
							throw new Exception("Cannot delete Default System Admin");
						}

						if (pPerson.ContactInfo != null) {
							ContactInfoManager.Delete(_db, pPerson.ContactInfo);
						}

						PersonManager.Delete(_db, pPerson.PersonId);
						_tx.Commit();
					}
					catch (Exception _ex) {
						_result.Success = false;
						_result.ErrorMessage = _ex.Message;
						TimokLogger.Instance.LogRbr(LogSeverity.Error, "PersonController.Delete", string.Format("Exception:\r\n{0}", _ex));
					}
				}
			}
			return _result;
		}

		#endregion Public Actions
	}
}
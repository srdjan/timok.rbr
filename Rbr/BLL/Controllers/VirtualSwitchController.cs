using System;
using Timok.Logger;
using Timok.Rbr.BLL.Managers;
using Timok.Rbr.Core;
using Timok.Rbr.Core.Config;
using Timok.Rbr.DAL.RbrDatabase;
using Timok.Rbr.DTO;

namespace Timok.Rbr.BLL.Controllers {
	public class VirtualSwitchController {
		VirtualSwitchController() {}

		#region Public Getters

		public static VirtualSwitchDto Get(int pVirtualSwitchId) {
			using (var _db = new Rbr_Db()) {
				return VirtualSwitchManager.Get(_db, pVirtualSwitchId);
			}
		}

		public static VirtualSwitchDto GetDefault() {
			using (var _db = new Rbr_Db()) {
				return VirtualSwitchManager.Get(_db, AppConstants.DefaultVirtualSwitchId);
			}
		}

		public static VirtualSwitchDto[] GetAll() {
			using (var _db = new Rbr_Db()) {
				return VirtualSwitchManager.GetAll(_db);
			}
		}

		#endregion Public Getters

		#region Public Actions

		public static Result Add(VirtualSwitchDto pVirtualSwitch) {
			var _result = new Result();
			using (var _db = new Rbr_Db()) {
				using (var _tx = new Transaction(_db, pVirtualSwitch)) {
					try {
						//1. REQUIRED set Contact Info
						if (pVirtualSwitch.ContactInfo.ContactInfoId == 0) {
							ContactInfoManager.Add(_db, pVirtualSwitch.ContactInfo);
						}

						VirtualSwitchManager.Add(_db, pVirtualSwitch);
						_tx.Commit();
					}
					catch (Exception _ex) {
						if (pVirtualSwitch.ContactInfo != null) {
							pVirtualSwitch.ContactInfo.ContactInfoId = 0;
						}
						_result.Success = false;
						_result.ErrorMessage = _ex.Message;
						TimokLogger.Instance.LogRbr(LogSeverity.Error, "VirtualSwitchController.Add", string.Format("Exception:\r\n{0}", _ex));
					}
				}
			}
			return _result;
		}

		public static Result Update(VirtualSwitchDto pVirtualSwitch) {
			var _result = new Result();
			using (var _db = new Rbr_Db()) {
				using (var _tx = new Transaction(_db, pVirtualSwitch)) {
					try {
						//1. REQUIRED set Contact Info
						ContactInfoManager.Update(_db, pVirtualSwitch.ContactInfo);
						VirtualSwitchManager.Update(_db, pVirtualSwitch);

						_tx.Commit();
					}
					catch (Exception _ex) {
						_result.Success = false;
						_result.ErrorMessage = _ex.Message;
						TimokLogger.Instance.LogRbr(LogSeverity.Error, "VirtualSwitchController.Delete", string.Format("Exception:\r\n{0}", _ex));
					}
				}
			}
			return _result;
		}

		public static Result Delete(VirtualSwitchDto pVirtualSwitch) {
			var _result = new Result();
			using (var _db = new Rbr_Db()) {
				using (var _tx = new Transaction(_db, pVirtualSwitch)) {
					try {
						if (pVirtualSwitch.VirtualSwitchId == AppConstants.DefaultVirtualSwitchId) {
							throw new Exception("Cannot delete Default Virtual Switch");
						}
						VirtualSwitchManager.Delete(_db, pVirtualSwitch);
						_tx.Commit();
					}
					catch (Exception _ex) {
						_result.Success = false;
						_result.ErrorMessage = _ex.Message;
						TimokLogger.Instance.LogRbr(LogSeverity.Error, "VirtualSwitchController.Delete", string.Format("Exception:\r\n{0}", _ex));
					}
				}
			}
			return _result;
		}

		#endregion Public Actions
	}
}
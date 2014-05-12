using System;
using System.Collections.Generic;
using Timok.Core;
using Timok.Rbr.Core.Config;
using Timok.Rbr.DAL.RbrDatabase;
using Timok.Rbr.DTO;

namespace Timok.Rbr.BLL.Managers {
	internal class PartnerManager {
		PartnerManager() { }

		#region Internals

		#region Getters

		internal static PartnerDto Get(Rbr_Db pDb, int pPartnerId) { return get(pDb, pPartnerId); }

		internal static bool IsNameInUse(Rbr_Db pDb, string pName, int pPartnerId) { return pDb.PartnerCollection.IsNameInUseByOtherPartner(pName, pPartnerId); }

		internal static PartnerDto[] GetAll(Rbr_Db pDb) {
			var _list = new List<PartnerDto>();
			PartnerRow[] _rows = pDb.PartnerCollection.GetAll();
			foreach (PartnerRow _partnerRow in _rows) {
				_list.Add(getInfo(pDb, _partnerRow));
			}
			return _list.ToArray();
		}

		internal static PartnerDto[] GetActiveResellers(Rbr_Db pDb) {
			var _list = new List<PartnerDto>();
			var _rows = pDb.PartnerCollection.GetActiveResellers();
			foreach (var _partnerRow in _rows) {
				_list.Add(getInfo(pDb, _partnerRow));
			}
			return _list.ToArray();
		}

		#endregion Getters

		#region Actions

		internal static void Add(Rbr_Db pDb, PartnerDto pPartner) {
			//TODO: NEW DAL - VirtualSwitch
			pPartner.VirtualSwitchId = AppConstants.DefaultVirtualSwitchId;

			validatePartner(pDb, pPartner);
			PartnerRow _partnerRow = MapToPartnerRow(pPartner);

			pDb.PartnerCollection.Insert(_partnerRow);
			pPartner.PartnerId = _partnerRow.Partner_id;
		}

		internal static void Update(Rbr_Db pDb, PartnerDto pPartner) {
			validatePartner(pDb, pPartner);
			PartnerRow _partnerRow = MapToPartnerRow(pPartner);
			if (! pDb.PartnerCollection.Update(_partnerRow)) {
				throw new Exception("Failed to update Partner; name:" + _partnerRow.Name);
			}
		}

		internal static void Delete(Rbr_Db pDb, PartnerDto pPartner) { pDb.PartnerCollection.DeleteByPrimaryKey(pPartner.PartnerId); }

		#endregion Actions

		#region mappings

		#region To DAL mappings

		internal static PartnerRow MapToPartnerRow(PartnerDto pPartner) {
			if (pPartner == null) {
				return null;
			}

			var _partnerRow = new PartnerRow();
			_partnerRow.Partner_id = pPartner.PartnerId;
			_partnerRow.Name = pPartner.Name;
			_partnerRow.AccountStatus = pPartner.Status;
			_partnerRow.Virtual_switch_id = pPartner.VirtualSwitchId;

			if (pPartner.ContactInfo != null) {
				_partnerRow.Contact_info_id = pPartner.ContactInfo.ContactInfoId;
			}
			if (pPartner.BillingSchedule != null) {
				_partnerRow.Billing_schedule_id = pPartner.BillingSchedule.ScheduleId;
			}

			return _partnerRow;
		}

		#endregion To DAL mappings

		#region To BLL mappings

		internal static PartnerDto MapToPartner(PartnerRow pPartnerRow, ScheduleDto pBillingSchedule, ContactInfoDto pContactInfo, PersonDto[] pEmployees) {
			if (pPartnerRow == null) {
				return null;
			}

			var _partner = new PartnerDto();
			_partner.PartnerId = pPartnerRow.Partner_id;
			_partner.Name = pPartnerRow.Name;
			_partner.Status = pPartnerRow.AccountStatus;
			_partner.VirtualSwitchId = pPartnerRow.Virtual_switch_id;

			_partner.BillingSchedule = pBillingSchedule;
			_partner.ContactInfo = pContactInfo;
			_partner.Employees = pEmployees;

			return _partner;
		}

		#endregion To BLL mappings

		#endregion mappings

		#endregion Internals

		#region privates

		static PartnerDto get(Rbr_Db pDb, int pPartnerId) {
			PartnerRow _partnerRow = pDb.PartnerCollection.GetByPrimaryKey(pPartnerId);
			return getInfo(pDb, _partnerRow);
		}

		static PartnerDto getInfo(Rbr_Db pDb, PartnerRow pPartnerRow) {
			if (pPartnerRow == null) {
				return null;
			}

			ScheduleDto _billingSchedule = ScheduleManager.Get(pDb, pPartnerRow.Billing_schedule_id);
			ContactInfoDto _contactInfo = ContactInfoManager.Get(pDb, pPartnerRow.Contact_info_id);
			PersonDto[] _employees = PersonManager.GetByPartnerId(pDb, pPartnerRow.Partner_id);
			return MapToPartner(pPartnerRow, _billingSchedule, _contactInfo, _employees);
		}

		static void validatePartner(Rbr_Db pDb, PartnerDto pPartner) {
			if (pPartner.ContactInfo != null) {
				if (pPartner.ContactInfo.Email.Length > 256) {
					throw new ArgumentException("Email Address is too long (max lenght 256): [" + pPartner.ContactInfo.Email + "]");
				}

				string _formatedEmailString = string.Empty;
				if (pPartner.ContactInfo.Email.Length > 0) {
					if (!EmailValidator.ValidateAndFormat(pPartner.ContactInfo.Email, true, out _formatedEmailString)) {
						throw new ArgumentException("Invalid Email Address: [" + pPartner.ContactInfo.Email + "]");
					}
				}
				pPartner.ContactInfo.Email = _formatedEmailString;
			}
		}

		#endregion privates
	}
}
using System;
using System.Collections.Generic;
using Timok.Rbr.Core.Config;
using Timok.Rbr.DAL.RbrDatabase;
using Timok.Rbr.DTO;

namespace Timok.Rbr.BLL.Managers {
	internal class VirtualSwitchManager {
		VirtualSwitchManager() { }

		#region Internals

		#region Getters

		internal static VirtualSwitchDto Get(Rbr_Db pDb, int pVirtualSwitchId) { return get(pDb, pVirtualSwitchId); }

		internal static VirtualSwitchDto[] GetAll(Rbr_Db pDb) {
			var _list = new List<VirtualSwitchDto>();
			var _rows = pDb.VirtualSwitchCollection.GetAll();
			foreach (var _virtualSwitchRow in _rows) {
				_list.Add(getInfo(pDb, _virtualSwitchRow));
			}
			return _list.ToArray();
		}

		#endregion Getters

		#region Actions

		internal static void Add(Rbr_Db pDb, VirtualSwitchDto pVirtualSwitch) {
			VirtualSwitchRow _virtualSwitchRow = MapToVirtualSwitchRow(pVirtualSwitch);

			pDb.VirtualSwitchCollection.Insert(_virtualSwitchRow);
			pVirtualSwitch.VirtualSwitchId = _virtualSwitchRow.Virtual_switch_id;
		}

		internal static void Update(Rbr_Db pDb, VirtualSwitchDto pVirtualSwitch) {
			VirtualSwitchRow _virtualSwitchRow = MapToVirtualSwitchRow(pVirtualSwitch);
			pDb.VirtualSwitchCollection.Update(_virtualSwitchRow);
		}

		internal static void Delete(Rbr_Db pDb, VirtualSwitchDto pVirtualSwitch) {
			if (pVirtualSwitch.VirtualSwitchId == AppConstants.DefaultVirtualSwitchId) {
				throw new Exception("Cannot Delete Default Virtual Switch");
			}
			pDb.VirtualSwitchCollection.DeleteByPrimaryKey(pVirtualSwitch.VirtualSwitchId);
		}

		#endregion Actions

		#region mappings

		#region To DAL mappings

		internal static VirtualSwitchRow MapToVirtualSwitchRow(VirtualSwitchDto pVirtualSwitch) {
			if (pVirtualSwitch == null) {
				return null;
			}

			var _virtualSwitchRow = new VirtualSwitchRow();
			_virtualSwitchRow.Virtual_switch_id = pVirtualSwitch.VirtualSwitchId;
			_virtualSwitchRow.Name = pVirtualSwitch.Name;
			_virtualSwitchRow.SwitchStatus = pVirtualSwitch.Status;

			if (pVirtualSwitch.ContactInfo != null) {
				_virtualSwitchRow.Contact_info_id = pVirtualSwitch.ContactInfoId;
			}

			return _virtualSwitchRow;
		}

		#endregion To DAL mappings

		#region To BLL mappings

		internal static VirtualSwitchDto MapToVirtualSwitch(VirtualSwitchRow pVirtualSwitchRow, ContactInfoDto pContactInfo) {
			if (pVirtualSwitchRow == null) {
				return null;
			}

			var _virtualSwitch = new VirtualSwitchDto();
			_virtualSwitch.VirtualSwitchId = pVirtualSwitchRow.Virtual_switch_id;
			_virtualSwitch.Name = pVirtualSwitchRow.Name;
			_virtualSwitch.Status = pVirtualSwitchRow.SwitchStatus;

			_virtualSwitch.ContactInfo = pContactInfo;

			return _virtualSwitch;
		}

		#endregion To BLL mappings

		#endregion mappings

		#endregion Internals

		#region privates

		static VirtualSwitchDto get(Rbr_Db pDb, int pVirtualSwitchId) {
			VirtualSwitchRow _virtualSwitchRow = pDb.VirtualSwitchCollection.GetByPrimaryKey(pVirtualSwitchId);
			return getInfo(pDb, _virtualSwitchRow);
		}

		static VirtualSwitchDto getInfo(Rbr_Db pDb, VirtualSwitchRow pVirtualSwitchRow) {
			if (pVirtualSwitchRow == null) {
				return null;
			}

			ContactInfoDto _contactInfo = ContactInfoManager.Get(pDb, pVirtualSwitchRow.Contact_info_id);
			return MapToVirtualSwitch(pVirtualSwitchRow, _contactInfo);
		}

		#endregion privates
	}
}
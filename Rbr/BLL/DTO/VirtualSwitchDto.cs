using System;
using Timok.Core;
using Timok.Rbr.Core.Config;

namespace Timok.Rbr.DTO {
	[Serializable]
	public class VirtualSwitchDto {
		public const string Name_PropName = "Name";
		public const string Status_PropName = "Status";
		public const string VirtualSwitchId_PropName = "VirtualSwitchId";
		ContactInfoDto contactInfo;

		int virtualSwitchId;
		public int VirtualSwitchId { get { return virtualSwitchId; } set { virtualSwitchId = value; } }

		public string Name { get; set; }

		public Status Status { get; set; }

		public ContactInfoDto ContactInfo { get { return contactInfo; } set { contactInfo = value; } }

		public int ContactInfoId { get { return contactInfo != null ? contactInfo.ContactInfoId : 0; } }

		//NOTE: compare object's values (not refs)
		public override bool Equals(object obj) {
			if (obj == null || obj.GetType() != GetType()) {
				return false;
			}

			return ObjectComparer.AreEqual(this, obj);
		}

		public override int GetHashCode() {
			//TODO: finish it, get hashes for all fields
			return virtualSwitchId.GetHashCode();
		}
	}
}
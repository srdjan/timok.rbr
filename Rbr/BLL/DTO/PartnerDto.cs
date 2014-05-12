using System;
using Timok.Core;
using Timok.Rbr.Core.Config;

namespace Timok.Rbr.DTO {
	[Serializable]
	public class PartnerDto {
		public const string name_PropName = "Name";
		public const string partnerId_PropName = "PartnerId";

		int partnerId;
		public int PartnerId { get { return partnerId; } set { partnerId = value; } }

		public string Name { get; set; }

		public Status Status { get; set; }

		public int VirtualSwitchId { get; set; }

		public ContactInfoDto ContactInfo { get; set; }

		public PersonDto[] Employees { get; set; }

		//public bool AccessEnabled {
		//  get { return person != null; }
		//}

		public ScheduleDto BillingSchedule { get; set; }

		//NOTE: compare object's values (not refs)
		public override bool Equals(object obj) {
			if (obj == null || obj.GetType() != GetType()) {
				return false;
			}
			return ObjectComparer.AreEqual(this, obj);
		}

		public override int GetHashCode() {
			//TODO: finish it, get hashes for all fields
			return partnerId.GetHashCode();
		}
	}
}
using System;
using Timok.Core;

namespace Timok.Rbr.DTO {
	[Serializable]
	public class CallingPlanDto {
		public const string CallingPlanId_PropName = "CallingPlanId";
		public const string Name_PropName = "Name";

		int callingPlanId;
		public int CallingPlanId { get { return callingPlanId; } set { callingPlanId = value; } }

		string name;
		public string Name { get { return name; } set { name = value; } }

		int virtualSwitchId;
		public int VirtualSwitchId { get { return virtualSwitchId; } set { virtualSwitchId = value; } }

		int version;
		public int Version { get { return version; } set { version = value; } }

		//NOTE: compare object's values (not refs)
		public override bool Equals(object obj) {
			if (obj == null || obj.GetType() != GetType()) {
				return false;
			}
			return ObjectComparer.AreEqual(this, obj);
		}

		public override int GetHashCode() {
			//TODO: finish it, get hashes for all fields
			return callingPlanId.GetHashCode();
		}
	}
}
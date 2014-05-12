using System;
using Timok.Core;

namespace Timok.Rbr.DTO {
	[Serializable]
	public class RoutingPlanDto {
		public const string RoutingPlanId_PropName = "RoutingPlanId";
		public const string Name_PropName = "Name";

		int routingPlanId;
		public int RoutingPlanId { get { return routingPlanId; } set { routingPlanId = value; } }

		string name;
		public string Name { get { return name; } set { name = value; } }

		public int CallingPlanId { get { return callingPlan.CallingPlanId; } }

		CallingPlanDto callingPlan;
		public CallingPlanDto CallingPlan { get { return callingPlan; } set { callingPlan = value; } }

		int virtualSwitchId;
		public int VirtualSwitchId { get { return virtualSwitchId; } set { virtualSwitchId = value; } }

		public RoutingPlanDto() {}

		//NOTE: compare object's values (not refs)
		public override bool Equals(object obj) {
			if (obj == null || obj.GetType() != GetType()) {
				return false;
			}

			return ObjectComparer.AreEqual(this, obj);
		}

		public override int GetHashCode() {
			//TODO: finish it, get hashes for all fields
			return routingPlanId.GetHashCode();
		}
	}
}
using System;
using Timok.Rbr.Core.Config;
using Timok.Core;

namespace Timok.Rbr.DTO {
	[Serializable]
	public class RoutingTableDto {
		public const string Name_PropName = "Name";
		public const string CountryName_PropName = "CountryName";

		public int RoutingPlanId { get; set; }

		public int BaseRouteId { get; set; }

		public RoutingAlgorithmType Algorithm { get; set; }

		public TerminationRouteDto[] AvailableTerminations { get; set; }

		public TerminationChoiceDto[] Terminations { get; set; }

		//NOTE: compare object's values (not refs)
		public override bool Equals(object obj) {
			if (obj == null || obj.GetType() != GetType()) {
				return false;
			}

			return ObjectComparer.AreEqual(this, obj);
		}

		public override int GetHashCode() {
			//TODO: finish it, get hashes for all fields
			return string.Concat(RoutingPlanId, "-", BaseRouteId).GetHashCode();
		}
	}
}
using System;
using Timok.Rbr.Core.Config;

namespace Timok.Rbr.DTO {
	[Serializable]
	public class TerminationChoiceDto {
		public int TerminationChoiceId { get; set; }

		public int RoutingPlanId { get; set; }

		public int RouteId { get; set; }

		public byte Priority { get; set; }

		public int Version { get; set; }

		public int CarrierRouteId { get; set; }

		public Status CarrierBaseRouteStatus { get; set; }

		public Status CarrierRouteStatus { get; set; }

		public string CarrierRouteName { get; set; }

		public string CarrierAcctName { get; set; }

		public Status CarrierAcctStatus { get; set; }

		public RouteState CarrierRouteState { get; set; }

		public bool CarrierRouteHasNoActiveEndpointMaps { get { return (CarrierRouteState & RouteState.NoActiveEndpoints) > 0; } }

		public bool CarrierRouteHasNoDialCodes { get { return (CarrierRouteState & RouteState.NoDialCodes) > 0; } }
	}
}
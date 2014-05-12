using System;
using Timok.Core;
using Timok.Rbr.Core.Config;

namespace Timok.Rbr.DTO {
	[Serializable]
	public class BaseRouteDto {
		public const string Name_PropName = "Name";
		public const string CountryName_PropName = "CountryName";
    public const string IsProper_PropName = "IsProper";//used for proper sorting

		public int BaseRouteId { get; set; }
		public int RoutingNumber { get; set; }
		public string Name { get; set; }
		public Status BaseStatus { get; set; }
		public CallingPlanDto CallingPlan { get; set; }
		public int CallingPlanId { get { return CallingPlan.CallingPlanId; } }

		public CountryDto Country { get; set; }
		public int CountryId { get { return Country.CountryId; } }
		public string CountryName { get { return Country.Name; } }

		public int Version { get; set; }

		public string BreakoutName {
			get {
				if (Name != null && Name.IndexOf(AppConstants.SubRouteSeparator) > 0) {
					return Name.Split(AppConstants.SubRouteSeparator.ToCharArray())[1];
				}
				return string.Empty;
			}
		}

		public bool IsProper { get { return BreakoutName.ToUpper() == AppConstants.ProperNameSuffix.ToUpper(); } }

    public bool IsValidRouteState {
      get { return RouteState == RouteState.Valid; }
    }

		public RouteState RouteState { get; set; }

		//NOTE: compare object's values (not refs)
		public override bool Equals(object obj) {
			if (obj == null || obj.GetType() != GetType()) {
				return false;
			}

			return ObjectComparer.AreEqual(this, obj);
		}

		public override int GetHashCode() {
			//TODO: finish it, get hashes for all fields
			return BaseRouteId.GetHashCode();
		}
	}
}
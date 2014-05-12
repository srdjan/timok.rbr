using System;
using Timok.Rbr.Core.Config;
using Timok.Core;

namespace Timok.Rbr.DTO {

  [Serializable]
  public class RoutingPlanDetailDto {
    public const string BaseRouteName_PropName = "BaseRouteName";
    public const string CountryName_PropName = "CountryName";
    public const string IsProper_PropName = "IsProper";//used for proper sorting

    public int RoutingPlanId {
      get { return RoutingPlan.RoutingPlanId; }
    }

  	public RoutingAlgorithmType Algorithm { get; set; }

  	public BaseRouteDto BaseRoute { get; set; }

  	public string BaseRouteName { get { return BaseRoute == null ? string.Empty : BaseRoute.Name; } }

    public int BaseRouteId { 
			get { return BaseRoute == null ? 0 : BaseRoute.BaseRouteId; } 
			set { BaseRoute.BaseRouteId = value; }
    }

    public bool IsProper { get { return BaseRoute != null && BaseRoute.IsProper; } }

    public CountryDto Country { get { return BaseRoute == null ? null : BaseRoute.Country; } }

    public string CountryName { get { return BaseRoute == null ? string.Empty : BaseRoute.CountryName; } }

    public int CountryId { get { return BaseRoute == null ? 0 : BaseRoute.CountryId; } }

    public int CountryCode {
      get {
        if (BaseRoute == null || BaseRoute.Country == null) 
					return 0;
        return BaseRoute.Country.CountryCode;
      }
    }

    public string BreakoutName { get { return BaseRoute == null ? string.Empty : BaseRoute.BreakoutName; } }

  	public RoutingPlanDto RoutingPlan { get; set; }

  	public bool IsValidRouteState { get { return RouteState == RouteState.Valid; } }

    RouteState routeState;
    public RouteState RouteState {
      //RoutingPlanDetail has no State for now, just return BaseRoute's State
      //TODO: check for NoTerminations (LCR and/or Manual/LoadBalance state)
      //      and combine with BaseRoute's state
      get {
        if (BaseRoute == null) return routeState;
        return routeState | BaseRoute.RouteState;
      }
      set { routeState = value; }
    }


    public RoutingPlanDetailDto() {
      BaseRoute = new BaseRouteDto();
    }
    public RoutingPlanDetailDto(BaseRouteDto pBaseRoute) {
      BaseRoute = pBaseRoute;
    }

    //NOTE: compare object's values (not refs)
    public override bool Equals(object obj) {
      if (obj == null || obj.GetType() != GetType())
        return false;

      return ObjectComparer.AreEqual(this, obj);
    }

    public override int GetHashCode() {
      //TODO: finish it, get hashes for all fields
      return string.Concat(RoutingPlanId, "-", BaseRouteId).GetHashCode();
    }

  }
}

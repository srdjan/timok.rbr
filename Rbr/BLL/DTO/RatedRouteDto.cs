using System;
using Timok.Rbr.BLL.DTO;
using Timok.Rbr.Core.Config;
using Timok.Core;

namespace Timok.Rbr.DTO {

  [Serializable]
  public class RatedRouteDto {
    public const string Name_PropName = "Name";
    public const string CountryName_PropName = "CountryName";
    public const string IsProper_PropName = "IsProper";//used for proper sorting

  	public int RatedRouteId { get; set; }

  	public short AccountId { get; set; }
  	public string AccountName { get; set; }
  	public Status AccountStatus { get; set; }

  	//TODO: !!! make sure it's filled in mappings
  	public RatingInfoDto DefaultRatingInfo { get; set; }
  	public int RoutingPlanId { get; set; }
  	public RoutingAlgorithmType Algorithm { get; set; }

  	public BaseRouteDto BaseRoute { get; set; }

  	public string Name { get { return BaseRoute == null ? string.Empty : BaseRoute.Name; } }

    public int BaseRouteId { get { return BaseRoute == null ? 0 : BaseRoute.BaseRouteId; } }

    public bool IsProper { get { return BaseRoute != null && BaseRoute.IsProper; } }

    public CountryDto Country { get { return BaseRoute == null ? null : BaseRoute.Country; } }

    public string CountryName { get { return BaseRoute == null ? string.Empty : BaseRoute.CountryName; } }

    public int CountryId { get { return BaseRoute == null ? 0 : BaseRoute.CountryId; } }

    public int CountryCode { get { return BaseRoute == null ? 0 : BaseRoute.Country.CountryCode; } }

    public string BreakoutName { get { return BaseRoute == null ? string.Empty : BaseRoute.BreakoutName; } }

  	public Status Status { get; set; }
  	public short ASRTarget { get; set; }
  	public int ASRTimeWindow { get; set; }
  	public short ACDTarget { get; set; }
    public int ACDTimeWindow { get; set; }
  	public byte NextEP { get; set; }

  	//private bool hasActiveEndpointMaps;
    public bool HasNoActiveEndpointMaps {
      //get { return hasActiveEndpointMaps; }
      //set { hasActiveEndpointMaps = value; }
      get { return (RouteState & RouteState.NoActiveEndpoints) > 0; }
    }

    public bool HasNoDialCodes {
      get { return (RouteState & RouteState.NoDialCodes) > 0; }
    }

    public bool IsValidRouteState {
      get { return RouteState == RouteState.Valid; }
    }

    RouteState routeState;
    public RouteState RouteState {//combine this state with BaseRoute's state
      get {
        if (BaseRoute == null) return routeState;
        return routeState | BaseRoute.RouteState;
      }
      set { routeState = value; }
    }

    //NOTE: compare object's values (not refs)
    public override bool Equals(object obj) {
      if (obj == null || obj.GetType() != GetType())
        return false;

      return ObjectComparer.AreEqual(this, obj);
    }

    public override int GetHashCode() {
      //TODO: finish it, get hashes for all fields
      return RatedRouteId.GetHashCode();
    }
  }
}

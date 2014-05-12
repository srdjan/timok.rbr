using System;

namespace Timok.Rbr.DTO {
  [Serializable]
	public class TerminationRouteDto {
    public const string LCRRank_PropName = "LCRRank";
    public const string PartialCoverage_PropName = "PartialCoverage";
    public const string CarrierName_PropName = "CarrierName";
    public const string CarrierRouteName_PropName = "CarrierRouteName";
    
    private int carrierRouteId;
    public int CarrierRouteId {
			get { return carrierRouteId; }
			set { carrierRouteId = value; }
		}

    private string carrierRouteName;
    public string CarrierRouteName {
      get { return carrierRouteName; }
      set { carrierRouteName = value; }
		}

		private int callingPlanId;
    public int CallingPlanId {
      get { return callingPlanId; }
      set { callingPlanId = value; }
		}

		private short carrierAcctId;
		public short CarrierAcctId {
			get { return carrierAcctId; }
			set { carrierAcctId = value; }
		}

		private string carrierName;
		public string CarrierName {
			get { return carrierName; }
			set { carrierName = value; }
		}

		private int lcrRank;
		public int LCRRank {
			get { return lcrRank; }
			set { lcrRank = value; }
		}

		public bool LCRCapable {
			get { return lcrRank > 0 && lcrRank < int.MaxValue; }
		}

    private bool partialCoverage;
    public bool PartialCoverage {
      get { return partialCoverage; }
      set { partialCoverage = value; }
    }

    private bool hasActiveEndpointMap;
    public bool HasActiveEndpointMap {
      get { return hasActiveEndpointMap; }
      set { hasActiveEndpointMap = value; }
    }

    //NOTE: REQIRED FOR DESERIALIZATION
    public TerminationRouteDto() { }

    public TerminationRouteDto(int pCarrierRouteId, string pCarrierRouteName, short pCarrierAcctId, int pCallingPlanId, string pCarrierName, int pLCRRank, bool pPartialCoverage, bool pHasActiveEndpointMap) {
			carrierAcctId = pCarrierAcctId;
      callingPlanId = pCallingPlanId;
			carrierName = pCarrierName;
			carrierRouteId = pCarrierRouteId;
      carrierRouteName = pCarrierRouteName;
      lcrRank = pLCRRank;
      partialCoverage = pPartialCoverage;
      hasActiveEndpointMap = pHasActiveEndpointMap;
    }
  }
}

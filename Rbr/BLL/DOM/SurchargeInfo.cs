using Timok.Rbr.Core.Config;

namespace Timok.Rbr.DOM {
	public sealed class SurchargeInfo {
		readonly decimal cost;
		public decimal Cost { get { return cost; } }

		readonly SurchargeType surchargeType;
		public SurchargeType SurchargeType { get { return surchargeType; } }

		public static SurchargeInfo Empty {
			get {
				return new SurchargeInfo(decimal.Zero, SurchargeType.None);
			}
		}

		public SurchargeInfo(decimal pCost, SurchargeType pSurchargeType) {
			cost = pCost;
			surchargeType = pSurchargeType;
		}
	}
}
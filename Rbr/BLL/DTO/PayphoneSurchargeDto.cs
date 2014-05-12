using System;
using Timok.Core;
using Timok.Rbr.Core.Config;

namespace Timok.Rbr.DTO {
	[Serializable]
	public class PayphoneSurchargeDto {
		//prop names
		public const string PayphoneSurchargeId_PropName = "PayphoneSurchargeId";
		public const string Surcharge_PropName = "Surcharge";
		public const string SurchargeType_PropName = "SurchargeType";

		int payphoneSurchargeId;

		decimal surcharge;
		public int PayphoneSurchargeId { get { return payphoneSurchargeId; } set { payphoneSurchargeId = value; } }

		public decimal Surcharge {
			get { return surcharge; }
			set {
				if (value > 99.9999999M) {
					surcharge = decimal.Zero;
				}
				else if (value < decimal.Zero) {
					surcharge = decimal.Zero;
				}
				else {
					surcharge = value;
				}
			}
		}

		public SurchargeType SurchargeType { get; set; }

		public override bool Equals(object obj) {
			if (obj == null || obj.GetType() != GetType()) {
				return false;
			}

			return ObjectComparer.AreEqual(this, obj);
		}

		public override int GetHashCode() {
			//TODO: finish it, get hashes for all fields
			return payphoneSurchargeId.GetHashCode();
		}
	}
}
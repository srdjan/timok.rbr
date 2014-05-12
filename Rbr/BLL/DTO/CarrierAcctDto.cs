using System;
using Timok.Core;
using Timok.Rbr.BLL.DTO;
using Timok.Rbr.Core.Config;

namespace Timok.Rbr.DTO {
	[Serializable]
	public class CarrierAcctDto {
		public const string CarrierAcctId_PropName = "CarrierAcctId";
		public const string Name_PropName = "Name";
		public const string Status_PropName = "Status";

		public short CarrierAcctId { get; set; }

		public string Name { get; set; }

		public Status Status { get; set; }

		public string PrefixOut { get; set; }

		public bool WithPrefix { get { return (!string.IsNullOrEmpty(PrefixOut)); } }

		public bool Strip1Plus { get; set; }

		string intlDialCode = Configuration.Instance.Main.DefaultIntlDialCode;
		public string IntlDialCode { get { return intlDialCode; } set { intlDialCode = value; } }

		public int PartnerId { get { return partner.PartnerId; } }

		PartnerDto partner;
		public PartnerDto Partner { get { return partner; } set { partner = value; } }

		public CallingPlanDto CallingPlan { get; set; }

		public RatedRouteDto DefaultRoute { get; set; }

		public int DefaultRateInfoId { get { return DefaultRatingInfo != null ? DefaultRatingInfo.RateInfoId : 0; } }

		public RatingInfoDto DefaultRatingInfo { get; set; }

		public RatingType RatingType { get; set; }

		public bool IsRatingEnabled { get { return RatingType != RatingType.Disabled; } }

		public string CurrencyFormat { get { return Configuration.Instance.Main.CarrierRateAmountFormat; } }

		public short MaxCallLength { get; set; }

		public CarrierAcctDto() { Status = Status.Active; }

		public override string ToString() { return Name; }

		//NOTE: compare object's values (not refs)
		public override bool Equals(object obj) {
			if (obj == null || obj.GetType() != GetType()) {
				return false;
			}

			return ObjectComparer.AreEqual(this, obj);
		}

		public override int GetHashCode() {
			//TODO: finish it, get hashes for all fields
			return CarrierAcctId.GetHashCode();
		}
	}
}
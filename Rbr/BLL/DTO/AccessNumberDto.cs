using System;
using Timok.Core;
using Timok.Rbr.Core.Config;

namespace Timok.Rbr.DTO {
	[Serializable]
	public class AccessNumberDto {
		public const string Number_PropName = "Number";
		public const string ScriptType_PropName = "ScriptType";
		public const string Language_PropName = "Language";
		public const string Surcharge_PropName = "Surcharge";
		public const string SurchargeType_PropName = "SurchargeType";

		long number;
		public long Number { get { return number; } set { number = value; } }

		public short ServiceId { get; set; }

		public short CustomerAcctId { get; set; }

		public ScriptLanguage Language { get; set; }

		public ScriptType ScriptType { get; set; }

		decimal surcharge;

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

		//NOTE: compare object's values (not refs)
		public override bool Equals(object obj) {
			if (obj == null || obj.GetType() != GetType()) {
				return false;
			}

			return ObjectComparer.AreEqual(this, obj);
		}

		public override int GetHashCode() {
			//TODO: finish it, get hashes for all fields
			return number.GetHashCode();
		}
	}
}
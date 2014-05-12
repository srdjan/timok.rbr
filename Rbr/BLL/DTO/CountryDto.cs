using System;
using Timok.Core;
using Timok.Rbr.Core.Config;

namespace Timok.Rbr.DTO {
	[Serializable]
	public class CountryDto {
		public const string CountryCode_PropName = "CountryCode";
		public const string Name_PropName = "Name";
		public const string Selected_PropName = "Selected";
		public const string Status_PropName = "Status";

		int countryId;
		public int CountryId { get { return countryId; } set { countryId = value; } }

		public string Name { get; set; }

		public int CountryCode { get; set; }

		public Status Status { get; set; }

		public int Version { get; set; }

		public bool Selected { get; set; }

		//NOTE: compare object's values (not refs)
		public override bool Equals(object obj) {
			if (obj == null || obj.GetType() != GetType()) {
				return false;
			}

			return ObjectComparer.AreEqual(this, obj);
		}

		public override int GetHashCode() {
			//TODO: finish it, get hashes for all fields
			return countryId.GetHashCode();
		}
	}
}
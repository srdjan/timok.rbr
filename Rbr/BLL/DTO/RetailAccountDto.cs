using System;
using Timok.Rbr.Core.Config;
using Timok.Core;

namespace Timok.Rbr.DTO {
	[Serializable]
	public class RetailAccountDto {
		int retailAcctId;
		public int RetailAcctId { get { return retailAcctId; } set { retailAcctId = value; } }

		public short CustomerAcctId { get; set; }

		public short ServiceId { get; set; }

		public RetailType RetailType { get; set; }

		public DateTime DateCreated { get; set; }

		public DateTime DateActive { get; set; }

		public DateTime DateToExpire { get; set; }

		public DateTime DateExpired { get; set; }

		public decimal StartBalance { get; set; }

		public short StartBonusMinutes { get; set; }

		public decimal CurrentBalance { get; set; }

		public short CurrentBonusMinutes { get; set; }

		public Status Status { get; set; }

		public PersonDto Person { get; set; }

		public bool AccessEnabled { get { return Person != null; } }

		public PhoneCardDto[] PhoneCards { get; set; }

		public ResidentialPSTNDto[] ResidentialPSTNs { get; set; }

		public RetailAccountDto() {
			DateToExpire = DateTime.Today.AddYears(1);
			DateCreated = DateTime.Today;
			DateActive = Configuration.Instance.Db.SqlSmallDateTimeMaxValue;
			DateExpired = Configuration.Instance.Db.SqlSmallDateTimeMaxValue;
		}

		//NOTE: compare object's values (not refs)
		public override bool Equals(object obj) {
			if (obj == null || obj.GetType() != GetType()) {
				return false;
			}

			return ObjectComparer.AreEqual(this, obj);
		}

		public override int GetHashCode() {
			//TODO: finish it, get hashes for all fields
			return retailAcctId.GetHashCode();
		}
	}
}
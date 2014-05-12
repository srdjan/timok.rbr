using System;
using Timok.Core;
using Timok.Rbr.Core.Config;

namespace Timok.Rbr.DTO {
	[Serializable]
	public class ResellAccountDto {
		short resellAccountId;
		public short ResellAccountId { get { return resellAccountId; } set { resellAccountId = value; } }

		public int PartnerId { get; set; }

		public int PersonId { get; set; }

		public short CustomerAcctId { get; set; }

		public bool PerRoute { get; set; }

		public CommisionType CommisionType { get; set; }

		public decimal MarkupDollar { get; set; }

		public decimal MarkupPercent { get; set; }

		public decimal FeePerCall { get; set; }

		public decimal FeePerMinute { get; set; }

		//NOTE: compare object's values (not refs)
		public override bool Equals(object obj) {
			if (obj == null || obj.GetType() != GetType()) {
				return false;
			}

			return ObjectComparer.AreEqual(this, obj);
		}

		public override int GetHashCode() {
			//TODO: finish it, get hashes for all fields
			return resellAccountId.GetHashCode();
		}
	}
}
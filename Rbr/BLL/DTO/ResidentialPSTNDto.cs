using System;
using Timok.Core;
using Timok.Rbr.Core.Config;

namespace Timok.Rbr.DTO {
	[Serializable]
	public class ResidentialPSTNDto {
		long ani;
		short serviceId;
		public short ServiceId { get { return serviceId; } set { serviceId = value; } }

		public long ANI { get { return ani; } set { ani = value; } }

		public Status Status { get; set; }

		public DateTime DateFirstUsed { get; set; }

		public DateTime DateLastUsed { get; set; }

		public int RetailAcctId { get; set; }

		//NOTE: compare object's values (not refs)
		public override bool Equals(object obj) {
			if (obj == null || obj.GetType() != GetType()) {
				return false;
			}

			return ObjectComparer.AreEqual(this, obj);
		}

		public override int GetHashCode() {
			//TODO: finish it, get hashes for all fields
			return string.Concat(serviceId.ToString(), "-", ani.ToString()).GetHashCode();
		}
	}
}
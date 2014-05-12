using System;
using Timok.Core;
using Timok.Rbr.Core.Config;

namespace Timok.Rbr.DTO {
	[Serializable]
	public class PhoneCardDto {
		DateTime dateToExpire;
		long pin;
		long serialNumber;
		public short ServiceId { get; set; }
		public long Pin { get { return pin; } set { pin = value; } }

		public string HiddenPin {
			get {
				var _temp = pin.ToString();
				if (_temp.Length > 4) {
					return "*******" + _temp.Remove(0, _temp.Length - 4);
				}
				return "none";
			}
		}

		public InventoryStatus InventoryStatus { get; set; }

		public Status Status { get; set; }

		public DateTime DateFirstUsed { get; set; }

		public DateTime DateLastUsed { get; set; }

		public DateTime DateLoaded { get; set; }

		public DateTime DateToExpire { get { return dateToExpire; } set { dateToExpire = value; } }

		public DateTime DateActive { get; set; }

		public DateTime DateDeactivated { get; set; }

		public DateTime DateArchived { get; set; }

		public long SerialNumber { get { return serialNumber; } set { serialNumber = value; } }

		public int RetailAcctId { get; set; }

		public bool Expired { get { return dateToExpire <= DateTime.Today; } }

		//NOTE: compare object's values (not refs)
		public override bool Equals(object obj) {
			if (obj == null || obj.GetType() != GetType()) {
				return false;
			}

			return ObjectComparer.AreEqual(this, obj);
		}

		public override int GetHashCode() {
			//TODO: finish it, get hashes for all fields
			return serialNumber.GetHashCode();
		}
	}
}
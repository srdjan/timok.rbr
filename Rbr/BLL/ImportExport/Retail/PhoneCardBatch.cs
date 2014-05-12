using System;
using System.Collections;
using Timok.Rbr.Core.Config;

namespace Timok.Rbr.BLL.ImportExport.Retail {
	[Serializable]
	public class PhoneCardBatch {
		public short CustomerAcctId { get; set; }

		public short ServiceId { get; set; }

		public DateTime DateToExpire { get; set; }

		public DateTime DateCreated { get; set; }

		public decimal StartBalance { get; set; }

		public short StartBonusMinutes { get; set; }

		public InventoryStatus InventoryStatus { get; set; }

		public ArrayList PhoneCards { get; set; }
	}
}
using System;
using Timok.Rbr.Core.Config;
using Timok.Rbr.DTO;

namespace Timok.Rbr.BLL.Inventory {
	[Serializable]
	public class InventoryCommandRequest {
		public short ServiceId { get; set; }

		public short CustomerAcctId { get; set; }

		public int PinLength { get; set; }

		public decimal Denomination { get; set; }

		public InventoryCommand InventoryCommand { get; set; }

		public bool ActivateOnLoad { get; set; }

		public BatchDto[] Batches { get; set; }

		public PersonDto Person { get; set; }

		public DateTime DateCreated { get; set; }

		public DateTime DateActive { get; set; }

		public DateTime DateToExpire { get; set; }

		public InventoryCommandFilter Filter { get; set; }

		public InventoryCommandRequest(short pServiceId, short pCustomerAcctId, InventoryCommand pInventoryCommand, PersonDto pPerson, InventoryCommandFilter pFilter) {
			ServiceId = pServiceId;
			CustomerAcctId = pCustomerAcctId;
			InventoryCommand = pInventoryCommand;
			Person = pPerson;
			Filter = pFilter;
		}

		public InventoryCommandRequest(short pServiceId, short pCustomerAcctId, int pPinLength, decimal pDenomination, InventoryCommand pInventoryCommand, BatchDto[] pBatches, PersonDto pPerson, bool pActivateOnLoad) {
			ServiceId = pServiceId;
			CustomerAcctId = pCustomerAcctId;
			PinLength = pPinLength;
			Denomination = pDenomination;
			InventoryCommand = pInventoryCommand;
			Batches = pBatches;
			Person = pPerson;
			ActivateOnLoad = pActivateOnLoad;
			Filter = InventoryCommandFilter.All;
		}
	} 
	
	[Serializable]
	public class InventoryExportRequest {
		public short ServiceId { get; set; }
		public short CustomerAcctId { get; private set; }
		public InventoryCommand Command { get; private set; }
		public PersonDto Person { get; private set; }
		public InventoryCommandFilter Filter { get; private set; }

		public InventoryExportRequest(short pServiceId, short pCustomerAcctId, InventoryCommand pCommand, BatchDto[] pSelectedBatches, PersonDto pPerson, InventoryCommandFilter pFilter) {
			ServiceId = pServiceId;
			CustomerAcctId = pCustomerAcctId;
			Command = pCommand;
			Person = pPerson;
			Filter = pFilter;
		}
	}

	public enum InventoryCommandFilter {
		All,
		Loaded,
		Active
	}
}
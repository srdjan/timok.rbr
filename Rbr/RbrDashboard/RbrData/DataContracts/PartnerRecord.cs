using System.Collections.Generic;
using System.Runtime.Serialization;

namespace RbrData.DataContracts {
	[DataContract]
	public class PartnerRecord {
		[DataMember]
		public int Id { get; private set; }
		[DataMember]
		public string Name { get; private set; }

		public List<CustomerAcctRecord> CustomerAccts { get; private set; }
		public List<CarrierAcctRecord> CarrierAccts { get; private set; }

		public PartnerRecord(int pPartnerId, string pName, List<CustomerAcctRecord> pCustomerAccts, List<CarrierAcctRecord> pCarrierAccts) {
			Id = pPartnerId;
			Name = pName;
			CustomerAccts = pCustomerAccts.Count > 0 ? pCustomerAccts : null;
			CarrierAccts = pCarrierAccts.Count > 0 ? pCarrierAccts : null;
		}
	}
}
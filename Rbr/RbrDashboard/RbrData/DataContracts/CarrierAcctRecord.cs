using System.Runtime.Serialization;

namespace RbrData.DataContracts {
	[DataContract]
	public class CarrierAcctRecord {
		[DataMember]
		public string Name { get; private set; }

		[DataMember]
		public short Id { get; private set; }

		public CarrierAcctRecord(short pCarrierAcctId, string pCarrierAcctName) {
			Id = pCarrierAcctId;
			Name = pCarrierAcctName;
		}
	}
}
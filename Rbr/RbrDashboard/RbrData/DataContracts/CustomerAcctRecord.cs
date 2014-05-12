using System.Runtime.Serialization;

namespace RbrData.DataContracts {
	[DataContract]
	public class CustomerAcctRecord {
		[DataMember]
		public string Name { get; private set; }

		[DataMember]
		public short Id { get; private set; }

		public CustomerAcctRecord(short pCustomerAcctId, string pCustomerAcctName) {
			Id = pCustomerAcctId;
			Name = pCustomerAcctName;
		}
	}
}
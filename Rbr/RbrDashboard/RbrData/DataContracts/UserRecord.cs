using System.Runtime.Serialization;

namespace RbrData.DataContracts {
	//[DataContract]
	public class UserRecord {
		//[DataMember]
		public int PersonId { get; private set; }
		//[DataMember]
		public string Name { get; private set; }
		//[DataMember]
		public int? PartnerId { get; private set; }
		//[DataMember]
		public string Password { get; private set; }

		public UserRecord(int pPersonId, string pName, string pPassword, int? pPartnerId) {
			PersonId = pPersonId;
			Name = pName;
			Password = pPassword;
			PartnerId = pPartnerId;
		}
	}
}
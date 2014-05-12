using System.Runtime.Serialization;

namespace RbrData.DataContracts {
	[DataContract]
	public class NodeRecord {
		[DataMember]
		public short? NodeId { get; private set; }

		[DataMember]
		public string Name { get; private set; }

		public NodeRecord(short? pNodeId, string pName) {
			NodeId = pNodeId;
			Name = pName;
		}
	}
}
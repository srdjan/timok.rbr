using System.Collections.Generic;
using System.Runtime.Serialization;

namespace RbrData.DataContracts {
	[DataContract]
	public class ReportContext {
		[DataMember]
		public List<PartnerRecord> Partners { get; private set; }
		//[DataMember]
		//public List<NodeRecord> Nodes { get; private set; }

		public ReportContext(List<PartnerRecord> pPartners/*, List<NodeRecord> pNodes*/) {
			Partners = pPartners != null && pPartners.Count > 0 ? pPartners : null;
			//Nodes = pNodes != null && pNodes.Count > 0 ? pNodes	: null;
		}
	}
}
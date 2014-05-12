using System.Runtime.Serialization;

namespace RbrData.DataContracts {
	[DataContract]
	public class CustomerReportRecord {
		[DataMember]
		public int? Day { get; private set; }
		[DataMember]
		public short CustomerAcctId { get; private set; }
		[DataMember]
		public short CarrierAcctId { get; private set; }
		[DataMember]
		public short? NodeId { get; private set; }
		[DataMember]
		public int? Total { get; private set; }
		[DataMember]
		public int? Completed { get; private set; }
		[DataMember]
		public int? InMinutes { get; private set; }
		[DataMember]
		public decimal? OutMinutes { get; private set; }
		[DataMember]
		public decimal? Cost { get; private set; }
		[DataMember]
		public string RouteName { get; private set; }
		[DataMember]
		public string CarrierName { get; private set; }

		public CustomerReportRecord(int? pDay, short? pNodeId, short pCustomerAcctId, short pCarrierAcctId, int? pTotal, int? pCompleted, int? pInMinutes, decimal? pOutMinutes, decimal? pCost, string pRouteName, string pCarrierName) {
			Day = pDay;
			CustomerAcctId = pCustomerAcctId;
			CarrierAcctId = pCarrierAcctId;
			NodeId = pNodeId;
			Total = pTotal;
			Completed = pCompleted;
			InMinutes = pInMinutes;
			OutMinutes = pOutMinutes;
			Cost = pCost;
			RouteName = pRouteName;
			CarrierName = pCarrierName;
		}
	}
}
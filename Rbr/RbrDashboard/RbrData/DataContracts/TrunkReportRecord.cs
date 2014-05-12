using System.Runtime.Serialization;

namespace RbrData.DataContracts {
	[DataContract]
	public class TrunkReportRecord {
		[DataMember]
		public int? Day { get; private set; }
		[DataMember]
		public short? NodeId { get; private set; }
		[DataMember]
		public short CustomerAcctId { get; private set; }
		[DataMember]
		public string CustomerName { get; private set; }
		[DataMember]
		public short CarrierAcctId { get; private set; }
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

		public TrunkReportRecord(int? pDay, short? pNodeId, short pCustomerAcctId, string pCustomerName, short pCarrierAcctId, int? pTotal, int? pCompleted, int? pInMinutes, decimal? pOutMinutes, decimal? pCost, string pRouteName) {
			Day = pDay;
			CustomerAcctId = pCustomerAcctId;
			CustomerName = pCustomerName;
			CarrierAcctId = pCarrierAcctId;
			NodeId = pNodeId;
			Total = pTotal;
			Completed = pCompleted;
			InMinutes = pInMinutes;
			OutMinutes = pOutMinutes;
			Cost = pCost;
			RouteName = pRouteName;
		}
	}
}
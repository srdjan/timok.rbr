using System.Runtime.Serialization;
using RbrData.Helpers;

namespace RbrData.DataContracts {
	[DataContract]
	public class RouteReportRecord {
		public const string ROUTE_REPORT_HEADER = "Date, Node, CustomerName, Total, Completed, InMinutes, OutMinutes, Cost, RouteName, CarrierName, Asr, Acd\r\n";

		[DataMember]
		public string Date { get; private set; }

		[DataMember]
		public string Node { get; private set; }

		[DataMember]
		public string CustomerName { get; private set; }

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

		[DataMember]
		public string Asr { get; private set; }

		[DataMember]
		public string Acd { get; private set; }

		//Day node_id  route_id carrier_acct_id Total Completed InMinutes OutMinutes Cost Name  PopName
		public RouteReportRecord(int? pDay, string pNode, string pCustomerName, int? pTotal, int? pCompleted, int? pInMinutes, decimal? pOutMinutes, decimal? pCost, string pRouteName, string pCarrierName) {
			Date = TimokDate.ToDateTime((int)pDay).ToShortDateString();
			Node = pNode;
			CustomerName = pCustomerName;
			Total = pTotal;
			Completed = pCompleted;
			InMinutes = pInMinutes;
			OutMinutes = pOutMinutes;
			Cost = pCost;
			RouteName = pRouteName;
			CarrierName = pCarrierName;
			Asr = StatsCalc.GetASR(Total, Completed);
			Acd = StatsCalc.GetACD(OutMinutes, Completed);
		}

		public override string ToString() {
			return string.Format("{0},{1},{2},{3},{4},{5},{6},{7},{8},{9},{10},{11}", Date, Node, CustomerName, Total, Completed, InMinutes, OutMinutes, Cost, RouteName, CarrierName, Asr, Acd);
		}
	}
}
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace RbrData.DataContracts {
	[DataContract]
	public class ReportContainer {
		[DataMember]
		public string Error { get; private set; }

		[DataMember]
		public List<NodeReportRecord> NodeReport;

		[DataMember]
		public List<CustomerReportRecord> CustomerReport;

		[DataMember]
		public List<RouteReportRecord> RouteReport;

		[DataMember]
		public List<TrunkReportRecord> TrunkReport;

		public ReportContainer(List<NodeReportRecord> pNodeReport, string pError) {
			NodeReport = pNodeReport;
			Error = pError;
		}
		public ReportContainer(List<CustomerReportRecord> pCustomerReport, string pError) {
			CustomerReport = pCustomerReport;
			Error = pError;
		}
		public ReportContainer(List<RouteReportRecord> pRouteReport, string pError) {
			RouteReport = pRouteReport;
			Error = pError;
		}
		public ReportContainer(List<TrunkReportRecord> pTrunkReport, string pError) {
			TrunkReport = pTrunkReport;
			Error = pError;
		}
	}
}
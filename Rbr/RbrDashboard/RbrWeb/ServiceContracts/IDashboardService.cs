using System.ServiceModel;
using RbrData.DataContracts;

namespace RbrWeb.ServiceContracts {
	[ServiceContract(Namespace = "http://www.timok.com/services/v3.0", Name = "DashboardService")]
	public interface IDashboardService {
		[OperationContract(Name = "Login", Action = "LoginRequest", ReplyAction = "LoginResponse")]
		ReportContext Login();

		[OperationContract(Name = "GetNodeReport", Action = "GetNodesReportRequest", ReplyAction = "GetNodesReportResponse")]
		ReportContainer GetNodeReport(string pShortDateString);

		[OperationContract(Name = "GetCustomerReport", Action = "GetCustomerReportRequest", ReplyAction = "GetCustomerReportResponse")]
		ReportContainer GetCustomerReport(int pPartnerId, string pShortDateString);

		[OperationContract(Name = "GetRouteReport", Action = "GetRouteReportRequest", ReplyAction = "GetRouteReportResponse")]
		ReportContainer GetRouteReport(int pPartnerId, string pShortDateString);

		[OperationContract(Name = "GetTrunkReport", Action = "GetTrunkReportRequest", ReplyAction = "GetTrunkReportResponse")]
		ReportContainer GetTrunkReport(int pPartnerId, string pShortDateString);

		[OperationContract(Name = "Logger", Action = "LogRequest", ReplyAction = "LogResponse")]
		void Log(string pMessage);
	}
}
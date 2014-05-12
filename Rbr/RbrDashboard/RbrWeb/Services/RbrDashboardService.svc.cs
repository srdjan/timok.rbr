using System;
using System.Collections.Generic;
using System.ServiceModel.Activation;
using RbrCommon;
using RbrData;
using RbrData.DataContracts;
using RbrWeb.ServiceContracts;

namespace RbrWeb.Services {
	[AspNetCompatibilityRequirements(RequirementsMode = AspNetCompatibilityRequirementsMode.Allowed)]
	public class RbrDashboardService : RbrDashboardServiceBase, IDashboardService {
		readonly RbrDashboardServiceInternal serviceImpl;

		public RbrDashboardService() {
			serviceImpl = new RbrDashboardServiceInternal();	
		}

		public ReportContext Login() {
			if (loggedIn) {
				return serviceImpl.GetReportContext(user.PartnerId);
			}
			return null;
		}

		public ReportContainer GetNodeReport(string pShortDateString) {
			return serviceImpl.GetNodeReport(pShortDateString);
		}

		public ReportContainer GetCustomerReport(int pPartnerAcctId, string pShortDateString) {
			return serviceImpl.GetCustomerReport(pPartnerAcctId, pShortDateString);
		}

		public ReportContainer GetRouteReport(int pPartnerAcctId, string pShortDateString) {
			return serviceImpl.GetRouteReport(pPartnerAcctId, pShortDateString);
		}

		public ReportContainer GetTrunkReport(int pPartnerAcctId, string pShortDateString) {
			return serviceImpl.GetTrunkReport(pPartnerAcctId, pShortDateString);
		}

		public void Log(string pMessage) {
			if (pMessage != null) Logger.Log(pMessage);
		}

		//[PrincipalPermission(SecurityAction.Demand, Authenticated = true)]
		//public List<ProductSummary> GetSummaryReport(int pageNumber, int itemsPerPage, string filter, string sortBy, out int totalCount) {
		//  throw new System.NotImplementedException();
		//}
	}
}
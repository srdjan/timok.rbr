using System;
using Timok.Rbr.Core.Config;

namespace Timok.Rbr.BLL.ImportExport.DialPlan {
	public class DialPlanImportExportArgs {
		public ImportExportType ImportExportType;
		public short AccountId;
		public string AccountName;
		public int CallingPlanId;
		public int RoutingPlanId;
		public ImportExportFilter ImportExportFilter;
		public ViewContext Context;
		public readonly string FilePath;
		public bool PerMinute;
		public readonly DateTime From;
		public readonly DateTime To;

		public DialPlanImportExportArgs(ImportExportType pImportExportType,
		                                ImportExportFilter pImportExportFilter,
		                                string pFilePath,
		                                short pAccountId,
		                                string pAccountName,
		                                int pCallingPlanId,
		                                int pRoutingPlanId,
		                                ViewContext pContext, 
																		bool pPerMinute,
		                                DateTime pFrom,
		                                DateTime pTo) {
			ImportExportType = pImportExportType;
			AccountId = pAccountId;
			AccountName = pAccountName;
			CallingPlanId = pCallingPlanId;
			RoutingPlanId = pRoutingPlanId;
			ImportExportFilter = pImportExportFilter;
			FilePath = pFilePath;
			Context = pContext;
			PerMinute = pPerMinute;
			From = pFrom;
			To = pTo;
		}

	}
}
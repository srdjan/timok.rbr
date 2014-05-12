using System;
using Timok.Rbr.Core.Config;
using Timok.Rbr.DTO;

namespace Timok.Rbr.BLL.ImportExport.Cdr {
	[Serializable]
	public class CustomerCdrExportInfo : CdrExportInfo {
		public CustomerAcctDto CustomerAcct { get; set; }

		public CustomerCdrExportInfo(CustomerAcctDto pCustomerAcct, string pExportDirectory, DateTime pDateStart, DateTime pDateEnd, CdrExportMapDto pCdrExportMap, string pDecimalFormatString, ViewContext pContext, bool pWithRerating) : base(pExportDirectory, pDateStart, pDateEnd, pCdrExportMap, pDecimalFormatString, pContext, pWithRerating) {
			CustomerAcct = pCustomerAcct;
		}
	}
}
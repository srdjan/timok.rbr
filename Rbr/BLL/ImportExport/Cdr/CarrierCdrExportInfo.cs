using System;
using Timok.Rbr.Core.Config;
using Timok.Rbr.DTO;

namespace Timok.Rbr.BLL.ImportExport.Cdr {
	[Serializable]
	public class CarrierCdrExportInfo : CdrExportInfo {
		public CarrierAcctDto CarrierAcctDto { get; set; }

		public CarrierCdrExportInfo(CarrierAcctDto pCarrierAcct, string pExportDirectory, DateTime pDateStart, DateTime pDateEnd, CdrExportMapDto pCdrExportMap, string pDecimalFormatString, ViewContext pContext, bool pWithRerating) : base(pExportDirectory, pDateStart, pDateEnd, pCdrExportMap, pDecimalFormatString, pContext, pWithRerating) {
			CarrierAcctDto = pCarrierAcct;
		}
	}
}
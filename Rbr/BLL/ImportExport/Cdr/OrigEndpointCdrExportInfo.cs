using System;
using Timok.Rbr.Core.Config;
using Timok.Rbr.DTO;

namespace Timok.Rbr.BLL.ImportExport.Cdr {
	[Serializable]
	public class OrigEndpointCdrExportInfo : CdrExportInfo {
		public short OrigEndpointId { get; set; }

		public OrigEndpointCdrExportInfo(short pOrigEndpointId, string pExportDirectory, DateTime pDateStart, DateTime pDateEnd, CdrExportMapDto pCdrExportMap, string pDecimalFormatString, ViewContext pContext, bool pWithRerating) : base(pExportDirectory, pDateStart, pDateEnd, pCdrExportMap, pDecimalFormatString, pContext, pWithRerating) {
			OrigEndpointId = pOrigEndpointId;
		}
	}
}
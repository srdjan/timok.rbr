using System;
using Timok.Core;
using Timok.Rbr.Core.Config;
using Timok.Rbr.DTO;

namespace Timok.Rbr.BLL.ImportExport.Cdr {
	[Serializable]
	public class CdrExportInfo : ICdrExportInfo {
		CdrExportMapDto cdrExportMap;
		public CdrExportInfo() { }

		public CdrExportInfo(string pExportDirectory, DateTime pDateStart, DateTime pDateEnd, CdrExportMapDto pCdrExportMap, string pDecimalFormatString, ViewContext pContext, bool pWithRerating) {
			DateStart = pDateStart;
			DateEnd = pDateEnd;
			cdrExportMap = pCdrExportMap;
			ExportDirectory = pExportDirectory;
			DecimalFormatString = pDecimalFormatString;
			Context = pContext;
			WithRerating = pWithRerating;
		}

		public ViewContext Context { get; set; }

		public string FieldDelimiter { get { return cdrExportMap != null ? ((char) cdrExportMap.CdrExportDelimeter).ToString() : string.Empty; } }

		#region ICdrExportInfo Members

		public DateTime DateStart { get; set; }

		public DateTime DateEnd { get; set; }

		public CdrExportMapDto CdrExportMap { get { return cdrExportMap; } set { cdrExportMap = value; } }

		public string DecimalFormatString { get; set; }

		public string ExportDirectory { get; set; }

		public int TimokDateStart { get { return TimokDate.Parse(DateStart); } }

		public int TimokDateEnd { get { return TimokDate.Parse(DateEnd); } }

		public bool WithRerating { get; set; }

		#endregion
	}
}
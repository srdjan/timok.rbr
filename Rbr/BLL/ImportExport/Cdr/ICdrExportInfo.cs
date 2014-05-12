using System;
using Timok.Rbr.Core.Config;
using Timok.Rbr.DTO;

namespace Timok.Rbr.BLL.ImportExport.Cdr {
	public interface ICdrExportInfo {
		DateTime DateStart { get; set; }
		DateTime DateEnd { get; set; }
		CdrExportMapDto CdrExportMap { get; set; }
		string ExportDirectory { get; set; }
		string DecimalFormatString { get; set; }
		ViewContext Context { get; set; }
		int TimokDateStart { get; }
		int TimokDateEnd { get; }
		bool WithRerating { get; set; }
	}
}
using System;
using Timok.Core.BackgroundProcessing;
using Timok.Rbr.BLL.Controllers;
using Timok.Rbr.Core.Config;

namespace Timok.Rbr.BLL.ImportExport.Cdr {
	public class CdrExporter : ITask {
		BackgroundWorker host;

		#region ITask Members

		public BackgroundWorker Host { set { host = value; } }

		public void Run(object sender, WorkerEventArgs e) {
			var _args = (ICdrExportInfo) e.Argument;

			try {
				string _exportedFilePath;
				if (_args.Context == ViewContext.CdrExport) {
					_exportedFilePath = CdrExportController.Export(e.Argument as CdrExportInfo, host);
				}
				else if (_args.Context == ViewContext.Customer) {
					_exportedFilePath = CdrExportController.ExportCustomerAcct(e.Argument as CustomerCdrExportInfo, host);
				}
				else if (_args.Context == ViewContext.Carrier) {
					_exportedFilePath = CdrExportController.ExportCarrierAcct(e.Argument as CarrierCdrExportInfo, host);
				}
				else if (_args.Context == ViewContext.OrigEndpoint) {
					_exportedFilePath = CdrExportController.ExportOrigEndPoint(e.Argument as OrigEndpointCdrExportInfo, host);
				}
				else {
					throw new NotSupportedException(string.Format("ViewContext: {0} is not supported in this context" + _args.Context));
				}

				if (_exportedFilePath.Length > 0) {
					host.ReportStatus(string.Format("{0}Finished Exporting Cdrs to File:{1}{2}", Environment.NewLine, Environment.NewLine, _exportedFilePath));
				}

				e.Result = "Finished Exporting";
			}
			catch (Exception _ex) {
				e.Cancel = true;
				e.Result = new Exception(string.Format("{0}ERROR: Export failed. Error:{1}{2}", Environment.NewLine, Environment.NewLine, _ex.Message), _ex);
				host.ReportWorkCompleted(e.Result, e.Result as Exception, true);
			}
		}

		public void CancelAsync() { host.CancelAsync(); }

		#endregion
	}
}
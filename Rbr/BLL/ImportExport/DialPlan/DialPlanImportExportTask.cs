using System;
using System.Collections.Generic;
using System.IO;
using Timok.Core.BackgroundProcessing;
using Timok.Logger;
using Timok.Rbr.BLL.Entities;
using Timok.Rbr.Core;
using Timok.Rbr.Core.Config;

namespace Timok.Rbr.BLL.ImportExport.DialPlan {
	public class DialPlanImportExportTask : ITask {
		BackgroundWorker host;

		#region ITask Members

		public BackgroundWorker Host { set { host = value; } }

		//-- Runs on SoftSwitch/IVR nodes during Import task replication

		//-- Runs on Admin node
		public void Run(object sender, WorkerEventArgs pWorkerEventArgs) {
			try {
				var _args = (DialPlanImportExportArgs) pWorkerEventArgs.Argument;

				if (run(_args)) {
					var _currentNode = new Entities.CurrentNode();
					if (_args.ImportExportType == ImportExportType.Import && _currentNode.IsAdmin) {
						copyToPublishingFolder(_args);
					}
				}

				pWorkerEventArgs.Result = "Ok";
			}
			catch (Exception _ex) {
				pWorkerEventArgs.Cancel = true;
				pWorkerEventArgs.Result = new Exception(string.Format("{0}Import failed. Error:{1}{2}", Environment.NewLine, Environment.NewLine, _ex.Message), _ex);
				host.ReportWorkCompleted(pWorkerEventArgs.Result, pWorkerEventArgs.Result as Exception, true);
			}
		}

		public void CancelAsync() { host.CancelAsync(); }

		#endregion

		public void Run(string pImportFilePath) {
			var _args = getImportArgs(pImportFilePath);

			if (! run(_args)) {
				TimokLogger.Instance.LogRbr(LogSeverity.Critical, "DialPlanImportExport.Run(RemoteImport)", string.Format("Error while processing file={0}", pImportFilePath));
			}
		}

		//-------------------------- Private ------------------------------------------------------
		bool run(DialPlanImportExportArgs pArgs) {
			bool _result;

			if (pArgs.ImportExportType == ImportExportType.Import) {
				var _importer = new DialPlanImporter(host, pArgs);
				IList<CountryRecord> _countries = (new DialPlanFileParser(host)).Process(pArgs.ImportExportFilter, pArgs.FilePath);
				_result = _importer.Import(_countries);
			}
			else if (pArgs.ImportExportType == ImportExportType.Export) {
				var _exporter = new DialPlanExporter(host, pArgs);
				_result = _exporter.Export();
			}
			else {
				throw new Exception(string.Format("Unknown ImportExportType={0}", pArgs.ImportExportType));
			}
			return _result;
		}

		//-- File name convention: 
		//-- Rates: 2007-05-05.0001223.Import.Rates.AccountId.CallingPlanId.RoutingPlanId.Customer.PerMinute.pending
		//-- DialPlan: 2007-05-05.0001223.Import.DialPlan.AccountId.CallingPlanId.RoutingPlanId.Callingplan.PerMinute.pending
		//-- Both: 2007-05-05.0001223.Import.Both.AccountId.CallingPlanId.RoutingPlanId.Carrier.PerMinute.pending
		//
		//-- Sample file name: "c:\_DialCodes\2007-05-05.0001223.Import.Rates.10001.10001.10001.Customer.PerMinute.pending"
		public DialPlanImportExportArgs getImportArgs(string pImportFilePath) {
			string _fileName = Path.GetFileName(pImportFilePath);
			string[] _fields = _fileName.Split('.');

			var _filter = (ImportExportFilter) Enum.Parse(typeof (ImportExportFilter), _fields[3]);
			short _accountId = short.Parse(_fields[4]);
			int _callingPlanId = short.Parse(_fields[5]);
			int _routingPlanId = short.Parse(_fields[6]);
			var _context = (ViewContext) Enum.Parse(typeof (ViewContext), _fields[7], true);
			var _from = new DateTime(year(_fields[8]), month(_fields[8]), day(_fields[8]), 0, 0, 0);
			var _to = new DateTime(year(_fields[9]), month(_fields[9]), day(_fields[9]), 0, 0, 0);
			bool _perMinute = _fields[10] == "PerMinute";

			return new DialPlanImportExportArgs(ImportExportType.Import, _filter, pImportFilePath, _accountId, string.Empty, _callingPlanId, _routingPlanId, _context, _perMinute, _from, _to);
		}

		int year(string pDateString) { return int.Parse(pDateString.Substring(0, 4)); }

		int month(string pDateString) { return int.Parse(pDateString.Substring(5, 2)); }

		int day(string pDateString) { return int.Parse(pDateString.Substring(8, 2)); }

		void copyToPublishingFolder(DialPlanImportExportArgs pArgs) {
			var _sequence = RbrClient.Instance.GetNextSequence().ToString("D7");
			var _replicationFileName =
				string.Format("{0}.{1}.{2}.{3}.{4}.{5}.{6}.{7}.{8}.{9}.{10}.{11}{12}",
				              DateTime.Now.ToString("yyyy-MM-dd"),
				              _sequence,
				              "Import",
				              pArgs.ImportExportFilter,
				              pArgs.AccountId,
				              pArgs.CallingPlanId,
				              pArgs.RoutingPlanId,
				              pArgs.Context,
				              pArgs.From.ToString("yyyy-MM-dd"),
				              pArgs.To.ToString("yyyy-MM-dd"),
				              pArgs.PerMinute ? "PerMinute" : "PerIncrements",
				              "Rbr",
				              AppConstants.PendingExtension);

			var _targetNodes = Node.GetNodes(NodeRole.H323);
			if (_targetNodes == null) {
				TimokLogger.Instance.LogRbr(LogSeverity.Critical, "DialPLanImportExportTask.copyToPublishingFolder", "No Targets");
				return;
			}

			//-- copy to all local per Node replication folders
			foreach (Node _node in _targetNodes) {
				string _replicationFilePath = Path.Combine(Configuration.Instance.Folders.RbrPublishingFolder, _node.UserName);
				if (! Directory.Exists(_replicationFilePath)) {
					Directory.CreateDirectory(_replicationFilePath);
				}

				_replicationFilePath = Path.Combine(_replicationFilePath, _replicationFileName);
				File.Copy(pArgs.FilePath, _replicationFilePath);
			}
		}
	}
}
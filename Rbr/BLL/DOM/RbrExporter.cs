using System;
using System.IO;
using System.Threading;
using Timok.Logger;
using Timok.Rbr.BLL.Entities;
using Timok.Rbr.Core;
using Timok.Rbr.Core.Config;

namespace Timok.Rbr.BLL.DOM {
	public sealed class RbrExporter {
		readonly IConfiguration configuration;
		readonly ILogger T;
		readonly string pendingFilesFilter;
		readonly Thread workerThread;

		//-- constructors
		public RbrExporter(IConfiguration pConfiguration, ILogger pLogger) {
			configuration = pConfiguration;
			T = pLogger;
			pendingFilesFilter = "*." + AppConstants.Rbr + AppConstants.PendingExtension;

			workerThread = new Thread(filePublishingLoop) { IsBackground = true, Priority = ThreadPriority.Lowest };
			workerThread.Start();
			T.LogRbr(LogSeverity.Status, "RbrExporter.Ctor", string.Format("RbrExporter thread started, FileFilter={0}", pendingFilesFilter));
		}

		//-------------------------------------Private ----------------------------------------------------
		void filePublishingLoop() {
			while (true) {
				try {
					Thread.Sleep(1000);

					var _error = false;
					var _targetNodes = Node.GetNodes(new[] {NodeRole.H323, NodeRole.SIP});
					if (_targetNodes == null) {
						T.LogRbr(LogSeverity.Critical, "RbrExporter.fialPublishingLoop", "No Targets ? RbrExporter");
						return;
					}

					var _pendingFiles = Directory.GetFiles(configuration.Folders.AuditFolder, pendingFilesFilter);
					foreach (var _filePath in _pendingFiles) {
						if (FileHelper.IsOpen(_filePath, T.LogRbr)) {
							T.LogRbr(LogSeverity.Error, "RbrExporter.filePublishingLoop", string.Format("Error: OPEN: {0}" + _filePath));
							break;
						}

						//-- copy to all local per Node replication folders
						foreach (var _node in _targetNodes) {
							if (! copy(_filePath, _node)) {
								_error = true;
								break;
							}
						}

						if (_error) {
							T.LogRbr(LogSeverity.Error, "RbrExporter.filePublishingLoop", string.Format("Error: COPY: {0}" + _filePath));
							break;
						}
						FileHelper.AddExtension(_filePath, AppConstants.PublishedExtension);
					}
				}
				catch (Exception _ex) {
					T.LogRbr(LogSeverity.Critical, "RbrExporter.filePublishingLoop", string.Format("Exception:\r\n{0}", _ex));
				}
			}
		}

		bool copy(string pFilePath, Node pNode) {
			var _result = true;

			try {
				var _folder = Path.Combine(configuration.Folders.RbrPublishingFolder, pNode.UserName);
				if (! Directory.Exists(_folder)) {
					Directory.CreateDirectory(_folder);
				}
				var _targetFilePath = Path.Combine(_folder, Path.GetFileName(pFilePath));
				File.Copy(pFilePath, _targetFilePath);
			}
			catch (Exception _ex) {
				if (!_ex.Message.Contains("already exists.")) {
					T.LogRbr(LogSeverity.Critical, "RbrExporter.copy", string.Format("Exception:\r\n{0}", _ex));
					_result = false;
				}
			}
			return _result;
		}
	}
}

//		//-- Special case: 
//		//-- Disregard incoming Node
//		public static bool PublishLocally(Node pNode, string pFilePath) {
//			bool _result = false;
//
//			try {				//-- Create per node folders in Publishing mode
//				Node[] _targetNodes = Node.GetConsumerNodes(NodeRole.Server);
//				foreach (Node _node in _targetNodes) {
//					string _folder = Path.Combine(Folders.PublishingReplicationFolder, _node.UserName);
//					if ( ! Directory.Exists(_folder)) {
//						Directory.CreateDirectory(_folder);
//					}
//
//					string _targetFilePath = Path.Combine(_folder, Path.GetFileName(pFilePath));
//					File.Copy(pFilePath, _targetFilePath);
//				}
//				_result = true;
//			}
//			catch (Exception _ex) {
//				T.LogCritical(string.Format("Exception: {0} ", _ex));
//			}
//			return _result;
//		}
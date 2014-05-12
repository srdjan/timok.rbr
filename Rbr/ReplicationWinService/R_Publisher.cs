using System;
using System.IO;
using System.Threading;
using Timok.Logger;
using Timok.Rbr.BLL.Entities;
using Timok.Rbr.Core;
using Timok.Rbr.Core.Config;

namespace Timok.Rbr.Replication {
	public delegate bool PublisherDelegate(Node pServer, string pFilePath);

	internal sealed class R_Publisher {
		const int MAX_ERROR_COUNT = 10;
		readonly string fromFolder;
		readonly string name;
		readonly string pendingFilesFilter;
		readonly PublisherDelegate publisherDelegate;
		int errorCount;
		bool stop;
		Thread workerThread;

		public R_Publisher(string pName, string pFromFolder, PublisherDelegate pDelegate) {
			if (pDelegate == null) {
				throw new Exception("R_Publisher.Ctor | Invalid PublisherDelegate! ");
			}

			name = pName;
			fromFolder = pFromFolder;
			pendingFilesFilter = "*." + name + AppConstants.PendingExtension;
			publisherDelegate = pDelegate;
			stop = false;

			TimokLogger.Instance.LogRbr(LogSeverity.Status, "R_Publisher.Ctor", string.Format("Replication Publisher started for={0}, FileFilter={1}", name, pendingFilesFilter));
		}

		public string Name { get { return name; } }

		//TODO: add reseting errorCount to Gui Configuration(?) menu
		public int ErrorCount { set { errorCount = value; } }

		//-- constructors

		public void Start() {
			workerThread = new Thread(filePublishingLoop) {IsBackground = true, Priority = ThreadPriority.Lowest};
			workerThread.Start();
		}

		public void Stop() { stop = true; }

		//-------------------------------------Private ----------------------------------------------------
		void filePublishingLoop() {
			while (! stop) {
				try {
					Thread.Sleep(1000);

					var _targetServers = (new CurrentNode()).IsAdmin ? Node.GetNodes(new[] {NodeRole.H323, NodeRole.SIP}) : Node.GetNodes(new[] { NodeRole.Admin});
					if (_targetServers == null) {
						TimokLogger.Instance.LogRbr(LogSeverity.Debug, "R_Publisher.filePublishingLoop", "No target servers");
						continue;
					}

					foreach (var _server in _targetServers) {
						if (_server.Status != Status.Active) {
							continue;
						}
						var _folder = Path.Combine(fromFolder, _server.UserName);
						if (! Directory.Exists(_folder)) {
							Directory.CreateDirectory(_folder);
							continue;
						}

						errorCount = 0;
						var _nodeFiles = Directory.GetFiles(_folder, pendingFilesFilter);
						foreach (var _filePath in _nodeFiles) {
							if (FileHelper.IsOpen(_filePath, TimokLogger.Instance.LogRbr)) {
								TimokLogger.Instance.LogRbr(LogSeverity.Error, "R_Publisher.filePublishingLoop", string.Format("Publisher={0}, OPEN: {1}", name, _filePath));
								break;
							}
							while (++errorCount < MAX_ERROR_COUNT) {
								if (! publisherDelegate(_server, _filePath)) {
									continue;
								}
								FileHelper.AddExtension(_filePath, AppConstants.PublishedExtension);
								errorCount = 0;
								break;
							}
						}
					}
				}
				catch (Exception _ex) {
					TimokLogger.Instance.LogRbr(LogSeverity.Critical, "R_Publisher.filePublishingLoop", string.Format("Publisher={0}, Exception:\r\n{1}", name, _ex));
				}
			}
		}
	}
}
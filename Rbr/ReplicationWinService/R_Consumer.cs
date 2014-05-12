using System;
using System.IO;
using System.Threading;
using Timok.Logger;
using Timok.Rbr.Core;
using Timok.Rbr.Core.Config;

namespace Timok.Rbr.Replication {
	public delegate bool ConsumerDelegate(string pFileName);

	internal sealed class R_Consumer {
		readonly ConsumerDelegate consumerDelegate;
		readonly string fromFolder;
		readonly string name;
		readonly string pendingFilesFilter;
		bool stop;
		Thread workerThread;

		public R_Consumer(string pName, string pFromFolder, ConsumerDelegate pDelegate) {
			if (pDelegate == null) {
				throw new Exception("R_Consumer.Ctor | Invalid ConsumerDelegate! ");
			}

			name = pName;
			fromFolder = pFromFolder;
			pendingFilesFilter = "*." + name + AppConstants.PendingExtension;
			consumerDelegate = pDelegate;
			stop = false;

			TimokLogger.Instance.LogRbr(LogSeverity.Status, "R_Consumer.Ctor", string.Format("Replication Consumer started, Consumer={0}, FileFilter={1}", name, pendingFilesFilter));
		}

		public string Name { get { return name; } }

		public void Start() {
			workerThread = new Thread(fileConsumingLoop)
			               	{
			               		Name = Name,
			               		IsBackground = true,
			               		Priority = ThreadPriority.Lowest
			               	};
			workerThread.Start();
		}

		public void Stop() { stop = true; }

		//-------------------------------------Private ----------------------------------------------------
		void fileConsumingLoop() {
			TimokLogger.Instance.LogRbr(LogSeverity.Status, "R_Consumer.fileConsumingLoop", string.Format("Consumer={0}, entering fileConsumingLoop", name));
			while (! stop) {
				try {
					Thread.Sleep(1000);

					string[] _nodeFiles = Directory.GetFiles(fromFolder, pendingFilesFilter);
					foreach (string _filePath in _nodeFiles) {
						if (FileHelper.IsOpen(_filePath, TimokLogger.Instance.LogRbr)) {
							TimokLogger.Instance.LogRbr(LogSeverity.Error, "R_Consumer.fileConsumingLoop", string.Format("Consumer={0}, IsOpen file: {1}", name, _filePath));
							break;
						}
						if (! consumerDelegate(_filePath)) {
							FileHelper.AddExtension(_filePath, AppConstants.ErrorExtension);
							break;
						}
						FileHelper.AddExtension(_filePath, AppConstants.ConsumedExtension);
					}
				}
				catch (Exception _ex) {
					TimokLogger.Instance.LogRbr(LogSeverity.Critical, "R_Consumer.fileConsumingLoop", string.Format("Consumer={0}, Exception:\r\n{1}", name, _ex));
				}
			}
		}
	}
}
using System.IO;
using Timok.Logger;

namespace Timok.Rbr.Core {
	public class FileWatcher {
		readonly string targetFolder;
		FileSystemWatcher fileWatcher;
		LogDelegate log;

		public FileWatcher(string pTargetFolder, LogDelegate pLog) {
			log = pLog;
			targetFolder = pTargetFolder;
			if (! Directory.Exists(targetFolder)) {
				Directory.CreateDirectory(targetFolder);
			}
		}

		public void Start(FileSystemEventHandler pFileChangedEventHandler, string pFileName) {
			Stop();

			fileWatcher = new FileSystemWatcher();
			fileWatcher.Path = targetFolder;
			fileWatcher.Filter = pFileName;
			fileWatcher.IncludeSubdirectories = false;
			fileWatcher.NotifyFilter = NotifyFilters.LastWrite;
			fileWatcher.Changed += pFileChangedEventHandler;
			fileWatcher.Error += errorHandler;
			fileWatcher.EnableRaisingEvents = true;
		}

		public void Stop() {
			if (fileWatcher != null) {
				try {
					fileWatcher.Dispose();
				}
				catch {}
				fileWatcher = null;
			}
		}

		void errorHandler(object pSender, ErrorEventArgs pArgs) {
			fileWatcher.EnableRaisingEvents = false;
			log(LogSeverity.Critical, "FileWatcher.errorHandler", string.Format("Exception:\r\n{0}", pArgs.GetException()));
			fileWatcher.EnableRaisingEvents = true;
		}
	}
}
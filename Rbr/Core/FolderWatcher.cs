using System.IO;
using Timok.Logger;

namespace Timok.Rbr.Core {
	public delegate void FileRenamedHandler(object pSender, RenamedEventArgs pArgs);

	public class FolderWatcher {
		readonly string targetFolder;
		FileSystemWatcher folderWatcher;
		LogDelegate log;

		public FolderWatcher(string pTargetFolder, LogDelegate pLog) {
			targetFolder = pTargetFolder;
			if (! Directory.Exists(targetFolder)) {
				Directory.CreateDirectory(targetFolder);
			}
			log = pLog;
		}

		public void Start(FileRenamedHandler pFileRenamedHandler, string pFilter) {
			Stop();

			folderWatcher = new FileSystemWatcher();
			folderWatcher.Path = targetFolder;
			folderWatcher.Filter = pFilter;
			folderWatcher.IncludeSubdirectories = false;
			folderWatcher.NotifyFilter = NotifyFilters.FileName;
			folderWatcher.Renamed += new RenamedEventHandler(pFileRenamedHandler);
			folderWatcher.Error += errorHandler;
			folderWatcher.EnableRaisingEvents = true;
		}

		public void Stop() {
			if (folderWatcher != null) {
				try {
					folderWatcher.Dispose();
				}
				catch {}
				folderWatcher = null;
			}
		}

		void errorHandler(object pSender, ErrorEventArgs pArgs) {
			folderWatcher.EnableRaisingEvents = false;
			log(LogSeverity.Critical, "FolderWatcher.errorHandler", string.Format("Exception:\r\n{0}", pArgs.GetException()));
			folderWatcher.EnableRaisingEvents = true;
		}
	}
}
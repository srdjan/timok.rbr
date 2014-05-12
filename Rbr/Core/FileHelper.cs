using System;
using System.IO;
using System.Threading;
using Timok.Logger;

namespace Timok.Rbr.Core {
	public class FileHelper {
		public static void Rename(string pFromFilePath, string pToFilePath, LogDelegate pLog) {
			if (IsOpen(pFromFilePath, pLog)) {
				return;
			}

			var _completed = false;
			for (var _i = 0; _i < 10; _i++) {
				try {
					File.Move(pFromFilePath, pToFilePath);
					_completed = true;
					break;
				}
				catch (Exception _ex) {
					pLog(LogSeverity.Error, "", string.Format("File.Rename: {0}, Exception:\r\n{1}", pFromFilePath, _ex));
					Thread.Sleep(500);
				}
			}
			if (! _completed) {
				throw new Exception(string.Format("FileHelper.Rename, All File.Rename retries Unsucesfull! {0}", pFromFilePath));
			}
		}

		public static bool AddExtension(string pFilePath, string pExt) {
			try {
				File.Move(pFilePath, string.Format("{0}{1}", pFilePath, pExt));
			}
			catch (Exception _ex) {
				throw new Exception(string.Format("FileHelper.AddExtension, File: {0}, Exception:\r\n{1}", pFilePath, _ex));
			}
			return true;
		}

		public static bool IsOpen(string pFilePath, LogDelegate pLog) {
			var _completed = true;

			for (var _i = 0; _i < 10; _i++) {
				try {
					using (var _fs = File.Open(pFilePath, FileMode.Open, FileAccess.ReadWrite, FileShare.None)) {
						_fs.Close();
						_completed = false;
						break;
					}
				}
				catch (Exception _ex) {
					pLog(LogSeverity.Error, "FileHelper.IsOpen", string.Format("File {0} Open, Exception:\r\n{1}", pFilePath, _ex));
					Thread.Sleep(500);
				}
			}
			return _completed;
		}

		public static string CleanupInvalidDirectoryCharacters(string pDirectory, char pReplaceInvalidWith) {
			var _cleanDirectory = pDirectory;
			var _invalidPathChars = Path.GetInvalidPathChars();
			foreach (var _invaldChar in _invalidPathChars) {
				_cleanDirectory = _cleanDirectory.Replace(_invaldChar, pReplaceInvalidWith);
			}

			return _cleanDirectory;
		}

		public static string CleanupInvalidFileNameCharacters(string pFileName, char pReplaceInvalidWith) {
			var _cleanFileName = pFileName;
			var _invalidFileChars = Path.GetInvalidFileNameChars();
			foreach (var _invaldChar in _invalidFileChars) {
				_cleanFileName = _cleanFileName.Replace(_invaldChar, pReplaceInvalidWith);
			}

			return _cleanFileName;
		}
	}
}
using System;
using System.IO;
using System.Threading;

namespace Timok.Core {
	public class DualFileWriter {
		static readonly object padlock = new object();
		readonly string copyToPath;
		readonly int fileLifeTime;
		readonly string fileNameFormat;
		readonly string fileNameSuffix;
		readonly string filePath;

		readonly FileWriter[] fwArray;
		readonly Timer recycleTimer;
		int index;
		int prevIndex;

		public DualFileWriter(string pTargetFilePath, string pCopyToPath, string pFileNameFormat, string pFileNameSuffix, int pFileLifeTime) {
			filePath = pTargetFilePath;
			copyToPath = pCopyToPath;
			fileNameFormat = pFileNameFormat;
			fileNameSuffix = pFileNameSuffix;
			fileLifeTime = pFileLifeTime;

			//-- check for TargetFolder existance:
			if (! Directory.Exists(filePath)) {
				Directory.CreateDirectory(filePath);
			}
			//-- check for CopyToFolder existance:
			if (! Directory.Exists(copyToPath)) {
				Directory.CreateDirectory(copyToPath);
			}

			//-- Initialize string writer array:
			index = 0;
			fwArray = new FileWriter[2];

			//-- create and open first file writer
			fwArray[0] = new FileWriter();
			fwArray[0].Open(filePath, fileName);

			//-- create second file writer
			fwArray[1] = new FileWriter();

			//-- initialize timer
			var _timerInterval = recycleTimerInterval;
			recycleTimer = new Timer(switchAndCopy, null, _timerInterval, _timerInterval);
		}

		int recycleTimerInterval {
			get {
				var _minutes = DateTime.Now.Minute;
				var _seconds = DateTime.Now.Second;

				//-- calculate timeout in minutes
				var _timeout = fileLifeTime - _minutes%fileLifeTime;
				if (_timeout == 0) {
					_timeout = fileLifeTime;
				}

				//-- convert minutes to seconds
				_timeout *= 60;

				//-- add 20 second offset
				if (_seconds <= 20) {
					_timeout += 20;
				}
				else if (_seconds > 20) {
					_timeout -= _seconds - 20;
				}

				//-- convert to miliseconds
				_timeout *= 1000;
				return _timeout;
			}
		}

		string fileName {
			get {
				var _dateTime = DateTime.Now.Subtract(new TimeSpan(0, DateTime.Now.Minute%fileLifeTime, 0));
				return _dateTime.ToString(fileNameFormat) + fileNameSuffix;
			}
		}

		public int WriteLine(string pLine) {
			lock (padlock) {
				fwArray[index].WriteLine(pLine);
				fwArray[index].Flush();
			}
			return 0;
		}

		void switchAndCopy(object pState) {
			Thread.Sleep(0); //Fix for SP1 bug, when timer stops fiaring after a while

			try {
				recycleTimer.Change(Timeout.Infinite, Timeout.Infinite);

				//-- switch files first:
				lock (padlock) {
					if (index == 0) {
						index = 1;
						prevIndex = 0;
					}
					else {
						index = 0;
						prevIndex = 1;
					}

					//-- open next file:
					fwArray[index].Open(filePath, fileName);
				}

				//-- close previous file:
				try {
					if (fwArray[prevIndex].IsOpen) {
						fwArray[prevIndex].Close();
					}

					if (! fwArray[prevIndex].IsDirty) {
						fwArray[prevIndex].Delete();
					}
					else { //-- copy previous file
						var _from = Path.Combine(filePath, fwArray[prevIndex].FileName);
						var _to = Path.Combine(copyToPath, fwArray[prevIndex].FileName);
						File.Copy(_from, _to);
					}
				}
				catch (Exception _ex) {
					throw new Exception(string.Format("DualFileWriter.Ctor: Closing {0}, index={1} Exception\r\n{2}", fwArray[prevIndex].FileName, prevIndex, _ex));
				}
			}
			finally {
				var _timerInterval = recycleTimerInterval;
				recycleTimer.Change(_timerInterval, _timerInterval);
			}
		}

		public void Close() {
			fwArray[0].Close();
			fwArray[1].Close();
		}
	}

	//------------------ Internal helper class -------------------------------------
	public class FileWriter : IDisposable {
		public string FileName;
		public string FilePath;
		bool isDirty;

		bool isOpen;
		StreamWriter sw;
		TextWriter tw;

		public bool IsOpen {
			get { return isOpen; }
		}

		public bool IsDirty {
			get { return isDirty; }
		}

		public void Dispose() {
			tw.Close();
			sw.Close();
			isOpen = false;
		}

		public void Open(string pFilePath, string pFileName) {
			try {
				isDirty = false;
				isOpen = true;

				FilePath = pFilePath;
				FileName = pFileName;
				sw = new StreamWriter(new FileStream(Path.Combine(pFilePath, pFileName), FileMode.Append, FileAccess.Write, FileShare.Read));
				tw = TextWriter.Synchronized(sw);
			}
			catch {
				isOpen = false;
			}
		}

		public void Flush() {
			tw.Flush();
		}

		public void WriteLine(string pData) {
			sw.WriteLine(pData);
			isDirty = true;
		}

		public void Write(string pText) {
			sw.Write(pText);
			isDirty = true;
		}

		public void Write(char[] pText) {
			sw.Write(pText);
			isDirty = true;
		}

		public void Delete() {
			Close();
			File.Delete(Path.Combine(FilePath, FileName));
		}

		public void Close() {
			Dispose();
		}
	}
}
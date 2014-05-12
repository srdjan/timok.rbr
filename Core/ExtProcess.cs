using System;
using System.IO;
using System.Diagnostics;

namespace Timok.Core {
	public class ExtProcessWindowsServiceWraper {
		TimokProcess process;
		readonly ProcessPriorityClass priority;
		readonly string name;
		readonly string filePath;
		readonly string workingFolder;
		readonly string args;

		public ExtProcessWindowsServiceWraper(string pProcessFilePath, string pArgs) {
			filePath = pProcessFilePath;
			workingFolder = Path.GetDirectoryName(filePath);
			name = Path.GetFileName(filePath);
			args = pArgs;
			priority = ProcessPriorityClass.High;
		}

		public void Start() {
			if (File.Exists(filePath)) {
				process = new TimokProcess(workingFolder, name, args, priority);
				process.Start();
			}
			else {
				throw new Exception(string.Format(name + ".Start: {0} NOT Started, file={1} NOT found", name, filePath));
			}
		}

		public void Stop() {
			if (File.Exists(filePath) && process != null) {
				process.Stop();
			}
		}
	}
}
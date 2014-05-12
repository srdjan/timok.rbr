using System;
using System.Diagnostics;
using System.Text;

namespace Timok.Core {
	public class TimokProcess : Process {
		readonly ProcessPriorityClass priority;

    public bool Enabled { get; private set; }

		public TimokProcess(string pWorkingDir, string pFileName, string pArguments, ProcessPriorityClass pPriority) {
			try {
				priority = pPriority;

				StartInfo.WorkingDirectory = pWorkingDir;
				StartInfo.FileName = pFileName;
				StartInfo.Arguments = pArguments;
				if (!string.IsNullOrEmpty(StartInfo.WorkingDirectory) && !string.IsNullOrEmpty(StartInfo.FileName)) {
					Enabled = true;
				}
				else {
					Enabled = false;
				}
			}
			catch(Exception _ex) {
				throw new ApplicationException("TimokProcess.Ctor | Exception:", _ex);
			}
		}
		
		public new bool Start() {
			EnableRaisingEvents = true;
			if ( ! base.Start()) {
				return false;
			}

			PriorityClass = priority;
			return true;
		}

		public void Stop() {
			Kill();
		}

		public override string ToString() {
			var _strBldr = new StringBuilder();
			_strBldr.Append("Process Name : [" + StartInfo.FileName + "]\r\n");
			_strBldr.Append("File Name : [" + StartInfo.FileName + "]\r\n");
			_strBldr.Append("Working Dir : [" + StartInfo.WorkingDirectory + "]\r\n");
			_strBldr.Append("Start Args : [" + StartInfo.Arguments + "] .");

			return _strBldr.ToString();
		}
	}
}

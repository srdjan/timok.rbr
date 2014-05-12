using System;
using System.Diagnostics;
using System.IO;

namespace Timok.Core 
{
	public class CMDHelper {
		protected CMDHelper() {}

		public static string Run(string exeName, string arguments, ProcessPriorityClass priorityClass, int timeoutSeconds){
			StreamReader outputStream = StreamReader.Null;
			string output = "";
			bool success = false;

			try{
				Process _cmdProcess = new Process();

				ProcessStartInfo _startInfo = new ProcessStartInfo();
				_startInfo.CreateNoWindow = true;
				_startInfo.UseShellExecute = false;
				_startInfo.WindowStyle = ProcessWindowStyle.Hidden;
				_startInfo.RedirectStandardOutput = true;

				_startInfo.FileName = exeName;
				_startInfo.Arguments = arguments;

				_cmdProcess.StartInfo = _startInfo;
				_cmdProcess.Start();
				_cmdProcess.PriorityClass = priorityClass;

				if (0 == timeoutSeconds) {
					outputStream = _cmdProcess.StandardOutput;
					output = outputStream.ReadToEnd();
					_cmdProcess.WaitForExit();
				}
				else {
					success = _cmdProcess.WaitForExit(timeoutSeconds * 1000);
	
					if (success) {
						outputStream = _cmdProcess.StandardOutput;
						output = outputStream.ReadToEnd();
					}
					else {
						output = "Timed out at " + timeoutSeconds + " seconds waiting for " + exeName + " to exit.";
					}
				}
			}
			catch(Exception ex) {
				throw (new Exception("An error occurred running " + exeName + ".", ex));
			}
			finally {
				outputStream.Close();
			}

			return output;
		}


	}
}

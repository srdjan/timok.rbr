using System;
using Timok_IVR;

namespace Test.Main {
	internal class Program {
		static void Main(string[] args) {
			IVR _ivr = new IVR();
			try {
				_ivr.Start();
				Console.WriteLine("Initialized and started in IVR mode...\r\n Press ENTER to exit.");
				Console.ReadLine();
			}
			catch (Exception _ex) {
				Console.WriteLine(string.Format("Exception while Initializing...{0}\r\n Press ENTER to exit.", _ex));
				Console.ReadLine();
				return;
			}
			finally {
				_ivr.Stop();
			}

			Console.WriteLine("Exit?");
			Environment.Exit(0);
		}
	}
}
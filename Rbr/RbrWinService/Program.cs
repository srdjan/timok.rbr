using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Configuration.Install;
using System.Diagnostics;
using System.Reflection;
using System.ServiceProcess;

namespace Timok.Rbr.WinService {
	internal static class Program {
		static void Main(string[] pArgs) {
			try {
				AppDomain.CurrentDomain.UnhandledException += onUnhandledException;
				EventLog.WriteEntry("Application", "Application level Exception handler installed...", EventLogEntryType.Information, 1);

				var _rbrWinService = new RbrWinService(Assembly.GetEntryAssembly().GetName().Name, ConfigurationManager.AppSettings.Get("RbrRoot"));
				EventLog.WriteEntry("Application", "RbrService Created...", EventLogEntryType.Information, 1);

				if (pArgs.Length == 0) { // no arguments, run as winservice
					ServiceBase.Run(new ServiceBase[1] {_rbrWinService});
					EventLog.WriteEntry("Application", "Application ended...", EventLogEntryType.Information, 1);
					Environment.Exit(1);
				}
				
				switch (pArgs[0].ToLower()) {
					case "/uninstall":
					case "/install":
						runInstaller(pArgs);
						break;
					case "/console":
						runInConsoleMode(pArgs, _rbrWinService);
						break;
					default:
						Console.WriteLine("Error, Valid usage:\r\n" + "/console version,  /install or /uninstall, /migrate. Press any ENTER to finish...");
						break;
				}
			}
			catch (Exception _ex) {
				Console.WriteLine("Timok.Rbr, Exception in [{0}] mode: {1}", pArgs[0], _ex);
				EventLog.WriteEntry("Application", string.Format("\r\nTimok.Rbr, mode={0}, Exception:\r\n{1}", pArgs[0], _ex), EventLogEntryType.Error, 1);
			}
			Console.WriteLine("\r\nApplication ended...");
			EventLog.WriteEntry("Application", "Application ended...", EventLogEntryType.Information, 1);
			Environment.Exit(1);
		}

		//---------------------- Private ---------------------------------------------------
		static void runInstaller(string[] pArgs) {
			if (pArgs.Length != 2) {
				Console.WriteLine("Usage: 'appname /install version' or '/uninstall version'");
				return;
			}

			var _ti = new TransactedInstaller();
			var _cmdLine = new string[1] { string.Format("/assemblypath={0}", Assembly.GetExecutingAssembly().Location) };
			_ti.Context = new InstallContext(string.Empty, _cmdLine);
			_ti.Installers.Add(new RbrServiceInstaller(pArgs[1], pArgs[2]));

			if (pArgs[0].ToLower() == "/install") {
				_ti.Install(new Hashtable());
			}
			else if (pArgs[0].ToLower() == "/uninstall") {
				_ti.Uninstall(null);
			}
		}

		static void runInConsoleMode(ICollection<string> pArgs, RbrWinService pRbrWinService) {
			if (pArgs.Count != 1) {
				Console.WriteLine("Usage: 'appname /console");
				return;
			}

			pRbrWinService.Start(null);

			Console.WriteLine("\r\nStarted... Press ENTER to stop.");
			Console.ReadLine();
			Console.WriteLine("Stopping...");

			pRbrWinService.End();
		}

		// Global Exception Handler
		static void onUnhandledException(object pSender, UnhandledExceptionEventArgs pException) {
			var _ex = pException.ExceptionObject as Exception;
			if (_ex != null) {
				EventLog.WriteEntry("Application", string.Format("\r\nTimok.Rbr, Unhandled Exception:\r\n{0}", _ex), EventLogEntryType.Error, 1);
				Environment.Exit(1);
			}
		}
	}
}
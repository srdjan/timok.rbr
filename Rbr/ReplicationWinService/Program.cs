using System;
using System.Collections;
using System.Configuration.Install;
using System.Diagnostics;
using System.Reflection;
using System.ServiceProcess;

namespace Timok.Rbr.Replication {
	internal static class Program {
		static void Main(string[] pArgs) {
			AppDomain.CurrentDomain.UnhandledException += onUnhandledException;
			EventLog.WriteEntry("Application", "Application level Exception handler installed...", EventLogEntryType.Information, 1);

			var _assemblyName = Assembly.GetEntryAssembly().GetName().Name;
			var _location = Assembly.GetEntryAssembly().Location;
			var _replicationService = new ReplicationService(_assemblyName, _location);

			if (pArgs.Length == 0) {
				// no arguments, just run		
				ServiceBase.Run(new ServiceBase[] {_replicationService});
				return;
			}

			string _opt = pArgs[0];
			try {
				if (_opt.ToLower() == "/install" || _opt.ToLower() == "/uninstall") {
					if (pArgs.Length != 2) {
						Console.WriteLine("Usage: appname /install (or /uninstall) version");
						return;
					}

					//-- Create installers
					var _ti = new TransactedInstaller();
					var _cmdLine = new[] {string.Format("/assemblypath={0}", Assembly.GetExecutingAssembly().Location)};
					_ti.Context = new InstallContext(string.Empty, _cmdLine);
					_ti.Installers.Add(new ReplicationServiceInstaller(pArgs[1]));

					if (_opt.ToLower() == "/install") {
						_ti.Install(new Hashtable());
					}
					else if (_opt.ToLower() == "/uninstall") {
						_ti.Uninstall(null);
					}
				}
				else if (_opt.ToLower() == "/console") {
					_replicationService.Start(null);
					Console.WriteLine("\r\nStarted... Press ENTER to stop.");

					Console.ReadLine();
					Console.WriteLine("Stopping...");

					_replicationService.Stop();
				}
				else {
					Console.WriteLine("Error, Valid Arguments are: /console,  /install or /uninstall. Press any ENTER to finish...");
				}
			}
			catch (Exception _ex) {
				Console.WriteLine("Timok.Rbr, Exception in [" + _opt + "] mode: " + _ex);
			}
		}

		/// <summary>
		/// Global Exception Handler
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		static void onUnhandledException(object sender, UnhandledExceptionEventArgs e) {
			var _ex = e.ExceptionObject as Exception;
			if (_ex != null) {
				EventLog.WriteEntry("Application", string.Format("\r\nTimok.Rbr, Unhandled Exception:\r\n{0}", _ex), EventLogEntryType.Error, 1);
				Environment.Exit(1);
			}
		}
	}
}
using System;
using System.Collections;
using System.Configuration.Install;
using System.Diagnostics;
using System.Reflection;
using System.ServiceProcess;
using Rbr.WebAPI.WinService;

namespace Timok.Core {
	public class RbrWinService : ServiceBase {
		private readonly string processFilePath;
		private readonly IService service;
		private readonly string[] args;

		public RbrWinService(IService pService, string[] pArgs) {
			ServiceName = Assembly.GetEntryAssembly().GetName().Name;
			processFilePath = Assembly.GetEntryAssembly().Location;
			service = pService;
			args = pArgs;
			EventLog.WriteEntry("Application", string.Format("RbrWinService.Ctor Process Name={0}, Path={1}", ServiceName, processFilePath));
			runService(args);
		}

		//--------------------------------- Protected -------------------------------------------------
		protected override void OnStart(string[] pArgs) {
			try {
				service.Start();
			}
			catch (Exception _ex) {
				EventLog.WriteEntry("Application", string.Format("RbrWinService.Start, Exception: " + _ex));
			}
		}

		protected override void OnStop() {
			service.Stop();
		}

		//----------------------------------- Private -------------------------------------------------
		private void runService(string[] pArgs) {
			AppDomain.CurrentDomain.UnhandledException += onUnhandledException;
			EventLog.WriteEntry("Application", "RbrWinService.Start, Application level Exception handler installed...", EventLogEntryType.Information, 1);

			//-- check for arguments, if no arguments given, just run		
			if (pArgs.Length == 0) {
				Run(new ServiceBase[1] {this});
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
					var _cmdLine = new string[1] {string.Format("/assemblypath={0}", Assembly.GetExecutingAssembly().Location)};
					_ti.Context = new InstallContext(string.Empty, _cmdLine);
					_ti.Installers.Add(new RbrServiceInstaller(new[] {"MSSQL$TRBR", "SharedAccess", "Timok.Rbr." + pArgs[1]}, pArgs[1]));

					if (_opt.ToLower() == "/install") {
						_ti.Install(new Hashtable());
					}
					else if (_opt.ToLower() == "/uninstall") {
						_ti.Uninstall(null);
					}
				}
				else if (_opt.ToLower() == "/console") {
					OnStart(args);
					Console.WriteLine("\r\nStarted... Press ENTER to stop.");
					Console.ReadLine();
					Console.WriteLine("Stopping...");
					OnStop();
				}
				else {
					Console.WriteLine("Error, Valid Arguments are: /console,  /install or /uninstall. Press any ENTER to finish...");
				}
			}
			catch (Exception _ex) {
				Console.WriteLine("Timok.Rbr, Exception in [" + _opt + "] mode: " + _ex);
			}
		}

		//-- Global Exception Handler
		private void onUnhandledException(object sender, UnhandledExceptionEventArgs e) {
			var _ex = e.ExceptionObject as Exception;
			if (_ex != null) {
				EventLog.WriteEntry("Application", string.Format("\r\n{0}, Unhandled Exception:\r\n{1}", ServiceName, _ex), EventLogEntryType.Error, 1);
				Environment.Exit(1);
			}
		}
	}
}
using System;
using System.Web;
using System.Reflection;

namespace Timok.Rbr.DAL {

	public class ConnectionStringExt {

		private ConnectionStringExt() { }

		public static string GetMachineNameAppInfo() {
				string _machineName = string.Empty;
				string _appInfo = string.Empty;

				try {
					_machineName = Environment.MachineName;
					if (HttpContext.Current == null) {	//win exe
						_appInfo = AppDomain.CurrentDomain.FriendlyName + 
							" v" + Assembly.GetEntryAssembly().GetName().Version;
					}
					else {	//web
						_appInfo = Assembly.GetExecutingAssembly().GetName().Name + 
							" v" + Assembly.GetExecutingAssembly().GetName().Version;
					}
				} 
				catch {
					return string.Empty;
				}
				return "Workstation ID=" + _machineName + ";Application Name=" + _appInfo + ";";
		}
	}
}

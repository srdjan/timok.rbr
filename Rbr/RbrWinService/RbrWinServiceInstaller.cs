using System.ComponentModel;
using System.Configuration.Install;
using System.ServiceProcess;

namespace Timok.Rbr.WinService {
	[RunInstaller(true)]
	internal class RbrServiceInstaller : Installer {
		internal RbrServiceInstaller(string pVersion, string pIsSoftSwitch) {
			var _spi = new ServiceProcessInstaller
			           	{
			           		Account = ServiceAccount.LocalSystem,
			           		Password = null,
			           		Username = null
			           	};
			Installers.Add(_spi);

			var _si = new ServiceInstaller
			          	{
			          		ServiceName = ("Timok.Rbr." + pVersion),
			          		DisplayName = ("Timok.Rbr." + pVersion),
			          		StartType = ServiceStartMode.Automatic
			          	};
			if (pIsSoftSwitch.ToLower() == "true") {
				_si.ServicesDependedOn = new string[4] {"acuresmgr", "MSSQL$TRBR", "Terminal Services", "SharedAccess"};
			}
			else {
				_si.ServicesDependedOn = new string[3] { "MSSQL$TRBR", "Terminal Services", "SharedAccess" };
			}
			Installers.Add(_si);
		}
	}
}
using System.ComponentModel;
using System.Configuration.Install;
using System.ServiceProcess;

namespace Rbr.WebAPI.WinService {
	[RunInstaller(true)]
	internal class RbrServiceInstaller : Installer {
		internal RbrServiceInstaller(string[] pDependedOn, string pVersion) {
			var _spi = new ServiceProcessInstaller();
			_spi.Account = ServiceAccount.LocalSystem;
			_spi.Password = null;
			_spi.Username = null;

			var _si = new ServiceInstaller();
			_si.ServiceName = "Timok.Rbr.WebAPI." + pVersion;
			_si.DisplayName = "Timok.Rbr.WebAPI." + pVersion;
			_si.StartType = ServiceStartMode.Automatic;
			_si.ServicesDependedOn = pDependedOn;

			Installers.Add(_spi);
			Installers.Add(_si);
		}
	}
}
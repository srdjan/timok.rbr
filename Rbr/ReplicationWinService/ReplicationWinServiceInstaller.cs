using System.ComponentModel;
using System.Configuration.Install;
using System.ServiceProcess;

namespace Timok.Rbr.Replication {
	[RunInstaller(true)]
	internal class ReplicationServiceInstaller : Installer {
		internal ReplicationServiceInstaller(string pVersion) {
			var _spi = new ServiceProcessInstaller();
			_spi.Account = ServiceAccount.LocalSystem;
			_spi.Password = null;
			_spi.Username = null;

			var _si = new ServiceInstaller();
			_si.ServiceName = "Timok.Replication." + pVersion;
			_si.DisplayName = "Timok.Replication." + pVersion;
			_si.StartType = ServiceStartMode.Automatic;
			_si.ServicesDependedOn = new string[3] {"MSSQL$TRBR", "SharedAccess", "Timok.Rbr." + pVersion};

			Installers.Add(_spi);
			Installers.Add(_si);
		}
	}
}
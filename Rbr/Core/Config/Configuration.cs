using System.Configuration;
using System.IO;

namespace Timok.Rbr.Core.Config {
	public interface IConfiguration {
		IMainConfiguration Main { get; }
		IFoldersConfiguration Folders { get; }
		IDbConfiguration Db { get; }
		IEmailConfiguration Email { get; }
		IIVRConfiguration IVR { get; }
	}

	public class Configuration : IConfiguration {
		public static Configuration Instance = new Configuration(ConfigurationManager.AppSettings.Get("RbrRoot"));

		public IMainConfiguration Main { get; private set; }
		public IFoldersConfiguration Folders { get; private set; }
		public IDbConfiguration Db { get; private set; }
		public IEmailConfiguration Email { get; private set; }
		public IIVRConfiguration IVR { get; private set; }

		Configuration(string pRbrConfigFileFolder) {
			if (Main == null) {
				Main = new MainConfiguration(pRbrConfigFileFolder);
				Db = new DbConfiguration(Main);
				Folders = new FoldersConfiguration(Main, Db);
				Email = new EmailConfiguration(Main);
				IVR = new IVRConfiguration(Main);
			}
		}

		public void CheckWorkingFolders() {
			//-- check for existance and create missing working folders
			if (!Directory.Exists(Folders.ArchiveFolder)) {
				Directory.CreateDirectory(Folders.ArchiveFolder);
			}

			if (!Directory.Exists(Folders.FtpFolder)) {
				Directory.CreateDirectory(Folders.FtpFolder);
			}

			if (!Directory.Exists(Folders.FtpReplicationFolder)) {
				Directory.CreateDirectory(Folders.FtpReplicationFolder);
			}

			if (!Directory.Exists(Folders.CdrAggrPublishingFolder)) {
				Directory.CreateDirectory(Folders.CdrAggrPublishingFolder);
			}

			if (!Directory.Exists(Folders.RbrPublishingFolder)) {
				Directory.CreateDirectory(Folders.RbrPublishingFolder);
			}

			if (!Directory.Exists(Folders.CdrExportFolder)) {
				Directory.CreateDirectory(Folders.CdrExportFolder);
			}

			if (!Directory.Exists(Folders.CdrPublishingFolder)) {
				Directory.CreateDirectory(Folders.CdrPublishingFolder);
			}
		}
	}
}
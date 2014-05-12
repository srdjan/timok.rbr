using System;
using Timok.Core;

namespace Timok.Rbr.Core.Config {
	public interface IDbConfiguration {
		string RbrDbPath { get; }
		string CdrDbPath { get; }
		string RbrDbVersion { get; }
		string CdrDbVersion { get; }

		string CdrDbDependencyKeyName { get; }
		string CdrDbNamePrefix { get; }
		string CdrDbVersionKeyName { get; }
		string RbrDbNamePrefix { get; }
		string RbrDbVersionKeyName { get; }
		string Salt { get; }
		int NumberOfDbOpenRetries { get; }
		DateTime SqlSmallDateTimeMaxValue { get; }
		DateTime SqlSmallDateTimeMinValue { get; }
		string CdrDbDependency { get; }
	}

	public class DbConfiguration : IDbConfiguration {
		public string CdrDbDependencyKeyName { get { return "CdrDbDependency"; } }
		public string CdrDbNamePrefix { get { return "CdrDb_"; } }
		public string CdrDbVersionKeyName { get { return "CdrDbVersion"; } }
		public string RbrDbNamePrefix { get { return "RbrDb_"; } }
		public string RbrDbVersionKeyName { get { return "RbrDbVersion"; } }
		public string Salt { get { return "Odessa1992"; } }
		public string CdrDbVersion { get; private set; }
		public int NumberOfDbOpenRetries { get; private set; }
		public string RbrDbVersion { get; private set; }
		public DateTime SqlSmallDateTimeMaxValue { get { return new DateTime(2079, 6, 6); } }
		public DateTime SqlSmallDateTimeMinValue { get { return new DateTime(1900, 1, 1); } }
		public string CdrDbDependency { get; private set; }


		string rbrDbPath = string.Empty;
		public string RbrDbPath {
			get {
				if (rbrDbPath == string.Empty) {
					rbrDbPath = AppConfig.GetValue(main.RbrConfigFile, "dbSettings", "RbrDbPath");
					if (string.IsNullOrEmpty(rbrDbPath)) {
						rbrDbPath = @"C:\Timok\Rbr\SqlDb\"; //default
					}
				}
				return rbrDbPath;
			}
		}

		string cdrDbPath = string.Empty;
		public string CdrDbPath {
			get {
				if (cdrDbPath == string.Empty) {
					cdrDbPath = AppConfig.GetValue(main.RbrConfigFile, "dbSettings", "CdrDbPath");
					if (string.IsNullOrEmpty(cdrDbPath)) {
						cdrDbPath = @"C:\Timok\Rbr\SqlDb\"; //default
					}
				}
				return cdrDbPath;
			}
		}

		readonly IMainConfiguration main;
		public DbConfiguration(IMainConfiguration pMain) {
			main = pMain;
			NumberOfDbOpenRetries = int.Parse(AppConfig.GetValue(main.RbrConfigFile, "dbSettings", "NumberOfDbOpenRetries"));
			CdrDbDependency = AppConfig.GetValue(main.RbrConfigFile, "dbSettings", CdrDbDependencyKeyName);
			RbrDbVersion = AppConfig.GetValue(main.RbrConfigFile, "dbSettings", RbrDbVersionKeyName);
			CdrDbVersion = AppConfig.GetValue(main.RbrConfigFile, "dbSettings", CdrDbVersionKeyName);
		}
	}
}
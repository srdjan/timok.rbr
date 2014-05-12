using System;
using System.Configuration;
using System.IO;
using System.Reflection;
using Timok.Core;
using Timok.Core.Events;
using Timok.Logger;

namespace Timok.Rbr.Core.Config {
	public interface IMainConfiguration {
		event EventHandler<GenericEventArgs<LogEntryArgs>> LogEvent;  

		string AppNickName { get; }
		string Version { get; }
		string Owner { get; }
		NodeRole NodeRole { get; }
		string HostName { get; }
		string HostIP { get; }
		string MediaIP { get; }
		string RbrRoot { get; }
		string RbrConfigFile { get; }
		string RbrRootParent { get; }

		string AmountFormat { get; }
		string CarrierRateAmountFormat { get; }
		string DefaultIntlDialCode { get; }
		string DisplayAmountFormat { get; }
		//short PrefixTypeId_NoPrefixes { get; }
		string RetailAmountFormat { get; }
		string RetailRateAmountFormat { get; }

		LogSeverity LogSeverity { get; }
		bool LogCdrToFile { get; }

		int CdrAggrExportFrequency { get; }
		int CdrAggrDaysToKeep { get; }
		string CDRExportDelimeterChar { get; }
		string CDRExportFormat { get; }
		int CDRExportFileRecyclePeriod { get; }
		string CdrExportFileNameFormat { get; }

		bool WithRerouting { get; }
		bool WithOriginationRouting { get; }

		bool ANIRequired { get; }
		int GlobalCPSLimit { get; }
		int PerCallTimeLimit { get; }

		bool ShowSharedServices { get; }
		bool SingleCallingPlan { get; }
		int TimeSyncFrequency { get; }
		double ArchiveDaysToKeep { get; }
		string SystemDrive { get; }
		string ArchiveDrive { get; }

		int BalanceWarningFrequency { get; }
		int MaintananceHour { get; }
		string CallingPlanImporterDelimeter { get; }
		string UdpRbrClientIPAddress { get;}
		int UdpServerRbrPort {get;}
		string UdpServerIp { get; }
		string CustomHeaderSourceName { get; }
		int NumberOfAuthenticationRetries { get; }
	}

	public class MainConfiguration : IMainConfiguration {
		const int defaultCdrAggrExportFrequency = 60;
		const bool defaultLogCdrToFile = true;
		const int defaultTimeSyncFrequency = 1;
		public event EventHandler<GenericEventArgs<LogEntryArgs>> LogEvent;
		public string AppNickName { get { return ConfigurationManager.AppSettings["AppNickName"]; } }
		bool? aniRequired;
		string archiveDrive = string.Empty;
		int cdrAggregateDaysToKeep = 31;
		int globalCPSLimit = -1;
		int savedDefaultCdrAggrExportFrequency = 60;
		int savedDefaultTimeSyncFrequency = 1;
		string showSharedServices = string.Empty;
		string singleCallingPlan = string.Empty;
		string systemDrive = string.Empty;
		bool? withOriginationRouting;
		int perCallTimeLimit = -1;

		public string AmountFormat { get; private set; }
		public string CarrierRateAmountFormat { get; private set; }
		//public short PrefixTypeId_NoPrefixes { get; private set; }
		public string DefaultIntlDialCode { get; private set; }
		public string DisplayAmountFormat { get; private set; }
		public string RetailAmountFormat { get; private set; }
		public string RetailRateAmountFormat { get; private set; }
		public string UdpServerIp { get { return HostIP; } }
		public string UdpRbrClientIPAddress { get { return HostIP; } }
		public int UdpServerRbrPort {
			get {
				var _serverPort = AppConfig.GetValue(RbrConfigFile, "udpSettings", "UdpServerRbrPort");
				return ( string.IsNullOrEmpty(_serverPort) ? 8080 : int.Parse(_serverPort) );
				//Rbr.UdpServerInstance runs on port 8080!!!
			}
		}

		int numberOfAuthenticationRetries = -1;
		public int NumberOfAuthenticationRetries {
			get {
				if (numberOfAuthenticationRetries == -1) {
					var _numberOfAuthenticationRetries = AppConfig.GetValue(RbrConfigFile, "ivrSettings", "NumberOfAuthenticationRetries");
					numberOfAuthenticationRetries = string.IsNullOrEmpty(_numberOfAuthenticationRetries) ? 2 : int.Parse(_numberOfAuthenticationRetries);
				}
				return numberOfAuthenticationRetries;
			}
		}

		string version = string.Empty;
		public string Version {
			get {
				if (version.Length > 0) {
					return version;
				}
				version = Assembly.GetExecutingAssembly().GetName().Version.ToString();
				return version;
			}
		}

		string owner = string.Empty;
		public string Owner {
			get {
				if (owner.Length == 0) {
					owner = AppConfig.GetValue(RbrConfigFile, "Owner");
					if (string.IsNullOrEmpty(owner)) {
						owner = "???";
					}
				}
				return owner;
			}
		}

		NodeRole nodeRole = NodeRole.Unknown;
		public NodeRole NodeRole {
			get {
				if (nodeRole == NodeRole.Unknown) {
					var _nodeRole = AppConfig.GetValue(RbrConfigFile, "NodeRole");
					if (!string.IsNullOrEmpty(_nodeRole)) {
						nodeRole = (NodeRole)Enum.Parse(typeof(NodeRole), _nodeRole);
					}
				}
				return nodeRole;
			}
		}

		public string HostName { get; private set; }

		string hostIP = string.Empty;
		public string HostIP {
			get {
				if (hostIP == null) {
					hostIP = string.Empty;
				}
				if (hostIP.Length == 0) {
					hostIP = AppConfig.GetValue(RbrConfigFile, "HostIP");
					if (string.IsNullOrEmpty(hostIP)) {
						throw new NotImplementedException("HostIP NOT set in Rbr Config File");
					}
				}
				return hostIP;
			}
		}		
		
		string mediaIP = string.Empty;
		public string MediaIP {
			get {
				if (mediaIP == null) {
					mediaIP = string.Empty;
				}
				if (mediaIP.Length == 0) {
					mediaIP = AppConfig.GetValue(RbrConfigFile, "MediaIP");
					if (string.IsNullOrEmpty(mediaIP)) {
						throw new NotImplementedException("MediaIP NOT set in Rbr Config File");
					}
				}
				return MediaIP;
			}
		}

		public string RbrRoot { get; private set; }
		public string RbrConfigFile { get; private set; }
		public string RbrRootParent { get; private set; }

		public bool LogCdrToFile { get; private set; }
		
		LogSeverity logSeverity;
		bool firstTime = true;
		public LogSeverity LogSeverity {
			get {
				if (firstTime) {
					firstTime = false;
					var _temp = AppConfig.GetValue(RbrConfigFile, "LogSeverity");
					if (string.IsNullOrEmpty(_temp)) {
						logSeverity = LogSeverity.Debug;
					}
					else {
						logSeverity = (LogSeverity)Enum.Parse(typeof(LogSeverity), _temp);
					}
				}
				return logSeverity;
			}
		}

		public int TimeSyncFrequency {
			get {
				var _timeSyncFrequencyStr = AppConfig.GetValue(RbrConfigFile, "TimeSyncFrequency");
				if (_timeSyncFrequencyStr == string.Empty) {
					return defaultTimeSyncFrequency;
				}

				try {
					var _newTimeSyncFrequency = int.Parse(_timeSyncFrequencyStr);
					if (savedDefaultTimeSyncFrequency != _newTimeSyncFrequency) {
						savedDefaultTimeSyncFrequency = _newTimeSyncFrequency;
					}
					return _newTimeSyncFrequency;
				}
				catch (Exception _ex) {
					LogEvent.Raise(this, new LogEntryArgs(LogSeverity.Critical, string.Format("Configuration.Main.TimeSyncFrequency: Invalid config value, DefaultTimeSyncFrequency={0} will be used. Exception:\r\n{1}", defaultTimeSyncFrequency, _ex)));
					return defaultTimeSyncFrequency;
				}
			}
		}

		public string CdrExportFileNameFormat { get { return "yyyy-MM-dd-HHmm"; } private set { CdrExportFileNameFormat = value; } }
		public int CdrAggrExportFrequency {
			get {
				var _cdrAggrExportFrequencyStr = AppConfig.GetValue(RbrConfigFile, "CdrAggrExportFrequency");
				if (_cdrAggrExportFrequencyStr == string.Empty) {
					LogEvent.Raise(this, new LogEntryArgs(LogSeverity.Status, string.Format("Configuration.Main.CdrAggregateFrequency: RbrConfig not set, Using Default CdrAggrExportFrequencyStr: {0}", defaultCdrAggrExportFrequency)));
					return defaultCdrAggrExportFrequency;
				}

				try {
					var _newCdrAggrExportFrequencyStr = int.Parse(_cdrAggrExportFrequencyStr);
					if (savedDefaultCdrAggrExportFrequency != _newCdrAggrExportFrequencyStr) {
						savedDefaultCdrAggrExportFrequency = _newCdrAggrExportFrequencyStr;
						LogEvent.Raise(this, new LogEntryArgs(LogSeverity.Status, string.Format("Configuration.Main.CdrAggrExportFrequency: CdrAggrExportFrequency changed: {0}", savedDefaultCdrAggrExportFrequency)));
					}
					return _newCdrAggrExportFrequencyStr;
				}
				catch (Exception _ex) {
					LogEvent.Raise(this, new LogEntryArgs(LogSeverity.Critical, string.Format("Configuration.Main.CdrAggrExportFrequency: Invalid config value for CdrAggrExportFrequency, DefaultCdrAggrExportFrequency={0} will be used. Exception:\r\n{1}", defaultCdrAggrExportFrequency, _ex)));
					return defaultCdrAggrExportFrequency;
				}
			}
		}

		public int CDRExportFileRecyclePeriod {
			get {
				int _cdrFileRecyclePeriod;

				try {
					var _recyclePeriod = AppConfig.GetValue(RbrConfigFile, "CDRFileRecyclePeriod");
					_cdrFileRecyclePeriod = int.Parse(_recyclePeriod);
				}
				catch {
					_cdrFileRecyclePeriod = 60; //default cdr file recycle period: 1 hour
				}

				if (_cdrFileRecyclePeriod <= 5) {
					_cdrFileRecyclePeriod = 5; // minimum cdr file recycle time: 5 minutes
				}
				else if (_cdrFileRecyclePeriod > 5 && _cdrFileRecyclePeriod <= 10) {
					_cdrFileRecyclePeriod = 10;
				}
				else if (_cdrFileRecyclePeriod > 10 && _cdrFileRecyclePeriod <= 15) {
					_cdrFileRecyclePeriod = 15;
				}
				else if (_cdrFileRecyclePeriod > 15 && _cdrFileRecyclePeriod <= 20) {
					_cdrFileRecyclePeriod = 20;
				}
				else if (_cdrFileRecyclePeriod > 20 && _cdrFileRecyclePeriod <= 30) {
					_cdrFileRecyclePeriod = 30;
				}
				else {
					_cdrFileRecyclePeriod = 60;
				}

				if (_cdrFileRecyclePeriod == 60) {
					CdrExportFileNameFormat = "yyyy-MM-dd-HH";
				}
				return _cdrFileRecyclePeriod;
			}
		}

		public string SystemDrive {
			get {
				if (systemDrive == string.Empty) {
					systemDrive = AppConfig.GetValue(RbrConfigFile, "SystemDrive");
					if (string.IsNullOrEmpty(systemDrive)) {
						systemDrive = @"C:\";
					}
				}
				return systemDrive;
			}
		}

		public string ArchiveDrive {
			get {
				if (archiveDrive == string.Empty) {
					archiveDrive = AppConfig.GetValue(RbrConfigFile, "ArchiveDrive");
					if (string.IsNullOrEmpty(archiveDrive)) {
						archiveDrive = @"C:\";
					}
				}
				return archiveDrive;
			}
		}

		public bool SingleCallingPlan {
			get {
				bool _res;
				if (singleCallingPlan.Length == 0) {
					singleCallingPlan = AppConfig.GetValue(RbrConfigFile, "SingleCallingPlan");
					if (string.IsNullOrEmpty(singleCallingPlan)) {
						singleCallingPlan = bool.FalseString;
					}
				}
				bool.TryParse(singleCallingPlan, out _res);
				return _res;
			}
		}

		public bool ShowSharedServices {
			get {
				if (showSharedServices.Length == 0) {
					showSharedServices = AppConfig.GetValue(RbrConfigFile, "ShowSharedServices");
					if (string.IsNullOrEmpty(showSharedServices)) {
						showSharedServices = bool.FalseString;
					}
				}
				bool _res;
				bool.TryParse(showSharedServices, out _res);
				return _res;
			}
		}

		public string CallingPlanImporterDelimeter {
			get {
				var _callingPlanImporterDelimeter = AppConfig.GetValue(RbrConfigFile, "CallingPlanImporterDelimeter");
				return ( string.IsNullOrEmpty(_callingPlanImporterDelimeter) ? "|" : _callingPlanImporterDelimeter );
			}
		}

		public int CdrAggrDaysToKeep {
			get {
				var _cdrAggrDaysToKeepStr = AppConfig.GetValue(RbrConfigFile, "CdrAggrDaysToKeep");
				if (!string.IsNullOrEmpty(_cdrAggrDaysToKeepStr) && isInt(_cdrAggrDaysToKeepStr)) {
					var _cdrAggrDaysToKeepInt = int.Parse(_cdrAggrDaysToKeepStr);
					if (_cdrAggrDaysToKeepInt > 0) {
						cdrAggregateDaysToKeep = _cdrAggrDaysToKeepInt;
					}
				}
				return cdrAggregateDaysToKeep;
			}
		}
		public string CDRExportDelimeterChar { get { return "CDRExportDelimeterChar"; } }
		public string CDRExportFormat { get { return "CDRExportFormat"; } }

		public bool WithOriginationRouting {
			get {
				if (withOriginationRouting == null) {
					var _withOriginationRouting = AppConfig.GetValue(RbrConfigFile, "WithOriginationRouting");
					withOriginationRouting = !string.IsNullOrEmpty(_withOriginationRouting) && bool.Parse(_withOriginationRouting);
				}
				return withOriginationRouting.Value;
			}
		}

		public bool ANIRequired {
			get {
				if (aniRequired == null) {
					var _aniRequiredStr = AppConfig.GetValue(RbrConfigFile, "ANIRequired");
					if (!string.IsNullOrEmpty(_aniRequiredStr)) {
						aniRequired = bool.Parse(_aniRequiredStr);
					}
					else {
						aniRequired = true; //default
					}
				}
				return aniRequired.Value;
			}
		}

		public int GlobalCPSLimit {
			get {
				if (globalCPSLimit == -1) {
					var _globalMaxCPS = AppConfig.GetValue(RbrConfigFile, "GlobalCPSLimit");
					globalCPSLimit = string.IsNullOrEmpty(_globalMaxCPS) ? 100 : int.Parse(_globalMaxCPS);
				}
				return globalCPSLimit;
			}
		}

		public int PerCallTimeLimit {
			get {
				if (perCallTimeLimit == -1) {
					var _perCallTimeLimit = AppConfig.GetValue(RbrConfigFile, "PerCallTimeLimit");
					perCallTimeLimit = string.IsNullOrEmpty(_perCallTimeLimit) ? 300 : int.Parse(_perCallTimeLimit);
				}
				return perCallTimeLimit;
			}
		}

/*
		public bool WithNumberPortability {
			get {
				var _withNumberPortability = AppConfig.GetValue(RbrConfigFile, "WithNumberPortability");
				if (!string.IsNullOrEmpty(_withNumberPortability)) {
					withNumberPortability = bool.Parse(_withNumberPortability);
				}
				return withNumberPortability;
			}
		}
*/
/*
		public string NumberPortabilityDialCode { get { return AppConfig.GetValue(RbrConfigFile, "NumberPortabilityDialCode"); } }
*/

		public double ArchiveDaysToKeep {
			get {
				double _archiveDaysToKeep = 31;
				try {
					var _archiveDatsToKeepStr = AppConfig.GetValue(RbrConfigFile, "ArchiveDaysToKeep");
					if (!string.IsNullOrEmpty(_archiveDatsToKeepStr) && isInt(_archiveDatsToKeepStr)) {
						var _archiveDatsToKeepDbl = double.Parse(_archiveDatsToKeepStr);
						if (_archiveDatsToKeepDbl > 0) {
							_archiveDaysToKeep = _archiveDatsToKeepDbl;
						}
					}
					else {
						LogEvent.Raise(this, new LogEntryArgs(LogSeverity.Status, string.Format("Configuration.Main.ArchiveDaysToKeep: Missing RbrConfig key")));
					}
				}
				catch (Exception _ex) {
					LogEvent.Raise(this, new LogEntryArgs(LogSeverity.Error, string.Format("Configuration.Main.ArchiveDaysToKeep: exception: {0}", _ex)));
				}
				return _archiveDaysToKeep;
			}
		}

		public int MaintananceHour {
			get {
				var _maintananceHour = 4;
				try {
					var _maintananceHourStr = AppConfig.GetValue(RbrConfigFile, "MaintananceHour");
					if (!string.IsNullOrEmpty(_maintananceHourStr) && isInt(_maintananceHourStr)) {
						var _maintananceHourInt = int.Parse(_maintananceHourStr);
						if (_maintananceHourInt >= 0 && _maintananceHourInt <= 23) {
							_maintananceHour = _maintananceHourInt;
						}
					}
					else {
						LogEvent.Raise(this, new LogEntryArgs(LogSeverity.Status, string.Format("Configuration.Main.ArchiveDaysToKeep: Missing MaintananceHour key")));
					}
				}
				catch (Exception _ex) {
					LogEvent.Raise(this, new LogEntryArgs(LogSeverity.Error, string.Format("Configuration.Main.MaintananceHour: exception: {0}", _ex)));
				}
				return _maintananceHour;
			}
		}

		public int BalanceWarningFrequency {
			get {
				var _balanceWarningFrequency = 6;
				try {
					var _balanceWarningFrequencyStr = AppConfig.GetValue(RbrConfigFile, "BalanceWarningFrequency");
					if (!string.IsNullOrEmpty(_balanceWarningFrequencyStr) && isInt(_balanceWarningFrequencyStr)) {
						var _balanceWarningFrequencyInt = int.Parse(_balanceWarningFrequencyStr);
						if (_balanceWarningFrequencyInt >= 0 && _balanceWarningFrequencyInt <= 23) {
							_balanceWarningFrequency = _balanceWarningFrequencyInt;
						}
					}
					else {
						LogEvent.Raise(this, new LogEntryArgs(LogSeverity.Status, string.Format("Configuration.Main.ArchiveDaysToKeep: Missing BalanceWarningFrequency key")));
					}
				}
				catch (Exception _ex) {
					LogEvent.Raise(this, new LogEntryArgs(LogSeverity.Error, string.Format("Configuration.Main.BalanceWarningFrequency: exception: {0}", _ex)));
				}
				return _balanceWarningFrequency;
			}
		}

		string withRerouting = string.Empty;
		public bool WithRerouting {
			get {
				bool _res;
				if (withRerouting.Length == 0) {
					withRerouting = AppConfig.GetValue(RbrConfigFile, "ivrSettings", "WithRerouting");
					if (string.IsNullOrEmpty(withRerouting)) {
						withRerouting = bool.FalseString;
					}
				}
				bool.TryParse(withRerouting, out _res);
				return _res;
			}
		}


		const string CUSTOM_SIP_HEADER_PREFIX = "x-";
		const string CUSTOM_SIP_HEADER_SUFIX = ": ";
		public string CustomHeaderSourceName { get { return CUSTOM_SIP_HEADER_PREFIX + Owner + CUSTOM_SIP_HEADER_SUFIX; } }

    public MainConfiguration(string pRbrConfigFileFolder) {
			HostName = Environment.MachineName;
			RbrRoot = pRbrConfigFileFolder;
			RbrRootParent = Directory.GetParent(RbrRoot).FullName;
			RbrConfigFile = Path.Combine(RbrRoot, "Rbr.Config");

			LogCdrToFile = ( AppConfig.GetValue(RbrConfigFile, "LogCdrToFile") == string.Empty ) ? defaultLogCdrToFile : bool.Parse(AppConfig.GetValue(RbrConfigFile, "LogCdrToFile"));

			CarrierRateAmountFormat = "0." + string.Empty.PadRight(AppConstants.CarrierRateDecimalDigits, '0');
			AmountFormat = ( AppConfig.GetValue(RbrConfigFile, "AmountFormat") == string.Empty ) ? AppConstants.DefaultAmountFormat : AppConfig.GetValue(RbrConfigFile, "AmountFormat");
			RetailRateAmountFormat = "0." + string.Empty.PadRight(AppConstants.RetailRateDecimalDigits, '0');
			RetailAmountFormat = "0." + string.Empty.PadRight(AppConstants.RetailAmountDecimalDigits, '0');
			DisplayAmountFormat = ( AppConfig.GetValue(RbrConfigFile, "DisplayAmountFormat") == string.Empty ) ? AppConstants.DefaultAmountFormat : AppConfig.GetValue(RbrConfigFile, "DisplayAmountFormat");
			DefaultIntlDialCode = ( AppConfig.GetValue(RbrConfigFile, "DefaultIntlDialCode") == string.Empty ) ? "011" : AppConfig.GetValue(RbrConfigFile, "DefaultIntlDialCode");
			//PrefixTypeId_NoPrefixes = 0;

		}

		//------------------------ private ----------------------------
		static bool isInt(string pString) {
			try {
				int.Parse(pString);
			}
			catch {
				return false;
			}
			return true;
		}
	}
}
using System;
using System.Collections.Generic;
using System.IO;
using Timok.Core;

namespace Timok.Rbr.Core.Config {
	public interface IIVRConfiguration {
		string RbrRoot { get; }
		string LocalIPAddress { get; }
		string MediaIPAddress { get; }
		bool IsProsodyX { get; }
		int NumberOfModules { get; }
		int TimeToWaitBeforeHWInit { get; }
		int MaxNumberOfCallsPerModule { get; }
		int MaxResourcesPerModule { get; }
		string LocalDisplayName { get; }
		string CredentialsRealm { get; }
		string CredentialsUser { get; }
		string CredentialsPassword { get; }
		int[] SupportedCodecTypes { get; }
		int Volume { get; }
		string ToneDetectTimeout { get; }
		string ThreadStackSize { get; }
		string TestTermIP { get; }
		string[] ScriptTypes { get; }
		int MaxRecordingTime { get; }
		int NumberOfAuthenticationRetries { get; }
		int NumberOfRetriesForRecording { get; }
		string NumberRoutingFilePath { get; }
		string CustomHeaderSourceName { get; }
	}

	public class IVRConfiguration : IIVRConfiguration {
		string isProsodyX = string.Empty;
		int maxRecordingTime = -1;
		int numberOfAuthenticationRetries = -1;
		int numberOfRetriesForRecording = -1;
		string[] scriptTypes;
		const string CUSTOM_SIP_HEADER_PREFIX = "x-";
		const string CUSTOM_SIP_HEADER_SUFIX = ": ";

		public string CustomHeaderSourceName { get { return CUSTOM_SIP_HEADER_PREFIX + main.Owner + CUSTOM_SIP_HEADER_SUFIX; } }
		public string IVRProcessInfoArgs { get; private set; }

		public string RbrRoot { get { return main.RbrRoot; } }

		public string LocalIPAddress { get { return main.HostIP; } }
		public string MediaIPAddress { get { return main.MediaIP; } }

		public bool IsProsodyX {
			get {
				bool _res;
				if (isProsodyX.Length == 0) {
					isProsodyX = AppConfig.GetValue(main.RbrConfigFile, "ivrSettings", "IsProsodyX");
					if (string.IsNullOrEmpty(isProsodyX)) {
						isProsodyX = bool.FalseString;
					}
				}
				bool.TryParse(isProsodyX, out _res);
				return _res;
			}
		}

		public int NumberOfModules { get { return int.Parse(AppConfig.GetValue(main.RbrConfigFile, "ivrSettings", "NumberOfModules")); } }
		public int TimeToWaitBeforeHWInit { get { return int.Parse(AppConfig.GetValue(main.RbrConfigFile, "ivrSettings", "TimeToWaitBeforeHWInit")); } }
		public int MaxNumberOfCallsPerModule { get { return int.Parse(AppConfig.GetValue(main.RbrConfigFile, "ivrSettings", "MaxNumberOfCallsPerModule")); } }
		public int MaxResourcesPerModule { get { return int.Parse(AppConfig.GetValue(main.RbrConfigFile, "ivrSettings", "MaxResourcesPerModule")); } }
		public string LocalDisplayName { get { return AppConfig.GetValue(main.RbrConfigFile, "ivrSettings", "LocalDisplayName"); } }
		public string CredentialsRealm { get { return AppConfig.GetValue(main.RbrConfigFile, "ivrSettings", "CredentialsRealm"); } }
		public string CredentialsUser { get { return AppConfig.GetValue(main.RbrConfigFile, "ivrSettings", "CredentialsUser"); } }
		public string CredentialsPassword { get { return AppConfig.GetValue(main.RbrConfigFile, "ivrSettings", "CredentialsPassword"); } }

		public int[] SupportedCodecTypes {
			get {
				var _codecTypeList = AppConfig.GetValue(main.RbrConfigFile, "ivrSettings", "SupportedCodecTypes");
				if (string.IsNullOrEmpty(_codecTypeList)) {
					return new[] { 0 };
				}

				var _intList = new List<int>();
				var _stringList = _codecTypeList.Split(',');
				foreach (var _codecType in _stringList) {
					_intList.Add(int.Parse(_codecType));
				}
				return _intList.ToArray();
			}
		}

		public int Volume { get { return int.Parse(AppConfig.GetValue(main.RbrConfigFile, "ivrSettings", "Volume")); } }
		public string ToneDetectTimeout { get { return AppConfig.GetValue(main.RbrConfigFile, "ivrSettings", "ToneDetectTimeout"); } }
		public string ThreadStackSize {
			get {
				try {
					return AppConfig.GetValue(main.RbrConfigFile, "ivrSettings", "ThreadStackSize");
				}
				catch {
					return "131072"; //default
				}
			}
		}
		public string TestTermIP { get { return AppConfig.GetValue(main.RbrConfigFile, "ivrSettings", "TestTermIP"); } }
		public string[] ScriptTypes {
			get {
				if (scriptTypes == null) {
					var _scriptTypes = AppConfig.GetValue(main.RbrConfigFile, "ivrSettings", "ScriptTypes");
					if (_scriptTypes != null) {
						scriptTypes = _scriptTypes.Split(',');
						var _enumMembers = ConvertEnum.ToArray(typeof(ScriptType));
						//foreach (var _enumMaskValue in scriptTypes) {
						//  if (!partOf(_enumMaskValue, _enumMembers)) {
						//    T.LogRbr(LogSeverity.Error, "Configuration.Main.ScryptTypes", string.Format("ERROR: Unknown ScriptType: {0}, please inform Support", _enumMaskValue));
						//  }
						//}
					}
					else {
						scriptTypes = new string[0];
					}
				}
				return scriptTypes;
			}
		}

		public int MaxRecordingTime {
			get {
				if (maxRecordingTime == -1) {
					var _maxRecordingTime = AppConfig.GetValue(main.RbrConfigFile, "ivrSettings", "MaxRecordingTime");
					maxRecordingTime = string.IsNullOrEmpty(_maxRecordingTime) ? 60000 : int.Parse(_maxRecordingTime);
				}
				return maxRecordingTime;
			}
		}

		public int NumberOfAuthenticationRetries {
			get {
				if (numberOfAuthenticationRetries == -1) {
					var _numberOfAuthenticationRetries = AppConfig.GetValue(main.RbrConfigFile, "ivrSettings", "NumberOfAuthenticationRetries");
					numberOfAuthenticationRetries = string.IsNullOrEmpty(_numberOfAuthenticationRetries) ? 2 : int.Parse(_numberOfAuthenticationRetries);
				}
				return numberOfAuthenticationRetries;
			}
		}

		public int NumberOfRetriesForRecording {
			get {
				if (numberOfRetriesForRecording == -1) {
					var _numberOfRetriesForRecording = AppConfig.GetValue(main.RbrConfigFile, "ivrSettings", "NumberOfRetriesForRecording");
					numberOfRetriesForRecording = string.IsNullOrEmpty(_numberOfRetriesForRecording) ? 3 : int.Parse(_numberOfRetriesForRecording);
				}
				return numberOfRetriesForRecording;
			}
		}

		public string NumberRoutingFilePath { get { return Path.Combine(Path.Combine(Path.Combine(main.RbrRoot, "IVR"), "NumberRouting"), "RoutingTable.txt"); } }

		readonly IMainConfiguration main;
		public IVRConfiguration(IMainConfiguration pMain) {
			main = pMain;
			IVRProcessInfoArgs = string.Empty;
		}

		//-----   private helpers  -------------------------
		bool partOf(string pEnumMaskValue, Array pEnumMembers) {
			foreach (var _obj in pEnumMembers) {
				if (_obj.ToString() == pEnumMaskValue) {
					return true;
				}
			}
			return false;
		}
	}
}
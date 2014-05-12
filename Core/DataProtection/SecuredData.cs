using System;
using System.Text;

namespace Timok.Core.DataProtection {
	public class SecuredData {
		SecuredData() {}

		#region Public Methods

		#region Public Registry access

		//		public static string GetFromRegistry(string pAppName, string pDataKeyName){
		//			string _clearData = string.Empty;
		//			string _regValue = string.Empty;
		//			_regValue = ReadFromRegistry(pAppName, pDataKeyName);
		//			_clearData = Decrypt(_regValue);
		//			return _clearData;
		//		}
		//
		//		public static string SaveToRegistry(string pAppName, string pDataKeyName, string pClearData){
		//			string _encryptedData = string.Empty;
		//			_encryptedData = Encrypt(pClearData);
		//			WriteToRegistry(pAppName, pDataKeyName, _encryptedData);
		//			return _encryptedData;
		//		}

		#endregion Public Registry access

		#region Public AppConfig access

		public static string GetFromAppConfig(string pConfigFilePath, string pDataKeyName) {
			return Decrypt(ReadFromAppConfig(pConfigFilePath, pDataKeyName));
		}

		public static string SaveToAppConfig(string pConfigFilePath, string pDataKeyName, string pData) {
			string _encryptedData = Encrypt(pData);
			if (! WriteToAppConfig(pConfigFilePath, pDataKeyName, _encryptedData)) {
				_encryptedData = string.Empty;
			}
			return _encryptedData;
		}

		#endregion Public AppConfig access

		#endregion Public Methods

		#region private constants

		//private const string software_RegKeyName = @"Software";
		//private const string TimokSoftware_RegKeyName = "TimokES";
		const string e1 = "Herjg8Q0A05FkHVeAAABMhebtkIAK2";
		const string e2 = "RjjzNzTHM1I=sWaQMWsUAAs9g/bU+hcAADCnmi";
		const string e3 = "AA8BFNCMnAQAd";
		const string e4 = "dERjHoAAlBwE/Cl+sBAAAASu";
		const string e5 = "hJuSNc/6h/jwQAAAACAAAADZgAkSpZwAAA";
		const string e6 = "AAAACBAAKaAqAAtz";
		const string e7 = "VAj6Aiob8AAxrKT0E65";
		const string e8 = e1 + e2;
		const string e9 = e8 + e3 + e4 + e5;
		const string entropy = e9 + e6 + e7;

		#endregion private constants

		#region private Methods

		#region private AppConfig access

		static string ReadFromAppConfig(string pConfigFilePath, string pDataKeyName) {
			return AppConfig.GetValue(pConfigFilePath, "dbSettings", pDataKeyName);
		}

		static bool WriteToAppConfig(string pConfigFilePath, string pDataKeyName, string pData) {
			return AppConfig.SetValue(ConfigFileType.CustomAppConfig, "dbSettings", pDataKeyName, pData, pConfigFilePath);
		}

		#endregion private AppConfig access

		#region private Encryption

		static string Encrypt(string pData) {
			DataProtector dp = null;
			string _encryptedData = string.Empty;
			try {
				dp = new DataProtector(DataProtector.Store.USE_MACHINE_STORE);

				byte[] _data = Encoding.ASCII.GetBytes(pData);
				byte[] _entropy = Encoding.ASCII.GetBytes(entropy);
				_encryptedData = Convert.ToBase64String(dp.Encrypt(_data, _entropy));
			}
			catch {
				dp = null;
			}
			return _encryptedData;
		}

		static string Decrypt(string pData) {
			DataProtector dp = null;
			string _res = string.Empty;

			try {
				dp = new DataProtector(DataProtector.Store.USE_MACHINE_STORE);

				byte[] _data = Convert.FromBase64String(pData);
				byte[] _entropy = Encoding.ASCII.GetBytes(entropy);
				_res = Encoding.ASCII.GetString(dp.Decrypt(_data, _entropy));
			}
			catch {
				dp = null;
			}
			return _res;
		}

		#endregion private Encryption

		#region private Registry access

		//		private static bool WriteToRegistry(string pAppName, string pDataKeyName, string pData){
		//			bool _res = false;
		//			RegistryKey rk = Registry.LocalMachine.OpenSubKey(software_RegKeyName, true);
		//			if(rk != null){
		//				rk = rk.CreateSubKey(Path.Combine(TimokSoftware_RegKeyName, pAppName));
		//				if(rk != null){
		//					// Write encrypted string to the registry
		//					rk.SetValue(pDataKeyName, pData);
		//					_res = true;
		//				}
		//			}
		//			return _res;
		//		}
		//
		//		private static string ReadFromRegistry(string pAppName, string pDataKeyName){
		//			string _res = string.Empty;
		//			string _appKeyName = Path.Combine(software_RegKeyName, TimokSoftware_RegKeyName);
		//			_appKeyName = Path.Combine(_appKeyName, pAppName);
		//			RegistryKey rk = Registry.LocalMachine.OpenSubKey(_appKeyName, false);
		//			if(rk != null){
		//				object _value = rk.GetValue(pDataKeyName);
		//				if(_value != null)
		//					_res = (string)_value;
		//			}
		//			return _res;
		//		}

		#endregion private Registry access

		#endregion private Methods
	}
}
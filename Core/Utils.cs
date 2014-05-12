using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Net;
using System.Reflection;
using System.Text;

namespace Timok.Core {
	public static class Utils {
		public static Assembly TryLoadIvrAssembly(string pPath) {
			Assembly _ivrAssembly;
			try {
				_ivrAssembly = Assembly.LoadFrom(pPath);
			}
			catch (Exception _ex) {
				throw new Exception(string.Format("Failed attempt, Exception: {0}", _ex.Message));
			}
			return _ivrAssembly;
		}

		/// <summary>
		/// Checks the specified value to see if it can be 
		/// converted into the specified type.
		/// <remarks>
		/// The method supports all the primitive types of the CLR
		/// such as int, boolean, double, guid etc. as well as other 
		/// simple types like Color and Unit and custom enum types.
		/// </remarks>
		/// </summary>
		/// <param name="pValue">The value to check.</param>
		/// <param name="pType">The type that the value will be checked against.</param>
		/// <returns>True if the value can convert to the given type, otherwise false.</returns>
		/// CanConvert("12", typeof(int)); // returns true 
		/// CanConvert("f637a876-9d58-4229-9559-a5e42a95fdac ", typeof(Guid)); // returns true 
		/// CanConvert("Backspace", typeof(System.ConsoleKey)); // returns true 
		/// CanConvert("10px", typeof(System.Web.UI.WebControls.Unit)); // returns true 
		/// CanConvert("red", typeof(System.Drawing.Color)); // returns true
		public static bool CanConvert(string pValue, Type pType) {
			if (string.IsNullOrEmpty(pValue) || pType == null) {
				return false;
			}

			var _conv = TypeDescriptor.GetConverter(pType);
			if (_conv.CanConvertFrom(typeof (string))) {
				try {
					_conv.ConvertFrom(pValue);
					return true;
				}
				catch {}
			}

			return false;
		}

		public static decimal CalculateMinutes(int pSeconds) {
			if (pSeconds == 0) {
				return decimal.Zero;
			}

			var _6SecIncrements = pSeconds / 6;
			if (pSeconds % 6 > 0) {
				_6SecIncrements += 1;
			}

			return decimal.Divide(_6SecIncrements, 10);
		}

		public static string GetFullExceptionInfo(Exception pEx) {
			var _str = new StringBuilder();
			_str.Append("MESSAGE: " + pEx.Message);
			_str.Append("\r\nSOURCE: " + pEx.Source);
			_str.Append("\r\nTARGET: " + pEx.TargetSite);
			_str.Append("\r\nSTACK: " + pEx.StackTrace + "\r\n");

			if (pEx.InnerException != null) {
				_str.Append("\r\n\t**** INNEREXCEPTION START ****");
				_str.Append("\r\n\tINNEREXCEPTION MESSAGE: " + pEx.InnerException.Message);
				_str.Append("\r\n\tINNEREXCEPTION SOURCE: " + pEx.InnerException.Source);
				_str.Append("\r\n\tINNEREXCEPTION STACK: " + pEx.InnerException.StackTrace);
				_str.Append("\r\n\tINNEREXCEPTION TARGETSITE: " + pEx.InnerException.TargetSite);
				_str.Append("\r\n\t****  INNEREXCEPTION END  ****");
			}
			return _str.ToString();
		}

		public static string GetFileAndLineNumber() {
			// param1 = 1 means skip up one frame in call stack
			// param2 = true means capture the file/line info
			var _callStack = new StackFrame(1, true);
			return ("File: " + _callStack.GetFileName() + ", Line: " + _callStack.GetFileLineNumber());
		}

		public static ArrayList GetDirectoriesRecursively(DirectoryInfo pDirInfo) {
			var _subDirs = new ArrayList();

			foreach (var _dirInfo in pDirInfo.GetDirectories()) {
				_subDirs.Add(_dirInfo.FullName);
				_subDirs.AddRange(GetDirectoriesRecursively(_dirInfo));
			}
			return _subDirs;
		}

		////TODO: Extract this method into utility that can run standalone - console mode
		public static List<RegPair> GetHashCode(string pSalt) {
			try {
				var _addressList = Dns.GetHostEntry(Environment.MachineName).AddressList;

				var _list = new List<RegPair>();
				foreach (var _address in _addressList) {
					_list.Add(GetHashCode(_address.ToString(), pSalt));
				}
				return _list;
			}
			catch (Exception _ex) {
				throw new ApplicationException(string.Format("Invalid Configuration, please contact support.\r\n{0}", _ex.GetType()));
			}
		}

		//public static RegPair GetHashCode(string pIPAddress, string pSalt) {
		//  var _ipAddrAsInt = IPUtil.ToInt32(pIPAddress);
		//  var _ipSalt = pIPAddress + pSalt; // + _mac;
		//  var _ipSaltHash = _ipSalt.GetHashCode();

		//  return new RegPair(_ipSaltHash, _ipAddrAsInt);
		//}

		public static void CheckHashCode(string pValueFromConfig, string pSalt, string pIP) {
			try {
				var _addressArray = Dns.GetHostEntry(Environment.MachineName).AddressList;
				var _addressList = new List<string>();
				foreach (var _address in _addressArray) {
					_addressList.Add(_address.ToString());
				}
#if DEBUG
				_addressList.Add(pIP);
#endif

				var _hashValue = int.Parse(pValueFromConfig);
				foreach (var _address in _addressList) {
					//try {
					//  var _ipInt = IPUtil.ToInt32(_address);
					//}
					//catch {}
				var _ipSalt = _address + pSalt; 
					var _ipSaltHash = _ipSalt.GetHashCode();
					if (_ipSaltHash == _hashValue) {
						return;
					}
				}
			}
			catch (Exception _ex) {
				throw new ApplicationException(string.Format("Invalid Configuration, please contact support.\r\n{0}", _ex.GetType()));
			}
#if DEBUG
			return;
#endif
			throw new ApplicationException("Invalid Installation, please contact support.");
		}

		//-- System Time Synchronization
		public static bool SyncSystemTime(int pTimeSyncFrequency, string[] pTimeServers) {
			var _now = DateTime.Now;
			if (_now.Hour % pTimeSyncFrequency == 0 && _now.Minute == 0) {
				foreach (var _timeServerUrl in pTimeServers) {
					if (syncSystemTime(_timeServerUrl)) {
						return true;
					}
				}
			}
			return false;
		}

		static bool syncSystemTime(string pTimeServerUrl) {
			try {
				var _sntpClient = new SNTPClient(pTimeServerUrl);
				_sntpClient.Connect(true);
			}
			catch (Exception _ex) {
				throw new Exception(string.Format("Utils.syncSystemTime: Time Server: {0}, Exception:\r\n{1}", pTimeServerUrl, _ex));
			}
			return true;
		}

		public static bool IsNumeric(string pValue) {
			if (pValue == null) {
				return false;
			}

			var _charArray = pValue.ToCharArray();
			for (var _i = 0; _i < _charArray.Length; _i++) {
				if (_charArray[_i] > 57 || _charArray[_i] < 48) {
					return false;
				}
			}
			return true;
		}

		public static long StrToLong(string pStr) {
			if (pStr == null || pStr.Trim() == string.Empty || pStr.Trim() == "0") {
				//TimokLogger.LogRbr(LogSeverity.Critical, "Utils.syncSystemTime", string.Format("Invalid String={0}", pStr));
				return 0;
			}

			long _long;
			try {
				_long = long.Parse(pStr);
			}
			catch {
				//TimokLogger.LogRbr(LogSeverity.Critical, "Utils.syncSystemTime", string.Format("Exception while parsing AuthToken={0}", pStr));
				return 0;
			}
			return _long;
		}

		public static int StrToInt(string pStr) {
			if (pStr == null || pStr.Trim() == string.Empty || pStr.Trim() == "0") {
				//TimokLogger.LogRbr(LogSeverity.Critical, "Utils.syncSystemTime", string.Format("Invalid String={0}", pStr));
				return 0;
			}

			int _int;
			try {
				_int = int.Parse(pStr);
			}
			catch {
				//TimokLogger.LogRbr(LogSeverity.Critical, "Utils.syncSystemTime", string.Format("Exception while parsing AuthToken={0}", pStr));
				return 0;
			}
			return _int;
		}

		public static string GetMachineIPList(string pDelimiter) {
			if (string.IsNullOrEmpty(pDelimiter)) {
				pDelimiter = ",";
			}
			var _addressList = Dns.GetHostEntry(Environment.MachineName).AddressList;
			var _ipList = string.Empty;
			foreach (var _address in _addressList) {
				_ipList += pDelimiter + _address;
			}
			if (_ipList.StartsWith(pDelimiter)) {
				_ipList = _ipList.Substring(pDelimiter.Length);
			}
			return _ipList;
		}
	}
}
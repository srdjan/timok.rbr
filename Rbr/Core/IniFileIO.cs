using System.Collections;
using System.Runtime.InteropServices;
using System.Text;
using Timok.Rbr.Core.Config;

namespace Timok.Rbr.Core {
	public class IniFileIO {				
		public static string GetKey(string pSectionName, string pKeyName) {
			return GetKey(pSectionName, pKeyName, string.Empty);
		}

		public static string GetKey(string pSectionName, string pKeyName, string pDefaultValue) {
			var _sb = new StringBuilder(MAX_BUFFER_SIZE);
			GetPrivateProfileString(pSectionName, pKeyName, pDefaultValue, _sb, MAX_BUFFER_SIZE, Configuration.Instance.Folders.GkIniFile);
			return _sb.ToString();
		}

		public static bool Write(string pSection, string pKey, string pValue) {
			return (WritePrivateProfileString(pSection, pKey, pValue, Configuration.Instance.Folders.GkIniFile) != 0);
		}

		public static bool DeleteKey(string pSection, string pKey) {
			return (WritePrivateProfileString(pSection, pKey, null, Configuration.Instance.Folders.GkIniFile) != 0);
		}

		public static void DeleteKeys(string pSection) {
			var _keys = GetKeyNames(pSection);
			for(var _i = 0; _i < _keys.Count; _i++)
				WritePrivateProfileString(pSection, _keys[_i].ToString(), null, Configuration.Instance.Folders.GkIniFile);
		}

		public static ArrayList GetKeyNames(string pSectionName) {
			if (pSectionName.Trim() == string.Empty) return null;

			var _bytes = new byte[MAX_BUFFER_SIZE];
			var _count = GetPrivateProfileSection(pSectionName, _bytes, MAX_BUFFER_SIZE, Configuration.Instance.Folders.GkIniFile);
			var _s = Encoding.ASCII.GetString(_bytes, 0, _count);
			var _keysValues = _s.Trim('\0').Split('\0'); //trim last '0' char; key=value pairs are delimited by '0' char
			var _keys = new ArrayList();
			for (var _i = 0; _i < _keysValues.Length; _i++) {
				try {
					_keys.Add(_keysValues[_i].Substring(0, _keysValues[_i].IndexOf('=')));
				}
				catch {}
			}
			return _keys;
		}

		//---------------------------------------------------------------------------------------------------------------------------------------------------
		[DllImport("KERNEL32.DLL", EntryPoint="WritePrivateProfileStringA", CharSet=CharSet.Ansi)]
		private static extern int WritePrivateProfileString (string lpApplicationName, string lpKeyName, string lpString, string lpiniFile);
		[DllImport("KERNEL32.DLL", EntryPoint="GetPrivateProfileStringA",  CharSet=CharSet.Ansi)]
		private static extern int GetPrivateProfileString (string lpApplicationName, string lpKeyName, string lpDefault, StringBuilder lpReturnedString, int nSize, string lpiniFile);
		[DllImport("kernel32.dll", EntryPoint="GetPrivateProfileSectionA", CharSet=CharSet.Ansi)]
		private static extern int GetPrivateProfileSection (string pSectionName, byte[] pReturnBuffer, int pSize, string piniFile);
		[DllImport("KERNEL32.DLL", EntryPoint="WritePrivateProfileSectionA", CharSet=CharSet.Ansi)]
		private static extern int WritePrivateProfileSection (string pAppName, string pString, string piniFile);
		private const int MAX_BUFFER_SIZE = 32767;//for Windows 95/98/Me: The maximum buffer size is 32,767 characters
	}
}
using System.IO.IsolatedStorage;

namespace RbrSiverlight {
	public static class ConfigurationSettings {
		private const string LAST_USER_NAME_KEY = "LastUserName";

		public static string LastUserName {
			get {
				var _value = "";
				if (IsolatedStorageSettings.ApplicationSettings.Contains(LAST_USER_NAME_KEY)) {
					_value = IsolatedStorageSettings.ApplicationSettings[LAST_USER_NAME_KEY].ToString();
				}
				return _value;
			}
			set { IsolatedStorageSettings.ApplicationSettings[LAST_USER_NAME_KEY] = value; }
		}
	}
}
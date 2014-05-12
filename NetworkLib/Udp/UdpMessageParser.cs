using System;
using System.Diagnostics;
using System.Text.RegularExpressions;

namespace Timok.NetworkLib.Udp {
	public class UdpMessageParser {
		public const string IN_DELIMETER = ">>";
		public const string OUT_DELIMETER = "<<";
		public const string UNKNOWN_VALUE = "?";

		#region Message description
		// message parser for the following message Format:
		// In:
		// cmd.seq:api.method.ver(param1>>param2>>...)
		// 
		// Out:
		// ret.seq:api.method.ver(result<<retParam1<<retParam2...)
		// 
		#endregion
		
		//Methods fpr parsing the incoming message
		// cmd.seq:api.method.ver(Param1, Param2,...)
		public static string GetAPIName(string pMessage) {
			var _indx1 = pMessage.IndexOf(':');
			var _indx2 = pMessage.IndexOf('.', _indx1);

			var _apiName = pMessage.Substring(_indx1 + 1, _indx2 - ( _indx1 + 1 ));
			Debug.Assert(!string.IsNullOrEmpty(_apiName));

			return _apiName;
		}

		// cmd.seq:api.method.ver(Param1, Param2,...)
		public static string GetCommandNameAndVersion(string pMessage) {
			var _indx = pMessage.IndexOf(':');
			var _indx1 = pMessage.IndexOf('.', _indx);
			var _indx2 = pMessage.IndexOf('(', _indx1);
			var _temp = pMessage.Substring(_indx1 + 1, _indx2 - (_indx1 + 1));
			return _temp;
		}

		// cmd.seq:api.method.ver(Param1, Param2,...)
		public static int GetSequence(string pMessage) {
			var _indx1 = pMessage.IndexOf('.');
			var _indx2 = pMessage.IndexOf(':', _indx1);
			var _temp = pMessage.Substring(_indx1 + 1, _indx2 - (_indx1 + 1));
			var _retInt = Int32.Parse(_temp);
			return _retInt;
		}

		// cmd.seq:api.method.ver(Param1, Param2,...)
		public static string[] GetInParameters(string pMessage) {
			return getParameters(pMessage, IN_DELIMETER);
		}

		public static string[] GetOutParameters(string pMessage) {
			return getParameters(pMessage, OUT_DELIMETER);
		}

		//-------------------- Private --------------------------------------------------
		private static string[] getParameters(string pMessage, string pDelimeter) {
			var _indx1 = pMessage.IndexOf('(');
			var _indx2 = pMessage.LastIndexOf(')');
			var _paramsString = pMessage.Substring(_indx1 + 1, _indx2 - (_indx1 + 1));
			var _params = Regex.Split(_paramsString, pDelimeter); 
			for (var _i=0; _i<_params.Length; _i++) {
				if (_params[_i].CompareTo(UNKNOWN_VALUE) == 0) {
					_params[_i] = string.Empty;
				}
			}
			return _params;
		}
	}
}
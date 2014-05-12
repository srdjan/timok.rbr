using System;
using Timok.Logger;

namespace Timok.Rbr.Core {
	public class SIPHelper {
		public static string GetIPAddress(string pAlias) {
			if (pAlias == null) {
				return string.Empty;
			}

			var _ip = extractOrigIP_v1(pAlias);
			if (string.IsNullOrEmpty(_ip)) {
				_ip = extractOrigIP_v2(pAlias);
			}
			return _ip;
		}

		public static string GetUserId(string pAlias, LogDelegate pLog) {
			if (pAlias == null) {
				return string.Empty;
			}

			var _sipUserId = string.Empty;
			try {
				var _indx = pAlias.IndexOf("id$");
				if (_indx == -1) {
					pLog(LogSeverity.Error, "SIPHelper.GetUserId", string.Format("Possibly Invalid SipUser Id: {0}", pAlias));
					return _sipUserId;
				}
				_sipUserId = pAlias.Substring(_indx + 3, pAlias.Length - _indx - 3);
				if (_sipUserId.IndexOf(':') > 0) {
					_sipUserId = _sipUserId.Substring(0, _sipUserId.IndexOf(':'));
				}
				else {
					pLog(LogSeverity.Status, "GkTelnetClient.GetUserId", string.Format("Invalid SipUser Id(2): {0}", pAlias));
				}
			}
			catch (Exception _ex) {
				pLog(LogSeverity.Critical, "SIPHelper.GetUserId", string.Format("Exception:\r\n{0}", _ex));
			}
			return _sipUserId;
		}

		//----------------------------- Private -----------------------------------------------------------------
		static string extractOrigIP_v1(string pAlias) {
			var _origEPaddress = string.Empty;

			try {
				//extract all after ip$ (if exist
				var _indx = pAlias.IndexOf("ip$");
				if (_indx == -1) {
					//log(LogSeverity.Error, "SIPHelper.extractIP_v1", string.Format("_indx == -1 _origEPaddress={0}", pAlias));
					return _origEPaddress;
				}
				_origEPaddress = pAlias.Substring(_indx + 3, pAlias.Length - _indx - 3);

				//extract ':H323_ID'
				if (_origEPaddress.IndexOf(':') > 0) {
					_origEPaddress = _origEPaddress.Substring(0, _origEPaddress.IndexOf(':'));
				}
				else {
					//pLog(LogSeverity.Error, "SIPHelper.extractIP_v1", string.Format("Invalid _origEPaddress={0}", pAlias));
				}
			}
			catch (Exception _ex) {
				throw new Exception(string.Format("SIPHelper.extractIP_v1: Exception:\r\n{0}", _ex));
			}
			return _origEPaddress;
		}

		static string extractOrigIP_v2(string pAlias) {
			var _origEPaddress = string.Empty;

			try {
				//extract square brackets
				var _indx = pAlias.IndexOf('[');
				if (_indx > -1) {
					_origEPaddress = pAlias.Substring(_indx + 1, pAlias.Length - _indx - 1);
				}
				_indx = _origEPaddress.IndexOf(']');
				if (_indx > -1) {
					_origEPaddress = _origEPaddress.Substring(0, _indx);
				}
			}
			catch (Exception _ex) {
				throw new Exception(string.Format("SIPHelper.extractIP_v2: Exception:\r\n{0}", _ex));
			}
			return _origEPaddress;
		}
	}
}
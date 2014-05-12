using System;
using System.Text.RegularExpressions;

namespace Timok.Core.NetworkLib {
	public class IPUtil {
		public const string PV4_REG_EXPR_PATTERN = @"\A(25[0-5]|2[0-4]\d|[0-1]?\d?\d)(\.(25[0-5]|2[0-4]\d|[0-1]?\d?\d)){3}\z"; //IPv4 

		public static int ToInt32(string pIPAddressStr) {
			var _ipAddressInt = 0;

			try {
				var _indx = pIPAddressStr.IndexOf(':');
				if (_indx <= -1) {}
				else {
					pIPAddressStr = pIPAddressStr.Remove(_indx);
				}

				if (pIPAddressStr == null || pIPAddressStr.Length < 7 || pIPAddressStr.Length > 15) {
					//T.LogRbr(LogSeverity.Error, "IPUtil.ToInt32", string.Format("Invalid IPAddress: {0}", pIPAddressStr));
					return _ipAddressInt;
				}

				var _ba = new byte[4] {0, 0, 0, 0};
				var _len = pIPAddressStr.IndexOf('.');
				_ba[0] = Convert.ToByte(pIPAddressStr.Substring(0, _len));

				var _startIndx = _len + 1;
				_len = pIPAddressStr.IndexOf('.', _startIndx);
				_ba[1] = Convert.ToByte(pIPAddressStr.Substring(_startIndx, _len - _startIndx));

				_startIndx = _len + 1;
				_len = pIPAddressStr.IndexOf('.', _startIndx);
				_ba[2] = Convert.ToByte(pIPAddressStr.Substring(_startIndx, _len - _startIndx));

				_startIndx = _len + 1;
				_ba[3] = Convert.ToByte(pIPAddressStr.Substring(_startIndx));
				_ipAddressInt = BitConverter.ToInt32(_ba, 0);
			}
			catch (Exception _ex) {
				//T.LogRbr(LogSeverity.Error, "IPUtil.ToInt32", string.Format("IPAddress={0}, Exception:\r\n{1}", pIPAddressStr, _ex));
				_ipAddressInt = 0;
			}
			return _ipAddressInt;
		}

		public static string ToString(int pIPAddressInt) {
			string _ipAddressStr;

			try {
				var _ba = BitConverter.GetBytes(pIPAddressInt);
				_ipAddressStr = string.Format((_ba[0] + "." + _ba[1] + "." + _ba[2] + "." + _ba[3]));
			}
			catch (Exception _ex) {
				//T.LogRbr(LogSeverity.Error, "IPUtil.ToString", string.Format("IPAddressInt={0}, Exception:\r\n{1}", pIPAddressInt, _ex));
				_ipAddressStr = string.Empty;
			}
			return _ipAddressStr;
		}

		public static long ToRange(string pIPAddressStr) {
			var _ba = new long[4] {0, 0, 0, 0};

			var _len = pIPAddressStr.IndexOf('.');
			_ba[0] = long.Parse(pIPAddressStr.Substring(0, _len));

			var _startIndx = _len + 1;
			_len = pIPAddressStr.IndexOf('.', _startIndx);
			_ba[1] = long.Parse(pIPAddressStr.Substring(_startIndx, _len - _startIndx));

			_startIndx = _len + 1;
			_len = pIPAddressStr.IndexOf('.', _startIndx);
			_ba[2] = long.Parse(pIPAddressStr.Substring(_startIndx, _len - _startIndx));

			_startIndx = _len + 1;
			_ba[3] = long.Parse(pIPAddressStr.Substring(_startIndx));

			var _range = _ba[0] * 1000000000;
			_range += _ba[1] * 1000000;
			_range += _ba[2] * 1000;
			_range += _ba[3];
			//ulong _range = (ulong) (ba[0] * 1000000000 + ba[1] * 1000000 + ba[2] * 1000 + ba[3]);
			return _range;
		}

		public static string FromRange(long pIPAddress) {
			var _ipArray = new long[4] {0, 0, 0, 0};

			_ipArray[0] = pIPAddress / 1000000000;
			_ipArray[1] = (pIPAddress % 1000000000) / 1000000;
			_ipArray[2] = (pIPAddress % 1000000000 % 1000000) / 1000;
			_ipArray[3] = (pIPAddress % 1000000000 % 1000000) % 1000;

			var _ip = _ipArray[0] + "." + _ipArray[1] + "." + _ipArray[2] + "." + _ipArray[3];
			return _ip;
		}

		public static string FromRange(long pStart, long pStop) {
			var _start = new long[4] {0, 0, 0, 0};
			_start[0] = pStart / 1000000000;
			_start[1] = (pStart % 1000000000) / 1000000;
			_start[2] = (pStart % 1000000000 % 1000000) / 1000;
			_start[3] = (pStart % 1000000000 % 1000000) % 1000;

			var _stop = new long[4] {0, 0, 0, 0};
			_stop[2] = (pStop % 1000000000 % 1000000) / 1000;
			_stop[3] = (pStop % 1000000000 % 1000000) % 1000;

			var _ip = _start[0] + "." + _start[1] + "." + _start[2] + "-" + _stop[2] + "." + _start[3] + "-" + _stop[3];
			return _ip;
		}

		public static string ExtractIPAddress(string pIPAddressAndPort) {
			var _indx = pIPAddressAndPort.IndexOf(':');
			if (_indx > -1) {
				pIPAddressAndPort = pIPAddressAndPort.Remove(_indx);
			}
			return pIPAddressAndPort;
		}

		public static int ExtractIPPort(string pIPAddressAndPort) {
			var _indx = pIPAddressAndPort.IndexOf(':');
			return _indx > -1 ? int.Parse(pIPAddressAndPort.Remove(0, _indx)) : 0;
		}

		/// <summary>
		/// Parses IPAddress range to string array
		/// </summary>
		/// <param name="pIPAddressRange">valid values: 1.1.1.1  OR  1.1.1.1-10</param>
		/// <returns>string array of IP Addresses </returns>
		public static string[] ParseRange(string pIPAddressRange) {
			if (string.IsNullOrEmpty(pIPAddressRange)) {
				throw new ArgumentNullException("pIPAddressRange", "IPAddressRange cannot be 'null' or zero length");
			}

			string _ipAddressSeed;
			int _startLastByte;
			int _endLastByte;
			if (pIPAddressRange.IndexOf('-') > 0) {
				_endLastByte = parseRangeIP(out _ipAddressSeed, out _startLastByte, pIPAddressRange);
			}
			else {
				_endLastByte = parseSingleIP(out _ipAddressSeed, out _startLastByte, pIPAddressRange);
			}

			int _count = _endLastByte - _startLastByte + 1;
			if (_count <= 0) {
				throw new ArgumentException("Invalid IPAddress Range : [" + pIPAddressRange + "]", "pIPAddressRange");
			}
			//_count = (_count > 0) ? _count : 0;

			return getIPList(_count, _ipAddressSeed, _startLastByte, pIPAddressRange);
		}

		//-------------------------------------------- private ------------------------------------------------------
		static string[] getIPList(int pCount, string pIPAddressSeed, int pStartLastByte, string pIPAddressRange) {
			var _list = new string[pCount];
			var _last = pStartLastByte;
			var _regEx = new Regex(PV4_REG_EXPR_PATTERN, RegexOptions.Compiled);
			for (var _i = 0; _i < _list.Length; _i++) {
				var _ip = string.Concat(pIPAddressSeed, '.', _last);
				if (! _regEx.IsMatch(_ip)) {
					throw new ArgumentException("Invalid IPAddress in the Range : [" + pIPAddressRange + "]", "pIPAddressRange");
				}
				_list[_i] = _ip;
				_last++;
			}
			return _list;
		}

		static int parseSingleIP(out string pIPAddressSeed, out int pStartLastByte, string pIPAddressRange) {
			var _startIP = pIPAddressRange;
			var _bytes = _startIP.Split('.');
			if (_bytes == null || _bytes.Length != 4) {
				throw new ArgumentException(string.Format("Invalid IPAddress Range: {0}", pIPAddressRange), "pIPAddressRange");
			}
			pIPAddressSeed = string.Join(".", _bytes, 0, 3);
			pStartLastByte = int.Parse(_startIP.Split('.')[3]);
			var _endLastByte = pStartLastByte;
			return _endLastByte;
		}

		static int parseRangeIP(out string pIPAddressSeed, out int pStartLastByte, string pIPAddressRange) {
			var _range = pIPAddressRange.Split('-');
			if (_range == null || _range.Length != 2) {
				throw new ArgumentException("Invalid IPAddress Range : [" + pIPAddressRange + "]", "pIPAddressRange");
			}
			var _startIP = _range[0];
			var _endLastByte = int.Parse(_range[1]);
			if (_endLastByte < 0 || _endLastByte > byte.MaxValue) {
				throw new ArgumentException("Invalid IPAddress Range : [" + pIPAddressRange + "]", "pIPAddressRange");
			}

			var _bytes = _startIP.Split('.');
			if (_bytes == null || _bytes.Length != 4) {
				throw new ArgumentException("Invalid IPAddress Range : [" + pIPAddressRange + "]", "pIPAddressRange");
			}
			pIPAddressSeed = string.Join(".", _bytes, 0, 3);
			pStartLastByte = int.Parse(_bytes[3]);
			return _endLastByte;
		}
	}
}
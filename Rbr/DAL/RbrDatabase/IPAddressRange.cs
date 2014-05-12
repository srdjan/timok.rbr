using System;
using System.Collections;
using System.Xml.Serialization;
using Timok.Core;
using Timok.NetworkLib;

namespace Timok.Rbr.DAL.RbrDatabase {
	[Serializable]
	public class IPAddressRange : IComparable {
		readonly string endIP;
		readonly int endLastByte = short.MinValue;
		readonly string ipAddressSeed;
		readonly string startIP;
		readonly int startLastByte = int.MinValue;

		public IPAddressRange() {
			//startIP = null; 
			//endIP = null;
		}

		//
		//		public IPAddressRange(int pStartIP, int pEndIP) {
		//			startIP = IPUtil.ToString(pStartIP); 
		//			endIP = IPUtil.ToString(pEndIP);
		//		}

		public IPAddressRange(IPAddressRow[] pIPAddressRows) {
			if (pIPAddressRows != null && pIPAddressRows.Length > 0) {
				if (pIPAddressRows.Length == 1) {
					startIP = pIPAddressRows[0].DottedIPAddress;
					endIP = pIPAddressRows[0].DottedIPAddress;
					startLastByte = pIPAddressRows[0]._lastByte;
					endLastByte = pIPAddressRows[0]._lastByte;
				}
				else {
					Array.Sort(pIPAddressRows, new ObjectComparer(IPAddressRow._lastByte_PropName, false));
					startIP = pIPAddressRows[0].DottedIPAddress;
					endIP = pIPAddressRows[pIPAddressRows.Length - 1].DottedIPAddress;
					startLastByte = pIPAddressRows[0]._lastByte;
					endLastByte = pIPAddressRows[pIPAddressRows.Length - 1]._lastByte;
				}
				ipAddressSeed = startIP.Substring(0, startIP.LastIndexOf('.'));
			}
		}

		public IPAddressRange(string ip_address_range) {
			if (ip_address_range == null) {
				throw new ArgumentNullException("ip_address_range", "ip_address_range cannot be 'null'");
			}
			if (ip_address_range.Length > 0) {
				if (ip_address_range.IndexOf('-') > 0) {
					string[] _range = ip_address_range.Split('-');
					if (_range == null || _range.Length != 2) {
						throw new ArgumentException("Invalid IPAddress Range : [" + ip_address_range + "]", "ip_address_range");
					}
					startIP = _range[0];
					endLastByte = int.Parse(_range[1]);

					string[] _bytes = startIP.Split('.');
					if (_bytes == null || _bytes.Length != 4) {
						throw new ArgumentException("Invalid IPAddress Range : [" + ip_address_range + "]", "ip_address_range");
					}
					ipAddressSeed = string.Join(".", _bytes, 0, 3);
					endIP = ipAddressSeed + "." + _range[1];

					startLastByte = int.Parse(_bytes[3]);
				}
				else {
					startIP = ip_address_range;
					endIP = (string) startIP.Clone();
					string[] _bytes = startIP.Split('.');
					if (_bytes == null || _bytes.Length != 4) {
						throw new ArgumentException("Invalid IPAddress Range : [" + ip_address_range + "]", "ip_address_range");
					}
					ipAddressSeed = string.Join(".", _bytes, 0, 3);
					startLastByte = int.Parse(startIP.Split('.')[3]);
					endLastByte = startLastByte;
				}
			}
			else {
				throw new ArgumentException("Invalid IPAddress Range : [" + ip_address_range + "]", "ip_address_range");
			}
		}

		[XmlIgnore]
		public string StartIPAddress { get { return startIP; } }

		[XmlIgnore]
		public string EndIPAddress { get { return endIP; } }

		[XmlIgnore]
		public string IPAddressSeed { get { return ipAddressSeed; } }

		[XmlIgnore]
		public int StartLastByte { get { return startLastByte; } }

		[XmlIgnore]
		public int EndLastByte { get { return endLastByte; } }

		[XmlIgnore]
		public int Count {
			get {
				int _count = endLastByte - startLastByte + 1;
				return (_count > 0) ? _count : 0;
			}
		}

		[XmlIgnore]
		public int[] IPAddressInt32List {
			get {
				var _list = new int[Count];
				int _last = startLastByte;
				for (int i = 0; i < _list.Length; i++) {
					string _ip = string.Concat(ipAddressSeed, '.', _last);
					_list[i] = IPUtil.ToInt32(_ip);
					_last++;
				}
				return _list;
			}
		}

		[XmlIgnore]
		public string[] IPAddressStringList {
			get {
				var _list = new string[Count];
				int _last = startLastByte;
				for (int i = 0; i < _list.Length; i++) {
					string _ip = string.Concat(ipAddressSeed, '.', _last);
					_list[i] = _ip;
					_last++;
				}
				return _list;
			}
		}

		#region IComparable Members

		public int CompareTo(object obj) {
			IComparer _comp = new StringLogicalComparer();
			//System.Collections.IComparer _comp = System.Collections.Comparer.Default;
			return _comp.Compare(ToString(), obj.ToString());
		}

		#endregion

		public override string ToString() {
			if (startIP != null && endIP != null
			    && startIP.Length > 0 && endIP.Length > 0) {
				if (startIP == endIP) {
					return startIP;
				}
				else {
					return startIP + "-" + EndLastByte;
				}
			}
			else {
				return string.Empty;
			}
		}
	}
}
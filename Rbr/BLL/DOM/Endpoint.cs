using System;
using Timok.Logger;
using Timok.NetworkLib;
using Timok.Rbr.Core;
using Timok.Rbr.Core.Config;
using Timok.Rbr.DAL.RbrDatabase;

namespace Timok.Rbr.BLL.DOM {
	public sealed class Endpoint {
		readonly CPSMeter cpsMeter;
		readonly EndPointRow endPointRow;

		public short Id { get { return endPointRow.End_point_id; } }
		public EndPointType Type { get { return endPointRow.EndPointType; } }
		public EndPointProtocol Protocol { get { return endPointRow.EndPointProtocol; } }
		public int ExtType { get { return (endPointRow.Type * 10 + endPointRow.Registration); } }
		public EPRegistration Registration { get { return (EPRegistration) endPointRow.Registration; } }
		public bool RegistrationRequired { get { return Registration == EPRegistration.Required ? true : false; } }
		public bool IsRegistered { get { return endPointRow.IsRegistered; } set { endPointRow.IsRegistered = value; } }
		public string Alias { get { return endPointRow.Alias; } }
		public bool WithAliasAuthentication { get { return endPointRow.WithAliasAuthentication; } }
		public string Password { get { return endPointRow.Password; } }
		public Status Status { get { return endPointRow.EPStatus; } }
		public bool WithPrefixes { get { return endPointRow.WithInPrefixes; } }

		public string IPAddressRangeAndPort { get { return string.Format("{0}:{1}", endPointRow.DottedIPAddressRange, endPointRow.Port); } }

		//NOTE: may not be the best way, if the endpoint is multy ip, this returns always the first in range.
		public string IPAddress { get { return endPointRow.IPAddressRange.StartIPAddress; } }
		public int IPAddressAsInt { get { return IPUtil.ToInt32(endPointRow.IPAddressRange.StartIPAddress); } }

		public int Port { get { return endPointRow.Port; } }

		bool prefixInfoIsNotSet = true;
		byte prefixDelimiter;
		public byte PrefixDelimiter { 
			get {
				if (prefixInfoIsNotSet) {
					prefixInfoIsNotSet = false;
					setPrefixInfo();
				}
				return prefixDelimiter;
			} 
		}

		byte prefixLength;
		public byte PrefixLength {
			get {
				if (prefixInfoIsNotSet) {
					prefixInfoIsNotSet = false;
					setPrefixInfo();
				}
				return prefixLength;
			}
		}

		public bool IsOrigination { get { return UsedAsOrigination; } }
		public bool IsTermination { get { return UsedAsTermination; } }

		int dialPeerCount = -1;
		public int DialPeerCount {
			get {
				if (dialPeerCount == -1) {
					using (var _db = new Rbr_Db()) {
						dialPeerCount = _db.DialPeerCollection.GetCountByEndPointID(Id);
					}
				}
				return dialPeerCount;
			}
		}

		int carrEPMapCount = -1;
		public int CarrEndpointMapCount {
			get {
				if (carrEPMapCount == -1) {
					using (var _db = new Rbr_Db()) {
						carrEPMapCount = _db.CarrierAcctEPMapCollection.GetCountByEndPointId(Id);
					}
				}
				return carrEPMapCount;
			}
		}

		public bool UsedAsOrigination { get { return DialPeerCount > 0; } }
		public bool UsedAsTermination { get { return CarrEndpointMapCount > 0; } }
		public bool IsBidirectional { get { return UsedAsOrigination && UsedAsTermination; } }

		public string DirectionText {
			get {
				if (IsBidirectional) {
					return "2-WAY";
				}
				if (UsedAsOrigination) {
					return "IN";
				}
				if (UsedAsTermination) {
					return "OUT";
				}
				return "NONE"; //string.Empty;//"n/a";
			}
		}

		public EndPointDirection Direction() {
			if (IsBidirectional) {
				return EndPointDirection.BOTH;
			}
			if (UsedAsOrigination) {
				return EndPointDirection.IN;
			}
			if (UsedAsTermination) {
				return EndPointDirection.OUT;
			}
			return EndPointDirection.BOTH;
		}

		public Endpoint(EndPointRow pEndPointRow) {
			endPointRow = pEndPointRow;
			cpsMeter = new CPSMeter(IPAddressRangeAndPort, endPointRow.Max_calls <= 0 ? 75 : endPointRow.Max_calls, TimokLogger.Instance.LogRbr);
		}

		//--------------------- Public static methods ---------------------------------------
		public static Endpoint Get(string pIPAddress) {
			Helper.CheckIP(pIPAddress);

			EndPointRow _endPointRow;
			try {
				using (var _db = new Rbr_Db()) {
					_endPointRow = _db.EndPointCollection.GetByIPAddress(pIPAddress);
				}
			}
			catch (Exception _ex) {
				throw new Exception(string.Format("EndPoint={0}, Exception:\r\n{1}", pIPAddress, _ex));
			}
			return _endPointRow == null ? null : new Endpoint(_endPointRow);
		}

		//--------------------- Public instance methods -------------------------------------
		public void TakeSample() {
			cpsMeter.TakeSample();
		}

		public string GetPrefixIn(string pDestNumber) {
			var _prefixIn = string.Empty;

			if (WithPrefixes) {
				if (PrefixDelimiter > 0) {
					int _delimeterIndex = pDestNumber.IndexOf((char) PrefixDelimiter);
					if (_delimeterIndex < 0) {
						//throw new RbrException(RbrResult.Prefix_NotValid, "Endpoint.GetPrefixIn", string.Format("DestNumber={0}, EPId={1}", pDestNumber, Id));
						return string.Empty; //Call Cenetrr ?>
					}
					if (pDestNumber.Length > ( _delimeterIndex + 1 )) {
						_prefixIn = pDestNumber.Substring(0, _delimeterIndex + 1);
					}
				}
				else if (PrefixLength > 0) {
					if (pDestNumber.Length > PrefixLength) {
						_prefixIn = pDestNumber.Substring(0, PrefixLength);
					}
				}
				else {
					throw new RbrException(RbrResult.Prefix_TypeNotValid, "Endpoint.GetPrefixIn", string.Format("DestNumber={0}, EpId={1}", pDestNumber, Id));
				}
			}
			return _prefixIn;
		}

		//------------------------ Private --------------------------------------------------------------------------
		void setPrefixInfo() {
			PrefixInTypeRow _prefixInTypeRow;
			using (var _db = new Rbr_Db()) {
				_prefixInTypeRow = _db.PrefixInTypeCollection.GetByPrimaryKey(endPointRow.Prefix_in_type_id);
			}
			if (_prefixInTypeRow == null) {
				throw new Exception(string.Format("EndPoint.Ctor | Exception, PrefixInType == null, EPAlias: {0}", Alias));
			}
			prefixDelimiter = _prefixInTypeRow.Delimiter;
			prefixLength = _prefixInTypeRow.Length;
		}
	}
}
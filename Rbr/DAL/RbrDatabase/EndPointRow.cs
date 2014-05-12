// <fileinfo name="EndPointRow.cs">
//		<copyright>
//			Copyright © 2002-2007 Timok ES LLC. All rights reserved.
//		</copyright>
//		<remarks>
//			You can update this source code manually. If the file
//			already exists it will not be rewritten by the generator.
//		</remarks>
//		<generator rewritefile="False" infourl="http://www.SharpPower.com">RapTier</generator>
// </fileinfo>

using System;
using System.Xml.Serialization;
using Timok.Rbr.Core;
using Timok.Rbr.Core.Config;
using Timok.Rbr.DAL.RbrDatabase.Base;

namespace Timok.Rbr.DAL.RbrDatabase {
	/// <summary>
	/// Represents a record in the <c>EndPoint</c> table.
	/// </summary>
	[Serializable]
	public class EndPointRow : EndPointRow_Base {
		public const string IP_ADDRESS_RANGE_PROP_NAME = "IPAddressRange";

		public const string REQUIRED_REGISTRATION_PROP_NAME = "RequiredRegistration";
		public const string REQUIRED_REGISTRATION_DISPLAY_NAME = "RR";

		public const string IS_REGISTERED_PROP_NAME = "IsRegistered";
		public const string IS_REGISTERED_DISPLAY_NAME = "Is Reg";

		public const string END_POINT_TYPE_PROP_NAME = "EndPointType";
		public const string END_POINT_TYPE_DISPLAY_NAME = "Type";

		public const string END_POINT_PROTOCOL_PROP_NAME = "EndPointProtocol";
		public const string END_POINT_PROTOCOL_DISPLAY_NAME = "Protocol";

		public const string EP_STATUS_PROP_NAME = "EPStatus";
		public const string EP_STATUS_DISPLAY_NAME = "Status";

		[XmlIgnore]
		public bool IsBidirectionalCapable {
			get {
				if (IPAddressRange != null) {
					return IPAddressRange.Count == 1;
				}
				else {
					return false;
				}
			}
		}

		int dialPeerCount = -1;
		public int DialPeerCount {
			get {
				if (dialPeerCount == -1) {
					using (Rbr_Db _db = new Rbr_Db()) {
						dialPeerCount = _db.DialPeerCollection.GetCountByEndPointID(End_point_id);
					}
				}
				return dialPeerCount;
			}
		}

		[XmlIgnore]
		public IPAddressRange IPAddressRange {
			get {
				if (Ip_address_range != null && Ip_address_range.Length > 0) {
					return new IPAddressRange(Ip_address_range);
				}
				else {
					return null;
				}
			}
			set { Ip_address_range = value.ToString(); }
		}

		[XmlIgnore]
		public string DottedIPAddressRange { get { return Ip_address_range; } set { Ip_address_range = value; } }

		[XmlIgnore]
		public EndPointType EndPointType { get { return (EndPointType) Type; } set { Type = (byte) value; } }

		[XmlIgnore]
		public EndPointProtocol EndPointProtocol { get { return (EndPointProtocol) Protocol; } set { Protocol = (byte) value; } }

		[XmlIgnore]
		public bool RequiredRegistration {
			get { return (Registration == (byte) EPRegistration.Required); }
			set {
				if (value) {
					Registration = (byte) EPRegistration.Required;
				}
				else {
					Registration = (byte) EPRegistration.Permanent;
				}
			}
		}

		[XmlIgnore]
		public bool WithInPrefixes { get { return Prefix_in_type_id != AppConstants.PrefixTypeId_NoPrefixes; } }

		[XmlIgnore]
		public Status EPStatus { get { return (Status) Status; } set { Status = (byte) value; } }

		[XmlIgnore]
		public bool IsRegistered {
			get { return Is_registered > 0; }
			set {
				byte _isRegistered;
				if (value) {
					_isRegistered = 1;
				}
				else {
					_isRegistered = 0;
				}
				Is_registered = _isRegistered;
			}
		}

		[XmlIgnore]
		public bool WithAliasAuthentication { get { return With_alias_authentication > 0; } set { With_alias_authentication = value ? (byte) 1 : (byte) 0; } }

		public string NodeText() {
			string _text = string.Empty;
			_text += (Alias != null) ? Alias.PadRight(30) : "UNKNOWN";
			_text += string.Concat(" [", DottedIPAddressRange, "]").PadRight(22); //max 22 chars _[255.255.255.255-255]
			_text += string.Concat(" [", EndPointProtocol.ToString(), "]").PadRight(4);
			_text += string.Concat(" [", EPStatus, "]").PadRight(11);

			return _text;
		}

		int carrEPMapCount = -1;
		public int CarrEndpointMapCount {
			get {
				if (carrEPMapCount == -1) {
					using (Rbr_Db _db = new Rbr_Db()) {
						carrEPMapCount = _db.CarrierAcctEPMapCollection.GetCountByEndPointId(End_point_id);
					}
				}
				return carrEPMapCount;
			}
		}

		public bool IsDeactivated(EndPointRow pOriginal) {
			if (EPStatus != pOriginal.EPStatus && pOriginal.EPStatus == Core.Config.Status.Active) {
				return true;
			}
			return false;
		}

		public bool IPAddressChanged(EndPointRow pOriginal) {
			if (Ip_address_range != pOriginal.Ip_address_range) {
				return true;
			}
			return false;
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

		public override string ToString() {
			return NodeText();
		}
	} // End of EndPointRow class
} // End of namespace
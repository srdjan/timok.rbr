using System;
using Timok.Rbr.Core;
using Timok.Rbr.Core.Config;

namespace Timok.Rbr.DTO {
	[Serializable]
	public class CdrDto {
		#region Prop Names

		//prop names
		public const string ANI_PropName = "ANI";
		public const string CarrierAcctId_PropName = "CarrierAcctId";
		public const string CarrierAcctName_PropName = "CarrierAcctName";
		public const string CarrierCost_PropName = "CarrierCost";
		public const string CarrierMinutes_PropName = "CarrierMinutes";
		public const string CarrierRouteId_PropName = "CarrierRouteId";
		public const string CarrierRouteName_PropName = "CarrierRouteName";
		public const string Ccode_PropName = "Ccode";
		public const string CustomerAcctId_PropName = "CustomerAcctId";
		public const string CustomerAcctName_PropName = "CustomerAcctName";
		public const string CustomerDuration_PropName = "CustomerDuration";
		public const string CustomerMinutes_PropName = "CustomerMinutes";
		public const string CustomerPrice_PropName = "CustomerPrice";
		public const string CustomerRouteId_PropName = "CustomerRouteId";
		public const string CustomerRouteName_PropName = "CustomerRouteName";
		public const string DateLogged_PropName = "DateLogged";
		public const string DialedNumber_PropName = "DialedNumber";
		public const string DisconnectCause_PropName = "DisconnectCause";
		public const string DisconnectCauseNumber_PropName = "DisconnectCauseNumber";
		public const string DisconnectSource_PropName = "DisconnectSource";
		public const string DisconnectSourceNumber_PropName = "DisconnectSourceNumber";
		public const string DNIS_PropName = "DNIS";
		public const string EndUserPrice_PropName = "EndUserPrice";
		public const string Id_PropName = "Id";
		public const string InfoDigits_PropName = "InfoDigits";
		public const string Leg1Duration_PropName = "Leg1Duration";
		public const string Leg2Duration_PropName = "Leg2Duration";
		public const string LocalNumber_PropName = "LocalNumber";
		public const string MappedDisconnectCause_PropName = "MappedDisconnectCause";
		public const string NodeId_PropName = "NodeId";
		public const string NodeName_PropName = "NodeName";
		public const string OrigAlias_PropName = "OrigAlias";
		public const string OrigDotIPAddress_PropName = "OrigDotIPAddress";
		public const string OrigEndPointId_PropName = "OrigEndpointId";
		public const string OrigIPAddress_PropName = "OrigIPAddress";
		public const string OrigPartnerId_PropName = "OrigPartnerId";
		public const string OrigPartnerName_PropName = "OrigPartnerName";
		public const string PrefixIn_PropName = "PrefixIn";
		public const string PrefixOut_PropName = "PrefixOut";
		public const string RbrResult_PropName = "RbrResult";
		public const string ResellerPrice_PropName = "ResellerPrice";
		public const string RetailAcctId_PropName = "RetailAcctId";
		public const string RetailMinutes_PropName = "RetailMinutes";
		public const string SerialNumber_PropName = "SerialNumber";
		public const string Start_PropName = "Start";
		public const string StartString_PropName = "StartString";
		public const string TermAlias_PropName = "TermAlias";
		public const string TermEndPointId_PropName = "TermEndpointId";
		public const string TermIPAddressRange_PropName = "TermIPAddressRange";
		public const string TermPartnerId_PropName = "TermPartnerId";
		public const string TermPartnerName_PropName = "TermPartnerName";
		public const string TimokDate_PropName = "TimokDate";
		public const string UsedBonusMinutes_PropName = "UsedBonusMinutes";

		#endregion Prop Names

		int disconnectCauseNumber;
		byte disconnectSourceNumber;
		int mappedDisconnectCauseNumber;
		short rbrResultNumber;
		DateTime start;
		public string Guid { get; set; }

		public DateTime DateLogged { get; set; }

		public int TimokDate { get; set; }

		public DateTime Start { get { return start; } set { start = value; } }

		public string StartString {
			get {
				if (start > DateTime.MinValue) {
					return start.ToString("MM/dd/yyyy HH:mm:ss");
				}
				return string.Empty;
			}
		}

		public short Leg1Duration { get; set; }

		public short Leg2Duration { get; set; }

		public int Ccode { get; set; }

		public string LocalNumber { get; set; }

		public int CarrierRouteId { get; set; }

		public decimal CustomerPrice { get; set; }

		public decimal CarrierCost { get; set; }

		public int OrigIPAddress { get; set; }

		public short OrigEndpointId { get; set; }

		public short TermEndpointId { get; set; }

		public short CustomerAcctId { get; set; }

		public int DisconnectCauseNumber { get { return disconnectCauseNumber; } set { disconnectCauseNumber = value; } }

		public string DisconnectCause {
			get {
				if (Enum.IsDefined(typeof (GkDisconnectCause), disconnectCauseNumber)) {
					return ((GkDisconnectCause) disconnectCauseNumber) + " (" + disconnectCauseNumber + ")";
				}
				return "UNKNOWN (" + disconnectCauseNumber + ")";
			}
		}

		public byte DisconnectSourceNumber { get { return disconnectSourceNumber; } set { disconnectSourceNumber = value; } }

		public string DisconnectSource {
			get {
				if (Enum.IsDefined(typeof (GkDisconnectSource), disconnectSourceNumber)) {
					return ((GkDisconnectSource) disconnectSourceNumber) + " (" + disconnectSourceNumber + ")";
				}
				return "UNKNOWN (" + disconnectSourceNumber + ")";
			}
		}

		public short RbrResultNumber { get { return rbrResultNumber; } set { rbrResultNumber = value; } }

		public string RbrResult {
			get {
				if (Enum.IsDefined(typeof (RbrResult), rbrResultNumber)) {
					return ((RbrResult) rbrResultNumber) + " (" + rbrResultNumber + ")";
				}
				return "UNKNOWN (" + rbrResultNumber + ")";
			}
		}

		public string PrefixIn { get; set; }

		public string PrefixOut { get; set; }

		public long DNIS { get; set; }

		public long ANI { get; set; }

		public long SerialNumber { get; set; }

		public decimal EndUserPrice { get; set; }

		public short UsedBonusMinutes { get; set; }

		public decimal ResellerPrice { get; set; }

		public short NodeId { get; set; }

		public int CustomerRouteId { get; set; }

		public int MappedDisconnectCauseNumber { get { return mappedDisconnectCauseNumber; } set { mappedDisconnectCauseNumber = value; } }

		public string MappedDisconnectCause {
			get {
				if (Enum.IsDefined(typeof (GkDisconnectCause), mappedDisconnectCauseNumber)) {
					return ((GkDisconnectCause) mappedDisconnectCauseNumber) + " (" + mappedDisconnectCauseNumber + ")";
				}
				return "UNKNOWN (" + mappedDisconnectCauseNumber + ")";
			}
		}

		public short CarrierAcctId { get; set; }

		public string OrigDotIPAddress { get; set; }

		public string DialedNumber { get; set; }

		public decimal CustomerMinutes { get; set; }

		public short CustomerDuration { get; set; }

		public decimal CarrierMinutes { get; set; }

		public short CarrierDuration { get; set; }

		public decimal RetailMinutes { get; set; }

		public short RetailDuration { get; set; }

		public int RetailAcctId { get; set; }

		public string CarrierRouteName { get; set; }

		public string CustomerRouteName { get; set; }

		public string OrigAlias { get; set; }

		public string TermAlias { get; set; }

		public string TermIPAddressRange { get; set; }

		public string CustomerAcctName { get; set; }

		public int OrigPartnerId { get; set; }

		public string OrigPartnerName { get; set; }

		public string CarrierAcctName { get; set; }

		public int TermPartnerId { get; set; }

		public string TermPartnerName { get; set; }

		public string NodeName { get; set; }

		public byte InfoDigits { get; set; }
	}
}
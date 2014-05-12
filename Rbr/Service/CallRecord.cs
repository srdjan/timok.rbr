using System;
using System.Text;
using Timok.Core;
using Timok.NetworkLib;
using Timok.Rbr.Core;
using Timok.Rbr.Core.Config;

namespace Timok.Rbr.Service {
	public class CallRecord {
		const string ANI_SUFIX = ":dialedDigits";
		const string DNIS_PREFIX = "DNIS:";
		const string SERIAL_NUMBER_PREFIX = "S#:";
		const string UNKNOWN = "0.0.0.0:0";

		int carrierAcctId;
		int carrierBaseRouteId;
		int customerBaseRouteId;
		string destIPAddress;
		int disconnectCause;
		byte disconnectSource;
		DateTime callStart;
		DateTime callEnd;
		DateTime leg2Start;
		DateTime leg2End;
		int mappedDisconnectCause;
		string origIPAddress;
		int rbrResult;
		long sequence;

		readonly string callId;
		public string CallId { get { return callId; } }

		public string ANI { get; set; }

		public string AccessNumber { get; set; }

		public byte InfoDigits { get; set; }

		public long SerialNumber { get; set; }

		public int RetailAcctId { get; set; }

		public string DestNumber { get; set; }

		public string CardNumber { get; set; }

		public short CustomerAcctId { get; set; }

		public int Leg1Length { get; set; }

		//public bool IsLeg1 { get { return leg2Start == callStart; } }

		public CallRecord(ISession pSession) {
			sequence = 0;
			callId = pSession.CallId; 
			CustomerAcctId = pSession.CustomerAcctId;
			ANI = pSession.ANI;
			AccessNumber = pSession.AccessNumber;
			InfoDigits = pSession.InfoDigits;
			SerialNumber = 0;
			origIPAddress = pSession.OrigIPAddress;
			destIPAddress = UNKNOWN;
			DestNumber = string.Empty;
			disconnectCause = (int) GkDisconnectCause.CallRejected;
			disconnectSource = (byte) GkDisconnectSource.Unknown;
			rbrResult = (int) RbrResult.Unknown;
			mappedDisconnectCause = (int) GkDisconnectCause.CallRejected;
			start();
		}

		void start() {
			Leg1Length = 0;
			callStart = DateTime.Now;
			callEnd = callStart;
			leg2Start = DateTime.MinValue;
			leg2End = leg2Start;
		}

		public void Leg2Start() {
			leg2Start = DateTime.Now;
			leg2End = leg2Start;
			Leg1Length = (int) leg2Start.Subtract(callStart).TotalSeconds;
		}

		public void End() {
			callEnd = DateTime.Now;
			if (leg2Start != DateTime.MinValue) {
				leg2End = callEnd;
			}
		}

		public void Set(string pDNIS, byte pInfoDigits, string pANI, string pOrigIPAddr) {
			sequence++;
			AccessNumber = pDNIS;
			InfoDigits = pInfoDigits;
			ANI = pANI;
			origIPAddress = pOrigIPAddr;
		}

		public void Set(LegIn pLegIn) {
			CustomerAcctId = pLegIn.CustomerAcctId;
			customerBaseRouteId = pLegIn.CustomerRouteId;
		}

		public void Set(LegOut pLegOut) {
			destIPAddress = IPUtil.ExtractIPAddress(pLegOut.DestIPAndPort);
			carrierAcctId = pLegOut.CarrierAcctId;
			carrierBaseRouteId = pLegOut.CarrierBaseRouteId;
		}

		public void SetCause(IVRDisconnectCause pDisconnectCause, IVRDisconnectSource pDisconnectSource, RbrResult pRbrResult) {
			switch (pDisconnectCause) {
				case IVRDisconnectCause.LC_NORMAL:
					disconnectCause = (int) GkDisconnectCause.NormalCallClearing;
					break;
				case IVRDisconnectCause.LC_NUMBER_BUSY:
					disconnectCause = (int) GkDisconnectCause.UserBusy;
					break;
				case IVRDisconnectCause.LC_NO_ANSWER:
					disconnectCause = (int) GkDisconnectCause.NoAnswer;
					break;
				case IVRDisconnectCause.LC_NUMBER_UNOBTAINABLE:
					disconnectCause = (int) GkDisconnectCause.UnallocatedNumber;
					break;
				case IVRDisconnectCause.LC_NUMBER_CHANGED:
					disconnectCause = (int) GkDisconnectCause.NumberChanged;
					break;
				case IVRDisconnectCause.LC_OUT_OF_ORDER:
					disconnectCause = (int) GkDisconnectCause.DestinationOutOfOrder;
					break;
				case IVRDisconnectCause.LC_INCOMING_CALLS_BARRED:
					disconnectCause = (int) GkDisconnectCause.InvalidNumberFormat;
					break;
				case IVRDisconnectCause.LC_CALL_REJECTED:
					disconnectCause = (int) GkDisconnectCause.CallRejected;
					break;
				case IVRDisconnectCause.LC_CALL_FAILED:
					disconnectCause = (int) GkDisconnectCause.ResourceUnavailable;
					break;
				case IVRDisconnectCause.LC_CHANNEL_BUSY:
					disconnectCause = (int) GkDisconnectCause.ChannelUnacceptable;
					break;
				case IVRDisconnectCause.LC_NO_CHANNELS:
					disconnectCause = (int) GkDisconnectCause.NoCircuitChannelAvailable;
					break;
				case IVRDisconnectCause.LC_CONGESTION:
					disconnectCause = (int) GkDisconnectCause.Congestion;
					break;
				default:
					break;
			}

			disconnectSource = (byte) pDisconnectSource;
			rbrResult = (int) pRbrResult;

			mappedDisconnectCause = (int) pDisconnectCause;
		}

		public override string ToString() {
			var _strBuilder = new StringBuilder();

			//field[0]: Connected Call length
			var _length = leg2End.Subtract(leg2Start.ToLocalTime());
			_strBuilder.Append(_length.TotalSeconds.ToString("0"));
			_strBuilder.Append("|");

			//field[1]: Start Time
			var _w3CDateTime = new W3CDateTime(callStart.ToLocalTime());
			_strBuilder.Append(_w3CDateTime.ToString("X"));
			_strBuilder.Append("|");

			//field[2]: Stop Time
			_w3CDateTime = new W3CDateTime(callEnd.ToLocalTime());
			_strBuilder.Append(_w3CDateTime.ToString("X"));
			_strBuilder.Append("|");

			//field[3]: OrigIPAddress
			_strBuilder.Append(origIPAddress);
			_strBuilder.Append(":5070"); //TODO: add real orig port
			_strBuilder.Append("|");

			//field[4]: empty field: usually SIP user ID
			_strBuilder.Append("|");

			//field[5]: DestIPAddress
			_strBuilder.Append(destIPAddress);
			_strBuilder.Append("|");

			//field[6]: usually caller ID, we put SerialNumber
			_strBuilder.Append(string.Format("{0}{1}", SERIAL_NUMBER_PREFIX, SerialNumber));
			_strBuilder.Append("|");

			//field[7]: DestNumber
			_strBuilder.Append(DestNumber);
			_strBuilder.Append("|");

			//field[8]: ANI
			_strBuilder.Append(string.Format("{0}{1}", ANI, ANI_SUFIX));
			_strBuilder.Append("|");

			//field[9]: empty field: usually GkID, we put DNIS
			_strBuilder.Append(string.Format("{0}{1}", DNIS_PREFIX, AccessNumber));
			_strBuilder.Append("|");

			//field[10]: RetailAccId
			_strBuilder.Append(RetailAcctId.ToString());
			_strBuilder.Append("|");

			//field[11]: CustomerId
			_strBuilder.Append(CustomerAcctId.ToString());
			_strBuilder.Append("|");

			//field[12]: CarrierId
			_strBuilder.Append(carrierAcctId.ToString());
			_strBuilder.Append("|");

			//field[13]: RouteId
			_strBuilder.Append(customerBaseRouteId.ToString());
			_strBuilder.Append("|");

			//field[14]: CarrierRouteID
			_strBuilder.Append(carrierBaseRouteId.ToString());
			_strBuilder.Append("|");

			//field[15]: DisconnectCause
			_strBuilder.Append(disconnectCause.ToString());
			_strBuilder.Append("|");

			//field[16]: DisconnectSource
			_strBuilder.Append(disconnectSource.ToString());
			_strBuilder.Append("|");

			//field[17]: RbrResult
			_strBuilder.Append(rbrResult.ToString());
			_strBuilder.Append("|");

			//field[18]: MappedDisconnectCause
			_strBuilder.Append(mappedDisconnectCause.ToString());
			_strBuilder.Append("|");

			//field[19]: InfoDigits
			_strBuilder.Append(InfoDigits.ToString("D2"));
			_strBuilder.Append(";");

			return _strBuilder.ToString();
		}
	}
}
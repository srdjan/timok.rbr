using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Globalization;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading;
using Timok.Core;
using Timok.Logger;
using Timok.NetworkLib;
using Timok.Rbr.BLL.DOM.Repositories;
using Timok.Rbr.BLL.Entities;
using Timok.Rbr.Core;
using Timok.Rbr.Core.Config;
using Timok.Rbr.DAL.CdrDatabase;
using Timok.Rbr.DAL.CdrDatabase.Base;
using Timok.Rbr.DAL.RbrDatabase;
using Timok.Rbr.DOM;

namespace Timok.Rbr.BLL.DOM {
	public class Cdr : Persistable {
		
		readonly string alias;
		readonly CDRRow cdrRow;
		readonly string sipUserId;
		public short CarrierRoundedMinutes;
		public string CarrierRouteName;
		public short CustomerRoundedMinutes;
		public string CustomerRouteName;
		public string DestNumber;
		public readonly string OrigIP;
		public readonly string TermIP;
		public short RetailRoundedMinutes;

		public Cdr(CDRViewRow_Base pCdrViewRow) {
			if (pCdrViewRow == null) {
				throw new ArgumentNullException("pCdrViewRow");
			}
			cdrRow = new CDRRow();
			cdrRow.Start = pCdrViewRow.Start;
			cdrRow.Date_logged = pCdrViewRow.Date_logged;
			cdrRow.Duration = pCdrViewRow.Duration;
			cdrRow.Customer_duration = pCdrViewRow.Customer_duration;
			cdrRow.Retail_duration = pCdrViewRow.Retail_duration;
			cdrRow.Carrier_duration = pCdrViewRow.Carrier_duration;
			cdrRow.Ccode = pCdrViewRow.Ccode;
			cdrRow.Local_number = pCdrViewRow.Local_number;
			cdrRow.Carrier_route_id = pCdrViewRow.Carrier_route_id;
			cdrRow.Customer_route_id = pCdrViewRow.Customer_route_id;
			cdrRow.Price = pCdrViewRow.Price;
			cdrRow.Cost = pCdrViewRow.Cost;
			cdrRow.Orig_IP_address = pCdrViewRow.Orig_IP_address;
			OrigIP = IPUtil.ToString(cdrRow.Orig_IP_address);
			cdrRow.Term_end_point_id = pCdrViewRow.Term_end_point_id;
			cdrRow.Customer_acct_id = pCdrViewRow.Customer_acct_id;
			cdrRow.Carrier_acct_id = pCdrViewRow.Carrier_acct_id;
			cdrRow.Disconnect_cause = pCdrViewRow.Disconnect_cause;
			cdrRow.Mapped_disconnect_cause = pCdrViewRow.Mapped_disconnect_cause;
			cdrRow.Rbr_result = pCdrViewRow.Rbr_result;
			cdrRow.Prefix_in = pCdrViewRow.Prefix_in;
			cdrRow.Prefix_out = pCdrViewRow.Prefix_out;
			cdrRow.DNIS = pCdrViewRow.DNIS;
			cdrRow.ANI = pCdrViewRow.ANI;
			cdrRow.Serial_number = pCdrViewRow.Serial_number;
			cdrRow.Used_bonus_minutes = pCdrViewRow.Used_bonus_minutes;
			cdrRow.Node_id = pCdrViewRow.Node_id;
			cdrRow.Id = pCdrViewRow.Id;
		}

		public Cdr(CDRRow pCdrRow) {
			if (pCdrRow == null) {
				throw new ArgumentNullException("pCdrRow");
			}
			cdrRow = pCdrRow;
		}

		public Cdr(string pGuid, string pRawCdr, bool pDateLoggedFromCdr) {
			cdrRow = new CDRRow();
			cdrRow.Id = pGuid;
			cdrRow.ANI = 0;
			cdrRow.DNIS = 0;
			cdrRow.Price = decimal.Zero;
			cdrRow.Cost = decimal.Zero;
			cdrRow.End_user_price = decimal.Zero;
			cdrRow.Used_bonus_minutes = 0;
			cdrRow.Prefix_in = string.Empty;
			cdrRow.Prefix_out = string.Empty;
			cdrRow.Local_number = string.Empty;
			cdrRow.Node_id = (new CurrentNode()).Id;

			sipUserId = string.Empty;
			alias = string.Empty;

			//--preset Cdr fields, in case route == null
			CountryCode = 0;
			LocalNumber = string.Empty;
			CarrierRouteName = "UNKNOWN";
			CustomerRouteName = "UNKNOWN";

			#region cdr format: 

			//	0		2|																		//duration
			//	1		Wed. 09 Mar 2005 14:20:43 -0500|			//startDateTime
			//	2		Wed. 09 Mar 2005 14:20:45 -0500|			//StopDateTime
			//	3		192.168.1.4:1719|											//OrigIp
			//	4		8113_endp|														//callerID
			//	5		192.168.1.10:1720|										//TermIp
			//	6		8473_endp|														//calledID or S#:xxx
			//	7		01152222234567:dialedDigits|					//DestNumber
			//	8		|1112222															//ANI(ANI), orig Alias?
			//	9		|Gk Alias															//Gk Alias or DNIS:xxx;ANIxxx
			// 10		customer route id
			// 11		carrier route id
			// 12   customer route id
			// 13   carrier route id
			// 14   Release cause code
			// 15   Release source
			// 16   Rbr result
			// 17   Original Release cause code

			#endregion

			var _fields = pRawCdr.Split('|');

			//-- fields[0]: Call Duration
			cdrRow.Duration = Convert.ToInt16(_fields[0]);

			//-- fields[1]: start date/time
			var _startDateTime = _fields[1];

			//-- fields[2]: stop date/time
			var _endDateTime = _fields[2];

			cdrRow.Start = getStandardDateTime(_startDateTime, _endDateTime);
			cdrRow.Timok_date = TimokDate.Parse(cdrRow.Start);

			//-- late logged
			if (pDateLoggedFromCdr) {
				cdrRow.Date_logged = cdrRow.Start.AddMinutes((cdrRow.Duration / 60) + (cdrRow.Duration % 60 > 0 ? 1 : 0));
			}
			else {
				var _dtNow = DateTime.Now;
				cdrRow.Date_logged = new DateTime(_dtNow.Year, _dtNow.Month, _dtNow.Day, _dtNow.Hour, _dtNow.Minute, 0);
			}

			//-- fields[3]: Orig ip address
			var _origIP = _fields[3].Substring(0, _fields[3].IndexOf(':'));
			cdrRow.Orig_IP_address = IPUtil.ToInt32(_origIP);
			OrigIP = _origIP;

			//-- fields[4]: sipUserId ( m_callerId )
			sipUserId = SIPHelper.GetUserId(_fields[4], TimokLogger.Instance.LogRbr);
			if (sipUserId.Length > 0) {
				alias = sipUserId;
			}

			//-- fields[5]: TermIP address
			//75|Wed, 07 Feb 2007 18:45:17 -0500|Wed, 07 Feb 2007 18:46:32 -0500|67.130.1.45:1276|8435225294:dialedDigits=8435225294 :h323_ID=id$8435225294:h323_ID=ip$67.130.1.40:h323_ID|67.130.1.31:1720|2486_endp|18128772101:dialedDigits|8435225294|MUNDETEL-SW1|0|10000|10000|1|10306|16|0|0|16;)	  
			var _indx = _fields[5].IndexOf(':');
			if (_indx > -1) {
				TermIP = _fields[5].Substring(0, _indx);
			}
			else {
				TermIP = _fields[5];
			}
			//-- This is because Gk sends HostIP address in the CalledIP field when Rbr did not find termination
			//-- we want to show 0.0.0.0 in cdr
			if (TermIP.CompareTo(Configuration.Instance.Main.HostIP) == 0) {
				TermIP = "0.0.0.0";
			}

			//-- fields[6]: m_calledId - check if SerialNumber present
			//NOTE: some h323 endpoints send orig alias here
			//alias = tryAlias(_fields[6]);
			cdrRow.Serial_number = trySerialNumber(_fields[6]);
			TimokLogger.Instance.LogRbr(LogSeverity.Debug, "Cdr.Ctor", string.Format("Is Serial# ? field[6]: {0}", _fields[6]));

			//-- fields[7]: Dest number
			//TimokLogger.Instance.LogDebug(string.Format("Is Dest# ? field[7]: {0}", _fields[7]));
			_indx = _fields[7].IndexOf(':');
			if (_indx > -1) {
				DestNumber = _fields[7].Substring(0, _indx);
			}
			else {
				DestNumber = _fields[7];
			}

			if (DestNumber == null) {
				DestNumber = string.Empty;
			}

			//if (DestNumber.Length > 0) {
			//  CountryCode = int.Parse(DestNumber.Substring(0,1));
			//  LocalNumber = DestNumber.Substring(1);
			//}

			//-- fields[8]: ANI, Orig Alias
			//TimokLogger.Instance.LogDebug(string.Format("Is ANI/Alias ? field[8]: {0}", _fields[8]));
			/*
	sourceAddress = 4 entries {
		[0]=dialedDigits "3605759548"
		[1]=h323_ID  11 characters {
			0033 0036 0030 0035 0037 0035 0039 0035   36057595
			0034 0038 0020                            48 
		}
		[2]=h323_ID  13 characters {
			0069 0064 0024 0033 0036 0030 0035 0037   id$36057
			0035 0039 0035 0034 0038                  59548
		}
		[3]=h323_ID  14 characters {
			0069 0070 0024 0036 0037 002e 0031 0033   ip$67.13
			0030 002e 0031 002e 0034 0030             0.1.40
		} 
	}
	75|Wed, 07 Feb 2007 18:45:17 -0500|Wed, 07 Feb 2007 18:46:32 -0500|67.130.1.45:1276|8435225294:dialedDigits=8435225294 :h323_ID=id$8435225294:h323_ID=ip$67.130.1.40:h323_ID|67.130.1.31:1720|2486_endp|18128772101:dialedDigits|8435225294|MUNDETEL-SW1|0|10000|10000|1|10306|16|0|0|16;)	  
 */
			var _subFields = _fields[8].Split('=');
			foreach (var _subfield in _subFields) {
				if (alias.Length == 0) {
					alias = tryAlias(_subfield);
					if (alias.Length > 0) {
						continue;
					}
				}
				if (cdrRow.ANI == 0) {
					cdrRow.ANI = tryANI(_subfield);
					if (cdrRow.ANI > 0) {
						continue;
					}
				}
			}

			//-- fields[9]: Gk Alias - check if DNIS is here
			cdrRow.DNIS = tryDNIS(_fields[9]);
			//TimokLogger.Instance.LogDebug(string.Format("Is DNIS ? field[9]: {0}", _fields[9]));

			//-- fields[10]: retail acct id
			try {
				cdrRow.Retail_acct_id = int.Parse(_fields[10]);
			}
			catch {
				cdrRow.Retail_acct_id = 0;
			}
			//TimokLogger.Instance.LogDebug(string.Format("Is RetailAcct ? field[10]: {0}", _fields[10]));

			//-- fields[11]: customer acct id
			try {
				cdrRow.Customer_acct_id = short.Parse(_fields[11]);
			}
			catch {
				cdrRow.Customer_acct_id = 0;
			}
			//TimokLogger.Instance.LogDebug(string.Format("Is CustomerAcct ? field[11]: {0}", _fields[11]));

			//-- fields[12]: carrier routaccte id
			try {
				cdrRow.Carrier_acct_id = short.Parse(_fields[12]);
			}
			catch {
				cdrRow.Carrier_acct_id = 0;
			}
			//TimokLogger.Instance.LogDebug(string.Format("Is CarrierAcct ? field[12]: {0}", _fields[12]));

			//-- fields[13]: customer route id
			try {
				cdrRow.Customer_route_id = int.Parse(_fields[13]);
			}
			catch {
				cdrRow.Customer_route_id = 0;
			}
			//TimokLogger.Instance.LogDebug(string.Format("Is CustomerRoute ? field[13]: {0}", _fields[13]));

			//-- fields[14]: carrier route id
			try {
				cdrRow.Carrier_route_id = int.Parse(_fields[14]);
			}
			catch {
				cdrRow.Carrier_route_id = 0;
			}
			//TimokLogger.Instance.LogDebug(string.Format("Is CarrierAcct ? field[14]: {0}", _fields[14]));

			//-- fields[15]: Release cause code
			var _causeStr = _fields[15];
			//TimokLogger.Instance.LogDebug(string.Format("Is DisconnectCause ? field[15]: {0}", _fields[15]));
			try {
				cdrRow.Disconnect_cause = short.Parse(_causeStr);
			}
			catch {
				cdrRow.Disconnect_cause = (short) GkDisconnectCause.Unknown;
			}

			//-- fields[16]: Release source
			var _sourceStr = _fields[16];
			//TimokLogger.Instance.LogDebug(string.Format("Is SourceStr ? field[16]: {0}", _fields[16]));
			try {
				cdrRow.Disconnect_source = byte.Parse(_sourceStr);
			}
			catch {
				cdrRow.Disconnect_source = (byte) GkDisconnectSource.Unknown;
			}

			//-- fields[17]: Rbr result
			var _rbrResultStr = _fields[17];
			//TimokLogger.Instance.LogDebug(string.Format("Is RbrResult ? field[17]: {0}", _fields[17]));
			try {
				cdrRow.Rbr_result = short.Parse(_rbrResultStr);
			}
			catch {
				cdrRow.Rbr_result = (short) RbrResult.Unknown;
			}

			//-- fields[18]: Original Release cause code
			//TimokLogger.Instance.LogDebug(string.Format("Is MappedCauseStr ? field[18]: {0}", _fields[18]));
			try {
				cdrRow.Mapped_disconnect_cause = short.Parse(_fields[18]);
			}
			catch {
				cdrRow.Mapped_disconnect_cause = (short) GkDisconnectCause.Unknown;
			}

			//-- fields[19]: Original Release cause code
			//TimokLogger.Instance.LogDebug(string.Format("Is InfoDigits ? field[19]: {0}", _fields[19]));
			string _infoDigits = _fields[19].TrimEnd(';');
			try {
				cdrRow.Info_digits = byte.Parse(_infoDigits);
			}
			catch {
				cdrRow.Info_digits = 0;
			}
		}

		public Cdr(string pCallId, string pOrigIP, string pPrefixIn, string pDestNumber, short pCustomerAcctId, int pCustomerRouteId) {
			cdrRow = new CDRRow();
			cdrRow.Id = pCallId;
			cdrRow.Start = DateTime.Now;
			cdrRow.Date_logged = cdrRow.Start;
			cdrRow.Timok_date = TimokDate.Parse(cdrRow.Start);
			cdrRow.Duration = 0;
			cdrRow.Customer_duration = 0;
			cdrRow.Retail_duration = 0;
			cdrRow.Carrier_duration = 0;
			cdrRow.Ccode = 0;
			cdrRow.Local_number = pDestNumber;
			cdrRow.Carrier_route_id = 0;
			cdrRow.Customer_route_id = pCustomerRouteId;
			cdrRow.Price = (decimal) 0.0;
			cdrRow.Cost = (decimal) 0.0;
			cdrRow.Orig_IP_address = IPUtil.ToInt32(pOrigIP);
			OrigIP = IPUtil.ToString(cdrRow.Orig_IP_address);
			cdrRow.Term_end_point_id = 0;
			cdrRow.Customer_acct_id = pCustomerAcctId;
			cdrRow.Carrier_acct_id = 0;
			cdrRow.Disconnect_cause = 0;
			cdrRow.Mapped_disconnect_cause = 0;
			cdrRow.Rbr_result = (short) RbrResult.Unknown;
			cdrRow.Prefix_in = pPrefixIn;
			cdrRow.Prefix_out = string.Empty;
			cdrRow.DNIS = 0;
			cdrRow.ANI = 0;
			cdrRow.Serial_number = 0;
			cdrRow.Used_bonus_minutes = 0;
			cdrRow.Node_id = 0;
		}

		//public string CallId { set { cdrRow.Id = value; } }
		public CDRRow CdrRow { get { return cdrRow; } }

		public bool IsConnected {
			get {
				TimokLogger.Instance.LogRbr(LogSeverity.Debug, "Cdr.IsConnected", string.Format("duration={0}, discCause={1}", cdrRow.Duration, cdrRow.Disconnect_cause));
				if (cdrRow.Duration > 0 || cdrRow.Disconnect_cause == (short) GkDisconnectCause.NormalCallClearing || cdrRow.Disconnect_cause == (short) GkDisconnectCause.NormalUnspecified) {
					return true;
				}
				return false;
			}
		}

		public decimal Minutes { get { return Utils.CalculateMinutes(cdrRow.Duration); } }

		//----------------------- database fields -- serialization -------------------------------------------------
		public DateTime StartTime { get { return cdrRow.Start; } }

		public short Duration { get { return cdrRow.Duration; } }

		public short CustomerDuration { get { return cdrRow.Customer_duration; } set { cdrRow.Customer_duration = value; } }

		public short RetailDuration { get { return cdrRow.Retail_duration; } set { cdrRow.Retail_duration = value; } }

		public short CarrierDuration { get { return cdrRow.Carrier_duration; } set { cdrRow.Carrier_duration = value; } }

		public int CountryCode { get { return cdrRow.Ccode; } set { cdrRow.Ccode = value; } }

		public string LocalNumber { get { return cdrRow.Local_number; } set { cdrRow.Local_number = value; } }

		public int CarrierBaseRouteId { get { return cdrRow.Carrier_route_id; } set { cdrRow.Carrier_route_id = value; } }

		public int CustomerBaseRouteId { get { return cdrRow.Customer_route_id; } set { cdrRow.Customer_route_id = value; } }

		public decimal CustomerPrice { get { return cdrRow.Price; } set { cdrRow.Price = value; } }

		public decimal CarrierCost { get { return cdrRow.Cost; } set { cdrRow.Cost = value; } }

		public short OrigEPId { get { return cdrRow.Orig_end_point_id; } set { cdrRow.Orig_end_point_id = value; } }

		public short TermEPId { get { return cdrRow.Term_end_point_id; } set { cdrRow.Term_end_point_id = value; } }

		public short CustomerAcctId { get { return cdrRow.Customer_acct_id; } set { cdrRow.Customer_acct_id = value; } }

		public short CarrierAcctId { get { return cdrRow.Carrier_acct_id; } set { cdrRow.Carrier_acct_id = value; } }

		public short DisconnectCause { get { return cdrRow.Disconnect_cause; } set { cdrRow.Disconnect_cause = value; } }

		public short MappedDisconnectSource { get { return cdrRow.Mapped_disconnect_cause; } set { cdrRow.Mapped_disconnect_cause = value; } }

		public short Rbr_Result { get { return cdrRow.Rbr_result; } set { cdrRow.Rbr_result = value; } }

		public string PrefixIn { get { return cdrRow.Prefix_in; } set { cdrRow.Prefix_in = value; } }

		public string PrefixOut { get { return cdrRow.Prefix_out; } set { cdrRow.Prefix_out = value; } }

		public long DNIS { get { return cdrRow.DNIS; } set { cdrRow.DNIS = value; } }

		public long ANI { get { return cdrRow.ANI; } }

		public string ANIStr { get { return cdrRow.ANI.ToString(); } }

		public string Alias { get { return alias; } }

		public string SIPUserId { get { return sipUserId; } }

		public long SerialNumber { get { return cdrRow.Serial_number; } set { cdrRow.Serial_number = value; } }

		public decimal RetailPrice { get { return cdrRow.End_user_price; } set { cdrRow.End_user_price = value; } }

		public short UsedBonusMinutes { get { return cdrRow.Used_bonus_minutes; } set { cdrRow.Used_bonus_minutes = value; } }

		public short NodeId { get { return cdrRow.Node_id; } set { cdrRow.Node_id = value; } }

		public byte InfoDigits { get { return cdrRow.Info_digits; } set { cdrRow.Info_digits = value; } }

		public CallStatus Status {
			get {
				if (cdrRow.Retail_duration == -2) {
					return CallStatus.Connecting;
				}
				if (cdrRow.Retail_duration == -1) {
					return CallStatus.Connected;
				}
				return CallStatus.Completed;
			}

			set {
				if (value == CallStatus.Connecting) {
					cdrRow.Retail_duration = -2;
				}
				if (value == CallStatus.Connected) {
					cdrRow.Retail_duration = -1;
				}
			}
		}

		//-- Mandatory default Constructor:

		public static void CreateDb() {
			const int _monthsAhead = 2;
			Cdr_Db.Create(DateTime.Today, _monthsAhead);
		}

		public static Cdr Get(string pCallId) {
			var _callId = CDRRow.StripSpaces(pCallId);

			CDRRow[] _cdrRows;
			using (var _db = new Cdr_Db(DateTime.Now)) {
				_cdrRows = _db.CDRCollection.GetById(_callId);
			}
			if (_cdrRows == null) {
				throw new RbrException(RbrResult.Cdr_NotFound, "Cdr.Get", string.Format("CallId={0}", _callId));
			}
			if (_cdrRows.Length != 1) {
				throw new RbrException(RbrResult.Cdr_Unexpected, "Cdr.Get", string.Format("CallId={0}", _callId));
			}
			return new Cdr(_cdrRows[0]);
		}

		public static int Update(Cdr pCdr) {
			using (var _db = new Cdr_Db(DateTime.Now)) {
				_db.CDRCollection.Update(pCdr.CdrRow);
			}
			return 0;
		}

		public int Insert() {
			try {
				//-- Make sure prefixIn and prefixOut is not more then 10 chars:
				if (PrefixIn != null && PrefixIn.Length > AppConstants.MaxPrefixLength) {
					PrefixIn = PrefixIn.Substring(0, AppConstants.MaxPrefixLength);
				}
				if (PrefixOut != null && PrefixOut.Length > AppConstants.MaxPrefixLength) {
					PrefixOut = PrefixOut.Substring(0, AppConstants.MaxPrefixLength);
				}

				//-- Make sure Local_number is not more then 18 chars:
				if (LocalNumber != null && LocalNumber.Length > 18) {
					LocalNumber = LocalNumber.Substring(0, 18);
				}
			}
			catch (Exception _ex) {
				TimokLogger.Instance.LogRbr(LogSeverity.Error, "Cdr.Insert", string.Format("Exception:\r\n{0}", _ex));
				return 1;
			}

			//--insert into local database. NOTE: use CDRDb based on start of the call !!!
			try {
				using (var _db = new Cdr_Db(CdrRow.Start)) {
					insert(_db, CdrRow);
				}
			}
			catch (Exception _ex) {
				TimokLogger.Instance.LogRbr(LogSeverity.Error, "Cdr.Insert", string.Format("Inserting to local db, Exception:\r\n{0}", _ex));
				return 1;
			}
			return 0;
		}

		public string Export() {
			var _strB = new StringBuilder();
			try {
				_strB.Append(cdrRow.Start.ToString(AppConstants.CdrDateFormat));
				_strB.Append(",");
				_strB.Append(cdrRow.Start.ToString(AppConstants.CdrTimeFormat));
				_strB.Append(",");
				_strB.Append(Duration.ToString());
				_strB.Append(",");
				_strB.Append(CountryCode.ToString());
				_strB.Append(",");
				_strB.Append(LocalNumber);
				_strB.Append(",");
				_strB.Append(CustomerRouteName);
				_strB.Append(",");
				_strB.Append(ANI);
				_strB.Append(",");
				_strB.Append(OrigEPId);
				_strB.Append(",");
				_strB.Append(CustomerPrice.ToString(AppConstants.CdrAmountFormat));
				_strB.Append(",");
				_strB.Append(TermEPId);
				_strB.Append(",");
				_strB.Append(CarrierCost.ToString(AppConstants.CdrAmountFormat));
				_strB.Append(",");
				_strB.Append(RetailPrice.ToString(AppConstants.CdrAmountFormat));
			}
			catch (Exception _ex) {
				TimokLogger.Instance.LogRbr(LogSeverity.Error, "Cdr.Export", string.Format("Exception:\r\n{0}", _ex));
			}
			return _strB.ToString();
		}

		//----------------------------------- Private -------------------------------------------
		/*
      sourceAddress = 4 entries {
        [0]=dialedDigits "3605759548"
        [1]=h323_ID  11 characters {
          0033 0036 0030 0035 0037 0035 0039 0035   36057595
          0034 0038 0020                            48 
        }
        [2]=h323_ID  13 characters {
          0069 0064 0024 0033 0036 0030 0035 0037   id$36057
          0035 0039 0035 0034 0038                  59548
        }
        [3]=h323_ID  14 characters {
          0069 0070 0024 0036 0037 002e 0031 0033   ip$67.13
          0030 002e 0031 002e 0034 0030             0.1.40
        } 
			}
		  
		 */

		static string tryAlias(string pSubfield) {
			var _index = pSubfield.IndexOf(AppConstants.AliasSuffix); // :H323_ID or :h323_ID or :h323_id ...
			if (_index > 1) {
				return pSubfield.Substring(0, _index - 2);
			}
			return string.Empty;
		}

		long tryANI(string pSubfield) {
			var _index = pSubfield.IndexOf(AppConstants.ANISuffix);
			if (_index == 0) {
				TimokLogger.Instance.LogRbr(LogSeverity.Error, "Cdr.tryANI:", string.Format("ANI Invalid, {0}", pSubfield));
				return 0;
			}

			if (_index > 0) {
				pSubfield = pSubfield.Substring(0, _index);
			}

			if (! Utils.IsNumeric(pSubfield)) {
				TimokLogger.Instance.LogRbr(LogSeverity.Error, "Cdr.tryANI:", string.Format("ANI NOT NUMERIC, {0}", pSubfield));
				return 0;
			}

			long _ani;
			try {
				_ani = long.Parse(pSubfield);
			}
			catch (Exception _ex) {
				TimokLogger.Instance.LogRbr(LogSeverity.Error, "Cdr.tryANI:", string.Format("ANI parse exception:\r\n, {0}", _ex));
				_ani = 0;
			}
			return _ani;
		}

		static long trySerialNumber(string pField) {
			long _serialNumber = 0;

			var _indx = pField.IndexOf(AppConstants.SerialNumberPrefix);
			if (_indx == 0) {		//NOTE: prefix has to start at zero index
				long.TryParse(pField.Substring(AppConstants.SerialNumberPrefix.Length), out _serialNumber);
			}
			return _serialNumber;
		}

		static long tryDNIS(string pField) {
			long _dnis = 0;

			var _indx = pField.IndexOf(AppConstants.DNISPrefix);
			if (_indx == 0) {			//NOTE: prefix has to start at zero index
				long.TryParse(pField.Substring(AppConstants.DNISPrefix.Length), out _dnis);
			}
			return _dnis;
		}

		//TODO: This whole thing bellow can be cleaned up...
		//--
		static DateTime getStandardDateTime(string pGkStartDateTime, string pGkEndDateTime) {
			string _dtStr;
			if (pGkStartDateTime.Length == 11) {	//unconnected
				_dtStr = ParseDate(pGkEndDateTime);
			}
			else {
				_dtStr = ParseDate(pGkStartDateTime);
			}

			//-- get time
			if (pGkStartDateTime.Length == 11) {
				//unconnected
				_dtStr += ParseTime(pGkEndDateTime);
			}
			else {
				_dtStr += ParseTime(pGkStartDateTime);
			}

			return DateTime.ParseExact(_dtStr, AppConstants.CdrDateFormat + AppConstants.CdrTimeFormat, CultureInfo.CurrentCulture);
		}

		static string ParseDate(string pStr) {
			//Mon, 18 Nov 2002 22:45:01 -0500
			var _strBld = new StringBuilder(pStr.Substring(12, 4));
			_strBld.Append(getMonth(pStr.Substring(8, 3)));
			_strBld.Append(pStr.Substring(5, 2));
			return _strBld.ToString();
		}

		static string ParseTime(string pStr) {
			//Mon, 18 Nov 2002 22:45:01 -0500
			var _strBld = new StringBuilder(pStr.Substring(17, 2));
			_strBld.Append(pStr.Substring(20, 2));
			_strBld.Append(pStr.Substring(23, 2));
			_strBld.Append("0");
			return _strBld.ToString();
		}

		static string getMonth(string pStr) {
			switch (pStr) {
			case "Jan":
				return "01";
			case "Feb":
				return "02";
			case "Mar":
				return "03";
			case "Apr":
				return "04";
			case "May":
				return "05";
			case "Jun":
				return "06";
			case "Jul":
				return "07";
			case "Aug":
				return "08";
			case "Sep":
				return "09";
			case "Oct":
				return "10";
			case "Nov":
				return "11";
			case "Dec":
				return "12";
			default:
				return "???";
			}
		}

		public static bool DeserializeAndImport(string pFilePath) {
			CDRRow[] _cdrRows;
			try {
				_cdrRows = deserializeFromFile(pFilePath);
			}
			catch {
				Thread.Sleep(250);

				//-- try again
				try {
					_cdrRows = deserializeFromFile(pFilePath);
				}
				catch {
					return false;
				}
			}

			var _dateFileCreated = getDateCreated(pFilePath);
			import(_cdrRows, _dateFileCreated);
			return true;
		}

		//----------------------- Private static --------------------------------------------------
		static CDRRow[] deserializeFromFile(string pFilePath) {
			using (var _fs = new FileStream(pFilePath, FileMode.Open, FileAccess.Read)) {
				IFormatter _formatter = new BinaryFormatter(); //SoapFormatter();
				return (CDRRow[]) _formatter.Deserialize(_fs);
			}
		}

		static DateTime getDateCreated(string pFilePath) {
			var _fileName = Path.GetFileNameWithoutExtension(Path.GetFileNameWithoutExtension(pFilePath));
			var _datePart = _fileName.Substring(_fileName.IndexOf('-') + 1);
			return DateTime.ParseExact(_datePart, AppConstants.CdrFileNameFormat, null);
		}

		static void import(ICollection<CDRRow> pCdrRows, DateTime pDateFileCreated) {
			if (pCdrRows != null && pCdrRows.Count > 0) {
				var _previousDayDate = pDateFileCreated.AddDays(-1);

				if (pDateFileCreated.Month == _previousDayDate.Month) {
					loadCDR(pCdrRows, pDateFileCreated);
				}
				else {
					loadCDR(pCdrRows, pDateFileCreated, _previousDayDate);
				}
			}
		}

		static void loadCDR(IEnumerable<CDRRow> pCDRRows, DateTime pCurrent) {
			using (var _dbCurrent = new Cdr_Db(pCurrent)) {
				_dbCurrent.BeginTransaction();
				foreach (var _cdrRow in pCDRRows) {
					insert(_dbCurrent, _cdrRow);
				}
				_dbCurrent.CommitTransaction();
			}
		}

		static void loadCDR(IEnumerable<CDRRow> pCDRRows, DateTime pCurrent, DateTime pPrevious) {
			using (var _dbCurrent = new Cdr_Db(pCurrent)) {
				using (var _dbPrevious = new Cdr_Db(pPrevious)) {
					_dbCurrent.BeginTransaction();
					_dbPrevious.BeginTransaction();
					foreach (var _cdrRow in pCDRRows) {
						if (_cdrRow.Start.Month == pCurrent.Month) {
							insert(_dbCurrent, _cdrRow);
						}
						else if (_cdrRow.Start.Month == pPrevious.Month) {
							insert(_dbPrevious, _cdrRow);
						}
						else {
							throw new Exception("How can a CDR NOT belong to current or previous Month?");
						}
					}
					_dbCurrent.CommitTransaction();
					_dbPrevious.CommitTransaction();
				}
			}
		}

		static void insert(Cdr_Db_Base pDb, CDRRow pCdrRow) {
			try {
				if (pCdrRow.Id != null && pCdrRow.Id.Length == 32) {
					var _cdrIdentityRow = pDb.CDRIdentityCollection.GetByPrimaryKey(pCdrRow.Id);
					if (_cdrIdentityRow != null) {
						pCdrRow.Id = Guid.NewGuid().ToString("N"); //NOTE: openh323 generates duplicate Guids?
					}
					_cdrIdentityRow = new CDRIdentityRow
					                  {
					                  	Id = pCdrRow.Id
					                  };
					pDb.CDRIdentityCollection.Insert(_cdrIdentityRow);
				}
				pDb.CDRCollection.Insert(pCdrRow);
			}
			catch {
				pDb.RollbackTransaction();
				throw;
			}
		}

		#region used by ReportEngine

		//used by ReportEngine
		public static RouteMinutesSummary[] GetCustomerRouteMinutesSummaries(int pTimokDate) {
			DataTable _dt;
			using (var _db = new Cdr_Db(TimokDate.ToDateTime(pTimokDate))) {
				_dt = _db.CDRCollection.GetCustomerRoutesTotalMinutes(pTimokDate);
			}
			var _list = new ArrayList();
			if (_dt != null && _dt.Rows.Count > 0) {
				using (var _db = new Rbr_Db()) {
					foreach (DataRow _dataRow in _dt.Rows) {
						var _routeMinutesSummary = new RouteMinutesSummary
						                           {
						                           	Id = ((int) _dataRow[CustomerRoutesTotalMinutesDataTable.Id_ColumnName]),
						                           	Minutes = ((decimal) _dataRow[CustomerRoutesTotalMinutesDataTable.Minutes_ColumnName])
						                           };
						if (_routeMinutesSummary.Id > 0) {
							var _routeRow = _db.RouteCollection.GetByPrimaryKey(_routeMinutesSummary.Id);
							_routeMinutesSummary.Name = _routeRow.Name;
						}
						_list.Add(_routeMinutesSummary);
					}
				}
				_list.Sort(new GenericComparer(RouteMinutesSummary.Name_PropName, ListSortDirection.Ascending));
			}
			return (RouteMinutesSummary[]) _list.ToArray(typeof (RouteMinutesSummary));
		}

		public static EndpointASR[] GetTermEndpointsASR(int pTimokDate) {
			DataTable _dt;
			using (var _db = new Cdr_Db(TimokDate.ToDateTime(pTimokDate))) {
				_dt = _db.CDRCollection.GetTermEpAsr(pTimokDate);
			}
			var _list = new ArrayList();
			if (_dt != null && _dt.Rows.Count > 0) {
				using (var _db = new Rbr_Db()) {
					foreach (DataRow _dataRow in _dt.Rows) {
						var _endpointAsr = new EndpointASR
						                   {
						                   	EndpointId = ((short) _dataRow[EndpointAsrDataTable.Id_ColumnName]),
						                   	Calls = ((int) _dataRow[EndpointAsrDataTable.Calls_ColumnName]),
						                   	ConnectedCalls = ((int) _dataRow[EndpointAsrDataTable.Connected_calls_ColumnName]),
						                   	Asr = ((int) _dataRow[EndpointAsrDataTable.Asr_ColumnName])
						                   };
						if (_endpointAsr.EndpointId > 0) {
							var _endPointRow = _db.EndPointCollection.GetByPrimaryKey(_endpointAsr.EndpointId);
							_endpointAsr.Alias = _endPointRow.Alias;
						}
						_list.Add(_endpointAsr);
					}
				}
				_list.Sort(new GenericComparer(EndpointASR.Alias_PropName, ListSortDirection.Ascending));
			}
			return (EndpointASR[]) _list.ToArray(typeof (EndpointASR));
		}

		////used by ReportEngine
		//public static DataTable GetCustomerRoutesTotalMinutes(int pTimokDate) {
		//  using (Cdr_Db db = new Cdr_Db(TimokDate.ToDateTime(pTimokDate))) {
		//    return db.CDRViewCollection.GetCustomerRoutesTotalMinutes(pTimokDate);
		//  }
		//}

		//public static DataTable GetTermEpAsr(int pTimokDate){
		//  using(Cdr_Db db = new Cdr_Db(TimokDate.ToDateTime(pTimokDate))){
		//    return db.CDRViewCollection.GetTermEpAsr(pTimokDate);
		//  }
		//}

		#endregion used by ReportEngine
	}
}
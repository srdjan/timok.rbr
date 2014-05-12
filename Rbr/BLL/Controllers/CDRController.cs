using System;
using System.Collections;
using System.Collections.Generic;
using Timok.Core;
using Timok.Logger;
using Timok.Rbr.DAL.CdrDatabase;
using Timok.Rbr.DTO;

namespace Timok.Rbr.BLL.Controllers {
	public class CDRController {
		private CDRController() {}

		public const int MonthsBackToCheckCDRs = -3;

		#region CDR[] Getters

		#region Paging

		public static List<CdrDto> GetCDRsByCustomerAcctIdPaged(DateTime pDate, short pCustomerAcctId, int pPageNumber, int pPageSize, out int pTotalCount) {
			pTotalCount = 0;
			var _cdrViewRows = new CDRViewRow[0];
			int _startTimokDate = TimokDate.Parse(pDate, TimokDate.MinHour);
			int _endTimokDate = TimokDate.Parse(pDate, TimokDate.MaxHour);

			if (Cdr_Db.Exists(pDate)) {
				using (var _db = new Cdr_Db(pDate)) {
					_cdrViewRows = _db.CDRViewCollection.GetByCustomerAcctIdPaged(_startTimokDate, _endTimokDate, pCustomerAcctId, pPageNumber, pPageSize, out pTotalCount);
				}
			}
			return mapToCDRs(_cdrViewRows);
		}

		public static int GetCDRCountByCustomerAcctId(DateTime pDate, short pCustomerAcctId, int pPageNumber, int pPageSize, out int pTotalCount) {
			pTotalCount = 0;
			int _startTimokDate = TimokDate.Parse(pDate, TimokDate.MinHour);
			int _endTimokDate = TimokDate.Parse(pDate, TimokDate.MaxHour);

			if (Cdr_Db.Exists(pDate)) {
				using (var _db = new Cdr_Db(pDate)) {
					return _db.CDRViewCollection.GetCountByCustomerAcctIdPaged(_startTimokDate, _endTimokDate, pCustomerAcctId);
				}
			}
			return 0;
		}

		public static List<CdrDto> GetCDRsByRetailAcctId(int pStartTimokDate, int pEndTimokDate, int pRetailAcctId, int pPageNumber, int pPageSize, out int pTotalCount) {
			pTotalCount = 0;
			var _cdrViewRows = new CDRViewRow[0];
			if (Cdr_Db.Exists(TimokDate.ToDateTime(pStartTimokDate))) {
				using (var _db = new Cdr_Db(TimokDate.ToDateTime(pStartTimokDate))) {
					_cdrViewRows = _db.CDRViewCollection.GetByRetailAcctIdPaged(pStartTimokDate, pEndTimokDate, pRetailAcctId, pPageNumber, pPageSize, out pTotalCount);
				}
			}
			return mapToCDRs(_cdrViewRows);
		}

		#endregion Paging

		public static List<CdrDto> GetCDRsByCustomerAcctId(DateTime pDate, short pCustomerAcctId) {
			var _cdrViewRows = new CDRViewRow[0];
			int _startTimokDate = TimokDate.Parse(pDate, TimokDate.MinHour);
			int _endTimokDate = TimokDate.Parse(pDate, TimokDate.MaxHour);

			if (Cdr_Db.Exists(pDate)) {
				using (var _db = new Cdr_Db(pDate)) {
					_cdrViewRows = _db.CDRViewCollection.GetByCustomer_acct_id(_startTimokDate, _endTimokDate, pCustomerAcctId);
				}
			}
			return mapToCDRs(_cdrViewRows);
		}

		public static List<CdrDto> GetByStartRetailAcctId(DateTime pDate, int pRetailAcctId) {
			var _cdrViewRows = new CDRViewRow[0];
			if (Cdr_Db.Exists(pDate)) {
				using (var db = new Cdr_Db(pDate)) {
					_cdrViewRows = db.CDRViewCollection.GetByStartRetailAcctId(pDate, pRetailAcctId);
				}
			}
			return mapToCDRs(_cdrViewRows);
		}

		public static List<CdrDto> GetCDRsByRetailAcctId(int pRetailAcctId, DateTime pDateActive) {
			var _cdrViewRows = new List<CDRViewRow>();

			for (int _i = 0; _i <= 12; _i++) {
				DateTime _date = pDateActive.AddMonths(_i);
				if (Cdr_Db.Exists(_date)) {
					using (var db = new Cdr_Db(_date)) {
						_cdrViewRows.AddRange(db.CDRViewCollection.GetByRetailAcctId(_date, pRetailAcctId));
					}
				}
			}
			return mapToCDRs(_cdrViewRows.ToArray());
		}

		public static List<CdrDto> GetCDRsByRetailAcctId(DateTime pDate, int pRetailAcctId) {
			var _cdrViewRows = new CDRViewRow[0];
			if (Cdr_Db.Exists(pDate)) {
				using (var db = new Cdr_Db(pDate)) {
					_cdrViewRows = db.CDRViewCollection.GetByRetailAcctId(pDate, pRetailAcctId);
				}
			}
			return mapToCDRs(_cdrViewRows);
		}

		public static List<CdrDto> GetRetailAcctCDRs(int pStartTimokDate, int pEndTimokDate, int pRetailAcctId) {
			var _cdrViewRows = new CDRViewRow[0];
			if (Cdr_Db.Exists(TimokDate.ToDateTime(pStartTimokDate))) {
				using (var db = new Cdr_Db(TimokDate.ToDateTime(pStartTimokDate))) {
					_cdrViewRows = db.CDRViewCollection.GetByRetailAcctId(pStartTimokDate, pEndTimokDate, pRetailAcctId);
				}
			}
			return mapToCDRs(_cdrViewRows);
		}

		public static List<CdrDto> GetCustomerAcctCDRs(int pStartTimokDate, int pEndTimokDate, short pCustomerAcctId) {
			var _cdrViewRows = new CDRViewRow[0];
			if (Cdr_Db.Exists(TimokDate.ToDateTime(pStartTimokDate))) {
				using (var db = new Cdr_Db(TimokDate.ToDateTime(pStartTimokDate))) {
					_cdrViewRows = db.CDRViewCollection.GetByCustomer_acct_id(pStartTimokDate, pEndTimokDate, pCustomerAcctId);
				}
			}
			return mapToCDRs(_cdrViewRows);
		}

		public static List<CdrDto> GetCDRsByCustomerAcctIdEndpointId(int pStartTimokDate, int pEndTimokDate, short pCustomerAcctId, short pEndpointId) {
			var _cdrViewRows = new CDRViewRow[0];
			if (Cdr_Db.Exists(TimokDate.ToDateTime(pStartTimokDate))) {
				using (var db = new Cdr_Db(TimokDate.ToDateTime(pStartTimokDate))) {
					_cdrViewRows = db.CDRViewCollection.GetByCustomer_acct_idEndpoint_id(pStartTimokDate, pEndTimokDate, pCustomerAcctId, pEndpointId);
				}
			}
			return mapToCDRs(_cdrViewRows);
		}

		public static List<CdrDto> GetCDRsByCarrierAcctId(int pStartTimokDate, int pEndTimokDate, short pCarrierAcctId) {
			var _cdrViewRows = new CDRViewRow[0];
			if (Cdr_Db.Exists(TimokDate.ToDateTime(pStartTimokDate))) {
				using (var db = new Cdr_Db(TimokDate.ToDateTime(pStartTimokDate))) {
					_cdrViewRows = db.CDRViewCollection.GetByCarrierAcctId(pStartTimokDate, pEndTimokDate, pCarrierAcctId);
				}
			}
			return mapToCDRs(_cdrViewRows);
		}

		public static List<CdrDto> GetCDRsByCarrierAcctIdEndpointId(int pStartTimokDate, int pEndTimokDate, short pCarrierAcctId, short pEndpointId) {
			var _cdrViewRows = new CDRViewRow[0];
			if (Cdr_Db.Exists(TimokDate.ToDateTime(pStartTimokDate))) {
				using (var db = new Cdr_Db(TimokDate.ToDateTime(pStartTimokDate))) {
					_cdrViewRows = db.CDRViewCollection.GetByCarrierAcctIdEndpointId(pStartTimokDate, pEndTimokDate, pCarrierAcctId, pEndpointId);
				}
			}
			return mapToCDRs(_cdrViewRows);
		}

		#endregion CDR[] Getters

		#region Actions

		//public static void ConfirmPayment(string pGuid) {
		//  Cdr _cdr = Cdr.Get(pGuid);
		//  _cdr.RetailPrice = - _cdr.RetailPrice;
		//  Cdr.Update(_cdr);
		//}

		#endregion Actions

		#region HasCDRs Getters

		public static bool HasCDRsByCustomerAcctId(short pCustomerAcctId) {
			int _count = 0;
			int _startTimokDate = TimokDate.Parse(DateTime.Now.AddMonths(MonthsBackToCheckCDRs));
			int _endTimokDate = TimokDate.Parse(DateTime.Now);
			ArrayList _dbDateList = getDBDateList(_startTimokDate, _endTimokDate);

			foreach (DateTime _dbDate in _dbDateList) {
				if (_count <= 0) {
					if (Cdr_Db.Exists(_dbDate)) {
						using (var _db = new Cdr_Db(_dbDate)) {
							_count += _db.CDRCollection.GetCountByCustomerAcctId(_startTimokDate, _endTimokDate, pCustomerAcctId);
						}
					}
				}
			}
			return _count > 0;
		}

		public static bool HasCDRsByCustomerRouteId(int pCustomerRouteId) {
			int _count = 0;
			int _startTimokDate = TimokDate.Parse(DateTime.Now.AddMonths(MonthsBackToCheckCDRs));
			int _endTimokDate = TimokDate.Parse(DateTime.Now);
			ArrayList _dbDateList = getDBDateList(_startTimokDate, _endTimokDate);

			foreach (DateTime _dbDate in _dbDateList) {
				if (_count <= 0) {
					if (Cdr_Db.Exists(_dbDate)) {
						using (var _db = new Cdr_Db(_dbDate)) {
							_count += _db.CDRCollection.GetCountByCustomerRouteId(_startTimokDate, _endTimokDate, pCustomerRouteId);
						}
					}
				}
			}
			return _count > 0;
		}

		public static bool HasCDRsByRetailAcctId(int pRetailAcctId) {
			int _count = 0;
			int _startTimokDate = TimokDate.Parse(DateTime.Now.AddMonths(MonthsBackToCheckCDRs));
			int _endTimokDate = TimokDate.Parse(DateTime.Now);
			ArrayList _dbDateList = getDBDateList(_startTimokDate, _endTimokDate);

			foreach (DateTime _dbDate in _dbDateList) {
				if (_count <= 0) {
					if (Cdr_Db.Exists(_dbDate)) {
						using (var _db = new Cdr_Db(_dbDate)) {
							_count += _db.CDRCollection.GetCountByRetailAcctId(_startTimokDate, _endTimokDate, pRetailAcctId);
						}
					}
				}
			}
			return _count > 0;
		}

		public static bool HasCDRsByCarrierAcctId(short pCarrierAcctId) {
			int _count = 0;
			int _startTimokDate = TimokDate.Parse(DateTime.Now.AddMonths(MonthsBackToCheckCDRs));
			int _endTimokDate = TimokDate.Parse(DateTime.Now);
			ArrayList _dbDateList = getDBDateList(_startTimokDate, _endTimokDate);

			foreach (DateTime _dbDate in _dbDateList) {
				if (_count <= 0) {
					if (Cdr_Db.Exists(_dbDate)) {
						using (var _db = new Cdr_Db(_dbDate)) {
							_count += _db.CDRCollection.GetCountByCarrierAcctId(_startTimokDate, _endTimokDate, pCarrierAcctId);
						}
					}
				}
			}
			return _count > 0;
		}

		public static bool HasCDRsByCarrierRouteId(int pCarrierRouteId) {
			int _count = 0;
			int _startTimokDate = TimokDate.Parse(DateTime.Now.AddMonths(MonthsBackToCheckCDRs));
			int _endTimokDate = TimokDate.Parse(DateTime.Now);
			ArrayList _dbDateList = getDBDateList(_startTimokDate, _endTimokDate);

			foreach (DateTime _dbDate in _dbDateList) {
				if (_count <= 0) {
					if (Cdr_Db.Exists(_dbDate)) {
						using (var _db = new Cdr_Db(_dbDate)) {
							_count += _db.CDRCollection.GetCountByCarrierRouteId(_startTimokDate, _endTimokDate, pCarrierRouteId);
						}
					}
				}
			}
			return _count > 0;
		}

		public static bool HasCDRsByOrigEndPointId(short pOrigEndPointId) {
			int _count = 0;
			int _startTimokDate = TimokDate.Parse(DateTime.Now.AddMonths(MonthsBackToCheckCDRs));
			int _endTimokDate = TimokDate.Parse(DateTime.Now);
			ArrayList _dbDateList = getDBDateList(_startTimokDate, _endTimokDate);

			foreach (DateTime _dbDate in _dbDateList) {
				if (_count <= 0) {
					if (Cdr_Db.Exists(_dbDate)) {
						using (var _db = new Cdr_Db(_dbDate)) {
							_count += _db.CDRCollection.GetCountByOrigEndPointId(_startTimokDate, _endTimokDate, pOrigEndPointId);
						}
					}
				}
			}
			return _count > 0;
		}

		public static bool HasCDRsByTermEndPointId(short pTermEndPointId) {
			int _count = 0;
			int _startTimokDate = TimokDate.Parse(DateTime.Now.AddMonths(MonthsBackToCheckCDRs));
			int _endTimokDate = TimokDate.Parse(DateTime.Now);
			ArrayList _dbDateList = getDBDateList(_startTimokDate, _endTimokDate);

			foreach (DateTime _dbDate in _dbDateList) {
				if (_count <= 0) {
					if (Cdr_Db.Exists(_dbDate)) {
						using (var _db = new Cdr_Db(_dbDate)) {
							_count += _db.CDRCollection.GetCountByTermEndPointId(_startTimokDate, _endTimokDate, pTermEndPointId);
						}
					}
				}
			}
			return _count > 0;
		}

		#endregion HasCDRs Getters

		//------------------------------------ Private ----------------------------------------

		#region mappings

		private static List<CdrDto> mapToCDRs(CDRViewRow[] pCDRViewRows) {
			var _list = new List<CdrDto>();
			foreach (var _cdrViewRow in pCDRViewRows) {
				_list.Add(mapToCDRDto(_cdrViewRow));
			}
			return _list;
		}

		private static CdrDto mapToCDRDto(CDRViewRow pCDRViewRow) {
			var _cdr = new CdrDto();
			_cdr.Guid = pCDRViewRow.Id;
			_cdr.ANI = pCDRViewRow.ANI;
			_cdr.CarrierAcctId = pCDRViewRow.Carrier_acct_id;
			_cdr.CarrierAcctName = pCDRViewRow.Carrier_acct_name;
			_cdr.CarrierRouteId = pCDRViewRow.Carrier_route_id;
			_cdr.CarrierRouteName = pCDRViewRow.Carrier_route_name;
			_cdr.CarrierDuration = pCDRViewRow.Carrier_duration;
			_cdr.CarrierMinutes = pCDRViewRow.Carrier_minutes;
			_cdr.Ccode = pCDRViewRow.Ccode;
			_cdr.CarrierCost = pCDRViewRow.Cost;
			_cdr.CustomerAcctId = pCDRViewRow.Customer_acct_id;
			_cdr.CustomerAcctName = pCDRViewRow.Customer_acct_name;
			_cdr.CustomerDuration = pCDRViewRow.Customer_duration;
			_cdr.CustomerMinutes = pCDRViewRow.Minutes; //customer minutes
			_cdr.CustomerRouteId = pCDRViewRow.Customer_route_id;
			_cdr.CustomerRouteName = pCDRViewRow.Customer_route_name;
			_cdr.DateLogged = pCDRViewRow.Date_logged;
			_cdr.DialedNumber = pCDRViewRow.Dialed_number;
			_cdr.DisconnectCauseNumber = pCDRViewRow.Disconnect_cause;
			_cdr.DisconnectSourceNumber = pCDRViewRow.Disconnect_source;
			_cdr.DNIS = pCDRViewRow.DNIS;
			_cdr.InfoDigits = pCDRViewRow.Info_digits;
			_cdr.Leg2Duration = pCDRViewRow.Duration;
			_cdr.Leg1Duration = (short) pCDRViewRow.Date_logged.Subtract(pCDRViewRow.Start).TotalSeconds;
			if (_cdr.Leg1Duration < 0) {
				TimokLogger.Instance.LogRbr(LogSeverity.Error, "", string.Format("Negative Leg1 Duration, callID: {0}", pCDRViewRow.Id));
				_cdr.Leg1Duration = (short) (_cdr.Leg2Duration + 2);
			}
			if (_cdr.Leg1Duration < _cdr.Leg2Duration) {
				TimokLogger.Instance.LogRbr(LogSeverity.Error, "", string.Format("Leg1 Duration < Leg2 Duration, callID: {0}", pCDRViewRow.Id));
				_cdr.Leg1Duration = (short) (_cdr.Leg2Duration + 3);
			}
			_cdr.EndUserPrice = pCDRViewRow.End_user_price;
			_cdr.LocalNumber = pCDRViewRow.Local_number;
			_cdr.MappedDisconnectCauseNumber = pCDRViewRow.Mapped_disconnect_cause;
			_cdr.NodeId = pCDRViewRow.Node_id;
			_cdr.NodeName = pCDRViewRow.Node_name;
			_cdr.OrigAlias = pCDRViewRow.Orig_alias;
			_cdr.OrigDotIPAddress = pCDRViewRow.Orig_dot_IP_address;
			_cdr.OrigEndpointId = pCDRViewRow.Orig_end_point_id;
			_cdr.OrigIPAddress = pCDRViewRow.Orig_IP_address;
			_cdr.OrigPartnerId = pCDRViewRow.Orig_partner_id;
			_cdr.OrigPartnerName = pCDRViewRow.Orig_partner_name;
			_cdr.PrefixIn = pCDRViewRow.Prefix_in;
			_cdr.PrefixOut = pCDRViewRow.Prefix_out;
			_cdr.CustomerPrice = pCDRViewRow.Price;
			_cdr.RbrResultNumber = pCDRViewRow.Rbr_result;
			_cdr.RetailAcctId = pCDRViewRow.Retail_acct_id;
			_cdr.RetailDuration = pCDRViewRow.Retail_duration;
			_cdr.RetailMinutes = pCDRViewRow.Retail_minutes;
			_cdr.SerialNumber = pCDRViewRow.Serial_number;
			_cdr.Start = pCDRViewRow.Start;
			_cdr.TermAlias = pCDRViewRow.Term_alias;
			_cdr.TermEndpointId = pCDRViewRow.Term_end_point_id;
			_cdr.TermIPAddressRange = pCDRViewRow.Term_ip_address_range;
			_cdr.TermPartnerId = pCDRViewRow.Term_partner_id;
			_cdr.TermPartnerName = pCDRViewRow.Term_partner_name;
			_cdr.TimokDate = pCDRViewRow.Timok_date;
			_cdr.UsedBonusMinutes = pCDRViewRow.Used_bonus_minutes;
			_cdr.ResellerPrice = pCDRViewRow.Reseller_price;
			return _cdr;
		}

		private static RawCDRDto[] toRawCDRs(CDRRow[] pCDRRows) {
			var _list = new List<RawCDRDto>();
			foreach (var _cdrRow in pCDRRows) {
				_list.Add(toRawCDR(_cdrRow));
			}
			return _list.ToArray();
		}

		private static RawCDRDto toRawCDR(CDRRow pCDRRow) {
			var _cdr = new RawCDRDto();
			_cdr.ANI = pCDRRow.ANI;
			_cdr.Carrier_route_id = pCDRRow.Carrier_route_id;
			_cdr.Ccode = pCDRRow.Ccode;
			_cdr.Cost = pCDRRow.Cost;
			_cdr.Customer_acct_id = pCDRRow.Customer_acct_id;
			_cdr.Customer_route_id = pCDRRow.Customer_route_id;
			_cdr.Date_logged = pCDRRow.Date_logged;
			_cdr.Disconnect_cause = pCDRRow.Disconnect_cause;
			_cdr.Disconnect_source = pCDRRow.Disconnect_source;
			_cdr.DNIS = pCDRRow.DNIS;
			_cdr.Duration = pCDRRow.Duration;
			_cdr.End_user_price = pCDRRow.End_user_price;
			_cdr.Local_number = pCDRRow.Local_number;
			_cdr.Mapped_disconnect_cause = pCDRRow.Mapped_disconnect_cause;
			_cdr.Node_id = pCDRRow.Node_id;
			_cdr.Orig_IP_address = pCDRRow.Orig_IP_address;
			_cdr.Prefix_in = pCDRRow.Prefix_in;
			_cdr.Prefix_out = pCDRRow.Prefix_out;
			_cdr.Price = pCDRRow.Price;
			_cdr.Rbr_result = pCDRRow.Rbr_result;
			_cdr.Serial_number = pCDRRow.Serial_number;
			_cdr.Start = pCDRRow.Start;
			_cdr.Term_end_point_id = pCDRRow.Term_end_point_id;
			_cdr.Timok_date = pCDRRow.Timok_date;
			_cdr.Used_bonus_minutes = pCDRRow.Used_bonus_minutes;
			_cdr.Customer_duration = pCDRRow.Customer_duration;
			_cdr.Retail_acct_id = pCDRRow.Retail_acct_id;
			_cdr.Reseller_price = pCDRRow.Reseller_price;
			_cdr.Carrier_duration = pCDRRow.Carrier_duration;
			_cdr.Retail_duration = pCDRRow.Retail_duration;
			return _cdr;
		}

		#endregion mappings

		#region DB Helpers

		private static ArrayList getDBDateList(int pStartTimokDate, int pEndTimokDate) {
			return getDBDateList(TimokDate.ToDateTime(pStartTimokDate), TimokDate.ToDateTime(pEndTimokDate));
		}

		private static ArrayList getDBDateList(DateTime pStartDate, DateTime pEndDate) {
			var _dbDateList = new ArrayList();
			long _diff = DateTimeUtils.DateDiff(Cdr_Db.DbDateInterval, pStartDate, pEndDate);

			DateTime _date = pStartDate;
			//case Cdr_Db_DateInterval.Weeks:
			//case Cdr_Db_DateInterval.Quarters:
			switch (Cdr_Db.DbDateInterval) {
				case DateInterval.Day:
					_dbDateList.Add(new DateTime(_date.Year, _date.Month, _date.Day));
					break;
				case DateInterval.Month:
					_dbDateList.Add(new DateTime(_date.Year, _date.Month, 1));
					break;
				case DateInterval.Year:
					_dbDateList.Add(new DateTime(_date.Year, 1, 1));
					break;
			}

			for (int i = 0; i < _diff; i++) {
				switch (Cdr_Db.DbDateInterval) {
					case DateInterval.Day:
						_date = _date.AddDays(1);
						break;
					case DateInterval.Month:
						_date = _date.AddMonths(1);
						break;
					case DateInterval.Year:
						_date = _date.AddYears(1);
						break;
				}
				_dbDateList.Add(_date);
			}
			return _dbDateList;
		}

		#endregion DB Helpers
	}
}
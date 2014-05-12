using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Timok.Core;
using Timok.Core.BackgroundProcessing;
using Timok.Logger;
using Timok.Rbr.BLL.DOM;
using Timok.Rbr.BLL.ImportExport.Cdr;
using Timok.Rbr.Core;
using Timok.Rbr.DAL.CdrDatabase;
using Timok.Rbr.DAL.CdrDatabase.Base;
using Timok.Rbr.DAL.RbrDatabase;
using Timok.Rbr.DAL.RbrDatabase.Base;
using Timok.Rbr.DTO;

namespace Timok.Rbr.BLL.Controllers {
	public class CdrExportController {
		const string GetcdrsLabel = "CdrExportController.getCDRs";
		CdrExportController() { }

		#region Export Cdrs

		public static string ExportCustomerAcct(CustomerCdrExportInfo pCustomerCdrExportInfo, BackgroundWorker pBackgroundWorker) {
			var _fileName = "[" + pCustomerCdrExportInfo.CustomerAcct.Name + "] " + pCustomerCdrExportInfo.DateStart.ToString("yyyyMMdd") + "-" + pCustomerCdrExportInfo.DateEnd.ToString("yyyyMMdd");

			_fileName += "[format-" + pCustomerCdrExportInfo.CdrExportMap.Name + "]";
			_fileName += ".CustomerAcct.cdr";
			_fileName = FileHelper.CleanupInvalidFileNameCharacters(_fileName, '_');

			string _filePath = Path.Combine(pCustomerCdrExportInfo.ExportDirectory, _fileName);
			string _where = CDRViewRow_Base.customer_acct_id_DbName + " = " + pCustomerCdrExportInfo.CustomerAcct.CustomerAcctId;

			export(_where, pCustomerCdrExportInfo, _filePath, pBackgroundWorker);

			return _filePath;
		}

		public static string ExportCarrierAcct(CarrierCdrExportInfo pCarrierCdrExportInfo, BackgroundWorker pBackgroundWorker) {
			string _fileName = "[" + pCarrierCdrExportInfo.CarrierAcctDto.Name + "] " + pCarrierCdrExportInfo.DateStart.ToString("yyyyMMdd") + "-" + pCarrierCdrExportInfo.DateEnd.ToString("yyyyMMdd");

			_fileName += "[format-" + pCarrierCdrExportInfo.CdrExportMap.Name + "]";
			_fileName += ".CarrierAcct.cdr";
			_fileName = FileHelper.CleanupInvalidFileNameCharacters(_fileName, '_');

			string _filePath = Path.Combine(pCarrierCdrExportInfo.ExportDirectory, _fileName);
			string _where = CDRViewRow_Base.carrier_acct_id_DbName + " = " + pCarrierCdrExportInfo.CarrierAcctDto.CarrierAcctId;

			export(_where, pCarrierCdrExportInfo, _filePath, pBackgroundWorker);

			return _filePath;
		}

		public static string ExportOrigEndPoint(OrigEndpointCdrExportInfo pOrigEndpointCdrExportInfo, BackgroundWorker pBackgroundWorker) {
			EndPointRow _endpointRow = EndpointController.GetEndpoint(pOrigEndpointCdrExportInfo.OrigEndpointId);
			string _alias;
			if (_endpointRow == null) {
				if (pOrigEndpointCdrExportInfo.OrigEndpointId > 0) {
					throw new ArgumentException(string.Format("Orig Endpoint NOT FOUND Id: {0}", pOrigEndpointCdrExportInfo.OrigEndpointId));
				}
				_alias = "_UNKNOWN_ORIG_ENDPOINT_";
			}
			else {
				_alias = _endpointRow.Alias;
			}

			string _fileName = "[" + _alias + "] " + pOrigEndpointCdrExportInfo.DateStart.ToString("yyyyMMdd") + "-" + pOrigEndpointCdrExportInfo.DateEnd.ToString("yyyyMMdd");

			_fileName += "[format-" + pOrigEndpointCdrExportInfo.CdrExportMap.Name + "]";
			_fileName += ".OrigEndpoint.cdr";
			_fileName = FileHelper.CleanupInvalidFileNameCharacters(_fileName, '_');

			string _filePath = Path.Combine(pOrigEndpointCdrExportInfo.ExportDirectory, _fileName);
			string _where = CDRViewRow_Base.orig_end_point_id_DbName + " = " + pOrigEndpointCdrExportInfo.OrigEndpointId;

			export(_where, pOrigEndpointCdrExportInfo, _filePath, pBackgroundWorker);

			return _filePath;
		}

		//public static string ExportTermEndPoint(CdrExportInfo pCdrExportInfo, BackgroundWorker pBackgroundWorker) {
		//  string _filePath = string.Empty;
		//  return _filePath;
		//}

		public static string Export(CdrExportInfo pCdrExportInfo, BackgroundWorker pBackgroundWorker) {
			string _fileName = "[ALL CDRs] " + pCdrExportInfo.DateStart.ToString("yyyyMMdd") + "-" + pCdrExportInfo.DateEnd.ToString("yyyyMMdd");

			_fileName += "[format-" + pCdrExportInfo.CdrExportMap.Name + "]";
			_fileName += ".ALL.cdr";
			_fileName = FileHelper.CleanupInvalidFileNameCharacters(_fileName, '_');

			string _filePath = Path.Combine(pCdrExportInfo.ExportDirectory, _fileName);

			export(string.Empty, pCdrExportInfo, _filePath, pBackgroundWorker);

			return _filePath;
		}

		static void export(string pWhereFilter, ICdrExportInfo pCdrExportInfo, string pFilePath, BackgroundWorker pBackgroundWorker) {
			pBackgroundWorker.ReportStatus("CDR Export started...");
			pBackgroundWorker.ReportProgress(0);
			DateTime _processDate = pCdrExportInfo.DateStart;

			if (File.Exists(pFilePath)) {
				File.Delete(pFilePath);
			}
			string _tempFilePath = pFilePath + ".temp";
			if (File.Exists(_tempFilePath)) {
				File.Delete(_tempFilePath);
			}

			try {
				int _totalRecords = 0;

				while (_processDate <= pCdrExportInfo.DateEnd) {
					if (pBackgroundWorker.CancellationPending) {
						throw new Exception("CDR Export canceled");
					}

					var _records = new List<string>();
					for (int _hour = 0; _hour <= 23; _hour++) {
						int _timokDate = TimokDate.Parse(_processDate, _hour); // YYYYJJJ00
						_records.AddRange(getCDRs(pWhereFilter, pCdrExportInfo, _timokDate, pBackgroundWorker));
						_totalRecords += _records.Count;
					}

					using (var _sw = new StreamWriter(_tempFilePath, true)) {
						foreach (string _record in _records) {
							_sw.WriteLine(_record);
						}
					}
					_processDate = _processDate.AddDays(1);
				}

				FileHelper.Rename(_tempFilePath, pFilePath, TimokLogger.Instance.LogRbr);
				pBackgroundWorker.ReportStatus(string.Format("Exported total of {0} CDRs", _totalRecords));
				pBackgroundWorker.ReportProgress(0);
			}
			catch {
				if (File.Exists(_tempFilePath)) {
					try {
						File.Delete(_tempFilePath);
					}
					catch {}
				}
				throw;
			}
		}

		///NOTE: depricated
		//static List<string> getCDRs(string pWhereFilter, CdrExportMap pCdrExportMap, int pStartTimokDate, int pEndTimokDate, string pDecimalFormatString, BackgroundWorker pBackgroundWorker) {
		//  pBackgroundWorker.ReportStatus("Retrieving CDRs...");
		//  List<string> _records = new List<string>();
		//  if (Cdr_Db.Exists(TimokDate.ToDateTime(pStartTimokDate))) {
		//    using (Cdr_Db _db = new Cdr_Db(TimokDate.ToDateTime(pStartTimokDate))) {
		//      IDbCommand _cmd = _db.Connection.CreateCommand();
		//      _cmd.CommandText = getSQLForCDRViewExport(_db.Connection.Database, pCdrExportMap.CdrExportMapDetails, pStartTimokDate, pEndTimokDate, pWhereFilter);
		//      IDataReader _reader = _cmd.ExecuteReader();
		//      while (_reader.Read()) {
		//        if (pBackgroundWorker.CancellationPending) {
		//          throw new Exception("CDR Export canceled");
		//        }
		//        StringBuilder _record = new StringBuilder();
		//        foreach (CdrExportMapDetail _field in pCdrExportMap.CdrExportMapDetails) {
		//          object _value = _reader.GetValue(_field.Sequence - 1);
		//          _record.Append(_value);
		//          //NOTE: if need to format prices, here is the place to do that
		//          //if (_value is Decimal) {
		//          //  _record.Append(((decimal) _value).ToString(pDecimalFormatString));
		//          //}
		//          //else {
		//          //  _record.Append(_value);
		//          //}
		//          _record.Append((char) pCdrExportMap.CdrExportDelimeter);
		//        }
		//        _records.Add(_record.ToString().TrimEnd((char) pCdrExportMap.CdrExportDelimeter));
		//      }
		//    }
		//  }
		//  pBackgroundWorker.ReportStatus(string.Format("Retrieved {0} CDRs", _records.Count));
		//  return _records;
		//}
		static List<string> getCDRs(string pWhereFilter, ICdrExportInfo pCdrExportInfo, int pTimokDate, IBackgroundWorker pBackgroundWorker) {
			var _records = new List<string>();
			if (Cdr_Db.Exists(TimokDate.ToDateTime(pTimokDate))) {
				CDRViewRow[] _cdrViewRows;
				try {
					using (var _db = new Cdr_Db(TimokDate.ToDateTime(pTimokDate))) {
						_cdrViewRows = _db.CDRViewCollection.Get(pTimokDate, pWhereFilter);
					}
				}
				catch (Exception _ex) {
					TimokLogger.Instance.LogRbr(LogSeverity.Critical, GetcdrsLabel, string.Format("Exception:\r\n{0}", _ex));
					pBackgroundWorker.ReportStatus(string.Format("Exception! Exported {0} CDRs", _records.Count));
					return _records;
				}
				if (_cdrViewRows == null || _cdrViewRows.Length == 0) {
					TimokLogger.Instance.LogRbr(LogSeverity.Error, GetcdrsLabel, "No CDR Records found");
					pBackgroundWorker.ReportStatus("No CDR Records found");
					return _records;
				}

				pBackgroundWorker.ReportStatus(string.Format("Retrieved {0} CDRs for: {1}", _cdrViewRows.Length, TimokDate.ToDateTime(pTimokDate).ToString("yyyy-MM-dd HH:mm")));

				var _recordCount = 0;
				foreach (var _cdrViewRow in _cdrViewRows) {
					if (pBackgroundWorker.CancellationPending) {
						throw new Exception("CDR Export canceled");
					}

					if (pCdrExportInfo.WithRerating && _cdrViewRow.Duration > 0) {
						if (! rerateCDR(_cdrViewRow, pBackgroundWorker)) {
							continue;
						}
					}

					_records.Add(mapToExportedRecord(_cdrViewRow, pCdrExportInfo.CdrExportMap));
					pBackgroundWorker.ReportProgress(++_recordCount * 100 / _cdrViewRows.Length);
				}

				if (_records.Count != _cdrViewRows.Length) {
					pBackgroundWorker.ReportStatus(string.Format("ERROR: Exported {0} out of {1} retreived CDRs", _records.Count, _cdrViewRows.Length));
				}
				else {
					pBackgroundWorker.ReportStatus(string.Format("Exported {0} CDRs for: {1}", _records.Count, TimokDate.ToDateTime(pTimokDate).ToString("yyyy-MM-dd HH:mm")));
				}
			}
			return _records;
		}

		static bool rerateCDR(CDRViewRow_Base pCdrViewRow, IBackgroundWorker pBackgroundWorker) {
			try {
				var _customerAcct = CustomerAcct.Get(pCdrViewRow.Customer_acct_id);
				if (_customerAcct == null) {
					pBackgroundWorker.ReportStatus(string.Format("ERROR! CustomerAcct NOT FOUND, {0}", pCdrViewRow.Customer_acct_id));
					return false;
				}

				var _customerRoute = CustomerRoute.Get(_customerAcct.ServiceId, _customerAcct.RoutingPlanId, pCdrViewRow.Customer_route_id);
				if (_customerRoute == null) {
					pBackgroundWorker.ReportStatus(string.Format("ERROR! CustomerRoute NOT FOUND, {0}", pCdrViewRow.Customer_route_id));
					return false;
				}

				var _carrierAcct = CarrierAcct.Get(pCdrViewRow.Carrier_acct_id);
				if (_carrierAcct == null) {
					pBackgroundWorker.ReportStatus(string.Format("ERROR! CarrierAcct NOT FOUND, {0}", pCdrViewRow.Carrier_acct_id));
					return false;
				}

				var _carrierRoute = CarrierRoute.Get(_carrierAcct.Id, pCdrViewRow.Carrier_route_id);
				if (_carrierRoute == null) {
					pBackgroundWorker.ReportStatus(string.Format("ERROR! CarrierRoute NOT FOUND, {0}", pCdrViewRow.Carrier_route_id));
					return false;
				}

				var _cdr = new Cdr(pCdrViewRow);
				_customerAcct.RateCall(_customerRoute, ref _cdr);
				pCdrViewRow.Customer_duration = _cdr.CustomerDuration;
				pCdrViewRow.Price = _cdr.CustomerPrice;
				_carrierAcct.RateCall(_carrierRoute, ref _cdr);
				pCdrViewRow.Carrier_duration = _cdr.CarrierDuration;
				pCdrViewRow.Cost = _cdr.CarrierCost;
			}
			catch (Exception _ex) {
				pBackgroundWorker.ReportStatus(string.Format("Exception! {0}", _ex.Message));
				TimokLogger.Instance.LogRbr(LogSeverity.Critical, "CdrExportController.rerate", string.Format("Exception:\r\n{0}", _ex));
				return false;
			}
			return true;
		}

		static string mapToExportedRecord(CDRViewRow pCDRViewRow, CdrExportMapDto pCdrExportMap) {
			var _record = new StringBuilder();
			foreach (var _field in pCdrExportMap.CdrExportMapDetails) {
				if (_field.IsDateTimeField && _field.FormatType != null && _field.FormatType.Trim().Length > 0) {
					var _date = DateTime.Parse(pCDRViewRow[_field.FieldName]);
					_record.Append(_date.ToString(_field.DateTimeFormat));
				}
				else {
					_record.Append(pCDRViewRow[_field.FieldName]);
				}
				_record.Append((char) pCdrExportMap.CdrExportDelimeter);
			}
			return _record.ToString();
		}

		#endregion Export Cdrs

		#region public static Getters

		public static CdrExportMapDto GetCdrExportMap(int pMapId) {
			using (var _db = new Rbr_Db()) {
				var _cdrExportMapRow = _db.CdrExportMapCollection.GetByPrimaryKey(pMapId);
				return getMap(_db, _cdrExportMapRow);
			}
		}

		static CdrExportMapDto getMap(Rbr_Db_Base pDb, CdrExportMapRow pCdrExportMapRow) {
			if (pCdrExportMapRow == null) {
				return null;
			}
			CdrExportMapDetailRow[] _cdrExportMapDetailRows = pDb.CdrExportMapDetailCollection.GetByMap_id(pCdrExportMapRow.Map_id);
			CdrExportMapDto _cdrExportMap = MapToCdrExportMap(pCdrExportMapRow);
			_cdrExportMap.CdrExportMapDetails = MapToCdrExportMapDetails(_cdrExportMapDetailRows);
			return _cdrExportMap;
		}

		public static CdrExportMapDto[] GetAllCdrExportMaps() {
			using (var _db = new Rbr_Db()) {
				var _list = new List<CdrExportMapDto>();
				CdrExportMapRow[] _cdrExportMapRows = _db.CdrExportMapCollection.GetAll();
				foreach (CdrExportMapRow _cdrExportMapRow in _cdrExportMapRows) {
					_list.Add(getMap(_db, _cdrExportMapRow));
				}
				return _list.ToArray();
			}
		}

		public static CdrExportMapDetailDto GetCdrExportMapDetail(int pMapDetailId) {
			using (var _db = new Rbr_Db()) {
				return MapToCdrExportMapDetail(_db.CdrExportMapDetailCollection.GetByPrimaryKey(pMapDetailId));
			}
		}

		public static CdrExportMapDetailDto[] GetCdrExportMapDetailsByMapId(int pMapId) {
			using (var _db = new Rbr_Db()) {
				return MapToCdrExportMapDetails(_db.CdrExportMapDetailCollection.GetByMap_id(pMapId));
			}
		}

		public static string[] GetExportTableDbFieldNames() {
			var _list = new List<string>();
			_list.Add(CDRViewRow_Base.date_logged_DbName);
			_list.Add(CDRViewRow_Base.start_DbName);
			_list.Add(CDRViewRow_Base.duration_DbName);
			_list.Add(CDRViewRow_Base.minutes_DbName);
			_list.Add(CDRViewRow_Base.ccode_DbName);
			_list.Add(CDRViewRow_Base.local_number_DbName);
			_list.Add(CDRViewRow_Base.dialed_number_DbName);
			_list.Add(CDRViewRow_Base.customer_route_name_DbName);

			_list.Add(CDRViewRow_Base.prefix_in_DbName);
			_list.Add(CDRViewRow_Base.orig_dot_IP_address_DbName);
			_list.Add(CDRViewRow_Base.orig_end_point_id_DbName);
			_list.Add(CDRViewRow_Base.orig_alias_DbName);
			_list.Add(CDRViewRow_Base.customer_acct_name_DbName);
			_list.Add(CDRViewRow_Base.orig_partner_name_DbName);

			_list.Add(CDRViewRow_Base.prefix_out_DbName);
			_list.Add(CDRViewRow_Base.term_ip_address_range_DbName);
			_list.Add(CDRViewRow_Base.term_end_point_id_DbName);
			_list.Add(CDRViewRow_Base.term_alias_DbName);
			_list.Add(CDRViewRow_Base.carrier_acct_name_DbName);
			_list.Add(CDRViewRow_Base.carrier_route_name_DbName);
			_list.Add(CDRViewRow_Base.term_partner_name_DbName);

			_list.Add(CDRViewRow_Base.disconnect_cause_DbName);
			_list.Add(CDRViewRow_Base.mapped_disconnect_cause_DbName);
			_list.Add(CDRViewRow_Base.disconnect_source_DbName);
			_list.Add(CDRViewRow_Base.rbr_result_DbName);

			_list.Add(CDRViewRow_Base.price_DbName);
			_list.Add(CDRViewRow_Base.cost_DbName);
			_list.Add(CDRViewRow_Base.end_user_price_DbName);

			_list.Add(CDRViewRow_Base.node_id_DbName);
			_list.Add(CDRViewRow_Base.ANI_DbName);
			_list.Add(CDRViewRow_Base.DNIS_DbName);
			_list.Add(CDRViewRow_Base.serial_number_DbName);

			_list.Add(CDRViewRow_Base.customer_duration_DbName);
			_list.Add(CDRViewRow_Base.retail_acct_id_DbName);

			return _list.ToArray();
		}

		#endregion public static Getters

		#region public static Actions

		public static void Add(CdrExportMapDto pCdrExportMap) {
			using (var _db = new Rbr_Db()) {
				using (var _tx = new Transaction(_db, pCdrExportMap)) {
					try {
						CdrExportMapRow _cdrExportMapRow = MapToCdrExportMapRow(pCdrExportMap);
						CdrExportMapDetailRow[] _cdrExportMapDetailRows = MapToCdrExportMapDetailRows(pCdrExportMap.CdrExportMapDetails);

						_db.CdrExportMapCollection.Insert(_cdrExportMapRow);
						pCdrExportMap.MapId = _cdrExportMapRow.Map_id;

						int _sequence = 0;
						foreach (CdrExportMapDetailRow _cdrExportMapDetailRow in _cdrExportMapDetailRows) {
							_sequence++;
							_cdrExportMapDetailRow.Map_id = _cdrExportMapRow.Map_id;
							_cdrExportMapDetailRow.Sequence = _sequence;
							_db.CdrExportMapDetailCollection.Insert(_cdrExportMapDetailRow);
						}

						pCdrExportMap.CdrExportMapDetails = MapToCdrExportMapDetails(_cdrExportMapDetailRows);

						_tx.Commit();
					}
					catch {
						pCdrExportMap.MapId = 0;
						throw;
					}
				}
			}
		}

		public static void Delete(int pMapId) {
			using (var _db = new Rbr_Db()) {
				using (var _tx = new Transaction(_db, pMapId)) {
					_db.CdrExportMapDetailCollection.DeleteByMap_id(pMapId);
					_db.CdrExportMapCollection.DeleteByPrimaryKey(pMapId);
					_tx.Commit();
				}
			}
		}

		public static void Update(CdrExportMapDto pCdrExportMap) {
			using (var _db = new Rbr_Db()) {
				using (var _tx = new Transaction(_db, pCdrExportMap)) {
					CdrExportMapRow _cdrExportMapRow = MapToCdrExportMapRow(pCdrExportMap);
					_db.CdrExportMapCollection.Update(_cdrExportMapRow);

					//delete existing details
					_db.CdrExportMapDetailCollection.DeleteByMap_id(_cdrExportMapRow.Map_id);

					CdrExportMapDetailRow[] _cdrExportMapDetailRows = MapToCdrExportMapDetailRows(pCdrExportMap.CdrExportMapDetails);
					int _sequence = 0;
					//insert new 
					foreach (CdrExportMapDetailRow _cdrExportMapDetailRow in _cdrExportMapDetailRows) {
						_sequence++;
						_cdrExportMapDetailRow.Map_detail_id = 0;
						_cdrExportMapDetailRow.Map_id = _cdrExportMapRow.Map_id;
						_cdrExportMapDetailRow.Sequence = _sequence;
						_db.CdrExportMapDetailCollection.Insert(_cdrExportMapDetailRow);
					}
					pCdrExportMap.CdrExportMapDetails = MapToCdrExportMapDetails(_cdrExportMapDetailRows);

					_tx.Commit();
				}
			}
		}

		#endregion public static Actions

		#region mappings

		#region To DAL mappings

		internal static CdrExportMapRow[] MapToCdrExportMapRows(CdrExportMapDto[] pCdrExportMaps) {
			var _list = new List<CdrExportMapRow>();
			if (pCdrExportMaps != null) {
				foreach (var _cdrExportMap in pCdrExportMaps) {
					var _cdrExportMapRow = MapToCdrExportMapRow(_cdrExportMap);
					_list.Add(_cdrExportMapRow);
				}
			}
			return _list.ToArray();
		}

		internal static CdrExportMapRow MapToCdrExportMapRow(CdrExportMapDto pCdrExportMap) {
			if (pCdrExportMap == null) {
				return null;
			}

			var _cdrExportMapRow = new CdrExportMapRow();
			_cdrExportMapRow.Map_id = pCdrExportMap.MapId;
			_cdrExportMapRow.Name = pCdrExportMap.Name;
			_cdrExportMapRow.CdrExportDelimeter = pCdrExportMap.CdrExportDelimeter;
			//NOTE: not in use yet
			_cdrExportMapRow.Target_dest_folder = string.Empty; // pCdrExportMap.TargetDestFolder;

			return _cdrExportMapRow;
		}

		internal static CdrExportMapDetailRow[] MapToCdrExportMapDetailRows(CdrExportMapDetailDto[] pCdrExportMapDetails) {
			var _list = new List<CdrExportMapDetailRow>();
			if (pCdrExportMapDetails != null) {
				foreach (var _cdrExportMapDetail in pCdrExportMapDetails) {
					var _cdrExportMapDetailRow = MapToCdrExportMapDetailRow(_cdrExportMapDetail);
					_list.Add(_cdrExportMapDetailRow);
				}
			}
			return _list.ToArray();
		}

		internal static CdrExportMapDetailRow MapToCdrExportMapDetailRow(CdrExportMapDetailDto pCdrExportMapDetail) {
			if (pCdrExportMapDetail == null) {
				return null;
			}

			var _cdrExportMapDetailRow = new CdrExportMapDetailRow();
			_cdrExportMapDetailRow.Map_detail_id = pCdrExportMapDetail.MapDetailId;
			_cdrExportMapDetailRow.Map_id = pCdrExportMapDetail.MapId;
			_cdrExportMapDetailRow.Sequence = pCdrExportMapDetail.Sequence;
			_cdrExportMapDetailRow.Field_name = pCdrExportMapDetail.FieldName;
			_cdrExportMapDetailRow.Format_type = pCdrExportMapDetail.FormatType;

			return _cdrExportMapDetailRow;
		}

		#endregion To DAL mappings

		#region To BLL mappings

		internal static CdrExportMapDto[] MapToCdrExportMaps(CdrExportMapRow[] pCdrExportMapRows) {
			var _list = new List<CdrExportMapDto>();
			if (pCdrExportMapRows != null) {
				foreach (var _cdrExportMapRow in pCdrExportMapRows) {
					var _cdrExportMap = MapToCdrExportMap(_cdrExportMapRow);
					_list.Add(_cdrExportMap);
				}
			}
			return _list.ToArray();
		}

		internal static CdrExportMapDto MapToCdrExportMap(CdrExportMapRow pCdrExportMapRow) {
			if (pCdrExportMapRow == null) {
				return null;
			}

			var _cdrExportMap = new CdrExportMapDto();
			_cdrExportMap.MapId = pCdrExportMapRow.Map_id;
			_cdrExportMap.Name = pCdrExportMapRow.Name;
			_cdrExportMap.CdrExportDelimeter = pCdrExportMapRow.CdrExportDelimeter;
			//NOTE: not in use yet
			//_cdrExportMap.TargetDestFolder = pCdrExportMapRow.Target_dest_folder;

			return _cdrExportMap;
		}

		internal static CdrExportMapDetailDto[] MapToCdrExportMapDetails(CdrExportMapDetailRow[] pCdrExportMapDetailRows) {
			var _list = new List<CdrExportMapDetailDto>();
			if (pCdrExportMapDetailRows != null) {
				foreach (var _cdrExportMapDetailRow in pCdrExportMapDetailRows) {
					var _cdrExportMapDetail = MapToCdrExportMapDetail(_cdrExportMapDetailRow);
					_list.Add(_cdrExportMapDetail);
				}
			}
			return _list.ToArray();
		}

		internal static CdrExportMapDetailDto MapToCdrExportMapDetail(CdrExportMapDetailRow pCdrExportMapDetailRow) {
			if (pCdrExportMapDetailRow == null) {
				return null;
			}

			var _cdrExportMapDetail = new CdrExportMapDetailDto();
			_cdrExportMapDetail.MapDetailId = pCdrExportMapDetailRow.Map_detail_id;
			_cdrExportMapDetail.MapId = pCdrExportMapDetailRow.Map_id;
			_cdrExportMapDetail.Sequence = pCdrExportMapDetailRow.Sequence;
			_cdrExportMapDetail.FieldName = pCdrExportMapDetailRow.Field_name;
			_cdrExportMapDetail.FormatType = pCdrExportMapDetailRow.Format_type;

			return _cdrExportMapDetail;
		}

		#endregion To BLL mappings

		#endregion mappings
	}
}
using System;
using System.Collections.Generic;
using System.IO;
using Timok.Core.BackgroundProcessing;
using Timok.Logger;
using Timok.Rbr.BLL.Managers;
using Timok.Rbr.Core;
using Timok.Rbr.Core.Config;
using Timok.Rbr.DAL.RbrDatabase;
using Timok.Rbr.DTO;

namespace Timok.Rbr.BLL.ImportExport.DialPlan {
	internal class DialPlanImportExportBase {
		//Brazil|55|ROC_CEL|Weekend|OffPeak|2am-7am,9pm-11pm|1/1|0.2220000|0.2264000
		internal const string RatesFileHeaderCostPerIncrements = "Country Name|CCode|Breakout Name|Type Of Day|Time Of Day|Time Ranges (FirstHour-LastHour)|First / Add Incr|First Cost|Add Cost";
		internal const string RatesFileHeaderCostPerMinute = "Country Name|CCode|Breakout Name|Type Of Day|Time Of Day|Time Ranges (FirstHour-LastHour)|First / Add Incr|Per Minute Cost";

		protected DialPlanImportExportArgs args;
		protected SortedList<string, CountryRecord> countries;
		protected string errorFilePath;
		protected IBackgroundWorker host;

		public DialPlanImportExportBase(IBackgroundWorker pBackgroundWorker, DialPlanImportExportArgs pArgs) {
			host = pBackgroundWorker;
			args = pArgs;
			errorFilePath = Path.Combine(Path.GetDirectoryName(args.FilePath), Path.GetFileNameWithoutExtension(args.FilePath) + ".ERRORS" + Path.GetExtension(args.FilePath));
		}

		public IBackgroundWorker Host {
			get {
				if (host != null) {
					return host;
				}
				return new DummyBackgroundWorker();
			}
		}

		protected RouteType getRouteType() {
			RouteType _routeType;
			if (args.Context == ViewContext.Carrier) {
				_routeType = RouteType.Carrier;
			}
			else if (args.Context == ViewContext.Customer) {
				_routeType = RouteType.Wholesale;
			}
			else {
				throw new ArgumentException(string.Format("UNEXPECTED ViewContext: {0}", args.Context));
			}
			return _routeType;
		}

		protected int getBaseRouteId(Rbr_Db pDb, RouteType pRouteType, short pId, int pRoutingPlanId, int pBaseRouteId) {
			if (pRouteType == RouteType.Wholesale) {
				return getWholesaleRouteId(pDb, pId, pBaseRouteId, pRoutingPlanId);
			}

			if (pRouteType == RouteType.Carrier) {
				return getCarrierRouteId(pDb, pId, pBaseRouteId);
			}

			if (pRouteType == RouteType.Retail) {
				return 0; //TODO...
			}

			throw new Exception(string.Format("Unknown RouteType for Route={0}", pRouteType));
		}

		protected int getWholesaleRouteId(Rbr_Db pDb, short pServiceId, int pBaseRouteId, int pRoutingPlanId) {
			try {
				RatedRouteDto _wholesaleRoute = CustomerRouteManager.GetRouteByServiceIdBaseRouteId(pDb, pServiceId, pBaseRouteId, pRoutingPlanId);
				if (_wholesaleRoute == null) {
					string _msg = string.Format("Skipping Route, doesn't exists in DialPlan. {0}, {1}, {2}.", pServiceId, pBaseRouteId, pRoutingPlanId);
					TimokLogger.Instance.LogRbr(LogSeverity.Error, "ServiceController.importRatesOnly", string.Format("{0}", _msg));
					Host.ReportStatus(_msg);
					return 0;
				}
				if (_wholesaleRoute.RoutingPlanId <= 0) {
					string _msg = string.Format("Skipping Route, doesn't exists in RoutingPlan. {0}, {1}, {2}.", pServiceId, pBaseRouteId, pRoutingPlanId);
					TimokLogger.Instance.LogRbr(LogSeverity.Error, "ServiceController.importRatesOnly", string.Format("ServiceController.importRatesOnly | {0}", _msg));
					Host.ReportStatus(_msg);
					return 0;
				}
				return _wholesaleRoute.RatedRouteId;
			}
			catch (Exception _ex) {
				string _msg = string.Format("ERROR: {0}, Retreiving WholesaleRoute: {1}, {2}, {3}", pServiceId, pBaseRouteId, pRoutingPlanId, _ex.Message);
				Host.ReportStatus(_msg);
				TimokLogger.Instance.LogRbr(LogSeverity.Error, "RatesImporter.getWholesaleRoute", string.Format("{0}\r\n{1}", _msg, _ex));
				throw;
			}
		}

		protected int getCarrierRouteId(Rbr_Db pDb, short pCarrierAcctId, int pBaseRouteId) {
			try {
				RatedRouteDto _carrierRoute = CarrierRouteManager.Get(pDb, pCarrierAcctId, pBaseRouteId);
				if (_carrierRoute == null) {
					string _msg = string.Format("Skipping Route, doesn't exists in DialPlan. {0}, {1}.", pCarrierAcctId, pBaseRouteId);
					TimokLogger.Instance.LogRbr(LogSeverity.Error, "ServiceController.getCarrierRouteId", string.Format("ServiceController.importRatesOnly | {0}", _msg));
					Host.ReportStatus(_msg);
					return 0;
				}
				return _carrierRoute.RatedRouteId;
			}
			catch (Exception _ex) {
				string _msg = string.Format("ERROR: {0}, getCarrierRouteId: {1}, {2}", pCarrierAcctId, pBaseRouteId, _ex.Message);
				Host.ReportStatus(_msg);
				TimokLogger.Instance.LogRbr(LogSeverity.Error, "RatesImporter.getCarrierRouteId", string.Format("{0}\r\n{1}", _msg, _ex));
				throw;
			}
		}

		protected int getCountryId(Rbr_Db pDb, string pCountryName, int pCountryCode) {
			CountryDto _country = CallingPlanManager.GetCountry(pDb, pCountryName);
			if (_country != null) {
				return _country.CountryId;
			}

			_country = new CountryDto();
			_country.Name = pCountryName;
			_country.CountryCode = pCountryCode;
			_country.Status = Status.Active;
			return CallingPlanManager.AddCountry(pDb, _country);
		}

		protected int getRouteId(Rbr_Db pDb, int pCountryId, RouteRecord pRouteRecord) {
			RouteRow _routeRow = RoutingManager.GetByName(pDb, args.CallingPlanId, pRouteRecord.FullName);
			if (_routeRow == null) {
				_routeRow = new RouteRow();
				_routeRow.Name = pRouteRecord.FullName;
				_routeRow.Country_id = pCountryId;
				_routeRow.Calling_plan_id = args.CallingPlanId;
				_routeRow.RouteStatus = Status.Active;
				RoutingManager.AddBaseRoute(pDb, RoutingManager.MapToBaseRoute(pDb, _routeRow));
			}
			else {
				// Existing RouteRow: delete all dialcodes
				pDb.DialCodeCollection.DeleteByRoute_id(_routeRow.Route_id);
			}
			return _routeRow.Route_id;
		}

		protected void writeBadLines(RouteRecord _routeRecord) {
			if (File.Exists(errorFilePath)) {
				File.Delete(errorFilePath);
			}

			using (var _sw = new StreamWriter(errorFilePath, false)) {
				_sw.WriteLine(args.PerMinute ? RatesFileHeaderCostPerMinute : RatesFileHeaderCostPerIncrements); //write header for Bad Rates File [only]
				foreach (string _routeLine in _routeRecord.BadRouteLines) {
					_sw.WriteLine(_routeLine);
				}
			}
		}

		protected RouteRow[] getBaseRouteRows(Rbr_Db _db) {
			RouteRow[] _baseRouteRows;
			if (args.RoutingPlanId > 0) {
				_baseRouteRows = _db.RouteCollection.GetByCallingPlanIdRoutingPlanId(args.CallingPlanId, args.RoutingPlanId);
			}
			else if (args.AccountId > 0) {
				_baseRouteRows = _db.RouteCollection.GetByCallingPlanIdCarrierAcctId(args.CallingPlanId, args.AccountId);
			}
			else {
				_baseRouteRows = _db.RouteCollection.GetByCalling_plan_id(args.CallingPlanId);
			}
			return _baseRouteRows;
		}

		protected string getFilePath() {
			string _baseFileName_NoExt = FileHelper.CleanupInvalidFileNameCharacters(args.AccountName + "_" + DateTime.Today.ToString("yyyy-MM-dd"), '_');

			string _ext;
			if (args.ImportExportFilter == ImportExportFilter.Rates) {
				_ext = ".Rates";
			}
			else {
				_ext = ".DialCodes";
			}

			string _directory = FileHelper.CleanupInvalidDirectoryCharacters(args.FilePath, '_');
			string _filePathDialPlan = Path.Combine(_directory, _baseFileName_NoExt + _ext);
			if (File.Exists(_filePathDialPlan)) {
				File.Delete(_filePathDialPlan);
			}
			return _filePathDialPlan;
		}

		protected ServiceDto getService(Rbr_Db _db) {
			ServiceDto _service = ServiceManager.GetService(_db, args.AccountId);
			if (_service == null) {
				throw new Exception("Service not found [Id: " + args.AccountId + "]");
			}
			return _service;
		}

		protected CarrierAcctDto getCarrierAcct(Rbr_Db _db) {
			CarrierAcctDto _carrierAcct = CarrierAcctManager.GetAcct(_db, args.AccountId);
			if (_carrierAcct == null) {
				throw new Exception("CarrierAcct not found [Id: " + args.AccountId + "]");
			}
			return _carrierAcct;
		}

		protected CountryRecord getCountryRecord(Rbr_Db pDb, RouteRow pBaseRouteRow) {
			CountryRow _countryRow = pDb.CountryCollection.GetByPrimaryKey(pBaseRouteRow.Country_id);
			if (_countryRow == null) {
				throw new Exception(string.Format("Country NOT FOUND CountryId: {0}", pBaseRouteRow.Country_id));
			}

			CountryRecord _countryRecord;
			if (countries.ContainsKey(_countryRow.Name)) {
				_countryRecord = countries[_countryRow.Name];
			}
			else {
				_countryRecord = new CountryRecord(_countryRow.Country_code, _countryRow.Name);
				countries.Add(_countryRecord.Name, _countryRecord);
			}

			if (_countryRecord.Routes.ContainsKey(pBaseRouteRow.Name)) {
				throw new Exception(string.Format("Unexpected: Route already processed? {0}", pBaseRouteRow.Name));
			}
			return _countryRecord;
		}

		protected RouteRecord getRouteRecord(RouteRow _baseRouteRow, CountryRecord _countryRecord) {
			var _routeRecord = new RouteRecord(_baseRouteRow.Name);
			_routeRecord.CountryName = _countryRecord.Name;
			_routeRecord.CountryCode = _countryRecord.Code;
			return _routeRecord;
		}

		protected void reportStatus(LogSeverity pSeverity, string pFullMethodName, string pMessage) {
			TimokLogger.Instance.LogRbr(pSeverity, pFullMethodName, pMessage);
			Host.ReportStatus(pMessage);
		}
	}
}
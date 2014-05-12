using System;
using System.Collections.Generic;
using System.IO;
using Timok.Core.BackgroundProcessing;
using Timok.Logger;
using Timok.Rbr.BLL.DTO;
using Timok.Rbr.BLL.Managers;
using Timok.Rbr.Core.Config;
using Timok.Rbr.DAL.RbrDatabase;
using Timok.Rbr.DTO;

namespace Timok.Rbr.BLL.ImportExport.DialPlan {
	internal class DialPlanExporter : DialPlanImportExportBase {
		public DialPlanExporter(IBackgroundWorker pHost, DialPlanImportExportArgs pArgs) : base(pHost, pArgs) { }

		public bool Export() {
			host.ReportProgress(0);

			if (args.ImportExportFilter == ImportExportFilter.Rates || args.ImportExportFilter == ImportExportFilter.Both) {
				reportStatus(LogSeverity.Status, "DialPlanExporter.Export", "Rates Export started...");
				if (args.Context == ViewContext.Customer || args.Context == ViewContext.Service) {
					exportWholesaleRates();
					return true;
				}

				if (args.Context == ViewContext.Carrier) {
					exportCarrierRates();
					return true;
				}
				throw new Exception(string.Format("Unknown ViewContext={0}", args.Context));
			}
			
			if (args.ImportExportFilter == ImportExportFilter.DialPlan || args.ImportExportFilter == ImportExportFilter.Both) {
				reportStatus(LogSeverity.Status, "DialPlanExporter.Export", "Dial Plan Export started...");
				exportDialPlan();
				return true;
			}
			return false;
		}

		//------------------------------------- Private ----------------------------------------------
		void exportWholesaleRates() {
			try {
				countries = new SortedList<string, CountryRecord>();

				using (var _db = new Rbr_Db()) {
					ServiceDto _service = getService(_db);

					RouteRow[] _baseRouteRows = _db.RouteCollection.GetByCallingPlanIdRoutingPlanId(_service.CallingPlanId, args.RoutingPlanId);
					if (_baseRouteRows == null || _baseRouteRows.Length <= 0) {
						reportStatus(LogSeverity.Status, "DialPlanExporter.exportWholesaleRates", "WARNING: No Routes to Export...");
						return;
					}

					_baseRouteRows = RoutingManager.SortRouteRows(_baseRouteRows);
					string _filePath = getFilePath();

					using (var _sw = new StreamWriter(_filePath, false)) {
						_sw.WriteLine(args.PerMinute ? RatesFileHeaderCostPerMinute : RatesFileHeaderCostPerIncrements);

						int _index = 0;
						CountryRecord _countryRecord = null;
						foreach (RouteRow _baseRouteRow in _baseRouteRows) {
							host.ReportProgress(_index++ * 100 / _baseRouteRows.Length);

							WholesaleRouteRow _wholesaleRouteRow = _db.WholesaleRouteCollection.GetByServiceIdBaseRouteId(_service.ServiceId, _baseRouteRow.Route_id);
							if (_wholesaleRouteRow == null) {
								continue;
							}

							if (_countryRecord == null || _countryRecord.Name != _baseRouteRow.Name) {
								_countryRecord = getCountryRecord(_db, _baseRouteRow);
							}
							RouteRecord _routeRecord = getRouteRecord(_baseRouteRow, _countryRecord);
							_countryRecord.Routes.Add(_routeRecord.FullName, _routeRecord);

							WholesaleRateHistoryRow _wholesaleRateHistoryRow = _db.WholesaleRateHistoryCollection.GetByWholesaleRouteIdDate(_wholesaleRouteRow.Wholesale_route_id, DateTime.Today);
							if (_wholesaleRateHistoryRow != null) {
								RatingInfoDto _ratingInfo = RatingManager.GetRatingInfo(_db, _wholesaleRateHistoryRow.Rate_info_id, false);
								if (_ratingInfo == null) {
									reportStatus(LogSeverity.Critical, "DialPlanExporter.exportWholesaleRates", string.Format("RatingInfo == null, {0}", _wholesaleRateHistoryRow.Rate_info_id));
									continue;
								}
								_routeRecord.RatingInfo = _ratingInfo;
								reportStatus(LogSeverity.Status, "DialPlanExporter.exportWholesaleRates", string.Format("Exporting Rates for Route: {0}", _routeRecord.FullName));
								_sw.Write(_routeRecord.GetRatesAsString(args.PerMinute));
							}
						}
					}
				}
			}
			catch (Exception _ex) {
				reportStatus(LogSeverity.Critical, "DialPlanExporter.exportWholesaleRates", string.Format("Exception:\r\n{0}", _ex));
				throw;
			}
		}

		void exportCarrierRates() {
			try {
				countries = new SortedList<string, CountryRecord>();

				using (var _db = new Rbr_Db()) {
					CarrierAcctDto _carrierAcct = getCarrierAcct(_db);

					RouteRow[] _baseRouteRows = _db.RouteCollection.GetByCalling_plan_id(_carrierAcct.CallingPlan.CallingPlanId);
					if (_baseRouteRows == null || _baseRouteRows.Length <= 0) {
						reportStatus(LogSeverity.Status, "DialPlanExporter.exportCarrierRates", "WARNING: No Routes to Process...");
						return;
					}

					_baseRouteRows = RoutingManager.SortRouteRows(_baseRouteRows);
					string _filePath = getFilePath();

					using (var _sw = new StreamWriter(_filePath, false)) {
						_sw.WriteLine(args.PerMinute ? RatesFileHeaderCostPerMinute : RatesFileHeaderCostPerIncrements);

						int _index = 0;
						CountryRecord _countryRecord = null;
						foreach (RouteRow _baseRouteRow in _baseRouteRows) {
							host.ReportProgress(_index++ * 100 / _baseRouteRows.Length);

							CarrierRouteRow _carrierRouteRow = _db.CarrierRouteCollection.GetByCarrierAcctIdRouteId(_carrierAcct.CarrierAcctId, _baseRouteRow.Route_id);
							if (_carrierRouteRow == null) {
								continue;
							}

							if (_countryRecord == null || _countryRecord.Name != _baseRouteRow.Name) {
								_countryRecord = getCountryRecord(_db, _baseRouteRow);
							}
							RouteRecord _routeRecord = getRouteRecord(_baseRouteRow, _countryRecord);
							_countryRecord.Routes.Add(_routeRecord.FullName, _routeRecord);

							CarrierRateHistoryRow _carrierRateHistoryRow = _db.CarrierRateHistoryCollection.GetByCarrierRouteIdDate(_carrierRouteRow.Carrier_route_id, DateTime.Today);
							if (_carrierRateHistoryRow != null) {
								RatingInfoDto _ratingInfo = RatingManager.GetRatingInfo(_db, _carrierRateHistoryRow.Rate_info_id, false);
								if (_ratingInfo == null) {
									reportStatus(LogSeverity.Critical, "DialPlanExporter.exportCarrierRates", string.Format("RatingInfo == null, {0}", _carrierRateHistoryRow.Rate_info_id));
									continue;
								}
								_routeRecord.RatingInfo = _ratingInfo;
								reportStatus(LogSeverity.Status, "DialPlanExporter.exportCarrierRates", string.Format("Exporting Rates for Route: {0}", _routeRecord.FullName));
								_sw.Write(_routeRecord.GetRatesAsString(args.PerMinute));
							}
						}
					}
				}
			}
			catch (Exception _ex) {
				TimokLogger.Instance.LogRbr(LogSeverity.Critical, "DialPlanExporter.exportCarrierRates", _ex.ToString());
				throw;
			}
		}

		void exportDialPlan() {
			countries = new SortedList<string, CountryRecord>();

			using (var _db = new Rbr_Db()) {
				RouteRow[] _baseRouteRows = getBaseRouteRows(_db);
				if (_baseRouteRows == null || _baseRouteRows.Length <= 0) {
					reportStatus(LogSeverity.Status, "DialPlanExporter.exportDialPlan", "No Routes to Process...");
					return;
				}

				_baseRouteRows = RoutingManager.SortRouteRows(_baseRouteRows);

				string _filePath = getFilePath();

				using (var _swDialPlan = new StreamWriter(_filePath, false)) {
					int _index = 0;
					CountryRecord _countryRecord = null;
					foreach (RouteRow _baseRouteRow in _baseRouteRows) {
						host.ReportProgress(_index++ * 100 / _baseRouteRows.Length);

						if (args.RoutingPlanId > 0) {
							RoutingPlanDetailRow _routingPlanDetailRow = _db.RoutingPlanDetailCollection.GetByPrimaryKey(args.RoutingPlanId, _baseRouteRow.Route_id);
							if (_routingPlanDetailRow == null) {
								continue; //NOTE: skip this route
							}
						}

						if (_countryRecord == null || _countryRecord.Name != _baseRouteRow.Name) {
							_countryRecord = getCountryRecord(_db, _baseRouteRow);
						}

						if (_countryRecord.Routes.ContainsKey(_baseRouteRow.Name)) {
							throw new Exception(string.Format("Unexpected: Route already processed? {0}", _baseRouteRow.Name));
						}

						RouteRecord _routeRecord = getRouteRecord(_baseRouteRow, _countryRecord);
						_countryRecord.Routes.Add(_routeRecord.FullName, _routeRecord);

						reportStatus(LogSeverity.Status, "DialPlanExporter.exportDialPlanToFile", string.Format("Exporting DialCodes for Route: {0}", _routeRecord.FullName));

						DialCodeRow[] _dialCodeRows = _db.DialCodeCollection.GetByRoute_id(_baseRouteRow.Route_id);
						if (_dialCodeRows != null && _dialCodeRows.Length > 0) {
							var _dialCodesAsStringArray = new string[_dialCodeRows.Length];
							for (int _i = 0; _i < _dialCodeRows.Length; _i++) {
								_dialCodesAsStringArray[_i] = _dialCodeRows[_i].Dial_code.ToString();
							}
							_routeRecord.AddDialCodes(_dialCodesAsStringArray);
						}
						_swDialPlan.Write(_routeRecord.DialCodesAsString);
					}
				}
			}
		}
	}
}
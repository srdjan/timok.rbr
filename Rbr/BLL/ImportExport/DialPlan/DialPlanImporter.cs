using System;
using System.Collections.Generic;
using Timok.Core.BackgroundProcessing;
using Timok.Logger;
using Timok.Rbr.BLL.Managers;
using Timok.Rbr.DAL.RbrDatabase;
using Timok.Rbr.DTO;

namespace Timok.Rbr.BLL.ImportExport.DialPlan {
	internal class DialPlanImporter : DialPlanImportExportBase {
		public DialPlanImporter(IBackgroundWorker pBackgroundWorker, DialPlanImportExportArgs pArgs) : base(pBackgroundWorker, pArgs) { }

		public bool Import(IList<CountryRecord> pCountries) { 
			Host.ReportProgress(0);
			try {
				foreach (var _countryRecord in pCountries) {
					importCountry(_countryRecord);
				}
			}
			catch (Exception _ex) {
				reportStatus(LogSeverity.Error, "DialPlanImporter.Import", _ex.Message);
				return false;
			}
			return true;
		}

		//--------------------------- Private -----------------------------------------------
		void importCountry(CountryRecord pCountryRecord) {
			reportStatus(LogSeverity.Status, "DialPlanImporter.importCountry", string.Format("Importing Country={0}", pCountryRecord.Name));

			int _indexRoute = 0;
			foreach (var _routeRecord in pCountryRecord.Routes.Values) {
				if (_routeRecord.BadRouteLines != null && _routeRecord.BadRouteLines.Length > 0) {
					writeBadLines(_routeRecord);
					throw new Exception(string.Format("ERROR: Bad input data, Route={0}", _routeRecord.FullName));
				}

				Host.ReportProgress(_indexRoute++ * 100 / pCountryRecord.Routes.Values.Count);

				if (args.ImportExportFilter == ImportExportFilter.DialPlan || args.ImportExportFilter == ImportExportFilter.Both) {
					importRouteDialCodes(_routeRecord);
				}
				else if (args.ImportExportFilter == ImportExportFilter.Rates || args.ImportExportFilter == ImportExportFilter.Both) {
					importRouteRates(_routeRecord);
				}
				else {
					throw new Exception(string.Format("ERROR: Bad ImportExport Filter={0}", args.ImportExportFilter));
				}
			}
		}

		void importRouteRates(RouteRecord pRouteRecord) {//}, int pCallingPlanId, short pId, RouteType pRouteType, int pRoutingPlanId) {
			if (pRouteRecord.RatingInfo == null) {
				throw new Exception(string.Format("ERROR: No Rating Info found for Route={0}", pRouteRecord.FullName));
			}

			reportStatus(LogSeverity.Status, "DialPlanImporter.importRouteRates", string.Format("Importing Route={0}", pRouteRecord.FullName));
			using (var _db = new Rbr_Db()) {
				_db.BeginTransaction();

				try {
					//-- get base RouteRow
					var _routeRow = RoutingManager.GetByName(_db, args.CallingPlanId, pRouteRecord.FullName);
					if (_routeRow == null) {
						reportStatus(LogSeverity.Status, "DialPlanImporter.importRouteRates", string.Format("SKIPPING: Route={0},  doesn't exists in CallingPlan={1}.", pRouteRecord.FullName, args.CallingPlanId));
						return;
					}

					var _routeType = getRouteType();
					int _rateRouteId = getBaseRouteId(_db, _routeType, args.AccountId, args.RoutingPlanId, _routeRow.Route_id);
					//-- Expects that imported file has records for Routes whose rates are changing only. 
					//-- If the rate import record for existing Route is missing; Route is skipped
					//-- If the route record is not found we create a new record and add rates to it
					//-- Add/Update ratingHistory: default time period today, maxTime used
					var _ratingHistoryEntry = new RatingHistoryEntry(_routeType, _rateRouteId, args.From, args.To,  pRouteRecord.RatingInfo);
					RatingManager.SaveRatingHistoryEntry(_db, _ratingHistoryEntry);
					_db.CommitTransaction();
				}
				catch (Exception _ex) {
					_db.RollbackTransaction();
					reportStatus(LogSeverity.Error, "DialPlanImporter.importRouteRates", _ex.Message);
					throw;
				}
			}
		}

		void importRouteDialCodes(RouteRecord pRouteRecord) {
			Host.ReportStatus(string.Format("Importing Dial Codes for Route: {0}", pRouteRecord.FullName));

			using (var _db = new Rbr_Db()) {
				_db.BeginTransaction();
				try {
					int _countryId = getCountryId(_db, pRouteRecord.CountryName, pRouteRecord.CountryCode);
					int _routeId = getRouteId(_db, _countryId, pRouteRecord);

					int _indexDialCode = 0;
					foreach (long _dialCode in pRouteRecord.DialCodes.Values) {
						if (Host.CancellationPending) {
							throw new Exception("Import canceled");
						}

						if (!_dialCode.ToString().StartsWith(pRouteRecord.CountryCode.ToString())) {
							throw new Exception(string.Format("Invalid Dial Code={0}, CCode={1}", _dialCode, pRouteRecord.CountryCode));
						}

						var _dialCodeDto = new DialCodeDto();
						_dialCodeDto.Code = _dialCode;
						_dialCodeDto.BaseRouteId = _routeId;
						_dialCodeDto.CallingPlanId = args.CallingPlanId;
						_dialCodeDto.Version = 0;
						try {
						CallingPlanManager.AddDialCode(_db, _dialCodeDto);
						}
						catch (Exception _ex) {
							TimokLogger.Instance.LogRbr(LogSeverity.Debug, "", string.Format("Error inserting dial code: {0}\r\n{1}", _dialCodeDto.Code, _ex));
						}

						if (++_indexDialCode % 10 == 0) {
							Host.ReportProgress(_indexDialCode * 100 / pRouteRecord.DialCodes.Values.Count);
						}
					}

					Host.ReportStatus(string.Format("Imported total of {0} Dial Codes for Route: {1}", pRouteRecord.DialCodes.Values.Count, pRouteRecord.FullName));
					_db.CommitTransaction();
				}
				catch (Exception _ex) {
					_db.RollbackTransaction();
					reportStatus(LogSeverity.Error, "DialPlanImporter.importRouteDialCodes", _ex.Message);
					throw;
				}
			}
		}
	}
}

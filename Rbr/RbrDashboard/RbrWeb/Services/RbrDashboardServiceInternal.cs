using System;
using System.Collections.Generic;
using RbrData;
using RbrData.DataContracts;

namespace RbrWeb.Services {
	public class RbrDashboardServiceInternal {

		public ReportContext GetReportContext(int? pPartnerId) {
			var _db = new Domain();
			var _partners = _db.GetPartner(pPartnerId);
			var _reportContext = new ReportContext(_partners);
			return _reportContext;
		}

		public ReportContainer GetNodeReport(string pShortDateString) {
			try {
				var _reports = new Reports();
				var _nodeReport = _reports.GetNodeReport(pShortDateString);
				return new ReportContainer(_nodeReport, string.Empty);
			}
			catch (Exception _ex) {
				return new ReportContainer(new List<NodeReportRecord>(), string.Format("GetNodeReport\r\n{0}", _ex));
			}
		}

		public ReportContainer GetCustomerReport(int pPartnerAcctId, string pShortDateString) {
			try {
				var _reports = new Reports();

				var _customerAccts = getCustomers(pPartnerAcctId);
				if (_customerAccts == null) {
					return new ReportContainer(new List<CustomerReportRecord>(), "No Partners found");
				}

				var _customerReport = _reports.GetCustomersReport(_customerAccts, pShortDateString);
				return new ReportContainer(_customerReport, string.Empty);
			}
			catch (Exception _ex) {
				return new ReportContainer(new List<CustomerReportRecord>(), string.Format("GetCustomerReport\r\n{0}", _ex));
			}
		}

		public ReportContainer GetRouteReport(int pPartnerAcctId, string pShortDateString) {
			try {
				var _reports = new Reports();

				var _customerAccts = getCustomers(pPartnerAcctId);
				if (_customerAccts == null) {
					return new ReportContainer(new List<RouteReportRecord>(), "No Partners found");
				}

				var _routeReport = _reports.GetRouteReport(_customerAccts, pShortDateString);
				return new ReportContainer(_routeReport, string.Empty);
			}
			catch (Exception _ex) {
				return new ReportContainer(new List<RouteReportRecord>(), string.Format("GetRouteReport\r\n{0}", _ex));
			}
		}

		public ReportContainer GetTrunkReport(int pPartnerAcctId, string pShortDateString) {
			try {
				var _reports = new Reports();

				var _carrierAccts = getCarriers(pPartnerAcctId);
				if (_carrierAccts == null) {
					return new ReportContainer(new List<TrunkReportRecord>(), "No Partners found");
				}

				var _trunkReport = _reports.GetCarrierTrunkReport(_carrierAccts, pShortDateString);
				return new ReportContainer(_trunkReport, string.Empty);
			}
			catch (Exception _ex) {
				return new ReportContainer(new List<TrunkReportRecord>(), string.Format("GetTrunkReport\r\n{0}", _ex));
			}
		}

		//------------------------------------------------------------------------------------
		List<CustomerAcctRecord> getCustomers(int pPartnerAcctId) {
			var _db = new Domain();
			var _partners = _db.GetPartner(pPartnerAcctId);
			if (_partners == null) {
				return null;
			}

			var _customerAccts = new List<CustomerAcctRecord>();
			foreach (var _partner in _partners) {
				if (_partner.CustomerAccts != null) {
					_customerAccts.AddRange(_partner.CustomerAccts);
				}
			}
			return _customerAccts;
		}

		List<CarrierAcctRecord> getCarriers(int pPartnerAcctId) {
			var _db = new Domain();
			var _partners = _db.GetPartner(pPartnerAcctId);
			if (_partners == null) {
				return null;
			}

			var _carrierAccts = new List<CarrierAcctRecord>();
			foreach (var _partner in _partners) {
				if (_partner.CarrierAccts != null) {
					_carrierAccts.AddRange(_partner.CarrierAccts);
				}
			}
			return _carrierAccts;
		}
	}
}
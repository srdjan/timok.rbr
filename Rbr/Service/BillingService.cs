using System;
using Timok.Core;
using Timok.Logger;
using Timok.Rbr.BLL.DOM;
using Timok.Rbr.Core;
using Timok.Rbr.Core.Config;

namespace Timok.Rbr.Service {
	public sealed class BillingService {
		readonly IConfiguration configuration;
		readonly ILogger T;
		readonly DualFileWriter dualFileWriter;

		public BillingService(IConfiguration pConfiguration, ILogger pLogger) {
			configuration = pConfiguration;
			T = pLogger;

			if (configuration.Main.LogCdrToFile) {
				dualFileWriter = new DualFileWriter(configuration.Folders.CdrExportFolder, 
																						configuration.Folders.FtpCdrExportFolder, 
																						configuration.Main.CdrExportFileNameFormat, 
																						AppConstants.CdrFileExtension, 
																						configuration.Main.CDRExportFileRecyclePeriod);
			}
			else {
				dualFileWriter = null;
			}
		}

		/// <summary> 
		/// ----------------------------------------------------------------------------------------
		/// </summary>
		public int ProcessCallComplete(Cdr pCdr) {
			//-- Get OrigEP
			var _origEP = Endpoint.Get(pCdr.OrigIP);
			if (_origEP == null) {	
				T.LogRbr(LogSeverity.Critical, "ProcessCallComplete:", string.Format("Endpoint NOT FOUND, IP={0}", pCdr.OrigIP));
				return 1;
			}
			pCdr.OrigEPId = _origEP.Id;

			//-- Remove prefix and '011' from destination number
			if (_origEP.WithPrefixes) {
				pCdr.PrefixIn = _origEP.GetPrefixIn(pCdr.DestNumber);
				if (pCdr.PrefixIn.Length > 0) {
					pCdr.DestNumber = pCdr.DestNumber.Substring(pCdr.PrefixIn.Length);
				}
			}
			pCdr.LocalNumber = pCdr.DestNumber;

			if (pCdr.DestNumber.StartsWith(AppConstants.ZeroOneOne)) {
				pCdr.DestNumber = pCdr.DestNumber.Substring(3);
			}

			//-- get carrier acct, termEp and route
			int _result = 0;
			CarrierRoute _carrierRoute = null;
			CarrierAcct _carrierAcct = null;
			if (_result == 0) {
				_result = parseCarrier(pCdr, out _carrierAcct, out _carrierRoute);
			}

			//-- Get CustomerAcct and route
			CustomerRoute _customerRoute = null;
			CustomerAcct _customerAcct = null;
			if (pCdr.CustomerAcctId > 0 || pCdr.DNIS > 0) {
				_result = parseCustomer(pCdr, out _customerAcct, out _customerRoute);
				if (_customerRoute != null) {
					pCdr.CountryCode = _customerRoute.CountryCode;
					pCdr.CustomerRouteName = _customerRoute.Name;
					pCdr.LocalNumber = pCdr.DestNumber.Substring(_customerRoute.CountryCode.ToString().Length);
				}
			}
			else {
				_result++;
			}

			//-- extract prefix-out
			if (_carrierAcct != null && !string.IsNullOrEmpty(_carrierAcct.PrefixOut)) {
				pCdr.PrefixOut = _carrierAcct.PrefixOut;
			}
			else if (_customerAcct != null && !string.IsNullOrEmpty(_customerAcct.PrefixOut)) {
				pCdr.PrefixOut = _customerAcct.PrefixOut;
			}

			//-- do rating, debiting
			if (_carrierAcct != null && _customerAcct != null && pCdr.Duration > 0 && _result == 0) {
				_result += _carrierAcct.RateCall(_carrierRoute, ref pCdr);
				_result += _customerAcct.RateCall(_customerRoute, ref pCdr);
				if (_customerAcct.IsPrepaid) {
					_customerAcct.Debit(pCdr.CustomerPrice);
				}
			}

			//-- always do Retail RetailAcct and SubAcct status needs to be reset to 'Active'
			var _retailAcct = RetailAccount.Get(_customerAcct, pCdr, configuration, T);
			if (_retailAcct != null && _customerRoute != null) {
				_result += _retailAcct.Rate(_customerRoute, ref pCdr);
				if (_retailAcct.IsPrepaid && pCdr.RetailPrice > decimal.Zero) {
					_retailAcct.Debit(pCdr.RetailPrice, pCdr.UsedBonusMinutes);
				}
			}

			//-- Write to text file
			if (configuration.Main.LogCdrToFile) {
				_result += dualFileWriter.WriteLine(pCdr.Export());
			}
			
			return _result;
		}

		int parseCarrier(Cdr pCdr, out CarrierAcct pCarrierAcct, out CarrierRoute pCarrierRoute) {
			pCarrierAcct = null;
			pCarrierRoute = null;

			if (pCdr.CarrierAcctId == 0) {
				T.LogRbr(LogSeverity.Error, "BillingService.parseCarrier:", "Carrier NOT FOUND, CarrierAcctId=0");
				return 1;
			}

			try {
				pCarrierAcct = CarrierAcct.Get(pCdr.CarrierAcctId);
				if (pCarrierAcct == null) {
					return 1;
				}

				if (CarrierAcct.NumberOfCallsCounter.ContainsKey(pCarrierAcct.Id)) {
					if (CarrierAcct.NumberOfCallsCounter[pCarrierAcct.Id] > 0) {
						T.LogRbr(LogSeverity.Debug, "BillingService.parseCarrier:", string.Format("Calls LIMIT stat, CarrierAcct={0}, NumberOfCalls={1}", pCarrierAcct.Id, CustomerAcct.NumberOfCallsCounter[pCarrierAcct.Id]));
						CarrierAcct.NumberOfCallsCounter[pCarrierAcct.Id] -= 1;
					}
				}

				//-- get carrier route
				pCarrierRoute = CarrierRoute.Get(pCdr.CarrierAcctId, pCdr.CarrierBaseRouteId);
				if (pCarrierRoute == null) {
					T.LogRbr(LogSeverity.Error, "BillingService.parseCarrier:", string.Format("Route NOT FOUND, CarrierAcctId={0}, CarrierBaseRouteId={1}, DestNumber={2}", pCarrierAcct.Id, pCdr.CarrierBaseRouteId, pCdr.DestNumber));
					return 1;
				}
				pCdr.CarrierRouteName = pCarrierRoute.Name;

				//-- get term ep
				var _termEP = (new RoutingService()).GetTermEP(pCdr.TermIP, pCdr.Duration);
				if (_termEP == null) {
					T.LogRbr(LogSeverity.Error, "BillingService.parseCarrier:", string.Format("TermEP NOT FOUND={0}", pCdr));
					pCdr.TermEPId = 0;
					return 1;
				}
				pCdr.TermEPId = _termEP.Id;
			}
			catch (Exception _ex) {
				T.LogRbr(LogSeverity.Error, "BillingService.parseCarrier:", string.Format("Exception:\r\n{0}", _ex));
				return 1;
			}
			return 0;
		}

		int parseCustomer(Cdr pCdr, out CustomerAcct pCustomerAcct, out CustomerRoute pCustomerRoute) {
			var _result = 0;
			pCustomerAcct = null;
			pCustomerRoute = null;

			try {
				pCustomerAcct = pCdr.CustomerAcctId > 0 ? CustomerAcct.Get(pCdr.CustomerAcctId) : CustomerAcct.Get(pCdr.DNIS);

				if (CustomerAcct.NumberOfCallsCounter.ContainsKey(pCustomerAcct.Id)) {
					if (CustomerAcct.NumberOfCallsCounter[pCustomerAcct.Id] > 0) {
						T.LogRbr(LogSeverity.Debug, "BillingService.parseCustomer:", string.Format("Calls LIMIT stat, CustomerAcct={0}, NumberOfCalls={1}", pCustomerAcct.Id, CustomerAcct.NumberOfCallsCounter[pCustomerAcct.Id]));
						CustomerAcct.NumberOfCallsCounter[pCustomerAcct.Id] -= 1;
					}
				}

				//-- get customer route
				pCustomerRoute = CustomerRoute.Get(pCustomerAcct.ServiceId, pCustomerAcct.RoutingPlanId, pCdr.CustomerBaseRouteId);
				if (pCustomerRoute == null) {
					T.LogRbr(LogSeverity.Debug, "BillingService.parseCustomer:", string.Format("Didn't find CustomerRoute by CustomerRouteId={0}", pCdr.CustomerBaseRouteId));
					try { pCustomerRoute = CustomerRoute.Get(pCustomerAcct.ServiceId, pCustomerAcct.CallingPlanId, pCustomerAcct.RoutingPlanId, pCdr.DestNumber); } catch { }
					if (pCustomerRoute == null) {
						T.LogRbr(LogSeverity.Debug, "BillingService.parseCustomer:", string.Format("Didn't find CustomerRoute by ServiceId={0}, CallingPlanId={1}, DestNumber={2}", pCustomerAcct.ServiceId, pCustomerAcct.CallingPlanId, pCdr.DestNumber));
						return ++_result;
					}
				}
			}
			catch (RbrException _ex) {
				T.LogRbr(LogSeverity.Error, "BillingService.parseCustomer:", string.Format("Exception:\r\n{0}", _ex.Message));
				return 1;
			}
			catch (Exception _ex) {
				T.LogRbr(LogSeverity.Error, "BillingService.parseCustomer:", string.Format("Exception:\r\n{0}", _ex));
				return 1;
			}
			return _result;
		}
	}
}
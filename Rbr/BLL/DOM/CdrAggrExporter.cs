using System;
using Timok.Logger;
using Timok.Rbr.BLL.DOM.Repositories;
using Timok.Rbr.Core.Config;

namespace Timok.Rbr.BLL.DOM {
	public sealed class CdrAggrExporter {
		IConfiguration configuration;
		readonly ILogger T;
		//-- this sync object is used from CallStatistics and From DualRepository
		public static readonly object Padlock = new object();

		readonly DualRepository dualRepository;

		public CdrAggrExporter(IConfiguration pConfiguration, ILogger pLogger) {
			configuration = pConfiguration;
			T = pLogger;
			dualRepository = new DualRepository(configuration, TimokLogger.Instance);

			T.LogRbr(LogSeverity.Debug, "CdrAggrExporter.ReloadIniFile", "Initializing CdrAggrExporter...");
			Get(string.Empty);
		}

		//public void Init() {
		//  T.LogRbr(LogSeverity.Debug, "CdrAggrExporter.ReloadIniFile", "Initializing CdrAggrExporter...");
		//  Get(string.Empty);
		//}

		public CdrAggregate Get(string pPk) {
			return dualRepository.Current.GetByPK(pPk) as CdrAggregate;
		}

		public void Put(CdrAggregate pCdrAggregate) {
			dualRepository.Current.Put(pCdrAggregate);
		}

		public void OnCallSetup(string pAccessNumber, short pCarrierAcctId, int pCarrierRouteId, short pCustomerAcctId, int pCustomerRouteId, string pOrigIP, string pTermIP) {
			try {
				var _cdrAggregate = new CdrAggregate(pAccessNumber, pOrigIP, pTermIP, pCustomerRouteId, pCarrierRouteId, pCustomerAcctId, pCarrierAcctId);
				lock (Padlock) {
					var _cdrAggregateFromImdb = Get(_cdrAggregate.PK);
					if (_cdrAggregateFromImdb == null) {
						T.LogRbr(LogSeverity.Status, "CdrAggrExporter.OnCallSetup", string.Format("CdrAggregate NOT FOUND, pk={0}", _cdrAggregate.PK));
						_cdrAggregate.CallsAttempted = 1;
						Put(_cdrAggregate);
					}
					else {
						T.LogRbr(LogSeverity.Status, "CdrAggrExporter.OnCallSetup", string.Format("CdrAggregate FOUND, pk:{0}", _cdrAggregate.PK));
						_cdrAggregateFromImdb.CallsAttempted += 1;
					}
				}
			}
			catch (Exception _ex) {
				T.LogRbr(LogSeverity.Critical, "CdrAggrExporter.OnCallSetup", string.Format("OnCallAttempt, Exception:\r\n{0}", _ex));
			}
		}

		public void OnCallConnect(string pAccessNumber, short pCarrierAcctId, int pCarrierBaseRouteId, short pCustomerAcctId, int pCustomerBaseRouteId, int pLeg1Seconds, string pOrigIP, string pTermIP) {
			try {
				var _cdrAggregate = new CdrAggregate(pAccessNumber, pOrigIP, pTermIP, pCustomerBaseRouteId, pCarrierBaseRouteId, pCustomerAcctId, pCarrierAcctId);
				lock (Padlock) {
					var _cdrAggregateFromImdb = Get(_cdrAggregate.PK);
					if (_cdrAggregateFromImdb == null) {
						T.LogRbr(LogSeverity.Debug, "CdrAggrExporter.OnCallConnect", string.Format("CdrAggregate NOT FOUND, pk:{0}", _cdrAggregate.PK));
						_cdrAggregate.SetupSeconds = pLeg1Seconds;
						Put(_cdrAggregate);
					}
					else {
						T.LogRbr(LogSeverity.Debug, "CdrAggrExporter.OnCallConnect", string.Format("CdrAggregate FOUND, pk:{0}", _cdrAggregate.PK));
						_cdrAggregateFromImdb.SetupSeconds += pLeg1Seconds;
					}
				}
			}
			catch (Exception _ex) {
				T.LogRbr(LogSeverity.Critical, "CdrAggrExporter.OnCallConnect", string.Format("Exception:\r\n{0}", _ex));
			}
		}

		public void OnCallComplete(Cdr pCdr) {
			try {
				T.LogRbr(LogSeverity.Debug, "CdrAggrExporter.OnCallComplete", string.Format("Duration {0}, InRouteId:{1}, CustomerAcctId:{2}, TermIP:{3}, Minutes: {4}, Cost: {5}, Resell: {6}, Sell: {7},", pCdr.Duration, pCdr.CustomerBaseRouteId, pCdr.CustomerAcctId, pCdr.TermIP, pCdr.Minutes, pCdr.CarrierCost, pCdr.CustomerPrice, pCdr.RetailPrice));
				if (pCdr.Duration <= 0) {
					if (pCdr.Duration < 0) {
						T.LogRbr(LogSeverity.Critical, "CdrAggrExporter.OnCallComplete", "Duration less then zero?");
					}
					return;
				}

				lock (Padlock) {
					var _cdrAggregate = new CdrAggregate(pCdr.DNIS.ToString(), pCdr.OrigIP, pCdr.TermIP, pCdr.CustomerBaseRouteId, pCdr.CarrierBaseRouteId, pCdr.CustomerAcctId, pCdr.CarrierAcctId);
					var _cdrAggregateFromImdb = Get(_cdrAggregate.PK);
					if (_cdrAggregateFromImdb == null) {
						T.LogRbr(LogSeverity.Debug, "CdrAggrExporter.OnCallComplete", string.Format("CdrAggregate NOT FOUND, Pk={0}", _cdrAggregate.PK));

						if (pCdr.Duration > 0) {
							_cdrAggregate.CallsCompleted = 1;

							_cdrAggregate.ConnectedSeconds = pCdr.Duration;
							_cdrAggregate.ConnectedMinutes = pCdr.Minutes;

							_cdrAggregate.CarrierCost = pCdr.CarrierCost;
							_cdrAggregate.CarrierRoundedMinutes = pCdr.CarrierRoundedMinutes;

							_cdrAggregate.WholesalePrice = pCdr.CustomerPrice;
							_cdrAggregate.WholesaleRoundedMinutes = pCdr.CustomerRoundedMinutes;

							_cdrAggregate.EndUserPrice = pCdr.RetailPrice;
							_cdrAggregate.EndUserRoundedMinutes = pCdr.RetailRoundedMinutes;
							Put(_cdrAggregate);
						}
						//else {
						//TODO: need to pass leg1 length from Gk....
						//_cdrAggregate.SetupSeconds = pCdr.Leg1Duration;
						//}
					}
					else {
						T.LogRbr(LogSeverity.Debug, "CdrAggrExporter.OnCallComplete", string.Format("CdrAggregate FOUND, Pk={0}", _cdrAggregate.PK));

						if (pCdr.Duration > 0) {
							_cdrAggregateFromImdb.CallsCompleted += 1;

							_cdrAggregateFromImdb.ConnectedSeconds += pCdr.Duration;
							_cdrAggregateFromImdb.ConnectedMinutes += pCdr.Minutes;

							_cdrAggregateFromImdb.CarrierCost = decimal.Add(_cdrAggregate.CarrierCost, pCdr.CarrierCost);
							_cdrAggregateFromImdb.CarrierRoundedMinutes = decimal.Add(_cdrAggregate.CarrierRoundedMinutes, pCdr.CarrierRoundedMinutes);

							_cdrAggregateFromImdb.WholesalePrice = decimal.Add(_cdrAggregate.WholesalePrice, pCdr.CustomerPrice);
							_cdrAggregateFromImdb.WholesaleRoundedMinutes = decimal.Add(_cdrAggregate.WholesaleRoundedMinutes, pCdr.CustomerRoundedMinutes);

							_cdrAggregateFromImdb.EndUserPrice = decimal.Add(_cdrAggregate.EndUserPrice, pCdr.RetailPrice);
							_cdrAggregateFromImdb.EndUserRoundedMinutes = decimal.Add(_cdrAggregate.EndUserRoundedMinutes, pCdr.RetailRoundedMinutes);
						}
						//else {
						//TODO: need to pass leg1 length from Gk....
						//_cdrAggregate.SetupSeconds += pCdr.Leg1Duration;
						//}
					}
				}
			}
			catch (Exception _ex) {
				T.LogRbr(LogSeverity.Critical, "CdrAggrExporter.OnCallComplete", string.Format("OnCallComplete, Exception:\r\n{0}", _ex));
			}
		}
	}
}
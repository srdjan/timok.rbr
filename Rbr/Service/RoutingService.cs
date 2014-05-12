using System;
using System.Collections.Generic;
using Timok.Logger;
using Timok.NetworkLib;
using Timok.Rbr.BLL.DOM;
using Timok.Rbr.Core;
using Timok.Rbr.Core.Config;
using Timok.Rbr.DAL.RbrDatabase;

namespace Timok.Rbr.Service {
	public sealed class RoutingService {
		readonly static IDictionary<long, byte> loadBalancingTable;

		static RoutingService() {
			loadBalancingTable = new Dictionary<long, byte>();
		}

		//NOTE: used in CallComplete
		public Endpoint GetTermEP(string pTermIP, int pDuration) {
			Endpoint _termEP = null;

			try {
				if (pTermIP.CompareTo("0.0.0.0") == 0) {
					if (pDuration > 0) {
						TimokLogger.Instance.LogRbr(LogSeverity.Critical, "BillingService.getTermEP", string.Format("Call duration > 0 and TermEP equal to HostIP or 0.0.0.0, TermIP: {0}", pTermIP));
					}
					return _termEP;
				}

				//-- get TermEP
				using (var _db = new Rbr_Db()) {
					var _endpointRow = _db.EndPointCollection.GetByIPAddress(IPUtil.ToInt32(pTermIP));
					if (_endpointRow != null) {
						_termEP = new Endpoint(_endpointRow);
					}
				}
			}
			catch (Exception _ex) {
				TimokLogger.Instance.LogRbr(LogSeverity.Error, "RoutingService.GetTermEP", string.Format("getTermEP Exception:\r\n{0}", _ex));
			}
			return _termEP;
		}

		public string CleanDestNumber(string pPrefixIn, string pDestNumber) {
			if (pDestNumber == null) {
				throw new RbrException(RbrResult.DialedNumber_Invalid, "RoutingService.CleanDestNumber", "DestNumber is NULL");
			}
			if (pDestNumber.Length < 5) {
				throw new RbrException(RbrResult.DialedNumber_Invalid, "RoutingService.CleanDestNumber", string.Format("DestNumber length < 5, [destNumber: {0}]", pDestNumber));
			}

			var _cleanDestNumber = pDestNumber;

			//-- strip-off prefix (if present):
			if (pPrefixIn.Length > 0) {
				_cleanDestNumber = pDestNumber.Substring(pPrefixIn.Length);
			}

			//NOTE: not checking last digit, we should let the call go trough in those cases
			if (_cleanDestNumber.IndexOfAny(new[] {'*', '#'}, 0, _cleanDestNumber.Length - 1) >= 0) {
				throw new RbrException(RbrResult.DialedNumber_Invalid, "RoutingService.CleanDestNumber", string.Format("INVALID dialed number: {0}", pDestNumber));
			}

			//-- strip-off intl-dial-code
			var _intlDialCodeLength = 0; //default: leave '1' infront
			if (_cleanDestNumber.StartsWith(AppConstants.One)) {
				if (_cleanDestNumber.Length != 11) {
					throw new RbrException(RbrResult.DialedNumber_Invalid, "RoutingService.CleanDestNumber", string.Format("INVALID 1+ dialed number length: {0}", pDestNumber));
				}
				return _cleanDestNumber;
			}

			if (_cleanDestNumber.StartsWith(AppConstants.Zero)) {
				if (_cleanDestNumber.StartsWith(AppConstants.ZeroZero)) {
					_intlDialCodeLength = 2;
				}
				else if (_cleanDestNumber.StartsWith(AppConstants.ZeroOneOne)) {
					_intlDialCodeLength = 3;
				}
				else {
					throw new RbrException(RbrResult.DialedNumber_Invalid, "RoutingService.CleanDestNumber", string.Format("INVALID 0+ dialed number: {0}", pDestNumber));
				}
			}

			if (_intlDialCodeLength > 0) {
				_cleanDestNumber = _cleanDestNumber.Substring(_intlDialCodeLength);
			}
			return _cleanDestNumber;
		}

		public List<LegOut> GetTerminationByDestination(CustomerAcct pCustomerAcct, CustomerRoute pCustomerRoute, string pDestNumber, int pTimeLimit) {
			var _routingAlgorithm = pCustomerRoute.RoutingAlgorithmType;
			TimokLogger.Instance.LogRbr(LogSeverity.Status, "RbrDispatcher.GetTerminationByDestination", string.Format("Routing Alghorithm={0}", _routingAlgorithm));

			var _carrierRoutes = getCarrierRoutes(pCustomerAcct, pCustomerRoute, _routingAlgorithm, pDestNumber);
			if (_carrierRoutes == null || _carrierRoutes.Count == 0) {
				throw new RbrException(RbrResult.Carrier_NoRoutesFound, "RoutingService.GetTerminationByDestination", string.Format("DestinationNumber={0}", pDestNumber));
			}
	
			var _termInfoList = new List<TerminationInfo>();
			foreach (var _carrierRoute in _carrierRoutes) {
				var _carrierAcct = CarrierAcct.Get(_carrierRoute.CarrierAcctId);
				if (_carrierAcct.Status != Status.Active) {
				  TimokLogger.Instance.LogRbr(LogSeverity.Error, "RoutingService.GetTerminationByDestination", string.Format("CarrierAcct NOT Active, CarrierAcct={0}", _carrierAcct.Id));
					continue;
				}

				if (CarrierAcct.NumberOfCallsCounter.ContainsKey(_carrierAcct.Id)) {
					if (CarrierAcct.NumberOfCallsCounter[_carrierAcct.Id] >= _carrierAcct.MaxNumberOfCalls) {
						TimokLogger.Instance.LogRbr(LogSeverity.Error, "RoutingService.GetTerminationByDestination", string.Format("CarrierAcct Max Calls Limit, CarrierAcct={0}", _carrierAcct.Id));
						continue;
					}
				}
				else {
					CarrierAcct.NumberOfCallsCounter.Add(_carrierAcct.Id, 0);
				}
				CarrierAcct.NumberOfCallsCounter[_carrierAcct.Id] += 1;
			
				var _termEP = _carrierRoute.GetFirstTermEndpoint();
				if (_termEP == null) {
					continue;
				}

				var _termInfo = new TerminationInfo(_carrierAcct, _carrierRoute, _termEP);
				TimokLogger.Instance.LogRbr(LogSeverity.Debug, "RbrDispatcher.GetTerminationByDestination", string.Format("TermInfo={0}", _termInfo));
				_termInfoList.Add(_termInfo);
      }

			if (_termInfoList == null || _termInfoList.Count == 0) {
				throw new RbrException(RbrResult.Carrier_NoTermEPsFound, "RoutingService.GetTerminationByDestination", string.Format("DestinationNumber={0}", pDestNumber));
			}

			return getLegOutOptions(pCustomerAcct, pDestNumber, _termInfoList, pTimeLimit);
		}

		//-------------------------- Private ---------------------------------------------
		List<LegOut> getLegOutOptions(CustomerAcct pCustomerAcct, string pDestNumber, IEnumerable<TerminationInfo> pTermInfoList, int pTimeLimit) {
			var _legOutOptions = new List<LegOut>();

			foreach (var _termInfo in pTermInfoList) {
				var _destNumber = rewriteDestNumber(pCustomerAcct, _termInfo.TermCarrier.IntlDialCode, _termInfo.TermCarrier.PrefixOut, _termInfo.TermCarrier.Strip1Plus, pDestNumber);

				var _customHeader = string.Empty;
				if (_termInfo.TermCarrier.PartnerName.IndexOf(AppConstants.IMT) > 0) {
					_customHeader = pCustomerAcct.PartnerName.Substring(0, pCustomerAcct.PartnerName.Length > 48 ? 48 : pCustomerAcct.PartnerName.Length);
					_customHeader = Configuration.Instance.Main.CustomHeaderSourceName + _customHeader;
				}

				var _timeLimit = pTimeLimit;
				if (pTimeLimit > _termInfo.TermCarrier.MaxCallLength) {
					_timeLimit = _termInfo.TermCarrier.MaxCallLength;
					TimokLogger.Instance.LogRbr(LogSeverity.Status, "RbrDispatcher.Authorize", string.Format("Max Call Time Limited by CarrierAcctId={0}, TimeLimit={1}", _termInfo.TermCarrier.Id, _timeLimit));
				}
		
				var _legOut = new LegOut { 
																	DestNumber = _destNumber,
																	DestIPAndPort = _termInfo.TermEP.IPAddressRangeAndPort, 
																	CarrierBaseRouteId = _termInfo.TermRoute.BaseRouteId, 
																	CarrierAcctId = _termInfo.TermCarrier.Id,
																	CustomHeader = _customHeader,
																	TimeLimit = _timeLimit
																 };
				_legOutOptions.Add(_legOut);
			}
			return _legOutOptions;
		}

		List<CarrierRoute> getCarrierRoutes(CustomerAcct pCustomerAcct, Route pCustomerRoute, RoutingAlgorithmType pRoutingAlgorithm, string pDestNumber) {
			List<CarrierRoute> _carrierRoutes;
			if (pRoutingAlgorithm == RoutingAlgorithmType.LCR) {
				//TODO: put number of LCR routes into config file
				const int _numberOfLCRRoutes = 2;
				_carrierRoutes = getLCRList(_numberOfLCRRoutes, pDestNumber);
			}
			else if (pRoutingAlgorithm == RoutingAlgorithmType.Manual) {
				_carrierRoutes = getManual(pCustomerAcct.RoutingPlanId, pCustomerRoute.BaseRouteId, 1);
			}
			else if (pRoutingAlgorithm == RoutingAlgorithmType.LoadBalance) {
				_carrierRoutes = getLoadBalancing(pCustomerAcct.RoutingPlanId, pCustomerRoute.BaseRouteId);
			}
			else {
				throw new RbrException(RbrResult.Routing_AlgorithmUnknown, "RoutingService.getCarrierRoutes", string.Format("Unknown Alghorithm={0}", pRoutingAlgorithm));
			}
			return _carrierRoutes;
		}

		List<CarrierRoute> getLCRList(int pNumberOfLCRRoutes, string pDialedNumber) {
			CarrierRouteRow[] _carrierRouteRows;
			using (var _db = new Rbr_Db()) {
				_carrierRouteRows = _db.CarrierRouteCollection.GetActive(pDialedNumber);
				if (_carrierRouteRows == null || _carrierRouteRows.Length == 0) {
					throw new RbrException(RbrResult.Carrier_NoRoutesFound, "RoutingService.GetLCR", string.Format("DialedNumber={0}", pDialedNumber));
				}
			}

			var _sortedCarrierRouteList = sort(_carrierRouteRows);
			return reorder(pNumberOfLCRRoutes, _sortedCarrierRouteList);
		}

		List<CarrierRoute> getManual(int pRoutingPlanId, int pRouteId, byte pPriority) {
			CarrierRouteRow _carrierRouteRow;
			using (var _db = new Rbr_Db()) {
				_carrierRouteRow = _db.CarrierRouteCollection.GetByRoutingPlanIdRouteIdPriority(pRoutingPlanId, pRouteId, pPriority);
				if (_carrierRouteRow == null) {
					throw new RbrException(RbrResult.Carrier_RouteNotFound, "RoutingService.GetManual", string.Format("CarrierRoute NOT FOUND, RoutingPlanId={0}, RouteId={1}, Priority={2}", pRoutingPlanId, pRouteId, pPriority));
				}
			}
			TimokLogger.Instance.LogRbr(LogSeverity.Debug, "RoutingService.GetManual", string.Format("CarrierRoute FOUND, CarrierAcctId={0}, CarrierRouteId={1}", _carrierRouteRow.Carrier_acct_id, _carrierRouteRow.Carrier_route_id));
			return new List<CarrierRoute> { new CarrierRoute(_carrierRouteRow) };
		}

		List<CarrierRoute> getLoadBalancing(int pRoutingPlanId, int pRouteId) {
			CarrierRouteRow[] _carrierRouteRows;
			using (var _db = new Rbr_Db()) {
				_carrierRouteRows = _db.CarrierRouteCollection.GetByRoutingPlanIdRouteId(pRoutingPlanId, pRouteId);
				if (_carrierRouteRows == null) {
					throw new RbrException(RbrResult.Carrier_RouteNotFound, "RoutingService.GetLoadBalancing", string.Format("CarrierRoutes NOT FOUND(1), RoutingPlanId={0}, RouteId={1}", pRoutingPlanId, pRouteId));
				}
				if (_carrierRouteRows.Length == 0) {
					throw new RbrException(RbrResult.Carrier_RouteNotFound, "RoutingService.GetLoadBalancing", string.Format("CarrierRoutes NOT FOUND(2), RoutingPlanId={0}, RouteId={1}", pRoutingPlanId, pRouteId));
				}
			}

			var _key = long.Parse(pRoutingPlanId.ToString() + pRouteId);
			if (loadBalancingTable.ContainsKey(_key)) {
				if ((loadBalancingTable[_key] + 1) >= _carrierRouteRows.Length) {
					loadBalancingTable[_key] = 0;
				}
				else {
					loadBalancingTable[_key] += 1;
				}
			}
			else {
				loadBalancingTable.Add(_key, 0);
			}

			var _carrierRouteRow = _carrierRouteRows[loadBalancingTable[_key]];

			TimokLogger.Instance.LogRbr(LogSeverity.Debug, "RoutingService.GetLoadBalancing", string.Format("CarrierRoute FOUND, CarrierAcctId={0}, CarrierRouteId={1}", _carrierRouteRow.Carrier_acct_id, _carrierRouteRow.Carrier_route_id));
			return new List<CarrierRoute> { new CarrierRoute(_carrierRouteRow) };
		}

		string rewriteDestNumber(CustomerAcct pCustomerAcct, string pIntlDialCode, string pCarrierPrefixOut, bool pStrip1Plus, string pDestNumber) {
			TimokLogger.Instance.LogRbr(LogSeverity.Debug, "RoutingService.RewritteDestNumber", string.Format("Args: [{0}][{1}][{2}][{3}][{4}]", pDestNumber, pIntlDialCode, pCustomerAcct.PrefixOut, pCarrierPrefixOut, pStrip1Plus));

			if (pDestNumber[0] == '0') {
				throw new RbrException(RbrResult.DialedNumber_Invalid, "RoutingService.RewriteDestNumber", string.Format("Unexpected zero at the begining: {0}", pDestNumber));
			}

			//-- if TermEP requires 'Intl dial code':
			if (pDestNumber[0] != '1' && pIntlDialCode.Length > 0) {
				pDestNumber = pDestNumber.Insert(0, pIntlDialCode);
			}

			if (pDestNumber[0] == '1' && pStrip1Plus) {
				pDestNumber = pDestNumber.Remove(0, 1);
			}

			//-- if configured, terminationEP prefix overwrites origination outgoing prefix:
			TimokLogger.Instance.LogRbr(LogSeverity.Debug, "RoutingService.RewritteDestNumber", string.Format("No NumberPortability for: {0}", pIntlDialCode));
			var _outgoingPrefix = pCarrierPrefixOut.Length > 0 ? pCarrierPrefixOut : pCustomerAcct.PrefixOut;
			if (_outgoingPrefix.Length > 0) {
				pDestNumber = pDestNumber.Insert(0, _outgoingPrefix);
			}

			TimokLogger.Instance.LogRbr(LogSeverity.Debug, "RoutingService.RewritteDestNumber", string.Format("DestNumber Out: {0}", pDestNumber));
			return pDestNumber;
		}

		List<CarrierRoute> reorder(int pNumberOfChoices, IDictionary<int, List<CarrierRoute>> pSortedCarrierRouteList) {
			var _carrierRouteList = new List<CarrierRoute>();
			foreach (var _carrierList in pSortedCarrierRouteList.Values) {
				foreach (var _carrierRoute in _carrierList) {
					_carrierRouteList.Add(_carrierRoute);
					if (_carrierRouteList.Count == pNumberOfChoices) {
						TimokLogger.Instance.LogRbr(LogSeverity.Debug, "RoutingService.reorder", string.Format("Returning {0} ordered Routes", pNumberOfChoices));
						return _carrierRouteList;
					}
				}
			}
			return _carrierRouteList;
		}

		SortedList<int, List<CarrierRoute>> sort(IEnumerable<CarrierRouteRow> pCarrierRouteRows) {
			var _sortedCarrierRouteList = new SortedList<int, List<CarrierRoute>>();
			foreach (var _carrierRouteRow in pCarrierRouteRows) {
				var _carrierRoute = new CarrierRoute(_carrierRouteRow);
				var _costRank = _carrierRoute.GetLCRCostRank();
				if (!_sortedCarrierRouteList.ContainsKey(_costRank)) {
					_sortedCarrierRouteList.Add(_costRank, new List<CarrierRoute>());
				}
				_sortedCarrierRouteList[_costRank].Add(_carrierRoute);
			}

			if (_sortedCarrierRouteList.Count == 0) {
				throw new RbrException(RbrResult.Carrier_RouteNotFound, "RoutingService.sort", "NOT Ranked Routes");
			}
			return _sortedCarrierRouteList;
		}
	}
}
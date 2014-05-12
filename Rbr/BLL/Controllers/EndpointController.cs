using System.Collections.Generic;
using Timok.Rbr.BLL.Entities;
using Timok.Rbr.BLL.Managers;
using Timok.Rbr.Core;
using Timok.Rbr.Core.Config;
using Timok.Rbr.DAL.RbrDatabase;
using Timok.Rbr.DTO;

namespace Timok.Rbr.BLL.Controllers {
	public class EndpointController {
		#region Public Getters

		public static EndPointRow[] GetEndpoints() {
			using (var _db = new Rbr_Db()) {
				return _db.EndPointCollection.GetAll();
			}
		}

		public static EndPointRow GetEndpoint(short pEndPointId) {
			using (var _db = new Rbr_Db()) {
				return EndpointManager.Get(_db, pEndPointId);
			}
		}

		public static EndPointRow GetEndpoint(string pAlias) {
			using (var _db = new Rbr_Db()) {
				return _db.EndPointCollection.GetByAlias(pAlias);
			}
		}

		public static EndPointRow GetEndpoint(int pIPAddress) {
			using (var _db = new Rbr_Db()) {
				return _db.EndPointCollection.GetByIPAddress(pIPAddress);
			}
		}

		//cust access list (will return Frontend list if Customer is retail)
		public static EndPointRow[] GetEndpointsByCustomerAcct(short pCustomerAcctId) {
			using (var _db = new Rbr_Db()) {
				return EndpointManager.GetAllByCustomerAcct(_db, pCustomerAcctId);
			}
		}

		//in use carr map by carrier
		public static EndPointRow[] GetEndpointsByCarrierAcct(short pCarrierAcctId) {
			using (var _db = new Rbr_Db()) {
				return _db.EndPointCollection.GetByCarrierAcctId(pCarrierAcctId, new[] {Status.Active, Status.Pending});
			}
		}

		//in use carr map by route
		public static EndPointRow[] GetEndpointsByCarrierAcctAndCarrierRoute(short pCarrierAcctId, int pCarrierRouteId) {
			using (var _db = new Rbr_Db()) {
				return _db.EndPointCollection.GetByCarrierAcctIdCarrierRouteId(pCarrierAcctId, pCarrierRouteId, new[] {Status.Active, Status.Pending});
			}
		}

		//in use carr map by country
		public static EndPointRow[] GetEndpointsByCarrierAcctAndCountry(short pCarrierAcctId, int pCountryId) {
			using (var _db = new Rbr_Db()) {
				return _db.EndPointCollection.GetByCarrierAcctIdCountryId(pCarrierAcctId, pCountryId, new[] {Status.Active, Status.Pending});
			}
		}

		//Unassigned
		public static EndPointRow[] GetUnassignedEndpoints(bool pExcludeMiltiIPAddressEndpoints) {
			using (var _db = new Rbr_Db()) {
				return
					_db.EndPointCollection.GetUnassignedByEndPointProtocol(pExcludeMiltiIPAddressEndpoints,
					                                                       new[] {EndPointProtocol.H323, EndPointProtocol.SIP},
					                                                       new[] {Status.Active, Status.Pending});
			}
		}

		//assigned to Partner, but not shared between Partners
		public static EndPointRow[] GetAvailableEndpointsFilteredByCarrierRoute(short pExcludeCarrierAcctId, int pExcludeCarrierRouteId) {
			using (var _db = new Rbr_Db()) {
				return
					_db.EndPointCollection.GetByEndPointProtocolExcludingSelectedCarrierRoute(pExcludeCarrierAcctId,
					                                                                          pExcludeCarrierRouteId,
					                                                                          new[] {EndPointProtocol.H323, EndPointProtocol.SIP},
					                                                                          new[] {Status.Active, Status.Pending});
			}
		}

		//assigned to Partner, but not shared between Partners
		public static EndPointRow[] GetAvailableEndpointsFilteredByCountry(short pExcludeCarrierAcctId, int pExcludeCountryId) {
			using (var _db = new Rbr_Db()) {
				return
					_db.EndPointCollection.GetByEndPointProtocolExcludingSelectedCarrierCountry(pExcludeCarrierAcctId,
					                                                                            pExcludeCountryId,
					                                                                            new[] {EndPointProtocol.H323, EndPointProtocol.SIP},
					                                                                            new[] {Status.Active, Status.Pending});
			}
		}

		//cust Available access list
		//NOTE: do not show eps from term that are used by diff partner
		public static EndPointRow[] GetEndpointsFilteredByCustomerAcct(short pCustomerAcctId) {
			using (var _db = new Rbr_Db()) {
				return
					_db.EndPointCollection.GetAvailableByPartnerIdExcludeCustomerAcctId(pCustomerAcctId,
					                                                                    new[] {EndPointProtocol.H323, EndPointProtocol.SIP},
					                                                                    new[] {Status.Active, Status.Pending});
			}
		}

		public static EndpointDto[] GetEndpointDtosByCustomerAcct(short pCustomerAcctId) {
			var _endpoints = new List<EndpointDto>();

			EndPointRow[] _endpointRows = GetEndpointsByCustomerAcct(pCustomerAcctId);
			if (_endpointRows != null && _endpointRows.Length > 0) {
				foreach (EndPointRow _endpointRow in _endpointRows) {
					var _endpoint = new EndpointDto(_endpointRow.End_point_id, _endpointRow.DottedIPAddressRange);
					_endpoints.Add(_endpoint);
				}
			}
			return _endpoints.ToArray();
		}

		public static AccessListViewRow[] GetAccessList() {
			using (var _db = new Rbr_Db()) {
				return EndpointManager.GetAll(_db);
			}
		}

		#endregion Public Getters

		#region Public Actions

		public static void AddEndpoint(EndPointRow pEndPointRow, EndpointContext pEndpointContext) {
			using (var _db = new Rbr_Db()) {
				using (var _tx = new Transaction(_db, pEndPointRow, pEndpointContext)) {
					//TODO: NEW DAL - VirtualSwitch
					pEndPointRow.Virtual_switch_id = AppConstants.DefaultVirtualSwitchId;

					EndpointManager.Add(_db, pEndPointRow, pEndpointContext);
					//CarrierAcctManager.AddDialPeer(_db, pEndPointRow, pEndpointContext);
					if (pEndpointContext.CarrierAcctEPMapRowToAdd != null && pEndpointContext.CarrierAcctEPMapRowToAdd.Length > 0) {
						foreach (var _carrierAcctEPMapRow in pEndpointContext.CarrierAcctEPMapRowToAdd) {
							_carrierAcctEPMapRow.End_point_id = pEndPointRow.End_point_id;
							CarrierAcctManager.AddDialPeer(_db, _carrierAcctEPMapRow, pEndPointRow);
						}
					}

					if (pEndpointContext.CustomerAcct != null) {
						if (pEndpointContext.CustomerAcct.ServiceDto.AccessNumbers != null && pEndpointContext.CustomerAcct.ServiceDto.AccessNumbers.Length > 0) {
							foreach (var _accessNumber in pEndpointContext.CustomerAcct.ServiceDto.AccessNumbers) {
								var _newDialPeer = new DialPeerRow
								                   	{
								                   		End_point_id = pEndPointRow.End_point_id, 
																			Prefix_in = _accessNumber.Number.ToString(), 
																			Customer_acct_id = pEndpointContext.CustomerAcct.CustomerAcctId
								                   	};
								CustomerAcctManager.AddDialPeer(_db, _newDialPeer, pEndPointRow);
							}
						}
						else {
							var _newDialPeer = new DialPeerRow
							                   	{
							                   		End_point_id = pEndPointRow.End_point_id, 
																		Prefix_in = pEndpointContext.CustomerAcct.PrefixIn, 
																		Customer_acct_id = pEndpointContext.CustomerAcct.CustomerAcctId
							                   	};
							CustomerAcctManager.AddDialPeer(_db, _newDialPeer, pEndPointRow);
						}
					}

					_tx.Commit();
				}
			}
		}

		public static void UpdateEndpoint(EndPointRow pEndPointRow, EndpointContext pEndpointContext) {
			//pEndpointContext.TransactionType = TxType.Delete;
			using (var _db = new Rbr_Db()) {
				using (var _tx = new Transaction(_db, pEndPointRow, pEndpointContext)) {
					var _original = EndpointManager.Get(_db, pEndPointRow.End_point_id);
					EndpointManager.Update(_db, _original, pEndPointRow);

					if (pEndpointContext.CarrierAcctEPMapRowToDelete != null) {
						foreach (var _carrierAcctEPMapRow in pEndpointContext.CarrierAcctEPMapRowToDelete) {
							CarrierAcctManager.DeleteDialPeer(_db, _carrierAcctEPMapRow, pEndPointRow);
						}
					}

					if (pEndpointContext.CarrierAcctEPMapRowToAdd != null) {
						foreach (var _carrierAcctEPMapRow in pEndpointContext.CarrierAcctEPMapRowToAdd) {
							if (_carrierAcctEPMapRow.Carrier_acct_EP_map_id <= 0) {
								_carrierAcctEPMapRow.End_point_id = pEndPointRow.End_point_id;
								CarrierAcctManager.AddDialPeer(_db, _carrierAcctEPMapRow, pEndPointRow);
							}
						}
					}
					_tx.Commit();
				}
			}
		}

		public static void DeleteEndpoint(EndPointRow pEndPointRow) {
			using (var _db = new Rbr_Db()) {
				using (var _tx = new Transaction(_db, pEndPointRow)) {
					CustomerAcctManager.DeleteDialPeersByEndpointId(_db, pEndPointRow.End_point_id);

					//- Delete Carrier Dialpeers 
					var _dialPeers = _db.CarrierAcctEPMapCollection.GetByEnd_point_id(pEndPointRow.End_point_id);
					if (_dialPeers != null && _dialPeers.Length > 0) {
						foreach (var _carrierAcctEPMapRow in _dialPeers) {
							CarrierAcctManager.DeleteDialPeer(_db, _carrierAcctEPMapRow, pEndPointRow);
						}
					}

					EndpointManager.Delete(_db, pEndPointRow);
					_tx.Commit();
				}
			}
		}

		#endregion Public Actions
	}
}
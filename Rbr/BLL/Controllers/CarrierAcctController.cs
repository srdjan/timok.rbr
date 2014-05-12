using System.Collections;
using Timok.Rbr.BLL.Managers;
using Timok.Rbr.Core;
using Timok.Rbr.DAL.RbrDatabase;
using Timok.Rbr.DTO;

namespace Timok.Rbr.BLL.Controllers {
	public class CarrierAcctController {
		CarrierAcctController() {}

		#region Public Getters

		public static bool IsNameInUseByOtherAcct(string pName, short pCarrierAcctId) {
			using (Rbr_Db _db = new Rbr_Db()) {
				return CarrierAcctManager.IsNameInUseByOtherCarrierAcct(_db, pName, pCarrierAcctId);
			}
		}

		public static int GetUsageCount(int pCallingPlanId) {
			using (Rbr_Db _db = new Rbr_Db()) {
				return CarrierAcctManager.GetUsageCount(_db, pCallingPlanId);
			}
		}

		public static CarrierAcctDto GetAcct(short pCarrierAcctId) {
			using (Rbr_Db _db = new Rbr_Db()) {
				return CarrierAcctManager.GetAcct(_db, pCarrierAcctId);
			}
		}

		public static CarrierAcctDto[] GetAccts() {
			using (Rbr_Db _db = new Rbr_Db()) {
				return CarrierAcctManager.GetAccts(_db);
			}
		}

		public static CarrierAcctDto[] GetAccts(int pPartnerId) {
			using (Rbr_Db _db = new Rbr_Db()) {
				return CarrierAcctManager.GetAccts(_db, pPartnerId);
			}
		}

		public static CarrierAcctEPMapRow GetDialPeer(int pCarrierAcctEPMapId) {
			using (Rbr_Db _db = new Rbr_Db()) {
				return CarrierAcctManager.GetDialPeer(_db, pCarrierAcctEPMapId);
			}
		}

		public static CarrierAcctEPMapRow GetDialPeer(short pCarrierActId, short pEndPointId, int pCarrierRouteId) {
			using (Rbr_Db _db = new Rbr_Db()) {
				return CarrierAcctManager.GetDialPeer(_db, pCarrierActId, pEndPointId, pCarrierRouteId);
			}
		}

		public static CarrierAcctEPMapRow[] GetDialPeers(short pEndPointId) {
			using (Rbr_Db _db = new Rbr_Db()) {
				return CarrierAcctManager.GetDialPeers(_db, pEndPointId);
			}
		}

		public static CarrierAcctEPMapRow[] GetDialPeers(int pCarrierRouteId) {
			using (Rbr_Db _db = new Rbr_Db()) {
				return CarrierAcctManager.GetDialPeers(_db, pCarrierRouteId);
			}
		}
		
		public static IList GetAllOutboundANIEntries(int pCarrierRouteId) {
			using (Rbr_Db _db = new Rbr_Db()) {
				return CarrierAcctManager.GetAllOutboundANIEntries(_db, pCarrierRouteId);
			}
		}

		#endregion Public Getters

		#region Public Actions

		public static void AddAcct(CarrierAcctDto pCarrierAcct, int[] pSelectedBaseRouteIds) {
			using (Rbr_Db _db = new Rbr_Db()) {
				using (Transaction _tx = new Transaction(_db, pCarrierAcct, pSelectedBaseRouteIds)) {
					CarrierAcctManager.AddCarrierAcct(_db, pCarrierAcct);

					//Create Default Route
					int _defaultCarrierRouteId;
					CarrierRouteManager.Add(_db, pCarrierAcct, 0, out _defaultCarrierRouteId);
					pCarrierAcct.DefaultRoute.RatedRouteId = _defaultCarrierRouteId;
					CarrierRouteManager.Add(_db, pCarrierAcct, pSelectedBaseRouteIds);

					_tx.Commit();
				}
			}
		}

		public static void UpdateAcct(CarrierAcctDto pCarrierAcct) {
			using (var _db = new Rbr_Db()) {
				using (var _tx = new Transaction(_db, pCarrierAcct)) {
					CarrierAcctManager.UpdateCarrierAcct(_db, pCarrierAcct);
					_tx.Commit();
				}
			}
		}

		public static void AddDialPeerByRoute(CarrierAcctDto pCarrierAcct, EndPointRow pEndPointRow, RatedRouteDto pCarrierRoute) {
			using (var _db = new Rbr_Db()) {
				using (var _tx = new Transaction(_db, pCarrierAcct, pEndPointRow, pCarrierRoute)) {
					var _carrierAcctEPMapRow = new CarrierAcctEPMapRow
					                           	{
					                           		Carrier_acct_id = pCarrierAcct.CarrierAcctId, 
																				Carrier_route_id = pCarrierRoute.RatedRouteId, 
																				End_point_id = pEndPointRow.End_point_id, 
																				Priority = 0
					                           	};
					CarrierAcctManager.AddDialPeer(_db, _carrierAcctEPMapRow, pEndPointRow);
					_tx.Commit();
				}
			}
		}

		public static void AddDialPeerByCountry(CarrierAcctDto pCarrierAcct, EndPointRow pEndPointRow, CountryDto pCountry) {
			using (var _db = new Rbr_Db()) {
				using (var _tx = new Transaction(_db, pCarrierAcct, pEndPointRow, pCountry)) {
					var _countryCarrierRoutes = CarrierAcctManager.GetByCarrierAcctIdCountryId(_db, pCarrierAcct.CarrierAcctId, pCountry.CountryId);
					var _list = new ArrayList();
					foreach (var _carrierRouteRow in _countryCarrierRoutes) {
						var _carrierAcctEPMapRow = new CarrierAcctEPMapRow
						                           	{
						                           		Carrier_acct_id = pCarrierAcct.CarrierAcctId, 
																					Carrier_route_id = _carrierRouteRow.Carrier_route_id, 
																					End_point_id = pEndPointRow.End_point_id, 
																					Priority = 0
						                           	};

						if (!_list.Contains(_carrierAcctEPMapRow)) {
							_list.Add(_carrierAcctEPMapRow);
						}
					}

					if (_list.Count > 0) {
						var _carrierAcctEPMapRows = (CarrierAcctEPMapRow[])_list.ToArray(typeof(CarrierAcctEPMapRow));
						foreach (var _carrierAcctEPMapRow in _carrierAcctEPMapRows) {
							CarrierAcctManager.AddDialPeer(_db, _carrierAcctEPMapRow, pEndPointRow);
						}
					}

					_tx.Commit();
				}
			}
		}

		public static void DeleteDialPeerByRoute(CarrierAcctDto pCarrierAcct, EndPointRow pEndPointRow, RatedRouteDto pCarrierRoute) {
			using (var _db = new Rbr_Db()) {
				using (var _tx = new Transaction(_db, pCarrierAcct, pEndPointRow, pCarrierRoute)) {
					var _carrierAcctEPMapRow = CarrierAcctManager.GetDialPeer(_db, pCarrierAcct.CarrierAcctId, pEndPointRow.End_point_id, pCarrierRoute.RatedRouteId);
					CarrierAcctManager.DeleteDialPeer(_db, _carrierAcctEPMapRow, pEndPointRow);
					_tx.Commit();
				}
			}
		}

		public static void DeleteDialPeerByCountry(CarrierAcctDto pCarrierAcct, EndPointRow pEndPointRow, CountryDto pCountry) {
			using (var _db = new Rbr_Db()) {
				using (var _tx = new Transaction(_db, pCarrierAcct, pEndPointRow, pCountry)) {
					//CarrierAcctManager.DeleteDialPeerByCountry(_db, pCarrierAcct, pCountry, pEndPointRow);
					var _countryCarrierRoutes = CarrierAcctManager.GetByCarrierAcctIdCountryId(_db, pCarrierAcct.CarrierAcctId, pCountry.CountryId);
					var _carrierAcctEPMapRows = CarrierAcctManager.GetByCountry(_db, _countryCarrierRoutes, pCarrierAcct.CarrierAcctId, pEndPointRow.End_point_id);
					foreach (var _carrierAcctEPMapRow in _carrierAcctEPMapRows) {
						CarrierAcctManager.DeleteDialPeer(_db, _carrierAcctEPMapRow, pEndPointRow);
					}
					_tx.Commit();
				}
			}
		}

		public static void AddOutboundANI(OutboundANIRow pOutboundANIRow) {
			using (Rbr_Db _db = new Rbr_Db()) {
				using (Transaction _tx = new Transaction(_db, pOutboundANIRow)) {
					CarrierAcctManager.AddOutboundANI(_db, pOutboundANIRow);
					_tx.Commit();
				}
			}
		}

		public static void DeleteOutboundANIEntries(OutboundANIRow[] pOutboundANIRows) {
			using (Rbr_Db _db = new Rbr_Db()) {
				using (Transaction _tx = new Transaction(_db, pOutboundANIRows)) {
					CarrierAcctManager.DeleteOutboundANIEntries(_db, pOutboundANIRows);
					_tx.Commit();
				}
			}
		}

		#endregion Public Actions
	}
}
using System;
using System.Collections;
using Timok.Logger;
using Timok.Rbr.Core.Config;
using Timok.Rbr.DAL.RbrDatabase;
using Timok.Rbr.DTO;

namespace Timok.Rbr.BLL.Managers {
	internal class CarrierAcctManager {
		CarrierAcctManager() { }

		#region Getters

		public static CarrierAcctEPMapRow[] GetByCountry(Rbr_Db pDb, CarrierRouteRow[] pCountryCarrierRouteRows, short pCarrierAcctId, short pEndpointId) {
			var _list = new ArrayList();
			foreach (var _carrierRouteRow in pCountryCarrierRouteRows) {
				var _carrierAcctEPMapRow = GetDialPeer(pDb, pCarrierAcctId, pEndpointId, _carrierRouteRow.Carrier_route_id);
				if (_carrierAcctEPMapRow != null) {
					if (!_list.Contains(_carrierAcctEPMapRow)) {
						_list.Add(_carrierAcctEPMapRow);
					}
				}
			}
			return (CarrierAcctEPMapRow[]) _list.ToArray(typeof (CarrierAcctEPMapRow));
		}

		public static CarrierRouteRow[] GetByCarrierAcctIdCountryId(Rbr_Db pDb, short pCarrierAcctId, int pCountryId) { return pDb.CarrierRouteCollection.GetByCarrierAcctIdCountryId(pCarrierAcctId, pCountryId); }

		internal static bool IsNameInUseByOtherCarrierAcct(Rbr_Db pDb, string pName, short pCarrierAcctId) { return pDb.CarrierAcctCollection.IsNameInUseByOtherCarrierAcct(pName, pCarrierAcctId); }

		internal static int GetUsageCount(Rbr_Db pDb, int pCallingPlanId) { return pDb.CarrierAcctCollection.GetCountByCallingPlanId(pCallingPlanId); }

		internal static bool Exist(Rbr_Db pDb, int pPartnerId) {
			var _carrierAcctRows = pDb.CarrierAcctCollection.GetByPartner_id(pPartnerId);
			return _carrierAcctRows != null && _carrierAcctRows.Length > 0;
		}

		internal static CarrierAcctDto GetAcct(Rbr_Db pDb, short pCarrierAcctId) {
			var _carrierAcctRow = pDb.CarrierAcctCollection.GetByPrimaryKey(pCarrierAcctId);
			return getAcct(pDb, _carrierAcctRow);
		}

		internal static CarrierAcctDto[] GetAccts(Rbr_Db pDb) {
			var _list = new ArrayList();
			var _carrierAcctRows = pDb.CarrierAcctCollection.GetAll();
			foreach (var _carrierAcctRow in _carrierAcctRows) {
				_list.Add(getAcct(pDb, _carrierAcctRow));
			}
			return (CarrierAcctDto[]) _list.ToArray(typeof (CarrierAcctDto));
		}

		internal static CarrierAcctDto[] GetAccts(Rbr_Db pDb, int pPartnerId) {
			var _list = new ArrayList();
			var _carrierAcctRows = pDb.CarrierAcctCollection.GetByPartner_id(pPartnerId);
			foreach (var _carrierAcctRow in _carrierAcctRows) {
				_list.Add(getAcct(pDb, _carrierAcctRow));
			}
			return (CarrierAcctDto[]) _list.ToArray(typeof (CarrierAcctDto));
		}

		internal static CarrierAcctEPMapRow GetDialPeer(Rbr_Db pDb, int pCarrierAcctEPMapId) { return pDb.CarrierAcctEPMapCollection.GetByPrimaryKey(pCarrierAcctEPMapId); }

		internal static CarrierAcctEPMapRow GetDialPeer(Rbr_Db pDb, short pCarrierAcctId, short pEndPointId, int pCarrierRouteId) { return pDb.CarrierAcctEPMapCollection.GetByCarrierAcctIDEndPointIDCarrierRouteID(pCarrierAcctId, pEndPointId, pCarrierRouteId); }

		internal static CarrierAcctEPMapRow[] GetDialPeers(Rbr_Db pDb, short pEndPointId) { return pDb.CarrierAcctEPMapCollection.GetByEnd_point_id(pEndPointId); }

		internal static CarrierAcctEPMapRow[] GetDialPeers(Rbr_Db pDb, int pCarrierRouteId) { return pDb.CarrierAcctEPMapCollection.GetByCarrier_route_id(pCarrierRouteId); }

		internal static CarrierAcctEPMapRow[] GetDialPeersWhereUsedLast(Rbr_Db pDb, short pEndPointId) { return pDb.CarrierAcctEPMapCollection.GetWhereEndpointIsUsedLast(pEndPointId); }

		#endregion Getters

		#region Actions

		//-------------------- Carrier Acct --------------------------------------------
		internal static void AddCarrierAcct(Rbr_Db pDb, CarrierAcctDto pCarrierAcct) {
			var _carrierAcctRow = mapToCarrierAcctRow(pCarrierAcct);
			pDb.CarrierAcctCollection.Insert(_carrierAcctRow);
			pCarrierAcct.CarrierAcctId = _carrierAcctRow.Carrier_acct_id;
		}

		internal static void UpdateCarrierAcct(Rbr_Db pDb, CarrierAcctDto pCarrierAcct) {
			if (pCarrierAcct.IsRatingEnabled) {
				//Update RatingInfo for CarrierAcct's DefaltRoute
				//NOTE: DefaultRatingInfo is always created on Add, no metter is RatingEnabled or not
				RatingManager.UpdateRatingInfo(pDb, pCarrierAcct.DefaultRatingInfo);

				//check RatingInfo for all others Routes, if they have no Rates - create it using DefaultrateInfo, if Route already have Rates, just leave it as is
				var _carrierRouteRows = pDb.CarrierRouteCollection.GetByCarrier_acct_id(pCarrierAcct.CarrierAcctId);
				foreach (var _carrierRouteRow in _carrierRouteRows) {
					var _carrierRateHistoryRows = pDb.CarrierRateHistoryCollection.GetByCarrier_route_id(_carrierRouteRow.Carrier_route_id);
					if (_carrierRateHistoryRows.Length == 0) {
						RatingManager.AddDefaultRatingInfo(pDb, _carrierRouteRow.Carrier_route_id, pCarrierAcct.DefaultRatingInfo, RouteType.Carrier);
					}
				}
			}
			pDb.CarrierAcctCollection.Update(mapToCarrierAcctRow(pCarrierAcct));
			//pDb.AddChangedObject(new CarrierAcctKey(TxType.Delete, pCarrierAcct.CarrierAcctId));
		}

		//---------------- Carrier DialPeers ------------------------------------------------------------
		internal static void AddDialPeer(Rbr_Db pDb, CarrierAcctEPMapRow pCarrierAcctEPMapRow, EndPointRow pEndPointRow) {
			pDb.CarrierAcctEPMapCollection.Insert(pCarrierAcctEPMapRow);
			//pDb.AddChangedObject(new EndpointKey(TxType.Add, pCarrierAcctEPMapRow.End_point_id));
		}

		internal static void DeleteDialPeer(Rbr_Db pDb, CarrierAcctEPMapRow pCarrierAcctEPMapRow, EndPointRow pEndPointRow) {
			pDb.CarrierAcctEPMapCollection.Delete(pCarrierAcctEPMapRow);
			//pDb.AddChangedObject(new CarrierDialPeerKey(TxType.Delete, pCarrierAcctEPMapRow.Carrier_acct_id, pCarrierAcctEPMapRow.Carrier_route_id));
		}

		#endregion Actions

		#region Mappings

		static CarrierAcctDto mapToCarrierAcct(CarrierAcctRow pCarrierAcctRow, PartnerDto pPartner, CallingPlanDto pCallingPlan) {
			if (pCarrierAcctRow == null) {
				return null;
			}
			if (pPartner == null) {
				return null;
			}
			if (pCallingPlan == null) {
				return null;
			}

			var _carrierAcct = new CarrierAcctDto
			                   	{
			                   		CarrierAcctId = pCarrierAcctRow.Carrier_acct_id, 
														IntlDialCode = pCarrierAcctRow.Intl_dial_code, 
														Name = pCarrierAcctRow.Name, 
														Strip1Plus = pCarrierAcctRow.Strip1plus, 
														PrefixOut = pCarrierAcctRow.Prefix_out, 
														Status = pCarrierAcctRow.AccountStatus, 
														RatingType = pCarrierAcctRow.RatingType, 
														MaxCallLength = pCarrierAcctRow.MaxCallLength, 
														Partner = pPartner, 
														CallingPlan = pCallingPlan
			                   	};

			return _carrierAcct;
		}

		static CarrierAcctRow mapToCarrierAcctRow(CarrierAcctDto pCarrierAcct) {
			if (pCarrierAcct == null) {
				return null;
			}

			var _carrierAcctRow = new CarrierAcctRow();
			_carrierAcctRow.Carrier_acct_id = pCarrierAcct.CarrierAcctId;
			_carrierAcctRow.Intl_dial_code = pCarrierAcct.IntlDialCode;
			_carrierAcctRow.Name = pCarrierAcct.Name;
			_carrierAcctRow.Strip1plus = pCarrierAcct.Strip1Plus;
			_carrierAcctRow.Prefix_out = pCarrierAcct.PrefixOut;
			_carrierAcctRow.AccountStatus = pCarrierAcct.Status;
			_carrierAcctRow.RatingType = pCarrierAcct.RatingType;
			_carrierAcctRow.MaxCallLength = pCarrierAcct.MaxCallLength;

			_carrierAcctRow.Partner_id = pCarrierAcct.PartnerId;
			_carrierAcctRow.Calling_plan_id = pCarrierAcct.CallingPlan.CallingPlanId;

			return _carrierAcctRow;
		}

		#endregion Mappings

		//-------------------- Private ----------------------------------------------

		static CarrierAcctDto getAcct(Rbr_Db pDb, CarrierAcctRow pCarrierAcctRow) {
			if (pCarrierAcctRow == null) {
				return null;
			}
			var _partner = PartnerManager.Get(pDb, pCarrierAcctRow.Partner_id);
			if (_partner == null) {
				return null;
			}
			var _callingPlan = CallingPlanManager.GetCallingPlan(pDb, pCarrierAcctRow.Calling_plan_id);
			if (_callingPlan == null) {
				return null;
			}
			var _carrierAcct = mapToCarrierAcct(pCarrierAcctRow, _partner, _callingPlan);
			_carrierAcct.DefaultRoute = CarrierRouteManager.GetDefaultRoute(pDb, _carrierAcct, RouteState.Valid);

			//-- GetDefaultCarrierRatingInfo
			if (_carrierAcct.IsRatingEnabled) {
				var _defaultCarrierRouteRow = pDb.CarrierRouteCollection.GetByPrimaryKey(-pCarrierAcctRow.Carrier_acct_id);
				var _carrierRateHistoryRows = pDb.CarrierRateHistoryCollection.GetByCarrier_route_id(_defaultCarrierRouteRow.Carrier_route_id);
				if (_carrierRateHistoryRows.Length < 1) {
					TimokLogger.Instance.LogRbr(LogSeverity.Critical, "CarrierAcctManager.GetDefaultCarrierRatingInfo", string.Format("Unexpected: _carrierRateHistoryRows.Length = {0}", _carrierRateHistoryRows.Length));
					throw new Exception(string.Format("CarrierAcctManager.GetDefaultCarrierRatingInfo, Unexpected: _carrierRateHistoryRows.Length = {0}", _carrierRateHistoryRows.Length));
				}
				_carrierAcct.DefaultRatingInfo = RatingManager.GetRatingInfo(pDb, _carrierRateHistoryRows[0].Rate_info_id, false);
			}

			return _carrierAcct;
		}

		public static void AddOutboundANI(Rbr_Db pDb, OutboundANIRow pOutboundANIRow) { pDb.OutboundANICollection.Insert(pOutboundANIRow); }

		public static void DeleteOutboundANIEntries(Rbr_Db pDb, OutboundANIRow[] pOutboundANIRows) {
			foreach (OutboundANIRow _outboundANIRow in pOutboundANIRows) {
				pDb.OutboundANICollection.Delete(_outboundANIRow);
			}
		}

		public static IList GetAllOutboundANIEntries(Rbr_Db pDb, int pCarrierRouteId) { return pDb.OutboundANICollection.GetByCarrier_route_id(pCarrierRouteId); }
	}
}
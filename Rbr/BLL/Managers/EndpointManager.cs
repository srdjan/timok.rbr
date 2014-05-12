using System;
using Timok.Rbr.BLL.Entities;
using Timok.Rbr.Core.Config;
using Timok.Rbr.DAL.RbrDatabase;

namespace Timok.Rbr.BLL.Managers {
	public class EndpointManager {
		EndpointManager() { }

		#region Geters

		public static EndPointRow[] GetAllByCustomerAcct(Rbr_Db pDb, short pCustomerAcctId) { return pDb.EndPointCollection.GetByCustomerAcctId(pCustomerAcctId, new[] {Status.Active, Status.Pending}); }

		internal static EndPointRow Get(Rbr_Db pDb, short pEndpointId) { return pDb.EndPointCollection.GetByPrimaryKey(pEndpointId); }

		internal static AccessListViewRow[] GetAll(Rbr_Db pDb) { return pDb.AccessListViewCollection.GetAll(); }

		#endregion

		#region Actions

		internal static void Add(Rbr_Db pDb, EndPointRow pEndPointRow, EndpointContext pEndpointContext) {
			pDb.EndPointCollection.Insert(pEndPointRow);
			addIPAddressCollection(pDb, pEndPointRow);
			//pDb.AddChangedObject(new EndpointKey(TxType.Add, pEndPointRow.Ip_address_range));
		}

		internal static void Update(Rbr_Db pDb, EndPointRow pOriginalEnpointRow, EndPointRow pNewEndpointRow) {
			if (pNewEndpointRow.IPAddressChanged(pOriginalEnpointRow)) {
				updateIPAddress(pDb, pNewEndpointRow);
			}
			pDb.EndPointCollection.Update(pNewEndpointRow);

			//pDb.AddChangedObject(new EndpointKey(TxType.Delete, pOriginalEnpointRow.Ip_address_range));
			//pDb.AddChangedObject(new EndpointKey(TxType.Add, pNewEndpointRow.Ip_address_range));
		}

		internal static void UpdatePrefixType(Rbr_Db pDb, EndPointRow pEndpointRow, short pNewPrefixTypeId) {
			if (pEndpointRow == null) {
				throw new Exception("Invalid operation: Endpoint should not be NULL.");
			}

			bool _endpointHasDialPeers = CustomerAcctManager.GetDialPeerCountByEndpointId(pDb, pEndpointRow.End_point_id) > 0;
			if (_endpointHasDialPeers) {
				throw new Exception("Invalid operation: expecting Endpoint without DialPeers.");
			}

			//pDb.AddChangedObject(new EndpointKey(TxType.Delete, pEndpointRow.Ip_address_range));

			pEndpointRow.Prefix_in_type_id = pNewPrefixTypeId;
			pDb.EndPointCollection.Update(pEndpointRow);

			//pDb.AddChangedObject(new EndpointKey(TxType.Add, pEndpointRow.Ip_address_range));
		}

		internal static void Delete(Rbr_Db pDb, EndPointRow pEndPointRow) {
			if (pEndPointRow == null) {
				return;
			}
			pDb.IPAddressCollection.DeleteByEnd_point_id(pEndPointRow.End_point_id);
			pDb.EndPointCollection.Delete(pEndPointRow);

			//pDb.AddChangedObject(new EndpointKey(TxType.Delete, pEndPointRow.Ip_address_range));
		}

		#endregion Actions

		#region privates

		static void updateIPAddress(Rbr_Db pDb, EndPointRow pEndPointRow) {
			pDb.IPAddressCollection.DeleteByEnd_point_id(pEndPointRow.End_point_id);
			addIPAddressCollection(pDb, pEndPointRow);
		}

		static void addIPAddressCollection(Rbr_Db pDb, EndPointRow pEndPointRow) {
			if (pEndPointRow == null || pEndPointRow.Ip_address_range == null) {
				throw new ApplicationException("At least one IPAddress should be passed");
			}
			foreach (int _ip in pEndPointRow.IPAddressRange.IPAddressInt32List) {
				var _ipAddressRow = new IPAddressRow();
				_ipAddressRow.IP_address = _ip;
				_ipAddressRow.End_point_id = pEndPointRow.End_point_id;
				pDb.IPAddressCollection.Insert(_ipAddressRow);
			}
		}

		//private static string getFrontendAlias(EndPointRow pEndPointRow) {
		//  return "FRONTEND_" + pEndPointRow.End_point_id;
		//}

		#endregion privates
	}
}
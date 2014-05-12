using System;
using System.Collections.Generic;
using Timok.Rbr.Core.Config;
using Timok.Rbr.DAL.RbrDatabase;

namespace Timok.Rbr.DOM {
	public sealed class CarrierDialPeer {
		IConfiguration configuration;
		readonly CarrierAcctEPMapRow dialPeerRow;

		public short CarrierId { get { return dialPeerRow.Carrier_acct_id; } }
		public short TermEPId { get { return dialPeerRow.End_point_id; } }
		public int CarrierRouteId { get { return dialPeerRow.Carrier_route_id; } }

		//-- Mandatory default Constructor:
		public CarrierDialPeer() { }

		public CarrierDialPeer(CarrierAcctEPMapRow pDialPeerRow, IConfiguration pConfiguration) {
			if (pDialPeerRow == null) {
				throw new ArgumentNullException("");
			}
			dialPeerRow = pDialPeerRow;
			configuration = pConfiguration;
		}

		//--------------------------------- Static ---------------------------------
		public static CarrierDialPeer[] GetAll(IConfiguration pConfiguration) {
			var _carrierDialPeers = new List<CarrierDialPeer>();

			using (var _db = new Rbr_Db()) {
				var _carrierDialPeerRows =  _db.CarrierAcctEPMapCollection.GetAll();
				foreach (var _carrierAcctEPMapRow in _carrierDialPeerRows) {
					_carrierDialPeers.Add(new CarrierDialPeer(_carrierAcctEPMapRow,  pConfiguration));		
				}
			}
			return _carrierDialPeers.ToArray();
		}
	}
}
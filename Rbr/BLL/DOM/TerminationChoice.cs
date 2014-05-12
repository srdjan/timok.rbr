using System;
using Timok.Rbr.Core;
using Timok.Rbr.DAL.RbrDatabase;

namespace Timok.Rbr.DOM {
	public sealed class TerminationChoice {
		readonly TerminationChoiceRow terminationChoiceRow;

		readonly short carrierAcctId;
		public short CarrierAcctId { get { return carrierAcctId; } }
		public int CarrierRouteId { get { return terminationChoiceRow.Carrier_route_id; } }
		//public int RouteId { get { return terminationChoiceRow.Route_id; } }
		public short Priority { get { return terminationChoiceRow.Priority; } }

		public TerminationChoice(TerminationChoiceRow pTermChoiceRow) {
			if (pTermChoiceRow == null) {
				throw new Exception("TerminationChoice.Ctor, TerminationChoiceRow == null");
			}
			terminationChoiceRow = pTermChoiceRow;

			using (Rbr_Db _db = new Rbr_Db()) {
				var _carrierRouteRow = _db.CarrierRouteCollection.GetByPrimaryKey(terminationChoiceRow.Carrier_route_id);
				if (_carrierRouteRow == null) {
					throw new RbrException(RbrResult.Carrier_NotFound, "TerminationChoice.Ctor", string.Format("CarrierRoute NOT FOUND, TerminationChoiceId={0}, CarrierRouteId={1}", terminationChoiceRow.Termination_choice_id, terminationChoiceRow.Carrier_route_id));
				}
				carrierAcctId = _carrierRouteRow.Carrier_acct_id;
			}
		}
	}
}
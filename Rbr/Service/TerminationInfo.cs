using Timok.Rbr.BLL.DOM;
using Timok.Rbr.DOM;

namespace Timok.Rbr.Service {
	internal class TerminationInfo {
		public CarrierAcct TermCarrier { get; private set; }
		public CarrierRoute TermRoute { get; private set; }
		public Endpoint TermEP { get; private set; }

		public TerminationInfo(CarrierAcct pCarrierAcct, CarrierRoute pCarrierRoute, Endpoint pTermEP) {
			TermCarrier = pCarrierAcct;
			TermRoute = pCarrierRoute;
			TermEP = pTermEP;
		}

		public override string ToString() {
			return string.Format("CarrierAcctId={0}, CarrierRoute={1}, TermEP={2}", TermCarrier.Id, TermRoute.Name, TermEP.IPAddress);
		}
	}
}
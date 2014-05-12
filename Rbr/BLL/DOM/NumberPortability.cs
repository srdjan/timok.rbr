using Timok.Rbr.BLL.DOM.Repositories;

namespace Timok.Rbr.BLL.DOM {
	public class NumberPortability : Persistable {
		public string TelephoneNumber { get; set; }
		public string RoutingNumber { get; set; }

		public NumberPortability(string pTelephoneNumber, string pRoutingNumber) {
			TelephoneNumber = pTelephoneNumber;
			RoutingNumber = pRoutingNumber;
		}

		public void Dispose() {
			//TODO: base.Deallocate();
		}
	}
}
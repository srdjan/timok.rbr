using System;

namespace Timok.Rbr.BLL.DOM {
	public class InventoryUsage {
		static public void Scan(DateTime pDateTime) {
			foreach (var _customerAcct in CustomerAcct.GetRetailAccounts()) {
				_customerAcct.SaveInventoryStats(pDateTime);
			}
		}
	}
}
using Timok.Rbr.BLL.Managers;
using Timok.Rbr.Core;
using Timok.Rbr.Core.Config;
using Timok.Rbr.DAL.RbrDatabase;
using Timok.Rbr.DTO;

namespace Timok.Rbr.BLL.Controllers {
	public class LoadBalancingMapController : ControllerBase {

		public LoadBalancingMapController(IConfiguration pConfiguration) : base(pConfiguration) {}

		#region Public Getters

		//public LoadBalancingMapViewRow[] GetAllViews() {
		//  using (var _db = new Rbr_Db()) {
		//    return LoadBalancingMapManager.GetAllViews(_db);
		//  }
		//}

		public static LoadBalancingMapViewRow[] GetAllViews(short pNodeId) {
			using (var _db = new Rbr_Db()) {
				return LoadBalancingMapManager.GetAllViews(_db, pNodeId);
			}
		}

		public LoadBalancingMapViewRow[] GetAllViewsByCustomerAcctId(short pCustomerAcctId) {
			using (var _db = new Rbr_Db()) {
				return LoadBalancingMapManager.GetAllViews(_db, pCustomerAcctId);
			}
		}

		//public static LoadBalancingMapViewRow GetView(short pNodeId, short pCustomerAcctId) {
		//  using (Rbr_Db _db = new Rbr_Db()) {
		//    return LoadBalancingMapManager.GetView(_db, pNodeId, pCustomerAcctId);
		//  }
		//}

		//public static LoadBalancingMapRow Get(short pNodeId, short pCustomerAcctId) {
		//  using (Rbr_Db _db = new Rbr_Db()) {
		//    return LoadBalancingMapManager.Get(_db, pNodeId, pCustomerAcctId);
		//  }
		//}

		public static LoadBalancingMapRow[] GetAll(short pNodeId) {
			using (var _db = new Rbr_Db()) {
				return LoadBalancingMapManager.GetAll(_db, pNodeId);
			}
		}

		//public static LoadBalancingMapRow[] GetAllByCustomerAcctId(short pCustomerAcctId) {
		//  using (Rbr_Db _db = new Rbr_Db()) {
		//    return LoadBalancingMapManager.GetAll(_db, pCustomerAcctId);
		//  }
		//}

		#endregion Public Getters

		#region Public Actions

		public static void Add(LoadBalancingMapRow pLoadBalancingMapRow) {
			using (Rbr_Db _db = new Rbr_Db()) {
				using (var _tx = new Transaction(_db, pLoadBalancingMapRow)) {
					LoadBalancingMapManager.Add(_db, pLoadBalancingMapRow);
					_tx.Commit();
				}
			}
		}

		public static void Update(LoadBalancingMapRow pLoadBalancingMapRow) {
			using (Rbr_Db _db = new Rbr_Db()) {
				using (var _tx = new Transaction(_db, pLoadBalancingMapRow)) {
					LoadBalancingMapManager.Update(_db, pLoadBalancingMapRow);
					_tx.Commit();
				}
			}
		}

		public static void Delete(NodeViewRow pNodeViewRow, CustomerAcctDto pCustomerAcct) {
			using (Rbr_Db _db = new Rbr_Db()) {
				using (Transaction _tx = new Transaction(_db, pNodeViewRow, pCustomerAcct)) {
					LoadBalancingMapRow _loadBalancingMapRow = LoadBalancingMapManager.Get(_db, pNodeViewRow.Node_id, pCustomerAcct.CustomerAcctId);
					LoadBalancingMapManager.Delete(_db, _loadBalancingMapRow);
					_tx.Commit();
				}
			}
		}

		public static void DeleteByCustomerAcct(CustomerAcctDto pCustomerAcct) {
			using (Rbr_Db _db = new Rbr_Db()) {
				using (Transaction _tx = new Transaction(_db, pCustomerAcct)) {
					LoadBalancingMapManager.Delete(_db, pCustomerAcct);
					_tx.Commit();
				}
			}
		}

		#endregion Public Actions
	}
}
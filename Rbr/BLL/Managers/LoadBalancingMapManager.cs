using Timok.Rbr.DAL.RbrDatabase;
using Timok.Rbr.DTO;

namespace Timok.Rbr.BLL.Managers {
	internal class LoadBalancingMapManager {
		LoadBalancingMapManager() { }

		#region Internals

		#region Getters

		internal static LoadBalancingMapViewRow[] GetAllViews(Rbr_Db pDb) { return pDb.LoadBalancingMapViewCollection.GetAll(); }

		internal static LoadBalancingMapViewRow[] GetAllViews(Rbr_Db pDb, short pNodeId) { return pDb.LoadBalancingMapViewCollection.GetByNodeId(pNodeId); }

		internal static LoadBalancingMapViewRow[] GetAllViewsByCustomerAcctId(Rbr_Db pDb, short pCustomerAcctId) { return pDb.LoadBalancingMapViewCollection.GetByCustomer_acct_id(pCustomerAcctId); }

		internal static LoadBalancingMapViewRow GetView(Rbr_Db pDb, short pNodeId, short pCustomerAcctId) { return pDb.LoadBalancingMapViewCollection.GetByPrimaryKey(pNodeId, pCustomerAcctId); }

		internal static LoadBalancingMapRow Get(Rbr_Db pDb, short pNodeId, short pCustomerAcctId) { return pDb.LoadBalancingMapCollection.GetByPrimaryKey(pNodeId, pCustomerAcctId); }

		internal static LoadBalancingMapRow[] GetAll(Rbr_Db pDb, short pNodeId) { return pDb.LoadBalancingMapCollection.GetByNode_id(pNodeId); }

		internal static LoadBalancingMapRow[] GetAllByCustomerAcctId(Rbr_Db pDb, short pCustomerAcctId) { return pDb.LoadBalancingMapCollection.GetByCustomer_acct_id(pCustomerAcctId); }

		#endregion Getters

		#region Actions

		internal static void Add(Rbr_Db pDb, LoadBalancingMapRow pLoadBalancingMapRow) { add(pDb, pLoadBalancingMapRow); }

		internal static void Add(Rbr_Db pDb, short pNodeId, short pCustomerAcctId) {
			LoadBalancingMapRow _loadBalancingMapRow = pDb.LoadBalancingMapCollection.GetByPrimaryKey(pNodeId, pCustomerAcctId);
			if (_loadBalancingMapRow == null) {
				_loadBalancingMapRow = new LoadBalancingMapRow();
				_loadBalancingMapRow.Node_id = pNodeId;
				_loadBalancingMapRow.Customer_acct_id = pCustomerAcctId;
				_loadBalancingMapRow.Max_calls = int.MaxValue;
				_loadBalancingMapRow.Current_calls = 0;
				add(pDb, _loadBalancingMapRow);
			}
		}

		internal static void Update(Rbr_Db pDb, LoadBalancingMapRow pLoadBalancingMapRow) {
			pLoadBalancingMapRow.Current_calls = 0;
			pDb.LoadBalancingMapCollection.Update(pLoadBalancingMapRow);
			//pDb.AddChangedObject(new CustomerAcctKey(TxType.Delete, pLoadBalancingMapRow.Customer_acct_id));
		}

		internal static void Delete(Rbr_Db pDb, LoadBalancingMapRow pLoadBalancingMapRow) {
			delete(pDb, pLoadBalancingMapRow);
			//pDb.AddChangedObject(new CustomerAcctKey(TxType.Delete, pLoadBalancingMapRow.Customer_acct_id));
		}

		internal static void Delete(Rbr_Db pDb, CustomerAcctDto pCustomerAcct) {
			delete(pDb, pCustomerAcct);
			//pDb.AddChangedObject(new CustomerAcctKey(TxType.Delete, pCustomerAcct.CustomerAcctId));
		}

		internal static void Delete(Rbr_Db pDb, NodeRow pNodeRow) { delete(pDb, pNodeRow); }

		#endregion Actions

		#endregion Internals

		#region privates

		static void add(Rbr_Db pDb, LoadBalancingMapRow pLoadBalancingMapRow) { pDb.LoadBalancingMapCollection.Insert(pLoadBalancingMapRow); }

		static void delete(Rbr_Db pDb, LoadBalancingMapRow pLoadBalancingMapRow) { pDb.LoadBalancingMapCollection.Delete(pLoadBalancingMapRow); }

		static void delete(Rbr_Db pDb, CustomerAcctDto pCustomerAcct) { pDb.LoadBalancingMapCollection.DeleteByCustomer_acct_id(pCustomerAcct.CustomerAcctId); }

		static void delete(Rbr_Db pDb, NodeRow pNodeRow) { pDb.LoadBalancingMapCollection.DeleteByNode_id(pNodeRow.Node_id); }

		#endregion privates
	}
}
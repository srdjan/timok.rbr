using Timok.Rbr.BLL.Managers;
using Timok.Rbr.Core;
using Timok.Rbr.Core.Config;
using Timok.Rbr.DAL.RbrDatabase;
using Timok.Rbr.DTO;

namespace Timok.Rbr.BLL.Controllers {
	public class ServiceController {
		ServiceController() {}

		#region Public Getters

		public static ServiceDto[] GetAll() {
			using (var _db = new Rbr_Db()) {
				return ServiceManager.GetServices(_db);
			}
		}

		public static ServiceDto[] GetAll(ServiceType pServiceType) {
			using (var _db = new Rbr_Db()) {
				return ServiceManager.GetServices(_db, pServiceType);
			}
		}

		public static ServiceDto[] GetShared() {
			using (Rbr_Db _db = new Rbr_Db()) {
				return ServiceManager.GetSharedServices(_db);
			}
		}

		public static ServiceDto[] GetShared(int pRoutingPlanId) {
			using (Rbr_Db _db = new Rbr_Db()) {
				return ServiceManager.GetSharedServices(_db, pRoutingPlanId);
			}
		}

		public static ServiceDto[] GetShared(RetailType pRetailType) {
			using (Rbr_Db _db = new Rbr_Db()) {
				return ServiceManager.GetSharedServices(_db, pRetailType);
			}
		}

		public static ServiceDto Get(short pServiceId) {
			using (Rbr_Db _db = new Rbr_Db()) {
				return ServiceManager.GetService(_db, pServiceId);
			}
		}

		public static int GetServiceUsageCount(short pServiceId) {
			using (Rbr_Db _db = new Rbr_Db()) {
				return CustomerAcctManager.GetAcctUsageCount(_db, pServiceId);
			}
		}
		#endregion Public Getters

		#region Public Actions

		public static void Add(ServiceDto pService, int[] pSelectedBaseRouteIds) {
			using (Rbr_Db _db = new Rbr_Db()) {
				using (Transaction _tx = new Transaction(_db, pService, pSelectedBaseRouteIds)) {
					ServiceManager.AddService(_db, pService, pSelectedBaseRouteIds);
					_tx.Commit();
				}
			}
		}

		public static void Update(ServiceDto pService) {
			using (Rbr_Db _db = new Rbr_Db()) {
				using (Transaction _tx = new Transaction(_db, pService)) {
					ServiceRow _originalServiceRow = ServiceManager.Get(_db, pService.ServiceId);
					ServiceManager.UpdateService(_db, pService);

					//TODO: NEW DAL - !!! check the logic
					if (pService.IsShared && pService.DefaultRoutingPlanId != _originalServiceRow.Default_routing_plan_id) {
						//NOTE: RoutingPlan changed - set same RoutingPlan for Customers using this Shared Service, 
						//BUT ONLY for those Customers that did NOT overwrote the RoutingPlan
						CustomerAcctRow[] _customerAcctRows = _db.CustomerAcctCollection.GetByService_id(pService.ServiceId);
						foreach (CustomerAcctRow _customerAcctRow in _customerAcctRows) {
							if (_customerAcctRow.Routing_plan_id == _originalServiceRow.Default_routing_plan_id) {
								_customerAcctRow.Routing_plan_id = pService.DefaultRoutingPlanId;
								CustomerAcctManager.UpdateAcct(_db, _customerAcctRow);
							}
						}
					}
					_tx.Commit();
				}
			}
		}

		public static void Delete(ServiceDto pService) {
			using (Rbr_Db _db = new Rbr_Db()) {
				using (Transaction _tx = new Transaction(_db, pService)) {
					ServiceManager.DeleteService(_db, pService);
					_tx.Commit();
				}
			}
		}

		#endregion Public Actions
	}
}
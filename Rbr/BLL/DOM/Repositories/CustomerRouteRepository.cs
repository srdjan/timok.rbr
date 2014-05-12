using System;
using Timok.Core.Logging;
using Timok.Rbr.DAL.RbrDatabase;
using T = Timok.Core.Logger.TimokLogger;

namespace Timok.Rbr.BLL.DOM.Repositories {
	internal sealed class CustomerRouteImdbRepository : RepositoryBase {
		static readonly object padlock = new object();

		public CustomerRouteImdbRepository() : base("CustomerRoute", typeof (long), true) {}

		public CustomerRoute[] GetAll() {
			return (CustomerRoute[]) pk.ToArray(typeof (CustomerRoute));
		}

		public CustomerRoute Get(short pServiceId, int pBaseRouteId) {
			CustomerRoute _route;
			long _key = getKey(pServiceId, pBaseRouteId);

			lock (padlock) {
				try {
					_route = pk.Get(_key) as CustomerRoute;
					if (_route == null) {
						//T.LogRbr(LogSeverity.Debug, "CustomerRouteImdbRepository.Get", string.Format("NOT FOUND in Imdb, {0}, {1}", pServiceId, pBaseRouteId));

						//-- try get one from DAL repository:
						_route = getCustomerRoute(pServiceId, pBaseRouteId);
						if (_route != null) {
							pk.Put(_key, _route);
							//T.LogRbr(LogSeverity.Status, "CustomerRouteImdbRepository.Get", string.Format("Added to Imdb, {0}, {1}", pServiceId, pBaseRouteId));
						}
						else {
							//T.LogRbr(LogSeverity.Error, "CustomerRouteImdbRepository.Get", string.Format("NOT FOUND in db, {0}, {1}", pServiceId, pBaseRouteId));
						}
					}
				}
				catch (Exception _ex) {
					//T.LogRbr(LogSeverity.Error, "CustomerRouteImdbRepository.Get", string.Format("Exception:\r\n{0}", _ex));
					_route = null;
				}
			}
			return _route;
		}

		public bool Remove(short pServiceId, int pBaseRouteId) {
			long _key = getKey(pServiceId, pBaseRouteId);

			lock (padlock) {
				var _route = pk.Get(_key) as CustomerRoute;
				if (_route != null) {
					pk.Remove(_key);
					_route.Deallocate();
				//	T.LogRbr(LogSeverity.Debug, "CustomerRouteImdbRepository.Remove", string.Format("REMOVED CustomerRoute from Imdb: {0}, {1}", pServiceId, pBaseRouteId));
					return true;
				}
				//T.LogRbr(LogSeverity.Debug, "CustomerRouteImdbRepository.Remove", string.Format("CustomerRoute NOT in Imdb: {0}, {1}", pServiceId, pBaseRouteId));
				return false;
			}
		}

		//-------------------------------------------- Private -----------------------------------
		CustomerRoute getCustomerRoute(short pServiceId, int pBaseRouteId) {
			CustomerRoute _customerRoute = null;

			using (Rbr_Db _db = new Rbr_Db()) {
				RouteRow _routeRow = _db.RouteCollection.GetByPrimaryKey(pBaseRouteId);
				if (_routeRow != null) {
					WholesaleRouteRow _wholesaleRouteRow = _db.WholesaleRouteCollection.GetByServiceIdBaseRouteId(pServiceId, pBaseRouteId);
					if (_wholesaleRouteRow != null) {
						_customerRoute = new CustomerRoute(_wholesaleRouteRow, _routeRow);
					}
					else {
						T.LogRbr(LogSeverity.Error, "CustomerRouteImdbRepository.getCustomerRoute", string.Format("ServiceRouteRow NOT FOUND: {0}, {1}", pServiceId, pBaseRouteId));
					}
				}
				else {
					T.LogRbr(LogSeverity.Error, "CustomerRouteImdbRepository.getCustomerRoute", string.Format("CustomerRouteRow NOT FOUND: {0}, {1}", pServiceId, pBaseRouteId));
				}
			}
			return _customerRoute;
		}

		long getKey(short pServiceId, int pBaseRouteId) {
			string _keyStr = pServiceId.ToString() + pBaseRouteId;
			return long.Parse(_keyStr);
		}
	}
}
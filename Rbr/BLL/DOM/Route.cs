using System;
using Timok.Rbr.Core.Config;
using Timok.Rbr.DAL.RbrDatabase;
using RatingType=Timok.Rbr.Core.Config.RatingType;

namespace Timok.Rbr.BLL.DOM {
	public abstract class Route {
		readonly int countryCode;
		protected short defaultPrimaryEPId;
		protected RatingType ratingType;
		protected RouteRow routeRow;

		protected int CallingPlanId { get { return routeRow.Calling_plan_id; } }
		public int BaseRouteId { get { return routeRow.Route_id; } }

		public Status Status { get { return routeRow.RouteStatus; } }
		public string Name { get { return routeRow.Name; } }
		public int CountryId { get { return routeRow.Country_id; } }

		public int CountryCode { get { return countryCode; } }

		//------------- Constructor -----------------------------------------------------------------
		protected Route() {}

		protected Route(RouteRow pRouteRow) {
			routeRow = pRouteRow;

			using (var _db = new Rbr_Db()) {
				var _countryRow = _db.CountryCollection.GetByPrimaryKey(routeRow.Country_id);
				if (_countryRow == null) {
					throw new Exception("Route.Ctor | CountryRow NOT FOUND: [routeId: " + routeRow.Route_id + "][countryId: " + routeRow.Country_id + "]");
				}
				countryCode = _countryRow.Country_code;
			}
		}
	}
}
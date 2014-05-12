using System;
using System.Collections;
using System.ComponentModel;

using Timok.Core;

namespace Timok.Rbr.DOM {
/*
	sealed internal class LCRSorter {
		//TODO: see if optimization possible
		public static CarrierRoute[] RankRoutes(bool pIsCallProcessing, SortedList pCarrierRouteList, DateTime pDateHour) {
			var _rankedRouteList = new ArrayList();
			foreach (CarrierRoute _carrierRoute in pCarrierRouteList.Values) {
				var _rankedRoute = new RankedRoute(_carrierRoute, pDateHour);
				if (_rankedRoute.LCRRank == int.MaxValue && pIsCallProcessing) {
					continue;
				}
				_rankedRouteList.Add(_rankedRoute);
			}
			
			var _sortInfo = new SortInfo[2];
			_sortInfo[0] = new SortInfo("LCRRank", ListSortDirection.Ascending);
			_sortInfo[1] = new SortInfo("Name", ListSortDirection.Ascending);
			
			IComparer _comparer = new GenericComparer(_sortInfo);
			_rankedRouteList.Sort(_comparer);

			var _carrierRoutes = new CarrierRoute[_rankedRouteList.Count];
			for (var _i = 0; _i < _carrierRoutes.Length; _i++) {
				_carrierRoutes[_i] = ((RankedRoute) _rankedRouteList[_i]).CarrierRoute;
			}
			return _carrierRoutes;
		}
	}
*/

	//----------------------- Private ---------------------------------------
	//sealed internal class RankedRoute {
	//  readonly CarrierRoute carrierRoute;
	//  public CarrierRoute CarrierRoute { get { return carrierRoute; } }


	//  //these two are used for sorting, they have to be as Property only
	//  readonly int lcrRank;
	//  public int LCRRank { get { return lcrRank; } }

	//  //TODO: name as second property for sorting?
	//  //public string Name { get { return carrierRoute.Name; } }

	//  public RankedRoute(CarrierRoute pCarrierRoute, DateTime pDateHour) {
	//    carrierRoute = pCarrierRoute;
	//    lcrRank = carrierRoute.GetLCRCostRank();
	//  }
	//}
}

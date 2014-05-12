using NUnit.Framework;
using Timok.Rbr.DOM;

namespace Timok.Rbr.Service.Test {
	[TestFixture]
	public class TestRoutingService {
		[Test]
		public void TestLCR() {
			var _customerAcct = CustomerAcct.Get(1180);
			var _customerRoute = CustomerRoute.Get(7248, 40, 258);
			var _legOutList = RoutingService.Instance.GetTerminationByDestination(_customerAcct, _customerRoute, "5535520312389", 10800);
			Assert.IsTrue(_legOutList.Count == 2);
		}
	}
}

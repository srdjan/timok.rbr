using NUnit.Framework;
using T = Timok.Core.Logging.Logger;

namespace Timok.Rbr.Service.Test {
	[TestFixture]
	public class TestBillingService {
	
		[Test]
		public void TestBalanceWarnings() {
			BillingService.Instance.CheckBalanceWarnings();
			Assert.IsTrue(true);
		}
	}
}
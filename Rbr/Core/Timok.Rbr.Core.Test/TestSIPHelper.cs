using NUnit.Framework;

namespace Timok.Rbr.Core.Test {
	[TestFixture]
	public class TestSIPHelper {
		[Test]
		public void TestGetIPAddress() {
			string _ip = SIPHelper.GetIPAddress("ip$192.168.1.1");
			Assert.IsTrue("192.168.1.1" == _ip);
		}
	}
}
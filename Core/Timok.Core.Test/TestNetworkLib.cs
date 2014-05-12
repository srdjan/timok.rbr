using System;
using System.Collections.Generic;
using System.Text;
using NUnit.Framework;
using Timok.Core.NetworkLib;

namespace Timok.Core.Test {
	[TestFixture]
	public class TestNetworkLib {

		[Test]
		public void TestIPConvertInt32ToString() {
			string _ipStr = IPUtil.ToString(-1763514680);
			Assert.AreEqual(_ipStr, "200.230.226.150");
		}
	}
}

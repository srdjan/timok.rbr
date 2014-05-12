using System;
using System.IO;
using System.Reflection;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Soap;

using NUnit.Framework;
using Timok.Core;
using Timok.Rbr.Core;
using Timok.Rbr.DAL.RbrDatabase;
using System.Diagnostics;

namespace Timok.Rbr.BLL.Test {

	[TestFixture]
	public class TestCdrAggregatePurge {
    public TestCdrAggregatePurge() { }

		[SetUp]
		public void Setup() {
			//	T.Init(Folders.LogFolder);
		}
		
		[Test]
    public void Test_Purge() {
      int _daysToKeep = 0;

      using (Rbr_Db _db = new Rbr_Db()) {
        //uncomment it for testing
        //int _res = _db.CdrAggregateCollection.Purge(_daysToKeep);
        //Debug.WriteLine("Rows deleted: " + _res);
      }

		}

		[TearDown]
		public void Teardown() { }
	}
}

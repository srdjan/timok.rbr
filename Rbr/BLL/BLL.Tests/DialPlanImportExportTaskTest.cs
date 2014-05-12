using MbUnit.Framework;
using Timok.Rbr.BLL.ImportExport;

namespace Timok.Rbr.BLL.Test {
	[TestFixture]
	public class DialPLanImportExportTaskTest {
		DialPlanImportExportTask dialPlanImportExportTask;
		[SetUp]
		public void Setup() {
			dialPlanImportExportTask = new DialPlanImportExportTask();
		}

		[Test]
		public void Test_Constructor() {
			Assert.IsNotNull(dialPlanImportExportTask);
		}

		[Test]
		public void Test_getImportExportArgs() {
			DialPlanImportExportArgs _args = dialPlanImportExportTask.getImportExportArgs(@"c:\_DialCodes\2007-05-05.0001223.Import.Rates.10001.10001.10001.CustomerAcct.pending");
			Assert.IsNotNull(_args);
			Assert.AreEqual(_args.ImportExportType, ImportExportType.Import);
			Assert.AreEqual(_args.ImportExportFilter, ImportExportFilter.Rates);
			Assert.AreEqual(_args.AccountId, 10001);
			Assert.AreEqual(_args.CallingPlanId, 10001);
			Assert.AreEqual(_args.RoutingPlanId, 10001);
			//TODO:			Assert.AreSame(_args.ViewContext, "CustomerAcct");
		}

		[TearDown]
		public void Teardown() { }
	}
}
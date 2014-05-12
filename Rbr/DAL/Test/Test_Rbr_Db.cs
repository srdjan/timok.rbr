//using System;
//using NUnit.Framework;

//using Timok.Core;
//using Timok.Rbr.Core;
//using Timok.Rbr.DAL.RbrDatabase;



//namespace Timok.Rbr.DAL.Test {

//  [TestFixture]
//  public class Test_Rbr_Db {
//    [SetUp]
//    public void Setup() {
//    }
		
//    public Test_Rbr_Db() {
//      //Timok.Core.Logging.TimokLogger.Init(Folders.LogFolder);
//    }

//    [Test]
//    public void Test_CreateEnumFilter() {
//      using (TestRbrDB _db = new TestRbrDB()) {
//        string _res = string.Empty;
//        _res = _db.Test_CreateEnumFilter("Country", "status", new object [] {Status.Active, Status.Blocked}, typeof(Status));
//        System.Diagnostics.Debug.WriteLine(_res);

//        _res = _db.Test_CreateEnumFilter("Country", "status", null, typeof(Status));
//        System.Diagnostics.Debug.WriteLine(_res);
			
//        _res = _db.Test_CreateEnumFilter("EndPoint", "protocol", new object [] { (byte) EndPointProtocol.SIP}, typeof(EndPointProtocol));
//        System.Diagnostics.Debug.WriteLine(_res);
			
//        #region to fail
//        //_res = _db.Test_CreateEnumFilter("protocol", new object [] { (byte) EndPointProtocol.SIP}, null);
//        //System.Diagnostics.Debug.WriteLine(_res);
//        #endregion to fail
//      }
//    }

//    [Test]
//    public void Test_GetActiveOriginationsByType_Registration() {
//      using (Rbr_Db _db = new Rbr_Db()) {
//        EndPointRow[] _endPointRows = _db.EndPointCollection.GetActiveOriginationsByType_Registration(
//          EndPointType.Gateway, 
//          EPRegistration.Required );
				
//        System.Diagnostics.Debug.WriteLine("\r\nResult:");
//        foreach (EndPointRow _endPointRow in _endPointRows) {
//          System.Diagnostics.Debug.WriteLine(_endPointRow);
//        }
//      }
//    }

//    [Test]
//    public void Test_GetActiveTerminationsByType_Registration() {
//      using (Rbr_Db _db = new Rbr_Db()) {
//        EndPointRow[] _endPointRows = _db.EndPointCollection.GetActiveTerminationsByType_Registration(
//          EndPointType.Gateway, 
//          EPRegistration.Required);

//        System.Diagnostics.Debug.WriteLine("\r\nResult:");
//        foreach (EndPointRow _endPointRow in _endPointRows) {
//          System.Diagnostics.Debug.WriteLine(_endPointRow);
//        }
//      }
//    }

//    [Test]
//    public void Test_GetByCarrierServiceIdDialedNumberAsArray() {
//      CarrierRoutesSet _temp = new CarrierRoutesSet();
//      Assert.IsTrue(_temp != null);
//      //using (Rbr_Db _db = new Rbr_Db()) {
//        //DialCodeRow[] _dialCodeRows = _db.DialCodeCollection.GetByCarrierServiceIdDialedNumberAsArray(
//        //  1, 
//        //  "180978889797");

//        //System.Diagnostics.Debug.WriteLine("\r\nResult:");
//        //foreach (DialCodeRow _dialCodeRow in _dialCodeRows) {
//        //  System.Diagnostics.Debug.WriteLine(_dialCodeRow);
//        //  System.Diagnostics.Debug.Flush();
//        //}
//        //System.Diagnostics.Debug.WriteLine("Finished:");
//        //System.Diagnostics.Debug.Flush();
//      //}
//    }
//  }

//  internal class TestRbrDB : Rbr_Db {
//    public TestRbrDB() { }

//    internal string Test_CreateEnumFilter(string pDbTableName, string pDbFieldName, object[] pEnumMembers, Type pEnumType) {
//      return this.CreateEnumFilter(pDbTableName, pDbFieldName, pEnumMembers, pEnumType);
//    }
//  }
//}

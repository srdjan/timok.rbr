//using System;
//using System.Collections.Generic;
//using System.Text;
//using System.Diagnostics;

//using NUnit.Framework;

//using Timok.Rbr.BLL;
//using Timok.Rbr.DOM;
//using Timok.Rbr.DTO;

//namespace Timok.Rbr.BLL.Test {
//  [TestFixture]
//  public class TestCarrierRateHistory {
//    [SetUp]
//    public void Setup() {
//      //	T.Init(Folders.LogFolder);
//    }

//    const int carrierRouteId = 10001;


//    [Test]
//    public void Test_AddCarrierRateHistory() {
//      Result _result = null;
//      CarrierRoute _carrierRoute = CarrierRouteController.Get(carrierRouteId);
//      //get existing history
//      CarrierRatingHistoryEntry[] _carrierRatingHistoryEntries = CarrierRouteController.GetCarrierRatingHistoryEntries(_carrierRoute.CarrierRouteId);
//      printList("Current rate history:", _carrierRatingHistoryEntries);

//      //clean existing history
//      _result = deleteHistory(_carrierRatingHistoryEntries);
//      Assert.IsTrue(_result.Success, _result.ErrorMessage);

//      //add 1-st one *************************************************************************
//      _result = add(_carrierRoute, new DateTime(2006, 12, 15), new DateTime(2006, 12, 25));
//      Assert.IsTrue(_result.Success, _result.ErrorMessage);
//      printList("Added 1-st", CarrierRouteController.GetCarrierRatingHistoryEntries(_carrierRoute.CarrierRouteId));

//      //add 2-nd good one *************************************************************************
//      _result = add(_carrierRoute, new DateTime(2006, 12, 26), new DateTime(2006, 12, 31));
//      Assert.IsTrue(_result.Success, _result.ErrorMessage);
//      printList("Added 2-nd", CarrierRouteController.GetCarrierRatingHistoryEntries(_carrierRoute.CarrierRouteId));


//      CarrierRatingHistoryEntry[] _gaps = CarrierRouteController.FindGaps(_carrierRoute.CarrierRouteId);
//      if (_gaps.Length > 0) {
//        printList("Found Gaps:", _gaps);
//        Assert.Fail("Found " + _gaps.Length + " gaps");
//      }

//      //add 3-rd good one*************************************************************************
//      _result = add(_carrierRoute, new DateTime(2007, 1, 1), new DateTime(2007, 1, 15));
//      Assert.IsTrue(_result.Success, _result.ErrorMessage);
//      printList("Added 3-rd", CarrierRouteController.GetCarrierRatingHistoryEntries(_carrierRoute.CarrierRouteId));

//      _gaps = CarrierRouteController.FindGaps(_carrierRoute.CarrierRouteId);
//      if (_gaps.Length > 0) {
//        printList("Found Gaps:", _gaps);
//        Assert.Fail("Found " + _gaps.Length + " gaps");
//      }
//    }

//    [Test]
//    public void Test_AddCarrierRateHistoryWithBadDateRange() {
//      CarrierRoute _carrierRoute = CarrierRouteController.Get(carrierRouteId);
//      //get existing history
//      CarrierRatingHistoryEntry[] _carrierRatingHistoryEntries = CarrierRouteController.GetCarrierRatingHistoryEntries(_carrierRoute.CarrierRouteId);
//      printList("Current rate history:", _carrierRatingHistoryEntries);

//      //add one with FirstDate grater the LastDate *************************************************************************
//      Result _result = null;
//      _result = add(_carrierRoute, new DateTime(2006, 12, 15), new DateTime(2006, 12, 14));
//      Assert.IsTrue(_result.Success, _result.ErrorMessage);
//    }

//    [Test]
//    public void Test_AddCarrierRateHistoryWithGap() {
//      CarrierRoute _carrierRoute = CarrierRouteController.Get(carrierRouteId);
//      //get existing history
//      CarrierRatingHistoryEntry[] _carrierRatingHistoryEntries = CarrierRouteController.GetCarrierRatingHistoryEntries(_carrierRoute.CarrierRouteId);
//      printList("Current rate history:", _carrierRatingHistoryEntries);

//      Result _result = null;
//      //clean existing history
//      _result = deleteHistory(_carrierRatingHistoryEntries);
//      Assert.IsTrue(_result.Success, _result.ErrorMessage);

//      //add 1-st one *************************************************************************
//      _result = add(_carrierRoute, new DateTime(2006, 12, 15), new DateTime(2006, 12, 25));
//      Assert.IsTrue(_result.Success, _result.ErrorMessage);
//      printList("Added 1-st", CarrierRouteController.GetCarrierRatingHistoryEntries(_carrierRoute.CarrierRouteId));

//      //add 2-nd good one *************************************************************************
//      _result = add(_carrierRoute, new DateTime(2006, 12, 26), new DateTime(2006, 12, 31));
//      Assert.IsTrue(_result.Success, _result.ErrorMessage);
//      printList("Added 2-nd", CarrierRouteController.GetCarrierRatingHistoryEntries(_carrierRoute.CarrierRouteId));


//      CarrierRatingHistoryEntry[] _gaps = CarrierRouteController.FindGaps(_carrierRoute.CarrierRouteId);
//      if (_gaps.Length > 0) {
//        printList("Found Gaps:", _gaps);
//        Assert.Fail("Found " + _gaps.Length + " gaps");
//      }

//      //add 3-rd one with a gap *************************************************************************
//      _result = add(_carrierRoute, new DateTime(2007, 1, 11), new DateTime(2007, 1, 15));
//      Assert.IsTrue(_result.Success, _result.ErrorMessage);
//      printList("Added 3-rd with a gap", CarrierRouteController.GetCarrierRatingHistoryEntries(_carrierRoute.CarrierRouteId));

//      _gaps = CarrierRouteController.FindGaps(_carrierRoute.CarrierRouteId);
//      if (_gaps.Length > 0) {
//        printList("Found Gaps:", _gaps);
//        Assert.Fail("Found " + _gaps.Length + " gaps");
//      }
//    }

//    [Test]
//    public void Test_UpdateCarrierRateHistory() {
//      CarrierRoute _carrierRoute = CarrierRouteController.Get(carrierRouteId);
//      CarrierRatingHistoryEntry[] _carrierRatingHistoryEntries = CarrierRouteController.GetCarrierRatingHistoryEntries(_carrierRoute.CarrierRouteId);
//      printList("Current rate history:", _carrierRatingHistoryEntries);

//      Result _result = null;
//      //clean existing history
//      _result = deleteHistory(_carrierRatingHistoryEntries);

//      //add 1-st one *************************************************************************
//      _result = add(_carrierRoute, new DateTime(2006, 12, 15), new DateTime(2006, 12, 25));
//      Assert.IsTrue(_result.Success, _result.ErrorMessage);
//      printList("Added 1-st", CarrierRouteController.GetCarrierRatingHistoryEntries(_carrierRoute.CarrierRouteId));

//      //add 2-nd good one *************************************************************************
//      _result = add(_carrierRoute, new DateTime(2006, 12, 26), new DateTime(2006, 12, 31));
//      Assert.IsTrue(_result.Success, _result.ErrorMessage);
//      printList("Added 2-nd", CarrierRouteController.GetCarrierRatingHistoryEntries(_carrierRoute.CarrierRouteId));

//      //add 3-rd good one *************************************************************************
//      _result = add(_carrierRoute, new DateTime(2007, 1, 1), new DateTime(2007, 1, 31));
//      Assert.IsTrue(_result.Success, _result.ErrorMessage);
//      printList("Added 2-nd", CarrierRouteController.GetCarrierRatingHistoryEntries(_carrierRoute.CarrierRouteId));

//      _carrierRatingHistoryEntries = CarrierRouteController.GetCarrierRatingHistoryEntries(_carrierRoute.CarrierRouteId);

//      _carrierRatingHistoryEntries[1].FirstDate = new DateTime(2006, 12, 27);
//      _result = RatingController.Save(_carrierRatingHistoryEntries[1]);
//      Assert.IsTrue(_result.Success, _result.ErrorMessage);
//      printList("Updated 2-nd produsing gap", CarrierRouteController.GetCarrierRatingHistoryEntries(_carrierRoute.CarrierRouteId));

//      CarrierRatingHistoryEntry[] _gaps = CarrierRouteController.FindGaps(_carrierRoute.CarrierRouteId);
//      if (_gaps.Length > 0) {
//        printList("Found Gaps:", _gaps);
//        Assert.Fail("Found " + _gaps.Length + " gaps");
//      }
//      //Debug.WriteLine("");
//    }

//    //[Test]
//    //public void Test_DeleteCarrierRateHistory() {
//    //  CarrierRoute _carrierRoute = CarrierRouteController.Get(carrierRouteId);
//    //  CarrierRatingHistoryEntry[] _carrierRatingHistoryEntries = CarrierRouteController.GetCarrierRatingHistoryEntries(_carrierRoute.CarrierRouteId);
//    //  Debug.WriteLine("Route " + _carrierRoute.CarrierRouteId + "   rate history:");
//    //  foreach (CarrierRatingHistoryEntry _carrierRatingHistoryEntry in _carrierRatingHistoryEntries) {
//    //    Debug.WriteLine(_carrierRatingHistoryEntry.FirstDate.ToString("MM/dd/yyyy") + " - " + _carrierRatingHistoryEntry.LastDate.ToString("MM/dd/yyyy"));        
//    //  }
//    //  Debug.WriteLine("");
//    //  //Debug.WriteLine("");
//    //}

//    private static Result add(CarrierRoute pCarrierRoute, DateTime pFirstDate, DateTime pLastDate) {
//      CarrierRatingHistoryEntry _carrierRatingHistoryEntry = new CarrierRatingHistoryEntry(
//        pCarrierRoute.CarrierRouteId, pFirstDate, pLastDate,
//        pCarrierRoute.CarrierAcct.DefaultRatingInfo.MakeClone());

//      return RatingController.Save(_carrierRatingHistoryEntry);
//    }

//    private Result deleteHistory(CarrierRatingHistoryEntry[] pCarrierRatingHistoryEntries) {
//      Result _result = new Result(true, null);
//      foreach (CarrierRatingHistoryEntry _carrierRatingHistoryEntry in pCarrierRatingHistoryEntries) {
//        _result = RatingController.Delete(_carrierRatingHistoryEntry);
//        if (!_result.Success) {
//          return _result;
//        }
//      }
//      return _result;
//    }

//    private void printList(string pPrintHeader, CarrierRatingHistoryEntry[] _carrierRatingHistoryEntries) {
//      Debug.WriteLine(pPrintHeader);
//      foreach (CarrierRatingHistoryEntry _carrierRatingHistoryEntry in _carrierRatingHistoryEntries) {
//        Debug.WriteLine(_carrierRatingHistoryEntry.FirstDate.ToString("MM/dd/yyyy") + " - " + _carrierRatingHistoryEntry.LastDate.ToString("MM/dd/yyyy"));
//      }
//      Debug.WriteLine("");
//    }

//  }
//}

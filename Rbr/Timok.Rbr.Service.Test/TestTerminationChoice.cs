//using System;
//using System.Diagnostics;
//using NUnit.Framework;
//
//using Timok.Core;
//using Timok.Rbr.Core;
//using Timok.Rbr.DOM;
//
//namespace Timok.Rbr.Engine {
//	[TestFixture]
//	public class TestTerminationChoice {
//		private static RBRDispatcher rbr;
//
//		static TestTerminationChoice() {
//			rbr = new RBRDispatcher();
//			Timok.Core.Logging.TimokLogger.Init(Folders.LogFolder);
//			rbr.Preload();
//		}
//
//		[Test]
//		public void TestCtor() {
//			bool _result = false;
//			TerminationChoice _termChoice = null;
//
//			try {
////				setRoutingAlgorithm(RoutingAlgorithmType.LCR, 2, 252);
////				_termChoice = new TerminationChoice(2, 252, 0);
////				_termChoice = new TerminationChoice(2, 252, 3);
////				_termChoice = new TerminationChoice(2, 252, 5);
////
////				setRoutingAlgorithm(RoutingAlgorithmType.Manual, 2, 252);
////				_termChoice = new TerminationChoice(2, 252, 0);
////				_termChoice = new TerminationChoice(2, 252, 3);
////				_termChoice = new TerminationChoice(2, 252, 5);
////
////				setRoutingAlgorithm(RoutingAlgorithmType.Load_Balance, 2, 252);
////				_termChoice = new TerminationChoice(2, 252, 0);
////				Debug.WriteLine("1:" + _termChoice.EP.TrunkGroupId);
////				_termChoice = new TerminationChoice(2, 252, 1);
////				Debug.WriteLine("2:" + _termChoice.EP.TrunkGroupId);
////				_termChoice = new TerminationChoice(2, 252, 2);
////				Debug.WriteLine("3:" + _termChoice.EP.TrunkGroupId);
////				_termChoice = new TerminationChoice(2, 252, 3);
////				Debug.WriteLine("4:" + _termChoice.EP.TrunkGroupId);
////				_termChoice = new TerminationChoice(2, 252, 4);
////				Debug.WriteLine("5:" + _termChoice.EP.TrunkGroupId);
////				_termChoice = new TerminationChoice(2, 252, 5);
////				Debug.WriteLine("6:" + _termChoice.EP.TrunkGroupId);
//				_result = true;
//			}
//			catch (Exception _ex) {
//				Debug.WriteLine(_ex.Message);
//				_result = false;
//			}
//			Assert.IsTrue(_result);
//
//		}
//		//----------------------------- Private ----------------------------------------
//		private void setRoutingAlgorithm(RoutingAlgorithmType pRoutingAlgorithm, short pRoutingPlanId, short pRouteId) {
////			RoutingPlanDetail _routingPlanDetail = RoutingPlanDetailRepository.Imdb.Get(pRoutingPlanId, pRouteId);
////			_routingPlanDetail.RoutingAlgorithm = pRoutingAlgorithm;
//		}
//	}
//}

using System;
using NUnit.Framework;
using Timok.Core;
using Timok.Core.NetworkLib.Udp;
using Timok.Rbr.Core;
using Timok.Rbr.Engine;
using Timok.Rbr.Server;

using T = Timok.Core.Logging.TimokLogger;

namespace Timok.Rbr.Server.Test
{
	[TestFixture]
	public class TestUdpServer {
		const string dateTimeFormat = "ddd, dd MMM yyyy HH:mm:ss";
		ICommandFactory commandFactory;
		
		[SetUp]
		public void Setup() {
			try {
				commandFactory = new RbrCommandFactory(RbrDispatcher.Instance);
			}
			catch (Exception _ex) {
				throw new Exception("Failed setup: " + _ex);
			}
		}
		
		[Test]
		public void TestAuthorizeIVR() {
			bool _result = dispatcher("cmd.000007:rbr.authorizeenduser.1(192.168.1.10>>2013138681>>6462744961>>noaccount>>>>19173716300>>02988A29-DF2F-11DA-9920-A09045FCDDFA)");
			Assert.AreEqual(true, _result);
		}

		[Test]
		public void TestProperties() {
			RbrServer _rbrServer = new RbrServer();

			Assert.AreEqual(_rbrServer.Role, NodeRole.IVR);
		}

		[Test]
		public void TestOnCallSetup() {
			bool _result = dispatcher("cmd.000012:rbr.oncallsetup.1(1>>192.168.1.10>>test_in_10:h323_ID>>?>>18097367890)");
			Assert.AreEqual(true, _result);

			_result = dispatcher("cmd.000001:rbr.oncallsetup.1(1>>192.168.1.10>>test_in_10:h323_ID>>9173716300>>12013138681)");
			Assert.AreEqual(true, _result);
	
			_result = dispatcher("cmd.000074:rbr.oncallsetup.1(1,192.168.1.10,test_in_10:h323_ID,,01159373301111)");
			Assert.AreEqual(true, _result);

			_result = dispatcher("cmd.000077:rbr.oncallsetup.1(1,192.168.1.10,test_in_10:h323_ID,,011525523456789)");
			Assert.AreEqual(true, _result);

			_result = dispatcher("cmd.000080:rbr.oncallsetup.1(1,192.168.1.10,test_in_10:h323_ID,,01152644444789)");
			Assert.AreEqual(true, _result);

			_result = dispatcher("cmd.000084:rbr.oncallsetup.1(1,192.168.1.10,test_in_10:h323_ID,,01154987688776)");
			Assert.AreEqual(true, _result);

			_result = dispatcher("cmd.000081:rbr.oncallsetup.1(1,192.168.1.10,test_in_10:h323_ID,,01152222234567)");
			Assert.AreEqual(true, _result);

			_result = dispatcher("cmd.000086:rbr.oncallsetup.1(1,192.168.1.10,test_in_10:h323_ID,,011525511111189)");
			Assert.AreEqual(true, _result);
		}

		[Test]
		public void TestOnCallSetupWithPrefixNotConnected() {
			bool _result = dispatcher("cmd.000074:rbr.oncallsetup.1(1,192.168.1.10,test_in_10:h323_ID,,22201159373301111)");
			Assert.AreEqual(false, _result);

			_result = dispatcher(@"cmd.000075:rbr.oncallcomplete.1(CDR|3|f4 56 ab 84 84 f5 18 10 8c 56 00 04 5a a2 9c 45|0|unconnected|Thu, 06 Apr 2006 22:14:20 -0400|192.168.1.10:3620|4060_endp|192.168.1.62:1720|4058_endp|22201159373301111:dialedDigits|test_in_10:h323_ID|DEMO-GK|0|0|16|0|0|16;)");
			Assert.AreEqual(true, _result);
		}

		[Test]
		public void TestOnCallConnect() {
			//Assert.AreEqual(true, _rc);
		}

		[Test]
		public void TestOnCallComplete() {
//			bool _result = dispatcher(@"cmd.000005:rbr.oncallcomplete.1(CDR|2|ed c1 e6 5c d3 6f 11 da 88 57 a0 90 45 fc dd fa|35|Mon, 24 Apr 2006 16:55:56 -0400|Mon, 24 Apr 2006 16:56:31-0400|192.168.1.10:38787|8819_endp|192.168.1.62:1720|8818_endp|12013138681:dialedDigits|9173716300:dialedDigits=ANIREC1:h323_ID|TRITEL-GK6|1002|7000|1900|624|16|1|0|16;)");			
//			Assert.AreEqual(true, _result);
      bool _result = dispatcher(@"cmd.000005:rbr.oncallcomplete.1(CDR|2|d5 28 63 d2 1a 1a 11 cc 82 93 e4 78 1f f7 85 89|8|Thu, 27 Jul 2006 05:36:15 GMT|Thu, 27 Jul 2006 05:36:23 GMT    |200.168.64.238:1720||201.30.72.137:1720|7666_endp|1001#551332733093:dialedDigits|1101|GTG-GK-7|1004|7074|4625|3176|16|0|0|16;)");
		//bool _result = dispatcher(@"cmd.000005:rbr.oncallcomplete.1(CDR|2|00 73 65 09 df 38 11 da 99 4a a0 90 45 fc dd fa|8|Tue, 09 May 2006 16:45:43 -0400|Tue, 09 May 2006 16:45:53 -0400|192.168.1.11:19430|7434_endp|192.168.1.62:1720|7433_endp|12018860696:dialedDigits|2013138681:dialedDigits=test_in_10:h323_ID|TRITEL-GK6|1010|7038|974|630|16|0|0|16;)");
			Assert.AreEqual(true, _result);

		}

		[Test]
		public void TestFullCall_SimpleConnected() {
			bool _result = dispatcher("cmd.000010:rbr.onregister.1(192.168.1.10:1720,test_in_10)");
			Assert.AreEqual(true, _result);
			
			_result = dispatcher("cmd.000011:rbr.oncallsetup.1(1,192.168.1.10,test_in_10:h323_ID,,01152222234567)");
			Assert.AreEqual(true, _result);

			_result = dispatcher("cmd.000012:rbr.oncallconnect.1(192.168.1.10:1720,,192.168.1.62:1720,,848,529,3)");
			Assert.AreEqual(true, _result);

			_result = dispatcher(@"cmd.000013:rbr.oncallcomplete.1(CDR|3|f4 56 ab 84 84 f5 18 10 8c 56 00 04 5a a2 9c 45|11|Thu\, 06 Apr 2006 22:14:09 -0400|Thu\, 06 Apr 2006 22:14:20 -0400|192.168.1.10:3620|4060_endp|192.168.1.62:1720|4058_endp|01152222234567:dialedDigits|test_in_10:h323_ID|DEMO-GK|848|529|16|0|0|16;)");
			Assert.AreEqual(true, _result);
		}

		[Test]
		public void TestFullCall_SimpleNotConnected() {
			bool _result = dispatcher("cmd.000010:rbr.onregister.1(192.168.1.10:1720,test_in_10)");
			Assert.AreEqual(true, _result);
			
			_result = dispatcher("cmd.000007:rbr.oncallsetup.1(1,192.168.1.10,test_in_10:h323_ID,,01159373301111)");
			Assert.AreEqual(false, _result);

			_result = dispatcher(@"cmd.000016:rbr.oncallcomplete.1(CDR|6|50 82 a9 f0 85 f5 18 10 8d ae 00 04 5a a2 9c 45|10|Fri\, 07 Apr 2006 15:07:40 -0400|Fri\, 07 Apr 2006 15:07:50 -0400|192.168.1.10:3717|4596_endp|192.168.1.62:1720|4595_endp|2220115028796655:dialedDigits|test_in_10:h323_ID|DEMO-GK|1407|450|16|0|0|16;)");
			Assert.AreEqual(true, _result);
		}

		[Test]
		public void TestFullCall_WithPrefixConnected() {
			bool _result = dispatcher("cmd.000013:rbr.onregister.1(192.168.1.10:1720,test_in_10)");
			Assert.AreEqual(true, _result);
			
			_result = dispatcher("cmd.000014:rbr.oncallsetup.1(1,192.168.1.10,test_in_10:h323_ID,,2220115028796655)");
			Assert.AreEqual(true, _result);

			_result = dispatcher("cmd.000015:rbr.oncallconnect.1(192.168.1.10:1720,,192.168.1.62:1720,,1407,450,2)");
			Assert.AreEqual(true, _result);

			_result = dispatcher(@"cmd.000013:rbr.oncallcomplete.1(CDR|3|f4 56 ab 84 84 f5 18 10 8c 56 00 04 5a a2 9c 45|11|Thu\, 06 Apr 2006 22:14:09 -0400|Thu\, 06 Apr 2006 22:14:20 -0400|192.168.1.10:3620|4060_endp|192.168.1.62:1720|4058_endp|01152222234567:dialedDigits|test_in_10:h323_ID|DEMO-GK|848|529|16|0|0|16;)");
			Assert.AreEqual(true, _result);
		}

		//-------------------------------------- Private --------------------------------------------------
		private bool dispatcher(string pCmd) {
			try {
        // Create a command factory and call it to create correct CommandMessage instance:
        ICommand _cmd = commandFactory.GetInstance(UdpMessageParser.GetCommandNameAndVersion(pCmd));
        _cmd.Sequence = UdpMessageParser.GetSequence(pCmd);
        _cmd.Parameters = UdpMessageParser.GetInParameters(pCmd);

        //Process command:
        _cmd.Execute();
        return _cmd.Result;
			}
			catch (Exception _ex) {
				T.LogCritical("Exception: " + Utils.GetFullExceptionInfo(_ex));
			}
			return false;
		}

		//private void licenseExpiredHandler() {
		//  udpServer.Stop();
		//}

	}
}
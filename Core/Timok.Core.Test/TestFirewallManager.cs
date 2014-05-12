using System;
using System.Collections;
using NUnit.Framework;

namespace Timok.Core.Test {
	[TestFixture]
	public class TestFirewallManager {
		[SetUp]
		public void Setup() {}

		[Test]
		public void TestAddingIpToEnabledList() {
			string _remoteAddresses = "LocalSubnet"; //"192.168.1.50/255.255.255.255";//"LocalSubnet,10.1.1.1/255.255.255.255";
			try {
				var _mgr = (INetFwMgr) new NetFwMgr();
				Console.WriteLine("CurrentProfileType: " + _mgr.CurrentProfileType);

				var _profile = _mgr.LocalPolicy.CurrentProfile;
				Console.WriteLine("FirewallEnabled: " + _profile.FirewallEnabled);

				var _e = _profile.AuthorizedApplications.NewEnum;

				/* Adding an application that doesn't currently exist
				 INetFwAuthorizedApplication newApp = (INetFwAuthorizedApplication)new NetFwAuthorizedApplication();
				newApp.Enabled = true;
				newApp.Name = "FooTestApp";
				newApp.Scope = Scope.Subnet;
				newApp.ProcessImageFileName = "c:\\work\\temp\\FirewallManager.exe";
				profile.AuthorizedApplications.Add(newApp);
				 */

				Console.WriteLine("\r\n-----  Applications  -----  ");
				while (_e.MoveNext()) {
					var _app = _e.Current as INetFwAuthorizedApplication;
					Console.WriteLine("\t{0}\r\n\t\tImageFileName={1}\r\n\t\tEnabled={2}\r\n\t\tIpVersion={3}\r\n\t\tScope={4}\r\n\t\tRemoteAddresses={5}", _app.Name, _app.ProcessImageFileName, _app.Enabled, _app.IpVersion, _app.Scope, _app.RemoteAddresses);
					if (_app.Name.CompareTo(@"Timok.Rbr.UdpServer") == 0 && _app.ProcessImageFileName.CompareTo(@"C:\Timok\Rbr\Gk\Release\Timok.Rbr.UdpServer.exe") == 0) {
						_app.RemoteAddresses = _remoteAddresses;
						_app.Enabled = true;
					}
				}

				//				_e = profile.Services.NewEnum;
				//				Console.WriteLine("\r\n-----  Services  -----  ");
				//				while (_e.MoveNext()) {
				//					INetFwService service = _e.Current as INetFwService;
				//					Console.WriteLine("\t{0}\r\n\t\tType={1}\r\n\t\tEnabled={2}\r\n\t\tIpVersion={3}"+
				//						"\r\n\t\tScope={4}\r\n\t\tCustomized={5}\r\n\t\tRemoteAddresses={6}",
				//						service.Name,
				//						service.Type,
				//						service.Enabled,
				//						service.IpVersion,
				//						service.Scope,
				//						service.Customized,
				//						service.RemoteAddresses);
				//				}
				//
				//				_e = profile.GloballyOpenPorts.NewEnum;
				//				Console.WriteLine("\r\n-----  Globally open ports  -----  ");
				//				while (_e.MoveNext()) {
				//					INetFwOpenPort port = _e.Current as INetFwOpenPort;
				//					Console.WriteLine("\t{0}\r\n\t\tIsBuiltIn={1}\r\n\t\tEnabled={2}\r\n\t\tIpVersion={3}"+
				//						"\r\n\t\tScope={4}\r\n\t\tProtocol={5}\r\n\t\tRemoteAddresses={6}",
				//						port.Name,
				//						port.BuiltIn,
				//						port.Enabled,
				//						port.IpVersion,
				//						port.Scope,
				//						port.Protocol,
				//						port.RemoteAddresses);
				//				}
			}
			catch (Exception _ex) {
				Console.WriteLine(_ex);
			}
		}
	}
}
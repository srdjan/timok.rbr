using System;
using System.Collections;
using System.IO;
using System.Runtime.InteropServices;

namespace Timok.Core {
	public static class FirewallManager {
		public static void StartFirewall(string pProcessFilePath, string pAppName) {
			if (File.Exists(pProcessFilePath)) {
				var _firewallManager = (INetFwMgr)new NetFwMgr();
				var _newApp = (INetFwAuthorizedApplication)new NetFwAuthorizedApplication();
				_newApp.Enabled = true;
				_newApp.Name = pAppName;
				_newApp.Scope = Scope.All;
				_newApp.ProcessImageFileName = pProcessFilePath;
				_firewallManager.LocalPolicy.CurrentProfile.AuthorizedApplications.Add(_newApp);
			}
			else {
				throw new Exception(string.Format("FirewallManager.StartFirewall, Error: Firewall NOT Enabled for App={0}", pProcessFilePath));
			}
		}

		public static void StopFirewall(string pProcessFilePath) {
			var _firewallManager = (INetFwMgr)new NetFwMgr();
			_firewallManager.LocalPolicy.CurrentProfile.AuthorizedApplications.Remove(pProcessFilePath);
		}
	}

	//--------------------------------------------------------------------------------------------
	[ComImport, ComVisible(false), Guid("304CE942-6E39-40D8-943A-B913C40C9CD4")]
	public class NetFwMgr { }

	//--------------------------------------------------------------------------------------------
	[ComImport, ComVisible(false), Guid("F7898AF5-CAC4-4632-A2EC-DA06E5111AF2"), InterfaceType(ComInterfaceType.InterfaceIsIDispatch)]
	public interface INetFwMgr {
		INetFwPolicy LocalPolicy {get;}
		FirewallProfileType CurrentProfileType {get;}
		void RestoreDefaults();
		void IsPortAllowed(string pImageFileName, IPVersion pIPVersion, long pOrtNumber, string pLocalAddress, IPProtocol pIPProtocol, [Out] out bool pAllowed, [Out] out bool pRestricted);
		void IsIcmpTypeAllowed(IPVersion pIPVersion, string pLocalAddress, byte pType, [Out] out bool pAllowed, [Out] out bool pRestricted);
	}

	//--------------------------------------------------------------------------------------------
	[ComImport, ComVisible(false), Guid("D46D2478-9AC9-4008-9DC7-5563CE5536CC"), InterfaceType(ComInterfaceType.InterfaceIsIDispatch)]
	public interface INetFwPolicy {
		INetFwProfile CurrentProfile{get;}
		INetFwProfile GetProfileByType(FirewallProfileType pRofileType);
	}

	//--------------------------------------------------------------------------------------------
	[ComImport, ComVisible(false), Guid("174A0DDA-E9F9-449D-993B-21AB667CA456"), InterfaceType(ComInterfaceType.InterfaceIsIDispatch)]
	public interface INetFwProfile {
		FirewallProfileType Type {get;}
		bool FirewallEnabled {get;set;}
		bool ExceptionsNotAllowed {get;set;}
		bool NotificationsDisabled {get;set;}
		bool UnicastResponsesToMulticastBroadcastDisabled {get;set;}
		INetFwRemoteAdminSettings RemoteAdminSettings {get;}
		INetFwIcmpSettings IcmpSettings {get;}
		INetFwOpenPorts GloballyOpenPorts {get;}
		INetFwServices Services {get;}
		INetFwAuthorizedApplications AuthorizedApplications {get;}
	}

	//--------------------------------------------------------------------------------------------
	[ComImport, ComVisible(false), Guid("D4BECDDF-6F73-4A83-B832-9C66874CD20E"), InterfaceType(ComInterfaceType.InterfaceIsIDispatch)]
	public interface INetFwRemoteAdminSettings {
		IPVersion IpVersion {get;set;}
        
		Scope Scope{get;set;}
       
		string RemoteAddresses{get;set;}
       
		bool Enabled {get;set;}
	}

	//--------------------------------------------------------------------------------------------
	[ComImport, ComVisible(false), Guid("A6207B2E-7CDD-426A-951E-5E1CBC5AFEAD"), InterfaceType(ComInterfaceType.InterfaceIsIDispatch)]
	public interface INetFwIcmpSettings {
		bool AllowOutboundDestinationUnreachable{get;set;}
       
		bool AllowRedirect{get;set;}
       
		bool AllowInboundEchoRequest{get;set;}

		bool AllowOutboundTimeExceeded{get;set;}

		bool AllowOutboundParameterProblem{get;set;}
       
		bool AllowOutboundSourceQuench{get;set;}

		bool AllowInboundRouterRequest{get;set;}
  
		bool AllowInboundTimestampRequest{get;set;}
       
		bool AllowInboundMaskRequest{get;set;}

		bool AllowOutboundPacketTooBig{get;set;}
	}

	//--------------------------------------------------------------------------------------------
	[ComImport, ComVisible(false), Guid("C0E9D7FA-E07E-430A-B19A-090CE82D92E2"), InterfaceType(ComInterfaceType.InterfaceIsIDispatch)]
	public interface INetFwOpenPorts {
		long Count {get;}
       
		void Add(INetFwOpenPort pOrt);

		void Remove(long pOrtNumber, IPProtocol pIPProtocol);
       
		INetFwOpenPort Item(long pOrtNumber, IPProtocol pIPProtocol);
       
		IEnumerator NewEnum{get;}
	}

	//--------------------------------------------------------------------------------------------
	[ComImport, ComVisible(false), Guid("E0483BA0-47FF-4D9C-A6D6-7741D0B195F7"), InterfaceType(ComInterfaceType.InterfaceIsIDispatch)]
	public interface INetFwOpenPort {
		string Name{get;set;}
       
		IPVersion IpVersion{get;set;}
  
		IPProtocol Protocol{get;set;}

		long Port {get;set;}

		Scope Scope{get;set;}
  
		string RemoteAddresses{get;set;}
  
		bool Enabled{get;set;}
    
		bool BuiltIn {get;}
	}

	//--------------------------------------------------------------------------------------------
	[ComImport, ComVisible(false), Guid("79649BB4-903E-421B-94C9-79848E79F6EE"), InterfaceType(ComInterfaceType.InterfaceIsIDispatch)]
	public interface INetFwServices {
		long Count {get;}
       
		INetFwService Item(NetworkServiceType pSvcType);
       
		IEnumerator NewEnum {get;}
	}

	//--------------------------------------------------------------------------------------------
	[ComImport, ComVisible(false), Guid("79FD57C8-908E-4A36-9888-D5B3F0A444CF"), InterfaceType(ComInterfaceType.InterfaceIsIDispatch)]
	public interface INetFwService {
		string Name{get;}
       
		NetworkServiceType Type{get;}

		bool Customized{get;}

		IPVersion IpVersion{get;set;}
  
		Scope Scope{get;set;}
  
		string RemoteAddresses{get;set;}

		bool Enabled{get;set;}
       
		INetFwOpenPorts GloballyOpenPorts {get;}
	}

	//--------------------------------------------------------------------------------------------
	[ComImport, ComVisible(false), Guid("644EFD52-CCF9-486C-97A2-39F352570B30"), InterfaceType(ComInterfaceType.InterfaceIsIDispatch)]
	public interface INetFwAuthorizedApplications {
		long Count {get;}
       
		void Add(INetFwAuthorizedApplication pOrt);

		void Remove(string pImageFileName);
       
		INetFwAuthorizedApplication Item(string pImageFileName);
       
		IEnumerator NewEnum{get;}
	}

	//--------------------------------------------------------------------------------------------
	[ComImport, ComVisible(false), Guid("EC9846B3-2762-4A6B-A214-6ACB603462D2")]
	class NetFwAuthorizedApplication {	}

	//--------------------------------------------------------------------------------------------
	[ComImport, ComVisible(false), Guid("B5E64FFA-C2C5-444E-A301-FB5E00018050"), InterfaceType(ComInterfaceType.InterfaceIsIDispatch)]
	public interface INetFwAuthorizedApplication {
		string Name{get;set;}
       
		string ProcessImageFileName{get;set;}

   
		IPVersion IpVersion{get;set;}
  
		Scope Scope{get;set;}
  
		string RemoteAddresses{get;set;}

		bool Enabled{get;set;}
	}

	//--------------------------------------------------------------------------------------------
	public enum FirewallProfileType {
		Domain = 0,
		Standard = 1,
		Current = 2,
		Max = 3
	}

	public enum IPVersion {
		IPv4 = 0,
		IPv6 = 1,
		IPAny = 2,
		IPMax = 3
	}

	public enum IPProtocol {
		Tcp= 6,
		Udp= 17
	}

	public enum Scope {
		All = 0,
		Subnet = 1,
		Custom = 2,
		Max = 3
	}

	public enum NetworkServiceType {
		FileAndPrint = 0,
		UPnP = 1,
		RemoteDesktop = 2,
		None = 3,
		Max = 4
 }
}
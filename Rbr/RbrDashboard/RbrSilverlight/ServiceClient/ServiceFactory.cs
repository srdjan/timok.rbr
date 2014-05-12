using System;
using System.ServiceModel;
using System.Windows;
using System.Windows.Browser;
using Microsoft.Silverlight.Samples;
using RbrSiverlight.ServiceReference;

namespace RbrSiverlight.ServiceClient {
	// A factory to create an instance of the web service
	public static class ServiceFactory {
		static BasicHttpSecurityMode securityMode = BasicHttpSecurityMode.None;
		static string webServiceUrlPath;


		public static DashboardServiceClient GetService() {
			var _address = new EndpointAddress(serviceUrlPath);
			var _binding = new BasicHttpMessageInspectorBinding(new ServiceMessageInspector(), securityMode);

			var _service = new DashboardServiceClient(_binding, _address);
			return _service;
		}	
		
		// Instantiates web service and configures the url and the credentials in the message header before returning the instance. 
		// <returns>An instance of the web service</returns>
		static string serviceUrlPath {
			get {
				if (webServiceUrlPath == null) {
					// We need to instantiate the service just to determine the security mode of the binding from the clientconfig file - 
					// easier than opening and reading the file itself
					var _tempClient = new DashboardServiceClient();
					var _scheme = _tempClient.Endpoint.Binding.Scheme;

					// Now build up the url of the service based upon the url the application was run from and the scheme specified in the client config
					var _url = new Uri(Application.Current.Host.Source, "../Services/RbrDashboardService.svc");
					webServiceUrlPath = _url.OriginalString.Replace(_url.Scheme + "://", _scheme + "://");
					if (_scheme.ToLower() == "https")
						securityMode = BasicHttpSecurityMode.Transport;
				}

				return webServiceUrlPath;
			}
		}
	}


	//------------------------------- alternate solution ----------------------------------------
	//link: http://geekswithblogs.net/mwatson/archive/2009/02/24/129655.aspx
	//usage:
	 // private SLGlobalCustomerClient client = GetClient<SLGlobalCustomer.SLGlobalCustomerClient>("SLCustomer.svc");
   // client.SaveCustomersCompleted += new EventHandler<SaveCustomersCompletedEventArgs>(client_SaveCustomersCompleted);
	 // client.LoadCustomersCompleted += new EventHandler<LoadCustomersCompletedEventArgs>(client_LoadCustomersCompleted);
	 // client.LoadCustomersAsync(AutoLeadID);
	public static class SLLocation {
		public static Uri SilverlightLocation { get { return Application.Current.Host.Source; } }
		public static Uri RootURL { get { return new Uri(SilverlightLocation, @"../"); } }
		public static Uri ServiceURL { get { return new Uri(RootURL, "Services/"); } }

		public static BasicHttpBinding WCFHttpBinding(long? pMaxReceivedMessageSize) {
			var _ret = new BasicHttpBinding();

			//had to wrap this in a try catch because Expression Blend would throw a fit over this at design time if 
			//referencing GetClient in the constructor (not in a method basically)
			try {
				if (HtmlPage.Document.DocumentUri.Scheme.ToLower() == "https") {
					_ret.Security.Mode = BasicHttpSecurityMode.Transport;
				}
				else {
					_ret.Security.Mode = BasicHttpSecurityMode.None;
				}
				_ret.MaxReceivedMessageSize = pMaxReceivedMessageSize ?? 65536; //65536 is default
			}
			catch {}
			return _ret;
		}

		public static T GetClient<T>(string pServiceName) where T : class {
			return GetClient<T>(pServiceName, null);
		}

		public static T GetClient<T>(string pServiceName, long? pMaxReceivedMessageSize) where T : class {
			var p = new object[] {WCFHttpBinding(pMaxReceivedMessageSize), getEndpoint(pServiceName)};
			return Activator.CreateInstance(typeof (T), p) as T;
		}

		static EndpointAddress getEndpoint(string serviceName) {
			var uri = new Uri(ServiceURL + serviceName);
			return new EndpointAddress(uri.ToString());
		}

	}
}
using System;
using Microsoft.Win32;

namespace Timok.Core
{
	/// <summary>
	/// TODO: create WinServiceLib and add code below
	/// </summary>
	public class WinServiceHelper
	{
		/// <summary>
		/// Adds the service description to the registry.
		/// </summary>
		/// <param name="serviceName"></param>
		/// <param name="description"></param>
		protected virtual void AddServiceDescriptionToRegistry(string serviceName, string description) {
			RegistryKey system;
			RegistryKey    currentControlSet; //HKEY_LOCAL_MACHINE\Services\CurrentControlSet
			RegistryKey services; //...\Services
			RegistryKey service; //...\<Service Name>
			RegistryKey config; //...\Parameters - this is where you can put service-specific configuration
			try {
				//Open the HKEY_LOCAL_MACHINE\SYSTEM key
				system = Registry.LocalMachine.OpenSubKey("System");
				//Open CurrentControlSet
				currentControlSet = system.OpenSubKey("CurrentControlSet");
				//Go to the services key
				services = currentControlSet.OpenSubKey("Services");
				//Open the key for your service, and allow writing
				service = services.OpenSubKey(serviceName, true);
				//Add your service's description as a REG_SZ value named "Description"
				service.SetValue("Description", description);
				//(Optional) Add some custom information your service will use...
				config = service.CreateSubKey("Parameters");
			}
			catch(Exception/* e*/) {
				//Log.Error("Error occurred while attempting to add a service description to the registry.", e);
			}
		}
		/// <summary>
		/// Removes the service description from the registry.
		/// </summary>
		/// <param name="serviceName"></param>
		protected virtual void RemoveServiceDescriptionFromRegistry(string serviceName) {
			RegistryKey system;
			RegistryKey    currentControlSet; //HKEY_LOCAL_MACHINE\Services\CurrentControlSet
			RegistryKey services; //...\Services
			RegistryKey service; //...\<Service Name>
			//Microsoft.Win32.RegistryKey config; //...\Parameters - this is where you can put service-specific configuration
			try {
				//Drill down to the service key and open it with write permission
				system = Registry.LocalMachine.OpenSubKey("System");
				currentControlSet = system.OpenSubKey("CurrentControlSet");
				services = currentControlSet.OpenSubKey("Services");
				service = services.OpenSubKey(serviceName, true);
				//Delete any keys you created during installation (or that your service created)
				service.DeleteSubKeyTree("Parameters");
				//...
			}
			catch(Exception/* e*/) {
				//Log.Error("Error occurred while trying to remove the service description from the registry.", e);
			}
		}	
	}
}

using System.Collections;
using System.Diagnostics;
using System.Management;
using System.Net;
using System.Net.Sockets;
using Helper.Performance;

namespace Helper.Net.NetworkAdapter
{
    /// <summary>
    /// Represents a network adapter installed on the machine.
    /// </summary>
    public sealed class NetworkAdapter
    {
        #region Variables

        private static ArrayList adapters; // The list of adapters on the computer.

        internal string name;       // The name of the adapter.
        internal string _ipAddress; // The IP addresss of the adapter

        internal NetworkMeter _networkMeter;

        #endregion

        #region Constructor

        static NetworkAdapter()
        {
            adapters = new ArrayList();
            EnumerateNetworkAdapters();
        }

        /// <summary>
        /// Instances of this class are supposed to be created only in an NetworkAdapter.
        /// </summary>
        private NetworkAdapter(string name)
        {
            this.name = name;

            _ipAddress = GetIPAddress();
        }

        private string GetIPAddress()
        {
            ManagementClass objMC = new ManagementClass("Win32_NetworkAdapterConfiguration");
            ManagementObjectCollection objMOC = objMC.GetInstances();

            foreach (ManagementObject objMO in objMOC)
            {
                if (!(bool)objMO["ipEnabled"])
                    continue;

                if (name == (string)objMO["Description"])
                    foreach (string sIP in (string[])objMO["IPAddress"])
                        return sIP;
            }

            return null;
        }

        /// <summary>
        /// Enumerates network adapters installed on the computer.
        /// </summary>
        private static void EnumerateNetworkAdapters()
        {
            PerformanceCounterCategory category = new PerformanceCounterCategory("Network Interface");

            foreach (string name in category.GetInstanceNames())
            {
                // This one exists on every computer.
                if (name == "MS TCP Loopback interface")
                    continue;
                // Create an instance of NetworkAdapter class, and create performance counters for it.
                NetworkAdapter adapter = new NetworkAdapter(name);

                adapters.Add(adapter); // Add it to ArrayList adapter
            }
        }

        #endregion

        #region Properties

        /// <summary>
        /// IP address.
        /// </summary>
        public string IPAddress
        {
            get { return _ipAddress; }
        }

        /// <summary>
        /// The name of the network adapter.
        /// </summary>
        public string Name
        {
            get { return name; }
        }

        /// <summary>
        /// Returns a meter.
        /// </summary>
        public Performance.NetworkMeter NetworkMeter
        {
            get
            {
                if (_networkMeter == null)
                    _networkMeter = new NetworkMeter(name);
                return _networkMeter;
            }
        }
                
        #endregion

        #region Features

        /// <summary>
        /// Returns the network adapter associated with the socket.
        /// </summary>
        public static NetworkAdapter GetNetworkAdapter(Socket socket)
        {
            ManagementClass objMC = new ManagementClass("Win32_NetworkAdapterConfiguration");
            ManagementObjectCollection objMOC = objMC.GetInstances();

            foreach (ManagementObject objMO in objMOC)
            {
                if (!(bool)objMO["ipEnabled"])
                    continue;

                foreach (string sIP in (string[])objMO["IPAddress"])
                    if (sIP == ((IPEndPoint)socket.LocalEndPoint).Address.ToString())
                    {
                        // Search the network adapter
                        foreach (NetworkAdapter adapter in adapters)
                            foreach (PropertyData o in objMO.Properties)
                                //System.Console.WriteLine(o.Name + "---" + objMO[o.Name]);
                                if (adapter.Name == (string)objMO["Description"])
                                    return adapter;
                    }
            }

            return null;
        }

        /// <summary>
        /// Get instances of NetworkAdapter for installed adapters on this computer.
        /// </summary>
        public static NetworkAdapter[] NetworkAdapters
        {
            get { return (NetworkAdapter[])adapters.ToArray(typeof(NetworkAdapter)); }
        }

        #endregion
    }
}
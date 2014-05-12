using System;
using System.Timers;
using Helper.Net.NetworkAdapter;

namespace Helper.Performance
{
    /// <summary>
    /// This class do a background machine performance Analysis. This monitor analyse
    /// the performance each 15 seconds.
    /// 
    /// You can have:
    /// - CPU Usage
    /// - Memory analysis
    /// - Network analysis
    /// </summary>
    public sealed class PerformanceMonitor
    {
        /// http://msdn.microsoft.com/library/default.asp?url=/library/en-us/dnpag/html/scalenetchapt15.asp
        /// http://www.z-3.us/Z3/performanceCounters.aspx

        #region Variables

        private static Timer _analysisTimer;

        private static CPUMeter _cpuMeter;
        private static MemoryMeter _memoryMeter;

        private static double _cpuUsage;
        private static double _physicalMemoryAvailable;
        private static double _physicalMemoryUsage;
        private static double _networkUsage;

        private static string _IPAddress = null;

        public static event PerformanceMonitorEventHandler OnAnalysisComplete;

        #endregion

        #region Constructor

        static PerformanceMonitor()
        {
            // Processor Time Performance
            //_cpuMeter = new CPUMeter(true);
            _memoryMeter = new MemoryMeter();

            // First analysis
            Analyse(null, null);

            _analysisTimer = new Timer(15000);
            _analysisTimer.Elapsed += new ElapsedEventHandler(Analyse);
            _analysisTimer.Enabled = true;
        }

        #endregion

        #region Features

        /// <summary>
        /// Set the IP address of the network adapter to analyse.
        /// </summary>
        static public void SetIPAddressToAnalyse(string ip)
        {
            _IPAddress = ip;
        }

        #endregion

        #region Properties

        /// <summary>
        /// Returns and set the Analysis interval in milliseconds.
        /// </summary>
        public static double AnalysisInterval
        {
            get
            {
                return _analysisTimer.Interval;
            }
            set
            {
                _analysisTimer.Interval = value;
            }
        }

        /// <summary>
        /// Returns the CPU usage in %.
        /// </summary>
        public static double CPUUsage
        {
            get
            {
                //lock( _cpuMeter )
                {
                    return _cpuUsage;
                }
            }
        }

        public static double NetworkUsage
        {
            get
            {
                //lock( _networkMonitor )
                {
                    //_networkUsage = 0.70;
                    return _networkUsage;
                }
            }
        }

        /// <summary>
        /// Returns the memory usage in %;
        /// </summary>
        public static double PhysicalMemoryUsage
        {
            get
            {
                //lock( _memoryMeter )
                {
                    return _physicalMemoryUsage;
                }
            }
        }

        /// <summary>
        /// Returns the available memory in Mega bytes;
        /// </summary>
        public static double PhysicalMemoryAvailable
        {
            get
            {
                //lock( _memoryMeter )
                {
                    return _physicalMemoryAvailable;
                }
            }
        }

        #endregion

        #region Analyse

        private static void Analyse(object sender, ElapsedEventArgs e)
        {
            //---- CPU utilization %
            lock (_cpuMeter)
            {
                _cpuUsage = _cpuMeter.CPUUsage;
            }

            //---- Memory usage and available
            lock (_memoryMeter)
            {
                _physicalMemoryAvailable = _memoryMeter.AvailablePhysicalMemory;
                _physicalMemoryUsage = _memoryMeter.PhysicalMemoryUsage;
            }

            //---- Network Utilization
            double networkUsage = 0;// Double.MaxValue;
            foreach (NetworkAdapter networkAdapter in NetworkAdapter.NetworkAdapters)
            {
                if (_IPAddress == null || _IPAddress == networkAdapter.IPAddress)
                {
                    networkAdapter.NetworkMeter.Refresh();
                    if (networkAdapter.NetworkMeter.BandWidth > 0)
                    {
                        /*
                        double remainingUsage = (networkAdapter.NetworkMeter.BandWidth - networkAdapter.NetworkMeter.TotalSpeed) / networkAdapter.NetworkMeter.BandWidth;
                        if (remainingUsage > 0) _networkUsage = Math.Min(networkUsage, remainingUsage);
                        */
                        networkUsage += networkAdapter.NetworkMeter.NetworkUsage;
                    }
                }
            }

            //if (networkUsage < 0) networkUsage = 0;
            //if (networkUsage > 1) networkUsage = 1;

            _networkUsage = networkUsage;

            //---- Fire Event
            if (OnAnalysisComplete != null) OnAnalysisComplete();
        }

        #endregion
    }

    #region Event

    public delegate void PerformanceMonitorEventHandler();

    #endregion
}
using System.Collections;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Management;
using System.Net;
using System.Net.Sockets;
using Helper.Net.NetworkAdapter;

namespace Helper.Performance
{
    /// <summary>
    /// The NetworkMeter class monitors network speed for each network adapter on the computer,
    /// using classes for Performance counter in .NET library.
    /// </summary>
    /// <remarks>http://www.codeproject.com/csharp/networkmonitorl.asp</remarks>
    public sealed class NetworkMeter
    {

        #region API

        [DllImport("Kernel32.dll")]
        private static extern void QueryPerformanceCounter(out long ticks);

        [DllImport("Kernel32.dll")]
        private static extern bool QueryPerformanceFrequency(out long lpFrequency);

        #endregion

        #region Variables

        private long dlValue, ulValue; // Download\Upload counter value in bytes.
        private long bwValue, tbValue;

        private long dlValueOld, ulValueOld; // Download\Upload counter value one second earlier, in bytes.
        private long bwValueOld, tbValueOld;

        internal PerformanceCounter dlCounter, ulCounter; // Performance counters to monitor download and upload speed.
        internal PerformanceCounter bwCounter, tbCounter; // Performance counters to monitor Bandwidth and total bytes.

        long startTime = 0;
        long endTime = 0;
        private long frequency;
        private double elapsedSeconds;

        #endregion

        #region Constructor

        /// <summary>
        /// Instances of this class are supposed to be created only in an NetworkMonitor.
        /// </summary>
        internal NetworkMeter(string name)
        {
            dlCounter = new PerformanceCounter("Network Interface", "Bytes Received/sec", name);
            ulCounter = new PerformanceCounter("Network Interface", "Bytes Sent/sec", name);
            bwCounter = new PerformanceCounter("Network Interface", "Current Bandwidth", name);
            tbCounter = new PerformanceCounter("Network Interface", "Bytes Total/Sec", name);

            Init();
        }

        /// <summary>
        /// Preparations for monitoring.
        /// </summary>
        internal void Init()
        {
            // Since dlValueOld and ulValueOld are used in method refresh() to calculate network speed, they must have be initialized.
            dlValueOld = dlCounter.NextSample().RawValue;
            ulValueOld = ulCounter.NextSample().RawValue;
            bwValueOld = bwCounter.NextSample().RawValue;
            tbValueOld = tbCounter.NextSample().RawValue;

            if (QueryPerformanceFrequency(out frequency) == false)
            {
                // high-performance counter not supported
                throw new System.Exception("Win32 Exception - QueryPerformanceFrequency");
            }

            QueryPerformanceCounter(out startTime);
            elapsedSeconds = 1;
        }

        #endregion

        #region Properties

        /// <summary>
        /// Current download speed in bits per second (BPS).
        /// </summary>
        public double DownloadSpeed
        {
            get { return ((dlValue - dlValueOld) * 8.0) / elapsedSeconds; }
        }

        /// <summary>
        /// Current upload speed in bits per second (BPS).
        /// </summary>
        public double UploadSpeed
        {
            get { return ((ulValue - ulValueOld) * 8.0) / elapsedSeconds; }
        }

        /// <summary>
        /// Current Bandwidth is an estimate of the current bandwidth of the
        /// network interface in bits per second (BPS).
        /// For interfaces that do not vary in bandwidth or for those where
        /// no accurate estimation can be made, this value is the nominal bandwidth.
        /// </summary>
        public long BandWidth
        {
            get { return bwValue; }
        }

        /// <summary>
        /// Current speed in bits per second (BPS).
        /// </summary>
        public double TotalSpeed
        {
            get
            {
                return ((tbValue - tbValueOld) * 8.0) / elapsedSeconds;
            }
        }

        /// <summary>
        /// Current upload+download speed in bytes per second.
        /// </summary>
        public double NetworkUsage
        {
            get
            {
                return 100.0 * (((double)TotalSpeed) / bwValue);
            }
        }

        #endregion

        #region Features

        /// <summary>
        /// Obtain new sample from performance counters, and refresh the values saved in dlSpeed, ulSpeed, etc.
        /// This method is supposed to be called only in NetworkMonitor, one time every second.
        /// </summary>
        internal void Refresh()
        {
            dlValueOld = dlValue;
            ulValueOld = ulValue;
            bwValueOld = bwValue;
            tbValueOld = tbValue;

            dlValue = dlCounter.NextSample().RawValue;
            ulValue = ulCounter.NextSample().RawValue;
            bwValue = bwCounter.NextSample().RawValue;
            tbValue = tbCounter.NextSample().RawValue;

            QueryPerformanceCounter(out endTime);
            elapsedSeconds = (double)(endTime - startTime) / (double)frequency;
            QueryPerformanceCounter(out startTime);
        }

        #endregion

    }
}
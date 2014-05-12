using System;
using System.Diagnostics;

namespace Helper.Performance
{
    /// <summary>
    /// This class is used to calculate the performance of the CPU.
    /// </summary>
    public sealed class CPUMeter : IDisposable
    {
        #region Variables

        private CounterSample _startSample;
        private PerformanceCounter _cnt;

        #endregion

        #region Constructor

        /// Creates a per-process CPU meter instance tied to the current process.
        public CPUMeter()
        {
            String instancename = GetCurrentProcessInstanceName();
            _cnt = new PerformanceCounter("Process", "% Processor Time", instancename, true);
            _startSample = _cnt.NextSample();
        }

        /// Creates a per-process CPU meter instance tied to a specific process.
        public CPUMeter(int pid)
        {
            String instancename = GetProcessInstanceName(pid);
            _cnt = new PerformanceCounter("Process", "% Processor Time", instancename, true);
            _startSample = _cnt.NextSample();
        }

        public CPUMeter(bool forProcessor)
        {
            if (forProcessor)
            {
                _cnt = new PerformanceCounter("Processor", "% Processor Time", "_Total", true);
            }
            else
            {
                String instancename = GetCurrentProcessInstanceName();
                _cnt = new PerformanceCounter("Process", "% Processor Time", instancename, true);
            }
            _startSample = _cnt.NextSample();
            _startSample = _cnt.NextSample();
            _startSample = _cnt.NextSample();
        }

        public void Dispose()
        {
            if (_cnt != null) _cnt.Dispose();
        }

        #endregion

        #region GetCpuUtilization

        /// <summary>
        /// Returns this process's CPU utilization since the last call to ResetCounter().
        /// </summary>
        public double CPUUsage
        {
            get
            {
                CounterSample curr = _cnt.NextSample();

                double diffValue = curr.RawValue - _startSample.RawValue;
                double diffTimestamp = curr.TimeStamp100nSec - _startSample.TimeStamp100nSec;

                double usage = 1.0 - (diffValue / diffTimestamp);
                _startSample = _cnt.NextSample();
                return usage;
            }
        }

        #endregion

        #region Process Instance

        private static string GetCurrentProcessInstanceName()
        {
            Process proc = Process.GetCurrentProcess();
            int pid = proc.Id;
            return GetProcessInstanceName(pid);
        }

        private static string GetProcessInstanceName(int pid)
        {
            PerformanceCounterCategory cat = new PerformanceCounterCategory("Process");

            string[] instances = cat.GetInstanceNames();
            foreach (string instance in instances)
            {
                using (PerformanceCounter cnt = new PerformanceCounter("Process", "ID Process", instance, true))
                {
                    int val = (int)cnt.RawValue;
                    if (val == pid)
                    {
                        return instance;
                    }
                }
            }
            throw new Exception("Could not find performance counter " +
                                "instance name for current process. This is truly strange ...");
        }

        #endregion
    }
}
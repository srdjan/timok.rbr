using System;
using System.Diagnostics;
using System.Management;

namespace Helper.Performance {
  /// <summary>
  /// This class is used to calculate the performance of the Memory.
  /// </summary>
  public sealed class MemoryMeter : IDisposable {
    #region Variables

    private CounterSample _startSample;
    private PerformanceCounter _cnt;
    private int _totalMemory = -1;

    #endregion

    #region Constructor

    public MemoryMeter() {
      if (SetupCategory()) {
        return;
      }
      _cnt = new PerformanceCounter("Memory", "Available MBytes");
      _startSample = _cnt.NextSample();
    }

    public void Dispose() {
      if (_cnt != null)
        _cnt.Dispose();
    }

    #endregion

    #region Memory

    /// <summary>
    /// Returns the total memory in MBytes.
    /// </summary>
    public double AvailablePhysicalMemory {
      get {
        return _cnt.NextValue();
      }
    }

    /// <summary>
    /// Returns the total memory in MBytes.
    /// </summary>
    public int TotalPhysicalMemory {
      get {
        if (_totalMemory > -1)
          return _totalMemory;

        ManagementObjectSearcher search = new ManagementObjectSearcher("SELECT * FROM Win32_ComputerSystem");
        foreach (ManagementObject info in search.Get()) {
          ulong memory = (ulong)info["totalphysicalmemory"];
          _totalMemory = (int)(memory / (1024 * 1024));
          break;
        }

        return _totalMemory;
      }
    }

    /// <summary>
    /// Returns the usage of the physical memory
    /// </summary>
    public float PhysicalMemoryUsage {
      get {
        return (TotalPhysicalMemory - _cnt.NextValue()) / TotalPhysicalMemory;
      }
    }

    #endregion


    private static bool SetupCategory() {
      if (!PerformanceCounterCategory.Exists("Memory")) {

        CounterCreationDataCollection CCDC = new CounterCreationDataCollection();

        // Add the counter.
        CounterCreationData memory = new CounterCreationData();
        memory.CounterType = PerformanceCounterType.NumberOfItems32;
        memory.CounterName = "Memory";
        CCDC.Add(memory);

        // Add the base counter.
        CounterCreationData averageCount64Base = new CounterCreationData();
        averageCount64Base.CounterType = PerformanceCounterType.MemoryBase;
        averageCount64Base.CounterName = "AverageCounter64SampleBase";
        CCDC.Add(averageCount64Base);

        // Create the category.
        PerformanceCounterCategory.Create("AverageCounter64SampleCategory",
            "Demonstrates usage of the AverageCounter64 performance counter type.",
            PerformanceCounterCategoryType.SingleInstance, CCDC);

        return (true);
      }
      else {
        Console.WriteLine("Category exists - AverageCounter64SampleCategory");
        return (false);
      }
    }
  }
}
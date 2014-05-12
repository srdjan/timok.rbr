using System;
using System.Collections.Generic;
using System.Text;
using System.Net;
using System.Net.Sockets;

using Helper.Net.NetworkAdapter;
using Helper.Performance;

namespace Test
{
    public class Test
    {
        static void Main(string[] args)
        {
            PerformanceMonitor.OnAnalysisComplete += new PerformanceMonitorEventHandler(PerformanceMonitor_OnAnalysisComplete);
            PerformanceMonitor.AnalysisInterval = 10000;

            IPAddress[] addresses = Dns.GetHostAddresses(Dns.GetHostName());
            Socket s = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            s.Bind(new IPEndPoint(addresses[0], 1010));
            s.Listen(10);

            NetworkAdapter na = NetworkAdapter.GetNetworkAdapter(s);

            while (true)
                System.Threading.Thread.Sleep(10);
        }

        static void PerformanceMonitor_OnAnalysisComplete()
        {
            Console.WriteLine("--------------------------------------------------------");
            Console.WriteLine("CPU Usage            :" + PerformanceMonitor.CPUUsage + " %");
            Console.WriteLine("Available Memory     :" + PerformanceMonitor.PhysicalMemoryAvailable + " MBytes");
            Console.WriteLine("Memory Usage         :" + PerformanceMonitor.PhysicalMemoryUsage + " %");
            Console.WriteLine("Network Usage        :" + PerformanceMonitor.NetworkUsage + " %");
        }
    }
}

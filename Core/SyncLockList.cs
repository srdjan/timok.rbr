using System.Threading;
using Timok.Core.Instrumentation;

namespace Timok.Core 
{
	public class SyncLock {
		static Stopwatch stopwatch = new Stopwatch();
		static readonly object padlock = new object();
		static bool open;

		public static void Wait() {
			lock(padlock) {
				stopwatch.Start();

				open = false;
				while (! open) {
					Monitor.Wait(padlock, 5000); //TODO: handle timeout exception
				}
			} 
		}

		public static void Pulse() {
			lock (padlock) {
				open = true;
				Monitor.Pulse(padlock);
			}
			stopwatch.Stop();
//		Console.WriteLine("Wait/Pulse: " + stopwatch.ElapsedMilliseconds);
			stopwatch.Reset();
		}	
	}
}
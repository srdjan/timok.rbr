using System;
using System.Runtime.InteropServices;

namespace Timok.Core.Instrumentation 
{
	/// <summary>
	/// Provides a set of methods and properties that you can use to accurately 
	/// measure elapsed time.
	/// </summary>
	public class Stopwatch {
		[DllImport("Kernel32")]
		static extern bool QueryPerformanceCounter(out long @ref);

		[DllImport("Kernel32")]
		static extern bool QueryPerformanceFrequency(out long @ref);

		private long start = 0;
		private long elapsed = 0;
		private bool isRunning = false;
		/// <summary>
		/// the current performance-counter frequency, in counts per second
		/// </summary>
		readonly private long counterFrequency;

		public Stopwatch() {
			/// prelinks all win32 api calls so there's less performance hit when called
			Marshal.PrelinkAll(typeof(Stopwatch));
			if (QueryPerformanceFrequency(out counterFrequency) == false) {
				throw new Exception("High resolution timers are not available on this CPU");
			}
		}

		/// <summary>
		/// Starts, or resumes, measuring elapsed time for an interval.
		/// </summary>
		public void Start() {
			if ( ! isRunning) {
				Stop();
			}
			start = CurrentTime;
			isRunning = true;
		}

		/// <summary>
		/// Stops measuring elapsed time for an interval.
		/// </summary>
		public void Stop() {
			if ( ! isRunning) {
				return;
			}
			elapsed += CurrentTime - start;
			start = 0;
			isRunning = false;
		}

		/// <summary>
		/// Stops time interval measurement and resets elapsed time span to zero.
		/// </summary>
		public void Reset() {
			if (isRunning) {
				Stop();
			}
			elapsed = 0;
		}

		/// <summary>
		/// retrieves the current value of the high-resolution performance counter. 
		/// </summary>
		private long CurrentTime {
			get {		
				long _l = 0;
				QueryPerformanceCounter(out _l);
				return _l;
			}
		}

		/// <summary>
		/// Indicates whether the Stopwatch timer is running.
		/// </summary>
		public bool IsRunning {
			get { return (isRunning); }
		}

		/// <summary>
		/// Gets the total elapsed time measured by the current instance.
		/// </summary>
		public TimeSpan Elapsed {
			get { return new TimeSpan(ElapsedTicks); }
		}

		/// <summary>
		/// Gets the total elapsed time measured by the current instance, in milliseconds
		/// </summary>
		public long ElapsedMilliseconds {
			get {
				if (elapsed == 0) {
					return 0;
				}
				return (long)(((double)elapsed / counterFrequency) * 1000);
			}
		}

		/// <summary>
		/// Gets the total elapsed time measured by the current instance, in timer ticks
		/// </summary>
		public long ElapsedTicks {
			get {
				return (long)(ElapsedMilliseconds * TimeSpan.TicksPerMillisecond);
			}
		}
	}
}
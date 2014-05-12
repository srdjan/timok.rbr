using System;
using Timok.Rbr.Core.Config;

namespace Timok.Rbr.Core {
	public class Stats {
		static readonly object padlock = new object();

		static DateTime dtStarted = DateTime.Now;
		static string started { get { return dtStarted.ToString("MM/dd/yyyy hh:mm:ss"); } }
		static string running { get { return DateTime.Now.Subtract(dtStarted).ToString(); } }

		int todayLeg1;
		int todayLeg2;

		int currentOpen;
		int currentConnecting;
		int currentConnected;

		public Stats() {
			todayLeg1 = 0;
			todayLeg2 = 0;
			currentOpen = 0;
			currentConnecting = 0;
			currentConnected = 0;
		}

		public void Reset() {
			lock (padlock) {
				todayLeg1 = 0;
				todayLeg2 = 0;
			}
		}

		public void OnCallStarted() {
			lock (padlock) {
				todayLeg1++;
				currentOpen++;
			}
		}

		public void OnCallConnecting() {
			lock (padlock) {
				currentOpen--;
				currentConnecting++;
			}
		}

		public void OnCallConnected() {
			lock (padlock) {
				todayLeg2++;
				currentConnecting--;
				currentConnected++;
			}
		}

		public void OnCallCompleted(CallState pLastCallState) {
			lock (padlock) {
				if (pLastCallState == CallState.Started) {
					currentOpen--;
				}
				if (pLastCallState == CallState.Connecting) {
					currentConnecting--;
				}
				if (pLastCallState == CallState.Connected) {
					currentConnected--;
				}
			}
		}

		public CallStats GetCallStats() {
			var _callStats = new CallStats();
			_callStats.Startup = started;
			_callStats.Running = running;

			lock (padlock) {
				var _currentOpen = currentOpen >= 0 ? currentOpen : 0;

				_callStats.TotalCalls = todayLeg1;
				_callStats.TotalSuccessfullCalls = todayLeg2;
				_callStats.OpenCalls = _currentOpen;
				_callStats.ConnectingCalls = currentConnecting;
				_callStats.ConnectedCalls = currentConnected;
				_callStats.TotalCurrentCalls = _currentOpen + currentConnecting + currentConnected;
			}

			return _callStats;
		}
	}
}
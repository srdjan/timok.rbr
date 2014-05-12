
using System;

namespace Timok.Rbr.Core {
	public class CallStats {
		int connectedCalls;
		int connectingCalls;
		int openCalls;
		string running;
		string startup;
		int totalCalls;
		int totalCurrentCalls;
		int totalSuccessfullCalls;

		public CallStats() {
			init();
		}

		//true<<08/02/2006 02:39:41<<00:00:30.0468750<<1<<0<<1<<0<<2<<7
		//[0]           [1]                 [2]       [3][4][5][6][7][8]
		public CallStats(string[] pParams) {
			init();
			if (pParams.Length != 9 || pParams[0] != "true") {
				throw new Exception(string.Format("CallStats.Ctor: Invalid CallStats parameters: {0}", pParams));
			}
			startup = pParams[1];
			running = pParams[2];
			int.TryParse(pParams[3], out totalCalls);
			int.TryParse(pParams[4], out totalSuccessfullCalls);
			int.TryParse(pParams[5], out openCalls);
			int.TryParse(pParams[6], out connectingCalls);
			int.TryParse(pParams[7], out connectedCalls);
			int.TryParse(pParams[8], out totalCurrentCalls);
		}

		public string Startup { get { return startup; } set { startup = value; } }

		public string Running { get { return running; } set { running = value; } }

		//Totals
		public int TotalCalls { get { return totalCalls; } set { totalCalls = value; } }

		public int TotalSuccessfullCalls { get { return totalSuccessfullCalls; } set { totalSuccessfullCalls = value; } }

		//Current
		public int OpenCalls { get { return openCalls; } set { openCalls = value; } }

		public int ConnectingCalls { get { return connectingCalls; } set { connectingCalls = value; } }

		public int ConnectedCalls { get { return connectedCalls; } set { connectedCalls = value; } }

		public int TotalCurrentCalls { get { return totalCurrentCalls; } set { totalCurrentCalls = value; } }

		//---------------------------- Private -------------------------------------
		void init() {
			startup = string.Empty;
			running = string.Empty;
			totalCalls = 0;
			totalSuccessfullCalls = 0;
			openCalls = 0;
			connectingCalls = 0;
			connectedCalls = 0;
			totalCurrentCalls = 0;
		}
	}
}
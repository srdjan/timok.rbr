using System;
using Timok.Logger;

namespace Timok.Rbr.Core {
	public class CPSMeter	{
		static readonly object padlock = new object();
		readonly LogDelegate log;
		readonly string name;
		int measuredSecond;
		readonly int cpsLimit;
		int topScore;
		int currentSecondScore;
		
		public CPSMeter(string pName, int pCPSLimit, LogDelegate pLog) {
			name = pName;
			cpsLimit = pCPSLimit;
			measuredSecond = DateTime.Now.Second;
			topScore = 0;
			currentSecondScore = 0;
			log = pLog;
		}

		public void TakeSample() {
			lock (padlock) {
				var _currentSecond = DateTime.Now.Second;
				if (measuredSecond == _currentSecond) {
					currentSecondScore++;
					if (currentSecondScore >= cpsLimit) {
						throw new Exception(string.Format("CPS Limit Reached: {0}, TopScore={1}", name, topScore));
					}
					return;
				}
			
				if (currentSecondScore > topScore) {
					topScore = currentSecondScore;
					log(LogSeverity.Status, "CPSMeter.TakeSample", string.Format("CPS Meter: {0}, TopScore={1}", name, topScore));
				}

				measuredSecond = _currentSecond;
				currentSecondScore = 1;
			}
		}

		public void ResetTopScore() {
			topScore = 0;
		}
	}
}
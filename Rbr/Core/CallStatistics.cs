using System.Collections.Generic;
using Timok.Rbr.Core.Config;

namespace Timok.Rbr.Core {
	public class CallStatistics {
		readonly static Stats globalStats;
		readonly static SortedDictionary<short, Stats> customerStats;
		readonly static SortedDictionary<short, Stats> carrierStats;

		static CallStatistics() {
			globalStats = new Stats();
			customerStats = new SortedDictionary<short, Stats>();
			carrierStats = new SortedDictionary<short, Stats>();
		}

		public void ResetDailyCallStats() {
			globalStats.Reset();
		}

		public CallStats GetCallStats() {
			return globalStats.GetCallStats();
		}

		public CallStats GetCustomerCallStats(short pCustomerAcctId) {
			if (customerStats.ContainsKey(pCustomerAcctId)) {
				return customerStats[pCustomerAcctId].GetCallStats();
			}
			return new CallStats();
		}

		public CallStats GetCarrierCallStats(short pCarrierAcctId) {
			if (carrierStats.ContainsKey(pCarrierAcctId)) {
				return carrierStats[pCarrierAcctId].GetCallStats();
			}
			return new CallStats();
		}

		//-------------------------- Setters -----------------------------------------------------------------------------------
		public CallState OnStarted() {
			globalStats.OnCallStarted();
			return CallState.Started;
		}

		public CallState OnConnecting(LegIn pLegIn, LegOut pLegOut) {
			globalStats.OnCallConnecting();

			if (pLegIn.CustomerAcctId > 0) {
				if (!customerStats.ContainsKey(pLegIn.CustomerAcctId)) {
					customerStats.Add(pLegIn.CustomerAcctId, new Stats());
				}
				customerStats[pLegIn.CustomerAcctId].OnCallConnecting();
			}

			if (pLegOut.CarrierAcctId > 0) {
				if (!carrierStats.ContainsKey(pLegOut.CarrierAcctId)) {
					carrierStats.Add(pLegOut.CarrierAcctId, new Stats());
				}
				carrierStats[pLegOut.CarrierAcctId].OnCallConnecting();
			}

			return CallState.Connecting;
		}

		public CallState OnConnected(short pCarrierAcctId, short pCustomerAcctId) {
			globalStats.OnCallConnected();

			customerStats[pCustomerAcctId].OnCallConnected();
			carrierStats[pCarrierAcctId].OnCallConnected();

			return CallState.Connected;
		}

		public void OnCompleted(CallState pLastCallState, short pCarrierAcctId, short pCustomerAcctId) {
			globalStats.OnCallCompleted(pLastCallState);

			if (pCustomerAcctId > 0) {
				if (!customerStats.ContainsKey(pCustomerAcctId)) {
					customerStats.Add(pCustomerAcctId, new Stats());
				}
				customerStats[pCustomerAcctId].OnCallCompleted(pLastCallState);
			}

			if (pCarrierAcctId > 0) {
				if (!carrierStats.ContainsKey(pCarrierAcctId)) {
					carrierStats.Add(pCarrierAcctId, new Stats());
				}
				carrierStats[pCarrierAcctId].OnCallCompleted(pLastCallState);
			}
		}
	}
}
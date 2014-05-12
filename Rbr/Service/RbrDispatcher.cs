using Timok.Core;
using Timok.Rbr.Core;
using Timok.Rbr.Core.Config;

namespace Timok.Rbr.Service {
	public sealed class RbrDispatcher : IRbrDispatcher {
		readonly SequenceGenerator sequenceGenerator;
		readonly CallStatistics callStatistics;

		public RbrDispatcher(CallStatistics pCallStatistics) {
			callStatistics = pCallStatistics;
			sequenceGenerator = new SequenceGenerator(Configuration.Instance.Folders.RbrSequenceFilePath);
		}

		public long GetNextSequence() {
			return sequenceGenerator.GetNextSequence();
		}

		//--- Call Statistics
		public CallStats GetCallStats() {
			return callStatistics.GetCallStats();
		}

		public CallStats GetCustomerCallStats(short pCustomerAcctId) {
			return callStatistics.GetCustomerCallStats(pCustomerAcctId);
		}

		public CallStats GetCarrierCallStats(short pCarrierAcctId) {
			return callStatistics.GetCarrierCallStats(pCarrierAcctId);
		}

		//public void OnCallSetup(string pAccessNumber, short pCarrierAcctId, int pCarrierRouteId, short pCustomerAcctId, int pCustomerRouteId, string pOrigIP, string pTermIP) {
		//  CdrAggrExporter.OnCallSetup(pAccessNumber, pCarrierAcctId, pCarrierRouteId, pCustomerAcctId, pCustomerRouteId, pOrigIP, pTermIP);
		//}
	}
}
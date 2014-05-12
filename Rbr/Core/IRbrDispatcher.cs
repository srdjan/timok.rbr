
namespace Timok.Rbr.Core {
	public interface IRbrDispatcher {
		long GetNextSequence();
		//void RbrSync(IChangedObject[] pChangedObjects);
		
		//-- CallStats
		CallStats GetCallStats();
		CallStats GetCustomerCallStats(short pCustomerAcctId);
		CallStats GetCarrierCallStats(short pCarrierAcctId);
	}
}

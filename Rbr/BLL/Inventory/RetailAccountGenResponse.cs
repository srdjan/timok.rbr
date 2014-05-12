using Timok.Rbr.BLL.Inventory;

namespace Timok.Rbr.BLL.Inventory {
	public class Batch {
		public int BatchId;
		public long FirstSerial;
		public long LastSerial;

		public Batch(int pBatchId, long pFirstSerial, long pLastSerial) {
			BatchId = pBatchId;
			FirstSerial = pFirstSerial;
			LastSerial = pLastSerial;
		}
	}

	public class RetailAccountGenResponse {
		public RetailAccountGenRequest Request;
		public bool Result;
		public Batch[] BatchList;

		public RetailAccountGenResponse(RetailAccountGenRequest pRequest, bool pResult, Batch[] pBatchList) {
			Request = pRequest;
			Result = pResult;
			BatchList = pBatchList;
		}
	}
}
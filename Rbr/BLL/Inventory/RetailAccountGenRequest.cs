using System.IO;
using Timok.Rbr.Core.Config;

namespace Timok.Rbr.BLL.Inventory {
	public class RetailAccountGenRequest {
		public short ServiceId;
		public int PinLength;
		public int NumberOfBatches;
		public int BatchSize;
		public decimal Denomination;

		public string LotFolder {
			get {
				var _lotFolder = Configuration.Instance.Folders.GetInventoryLotFolder(ServiceId, Denomination);
				if (!Directory.Exists(_lotFolder)) {
					Directory.CreateDirectory(_lotFolder);
				}
				return _lotFolder;
			}
		}
		
		public RetailAccountGenRequest(short pServiceId, int pPinLength, int pNumberOfBatches, int pBatchSize, decimal pDenomination) {
			ServiceId = pServiceId;
			PinLength = pPinLength;
			NumberOfBatches = pNumberOfBatches;
			BatchSize = pBatchSize;
			Denomination = pDenomination;
		}

		public string BatchFilePath(int pBatchId) {
			return Path.Combine(LotFolder, Configuration.Instance.Folders.GetInventoryBatchFileName(ServiceId, Denomination, pBatchId));
		}
	}
}
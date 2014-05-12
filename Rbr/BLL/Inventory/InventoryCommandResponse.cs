namespace Timok.Rbr.BLL.Inventory {
	public class InventoryCommandResponse {
		public InventoryCommandRequest Request;
		public bool Result;

		public InventoryCommandResponse(InventoryCommandRequest pRequest, bool pResult) {
			Request = pRequest;
			Result = pResult;
		}
	}
}
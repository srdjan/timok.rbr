namespace RbrSiverlight.Delegates {
	public enum PageStatus {
		Loading,
		Active,
		RetrievingData,
		Closed,
		LoggingFailed
	}

  public class PageStatusChangeArgs {
		public PageStatus Status { get; set; }
		
		public PageStatusChangeArgs(PageStatus pStatus) {
			Status = pStatus;
		}
	}
}
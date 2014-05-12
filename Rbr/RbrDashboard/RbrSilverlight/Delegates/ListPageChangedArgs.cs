namespace RbrSiverlight.Delegates {
	public class ListPageChangedArgs {
		public ListPageChangedArgs(int pageNumber) {
			PageNumber = pageNumber;
		}

		public int PageNumber { get; set; }
	}
}
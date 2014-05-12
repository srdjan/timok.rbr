namespace RbrSiverlight.Delegates {
	public delegate void NavigateRequestHandler(object sender, NavigateRequestArgs e);
	public delegate void PageStatusChangeHandler(object sender, PageStatusChangeArgs e);
	public delegate void ListSearchEventHandler(object sender, ListSearchArgs e);
	public delegate void ListPageChangedEventHandler(object sender, ListPageChangedArgs e);
}
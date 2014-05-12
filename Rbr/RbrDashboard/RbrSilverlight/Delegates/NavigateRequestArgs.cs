namespace RbrSiverlight.Delegates {
	public class NavigateRequestArgs {
		public NavigateRequestArgs(string action, object parameters) {
			Action = action;
			Parameters = parameters;
		}

		public string Action { get; set; }
		public object Parameters { get; set; }
	}
}
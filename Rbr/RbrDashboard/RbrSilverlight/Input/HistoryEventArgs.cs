using System;

namespace RbrSiverlight.Input {
	public class HistoryEventArgs<T> : EventArgs {
		public T State { get; set; }
	}
}
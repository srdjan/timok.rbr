using System;

namespace RbrSiverlight.Input {
	public interface IManageHistory<TState> {
		void AddHistoryPoint(TState state, string title);
		event EventHandler<HistoryEventArgs<TState>> Navigate;
	}
}
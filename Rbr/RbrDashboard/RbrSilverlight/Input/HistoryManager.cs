using System;
using System.Windows.Browser;
using RbrSiverlight.Extensions;

namespace RbrSiverlight.Input {
	public class HistoryManager<TState> : IManageHistory<TState> {
		public HistoryManager() {
			if (HtmlPage.IsEnabled) {
				HtmlPage.RegisterScriptableObject("HistoryManager", this);
				string initScript =
					@"var __navigateHandler = new Function('obj','args','$get(\'" + HtmlPage.Plugin.Id +
					@"\').content.HistoryManager.HandleNavigate(args.get_state())');
                          Sys.Application.add_navigate(__navigateHandler);";
				HtmlPage.Window.Eval(initScript);
			}
		}

		public event EventHandler<HistoryEventArgs<TState>> Navigate;

		public void AddHistoryPoint(TState state, string title) {
			if (HtmlPage.IsEnabled) {
				string script = "Sys.Application.addHistoryPoint({0}, '{1}');";
				HtmlPage.Window.Eval(string.Format(script, state.ToJson(), title));
			}
		}

		[ScriptableMember]
		public void HandleNavigate(ScriptObject state) {
			if (Navigate != null) {
				var historyState = state.ConvertTo<TState>();
				var eventArg = new HistoryEventArgs<TState> {
				                                            	State = historyState
				                                            };
				Navigate(this, eventArg);
			}
		}
	}
}
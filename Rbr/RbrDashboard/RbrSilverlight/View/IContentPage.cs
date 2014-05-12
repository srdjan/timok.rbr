using System.Windows.Controls;
using RbrSiverlight.Delegates;

namespace RbrSiverlight.View {
	public interface IContentPage {
		event NavigateRequestHandler NavigateRequest;
		event PageStatusChangeHandler PageStatusChange;

		UserControl Control { get; }
	}
}
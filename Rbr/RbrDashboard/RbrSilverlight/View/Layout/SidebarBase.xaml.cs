using System.Windows.Controls;

namespace RbrSiverlight.View.Layout {
	public partial class SidebarBase : UserControl {
		public SidebarBase() {
			InitializeComponent();
		}

		public UIElementCollection Children {
			get { return ButtonStack.Children; }
		}
	}
}
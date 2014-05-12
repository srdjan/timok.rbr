using System.Windows;
using System.Windows.Controls;

namespace RbrSiverlight.View.Toolbar {
	public partial class Toolbar : UserControl {
		public Toolbar() {
			InitializeComponent();
		}

		public UIElementCollection Children {
			get { return ButtonStack.Children; }
		}

		public int VerticalButtonPadding {
			get { return (int) ButtonStack.Margin.Top; }
			set { ButtonStack.Margin = new Thickness(ButtonStack.Margin.Left, value, ButtonStack.Margin.Right, value); }
		}

		public Orientation Orientation {
			get { return ButtonStack.Orientation; }
			set {
				ButtonStack.Orientation = value;

				if (ButtonStack.Orientation == Orientation.Vertical) {
					toolbarRect.Visibility = Visibility.Collapsed;
				}
			}
		}
	}
}
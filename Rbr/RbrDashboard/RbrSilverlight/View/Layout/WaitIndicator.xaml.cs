using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;

namespace RbrSiverlight.View.Layout {
	public partial class WaitIndicator : UserControl {
		public WaitIndicator() {
			InitializeComponent();

			if (!DesignerProperties.GetIsInDesignMode(this))
				LayoutRoot.Visibility = Visibility.Collapsed;
		}

		public void Start() {
			LayoutRoot.Visibility = Visibility.Visible;
			IndicatorStoryboard.Begin();
		}

		public void Stop() {
			LayoutRoot.Visibility = Visibility.Collapsed;
			IndicatorStoryboard.Stop();
		}
	}
}
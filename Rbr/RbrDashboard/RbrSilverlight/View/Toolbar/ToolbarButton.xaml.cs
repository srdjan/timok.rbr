using System;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace RbrSiverlight.View.Toolbar {
	public partial class ToolbarButton : UserControl {
		bool isEnabled = true;

		public string Action { get; set; }
		public event EventHandler ButtonClick;

		public string Text {
			get { return buttonText.Text; }
			set { buttonText.Text = value; }
		}

		public bool Enabled {
			get { return isEnabled; }
			set {
				isEnabled = value;

				buttonText.Foreground = isEnabled ? new SolidColorBrush(Colors.White) : new SolidColorBrush(Colors.Gray);
				Cursor = isEnabled ? Cursors.Hand : Cursors.Arrow;
				IsHitTestVisible = isEnabled;
			}
		}

		public ToolbarButton() {
			InitializeComponent();
		}

		void ToolbarButton_MouseEnter(object sender, MouseEventArgs e) {
			selectedIndicator.Opacity = 0.4;
		}

		void ToolbarButton_MouseLeave(object sender, MouseEventArgs e) {
			selectedIndicator.Opacity = 0;
		}

		void ToolbarButton_MouseLeftButtonDown(object sender, MouseButtonEventArgs e) {
			selectedIndicator.Opacity = 1;
			OnButtonClick();
		}

		void ToolbarButton_MouseLeftButtonUp(object sender, MouseButtonEventArgs e) {
			selectedIndicator.Opacity = 0.4;
		}

		protected void OnButtonClick() {
			if (ButtonClick != null) {
				Enabled = true;
				ButtonClick(this, new EventArgs());
			}
		}
	}
}
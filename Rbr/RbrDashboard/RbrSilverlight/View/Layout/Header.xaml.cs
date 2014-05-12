using System;
using System.Windows;
using System.Windows.Controls;
using System.Configuration;
using RbrSiverlight.Delegates;
using RbrSiverlight.View.Toolbar;

namespace RbrSiverlight.View.Layout {
	public partial class Header : UserControl {
		ToolbarButton current;

		public Header() {
			InitializeComponent();
			dashboardLabel.Text = string.Format("Web Reports Dashboard");
		}

		public event NavigateRequestHandler NavigateRequest;

		void ToolbarButton_ButtonClick(object sender, EventArgs e) {
			if (current != null) {
				current.FontWeight = FontWeights.Normal;
				current = (ToolbarButton) sender;
				current.FontWeight = FontWeights.Bold;
			}
			else {
				current = (ToolbarButton) sender;
				current.FontWeight = FontWeights.Bold;
			}
			if (NavigateRequest != null) {
				NavigateRequest(sender, new NavigateRequestArgs(((ToolbarButton) sender).Action, null));
			}
		}
	}
}
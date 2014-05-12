using System.Windows.Media;
using RbrSiverlight.Delegates;
using System.Windows.Controls;

namespace RbrSiverlight.View.Layout {
	public partial class Footer {
		public Footer() {
			InitializeComponent();
			StatusLabel.Text = "";
		}

		public void ContentPageStateChanged(PageStatus pStatus, string pMessage) {
			StatusLabel.Foreground = new SolidColorBrush(Colors.White); 
			switch (pStatus) {
				case PageStatus.LoggingFailed:
					StatusLabel.Foreground = new SolidColorBrush(Colors.Red);
					StatusLabel.Text = "Login ERROR";
					waitIndicator.Stop();
					break;
				case PageStatus.Loading:
					StatusLabel.Text = "Loading...";
					waitIndicator.Stop();
					break;
				case PageStatus.Active:
					StatusLabel.Text = string.Empty;
					waitIndicator.Stop();
					break;
				case PageStatus.RetrievingData:
					StatusLabel.Text = "Please wait - communicating with server...";
					waitIndicator.Start();
					break;
				case PageStatus.Closed:
					StatusLabel.Text = string.Empty;
					waitIndicator.Stop();
					break;
				default:
					StatusLabel.Text = "Unknown Status";
					waitIndicator.Stop();
					break;
			}
		}
	}
}
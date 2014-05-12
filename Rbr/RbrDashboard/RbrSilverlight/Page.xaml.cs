using System.Windows;
using System.Windows.Controls;
using RbrSiverlight.Delegates;
using RbrSiverlight.View;
using RbrSiverlight.View.Dashboard;
using RbrSiverlight.View.Login;

namespace RbrSiverlight {
	public partial class Page : UserControl {
		IContentPage currentPage;
		DashboardPage dash;

		public Page() {
			InitializeComponent();
		}

		void pageLoaded(object pSender, RoutedEventArgs pArgs) {
			showPage(new LoginPage());
		}

		void navigateRequest(object pSender, NavigateRequestArgs pArgs) {
			IContentPage _navigateToPage;

			if (!Globals.IsLoggedIn) {	// Force user to log in first
				_navigateToPage = new LoginPage(pArgs.Action);
				showPage(_navigateToPage);
				return;
			}

			switch (pArgs.Action) {
				case Globals.SET_REPORT_DATE_LAST_HOUR:
					dash.ReportDate = Globals.SET_REPORT_DATE_LAST_HOUR;
					_navigateToPage = dash;
					break;
				case Globals.SET_REPORT_DATE_TODAY:
					dash.ReportDate = Globals.SET_REPORT_DATE_TODAY;
					_navigateToPage = dash;
					break;
				case Globals.SET_REPORT_DATE_LAST24:
					dash.ReportDate = Globals.SET_REPORT_DATE_LAST24;
					_navigateToPage = dash;
					break;
				case Globals.SET_REPORT_DATE_THIS_HOUR:
					dash.ReportDate = Globals.SET_REPORT_DATE_THIS_HOUR;
					_navigateToPage = dash;
					break;
				case Globals.PAGE_DASHBOARD:
					if (dash == null) {
						dash = new DashboardPage();
					}
					dash.ReportDate = Globals.SET_REPORT_DATE_THIS_HOUR;
					_navigateToPage = dash;
					break;
				default:
					if (dash == null) {
						dash = new DashboardPage();
					}
					dash.ReportDate = Globals.SET_REPORT_DATE_THIS_HOUR;
					_navigateToPage = dash;
					break;
			}

			if (_navigateToPage == currentPage && currentPage is DashboardPage) {
				( currentPage as DashboardPage ).ReloadCurrentlyMaximizedPanel();
				return;
			}
			showPage(_navigateToPage);
		}

		void showPage(IContentPage pPageToLoad) {
			if (currentPage != null) {	// Close the current page first
				LayoutRoot.Children.Remove(currentPage.Control);
			}
			currentPage = pPageToLoad;

			if (pPageToLoad != null) {
				currentPage.NavigateRequest += navigateRequest;
				currentPage.PageStatusChange += pageStatusChange;

				// Set page size to fill the available area
				pPageToLoad.Control.Width = double.NaN;
				pPageToLoad.Control.Height = double.NaN;

				// Now display the specified page
				pPageToLoad.Control.SetValue(Grid.RowProperty, 1);
				LayoutRoot.Children.Add(pPageToLoad.Control);
				pPageToLoad.Control.Focus();
			}
		}

		void pageStatusChange(object pSender, PageStatusChangeArgs pArgs) {
			// Send the state change to the footer for display
			Footer.ContentPageStateChanged(pArgs.Status, string.Empty);
		}
	}
}
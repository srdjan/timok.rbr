using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using Blacklight.Controls;
using RbrSiverlight.Delegates;
using RbrSiverlight.Helpers;
using RbrSiverlight.ServiceClient;
using RbrSiverlight.ServiceReference;

namespace RbrSiverlight.View.Dashboard {
	public partial class DashboardPage : IContentPage {
		readonly DashboardServiceClient service;
		DragDockPanel currentlyMaximizedPanel;
		bool customerReportLoaded;
		bool nodeReportLoaded;
		string reportDate;
		bool routeReportLoaded;
		bool trunkReportLoaded;

		public DashboardPage() {
			InitializeComponent();

			service = ServiceFactory.GetService();
			service.GetNodeReportCompleted += getNodeReportCompleted;
			service.GetCustomerReportCompleted += getCustomersReportCompleted;
			service.GetRouteReportCompleted += getRouteReportCompleted;
			service.GetTrunkReportCompleted += getTrunkReportCompleted;
		}

		public string ReportDate {
			set {
				reportDate = value;
				customerReportLoaded = false;
				trunkReportLoaded = false;
				routeReportLoaded = false;
				nodeReportLoaded = false;
				if (currentlyMaximizedPanel == null) {
					RouteReport.PanelState = PanelState.Maximized;
				}
			}
		}

		public event NavigateRequestHandler NavigateRequest;
		public event PageStatusChangeHandler PageStatusChange;

		public UserControl Control {
			get { return this; }
		}

		public void ReloadCurrentlyMaximizedPanel() {
			if (currentlyMaximizedPanel.Name == "NodesReport") {
				getNodesReport(null, null);
			}
			else if (currentlyMaximizedPanel.Name == "CustomerReport") {
				getCustomerReport(null, null);
			}
			else if (currentlyMaximizedPanel.Name == "RouteReport") {
				getRouteReport(null, null);
			}
			else if (currentlyMaximizedPanel.Name == "TrunkReport") {
				getTrunkReport(null, null);
			}
			else {
				throw new Exception("Unknown Panel type");
			}
		}

		//------------------------------ 
		void getNodesReport(object sender, EventArgs e) {
			currentlyMaximizedPanel = NodesReport;
			if (nodeReportLoaded) {
				return;
			}
			nodeReportLoaded = true;
			service.GetNodeReportAsync(reportDate);
			Cursor = Cursors.Wait;
		}

		void getNodeReportCompleted(object pSender, GetNodeReportCompletedEventArgs pArgs) {
			try {
				if (pArgs != null && pArgs.Error == null) {
					if (pArgs.Result.Error.Length > 0) {
						MessageBox.Show(string.Format("Daily Nodes Report: {0}", pArgs.Result.Error), "Error", MessageBoxButton.OK);
					}
					LayoutRoot.DataContext = pArgs.Result;
					grid.DataSource = pArgs.Result.NodeReport;
				}
				else {
					MessageBox.Show("Node Report...", "Error", MessageBoxButton.OK);
				}
			}
			finally {
				Cursor = Cursors.Arrow;
			}
		}

		//------------------------------ 
		void getCustomerReport(object pSender, EventArgs pArgs) {
			currentlyMaximizedPanel = CustomerReport;
			if (customerReportLoaded) {
				return;
			}
			customerReportLoaded = true;
			service.GetCustomerReportAsync(0, reportDate);
			Cursor = Cursors.Wait;
		}

		void getCustomersReportCompleted(object pSender, GetCustomerReportCompletedEventArgs pArgs) {
			try {
				if (pArgs != null && pArgs.Error == null) {
					if (pArgs.Result.Error.Length > 0) {
						MessageBox.Show(string.Format("Customer Report: {0}", pArgs.Result.Error), "Error", MessageBoxButton.OK);
					}
					LayoutRoot.DataContext = pArgs.Result;
					grid1.DataSource = pArgs.Result.CustomerReport;
				}
				else {
					MessageBox.Show("Customer Report...", "Error", MessageBoxButton.OK);
				}
			}
			finally {
				Cursor = Cursors.Arrow;
			}
		}

		//------------------------------ 
		void getRouteReport(object pSender, EventArgs pArgs) {
			currentlyMaximizedPanel = RouteReport;
			if (routeReportLoaded) {
				return;
			}
			routeReportLoaded = true;
			service.GetRouteReportAsync(0, reportDate);
			Cursor = Cursors.Wait;
		}

		void getRouteReportCompleted(object pSender, GetRouteReportCompletedEventArgs pArgs) {
			try {
				if (pArgs != null && pArgs.Error == null) {
					if (pArgs.Result.Error.Length > 0) {
						MessageBox.Show(string.Format("Route Report: {0}", pArgs.Result.Error), "Error", MessageBoxButton.OK);
					}
					LayoutRoot.DataContext = pArgs.Result;
					grid2.DataSource = pArgs.Result.RouteReport;
				}
				else {
					MessageBox.Show("Route Report...", "Error", MessageBoxButton.OK);
				}
			}
			finally {
				Cursor = Cursors.Arrow;
			}
		}

		//------------------------------ 
		void getTrunkReport(object pSender, EventArgs pArgs) {
			currentlyMaximizedPanel = TrunkReport;
			if (trunkReportLoaded) {
				return;
			}
			trunkReportLoaded = true;
			service.GetTrunkReportAsync(0, reportDate);
			Cursor = Cursors.Wait;
		}

		void getTrunkReportCompleted(object pSender, GetTrunkReportCompletedEventArgs pArgs) {
			try {
				if (pArgs != null && pArgs.Error == null) {
					if (pArgs.Result.Error.Length > 0) {
						MessageBox.Show(string.Format("Trunk Report: {0}", pArgs.Result.Error), "Error", MessageBoxButton.OK);
					}
					LayoutRoot.DataContext = pArgs.Result;
					grid3.DataSource = pArgs.Result.TrunkReport;
				}
				else {
					MessageBox.Show("Trunk Report...", "Error", MessageBoxButton.OK);
				}
			}
			finally {
				Cursor = Cursors.Arrow;
			}
		}

		//-------------------------- Export to Excel
		void Button_Click(object pSender, RoutedEventArgs pArgs) {
			var _fsfg = new FpSaveFileDialog(new Uri("FpSaveFileDialogServerSideHandler.aspx", UriKind.Relative));
			var _data = new byte[] {0x48, 0x65, 0x6C, 0x6C, 0x6F, 0x20, 0x57, 0x6F, 0x72, 0x6C, 0x64};
			_fsfg.Save(_data, new FpSaveFileDialog.FpSaveFileDialogOption("RouteReport.csv"));
		}
	}
}
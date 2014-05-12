using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using RbrSiverlight.Delegates;

namespace RbrSiverlight.View.Layout {
	public partial class SummaryListToolbar : UserControl {
		private int m_currentPageNumber = 1;
		private bool m_isGroupingOn;
		private int m_totalPageCount;

		public event ListSearchEventHandler SearchList;
		public event ListPageChangedEventHandler PageChanged;
		public event EventHandler GroupingStateChanged;

		public SummaryListToolbar() {
			InitializeComponent();
			UpdatePageButtons();
		}

		private void FirstPageButton_Click(object sender, RoutedEventArgs e) {
			CurrentPageNumber = 1;
		}

		private void SearchButton_Click(object sender, RoutedEventArgs e) {
			StartSearch();
		}

		private void PreviousPageButton_Click(object sender, RoutedEventArgs e) {
			CurrentPageNumber--;
		}

		private void NextPageButton_Click(object sender, RoutedEventArgs e) {
			CurrentPageNumber++;
		}

		private void LastPageButton_Click(object sender, RoutedEventArgs e) {
			CurrentPageNumber = m_totalPageCount;
		}

		private void SummaryListToolbar_KeyDown(object sender, KeyEventArgs e) {
			if (e.Key == Key.Enter)
				StartSearch();
		}

		private void ShowGroupPanelToggleButton_Click(object sender, RoutedEventArgs e) {
			m_isGroupingOn = (bool) ShowGroupPanelToggleButton.IsChecked;

			if (GroupingStateChanged != null)
				GroupingStateChanged(this, new EventArgs());
		}

		public int CurrentPageNumber {
			get { return m_currentPageNumber; }
			set {
				if (value < 1 || value > m_totalPageCount)
					throw new Exception("Invalid page number");

				if (value != m_currentPageNumber) {
					m_currentPageNumber = value;
					UpdatePageNumberLabel();
					UpdatePageButtons();

					if (PageChanged != null)
						PageChanged(this, new ListPageChangedArgs(m_currentPageNumber));
				}
			}
		}

		public int TotalPageCount {
			get { return m_totalPageCount; }
			set {
				m_totalPageCount = value;

				if (m_currentPageNumber > m_totalPageCount)
					CurrentPageNumber = m_totalPageCount;

				UpdatePageNumberLabel();
				UpdatePageButtons();
			}
		}

		public bool IsGroupingOn {
			get { return m_isGroupingOn; }
			set {
				m_isGroupingOn = value;
				ShowGroupPanelToggleButton.IsChecked = m_isGroupingOn;
			}
		}

		private void StartSearch() {
			m_currentPageNumber = 1; // This avoids the logic of going through the property wrapping it and
			// stops the PageChanged event being called when the page number is set elsewhere in the code to 1

			if (SearchList != null)
				SearchList(this, new ListSearchArgs(SearchTextBox.Text));
		}

		private void UpdatePageNumberLabel() {
			PageNumberLabel.Text = "Page " + m_currentPageNumber + " of " + m_totalPageCount;
		}

		private void UpdatePageButtons() {
			if (m_totalPageCount == 0) {
				FirstPageButton.IsEnabled = false;
				PreviousPageButton.IsEnabled = false;
				NextPageButton.IsEnabled = false;
				LastPageButton.IsEnabled = false;
			}
			else {
				FirstPageButton.IsEnabled = (m_currentPageNumber != 1);
				PreviousPageButton.IsEnabled = (m_currentPageNumber != 1);
				NextPageButton.IsEnabled = (m_currentPageNumber < m_totalPageCount);
				LastPageButton.IsEnabled = (m_currentPageNumber < m_totalPageCount);
			}
		}
	}
}
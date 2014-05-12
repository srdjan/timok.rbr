using System.ComponentModel;

namespace RbrSiverlight.ViewModel {
	public class Sale : INotifyPropertyChanged {
		private int _count;
		public string ChartType { get; set; }

		public int ChartSaleCount {
			get { return _count; }
			set {
				_count = value;
				NotifyPropertyChanged("ChartSaleCount");
			}
		}

		public event PropertyChangedEventHandler PropertyChanged;

		public void NotifyPropertyChanged(string propertyName) {
			if (PropertyChanged != null) {
				PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
			}
		}
	}
}
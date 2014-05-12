using System.Collections.ObjectModel;
using RbrSiverlight.ViewModel;

namespace RbrSiverlight.TestData {
	public class SalesData {
		public static ObservableCollection<Sale> GetData() {
			var sales = new ObservableCollection<Sale>();
			sales.Add(new Sale {ChartSaleCount = 100, ChartType = "Bar"});
			sales.Add(new Sale {ChartSaleCount = 73, ChartType = "Pie"});
			sales.Add(new Sale {ChartSaleCount = 12, ChartType = "Line"});
			sales.Add(new Sale {ChartSaleCount = 24, ChartType = "Spline"});
			sales.Add(new Sale {ChartSaleCount = 72, ChartType = "Line"});
			sales.Add(new Sale {ChartSaleCount = 29, ChartType = "Spline"});
			return sales;
		}
	}
}
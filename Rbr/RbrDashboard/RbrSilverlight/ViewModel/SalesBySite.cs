//using System;
//using System.Collections.ObjectModel;
//using System.ComponentModel;
//using System.Windows.Browser;
//using Ninject.Core;
//using RbrSiverlight.Input;
//using RbrSiverlight.RbrService;

//namespace RbrSiverlight.ViewModel {
//  public class SalesBySite : INotifyPropertyChanged {
//    public event PropertyChangedEventHandler PropertyChanged;
//    ObservableCollection<SalesBySite> sales;
//    readonly IManageHistory<SalesHistoryState> historyManager;
//    readonly IRbrService proxy;
//    long initialBookmarkId = -1;
			
//    [Inject]
//    public SalesBySite(IRbrService pProxy, IManageHistory<SalesHistoryState> pHistoryManager) {
//      historyManager = pHistoryManager;
//      historyManager.Navigate += BrowserNavigate;

//      if (HtmlPage.IsEnabled && HtmlPage.Window.CurrentBookmark.Length > 0) {
//        initialBookmarkId = Convert.ToInt64(HtmlPage.Window.CurrentBookmark.Split('=')[1]);
//      }

//      //sales = new ObservableCollection<SalesBySite>();
//      sales = Sales;

//      proxy = pProxy;
//      //proxy.SaveDivesCompleted += SaveDivesCompleted;
//      //proxy.GetDivesCompleted += GetDivesCompleted;
//      //proxy.DeleteDiveCompleted += DeleteDiveCompleted;

//      //Commands.GetDives.Executed += GetLogExecuted;
//      //Commands.NewDive.Executed += NewLogExecuted;
//      //Commands.SaveDives.Executed += SubmitChangesExecuted;
//      //Commands.DeleteDives.Executed += DeleteDiveExecuted;

//      //NewDive();
//      //Commands.GetDives.Execute();
			
//    }

//    public ObservableCollection<SalesBySite> Sales {
//      get { return sales; }
//      set {
//        if (value != Sales) {
//          sales = value;
//          RaisePropertyChanged("SalesBySite");
//        }
//      }
//    }

//    protected void RaisePropertyChanged(string propertyName) {
//      var propertyChanged = PropertyChanged;
//      if (propertyChanged != null) {
//        propertyChanged(this, new PropertyChangedEventArgs(propertyName));
//      }
//    }

//    void BrowserNavigate(object sender, HistoryEventArgs<SalesHistoryState> e) {
//    }
//  }
//}
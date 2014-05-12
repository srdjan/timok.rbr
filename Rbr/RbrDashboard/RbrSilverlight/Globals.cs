using RbrSiverlight.ServiceReference;

namespace RbrSiverlight {
	public static class Globals {
		public static bool IsLoggedIn;
		public static ReportContext Reportcontext;

		public static bool ValidatePartners {
			get {
				if (Reportcontext.Partners != null && Reportcontext.Partners.Count > 0) {
					return true;
				}
				return false;
			}
		}
		//public static bool ValidateCustomers {
		//  get {
		//    if (Reportcontext.Partners[0].CustomerAccts != null && Reportcontext.Partners[0].CustomerAccts.Count > 0) {
		//      return true;
		//    }
		//    return false;
		//  }
		//}
		//public static bool ValidateCarriers {
		//  get {
		//    if (Reportcontext.Partners[0].CarrierAccts != null && Reportcontext.Partners[0].CarrierAccts.Count > 0) {
		//      return true;
		//    }
		//    return false;
		//  }
		//}
	
		public static string Password = "";
		public static string UserName = "";
		public static string[] UserRoles;

		public const string SET_REPORT_DATE_TODAY = "today";
		public const string SET_REPORT_DATE_THIS_HOUR = "thishour";
		public const string SET_REPORT_DATE_LAST_HOUR = "previoushour";
		public const string SET_REPORT_DATE_LAST24 = "yesterday";

		public const string PAGE_ADMIN_MENU = "GotoAdminMenu";
		public const string PAGE_DASHBOARD = "GotoDashboard";
		public const string PAGE_INVENTORY_LIST = "GotoInventoryList";
		public const string PAGE_LOGIN = "GotoLoginPage";
		public const string PAGE_PRODUCT_DETAILS = "GotoProductDetails";
		public const string PAGE_PURCHASING_LIST = "GotoPurchasingList";
		public const string PAGE_REPORTS_MENU = "GotoReportsMenu";
		public const string PAGE_SALES_LIST = "GotoSalesList";

		public const int LIST_ITEMS_PER_PAGE = 100;
	}
}
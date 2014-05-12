using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using RbrSiverlight.Delegates;
using RbrSiverlight.ServiceClient;
using RbrSiverlight.ServiceReference;

namespace RbrSiverlight.View.Login {
	public partial class LoginPage : IContentPage {
		private readonly string destinationPage = Globals.PAGE_DASHBOARD;

		public LoginPage() {
			InitializeComponent();
		}

		public LoginPage(string pDestinationPage) {
			InitializeComponent();
			destinationPage = pDestinationPage;
		}

		public event NavigateRequestHandler NavigateRequest;
		public event PageStatusChangeHandler PageStatusChange;

		public UserControl Control {
			get { return this; }
		}

		//------------------------------ Private --------------------------------------
		void loginPageLoaded(object sender, RoutedEventArgs e) {
			System.Windows.Browser.HtmlPage.Plugin.Focus();
			UserNameTextbox.Focus();
			//var _tbap = new TextBoxAutomationPeer(UserNameTextbox);
			//( (IValueProvider) _tbap ).SetValue("?");
			// Automatically insert the user name last entered as the login, retrieved from isolated storage
			//UserNameTextbox.Text = ConfigurationSettings.LastUserName;

			if (UserNameTextbox.Text.Length == 0) {
				UserNameTextbox.Focus();
			}
			else {
				PasswordTextbox.Focus();
			}
		}

		void loginPageKeyDown(object sender, KeyEventArgs e) {
			//setPageStatusActive();

			if (e.Key == Key.Enter) {
				startLogin();
			}
		}

		void loginButtonClick(object sender, RoutedEventArgs e) {
			startLogin();
		}

		void startLogin() {
			setPageStatusRetreivingData();

			Globals.UserName = UserNameTextbox.Text;
			Globals.Password = PasswordTextbox.Password;

			var _service = ServiceFactory.GetService();
			_service.LoginCompleted += loginCompleted;
			_service.LoginAsync();
		}

		void loginCompleted(object pSender, LoginCompletedEventArgs pArgs) {
			Globals.IsLoggedIn = false;

			if (pArgs.Error != null) {
				setPageStatusLoggingFailed();
				return;
			}

			Globals.Reportcontext = pArgs.Result;
			if (Globals.Reportcontext == null || !Globals.ValidatePartners) {
				setPageStatusLoggingFailed();
				return;
			}

			Globals.IsLoggedIn = true;
			Globals.UserName = UserNameTextbox.Text;
			Globals.Password = PasswordTextbox.Password;
			Globals.UserRoles = null; //TODO: user.Roles.ToArray();

			//TODO:  Save the user name to isolated storage so it can be inserted next time user loads application
			//ConfigurationSettings.LastUserName = Globals.UserName;

			setPageStatusActive();
			if (NavigateRequest != null) {
				NavigateRequest(this, new NavigateRequestArgs(destinationPage, null));
			}
		}

		//------------------------- helpers -------------------------------------------
		void setPageStatusLoggingFailed() {
			if (PageStatusChange != null) {
				PageStatusChange(this, new PageStatusChangeArgs(PageStatus.LoggingFailed));
			}
		}

		void setPageStatusActive() {
			if (PageStatusChange != null) {
				PageStatusChange(this, new PageStatusChangeArgs(PageStatus.Active));
			}
		}
		void setPageStatusRetreivingData() {
			if (PageStatusChange != null) {
				PageStatusChange(this, new PageStatusChangeArgs(PageStatus.RetrievingData));
			}
		}

		//public static class Mouse {
		//  private const UInt32 MouseEventLeftDown = 0x0002;
		//  private const UInt32 MouseEventLeftUp = 0x0004;
		//  [DllImport("user32.dll")]

		//  private static extern void mouse_event(UInt32 dwFlags, UInt32 dx, UInt32 dy, UInt32 dwData, IntPtr dwExtraInfo);

		//  public static void Click() {
		//    mouse_event(MouseEventLeftDown, 0, 0, 0, IntPtr.Zero);
		//    mouse_event(MouseEventLeftUp, 0, 0, 0, IntPtr.Zero);
		//  }
		//}
	}
}
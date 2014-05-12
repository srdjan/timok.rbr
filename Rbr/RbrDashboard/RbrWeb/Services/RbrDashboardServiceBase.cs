using System.ServiceModel;
using RbrData;
using RbrCommon;
using RbrData.DataContracts;
using RbrWeb.Helpers;

namespace RbrWeb.Services {
	public class RbrDashboardServiceBase {
		protected UserRecord user;
		protected bool loggedIn;

		protected RbrDashboardServiceBase() {
			validateUserCredentials();
		}
	
		internal void validateUserCredentials() {
			var _headers = OperationContext.Current.IncomingMessageHeaders;
			var _headerIndex = _headers.FindHeader("UserCredentials", "urn:security");

			if (_headerIndex != -1) {
				var _encryptedUserCredentials = _headers.GetHeader<string>(_headerIndex);
				var _userCredentials = EncryptionHelper.Decrypt(_encryptedUserCredentials, "password", "-578030019");

				var _tokens = _userCredentials.Split(',');
				if (!string.IsNullOrEmpty(_tokens[0]) && !string.IsNullOrEmpty(_tokens[1])) {
					//TODO: chanck fo rLength > 6
					var _db = new Domain();
					user = _db.GetUser(_tokens[0], _tokens[1]);
					if (user != null) {
						loggedIn = true;
						//var securityPrincipal = new RolePrincipal(new GenericIdentity(_tokens[0]));
						//HttpContext.Current.User = securityPrincipal;
						//Thread.CurrentPrincipal = securityPrincipal;
					}
					else {
						Logger.Log(string.Format("validateUserCredentials: error"));
					}
				}
			}
		}
	}
}
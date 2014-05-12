using System.ServiceModel;
using System.ServiceModel.Channels;
using Microsoft.Silverlight.Samples;
using RbrWeb.Helpers;

namespace RbrSiverlight.ServiceClient {
	// This class adds user credentials to service call's message header, so OperationContextScope is no longer required to wrap the service calls
	public class ServiceMessageInspector : IClientMessageInspector {
		public object BeforeSendRequest(ref Message request, IClientChannel channel) {
			if (Globals.UserName.Length != 0) {		// Add our user credentials to the message headers
			  var header = MessageHeader.CreateHeader("UserCredentials", "urn:security", getUserCredentials(), false);
			  request.Headers.Add(header);
			}
			return null;
		}

		public void AfterReceiveReply(ref Message reply, object correlationState) {}

		//-------------------------------- private --------------------------------------
		static string getUserCredentials() {
			var _str = string.Format("{0},{1}", Globals.UserName, Globals.Password);
			return EncryptionHelper.Encrypt(_str, "password", "-578030019");
		}
	}
}
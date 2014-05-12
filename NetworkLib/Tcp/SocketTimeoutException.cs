using System;

namespace Timok.NetworkLib.Tcp {
	public class SocketTimeoutException: ApplicationException {
		public SocketTimeoutException() {
		}

		public SocketTimeoutException(string pMessage)	: base(pMessage) {
		}

		public SocketTimeoutException(string pMessage, Exception pInner): base(pMessage, pInner) {
		}
	}
}
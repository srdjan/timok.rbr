using System;

namespace Timok.NetworkLib.Tcp {
	public class ConnectionTimeoutException : ApplicationException {
		public ConnectionTimeoutException() {     }
		public ConnectionTimeoutException(string	pMessage) : base(pMessage) {     }
		public ConnectionTimeoutException(string	pMessage, Exception pInner) : base(pMessage, pInner) {     }
	}
}
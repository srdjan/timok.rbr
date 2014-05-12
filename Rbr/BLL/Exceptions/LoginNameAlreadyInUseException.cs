using System;

namespace Timok.Rbr.BLL {

	public class LoginNameAlreadyInUseException : Exception {
		public LoginNameAlreadyInUseException() : 
			base("Login Name already in use, cannot have duplicates.") {
		}

		public LoginNameAlreadyInUseException(string message) : base(message) {
		}

		public LoginNameAlreadyInUseException(string message, Exception innerException) : base(message, innerException) {
		}
	}
}

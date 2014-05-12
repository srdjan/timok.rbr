using System;

namespace Timok.Rbr.BLL {

	public class ANIAlreadyInUseException : Exception {
		private const string defaultMessage = "ANI already in use, cannot have duplicates.";
		
		public ANIAlreadyInUseException() : base(defaultMessage) {
		}

		public ANIAlreadyInUseException(string message) : base(message) {
		}

		public ANIAlreadyInUseException(string message, Exception innerException) : base(message, innerException) {
		}
	}
}

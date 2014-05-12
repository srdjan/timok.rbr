using System;

namespace Timok.Rbr.DAL {

	public class AlternateKeyException : Exception {

		public AlternateKeyException() : base() { }
		public AlternateKeyException(string pMessage) : base(pMessage) { }
		public AlternateKeyException(string pMessage, Exception pInnerException) : base(pMessage, pInnerException) { }

	}
}

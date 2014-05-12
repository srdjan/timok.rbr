using System;

namespace Timok.Rbr.DAL {

	public class PrimaryKeyException : Exception {

		public PrimaryKeyException() : base() { }
		public PrimaryKeyException(string pMessage) : base(pMessage) { }
		public PrimaryKeyException(string pMessage, Exception pInnerException) : base(pMessage, pInnerException) { }

	}
}

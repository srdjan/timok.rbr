using System;
using Timok.Rbr.Core.Config;

namespace Timok.Rbr.Core {
	public class RbrException : Exception {
		const string head = "RbrResult: ";
		public RbrResult RbrResult;

		public RbrException(RbrResult pRbrResult, string pSource, string pMessage) : base(pMessage) {
			RbrResult = pRbrResult;
			base.Source = pSource;
		}

		public RbrException(RbrResult pRbrResult, string pMessage) : base(pMessage) {
			RbrResult = pRbrResult;
		}

		public override string Message { get { return string.Format("RbrResult: {0}, Message: {1}", RbrResult, base.Message); } }

		public override string ToString() {
			return head + RbrResult + Environment.NewLine + base.ToString();
		}
	}
}
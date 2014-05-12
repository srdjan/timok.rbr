namespace Timok.Rbr.Core {
	public static class Helper {
		public static void CheckIP(string pCallingIp) {
			if (pCallingIp == null) {
				throw new RbrException(RbrResult.OrigEP_IPInvalid, "Helper.CheckIP", string.Format("Calling IP is null, {0}", pCallingIp));
			}
			if (pCallingIp.Length < 7) {
				throw new RbrException(RbrResult.OrigEP_IPInvalid, "Helper.CheckIP", string.Format("Calling IP length invalid: {0}", pCallingIp));
			}
		}

		public static void CheckDestNumber(string pDestNumber) {
			if (pDestNumber == null) {
				throw new RbrException(RbrResult.DialedNumber_Invalid, "Helper.CheckDestNumber", "DestNumber is null");
			}
			if (pDestNumber.Length < 5) {
				throw new RbrException(RbrResult.DialedNumber_Invalid, "Helper.CheckDestNumber", "DestNumber length invalid");
			}
		}
	}
}
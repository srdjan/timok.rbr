namespace Timok.Core {
	public class RegPair {
		public int IPAsInt;
		public int Hash;

		public RegPair(int pIpSaltHash, int pIPAsInt) {
			IPAsInt = pIPAsInt;
			Hash = pIpSaltHash;
		}

		//public override string ToString() {
		//  return string.Format("Hash: {0}, IP: {1}={2}", Hash, IPUtil.ToString(IPAsInt), IPAsInt);
		//}
	}
}
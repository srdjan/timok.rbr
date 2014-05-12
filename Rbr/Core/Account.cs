
namespace Timok.Rbr.Core {
	public class Account {
		public int RetialAcctId;
		public bool Obtained;
		public long SerialNumber;
		public decimal StartingBalance;
		public decimal Balance;
		public int BonusMinutes;
		public bool NeverUsed;

		public Account() {
			Obtained = false;
			RetialAcctId = 0;
			SerialNumber = 0;
			Balance = StartingBalance = decimal.Zero;
			BonusMinutes = 0;
			NeverUsed = false;
		}
	}
}
using Timok.Rbr.DAL.RbrDatabase;

namespace Timok.Rbr.BLL.DOM {
	public sealed class PhoneCard : RetailAccount {
		readonly PhoneCardRow phoneCardRow;

		public PhoneCard(RetailAccountRow pRetailAcctRow, PhoneCardRow pPhoneCardRow) : base(pRetailAcctRow) {
			phoneCardRow = pPhoneCardRow;
		}

		public override short ServiceId { get { return phoneCardRow.Service_id; } } //pk-1,2
		public long Pin { get { return phoneCardRow.Pin; } } //pk-1,2
		public override long SerialNumber { get { return phoneCardRow.Serial_number; } }

		public override void UpdateUsage() {
			using (var _db = new Rbr_Db()) {
				if (phoneCardRow.IsDate_first_usedNull) {
					_db.PhoneCardCollection.UpdateFirstUsedAndLastUsed(ServiceId, Pin);
				}
				else {
					_db.PhoneCardCollection.UpdateLastUsed(ServiceId, Pin);
				}
			}
		}
	}
}
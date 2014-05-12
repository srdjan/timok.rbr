using Timok.Rbr.DAL.RbrDatabase;

namespace Timok.Rbr.BLL.DOM {
	public sealed class Residential : RetailAccount {
		readonly ResidentialPSTNRow residentialPSTNRow;

		public override short ServiceId { get { return residentialPSTNRow.Service_id; } } //pk-1,2
		public long ANI { get { return residentialPSTNRow.ANI; } } //pk-1,2
		public override long SerialNumber { get { return residentialPSTNRow.ANI; } } //TODO: add Serail to all Retail SubAccts?

		public Residential(RetailAccountRow pRetailAcctRow, ResidentialPSTNRow pResidentialRow) : base(pRetailAcctRow) {
			residentialPSTNRow = pResidentialRow;
		}

		//--------------------------------- Public ----------------------------------------------
		public override void UpdateUsage() {
			using (var _db = new Rbr_Db()) {
				if (residentialPSTNRow.IsDate_first_usedNull) {
					_db.ResidentialPSTNCollection.UpdateFirstUsedAndLastUsed(ServiceId, ANI);
				}
				else {
					_db.ResidentialPSTNCollection.UpdateLastUsed(ServiceId, ANI);
				}
			}
		}
	}
}
using System;
using Timok.Rbr.Core.Config;
using Timok.Core;

namespace Timok.Rbr.DTO {

	[Serializable]
	public class BalanceAdjustmentReasonDto {

    public const string BalanceAdjustmentReasonId_PropName = "BalanceAdjustmentReasonId";
    public const string Description_PropName = "Description";

		public int BalanceAdjustmentReasonId { get; set; }

		public string Description { get; set; }

		public BalanceAdjustmentReasonType BalanceAdjustmentReasonType { get; set; }

    //NOTE: compare object's values (not refs)
    public override bool Equals(object obj) {
      if (obj == null || obj.GetType() != GetType())
        return false;

      return ObjectComparer.AreEqual(this, obj);
    }

    public override int GetHashCode() {
      //TODO: finish it, get hashes for all fields
      return BalanceAdjustmentReasonId.GetHashCode();
    }
  }
}

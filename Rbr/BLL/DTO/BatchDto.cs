using System;

using Timok.Core;
using Timok.Rbr.Core.Config;

namespace Timok.Rbr.DTO {

  [Serializable]
  public class BatchDto {
    public const string BatchId_PropName = "BatchId";
    public const string InventoryStatus_PropName = "InventoryStatus";
    public const string Denomination_PropName = "Denomination";
    public const string Selected_PropName = "Selected";
    public const string BatchSize_PropName = "BatchSize";
    public const string FirstSerial_PropName = "FirstSerial";
    public const string LastSerial_PropName = "LastSerial";
    public const string DateRequested_PropName = "DateRequested";
    public const string CustomerAcctId_PropName = "CustomerAcctId";

  	public int BatchId { get; set; }

  	public InventoryStatus InventoryStatus { get; set; }

  	public long FirstSerial { get; set; }

  	public long LastSerial { get; set; }

  	public short CustomerAcctId { get; set; }

  	public bool Selected { get; set; }

  	#region parents' props

    #region Lot

  	public int LotId { get; set; }

  	public short ServiceId { get; set; }

  	public decimal Denomination { get; set; }

  	#endregion

    #region Generation Request

  	public int RequestId { get; set; }

  	public DateTime DateRequested { get; set; }

  	public DateTime DateToProcess { get; set; }

  	public DateTime DateCopleted { get; set; }

  	public int NumberOfBatches { get; set; }

  	public int BatchSize { get; set; }

  	#endregion Generation Request

    #region Box

  	public int BoxId { get; set; }

  	#endregion Box

    #endregion parents' props

  	//NOTE: compare object's values (not refs)
    public override bool Equals(object obj) {
      if (obj == null || obj.GetType() != GetType())
        return false;

      return ObjectComparer.AreEqual(this, obj);
    }

    public override int GetHashCode() {
      //TODO: finish it, get hashes for all fields
      return BatchId.GetHashCode();
    }
  }
}

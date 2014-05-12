using System;

using Timok.Core;
using Timok.Rbr.Core.Config;

namespace Timok.Rbr.DTO {
  [Serializable]
  public class CustomerSupportGroupDto {
    public const string GroupId_PropName = "GroupId";
    public const string Description_PropName = "Description";
    public const string GroupRole_PropName = "GroupRole";
    public const string MaxAmount_PropName = "MaxAmount";
    public const string AllowStatusChange_PropName = "AllowStatusChange";

  	public int GroupId { get; set; }

  	public string Description { get; set; }

  	public CustomerSupportGroupRole GroupRole { get; set; }

  	public decimal MaxAmount { get; set; }

  	public bool AllowStatusChange { get; set; }

  	public int VendorId { get; set; }

  	public CustomerSupportGroupDto() { Description = string.Empty; }

  	//NOTE: compare object's values (not refs)
    public override bool Equals(object obj) {
      if (obj == null || obj.GetType() != GetType())
        return false;

      return ObjectComparer.AreEqual(this, obj);
    }

    public override int GetHashCode() {
      //TODO: finish it, get hashes for all fields
      return GroupId.GetHashCode();
    }
  }
}

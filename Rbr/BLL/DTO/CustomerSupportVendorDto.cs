using System;

using Timok.Core;
using Timok.Rbr.Core;

namespace Timok.Rbr.DTO {

  [Serializable]
  public class CustomerSupportVendorDto {
    public const string VendorId_PropName = "VendorId";
    public const string Name_PropName = "Name";

    private int vendorId;
    public int VendorId {
      get { return vendorId; }
      set { vendorId = value; }
    }

    private string name = string.Empty;
    public string Name {
      get { return name; }
      set { name = value; }
    }

    private ContactInfoDto contactInfo;
    public ContactInfoDto ContactInfo {
      get { return contactInfo; }
      set { contactInfo = value; }
    }

    public CustomerSupportVendorDto() { }


    //NOTE: compare object's values (not refs)
    public override bool Equals(object obj) {
      if (obj == null || obj.GetType() != this.GetType())
        return false;

      return ObjectComparer.AreEqual(this, obj);
    }

    public override int GetHashCode() {
      //TODO: finish it, get hashes for all fields
      return this.vendorId.GetHashCode();
    }
  }
}

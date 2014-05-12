using System;

using Timok.Core;

namespace Timok.Rbr.DTO {

  [Serializable]
  public class CustomerAcctSupportMapDto {
    public const string CustomerAcctId_PropName = "CustomerAcctId";
    public const string VendorId_PropName = "VendorId";
    public const string CustomerAcctName_PropName = "CustomerAcctName";
    public const string ServiceType_PropName = "ServiceType";
    public const string RetailType_PropName = "RetailType";
    public const string Assigned_PropName = "Assigned";

    private short customerAcctId;
    public short CustomerAcctId {
      get { return customerAcctId; }
      set { customerAcctId = value; }
    }

    private int vendorId;
    public int VendorId {
      get { return vendorId; }
      set { vendorId = value; }
    }

    private CustomerAcctDto customerAcct;
    public CustomerAcctDto CustomerAcct {
      get { return customerAcct; }
      set { customerAcct = value; }
    }

    public string CustomerAcctName {
      get { return customerAcct.Name; }
    }

    public string ServiceType {
      get { return customerAcct.ServiceType.ToString(); }
    }

    public string RetailType {
      get { return customerAcct.RetailType.ToString(); }
    }

    private bool assigned;
    public bool Assigned {
      get { return assigned; }
      set { assigned = value; }
    }

    public CustomerAcctSupportMapDto() { }

    public CustomerAcctSupportMapDto(short pCustomerAcctId, int pVendorId, CustomerAcctDto pCustomerAcct, bool pAssigned) {
      customerAcctId = pCustomerAcctId;
      vendorId = pVendorId;
      customerAcct = pCustomerAcct;
      assigned = pAssigned;
    }

    //NOTE: compare object's values (not refs)
    public override bool Equals(object obj) {
      if (obj == null || obj.GetType() != this.GetType())
        return false;

      return ObjectComparer.AreEqual(this, obj);
    }

    public override int GetHashCode() {
      //TODO: finish it, get hashes for all fields
      return string.Concat(customerAcctId, "-", vendorId).GetHashCode();
    }
  }
}

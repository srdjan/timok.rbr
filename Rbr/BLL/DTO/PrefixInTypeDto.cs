using System;
using System.Collections.Generic;
using System.Text;
using Timok.Core;

namespace Timok.Rbr.DTO {

  [Serializable]
  public class PrefixInTypeDto {
    public const string PrefixInTypeId_PropName = "PrefixInTypeId";
    public const string Description_PropName = "Description";
    public const string Length_PropName = "Length";
    public const string Delimiter_PropName = "Delimiter";

    private short prefixInTypeId;
    public short PrefixInTypeId {
      get { return prefixInTypeId; }
      set { prefixInTypeId = value; }
    }

    private string description;
    public string Description {
      get { return description; }
      set { description = value; }
    }

    private byte length;
    public byte Length {
      get { return length; }
      set { length = value; }
    }

    private byte delimiter;
    public byte Delimiter {
      get { return delimiter; }
      set { delimiter = value; }
    }

    public PrefixInTypeDto() { }

    //NOTE: compare object's values (not refs)
    public override bool Equals(object obj) {
      if (obj == null || obj.GetType() != this.GetType())
        return false;

      return ObjectComparer.AreEqual(this, obj);
    }

    public override int GetHashCode() {
      //TODO: finish it, get hashes for all fields
      return this.prefixInTypeId.GetHashCode();
    }

  }
}

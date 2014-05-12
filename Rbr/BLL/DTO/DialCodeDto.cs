using System;
using Timok.Core;

namespace Timok.Rbr.DTO {

  [Serializable]
  public class DialCodeDto {
    public const string DialCodeString_PropName = "DialCodeString";
    public const string DialCodeString_DisplayName = "Code";

    public string DialCodeString {
      get { return code.ToString(); }
    }

    private int callingPlanId;
    public int CallingPlanId {
      get { return callingPlanId; }
      set { callingPlanId = value; }
    }

    private long code;
    public long Code {
      get { return code; }
      set { code = value; }
    }

    private int baseRouteId;
    public int BaseRouteId {
      get { return baseRouteId; }
      set { baseRouteId = value; }
    }

    private int version;
    public int Version {
      get { return version; }
      set { version = value; }
    }

    //NOTE: compare object's values (not refs)
    public override bool Equals(object obj) {
      if (obj == null || obj.GetType() != this.GetType())
        return false;

      return ObjectComparer.AreEqual(this, obj);
    }

    public override int GetHashCode() {
      //TODO: finish it, get hashes for all fields
      return string.Concat(callingPlanId.ToString("d8-"), code).GetHashCode();
    }
  }
}

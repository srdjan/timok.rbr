using System;
using Timok.Rbr.Core.Config;
using Timok.Core;

namespace Timok.Rbr.DTO {

  [Serializable]
  public class PlatformDto : DTOBase {
    public const string PlatformId_PropName = "PlatformId";
    public const string Location_PropName = "Location";
    public const string Status_PropName = "Status";
    public const string PlatformConfig_PropName = "PlatformConfig";

  	public short PlatformId { get; set; }

  	public string Location { get; set; }

  	public Status Status { get; set; }

  	public PlatformConfig PlatformConfig { get; set; }

  	public override bool Equals(object obj) {
      if (obj == null || obj.GetType() != GetType())
        return false;

      return ObjectComparer.AreEqual(this, obj);
    }

    public override int GetHashCode() {
      return PlatformId.GetHashCode();
    }
  }
}
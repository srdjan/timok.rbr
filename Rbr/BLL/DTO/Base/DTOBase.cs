using System;
using System.ComponentModel;
using Timok.Core;

namespace Timok.Rbr.DTO {

  [Serializable]
	public abstract class DTOBase {

    public abstract override bool Equals(object obj);
    public abstract override int GetHashCode();
  }
}

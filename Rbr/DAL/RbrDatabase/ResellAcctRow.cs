// <fileinfo name="ResellAcctRow.cs">
//		<copyright>
//			Copyright © 2002-2007 Timok ES LLC. All rights reserved.
//		</copyright>
//		<remarks>
//			You can update this source code manually. If the file
//			already exists it will not be rewritten by the generator.
//		</remarks>
//		<generator rewritefile="False" infourl="http://www.SharpPower.com">RapTier</generator>
// </fileinfo>

using System;
using Timok.Rbr.DAL.RbrDatabase.Base;
using System.Runtime.Serialization;
using Timok.Rbr.Core.Config;

namespace Timok.Rbr.DAL.RbrDatabase {
  /// <summary>
  /// Represents a record in the <c>ResellAcct</c> table.
  /// </summary>
  [Serializable]
  public class ResellAcctRow : ResellAcctRow_Base {

    public CommisionType CommisionType {
      get { return (CommisionType) Commision_type; }
      set { Commision_type = (byte) value; }
    }

    public bool PerRoute {
      get { return Per_route == 0 ? false : true; }
      set { Per_route = (byte) ((value) ? 1 : 0); }
    }
    /// <summary>
    /// Initializes a new instance of the <see cref="ResellAcctRow"/> class.
    /// </summary>
    public ResellAcctRow() {
      // EMPTY
    }

    public override bool Equals(object obj) {
      return base.Equals(obj);
    }

    public override int GetHashCode() {
      return base.GetHashCode();
    }

  } // End of ResellAcctRow class
} // End of namespace

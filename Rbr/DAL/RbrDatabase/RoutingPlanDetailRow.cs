// <fileinfo name="RoutingPlanDetailRow.cs">
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
  /// Represents a record in the <c>RoutingPlanDetail</c> table.
  /// </summary>
  [Serializable]
  public class RoutingPlanDetailRow : RoutingPlanDetailRow_Base {

    public RoutingAlgorithmType Algorithm {
      get { return (RoutingAlgorithmType) this.Routing_algorithm; }
      set { Routing_algorithm = (byte) value; }
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="RoutingPlanDetailRow"/> class.
    /// </summary>
    public RoutingPlanDetailRow() {
      // EMPTY
    }

    public override bool Equals(object obj) {
      return base.Equals(obj);
    }

    public override int GetHashCode() {
      return base.GetHashCode();
    }

  } // End of RoutingPlanDetailRow class
} // End of namespace

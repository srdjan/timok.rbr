// <fileinfo name="TerminationRouteViewRow.cs">
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
  /// Represents a record in the <c>TerminationRouteView</c> view.
  /// </summary>
  [Serializable]
  public class TerminationRouteViewRow : TerminationRouteViewRow_Base {

    public bool PartialCoverage {
      get { return Partial_coverage > 0; }
    }

    public RatingType RatingType {
      get { return (RatingType) Rating_type; }
    }
    /// <summary>
    /// Initializes a new instance of the <see cref="TerminationRouteViewRow"/> class.
    /// </summary>
    public TerminationRouteViewRow() {
      // EMPTY
    }

    public override bool Equals(object obj) {
      return base.Equals(obj);
    }

    public override int GetHashCode() {
      return base.GetHashCode();
    }

  } // End of TerminationRouteViewRow class
} // End of namespace

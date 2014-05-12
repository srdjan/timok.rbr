// <fileinfo name="CarrierRouteRow.cs">
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
using System.Xml.Serialization;

namespace Timok.Rbr.DAL.RbrDatabase {
  /// <summary>
  /// Represents a record in the <c>CarrierRoute</c> table.
  /// </summary>
  [Serializable]
  public class CarrierRouteRow : CarrierRouteRow_Base {

    public const string RouteStatus_PropName = "RouteStatus";
    public const string RouteStatus_DisplayName = "Status";

    [XmlIgnore]
    public Status RouteStatus {
      get { return (Status) this.Status; }
      set { this.Status = (byte) value; }
    }

    public bool IsDefault {
      get { return Carrier_route_id == -Carrier_acct_id; }
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="CarrierRouteRow"/> class.
    /// </summary>
    public CarrierRouteRow() {
      // EMPTY
    }

    public override bool Equals(object obj) {
      return base.Equals(obj);
    }

    public override int GetHashCode() {
      return base.GetHashCode();
    }

  } // End of CarrierRouteRow class
} // End of namespace

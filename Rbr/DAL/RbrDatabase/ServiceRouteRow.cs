// <fileinfo name="ServiceRouteRow.cs">
//		<copyright>
//			Copyright © 2002-2006 Timok ES LLC. All rights reserved.
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
using Timok.Rbr.Core;
using System.Xml.Serialization;

namespace Timok.Rbr.DAL.RbrDatabase {
	/// <summary>
	/// Represents a record in the <c>ServiceRoute</c> table.
	/// </summary>
	[Serializable]
	public class ServiceRouteRow : ServiceRouteRow_Base {

    public const string RoutingAlgorithm_PropName = "RoutingAlgorithm";
    public const string RoutingAlgorithm_DisplayName = "Routing Algorithm";

    public const string WithBonusMinutes_PropName = "WithBonusMinutes";
    public const string WithBonusMinutes_DisplayName = "With Bonus Minutes";

    public const string BonusMinutesType_PropName = "BonusMinutesType";
    public const string BonusMinutesType_DisplayName = "Bonus Minutes Type";

    public const string RouteStatus_PropName = "RouteStatus";
    public const string RouteStatus_DisplayName = "Status";

    [XmlIgnore]
    public Status RouteStatus {
      get { return (Status) this.Status; }
      set { this.Status = (byte) value; }
    }

    [XmlIgnore]
    public bool WithBonusMinutes {
      get { return (BonusMinutesType != BonusMinutesType.None); }
    }

    [XmlIgnore]
    public BonusMinutesType BonusMinutesType {
      get { return (BonusMinutesType) this.Bonus_minutes_type; }
      set { this.Bonus_minutes_type = (byte) value; }
    }

    //TODO: NEW DAL
    //[XmlIgnore]
    //public RoutingAlgorithmType RoutingAlgorithm {
    //  get { return (RoutingAlgorithmType) this.Algorithm; }
    //  set { this.Algorithm = (byte) value; }
    //}

    /// <summary>
		/// Initializes a new instance of the <see cref="ServiceRouteRow"/> class.
		/// </summary>
		public ServiceRouteRow(){
			// EMPTY
		}

		public override bool Equals(object obj) {
			return base.Equals(obj);
		}

		public override int GetHashCode() {
			return base.GetHashCode ();
		}

	} // End of ServiceRouteRow class
} // End of namespace

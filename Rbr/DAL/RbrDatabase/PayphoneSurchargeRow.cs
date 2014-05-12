// <fileinfo name="PayphoneSurchargeRow.cs">
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
	/// Represents a record in the <c>PayphoneSurcharge</c> table.
	/// </summary>
	[Serializable]
	public class PayphoneSurchargeRow : PayphoneSurchargeRow_Base {
    public const string SurchargeType_PropName = "SurchargeType";
    public const string SurchargeType_DisplayName = "Surcharge Type";

    public SurchargeType SurchargeType {
      get { return (SurchargeType) this.Surcharge_type; }
      set { this.Surcharge_type = (byte) value; }
    }

    public new decimal Surcharge {
      get { return base.Surcharge; }
      set {
        if (value > 99.9999999M) {
          base.Surcharge = decimal.Zero;
        }
        else if (value < decimal.Zero) {
          base.Surcharge = decimal.Zero;
        }
        else {
          base.Surcharge = value;
        }
      }
    }
    /// <summary>
		/// Initializes a new instance of the <see cref="PayphoneSurchargeRow"/> class.
		/// </summary>
		public PayphoneSurchargeRow(){
			// EMPTY
		}

		public override bool Equals(object obj) {
			return base.Equals(obj);
		}

		public override int GetHashCode() {
			return base.GetHashCode ();
		}

	} // End of PayphoneSurchargeRow class
} // End of namespace

// <fileinfo name="CarrierAcctRow.cs">
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
using System.Runtime.Serialization;
using System.Xml.Serialization;
using Timok.Rbr.DAL.RbrDatabase.Base;
using Timok.Rbr.Core.Config;

namespace Timok.Rbr.DAL.RbrDatabase {
	/// <summary>
	/// Represents a record in the <c>CarrierAcct</c> table.
	/// </summary>
	[Serializable]
	public class CarrierAcctRow : CarrierAcctRow_Base {
		public const string AccountStatus_PropName = "AccountStatus";
		public const string AccountStatus_DisplayName = "Status";

		public Status AccountStatus {
			get { return (Status)this.Status; }
			set { this.Status = (byte) value; }
		}
		
		public bool WithPrefix {
			get { return (this.Prefix_out != null && this.Prefix_out.Length > 0);}
		}

		public bool Strip1plus {
			get {return this.Strip_1plus > 0;}
			set { this.Strip_1plus = (short)((value == true) ? 1 : 0); }
		}

    public RatingType RatingType {
      get { return (RatingType) this.Rating_type; }
      set { this.Rating_type = (byte) (value); }
    }

    public bool IsRatingEnabled {
      get { return this.RatingType != RatingType.Disabled; }
    }

		public short MaxCallLength {
			get { return Max_call_length; }
			set { Max_call_length = (short) value; }
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="CarrierAcctRow"/> class.
		/// </summary>
		public CarrierAcctRow(){
			// EMPTY
		}

		public bool IsNameChanged(CarrierAcctRow pOriginal) {
			return this.Name != pOriginal.Name;
		}

		public bool IsDeactivated(CarrierAcctRow pOriginal) {
			return (this.AccountStatus != pOriginal.AccountStatus && this.AccountStatus != Rbr.Core.Config.Status.Active);
		}

		public override bool Equals(object obj) {
			return base.Equals(obj);
		}

		public override int GetHashCode() {
			return base.GetHashCode ();
		}

		public override string ToString() {
			return this.Name;
		}


	} // End of CarrierAcctRow class
} // End of namespace

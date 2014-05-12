// <fileinfo name="CustomerAcctRow.cs">
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
using Timok.Rbr.Core.Config;
using System.Runtime.Serialization;
using System.Xml.Serialization;

namespace Timok.Rbr.DAL.RbrDatabase {
	/// <summary>
	/// Represents a record in the <c>CustomerAcct</c> table.
	/// </summary>
	[Serializable]
	public class CustomerAcctRow : CustomerAcctRow_Base {

		public const string SaleChannel_PropName = "SaleChannel";
		public const string SaleChannel_DisplayName = "Sale Channel";
		public const string AccountStatus_PropName = "AccountStatus";
		public const string AccountStatus_DisplayName = "Status";
		public const string ConcurrentUse_PropName = "ConcurrentUse";
		public const string ConcurrentUse_DisplayName = "Concurrent Use";

    public const string IsPrepaid_PropName = "IsPrepaid";
    public const string IsPrepaid_DisplayName = "Prepaid";
    public const string RetailMarkupType_PropName = "RetailMarkupType";
    public const string RetailMarkupType_DisplayName = "Retail Markup Type";


		[XmlIgnore]
		public bool ConcurrentUse {
			get {
				return (this.Concurrent_use > 0);
			}
			set {
				if (value == true) {
					this.Concurrent_use = 1;
				}
				else {
					this.Concurrent_use = 0;
				}
			}
		}

    public bool IsPrepaid {
      get { return (this.Is_prepaid > 0); }
      set { this.Is_prepaid = (byte) ((value == true) ? 1 : 0); }
    }

		public bool BonusMinutesEnabled {
			get {
				return (
					this.Default_bonus_minutes_type == (byte)BonusMinutesType.One_Time 
					||
          this.Default_bonus_minutes_type == (byte) BonusMinutesType.Re_Occuring 
					);
			}
		}

		[XmlIgnore]
    public BonusMinutesType DefaultBonusMinutesType {
			get {
        return ((BonusMinutesType) this.Default_bonus_minutes_type);
			}
			set {
        this.Default_bonus_minutes_type = (byte) value;
			}
		}
    
    [XmlIgnore]
		public bool WithPrefixes { 
			get {
				return
					this.Prefix_in_type_id != 0;//Configuration.Main.PrefixTypeId_NoPrefixes;
					//&& this.Prefix_in_type_id != Configuration.Main.PrefixTypeId_Frontend;
			}
		}

		[XmlIgnore]
		public bool AllowRerouting {
			get { return this.Allow_rerouting == 1; }
			set { this.Allow_rerouting = (byte)((value == true) ? 1 : 0); }
		}

		[XmlIgnore]
		public Status AccountStatus {
			get { return (Status)this.Status; }
			set { this.Status = (byte) value; }
		}

		[XmlIgnore]
    public MarkupType RetailMarkupType {
      get { return (MarkupType) this.Retail_markup_type; }
			set { this.Retail_markup_type = (byte) value; }
		}
		
		/// <summary>
		/// Initializes a new instance of the <see cref="CustomerAcctRow"/> class.
		/// </summary>
		public CustomerAcctRow() {
			// EMPTY
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
	} // End of CustomerAcctRow class
} // End of namespace

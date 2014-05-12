// <fileinfo name="ServiceRow.cs">
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
using Timok.Rbr.DAL.RbrDatabase.Base;
using Timok.Rbr.Core.Config;

namespace Timok.Rbr.DAL.RbrDatabase {
	/// <summary>
	/// Represents a record in the <c>Service</c> table.
	/// </summary>
	[Serializable]
	public class ServiceRow : ServiceRow_Base {
    public const string ServiceType_PropName = "ServiceType";
    public const string ServiceType_DisplayName = "Type";

		public const string IsShared_PropName = "IsShared";
		public const string IsShared_DisplayName = "Shared";

    public const string RatingType_PropName = "RatingType";
		public const string RatingType_DisplayName = "Rating Type";
		
    public const string RetailType_PropName = "RetailType";
		public const string RetailType_DisplayName = "Retail Type";
    
    public const string PayphoneSurcharge_PropName = "PayphoneSurcharge";
    public const string PayphoneSurcharge_DisplayName = "Apply Payphone Surcharge";
    
    public const string DisplayName_PropName = "DisplayName";
    public const string DisplayName_DisplayName = "Name";

		public string DisplayName {
			get {
        if (IsShared) {
          return this.Name;
        }
        else {
          return this.Name.Replace(AppConstants.CustomerServiceNamePrefix, string.Empty);
        }
      }
		}

    public ServiceType ServiceType {
      get { return (ServiceType) this.Type; }
			set { this.Type = (byte) value; }
		}

		public bool IsShared {
			get { return this.Is_shared != 0; }
			set { this.Is_shared = (byte) ((value) ? 1 : 0); }
		}

		public bool IsDedicated {
			get { return ! IsShared; }
		}

		public RatingType RatingType {
			get { return (RatingType) this.Rating_type; }
			set { this.Rating_type = (byte) (value); }
		}

		public bool IsRatingEnabled {
			get { return this.RatingType != RatingType.Disabled; }
		}

		public RetailType RetailType {
			get { return (RetailType) this.Retail_type; }
			set { this.Retail_type = (int) value; }
		}

    public bool PayphoneSurcharge {
			get { return ! this.IsPayphone_surcharge_idNull; }
		}

    public BalancePromptType BalancePromptType {
      get { return (BalancePromptType) this.Balance_prompt_type; }
			set { this.Balance_prompt_type = (byte) value; }
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="ServiceRow"/> class.
		/// </summary>
		public ServiceRow(){
			// EMPTY
		}

		public override bool Equals(object obj) {
			return base.Equals(obj);
		}

		public override int GetHashCode() {
			return base.GetHashCode ();
		}

	} // End of ServiceRow class
} // End of namespace

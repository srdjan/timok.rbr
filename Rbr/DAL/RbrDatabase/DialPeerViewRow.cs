// <fileinfo name="DialPeerViewRow.cs">
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
	/// Represents a record in the <c>DialPeerView</c> view.
	/// </summary>
	[Serializable]
	public class DialPeerViewRow : DialPeerViewRow_Base {
		public const string CustomerAccountStatus_PropName = "CustomerAccountStatus";
		public const string CustomerAccountStatus_DisplayName = "Status";
		public const string ServiceRetailType_PropName = "ServiceRetailType";
		public const string ServiceRetailType_DisplayName = "Service Retail Type";
		public const string ServiceType_PropName = "ServiceType";
		public const string ServiceType_DisplayName = "Service Type";

		public ServiceType ServiceType {
      get { return (ServiceType) this.Service_type; }
		}

		public Status CustomerAccountStatus {
			get { return (Status) this.Customer_acct_status; }
		}

		public RetailType ServiceRetailType {
			get { return (RetailType) this.Service_retail_type; }
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="DialPeerViewRow"/> class.
		/// </summary>
		public DialPeerViewRow(){
			// EMPTY
		}

		public override bool Equals(object obj) {
			return base.Equals(obj);
		}

		public override int GetHashCode() {
			return base.GetHashCode ();
		}

	} // End of DialPeerViewRow class
} // End of namespace

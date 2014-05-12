// <fileinfo name="OutDialPeerViewRow.cs">
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
	/// Represents a record in the <c>OutDialPeerView</c> view.
	/// </summary>
	[Serializable]
	public class OutDialPeerViewRow : OutDialPeerViewRow_Base {
		public const string CarrierAccountStatus_PropName = "CarrierAccountStatus";
		public const string CarrierAccountStatus_DisplayName = "Status";

		public Status CarrierAccountStatus {
			get { return (Status) this.Carrier_acct_status; }
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="OutDialPeerViewRow"/> class.
		/// </summary>
		public OutDialPeerViewRow(){
			// EMPTY
		}

		public override bool Equals(object obj) {
			return base.Equals(obj);
		}

		public override int GetHashCode() {
			return base.GetHashCode ();
		}

	} // End of OutDialPeerViewRow class
} // End of namespace

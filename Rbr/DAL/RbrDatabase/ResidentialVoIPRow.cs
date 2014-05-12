// <fileinfo name="ResidentialVoIPRow.cs">
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
	/// Represents a record in the <c>ResidentialVoIP</c> table.
	/// </summary>
	[Serializable]
	public class ResidentialVoIPRow : ResidentialVoIPRow_Base {
		public const string AccountStatus_PropName = "AccountStatus";
		public const string AccountStatus_DisplayName = "Status";
		public const string AllowInboundCalls_PropName = "AllowInboundCalls";
		public const string AllowInboundCalls_DisplayName = "AllowInboundCalls";

		public Status AccountStatus {
			get { return (Status)this.Status; }
			set { this.Status = (byte) value; }
		}

		public bool AllowInboundCalls {
			get { return this.Allow_inbound_calls > 0; }
			set { this.Allow_inbound_calls = (value == true) ? (byte) 1 : (byte) 0 ; }
		}
		
		/// <summary>
		/// Initializes a new instance of the <see cref="ResidentialVoIPRow"/> class.
		/// </summary>
		public ResidentialVoIPRow(){
			// EMPTY
		}

		public override bool Equals(object obj) {
			return base.Equals(obj);
		}

		public override int GetHashCode() {
			return base.GetHashCode ();
		}

	} // End of ResidentialVoIPRow class
} // End of namespace

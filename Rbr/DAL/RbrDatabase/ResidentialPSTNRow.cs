// <fileinfo name="ResidentialPSTNRow.cs">
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
	/// Represents a record in the <c>ResidentialPSTN</c> table.
	/// </summary>
	[Serializable]
	public class ResidentialPSTNRow : ResidentialPSTNRow_Base {
		public const string AccountStatus_PropName = "AccountStatus";
		public const string AccountStatus_DisplayName = "Status";

		public Status AccountStatus {
			get { return (Status)this.Status; }
			set { this.Status = (byte) value; }
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="ResidentialPSTNRow"/> class.
		/// </summary>
		public ResidentialPSTNRow(){
			// EMPTY
		}

		public override bool Equals(object obj) {
			return base.Equals(obj);
		}

		public override int GetHashCode() {
			return base.GetHashCode ();
		}

	} // End of ResidentialPSTNRow class
} // End of namespace

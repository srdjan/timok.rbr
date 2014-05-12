// <fileinfo name="CustomerAcctSupportMapRow.cs">
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

namespace Timok.Rbr.DAL.RbrDatabase {
	/// <summary>
	/// Represents a record in the <c>CustomerAcctSupportMap</c> table.
	/// </summary>
	[Serializable]
	public class CustomerAcctSupportMapRow : CustomerAcctSupportMapRow_Base {
		/// <summary>
		/// Initializes a new instance of the <see cref="CustomerAcctSupportMapRow"/> class.
		/// </summary>
		public CustomerAcctSupportMapRow(){
			// EMPTY
		}

		public override bool Equals(object obj) {
			return base.Equals(obj);
		}

		public override int GetHashCode() {
			return base.GetHashCode ();
		}

	} // End of CustomerAcctSupportMapRow class
} // End of namespace

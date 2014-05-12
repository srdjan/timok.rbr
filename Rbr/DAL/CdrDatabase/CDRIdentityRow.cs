// <fileinfo name="CDRIdentityRow.cs">
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
using Timok.Rbr.DAL.CdrDatabase.Base;
using System.Runtime.Serialization;

namespace Timok.Rbr.DAL.CdrDatabase {
	/// <summary>
	/// Represents a record in the <c>CDRIdentity</c> table.
	/// </summary>
	[Serializable]
	public class CDRIdentityRow : CDRIdentityRow_Base {
		/// <summary>
		/// Initializes a new instance of the <see cref="CDRIdentityRow"/> class.
		/// </summary>
		public CDRIdentityRow(){
			// EMPTY
		}

		public override bool Equals(object obj) {
			return base.Equals(obj);
		}

		public override int GetHashCode() {
			return base.GetHashCode ();
		}

	} // End of CDRIdentityRow class
} // End of namespace

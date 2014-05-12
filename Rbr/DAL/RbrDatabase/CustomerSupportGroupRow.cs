// <fileinfo name="CustomerSupportGroupRow.cs">
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
	/// Represents a record in the <c>CustomerSupportGroup</c> table.
	/// </summary>
	[Serializable]
	public class CustomerSupportGroupRow : CustomerSupportGroupRow_Base {

    public const int DefaultId = 700;

    public CustomerSupportGroupRole GroupRole {
      get { return (CustomerSupportGroupRole) Role; }
      set { Role = (int) value; }
    }

    public bool AllowStatusChange {
      get { return Allow_status_change > 0; }
      set { Allow_status_change = (byte) ((value) ? 1 : 0); }
    }

		/// <summary>
		/// Initializes a new instance of the <see cref="CustomerSupportGroupRow"/> class.
		/// </summary>
		public CustomerSupportGroupRow(){
			// EMPTY
		}

		public override bool Equals(object obj) {
			return base.Equals(obj);
		}

		public override int GetHashCode() {
			return base.GetHashCode ();
		}

	} // End of CustomerSupportGroupRow class
} // End of namespace

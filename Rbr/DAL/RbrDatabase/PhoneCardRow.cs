// <fileinfo name="PhoneCardRow.cs">
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
	/// Represents a record in the <c>PhoneCard</c> table.
	/// </summary>
	[Serializable]
	public class PhoneCardRow : PhoneCardRow_Base {

    public InventoryStatus InventoryStatus {
      get { return (InventoryStatus) this.Inventory_status; }
			set { this.Inventory_status  = (byte) value; }
		}

    public Status CardStatus {
      get { return (Status) this.Status; }
      set { this.Status = (byte) value; }
    }

    public bool Expired {
      get { return this.Date_to_expire <= DateTime.Today; }
    }

		/// <summary>
		/// Initializes a new instance of the <see cref="PhoneCardRow"/> class.
		/// </summary>
		public PhoneCardRow(){
			// EMPTY
		}

		public override bool Equals(object obj) {
			return base.Equals(obj);
		}

		public override int GetHashCode() {
			return base.GetHashCode ();
		}

	} // End of PhoneCardRow class
} // End of namespace

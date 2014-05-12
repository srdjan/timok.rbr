// <fileinfo name="BatchRow.cs">
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
	/// Represents a record in the <c>Batch</c> table.
	/// </summary>
	[Serializable]
	public class BatchRow : BatchRow_Base {
    public const string NumberOfCards_PropName = "NumberOfCards";
    public const string InventoryStatus_PropName = "InventoryStatus";

    public int NumberOfCards {
      get {
        if (this.Last_serial + this.First_serial == 0) {
          return 0;
        }
        else {
          return (int) (this.Last_serial - this.First_serial) + 1;
        }
      }
    }

    public InventoryStatus InventoryStatus {
      get { return (InventoryStatus) this.Status; }
      set { this.Status = (byte) value; }
    }

		/// <summary>
		/// Initializes a new instance of the <see cref="BatchRow"/> class.
		/// </summary>
		public BatchRow(){
			// EMPTY
		}

		public override bool Equals(object obj) {
			return base.Equals(obj);
		}

		public override int GetHashCode() {
			return base.GetHashCode ();
		}

	} // End of BatchRow class
} // End of namespace

// <fileinfo name="InventoryHistoryRow.cs">
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
	/// Represents a record in the <c>InventoryHistory</c> table.
	/// </summary>
	[Serializable]
	public class InventoryHistoryRow : InventoryHistoryRow_Base {
    public const string InventoryCommand_PropName = "InventoryCommand";

    public InventoryCommand InventoryCommand {
      get { return (InventoryCommand) this.Command; }
      set { this.Command = (byte) value; }
    }
    
    /// <summary>
		/// Initializes a new instance of the <see cref="InventoryHistoryRow"/> class.
		/// </summary>
		public InventoryHistoryRow(){
			// EMPTY
		}

		public override bool Equals(object obj) {
			return base.Equals(obj);
		}

		public override int GetHashCode() {
			return base.GetHashCode ();
		}

	} // End of InventoryHistoryRow class
} // End of namespace

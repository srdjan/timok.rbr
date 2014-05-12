// <fileinfo name="InventoryUsageRow.cs">
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
	/// Represents a record in the <c>InventoryUsage</c> table.
	/// </summary>
	[Serializable]
	public class InventoryUsageRow : InventoryUsageRow_Base {
		/// <summary>
		/// Initializes a new instance of the <see cref="InventoryUsageRow"/> class.
		/// </summary>
		public InventoryUsageRow(){
			// EMPTY
		}

		public override bool Equals(object obj) {
			return base.Equals(obj);
		}

		public override int GetHashCode() {
			return base.GetHashCode ();
		}

	} // End of InventoryUsageRow class
} // End of namespace

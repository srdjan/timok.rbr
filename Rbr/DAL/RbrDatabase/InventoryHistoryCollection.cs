// <fileinfo name="InventoryHistoryCollection.cs">
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
using System.Data;
using Timok.Rbr.DAL.RbrDatabase.Base;

namespace Timok.Rbr.DAL.RbrDatabase
{
	/// <summary>
	/// Represents the <c>InventoryHistory</c> table.
	/// </summary>
	public class InventoryHistoryCollection : InventoryHistoryCollection_Base
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="InventoryHistoryCollection"/> class.
		/// </summary>
		/// <param name="db">The database object.</param>
		internal InventoryHistoryCollection(Rbr_Db db)
				: base(db)
		{
			// EMPTY
		}

		public static InventoryHistoryRow Parse(System.Data.DataRow row){
			return new InventoryHistoryCollection(null).MapRow(row);
		}
	} // End of InventoryHistoryCollection class
} // End of namespace

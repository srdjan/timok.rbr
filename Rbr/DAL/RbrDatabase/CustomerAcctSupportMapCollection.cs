// <fileinfo name="CustomerAcctSupportMapCollection.cs">
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
	/// Represents the <c>CustomerAcctSupportMap</c> table.
	/// </summary>
	public class CustomerAcctSupportMapCollection : CustomerAcctSupportMapCollection_Base
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="CustomerAcctSupportMapCollection"/> class.
		/// </summary>
		/// <param name="db">The database object.</param>
		internal CustomerAcctSupportMapCollection(Rbr_Db db)
				: base(db)
		{
			// EMPTY
		}

		public static CustomerAcctSupportMapRow Parse(System.Data.DataRow row){
			return new CustomerAcctSupportMapCollection(null).MapRow(row);
		}
	} // End of CustomerAcctSupportMapCollection class
} // End of namespace

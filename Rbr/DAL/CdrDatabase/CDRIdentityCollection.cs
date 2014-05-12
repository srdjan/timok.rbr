// <fileinfo name="CDRIdentityCollection.cs">
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
using System.Data;
using Timok.Rbr.DAL.CdrDatabase.Base;

namespace Timok.Rbr.DAL.CdrDatabase
{
	/// <summary>
	/// Represents the <c>CDRIdentity</c> table.
	/// </summary>
	public class CDRIdentityCollection : CDRIdentityCollection_Base
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="CDRIdentityCollection"/> class.
		/// </summary>
		/// <param name="db">The database object.</param>
		internal CDRIdentityCollection(Cdr_Db db)
				: base(db)
		{
			// EMPTY
		}

		public static CDRIdentityRow Parse(System.Data.DataRow row){
			return new CDRIdentityCollection(null).MapRow(row);
		}
	} // End of CDRIdentityCollection class
} // End of namespace

// <fileinfo name="PrefixInTypeCollection.cs">
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

namespace Timok.Rbr.DAL.RbrDatabase {
	/// <summary>
	/// Represents the <c>PrefixInType</c> table.
	/// </summary>
	public class PrefixInTypeCollection : PrefixInTypeCollection_Base {
		/// <summary>
		/// Initializes a new instance of the <see cref="PrefixInTypeCollection"/> class.
		/// </summary>
		/// <param name="db">The database object.</param>
		internal PrefixInTypeCollection(Rbr_Db db)
			: base(db) {
			// EMPTY
		}


    public PrefixInTypeRow[] GetAll(short pExcludePrefixInTypeId) {
      string _where = PrefixInTypeRow.prefix_in_type_id_DbName + "<>" + pExcludePrefixInTypeId;
      return GetAsArray(_where, PrefixInTypeRow.prefix_in_type_id_DbName);
    }

		public static PrefixInTypeRow Parse(System.Data.DataRow row){
			return new PrefixInTypeCollection(null).MapRow(row);
		}
	} // End of PrefixInTypeCollection class
} // End of namespace

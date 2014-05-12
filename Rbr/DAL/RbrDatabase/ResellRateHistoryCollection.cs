// <fileinfo name="ResellRateHistoryCollection.cs">
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
	/// Represents the <c>ResellRateHistory</c> table.
	/// </summary>
	public class ResellRateHistoryCollection : ResellRateHistoryCollection_Base
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="ResellRateHistoryCollection"/> class.
		/// </summary>
		/// <param name="db">The database object.</param>
		internal ResellRateHistoryCollection(Rbr_Db db)
				: base(db)
		{
			// EMPTY
		}

		public static ResellRateHistoryRow Parse(System.Data.DataRow row){
			return new ResellRateHistoryCollection(null).MapRow(row);
		}
	} // End of ResellRateHistoryCollection class
} // End of namespace

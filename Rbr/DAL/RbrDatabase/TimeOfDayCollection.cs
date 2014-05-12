// <fileinfo name="TimeOfDayCollection.cs">
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
	/// Represents the <c>TimeOfDay</c> table.
	/// </summary>
	public class TimeOfDayCollection : TimeOfDayCollection_Base
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="TimeOfDayCollection"/> class.
		/// </summary>
		/// <param name="db">The database object.</param>
		internal TimeOfDayCollection(Rbr_Db db)
				: base(db)
		{
			// EMPTY
		}

		public static TimeOfDayRow Parse(System.Data.DataRow row){
			return new TimeOfDayCollection(null).MapRow(row);
		}
	} // End of TimeOfDayCollection class
} // End of namespace

// <fileinfo name="CdrAggregateCollection.cs">
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
using Timok.Core;

namespace Timok.Rbr.DAL.RbrDatabase
{
	/// <summary>
	/// Represents the <c>CdrAggregate</c> table.
	/// </summary>
	public class CdrAggregateCollection : CdrAggregateCollection_Base
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="CdrAggregateCollection"/> class.
		/// </summary>
		/// <param name="db">The database object.</param>
		internal CdrAggregateCollection(Rbr_Db db)
				: base(db)
		{
			// EMPTY
		}

		public static CdrAggregateRow Parse(System.Data.DataRow row){
			return new CdrAggregateCollection(null).MapRow(row);
		}

    /// <summary>
    /// Purges the CdrAggregate table, leaving specified number of Days (including today)
    /// </summary>
    /// <param name="pDaysToKeep">Number of Days to keep in a table, (including today); Value 0 or less will delete all records.</param>
    /// <returns>Number of deleted records </returns>
    public int Purge(int pDaysToKeep) {
			int _maxDateHourToDelete = TimokDate.Parse(DateTime.Today.AddDays(-pDaysToKeep), 23);
			
			string _sqlStr = "DELETE TOP (1000) FROM CdrAggregate " +
        " WHERE " + CdrAggregateRow.date_hour_DbName + " < " + Database.CreateSqlParameterName(CdrAggregateRow.date_hour_PropName);

      IDbCommand _cmd = Database.CreateCommand(_sqlStr);
      AddParameter(_cmd, CdrAggregateRow.date_hour_PropName, _maxDateHourToDelete);
			
			int _rowsAffected = 0;
			int _totalRowsAffected = 0;
			do {
				_rowsAffected = _cmd.ExecuteNonQuery();
				_totalRowsAffected += _rowsAffected;
			} while (_rowsAffected == 1000);

      return _totalRowsAffected;
    }
	} // End of CdrAggregateCollection class
} // End of namespace

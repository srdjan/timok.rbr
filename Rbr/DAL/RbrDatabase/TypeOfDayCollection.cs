// <fileinfo name="TypeOfDayCollection.cs">
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
using Timok.Rbr.Core.Config;

namespace Timok.Rbr.DAL.RbrDatabase {
	/// <summary>
	/// Represents the <c>TypeOfDay</c> table.
	/// </summary>
	public class TypeOfDayCollection : TypeOfDayCollection_Base {
		/// <summary>
		/// Initializes a new instance of the <see cref="TypeOfDayCollection"/> class.
		/// </summary>
		/// <param name="db">The database object.</param>
		internal TypeOfDayCollection(Rbr_Db db)
			: base(db) {
			// EMPTY
		}

		public static TypeOfDayRow Parse(System.Data.DataRow row){
			return new TypeOfDayCollection(null).MapRow(row);
		}
		
		public TypeOfDayRow GetByRateInfoIDTypeOfDayChoice(int pRateInfoID, TypeOfDayChoice pTypeOfDayChoice) {
			string _sqlStr = "SELECT * FROM [dbo].[TypeOfDay] WHERE " +
				"[" + TypeOfDayRow.rate_info_id_DbName + "]=" + base.Database.CreateSqlParameterName(TypeOfDayRow.rate_info_id_PropName) + 
				" AND " + 
				"[" + TypeOfDayRow.type_of_day_choice_DbName + "]=" + base.Database.CreateSqlParameterName(TypeOfDayRow.type_of_day_choice_PropName);
			IDbCommand _cmd = base.Database.CreateCommand(_sqlStr);
			AddParameter(_cmd, TypeOfDayRow.rate_info_id_PropName, pRateInfoID);
			AddParameter(_cmd, TypeOfDayRow.type_of_day_choice_PropName, (byte) pTypeOfDayChoice);

			using (IDataReader _reader = _cmd.ExecuteReader()) {
				TypeOfDayRow[] _tempArray = MapRecords(_reader);
				return _tempArray.Length == 0 ? null : _tempArray[0];
			}
		}
	} // End of TypeOfDayCollection class
} // End of namespace

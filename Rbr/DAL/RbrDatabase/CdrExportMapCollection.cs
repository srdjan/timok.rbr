// <fileinfo name="CdrExportMapCollection.cs">
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
	/// Represents the <c>CdrExportMap</c> table.
	/// </summary>
	public class CdrExportMapCollection : CdrExportMapCollection_Base {
		/// <summary>
		/// Initializes a new instance of the <see cref="CdrExportMapCollection"/> class.
		/// </summary>
		/// <param name="db">The database object.</param>
		internal CdrExportMapCollection(Rbr_Db db)
			: base(db) {
			// EMPTY
		}

		public static CdrExportMapRow Parse(System.Data.DataRow row){
			return new CdrExportMapCollection(null).MapRow(row);
		}

		public override void Insert(CdrExportMapRow value) {
			string _sqlStr = "DECLARE " + base.Database.CreateSqlParameterName(CdrExportMapRow.map_id_PropName) + " int " + 
				"SET " + base.Database.CreateSqlParameterName(CdrExportMapRow.map_id_PropName) + 
				" = COALESCE((SELECT MAX(" + CdrExportMapRow.map_id_DbName + ") FROM CdrExportMap) + 1, 1) " + 

				"INSERT INTO [dbo].[CdrExportMap] (" + 
				"[" + CdrExportMapRow.map_id_DbName + "], " +
				"[" + CdrExportMapRow.name_DbName + "], " +
				"[" + CdrExportMapRow.delimiter_DbName + "], " +
				"[" + CdrExportMapRow.target_dest_folder_DbName + "]" +
				") VALUES (" +
				Database.CreateSqlParameterName(CdrExportMapRow.map_id_PropName) + ", " +
				Database.CreateSqlParameterName(CdrExportMapRow.name_PropName) + ", " +
				Database.CreateSqlParameterName(CdrExportMapRow.delimiter_PropName) + ", " +
				Database.CreateSqlParameterName(CdrExportMapRow.target_dest_folder_PropName) + ")" + 
				" SELECT " + Database.CreateSqlParameterName(CdrExportMapRow.map_id_PropName);
			
			IDbCommand _cmd = Database.CreateCommand(_sqlStr);
			AddParameter(_cmd, CdrExportMapRow.name_PropName, value.Name);
			AddParameter(_cmd, CdrExportMapRow.delimiter_PropName, value.Delimiter);
			AddParameter(_cmd, CdrExportMapRow.target_dest_folder_PropName, value.Target_dest_folder);

			value.Map_id = (int) _cmd.ExecuteScalar();

		}


		protected override IDbCommand CreateGetAllCommand() {
			return CreateGetCommand(null, CdrExportMapRow.name_DbName);
		}

	} // End of CdrExportMapCollection class
} // End of namespace

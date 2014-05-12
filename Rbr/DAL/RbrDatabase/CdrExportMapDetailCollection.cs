// <fileinfo name="CdrExportMapDetailCollection.cs">
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
	/// Represents the <c>CdrExportMapDetail</c> table.
	/// </summary>
	public class CdrExportMapDetailCollection : CdrExportMapDetailCollection_Base
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="CdrExportMapDetailCollection"/> class.
		/// </summary>
		/// <param name="db">The database object.</param>
		internal CdrExportMapDetailCollection(Rbr_Db db)
				: base(db)
		{
			// EMPTY
		}

		public static CdrExportMapDetailRow Parse(System.Data.DataRow row){
			return new CdrExportMapDetailCollection(null).MapRow(row);
		}

		public override void Insert(CdrExportMapDetailRow value) {
			string _sqlStr = "DECLARE " + base.Database.CreateSqlParameterName(CdrExportMapDetailRow.map_detail_id_PropName) + " int " + 
				"SET " + base.Database.CreateSqlParameterName(CdrExportMapDetailRow.map_detail_id_PropName) + 
				" = COALESCE((SELECT MAX(" + CdrExportMapDetailRow.map_detail_id_DbName + ") FROM CdrExportMapDetail) + 1, 1) " + 
				
				"INSERT INTO [dbo].[CdrExportMapDetail] (" +
				"[" + CdrExportMapDetailRow.map_detail_id_DbName + "], " +
				"[" + CdrExportMapDetailRow.map_id_DbName + "], " +
				"[" + CdrExportMapDetailRow.sequence_DbName + "], " +
				"[" + CdrExportMapDetailRow.field_name_DbName + "], " +
				"[" + CdrExportMapDetailRow.format_type_DbName + "]" +
				") VALUES (" +
				Database.CreateSqlParameterName(CdrExportMapDetailRow.map_detail_id_PropName) + ", " +
				Database.CreateSqlParameterName(CdrExportMapDetailRow.map_id_PropName) + ", " +
				Database.CreateSqlParameterName(CdrExportMapDetailRow.sequence_PropName) + ", " +
				Database.CreateSqlParameterName(CdrExportMapDetailRow.field_name_PropName) + ", " + 
				Database.CreateSqlParameterName(CdrExportMapDetailRow.format_type_PropName) + ")" + 
				" SELECT " + Database.CreateSqlParameterName(CdrExportMapDetailRow.map_detail_id_PropName);

			IDbCommand _cmd = Database.CreateCommand(_sqlStr);
			AddParameter(_cmd, CdrExportMapDetailRow.map_id_PropName, value.Map_id);
			AddParameter(_cmd, CdrExportMapDetailRow.sequence_PropName, value.Sequence);
			AddParameter(_cmd, CdrExportMapDetailRow.field_name_PropName, value.Field_name);
			AddParameter(_cmd, CdrExportMapDetailRow.format_type_PropName, value.Format_type);

			value.Map_detail_id = (int) _cmd.ExecuteScalar();
		}

		protected override IDbCommand CreateGetByMap_idCommand(int pMap_id) {
			string _whereSql = "";
			_whereSql += "[" + CdrExportMapDetailRow.map_id_DbName + "]=" + Database.CreateSqlParameterName(CdrExportMapDetailRow.map_id_PropName);

			string _orderBy = " [" + CdrExportMapDetailRow.map_id_DbName + "],[" + CdrExportMapDetailRow.sequence_DbName + "]";
			IDbCommand _cmd = CreateGetCommand(_whereSql, _orderBy);
			AddParameter(_cmd, CdrExportMapDetailRow.map_id_PropName, pMap_id);
			return _cmd;
		}


	} // End of CdrExportMapDetailCollection class
} // End of namespace

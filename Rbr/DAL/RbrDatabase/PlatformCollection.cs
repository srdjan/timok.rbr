// <fileinfo name="PlatformCollection.cs">
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
	/// Represents the <c>Platform</c> table.
	/// </summary>
	public class PlatformCollection : PlatformCollection_Base
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="PlatformCollection"/> class.
		/// </summary>
		/// <param name="db">The database object.</param>
		internal PlatformCollection(Rbr_Db db)
				: base(db)
		{
			// EMPTY
		}

		public static PlatformRow Parse(System.Data.DataRow row){
			return new PlatformCollection(null).MapRow(row);
		}

    protected override IDbCommand CreateGetAllCommand() {
      return CreateGetCommand(null, PlatformRow.location_DbName);
    }

		public bool IsLocationInUseByOtherPlatform(string pLocation, short pPlatformId){
			string sqlStr = "SELECT COUNT(*) FROM [dbo].[Platform] WHERE " +
				"[" + PlatformRow.location_DbName + "]=" + base.Database.CreateSqlParameterName(PlatformRow.location_PropName) + 
				" AND " + 
				"[" + PlatformRow.platform_id_DbName + "]<>" + base.Database.CreateSqlParameterName(PlatformRow.platform_id_PropName);

			IDbCommand cmd = base.Database.CreateCommand(sqlStr);
			base.Database.AddParameter(cmd, PlatformRow.location_PropName, DbType.String, pLocation);
			base.Database.AddParameter(cmd, PlatformRow.platform_id_PropName, DbType.Int16, pPlatformId);

			int _count = (int) cmd.ExecuteScalar();
			return _count == 0 ? false : true;
		}

		public override void Insert(PlatformRow value) {
			string sqlStr = "DECLARE " + base.Database.CreateSqlParameterName(PlatformRow.platform_id_PropName) + " smallint " + 
				"SET " + base.Database.CreateSqlParameterName(PlatformRow.platform_id_PropName) + 
				" = COALESCE((SELECT MAX(" + PlatformRow.platform_id_DbName + ") FROM Platform) + 1, 1) " + 

				"INSERT INTO [dbo].[Platform] (" +
				"[" + PlatformRow.platform_id_DbName + "], " +
				"[" + PlatformRow.location_DbName + "], " +
				"[" + PlatformRow.status_DbName + "], " +
				"[" + PlatformRow.platform_config_DbName + "] " +
				") VALUES (" +
				base.Database.CreateSqlParameterName(PlatformRow.platform_id_PropName) + ", " +
				base.Database.CreateSqlParameterName(PlatformRow.location_PropName) + ", " +
				base.Database.CreateSqlParameterName(PlatformRow.status_PropName) + ", " +
				base.Database.CreateSqlParameterName(PlatformRow.platform_config_PropName) + ") " +
				" SELECT " + base.Database.CreateSqlParameterName(PlatformRow.platform_id_PropName);

			IDbCommand cmd = base.Database.CreateCommand(sqlStr);
			base.Database.AddParameter(cmd, PlatformRow.location_PropName, DbType.AnsiString, value.Location);
			base.Database.AddParameter(cmd, PlatformRow.status_PropName, DbType.Byte, value.Status);
			base.Database.AddParameter(cmd, PlatformRow.platform_config_PropName, DbType.Int32, value.Platform_config);
			
			object _res = cmd.ExecuteScalar();

			value.Platform_id = (short)_res;
		}
	} // End of PlatformCollection class
} // End of namespace

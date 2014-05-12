// <fileinfo name="VirtualSwitchCollection.cs">
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

namespace Timok.Rbr.DAL.RbrDatabase
{
	/// <summary>
	/// Represents the <c>VirtualSwitch</c> table.
	/// </summary>
	public class VirtualSwitchCollection : VirtualSwitchCollection_Base
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="VirtualSwitchCollection"/> class.
		/// </summary>
		/// <param name="db">The database object.</param>
		internal VirtualSwitchCollection(Rbr_Db db)
				: base(db)
		{
			// EMPTY
		}

		public static VirtualSwitchRow Parse(System.Data.DataRow row){
			return new VirtualSwitchCollection(null).MapRow(row);
		}

    public override void Insert(VirtualSwitchRow value) {
      try {
        string _sqlStr =
          "DECLARE " +
          Database.CreateSqlParameterName(VirtualSwitchRow.virtual_switch_id_PropName) + " int " +
          "SET " +
          Database.CreateSqlParameterName(VirtualSwitchRow.virtual_switch_id_PropName) +
          " = COALESCE((SELECT MAX(" + VirtualSwitchRow.virtual_switch_id_DbName + ") FROM VirtualSwitch WHERE " + VirtualSwitchRow.virtual_switch_id_DbName + " > 0) + 1, 1) " +

        "INSERT INTO [dbo].[VirtualSwitch] (" +
        "[" + VirtualSwitchRow.virtual_switch_id_DbName + "], " +
        "[" + VirtualSwitchRow.name_DbName + "], " +
        "[" + VirtualSwitchRow.status_DbName + "], " +
        "[" + VirtualSwitchRow.contact_info_id_DbName + "]" +
        ") VALUES (" +
        Database.CreateSqlParameterName(VirtualSwitchRow.virtual_switch_id_PropName) + ", " +
        Database.CreateSqlParameterName(VirtualSwitchRow.name_PropName) + ", " +
        Database.CreateSqlParameterName(VirtualSwitchRow.status_PropName) + ", " +
        Database.CreateSqlParameterName(VirtualSwitchRow.contact_info_id_PropName) + ")" +

        " SELECT " + base.Database.CreateSqlParameterName(VirtualSwitchRow.virtual_switch_id_PropName) + " ";

        IDbCommand _cmd = Database.CreateCommand(_sqlStr);
        //AddParameter(_cmd, VirtualSwitchRow.virtual_switch_id_PropName, value.Virtual_switch_id);
        AddParameter(_cmd, VirtualSwitchRow.name_PropName, value.Name);
        AddParameter(_cmd, VirtualSwitchRow.status_PropName, value.Status);
        AddParameter(_cmd, VirtualSwitchRow.contact_info_id_PropName, value.Contact_info_id);

        value.Virtual_switch_id = (int) _cmd.ExecuteScalar();
      }
      catch (System.Data.SqlClient.SqlException _sqlEx) {
        if (_sqlEx.Message.IndexOf("Cannot insert duplicate key row in object") > -1) {
          throw new AlternateKeyException("Name already in use!", _sqlEx);
        }
        throw;// any other ex
      }
    }

    protected override IDbCommand CreateGetAllCommand() {
      string _where = VirtualSwitchRow.virtual_switch_id_DbName + "<>" + AppConstants.DefaultVirtualSwitchId; 
      return CreateGetCommand(_where, VirtualSwitchRow.name_DbName);
    }
	} // End of VirtualSwitchCollection class
} // End of namespace

// <fileinfo name="BoxCollection.cs">
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
	/// Represents the <c>Box</c> table.
	/// </summary>
	public class BoxCollection : BoxCollection_Base
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="BoxCollection"/> class.
		/// </summary>
		/// <param name="db">The database object.</param>
		internal BoxCollection(Rbr_Db db)
				: base(db)
		{
			// EMPTY
		}

		public static BoxRow Parse(System.Data.DataRow row){
			return new BoxCollection(null).MapRow(row);
		}

    public override void Insert(BoxRow value) {
      string _sqlStr = "DECLARE " + base.Database.CreateSqlParameterName(BoxRow.box_id_PropName) + " int " +
        "SET " + base.Database.CreateSqlParameterName(BoxRow.box_id_PropName) +
        " = COALESCE((SELECT MAX(" + BoxRow.box_id_DbName + ") FROM Box) + 1, 1000) " +

			  " INSERT INTO [dbo].[Box] (" +
        "[" + BoxRow.box_id_DbName + "], " +
        "[" + BoxRow.date_created_DbName + "], " +
        "[" + BoxRow.date_activated_DbName + "]" +
				") VALUES (" +
				Database.CreateSqlParameterName(BoxRow.box_id_PropName) + ", " +
				Database.CreateSqlParameterName(BoxRow.date_created_PropName) + ", " +
				Database.CreateSqlParameterName(BoxRow.date_activated_PropName) + ")" + 
        " SELECT " + Database.CreateSqlParameterName(BoxRow.box_id_PropName);


      IDbCommand _cmd = Database.CreateCommand(_sqlStr);
      //AddParameter(cmd, "Box_id", value.Box_id);
      AddParameter(_cmd, BoxRow.date_created_PropName, value.Date_created);
      AddParameter(_cmd, BoxRow.date_activated_PropName,
        value.IsDate_activatedNull ? DBNull.Value : (object) value.Date_activated);
      object _res = _cmd.ExecuteScalar();

      value.Box_id = (int) _res;
    }
  } // End of BoxCollection class
} // End of namespace

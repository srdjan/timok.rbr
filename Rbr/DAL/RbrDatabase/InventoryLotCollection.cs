// <fileinfo name="InventoryLotCollection.cs">
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
	/// Represents the <c>InventoryLot</c> table.
	/// </summary>
	public class InventoryLotCollection : InventoryLotCollection_Base
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="InventoryLotCollection"/> class.
		/// </summary>
		/// <param name="db">The database object.</param>
		internal InventoryLotCollection(Rbr_Db db)
				: base(db)
		{
			// EMPTY
		}

		public static InventoryLotRow Parse(System.Data.DataRow row){
			return new InventoryLotCollection(null).MapRow(row);
		}

    public override void Insert(InventoryLotRow value) {
      string _sqlStr = "DECLARE " + base.Database.CreateSqlParameterName(InventoryLotRow.lot_id_PropName) + " int " +
        "SET " + base.Database.CreateSqlParameterName(InventoryLotRow.lot_id_PropName) +
        " = COALESCE((SELECT MAX(" + InventoryLotRow.lot_id_DbName + ") FROM InventoryLot) + 1, 1000) " +

			" INSERT INTO [dbo].[InventoryLot] (" +
				"[" + InventoryLotRow.lot_id_DbName + "], " +
				"[" + InventoryLotRow.service_id_DbName + "], " +
        "[" + InventoryLotRow.denomination_DbName + "]" +
				") VALUES (" +
				Database.CreateSqlParameterName(InventoryLotRow.lot_id_PropName) + ", " +
				Database.CreateSqlParameterName(InventoryLotRow.service_id_PropName) + ", " +
        Database.CreateSqlParameterName(InventoryLotRow.denomination_PropName) + ")" +
        " SELECT " + base.Database.CreateSqlParameterName(InventoryLotRow.lot_id_PropName);

			IDbCommand _cmd = Database.CreateCommand(_sqlStr);
			//AddParameter(_cmd, "Lot_id", value.Lot_id);
			AddParameter(_cmd, InventoryLotRow.service_id_PropName, value.Service_id);
			AddParameter(_cmd, InventoryLotRow.denomination_PropName, value.Denomination);
      object _res = _cmd.ExecuteScalar();

      value.Lot_id = (int) _res;
    }

    public InventoryLotRow GetByServiceIdDenomination(short pServiceId, decimal pDenomination) {
      string _where = InventoryLotRow.service_id_DbName + " = " + Database.CreateSqlParameterName(InventoryLotRow.service_id_PropName) +
          " AND " + InventoryLotRow.denomination_DbName + " = " + Database.CreateSqlParameterName(InventoryLotRow.denomination_PropName);

      IDbCommand _cmd = base.CreateGetCommand(_where, null);
      AddParameter(_cmd, InventoryLotRow.service_id_PropName, pServiceId);
      AddParameter(_cmd, InventoryLotRow.denomination_PropName, pDenomination);
      using (IDataReader _reader = _cmd.ExecuteReader()) {
        InventoryLotRow[] _tempArray = MapRecords(_reader);
        return 0 == _tempArray.Length ? null : _tempArray[0];
      }
    }
	} // End of InventoryLotCollection class
} // End of namespace

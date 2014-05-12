// <fileinfo name="RetailRouteCollection.cs">
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
	/// Represents the <c>RetailRoute</c> table.
	/// </summary>
	public class RetailRouteCollection : RetailRouteCollection_Base
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="RetailRouteCollection"/> class.
		/// </summary>
		/// <param name="db">The database object.</param>
		internal RetailRouteCollection(Rbr_Db db)
				: base(db)
		{
			// EMPTY
		}

		public static RetailRouteRow Parse(System.Data.DataRow row){
			return new RetailRouteCollection(null).MapRow(row);
		}

    public override void Insert(RetailRouteRow value) {
      string _sqlStr =
      "DECLARE " + Database.CreateSqlParameterName(RetailRouteRow.retail_route_id_PropName) + " int " +
      "SET " + Database.CreateSqlParameterName(RetailRouteRow.retail_route_id_PropName);

      if (value.Retail_route_id == -(value.Customer_acct_id)) {
        //DEFAULT RetailRoute 
        _sqlStr += " = " + value.Retail_route_id;
      }
      else {
        //Regular RetailRoute
        _sqlStr += " = COALESCE((SELECT MAX(" + RetailRouteRow.retail_route_id_DbName + ") FROM RetailRoute WHERE " + RetailRouteRow.retail_route_id_DbName + " > 0) + 1, 10000) ";
      }

      _sqlStr += "INSERT INTO [dbo].[RetailRoute] (" +
        "[" + RetailRouteRow.retail_route_id_DbName + "], " +
        "[" + RetailRouteRow.customer_acct_id_DbName + "], " +
        "[" + RetailRouteRow.route_id_DbName + "], " +
        "[" + RetailRouteRow.status_DbName + "], " +
        "[" + RetailRouteRow.start_bonus_minutes_DbName + "], " +
        "[" + RetailRouteRow.bonus_minutes_type_DbName + "], " +
        "[" + RetailRouteRow.multiplier_DbName + "]" +
        ") VALUES (" +
        Database.CreateSqlParameterName(RetailRouteRow.retail_route_id_PropName) + ", " +
        Database.CreateSqlParameterName(RetailRouteRow.customer_acct_id_PropName) + ", " +
        Database.CreateSqlParameterName(RetailRouteRow.route_id_PropName) + ", " +
        Database.CreateSqlParameterName(RetailRouteRow.status_PropName) + ", " +
        Database.CreateSqlParameterName(RetailRouteRow.start_bonus_minutes_PropName) + ", " +
        Database.CreateSqlParameterName(RetailRouteRow.bonus_minutes_type_PropName) + ", " +
        Database.CreateSqlParameterName(RetailRouteRow.multiplier_PropName) + ") " +
        " SELECT " + Database.CreateSqlParameterName(RetailRouteRow.retail_route_id_PropName);

      IDbCommand _cmd = Database.CreateCommand(_sqlStr);
      //AddParameter(cmd, RetailRouteRow.Retail_route_id, value.Retail_route_id);
      AddParameter(_cmd, RetailRouteRow.customer_acct_id_PropName, value.Customer_acct_id);
      AddParameter(_cmd, RetailRouteRow.route_id_PropName,
        value.IsRoute_idNull ? DBNull.Value : (object) value.Route_id);
      AddParameter(_cmd, RetailRouteRow.status_PropName, value.Status);
      AddParameter(_cmd, RetailRouteRow.start_bonus_minutes_PropName, value.Start_bonus_minutes);
      AddParameter(_cmd, RetailRouteRow.bonus_minutes_type_PropName, value.Bonus_minutes_type);
      AddParameter(_cmd, RetailRouteRow.multiplier_PropName, value.Multiplier);
      
      value.Retail_route_id = (int) _cmd.ExecuteScalar();
    }

    public int GetCount(int pBaseRouteId) {
      string _sqlStr = "SELECT COUNT(*) FROM RetailRoute WHERE " +
        "[" + RetailRouteRow.route_id_DbName + "]=" + base.Database.CreateSqlParameterName(RetailRouteRow.route_id_PropName);

      IDbCommand _cmd = base.Database.CreateCommand(_sqlStr);
      AddParameter(_cmd, RetailRouteRow.route_id_PropName, pBaseRouteId);
      return ((int) _cmd.ExecuteScalar());
    }
  } // End of RetailRouteCollection class
} // End of namespace

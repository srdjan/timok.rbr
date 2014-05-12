// <fileinfo name="RoutingPlanDetailCollection.cs">
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
	/// Represents the <c>RoutingPlanDetail</c> table.
	/// </summary>
	public class RoutingPlanDetailCollection : RoutingPlanDetailCollection_Base
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="RoutingPlanDetailCollection"/> class.
		/// </summary>
		/// <param name="db">The database object.</param>
		internal RoutingPlanDetailCollection(Rbr_Db db)
				: base(db)
		{
			// EMPTY
		}

		public static RoutingPlanDetailRow Parse(System.Data.DataRow row){
			return new RoutingPlanDetailCollection(null).MapRow(row);
		}

    public RoutingPlanDetailRow[] GetByRoutingPlanIdCountryId(int pRoutingPlanId, int pCountryId) {
      /*
      SELECT * 
      FROM RoutingPlanDetail
      WHERE routing_plan_id = 8000 
      AND route_id IN (
        SELECT route_id FROM Route 
        WHERE country_id = 202
      ) 
       */
      string _where =
        RoutingPlanDetailRow.routing_plan_id_DbName + " = " +
        Database.CreateSqlParameterName(RoutingPlanDetailRow.routing_plan_id_PropName) +
        " AND " +
        RoutingPlanDetailRow.route_id_DbName + " IN (" +
        "     SELECT " + RouteRow.route_id_DbName + " FROM Route " +
        "     WHERE " + RouteRow.country_id_DbName + "=" + pCountryId +
        ")";

      IDbCommand _cmd = CreateGetCommand(_where, null);
      AddParameter(_cmd, RoutingPlanDetailRow.routing_plan_id_PropName, pRoutingPlanId);

      using (IDataReader reader = Database.ExecuteReader(_cmd)) {
        return MapRecords(reader);
      }
    }
	} // End of RoutingPlanDetailCollection class
} // End of namespace

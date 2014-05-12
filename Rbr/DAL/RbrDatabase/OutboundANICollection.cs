// <fileinfo name="OutboundANICollection.cs">
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
using Timok.Rbr.Core.Config;

using Timok.Rbr.DAL.RbrDatabase.Base;

namespace Timok.Rbr.DAL.RbrDatabase {
	/// <summary>
	/// Represents the <c>OutboundANI</c> table.
	/// </summary>
	public class OutboundANICollection : OutboundANICollection_Base {
		/// <summary>
		/// Initializes a new instance of the <see cref="OutboundANICollection"/> class.
		/// </summary>
		/// <param name="db">The database object.</param>
		internal OutboundANICollection(Rbr_Db db)
			: base(db) {
			// EMPTY
		}

		public static OutboundANIRow Parse(System.Data.DataRow row){
			return new OutboundANICollection(null).MapRow(row);
		}

		public override void Insert(OutboundANIRow value) {
			string sqlStr = "DECLARE " + base.Database.CreateSqlParameterName(OutboundANIRow.outbound_ani_id_PropName) + " int " + 
				"SET " + base.Database.CreateSqlParameterName(OutboundANIRow.outbound_ani_id_PropName) + 
				" = COALESCE((SELECT MAX(" + OutboundANIRow.outbound_ani_id_DbName + ") FROM OutboundANI) + 1, 1) " + 

				"INSERT INTO [dbo].[OutboundANI] (" +
				"[" + OutboundANIRow.outbound_ani_id_DbName + "], " +
				"[" + OutboundANIRow.ANI_DbName + "], " +
				"[" + OutboundANIRow.carrier_route_id_DbName + "], " +
				"[" + OutboundANIRow.version_DbName + "] " +
				") VALUES (" +
				base.Database.CreateSqlParameterName(OutboundANIRow.outbound_ani_id_PropName) + ", " +
				base.Database.CreateSqlParameterName(OutboundANIRow.ANI_PropName) + ", " +
				base.Database.CreateSqlParameterName(OutboundANIRow.carrier_route_id_PropName) + ", " +
        base.Database.CreateSqlParameterName(OutboundANIRow.version_PropName) + ") " +
        "SELECT " + base.Database.CreateSqlParameterName(OutboundANIRow.outbound_ani_id_PropName);
			IDbCommand cmd = base.Database.CreateCommand(sqlStr);
			//AddParameter(cmd, "Ani_id", value.Ani_id);
			AddParameter(cmd, OutboundANIRow.ANI_PropName, value.ANI);
			AddParameter(cmd, OutboundANIRow.carrier_route_id_PropName, value.Carrier_route_id);
      AddParameter(cmd, OutboundANIRow.version_PropName, value.Version);
      object _res = cmd.ExecuteScalar();

			value.Outbound_ani_id = (int)_res;
		}

//
//		public int DeleteByCarrierRouteId(int pCarrierRouteId) {
//			string _whereSql = "[" + OutboundANIRow.carrier_route_id_DbName + "]=" + Database.CreateSqlParameterName(OutboundANIRow.carrier_route_id_PropName);
//
//			IDbCommand _cmd = CreateDeleteCommand(_whereSql);
//			AddParameter(_cmd, OutboundANIRow.carrier_route_id_PropName, pCarrierRouteId);
//			return _cmd.ExecuteNonQuery();
//		}


	} // End of OutboundANICollection class
} // End of namespace

// <fileinfo name="LoadBalancingMapViewCollection.cs">
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
	/// Represents the <c>LoadBalancingMapView</c> view.
	/// </summary>
	public class LoadBalancingMapViewCollection : LoadBalancingMapViewCollection_Base {
		/// <summary>
		/// Initializes a new instance of the <see cref="LoadBalancingMapViewCollection"/> class.
		/// </summary>
		/// <param name="db">The database object.</param>
		internal LoadBalancingMapViewCollection(Rbr_Db db)
			: base(db) {
			// EMPTY
		}

		public static LoadBalancingMapViewRow Parse(System.Data.DataRow row){
			return new LoadBalancingMapViewCollection(null).MapRow(row);
		}

		public LoadBalancingMapViewRow[] GetByNodeId(short node_id) {
			string sqlStr = "SELECT * FROM [dbo].[LoadBalancingMapView] WHERE " +
				LoadBalancingMapViewRow.node_id_DbName + "=" + base.Database.CreateSqlParameterName(LoadBalancingMapViewRow.node_id_PropName) + 
				" ORDER BY [" + LoadBalancingMapViewRow.customer_acct_name_DbName + "]";
			IDbCommand cmd = base.Database.CreateCommand(sqlStr);
			AddParameter(cmd, LoadBalancingMapViewRow.node_id_PropName, node_id);
			using(IDataReader reader = cmd.ExecuteReader()) {
				return MapRecords(reader);
			}
		}

		public LoadBalancingMapViewRow[] GetByCustomer_acct_id(short customer_acct_id) {
			string sqlStr = "SELECT * FROM [dbo].[LoadBalancingMapView] WHERE " +
				LoadBalancingMapViewRow.customer_acct_id_DbName + "=" + base.Database.CreateSqlParameterName(LoadBalancingMapViewRow.customer_acct_id_PropName) + 
				" ORDER BY [" + LoadBalancingMapViewRow.platform_location_DbName + "], [" + LoadBalancingMapViewRow.dot_ip_address_DbName + "]";
			IDbCommand cmd = base.Database.CreateCommand(sqlStr);
			AddParameter(cmd, LoadBalancingMapViewRow.customer_acct_id_PropName, customer_acct_id);
			using(IDataReader reader = cmd.ExecuteReader()) {
				return MapRecords(reader);
			}
		}

		public LoadBalancingMapViewRow GetByPrimaryKey(short node_id, short customer_acct_id) {
			string sqlStr = "SELECT * FROM [dbo].[LoadBalancingMapView] WHERE " +
				LoadBalancingMapViewRow.node_id_DbName + "=" + base.Database.CreateSqlParameterName(LoadBalancingMapViewRow.node_id_PropName) + 
				" AND " + 
				LoadBalancingMapViewRow.customer_acct_id_DbName + "=" + base.Database.CreateSqlParameterName(LoadBalancingMapViewRow.customer_acct_id_PropName); 
			IDbCommand cmd = base.Database.CreateCommand(sqlStr);
			AddParameter(cmd, LoadBalancingMapViewRow.node_id_PropName, node_id);
			AddParameter(cmd, LoadBalancingMapViewRow.customer_acct_id_PropName, customer_acct_id);
			using(IDataReader reader = cmd.ExecuteReader()) {
				LoadBalancingMapViewRow[] rows = MapRecords(reader);
				return (rows.Length == 0) ? null : rows[0];
			}
		}

		protected override IDbCommand CreateGetAllCommand() {
			return CreateGetCommand(null, 
				LoadBalancingMapViewRow.platform_location_DbName + "," + 
				LoadBalancingMapViewRow.dot_ip_address_DbName + "," + 
				LoadBalancingMapViewRow.customer_acct_name_DbName);
		}

	} // End of LoadBalancingMapViewCollection class
} // End of namespace

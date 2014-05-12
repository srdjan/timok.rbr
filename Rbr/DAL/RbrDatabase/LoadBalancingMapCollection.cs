// <fileinfo name="LoadBalancingMapCollection.cs">
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
	/// Represents the <c>LoadBalancingMap</c> table.
	/// </summary>
	public class LoadBalancingMapCollection : LoadBalancingMapCollection_Base {
		/// <summary>
		/// Initializes a new instance of the <see cref="LoadBalancingMapCollection"/> class.
		/// </summary>
		/// <param name="db">The database object.</param>
		internal LoadBalancingMapCollection(Rbr_Db db)
			: base(db) {
			// EMPTY
		}

		public static LoadBalancingMapRow Parse(System.Data.DataRow row){
			return new LoadBalancingMapCollection(null).MapRow(row);
		}

		public override void Insert(LoadBalancingMapRow value) {
			string sqlStr = "INSERT INTO [dbo].[LoadBalancingMap] (" +
				"[" + LoadBalancingMapRow.node_id_DbName + "], " +
				"[" + LoadBalancingMapRow.customer_acct_id_DbName + "], " +
				"[" + LoadBalancingMapRow.max_calls_DbName + "]," +
				"[" + LoadBalancingMapRow.current_calls_DbName + "]" +
				") VALUES (" +
				base.Database.CreateSqlParameterName(LoadBalancingMapRow.node_id_PropName) + ", " +
				base.Database.CreateSqlParameterName(LoadBalancingMapRow.customer_acct_id_PropName) + ", " +
				base.Database.CreateSqlParameterName(LoadBalancingMapRow.max_calls_PropName) + ", " + 
				base.Database.CreateSqlParameterName(LoadBalancingMapRow.current_calls_PropName) + ")";
			IDbCommand cmd = base.Database.CreateCommand(sqlStr);
			AddParameter(cmd, LoadBalancingMapRow.node_id_PropName, value.Node_id);
			AddParameter(cmd, LoadBalancingMapRow.customer_acct_id_PropName, value.Customer_acct_id);
			AddParameter(cmd, LoadBalancingMapRow.max_calls_PropName, value.Max_calls);
			AddParameter(cmd, LoadBalancingMapRow.current_calls_PropName, 0);
			cmd.ExecuteNonQuery();
		}

		
		public bool IncrementCustomerAcctCurrentCalls(short node_id, short customer_acct_id) {
			string sqlStr = "UPDATE [dbo].[LoadBalancingMap] " + 
				"SET [" + LoadBalancingMapRow.current_calls_DbName + "] = [" + LoadBalancingMapRow.current_calls_DbName + "] + 1 " + 
				"WHERE [" + LoadBalancingMapRow.node_id_DbName + "] = " + 
				base.Database.CreateSqlParameterName(LoadBalancingMapRow.node_id_PropName) + " " + 
				"AND [" + LoadBalancingMapRow.customer_acct_id_DbName + "] = " + 
				base.Database.CreateSqlParameterName(LoadBalancingMapRow.customer_acct_id_PropName) + " " + 
				"SELECT @@ROWCOUNT ";
			IDbCommand cmd = base.Database.CreateCommand(sqlStr);
			AddParameter(cmd, LoadBalancingMapRow.node_id_PropName, node_id);
			AddParameter(cmd, LoadBalancingMapRow.customer_acct_id_PropName, customer_acct_id);
			int _count = (int)cmd.ExecuteScalar();
			return _count == 1;
		}

		public bool DecrementCustomerAcctCurrentCalls(short node_id, short customer_acct_id) {
			string sqlStr = "UPDATE [dbo].[LoadBalancingMap] " + 
				"SET [" + LoadBalancingMapRow.current_calls_DbName + "] = [" + LoadBalancingMapRow.current_calls_DbName + "] - 1 " + 
				"WHERE [" + LoadBalancingMapRow.node_id_DbName + "] = " + 
				base.Database.CreateSqlParameterName(LoadBalancingMapRow.node_id_PropName) + " " + 
				"AND [" + LoadBalancingMapRow.customer_acct_id_DbName + "] = " + 
				base.Database.CreateSqlParameterName(LoadBalancingMapRow.customer_acct_id_PropName) + " " + 
				"SELECT @@ROWCOUNT ";
			IDbCommand cmd = base.Database.CreateCommand(sqlStr);
			AddParameter(cmd, LoadBalancingMapRow.node_id_PropName, node_id);
			AddParameter(cmd, LoadBalancingMapRow.customer_acct_id_PropName, customer_acct_id);
			int _count = (int)cmd.ExecuteScalar();
			return _count == 1;
		}

	} // End of LoadBalancingMapCollection class
} // End of namespace

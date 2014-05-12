// <fileinfo name="AccessListViewCollection.cs">
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

namespace Timok.Rbr.DAL.RbrDatabase {
	/// <summary>
	/// Represents the <c>AccessListView</c> view.
	/// </summary>
	public class AccessListViewCollection : AccessListViewCollection_Base {
		/// <summary>
		/// Initializes a new instance of the <see cref="AccessListViewCollection"/> class.
		/// </summary>
		/// <param name="db">The database object.</param>
		internal AccessListViewCollection(Rbr_Db db)
			: base(db) {
			// EMPTY
		}

		public static AccessListViewRow Parse(System.Data.DataRow row){
			return new AccessListViewCollection(null).MapRow(row);
		}

		public AccessListViewRow[] GetByCustomer_acct_id(short customer_acct_id) {
			IDbCommand cmd = this.CreateGetByCustomer_acct_idCommand(customer_acct_id);
			using(IDataReader reader = cmd.ExecuteReader()) {
				return MapRecords(reader);
			}
		}

		public DataTable GetByCustomer_acct_idAsDataTable(short customer_acct_id) {
			IDbCommand cmd = this.CreateGetByCustomer_acct_idCommand(customer_acct_id);
			using(IDataReader reader = cmd.ExecuteReader()) {
				return MapRecordsToDataTable(reader);
			}
		}

		private IDbCommand CreateGetByCustomer_acct_idCommand(short customer_acct_id){
			string _where = AccessListViewRow.customer_acct_id_DbName + "=" + 
				base.Database.CreateSqlParameterName(AccessListViewRow.customer_acct_id_PropName);
			string _order = AccessListViewRow.end_point_id_DbName;
			
			IDbCommand cmd = base.CreateGetCommand(_where, _order);
			this.AddParameter(cmd, AccessListViewRow.customer_acct_id_PropName, customer_acct_id);

			return cmd;
		}
	} // End of AccessListViewCollection class
} // End of namespace

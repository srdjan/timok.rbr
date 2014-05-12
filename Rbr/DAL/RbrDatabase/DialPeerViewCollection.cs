// <fileinfo name="DialPeerViewCollection.cs">
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
	/// Represents the <c>DialPeerView</c> view.
	/// </summary>
	public class DialPeerViewCollection : DialPeerViewCollection_Base {
		/// <summary>
		/// Initializes a new instance of the <see cref="DialPeerViewCollection"/> class.
		/// </summary>
		/// <param name="db">The database object.</param>
		internal DialPeerViewCollection(Rbr_Db db)
			: base(db) {
			// EMPTY
		}

		public static DialPeerViewRow Parse(System.Data.DataRow row){
			return new DialPeerViewCollection(null).MapRow(row);
		}
		
		public DialPeerViewRow[] GetByEndPointID(short pEndPointID){
			using(IDataReader reader = base.Database.ExecuteReader(CreateGetByEndPointIDCommand(pEndPointID))) {
				return MapRecords(reader);
			}		
		}
		
		public DialPeerViewRow[] GetByCustomerAcctId(short pCustomerAcctId){
			using(IDataReader reader = base.Database.ExecuteReader(CreateGetByCustomerAcctIdCommand(pCustomerAcctId))) {
				return MapRecords(reader);
			}		
		}

		public DialPeerViewRow GetByEndPointIDPrefix_in(short pEndPointID, string prefix_in){
			using(IDataReader reader = base.Database.ExecuteReader(CreateGetByEndPointIDPrefix_inCommand(pEndPointID, prefix_in))) {
				DialPeerViewRow[] rows = MapRecords(reader);
				return (rows.Length == 0) ? null : rows[0];
			}		
		}

		public bool HasWholesaleCustomersByEndPointIDAsDataTable(short pEndPointID){
			string _sqlStr = "SELECT COUNT(*) FROM DialPeerView WHERE ";
			_sqlStr += DialPeerViewRow.end_point_id_DbName + "=" + base.Database.CreateSqlParameterName(DialPeerViewRow.end_point_id_PropName);
			_sqlStr += " AND ";
			_sqlStr += "[" + DialPeerViewRow.service_type_DbName + "]=" + base.Database.CreateSqlParameterName(DialPeerViewRow.service_type_PropName);

			IDbCommand _cmd = base.Database.CreateCommand(_sqlStr);

			AddParameter(_cmd, DialPeerViewRow.end_point_id_PropName, pEndPointID);
			AddParameter(_cmd, DialPeerViewRow.service_type_PropName, (byte) ServiceType.Wholesale);
			
			int _count = (int)_cmd.ExecuteScalar();

			return (_count > 0) ? true : false;
		}
		
		protected System.Data.IDbCommand CreateGetByEndPointIDCommand(short pEndPointID) {
			string whereSql = DialPeerViewRow.end_point_id_DbName + "=" + 
				base.Database.CreateSqlParameterName(DialPeerViewRow.end_point_id_PropName);

			IDbCommand cmd = CreateGetCommand(whereSql, DialPeerViewRow.customer_acct_name_DbName + "," + DialPeerViewRow.prefix_in_DbName);
			AddParameter(cmd, DialPeerViewRow.end_point_id_PropName, pEndPointID);
			return cmd;
		}
		
		protected System.Data.IDbCommand CreateGetByCustomerAcctIdCommand(short pCustomerAcctId) {
			string whereSql = DialPeerViewRow.customer_acct_id_DbName + "=" + 
				base.Database.CreateSqlParameterName(DialPeerViewRow.customer_acct_id_PropName);

			IDbCommand cmd = CreateGetCommand(whereSql, DialPeerViewRow.prefix_in_DbName);
			AddParameter(cmd, DialPeerViewRow.customer_acct_id_PropName, pCustomerAcctId);
			return cmd;
		}
		
		protected System.Data.IDbCommand CreateGetByEndPointIDPrefix_inCommand(short pEndPointID, string prefix_in) {
			string whereSql = 
				DialPeerViewRow.end_point_id_DbName + "=" + 
				base.Database.CreateSqlParameterName(DialPeerViewRow.end_point_id_PropName) + 
				" AND " + 
				DialPeerViewRow.prefix_in_DbName + "=" + 
				base.Database.CreateSqlParameterName(DialPeerViewRow.prefix_in_PropName);

			IDbCommand cmd = CreateGetCommand(whereSql, null);
			AddParameter(cmd, DialPeerViewRow.end_point_id_PropName, pEndPointID);
			AddParameter(cmd, DialPeerViewRow.prefix_in_PropName, prefix_in);
			return cmd;
		}

	} // End of DialPeerViewCollection class
} // End of namespace

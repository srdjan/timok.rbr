// <fileinfo name="DialPeerCollection.cs">
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
	/// Represents the <c>DialPeer</c> table.
	/// </summary>
	public class DialPeerCollection : DialPeerCollection_Base
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="DialPeerCollection"/> class.
		/// </summary>
		/// <param name="db">The database object.</param>
		internal DialPeerCollection(Rbr_Db db)
				: base(db)
		{
			// EMPTY
		}

		public static DialPeerRow Parse(System.Data.DataRow row){
			return new DialPeerCollection(null).MapRow(row);
		}

		public int GetCountByEndPointID(short pEndPointID){
			string sqlStr = "SELECT COUNT(*) FROM DialPeer WHERE " + 
				"[" + DialPeerRow.end_point_id_DbName + "]=" + base.Database.CreateSqlParameterName(DialPeerRow.end_point_id_PropName);

			IDbCommand cmd = base.Database.CreateCommand(sqlStr);
			AddParameter(cmd, DialPeerRow.end_point_id_PropName, pEndPointID);
			return ((int) cmd.ExecuteScalar());
		}

		public int GetCountByCustomer_acct_id(short customer_acct_id){
			string sqlStr = "SELECT COUNT(*) FROM DialPeer WHERE " + 
				"[" + DialPeerRow.customer_acct_id_DbName + "]=" + base.Database.CreateSqlParameterName(DialPeerRow.customer_acct_id_PropName);

			IDbCommand cmd = base.Database.CreateCommand(sqlStr);
			AddParameter(cmd, DialPeerRow.customer_acct_id_PropName, customer_acct_id);
			return ((int) cmd.ExecuteScalar());
		}

		public override bool Update(DialPeerRow value) {
			throw new NotSupportedException();
		}

		public override void Insert(DialPeerRow value) {
			validate(value.End_point_id, value.Prefix_in);
			base.Insert (value);
		}


		public bool Update(string pNewPrefixIn, string pOldPrefixIn, DialPeerRow value) {
			if (pNewPrefixIn != pOldPrefixIn) {
				validate(value.End_point_id, pNewPrefixIn);
			}

			string _sqlStr = "UPDATE [dbo].[DialPeer] SET " +
				"[" + DialPeerRow.customer_acct_id_DbName + "]=" + base.Database.CreateSqlParameterName(DialPeerRow.customer_acct_id_PropName) + "," + 
				"[" + DialPeerRow.prefix_in_DbName + "]=" + base.Database.CreateSqlParameterName("NewPrefix_in") +
				" WHERE " +
				"[" + DialPeerRow.end_point_id_DbName + "]=" + base.Database.CreateSqlParameterName(DialPeerRow.end_point_id_PropName) + 
				" AND " +
				"[" + DialPeerRow.prefix_in_DbName + "]=" + base.Database.CreateSqlParameterName("OldPrefix_in");
			IDbCommand _cmd = Database.CreateCommand(_sqlStr);
			AddParameter(_cmd, DialPeerRow.customer_acct_id_PropName, value.Customer_acct_id);
			AddParameter(_cmd, DialPeerRow.end_point_id_PropName, value.End_point_id);
			base.Database.AddParameter(_cmd, "NewPrefix_in", DbType.AnsiString, pNewPrefixIn);
			base.Database.AddParameter(_cmd, "OldPrefix_in", DbType.AnsiString, pOldPrefixIn);
			return 0 != _cmd.ExecuteNonQuery();
		}

    private void validate(short pEndPointID, string pPrefixIn) {
      if (this.exists(pEndPointID, pPrefixIn)) {
        EndPointRow _ep = Database.EndPointCollection.GetByPrimaryKey(pEndPointID);
        throw new Exception("End Point " + _ep.ToString() + "\r\n" +
          "already defines Dial Peer with provided Prefix [" + pPrefixIn + "].\r\n");
      }
    }

		private bool exists(short pEndPointID, string pPrefixIn) {
			string sqlStr = "SELECT COUNT(*) FROM [dbo].[DialPeer] WHERE " +
				"[" + DialPeerRow.end_point_id_DbName + "]=" + base.Database.CreateSqlParameterName(DialPeerRow.end_point_id_PropName) + 
				" AND " + 
				"[" + DialPeerRow.prefix_in_DbName + "]=" + base.Database.CreateSqlParameterName(DialPeerRow.prefix_in_PropName);
			IDbCommand cmd = base.Database.CreateCommand(sqlStr);
			AddParameter(cmd, DialPeerRow.end_point_id_PropName, pEndPointID);
			AddParameter(cmd, DialPeerRow.prefix_in_PropName, pPrefixIn);
			object _count = cmd.ExecuteScalar();
			return (_count == null || (int)_count == 0) ? false : true;
		}


	} // End of DialPeerCollection class
} // End of namespace

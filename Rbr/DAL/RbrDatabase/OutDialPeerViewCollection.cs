// <fileinfo name="OutDialPeerViewCollection.cs">
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
	/// Represents the <c>OutDialPeerView</c> view.
	/// </summary>
	public class OutDialPeerViewCollection : OutDialPeerViewCollection_Base {
		/// <summary>
		/// Initializes a new instance of the <see cref="OutDialPeerViewCollection"/> class.
		/// </summary>
		/// <param name="db">The database object.</param>
		internal OutDialPeerViewCollection(Rbr_Db db)
			: base(db) {
			// EMPTY
		}

		public static OutDialPeerViewRow Parse(System.Data.DataRow row){
			return new OutDialPeerViewCollection(null).MapRow(row);
		}
		
		public OutDialPeerViewRow[] GetByEndPointID(short pEndPointID){
			using(IDataReader reader = base.Database.ExecuteReader(CreateGetByEndPointIDCommand(pEndPointID))) {
				return MapRecords(reader);
			}		
		}
		
		protected System.Data.IDbCommand CreateGetByEndPointIDCommand(short pEndPointID) {
			string whereSql = OutDialPeerViewRow.end_point_id_DbName + "=" + 
				base.Database.CreateSqlParameterName(OutDialPeerViewRow.end_point_id_PropName);

			IDbCommand cmd = CreateGetCommand(whereSql, OutDialPeerViewRow.carrier_acct_name_DbName);
			AddParameter(cmd, OutDialPeerViewRow.end_point_id_PropName, pEndPointID);
			return cmd;
		}
	} // End of OutDialPeerViewCollection class
} // End of namespace

// <fileinfo name="NodeViewCollection.cs">
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
	/// Represents the <c>NodeView</c> view.
	/// </summary>
	public class NodeViewCollection : NodeViewCollection_Base
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="NodeViewCollection"/> class.
		/// </summary>
		/// <param name="db">The database object.</param>
		internal NodeViewCollection(Rbr_Db db)
				: base(db)
		{
			// EMPTY
		}

		public static NodeViewRow Parse(System.Data.DataRow row){
			return new NodeViewCollection(null).MapRow(row);
		}
		
		public NodeViewRow GetByPrimaryKey(short pNodeId) {
			string _where = NodeViewRow.node_id_DbName + "=" + pNodeId;
			return GetRow(_where);
		}


		public NodeViewRow[] GetByNodeRoleFilter(params NodeRole[] pNodeRoles) {
			if (pNodeRoles == null || pNodeRoles.Length == 0) {
				return null;
			}

			string _where = NodeCollection.GetNodeRoleFilter(pNodeRoles);
			return GetAsArray(_where, NodeViewRow.platform_id_DbName + "," + NodeViewRow.description_DbName);
		}


		public NodeViewRow[] GetByPlatform_id(short pPlatformId) {
			using(IDataReader reader = base.Database.ExecuteReader(CreateGetByPlatform_idCommand(pPlatformId))) {
				return MapRecords(reader);
			}		
		}

		protected System.Data.IDbCommand CreateGetByPlatform_idCommand(short pPlatformId) {
			string whereSql = NodeViewRow.platform_id_DbName + "=" + 
				base.Database.CreateSqlParameterName(NodeViewRow.platform_id_PropName);

			IDbCommand cmd = CreateGetCommand(whereSql, NodeViewRow.dot_ip_address_DbName);
			AddParameter(cmd, NodeViewRow.platform_id_PropName, pPlatformId);
			return cmd;
		}
		
	} // End of NodeViewCollection class
} // End of namespace

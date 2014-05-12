// <fileinfo name="NodeCollection.cs">
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
using Timok.Core;
using Timok.NetworkLib;
using Timok.Rbr.Core.Config;
using Timok.Rbr.DAL.RbrDatabase.Base;

namespace Timok.Rbr.DAL.RbrDatabase {
	/// <summary>
	/// Represents the <c>Node</c> table.
	/// </summary>
	public class NodeCollection : NodeCollection_Base {
		/// <summary>
		/// Initializes a new instance of the <see cref="NodeCollection"/> class.
		/// </summary>
		/// <param name="db">The database object.</param>
		internal NodeCollection(Rbr_Db db)
			: base(db) {
			// EMPTY
		}

		public static NodeRow Parse(System.Data.DataRow row){
			return new NodeCollection(null).MapRow(row);
		}

		public int GetCountAll() {
			string _sqlStr = "SELECT COUNT(*) FROM Node ";

			IDbCommand _cmd = Database.CreateCommand(_sqlStr);
			return ((int) _cmd.ExecuteScalar());
		}

		public int GetCountByPlatformId(short pPlatformId) {
			string _sqlStr = "SELECT COUNT(*) FROM Node WHERE " + 
				"[" + NodeRow.platform_id_DbName + "]=" + Database.CreateSqlParameterName(NodeRow.platform_id_PropName);

			IDbCommand _cmd = Database.CreateCommand(_sqlStr);
			AddParameter(_cmd, NodeRow.platform_id_PropName, pPlatformId);
			return ((int) _cmd.ExecuteScalar());
		}

		public NodeRow GetByIPAddress(string dot_ip_address) {
			int _ip = IPUtil.ToInt32(dot_ip_address);//convert to int
			return GetByIPAddress(_ip);
		}

		public NodeRow GetByIPAddress(int ip_address) {
			string sqlStr = "SELECT * FROM [dbo].[Node] WHERE " +
				"[" + NodeRow.ip_address_DbName + "]=" + base.Database.CreateSqlParameterName(NodeRow.ip_address_PropName);
			IDbCommand cmd = base.Database.CreateCommand(sqlStr);
			base.Database.AddParameter(cmd, NodeRow.ip_address_PropName, DbType.Int32, ip_address);
			using(IDataReader reader = cmd.ExecuteReader()) {
				NodeRow[] tempArray = MapRecords(reader);
				return (tempArray.Length > 0) ? tempArray[0] : null;
			}
		}

		public override void Insert(NodeRow value) {
			NodeRow _existing = GetByIPAddress(value.Ip_address);
			if (_existing != null) {
				throw new Exception("Node with the same IP Address [" + value.DottedIPAddress + "] already exists.");
			}


			string sqlStr = "DECLARE " + base.Database.CreateSqlParameterName(NodeRow.node_id_PropName) + " smallint " + 
				"SET " + base.Database.CreateSqlParameterName(NodeRow.node_id_PropName) + 
				" = COALESCE((SELECT MAX(" + NodeRow.node_id_DbName + ") FROM Node) + 1, 1) " + 

				"INSERT INTO [dbo].[Node] (" +
				"[" + NodeRow.node_id_DbName + "], " +
				"[" + NodeRow.platform_id_DbName + "], " +
				"[" + NodeRow.description_DbName + "], " +
				"[" + NodeRow.node_config_DbName + "], " +
				"[" + NodeRow.transport_type_DbName + "], " +
				"[" + NodeRow.user_name_DbName + "], " +
				"[" + NodeRow.password_DbName + "], " +
				"[" + NodeRow.ip_address_DbName + "], " +
				"[" + NodeRow.port_DbName + "], " +
				"[" + NodeRow.status_DbName + "], " +
				"[" + NodeRow.billing_export_frequency_DbName + "], " +
				"[" + NodeRow.cdr_publishing_frequency_DbName + "]" +
				") VALUES (" +
				base.Database.CreateSqlParameterName(NodeRow.node_id_PropName) + ", " +
				base.Database.CreateSqlParameterName(NodeRow.platform_id_PropName) + ", " +
				base.Database.CreateSqlParameterName(NodeRow.description_PropName) + ", " +
				base.Database.CreateSqlParameterName(NodeRow.node_config_PropName) + ", " +
				base.Database.CreateSqlParameterName(NodeRow.transport_type_PropName) + ", " +
				base.Database.CreateSqlParameterName(NodeRow.user_name_PropName) + ", " + 
				base.Database.CreateSqlParameterName(NodeRow.password_PropName) + ", " + 
				base.Database.CreateSqlParameterName(NodeRow.ip_address_PropName) + ", " + 
				base.Database.CreateSqlParameterName(NodeRow.port_PropName) + ", " + 
				base.Database.CreateSqlParameterName(NodeRow.status_PropName) + ", " + 
				base.Database.CreateSqlParameterName(NodeRow.billing_export_frequency_PropName) + ", " + 
				base.Database.CreateSqlParameterName(NodeRow.cdr_publishing_frequency_PropName) + ")" + 
				" SELECT " + base.Database.CreateSqlParameterName(NodeRow.node_id_PropName);

			IDbCommand cmd = base.Database.CreateCommand(sqlStr);
			//base.Database.AddParameter(cmd, NodeRow.node_id_PropName, DbType.Int16, value.Node_id);
			base.Database.AddParameter(cmd, NodeRow.platform_id_PropName, DbType.Int16, value.Platform_id);
			base.Database.AddParameter(cmd, NodeRow.description_PropName, DbType.AnsiString, value.Description);
			base.Database.AddParameter(cmd, NodeRow.node_config_PropName, DbType.Int32, value.Node_config);
			base.Database.AddParameter(cmd, NodeRow.transport_type_PropName, DbType.Byte, value.Transport_type);
			base.Database.AddParameter(cmd, NodeRow.user_name_PropName, DbType.AnsiString, value.User_name);
			base.Database.AddParameter(cmd, NodeRow.password_PropName, DbType.AnsiString, value.Password);
			base.Database.AddParameter(cmd, NodeRow.ip_address_PropName, DbType.Int32, value.Ip_address);
			base.Database.AddParameter(cmd, NodeRow.port_PropName, DbType.Int32, value.Port);
			base.Database.AddParameter(cmd, NodeRow.status_PropName, DbType.Byte, value.Status);

			base.Database.AddParameter(cmd, NodeRow.billing_export_frequency_PropName, DbType.Int32, value.Billing_export_frequency);
			base.Database.AddParameter(cmd, NodeRow.cdr_publishing_frequency_PropName, DbType.Int32, value.Cdr_publishing_frequency);

			object _res = cmd.ExecuteScalar();

			value.Node_id = (short)_res;
		}

		
//		public NodeRow[] GetSoftSwitchNodes() {
//			string _where = getByNodeRole_WhereSql(NodeRoleFlags.H323);
//			return GetAsArray(_where, NodeRow.platform_id_DbName + "," + NodeRow.description_DbName);
//		}
//		
//		public NodeRow[] GetSoftSwitchNodesByPlatform_id(short pPlatform_id) {
//			string _where = "[" + NodeRow.platform_id_DbName + "]=" + pPlatform_id + 
//				" AND " + 
//				getByNodeRole_WhereSql(NodeRoleFlags.H323);
//			return GetAsArray(_where, NodeRow.description_DbName);
//		}
//		
//		public NodeRow[] GetGuiHostNodes() {
//			string _where = getByNodeRole_WhereSql(NodeRoleFlags.GuiHost);
//			return GetAsArray(_where, NodeRow.platform_id_DbName + "," + NodeRow.description_DbName);
//		}
//		
//		public NodeRow GetGuiHostNodeByPlatform_id(short pPlatform_id) {
//			string _where = "[" + NodeRow.platform_id_DbName + "]=" + pPlatform_id + 
//				" AND " + 
//				getByNodeRole_WhereSql(NodeRoleFlags.GuiHost);
//			return GetRow(_where);
//		}
//		
//		public NodeRow[] GetWebReportsHostNodes() {
//			string _where = getByNodeRole_WhereSql(NodeRoleFlags.WebReportsHost);
//			return GetAsArray(_where, NodeRow.platform_id_DbName + "," + NodeRow.description_DbName);
//		}
//		
//		public NodeRow GetWebReportsHostNodesByPlatform_id(short pPlatform_id) {
//			string _where = "[" + NodeRow.platform_id_DbName + "]=" + pPlatform_id + 
//				" AND " + 
//				getByNodeRole_WhereSql(NodeRoleFlags.WebReportsHost);
//			return GetRow(_where);
//		}

		private new IDbCommand CreateGetByPlatform_idCommand(short platform_id) {
			string whereSql = "[" + NodeRow.platform_id_DbName + "]=" + 
				this.Database.CreateSqlParameterName(NodeRow.platform_id_PropName);

			IDbCommand cmd = CreateGetCommand(whereSql, NodeRow.node_config_DbName + "," + NodeRow.description_DbName);
			AddParameter(cmd, NodeRow.platform_id_PropName, platform_id);
			return cmd;
		}

		public NodeRow[] GetByPlatformIdAndNodeRoleFilter(short pPlatformId, params NodeRole[] pNodeRoles) {
			if (pNodeRoles == null || pNodeRoles.Length == 0 || pPlatformId <= 0) {
				return null;
			}

			string _where = "[" + NodeRow.platform_id_DbName + "]=" + pPlatformId + 
				" AND " + GetNodeRoleFilter(pNodeRoles);

			return GetAsArray(_where, NodeRow.platform_id_DbName + "," + NodeRow.description_DbName);
		}

		public NodeRow[] GetByNodeRoleFilter(params NodeRole[] pNodeRoles) {
			if (pNodeRoles == null || pNodeRoles.Length == 0) {
				return null;
			}

			string _where = GetNodeRoleFilter(pNodeRoles);
			return GetAsArray(_where, NodeRow.platform_id_DbName + "," + NodeRow.description_DbName);
		}

		public static string GetNodeRoleFilter(params NodeRole[] pNodeRoles) {
			string _filter = string.Empty;
			foreach(int _NodeRole in pNodeRoles){
				_filter += "," + _NodeRole;
			}
			_filter = _filter.TrimStart(',');
			return " " + NodeViewRow.node_config_DbName + " IN(" + _filter + ") ";
		}
	} // End of NodeCollection class
} // End of namespace

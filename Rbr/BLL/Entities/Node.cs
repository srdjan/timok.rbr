using System;
using System.Collections.Generic;
using Timok.NetworkLib;
using Timok.Rbr.Core.Config;
using Timok.Rbr.DAL.RbrDatabase;

namespace Timok.Rbr.BLL.Entities {
	public class Node {
		NodeRow nodeRow;
		PlatformRow platformRow;

		public short Id { get { return nodeRow.Node_id; } }
		public short PlatformId { get { return nodeRow.Platform_id; } }

		public int IPAddress { get { return nodeRow.Ip_address; } }
		public string IPAddressString { get { return nodeRow.DottedIPAddress; } }
		public int Port { get { return nodeRow.Port; } }
		public string UserName { get { return nodeRow.User_name; } }
		public string Password { get { return nodeRow.Password; } }

		public NodeRole NodeRole { get { return Configuration.Instance.Main.NodeRole; } }

		public bool IsH323 { get { return Configuration.Instance.Main.NodeRole == NodeRole.H323; } }
		public bool IsSIP { get { return Configuration.Instance.Main.NodeRole == NodeRole.SIP; } }
		public bool IsNotSIP { get { return !IsSIP; } }

		public bool IsAdmin { get { return Configuration.Instance.Main.NodeRole == NodeRole.Admin; } }
		public bool IsNotAdmin { get { return !IsAdmin; } }

		public int CdrPublishingFrequency { get { return nodeRow.Cdr_publishing_frequency; } }
		public int BillingExportFrequency { get { return nodeRow.Billing_export_frequency; } }

		public Status Status { get { return (Status) nodeRow.Status; } }

		public bool BelongsToStandalonePlatform { get { return platformRow.PlatformConfiguration == PlatformConfig.Standalone; } }

		public Node(int pIpAddress) {
			using (var _db = new Rbr_Db()) {
				nodeRow = _db.NodeCollection.GetByIPAddress(pIpAddress);
			}
			init(nodeRow, pIpAddress);
		}

		Node() {}

		//-- Get all nodes of a particular type: 
		public static Node[] GetNodes(params NodeRole[] pNodeRoleFilters) {
			NodeRow[] _nodeRows;
			using (var _db = new Rbr_Db()) {
				_nodeRows = _db.NodeCollection.GetByNodeRoleFilter(pNodeRoleFilters);
			}
			if (_nodeRows == null || _nodeRows.Length == 0) {
				return null;
			}

			//-- Load all nodeRows into node array:
			var _nodeList = new List<Node>();
			for (var _i = 0; _i < _nodeRows.Length; _i++) {
				var _node = new Node();
				_node.init(_nodeRows[_i], _nodeRows[_i].Ip_address);
				_nodeList.Add(_node);
			}
			var _nodes = _nodeList.ToArray();
			return _nodes;
		}

		//-- this conversion from Node -> TargetNode is done because Replication assembly doesn't have access to DOM (Node)
		public static Node[] GetTargetNodes() {
			Node[] _nodes = null;
			var _currentNode = new CurrentNode();
			if (!_currentNode.BelongsToStandalonePlatform) {
				if (_currentNode.IsAdmin) {
					_nodes = GetNodes(new NodeRole[1] { NodeRole.SIP });
				}
				else if (_currentNode.IsSIP) {
					_nodes = GetNodes(new NodeRole[1] { NodeRole.Admin });
				}
				else {
					throw new Exception(string.Format("GetTarget Nodes | Inknown Node role, id: {0}", _currentNode.Id));
				}
			}
			return _nodes;
		}

		//------------------------------- Private ----------------------------------------------
		void init(NodeRow pNodeRow, int pIPAddress) {
			if (pNodeRow == null) {
				throw new Exception("Node: " + IPUtil.ToString(pIPAddress) + " NOT FOUND");
			}
			nodeRow = pNodeRow;

			//-- get site
			using (var _db = new Rbr_Db()) {
				platformRow = _db.PlatformCollection.GetByPrimaryKey(nodeRow.Platform_id);
			}
			if (platformRow == null) {
				throw new Exception("Site: " + nodeRow.Platform_id + " NOT FOUND");
			}
		}

		//void init(Rbr_Db pDb, NodeRow pNodeRow, int pIPAddress) {
		//  if (pNodeRow == null) {
		//    throw new Exception("Node: " + IPUtil.ToString(pIPAddress) + " NOT FOUND");
		//  }
		//  nodeRow = pNodeRow;

		//  //-- get site
		//  platformRow = pDb.PlatformCollection.GetByPrimaryKey(nodeRow.Platform_id);
		//  if (platformRow == null) {
		//    throw new Exception("Site: " + nodeRow.Platform_id + " NOT FOUND");
		//  }
		//}
	}
}
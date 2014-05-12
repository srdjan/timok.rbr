using System;
using System.Data;
using Timok.NetworkLib;
using Timok.Rbr.Core.Config;
using Timok.Rbr.DAL.RbrDatabase;
using Timok.Rbr.DAL.RbrDatabase.Base;

namespace Timok.Rbr.BLL.Managers {
	internal class PlatformManager {
		PlatformManager() { }

		#region Getters

		internal static PlatformRow[] GetAll(Rbr_Db pDb) { return pDb.PlatformCollection.GetAll(); }

		internal static bool IsLocationInUse(Rbr_Db pDb, string pLocation, short pPlatformId) { return pDb.PlatformCollection.IsLocationInUseByOtherPlatform(pLocation, pPlatformId); }

		internal static NodeViewRow[] GetAllNodeViews(Rbr_Db pDb) { return NodeManager.GetAllViews(pDb); }

		internal static NodeViewRow[] GetAllNodeViews(Rbr_Db pDb, short pPlatformId) { return NodeManager.GetAllViews(pDb, pPlatformId); }

		internal static DataTable GetAllNodesAsDataTable(Rbr_Db pDb) { return NodeManager.GetAllAsDataTable(pDb); }

		internal static NodeViewRow[] GetAllSoftSwitchNodeViews(Rbr_Db pDb) { return NodeManager.GetAllSoftSwitchViews(pDb); }

		internal static NodeViewRow GetNodeView(Rbr_Db pDb, short pNodeId) { return NodeManager.GetView(pDb, pNodeId); }

		internal static NodeRow[] GetAllNodes(Rbr_Db pDb, short pPlatformId) { return NodeManager.GetAll(pDb, pPlatformId); }

		internal static NodeRow GetNode(Rbr_Db pDb, short pNodeId) { return NodeManager.Get(pDb, pNodeId); }

		internal static NodeRow GetNodeByIPAddress(Rbr_Db pDb, int pIPAddress) { return NodeManager.GetByIPAddress(pDb, pIPAddress); }

		internal static NodeRow GetNodeByIPAddress(Rbr_Db pDb, string pIPAddress) { return NodeManager.GetByIPAddress(pDb, pIPAddress); }

		internal static NodeRow[] GetAllSoftSwitchNodes(Rbr_Db pDb) { return NodeManager.GetAllSoftSwitch(pDb); }

		internal static NodeRow[] GetAllIVR(Rbr_Db pDb) { return NodeManager.GetAllIVR(pDb); }

		internal static int GetNodeCount(Rbr_Db pDb) { return getNodeCount(pDb); }

		#endregion Getters

		#region Actions

		internal static void AddPlatform(Rbr_Db pDb, PlatformRow pPlatform) { pDb.PlatformCollection.Insert(pPlatform); }

		internal static void UpdatePlatform(Rbr_Db pDb, PlatformRow pPlatform) { pDb.PlatformCollection.Update(pPlatform); }

		internal static void DeletePlatform(Rbr_Db pDb, PlatformRow pPlatform) {
			NodeRow[] _nodes = NodeManager.GetAll(pDb, pPlatform.Platform_id);
			if (_nodes != null && _nodes.Length > 0) {
				throw new ApplicationException("Platform has existing child Nodes. Cannot delete.");
			}
			pDb.PlatformCollection.Delete(pPlatform);
		}

		internal static void AddNode(Rbr_Db pDb, NodeRow pNodeRow) { NodeManager.Add(pDb, pNodeRow); }

		internal static void UpdateNode(Rbr_Db pDb, NodeRow pNodeRow) { NodeManager.Update(pDb, pNodeRow); }

		internal static void DeleteNode(Rbr_Db pDb, NodeRow pNode) { NodeManager.Delete(pDb, pNode); }

		internal static void CreateDefaultPlatform(Rbr_Db pDb) {
			//NOTE: HARDCODED VALUE - platform_id = 1
			PlatformRow _firstPlatform = pDb.PlatformCollection.GetByPrimaryKey(1);
			if (_firstPlatform == null) {
				_firstPlatform = new PlatformRow
				                 	{
				                 		PlatformConfiguration = PlatformConfig.Standalone,
				                 		Location = "Please Enter Real Location",
				                 		Status = ((byte) Status.Active)
				                 	};
				addPlatform(pDb, _firstPlatform);
			}

			//add current node
			NodeManager.CreateCurrentNode(pDb, _firstPlatform.Platform_id);
		}

		#endregion Actions

		//------------------- privates ------------------------------------------------
		static int getNodeCount(Rbr_Db_Base pDb) { return pDb.NodeCollection.GetCountAll(); }

		static void addPlatform(Rbr_Db_Base pDb, PlatformRow pPlatform) { pDb.PlatformCollection.Insert(pPlatform); }

		//------------------ Internal Class NodeMngr ----------------------------------

		#region Nested type: NodeManager

		internal class NodeManager {
			NodeManager() { }

			#region Internals

			#region Getters

			internal static NodeViewRow GetView(Rbr_Db pDb, short pNodeId) { return pDb.NodeViewCollection.GetByPrimaryKey(pNodeId); }

			internal static NodeViewRow[] GetAllViews(Rbr_Db pDb) { return pDb.NodeViewCollection.GetAll(); }

			internal static NodeViewRow[] GetAllViews(Rbr_Db pDb, short pPlatformId) { return pDb.NodeViewCollection.GetByPlatform_id(pPlatformId); }

			internal static NodeViewRow[] GetAllSoftSwitchViews(Rbr_Db pDb) { return pDb.NodeViewCollection.GetByNodeRoleFilter(NodeRole.H323); }

			internal static NodeViewRow[] GetAllIVRViews(Rbr_Db pDb) { return pDb.NodeViewCollection.GetByNodeRoleFilter(NodeRole.SIP); }

			internal static DataTable GetAllAsDataTable(Rbr_Db pDb) { return pDb.NodeCollection.GetAllAsDataTable(); }

			internal static NodeRow Get(Rbr_Db pDb, short pNodeId) { return pDb.NodeCollection.GetByPrimaryKey(pNodeId); }

			internal static NodeRow GetByIPAddress(Rbr_Db pDb, int pIPAddress) { return pDb.NodeCollection.GetByIPAddress(pIPAddress); }

			internal static NodeRow GetByIPAddress(Rbr_Db pDb, string pIPAddress) { return pDb.NodeCollection.GetByIPAddress(pIPAddress); }

			internal static NodeRow[] GetAll(Rbr_Db pDb, short pPlatformId) { return pDb.NodeCollection.GetByPlatform_id(pPlatformId); }

			internal static NodeRow[] GetAllSoftSwitch(Rbr_Db pDb) { return pDb.NodeCollection.GetByNodeRoleFilter(NodeRole.H323); }

			internal static NodeRow[] GetAllIVR(Rbr_Db pDb) { return pDb.NodeCollection.GetByNodeRoleFilter(NodeRole.SIP); }

			#endregion Getters

			#region Actions

			//NOTE: !!! Manual Replication required !!!
			internal static void Add(Rbr_Db pDb, NodeRow pNodeRow) {
				validateRole(pDb, pNodeRow);
				pDb.NodeCollection.Insert(pNodeRow);
			}

			//NOTE: !!! Manual Replication required !!!
			internal static void Update(Rbr_Db pDb, NodeRow pNodeRow) { pDb.NodeCollection.Update(pNodeRow); }

			//NOTE: !!! Manual Replication required !!!
			internal static void Delete(Rbr_Db pDb, NodeRow pNodeRow) {
				if (pNodeRow.DottedIPAddress == Configuration.Instance.Main.HostIP) {
					return;
				}
				LoadBalancingMapManager.Delete(pDb, pNodeRow);
				pDb.NodeCollection.Delete(pNodeRow);
			}

			internal static void CreateCurrentNode(Rbr_Db pDb, short pPlatformId) {
				//add DEFAULT NODE  - GuiHost and SoftSwitch
				var _nodeRow = new NodeRow
				               	{
				               		Platform_id = pPlatformId,
				               		NodeRole = NodeRole.H323
				               	};
				_nodeRow.Description = _nodeRow.NodeRole.ToString();
				_nodeRow.Transport_type = (byte) TransportType.Ftp;
				_nodeRow.User_name = Configuration.Instance.Main.HostName;
				_nodeRow.Password = Configuration.Instance.Main.HostIP;
				_nodeRow.Ip_address = IPUtil.ToInt32(Configuration.Instance.Main.HostIP);
				_nodeRow.Port = 21;
				_nodeRow.Status = (byte) Status.Active;
				_nodeRow.Billing_export_frequency = 0;
				_nodeRow.Cdr_publishing_frequency = 0;

				pDb.NodeCollection.Insert(_nodeRow);
			}

			#endregion Actions

			#endregion Internals

			#region privates

			static void validateRole(Rbr_Db_Base pDb, NodeRow pNodeRow) {
				if (pNodeRow.NodeRole == NodeRole.Admin) {
					var _siteNodes = pDb.NodeCollection.GetByPlatform_id(pNodeRow.Platform_id);
					foreach (var _node in _siteNodes) {
						if (_node.NodeRole == NodeRole.Admin) {
							throw new ApplicationException("Error: Only one node per Platform can have role: Admin");
						}
					}
				}
			}

			#endregion privates
		}

		#endregion
	}
}
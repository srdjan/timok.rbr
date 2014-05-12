using System;
using System.Collections.Generic;
using Timok.Logger;
using Timok.Rbr.BLL.DTO;
using Timok.Rbr.BLL.Entities;
using Timok.Rbr.BLL.Managers;
using Timok.Rbr.Core.Config;
using Timok.Rbr.DAL.RbrDatabase;
using Timok.Rbr.DAL.RbrDatabase.Base;
using Timok.Rbr.DTO;

namespace Timok.Rbr.BLL.Controllers {
	public delegate void PlatformAddedDelegate(PlatformDto pPlatform);

	public delegate void PlatformChangedDelegate(PlatformDto pPlatform);

	public delegate void PlatformDeletedDelegate(PlatformDto pPlatform);

	public class PlatformController {
		PlatformController() {}

		public static CurrentPlatformNodeDto CurrentPlatformNode { 
			get {
				NodeRow _nodeRow;
				PlatformRow _platformRow;
				using (var _db = new Rbr_Db()) {
					_nodeRow = _db.NodeCollection.GetByIPAddress(Configuration.Instance.Main.HostIP);
					if (_nodeRow == null) {
						throw new Exception(string.Format("CurrentPlatformNode: Unknown IP: {0}", Configuration.Instance.Main.HostIP));
					}
					_platformRow = _db.PlatformCollection.GetByPrimaryKey(_nodeRow.Platform_id);
					if (_platformRow == null) {
						throw new Exception(string.Format("CurrentPlatformNode: Unknown platform: {0}", Configuration.Instance.Main.HostIP));
					}
				}
				return new CurrentPlatformNodeDto(_nodeRow.Node_id, 
																					_nodeRow.NodeRole, 
																					_platformRow.PlatformConfiguration == PlatformConfig.Standalone); 
			} 
		}

		#region Public DTO Getters

		public static PlatformDto[] GetAllPlatforms() {
			using (var _db = new Rbr_Db()) {
				return MapToPlatforms(PlatformManager.GetAll(_db));
			}
		}

		public static bool IsPlatformLocationInUse(string pLocation, short pPlatformId) {
			using (var _db = new Rbr_Db()) {
				return PlatformManager.IsLocationInUse(_db, pLocation, pPlatformId);
			}
		}

		public static NodeDto GetNode(short pNodeId) {
			using (var _db = new Rbr_Db()) {
				return getNodeInfo(_db, _db.NodeCollection.GetByPrimaryKey(pNodeId));
			}
		}

		public static NodeDto GetNodeByIPAddress(int pIPAddress) {
			using (var _db = new Rbr_Db()) {
				return getNodeInfo(_db, _db.NodeCollection.GetByIPAddress(pIPAddress));
			}
		}

		public static NodeDto GetNodeByIPAddress(string pIPAddress) {
			using (var _db = new Rbr_Db()) {
				return getNodeInfo(_db, _db.NodeCollection.GetByIPAddress(pIPAddress));
			}
		}

		public static NodeDto[] GetAllNodes() {
			using (var _db = new Rbr_Db()) {
				return MapToNodes(_db, _db.NodeCollection.GetAll());
			}
		}

		public static NodeDto[] GetAllNodes(short pPlatformId) {
			using (var _db = new Rbr_Db()) {
				return MapToNodes(_db, _db.NodeCollection.GetByPlatform_id(pPlatformId));
			}
		}

		public static NodeDto[] GetAllSoftSwitchNodes() {
			using (var _db = new Rbr_Db()) {
				return MapToNodes(_db, _db.NodeCollection.GetByNodeRoleFilter(NodeRole.H323));
			}
		}

		public static NodeDto[] GetAllIVRNodes() {
			using (var _db = new Rbr_Db()) {
				return MapToNodes(_db, _db.NodeCollection.GetByNodeRoleFilter(NodeRole.SIP));
			}
		}

		#endregion Public DTO Getters

		#region Public Actions

		public static bool Init() {
			using (var _db = new Rbr_Db()) {
				var _nodeCount = PlatformManager.GetNodeCount(_db);
				if (_nodeCount == 0) {
					_db.BeginTransaction();
					try {
						//add DEFAULT Platform - PlatformConfiguration.Instance.Standalone
						PlatformManager.CreateDefaultPlatform(_db);

						//TODO: remove - not needed, Currrent Node is not singleton anymore
						//reset CusrrentNode to load new(created) Node
						//CurrentNode.Instance.Reload(_db);

						_db.CommitTransaction();
					}
					catch {
						_db.RollbackTransaction();
						throw;
					}
				}
				return true;
			}
		}

		public static void AddPlatform(PlatformDto pPlatform, out Result pResult) {
			pResult = new Result();
			using (var _db = new Rbr_Db()) {
				_db.BeginTransaction();
				try {
					if (PlatformManager.IsLocationInUse(_db, pPlatform.Location, pPlatform.PlatformId)) {
						throw new Exception("Platform with the same name already exists.");
					}
					var _platformRow = MapToPlatformRow(pPlatform);
					PlatformManager.AddPlatform(_db, _platformRow);
					pPlatform.PlatformId = _platformRow.Platform_id;
					_db.CommitTransaction();
				}
				catch (Exception _ex) {
					pPlatform.PlatformId = 0;
					_db.RollbackTransaction();
					pResult.Success = false;
					pResult.ErrorMessage = _ex.Message;
					TimokLogger.Instance.LogRbr(LogSeverity.Critical, "PlatformController.AddPlatform", string.Format("Exception:\r\n{0}", _ex));
				}
			}
		}

		public static void UpdatePlatform(PlatformDto pPlatform, out Result pResult) {
			pResult = new Result();
			using (var _db = new Rbr_Db()) {
				_db.BeginTransaction();
				try {
					if (PlatformManager.IsLocationInUse(_db, pPlatform.Location, pPlatform.PlatformId)) {
						throw new Exception("Platform with the same name already exists.");
					}
					var _platformRow = MapToPlatformRow(pPlatform);
					PlatformManager.UpdatePlatform(_db, _platformRow);
					_db.CommitTransaction();
				}
				catch (Exception _ex) {
					_db.RollbackTransaction();
					pResult.Success = false;
					pResult.ErrorMessage = _ex.Message;
					TimokLogger.Instance.LogRbr(LogSeverity.Critical, "PlatformController.UpdatePlatform", string.Format("Exception:\r\n{0}", _ex));
				}
			}
		}

		public static void DeletePlatform(PlatformDto pPlatform, out Result pResult) {
			pResult = new Result();
			using (var _db = new Rbr_Db()) {
				_db.BeginTransaction();
				try {
					var _currNode = PlatformManager.GetNode(_db, CurrentPlatformNode.Id);
					if (_currNode.Platform_id == pPlatform.PlatformId) {
						throw new Exception("Cannot delete Current Platform.");
					}

					var _platformRow = MapToPlatformRow(pPlatform);
					PlatformManager.DeletePlatform(_db, _platformRow);
					_db.CommitTransaction();
				}
				catch (Exception _ex) {
					_db.RollbackTransaction();
					pResult.Success = false;
					pResult.ErrorMessage = _ex.Message;
					TimokLogger.Instance.LogRbr(LogSeverity.Critical, "PlatformController.DeletePlatform", string.Format("Exception:\r\n{0}", _ex));
				}
			}
		}

		public static void AddNode(NodeDto pNode, out Result pResult) {
			pResult = new Result();
			using (var _db = new Rbr_Db()) {
				_db.BeginTransaction();
				try {
					var _nodeRow = MapToNodeRow(pNode);
					PlatformManager.AddNode(_db, _nodeRow);
					_db.CommitTransaction();
				}
				catch (Exception _ex) {
					_db.RollbackTransaction();
					pResult.Success = false;
					pResult.ErrorMessage = _ex.Message;
					TimokLogger.Instance.LogRbr(LogSeverity.Critical, "PlatformController.AddNode", string.Format("Exception:\r\n{0}", _ex));
				}
			}
		}

		public static void UpdateNode(NodeDto pNode, out Result pResult) {
			pResult = new Result();
			using (var _db = new Rbr_Db()) {
				_db.BeginTransaction();
				try {
					//TODO: TO BE removed
					//NodeRow _original = PlatformManager.GetNode(_db, pNode.NodeId);

					//if (_original.NodeRole == NodeRole.H323 && pNode.NodeRole != NodeRole.H323) {
					//  LoadBalancingMapManager.Delete(_db, pNode);
					//}

					var _nodeRow = MapToNodeRow(pNode);
					PlatformManager.UpdateNode(_db, _nodeRow);

					//TODO: 
					//if (pNode.NodeId == CurrentNode.Instance.Id) {
					//  CurrentNode.Instance.Reload(_db);
					//}

					_db.CommitTransaction();
				}
				catch (Exception _ex) {
					_db.RollbackTransaction();
					pResult.Success = false;
					pResult.ErrorMessage = _ex.Message;
					TimokLogger.Instance.LogRbr(LogSeverity.Critical, "PlatformController.UpdateNode", string.Format("Exception:\r\n{0}", _ex));
				}
			}
		}

		public static void DeleteNode(NodeDto pNode, out Result pResult) {
			pResult = new Result();
			using (var _db = new Rbr_Db()) {
				_db.BeginTransaction();
				try {
					if (pNode.NodeId == (new CurrentNode()).Id) {
						throw new Exception("Cannot Delete Current Node.");
					}
					//LoadBalancingMapManager.Delete(_db, pNode);
					var _nodeRow = MapToNodeRow(pNode);
					PlatformManager.DeleteNode(_db, _nodeRow);

					_db.CommitTransaction();
				}
				catch (Exception _ex) {
					_db.RollbackTransaction();
					pResult.Success = false;
					pResult.ErrorMessage = _ex.Message;
					TimokLogger.Instance.LogRbr(LogSeverity.Critical, "PlatformController.DeleteNode", string.Format("Exception:\r\n{0}", _ex));
				}
			}
		}

		#endregion Public Actions

		#region mappings

		#region To DAL mappings

		internal static PlatformRow MapToPlatformRow(PlatformDto pPlatform) {
			if (pPlatform == null) {
				return null;
			}

			PlatformRow _platformRow = new PlatformRow();
			_platformRow.Platform_id = pPlatform.PlatformId;
			_platformRow.Location = pPlatform.Location;
			_platformRow.Status = (byte) pPlatform.Status;
			_platformRow.PlatformConfiguration = pPlatform.PlatformConfig;

			return _platformRow;
		}

		internal static NodeRow MapToNodeRow(NodeDto pNode) {
			if (pNode == null) {
				return null;
			}

			NodeRow _nodeRow = new NodeRow();
			_nodeRow.Node_id = pNode.NodeId;
			_nodeRow.Platform_id = pNode.PlatformId;
			_nodeRow.Billing_export_frequency = pNode.BillingExportFrequency;
			_nodeRow.Cdr_publishing_frequency = pNode.CdrPublishingFrequency;
			_nodeRow.Description = pNode.Description;
			_nodeRow.Ip_address = pNode.IpAddress;
			_nodeRow.NodeRole = pNode.NodeRole;
			_nodeRow.Password = pNode.Password;
			_nodeRow.Port = pNode.Port;
			_nodeRow.Status = (byte) pNode.Status;
			_nodeRow.Transport_type = (byte) pNode.TransportType;
			_nodeRow.User_name = pNode.UserName;

			return _nodeRow;
		}

		#endregion To DAL mappings

		#region To BLL mappings

		internal static PlatformDto[] MapToPlatforms(PlatformRow[] pPlatformRows) {
			List<PlatformDto> _list = new List<PlatformDto>();
			if (pPlatformRows != null) {
				foreach (PlatformRow _platformRow in pPlatformRows) {
					_list.Add(MapToPlatform(_platformRow));
				}
			}
			return _list.ToArray();
		}

		internal static PlatformDto MapToPlatform(PlatformRow pPlatformRow) {
			if (pPlatformRow == null) {
				return null;
			}

			PlatformDto _platform = new PlatformDto();
			_platform.PlatformId = pPlatformRow.Platform_id;
			_platform.Location = pPlatformRow.Location;
			_platform.Status = (Status) pPlatformRow.Status;
			_platform.PlatformConfig = pPlatformRow.PlatformConfiguration;

			return _platform;
		}

		internal static NodeDto[] MapToNodes(Rbr_Db pDb, NodeRow[] pNodeRows) {
			List<NodeDto> _list = new List<NodeDto>();
			if (pNodeRows != null) {
				foreach (NodeRow _nodeRow in pNodeRows) {
					_list.Add(getNodeInfo(pDb, _nodeRow));
				}
			}
			return _list.ToArray();
		}

		internal static NodeDto MapToNode(NodeRow pNodeRow, PlatformDto pPlatform) {
			if (pNodeRow == null) {
				return null;
			}

			NodeDto _node = new NodeDto();
			_node.NodeId = pNodeRow.Node_id;
			_node.BillingExportFrequency = pNodeRow.Billing_export_frequency;
			_node.CdrPublishingFrequency = pNodeRow.Cdr_publishing_frequency;
			_node.Description = pNodeRow.Description;
			_node.IpAddress = pNodeRow.Ip_address;
			_node.NodeRole = pNodeRow.NodeRole;
			_node.Password = pNodeRow.Password;
			_node.Port = pNodeRow.Port;
			_node.Status = (Status) pNodeRow.Status;
			_node.TransportType = (TransportType) pNodeRow.Transport_type;
			_node.UserName = pNodeRow.User_name;

			_node.Platform = pPlatform;

			return _node;
		}

		#endregion To BLL mappings

		#endregion mappings

		static NodeDto getNodeInfo(Rbr_Db_Base pDb, NodeRow pNodeRow) {
			if (pNodeRow == null) {
				return null;
			}

			var _platform = MapToPlatform(pDb.PlatformCollection.GetByPrimaryKey(pNodeRow.Platform_id));
			return MapToNode(pNodeRow, _platform);
		}
	}
}
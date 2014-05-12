using System;
using Timok.Core;
using Timok.NetworkLib;
using Timok.Rbr.Core.Config;
using Timok.Rbr.DTO;

namespace Timok.Rbr.BLL.DTO {
	[Serializable]
	public class NodeDto : DTOBase {
		public const string BillingExportFrequency_PropName = "BillingExportFrequency";
		public const string CdrPublishingFrequency_PropName = "CdrPublishingFrequency";
		public const string Description_PropName = "Description";
		public const string DottedIPAddress_PropName = "DottedIPAddress";
		public const string IpAddress_PropName = "IpAddress";
		public const string IsGuiHost_PropName = "IsGuiHost";
		public const string NodeId_PropName = "NodeId";
		public const string NodeRole_PropName = "NodeRole";
		public const string Password_PropName = "Password";
		public const string PlatformId_PropName = "PlatformId";
		public const string Port_PropName = "Port";
		public const string Status_PropName = "Status";
		public const string TransportType_PropName = "TransportType";
		public const string UserName_PropName = "UserName";
		int ipAddress;

		short nodeId;
		PlatformDto platform;
		public NodeDto() { Port = 21; }
		public short NodeId { get { return nodeId; } set { nodeId = value; } }

		public string Description { get; set; }

		public NodeRole NodeRole { get; set; }

		public TransportType TransportType { get; set; }

		public string UserName { get; set; }

		public string Password { get; set; }

		public string DottedIPAddress { get { return IPUtil.ToString(ipAddress); } set { ipAddress = IPUtil.ToInt32(value); } }

		public int IpAddress { get { return ipAddress; } set { ipAddress = value; } }

		public int Port { get; set; }

		public Status Status { get; set; }

		public int BillingExportFrequency { get; set; }

		public int CdrPublishingFrequency { get; set; }

		public PlatformDto Platform { get { return platform; } set { platform = value; } }

		public short PlatformId {
			get {
				if (platform != null) {
					return platform.PlatformId;
				}
				return 0;
			}
		}

		public bool IsGuiHost { get { return (NodeRole == NodeRole.Admin || Platform.PlatformConfig == PlatformConfig.Standalone); } }

		public override string ToString() { return Description + "  [ " + DottedIPAddress + " ]"; }

		public override bool Equals(object obj) {
			if (obj == null || obj.GetType() != GetType()) {
				return false;
			}

			return ObjectComparer.AreEqual(this, obj);
		}

		public override int GetHashCode() { return nodeId.GetHashCode(); }
	}
}
using Timok.Rbr.Core.Config;

namespace Timok.Rbr.BLL.DTO {
	public class CurrentPlatformNodeDto {
		readonly bool belongsToStandalonePlatform;
		readonly short id;
		readonly NodeRole nodeRole;

		public CurrentPlatformNodeDto(short pNodeId, NodeRole pNodeRole, bool pBelongsToStandalonePlatform) {
			id = pNodeId;
			nodeRole = pNodeRole;
			belongsToStandalonePlatform = pBelongsToStandalonePlatform;
		}

		public short Id { get { return id; } }

		public NodeRole NodeRole { get { return nodeRole; } }

		public bool BelongsToStandalonePlatform { get { return belongsToStandalonePlatform; } }
		public bool IsSIP { get { return nodeRole == NodeRole.SIP; } }
		public bool IsAdmin { get { return nodeRole == NodeRole.Admin; } }
		public bool IsNotAdmin { get { return !IsAdmin; } }
		public bool IsH323 { get { return nodeRole == NodeRole.H323; } }
	}
}
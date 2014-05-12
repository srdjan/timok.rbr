using System;
using ObjectComparer=Timok.Core.ObjectComparer;

namespace Timok.Rbr.DTO 
{
	[Serializable]
	public class EndpointDto {
		public const string EndpointId_PropName = "EndpointId";
		public const string IPAddress_PropName = "IPAddress";

		short endpointId;
		public short EndpointId { get { return endpointId; } set { endpointId = value; } }

		string ipAddress;
		public string IPAddress { get { return ipAddress; } set { ipAddress = value; } }

		public EndpointDto(short pEndpointId, string pIPAddress) {
			endpointId = pEndpointId;
			ipAddress = pIPAddress;
		}

		//NOTE: compare object's values (not refs)
		public override bool Equals(object obj) {
			if (obj == null || obj.GetType() != GetType()) {
				return false;
			}
			return ObjectComparer.AreEqual(this, obj);
		}

		public override int GetHashCode() {
			return string.Concat(endpointId.ToString(), ipAddress).GetHashCode();
		}
	}
}


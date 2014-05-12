using System;
using Timok.Core;

namespace Timok.Rbr.DTO 
{
	[Serializable]
	public class CabinaDto {
		public const string EndpointIP_PropName = "EndpointIP";
		string endpointIP;
		public string EndpointIP { get { return endpointIP; } set { endpointIP = value; } }

		short endpointId;
		public short EndpointId { get { return endpointId; } set { endpointId = value; } }

		public const string Cabina_PropName = "Cabina";
		string cabina;
		public string Cabina { get { return cabina; } set { cabina = value; } }
		
		short customerAcctId;
		public short CustomerAcctId { get { return customerAcctId; } set { customerAcctId = value; } }

		public CabinaDto() {}

		public CabinaDto(short pEndpointId, string pIP, string pPrefix, short pCustomerId) {
			endpointIP = pIP;
			endpointId = pEndpointId;
			cabina = pPrefix;
			customerAcctId = pCustomerId;
		}

		//NOTE: compare object's values (not refs)
		public override bool Equals(object obj) {
			if (obj == null || obj.GetType() != GetType()) {
				return false;
			}
			return ObjectComparer.AreEqual(this, obj);
		}

		public override int GetHashCode() {
			return string.Concat(string.Concat(string.Concat(EndpointId.ToString("d8-"), Cabina), EndpointIP), CustomerAcctId.ToString()).GetHashCode();
		}
	}
}
using System;

namespace Timok.Rbr.DOM 
{
	[Serializable]
	public class EndpointASR {
		public const string EndpointId_PropName = "EndpointId";
		public const string Alias_PropName = "Alias";
		public const string Calls_PropName = "Calls";
		public const string ConnectedCalls_PropName = "ConnectedCalls";
		public const string Asr_PropName = "Asr";

		short endpointId;
		public short EndpointId { get { return endpointId; } set { endpointId = value; } }

		string alias = " UNKNOWN";
		public string Alias { get { return alias; } set { alias = value; } }

		int calls;
		public int Calls { get { return calls; } set { calls = value; } }

		int connectedCalls;
		public int ConnectedCalls { get { return connectedCalls; } set { connectedCalls = value; } }

		int asr;
		public int Asr { get { return asr; } set { asr = value; } }
	}
}

namespace Timok.Rbr.Core {
	public class LegIn {
		public string ANI;
		public string IP;
		public string IPAndPort;
		public string Protocol;
		public short CustomerAcctId;
		public int CustomerRouteId;
		public int PromptTimeLimit;
	}

	public class LegOut {
		public string DestNumber;
		public string DestIPAndPort;
		public short CarrierAcctId;
		public int CarrierBaseRouteId;
		public string CustomHeader;
		public int TimeLimit;
	}
}

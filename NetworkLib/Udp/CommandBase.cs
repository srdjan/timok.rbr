using System.Net;
using System.Net.Sockets;
using System.Text;

namespace Timok.NetworkLib.Udp {
	public abstract class CommandBase : ICommand {
		const string ONGETCALLSTATS = "ongetcallstats.1";
		const string REQUEST = "Request:";
		const string RESPONSE = "Response:";
		const string D6 = "D6";
		const string HEADER = "ret.{0}:{1}.{2}(";
		const string TAIL = ")";

		protected string apiName;
		public string APIName { set { apiName = value; } }

		protected string nameAndVersion;
		public string NameAndVersion { set { nameAndVersion = value; } }

		protected int sequence;
		protected string[] parameters;

		public string FullRequest {
			set {
				requestMessage = value;
				sequence = UdpMessageParser.GetSequence(requestMessage);
				parameters = UdpMessageParser.GetInParameters(requestMessage);
			}
		}

		protected bool hasResponse;
		StringBuilder strBldrResponse;
		protected int counter;

		string responseMessage { get { return strBldrResponse.ToString(); } }
		protected string requestMessage { get; private set; }

		protected CommandBase() {
		}

		protected void reset() {
			strBldrResponse = null;	
		}

		public void AddResponseField(string pField, bool pIsLast) {
			if (strBldrResponse == null || strBldrResponse.Length == 0) {
				hasResponse = true;
				strBldrResponse = new StringBuilder(string.Format(HEADER, sequence.ToString(D6), apiName, nameAndVersion));
			}

			if (string.IsNullOrEmpty(pField)) {
				pField = UdpMessageParser.UNKNOWN_VALUE;
			}
			strBldrResponse.Append(pField);

			if (pIsLast) {
				strBldrResponse.Append(TAIL);
			}
			else {
				strBldrResponse.Append(UdpMessageParser.OUT_DELIMETER);
			}
		}

		protected abstract void execute();

		public void Execute() {
			execute();
		}

		public void SendResponse(LocalHost pLocalHost, EndPoint pRemoteEP) {
			if ( ! hasResponse) {
				return;	
			}

			var _responseByteArray = Encoding.ASCII.GetBytes(responseMessage.ToCharArray());
			pLocalHost.SendTo(_responseByteArray, pRemoteEP);
		}
	}
}
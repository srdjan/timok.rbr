using System;
using System.Net;

namespace Timok.NetworkLib.Udp {
	class CmdContext {
		const string openBrackets = "[";
		const string closeOpenBrackets = "][";
		const string closeBrackets = "]";

		public string DataReceived;
		public EndPoint RmtEndPoint;

		public CmdContext(string pDataReceived, EndPoint pRemoteEP) {
			if (pDataReceived == null) {
				throw new Exception("CmdContext.Ctor: DataReceived == null");
			}
			DataReceived = pDataReceived;

			if (pRemoteEP == null) {
				throw new Exception("CmdContext.Ctor: pRemoteEP == null");
			}
			RmtEndPoint = pRemoteEP;
		}

		public override string ToString() {
			return openBrackets + DataReceived + closeOpenBrackets + RmtEndPoint + closeBrackets; 
		}
	}
}
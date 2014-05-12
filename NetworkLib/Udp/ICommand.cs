using System.Net;

namespace Timok.NetworkLib.Udp {
	public interface ICommand {
		string APIName { set; }
		string NameAndVersion { set; }
		string FullRequest { set; }
		void Execute();
		void AddResponseField(string pField, bool pIsLast);
		void SendResponse(LocalHost pLocalHost, EndPoint pRmtEndPoint);
	}
}
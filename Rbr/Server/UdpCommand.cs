using Timok.NetworkLib.Udp;
using Timok.Rbr.Core;

namespace Timok.Rbr.Server {
	public abstract class UdpCommand : CommandBase {
		protected IRbrDispatcher Dispatcher;
		protected UdpCommand() : base() {}
	}
}
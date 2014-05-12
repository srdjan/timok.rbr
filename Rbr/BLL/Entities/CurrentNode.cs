using Timok.NetworkLib;
using Timok.Rbr.Core.Config;

namespace Timok.Rbr.BLL.Entities {
	public class CurrentNode : Node {
		public CurrentNode() : base(IPUtil.ToInt32(Configuration.Instance.Main.HostIP)) {}
	}
}
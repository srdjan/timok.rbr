using Timok.Rbr.Core.Config;

namespace Timok.Rbr.BLL.Controllers {
	public class ControllerBase {
		protected IConfiguration configuration;

		protected ControllerBase(IConfiguration pConfiguration) {
			configuration = pConfiguration;
		}
	}
}
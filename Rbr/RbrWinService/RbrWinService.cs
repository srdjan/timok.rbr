using System.IO;
using System.ServiceProcess;
using Timok.Rbr.Server;

namespace Timok.Rbr.WinService {
	public class RbrWinService : ServiceBase {
		readonly string rbrProcessFilePath;
		RbrServer rbrServer;

		public RbrWinService(string pProcessName, string pProcessPath) {
			ServiceName = pProcessName;
			rbrProcessFilePath = Path.Combine(pProcessPath, pProcessName + ".exe");
		}
			
		public void Start(string[] pArgs) {
			OnStart(pArgs);
		}

		public void End() {
			OnStop();
		}

		//--------------------------------- Protected -------------------------------------------------
		protected override void OnStart(string[] pArgs) {
			rbrServer = new RbrServer(rbrProcessFilePath);
			rbrServer.Start();
		}

		protected override void OnStop() {
			if (rbrServer != null) {
				rbrServer.Stop();
			}
		}
	}
}
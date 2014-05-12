using System;
using System.Net;
using Timok.Core.BackgroundProcessing;
using Timok.Logger;

namespace Timok.Rbr.Core {
	public class GkTelnetClientTask : ITask {
		readonly string startCommand;
		GkTelnetClient gkTelnetClient;

		BackgroundWorker host;

		public GkTelnetClientTask(string pStartCommand, IPAddress pIPAddress, int pPort, TimeSpan pConnectionTimeout, LogDelegate pLog) {
			startCommand = pStartCommand;
			gkTelnetClient = new GkTelnetClient(pIPAddress, pPort, pConnectionTimeout, pLog);
			gkTelnetClient.Status += gkTelnetClient_Status;
			gkTelnetClient.Connected += gkTelnetClient_Connected;
			gkTelnetClient.DataReceived += gkTelnetClient_DataReceived;
			gkTelnetClient.Disconnected += gkTelnetClient_Disconnected;
		}

		public BackgroundWorker Host {
			set { host = value; }
		}

		public void Run(object pSender, WorkerEventArgs pArgs) {
			if (gkTelnetClient != null) {
				gkTelnetClient.OpenSession(startCommand);
				host.WorkCompleted += host_WorkCompleted;
			}
		}

		public void CancelAsync() {
			host.CancelAsync();
		}

		public bool SendCommand(string pCommand) {
			if (gkTelnetClient != null) {
				return gkTelnetClient.SendCommand(pCommand);
			}
			return false;
		}

		void host_WorkCompleted(object pSender, WorkCompletedEventArgs pArgs) {
			if (gkTelnetClient != null) {
				gkTelnetClient.CloseSession();
				gkTelnetClient = null;
			}
		}

		void gkTelnetClient_Status(string pMsg) {
			host.ReportStatus(pMsg);
		}

		void gkTelnetClient_Connected() {
			host.ReportStatus("Session opened...");
		}

		void gkTelnetClient_DataReceived(string pData) {
			host.ReportStatus(pData);
		}

		void gkTelnetClient_Disconnected() {
			host.ReportStatus("Session closed.");
		}
	}
}
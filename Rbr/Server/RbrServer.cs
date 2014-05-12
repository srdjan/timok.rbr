using System;
using System.IO;
using System.Reflection;
using Timok.Core;
using Timok.Logger;
using Timok.NetworkLib.Udp;
using Timok.Rbr.BLL.DOM;
using Timok.Rbr.BLL.Entities;
using Timok.Rbr.Core;
using Timok.Rbr.Service;
using Timok.Rbr.Core.Config;

namespace Timok.Rbr.Server {
	public class RbrServer {
		const string RBR_SERVER_CTOR_LABEL = "RbrServer.Ctor:";
		const string ON_START_LABEL = "RbrServer.Start:";
		const string ON_STOP_LABEL = "RbrServer.Stop:";

		readonly CurrentNode currentNode;
		readonly UdpServer udpServer;
		readonly Houskeeper houskeeper;
		readonly CallStatistics callStatistics;
		readonly string rbrProcessFilePath;

		public RbrServer(string pFilePath) {
			rbrProcessFilePath = pFilePath;
			currentNode = new CurrentNode();

			TimokLogger.Instance.LogRbr(LogSeverity.Status, "===============================================================================");
			TimokLogger.Instance.LogRbr(LogSeverity.Status, RBR_SERVER_CTOR_LABEL, string.Format("Version: {0}", Assembly.GetExecutingAssembly().GetName().Version));
			TimokLogger.Instance.LogRbr(LogSeverity.Status, RBR_SERVER_CTOR_LABEL, string.Format("LogSeverity Level: {0}", TimokLogger.Instance.LogSeverity));
			
			//-- add and set email logger
			var _folder = Configuration.Instance.Folders.EmailFolder;
			var _server = string.Format("{0}@{1}", Configuration.Instance.Main.HostName, Configuration.Instance.Main.HostIP);
			var _s = Configuration.Instance.Email.SupportEmailServer;
			var _from = Configuration.Instance.Email.SupportFrom;
			var _password = Configuration.Instance.Email.SupportEmailPassword;
			var _to = Configuration.Instance.Email.SupportTo;
			//TODO: srdjan
			//TimokLogger.Instance.AddEmailLogger(_folder, _server, _s, _from, _password, _to, Email.SetForSending);

			try {
				houskeeper = new Houskeeper();
				callStatistics = new CallStatistics();

				var _udpCommandFactory = new UdpCommandFactory(callStatistics);
				udpServer = new UdpServer(Configuration.Instance.Main.UdpServerIp, Configuration.Instance.Main.UdpServerRbrPort, _udpCommandFactory, TimokLogger.Instance.LogRbr);
				
				TimokLogger.Instance.LogRbr(LogSeverity.Status, "===============================================================================");
			}
			catch (Exception _ex) {
				TimokLogger.Instance.LogRbr(LogSeverity.Critical, RBR_SERVER_CTOR_LABEL, string.Format("Exception:\r\n{0}", _ex));
				throw;
			}
		}

		public void Start() {
			try {
				if (!(new Initializer()).WaitUntilSQLStarts()) {
					throw new Exception("SQL Engine didn't start on time!");
				}

				FirewallManager.StartFirewall(rbrProcessFilePath, "RBR");

				if (!houskeeper.Start(instanceLicenseExpired)) {
					throw new Exception("Houskeeper didn't start!");
				}

				udpServer.Start();

				if (Configuration.Instance.Main.NodeRole == NodeRole.SIP) {
					ChannelListenerFactory.Create(rbrProcessFilePath, new SessionDispatcher(callStatistics));
					TimokLogger.Instance.LogRbr(LogSeverity.Status, ON_START_LABEL, "RbrServer started in Softswitch mode");
				}
				else {
					TimokLogger.Instance.LogRbr(LogSeverity.Status, ON_START_LABEL, "RbrServer Started in Admin mode");
				}
			}
			catch (Exception _ex) {
				TimokLogger.Instance.LogRbr(LogSeverity.Critical, ON_START_LABEL, string.Format("Exception:\r\n{0}", _ex));
				throw;
			}
		}

		void instanceLicenseExpired() {
			TimokLogger.Instance.LogRbr(LogSeverity.Status, "RbrServer.instanceLicenseExpired:", "License Expired");
			Stop();
		}

		public void Stop() {
			if (File.Exists(rbrProcessFilePath)) {
				FirewallManager.StopFirewall(rbrProcessFilePath);
				TimokLogger.Instance.LogRbr(LogSeverity.Debug, ON_STOP_LABEL, string.Format("FirewallDisabled for app={0}", rbrProcessFilePath));
			}

			if (currentNode.IsSIP) {
				ChannelListenerFactory.Destroy();
				TimokLogger.Instance.LogRbr(LogSeverity.Status, ON_STOP_LABEL, "IVR Stopped");
			}

			udpServer.Stop();
			houskeeper.Stop();
			TimokLogger.Instance.LogRbr(LogSeverity.Status, ON_STOP_LABEL, "RbrServer Stoped");
		}
	}
}
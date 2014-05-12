using System;
using System.IO;
using System.ServiceProcess;
using Timok.Core;
using Timok.Logger;
using Timok.Rbr.BLL.Entities;
using Timok.Rbr.Core.Config;

namespace Timok.Rbr.Replication {
	public class ReplicationService : ServiceBase {
		readonly string processFilePath;
		readonly ReplicationEngine engine;

		public ReplicationService(string pProcessName, string pProcessPath) {
			ServiceName = pProcessName;
			processFilePath = pProcessPath;
			engine = new ReplicationEngine(/*CurrentNode.Instance.Id*/);
			TimokLogger.Instance.LogRbr(LogSeverity.Status, "R_ReplicationService.Ctor", string.Format("Process Name={0}, Path={1}", ServiceName, processFilePath));
		}

		public void Start(string[] args) { OnStart(args); }

		public new void Stop() { OnStop(); }

		//--------------------------------- Protected -------------------------------------------------
		protected override void OnStart(string[] args) {
			FirewallManager.StartFirewall(processFilePath, "Replication");

			//engine = new ReplicationEngine(CurrentNode.Instance.Id);
			if (! startReplication()) {
				TimokLogger.Instance.LogRbr(LogSeverity.Critical, "R_ReplicationService.OnStart", "Replication Service start failed.");
				throw new Exception(string.Format("Replication start failed: {0}", ServiceName));
			}
		}

		protected override void OnStop() {
			closeFirewall();
			engine.StopAll();
		}

		//----------------------------------- Private -------------------------------------------------
		bool startReplication() {
			try {
				//-- add email consumer
				engine.AddConsumer(Configuration.Instance.Email.Email, Configuration.Instance.Folders.EmailFolder, Email.CheckPending);
				engine.AddConsumer(AppConstants.NumberPortability, Configuration.Instance.Folders.FtpNumberPortabilityFolder, R_NumberPortability.Consume);

				var _currentNode = new CurrentNode();
				if (_currentNode.IsAdmin) {
					engine.AddPublisher(AppConstants.Rbr, Configuration.Instance.Folders.RbrPublishingFolder, R_Rbr.Publish);
					engine.AddConsumer(AppConstants.Cdr, Configuration.Instance.Folders.FtpReplicationFolder, R_Cdr.Consume);
					engine.AddConsumer(AppConstants.Aggr, Configuration.Instance.Folders.FtpReplicationFolder, R_CdrAggr.Consume);
					return true;
				}

				if (_currentNode.IsSIP || _currentNode.IsH323) {
					engine.AddConsumer(AppConstants.Rbr, Configuration.Instance.Folders.FtpReplicationFolder, R_Rbr.Consume);
					engine.AddPublisher(AppConstants.Cdr, Configuration.Instance.Folders.CdrPublishingFolder, R_Cdr.Publish);
					engine.AddPublisher(AppConstants.Aggr, Configuration.Instance.Folders.CdrAggrPublishingFolder, R_CdrAggr.Publish);
					return true;
				}
				TimokLogger.Instance.LogRbr(LogSeverity.Critical, "startReplication", "NOT Supported Node Type: " + _currentNode.NodeRole);
			}
			catch (Exception _ex) {
				TimokLogger.Instance.LogRbr(LogSeverity.Critical, "startReplication", "Exception: " + _ex);
			}
			return false;
		}

		void closeFirewall() {
			if (File.Exists(processFilePath)) {
				FirewallManager.StopFirewall(processFilePath);
				TimokLogger.Instance.LogRbr(LogSeverity.Debug, "R_ReplicationService.closeFirewall", string.Format("FirewallDisabled for app={0}", processFilePath));
			}
		}
	}
}
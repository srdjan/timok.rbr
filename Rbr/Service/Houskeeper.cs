using System;
using System.IO;
using System.Threading;
using Timok.Core;
using Timok.Logger;
using Timok.Rbr.BLL.DOM;
using Timok.Rbr.BLL.Entities;
using Timok.Rbr.Core;
using Timok.Rbr.Core.Config;
using Timok.Rbr.DAL.RbrDatabase;
using Timok.Rbr.Service.EmailReports;

namespace Timok.Rbr.Service {
	public delegate void LicenseExpiredEventHandler();

	public sealed class Houskeeper {
		const string ARCHIVE_SUB_FOLDER_NAME_FORMAT = "yyyy-MM-dd";
		readonly DateTime dtexp = DateTime.MaxValue; //new DateTime(2005,9,1); 
		Thread workerThread;
		bool workerThreadStarted;
		readonly ActivationReport activationReport;

		public Houskeeper() {
			activationReport = new ActivationReport();
			workerThreadStarted = false;
			Configuration.Instance.CheckWorkingFolders();
		}

		public event LicenseExpiredEventHandler LicenseExpired;

		public bool Start(LicenseExpiredEventHandler pLicenseExpiredEventHandler) {
			LicenseExpired += pLicenseExpiredEventHandler;

			if (! startExporters()) {
				return false;
			}

			try {
				if (! workerThreadStarted) {
					workerThread = new Thread(process) {IsBackground = true, Priority = ThreadPriority.Lowest};
					workerThread.Start();
					workerThreadStarted = true;
				}
			}
			catch (Exception _ex) {
				TimokLogger.Instance.LogRbr(LogSeverity.Critical, "Houskeeper.Start:", string.Format("Houskeeper Service Start, Exception:\r\n{0}", _ex));
				return false;
			}
			TimokLogger.Instance.LogRbr(LogSeverity.Status, "Houskeeper.Start:", "Houskeeper Started");
			return true;
		}

		public void Stop() {
			workerThreadStarted = false;
		}

		//---------------------------- Private Methods ------------------------------------------
		void process() {
			timeSynchronization();
			var _savedDt = DateTime.Now;
			var _dateReportsLastRan = DateTime.Now;

			while (workerThreadStarted) {
				Thread.Sleep(60000);

				try {
					timeSynchronization();

					resetDailyCallStats();

					checkExpiration();

					checkBalanceWarnings();

					purgeCdrAggregates();

					createNewMonthlyCDRdb();

					if (_dateReportsLastRan.Day != DateTime.Now.Day && DateTime.Now.Hour == Configuration.Instance.Main.MaintananceHour) {
						_dateReportsLastRan = DateTime.Now;
						dailyReports();
					}

					if (_savedDt.Day != DateTime.Now.Day && DateTime.Now.Hour == Configuration.Instance.Main.MaintananceHour) {
						_savedDt = DateTime.Now;
						archive(_savedDt);
						purgeArchive();
					}

					checkDiskSpace();
				}
				catch (Exception _ex) {
					TimokLogger.Instance.LogRbr(LogSeverity.Critical, "Houskeeper.process", string.Format("Unexpected Exception:\r\n{0}", _ex));
				}
			}
			TimokLogger.Instance.LogRbr(LogSeverity.Status, "Houskeeper.process", "Houskeeper Exited");
		}

		void purgeArchive() {
			try {
				var _folders = Directory.GetDirectories(Configuration.Instance.Folders.ArchiveFolder, "*.*", SearchOption.AllDirectories);
				foreach (var _folder in _folders) {
					var _files = Directory.GetFiles(_folder, "*.*");
					foreach (var _file in _files) {
						if (File.GetCreationTime(_file).Date.CompareTo(DateTime.Now.Date.AddDays(- Configuration.Instance.Main.ArchiveDaysToKeep)) < 0) {
							File.Delete(_file);
							TimokLogger.Instance.LogRbr(LogSeverity.Status, "Housekeeper.purgeArchive", string.Format("File={0} deleted", _file));
						}
					}
				}
			}
			catch (Exception _ex) {
				TimokLogger.Instance.LogRbr(LogSeverity.Critical, "Housekeeper.purgeArchive", string.Format("Exception:\r\n{0}", _ex));
			}
		}

		void archive(DateTime pSavedDt) {
			try {
				var _archiveSubFolder = Path.Combine(Configuration.Instance.Folders.ArchiveFolder, DateTime.Now.ToString(ARCHIVE_SUB_FOLDER_NAME_FORMAT));
				if (! Directory.Exists(_archiveSubFolder)) {
					Directory.CreateDirectory(_archiveSubFolder);
				}

				var _prevArchiveSubFolder = Path.Combine(Configuration.Instance.Folders.ArchiveFolder, pSavedDt.ToString(ARCHIVE_SUB_FOLDER_NAME_FORMAT));

				//-- Current/Audit
				moveFiles(Configuration.Instance.Folders.AuditFolder, Path.Combine(_prevArchiveSubFolder, AppConstants.AuditFolderName), AppConstants.PublishedFilter);

				//-- Current/Email
				moveFiles(Configuration.Instance.Folders.EmailFolder, Path.Combine(_prevArchiveSubFolder, AppConstants.EmailFolderName), AppConstants.ConsumedFilter);

				//Current/Export/*
				moveFolders(Configuration.Instance.Folders.ExportFolder, _prevArchiveSubFolder, "Export", "*.*");

				//Current/Publishing/Cdr/*
				moveFolders(Configuration.Instance.Folders.CdrPublishingFolder, _prevArchiveSubFolder, "Cdr", AppConstants.PublishedFilter);

				//Current/Publishing/CdrAggr/*
				moveFolders(Configuration.Instance.Folders.CdrAggrPublishingFolder, _prevArchiveSubFolder, "CdrAggr", AppConstants.PublishedFilter);

				//Current/Publishing/Rbr/*
				moveFolders(Configuration.Instance.Folders.RbrPublishingFolder, _prevArchiveSubFolder, "Rbr", AppConstants.PublishedFilter);

				//Ftp/CdrExport
				moveFiles(Configuration.Instance.Folders.FtpCdrExportFolder, Path.Combine(_prevArchiveSubFolder, AppConstants.CdrExportFolderName), AppConstants.CdrFileFilter);

				//Ftp/Replication
				moveFiles(Configuration.Instance.Folders.FtpReplicationFolder, Path.Combine(_prevArchiveSubFolder, Configuration.Instance.Folders.RbrConsumingFolderName), AppConstants.ConsumedFilter);

				//Configuration.Instance.Main.RbrRoot/Log
				moveFolders(Configuration.Instance.Folders.LogFolder, _prevArchiveSubFolder, "Log", AppConstants.LogFileFilter);
			}
			catch (Exception _ex) {
				TimokLogger.Instance.LogRbr(LogSeverity.Critical, "Housekeeper.archive", string.Format("Exception:\r\n{0}", _ex));
			}
		}

		void moveFolders(string pRootFolder, string pArchiveFolder, string pFolderName, string pFilter) {
			var _archiveSubFolder = Path.Combine(pArchiveFolder, pFolderName);

			//-- move files from root folder
			moveFiles(pRootFolder, _archiveSubFolder, pFilter);

			//-- move files from subfolders
			var _folders = Directory.GetDirectories(pRootFolder);
			foreach (var _folder in _folders) {
				var _subFolderName = _folder.Substring(_folder.LastIndexOf('\\') + 1);
				moveFiles(_folder, Path.Combine(_archiveSubFolder, _subFolderName), pFilter);
			}
		}

		void moveFiles(string pFromFolder, string pToFolder, string pFilter) {
			if (! Directory.Exists(pFromFolder)) {
				TimokLogger.Instance.LogRbr(LogSeverity.Critical, "Housekeeper.moveFiles", string.Format("FromFolder doesn't exist={0}", pFromFolder));
				return;
			}

			if (! Directory.Exists(pToFolder)) {
				Directory.CreateDirectory(pToFolder);
			}

			var _files = Directory.GetFiles(pFromFolder, pFilter);
			foreach (var _file in _files) {
				var _targetFile = string.Empty;
				try {
					_targetFile = Path.Combine(pToFolder, _file.Substring(_file.LastIndexOf('\\') + 1));

					if (File.GetCreationTime(_file).Date != DateTime.Now.Date) {
						TimokLogger.Instance.LogRbr(LogSeverity.Status, "Housekeeper.moveFiles", string.Format("Try moving File={0} to={1}", _file, _targetFile));
						File.Copy(_file, _targetFile);
						File.Delete(_file);
						TimokLogger.Instance.LogRbr(LogSeverity.Status, "Housekeeper.moveFiles", string.Format("File={0} moved to={1}", _file, _targetFile));
					}
				}
				catch (Exception _ex) {
					TimokLogger.Instance.LogRbr(LogSeverity.Critical, "Housekeeper.moveFiles", string.Format("File={0}, Exception:\r\n{1}", _targetFile, _ex));
				}
			}
		}

		void dailyReports() {
			try {
				TimokLogger.Instance.LogRbr(LogSeverity.Status, "Housekeeper.dailyReports", string.Format("Reports running on: {0}", DateTime.Now.ToShortTimeString()));
				(new ReportEngine()).Run(DateTime.Today.AddDays(-1), true);

				TimokLogger.Instance.LogRbr(LogSeverity.Status, "Housekeeper.dailyReports", string.Format("Inventory Scaner running on: {0}", DateTime.Now.ToShortTimeString()));
				InventoryUsage.Scan(DateTime.Today.AddDays(-1));

				TimokLogger.Instance.LogRbr(LogSeverity.Status, "Housekeeper.dailyReports", string.Format("PinActivationReport running on: {0}", DateTime.Now.ToShortTimeString()));
				activationReport.Run(DateTime.Today.AddDays(-1));
			}
			catch (Exception _ex) {
				TimokLogger.Instance.LogRbr(LogSeverity.Critical, "Housekeeper.dailyReports", string.Format("DailyReports, Exception: {0}", _ex));
			}
		}

		void checkDiskSpace() {
			if (DateTime.Now.Minute == 0) {
				if (DiskSpace.GetAvailable(Configuration.Instance.Main.SystemDrive) <= 10) {
					var _subject = string.Format("From> {0}@{1} -WARNING- System Disk Space almost used", Configuration.Instance.Main.HostName, Configuration.Instance.Main.HostIP);
					TimokLogger.Instance.LogRbr(LogSeverity.Critical, "Houskeeper.checkDiskSpace", _subject);
					Email.SetForSending(Path.Combine(Configuration.Instance.Folders.EmailFolder, Guid.NewGuid().ToString("N")),
										 Configuration.Instance.Email.ClientFrom,
					           Configuration.Instance.Email.ClientTo,
					           Configuration.Instance.Email.SupportTo,
					           null,
					           Configuration.Instance.Email.ClientEmailServer,
					           Configuration.Instance.Email.ClientEmailPassword,
					           _subject,
					           string.Empty);
				}

				if (Configuration.Instance.Main.SystemDrive != Configuration.Instance.Main.ArchiveDrive) {
					if (DiskSpace.GetAvailable(Configuration.Instance.Main.ArchiveDrive) <= 10) {
						var _subject = string.Format("From> {0}@{1} -WARNING- Archive Disk Space almost used", Configuration.Instance.Main.HostName, Configuration.Instance.Main.HostIP);
						TimokLogger.Instance.LogRbr(LogSeverity.Critical, "Houskeeper.checkDiskSpace", _subject);
						Email.SetForSending(Path.Combine(Configuration.Instance.Folders.EmailFolder, Guid.NewGuid().ToString("N")), 
											 Configuration.Instance.Email.ClientFrom,
						           Configuration.Instance.Email.ClientTo,
						           Configuration.Instance.Email.SupportTo,
						           null,
						           Configuration.Instance.Email.ClientEmailServer,
						           Configuration.Instance.Email.ClientEmailPassword,
						           _subject,
						           string.Empty);
					}
				}
			}
		}

		void createNewMonthlyCDRdb() {
			if (DateTime.Now.Day > 25 && DateTime.Now.Hour == 6 && DateTime.Now.Minute == 5) {
				try {
					Cdr.CreateDb();
				}
				catch (Exception _ex) {
					TimokLogger.Instance.LogRbr(LogSeverity.Critical, "Housekeeper.createNewMonthlyCDRdb", "Create CdrDb, Exception: " + _ex);
				}
			}
		}

		void checkBalanceWarnings() {
			if ((new CurrentNode()).IsNotAdmin) {

				try {
					if (DateTime.Now.Minute != 30 && (DateTime.Now.Hour != 6 || DateTime.Now.Hour != 16)) {
						return;
					}
					TimokLogger.Instance.LogRbr(LogSeverity.Status, "Housekeeper.CheckBalanceWarnings:", "Check balance warnings.");

					//-- check each customerAcct
					var _customerAccts = CustomerAcct.GetAllWithBalanceWarning();
					if (_customerAccts.Length == 0) {
						TimokLogger.Instance.LogRbr(LogSeverity.Status, "Housekeeper.CheckBalanceWarnings:", "No balance warnings.");
						return;
					}

					foreach (var _customerAcct in _customerAccts) {
						if (DateTime.Now.Hour == 16) {
							if (_customerAcct.IsBalanceLimitReached) {
								if (_customerAcct.ShouldSendLimitMessage) {
									sendBalanceReport(_customerAcct, "Balance Limit REACHED, ");
								}
							}
							else if (_customerAcct.IsBalanceWarningLimitReached) {
								if (_customerAcct.ShouldSendWarningMessage) {
									sendBalanceReport(_customerAcct, "Balance Limit WARNING, ");
								}
							}
						}

						if (DateTime.Now.Hour == 6) {
							sendBalanceReport(_customerAcct, "Daily Balance Report, ");
						}
					}
				}
				catch (Exception _ex) {
					TimokLogger.Instance.LogRbr(LogSeverity.Critical, "Housekeeper.CheckBalanceWarnings:", string.Format("Exception\r\n{0}", _ex));
				}
			}
		}

		void timeSynchronization() {
			try {
				if ( ! Utils.SyncSystemTime(Configuration.Instance.Main.TimeSyncFrequency, AppConstants.TimeServers)) {
					TimokLogger.Instance.LogRbr(LogSeverity.Error, "Housekeeper.timeSynchronization", "SyncSystemTime, Failed");
				}
			}
			catch (Exception _ex) {
				TimokLogger.Instance.LogRbr(LogSeverity.Critical, "Housekeeper.timeSynchronization", string.Format("SyncSystemTime, Exception:\r\n{0}", _ex));
			}
		}

		void checkExpiration() {
			try {
				if (DateTime.Compare(DateTime.Now, dtexp) > 0) {
					TimokLogger.Instance.LogRbr(LogSeverity.Error, "Housekeeper.checkExpiration", "CheckExpiration: Expired !");
					if (LicenseExpired != null) {
						LicenseExpired();
					}
				}
			}
			catch (Exception _ex) {
				TimokLogger.Instance.LogRbr(LogSeverity.Critical, "Housekeeper.checkExpiration", string.Format("CheckExpiration, Exception:\r\n{0}", _ex));
			}
		}

		void purgeCdrAggregates() {
			if ((new CurrentNode()).IsAdmin) {
				if (DateTime.Now.Hour == 2 && DateTime.Now.Minute == 0) {
					try {
						using (var _db = new Rbr_Db()) {
							if (Configuration.Instance.Main.CdrAggrDaysToKeep >= 0) {
								var _numberOfDeletedRecords = _db.CdrAggregateCollection.Purge(Configuration.Instance.Main.CdrAggrDaysToKeep);
								TimokLogger.Instance.LogRbr(LogSeverity.Status, "Housekeeper.Purge", string.Format("Number of purged CDrAggregate records: {0}", _numberOfDeletedRecords));
							}
							else {
								TimokLogger.Instance.LogRbr(LogSeverity.Critical, "Housekeeper.Purge", "Invalid CdrAggrDaysToKeep in Rbr.Config");
							}
						}
					}
					catch (Exception _ex) {
						TimokLogger.Instance.LogRbr(LogSeverity.Critical, "Housekeeper.purgeCdrAggregates", string.Format("ResetCallStats, Exception:\r\n{0}", _ex));
					}
				}
			}
		}

		void resetDailyCallStats() {
			if (DateTime.Now.Hour == 0 && DateTime.Now.Minute == 0) {
				try {
					(new CallStatistics()).ResetDailyCallStats();
				}
				catch (Exception _ex) {
					TimokLogger.Instance.LogRbr(LogSeverity.Critical, "Housekeeper.resetDailyCallStats", "ResetCallStats, Exception: " + _ex);
				}
			}
		}

		bool startExporters() {
			try {
				var _currentNode = new CurrentNode();
				if (_currentNode.BelongsToStandalonePlatform) {
					return true;
				}

				if (_currentNode.IsAdmin) {
					//this Exporter simply locally copies gui audit files to target nodes folders
					return true;
				}

				if (_currentNode.IsSIP) {
					//cdrAggrExporter.Init();
					return true;
				}
				TimokLogger.Instance.LogRbr(LogSeverity.Critical, "Housekeeper.startExporters", "Unknown Node Type");
			}
			catch (Exception _ex) {
				TimokLogger.Instance.LogRbr(LogSeverity.Critical, "Housekeeper.startExporters", string.Format("Exception:\r\n{0}", _ex));
			}
			return false;
		}

		//---------------------------------- Private ----------------------------------------------------
		static void sendBalanceReport(CustomerAcct pCustomerAcct, string pSubject) {
			string _partnerName;
			string _partnerAddresses;
			pCustomerAcct.GetPartnerInfo(out _partnerName, out _partnerAddresses);

			pSubject += "[Partner]> " + _partnerName + " [Acct]> " + pCustomerAcct.Name;
			var _body = "Current Balance> " + pCustomerAcct.Balance.ToString("0.00");

			Email.SetForSending(Path.Combine(Configuration.Instance.Folders.EmailFolder,
															Guid.NewGuid().ToString("N")),
															Configuration.Instance.Email.ClientFrom,
															_partnerAddresses,
															Configuration.Instance.Email.ClientTo,
															Configuration.Instance.Email.SupportTo,
															Configuration.Instance.Email.ClientEmailServer,
															Configuration.Instance.Email.ClientEmailPassword,
															pSubject,
															_body);
		}
	}
}

using System.IO;

namespace Timok.Rbr.Core.Config {
	public interface IFoldersConfiguration {
		string RbrConsumingFolderName { get; }
		string ArchiveFolder { get; }
		string CurrentFolder { get; }
		string SqlDbFolder { get; }
		string PayphoneNumbersFilePath { get; }
		string IVRFolder { get; }
		string IVRProcessFilePath { get; }
		string AuditFolder { get; }
		string CdrExportFolder { get; }
		string ExportFolder { get; }
		string FtpFolder { get; }
		string OrigRoutingFolder { get; }
		string FtpCdrExportFolder { get; }
		string FtpReplicationFolder { get; }
		string FtpNumberPortabilityFolder { get; }
		string EmailFolder { get; }
		string CdrAggrPublishingFolder { get; }
		string RbrPublishingFolder { get; }
		string RbrSequenceFilePath { get; }
		string CdrPublishingFolder { get; }
		string InventoryFolder { get; }
		string ImportFilesPath { get; }
		string NumberPortabilityDbPath { get; }
		string LogFolder { get; }
		string GkIniFile { get; }
		string GetInventoryServiceFolder(short pServiceId);
		string GetInventoryLotFolder(short pServiceId, decimal pDenomination);
		string GetInventoryBatchFileName(short pServiceId, decimal pDenomination, int pBatchId);
		string GetInventoryBatchFilePath(short pServiceId, decimal pDenomination, int pBatchId);
	}

	internal class FoldersConfiguration : IFoldersConfiguration {
		readonly IMainConfiguration main;
		readonly IDbConfiguration db;

		public string GkIniFile {
			get {
				if (string.IsNullOrEmpty(H323Folder)) {
					return null;
				}
				return Path.Combine(H323Folder, "Gatekeeper.ini");
			}
		}

		public string H323Folder {
			get {
				if (string.IsNullOrEmpty(AppConstants.RbrRoot)) {
					return null;
				}
				return Path.Combine(AppConstants.RbrRoot, "H323");
			}
		}
		
		public FoldersConfiguration(IMainConfiguration pMain, IDbConfiguration pDb) {
			main = pMain;
			db = pDb;
			LogFolder = Path.Combine(main.RbrRoot, "Log");
		}

		//-- top level folder: Archive
		string archiveFolder;
		public string ArchiveFolder {
			get {
				if (archiveFolder == null) {
					archiveFolder = Path.Combine(main.ArchiveDrive, @"Timok\Rbr\Archive");
					if (!Directory.Exists(archiveFolder)) {
						Directory.CreateDirectory(archiveFolder);
					}
				}
				return archiveFolder;
			}
		}

		//-- top level folder: Current
		string currentFolder;
		public string CurrentFolder {
			get {
				if (currentFolder == null) {
					currentFolder = Path.Combine(main.RbrRootParent, "Current");
					if (!Directory.Exists(currentFolder)) {
						Directory.CreateDirectory(currentFolder);
					}
				}
				return currentFolder;
			}
		}

		//-- top level folder: SqlDb
		string sqlDbFolder;
		public string SqlDbFolder {
			get {
				if (sqlDbFolder == null) {
					sqlDbFolder = Path.Combine(main.RbrRootParent, "SqlDb");
					if (!Directory.Exists(sqlDbFolder)) {
						Directory.CreateDirectory(sqlDbFolder);
					}
				}
				return sqlDbFolder;
			}
		}

		public string PayphoneNumbersFilePath { get { return Path.Combine(SqlDbFolder, "PayphoneNumbers.txt"); } }

		public string IVRFolder { get { return main.RbrRoot; } }
		public string IVRProcessFilePath {
			get {
				return string.IsNullOrEmpty(IVRFolder) ? null : Path.Combine(IVRFolder, "Timok.Rbr.IVR.exe");
			}
		}

		public string LogFolder { get; private set; }

		//-- gui Audit Publishing Folder
		string auditFolder;
		public string AuditFolder {
			get {
				if (auditFolder == null) {
					auditFolder = Path.Combine(CurrentFolder, "Audit");
					if (!Directory.Exists(auditFolder)) {
						Directory.CreateDirectory(auditFolder);
					}
				}
				return auditFolder;
			}
		}

		string cdrExportFolder;
		public string CdrExportFolder {
			get {
				if (cdrExportFolder == null) {
					cdrExportFolder = Path.Combine(Path.Combine(CurrentFolder, "Export"), "Cdr");
					if (!Directory.Exists(cdrExportFolder)) {
						Directory.CreateDirectory(cdrExportFolder);
					}
				}
				return cdrExportFolder;
			}
		}

		string exportFolder;
		public string ExportFolder {
			get {
				if (exportFolder == null) {
					exportFolder = Path.Combine(CurrentFolder, "Export");
					if (!Directory.Exists(exportFolder)) {
						Directory.CreateDirectory(exportFolder);
					}
				}
				return exportFolder;
			}
		}

		//-- ftp folder
		string ftpFolder;
		public string FtpFolder {
			get {
				if (ftpFolder == null) {
					ftpFolder = Path.Combine(main.RbrRootParent, "Ftp");
					if (!Directory.Exists(ftpFolder)) {
						Directory.CreateDirectory(ftpFolder);
					}
				}
				return ftpFolder;
			}
		}		
		
		//-- orig routing folder
		string origRoutingFolder;
		public string OrigRoutingFolder {
			get {
				if (origRoutingFolder == null) {
					origRoutingFolder = Path.Combine(main.RbrRootParent, "OrigRouting");
					if (!Directory.Exists(origRoutingFolder)) {
						Directory.CreateDirectory(origRoutingFolder);
					}
				}
				return origRoutingFolder;
			}
		}

		string ftpCdrExportFolder;
		public string FtpCdrExportFolder {
			get {
				if (ftpCdrExportFolder == null) {
					ftpCdrExportFolder = Path.Combine(FtpFolder, "CdrExport");
					if (!Directory.Exists(ftpCdrExportFolder)) {
						Directory.CreateDirectory(ftpCdrExportFolder);
					}
				}
				return ftpCdrExportFolder;
			}
		}

		//-- Replication folder
		string replicationFolder;
		public string FtpReplicationFolder {
			get {
				if (replicationFolder == null) {
					replicationFolder = Path.Combine(FtpFolder, "Replication");
					if (!Directory.Exists(replicationFolder)) {
						Directory.CreateDirectory(replicationFolder);
					}
				}
				return replicationFolder;
			}
		}

		//-- Number Portability Folder
		string numberPortabilityFolder;
		public string FtpNumberPortabilityFolder {
			get {
				if (numberPortabilityFolder == null) {
					numberPortabilityFolder = Path.Combine(FtpFolder, "NumberPortability");
					if (!Directory.Exists(numberPortabilityFolder)) {
						Directory.CreateDirectory(numberPortabilityFolder);
					}
				}
				return numberPortabilityFolder;
			}
		}

		//-- Email folder
		string emailFolder;
		public string EmailFolder {
			get {
				if (emailFolder == null) {
					emailFolder = Path.Combine(CurrentFolder, "Email");
					if (!Directory.Exists(emailFolder)) {
						Directory.CreateDirectory(emailFolder);
					}
				}
				return emailFolder;
			}
		}

		string cdrAggrPublishingFolder;
		public string CdrAggrPublishingFolder {
			get {
				if (cdrAggrPublishingFolder == null) {
					cdrAggrPublishingFolder = Path.Combine(Path.Combine(CurrentFolder, "Publishing"), "CdrAggr");
					if (!Directory.Exists(cdrAggrPublishingFolder)) {
						Directory.CreateDirectory(cdrAggrPublishingFolder);
					}
				}
				return cdrAggrPublishingFolder;
			}
		}

		public string RbrConsumingFolderName { get { return "RbrConsuming"; } }

		string rbrPublishingFolder;
		public string RbrPublishingFolder {
			get {
				if (rbrPublishingFolder == null) {
					rbrPublishingFolder = Path.Combine(Path.Combine(CurrentFolder, "Publishing"), "Rbr");
					if (!Directory.Exists(rbrPublishingFolder)) {
						Directory.CreateDirectory(rbrPublishingFolder);
					}
				}
				return rbrPublishingFolder;
			}
		}

		//TODO: to be removed or should use diff file name for each app ???
		public string RbrSequenceFilePath {
			get {
				if (string.IsNullOrEmpty(RbrPublishingFolder)) {
					return null;
				}
				return Path.Combine(RbrPublishingFolder, "RbrSeqFile.txt");
			}
		}

		string cdrPublishingFolder;
		public string CdrPublishingFolder {
			get {
				if (cdrPublishingFolder == null) {
					cdrPublishingFolder = Path.Combine(Path.Combine(CurrentFolder, "Publishing"), "Cdr");
					if (!Directory.Exists(cdrPublishingFolder)) {
						Directory.CreateDirectory(cdrPublishingFolder);
					}
				}
				return cdrPublishingFolder;
			}
		}

		string inventoryFolder;
		public string InventoryFolder {
			get {
				if (inventoryFolder == null) {
					inventoryFolder = Path.Combine(main.RbrRootParent, "Inventory");
					if (!Directory.Exists(inventoryFolder)) {
						Directory.CreateDirectory(inventoryFolder);
					}
				}
				return inventoryFolder;
			}
		}

		//string importFilesPath; 
		public string ImportFilesPath { get { return Path.Combine(@"c:\Timok\Rbr\Sqldb\ExportedData", db.RbrDbVersion); } }
		public string NumberPortabilityDbPath { get { return @"c:\Timok\Rbr\Sqldb\NumberPortability.db"; } }
		//importFilesPath; } }

		public string GetInventoryServiceFolder(short pServiceId) {
			return Path.Combine(InventoryFolder, pServiceId.ToString("00000"));
		}

		public string GetInventoryLotFolder(short pServiceId, decimal pDenomination) {
			return Path.Combine(GetInventoryServiceFolder(pServiceId), pDenomination.ToString("000000.00"));
		}

		public string GetInventoryBatchFileName(short pServiceId, decimal pDenomination, int pBatchId) {
			return "Service_" + pServiceId.ToString("00000") + "_Batch_" + pBatchId.ToString("0000000000") + "_$" + pDenomination.ToString("000000.00") + ".txt";
		}

		public string GetInventoryBatchFilePath(short pServiceId, decimal pDenomination, int pBatchId) {
			return Path.Combine(GetInventoryLotFolder(pServiceId, pDenomination), GetInventoryBatchFileName(pServiceId, pDenomination, pBatchId));
		}
	}
}
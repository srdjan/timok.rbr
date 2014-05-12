using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using Timok.Logger;
using Timok.Rbr.BLL.Managers;
using Timok.Rbr.Core;
using Timok.Rbr.Core.Config;
using Timok.Rbr.DAL.RbrDatabase;
using Timok.Rbr.DTO;
using WinFormsEx;

namespace Timok.Rbr.BLL.Inventory {
	public class InventoryController {
		public const char FieldDelimiter = '\t';

		#region Getters

		public static BatchDto[] GetGeneratedBatchesByServiceId(short pServiceId) {
			var _list = new ArrayList();
			using (var _db = new Rbr_Db()) {
				InventoryLotRow[] _inventoryLotRows = _db.InventoryLotCollection.GetByService_id(pServiceId);
				foreach (var _inventoryLotRow in _inventoryLotRows) {
					GenerationRequestRow[] _generationRequestRows = _db.GenerationRequestCollection.GetByLot_id(_inventoryLotRow.Lot_id);
					foreach (var _generationRequestRow in _generationRequestRows) {
						BatchRow[] _batchRows = _db.BatchCollection.GetGeneratedByRequestId(_generationRequestRow.Request_id);
						_list.AddRange(MapToBatches(_batchRows, _inventoryLotRow, _generationRequestRow));
					}
				}
			}
			return (BatchDto[]) _list.ToArray(typeof (BatchDto));
		}

		public static BatchDto[] GetBatchesByInventoryStatusServiceIdCustomerAcctId(InventoryStatus pInventoryStatus, short pServiceId, short pCustomerAcctId) {
			var _list = new ArrayList();
			using (var _db = new Rbr_Db()) {
				var _inventoryLotRows = _db.InventoryLotCollection.GetByService_id(pServiceId);
				foreach (var _inventoryLotRow in _inventoryLotRows) {
					var _generationRequestRows = _db.GenerationRequestCollection.GetByLot_id(_inventoryLotRow.Lot_id);
					foreach (var _generationRequestRow in _generationRequestRows) {
						var _batchRows = _db.BatchCollection.GetByInventoryStatusCustomerAcctIdRequestId(pInventoryStatus, pCustomerAcctId, _generationRequestRow.Request_id);
						_list.AddRange(MapToBatches(_batchRows, _inventoryLotRow, _generationRequestRow));
					}
				}
			}
			return (BatchDto[]) _list.ToArray(typeof (BatchDto));
		}

		public static RetailAccountDto GetAcct(int pRetailAcctId) {
			using (var _db = new Rbr_Db()) {
				//-- get account
				var _retailAcct = RetailAccountManager.Instance.GetAcct(_db, pRetailAcctId);
				if (_retailAcct == null) {
					throw new RbrException(RbrResult.POSA_AcctNotFound, string.Empty);
				}
				return _retailAcct;
			}
		}

		#endregion Getters

		#region Actions

		public static bool AddLot(RetailAccountGenResponse pResponse, PersonDto pPerson) {
			bool _result = false;

			using (var _db = new Rbr_Db()) {
				_db.BeginTransaction();

				try {
					InventoryLotRow _lotRow = _db.InventoryLotCollection.GetByServiceIdDenomination(pResponse.Request.ServiceId, pResponse.Request.Denomination);
					if (_lotRow == null) {
						_lotRow = new InventoryLotRow();
						_lotRow.Service_id = pResponse.Request.ServiceId;
						_lotRow.Denomination = pResponse.Request.Denomination;
						_db.InventoryLotCollection.Insert(_lotRow);
					}

					var _requestRow = new GenerationRequestRow();
					_requestRow.Date_requested = DateTime.Now;
					_requestRow.Date_completed = DateTime.Now;
					_requestRow.Date_to_process = DateTime.Now;
					_requestRow.Number_of_batches = pResponse.Request.NumberOfBatches;
					_requestRow.Batch_size = pResponse.Request.BatchSize;
					_requestRow.Lot_id = _lotRow.Lot_id;
					_db.GenerationRequestCollection.Insert(_requestRow);

					foreach (var _batch in pResponse.BatchList) {
						var _batchRow = new BatchRow();
						_batchRow.Batch_id = _batch.BatchId;
						_batchRow.First_serial = _batch.FirstSerial;
						_batchRow.Last_serial = _batch.LastSerial;
						_batchRow.Request_id = _requestRow.Request_id;
						//_batchRow.Box_id = ???? xxx; NOT SET for now
						//_batchRow.Customer_acct_id = WILL BE SET ON LOAD/ACTIVATE;
						_batchRow.InventoryStatus = InventoryStatus.Generated;
						_db.BatchCollection.Insert(_batchRow);

						logInventoryHistory(_db, pPerson, DateTime.Now, _lotRow.Service_id, _lotRow.Denomination, _batchRow.Batch_id, _batchRow.NumberOfCards, InventoryCommand.Generate, 0, //CustomerAcctId - N/A FOR THIS COMMAND
						                    0, //ResellerPartnerId - N/A FOR THIS COMMAND
						                    0 //ResellerAgentId - N/A FOR THIS COMMAND
							);
					}
					_db.CommitTransaction();
					_result = true;
				}
				catch (Exception _ex) {
					_db.RollbackTransaction();
					TimokLogger.Instance.LogRbr(LogSeverity.Critical, "InventoryController.AddLot", string.Format("Exception: {0}", _ex));
				}
			}

			return _result;
		}

		public static void ExecuteCommand(object sender, DoWorkEventArgs pEvtArg) {
			using (var _db = new Rbr_Db()) {
				_db.BeginTransaction();

				var _backgroundWorker = (BackgroundWorker) sender;
				var _inventoryCommandRequest = (InventoryCommandRequest) pEvtArg.Argument;

				_backgroundWorker.ReportStatus("Executing Batch Command: [" + _inventoryCommandRequest.InventoryCommand + "]");

				switch (_inventoryCommandRequest.InventoryCommand) {
					case InventoryCommand.Load:
						executeLoadCommand(_db, _backgroundWorker, pEvtArg, _inventoryCommandRequest);
						break;
					case InventoryCommand.Activate:
					case InventoryCommand.Deactivate:
					case InventoryCommand.Archive:
						executeCommand(_db, _backgroundWorker, pEvtArg, _inventoryCommandRequest);
						break;
					case InventoryCommand.Export:
						executeExport(_db, _backgroundWorker, pEvtArg, _inventoryCommandRequest);
						break;
					default:
						throw new ArgumentException("Unexpected InventoryCommand [" + _inventoryCommandRequest.InventoryCommand + "]");
				}

				if (pEvtArg.Cancel) {
					pEvtArg.Result = new InventoryCommandResponse(_inventoryCommandRequest, false);
					return;
				}

				_db.CommitTransaction();
				pEvtArg.Result = new InventoryCommandResponse(_inventoryCommandRequest, true);
				_backgroundWorker.ReportStatus("Finished Batch Command: [" + _inventoryCommandRequest.InventoryCommand + "]");
			}
		}

		public static void Activate(int pRetailAcctId, decimal pAmount, List<long> pPhoneNumbers, out long pPIN, out long pSerialNumber) {
			pPIN = 0;
			pSerialNumber = 0;
			using (var _db = new Rbr_Db()) {
				_db.BeginTransaction();

				//-- get account
				var _retailAcct = RetailAccountManager.Instance.GetAcct(_db, pRetailAcctId);
				if (_retailAcct == null) {
					throw new RbrException(RbrResult.POSA_AcctNotFound, string.Empty);
				}

				if (_retailAcct.Status != Status.Active) {
					_retailAcct.Status = Status.Active;
					RetailAccountManager.Instance.UpdateAcct(_db, _retailAcct);
				}
				_db.CommitTransaction();
			}
		}

		public static void Recharge(int pRetailAcctId, decimal pAmount) {
			using (var _db = new Rbr_Db()) {
				_db.BeginTransaction();

				//-- get account
				var _retailAcct = RetailAccountManager.Instance.GetAcct(_db, pRetailAcctId);
				if (_retailAcct == null) {
					throw new RbrException(RbrResult.POSA_AcctNotFound, string.Empty);
				}

				if (_retailAcct.Status != Status.Active) {
					throw new RbrException(RbrResult.POSA_InvalidAcctStatus, string.Empty);
				}

				_retailAcct.Status = Status.Active;
				RetailAccountManager.Instance.UpdateAcct(_db, _retailAcct);
				_db.CommitTransaction();
			}
		}

		public static void Deactivate(int pRetailAcctId) {
			using (var _db = new Rbr_Db()) {
				_db.BeginTransaction();

				//-- get account
				var _retailAcct = RetailAccountManager.Instance.GetAcct(_db, pRetailAcctId);
				if (_retailAcct == null) {
					throw new RbrException(RbrResult.POSA_AcctNotFound, string.Empty);
				}

				if (_retailAcct.Status == Status.Active) {
					_retailAcct.Status = Status.Blocked;
					RetailAccountManager.Instance.UpdateAcct(_db, _retailAcct);
				}

				_db.CommitTransaction();
			}
		}

		public static RetailAccountRow CreateRetailAccount(InventoryCommandRequest pInventoryCommandRequest) {
			using (var _db = new Rbr_Db()) {
				return createRetailAccount(_db, pInventoryCommandRequest);
			}
		}

		#endregion Actions

		#region Mappings

		internal static BatchDto[] MapToBatches(BatchRow[] pBatchRows, InventoryLotRow pInventoryLotRow, GenerationRequestRow pGenerationRequestRow) {
			var _list = new ArrayList();
			foreach (var _batchRow in pBatchRows) {
				_list.Add(MapToBatch(_batchRow, pInventoryLotRow, pGenerationRequestRow));
			}
			return (BatchDto[]) _list.ToArray(typeof (BatchDto));
		}

		internal static BatchDto MapToBatch(BatchRow pBatchRow, InventoryLotRow pInventoryLotRow, GenerationRequestRow pGenerationRequestRow) {
			if (pBatchRow == null || pInventoryLotRow == null || pGenerationRequestRow == null) {
				return null;
			}
			var _batch = new BatchDto();
			_batch.BatchId = pBatchRow.Batch_id;
			_batch.InventoryStatus = pBatchRow.InventoryStatus;
			_batch.BoxId = pBatchRow.Box_id;
			_batch.CustomerAcctId = pBatchRow.Customer_acct_id;
			_batch.FirstSerial = pBatchRow.First_serial;
			_batch.LastSerial = pBatchRow.Last_serial;

			_batch.RequestId = pBatchRow.Request_id;
			_batch.Selected = false;

			_batch.LotId = pInventoryLotRow.Lot_id;
			_batch.ServiceId = pInventoryLotRow.Service_id;
			_batch.Denomination = pInventoryLotRow.Denomination;

			_batch.NumberOfBatches = pGenerationRequestRow.Number_of_batches;
			_batch.BatchSize = pGenerationRequestRow.Batch_size;
			_batch.DateRequested = pGenerationRequestRow.Date_requested;
			_batch.DateToProcess = pGenerationRequestRow.Date_to_process;
			_batch.DateCopleted = pGenerationRequestRow.Date_completed;

			return _batch;
		}

		internal static BatchRow MapToBatchRow(BatchDto pBatch) {
			if (pBatch == null) {
				return null;
			}
			var _batchRow = new BatchRow();
			_batchRow.Batch_id = pBatch.BatchId;
			_batchRow.InventoryStatus = pBatch.InventoryStatus;
			_batchRow.First_serial = pBatch.FirstSerial;
			_batchRow.Last_serial = pBatch.LastSerial;
			_batchRow.Request_id = pBatch.RequestId;

			if (pBatch.BoxId > 0) {
				_batchRow.Box_id = pBatch.BoxId;
			}

			if (pBatch.CustomerAcctId > 0) {
				_batchRow.Customer_acct_id = pBatch.CustomerAcctId;
			}

			return _batchRow;
		}

		#endregion Mappings

		#region Privates

		//--------------------------------- Privates -----------------------------------
		private static void executeLoadCommand(Rbr_Db pDb, BackgroundWorker pBackgroundWorker, DoWorkEventArgs pEvtArg, InventoryCommandRequest pInventoryCommandRequest) {
			pBackgroundWorker.ReportStatus(string.Format("Loading {0} Batches", pInventoryCommandRequest.Batches));

			foreach (var _batch in pInventoryCommandRequest.Batches) {
				string _batchFilePath = Configuration.Instance.Folders.GetInventoryBatchFilePath(_batch.ServiceId, _batch.Denomination, _batch.BatchId);
				pBackgroundWorker.ReportStatus("Loading Batch File: " + _batchFilePath);

				using (var _sr = new StreamReader(_batchFilePath)) {
					_batch.CustomerAcctId = pInventoryCommandRequest.CustomerAcctId;

					if (pInventoryCommandRequest.ActivateOnLoad) {
						_batch.InventoryStatus = InventoryStatus.Activated;
					}
					else {
						_batch.InventoryStatus = InventoryStatus.Loaded;
					}

					try {
						int _cardCount = 0;
						string _line;
						while ((_line = _sr.ReadLine()) != null) {
							//pBackgroundWorker.ReportStatus(string.Format("{0}", _line));
							if (pBackgroundWorker.CancellationPending) {
								pBackgroundWorker.ReportStatus("Canceled");
								pEvtArg.Cancel = true;
								return;
							}

							string[] _fields = _line.Split(FieldDelimiter);
							long _serial = long.Parse(_fields[0]);
							long _pin = long.Parse(_fields[1]);
							loadPhoneCardAndRetailAccount(pDb, _serial, _pin, pInventoryCommandRequest);

							pBackgroundWorker.ReportProgress(++_cardCount*100/_batch.BatchSize);
						}
					}
					catch (Exception _ex) {
						pBackgroundWorker.ReportStatus(string.Format("Exception:\r\n{0}", _ex));
						pEvtArg.Cancel = true;
						return;
					}

					BatchRow _batchRow = MapToBatchRow(_batch);
					pBackgroundWorker.ReportStatus(string.Format("Mapped BatchId={0}", _batch.BatchId));

					pBackgroundWorker.ReportStatus(string.Format("Updated BatchId={0}", _batch.BatchId));
					pDb.BatchCollection.Update(_batchRow);

					logInventoryHistory(pDb, pInventoryCommandRequest.Person, DateTime.Now, pInventoryCommandRequest.ServiceId, _batch.Denomination, _batch.BatchId, _batch.BatchSize, InventoryCommand.Load, pInventoryCommandRequest.CustomerAcctId, 0, //TODO: !!! ResellerPartnerId - N/A FOR NOW
					                    0 //TODO: !!! ResellerAgentId - N/A FOR NOW
						);
					pBackgroundWorker.ReportStatus("Logged Inventory History");

					if (pInventoryCommandRequest.ActivateOnLoad) {
						logInventoryHistory(pDb, pInventoryCommandRequest.Person, DateTime.Now.AddMilliseconds(11), //Just to make sure that time is different from prev History entry
						                    pInventoryCommandRequest.ServiceId, _batch.Denomination, _batch.BatchId, _batch.BatchSize, InventoryCommand.Activate, pInventoryCommandRequest.CustomerAcctId, 0, //TODO: !!! SalesRepAcctId - N/A FOR NOW
						                    0 //TODO: !!! ResellerAgentId - N/A FOR NOW
							);
					}
					pBackgroundWorker.ReportStatus("Finished Loading Batch File: " + _batchFilePath);
				}
			}
		}

		private static RetailAccountRow createRetailAccount(Rbr_Db pDb, InventoryCommandRequest pInventoryCommandRequest) {
			var _retailAccountRow = new RetailAccountRow();
			_retailAccountRow.Customer_acct_id = pInventoryCommandRequest.CustomerAcctId;
			_retailAccountRow.Start_balance = pInventoryCommandRequest.Denomination;
			_retailAccountRow.Start_bonus_minutes = 0;
			_retailAccountRow.Current_balance = pInventoryCommandRequest.Denomination;
			_retailAccountRow.Current_bonus_minutes = 0;
			_retailAccountRow.AccountStatus = Status.Active;
			_retailAccountRow.Date_active = pInventoryCommandRequest.DateCreated;
			_retailAccountRow.Date_created = pInventoryCommandRequest.DateCreated;
			_retailAccountRow.Date_to_expire = pInventoryCommandRequest.DateToExpire;
			_retailAccountRow.Date_expired = Configuration.Instance.Db.SqlSmallDateTimeMaxValue;

			pDb.RetailAccountCollection.Insert(_retailAccountRow);
			TimokLogger.Instance.LogRbr(LogSeverity.Debug, "InventoryController.loadPhoneCardAndRetailAccount", string.Format("RetailAccountRow inserted, Id={0}", _retailAccountRow.Retail_acct_id));
			return _retailAccountRow;
		}

		private static void loadPhoneCardAndRetailAccount(Rbr_Db pDb, long pSerial, long pPin, InventoryCommandRequest pInventoryCommandRequest) {
			//PoneCard status = Loaded | (Activeted if Activate on load)
			//RetAcct  status = Active, date_active = pInventoryCommandRequest.DateCreated

			RetailAccountRow _retailAccountRow = createRetailAccount(pDb, pInventoryCommandRequest);

			var _phoneCardRow = new PhoneCardRow();
			//TODO: !!! setup new dates here...
			if (pInventoryCommandRequest.ActivateOnLoad) {
				_phoneCardRow.InventoryStatus = InventoryStatus.Activated;
				_phoneCardRow.CardStatus = Status.Active;
				_phoneCardRow.Date_active = pInventoryCommandRequest.DateActive;
			}
			else {
				_phoneCardRow.InventoryStatus = InventoryStatus.Loaded;
				_phoneCardRow.CardStatus = Status.Pending;
				_phoneCardRow.IsDate_activeNull = true;
			}
			_phoneCardRow.Pin = pPin;
			_phoneCardRow.Retail_acct_id = _retailAccountRow.Retail_acct_id;
			_phoneCardRow.Serial_number = pSerial;
			_phoneCardRow.Service_id = pInventoryCommandRequest.ServiceId;

			_phoneCardRow.Date_loaded = pInventoryCommandRequest.DateCreated;
			_phoneCardRow.Date_to_expire = pInventoryCommandRequest.DateToExpire;
			_phoneCardRow.IsDate_deactivatedNull = true;
			_phoneCardRow.IsDate_archivedNull = true;

			pDb.PhoneCardCollection.Insert(_phoneCardRow);
			TimokLogger.Instance.LogRbr(LogSeverity.Debug, "InventoryController.loadPhoneCardAndRetailAccount", string.Format("PhoneCardRow inserted, Serial={0}", _phoneCardRow.Serial_number));
		}

		private static void executeCommand(Rbr_Db pDb, BackgroundWorker pBackgroundWorker, DoWorkEventArgs pEvtArg, InventoryCommandRequest pInventoryCommandRequest) {
			#region exec logic

			//RetAcct  NO CHANGE
			//PoneCard status = Activeted,    date_active =       pInventoryCommandRequest.DateActive
			//PoneCard status = Deactivated,  date_deactivated =  pInventoryCommandRequest.DateDeactive
			//PoneCard status = Archived,     date_deactivated =  pInventoryCommandRequest.DateArchive

			#endregion exec logic

			int _count = 0;
			foreach (var _batch in pInventoryCommandRequest.Batches) {
				pBackgroundWorker.ReportStatus("Start executing " + pInventoryCommandRequest.InventoryCommand + " command on Batch: [id=" + _batch.BatchId + "] [CardCount=" + _batch.BatchSize + "]");

				if (pBackgroundWorker.CancellationPending) {
					pEvtArg.Cancel = true;
					return;
				}

				switch (pInventoryCommandRequest.InventoryCommand) {
					case InventoryCommand.Activate:
						_batch.InventoryStatus = InventoryStatus.Activated;
						pDb.PhoneCardCollection.ActivateInventory(pInventoryCommandRequest.DateActive, _batch.FirstSerial, _batch.LastSerial);
						break;
					case InventoryCommand.Deactivate:
						_batch.InventoryStatus = InventoryStatus.Deactivated;
						pDb.PhoneCardCollection.DeactivateInventory(_batch.FirstSerial, _batch.LastSerial);
						break;
					case InventoryCommand.Archive:
						_batch.InventoryStatus = InventoryStatus.Archived;
						pDb.PhoneCardCollection.ArchiveInventory(_batch.FirstSerial, _batch.LastSerial);
						break;
					case InventoryCommand.Load:
					default:
						throw new ArgumentException("Unexpected InventoryCommand [" + pInventoryCommandRequest.InventoryCommand + "]");
				}
				var _batchRow = MapToBatchRow(_batch);
				pDb.BatchCollection.Update(_batchRow);

				//TODO: !!! ResellerAgentId - N/A FOR NOW
				//TODO: !!! SalesRepAcctId - N/A FOR NOW				
				logInventoryHistory(pDb, pInventoryCommandRequest.Person, DateTime.Now, pInventoryCommandRequest.ServiceId, _batch.Denomination, _batch.BatchId, _batch.BatchSize, pInventoryCommandRequest.InventoryCommand, pInventoryCommandRequest.CustomerAcctId, 0, 0);
				pBackgroundWorker.ReportProgress(++_count*100/pInventoryCommandRequest.Batches.Length);
				pBackgroundWorker.ReportStatus("Finished executing " + pInventoryCommandRequest.InventoryCommand + " command on Batch: [id=" + _batch.BatchId + "] [CardCount=" + _batch.BatchSize + "]");
			}
		}

		private static void executeExport(Rbr_Db pDb, BackgroundWorker pBackgroundWorker, DoWorkEventArgs pEvtArg, InventoryCommandRequest pInventoryCommandRequest) {
			int _count = 0;
			foreach (var _batch in pInventoryCommandRequest.Batches) {
				pBackgroundWorker.ReportStatus("Start exporting " + pInventoryCommandRequest.InventoryCommand + " command on Batch: [id=" + _batch.BatchId + "] [CardCount=" + _batch.BatchSize + "]");

				if (pBackgroundWorker.CancellationPending) {
					pEvtArg.Cancel = true;
					return;
				}

				int[] _retailAcctIds = pDb.PhoneCardCollection.Export(InventoryStatus.Loaded, _batch.FirstSerial, _batch.LastSerial);
				var _batchRow = MapToBatchRow(_batch);
				pDb.BatchCollection.Update(_batchRow);

				//TODO: !!! ResellerAgentId - N/A FOR NOW
				//TODO: !!! SalesRepAcctId - N/A FOR NOW				
				logInventoryHistory(pDb, pInventoryCommandRequest.Person, DateTime.Now, pInventoryCommandRequest.ServiceId, _batch.Denomination, _batch.BatchId, _batch.BatchSize, pInventoryCommandRequest.InventoryCommand, pInventoryCommandRequest.CustomerAcctId, 0, 0);
				pBackgroundWorker.ReportProgress(++_count*100/pInventoryCommandRequest.Batches.Length);
				pBackgroundWorker.ReportStatus("Finished executing " + pInventoryCommandRequest.InventoryCommand + " command on Batch: [id=" + _batch.BatchId + "] [CardCount=" + _batch.BatchSize + "]");
			}
		}

		private static void logInventoryHistory(Rbr_Db pDb, PersonDto pPerson, DateTime pTimestamp, short pServiceId, decimal pDenomination, int pBatchId, int pNumberOfCards, InventoryCommand pInventoryCommand, short pCustomerAcctId, int pResellerPartnerId, int pResellerAgentId) {
			var _inventoryHistoryRow = new InventoryHistoryRow();
			_inventoryHistoryRow.Service_id = pServiceId;
			_inventoryHistoryRow.Batch_id = pBatchId;
			_inventoryHistoryRow.Timestamp = pTimestamp;
			_inventoryHistoryRow.InventoryCommand = pInventoryCommand;
			_inventoryHistoryRow.Number_of_cards = pNumberOfCards;
			_inventoryHistoryRow.Denomination = pDenomination;
			_inventoryHistoryRow.Person_id = pPerson.PersonId;

			if (pCustomerAcctId > 0) {
				_inventoryHistoryRow.Customer_acct_id = pCustomerAcctId; //N/A FOR THIS COMMAND
			}

			if (pResellerPartnerId > 0) {
				_inventoryHistoryRow.Reseller_partner_id = pResellerPartnerId; //N/A FOR THIS COMMAND
			}

			if (pResellerAgentId > 0) {
				_inventoryHistoryRow.Reseller_agent_id = pResellerAgentId; //N/A FOR THIS COMMAND
			}

			pDb.InventoryHistoryCollection.Insert(_inventoryHistoryRow);
		}

		#endregion Privates
	}
}
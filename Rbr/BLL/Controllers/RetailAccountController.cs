using System;
using Timok.Core.BackgroundProcessing;
using Timok.Core.DataProtection;
using Timok.Rbr.BLL.ImportExport.Retail;
using Timok.Rbr.BLL.Managers;
using Timok.Rbr.Core;
using Timok.Rbr.Core.Config;
using Timok.Rbr.DAL.RbrDatabase;
using Timok.Rbr.DTO;

namespace Timok.Rbr.BLL.Controllers {
	public class RetailAccountController {
		RetailAccountController() { }

		#region Public Getters

		public static int GetCount(short pCustomerAcctId) {
			using (var _db = new Rbr_Db()) {
				return RetailAccountManager.Instance.GetCount(_db, pCustomerAcctId);
			}
		}

		public static RetailAccountDto GetByRetailAcctId(int pRetailAcctId) {
			using (var _db = new Rbr_Db()) {
				return RetailAccountManager.Instance.GetAcct(_db, pRetailAcctId);
			}
		}

		public static RetailAccountDto GetByVoIPUserId(short pServiceId, string pUserId) {
			using (var _db = new Rbr_Db()) {
				return RetailAccountManager.Instance.GetByVoIPUserId(_db, pServiceId, pUserId);
			}
		}

		public static RetailAccountDto GetByANI(short pServiceId, long pANI) {
			using (var _db = new Rbr_Db()) {
				return RetailAccountManager.Instance.GetByANI(_db, pServiceId, pANI);
			}
		}

		public static RetailAccountDto GetByPIN(short pServiceId, long pPIN) {
			using (var _db = new Rbr_Db()) {
				return RetailAccountManager.Instance.GetByPIN(_db, pServiceId, pPIN);
			}
		}

		public static RetailAccountDto GetBySerialNumber(short pServiceId, long pSerialNumber) {
			using (var _db = new Rbr_Db()) {
				RetailAccountDto _retailAccount = RetailAccountManager.Instance.GetBySerialNumber(_db, pServiceId, pSerialNumber);
				if (_retailAccount != null && _retailAccount.ServiceId != pServiceId) {
					return null;
				}
				return _retailAccount;
			}
		}

		#region Get Payments

		public static RetailAccountPaymentDto[] GetPaymentsByRetailAcctId(int pRetailAcctId) {
			using (var _db = new Rbr_Db()) {
				return RetailAccountManager.GetByRetailAcctId(_db, pRetailAcctId);
			}
		}

		public static RetailAccountPaymentDto[] GetPaymentsByPersonId(int pPersonId) {
			using (var _db = new Rbr_Db()) {
				return RetailAccountManager.GetByPersonId(_db, pPersonId);
			}
		}

		public static BalanceAdjustmentReasonDto[] GetBalanceAdjustmentReasons() {
			using (var _db = new Rbr_Db()) {
				return BalanceAdjustmentReasonManager.GetByType(_db, BalanceAdjustmentReasonType.Retail);
			}
		}

		#endregion Get Payments

		#endregion Public Getters

		#region Public Actions

		public static void Save(RetailAccountDto pRetailAccount) {
			//IMPORTANT: !!! SALT MUST BE THE SAME ON ALL SERVERS !!!
			//IMPORTANT: !!! SALT MUST BE SET ONLY ONCE !!!
			//that's why it's set outside of the transaction, so we can replicate it to other servers
			string _salt = SaltHashedPwd.CreateRandomSalt();
			if (pRetailAccount.RetailAcctId == 0) {
				Add(_salt, pRetailAccount);
			}
			else {
				Update(_salt, pRetailAccount);
			}
		}

		public static void Add(string pSalt, RetailAccountDto pRetailAccount) {
			using (var _db = new Rbr_Db()) {
				using (var _tx = new Transaction(_db, pSalt, pRetailAccount)) {
					RetailAccountRow _retailAccountRow = RetailAccountManager.MapToRetailAccountRow(pRetailAccount);

					//TODO: ! CONFIRM IT before implementing
					//					CustomerAcctRow _customerAcctRow = _db.CustomerAcctCollection.GetByPrimaryKey(pRetailAccount.CustomerAcctId);
					//					if (_customerAcctRow.BonusMinutesType == BonusMinutesType.None) {
					//						_retailAccountRow.Current_bonus_minutes = -1;
					//					}

					_retailAccountRow.Date_created = DateTime.Now;
					_retailAccountRow.Date_expired = Configuration.Instance.Db.SqlSmallDateTimeMaxValue;
					_retailAccountRow.Current_balance = _retailAccountRow.Start_balance;
					_retailAccountRow.Current_bonus_minutes = _retailAccountRow.Start_bonus_minutes;

					if (_retailAccountRow.AccountStatus == Status.Active) {
						_retailAccountRow.Date_active = _retailAccountRow.Date_created;
					}
					else {
						_retailAccountRow.Date_active = Configuration.Instance.Db.SqlSmallDateTimeMaxValue;
					}

					RetailAccountManager.Instance.Add(_db, _retailAccountRow);
					pRetailAccount.RetailAcctId = _retailAccountRow.Retail_acct_id;

					//prepare PhoneCard (s)
					if (pRetailAccount.PhoneCards != null && pRetailAccount.PhoneCards.Length > 0) {
						//NOTE: !!! in this virsion only ONE PhoneCard p/RetailAccount allowed!!!
						if (pRetailAccount.PhoneCards.Length > 1) {
							throw new Exception("In this virsion only ONE PhoneCard p/RetailAccount allowed!!!");
						}
						foreach (PhoneCardDto _phoneCard in pRetailAccount.PhoneCards) {
							long _pin = 0;
							if (_phoneCard.SerialNumber == 0) {
								ServiceRow _serviceRow = ServiceManager.Get(_db, pRetailAccount.ServiceId);
								long _serialNumber;
								generateTestSerialNumberPIN(_db, _serviceRow, out _serialNumber, out _pin);
								_phoneCard.SerialNumber = _serialNumber;
							}
							if (_phoneCard.Pin == 0) {
								_phoneCard.Pin = _pin;
							}
							switch (pRetailAccount.Status) {
								case Status.Active:
									_phoneCard.InventoryStatus = InventoryStatus.Activated;
									break;
								case Status.Pending:
								case Status.Blocked:
								case Status.Archived:
								case Status.InUse:
								default:
									throw new ArgumentException(string.Format("Unexpected Status [{0}]", pRetailAccount.Status));
							}
							_phoneCard.ServiceId = pRetailAccount.ServiceId;
							_phoneCard.RetailAcctId = pRetailAccount.RetailAcctId;

							_phoneCard.Status = pRetailAccount.Status;
							//TODO: ??? set it based on RetAcct status ???
							_phoneCard.InventoryStatus = InventoryStatus.Activated;

							_phoneCard.DateLoaded = _retailAccountRow.Date_created;
							_phoneCard.DateActive = _retailAccountRow.Date_created;
							_phoneCard.DateToExpire = _retailAccountRow.Date_to_expire;
							_phoneCard.DateDeactivated = Configuration.Instance.Db.SqlSmallDateTimeMaxValue;
							_phoneCard.DateArchived = Configuration.Instance.Db.SqlSmallDateTimeMaxValue;
						}
					}

					//prepare ResidentialPSTN (s)
					if (pRetailAccount.ResidentialPSTNs != null && pRetailAccount.ResidentialPSTNs.Length > 0) {
						//NOTE: !!! in this virsion only ONE ResidentialPSTN p/RetailAccount allowed!!!
						if (pRetailAccount.ResidentialPSTNs.Length > 1) {
							throw new Exception("In this virsion only ONE ResidentialPSTN p/RetailAccount allowed!!!");
						}
						foreach (var _residentialPSTN in pRetailAccount.ResidentialPSTNs) {
							_residentialPSTN.Status = pRetailAccount.Status;
							_residentialPSTN.ServiceId = pRetailAccount.ServiceId;
							_residentialPSTN.RetailAcctId = pRetailAccount.RetailAcctId;
						}
					}

					////prepare ResidentialVoIP (s)
					//if (pRetailAccount.ResidentialVoIPs != null && pRetailAccount.ResidentialVoIPs.Length > 0) {
					//  //NOTE: !!! in this virsion only ONE ResidentialVoIP p/RetailAccount allowed!!!
					//  if (pRetailAccount.ResidentialVoIPs.Length > 1) {
					//    throw new Exception("In this virsion only ONE ResidentialVoIP p/RetailAccount allowed!!!");
					//  }
					//  foreach (ResidentialVoIP _residentialVoIP in pRetailAccount.ResidentialVoIPs) {
					//    _residentialVoIP.Status = pRetailAccount.Status;
					//    _residentialVoIP.ServiceId = pRetailAccount.ServiceId;
					//    _residentialVoIP.RetailAcctId = pRetailAccount.RetailAcctId;
					//  }
					//}

					if (pRetailAccount.AccessEnabled) {
						pRetailAccount.Person.RetailAcctId = pRetailAccount.RetailAcctId;
						PersonManager.Save(_db, pSalt, pRetailAccount.Person);
					}

					if (pRetailAccount.PhoneCards != null && pRetailAccount.PhoneCards.Length > 0) {
						//Insert PhoneCard
						//TODO: now works only with 1 (one) card per RetailAcct
						PhoneCardRow _phoneCardRow = RetailAccountManager.MapToPhoneCardRow(pRetailAccount.PhoneCards[0]);
						RetailAccountManager.AddPhoneCard(_db, _phoneCardRow);
					}

					if (pRetailAccount.ResidentialPSTNs != null && pRetailAccount.ResidentialPSTNs.Length > 0) {
						//Insert ResidentialPSTN
						//TODO: now works only with 1 (one) PSTN # per RetailAcct
						ResidentialPSTNRow _residentialPSTNRow = RetailAccountManager.MapToResidentialPSTNRow(pRetailAccount.ResidentialPSTNs[0]);
						RetailAccountManager.AddResidentialPSTNSubAcct(_db, _residentialPSTNRow);
					}

					_tx.Commit();
				}
			}
		}

		public static void Update(string pSalt, RetailAccountDto pRetailAccount) {
			if (pRetailAccount.AccessEnabled && (pRetailAccount.Person.Salt == null || pRetailAccount.Person.Salt.Trim().Length == 0)) {
				pRetailAccount.Person.Salt = pSalt;
			}

			using (var _db = new Rbr_Db()) {
				using (var _tx = new Transaction(_db, pSalt, pRetailAccount)) {
					RetailAccountRow _originalRetailAccountRow = RetailAccountManager.Instance.Get(_db, pRetailAccount.RetailAcctId);
					RetailAccountRow _retailAccountRow = RetailAccountManager.MapToRetailAccountRow(pRetailAccount);

					if (_originalRetailAccountRow.AccountStatus != Status.Active && _retailAccountRow.AccountStatus == Status.Active && _retailAccountRow.Date_active == Configuration.Instance.Db.SqlSmallDateTimeMaxValue) {
						_retailAccountRow.Date_active = DateTime.Today;
					}

					if (pRetailAccount.AccessEnabled) {
						pRetailAccount.Person.RetailAcctId = pRetailAccount.RetailAcctId;
						PersonManager.Save(_db, pSalt, pRetailAccount.Person);
					}
					else {
						PersonManager.DeleteByRetailAcctId(_db, pRetailAccount.RetailAcctId);
					}

					RetailAccountManager.Instance.Update(_db, _retailAccountRow);

					if (pRetailAccount.PhoneCards != null && pRetailAccount.PhoneCards.Length > 0) {
						//Update PhoneCard
						//TODO: now works only with 1 (one) PhoneCard per RetailAcct
						PhoneCardRow _phoneCardRow = RetailAccountManager.MapToPhoneCardRow(pRetailAccount.PhoneCards[0]);
						_phoneCardRow.CardStatus = pRetailAccount.Status;
						RetailAccountManager.UpdatePhoneCard(_db, _phoneCardRow);
					}

					if (pRetailAccount.ResidentialPSTNs != null && pRetailAccount.ResidentialPSTNs.Length > 0) {
						//Update ResidentialPSTN
						//TODO: now works only with 1 (one) PSTN # per RetailAcct
						ResidentialPSTNRow _residentialPSTNRow = RetailAccountManager.MapToResidentialPSTNRow(pRetailAccount.ResidentialPSTNs[0]);
						_residentialPSTNRow.AccountStatus = pRetailAccount.Status;
						RetailAccountManager.UpdateResidentialPSTNSubAcct(_db, _residentialPSTNRow);
					}

					_tx.Commit();
				}
			}
		}

		public static void Delete(RetailAccountDto pRetailAccount) {
			using (var _db = new Rbr_Db()) {
				using (var _tx = new Transaction(_db, pRetailAccount)) {
					RetailAccountManager.DeletePhoneCard(_db, pRetailAccount.RetailAcctId);
					RetailAccountManager.DeleteResidentialPSTNSubAcct(_db, pRetailAccount.RetailAcctId);
					PersonManager.DeleteByRetailAcctId(_db, pRetailAccount.RetailAcctId);
					RetailAccountManager.Instance.Delete(_db, pRetailAccount);
					_tx.Commit();
				}
			}
		}

		public static void Credit(PersonDto pPerson, RetailAccountPaymentDto pRetailAccountPayment) {
			using (var _db = new Rbr_Db()) {
				using (var _tx = new Transaction(_db, pRetailAccountPayment)) {
					//NOTE: make sure we got prev amnt
					RetailAccountRow _retailAccountRow = _db.RetailAccountCollection.GetByPrimaryKey(pRetailAccountPayment.RetailAcctId);
					pRetailAccountPayment.DateTime = DateTime.Now;
					pRetailAccountPayment.PreviousAmount = _retailAccountRow.Current_balance;
					pRetailAccountPayment.PreviousBonusMinutes = _retailAccountRow.Current_bonus_minutes;
					pRetailAccountPayment.Person = pPerson;

					RetailAccountManager.Instance.Credit(_db, pRetailAccountPayment);
					RetailAccountManager.Add(_db, pRetailAccountPayment);
					_tx.Commit();
				}
			}
		}

		//BUG: will not work on remote (replicated nodes
		//TODO: !!!!
		public static void Import(PhoneCardBatch pPhoneCardBatch, BackgroundWorker pBackgroundWorker) {
			reportStatus("Connecting to DB...", pBackgroundWorker);
			using (var _db = new Rbr_Db()) {
				//using (Transaction _tx = new Transaction(_db, pPhoneCardBatch, null)) {
				ServiceRow _serviceRow = ServiceManager.Get(_db, pPhoneCardBatch.ServiceId);
				if (_serviceRow == null) {
					throw new Exception("Service not found [Id: " + pPhoneCardBatch.ServiceId + "]");
				}
				checkPendingCancellation(pBackgroundWorker);
				reportStatus("Data Import Stated", pBackgroundWorker);

				importBatch(_db, pPhoneCardBatch, pBackgroundWorker);

				//_tx.Commit();
				//}
			}
		}

		static void importBatch(Rbr_Db pDb, PhoneCardBatch pPhoneCardBatch, BackgroundWorker pBackgroundWorker) {
			if (pPhoneCardBatch.InventoryStatus != InventoryStatus.Activated) {
				throw new ArgumentException("Unexpected pPhoneCardBatch.InventoryStatus [" + pPhoneCardBatch.InventoryStatus + "]");
			}

			pPhoneCardBatch.DateCreated = DateTime.Now;

			int _index = 0;
			pDb.BeginTransaction();
			try {
				foreach (PhoneCardDto _phoneCard in pPhoneCardBatch.PhoneCards) {
					_index++;
					importPhoneCardAndRetailAccount(pDb, _phoneCard, pPhoneCardBatch.InventoryStatus, pPhoneCardBatch);

					reportProgress(_index * 100 / pPhoneCardBatch.PhoneCards.Count, pBackgroundWorker);
					if (_index % 100 == 0) {
						reportStatus("Loaded " + _index + " Phone Cards", pBackgroundWorker);
					}
				}
			}
			catch {
				pDb.RollbackTransaction();
				throw;
			}
			pDb.CommitTransaction();
		}

		static void importPhoneCardAndRetailAccount(Rbr_Db pDb, PhoneCardDto pPhoneCard, InventoryStatus pInitialInventoryStatus, PhoneCardBatch pPhoneCardBatch) {
			var _retailAccountRow = new RetailAccountRow();
			_retailAccountRow.AccountStatus = Status.Active; //NOTE
			_retailAccountRow.Start_balance = pPhoneCardBatch.StartBalance;
			_retailAccountRow.Start_bonus_minutes = pPhoneCardBatch.StartBonusMinutes;
			_retailAccountRow.Current_balance = pPhoneCardBatch.StartBalance;
			_retailAccountRow.Current_bonus_minutes = pPhoneCardBatch.StartBonusMinutes;
			_retailAccountRow.Customer_acct_id = pPhoneCardBatch.CustomerAcctId;
			_retailAccountRow.Date_created = pPhoneCardBatch.DateCreated; //NOTE
			_retailAccountRow.Date_active = pPhoneCardBatch.DateCreated; //NOTE
			_retailAccountRow.Date_to_expire = pPhoneCardBatch.DateToExpire;
			_retailAccountRow.Date_expired = Configuration.Instance.Db.SqlSmallDateTimeMaxValue;

			RetailAccountManager.Instance.Add(pDb, _retailAccountRow);
			pPhoneCard.RetailAcctId = _retailAccountRow.Retail_acct_id;

			var _phoneCardRow = new PhoneCardRow();
			_phoneCardRow.InventoryStatus = pInitialInventoryStatus;
			if (pInitialInventoryStatus == InventoryStatus.Activated) {
				_phoneCardRow.CardStatus = Status.Active;
			}
			else {
				_phoneCardRow.CardStatus = Status.Pending;
			}
			_phoneCardRow.Pin = pPhoneCard.Pin;
			_phoneCardRow.Serial_number = pPhoneCard.SerialNumber;
			_phoneCardRow.Service_id = pPhoneCard.ServiceId;
			_phoneCardRow.Retail_acct_id = pPhoneCard.RetailAcctId;

			_phoneCardRow.Date_loaded = pPhoneCardBatch.DateCreated;
			_phoneCardRow.Date_active = pPhoneCardBatch.DateCreated;
			_phoneCardRow.Date_to_expire = pPhoneCardBatch.DateToExpire;
			_phoneCardRow.IsDate_deactivatedNull = true;
			_phoneCardRow.IsDate_archivedNull = true;

			RetailAccountManager.AddPhoneCard(pDb, _phoneCardRow);
		}

		#endregion Public Actions

		#region privates

		static void checkPendingCancellation(BackgroundWorker pBackgroundWorker) {
			if (pBackgroundWorker != null && pBackgroundWorker.CancellationPending) {
				throw new Exception("Import canceled");
			}
		}

		static void reportStatus(string pStatus, BackgroundWorker pBackgroundWorker) {
			if (pBackgroundWorker != null) {
				pBackgroundWorker.ReportStatus(pStatus);
			}
		}

		static void reportProgress(int pPercent, BackgroundWorker pBackgroundWorker) {
			if (pBackgroundWorker != null) {
				pBackgroundWorker.ReportProgress(pPercent);
			}
		}

		static void generateTestSerialNumberPIN(Rbr_Db pDb, ServiceRow pServiceRow, out long pSerialNumber, out long pPIN) {
			var _rnd = new Random();
			pPIN = 0;

			pSerialNumber = getRandomNumber(_rnd, -597893, -307800); // negative number;
			int _tryCount = 0;
			//!!! check if already exists...
			while (RetailAccountManager.PhoneCardExistsBySerialNumber(pDb, pServiceRow.Service_id, pSerialNumber)) {
				_tryCount++;
				if (_tryCount > 7) {
					pSerialNumber = 0;
					throw new Exception("Failed to generate Test SerialNumber");
				}
				pSerialNumber = getRandomNumber(_rnd, -597893, -307800); // negative number;
			}

			if (pServiceRow.RetailType == RetailType.PhoneCard) {
				_tryCount = 0;
				pPIN = getRandomNumber(_rnd, 307800, 597893); //6 numbers
				//!!! check if already exists...
				while (RetailAccountManager.PhoneCardExistsByCardNumber(pDb, pServiceRow.Service_id, pPIN)) {
					_tryCount++;
					if (_tryCount > 7) {
						pPIN = 0;
						throw new Exception("Failed to generate Test PIN");
					}
					pPIN = getRandomNumber(_rnd, 307800, 597893); //6 numbers
				}
			}
		}

		static long getRandomNumber(Random pRandom, int pMin, int pMax) { return pRandom.Next(pMin, pMax); }

		#endregion privates
	}
}
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using Timok.Core;
using Timok.Rbr.Core.Config;
using Timok.Rbr.DAL;
using Timok.Rbr.DAL.RbrDatabase;
using Timok.Rbr.DTO;

namespace Timok.Rbr.BLL.Managers {
	internal class RetailAccountManager {
		internal static readonly RetailAccountManager Instance = new RetailAccountManager();
		RetailAccountManager() { }

		#region Getters

		internal int GetCount(Rbr_Db pDb, short pCustomerAcctId) { return pDb.RetailAccountCollection.GetCountByCustomer_acct_id(pCustomerAcctId); }

		internal RetailAccountRow Get(Rbr_Db pDb, int pRetailAcctId) { return pDb.RetailAccountCollection.GetByPrimaryKey(pRetailAcctId); }

		internal RetailAccountDto GetAcct(Rbr_Db pDb, int pRetailAcctId) {
			RetailAccountRow _retailAccountRow = pDb.RetailAccountCollection.GetByPrimaryKey(pRetailAcctId);
			return get(pDb, _retailAccountRow);
		}

		internal RetailAccountDto GetBySerialNumber(Rbr_Db pDb, short pServiceId, long pSerialNumber) {
			PhoneCardRow _phoneCardRow = pDb.PhoneCardCollection.GetBySerialNumber(pServiceId, pSerialNumber);
			if (_phoneCardRow != null) {
				RetailAccountRow _retailAccountRow = pDb.RetailAccountCollection.GetByPrimaryKey(_phoneCardRow.Retail_acct_id);
				return get(pDb, _phoneCardRow.Service_id, _retailAccountRow);
			}
			return null;
		}

		internal RetailAccountDto GetByVoIPUserId(Rbr_Db pDb, short pServiceId, string pUserId) {
			ResidentialVoIPRow _residentialVoIPRow = pDb.ResidentialVoIPCollection.GetByPrimaryKey(pUserId);
			if (_residentialVoIPRow != null) {
				RetailAccountRow _retailAccountRow = pDb.RetailAccountCollection.GetByPrimaryKey(_residentialVoIPRow.Retail_acct_id);
				return get(pDb, pServiceId, _retailAccountRow);
			}
			return null;
		}

		internal RetailAccountDto GetByANI(Rbr_Db pDb, short pServiceId, long pANI) {
			ResidentialPSTNRow _residentialPSTNRow = pDb.ResidentialPSTNCollection.GetByPrimaryKey(pServiceId, pANI);
			if (_residentialPSTNRow != null) {
				RetailAccountRow _retailAccountRow = pDb.RetailAccountCollection.GetByPrimaryKey(_residentialPSTNRow.Retail_acct_id);
				return get(pDb, pServiceId, _retailAccountRow);
			}
			return null;
		}

		internal RetailAccountDto GetByPIN(Rbr_Db pDb, short pServiceId, long pPIN) {
			PhoneCardRow _phoneCardRow = pDb.PhoneCardCollection.GetByPrimaryKey(pServiceId, pPIN);
			if (_phoneCardRow != null) {
				RetailAccountRow _retailAccountRow = pDb.RetailAccountCollection.GetByPrimaryKey(_phoneCardRow.Retail_acct_id);
				return get(pDb, pServiceId, _retailAccountRow);
			}
			return null;
		}

		internal static bool HasPayments(Rbr_Db pDb, int pPersonId) { return pDb.RetailAccountPaymentCollection.HasPayments(pPersonId); }

		internal static RetailAccountPaymentDto[] GetByRetailAcctId(Rbr_Db pDb, int pRetailAcctId) {
			var _list = new ArrayList();
			RetailAccountPaymentRow[] _retailAccountPaymentRows = pDb.RetailAccountPaymentCollection.GetByRetail_acct_id(pRetailAcctId);
			foreach (RetailAccountPaymentRow _retailAccountPaymentRow in _retailAccountPaymentRows) {
				BalanceAdjustmentReasonDto _balanceAdjustmentReason = BalanceAdjustmentReasonManager.Get(pDb, _retailAccountPaymentRow.Balance_adjustment_reason_id);
				PersonDto _person = PersonManager.Get(pDb, _retailAccountPaymentRow.Person_id);
				_list.Add(mapToRetailAccountPayment(_retailAccountPaymentRow, _person, _balanceAdjustmentReason));
			}
			if (_list.Count > 1) {
				_list.Sort(new GenericComparer(RetailAccountPaymentDto.DateTime_PropName, ListSortDirection.Descending));
			}
			return (RetailAccountPaymentDto[]) _list.ToArray(typeof (RetailAccountPaymentDto));
		}

		internal static RetailAccountPaymentDto[] GetByPersonId(Rbr_Db pDb, int pPersonId) {
			var _list = new ArrayList();
			RetailAccountPaymentRow[] _retailAccountPaymentRows = pDb.RetailAccountPaymentCollection.GetByPerson_id(pPersonId);
			foreach (RetailAccountPaymentRow _retailAccountPaymentRow in _retailAccountPaymentRows) {
				BalanceAdjustmentReasonDto _balanceAdjustmentReason = BalanceAdjustmentReasonManager.Get(pDb, _retailAccountPaymentRow.Balance_adjustment_reason_id);
				PersonDto _person = PersonManager.Get(pDb, _retailAccountPaymentRow.Person_id);
				_list.Add(mapToRetailAccountPayment(_retailAccountPaymentRow, _person, _balanceAdjustmentReason));
			}
			if (_list.Count > 1) {
				_list.Sort(new GenericComparer(RetailAccountPaymentDto.DateTime_PropName, ListSortDirection.Descending));
			}
			return (RetailAccountPaymentDto[]) _list.ToArray(typeof (RetailAccountPaymentDto));
		}

		#endregion Getters

		#region Actions

		internal void Add(Rbr_Db pDb, RetailAccountRow pRetailAccountRow) { pDb.RetailAccountCollection.Insert(pRetailAccountRow); }

		internal void Update(Rbr_Db pDb, RetailAccountRow pRetailAccountRow) { pDb.RetailAccountCollection.Update(pRetailAccountRow); }

		internal void Delete(Rbr_Db pDb, RetailAccountDto pRetailAccount) { pDb.RetailAccountCollection.DeleteByPrimaryKey(pRetailAccount.RetailAcctId); }

		internal void Credit(Rbr_Db pDb, RetailAccountPaymentDto pRetailAccountPayment) {
			if (! pDb.RetailAccountCollection.Credit(pRetailAccountPayment.RetailAcctId, pRetailAccountPayment.Amount, pRetailAccountPayment.AddedBonusMinutes)) {
				throw new ApplicationException("Failed to Credit Retail Account; RetailAcctId: " + pRetailAccountPayment.RetailAcctId);
			}
		}

		internal static void Add(Rbr_Db pDb, RetailAccountPaymentDto pRetailAccountPayment) { pDb.RetailAccountPaymentCollection.Insert(mapToRetailAccountPaymentRow(pRetailAccountPayment)); }

		public static int AddPayphoneSurcharge(Rbr_Db pDb, PayphoneSurchargeDto pPayphoneSurcharge) {
			PayphoneSurchargeRow _payphoneSurchargeRow = MapToPayphoneSurchargeRow(pPayphoneSurcharge);
			pDb.PayphoneSurchargeCollection.Insert(_payphoneSurchargeRow);
			return _payphoneSurchargeRow.Payphone_surcharge_id;
		}

		public static void UpdatePayphoneSurcharge(Rbr_Db pDb, ServiceDto pService) {
			PayphoneSurchargeRow _payphoneSurchargeRow = MapToPayphoneSurchargeRow(pService.PayphoneSurcharge);
			pDb.PayphoneSurchargeCollection.Update(_payphoneSurchargeRow);
		}

		internal static void AddResidentialPSTNSubAcct(Rbr_Db pDb, ResidentialPSTNRow pResidentialPSTNRow) {
			try {
				pDb.ResidentialPSTNCollection.Insert(pResidentialPSTNRow);
			}
			catch (PrimaryKeyException) {
				throw new ANIAlreadyInUseException();
			}
			catch (Exception _ex) {
				throw new Exception("Unexpected Error", _ex);
			}
		}

		internal static void UpdateResidentialPSTNSubAcct(Rbr_Db pDb, ResidentialPSTNRow pResidentialPSTNRow) { pDb.ResidentialPSTNCollection.Update(pResidentialPSTNRow); }

		internal static void DeleteResidentialPSTNSubAcct(Rbr_Db pDb, int pRetailAcctId) { pDb.ResidentialPSTNCollection.DeleteByRetail_acct_id(pRetailAcctId); }

		internal static bool PhoneCardExistsByCardNumber(Rbr_Db pDb, short pServiceId, long pPIN) { return pDb.PhoneCardCollection.ExistsByCardNumber(pServiceId, pPIN); }

		internal static bool PhoneCardExistsBySerialNumber(Rbr_Db pDb, short pServiceId, long pSerialNumber) { return pDb.PhoneCardCollection.ExistsBySerialNumber(pServiceId, pSerialNumber); }

		internal static void AddPhoneCard(Rbr_Db pDb, PhoneCardRow pPhoneCardRow) { pDb.PhoneCardCollection.Insert(pPhoneCardRow); }

		internal static void UpdatePhoneCard(Rbr_Db pDb, PhoneCardRow pPhoneCardRow) { pDb.PhoneCardCollection.Update(pPhoneCardRow); }

		internal static void DeletePhoneCard(Rbr_Db pDb, int pRetailAcctId) { pDb.PhoneCardCollection.DeleteByRetail_acct_id(pRetailAcctId); }

		public void UpdateAcct(Rbr_Db pDb, RetailAccountDto pRetailAcct) { Update(pDb, MapToRetailAccountRow(pRetailAcct)); }

		#endregion Actions

		#region mappings

		#region To DAL mappings

		internal static RetailAccountRow[] MapToRetailAccountRows(RetailAccountDto[] pRetailAccounts) {
			var _list = new ArrayList();
			if (pRetailAccounts != null) {
				foreach (RetailAccountDto _retailAccount in pRetailAccounts) {
					RetailAccountRow _retailAccountRow = MapToRetailAccountRow(_retailAccount);
					_list.Add(_retailAccountRow);
				}
			}
			return (RetailAccountRow[]) _list.ToArray(typeof (RetailAccountRow));
		}

		internal static RetailAccountRow MapToRetailAccountRow(RetailAccountDto pRetailAccount) {
			var _retailAccountRow = new RetailAccountRow
			                        	{
			                        		AccountStatus = pRetailAccount.Status, 
																	Current_balance = pRetailAccount.CurrentBalance, 
																	Current_bonus_minutes = pRetailAccount.CurrentBonusMinutes, 
																	Date_active = pRetailAccount.DateActive, 
																	Date_created = pRetailAccount.DateCreated, 
																	Date_expired = pRetailAccount.DateExpired, 
																	Date_to_expire = pRetailAccount.DateToExpire, 
																	Retail_acct_id = pRetailAccount.RetailAcctId, 
																	Start_balance = pRetailAccount.StartBalance, 
																	Start_bonus_minutes = pRetailAccount.StartBonusMinutes
			                        	};
			if (pRetailAccount.CustomerAcctId > 0) {
				_retailAccountRow.Customer_acct_id = pRetailAccount.CustomerAcctId;
			}

			return _retailAccountRow;
		}

		internal static PhoneCardRow MapToPhoneCardRow(PhoneCardDto pPhoneCard) {
			var _phoneCardRow = new PhoneCardRow();
			_phoneCardRow.InventoryStatus = pPhoneCard.InventoryStatus;
			_phoneCardRow.CardStatus = pPhoneCard.Status;
			_phoneCardRow.Pin = pPhoneCard.Pin;
			_phoneCardRow.Serial_number = pPhoneCard.SerialNumber;
			_phoneCardRow.Service_id = pPhoneCard.ServiceId;
			_phoneCardRow.Retail_acct_id = pPhoneCard.RetailAcctId;

			_phoneCardRow.Date_loaded = pPhoneCard.DateLoaded;
			_phoneCardRow.Date_to_expire = pPhoneCard.DateToExpire;
			if (pPhoneCard.DateActive < Configuration.Instance.Db.SqlSmallDateTimeMaxValue && pPhoneCard.DateActive > DateTime.MinValue) {
				_phoneCardRow.Date_active = pPhoneCard.DateActive;
			}
			if (pPhoneCard.DateDeactivated < Configuration.Instance.Db.SqlSmallDateTimeMaxValue && pPhoneCard.DateDeactivated > DateTime.MinValue) {
				_phoneCardRow.Date_deactivated = pPhoneCard.DateDeactivated;
			}
			if (pPhoneCard.DateArchived < Configuration.Instance.Db.SqlSmallDateTimeMaxValue && pPhoneCard.DateArchived > DateTime.MinValue) {
				_phoneCardRow.Date_archived = pPhoneCard.DateArchived;
			}
			if (pPhoneCard.DateFirstUsed < Configuration.Instance.Db.SqlSmallDateTimeMaxValue && pPhoneCard.DateFirstUsed > DateTime.MinValue) {
				_phoneCardRow.Date_first_used = pPhoneCard.DateFirstUsed;
			}
			if (pPhoneCard.DateLastUsed < Configuration.Instance.Db.SqlSmallDateTimeMaxValue && pPhoneCard.DateLastUsed > DateTime.MinValue) {
				_phoneCardRow.Date_last_used = pPhoneCard.DateLastUsed;
			}

			return _phoneCardRow;
		}

		internal static PhoneCardRow[] MapToPhoneCardRows(PhoneCardDto[] pPhoneCards) {
			var _list = new ArrayList();
			if (pPhoneCards != null) {
				foreach (PhoneCardDto _phoneCard in pPhoneCards) {
					PhoneCardRow _phoneCardRow = MapToPhoneCardRow(_phoneCard);
					_list.Add(_phoneCardRow);
				}
			}
			return (PhoneCardRow[]) _list.ToArray(typeof (PhoneCardRow));
		}

		internal static ResidentialPSTNRow MapToResidentialPSTNRow(ResidentialPSTNDto pResidentialPSTN) {
			var _residentialPSTNRow = new ResidentialPSTNRow
			                          	{
			                          		AccountStatus = pResidentialPSTN.Status, 
																		ANI = pResidentialPSTN.ANI, 
																		Service_id = pResidentialPSTN.ServiceId, 
																		Retail_acct_id = pResidentialPSTN.RetailAcctId
			                          	};
			if (pResidentialPSTN.DateFirstUsed < Configuration.Instance.Db.SqlSmallDateTimeMaxValue && pResidentialPSTN.DateFirstUsed > DateTime.MinValue) {
				_residentialPSTNRow.Date_first_used = pResidentialPSTN.DateFirstUsed;
			}
			if (pResidentialPSTN.DateLastUsed < Configuration.Instance.Db.SqlSmallDateTimeMaxValue && pResidentialPSTN.DateLastUsed > DateTime.MinValue) {
				_residentialPSTNRow.Date_last_used = pResidentialPSTN.DateLastUsed;
			}

			return _residentialPSTNRow;
		}

		internal static ResidentialPSTNRow[] MapToResidentialPSTNRows(ResidentialPSTNDto[] pResidentialPSTNs) {
			var _list = new ArrayList();
			if (pResidentialPSTNs != null) {
				foreach (var _residentialPSTN in pResidentialPSTNs) {
					var _residentialPSTNRow = MapToResidentialPSTNRow(_residentialPSTN);
					_list.Add(_residentialPSTNRow);
				}
			}
			return (ResidentialPSTNRow[]) _list.ToArray(typeof (ResidentialPSTNRow));
		}

		#endregion To DAL mappings

		#region To BLL mappings

		RetailAccountDto[] mapToRetailAccounts(IEnumerable<RetailAccountRow> pRetailAccountRows) {
			var _list = new ArrayList();
			if (pRetailAccountRows != null) {
				foreach (var _retailAccountRow in pRetailAccountRows) {
					var _retailAccount = mapToRetailAccount(_retailAccountRow);
					_list.Add(_retailAccount);
				}
			}
			return (RetailAccountDto[]) _list.ToArray(typeof (RetailAccountDto));
		}

		RetailAccountDto mapToRetailAccount(RetailAccountRow pRetailAccountRow) {
			var _retailAccount = new RetailAccountDto();
			_retailAccount.CurrentBalance = pRetailAccountRow.Current_balance;
			_retailAccount.CurrentBonusMinutes = pRetailAccountRow.Current_bonus_minutes;
			_retailAccount.CustomerAcctId = pRetailAccountRow.Customer_acct_id;
			_retailAccount.DateActive = pRetailAccountRow.Date_active;
			_retailAccount.DateCreated = pRetailAccountRow.Date_created;
			_retailAccount.DateExpired = pRetailAccountRow.Date_expired;
			_retailAccount.DateToExpire = pRetailAccountRow.Date_to_expire;
			_retailAccount.RetailAcctId = pRetailAccountRow.Retail_acct_id;
			_retailAccount.StartBalance = pRetailAccountRow.Start_balance;
			_retailAccount.StartBonusMinutes = pRetailAccountRow.Start_bonus_minutes;
			_retailAccount.Status = pRetailAccountRow.AccountStatus;

			return _retailAccount;
		}

		PhoneCardDto[] mapToPhoneCards(IEnumerable<PhoneCardRow> pPhoneCardRows) {
			var _list = new ArrayList();
			if (pPhoneCardRows != null) {
				foreach (PhoneCardRow _phoneCardRow in pPhoneCardRows) {
					PhoneCardDto _phoneCard = mapToPhoneCard(_phoneCardRow);
					_list.Add(_phoneCard);
				}
			}
			return (PhoneCardDto[]) _list.ToArray(typeof (PhoneCardDto));
		}

		PhoneCardDto mapToPhoneCard(PhoneCardRow pPhoneCardRow) {
			var _phoneCard = new PhoneCardDto();
			_phoneCard.Pin = pPhoneCardRow.Pin;
			_phoneCard.SerialNumber = pPhoneCardRow.Serial_number;
			_phoneCard.ServiceId = pPhoneCardRow.Service_id;
			_phoneCard.InventoryStatus = pPhoneCardRow.InventoryStatus;
			_phoneCard.Status = pPhoneCardRow.CardStatus;
			_phoneCard.RetailAcctId = pPhoneCardRow.Retail_acct_id;

			_phoneCard.DateLoaded = pPhoneCardRow.Date_loaded;
			_phoneCard.DateToExpire = pPhoneCardRow.Date_to_expire;

			if (pPhoneCardRow.IsDate_activeNull) {
				_phoneCard.DateActive = pPhoneCardRow.Date_active;
			}
			else {
				_phoneCard.DateActive = Configuration.Instance.Db.SqlSmallDateTimeMaxValue;
			}
			if (pPhoneCardRow.IsDate_deactivatedNull) {
				_phoneCard.DateDeactivated = pPhoneCardRow.Date_deactivated;
			}
			else {
				_phoneCard.DateDeactivated = Configuration.Instance.Db.SqlSmallDateTimeMaxValue;
			}
			if (pPhoneCardRow.IsDate_archivedNull) {
				_phoneCard.DateArchived = pPhoneCardRow.Date_archived;
			}
			else {
				_phoneCard.DateArchived = Configuration.Instance.Db.SqlSmallDateTimeMaxValue;
			}
			if (pPhoneCardRow.IsDate_first_usedNull) {
				_phoneCard.DateFirstUsed = pPhoneCardRow.Date_first_used;
			}
			else {
				_phoneCard.DateFirstUsed = Configuration.Instance.Db.SqlSmallDateTimeMaxValue;
			}
			if (pPhoneCardRow.IsDate_last_usedNull) {
				_phoneCard.DateLastUsed = pPhoneCardRow.Date_last_used;
			}
			else {
				_phoneCard.DateLastUsed = Configuration.Instance.Db.SqlSmallDateTimeMaxValue;
			}

			return _phoneCard;
		}

		ResidentialPSTNDto[] mapToResidentialPSTNs(ResidentialPSTNRow[] pResidentialPSTNRows) {
			var _list = new ArrayList();
			if (pResidentialPSTNRows != null) {
				foreach (ResidentialPSTNRow _residentialPSTNRow in pResidentialPSTNRows) {
					ResidentialPSTNDto _residentialPSTN = mapToResidentialPSTN(_residentialPSTNRow);
					_list.Add(_residentialPSTN);
				}
			}
			return (ResidentialPSTNDto[]) _list.ToArray(typeof (ResidentialPSTNDto));
		}

		ResidentialPSTNDto mapToResidentialPSTN(ResidentialPSTNRow pResidentialPSTNRow) {
			var _residentialPSTN = new ResidentialPSTNDto();
			_residentialPSTN.ANI = pResidentialPSTNRow.ANI;
			_residentialPSTN.ServiceId = pResidentialPSTNRow.Service_id;
			_residentialPSTN.Status = pResidentialPSTNRow.AccountStatus;
			_residentialPSTN.RetailAcctId = pResidentialPSTNRow.Retail_acct_id;
			if (pResidentialPSTNRow.IsDate_first_usedNull) {
				_residentialPSTN.DateFirstUsed = pResidentialPSTNRow.Date_first_used;
			}
			else {
				_residentialPSTN.DateFirstUsed = Configuration.Instance.Db.SqlSmallDateTimeMaxValue;
			}
			if (pResidentialPSTNRow.IsDate_last_usedNull) {
				_residentialPSTN.DateLastUsed = pResidentialPSTNRow.Date_last_used;
			}
			else {
				_residentialPSTN.DateLastUsed = Configuration.Instance.Db.SqlSmallDateTimeMaxValue;
			}

			return _residentialPSTN;
		}

		static RetailAccountPaymentRow mapToRetailAccountPaymentRow(RetailAccountPaymentDto pRetailAccountPayment) {
			var _retailAccountPaymentRow = new RetailAccountPaymentRow();
			_retailAccountPaymentRow.Date_time = pRetailAccountPayment.DateTime;
			_retailAccountPaymentRow.Retail_acct_id = pRetailAccountPayment.RetailAcctId;
			_retailAccountPaymentRow.Previous_amount = pRetailAccountPayment.PreviousAmount;
			_retailAccountPaymentRow.Payment_amount = pRetailAccountPayment.Amount;
			_retailAccountPaymentRow.Previous_bonus_minutes = pRetailAccountPayment.PreviousBonusMinutes;
			_retailAccountPaymentRow.Added_bonus_minutes = pRetailAccountPayment.AddedBonusMinutes;
			_retailAccountPaymentRow.Comments = pRetailAccountPayment.Comments;
			_retailAccountPaymentRow.Balance_adjustment_reason_id = pRetailAccountPayment.BalanceAdjustmentReasonId;
			_retailAccountPaymentRow.Person_id = pRetailAccountPayment.PersonId;
			_retailAccountPaymentRow.Cdr_key = pRetailAccountPayment.CdrKey;

			return _retailAccountPaymentRow;
		}

		static RetailAccountPaymentDto mapToRetailAccountPayment(RetailAccountPaymentRow pRetailAccountPaymentRow, PersonDto pPerson, BalanceAdjustmentReasonDto pBalanceAdjustmentReason) {
			var _retailAccountPayment = new RetailAccountPaymentDto();
			_retailAccountPayment.DateTime = pRetailAccountPaymentRow.Date_time;
			_retailAccountPayment.RetailAcctId = pRetailAccountPaymentRow.Retail_acct_id;
			_retailAccountPayment.PreviousAmount = pRetailAccountPaymentRow.Previous_amount;
			_retailAccountPayment.Amount = pRetailAccountPaymentRow.Payment_amount;
			_retailAccountPayment.PreviousBonusMinutes = pRetailAccountPaymentRow.Previous_bonus_minutes;
			_retailAccountPayment.AddedBonusMinutes = pRetailAccountPaymentRow.Added_bonus_minutes;
			_retailAccountPayment.Comments = pRetailAccountPaymentRow.Comments;
			_retailAccountPayment.BalanceAdjustmentReason = pBalanceAdjustmentReason;
			_retailAccountPayment.Person = pPerson;
			_retailAccountPayment.CdrKey = pRetailAccountPaymentRow.Cdr_key;

			return _retailAccountPayment;
		}

		#endregion To BLL mappings

		#endregion mappings

		#region privates

		//-------------------------------------------------- Privates -------------------------------------------------------------

		RetailAccountDto get(Rbr_Db pDb, RetailAccountRow pRetailAccountRow) {
			if (pRetailAccountRow != null) {
				CustomerAcctRow _customerAcctRow = CustomerAcctManager.Get(pDb, pRetailAccountRow.Customer_acct_id);
				return get(pDb, _customerAcctRow.Service_id, pRetailAccountRow);
			}
			return null;
		}

		RetailAccountDto get(Rbr_Db pDb, short pServiceId, RetailAccountRow pRetailAccountRow) {
			if (pRetailAccountRow != null) {
				ServiceRow _serviceRow = ServiceManager.Get(pDb, pServiceId);
				RetailAccountDto _retailAccount = mapToRetailAccount(pRetailAccountRow);
				_retailAccount.ServiceId = pServiceId;
				_retailAccount.RetailType = _serviceRow.RetailType;

				_retailAccount.Person = PersonManager.GetByRetailAcctId(pDb, pRetailAccountRow.Retail_acct_id);

				PhoneCardDto[] _phoneCards = mapToPhoneCards(pDb.PhoneCardCollection.GetByRetail_acct_id(_retailAccount.RetailAcctId));
				if (_phoneCards != null && _phoneCards.Length > 0) {
					_retailAccount.PhoneCards = _phoneCards;
					foreach (PhoneCardDto _phoneCard in _phoneCards) {
						if (_retailAccount.ServiceId != _phoneCard.ServiceId) {
							throw new Exception("Retail Account has a card from a different Service. [RetailAcctId=" + _retailAccount.RetailAcctId + "] [RetailAccount.ServiceId=" + _retailAccount.ServiceId + "] [PhoneCard.ServiceId=" + _phoneCard.ServiceId + "]");
						}
					}
				}

				ResidentialPSTNDto[] _residentialPSTNs = mapToResidentialPSTNs(pDb.ResidentialPSTNCollection.GetByRetail_acct_id(_retailAccount.RetailAcctId));
				if (_residentialPSTNs != null && _residentialPSTNs.Length > 0) {
					_retailAccount.ResidentialPSTNs = _residentialPSTNs;
					foreach (ResidentialPSTNDto _residentialPSTN in _residentialPSTNs) {
						if (_retailAccount.ServiceId != _residentialPSTN.ServiceId) {
							throw new Exception("Retail Account has a ResidentialPSTN from a different Service. [RetailAcctId=" + _retailAccount.RetailAcctId + "] [RetailAccount.ServiceId=" + _retailAccount.ServiceId + "] [ResidentialPSTN.ServiceId=" + _residentialPSTN.ServiceId + "]");
						}
					}
				}

				//ResidentialVoIP[] _residentialVoIPs = mapToResidentialVoIPs(pDb.ResidentialVoIPCollection.GetByRetail_acct_id(_retailAccount.RetailAcctId));
				//if (_residentialVoIPs != null && _residentialVoIPs.Length > 0) {
				//  _retailAccount.ResidentialVoIPs = _residentialVoIPs;
				//  foreach (ResidentialVoIP _residentialVoIP in _residentialVoIPs) {
				//    if (_retailAccount.ServiceId != _residentialVoIP.ServiceId) {
				//      throw new Exception("Retail Account has a ResidentialVoIP from a different Service. [RetailAcctId=" + _retailAccount.RetailAcctId + "] [RetailAccount.ServiceId=" + _retailAccount.ServiceId + "] [ResidentialVoIP.ServiceId=" + _residentialVoIP.ServiceId + "]");
				//    }
				//  }
				//}
				return _retailAccount;
			}
			return null;
		}

		public static PayphoneSurchargeDto MapToPayphoneSurcharge(PayphoneSurchargeRow pPayphoneSurchargeRow) {
			if (pPayphoneSurchargeRow == null) {
				return null;
			}
			var _payphoneSurcharge = new PayphoneSurchargeDto();
			_payphoneSurcharge.PayphoneSurchargeId = pPayphoneSurchargeRow.Payphone_surcharge_id;
			_payphoneSurcharge.Surcharge = pPayphoneSurchargeRow.Surcharge;
			_payphoneSurcharge.SurchargeType = pPayphoneSurchargeRow.SurchargeType;

			return _payphoneSurcharge;
		}

		public static PayphoneSurchargeRow MapToPayphoneSurchargeRow(PayphoneSurchargeDto pPayphoneSurcharge) {
			if (pPayphoneSurcharge == null) {
				return null;
			}
			var _payphoneSurchargeRow = new PayphoneSurchargeRow();
			_payphoneSurchargeRow.Payphone_surcharge_id = pPayphoneSurcharge.PayphoneSurchargeId;
			_payphoneSurchargeRow.Surcharge = pPayphoneSurcharge.Surcharge;
			_payphoneSurchargeRow.SurchargeType = pPayphoneSurcharge.SurchargeType;

			return _payphoneSurchargeRow;
		}

		#endregion privates
	}
}
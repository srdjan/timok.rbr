using System;
using Timok.Logger;
using Timok.Rbr.Core.Config;
using Timok.Rbr.DAL.RbrDatabase;
using Timok.Rbr.DOM;

namespace Timok.Rbr.BLL.DOM {
	public abstract class RetailAccount : IRetailAccount {
		const string RETAIL_ACCT_DEBIT_LABEL = "RetailAccount.Debit";
		readonly RetailAccountRow retailAcctRow;

		protected RetailAccount(RetailAccountRow pRetailAcctRow) {
			retailAcctRow = pRetailAcctRow;
		}

		public int Id { get { return retailAcctRow.Retail_acct_id; } }
		public short CustomerAcctId { get { return retailAcctRow.Customer_acct_id; } }
		public bool WithBonusMinutes { get { return retailAcctRow.Current_bonus_minutes > -1; } } //NOTE: Gui needs to support this rule
		public decimal StartingBalance { get { return retailAcctRow.Start_balance; } }
		public decimal CurrentBalance { get { return retailAcctRow.Current_balance; } }
		public short CurrentBonusBalance { get { return retailAcctRow.Current_bonus_minutes; } }
		public bool IsPrepaid { get { return true; } }
		public DateTime DateToExpire { get { return retailAcctRow.Date_to_expire; } }

		public abstract short ServiceId { get; }
		public abstract long SerialNumber { get; }

		public Status AcctStatus {
			get { return (Status) retailAcctRow.Status; }
			set {
				using (var _db = new Rbr_Db()) {
					_db.RetailAccountCollection.UpdateStatus(retailAcctRow.Retail_acct_id, value);
				}
				retailAcctRow.Status = (byte) value;
			}
		}

		public bool NeverUsed { get { return retailAcctRow.Current_balance == retailAcctRow.Start_balance; } }

		//-------------------------------------  Ctor  --------------------------------------

		//------------------------------------- Public methods -----------------------------
		public abstract void UpdateUsage();

		public int Rate(CustomerRoute pCustomerRoute, ref Cdr pCdr) {
			short _roundedSeconds = 0;

			try {
				pCdr.RetailDuration = 0;
				if (pCdr.Duration > 0) {
					pCdr.RetailDuration = (short) ((pCdr.Duration / 6) * 6);
					pCdr.RetailDuration += (short) (pCdr.Duration % 6 > 0 ? 6 : 0);
				}

				if (pCustomerRoute != null && pCustomerRoute.WithBonusMinutes && WithBonusMinutes && CurrentBonusBalance > 0) {
					pCdr.RetailRoundedMinutes = (short) (pCdr.Duration / 60 + (pCdr.Duration % 60 > 0 ? 1 : 0));
					if (CurrentBonusBalance < pCdr.RetailRoundedMinutes) {
						TimokLogger.Instance.LogRbr(LogSeverity.Debug, "RetailAccount.Rate", string.Format("All bonus minutes used: {0} | {1}", CurrentBonusBalance, pCdr.RetailRoundedMinutes));
						var _overflowDuration = (pCdr.RetailRoundedMinutes - CurrentBonusBalance) * 60; //in seconds
						pCdr.RetailPrice = pCustomerRoute.GetWholesaleCost(pCdr.StartTime, _overflowDuration, out _roundedSeconds);
					}
					else {
						//TODO: should substract bonus minutes used from duration and then chargae balance for the remianing time used!!!
						//NOTE: this is NOT correct	
						_roundedSeconds = (short) (pCdr.RetailRoundedMinutes * 60);
						pCdr.UsedBonusMinutes = pCdr.RetailRoundedMinutes;
					}
				}
				else {
					var _service = RetailService.Get(pCdr.DNIS.ToString());
					if (_service == null) {
						TimokLogger.Instance.LogRbr(LogSeverity.Error, "RetailAccount.Rate", string.Format("Service NOT found, AccessNumber: {0}", pCdr.DNIS));
						return 1;
					}

					var _payphoneSurcharge = SurchargeInfo.Empty;
					if (pCdr.InfoDigits > 0) {
						if (_service.PayphoneSurchargeInfo != null) {
							_payphoneSurcharge = _service.PayphoneSurchargeInfo;
						}
						else {
							TimokLogger.Instance.LogRbr(LogSeverity.Error, "RetailAccount.Rate", "PayphoneSurcharge required, but NOT defined");
						}
					}

					if (pCustomerRoute != null) {
						pCdr.RetailPrice = pCustomerRoute.GetRetailCost(pCdr.StartTime, pCdr.Duration, _service.AccessNumberSurchargeInfo, _payphoneSurcharge, out _roundedSeconds);
					}
					else {
						//- Apply surcharges only!
						if (_service.AccessNumberSurchargeInfo != null) {
							pCdr.RetailPrice += _service.AccessNumberSurchargeInfo.Cost;
						}
						if (_payphoneSurcharge != null) {
							pCdr.RetailPrice += _payphoneSurcharge.Cost;
						}
					}
				}

				if (pCdr.RetailPrice < decimal.Zero) {
					TimokLogger.Instance.LogRbr(LogSeverity.Critical, "RetailAccount.Rate", string.Format("Price < decimal.Zero: {0} | 0.0", pCdr.RetailPrice));
					return 1;
				}
				if (_roundedSeconds > short.MaxValue) {
					TimokLogger.Instance.LogRbr(LogSeverity.Critical, "RetailAccount.Rate", "Rounded Seconds greater then short.MaxValue !");
					return 1;
				}
				pCdr.RetailDuration = _roundedSeconds;
				pCdr.RetailRoundedMinutes = (short) (_roundedSeconds / 60);
				pCdr.UsedBonusMinutes = CurrentBonusBalance;
			}
			catch (Exception _ex) {
				TimokLogger.Instance.LogRbr(LogSeverity.Critical, "RetailAccount.Rate", string.Format("Exception:\r\n{0}", _ex));
				return 1;
			}
			return 0;
		}

		public void Debit(decimal pAmount, short pBonusMinutes) {
			if (retailAcctRow.Current_balance < pAmount) {
				TimokLogger.Instance.LogRbr(LogSeverity.Critical,
				                            RETAIL_ACCT_DEBIT_LABEL,
				                            string.Format("Call Cost={0}, greater then available balance={1}, Serial#={2}", pAmount, retailAcctRow.Current_balance, SerialNumber));
				pAmount = retailAcctRow.Current_balance;
			}
			if (pBonusMinutes > 0 && retailAcctRow.Current_bonus_minutes < pBonusMinutes) {
				TimokLogger.Instance.LogRbr(LogSeverity.Error,
				                            RETAIL_ACCT_DEBIT_LABEL,
				                            string.Format("Call Cost={0}, greater then available balance={1}, Serial#={2}", pAmount, retailAcctRow.Current_balance, SerialNumber));
				pBonusMinutes = retailAcctRow.Current_bonus_minutes;
			}

			using (var _db = new Rbr_Db()) {
				if (! _db.RetailAccountCollection.Debit(retailAcctRow.Retail_acct_id, pAmount, pBonusMinutes)) {
					throw new Exception(string.Format("RetailAccount.Debit, IncrementUsage, CustomerAcctId={0}, Serial#={1}, Price={2}, BonusMinutes={3}", CustomerAcctId, SerialNumber, pAmount, pBonusMinutes));
				}
				TimokLogger.Instance.LogRbr(LogSeverity.Debug,
				                            RETAIL_ACCT_DEBIT_LABEL,
				                            string.Format("CustomerAcctId={0}, Serial#={1}, Price={2}, BonusMinutes={3}, Balance={4}", CustomerAcctId, SerialNumber, pAmount, pBonusMinutes, CurrentBalance));
			}
			retailAcctRow.Current_balance -= pAmount;
			retailAcctRow.Current_bonus_minutes -= pBonusMinutes;
		}

		//--------------- Static factories -------------------------------------------------------------
		public static IRetailAccount Get(CustomerAcct pCustomerAcct, Cdr pCdr, IConfiguration pConfiguration, ILogger pLogger) {
			if (pCustomerAcct == null) {
				TimokLogger.Instance.LogRbr(LogSeverity.Error, "RetailAccount.Get", "CustomerAcct NOT Found");
				return null;
			}
			if (!pCustomerAcct.IsRetail) {
				TimokLogger.Instance.LogRbr(LogSeverity.Status, "RetailAccount.Get", string.Format("CustomerAcct NOT Retail: {0}", pCustomerAcct.Id));
				return null;
			}

			IRetailAccount _retailAcct = null;
			try {
				if (pCustomerAcct.RetailType == RetailType.PhoneCard) {
					_retailAcct = getPhoneCardBySerialNumber(pCustomerAcct.ServiceId, pCdr.SerialNumber, pConfiguration, pLogger);
				}
				else if (pCustomerAcct.RetailType == RetailType.Residential) {
					_retailAcct = getResidential(pCustomerAcct.ServiceId, pCdr.ANI);
				}
				else {
					TimokLogger.Instance.LogRbr(LogSeverity.Critical, "RetailAccount.Get", string.Format("Unknown Retailtype: CustomerAcct: {0} SerialNumber: {1}", pCustomerAcct.Id, pCdr.SerialNumber));
				}

				if (_retailAcct == null) {
					TimokLogger.Instance.LogRbr(LogSeverity.Error, "RetailAccount.Get", string.Format("RetailAcct NOT FOUND: CustomerAcct: {0} SerialNumber: {1}", pCustomerAcct.Id, pCdr.SerialNumber));
					return null;
				}
			}
			catch (Exception _ex) {
				TimokLogger.Instance.LogRbr(LogSeverity.Critical, "RetailAccount.Get", string.Format("Exception:\r\n{0}", _ex));
				return null;
			}
			return _retailAcct;
		}

		public static IRetailAccount GetResidential(string pANI, short pServiceId) {
			long _ani;
			long.TryParse(pANI, out _ani);
			if (_ani == 0) {
				TimokLogger.Instance.LogRbr(LogSeverity.Error, "RetailAccount.GetResidential", string.Format("Invalid ANI={0}", pANI));
				return null;
			}
			return getResidential(pServiceId, _ani);
		}

		public static IRetailAccount GetPhoneCard(string pCardNumber, short pServiceId) {
			long _cardNumber;
			long.TryParse(pCardNumber, out _cardNumber);
			if (_cardNumber == 0) {
				TimokLogger.Instance.LogRbr(LogSeverity.Error, "RetailAccount.GetPhoneCard", string.Format("Invalid Card Number={0}", pCardNumber));
				return null;
			}
			return getPhoneCardByCardNumber(pServiceId, _cardNumber);
		}

		//--------------------- Private static -----------------------------------------------
		static IRetailAccount getResidential(short pServiceId, long pANI) {
			try {
				ResidentialPSTNRow _residentialRow;
				using (var _db = new Rbr_Db()) {
					_residentialRow = _db.ResidentialPSTNCollection.GetByPrimaryKey(pServiceId, pANI);
				}
				if (_residentialRow == null) {
					TimokLogger.Instance.LogRbr(LogSeverity.Error, "RetailAccount.getResidential", string.Format("ResidentialRow == null, {0},{1}", pServiceId, pANI));
					return null;
				}

				//-- Get Get parent retail Account
				var _retailAcctRow = getRetailAcct(_residentialRow.Retail_acct_id);
				if (_retailAcctRow == null) {
					TimokLogger.Instance.LogRbr(LogSeverity.Error, "RetailAccount.getResidential", string.Format("RetailAcctRow == null, {0}", _residentialRow.Retail_acct_id));
					return null;
				}
				return new Residential(_retailAcctRow, _residentialRow);
			}
			catch (Exception _ex) {
				TimokLogger.Instance.LogRbr(LogSeverity.Critical, "RetailAccount.getResidential", string.Format("Exception: {0}", _ex));
			}
			return null;
		}

		static IRetailAccount getPhoneCardBySerialNumber(short pServiceId, long pSerialNumber, IConfiguration pConfiguration, ILogger pLogger) {
			try {
				PhoneCardRow _phoneCardRow;
				using (var _db = new Rbr_Db()) {
					_phoneCardRow = _db.PhoneCardCollection.GetBySerialNumber(pServiceId, pSerialNumber);
				}
				if (_phoneCardRow == null) {
					TimokLogger.Instance.LogRbr(LogSeverity.Error, "RetailAccount.getPhoneCard", string.Format("Phone Card NOT FOUND: [{0}]", pSerialNumber));
					return null;
				}
				var _retailAcctRow = getRetailAcct(_phoneCardRow.Retail_acct_id);
				if (_retailAcctRow == null) {
					TimokLogger.Instance.LogRbr(LogSeverity.Error, "RetailAccount.getPhoneCard", string.Format("RetailAcct NOT FOUND: [{0}]", pSerialNumber));
					return null;
				}
				return new PhoneCard(_retailAcctRow, _phoneCardRow);
			}
			catch (Exception _ex) {
				TimokLogger.Instance.LogRbr(LogSeverity.Critical, "RetailAccount.getPhoneCard", string.Format("Exception\r\n{0}", _ex));
			}
			return null;
		}

		static IRetailAccount getPhoneCardByCardNumber(short pServiceId, long pCardnumber) {
			try {
				PhoneCardRow _phoneCardRow;
				using (var _db = new Rbr_Db()) {
					_phoneCardRow = _db.PhoneCardCollection.GetByPrimaryKey(pServiceId, pCardnumber);
				}
				if (_phoneCardRow == null) {
					TimokLogger.Instance.LogRbr(LogSeverity.Error, "RetailAccount.getPhoneCard", string.Format("Phone Card NOT FOUND: {0},{1}", pServiceId, pCardnumber));
					return null;
				}
				var _retailAcctRow = getRetailAcct(_phoneCardRow.Retail_acct_id);
				if (_retailAcctRow == null) {
					TimokLogger.Instance.LogRbr(LogSeverity.Error, "RetailAccount.getPhoneCard", string.Format("RetailAcct NOT FOUND: {0},{1}", pServiceId, pCardnumber));
					return null;
				}
				return new PhoneCard(_retailAcctRow, _phoneCardRow);
			}
			catch (Exception _ex) {
				TimokLogger.Instance.LogRbr(LogSeverity.Critical, "RetailAccount.getPhoneCard1", string.Format("Exception:\r\n{0}", _ex));
			}
			return null;
		}

		static RetailAccountRow getRetailAcct(int pRetailAcctId) {
			RetailAccountRow _retailAcctRow;
			using (var _db = new Rbr_Db()) {
				_retailAcctRow = _db.RetailAccountCollection.GetByPrimaryKey(pRetailAcctId);
			}
			if (_retailAcctRow == null) {
				TimokLogger.Instance.LogRbr(LogSeverity.Critical, "RetailAccount.getRetailAcct", string.Format("RetailAcct NOT FOUND: {0}", pRetailAcctId));
			}
			return _retailAcctRow;
		}
	}
}
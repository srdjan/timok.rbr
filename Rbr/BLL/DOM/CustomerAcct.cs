using System;
using System.Collections.Generic;
using Timok.Logger;
using Timok.Rbr.Core;
using Timok.Rbr.Core.Config;
using Timok.Rbr.DAL.RbrDatabase;

namespace Timok.Rbr.BLL.DOM {
	public sealed class CustomerAcct {
		readonly CustomerAcctRow customerAcctRow;
		readonly PartnerRow partnerRow;

		public short Id { get { return customerAcctRow.Customer_acct_id; } }
		public string Name { get { return customerAcctRow.Name; } }
		public string PartnerName { get { return partnerRow.Name; } }

		public short MaxCallLength { get { return customerAcctRow.Max_call_length; } }
		public int MaxNumberOfCalls { get; set; }

		public static Dictionary<int, int> NumberOfCallsCounter { get; private set; }

		readonly ServiceType serviceType;
		public bool IsWholesale { get { return serviceType == ServiceType.Wholesale; } }
		public bool IsRetail { get { return serviceType == ServiceType.Retail; } }

		public short ServiceId { get { return customerAcctRow.Service_id; } }

		readonly int callingPlanId;
		public int CallingPlanId { get { return callingPlanId; } }

		public int RoutingPlanId { get { return customerAcctRow.Routing_plan_id; } }

		readonly RetailType retailType;
		public RetailType RetailType { get { return retailType; } }

		public decimal Balance { get { return customerAcctRow.Current_amount; } }

		public bool IsBalanceLimitReached {
			get {
				if (IsWholesale && IsPrepaid && customerAcctRow.Current_amount <= decimal.Zero) {
					return true;
				}
				return false;
			}
		}

		public bool IsBalanceWarningLimitReached {
			get {
				if (IsWholesale && IsPrepaid && customerAcctRow.Current_amount < customerAcctRow.Warning_amount) {
					return true;
				}
				return false;
			}
		}

		bool firstWarningMessageSent;
		public bool ShouldSendWarningMessage {
			get {
				if (!firstWarningMessageSent) {
					firstWarningMessageSent = true;
					return true;
				}
				if (DateTime.Now.Minute == 1 && ( DateTime.Now.Hour % Configuration.Instance.Main.BalanceWarningFrequency ) == 0) {
					return true;
				}
				return false;
			}
		}

		bool firstLimitMessageSent;
		public bool ShouldSendLimitMessage {
			get {
				if (!firstLimitMessageSent) {
					firstLimitMessageSent = true;
					return true;
				}
				if (DateTime.Now.Minute == 1 && ( DateTime.Now.Hour % Configuration.Instance.Main.BalanceWarningFrequency ) == 0) {
					return true;
				}
				return false;
			}
		}

		Status status;
		public Status Status {
			get { return status; }
			set {
				using (var _db = new Rbr_Db()) {
					_db.CustomerAcctCollection.UpdateStatus(Id, (byte) value);
				}
				status = value;
			}
		}

		readonly bool isRatingEnabled;
		public bool IsRatingEnabled { get { return isRatingEnabled; } }

		readonly bool isPrepaid;
		public bool IsPrepaid { get { return isPrepaid; } }

		public bool WithBonusMinutes { get { return customerAcctRow.BonusMinutesEnabled; } }
		public string PrefixIn { get { return customerAcctRow.Prefix_in; } }
		public string PrefixOut { get { return customerAcctRow.Prefix_out; } }

		//TODO: get it from Db all the time, or invalidate Custumers (in imdb) on Partner's email change
		readonly string email;
		public string Email { get { return email; } }

		static CustomerAcct() {
			NumberOfCallsCounter = new Dictionary<int, int>();
		}

		public CustomerAcct(CustomerAcctRow pCustomerAcctRow) {
			customerAcctRow = pCustomerAcctRow;
			status = (Status) customerAcctRow.Status;
			email = string.Empty;

			//--
			using (var _db = new Rbr_Db()) {
				partnerRow = _db.PartnerCollection.GetByPrimaryKey(customerAcctRow.Partner_id);
			}
			if (partnerRow == null) {
				throw new Exception("Couldn't FIND PartnerRow for Customer: " + customerAcctRow.Customer_acct_id);
			}

			//--
			ServiceRow _serviceRow;
			using (var _db = new Rbr_Db()) {
				_serviceRow = _db.ServiceCollection.GetByPrimaryKey(customerAcctRow.Service_id);
			}
			if (_serviceRow == null) {
				throw new Exception("Couldn't FIND ServiceRow for Customer: " + customerAcctRow.Customer_acct_id);
			}
			serviceType = _serviceRow.ServiceType;
			retailType = _serviceRow.RetailType;
			isPrepaid = customerAcctRow.IsPrepaid;
			isRatingEnabled = _serviceRow.IsRatingEnabled;
			callingPlanId = _serviceRow.Calling_plan_id;

			//TODO: set max number of calls from db
			MaxNumberOfCalls = 1000;

			if (isPrepaid) {	//get email, so we can send balance warnings to Partners as well
				using (var _db = new Rbr_Db()) {
					var _partnerRow = _db.PartnerCollection.GetByPrimaryKey(customerAcctRow.Partner_id);
					if (_partnerRow == null) {
						throw new Exception("Couldn't FIND PartnerRow for Customer: " + customerAcctRow.Customer_acct_id);
					}
					var _contactInfoRow = _db.ContactInfoCollection.GetByPrimaryKey(_partnerRow.Contact_info_id);
					if (_contactInfoRow != null) {
						email = _contactInfoRow.Email;
					}
				}
			}
		}

		//--------------------------------------- Static methods ---------------------------------------------
		public static CustomerAcct[] GetAllWithBalanceWarning() {
			var _customerAccts = new List<CustomerAcct>();
			CustomerAcctRow[] _customerAcctRows;
			try {
				using (var _db = new Rbr_Db()) {
					_customerAcctRows = _db.CustomerAcctCollection.GetAll();
				}
			}
			catch (Exception _ex) {
				throw new Exception(string.Format("Getting All CustomerAccts, Exception:\r\n{0}", _ex));
			}

			foreach (var _customerAcctRow in _customerAcctRows) {
				var _customerAcct = new CustomerAcct(_customerAcctRow);
				if (_customerAcct.IsBalanceWarningLimitReached) {
					_customerAccts.Add(_customerAcct);
				}
			}
			return _customerAccts.ToArray();
		}

		public static CustomerAcct Get(short pCustomerId) {
			CustomerAcct _customerAcct = null;

			using (var _db = new Rbr_Db()) {
				var _customerAcctRow = _db.CustomerAcctCollection.GetByPrimaryKey(pCustomerId);
				if (_customerAcctRow != null) {
					_customerAcct = new CustomerAcct(_customerAcctRow);
				}
			}

			if (_customerAcct == null) {
				throw new RbrException(RbrResult.Customer_NotFound, "CustomerAcct.Get:", string.Format("CustomerAcctId={0}", pCustomerId));
			}
			return _customerAcct;
		}

		public static CustomerAcct Get(long pAccessNumber) {
			AccessNumberListRow _accessNumber;
			using (var _db = new Rbr_Db()) {
				_accessNumber = _db.AccessNumberListCollection.GetByPrimaryKey(pAccessNumber);
			}
			if (_accessNumber != null) {
				return Get(_accessNumber.Customer_acct_id);
			}
			throw new RbrException(RbrResult.Customer_NotFound, "CustomerAcct.Get:", string.Format("AccessNumber={0}", pAccessNumber));
		}

		public static CustomerAcct Get(Endpoint pOrigEP, string pPrefixIn) {
			var _dialPeer = CustomerDialPeer.Get(pOrigEP.Id, pPrefixIn);
			if (_dialPeer == null) {
				return null;
			}

			var _customerAcct = Get(_dialPeer.CustomerId);
			return _customerAcct;
		}

		public static CustomerAcct[] GetRetailAccounts() {
			var _customerAccts = new List<CustomerAcct>();

			using (var _db = new Rbr_Db()) {
				var _customerAcctRows = _db.CustomerAcctCollection.GetByRetailType(new RetailType[1] {RetailType.PhoneCard});
				if (_customerAcctRows != null) {
					foreach (var _customerAcctsRow in _customerAcctRows) {
						_customerAccts.Add(new CustomerAcct(_customerAcctsRow));
					}
				}
			}
			return _customerAccts.ToArray();
		}


		public static short GetCustomerAcctId(string pAccessNumber) {
			try {
				var _accessNumber = new AccessNumber(pAccessNumber);
				return Get(_accessNumber.Number).Id; 
			}
			catch (Exception _ex) {
				throw new RbrException(RbrResult.DNIS_Invalid, "CustomerAcct.GetCustomerAcctId", string.Format("Exception={0}", _ex));
			}
		}

		//----------------------------------------- Public Instance Methods ------------------------------------
		public int Authorize(CustomerRoute pCustomerRoute) {
			if (Status != Status.Active) {
				throw new RbrException(RbrResult.Customer_NotActive, "CustomerAcct.Authorize", string.Format("CustomerAcctId={0}", Id));
			}

			if (NumberOfCallsCounter.ContainsKey(Id)) {
				if (NumberOfCallsCounter[Id] >= MaxNumberOfCalls) {
					throw new RbrException(RbrResult.Customer_LimitReached, "CustomerAcct.Authorize", string.Format("CustomerAcctId={0}", Id));
				}
			}

			if (! IsPrepaid) {
				return MaxCallLength;
			}

			//-- Check prepaid conditions
			if (IsBalanceLimitReached) {
				throw new RbrException(RbrResult.Customer_BalanceInvalid, "CustomerAcct.Authorize", string.Format("Customer Balance INVALID, CustomerAcctId={0}", Id));
			}
			if (IsBalanceWarningLimitReached) {
				TimokLogger.Instance.LogRbr(LogSeverity.Error, "CustomerAcct.Authorize", string.Format("Customer Warning REACHED, CustomerAcctId={0}", Id));
			}

			if (pCustomerRoute.Status != Status.Active) {
				throw new RbrException(RbrResult.Customer_RouteBlocked, "CustomerAcct.Authorize", string.Format("CustomerAcctId={0}, CustomerRouteId={1}", Id, pCustomerRoute.WholesaleRouteId));
			}

			int _timeLimit = pCustomerRoute.GetTimeLimit(Balance);
			if (_timeLimit <= 0) {
				throw new RbrException(RbrResult.Customer_LimitReached, "CustomerAcct.Authorize", string.Format("Customer Limit REACHED, CustomerAcct={0}", Id));
			}

			if (_timeLimit > MaxCallLength) {
				_timeLimit = MaxCallLength;
				TimokLogger.Instance.LogRbr(LogSeverity.Status, "CustomerAcct.Authorize", string.Format("Max Call Time Limit REACHED, CustomerAcct={0}, Calculated TimeLimit={1}", Id, _timeLimit));
			}
			return _timeLimit;
		}

		public int RateCall(CustomerRoute pCustomerRoute, ref Cdr pCdr) {
			int _result = 0;

			pCdr.CustomerDuration = (short) ((pCdr.Duration / 6) * 6);
			pCdr.CustomerDuration += (short) (pCdr.Duration % 6 > 0 ? 6 : 0);

			if (IsRatingEnabled && pCustomerRoute != null) {
				if (IsWholesale) {
					try {
						short _roundedSeconds;
						pCdr.CustomerPrice = pCustomerRoute.GetWholesaleCost(pCdr.StartTime, pCdr.Duration, out _roundedSeconds);
						if (_roundedSeconds > short.MaxValue) {
							TimokLogger.Instance.LogRbr(LogSeverity.Critical, "CustomerAcct.RateCall", "Rounded Seconds greater then short.MaxValue");
						}
						pCdr.CustomerDuration = _roundedSeconds;
						pCdr.CustomerRoundedMinutes = (short) (_roundedSeconds / 60);
					}
					catch (Exception _ex) {
						_result = 1;
						TimokLogger.Instance.LogRbr(LogSeverity.Error, "CustomerAcct.RateCall", string.Format("Finding Wholesale price, Exception:\r\n{0}", _ex));
					}
				}
				else if (IsRetail) {
					//-- Add retail part, to supress errors, retail rated separately
				}
				else {
					_result = 1;
					TimokLogger.Instance.LogRbr(LogSeverity.Critical, "CustomerAcct.RateCall", string.Format("Unknown CustomerType, {0}", serviceType));
				}
			}
			return _result;
		}

		//TODO: use Transaction
		public void Debit(decimal pAmount) {
			using (var _db = new Rbr_Db()) {
				var _result = _db.CustomerAcctCollection.Debit(customerAcctRow.Customer_acct_id, pAmount);
				if (! _result) {
					throw new Exception("CustomerAcct.Debit | IncrementUsage exception: [customerAcctId: " + customerAcctRow.Customer_acct_id + "][amount: " + pAmount + "]");
				}
			}
			//-- update in imdb also:
			customerAcctRow.Current_amount -= pAmount;
		}

		public void GetPartnerInfo(out string pPartnerName, out string pEmail) {
			pPartnerName = string.Empty;
			pEmail = string.Empty;
			if (isPrepaid) {
				//get partner's name and email, so we can send balance warnings to Partners as well
				//TODO: do it real-time for now, untill we have Customer(s) imdb invalidation on Partner change 
				using (var _db = new Rbr_Db()) {
					var _partnerRow = _db.PartnerCollection.GetByPrimaryKey(customerAcctRow.Partner_id);
					if (_partnerRow == null) {
						throw new Exception("Couldn't FIND PartnerRow for Customer: " + customerAcctRow.Customer_acct_id);
					}
					pPartnerName = _partnerRow.Name;
					var _contactInfoRow = _db.ContactInfoCollection.GetByPrimaryKey(_partnerRow.Contact_info_id);
					if (_contactInfoRow != null) {
						pEmail = _contactInfoRow.Email;
					}
				}
			}
		}

		public void SaveInventoryStats(DateTime pDateTime) {
			int _totalUsed, _firstUsed, _depleted, _expired;
			getInventoryStats(pDateTime, out _totalUsed, out _firstUsed, out _depleted, out _expired);
			saveInvetoryStats(pDateTime, _totalUsed, _firstUsed, _depleted, _expired);
		}

		//------------------------------------------- Private --------------------------------------------------------
		void getInventoryStats(DateTime pDateTime, out int pTotalUsed, out int pFirstUsed, out int pDepleted, out int pExpired) {
			using (var _db = new Rbr_Db()) {
				pTotalUsed = _db.PhoneCardCollection.GetCountByTotalUsed(pDateTime);
				pDepleted = _db.PhoneCardCollection.GetCountByDepleted(pDateTime);
				pFirstUsed = _db.PhoneCardCollection.GetCountByFirstUsed(pDateTime);
				pExpired = _db.PhoneCardCollection.GetCountByExpired(pDateTime);
			}
		}

		void saveInvetoryStats(DateTime pDateTime, int pTotalUsed, int pFirstUsed, int pDepleted, int pExpired) {
			using (var _db = new Rbr_Db()) {
				var _inventoryUsageRow = new InventoryUsageRow {Service_id = ServiceId, Customer_acct_id = Id, Timestamp = pDateTime, Total_used = pTotalUsed, First_used = pFirstUsed, Balance_depleted = pDepleted, Expired = pExpired};
				_db.InventoryUsageCollection.Insert(_inventoryUsageRow);
			}
		}
	}
}
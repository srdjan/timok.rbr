using System;
using Timok.Logger;
using Timok.Rbr.Core;
using Timok.Rbr.Core.Config;
using Timok.Rbr.DAL.RbrDatabase;
using Timok.Rbr.DOM;

namespace Timok.Rbr.BLL.DOM {
	public sealed class RetailService {
		const string GET_RETAIL_ACCOUNT_LABEL = "BillingService.GET_RETAIL_ACCOUNT_LABEL:";
		readonly AccessNumber accessNumber;
		readonly ServiceRow serviceRow;

		public AccessNumber AccessNumber { get { return accessNumber; } }
		public SurchargeInfo AccessNumberSurchargeInfo { get { return accessNumber.SurchargeInfo; } }
		public string Name { get { return serviceRow.Name; } }
		public int PinLength { get { return serviceRow.Pin_length; } }

		short serviceId { get { return serviceRow.Service_id; } }
		RetailType retailType { get { return serviceRow.RetailType; } }

		public ServiceType ServiceType { get { return serviceRow.ServiceType; } }
		public bool IsRatingEnabled { get { return serviceRow.IsRatingEnabled; } }
		public Status Status { get { return (Status)serviceRow.Status; } }
		public BalancePromptType BalancePromptType { get { return serviceRow.BalancePromptType; } }
		public decimal BalancePromptPerUnit { get { return serviceRow.Balance_prompt_per_unit; } }

		public SurchargeInfo PayphoneSurchargeInfo { get; set; }

		public RetailService(long pAccessNumber) {
			AccessNumberListRow _accessNumberRow;
			using (var _db = new Rbr_Db()) {
				_accessNumberRow = _db.AccessNumberListCollection.GetByPrimaryKey(pAccessNumber);
			}
			if (_accessNumberRow == null) {
				throw new Exception(string.Format("Service.Ctor: AccessNumber NOT FOUND, AccessNumber={0}", pAccessNumber));
			}
			accessNumber = new AccessNumber(_accessNumberRow);

			using (var _db = new Rbr_Db()) {
				serviceRow = _db.ServiceCollection.GetByPrimaryKey(_accessNumberRow.Service_id);
			}
			if (serviceRow == null) {
				throw new Exception(string.Format("Service.Ctor: Service NOT FOUND, ServiceID={0}", _accessNumberRow.Service_id));
			}
			
			//-- init
			PayphoneSurchargeInfo = SurchargeInfo.Empty;
			if (serviceRow.Payphone_surcharge_id > 0) {
				using (var _db = new Rbr_Db()) {
					var _payphoneSurchargeRow = _db.PayphoneSurchargeCollection.GetByPrimaryKey(serviceRow.Payphone_surcharge_id);
					if (_payphoneSurchargeRow != null) {
						PayphoneSurchargeInfo = new SurchargeInfo(_payphoneSurchargeRow.Surcharge, _payphoneSurchargeRow.SurchargeType);
					}
				}
			}
		}

		//--------------------- Static -----------------------------------------------------------
		public static RetailService Get(string pAccessNumber) {
			try {
				var _accessNumber = new AccessNumber(pAccessNumber);
				return new RetailService(_accessNumber.Number);
			}
			catch (Exception _ex) {
				TimokLogger.Instance.LogRbr(LogSeverity.Error, "RetailService.Get", string.Format("Exception: {0}", _ex));
			}
			return null;
		}
		//--------------------- End-Static -------------------------------------------------------
		
		public void Authorize(ISession pSession, IRetailAccount pRetailAcct, CustomerRoute pCustomerRoute, out int pTimeLimit, out int pPromptTimeLimit) {
			pTimeLimit = 0;

			//--Check for Domestic Bonus minutes:
			if (pRetailAcct.WithBonusMinutes && pCustomerRoute.WithBonusMinutes && pRetailAcct.CurrentBonusBalance > 0) {
				pTimeLimit = pRetailAcct.CurrentBonusBalance * 60;
			}

			//-- Check money based balance and add to the TimeLimit if balance > 0:
			if (pRetailAcct.CurrentBalance > decimal.Zero) {
				var _accessNumberSurcharge = AccessNumberSurchargeInfo;
				var _payphoneSurcharge = SurchargeInfo.Empty;
				if (pSession.InfoDigits > 0) {
					_payphoneSurcharge = PayphoneSurchargeInfo;
				}

				pTimeLimit += pCustomerRoute.GetTimeLimit(pRetailAcct.CurrentBalance, _accessNumberSurcharge, _payphoneSurcharge);
			}

			if (pTimeLimit < 0) {
				pTimeLimit = 0;
				TimokLogger.Instance.LogRbr(LogSeverity.Critical, "BillingService.Authorize:", string.Format("TimeLimit less then Zero! Serial={0}, CustomerId={1} TimeLimit={2}", pRetailAcct.SerialNumber, pRetailAcct.CustomerAcctId, pTimeLimit));
			}

			//-- propmpt multiplier
			if (pCustomerRoute.Multiplier > 0) {
				pPromptTimeLimit = (pTimeLimit * pCustomerRoute.Multiplier) / 100;
			}
			else {
				pPromptTimeLimit = pTimeLimit;
			}
		}

		public IRetailAccount GetRetailAccount(ISession pSession) {
			IRetailAccount _retailAcct;
			if (retailType == RetailType.PhoneCard) {
				_retailAcct = RetailAccount.GetPhoneCard(pSession.CardNumber, serviceId);
			}
			else if (retailType == RetailType.Residential) {
				_retailAcct = RetailAccount.GetResidential(pSession.ANI, serviceId);
			}
			else {
				throw new RbrException(RbrResult.Retail_UnknownType, GET_RETAIL_ACCOUNT_LABEL, string.Format("AccessNumber={0}, serviceId={1}", pSession.AccessNumber, serviceId));
			}

			if (_retailAcct == null) {
				throw new RbrException(RbrResult.Retail_AcctNotFound, GET_RETAIL_ACCOUNT_LABEL, string.Format("ANI={0}, CardNumber={1}, AccessNumber={2}", pSession.ANI, pSession.CardNumber, pSession.AccessNumber));
			}

			if (_retailAcct.CurrentBonusBalance < 0) {
				TimokLogger.Instance.LogRbr(LogSeverity.Critical, GET_RETAIL_ACCOUNT_LABEL, string.Format("BonusMinutes less Then zero! ANI={0}, AccessNumber={1}, Card_number={2}", pSession.ANI, pSession.AccessNumber, pSession.CardNumber));
			}
			if (_retailAcct.CurrentBalance < decimal.Zero) {
				TimokLogger.Instance.LogRbr(LogSeverity.Critical, GET_RETAIL_ACCOUNT_LABEL, string.Format("Balance less Zero! ANI={0}, AccessNumber={1}, Card_number={2}", pSession.ANI, pSession.AccessNumber, pSession.CardNumber));
			}

			return _retailAcct;
		}

		public ScriptInfo GetRetailScript() {
			TimokLogger.Instance.LogRbr(LogSeverity.Debug, GET_RETAIL_ACCOUNT_LABEL, "Get Retail Script");
			var _scriptInfo = new ScriptInfo
			                  {
			                  	ServiceName = Name,
			                  	PinLength = PinLength,
			                  	PromptType = BalancePromptType,
			                  	PerUnit = BalancePromptPerUnit,
			                  	ScriptLanguage = AccessNumber.ScriptLanguage,
			                  	ScriptType = AccessNumber.ScriptType
			                  };
			return _scriptInfo;
		}
	}
}
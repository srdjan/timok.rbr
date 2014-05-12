using System;
using System.Collections.Generic;
using Timok.Logger;
using Timok.Rbr.Core;
using Timok.Rbr.Core.Config;
using Timok.Rbr.DAL.RbrDatabase;

namespace Timok.Rbr.BLL.DOM {
	public sealed class CarrierAcct {
		const string CARRIER_ACCT_GET_LABEL = "CarrierAcct.Get";
		readonly CarrierAcctRow carrierAcctRow;
		readonly PartnerRow partnerRow;

		public short Id { get { return carrierAcctRow.Carrier_acct_id; } }
		public bool IsRatingEnabled { get { return carrierAcctRow.IsRatingEnabled; } }
		public Status Status { get { return (Status) carrierAcctRow.Status; } }
		public string IntlDialCode { get { return carrierAcctRow.Intl_dial_code; } }
		public bool Strip1Plus { get { return carrierAcctRow.Strip1plus; } }
		public string PrefixOut { get { return carrierAcctRow.Prefix_out; } }
		public short MaxCallLength { get { return carrierAcctRow.MaxCallLength; } }
		public int MaxNumberOfCalls { get; set; }

		public static Dictionary<short, int> NumberOfCallsCounter { get; private set; }

		public string PartnerName { get { return partnerRow.Name; } }
		public string Name { get { return carrierAcctRow.Name;  } }

		static CarrierAcct() {
			NumberOfCallsCounter = new Dictionary<short, int>();
		}

		CarrierAcct(CarrierAcctRow pCarrierAcctRow) {
			carrierAcctRow = pCarrierAcctRow;

			using (var _db = new Rbr_Db()) {
				partnerRow = _db.PartnerCollection.GetByPrimaryKey(carrierAcctRow.Partner_id);
			}
			if (partnerRow == null) {
				throw new Exception("Couldn't FIND PartnerAcct for CarrierAcctId: " + carrierAcctRow.Carrier_acct_id);
			}
			if (partnerRow.Status != (byte) Status.Active) {
				throw new Exception("PartnerAcct NOT Active for CarrierAcctId: " + carrierAcctRow.Carrier_acct_id);
			}

			//TODO: set max number of calls from db
			MaxNumberOfCalls = 1000;
		}

		//------------------------------------- Static methods ---------------------------------------------
		public static CarrierAcct Get(short pCarrierAcctId) {
			CarrierAcct _carrierAcct = null;
			try {
				using (var _db = new Rbr_Db()) {
					var _carrierAcctRow = _db.CarrierAcctCollection.GetByPrimaryKey(pCarrierAcctId);
					if (_carrierAcctRow != null) {
						_carrierAcct = new CarrierAcct(_carrierAcctRow);
					}
				}
			}
			catch (Exception _ex) {
				TimokLogger.Instance.LogRbr(LogSeverity.Critical, CARRIER_ACCT_GET_LABEL, string.Format("Exception:\r\n{0}", _ex));
			}

			if (_carrierAcct == null) {
				throw new RbrException(RbrResult.Carrier_NotFound, CARRIER_ACCT_GET_LABEL, string.Format("CarrierAcct NOT FOUND, CarrierAcctId={0}", pCarrierAcctId));
			}

			if (NumberOfCallsCounter.ContainsKey(_carrierAcct.Id)) {
				if (NumberOfCallsCounter[_carrierAcct.Id] >= _carrierAcct.MaxNumberOfCalls) {
					throw new RbrException(RbrResult.Carrier_LimitReached, CARRIER_ACCT_GET_LABEL, string.Format("CarrerAcctId={0}", _carrierAcct.Id));
				}
			}
			return _carrierAcct;
		}

		//----------------------------------------- Public Instance Methods ------------------------------------
		public int RateCall(CarrierRoute pCarrierRoute, ref Cdr pCdr) {
			int _result = 0;

			pCdr.CarrierDuration = (short) ((pCdr.Duration / 6) * 6);
			pCdr.CarrierDuration += (short) (pCdr.Duration % 6 > 0 ? 6 : 0);

			if (IsRatingEnabled && pCarrierRoute != null) {
				try {
					short _roundedSeconds;
					pCdr.CarrierCost = pCarrierRoute.GetCost(pCdr.StartTime, pCdr.Duration, out _roundedSeconds);
					if (_roundedSeconds > short.MaxValue) {
						TimokLogger.Instance.LogRbr(LogSeverity.Critical, "CarrierAcct.RateCall", "Rounded Seconds greater then short.MaxValue");
					}
					pCdr.CarrierDuration = _roundedSeconds;
					pCdr.CarrierRoundedMinutes = (short) (_roundedSeconds / 60);
				}
				catch (Exception _ex) {
					_result = 1;
					TimokLogger.Instance.LogRbr(LogSeverity.Error, "CarrierAcct.RateCall", string.Format("Finding Carrier's cost, Exception:\r\n{0}", _ex));
				}
			}
			return _result;
		}

		public void Dispose() {}
	}
}
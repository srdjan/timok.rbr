using System;
using System.IO;
using System.Xml.Serialization;
using Timok.Core;
using Timok.Logger;
using Timok.NetworkLib;
using Timok.Rbr.BLL.DOM.Repositories;
using Timok.Rbr.BLL.Entities;
using Timok.Rbr.Core;
using Timok.Rbr.DAL.RbrDatabase;

namespace Timok.Rbr.BLL.DOM {
	[Serializable]
	public sealed class CdrAggregate : Persistable {
		readonly string pk;
		[XmlIgnore]
		public string PK { get { return pk; } }

		[XmlAttribute("date_hour")]
		public int DateHour { get; set; }

		[XmlAttribute("node_id")]
		public short NodeID { get; set; }

		long accessNumber;
		[XmlAttribute("access_number")]
		public long AccessNumber { get { return accessNumber; } set { accessNumber = value; } }

		short customerAcctId;
		[XmlAttribute("customer_acct_id")]
		public short CustomerAcctId { get { return customerAcctId; } set { customerAcctId = value; } }

		int customerRouteId;
		[XmlAttribute("customer_route_id")]
		public int CustomerRouteId { get { return customerRouteId; } set { customerRouteId = value; } }

		int origIP;
		[XmlAttribute("orig_ep_ip")]
		public int OrigIP { get { return origIP; } set { origIP = value; } }

		short origId;
		[XmlAttribute("orig_ep_id")]
		public short OrigId { get { return origId; } set { origId = value; } }

		int termIP;
		[XmlAttribute("term_ep_ip")]
		public int TermIP { get { return termIP; } set { termIP = value; } }

		short termId;
		[XmlAttribute("term_ep_id")]
		public short TermId { get { return termId; } set { termId = value; } }

		//----------------Aggregates --------------------------
		[XmlElement("calls_attempted")]
		public int CallsAttempted { get; set; }

		[XmlElement("calls_completed")]
		public int CallsCompleted { get; set; }

		//-- Call Setup info
		//-----------------------------------------------------------------------------------
		[XmlElement("setup_seconds")]
		public int SetupSeconds { get; set; }

		[XmlElement("alert_seconds")]
		public int AlertSeconds { get; set; }

		[XmlElement("connected_seconds")]
		public int ConnectedSeconds { get; set; }

		//rounded to a 1 tenth
		[XmlElement("connected_minutes")]
		public decimal ConnectedMinutes { get; set; }

		//-- Carrier info
		//-----------------------------------------------------------------------------------
		short carrierAcctId;
		[XmlAttribute("carrier_acct_id")]
		public short CarrierAcctId { get { return carrierAcctId; } set { carrierAcctId = value; } }

		int carrierRouteId;
		[XmlAttribute("carrier_route_id")]
		public int CarrierRouteId { get { return carrierRouteId; } set { carrierRouteId = value; } }

		[XmlElement("carrier_cost")]
		public decimal CarrierCost { get; set; }

		//rounded using carrier's first/additional increment length
		[XmlElement("carrier_rounded_minutes")]
		public decimal CarrierRoundedMinutes { get; set; }

		//-- Wholesale info
		//-----------------------------------------------------------------------------------
		[XmlElement("wholesale_price")]
		public decimal WholesalePrice { get; set; }

		//rounded using wholesale first/additional increment length
		[XmlElement("wholesale_rounded_minutes")]
		public decimal WholesaleRoundedMinutes { get; set; }

		//-- EndUser info
		//-----------------------------------------------------------------------------------
		[XmlElement("end_user_price")]
		public decimal EndUserPrice { get; set; }

		//rounded using endUser's first/additional increment length
		[XmlElement("end_user_rounded_minutes")]
		public decimal EndUserRoundedMinutes { get; set; }

		//-- ctor, called OnCallAttempted
		public CdrAggregate() {
		}

		public CdrAggregate(string pAccessNumber, string pOrigIP, string pTermIP, int pCustomerRouteId, int pCarrierRouteId, short pCustomerAcctId, short pCarrierAcctId) {
			NodeID = (new CurrentNode()).Id;
			DateHour = TimokDate.Parse(DateTime.Now, DateTime.Now.Hour);

			//-- Parse AccessNumber
			long.TryParse(pAccessNumber, out accessNumber);
			if (accessNumber == 0) {
				TimokLogger.Instance.LogRbr(LogSeverity.Debug, "CdrAggregate.Ctor", string.Format("Invalid Access Number:{0}", pAccessNumber));
			}

			//-- Parse IP
			if (origIP != 0) {
				try {
					origIP = IPUtil.ToInt32(pOrigIP);
					var _ep = Endpoint.Get(pOrigIP);
					if (_ep == null) {
						throw new RbrException(RbrResult.OrigEP_NotFound, "CdrAggregate.Ctor", string.Format("Endpoint NOT FOUND, IP={0}", pOrigIP));
					}
					origId = _ep.Id;
				}
				catch (RbrException _rbrex) {
					TimokLogger.Instance.LogRbr(LogSeverity.Error, "CdrAggregate.Ctor", string.Format("Orig Endpoint NOT FOUND, IP={0}, Exception:\r\n{1}", pOrigIP, _rbrex));
				}
				catch (Exception _ex) {
					TimokLogger.Instance.LogRbr(LogSeverity.Error, "CdrAggregate.Ctor", string.Format("Orig Endpoint NOT FOUND, IP={0}, Exception:\r\n{1}", pOrigIP, _ex));
				}
			}

			//-- Parse TermIP
			if (termIP != 0) {
				try {
					termIP = IPUtil.ToInt32(pTermIP);
					var _ep = Endpoint.Get(pTermIP);
					if (_ep == null) {
						throw new RbrException(RbrResult.OrigEP_NotFound, "CdrAggregate.Ctor", string.Format("Endpoint NOT FOUND, IP={0}", pTermIP));
					}

					termId = _ep.Id;
				}
				catch (RbrException _rbrex) {
					TimokLogger.Instance.LogRbr(LogSeverity.Error, "CdrAggregate.Ctor", string.Format("Term Endpoint NOT FOUND, IP={0}, Exception:\r\n{1}", pTermIP, _rbrex));
				}
				catch (Exception _ex) {
					TimokLogger.Instance.LogRbr(LogSeverity.Error, "CdrAggregate.Ctor", string.Format("Term Endpoint NOT FOUND, IP={0}, Exception:\r\n{1}", pTermIP, _ex));
				}
			}

			customerRouteId = pCustomerRouteId;
			carrierRouteId = pCarrierRouteId;
			customerAcctId = pCustomerAcctId;
			carrierAcctId = pCarrierAcctId;

			//-- set Pkey
			var _prep = string.Format("{0}{1}{2}{3}{4}{5}{6}", accessNumber, origId, termId, customerAcctId, customerRouteId, carrierAcctId, carrierRouteId);
			TimokLogger.Instance.LogRbr(LogSeverity.Debug, "CdrAggregate.Ctor", string.Format("Key: {0}-{1}-{2}-{3}-{4}-{5}-{6}", accessNumber, origId, termId, customerAcctId, customerRouteId, carrierAcctId, carrierRouteId));
			pk = Md5hash.Get(_prep);
		}

		#region Documentation

		/*
		call time line:

		x---------------x--------------x-------------------------------x
		setup              alerting           connected                              completed

	   (1) Time between 'setup' and 'alerting' is delay added by our server

		(2) Time between 'alerting' and 'connected'
		this is  variable (enduser dependent), but together with (1) represents length of
		the call that has a cost to it (mostly for the cases when inbound line is T1
		(ie 800#... Joe) for the service provider. VoIP origination doesn't have per minute
		origination cost.

		(3) Time between 'connected' and 'completed'
		this is part of the call that will be billed to enduser but also it has to be payed for
		to the terminating carrier.

		here is how would we calculate call cost:

		callcost=(1) + (2) * originationCost + (3) * terminationCost
		(1) + (2) can be used to adjust end user prices if ratio between connected
		time and setup time was not originally built in correctly to the enduser pricing.
		So, I think that it would be great if we can start measuring 'alerting' event too.
		It gives us a measure of how long it takes for call to reach far end. 
		*/

		#endregion

		//-- ctor, called by Serializer

		public static bool DeserializeAndImport(string pFilePath) {
			using (var _fs = new FileStream(pFilePath, FileMode.Open, FileAccess.Read)) {
				var _serializer = new XmlSerializer(typeof (CdrAggregate[]));
				var _cdrAggregates = (CdrAggregate[]) _serializer.Deserialize(_fs);
				if (_cdrAggregates != null && _cdrAggregates.Length > 0) {
					var _cdrAggregateRows = map(_cdrAggregates);

					using (var _db = new Rbr_Db()) {
						_db.BeginTransaction();
						try {
							foreach (var _cdrAggregateRow in _cdrAggregateRows) {
								_db.CdrAggregateCollection.Insert(_cdrAggregateRow);
							}
							_db.CommitTransaction();
						}
						catch (Exception _ex) {
							_db.RollbackTransaction();
							TimokLogger.Instance.LogRbr(LogSeverity.Critical, "CdrAggregate.ImportToDb", string.Format("Exception:\r\n{0}", _ex));
							throw;
						}
					}
				}
			}
			return true;
		}

		public void ImportToDb(CdrAggregate[] pCdrAggregates) {
			if (pCdrAggregates != null && pCdrAggregates.Length > 0) {
				var _cdrAggregateRows = map(pCdrAggregates);

				using (var _db = new Rbr_Db()) {
					_db.BeginTransaction();
					try {
						foreach (var _cdrAggregateRow in _cdrAggregateRows) {
							_db.CdrAggregateCollection.Insert(_cdrAggregateRow);
						}
						_db.CommitTransaction();
					}
					catch (Exception _ex) {
						_db.RollbackTransaction();
						TimokLogger.Instance.LogRbr(LogSeverity.Critical, "CdrAggregate.ImportToDb", string.Format("Exception:\r\n{0}", _ex));
						throw;
					}
				}
			}
		}

		//---------------------------- Private -----------------------------------------------
		//TODO: optimize - bad array copy !
		static CdrAggregateRow[] map(CdrAggregate[] pCdrAggregates) {
			var _cdrAggrRows = new CdrAggregateRow[pCdrAggregates.Length];

			for (int _i = 0; _i < pCdrAggregates.Length; _i++) {
				_cdrAggrRows[_i] = new CdrAggregateRow();
				_cdrAggrRows[_i].Date_hour = pCdrAggregates[_i].DateHour;
				_cdrAggrRows[_i].Node_id = pCdrAggregates[_i].NodeID;
				_cdrAggrRows[_i].Access_number = pCdrAggregates[_i].AccessNumber;
				_cdrAggrRows[_i].Orig_end_point_IP = pCdrAggregates[_i].OrigIP;
				_cdrAggrRows[_i].Orig_end_point_id = pCdrAggregates[_i].OrigId;
				_cdrAggrRows[_i].Customer_acct_id = pCdrAggregates[_i].CustomerAcctId;
				_cdrAggrRows[_i].Customer_route_id = pCdrAggregates[_i].CustomerRouteId;
				_cdrAggrRows[_i].Carrier_acct_id = pCdrAggregates[_i].CarrierAcctId;
				_cdrAggrRows[_i].Carrier_route_id = pCdrAggregates[_i].CarrierRouteId;
				_cdrAggrRows[_i].Term_end_point_IP = pCdrAggregates[_i].TermIP;
				_cdrAggrRows[_i].Term_end_point_id = pCdrAggregates[_i].TermId;

				_cdrAggrRows[_i].Calls_attempted = pCdrAggregates[_i].CallsAttempted;
				_cdrAggrRows[_i].Calls_completed = pCdrAggregates[_i].CallsCompleted;
				_cdrAggrRows[_i].Setup_seconds = pCdrAggregates[_i].SetupSeconds;
				_cdrAggrRows[_i].Alert_seconds = pCdrAggregates[_i].AlertSeconds;
				_cdrAggrRows[_i].Connected_seconds = pCdrAggregates[_i].ConnectedSeconds;
				_cdrAggrRows[_i].Connected_minutes = pCdrAggregates[_i].ConnectedMinutes;
				_cdrAggrRows[_i].Carrier_cost = pCdrAggregates[_i].CarrierCost;
				_cdrAggrRows[_i].Carrier_rounded_minutes = pCdrAggregates[_i].CarrierRoundedMinutes;
				_cdrAggrRows[_i].Wholesale_price = pCdrAggregates[_i].WholesalePrice;
				_cdrAggrRows[_i].Wholesale_rounded_minutes = pCdrAggregates[_i].WholesaleRoundedMinutes;
				_cdrAggrRows[_i].End_user_price = pCdrAggregates[_i].EndUserPrice;
				_cdrAggrRows[_i].End_user_rounded_minutes = pCdrAggregates[_i].EndUserRoundedMinutes;
			}
			return _cdrAggrRows;
		}
	}
}
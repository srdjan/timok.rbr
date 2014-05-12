using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using RbrCommon;
using RbrData.DataContracts;
using RbrData.Helpers;

namespace RbrData {
	public class Domain {
		readonly CdrAggrDataContext db;

		public Domain() {
			//@"Data Source=.\TRBR;Initial Catalog=RbrDb_267;Integrated Security=True"
			var _connectionString = ConfigurationManager.ConnectionStrings["RbrDb"].ConnectionString;
			db = new CdrAggrDataContext(_connectionString);
		}

		public UserRecord GetUser(string pLogin, string pPassword) {
			try {
				Logger.Log(string.Format("ERROR: Person NOT found [Login: {0}]", pLogin));

				var _person = ( from _p in db.Persons
										where _p.login == pLogin
										select _p).SingleOrDefault();
				var _shp = PsswdHasher.FromSaltHashedPwd(_person.password, _person.salt);
				if (!_shp.Verify(pPassword)) {
					Logger.Log(string.Format("ERROR: Person password NOT valid!!! [Login: {0}] [Status: {1}]", _person.login, _person.status));
					return null;
				}

				if (_person.status != (byte)Status.Active) {// || _person.status == (byte)Status.InUse) {
					Logger.Log(string.Format("ERROR: Person status NOT valid!!! [Login: {0}] [Status: {1}]", _person.login, _person.status));
					return null;
				}
				var _userRecord = new UserRecord(_person.person_id, _person.name, _person.password, _person.partner_id);
				return _userRecord;
			}
			catch (Exception _ex) {
				Logger.Log(string.Format("ERROR: Person NOT found [Login: {0}] [Ex: {1}]", pLogin, _ex));
			}
			return null;
		}

		public List<PartnerRecord> GetPartner(int? pPartnerId) {
			if (pPartnerId == null || pPartnerId == 0) {
				return GetAllPartners();
			}

			Partner _partner;
			try {
				_partner = (from _p in db.Partners
				                where _p.partner_id == pPartnerId
				                select _p).Single();
			}
			catch (Exception _ex) {
				if (_ex.Message == "Sequence contains no elements") {
					Logger.Log(string.Format("ERROR: Partner NOT found [Id: {0}]", pPartnerId));
					return null;
				}
				throw;
			}

			var _partnerRecord = new PartnerRecord(_partner.partner_id, _partner.name, GetCustomerAcctRecords(_partner), GetCarrierAcctRecords(_partner));
			return new List<PartnerRecord> {_partnerRecord};
		}

		public NodeRecord GetNode(short pId) {
			var _node = (from _n in db.Nodes
										where _n.node_id == pId
										select _n).Single();

			var _nodeRecord = new NodeRecord(_node.node_id, _node.description);
			return _nodeRecord;
		}

		public List<NodeRecord> GetAllNodes() {
			var _nodes = from _n in db.Nodes select _n;

			var _list = new List<NodeRecord>();
			foreach (var _result in _nodes) {
				_list.Add(new NodeRecord(_result.node_id, _result.description));
			}
			return _list;
		}

		public List<PartnerRecord> GetAllPartners() {
			var _partners = from _p in db.Partners select _p;

			var _list = new List<PartnerRecord>();
			foreach (var _partner in _partners) {
				if (_partner.name.StartsWith("zzz")) {
					continue;
				}
				_list.Add(new PartnerRecord(_partner.partner_id, _partner.name, GetCustomerAcctRecords(_partner), GetCarrierAcctRecords(_partner)));
			}
			return _list;
		}

		//----------------------------------------------------------------------------------
		protected List<CustomerAcctRecord> GetCustomerAcctRecords(Partner pPartner) {
			var _list = new List<CustomerAcctRecord>();
			foreach (var _customerAcct in pPartner.CustomerAccts) {
				if (_customerAcct.name.StartsWith("zzz")) {
					continue;
				}
				_list.Add(new CustomerAcctRecord(_customerAcct.customer_acct_id, _customerAcct.name));
			}
			return _list;
		}

		protected List<CarrierAcctRecord> GetCarrierAcctRecords(Partner pPartner) {
			var _list = new List<CarrierAcctRecord>();
			foreach (var _carrierAcct in pPartner.CarrierAccts) {
				if (_carrierAcct.name.StartsWith("zzz")) {
					continue;
				}
				_list.Add(new CarrierAcctRecord(_carrierAcct.carrier_acct_id, _carrierAcct.name));
			}
			return _list;
		}
	}
}
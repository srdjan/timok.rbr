using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using RbrData.DataContracts;

namespace RbrData {
	public class Reports {
		readonly CdrAggrDataContext db;
		readonly Domain domain;
		readonly List<NodeRecord> nodes;

		public Reports() {
			//@"Data Source=.\TRBR;Initial Catalog=RbrDb_267;Integrated Security=True"
			var _connectionString = ConfigurationManager.ConnectionStrings["RbrDb"].ConnectionString;
			db = new CdrAggrDataContext(_connectionString);
			domain = new Domain();
			nodes = domain.GetAllNodes();
		}

		//-------------------------- Node report ---------------------------------------------------------------------------------
		public List<NodeReportRecord> GetNodeReport(string pShortDateString) {
			var _list = new List<NodeReportRecord>();
			foreach (var _node in nodes) {
				var _collection = getNodeReport(_node.NodeId, pShortDateString);
				if (_collection != null && _collection.Count > 0) {
					_list.AddRange(_collection);
				}
			}
			return _list;
		}

		List<NodeReportRecord> getNodeReport(short? pNodeId, string pShortDateString) {
			var _node = pNodeId == null ? string.Empty : pNodeId.ToString();
			var _results = db.proc_q_daily(_node, pShortDateString).ToList();

			var _list = new List<NodeReportRecord>();
			foreach (var _result in _results) {
				_list.Add(new NodeReportRecord(_result.aggr_1_node_id,
																						_result.aggr_1_date_hour,
																						_result.aggr_1_InMinutes,
																						_result.aggr_1_OutMinutes,
																						_result.aggr_1_Total,
																						_result.aggr_1_Completed));
			}
			return _list;
		}

		//-------------------------- Customer report ---------------------------------------------------------------------------------
		public List<CustomerReportRecord> GetCustomersReport(List<CustomerAcctRecord> pCustomerAccts, string pShortDateString) {
			var _list = new List<CustomerReportRecord>();
			foreach (var _node in nodes) {
				foreach (var _customer in pCustomerAccts) {
					var _collection = getCustomerReport(_node.NodeId, _customer.Id.ToString(), pShortDateString);
					if (_collection != null && _collection.Count > 0) {
						_list.AddRange(_collection);
					}
				}
			}
			return _list;
		}

		List<CustomerReportRecord> getCustomerReport(short? pNodeId, string pCustomerAcctId, string pShortDateString) {
			var _results = db.proc_q_customer(pNodeId.ToString(), pShortDateString, pCustomerAcctId);

			var _list = new List<CustomerReportRecord>();
			try {
				foreach (var _result in _results) {
					_list.Add(new CustomerReportRecord(_result.aggr_1_Day,
																									_result.aggr_1_node_id,
																									_result.aggr_1_customer_acct_id,
																									_result.aggr_1_carrier_acct_id,
																									_result.aggr_1_Total,
																									_result.aggr_1_Completed,
																									_result.aggr_1_InMinutes,
																									_result.aggr_1_OutMinutes,
																									_result.aggr_1_Cost,
																									_result.aggr_1_Name,
																									_result.aggr_1_PopName));
				}
			}
			catch (Exception _ex) {
				//TODO: log exception
				return null;
			}
			return _list;
		}

		//-------------------------- Route report ---------------------------------------------------------------------------------
		public List<RouteReportRecord> GetRouteReport(List<CustomerAcctRecord> pCustomerAccts, string pShortDateString) {
			var _list = new List<RouteReportRecord>();
			foreach (var _node in nodes) {
				foreach (var _customer in pCustomerAccts) {
					var _collection = getRouteReport(_node.NodeId, _node.Name, _customer.Id.ToString(), _customer.Name, pShortDateString);
					if (_collection != null && _collection.Count > 0) {
						_list.AddRange(_collection);
					}
				}
			}
			return _list;
		}

		List<RouteReportRecord> getRouteReport(short? pNodeId, string pNode, string pCustomerAcctId, string pCustomerAcctName, string pShortDateString) {
			var _results = db.proc_q_route(pNodeId.ToString(), pShortDateString, pCustomerAcctId);

			var _list = new List<RouteReportRecord>();
			try {
				foreach (var _result in _results) {
					_list.Add(new RouteReportRecord(_result.aggr_1_Day,
																							 pNode ,
																							 pCustomerAcctName,
																							 _result.aggr_1_Total,
																							 _result.aggr_1_Completed,
																							 _result.aggr_1_InMinutes,
																							 _result.aggr_1_OutMinutes,
																							 _result.aggr_1_Cost,
																							 _result.aggr_1_Name,
																							 _result.aggr_1_PopName));
				}
			}
			catch (Exception _ex) {
				//TODO: log exception
				return null;
			}
			return _list;
		}

		//-------------------------- Trunk reports ---------------------------------------------------------------------------------
		public List<TrunkReportRecord> GetCarrierTrunkReport(List<CarrierAcctRecord> pCarrierAccts, string pShortDateString) {
			var _list = new List<TrunkReportRecord>();
			foreach (var _node in nodes) {
				foreach (var _carrier in pCarrierAccts) {
					var _collection = getCarrierTrunkReport(_node.NodeId, _carrier.Id.ToString(), pShortDateString);
					if (_collection != null && _collection.Count > 0) {
						_list.AddRange(_collection);
					}
				}
			}
			return _list;
		}

		List<TrunkReportRecord> getCarrierTrunkReport(short? pNodeId, string pCarrierAcctId, string pShortDateString) {
			var _results = db.proc_q_trunk(pNodeId.ToString(), pShortDateString, pCarrierAcctId);

			var _list = new List<TrunkReportRecord>();

			try {
				foreach (var _result in _results) {
					_list.Add(new TrunkReportRecord(_result.aggr_1_Day,
																							 _result.aggr_1_node_id,
																							 _result.aggr_1_customer_acct_id,
																							 _result.aggr_1_CustName,
																							 _result.aggr_1_carrier_acct_id,
																							 _result.aggr_1_Total,
																							 _result.aggr_1_Completed,
																							 _result.aggr_1_InMinutes,
																							 _result.aggr_1_OutMinutes,
																							 _result.aggr_1_Cost,
																							 _result.aggr_1_Name
											));
				}
			}
			catch (Exception _ex) {
				//TODO: log exception
				return null;
			}
			return _list;
		}
	}
}
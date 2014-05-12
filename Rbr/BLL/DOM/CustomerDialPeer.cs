using System;
using System.Collections.Generic;
using Timok.Logger;
using Timok.Rbr.Core.Config;
using Timok.Rbr.DAL.RbrDatabase;

namespace Timok.Rbr.BLL.DOM {
	public sealed class CustomerDialPeer {
		readonly DialPeerRow dialPeerRow;

		public CustomerDialPeer(DialPeerRow pDialPeerRow) {
			dialPeerRow = pDialPeerRow;
		}

		public short CustomerId { get { return dialPeerRow.Customer_acct_id; } }
		public short OrigEPId { get { return dialPeerRow.End_point_id; } }
		public string PrefixIn { get { return dialPeerRow.Prefix_in; } }

		public static CustomerDialPeer Get(short pEndpointId, string pPrefix) {
			DialPeerRow _dialPeerRow;
			try {
				using (var _db = new Rbr_Db()) {
					_dialPeerRow = _db.DialPeerCollection.GetByPrimaryKey(pEndpointId, pPrefix);
					if (_dialPeerRow == null) {
						TimokLogger.Instance.LogRbr(LogSeverity.Error, "CustomerDialPeer.Get", string.Format("DialPeer NOT FOUND in Db: [Ep: {0}][Prefix: {1}]", pEndpointId, pPrefix));
						return null;
					}
				}
			}
			catch (Exception _ex) {
				TimokLogger.Instance.LogRbr(LogSeverity.Critical, "CustomerDialPeer.Get", string.Format("Exception:\r\n{0}", _ex));
				return null;
			}
			return new CustomerDialPeer(_dialPeerRow);
		}

		public static IList<CustomerDialPeer> GetAll(IConfiguration pConfiguration, ILogger pLogger) {
			var _dialPeerRows = new DialPeerRow[] {};
			try {
				using (var _db = new Rbr_Db()) {
					_dialPeerRows = _db.DialPeerCollection.GetAll();
				}
			}
			catch (Exception _ex) {
				pLogger.LogRbr(LogSeverity.Critical, "CustomerDialPeer.GetAll", string.Format("Exception:\r\n{0}", _ex));
			}
			if (_dialPeerRows == null || _dialPeerRows.Length == 0) {
				throw new Exception("No Customer DialPeers FOUND in Db");
			}

			var _customerDialPeers = new List<CustomerDialPeer>();
			foreach (var _dialPeerRow in _dialPeerRows) {
				_customerDialPeers.Add(new CustomerDialPeer(_dialPeerRow));
			}
			return _customerDialPeers;
		}
	}
}
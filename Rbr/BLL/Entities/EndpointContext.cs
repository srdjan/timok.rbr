using System;
using System.Collections;
using Timok.Rbr.Core.Config;
using Timok.Rbr.DAL.RbrDatabase;
using Timok.Rbr.DTO;

namespace Timok.Rbr.BLL.Entities {
	[Serializable]
	public class EndpointContext {
		//if set and != null - fixed to only one

		ArrayList allowedEndPointProtocols = new ArrayList();
		public PrefixInTypeDto PrefixInType { get; set; }

		public ArrayList AllowedEndPointProtocols {
			get { return allowedEndPointProtocols; }
			set {
				if (value == null || value.Count == 0) {
					throw new Exception("At least one EndPointProtocol has to be specified.");
				}
				allowedEndPointProtocols = value;
			}
		}

		//public TxType TransactionType { get; set; }

		//indicates that DialPeer has to be added for this cust
		public CustomerAcctDto CustomerAcct { get; set; }

		public ServiceDto ServiceDto { get; set; }

		public CarrierAcctDto CarrierAcct { get; set; }

		public CarrierAcctEPMapRow[] CarrierAcctEPMapRowToAdd { get; set; }

		public CarrierAcctEPMapRow[] CarrierAcctEPMapRowToDelete { get; set; }

		//		private bool isShared;
		//		public bool IsShared {
		//			get { return isShared; }
		//			set { isShared = value; }
		//		}
	}
}
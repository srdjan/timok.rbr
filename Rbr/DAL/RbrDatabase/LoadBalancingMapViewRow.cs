// <fileinfo name="LoadBalancingMapViewRow.cs">
//		<copyright>
//			Copyright © 2002-2007 Timok ES LLC. All rights reserved.
//		</copyright>
//		<remarks>
//			You can update this source code manually. If the file
//			already exists it will not be rewritten by the generator.
//		</remarks>
//		<generator rewritefile="False" infourl="http://www.SharpPower.com">RapTier</generator>
// </fileinfo>

using System;
using Timok.Rbr.Core.Config;
using Timok.Rbr.DAL.RbrDatabase.Base;
using System.Runtime.Serialization;
using System.Xml.Serialization;

namespace Timok.Rbr.DAL.RbrDatabase {
	/// <summary>
	/// Represents a record in the <c>LoadBalancingMapView</c> view.
	/// </summary>
	[Serializable]
	public class LoadBalancingMapViewRow : LoadBalancingMapViewRow_Base {
		public const string ServiceRetailType_PropName = "ServiceRetailType";
		public const string ServiceRetailType_DisplayName = "Service Retail Type";
		public const string AccountStatus_PropName = "AccountStatus";
		public const string AccountStatus_DisplayName = "Acct Status";
		public const string IsPrepaid_PropName = "IsPrepaid";
		public const string IsPrepaid_DisplayName = "Prepaid";
		public const string WithPrefixes_PropName = "WithPrefixes";
		public const string WithPrefixes_DisplayName = "w/Prefix";

		
		
		public const string NodeConfiguration_PropName = "NodeConfiguration";
		public const string NodeConfiguration_DisplayName = "Node Config";
		public const string PlatformConfig_PropName = "PlatformConfiguration";
		public const string PlatformConfig_DisplayName = "Platform Config";
    
    public const string ServiceType_PropName = "ServiceType";
    public const string ServiceType_DisplayName = "Service Type";

    public ServiceType ServiceType {
      get { return (ServiceType) this.Service_type; }
    }

		[XmlIgnore]
		public RetailType ServiceRetailType {
			get { return (RetailType)this.Service_retail_type; }
		}
		
		[XmlIgnore]
		public Status AccountStatus {
			get { return (Status)this.Customer_acct_status; }
		}

		[XmlIgnore]
		public bool IsPrepaid {
			get { return (this.Is_prepaid > 0); }
		}

		[XmlIgnore]
		public bool WithPrefixes {
			get { return this.Prefix_in_type_id > 0; }
		}

		[XmlIgnore]
		public NodeRole NodeConfiguration {
			get { return (NodeRole) this.Node_config; }
		}

		[XmlIgnore]
		public PlatformConfig PlatformConfiguration {
			get { return (PlatformConfig) this.Platform_config; }
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="LoadBalancingMapViewRow"/> class.
		/// </summary>
		public LoadBalancingMapViewRow(){
			// EMPTY
		}

		public LoadBalancingMapRow ToLoadBalancingMapRow() {
			LoadBalancingMapRow _map = new LoadBalancingMapRow();
			_map.Node_id = this.Node_id;
			_map.Customer_acct_id = this.Customer_acct_id;
			_map.Max_calls = this.Max_calls;
			_map.Current_calls = this.Current_calls;

			return _map;
		}

		public override bool Equals(object obj) {
			return base.Equals(obj);
		}

		public override int GetHashCode() {
			return base.GetHashCode ();
		}

	} // End of LoadBalancingMapViewRow class
} // End of namespace

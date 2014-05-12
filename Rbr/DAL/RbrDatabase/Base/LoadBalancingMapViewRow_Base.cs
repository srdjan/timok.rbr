// <fileinfo name="Base\LoadBalancingMapViewRow_Base.cs">
//		<copyright>
//			Copyright © 2002-2007 Timok ES LLC. All rights reserved.
//		</copyright>
//		<remarks>
//			Do not change this source code manually. Changes to this file may 
//			cause incorrect behavior and will be lost if the code is regenerated.
//		</remarks>
//		<generator rewritefile="True" infourl="http://www.SharpPower.com">RapTier</generator>
// </fileinfo>

using System;
using System.Runtime.Serialization;
using Timok.Core;

namespace Timok.Rbr.DAL.RbrDatabase.Base
{
	/// <summary>
	/// The base class for <see cref="LoadBalancingMapViewRow"/> that 
	/// represents a record in the <c>LoadBalancingMapView</c> view.
	/// </summary>
	/// <remarks>
	/// Do not change this source code manually. Update the <see cref="LoadBalancingMapViewRow"/>
	/// class if you need to add or change some functionality.
	/// </remarks>
	[Serializable]
	public abstract class LoadBalancingMapViewRow_Base
	{
	#region Timok Custom

		//db field names
		public const string node_id_DbName = "node_id";
		public const string customer_acct_id_DbName = "customer_acct_id";
		public const string max_calls_DbName = "max_calls";
		public const string current_calls_DbName = "current_calls";
		public const string platform_id_DbName = "platform_id";
		public const string node_description_DbName = "node_description";
		public const string node_config_DbName = "node_config";
		public const string transport_type_DbName = "transport_type";
		public const string node_status_DbName = "node_status";
		public const string ip_address_DbName = "ip_address";
		public const string dot_ip_address_DbName = "dot_ip_address";
		public const string platform_location_DbName = "platform_location";
		public const string platform_status_DbName = "platform_status";
		public const string platform_config_DbName = "platform_config";
		public const string customer_acct_name_DbName = "customer_acct_name";
		public const string customer_acct_status_DbName = "customer_acct_status";
		public const string prefix_in_DbName = "prefix_in";
		public const string prefix_out_DbName = "prefix_out";
		public const string prefix_in_type_id_DbName = "prefix_in_type_id";
		public const string partner_id_DbName = "partner_id";
		public const string partner_name_DbName = "partner_name";
		public const string partner_status_DbName = "partner_status";
		public const string service_type_DbName = "service_type";
		public const string service_retail_type_DbName = "service_retail_type";
		public const string is_prepaid_DbName = "is_prepaid";
		public const string is_shared_service_DbName = "is_shared_service";

		//prop names
		public const string node_id_PropName = "Node_id";
		public const string customer_acct_id_PropName = "Customer_acct_id";
		public const string max_calls_PropName = "Max_calls";
		public const string current_calls_PropName = "Current_calls";
		public const string platform_id_PropName = "Platform_id";
		public const string node_description_PropName = "Node_description";
		public const string node_config_PropName = "Node_config";
		public const string transport_type_PropName = "Transport_type";
		public const string node_status_PropName = "Node_status";
		public const string ip_address_PropName = "Ip_address";
		public const string dot_ip_address_PropName = "Dot_ip_address";
		public const string platform_location_PropName = "Platform_location";
		public const string platform_status_PropName = "Platform_status";
		public const string platform_config_PropName = "Platform_config";
		public const string customer_acct_name_PropName = "Customer_acct_name";
		public const string customer_acct_status_PropName = "Customer_acct_status";
		public const string prefix_in_PropName = "Prefix_in";
		public const string prefix_out_PropName = "Prefix_out";
		public const string prefix_in_type_id_PropName = "Prefix_in_type_id";
		public const string partner_id_PropName = "Partner_id";
		public const string partner_name_PropName = "Partner_name";
		public const string partner_status_PropName = "Partner_status";
		public const string service_type_PropName = "Service_type";
		public const string service_retail_type_PropName = "Service_retail_type";
		public const string is_prepaid_PropName = "Is_prepaid";
		public const string is_shared_service_PropName = "Is_shared_service";

		//db field display names
		public const string node_id_DisplayName = "node id";
		public const string customer_acct_id_DisplayName = "customer acct id";
		public const string max_calls_DisplayName = "max calls";
		public const string current_calls_DisplayName = "current calls";
		public const string platform_id_DisplayName = "platform id";
		public const string node_description_DisplayName = "node description";
		public const string node_config_DisplayName = "node config";
		public const string transport_type_DisplayName = "transport type";
		public const string node_status_DisplayName = "node status";
		public const string ip_address_DisplayName = "ip address";
		public const string dot_ip_address_DisplayName = "dot ip address";
		public const string platform_location_DisplayName = "platform location";
		public const string platform_status_DisplayName = "platform status";
		public const string platform_config_DisplayName = "platform config";
		public const string customer_acct_name_DisplayName = "customer acct name";
		public const string customer_acct_status_DisplayName = "customer acct status";
		public const string prefix_in_DisplayName = "prefix in";
		public const string prefix_out_DisplayName = "prefix out";
		public const string prefix_in_type_id_DisplayName = "prefix in type id";
		public const string partner_id_DisplayName = "partner id";
		public const string partner_name_DisplayName = "partner name";
		public const string partner_status_DisplayName = "partner status";
		public const string service_type_DisplayName = "service type";
		public const string service_retail_type_DisplayName = "service retail type";
		public const string is_prepaid_DisplayName = "is prepaid";
		public const string is_shared_service_DisplayName = "is shared service";
	#endregion Timok Custom


		private short _node_id;
		private short _customer_acct_id;
		private int _max_calls;
		private int _current_calls;
		private short _platform_id;
		private bool _platform_idNull = true;
		private string _node_description;
		private int _node_config;
		private bool _node_configNull = true;
		private byte _transport_type;
		private bool _transport_typeNull = true;
		private byte _node_status;
		private bool _node_statusNull = true;
		private int _ip_address;
		private bool _ip_addressNull = true;
		private string _dot_ip_address;
		private string _platform_location;
		private byte _platform_status;
		private bool _platform_statusNull = true;
		private int _platform_config;
		private bool _platform_configNull = true;
		private string _customer_acct_name;
		private byte _customer_acct_status;
		private bool _customer_acct_statusNull = true;
		private string _prefix_in;
		private string _prefix_out;
		private short _prefix_in_type_id;
		private bool _prefix_in_type_idNull = true;
		private int _partner_id;
		private bool _partner_idNull = true;
		private string _partner_name;
		private byte _partner_status;
		private bool _partner_statusNull = true;
		private byte _service_type;
		private bool _service_typeNull = true;
		private int _service_retail_type;
		private bool _service_retail_typeNull = true;
		private byte _is_prepaid;
		private bool _is_prepaidNull = true;
		private byte _is_shared_service;
		private bool _is_shared_serviceNull = true;

		/// <summary>
		/// Initializes a new instance of the <see cref="LoadBalancingMapViewRow_Base"/> class.
		/// </summary>
		public LoadBalancingMapViewRow_Base()
		{
			// EMPTY
		}

		/// <summary>
		/// Gets or sets the <c>node_id</c> column value.
		/// </summary>
		/// <value>The <c>node_id</c> column value.</value>
		public short Node_id
		{
			get { return _node_id; }
			set { _node_id = value; }
		}

		/// <summary>
		/// Gets or sets the <c>customer_acct_id</c> column value.
		/// </summary>
		/// <value>The <c>customer_acct_id</c> column value.</value>
		public short Customer_acct_id
		{
			get { return _customer_acct_id; }
			set { _customer_acct_id = value; }
		}

		/// <summary>
		/// Gets or sets the <c>max_calls</c> column value.
		/// </summary>
		/// <value>The <c>max_calls</c> column value.</value>
		public int Max_calls
		{
			get { return _max_calls; }
			set { _max_calls = value; }
		}

		/// <summary>
		/// Gets or sets the <c>current_calls</c> column value.
		/// </summary>
		/// <value>The <c>current_calls</c> column value.</value>
		public int Current_calls
		{
			get { return _current_calls; }
			set { _current_calls = value; }
		}

		/// <summary>
		/// Gets or sets the <c>platform_id</c> column value.
		/// This column is nullable.
		/// </summary>
		/// <value>The <c>platform_id</c> column value.</value>
		public short Platform_id
		{
			get
			{
				//if(IsPlatform_idNull)
					//throw new InvalidOperationException("Cannot get value because it is DBNull.");
				return _platform_id;
			}
			set
			{
				_platform_idNull = false;
				_platform_id = value;
			}
		}

		/// <summary>
		/// Indicates whether the <see cref="Platform_id"/>
		/// property value is null.
		/// </summary>
		/// <value>true if the property value is null, otherwise false.</value>
		public bool IsPlatform_idNull
		{
			get { return _platform_idNull; }
			set { _platform_idNull = value; }
		}

		/// <summary>
		/// Gets or sets the <c>node_description</c> column value.
		/// This column is nullable.
		/// </summary>
		/// <value>The <c>node_description</c> column value.</value>
		public string Node_description
		{
			get { return _node_description; }
			set { _node_description = value; }
		}

		/// <summary>
		/// Gets or sets the <c>node_config</c> column value.
		/// This column is nullable.
		/// </summary>
		/// <value>The <c>node_config</c> column value.</value>
		public int Node_config
		{
			get
			{
				//if(IsNode_configNull)
					//throw new InvalidOperationException("Cannot get value because it is DBNull.");
				return _node_config;
			}
			set
			{
				_node_configNull = false;
				_node_config = value;
			}
		}

		/// <summary>
		/// Indicates whether the <see cref="Node_config"/>
		/// property value is null.
		/// </summary>
		/// <value>true if the property value is null, otherwise false.</value>
		public bool IsNode_configNull
		{
			get { return _node_configNull; }
			set { _node_configNull = value; }
		}

		/// <summary>
		/// Gets or sets the <c>transport_type</c> column value.
		/// This column is nullable.
		/// </summary>
		/// <value>The <c>transport_type</c> column value.</value>
		public byte Transport_type
		{
			get
			{
				//if(IsTransport_typeNull)
					//throw new InvalidOperationException("Cannot get value because it is DBNull.");
				return _transport_type;
			}
			set
			{
				_transport_typeNull = false;
				_transport_type = value;
			}
		}

		/// <summary>
		/// Indicates whether the <see cref="Transport_type"/>
		/// property value is null.
		/// </summary>
		/// <value>true if the property value is null, otherwise false.</value>
		public bool IsTransport_typeNull
		{
			get { return _transport_typeNull; }
			set { _transport_typeNull = value; }
		}

		/// <summary>
		/// Gets or sets the <c>node_status</c> column value.
		/// This column is nullable.
		/// </summary>
		/// <value>The <c>node_status</c> column value.</value>
		public byte Node_status
		{
			get
			{
				//if(IsNode_statusNull)
					//throw new InvalidOperationException("Cannot get value because it is DBNull.");
				return _node_status;
			}
			set
			{
				_node_statusNull = false;
				_node_status = value;
			}
		}

		/// <summary>
		/// Indicates whether the <see cref="Node_status"/>
		/// property value is null.
		/// </summary>
		/// <value>true if the property value is null, otherwise false.</value>
		public bool IsNode_statusNull
		{
			get { return _node_statusNull; }
			set { _node_statusNull = value; }
		}

		/// <summary>
		/// Gets or sets the <c>ip_address</c> column value.
		/// This column is nullable.
		/// </summary>
		/// <value>The <c>ip_address</c> column value.</value>
		public int Ip_address
		{
			get
			{
				//if(IsIp_addressNull)
					//throw new InvalidOperationException("Cannot get value because it is DBNull.");
				return _ip_address;
			}
			set
			{
				_ip_addressNull = false;
				_ip_address = value;
			}
		}

		/// <summary>
		/// Indicates whether the <see cref="Ip_address"/>
		/// property value is null.
		/// </summary>
		/// <value>true if the property value is null, otherwise false.</value>
		public bool IsIp_addressNull
		{
			get { return _ip_addressNull; }
			set { _ip_addressNull = value; }
		}

		/// <summary>
		/// Gets or sets the <c>dot_ip_address</c> column value.
		/// This column is nullable.
		/// </summary>
		/// <value>The <c>dot_ip_address</c> column value.</value>
		public string Dot_ip_address
		{
			get { return _dot_ip_address; }
			set { _dot_ip_address = value; }
		}

		/// <summary>
		/// Gets or sets the <c>platform_location</c> column value.
		/// This column is nullable.
		/// </summary>
		/// <value>The <c>platform_location</c> column value.</value>
		public string Platform_location
		{
			get { return _platform_location; }
			set { _platform_location = value; }
		}

		/// <summary>
		/// Gets or sets the <c>platform_status</c> column value.
		/// This column is nullable.
		/// </summary>
		/// <value>The <c>platform_status</c> column value.</value>
		public byte Platform_status
		{
			get
			{
				//if(IsPlatform_statusNull)
					//throw new InvalidOperationException("Cannot get value because it is DBNull.");
				return _platform_status;
			}
			set
			{
				_platform_statusNull = false;
				_platform_status = value;
			}
		}

		/// <summary>
		/// Indicates whether the <see cref="Platform_status"/>
		/// property value is null.
		/// </summary>
		/// <value>true if the property value is null, otherwise false.</value>
		public bool IsPlatform_statusNull
		{
			get { return _platform_statusNull; }
			set { _platform_statusNull = value; }
		}

		/// <summary>
		/// Gets or sets the <c>platform_config</c> column value.
		/// This column is nullable.
		/// </summary>
		/// <value>The <c>platform_config</c> column value.</value>
		public int Platform_config
		{
			get
			{
				//if(IsPlatform_configNull)
					//throw new InvalidOperationException("Cannot get value because it is DBNull.");
				return _platform_config;
			}
			set
			{
				_platform_configNull = false;
				_platform_config = value;
			}
		}

		/// <summary>
		/// Indicates whether the <see cref="Platform_config"/>
		/// property value is null.
		/// </summary>
		/// <value>true if the property value is null, otherwise false.</value>
		public bool IsPlatform_configNull
		{
			get { return _platform_configNull; }
			set { _platform_configNull = value; }
		}

		/// <summary>
		/// Gets or sets the <c>customer_acct_name</c> column value.
		/// This column is nullable.
		/// </summary>
		/// <value>The <c>customer_acct_name</c> column value.</value>
		public string Customer_acct_name
		{
			get { return _customer_acct_name; }
			set { _customer_acct_name = value; }
		}

		/// <summary>
		/// Gets or sets the <c>customer_acct_status</c> column value.
		/// This column is nullable.
		/// </summary>
		/// <value>The <c>customer_acct_status</c> column value.</value>
		public byte Customer_acct_status
		{
			get
			{
				//if(IsCustomer_acct_statusNull)
					//throw new InvalidOperationException("Cannot get value because it is DBNull.");
				return _customer_acct_status;
			}
			set
			{
				_customer_acct_statusNull = false;
				_customer_acct_status = value;
			}
		}

		/// <summary>
		/// Indicates whether the <see cref="Customer_acct_status"/>
		/// property value is null.
		/// </summary>
		/// <value>true if the property value is null, otherwise false.</value>
		public bool IsCustomer_acct_statusNull
		{
			get { return _customer_acct_statusNull; }
			set { _customer_acct_statusNull = value; }
		}

		/// <summary>
		/// Gets or sets the <c>prefix_in</c> column value.
		/// This column is nullable.
		/// </summary>
		/// <value>The <c>prefix_in</c> column value.</value>
		public string Prefix_in
		{
			get { return _prefix_in; }
			set { _prefix_in = value; }
		}

		/// <summary>
		/// Gets or sets the <c>prefix_out</c> column value.
		/// This column is nullable.
		/// </summary>
		/// <value>The <c>prefix_out</c> column value.</value>
		public string Prefix_out
		{
			get { return _prefix_out; }
			set { _prefix_out = value; }
		}

		/// <summary>
		/// Gets or sets the <c>prefix_in_type_id</c> column value.
		/// This column is nullable.
		/// </summary>
		/// <value>The <c>prefix_in_type_id</c> column value.</value>
		public short Prefix_in_type_id
		{
			get
			{
				//if(IsPrefix_in_type_idNull)
					//throw new InvalidOperationException("Cannot get value because it is DBNull.");
				return _prefix_in_type_id;
			}
			set
			{
				_prefix_in_type_idNull = false;
				_prefix_in_type_id = value;
			}
		}

		/// <summary>
		/// Indicates whether the <see cref="Prefix_in_type_id"/>
		/// property value is null.
		/// </summary>
		/// <value>true if the property value is null, otherwise false.</value>
		public bool IsPrefix_in_type_idNull
		{
			get { return _prefix_in_type_idNull; }
			set { _prefix_in_type_idNull = value; }
		}

		/// <summary>
		/// Gets or sets the <c>partner_id</c> column value.
		/// This column is nullable.
		/// </summary>
		/// <value>The <c>partner_id</c> column value.</value>
		public int Partner_id
		{
			get
			{
				//if(IsPartner_idNull)
					//throw new InvalidOperationException("Cannot get value because it is DBNull.");
				return _partner_id;
			}
			set
			{
				_partner_idNull = false;
				_partner_id = value;
			}
		}

		/// <summary>
		/// Indicates whether the <see cref="Partner_id"/>
		/// property value is null.
		/// </summary>
		/// <value>true if the property value is null, otherwise false.</value>
		public bool IsPartner_idNull
		{
			get { return _partner_idNull; }
			set { _partner_idNull = value; }
		}

		/// <summary>
		/// Gets or sets the <c>partner_name</c> column value.
		/// This column is nullable.
		/// </summary>
		/// <value>The <c>partner_name</c> column value.</value>
		public string Partner_name
		{
			get { return _partner_name; }
			set { _partner_name = value; }
		}

		/// <summary>
		/// Gets or sets the <c>partner_status</c> column value.
		/// This column is nullable.
		/// </summary>
		/// <value>The <c>partner_status</c> column value.</value>
		public byte Partner_status
		{
			get
			{
				//if(IsPartner_statusNull)
					//throw new InvalidOperationException("Cannot get value because it is DBNull.");
				return _partner_status;
			}
			set
			{
				_partner_statusNull = false;
				_partner_status = value;
			}
		}

		/// <summary>
		/// Indicates whether the <see cref="Partner_status"/>
		/// property value is null.
		/// </summary>
		/// <value>true if the property value is null, otherwise false.</value>
		public bool IsPartner_statusNull
		{
			get { return _partner_statusNull; }
			set { _partner_statusNull = value; }
		}

		/// <summary>
		/// Gets or sets the <c>service_type</c> column value.
		/// This column is nullable.
		/// </summary>
		/// <value>The <c>service_type</c> column value.</value>
		public byte Service_type
		{
			get
			{
				//if(IsService_typeNull)
					//throw new InvalidOperationException("Cannot get value because it is DBNull.");
				return _service_type;
			}
			set
			{
				_service_typeNull = false;
				_service_type = value;
			}
		}

		/// <summary>
		/// Indicates whether the <see cref="Service_type"/>
		/// property value is null.
		/// </summary>
		/// <value>true if the property value is null, otherwise false.</value>
		public bool IsService_typeNull
		{
			get { return _service_typeNull; }
			set { _service_typeNull = value; }
		}

		/// <summary>
		/// Gets or sets the <c>service_retail_type</c> column value.
		/// This column is nullable.
		/// </summary>
		/// <value>The <c>service_retail_type</c> column value.</value>
		public int Service_retail_type
		{
			get
			{
				//if(IsService_retail_typeNull)
					//throw new InvalidOperationException("Cannot get value because it is DBNull.");
				return _service_retail_type;
			}
			set
			{
				_service_retail_typeNull = false;
				_service_retail_type = value;
			}
		}

		/// <summary>
		/// Indicates whether the <see cref="Service_retail_type"/>
		/// property value is null.
		/// </summary>
		/// <value>true if the property value is null, otherwise false.</value>
		public bool IsService_retail_typeNull
		{
			get { return _service_retail_typeNull; }
			set { _service_retail_typeNull = value; }
		}

		/// <summary>
		/// Gets or sets the <c>is_prepaid</c> column value.
		/// This column is nullable.
		/// </summary>
		/// <value>The <c>is_prepaid</c> column value.</value>
		public byte Is_prepaid
		{
			get
			{
				//if(IsIs_prepaidNull)
					//throw new InvalidOperationException("Cannot get value because it is DBNull.");
				return _is_prepaid;
			}
			set
			{
				_is_prepaidNull = false;
				_is_prepaid = value;
			}
		}

		/// <summary>
		/// Indicates whether the <see cref="Is_prepaid"/>
		/// property value is null.
		/// </summary>
		/// <value>true if the property value is null, otherwise false.</value>
		public bool IsIs_prepaidNull
		{
			get { return _is_prepaidNull; }
			set { _is_prepaidNull = value; }
		}

		/// <summary>
		/// Gets or sets the <c>is_shared_service</c> column value.
		/// This column is nullable.
		/// </summary>
		/// <value>The <c>is_shared_service</c> column value.</value>
		public byte Is_shared_service
		{
			get
			{
				//if(IsIs_shared_serviceNull)
					//throw new InvalidOperationException("Cannot get value because it is DBNull.");
				return _is_shared_service;
			}
			set
			{
				_is_shared_serviceNull = false;
				_is_shared_service = value;
			}
		}

		/// <summary>
		/// Indicates whether the <see cref="Is_shared_service"/>
		/// property value is null.
		/// </summary>
		/// <value>true if the property value is null, otherwise false.</value>
		public bool IsIs_shared_serviceNull
		{
			get { return _is_shared_serviceNull; }
			set { _is_shared_serviceNull = value; }
		}

		/// <summary>
		/// Returns the string representation of this instance.
		/// </summary>
		/// <returns>The string representation of this instance.</returns>
		public override string ToString()
		{
			System.Text.StringBuilder dynStr = new System.Text.StringBuilder(GetType().Name);
			dynStr.Append(':');
			dynStr.Append("  Node_id=");
			dynStr.Append(Node_id);
			dynStr.Append("  Customer_acct_id=");
			dynStr.Append(Customer_acct_id);
			dynStr.Append("  Max_calls=");
			dynStr.Append(Max_calls);
			dynStr.Append("  Current_calls=");
			dynStr.Append(Current_calls);
			dynStr.Append("  Platform_id=");
			dynStr.Append(IsPlatform_idNull ? (object)"<NULL>" : Platform_id);
			dynStr.Append("  Node_description=");
			dynStr.Append(Node_description);
			dynStr.Append("  Node_config=");
			dynStr.Append(IsNode_configNull ? (object)"<NULL>" : Node_config);
			dynStr.Append("  Transport_type=");
			dynStr.Append(IsTransport_typeNull ? (object)"<NULL>" : Transport_type);
			dynStr.Append("  Node_status=");
			dynStr.Append(IsNode_statusNull ? (object)"<NULL>" : Node_status);
			dynStr.Append("  Ip_address=");
			dynStr.Append(IsIp_addressNull ? (object)"<NULL>" : Ip_address);
			dynStr.Append("  Dot_ip_address=");
			dynStr.Append(Dot_ip_address);
			dynStr.Append("  Platform_location=");
			dynStr.Append(Platform_location);
			dynStr.Append("  Platform_status=");
			dynStr.Append(IsPlatform_statusNull ? (object)"<NULL>" : Platform_status);
			dynStr.Append("  Platform_config=");
			dynStr.Append(IsPlatform_configNull ? (object)"<NULL>" : Platform_config);
			dynStr.Append("  Customer_acct_name=");
			dynStr.Append(Customer_acct_name);
			dynStr.Append("  Customer_acct_status=");
			dynStr.Append(IsCustomer_acct_statusNull ? (object)"<NULL>" : Customer_acct_status);
			dynStr.Append("  Prefix_in=");
			dynStr.Append(Prefix_in);
			dynStr.Append("  Prefix_out=");
			dynStr.Append(Prefix_out);
			dynStr.Append("  Prefix_in_type_id=");
			dynStr.Append(IsPrefix_in_type_idNull ? (object)"<NULL>" : Prefix_in_type_id);
			dynStr.Append("  Partner_id=");
			dynStr.Append(IsPartner_idNull ? (object)"<NULL>" : Partner_id);
			dynStr.Append("  Partner_name=");
			dynStr.Append(Partner_name);
			dynStr.Append("  Partner_status=");
			dynStr.Append(IsPartner_statusNull ? (object)"<NULL>" : Partner_status);
			dynStr.Append("  Service_type=");
			dynStr.Append(IsService_typeNull ? (object)"<NULL>" : Service_type);
			dynStr.Append("  Service_retail_type=");
			dynStr.Append(IsService_retail_typeNull ? (object)"<NULL>" : Service_retail_type);
			dynStr.Append("  Is_prepaid=");
			dynStr.Append(IsIs_prepaidNull ? (object)"<NULL>" : Is_prepaid);
			dynStr.Append("  Is_shared_service=");
			dynStr.Append(IsIs_shared_serviceNull ? (object)"<NULL>" : Is_shared_service);
			return dynStr.ToString();
		}

	#region Timok Custom

		public override bool Equals(object obj) {
			if(obj == null || obj.GetType() != this.GetType())
				return false;

			return ObjectComparer.AreEqual(this, obj);
		}

		public override int GetHashCode() {
			//TODO: finish it, get hashes for all fields
			return this.ToString().GetHashCode();
		}
	#endregion Timok Custom

	} // End of LoadBalancingMapViewRow_Base class
} // End of namespace

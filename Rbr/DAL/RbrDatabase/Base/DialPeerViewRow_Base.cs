// <fileinfo name="Base\DialPeerViewRow_Base.cs">
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
	/// The base class for <see cref="DialPeerViewRow"/> that 
	/// represents a record in the <c>DialPeerView</c> view.
	/// </summary>
	/// <remarks>
	/// Do not change this source code manually. Update the <see cref="DialPeerViewRow"/>
	/// class if you need to add or change some functionality.
	/// </remarks>
	[Serializable]
	public abstract class DialPeerViewRow_Base
	{
	#region Timok Custom

		//db field names
		public const string end_point_id_DbName = "end_point_id";
		public const string prefix_in_DbName = "prefix_in";
		public const string customer_acct_id_DbName = "customer_acct_id";
		public const string alias_DbName = "alias";
		public const string prefix_type_descr_DbName = "prefix_type_descr";
		public const string prefix_type_length_DbName = "prefix_type_length";
		public const string prefix_type_delimiter_DbName = "prefix_type_delimiter";
		public const string customer_acct_name_DbName = "customer_acct_name";
		public const string service_id_DbName = "service_id";
		public const string customer_acct_status_DbName = "customer_acct_status";
		public const string prefix_out_DbName = "prefix_out";
		public const string partner_id_DbName = "partner_id";
		public const string allow_rerouting_DbName = "allow_rerouting";
		public const string service_name_DbName = "service_name";
		public const string service_type_DbName = "service_type";
		public const string service_retail_type_DbName = "service_retail_type";
		public const string partner_name_DbName = "partner_name";
		public const string partner_status_DbName = "partner_status";

		//prop names
		public const string end_point_id_PropName = "End_point_id";
		public const string prefix_in_PropName = "Prefix_in";
		public const string customer_acct_id_PropName = "Customer_acct_id";
		public const string alias_PropName = "Alias";
		public const string prefix_type_descr_PropName = "Prefix_type_descr";
		public const string prefix_type_length_PropName = "Prefix_type_length";
		public const string prefix_type_delimiter_PropName = "Prefix_type_delimiter";
		public const string customer_acct_name_PropName = "Customer_acct_name";
		public const string service_id_PropName = "Service_id";
		public const string customer_acct_status_PropName = "Customer_acct_status";
		public const string prefix_out_PropName = "Prefix_out";
		public const string partner_id_PropName = "Partner_id";
		public const string allow_rerouting_PropName = "Allow_rerouting";
		public const string service_name_PropName = "Service_name";
		public const string service_type_PropName = "Service_type";
		public const string service_retail_type_PropName = "Service_retail_type";
		public const string partner_name_PropName = "Partner_name";
		public const string partner_status_PropName = "Partner_status";

		//db field display names
		public const string end_point_id_DisplayName = "end point id";
		public const string prefix_in_DisplayName = "prefix in";
		public const string customer_acct_id_DisplayName = "customer acct id";
		public const string alias_DisplayName = "alias";
		public const string prefix_type_descr_DisplayName = "prefix type descr";
		public const string prefix_type_length_DisplayName = "prefix type length";
		public const string prefix_type_delimiter_DisplayName = "prefix type delimiter";
		public const string customer_acct_name_DisplayName = "customer acct name";
		public const string service_id_DisplayName = "service id";
		public const string customer_acct_status_DisplayName = "customer acct status";
		public const string prefix_out_DisplayName = "prefix out";
		public const string partner_id_DisplayName = "partner id";
		public const string allow_rerouting_DisplayName = "allow rerouting";
		public const string service_name_DisplayName = "service name";
		public const string service_type_DisplayName = "service type";
		public const string service_retail_type_DisplayName = "service retail type";
		public const string partner_name_DisplayName = "partner name";
		public const string partner_status_DisplayName = "partner status";
	#endregion Timok Custom


		private short _end_point_id;
		private string _prefix_in;
		private short _customer_acct_id;
		private string _alias;
		private string _prefix_type_descr;
		private byte _prefix_type_length;
		private bool _prefix_type_lengthNull = true;
		private byte _prefix_type_delimiter;
		private bool _prefix_type_delimiterNull = true;
		private string _customer_acct_name;
		private short _service_id;
		private bool _service_idNull = true;
		private byte _customer_acct_status;
		private bool _customer_acct_statusNull = true;
		private string _prefix_out;
		private int _partner_id;
		private bool _partner_idNull = true;
		private byte _allow_rerouting;
		private bool _allow_reroutingNull = true;
		private string _service_name;
		private byte _service_type;
		private bool _service_typeNull = true;
		private int _service_retail_type;
		private bool _service_retail_typeNull = true;
		private string _partner_name;
		private byte _partner_status;
		private bool _partner_statusNull = true;

		/// <summary>
		/// Initializes a new instance of the <see cref="DialPeerViewRow_Base"/> class.
		/// </summary>
		public DialPeerViewRow_Base()
		{
			// EMPTY
		}

		/// <summary>
		/// Gets or sets the <c>end_point_id</c> column value.
		/// </summary>
		/// <value>The <c>end_point_id</c> column value.</value>
		public short End_point_id
		{
			get { return _end_point_id; }
			set { _end_point_id = value; }
		}

		/// <summary>
		/// Gets or sets the <c>prefix_in</c> column value.
		/// </summary>
		/// <value>The <c>prefix_in</c> column value.</value>
		public string Prefix_in
		{
			get { return _prefix_in; }
			set { _prefix_in = value; }
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
		/// Gets or sets the <c>alias</c> column value.
		/// This column is nullable.
		/// </summary>
		/// <value>The <c>alias</c> column value.</value>
		public string Alias
		{
			get { return _alias; }
			set { _alias = value; }
		}

		/// <summary>
		/// Gets or sets the <c>prefix_type_descr</c> column value.
		/// This column is nullable.
		/// </summary>
		/// <value>The <c>prefix_type_descr</c> column value.</value>
		public string Prefix_type_descr
		{
			get { return _prefix_type_descr; }
			set { _prefix_type_descr = value; }
		}

		/// <summary>
		/// Gets or sets the <c>prefix_type_length</c> column value.
		/// This column is nullable.
		/// </summary>
		/// <value>The <c>prefix_type_length</c> column value.</value>
		public byte Prefix_type_length
		{
			get
			{
				//if(IsPrefix_type_lengthNull)
					//throw new InvalidOperationException("Cannot get value because it is DBNull.");
				return _prefix_type_length;
			}
			set
			{
				_prefix_type_lengthNull = false;
				_prefix_type_length = value;
			}
		}

		/// <summary>
		/// Indicates whether the <see cref="Prefix_type_length"/>
		/// property value is null.
		/// </summary>
		/// <value>true if the property value is null, otherwise false.</value>
		public bool IsPrefix_type_lengthNull
		{
			get { return _prefix_type_lengthNull; }
			set { _prefix_type_lengthNull = value; }
		}

		/// <summary>
		/// Gets or sets the <c>prefix_type_delimiter</c> column value.
		/// This column is nullable.
		/// </summary>
		/// <value>The <c>prefix_type_delimiter</c> column value.</value>
		public byte Prefix_type_delimiter
		{
			get
			{
				//if(IsPrefix_type_delimiterNull)
					//throw new InvalidOperationException("Cannot get value because it is DBNull.");
				return _prefix_type_delimiter;
			}
			set
			{
				_prefix_type_delimiterNull = false;
				_prefix_type_delimiter = value;
			}
		}

		/// <summary>
		/// Indicates whether the <see cref="Prefix_type_delimiter"/>
		/// property value is null.
		/// </summary>
		/// <value>true if the property value is null, otherwise false.</value>
		public bool IsPrefix_type_delimiterNull
		{
			get { return _prefix_type_delimiterNull; }
			set { _prefix_type_delimiterNull = value; }
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
		/// Gets or sets the <c>service_id</c> column value.
		/// This column is nullable.
		/// </summary>
		/// <value>The <c>service_id</c> column value.</value>
		public short Service_id
		{
			get
			{
				//if(IsService_idNull)
					//throw new InvalidOperationException("Cannot get value because it is DBNull.");
				return _service_id;
			}
			set
			{
				_service_idNull = false;
				_service_id = value;
			}
		}

		/// <summary>
		/// Indicates whether the <see cref="Service_id"/>
		/// property value is null.
		/// </summary>
		/// <value>true if the property value is null, otherwise false.</value>
		public bool IsService_idNull
		{
			get { return _service_idNull; }
			set { _service_idNull = value; }
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
		/// Gets or sets the <c>allow_rerouting</c> column value.
		/// This column is nullable.
		/// </summary>
		/// <value>The <c>allow_rerouting</c> column value.</value>
		public byte Allow_rerouting
		{
			get
			{
				//if(IsAllow_reroutingNull)
					//throw new InvalidOperationException("Cannot get value because it is DBNull.");
				return _allow_rerouting;
			}
			set
			{
				_allow_reroutingNull = false;
				_allow_rerouting = value;
			}
		}

		/// <summary>
		/// Indicates whether the <see cref="Allow_rerouting"/>
		/// property value is null.
		/// </summary>
		/// <value>true if the property value is null, otherwise false.</value>
		public bool IsAllow_reroutingNull
		{
			get { return _allow_reroutingNull; }
			set { _allow_reroutingNull = value; }
		}

		/// <summary>
		/// Gets or sets the <c>service_name</c> column value.
		/// This column is nullable.
		/// </summary>
		/// <value>The <c>service_name</c> column value.</value>
		public string Service_name
		{
			get { return _service_name; }
			set { _service_name = value; }
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
		/// Returns the string representation of this instance.
		/// </summary>
		/// <returns>The string representation of this instance.</returns>
		public override string ToString()
		{
			System.Text.StringBuilder dynStr = new System.Text.StringBuilder(GetType().Name);
			dynStr.Append(':');
			dynStr.Append("  End_point_id=");
			dynStr.Append(End_point_id);
			dynStr.Append("  Prefix_in=");
			dynStr.Append(Prefix_in);
			dynStr.Append("  Customer_acct_id=");
			dynStr.Append(Customer_acct_id);
			dynStr.Append("  Alias=");
			dynStr.Append(Alias);
			dynStr.Append("  Prefix_type_descr=");
			dynStr.Append(Prefix_type_descr);
			dynStr.Append("  Prefix_type_length=");
			dynStr.Append(IsPrefix_type_lengthNull ? (object)"<NULL>" : Prefix_type_length);
			dynStr.Append("  Prefix_type_delimiter=");
			dynStr.Append(IsPrefix_type_delimiterNull ? (object)"<NULL>" : Prefix_type_delimiter);
			dynStr.Append("  Customer_acct_name=");
			dynStr.Append(Customer_acct_name);
			dynStr.Append("  Service_id=");
			dynStr.Append(IsService_idNull ? (object)"<NULL>" : Service_id);
			dynStr.Append("  Customer_acct_status=");
			dynStr.Append(IsCustomer_acct_statusNull ? (object)"<NULL>" : Customer_acct_status);
			dynStr.Append("  Prefix_out=");
			dynStr.Append(Prefix_out);
			dynStr.Append("  Partner_id=");
			dynStr.Append(IsPartner_idNull ? (object)"<NULL>" : Partner_id);
			dynStr.Append("  Allow_rerouting=");
			dynStr.Append(IsAllow_reroutingNull ? (object)"<NULL>" : Allow_rerouting);
			dynStr.Append("  Service_name=");
			dynStr.Append(Service_name);
			dynStr.Append("  Service_type=");
			dynStr.Append(IsService_typeNull ? (object)"<NULL>" : Service_type);
			dynStr.Append("  Service_retail_type=");
			dynStr.Append(IsService_retail_typeNull ? (object)"<NULL>" : Service_retail_type);
			dynStr.Append("  Partner_name=");
			dynStr.Append(Partner_name);
			dynStr.Append("  Partner_status=");
			dynStr.Append(IsPartner_statusNull ? (object)"<NULL>" : Partner_status);
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

	} // End of DialPeerViewRow_Base class
} // End of namespace

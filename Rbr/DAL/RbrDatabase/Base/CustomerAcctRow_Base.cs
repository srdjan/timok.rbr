// <fileinfo name="Base\CustomerAcctRow_Base.cs">
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
	/// The base class for <see cref="CustomerAcctRow"/> that 
	/// represents a record in the <c>CustomerAcct</c> table.
	/// </summary>
	/// <remarks>
	/// Do not change this source code manually. Update the <see cref="CustomerAcctRow"/>
	/// class if you need to add or change some functionality.
	/// </remarks>
	[Serializable]
	public abstract class CustomerAcctRow_Base
	{
	#region Timok Custom

		//db field names
		public const string customer_acct_id_DbName = "customer_acct_id";
		public const string name_DbName = "name";
		public const string status_DbName = "status";
		public const string default_bonus_minutes_type_DbName = "default_bonus_minutes_type";
		public const string default_start_bonus_minutes_DbName = "default_start_bonus_minutes";
		public const string is_prepaid_DbName = "is_prepaid";
		public const string current_amount_DbName = "current_amount";
		public const string limit_amount_DbName = "limit_amount";
		public const string warning_amount_DbName = "warning_amount";
		public const string allow_rerouting_DbName = "allow_rerouting";
		public const string concurrent_use_DbName = "concurrent_use";
		public const string prefix_in_type_id_DbName = "prefix_in_type_id";
		public const string prefix_in_DbName = "prefix_in";
		public const string prefix_out_DbName = "prefix_out";
		public const string partner_id_DbName = "partner_id";
		public const string service_id_DbName = "service_id";
		public const string retail_calling_plan_id_DbName = "retail_calling_plan_id";
		public const string retail_markup_type_DbName = "retail_markup_type";
		public const string retail_markup_dollar_DbName = "retail_markup_dollar";
		public const string retail_markup_percent_DbName = "retail_markup_percent";
		public const string max_call_length_DbName = "max_call_length";
		public const string routing_plan_id_DbName = "routing_plan_id";

		//prop names
		public const string customer_acct_id_PropName = "Customer_acct_id";
		public const string name_PropName = "Name";
		public const string status_PropName = "Status";
		public const string default_bonus_minutes_type_PropName = "Default_bonus_minutes_type";
		public const string default_start_bonus_minutes_PropName = "Default_start_bonus_minutes";
		public const string is_prepaid_PropName = "Is_prepaid";
		public const string current_amount_PropName = "Current_amount";
		public const string limit_amount_PropName = "Limit_amount";
		public const string warning_amount_PropName = "Warning_amount";
		public const string allow_rerouting_PropName = "Allow_rerouting";
		public const string concurrent_use_PropName = "Concurrent_use";
		public const string prefix_in_type_id_PropName = "Prefix_in_type_id";
		public const string prefix_in_PropName = "Prefix_in";
		public const string prefix_out_PropName = "Prefix_out";
		public const string partner_id_PropName = "Partner_id";
		public const string service_id_PropName = "Service_id";
		public const string retail_calling_plan_id_PropName = "Retail_calling_plan_id";
		public const string retail_markup_type_PropName = "Retail_markup_type";
		public const string retail_markup_dollar_PropName = "Retail_markup_dollar";
		public const string retail_markup_percent_PropName = "Retail_markup_percent";
		public const string max_call_length_PropName = "Max_call_length";
		public const string routing_plan_id_PropName = "Routing_plan_id";

		//db field display names
		public const string customer_acct_id_DisplayName = "customer acct id";
		public const string name_DisplayName = "name";
		public const string status_DisplayName = "status";
		public const string default_bonus_minutes_type_DisplayName = "default bonus minutes type";
		public const string default_start_bonus_minutes_DisplayName = "default start bonus minutes";
		public const string is_prepaid_DisplayName = "is prepaid";
		public const string current_amount_DisplayName = "current amount";
		public const string limit_amount_DisplayName = "limit amount";
		public const string warning_amount_DisplayName = "warning amount";
		public const string allow_rerouting_DisplayName = "allow rerouting";
		public const string concurrent_use_DisplayName = "concurrent use";
		public const string prefix_in_type_id_DisplayName = "prefix in type id";
		public const string prefix_in_DisplayName = "prefix in";
		public const string prefix_out_DisplayName = "prefix out";
		public const string partner_id_DisplayName = "partner id";
		public const string service_id_DisplayName = "service id";
		public const string retail_calling_plan_id_DisplayName = "retail calling plan id";
		public const string retail_markup_type_DisplayName = "retail markup type";
		public const string retail_markup_dollar_DisplayName = "retail markup dollar";
		public const string retail_markup_percent_DisplayName = "retail markup percent";
		public const string max_call_length_DisplayName = "max call length";
		public const string routing_plan_id_DisplayName = "routing plan id";
	#endregion Timok Custom


		private short _customer_acct_id;
		private string _name;
		private byte _status;
		private byte _default_bonus_minutes_type;
		private short _default_start_bonus_minutes;
		private byte _is_prepaid;
		private decimal _current_amount;
		private decimal _limit_amount;
		private decimal _warning_amount;
		private byte _allow_rerouting;
		private byte _concurrent_use;
		private short _prefix_in_type_id;
		private string _prefix_in;
		private string _prefix_out;
		private int _partner_id;
		private short _service_id;
		private int _retail_calling_plan_id;
		private bool _retail_calling_plan_idNull = true;
		private byte _retail_markup_type;
		private decimal _retail_markup_dollar;
		private decimal _retail_markup_percent;
		private short _max_call_length;
		private int _routing_plan_id;

		/// <summary>
		/// Initializes a new instance of the <see cref="CustomerAcctRow_Base"/> class.
		/// </summary>
		public CustomerAcctRow_Base()
		{
			// EMPTY
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
		/// Gets or sets the <c>name</c> column value.
		/// </summary>
		/// <value>The <c>name</c> column value.</value>
		public string Name
		{
			get { return _name; }
			set { _name = value; }
		}

		/// <summary>
		/// Gets or sets the <c>status</c> column value.
		/// </summary>
		/// <value>The <c>status</c> column value.</value>
		public byte Status
		{
			get { return _status; }
			set { _status = value; }
		}

		/// <summary>
		/// Gets or sets the <c>default_bonus_minutes_type</c> column value.
		/// </summary>
		/// <value>The <c>default_bonus_minutes_type</c> column value.</value>
		public byte Default_bonus_minutes_type
		{
			get { return _default_bonus_minutes_type; }
			set { _default_bonus_minutes_type = value; }
		}

		/// <summary>
		/// Gets or sets the <c>default_start_bonus_minutes</c> column value.
		/// </summary>
		/// <value>The <c>default_start_bonus_minutes</c> column value.</value>
		public short Default_start_bonus_minutes
		{
			get { return _default_start_bonus_minutes; }
			set { _default_start_bonus_minutes = value; }
		}

		/// <summary>
		/// Gets or sets the <c>is_prepaid</c> column value.
		/// </summary>
		/// <value>The <c>is_prepaid</c> column value.</value>
		public byte Is_prepaid
		{
			get { return _is_prepaid; }
			set { _is_prepaid = value; }
		}

		/// <summary>
		/// Gets or sets the <c>current_amount</c> column value.
		/// </summary>
		/// <value>The <c>current_amount</c> column value.</value>
		public decimal Current_amount
		{
			get { return _current_amount; }
			set { _current_amount = value; }
		}

		/// <summary>
		/// Gets or sets the <c>limit_amount</c> column value.
		/// </summary>
		/// <value>The <c>limit_amount</c> column value.</value>
		public decimal Limit_amount
		{
			get { return _limit_amount; }
			set { _limit_amount = value; }
		}

		/// <summary>
		/// Gets or sets the <c>warning_amount</c> column value.
		/// </summary>
		/// <value>The <c>warning_amount</c> column value.</value>
		public decimal Warning_amount
		{
			get { return _warning_amount; }
			set { _warning_amount = value; }
		}

		/// <summary>
		/// Gets or sets the <c>allow_rerouting</c> column value.
		/// </summary>
		/// <value>The <c>allow_rerouting</c> column value.</value>
		public byte Allow_rerouting
		{
			get { return _allow_rerouting; }
			set { _allow_rerouting = value; }
		}

		/// <summary>
		/// Gets or sets the <c>concurrent_use</c> column value.
		/// </summary>
		/// <value>The <c>concurrent_use</c> column value.</value>
		public byte Concurrent_use
		{
			get { return _concurrent_use; }
			set { _concurrent_use = value; }
		}

		/// <summary>
		/// Gets or sets the <c>prefix_in_type_id</c> column value.
		/// </summary>
		/// <value>The <c>prefix_in_type_id</c> column value.</value>
		public short Prefix_in_type_id
		{
			get { return _prefix_in_type_id; }
			set { _prefix_in_type_id = value; }
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
		/// Gets or sets the <c>prefix_out</c> column value.
		/// </summary>
		/// <value>The <c>prefix_out</c> column value.</value>
		public string Prefix_out
		{
			get { return _prefix_out; }
			set { _prefix_out = value; }
		}

		/// <summary>
		/// Gets or sets the <c>partner_id</c> column value.
		/// </summary>
		/// <value>The <c>partner_id</c> column value.</value>
		public int Partner_id
		{
			get { return _partner_id; }
			set { _partner_id = value; }
		}

		/// <summary>
		/// Gets or sets the <c>service_id</c> column value.
		/// </summary>
		/// <value>The <c>service_id</c> column value.</value>
		public short Service_id
		{
			get { return _service_id; }
			set { _service_id = value; }
		}

		/// <summary>
		/// Gets or sets the <c>retail_calling_plan_id</c> column value.
		/// This column is nullable.
		/// </summary>
		/// <value>The <c>retail_calling_plan_id</c> column value.</value>
		public int Retail_calling_plan_id
		{
			get
			{
				//if(IsRetail_calling_plan_idNull)
					//throw new InvalidOperationException("Cannot get value because it is DBNull.");
				return _retail_calling_plan_id;
			}
			set
			{
				_retail_calling_plan_idNull = false;
				_retail_calling_plan_id = value;
			}
		}

		/// <summary>
		/// Indicates whether the <see cref="Retail_calling_plan_id"/>
		/// property value is null.
		/// </summary>
		/// <value>true if the property value is null, otherwise false.</value>
		public bool IsRetail_calling_plan_idNull
		{
			get { return _retail_calling_plan_idNull; }
			set { _retail_calling_plan_idNull = value; }
		}

		/// <summary>
		/// Gets or sets the <c>retail_markup_type</c> column value.
		/// </summary>
		/// <value>The <c>retail_markup_type</c> column value.</value>
		public byte Retail_markup_type
		{
			get { return _retail_markup_type; }
			set { _retail_markup_type = value; }
		}

		/// <summary>
		/// Gets or sets the <c>retail_markup_dollar</c> column value.
		/// </summary>
		/// <value>The <c>retail_markup_dollar</c> column value.</value>
		public decimal Retail_markup_dollar
		{
			get { return _retail_markup_dollar; }
			set { _retail_markup_dollar = value; }
		}

		/// <summary>
		/// Gets or sets the <c>retail_markup_percent</c> column value.
		/// </summary>
		/// <value>The <c>retail_markup_percent</c> column value.</value>
		public decimal Retail_markup_percent
		{
			get { return _retail_markup_percent; }
			set { _retail_markup_percent = value; }
		}

		/// <summary>
		/// Gets or sets the <c>max_call_length</c> column value.
		/// </summary>
		/// <value>The <c>max_call_length</c> column value.</value>
		public short Max_call_length
		{
			get { return _max_call_length; }
			set { _max_call_length = value; }
		}

		/// <summary>
		/// Gets or sets the <c>routing_plan_id</c> column value.
		/// </summary>
		/// <value>The <c>routing_plan_id</c> column value.</value>
		public int Routing_plan_id
		{
			get { return _routing_plan_id; }
			set { _routing_plan_id = value; }
		}

		/// <summary>
		/// Returns the string representation of this instance.
		/// </summary>
		/// <returns>The string representation of this instance.</returns>
		public override string ToString()
		{
			System.Text.StringBuilder dynStr = new System.Text.StringBuilder(GetType().Name);
			dynStr.Append(':');
			dynStr.Append("  Customer_acct_id=");
			dynStr.Append(Customer_acct_id);
			dynStr.Append("  Name=");
			dynStr.Append(Name);
			dynStr.Append("  Status=");
			dynStr.Append(Status);
			dynStr.Append("  Default_bonus_minutes_type=");
			dynStr.Append(Default_bonus_minutes_type);
			dynStr.Append("  Default_start_bonus_minutes=");
			dynStr.Append(Default_start_bonus_minutes);
			dynStr.Append("  Is_prepaid=");
			dynStr.Append(Is_prepaid);
			dynStr.Append("  Current_amount=");
			dynStr.Append(Current_amount);
			dynStr.Append("  Limit_amount=");
			dynStr.Append(Limit_amount);
			dynStr.Append("  Warning_amount=");
			dynStr.Append(Warning_amount);
			dynStr.Append("  Allow_rerouting=");
			dynStr.Append(Allow_rerouting);
			dynStr.Append("  Concurrent_use=");
			dynStr.Append(Concurrent_use);
			dynStr.Append("  Prefix_in_type_id=");
			dynStr.Append(Prefix_in_type_id);
			dynStr.Append("  Prefix_in=");
			dynStr.Append(Prefix_in);
			dynStr.Append("  Prefix_out=");
			dynStr.Append(Prefix_out);
			dynStr.Append("  Partner_id=");
			dynStr.Append(Partner_id);
			dynStr.Append("  Service_id=");
			dynStr.Append(Service_id);
			dynStr.Append("  Retail_calling_plan_id=");
			dynStr.Append(IsRetail_calling_plan_idNull ? (object)"<NULL>" : Retail_calling_plan_id);
			dynStr.Append("  Retail_markup_type=");
			dynStr.Append(Retail_markup_type);
			dynStr.Append("  Retail_markup_dollar=");
			dynStr.Append(Retail_markup_dollar);
			dynStr.Append("  Retail_markup_percent=");
			dynStr.Append(Retail_markup_percent);
			dynStr.Append("  Max_call_length=");
			dynStr.Append(Max_call_length);
			dynStr.Append("  Routing_plan_id=");
			dynStr.Append(Routing_plan_id);
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

	} // End of CustomerAcctRow_Base class
} // End of namespace

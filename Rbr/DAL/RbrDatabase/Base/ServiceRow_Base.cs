// <fileinfo name="Base\ServiceRow_Base.cs">
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
	/// The base class for <see cref="ServiceRow"/> that 
	/// represents a record in the <c>Service</c> table.
	/// </summary>
	/// <remarks>
	/// Do not change this source code manually. Update the <see cref="ServiceRow"/>
	/// class if you need to add or change some functionality.
	/// </remarks>
	[Serializable]
	public abstract class ServiceRow_Base
	{
	#region Timok Custom

		//db field names
		public const string service_id_DbName = "service_id";
		public const string name_DbName = "name";
		public const string virtual_switch_id_DbName = "virtual_switch_id";
		public const string calling_plan_id_DbName = "calling_plan_id";
		public const string default_routing_plan_id_DbName = "default_routing_plan_id";
		public const string status_DbName = "status";
		public const string type_DbName = "type";
		public const string retail_type_DbName = "retail_type";
		public const string is_shared_DbName = "is_shared";
		public const string rating_type_DbName = "rating_type";
		public const string pin_length_DbName = "pin_length";
		public const string payphone_surcharge_id_DbName = "payphone_surcharge_id";
		public const string sweep_schedule_id_DbName = "sweep_schedule_id";
		public const string sweep_fee_DbName = "sweep_fee";
		public const string sweep_rule_DbName = "sweep_rule";
		public const string balance_prompt_type_DbName = "balance_prompt_type";
		public const string balance_prompt_per_unit_DbName = "balance_prompt_per_unit";

		//prop names
		public const string service_id_PropName = "Service_id";
		public const string name_PropName = "Name";
		public const string virtual_switch_id_PropName = "Virtual_switch_id";
		public const string calling_plan_id_PropName = "Calling_plan_id";
		public const string default_routing_plan_id_PropName = "Default_routing_plan_id";
		public const string status_PropName = "Status";
		public const string type_PropName = "Type";
		public const string retail_type_PropName = "Retail_type";
		public const string is_shared_PropName = "Is_shared";
		public const string rating_type_PropName = "Rating_type";
		public const string pin_length_PropName = "Pin_length";
		public const string payphone_surcharge_id_PropName = "Payphone_surcharge_id";
		public const string sweep_schedule_id_PropName = "Sweep_schedule_id";
		public const string sweep_fee_PropName = "Sweep_fee";
		public const string sweep_rule_PropName = "Sweep_rule";
		public const string balance_prompt_type_PropName = "Balance_prompt_type";
		public const string balance_prompt_per_unit_PropName = "Balance_prompt_per_unit";

		//db field display names
		public const string service_id_DisplayName = "service id";
		public const string name_DisplayName = "name";
		public const string virtual_switch_id_DisplayName = "virtual switch id";
		public const string calling_plan_id_DisplayName = "calling plan id";
		public const string default_routing_plan_id_DisplayName = "default routing plan id";
		public const string status_DisplayName = "status";
		public const string type_DisplayName = "type";
		public const string retail_type_DisplayName = "retail type";
		public const string is_shared_DisplayName = "is shared";
		public const string rating_type_DisplayName = "rating type";
		public const string pin_length_DisplayName = "pin length";
		public const string payphone_surcharge_id_DisplayName = "payphone surcharge id";
		public const string sweep_schedule_id_DisplayName = "sweep schedule id";
		public const string sweep_fee_DisplayName = "sweep fee";
		public const string sweep_rule_DisplayName = "sweep rule";
		public const string balance_prompt_type_DisplayName = "balance prompt type";
		public const string balance_prompt_per_unit_DisplayName = "balance prompt per unit";
	#endregion Timok Custom


		private short _service_id;
		private string _name;
		private int _virtual_switch_id;
		private int _calling_plan_id;
		private int _default_routing_plan_id;
		private byte _status;
		private byte _type;
		private int _retail_type;
		private byte _is_shared;
		private byte _rating_type;
		private int _pin_length;
		private int _payphone_surcharge_id;
		private bool _payphone_surcharge_idNull = true;
		private int _sweep_schedule_id;
		private bool _sweep_schedule_idNull = true;
		private decimal _sweep_fee;
		private int _sweep_rule;
		private byte _balance_prompt_type;
		private decimal _balance_prompt_per_unit;

		/// <summary>
		/// Initializes a new instance of the <see cref="ServiceRow_Base"/> class.
		/// </summary>
		public ServiceRow_Base()
		{
			// EMPTY
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
		/// Gets or sets the <c>name</c> column value.
		/// </summary>
		/// <value>The <c>name</c> column value.</value>
		public string Name
		{
			get { return _name; }
			set { _name = value; }
		}

		/// <summary>
		/// Gets or sets the <c>virtual_switch_id</c> column value.
		/// </summary>
		/// <value>The <c>virtual_switch_id</c> column value.</value>
		public int Virtual_switch_id
		{
			get { return _virtual_switch_id; }
			set { _virtual_switch_id = value; }
		}

		/// <summary>
		/// Gets or sets the <c>calling_plan_id</c> column value.
		/// </summary>
		/// <value>The <c>calling_plan_id</c> column value.</value>
		public int Calling_plan_id
		{
			get { return _calling_plan_id; }
			set { _calling_plan_id = value; }
		}

		/// <summary>
		/// Gets or sets the <c>default_routing_plan_id</c> column value.
		/// </summary>
		/// <value>The <c>default_routing_plan_id</c> column value.</value>
		public int Default_routing_plan_id
		{
			get { return _default_routing_plan_id; }
			set { _default_routing_plan_id = value; }
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
		/// Gets or sets the <c>type</c> column value.
		/// </summary>
		/// <value>The <c>type</c> column value.</value>
		public byte Type
		{
			get { return _type; }
			set { _type = value; }
		}

		/// <summary>
		/// Gets or sets the <c>retail_type</c> column value.
		/// </summary>
		/// <value>The <c>retail_type</c> column value.</value>
		public int Retail_type
		{
			get { return _retail_type; }
			set { _retail_type = value; }
		}

		/// <summary>
		/// Gets or sets the <c>is_shared</c> column value.
		/// </summary>
		/// <value>The <c>is_shared</c> column value.</value>
		public byte Is_shared
		{
			get { return _is_shared; }
			set { _is_shared = value; }
		}

		/// <summary>
		/// Gets or sets the <c>rating_type</c> column value.
		/// </summary>
		/// <value>The <c>rating_type</c> column value.</value>
		public byte Rating_type
		{
			get { return _rating_type; }
			set { _rating_type = value; }
		}

		/// <summary>
		/// Gets or sets the <c>pin_length</c> column value.
		/// </summary>
		/// <value>The <c>pin_length</c> column value.</value>
		public int Pin_length
		{
			get { return _pin_length; }
			set { _pin_length = value; }
		}

		/// <summary>
		/// Gets or sets the <c>payphone_surcharge_id</c> column value.
		/// This column is nullable.
		/// </summary>
		/// <value>The <c>payphone_surcharge_id</c> column value.</value>
		public int Payphone_surcharge_id
		{
			get
			{
				//if(IsPayphone_surcharge_idNull)
					//throw new InvalidOperationException("Cannot get value because it is DBNull.");
				return _payphone_surcharge_id;
			}
			set
			{
				_payphone_surcharge_idNull = false;
				_payphone_surcharge_id = value;
			}
		}

		/// <summary>
		/// Indicates whether the <see cref="Payphone_surcharge_id"/>
		/// property value is null.
		/// </summary>
		/// <value>true if the property value is null, otherwise false.</value>
		public bool IsPayphone_surcharge_idNull
		{
			get { return _payphone_surcharge_idNull; }
			set { _payphone_surcharge_idNull = value; }
		}

		/// <summary>
		/// Gets or sets the <c>sweep_schedule_id</c> column value.
		/// This column is nullable.
		/// </summary>
		/// <value>The <c>sweep_schedule_id</c> column value.</value>
		public int Sweep_schedule_id
		{
			get
			{
				//if(IsSweep_schedule_idNull)
					//throw new InvalidOperationException("Cannot get value because it is DBNull.");
				return _sweep_schedule_id;
			}
			set
			{
				_sweep_schedule_idNull = false;
				_sweep_schedule_id = value;
			}
		}

		/// <summary>
		/// Indicates whether the <see cref="Sweep_schedule_id"/>
		/// property value is null.
		/// </summary>
		/// <value>true if the property value is null, otherwise false.</value>
		public bool IsSweep_schedule_idNull
		{
			get { return _sweep_schedule_idNull; }
			set { _sweep_schedule_idNull = value; }
		}

		/// <summary>
		/// Gets or sets the <c>sweep_fee</c> column value.
		/// </summary>
		/// <value>The <c>sweep_fee</c> column value.</value>
		public decimal Sweep_fee
		{
			get { return _sweep_fee; }
			set { _sweep_fee = value; }
		}

		/// <summary>
		/// Gets or sets the <c>sweep_rule</c> column value.
		/// </summary>
		/// <value>The <c>sweep_rule</c> column value.</value>
		public int Sweep_rule
		{
			get { return _sweep_rule; }
			set { _sweep_rule = value; }
		}

		/// <summary>
		/// Gets or sets the <c>balance_prompt_type</c> column value.
		/// </summary>
		/// <value>The <c>balance_prompt_type</c> column value.</value>
		public byte Balance_prompt_type
		{
			get { return _balance_prompt_type; }
			set { _balance_prompt_type = value; }
		}

		/// <summary>
		/// Gets or sets the <c>balance_prompt_per_unit</c> column value.
		/// </summary>
		/// <value>The <c>balance_prompt_per_unit</c> column value.</value>
		public decimal Balance_prompt_per_unit
		{
			get { return _balance_prompt_per_unit; }
			set { _balance_prompt_per_unit = value; }
		}

		/// <summary>
		/// Returns the string representation of this instance.
		/// </summary>
		/// <returns>The string representation of this instance.</returns>
		public override string ToString()
		{
			System.Text.StringBuilder dynStr = new System.Text.StringBuilder(GetType().Name);
			dynStr.Append(':');
			dynStr.Append("  Service_id=");
			dynStr.Append(Service_id);
			dynStr.Append("  Name=");
			dynStr.Append(Name);
			dynStr.Append("  Virtual_switch_id=");
			dynStr.Append(Virtual_switch_id);
			dynStr.Append("  Calling_plan_id=");
			dynStr.Append(Calling_plan_id);
			dynStr.Append("  Default_routing_plan_id=");
			dynStr.Append(Default_routing_plan_id);
			dynStr.Append("  Status=");
			dynStr.Append(Status);
			dynStr.Append("  Type=");
			dynStr.Append(Type);
			dynStr.Append("  Retail_type=");
			dynStr.Append(Retail_type);
			dynStr.Append("  Is_shared=");
			dynStr.Append(Is_shared);
			dynStr.Append("  Rating_type=");
			dynStr.Append(Rating_type);
			dynStr.Append("  Pin_length=");
			dynStr.Append(Pin_length);
			dynStr.Append("  Payphone_surcharge_id=");
			dynStr.Append(IsPayphone_surcharge_idNull ? (object)"<NULL>" : Payphone_surcharge_id);
			dynStr.Append("  Sweep_schedule_id=");
			dynStr.Append(IsSweep_schedule_idNull ? (object)"<NULL>" : Sweep_schedule_id);
			dynStr.Append("  Sweep_fee=");
			dynStr.Append(Sweep_fee);
			dynStr.Append("  Sweep_rule=");
			dynStr.Append(Sweep_rule);
			dynStr.Append("  Balance_prompt_type=");
			dynStr.Append(Balance_prompt_type);
			dynStr.Append("  Balance_prompt_per_unit=");
			dynStr.Append(Balance_prompt_per_unit);
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

	} // End of ServiceRow_Base class
} // End of namespace

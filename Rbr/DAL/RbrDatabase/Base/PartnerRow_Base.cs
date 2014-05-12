// <fileinfo name="Base\PartnerRow_Base.cs">
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
	/// The base class for <see cref="PartnerRow"/> that 
	/// represents a record in the <c>Partner</c> table.
	/// </summary>
	/// <remarks>
	/// Do not change this source code manually. Update the <see cref="PartnerRow"/>
	/// class if you need to add or change some functionality.
	/// </remarks>
	[Serializable]
	public abstract class PartnerRow_Base
	{
	#region Timok Custom

		//db field names
		public const string partner_id_DbName = "partner_id";
		public const string name_DbName = "name";
		public const string status_DbName = "status";
		public const string contact_info_id_DbName = "contact_info_id";
		public const string billing_schedule_id_DbName = "billing_schedule_id";
		public const string virtual_switch_id_DbName = "virtual_switch_id";

		//prop names
		public const string partner_id_PropName = "Partner_id";
		public const string name_PropName = "Name";
		public const string status_PropName = "Status";
		public const string contact_info_id_PropName = "Contact_info_id";
		public const string billing_schedule_id_PropName = "Billing_schedule_id";
		public const string virtual_switch_id_PropName = "Virtual_switch_id";

		//db field display names
		public const string partner_id_DisplayName = "partner id";
		public const string name_DisplayName = "name";
		public const string status_DisplayName = "status";
		public const string contact_info_id_DisplayName = "contact info id";
		public const string billing_schedule_id_DisplayName = "billing schedule id";
		public const string virtual_switch_id_DisplayName = "virtual switch id";
	#endregion Timok Custom


		private int _partner_id;
		private string _name;
		private byte _status;
		private int _contact_info_id;
		private int _billing_schedule_id;
		private bool _billing_schedule_idNull = true;
		private int _virtual_switch_id;

		/// <summary>
		/// Initializes a new instance of the <see cref="PartnerRow_Base"/> class.
		/// </summary>
		public PartnerRow_Base()
		{
			// EMPTY
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
		/// Gets or sets the <c>contact_info_id</c> column value.
		/// </summary>
		/// <value>The <c>contact_info_id</c> column value.</value>
		public int Contact_info_id
		{
			get { return _contact_info_id; }
			set { _contact_info_id = value; }
		}

		/// <summary>
		/// Gets or sets the <c>billing_schedule_id</c> column value.
		/// This column is nullable.
		/// </summary>
		/// <value>The <c>billing_schedule_id</c> column value.</value>
		public int Billing_schedule_id
		{
			get
			{
				//if(IsBilling_schedule_idNull)
					//throw new InvalidOperationException("Cannot get value because it is DBNull.");
				return _billing_schedule_id;
			}
			set
			{
				_billing_schedule_idNull = false;
				_billing_schedule_id = value;
			}
		}

		/// <summary>
		/// Indicates whether the <see cref="Billing_schedule_id"/>
		/// property value is null.
		/// </summary>
		/// <value>true if the property value is null, otherwise false.</value>
		public bool IsBilling_schedule_idNull
		{
			get { return _billing_schedule_idNull; }
			set { _billing_schedule_idNull = value; }
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
		/// Returns the string representation of this instance.
		/// </summary>
		/// <returns>The string representation of this instance.</returns>
		public override string ToString()
		{
			System.Text.StringBuilder dynStr = new System.Text.StringBuilder(GetType().Name);
			dynStr.Append(':');
			dynStr.Append("  Partner_id=");
			dynStr.Append(Partner_id);
			dynStr.Append("  Name=");
			dynStr.Append(Name);
			dynStr.Append("  Status=");
			dynStr.Append(Status);
			dynStr.Append("  Contact_info_id=");
			dynStr.Append(Contact_info_id);
			dynStr.Append("  Billing_schedule_id=");
			dynStr.Append(IsBilling_schedule_idNull ? (object)"<NULL>" : Billing_schedule_id);
			dynStr.Append("  Virtual_switch_id=");
			dynStr.Append(Virtual_switch_id);
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

	} // End of PartnerRow_Base class
} // End of namespace

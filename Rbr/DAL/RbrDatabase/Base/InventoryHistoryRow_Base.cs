// <fileinfo name="Base\InventoryHistoryRow_Base.cs">
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
	/// The base class for <see cref="InventoryHistoryRow"/> that 
	/// represents a record in the <c>InventoryHistory</c> table.
	/// </summary>
	/// <remarks>
	/// Do not change this source code manually. Update the <see cref="InventoryHistoryRow"/>
	/// class if you need to add or change some functionality.
	/// </remarks>
	[Serializable]
	public abstract class InventoryHistoryRow_Base
	{
	#region Timok Custom

		//db field names
		public const string service_id_DbName = "service_id";
		public const string batch_id_DbName = "batch_id";
		public const string timestamp_DbName = "timestamp";
		public const string command_DbName = "command";
		public const string number_of_cards_DbName = "number_of_cards";
		public const string denomination_DbName = "denomination";
		public const string customer_acct_id_DbName = "customer_acct_id";
		public const string reseller_partner_id_DbName = "reseller_partner_id";
		public const string reseller_agent_id_DbName = "reseller_agent_id";
		public const string person_id_DbName = "person_id";

		//prop names
		public const string service_id_PropName = "Service_id";
		public const string batch_id_PropName = "Batch_id";
		public const string timestamp_PropName = "Timestamp";
		public const string command_PropName = "Command";
		public const string number_of_cards_PropName = "Number_of_cards";
		public const string denomination_PropName = "Denomination";
		public const string customer_acct_id_PropName = "Customer_acct_id";
		public const string reseller_partner_id_PropName = "Reseller_partner_id";
		public const string reseller_agent_id_PropName = "Reseller_agent_id";
		public const string person_id_PropName = "Person_id";

		//db field display names
		public const string service_id_DisplayName = "service id";
		public const string batch_id_DisplayName = "batch id";
		public const string timestamp_DisplayName = "timestamp";
		public const string command_DisplayName = "command";
		public const string number_of_cards_DisplayName = "number of cards";
		public const string denomination_DisplayName = "denomination";
		public const string customer_acct_id_DisplayName = "customer acct id";
		public const string reseller_partner_id_DisplayName = "reseller partner id";
		public const string reseller_agent_id_DisplayName = "reseller agent id";
		public const string person_id_DisplayName = "person id";
	#endregion Timok Custom


		private short _service_id;
		private int _batch_id;
		private System.DateTime _timestamp;
		private byte _command;
		private int _number_of_cards;
		private decimal _denomination;
		private short _customer_acct_id;
		private bool _customer_acct_idNull = true;
		private int _reseller_partner_id;
		private bool _reseller_partner_idNull = true;
		private int _reseller_agent_id;
		private bool _reseller_agent_idNull = true;
		private int _person_id;

		/// <summary>
		/// Initializes a new instance of the <see cref="InventoryHistoryRow_Base"/> class.
		/// </summary>
		public InventoryHistoryRow_Base()
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
		/// Gets or sets the <c>batch_id</c> column value.
		/// </summary>
		/// <value>The <c>batch_id</c> column value.</value>
		public int Batch_id
		{
			get { return _batch_id; }
			set { _batch_id = value; }
		}

		/// <summary>
		/// Gets or sets the <c>timestamp</c> column value.
		/// </summary>
		/// <value>The <c>timestamp</c> column value.</value>
		public System.DateTime Timestamp
		{
			get { return _timestamp; }
			set { _timestamp = value; }
		}

		/// <summary>
		/// Gets or sets the <c>command</c> column value.
		/// </summary>
		/// <value>The <c>command</c> column value.</value>
		public byte Command
		{
			get { return _command; }
			set { _command = value; }
		}

		/// <summary>
		/// Gets or sets the <c>number_of_cards</c> column value.
		/// </summary>
		/// <value>The <c>number_of_cards</c> column value.</value>
		public int Number_of_cards
		{
			get { return _number_of_cards; }
			set { _number_of_cards = value; }
		}

		/// <summary>
		/// Gets or sets the <c>denomination</c> column value.
		/// </summary>
		/// <value>The <c>denomination</c> column value.</value>
		public decimal Denomination
		{
			get { return _denomination; }
			set { _denomination = value; }
		}

		/// <summary>
		/// Gets or sets the <c>customer_acct_id</c> column value.
		/// This column is nullable.
		/// </summary>
		/// <value>The <c>customer_acct_id</c> column value.</value>
		public short Customer_acct_id
		{
			get
			{
				//if(IsCustomer_acct_idNull)
					//throw new InvalidOperationException("Cannot get value because it is DBNull.");
				return _customer_acct_id;
			}
			set
			{
				_customer_acct_idNull = false;
				_customer_acct_id = value;
			}
		}

		/// <summary>
		/// Indicates whether the <see cref="Customer_acct_id"/>
		/// property value is null.
		/// </summary>
		/// <value>true if the property value is null, otherwise false.</value>
		public bool IsCustomer_acct_idNull
		{
			get { return _customer_acct_idNull; }
			set { _customer_acct_idNull = value; }
		}

		/// <summary>
		/// Gets or sets the <c>reseller_partner_id</c> column value.
		/// This column is nullable.
		/// </summary>
		/// <value>The <c>reseller_partner_id</c> column value.</value>
		public int Reseller_partner_id
		{
			get
			{
				//if(IsReseller_partner_idNull)
					//throw new InvalidOperationException("Cannot get value because it is DBNull.");
				return _reseller_partner_id;
			}
			set
			{
				_reseller_partner_idNull = false;
				_reseller_partner_id = value;
			}
		}

		/// <summary>
		/// Indicates whether the <see cref="Reseller_partner_id"/>
		/// property value is null.
		/// </summary>
		/// <value>true if the property value is null, otherwise false.</value>
		public bool IsReseller_partner_idNull
		{
			get { return _reseller_partner_idNull; }
			set { _reseller_partner_idNull = value; }
		}

		/// <summary>
		/// Gets or sets the <c>reseller_agent_id</c> column value.
		/// This column is nullable.
		/// </summary>
		/// <value>The <c>reseller_agent_id</c> column value.</value>
		public int Reseller_agent_id
		{
			get
			{
				//if(IsReseller_agent_idNull)
					//throw new InvalidOperationException("Cannot get value because it is DBNull.");
				return _reseller_agent_id;
			}
			set
			{
				_reseller_agent_idNull = false;
				_reseller_agent_id = value;
			}
		}

		/// <summary>
		/// Indicates whether the <see cref="Reseller_agent_id"/>
		/// property value is null.
		/// </summary>
		/// <value>true if the property value is null, otherwise false.</value>
		public bool IsReseller_agent_idNull
		{
			get { return _reseller_agent_idNull; }
			set { _reseller_agent_idNull = value; }
		}

		/// <summary>
		/// Gets or sets the <c>person_id</c> column value.
		/// </summary>
		/// <value>The <c>person_id</c> column value.</value>
		public int Person_id
		{
			get { return _person_id; }
			set { _person_id = value; }
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
			dynStr.Append("  Batch_id=");
			dynStr.Append(Batch_id);
			dynStr.Append("  Timestamp=");
			dynStr.Append(Timestamp);
			dynStr.Append("  Command=");
			dynStr.Append(Command);
			dynStr.Append("  Number_of_cards=");
			dynStr.Append(Number_of_cards);
			dynStr.Append("  Denomination=");
			dynStr.Append(Denomination);
			dynStr.Append("  Customer_acct_id=");
			dynStr.Append(IsCustomer_acct_idNull ? (object)"<NULL>" : Customer_acct_id);
			dynStr.Append("  Reseller_partner_id=");
			dynStr.Append(IsReseller_partner_idNull ? (object)"<NULL>" : Reseller_partner_id);
			dynStr.Append("  Reseller_agent_id=");
			dynStr.Append(IsReseller_agent_idNull ? (object)"<NULL>" : Reseller_agent_id);
			dynStr.Append("  Person_id=");
			dynStr.Append(Person_id);
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

	} // End of InventoryHistoryRow_Base class
} // End of namespace

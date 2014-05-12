// <fileinfo name="Base\RetailAccountRow_Base.cs">
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
	/// The base class for <see cref="RetailAccountRow"/> that 
	/// represents a record in the <c>RetailAccount</c> table.
	/// </summary>
	/// <remarks>
	/// Do not change this source code manually. Update the <see cref="RetailAccountRow"/>
	/// class if you need to add or change some functionality.
	/// </remarks>
	[Serializable]
	public abstract class RetailAccountRow_Base
	{
	#region Timok Custom

		//db field names
		public const string retail_acct_id_DbName = "retail_acct_id";
		public const string customer_acct_id_DbName = "customer_acct_id";
		public const string date_created_DbName = "date_created";
		public const string date_active_DbName = "date_active";
		public const string date_to_expire_DbName = "date_to_expire";
		public const string date_expired_DbName = "date_expired";
		public const string status_DbName = "status";
		public const string start_balance_DbName = "start_balance";
		public const string current_balance_DbName = "current_balance";
		public const string start_bonus_minutes_DbName = "start_bonus_minutes";
		public const string current_bonus_minutes_DbName = "current_bonus_minutes";

		//prop names
		public const string retail_acct_id_PropName = "Retail_acct_id";
		public const string customer_acct_id_PropName = "Customer_acct_id";
		public const string date_created_PropName = "Date_created";
		public const string date_active_PropName = "Date_active";
		public const string date_to_expire_PropName = "Date_to_expire";
		public const string date_expired_PropName = "Date_expired";
		public const string status_PropName = "Status";
		public const string start_balance_PropName = "Start_balance";
		public const string current_balance_PropName = "Current_balance";
		public const string start_bonus_minutes_PropName = "Start_bonus_minutes";
		public const string current_bonus_minutes_PropName = "Current_bonus_minutes";

		//db field display names
		public const string retail_acct_id_DisplayName = "retail acct id";
		public const string customer_acct_id_DisplayName = "customer acct id";
		public const string date_created_DisplayName = "date created";
		public const string date_active_DisplayName = "date active";
		public const string date_to_expire_DisplayName = "date to expire";
		public const string date_expired_DisplayName = "date expired";
		public const string status_DisplayName = "status";
		public const string start_balance_DisplayName = "start balance";
		public const string current_balance_DisplayName = "current balance";
		public const string start_bonus_minutes_DisplayName = "start bonus minutes";
		public const string current_bonus_minutes_DisplayName = "current bonus minutes";
	#endregion Timok Custom


		private int _retail_acct_id;
		private short _customer_acct_id;
		private System.DateTime _date_created;
		private System.DateTime _date_active;
		private System.DateTime _date_to_expire;
		private System.DateTime _date_expired;
		private byte _status;
		private decimal _start_balance;
		private decimal _current_balance;
		private short _start_bonus_minutes;
		private short _current_bonus_minutes;

		/// <summary>
		/// Initializes a new instance of the <see cref="RetailAccountRow_Base"/> class.
		/// </summary>
		public RetailAccountRow_Base()
		{
			// EMPTY
		}

		/// <summary>
		/// Gets or sets the <c>retail_acct_id</c> column value.
		/// </summary>
		/// <value>The <c>retail_acct_id</c> column value.</value>
		public int Retail_acct_id
		{
			get { return _retail_acct_id; }
			set { _retail_acct_id = value; }
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
		/// Gets or sets the <c>date_created</c> column value.
		/// </summary>
		/// <value>The <c>date_created</c> column value.</value>
		public System.DateTime Date_created
		{
			get { return _date_created; }
			set { _date_created = value; }
		}

		/// <summary>
		/// Gets or sets the <c>date_active</c> column value.
		/// </summary>
		/// <value>The <c>date_active</c> column value.</value>
		public System.DateTime Date_active
		{
			get { return _date_active; }
			set { _date_active = value; }
		}

		/// <summary>
		/// Gets or sets the <c>date_to_expire</c> column value.
		/// </summary>
		/// <value>The <c>date_to_expire</c> column value.</value>
		public System.DateTime Date_to_expire
		{
			get { return _date_to_expire; }
			set { _date_to_expire = value; }
		}

		/// <summary>
		/// Gets or sets the <c>date_expired</c> column value.
		/// </summary>
		/// <value>The <c>date_expired</c> column value.</value>
		public System.DateTime Date_expired
		{
			get { return _date_expired; }
			set { _date_expired = value; }
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
		/// Gets or sets the <c>start_balance</c> column value.
		/// </summary>
		/// <value>The <c>start_balance</c> column value.</value>
		public decimal Start_balance
		{
			get { return _start_balance; }
			set { _start_balance = value; }
		}

		/// <summary>
		/// Gets or sets the <c>current_balance</c> column value.
		/// </summary>
		/// <value>The <c>current_balance</c> column value.</value>
		public decimal Current_balance
		{
			get { return _current_balance; }
			set { _current_balance = value; }
		}

		/// <summary>
		/// Gets or sets the <c>start_bonus_minutes</c> column value.
		/// </summary>
		/// <value>The <c>start_bonus_minutes</c> column value.</value>
		public short Start_bonus_minutes
		{
			get { return _start_bonus_minutes; }
			set { _start_bonus_minutes = value; }
		}

		/// <summary>
		/// Gets or sets the <c>current_bonus_minutes</c> column value.
		/// </summary>
		/// <value>The <c>current_bonus_minutes</c> column value.</value>
		public short Current_bonus_minutes
		{
			get { return _current_bonus_minutes; }
			set { _current_bonus_minutes = value; }
		}

		/// <summary>
		/// Returns the string representation of this instance.
		/// </summary>
		/// <returns>The string representation of this instance.</returns>
		public override string ToString()
		{
			System.Text.StringBuilder dynStr = new System.Text.StringBuilder(GetType().Name);
			dynStr.Append(':');
			dynStr.Append("  Retail_acct_id=");
			dynStr.Append(Retail_acct_id);
			dynStr.Append("  Customer_acct_id=");
			dynStr.Append(Customer_acct_id);
			dynStr.Append("  Date_created=");
			dynStr.Append(Date_created);
			dynStr.Append("  Date_active=");
			dynStr.Append(Date_active);
			dynStr.Append("  Date_to_expire=");
			dynStr.Append(Date_to_expire);
			dynStr.Append("  Date_expired=");
			dynStr.Append(Date_expired);
			dynStr.Append("  Status=");
			dynStr.Append(Status);
			dynStr.Append("  Start_balance=");
			dynStr.Append(Start_balance);
			dynStr.Append("  Current_balance=");
			dynStr.Append(Current_balance);
			dynStr.Append("  Start_bonus_minutes=");
			dynStr.Append(Start_bonus_minutes);
			dynStr.Append("  Current_bonus_minutes=");
			dynStr.Append(Current_bonus_minutes);
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

	} // End of RetailAccountRow_Base class
} // End of namespace

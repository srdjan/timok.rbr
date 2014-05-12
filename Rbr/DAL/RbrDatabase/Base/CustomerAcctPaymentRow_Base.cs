// <fileinfo name="Base\CustomerAcctPaymentRow_Base.cs">
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
	/// The base class for <see cref="CustomerAcctPaymentRow"/> that 
	/// represents a record in the <c>CustomerAcctPayment</c> table.
	/// </summary>
	/// <remarks>
	/// Do not change this source code manually. Update the <see cref="CustomerAcctPaymentRow"/>
	/// class if you need to add or change some functionality.
	/// </remarks>
	[Serializable]
	public abstract class CustomerAcctPaymentRow_Base
	{
	#region Timok Custom

		//db field names
		public const string customer_acct_id_DbName = "customer_acct_id";
		public const string date_time_DbName = "date_time";
		public const string previous_amount_DbName = "previous_amount";
		public const string payment_amount_DbName = "payment_amount";
		public const string comments_DbName = "comments";
		public const string person_id_DbName = "person_id";
		public const string balance_adjustment_reason_id_DbName = "balance_adjustment_reason_id";

		//prop names
		public const string customer_acct_id_PropName = "Customer_acct_id";
		public const string date_time_PropName = "Date_time";
		public const string previous_amount_PropName = "Previous_amount";
		public const string payment_amount_PropName = "Payment_amount";
		public const string comments_PropName = "Comments";
		public const string person_id_PropName = "Person_id";
		public const string balance_adjustment_reason_id_PropName = "Balance_adjustment_reason_id";

		//db field display names
		public const string customer_acct_id_DisplayName = "customer acct id";
		public const string date_time_DisplayName = "date time";
		public const string previous_amount_DisplayName = "previous amount";
		public const string payment_amount_DisplayName = "payment amount";
		public const string comments_DisplayName = "comments";
		public const string person_id_DisplayName = "person id";
		public const string balance_adjustment_reason_id_DisplayName = "balance adjustment reason id";
	#endregion Timok Custom


		private short _customer_acct_id;
		private System.DateTime _date_time;
		private decimal _previous_amount;
		private decimal _payment_amount;
		private string _comments;
		private int _person_id;
		private int _balance_adjustment_reason_id;

		/// <summary>
		/// Initializes a new instance of the <see cref="CustomerAcctPaymentRow_Base"/> class.
		/// </summary>
		public CustomerAcctPaymentRow_Base()
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
		/// Gets or sets the <c>date_time</c> column value.
		/// </summary>
		/// <value>The <c>date_time</c> column value.</value>
		public System.DateTime Date_time
		{
			get { return _date_time; }
			set { _date_time = value; }
		}

		/// <summary>
		/// Gets or sets the <c>previous_amount</c> column value.
		/// </summary>
		/// <value>The <c>previous_amount</c> column value.</value>
		public decimal Previous_amount
		{
			get { return _previous_amount; }
			set { _previous_amount = value; }
		}

		/// <summary>
		/// Gets or sets the <c>payment_amount</c> column value.
		/// </summary>
		/// <value>The <c>payment_amount</c> column value.</value>
		public decimal Payment_amount
		{
			get { return _payment_amount; }
			set { _payment_amount = value; }
		}

		/// <summary>
		/// Gets or sets the <c>comments</c> column value.
		/// </summary>
		/// <value>The <c>comments</c> column value.</value>
		public string Comments
		{
			get { return _comments; }
			set { _comments = value; }
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
		/// Gets or sets the <c>balance_adjustment_reason_id</c> column value.
		/// </summary>
		/// <value>The <c>balance_adjustment_reason_id</c> column value.</value>
		public int Balance_adjustment_reason_id
		{
			get { return _balance_adjustment_reason_id; }
			set { _balance_adjustment_reason_id = value; }
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
			dynStr.Append("  Date_time=");
			dynStr.Append(Date_time);
			dynStr.Append("  Previous_amount=");
			dynStr.Append(Previous_amount);
			dynStr.Append("  Payment_amount=");
			dynStr.Append(Payment_amount);
			dynStr.Append("  Comments=");
			dynStr.Append(Comments);
			dynStr.Append("  Person_id=");
			dynStr.Append(Person_id);
			dynStr.Append("  Balance_adjustment_reason_id=");
			dynStr.Append(Balance_adjustment_reason_id);
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

	} // End of CustomerAcctPaymentRow_Base class
} // End of namespace

// <fileinfo name="Base\RetailAccountPaymentRow_Base.cs">
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
	/// The base class for <see cref="RetailAccountPaymentRow"/> that 
	/// represents a record in the <c>RetailAccountPayment</c> table.
	/// </summary>
	/// <remarks>
	/// Do not change this source code manually. Update the <see cref="RetailAccountPaymentRow"/>
	/// class if you need to add or change some functionality.
	/// </remarks>
	[Serializable]
	public abstract class RetailAccountPaymentRow_Base
	{
	#region Timok Custom

		//db field names
		public const string retail_acct_id_DbName = "retail_acct_id";
		public const string date_time_DbName = "date_time";
		public const string previous_amount_DbName = "previous_amount";
		public const string payment_amount_DbName = "payment_amount";
		public const string previous_bonus_minutes_DbName = "previous_bonus_minutes";
		public const string added_bonus_minutes_DbName = "added_bonus_minutes";
		public const string comments_DbName = "comments";
		public const string person_id_DbName = "person_id";
		public const string balance_adjustment_reason_id_DbName = "balance_adjustment_reason_id";
		public const string cdr_key_DbName = "cdr_key";

		//prop names
		public const string retail_acct_id_PropName = "Retail_acct_id";
		public const string date_time_PropName = "Date_time";
		public const string previous_amount_PropName = "Previous_amount";
		public const string payment_amount_PropName = "Payment_amount";
		public const string previous_bonus_minutes_PropName = "Previous_bonus_minutes";
		public const string added_bonus_minutes_PropName = "Added_bonus_minutes";
		public const string comments_PropName = "Comments";
		public const string person_id_PropName = "Person_id";
		public const string balance_adjustment_reason_id_PropName = "Balance_adjustment_reason_id";
		public const string cdr_key_PropName = "Cdr_key";

		//db field display names
		public const string retail_acct_id_DisplayName = "retail acct id";
		public const string date_time_DisplayName = "date time";
		public const string previous_amount_DisplayName = "previous amount";
		public const string payment_amount_DisplayName = "payment amount";
		public const string previous_bonus_minutes_DisplayName = "previous bonus minutes";
		public const string added_bonus_minutes_DisplayName = "added bonus minutes";
		public const string comments_DisplayName = "comments";
		public const string person_id_DisplayName = "person id";
		public const string balance_adjustment_reason_id_DisplayName = "balance adjustment reason id";
		public const string cdr_key_DisplayName = "cdr key";
	#endregion Timok Custom


		private int _retail_acct_id;
		private System.DateTime _date_time;
		private decimal _previous_amount;
		private decimal _payment_amount;
		private short _previous_bonus_minutes;
		private short _added_bonus_minutes;
		private string _comments;
		private int _person_id;
		private int _balance_adjustment_reason_id;
		private string _cdr_key;

		/// <summary>
		/// Initializes a new instance of the <see cref="RetailAccountPaymentRow_Base"/> class.
		/// </summary>
		public RetailAccountPaymentRow_Base()
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
		/// Gets or sets the <c>previous_bonus_minutes</c> column value.
		/// </summary>
		/// <value>The <c>previous_bonus_minutes</c> column value.</value>
		public short Previous_bonus_minutes
		{
			get { return _previous_bonus_minutes; }
			set { _previous_bonus_minutes = value; }
		}

		/// <summary>
		/// Gets or sets the <c>added_bonus_minutes</c> column value.
		/// </summary>
		/// <value>The <c>added_bonus_minutes</c> column value.</value>
		public short Added_bonus_minutes
		{
			get { return _added_bonus_minutes; }
			set { _added_bonus_minutes = value; }
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
		/// Gets or sets the <c>cdr_key</c> column value.
		/// </summary>
		/// <value>The <c>cdr_key</c> column value.</value>
		public string Cdr_key
		{
			get { return _cdr_key; }
			set { _cdr_key = value; }
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
			dynStr.Append("  Date_time=");
			dynStr.Append(Date_time);
			dynStr.Append("  Previous_amount=");
			dynStr.Append(Previous_amount);
			dynStr.Append("  Payment_amount=");
			dynStr.Append(Payment_amount);
			dynStr.Append("  Previous_bonus_minutes=");
			dynStr.Append(Previous_bonus_minutes);
			dynStr.Append("  Added_bonus_minutes=");
			dynStr.Append(Added_bonus_minutes);
			dynStr.Append("  Comments=");
			dynStr.Append(Comments);
			dynStr.Append("  Person_id=");
			dynStr.Append(Person_id);
			dynStr.Append("  Balance_adjustment_reason_id=");
			dynStr.Append(Balance_adjustment_reason_id);
			dynStr.Append("  Cdr_key=");
			dynStr.Append(Cdr_key);
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

	} // End of RetailAccountPaymentRow_Base class
} // End of namespace

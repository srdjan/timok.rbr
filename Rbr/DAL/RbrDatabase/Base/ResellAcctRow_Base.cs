// <fileinfo name="Base\ResellAcctRow_Base.cs">
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
	/// The base class for <see cref="ResellAcctRow"/> that 
	/// represents a record in the <c>ResellAcct</c> table.
	/// </summary>
	/// <remarks>
	/// Do not change this source code manually. Update the <see cref="ResellAcctRow"/>
	/// class if you need to add or change some functionality.
	/// </remarks>
	[Serializable]
	public abstract class ResellAcctRow_Base
	{
	#region Timok Custom

		//db field names
		public const string resell_acct_id_DbName = "resell_acct_id";
		public const string partner_id_DbName = "partner_id";
		public const string customer_acct_id_DbName = "customer_acct_id";
		public const string person_id_DbName = "person_id";
		public const string per_route_DbName = "per_route";
		public const string commision_type_DbName = "commision_type";
		public const string markup_dollar_DbName = "markup_dollar";
		public const string markup_percent_DbName = "markup_percent";
		public const string fee_per_call_DbName = "fee_per_call";
		public const string fee_per_minute_DbName = "fee_per_minute";

		//prop names
		public const string resell_acct_id_PropName = "Resell_acct_id";
		public const string partner_id_PropName = "Partner_id";
		public const string customer_acct_id_PropName = "Customer_acct_id";
		public const string person_id_PropName = "Person_id";
		public const string per_route_PropName = "Per_route";
		public const string commision_type_PropName = "Commision_type";
		public const string markup_dollar_PropName = "Markup_dollar";
		public const string markup_percent_PropName = "Markup_percent";
		public const string fee_per_call_PropName = "Fee_per_call";
		public const string fee_per_minute_PropName = "Fee_per_minute";

		//db field display names
		public const string resell_acct_id_DisplayName = "resell acct id";
		public const string partner_id_DisplayName = "partner id";
		public const string customer_acct_id_DisplayName = "customer acct id";
		public const string person_id_DisplayName = "person id";
		public const string per_route_DisplayName = "per route";
		public const string commision_type_DisplayName = "commision type";
		public const string markup_dollar_DisplayName = "markup dollar";
		public const string markup_percent_DisplayName = "markup percent";
		public const string fee_per_call_DisplayName = "fee per call";
		public const string fee_per_minute_DisplayName = "fee per minute";
	#endregion Timok Custom


		private short _resell_acct_id;
		private int _partner_id;
		private short _customer_acct_id;
		private int _person_id;
		private byte _per_route;
		private byte _commision_type;
		private decimal _markup_dollar;
		private decimal _markup_percent;
		private decimal _fee_per_call;
		private decimal _fee_per_minute;

		/// <summary>
		/// Initializes a new instance of the <see cref="ResellAcctRow_Base"/> class.
		/// </summary>
		public ResellAcctRow_Base()
		{
			// EMPTY
		}

		/// <summary>
		/// Gets or sets the <c>resell_acct_id</c> column value.
		/// </summary>
		/// <value>The <c>resell_acct_id</c> column value.</value>
		public short Resell_acct_id
		{
			get { return _resell_acct_id; }
			set { _resell_acct_id = value; }
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
		/// Gets or sets the <c>customer_acct_id</c> column value.
		/// </summary>
		/// <value>The <c>customer_acct_id</c> column value.</value>
		public short Customer_acct_id
		{
			get { return _customer_acct_id; }
			set { _customer_acct_id = value; }
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
		/// Gets or sets the <c>per_route</c> column value.
		/// </summary>
		/// <value>The <c>per_route</c> column value.</value>
		public byte Per_route
		{
			get { return _per_route; }
			set { _per_route = value; }
		}

		/// <summary>
		/// Gets or sets the <c>commision_type</c> column value.
		/// </summary>
		/// <value>The <c>commision_type</c> column value.</value>
		public byte Commision_type
		{
			get { return _commision_type; }
			set { _commision_type = value; }
		}

		/// <summary>
		/// Gets or sets the <c>markup_dollar</c> column value.
		/// </summary>
		/// <value>The <c>markup_dollar</c> column value.</value>
		public decimal Markup_dollar
		{
			get { return _markup_dollar; }
			set { _markup_dollar = value; }
		}

		/// <summary>
		/// Gets or sets the <c>markup_percent</c> column value.
		/// </summary>
		/// <value>The <c>markup_percent</c> column value.</value>
		public decimal Markup_percent
		{
			get { return _markup_percent; }
			set { _markup_percent = value; }
		}

		/// <summary>
		/// Gets or sets the <c>fee_per_call</c> column value.
		/// </summary>
		/// <value>The <c>fee_per_call</c> column value.</value>
		public decimal Fee_per_call
		{
			get { return _fee_per_call; }
			set { _fee_per_call = value; }
		}

		/// <summary>
		/// Gets or sets the <c>fee_per_minute</c> column value.
		/// </summary>
		/// <value>The <c>fee_per_minute</c> column value.</value>
		public decimal Fee_per_minute
		{
			get { return _fee_per_minute; }
			set { _fee_per_minute = value; }
		}

		/// <summary>
		/// Returns the string representation of this instance.
		/// </summary>
		/// <returns>The string representation of this instance.</returns>
		public override string ToString()
		{
			System.Text.StringBuilder dynStr = new System.Text.StringBuilder(GetType().Name);
			dynStr.Append(':');
			dynStr.Append("  Resell_acct_id=");
			dynStr.Append(Resell_acct_id);
			dynStr.Append("  Partner_id=");
			dynStr.Append(Partner_id);
			dynStr.Append("  Customer_acct_id=");
			dynStr.Append(Customer_acct_id);
			dynStr.Append("  Person_id=");
			dynStr.Append(Person_id);
			dynStr.Append("  Per_route=");
			dynStr.Append(Per_route);
			dynStr.Append("  Commision_type=");
			dynStr.Append(Commision_type);
			dynStr.Append("  Markup_dollar=");
			dynStr.Append(Markup_dollar);
			dynStr.Append("  Markup_percent=");
			dynStr.Append(Markup_percent);
			dynStr.Append("  Fee_per_call=");
			dynStr.Append(Fee_per_call);
			dynStr.Append("  Fee_per_minute=");
			dynStr.Append(Fee_per_minute);
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

	} // End of ResellAcctRow_Base class
} // End of namespace

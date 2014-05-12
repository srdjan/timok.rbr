// <fileinfo name="Base\CdrAggregateRow_Base.cs">
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
	/// The base class for <see cref="CdrAggregateRow"/> that 
	/// represents a record in the <c>CdrAggregate</c> table.
	/// </summary>
	/// <remarks>
	/// Do not change this source code manually. Update the <see cref="CdrAggregateRow"/>
	/// class if you need to add or change some functionality.
	/// </remarks>
	[Serializable]
	public abstract class CdrAggregateRow_Base
	{
	#region Timok Custom

		//db field names
		public const string date_hour_DbName = "date_hour";
		public const string node_id_DbName = "node_id";
		public const string orig_end_point_IP_DbName = "orig_end_point_IP";
		public const string orig_end_point_id_DbName = "orig_end_point_id";
		public const string customer_acct_id_DbName = "customer_acct_id";
		public const string customer_route_id_DbName = "customer_route_id";
		public const string term_end_point_IP_DbName = "term_end_point_IP";
		public const string term_end_point_id_DbName = "term_end_point_id";
		public const string calls_attempted_DbName = "calls_attempted";
		public const string calls_completed_DbName = "calls_completed";
		public const string setup_seconds_DbName = "setup_seconds";
		public const string alert_seconds_DbName = "alert_seconds";
		public const string connected_seconds_DbName = "connected_seconds";
		public const string connected_minutes_DbName = "connected_minutes";
		public const string carrier_cost_DbName = "carrier_cost";
		public const string carrier_rounded_minutes_DbName = "carrier_rounded_minutes";
		public const string wholesale_price_DbName = "wholesale_price";
		public const string wholesale_rounded_minutes_DbName = "wholesale_rounded_minutes";
		public const string end_user_price_DbName = "end_user_price";
		public const string end_user_rounded_minutes_DbName = "end_user_rounded_minutes";
		public const string carrier_acct_id_DbName = "carrier_acct_id";
		public const string carrier_route_id_DbName = "carrier_route_id";
		public const string access_number_DbName = "access_number";

		//prop names
		public const string date_hour_PropName = "Date_hour";
		public const string node_id_PropName = "Node_id";
		public const string orig_end_point_IP_PropName = "Orig_end_point_IP";
		public const string orig_end_point_id_PropName = "Orig_end_point_id";
		public const string customer_acct_id_PropName = "Customer_acct_id";
		public const string customer_route_id_PropName = "Customer_route_id";
		public const string term_end_point_IP_PropName = "Term_end_point_IP";
		public const string term_end_point_id_PropName = "Term_end_point_id";
		public const string calls_attempted_PropName = "Calls_attempted";
		public const string calls_completed_PropName = "Calls_completed";
		public const string setup_seconds_PropName = "Setup_seconds";
		public const string alert_seconds_PropName = "Alert_seconds";
		public const string connected_seconds_PropName = "Connected_seconds";
		public const string connected_minutes_PropName = "Connected_minutes";
		public const string carrier_cost_PropName = "Carrier_cost";
		public const string carrier_rounded_minutes_PropName = "Carrier_rounded_minutes";
		public const string wholesale_price_PropName = "Wholesale_price";
		public const string wholesale_rounded_minutes_PropName = "Wholesale_rounded_minutes";
		public const string end_user_price_PropName = "End_user_price";
		public const string end_user_rounded_minutes_PropName = "End_user_rounded_minutes";
		public const string carrier_acct_id_PropName = "Carrier_acct_id";
		public const string carrier_route_id_PropName = "Carrier_route_id";
		public const string access_number_PropName = "Access_number";

		//db field display names
		public const string date_hour_DisplayName = "date hour";
		public const string node_id_DisplayName = "node id";
		public const string orig_end_point_IP_DisplayName = "orig end point IP";
		public const string orig_end_point_id_DisplayName = "orig end point id";
		public const string customer_acct_id_DisplayName = "customer acct id";
		public const string customer_route_id_DisplayName = "customer route id";
		public const string term_end_point_IP_DisplayName = "term end point IP";
		public const string term_end_point_id_DisplayName = "term end point id";
		public const string calls_attempted_DisplayName = "calls attempted";
		public const string calls_completed_DisplayName = "calls completed";
		public const string setup_seconds_DisplayName = "setup seconds";
		public const string alert_seconds_DisplayName = "alert seconds";
		public const string connected_seconds_DisplayName = "connected seconds";
		public const string connected_minutes_DisplayName = "connected minutes";
		public const string carrier_cost_DisplayName = "carrier cost";
		public const string carrier_rounded_minutes_DisplayName = "carrier rounded minutes";
		public const string wholesale_price_DisplayName = "wholesale price";
		public const string wholesale_rounded_minutes_DisplayName = "wholesale rounded minutes";
		public const string end_user_price_DisplayName = "end user price";
		public const string end_user_rounded_minutes_DisplayName = "end user rounded minutes";
		public const string carrier_acct_id_DisplayName = "carrier acct id";
		public const string carrier_route_id_DisplayName = "carrier route id";
		public const string access_number_DisplayName = "access number";
	#endregion Timok Custom


		private int _date_hour;
		private short _node_id;
		private int _orig_end_point_IP;
		private short _orig_end_point_id;
		private short _customer_acct_id;
		private int _customer_route_id;
		private int _term_end_point_IP;
		private short _term_end_point_id;
		private int _calls_attempted;
		private int _calls_completed;
		private int _setup_seconds;
		private int _alert_seconds;
		private int _connected_seconds;
		private decimal _connected_minutes;
		private decimal _carrier_cost;
		private decimal _carrier_rounded_minutes;
		private decimal _wholesale_price;
		private decimal _wholesale_rounded_minutes;
		private decimal _end_user_price;
		private decimal _end_user_rounded_minutes;
		private short _carrier_acct_id;
		private int _carrier_route_id;
		private long _access_number;

		/// <summary>
		/// Initializes a new instance of the <see cref="CdrAggregateRow_Base"/> class.
		/// </summary>
		public CdrAggregateRow_Base()
		{
			// EMPTY
		}

		/// <summary>
		/// Gets or sets the <c>date_hour</c> column value.
		/// </summary>
		/// <value>The <c>date_hour</c> column value.</value>
		public int Date_hour
		{
			get { return _date_hour; }
			set { _date_hour = value; }
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
		/// Gets or sets the <c>orig_end_point_IP</c> column value.
		/// </summary>
		/// <value>The <c>orig_end_point_IP</c> column value.</value>
		public int Orig_end_point_IP
		{
			get { return _orig_end_point_IP; }
			set { _orig_end_point_IP = value; }
		}

		/// <summary>
		/// Gets or sets the <c>orig_end_point_id</c> column value.
		/// </summary>
		/// <value>The <c>orig_end_point_id</c> column value.</value>
		public short Orig_end_point_id
		{
			get { return _orig_end_point_id; }
			set { _orig_end_point_id = value; }
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
		/// Gets or sets the <c>customer_route_id</c> column value.
		/// </summary>
		/// <value>The <c>customer_route_id</c> column value.</value>
		public int Customer_route_id
		{
			get { return _customer_route_id; }
			set { _customer_route_id = value; }
		}

		/// <summary>
		/// Gets or sets the <c>term_end_point_IP</c> column value.
		/// </summary>
		/// <value>The <c>term_end_point_IP</c> column value.</value>
		public int Term_end_point_IP
		{
			get { return _term_end_point_IP; }
			set { _term_end_point_IP = value; }
		}

		/// <summary>
		/// Gets or sets the <c>term_end_point_id</c> column value.
		/// </summary>
		/// <value>The <c>term_end_point_id</c> column value.</value>
		public short Term_end_point_id
		{
			get { return _term_end_point_id; }
			set { _term_end_point_id = value; }
		}

		/// <summary>
		/// Gets or sets the <c>calls_attempted</c> column value.
		/// </summary>
		/// <value>The <c>calls_attempted</c> column value.</value>
		public int Calls_attempted
		{
			get { return _calls_attempted; }
			set { _calls_attempted = value; }
		}

		/// <summary>
		/// Gets or sets the <c>calls_completed</c> column value.
		/// </summary>
		/// <value>The <c>calls_completed</c> column value.</value>
		public int Calls_completed
		{
			get { return _calls_completed; }
			set { _calls_completed = value; }
		}

		/// <summary>
		/// Gets or sets the <c>setup_seconds</c> column value.
		/// </summary>
		/// <value>The <c>setup_seconds</c> column value.</value>
		public int Setup_seconds
		{
			get { return _setup_seconds; }
			set { _setup_seconds = value; }
		}

		/// <summary>
		/// Gets or sets the <c>alert_seconds</c> column value.
		/// </summary>
		/// <value>The <c>alert_seconds</c> column value.</value>
		public int Alert_seconds
		{
			get { return _alert_seconds; }
			set { _alert_seconds = value; }
		}

		/// <summary>
		/// Gets or sets the <c>connected_seconds</c> column value.
		/// </summary>
		/// <value>The <c>connected_seconds</c> column value.</value>
		public int Connected_seconds
		{
			get { return _connected_seconds; }
			set { _connected_seconds = value; }
		}

		/// <summary>
		/// Gets or sets the <c>connected_minutes</c> column value.
		/// </summary>
		/// <value>The <c>connected_minutes</c> column value.</value>
		public decimal Connected_minutes
		{
			get { return _connected_minutes; }
			set { _connected_minutes = value; }
		}

		/// <summary>
		/// Gets or sets the <c>carrier_cost</c> column value.
		/// </summary>
		/// <value>The <c>carrier_cost</c> column value.</value>
		public decimal Carrier_cost
		{
			get { return _carrier_cost; }
			set { _carrier_cost = value; }
		}

		/// <summary>
		/// Gets or sets the <c>carrier_rounded_minutes</c> column value.
		/// </summary>
		/// <value>The <c>carrier_rounded_minutes</c> column value.</value>
		public decimal Carrier_rounded_minutes
		{
			get { return _carrier_rounded_minutes; }
			set { _carrier_rounded_minutes = value; }
		}

		/// <summary>
		/// Gets or sets the <c>wholesale_price</c> column value.
		/// </summary>
		/// <value>The <c>wholesale_price</c> column value.</value>
		public decimal Wholesale_price
		{
			get { return _wholesale_price; }
			set { _wholesale_price = value; }
		}

		/// <summary>
		/// Gets or sets the <c>wholesale_rounded_minutes</c> column value.
		/// </summary>
		/// <value>The <c>wholesale_rounded_minutes</c> column value.</value>
		public decimal Wholesale_rounded_minutes
		{
			get { return _wholesale_rounded_minutes; }
			set { _wholesale_rounded_minutes = value; }
		}

		/// <summary>
		/// Gets or sets the <c>end_user_price</c> column value.
		/// </summary>
		/// <value>The <c>end_user_price</c> column value.</value>
		public decimal End_user_price
		{
			get { return _end_user_price; }
			set { _end_user_price = value; }
		}

		/// <summary>
		/// Gets or sets the <c>end_user_rounded_minutes</c> column value.
		/// </summary>
		/// <value>The <c>end_user_rounded_minutes</c> column value.</value>
		public decimal End_user_rounded_minutes
		{
			get { return _end_user_rounded_minutes; }
			set { _end_user_rounded_minutes = value; }
		}

		/// <summary>
		/// Gets or sets the <c>carrier_acct_id</c> column value.
		/// </summary>
		/// <value>The <c>carrier_acct_id</c> column value.</value>
		public short Carrier_acct_id
		{
			get { return _carrier_acct_id; }
			set { _carrier_acct_id = value; }
		}

		/// <summary>
		/// Gets or sets the <c>carrier_route_id</c> column value.
		/// </summary>
		/// <value>The <c>carrier_route_id</c> column value.</value>
		public int Carrier_route_id
		{
			get { return _carrier_route_id; }
			set { _carrier_route_id = value; }
		}

		/// <summary>
		/// Gets or sets the <c>access_number</c> column value.
		/// </summary>
		/// <value>The <c>access_number</c> column value.</value>
		public long Access_number
		{
			get { return _access_number; }
			set { _access_number = value; }
		}

		/// <summary>
		/// Returns the string representation of this instance.
		/// </summary>
		/// <returns>The string representation of this instance.</returns>
		public override string ToString()
		{
			System.Text.StringBuilder dynStr = new System.Text.StringBuilder(GetType().Name);
			dynStr.Append(':');
			dynStr.Append("  Date_hour=");
			dynStr.Append(Date_hour);
			dynStr.Append("  Node_id=");
			dynStr.Append(Node_id);
			dynStr.Append("  Orig_end_point_IP=");
			dynStr.Append(Orig_end_point_IP);
			dynStr.Append("  Orig_end_point_id=");
			dynStr.Append(Orig_end_point_id);
			dynStr.Append("  Customer_acct_id=");
			dynStr.Append(Customer_acct_id);
			dynStr.Append("  Customer_route_id=");
			dynStr.Append(Customer_route_id);
			dynStr.Append("  Term_end_point_IP=");
			dynStr.Append(Term_end_point_IP);
			dynStr.Append("  Term_end_point_id=");
			dynStr.Append(Term_end_point_id);
			dynStr.Append("  Calls_attempted=");
			dynStr.Append(Calls_attempted);
			dynStr.Append("  Calls_completed=");
			dynStr.Append(Calls_completed);
			dynStr.Append("  Setup_seconds=");
			dynStr.Append(Setup_seconds);
			dynStr.Append("  Alert_seconds=");
			dynStr.Append(Alert_seconds);
			dynStr.Append("  Connected_seconds=");
			dynStr.Append(Connected_seconds);
			dynStr.Append("  Connected_minutes=");
			dynStr.Append(Connected_minutes);
			dynStr.Append("  Carrier_cost=");
			dynStr.Append(Carrier_cost);
			dynStr.Append("  Carrier_rounded_minutes=");
			dynStr.Append(Carrier_rounded_minutes);
			dynStr.Append("  Wholesale_price=");
			dynStr.Append(Wholesale_price);
			dynStr.Append("  Wholesale_rounded_minutes=");
			dynStr.Append(Wholesale_rounded_minutes);
			dynStr.Append("  End_user_price=");
			dynStr.Append(End_user_price);
			dynStr.Append("  End_user_rounded_minutes=");
			dynStr.Append(End_user_rounded_minutes);
			dynStr.Append("  Carrier_acct_id=");
			dynStr.Append(Carrier_acct_id);
			dynStr.Append("  Carrier_route_id=");
			dynStr.Append(Carrier_route_id);
			dynStr.Append("  Access_number=");
			dynStr.Append(Access_number);
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

	} // End of CdrAggregateRow_Base class
} // End of namespace

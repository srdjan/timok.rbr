// <fileinfo name="Base\RetailRateHistoryRow_Base.cs">
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
	/// The base class for <see cref="RetailRateHistoryRow"/> that 
	/// represents a record in the <c>RetailRateHistory</c> table.
	/// </summary>
	/// <remarks>
	/// Do not change this source code manually. Update the <see cref="RetailRateHistoryRow"/>
	/// class if you need to add or change some functionality.
	/// </remarks>
	[Serializable]
	public abstract class RetailRateHistoryRow_Base
	{
	#region Timok Custom

		//db field names
		public const string retail_route_id_DbName = "retail_route_id";
		public const string date_on_DbName = "date_on";
		public const string date_off_DbName = "date_off";
		public const string rate_info_id_DbName = "rate_info_id";
		public const string connect_fee_DbName = "connect_fee";
		public const string disconnect_fee_DbName = "disconnect_fee";
		public const string per_call_cost_DbName = "per_call_cost";
		public const string cost_increase_per_call_DbName = "cost_increase_per_call";
		public const string cost_increase_per_call_start_DbName = "cost_increase_per_call_start";
		public const string cost_increase_per_call_stop_DbName = "cost_increase_per_call_stop";
		public const string tax_first_incr_cost_DbName = "tax_first_incr_cost";
		public const string tax_add_incr_cost_DbName = "tax_add_incr_cost";
		public const string surcharge_delay_DbName = "surcharge_delay";
		public const string rating_delay_DbName = "rating_delay";

		//prop names
		public const string retail_route_id_PropName = "Retail_route_id";
		public const string date_on_PropName = "Date_on";
		public const string date_off_PropName = "Date_off";
		public const string rate_info_id_PropName = "Rate_info_id";
		public const string connect_fee_PropName = "Connect_fee";
		public const string disconnect_fee_PropName = "Disconnect_fee";
		public const string per_call_cost_PropName = "Per_call_cost";
		public const string cost_increase_per_call_PropName = "Cost_increase_per_call";
		public const string cost_increase_per_call_start_PropName = "Cost_increase_per_call_start";
		public const string cost_increase_per_call_stop_PropName = "Cost_increase_per_call_stop";
		public const string tax_first_incr_cost_PropName = "Tax_first_incr_cost";
		public const string tax_add_incr_cost_PropName = "Tax_add_incr_cost";
		public const string surcharge_delay_PropName = "Surcharge_delay";
		public const string rating_delay_PropName = "Rating_delay";

		//db field display names
		public const string retail_route_id_DisplayName = "retail route id";
		public const string date_on_DisplayName = "date on";
		public const string date_off_DisplayName = "date off";
		public const string rate_info_id_DisplayName = "rate info id";
		public const string connect_fee_DisplayName = "connect fee";
		public const string disconnect_fee_DisplayName = "disconnect fee";
		public const string per_call_cost_DisplayName = "per call cost";
		public const string cost_increase_per_call_DisplayName = "cost increase per call";
		public const string cost_increase_per_call_start_DisplayName = "cost increase per call start";
		public const string cost_increase_per_call_stop_DisplayName = "cost increase per call stop";
		public const string tax_first_incr_cost_DisplayName = "tax first incr cost";
		public const string tax_add_incr_cost_DisplayName = "tax add incr cost";
		public const string surcharge_delay_DisplayName = "surcharge delay";
		public const string rating_delay_DisplayName = "rating delay";
	#endregion Timok Custom


		private int _retail_route_id;
		private System.DateTime _date_on;
		private System.DateTime _date_off;
		private int _rate_info_id;
		private decimal _connect_fee;
		private decimal _disconnect_fee;
		private decimal _per_call_cost;
		private int _cost_increase_per_call;
		private int _cost_increase_per_call_start;
		private int _cost_increase_per_call_stop;
		private decimal _tax_first_incr_cost;
		private decimal _tax_add_incr_cost;
		private byte _surcharge_delay;
		private byte _rating_delay;

		/// <summary>
		/// Initializes a new instance of the <see cref="RetailRateHistoryRow_Base"/> class.
		/// </summary>
		public RetailRateHistoryRow_Base()
		{
			// EMPTY
		}

		/// <summary>
		/// Gets or sets the <c>retail_route_id</c> column value.
		/// </summary>
		/// <value>The <c>retail_route_id</c> column value.</value>
		public int Retail_route_id
		{
			get { return _retail_route_id; }
			set { _retail_route_id = value; }
		}

		/// <summary>
		/// Gets or sets the <c>date_on</c> column value.
		/// </summary>
		/// <value>The <c>date_on</c> column value.</value>
		public System.DateTime Date_on
		{
			get { return _date_on; }
			set { _date_on = value; }
		}

		/// <summary>
		/// Gets or sets the <c>date_off</c> column value.
		/// </summary>
		/// <value>The <c>date_off</c> column value.</value>
		public System.DateTime Date_off
		{
			get { return _date_off; }
			set { _date_off = value; }
		}

		/// <summary>
		/// Gets or sets the <c>rate_info_id</c> column value.
		/// </summary>
		/// <value>The <c>rate_info_id</c> column value.</value>
		public int Rate_info_id
		{
			get { return _rate_info_id; }
			set { _rate_info_id = value; }
		}

		/// <summary>
		/// Gets or sets the <c>connect_fee</c> column value.
		/// </summary>
		/// <value>The <c>connect_fee</c> column value.</value>
		public decimal Connect_fee
		{
			get { return _connect_fee; }
			set { _connect_fee = value; }
		}

		/// <summary>
		/// Gets or sets the <c>disconnect_fee</c> column value.
		/// </summary>
		/// <value>The <c>disconnect_fee</c> column value.</value>
		public decimal Disconnect_fee
		{
			get { return _disconnect_fee; }
			set { _disconnect_fee = value; }
		}

		/// <summary>
		/// Gets or sets the <c>per_call_cost</c> column value.
		/// </summary>
		/// <value>The <c>per_call_cost</c> column value.</value>
		public decimal Per_call_cost
		{
			get { return _per_call_cost; }
			set { _per_call_cost = value; }
		}

		/// <summary>
		/// Gets or sets the <c>cost_increase_per_call</c> column value.
		/// </summary>
		/// <value>The <c>cost_increase_per_call</c> column value.</value>
		public int Cost_increase_per_call
		{
			get { return _cost_increase_per_call; }
			set { _cost_increase_per_call = value; }
		}

		/// <summary>
		/// Gets or sets the <c>cost_increase_per_call_start</c> column value.
		/// </summary>
		/// <value>The <c>cost_increase_per_call_start</c> column value.</value>
		public int Cost_increase_per_call_start
		{
			get { return _cost_increase_per_call_start; }
			set { _cost_increase_per_call_start = value; }
		}

		/// <summary>
		/// Gets or sets the <c>cost_increase_per_call_stop</c> column value.
		/// </summary>
		/// <value>The <c>cost_increase_per_call_stop</c> column value.</value>
		public int Cost_increase_per_call_stop
		{
			get { return _cost_increase_per_call_stop; }
			set { _cost_increase_per_call_stop = value; }
		}

		/// <summary>
		/// Gets or sets the <c>tax_first_incr_cost</c> column value.
		/// </summary>
		/// <value>The <c>tax_first_incr_cost</c> column value.</value>
		public decimal Tax_first_incr_cost
		{
			get { return _tax_first_incr_cost; }
			set { _tax_first_incr_cost = value; }
		}

		/// <summary>
		/// Gets or sets the <c>tax_add_incr_cost</c> column value.
		/// </summary>
		/// <value>The <c>tax_add_incr_cost</c> column value.</value>
		public decimal Tax_add_incr_cost
		{
			get { return _tax_add_incr_cost; }
			set { _tax_add_incr_cost = value; }
		}

		/// <summary>
		/// Gets or sets the <c>surcharge_delay</c> column value.
		/// </summary>
		/// <value>The <c>surcharge_delay</c> column value.</value>
		public byte Surcharge_delay
		{
			get { return _surcharge_delay; }
			set { _surcharge_delay = value; }
		}

		/// <summary>
		/// Gets or sets the <c>rating_delay</c> column value.
		/// </summary>
		/// <value>The <c>rating_delay</c> column value.</value>
		public byte Rating_delay
		{
			get { return _rating_delay; }
			set { _rating_delay = value; }
		}

		/// <summary>
		/// Returns the string representation of this instance.
		/// </summary>
		/// <returns>The string representation of this instance.</returns>
		public override string ToString()
		{
			System.Text.StringBuilder dynStr = new System.Text.StringBuilder(GetType().Name);
			dynStr.Append(':');
			dynStr.Append("  Retail_route_id=");
			dynStr.Append(Retail_route_id);
			dynStr.Append("  Date_on=");
			dynStr.Append(Date_on);
			dynStr.Append("  Date_off=");
			dynStr.Append(Date_off);
			dynStr.Append("  Rate_info_id=");
			dynStr.Append(Rate_info_id);
			dynStr.Append("  Connect_fee=");
			dynStr.Append(Connect_fee);
			dynStr.Append("  Disconnect_fee=");
			dynStr.Append(Disconnect_fee);
			dynStr.Append("  Per_call_cost=");
			dynStr.Append(Per_call_cost);
			dynStr.Append("  Cost_increase_per_call=");
			dynStr.Append(Cost_increase_per_call);
			dynStr.Append("  Cost_increase_per_call_start=");
			dynStr.Append(Cost_increase_per_call_start);
			dynStr.Append("  Cost_increase_per_call_stop=");
			dynStr.Append(Cost_increase_per_call_stop);
			dynStr.Append("  Tax_first_incr_cost=");
			dynStr.Append(Tax_first_incr_cost);
			dynStr.Append("  Tax_add_incr_cost=");
			dynStr.Append(Tax_add_incr_cost);
			dynStr.Append("  Surcharge_delay=");
			dynStr.Append(Surcharge_delay);
			dynStr.Append("  Rating_delay=");
			dynStr.Append(Rating_delay);
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

	} // End of RetailRateHistoryRow_Base class
} // End of namespace

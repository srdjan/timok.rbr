// <fileinfo name="Base\ResellRateHistoryRow_Base.cs">
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
	/// The base class for <see cref="ResellRateHistoryRow"/> that 
	/// represents a record in the <c>ResellRateHistory</c> table.
	/// </summary>
	/// <remarks>
	/// Do not change this source code manually. Update the <see cref="ResellRateHistoryRow"/>
	/// class if you need to add or change some functionality.
	/// </remarks>
	[Serializable]
	public abstract class ResellRateHistoryRow_Base
	{
	#region Timok Custom

		//db field names
		public const string resell_acct_id_DbName = "resell_acct_id";
		public const string wholesale_route_id_DbName = "wholesale_route_id";
		public const string date_on_DbName = "date_on";
		public const string date_off_DbName = "date_off";
		public const string rate_info_id_DbName = "rate_info_id";
		public const string commision_type_DbName = "commision_type";
		public const string markup_dollar_DbName = "markup_dollar";
		public const string markup_percent_DbName = "markup_percent";
		public const string markup_per_call_DbName = "markup_per_call";
		public const string markup_per_minute_DbName = "markup_per_minute";

		//prop names
		public const string resell_acct_id_PropName = "Resell_acct_id";
		public const string wholesale_route_id_PropName = "Wholesale_route_id";
		public const string date_on_PropName = "Date_on";
		public const string date_off_PropName = "Date_off";
		public const string rate_info_id_PropName = "Rate_info_id";
		public const string commision_type_PropName = "Commision_type";
		public const string markup_dollar_PropName = "Markup_dollar";
		public const string markup_percent_PropName = "Markup_percent";
		public const string markup_per_call_PropName = "Markup_per_call";
		public const string markup_per_minute_PropName = "Markup_per_minute";

		//db field display names
		public const string resell_acct_id_DisplayName = "resell acct id";
		public const string wholesale_route_id_DisplayName = "wholesale route id";
		public const string date_on_DisplayName = "date on";
		public const string date_off_DisplayName = "date off";
		public const string rate_info_id_DisplayName = "rate info id";
		public const string commision_type_DisplayName = "commision type";
		public const string markup_dollar_DisplayName = "markup dollar";
		public const string markup_percent_DisplayName = "markup percent";
		public const string markup_per_call_DisplayName = "markup per call";
		public const string markup_per_minute_DisplayName = "markup per minute";
	#endregion Timok Custom


		private short _resell_acct_id;
		private int _wholesale_route_id;
		private System.DateTime _date_on;
		private System.DateTime _date_off;
		private int _rate_info_id;
		private byte _commision_type;
		private decimal _markup_dollar;
		private decimal _markup_percent;
		private decimal _markup_per_call;
		private decimal _markup_per_minute;

		/// <summary>
		/// Initializes a new instance of the <see cref="ResellRateHistoryRow_Base"/> class.
		/// </summary>
		public ResellRateHistoryRow_Base()
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
		/// Gets or sets the <c>wholesale_route_id</c> column value.
		/// </summary>
		/// <value>The <c>wholesale_route_id</c> column value.</value>
		public int Wholesale_route_id
		{
			get { return _wholesale_route_id; }
			set { _wholesale_route_id = value; }
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
		/// Gets or sets the <c>markup_per_call</c> column value.
		/// </summary>
		/// <value>The <c>markup_per_call</c> column value.</value>
		public decimal Markup_per_call
		{
			get { return _markup_per_call; }
			set { _markup_per_call = value; }
		}

		/// <summary>
		/// Gets or sets the <c>markup_per_minute</c> column value.
		/// </summary>
		/// <value>The <c>markup_per_minute</c> column value.</value>
		public decimal Markup_per_minute
		{
			get { return _markup_per_minute; }
			set { _markup_per_minute = value; }
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
			dynStr.Append("  Wholesale_route_id=");
			dynStr.Append(Wholesale_route_id);
			dynStr.Append("  Date_on=");
			dynStr.Append(Date_on);
			dynStr.Append("  Date_off=");
			dynStr.Append(Date_off);
			dynStr.Append("  Rate_info_id=");
			dynStr.Append(Rate_info_id);
			dynStr.Append("  Commision_type=");
			dynStr.Append(Commision_type);
			dynStr.Append("  Markup_dollar=");
			dynStr.Append(Markup_dollar);
			dynStr.Append("  Markup_percent=");
			dynStr.Append(Markup_percent);
			dynStr.Append("  Markup_per_call=");
			dynStr.Append(Markup_per_call);
			dynStr.Append("  Markup_per_minute=");
			dynStr.Append(Markup_per_minute);
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

	} // End of ResellRateHistoryRow_Base class
} // End of namespace

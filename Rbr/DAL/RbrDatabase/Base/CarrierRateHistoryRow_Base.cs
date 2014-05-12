// <fileinfo name="Base\CarrierRateHistoryRow_Base.cs">
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
	/// The base class for <see cref="CarrierRateHistoryRow"/> that 
	/// represents a record in the <c>CarrierRateHistory</c> table.
	/// </summary>
	/// <remarks>
	/// Do not change this source code manually. Update the <see cref="CarrierRateHistoryRow"/>
	/// class if you need to add or change some functionality.
	/// </remarks>
	[Serializable]
	public abstract class CarrierRateHistoryRow_Base
	{
	#region Timok Custom

		//db field names
		public const string carrier_route_id_DbName = "carrier_route_id";
		public const string date_on_DbName = "date_on";
		public const string date_off_DbName = "date_off";
		public const string rate_info_id_DbName = "rate_info_id";

		//prop names
		public const string carrier_route_id_PropName = "Carrier_route_id";
		public const string date_on_PropName = "Date_on";
		public const string date_off_PropName = "Date_off";
		public const string rate_info_id_PropName = "Rate_info_id";

		//db field display names
		public const string carrier_route_id_DisplayName = "carrier route id";
		public const string date_on_DisplayName = "date on";
		public const string date_off_DisplayName = "date off";
		public const string rate_info_id_DisplayName = "rate info id";
	#endregion Timok Custom


		private int _carrier_route_id;
		private System.DateTime _date_on;
		private System.DateTime _date_off;
		private int _rate_info_id;

		/// <summary>
		/// Initializes a new instance of the <see cref="CarrierRateHistoryRow_Base"/> class.
		/// </summary>
		public CarrierRateHistoryRow_Base()
		{
			// EMPTY
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
		/// Returns the string representation of this instance.
		/// </summary>
		/// <returns>The string representation of this instance.</returns>
		public override string ToString()
		{
			System.Text.StringBuilder dynStr = new System.Text.StringBuilder(GetType().Name);
			dynStr.Append(':');
			dynStr.Append("  Carrier_route_id=");
			dynStr.Append(Carrier_route_id);
			dynStr.Append("  Date_on=");
			dynStr.Append(Date_on);
			dynStr.Append("  Date_off=");
			dynStr.Append(Date_off);
			dynStr.Append("  Rate_info_id=");
			dynStr.Append(Rate_info_id);
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

	} // End of CarrierRateHistoryRow_Base class
} // End of namespace

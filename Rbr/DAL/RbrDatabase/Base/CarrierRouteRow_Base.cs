// <fileinfo name="Base\CarrierRouteRow_Base.cs">
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
	/// The base class for <see cref="CarrierRouteRow"/> that 
	/// represents a record in the <c>CarrierRoute</c> table.
	/// </summary>
	/// <remarks>
	/// Do not change this source code manually. Update the <see cref="CarrierRouteRow"/>
	/// class if you need to add or change some functionality.
	/// </remarks>
	[Serializable]
	public abstract class CarrierRouteRow_Base
	{
	#region Timok Custom

		//db field names
		public const string carrier_route_id_DbName = "carrier_route_id";
		public const string carrier_acct_id_DbName = "carrier_acct_id";
		public const string route_id_DbName = "route_id";
		public const string status_DbName = "status";
		public const string asr_time_window_DbName = "asr_time_window";
		public const string asr_target_DbName = "asr_target";
		public const string acd_time_window_DbName = "acd_time_window";
		public const string acd_target_DbName = "acd_target";
		public const string next_ep_DbName = "next_ep";

		//prop names
		public const string carrier_route_id_PropName = "Carrier_route_id";
		public const string carrier_acct_id_PropName = "Carrier_acct_id";
		public const string route_id_PropName = "Route_id";
		public const string status_PropName = "Status";
		public const string asr_time_window_PropName = "Asr_time_window";
		public const string asr_target_PropName = "Asr_target";
		public const string acd_time_window_PropName = "Acd_time_window";
		public const string acd_target_PropName = "Acd_target";
		public const string next_ep_PropName = "Next_ep";

		//db field display names
		public const string carrier_route_id_DisplayName = "carrier route id";
		public const string carrier_acct_id_DisplayName = "carrier acct id";
		public const string route_id_DisplayName = "route id";
		public const string status_DisplayName = "status";
		public const string asr_time_window_DisplayName = "asr time window";
		public const string asr_target_DisplayName = "asr target";
		public const string acd_time_window_DisplayName = "acd time window";
		public const string acd_target_DisplayName = "acd target";
		public const string next_ep_DisplayName = "next ep";
	#endregion Timok Custom


		private int _carrier_route_id;
		private short _carrier_acct_id;
		private int _route_id;
		private bool _route_idNull = true;
		private byte _status;
		private int _asr_time_window;
		private short _asr_target;
		private int _acd_time_window;
		private short _acd_target;
		private byte _next_ep;

		/// <summary>
		/// Initializes a new instance of the <see cref="CarrierRouteRow_Base"/> class.
		/// </summary>
		public CarrierRouteRow_Base()
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
		/// Gets or sets the <c>carrier_acct_id</c> column value.
		/// </summary>
		/// <value>The <c>carrier_acct_id</c> column value.</value>
		public short Carrier_acct_id
		{
			get { return _carrier_acct_id; }
			set { _carrier_acct_id = value; }
		}

		/// <summary>
		/// Gets or sets the <c>route_id</c> column value.
		/// This column is nullable.
		/// </summary>
		/// <value>The <c>route_id</c> column value.</value>
		public int Route_id
		{
			get
			{
				//if(IsRoute_idNull)
					//throw new InvalidOperationException("Cannot get value because it is DBNull.");
				return _route_id;
			}
			set
			{
				_route_idNull = false;
				_route_id = value;
			}
		}

		/// <summary>
		/// Indicates whether the <see cref="Route_id"/>
		/// property value is null.
		/// </summary>
		/// <value>true if the property value is null, otherwise false.</value>
		public bool IsRoute_idNull
		{
			get { return _route_idNull; }
			set { _route_idNull = value; }
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
		/// Gets or sets the <c>asr_time_window</c> column value.
		/// </summary>
		/// <value>The <c>asr_time_window</c> column value.</value>
		public int Asr_time_window
		{
			get { return _asr_time_window; }
			set { _asr_time_window = value; }
		}

		/// <summary>
		/// Gets or sets the <c>asr_target</c> column value.
		/// </summary>
		/// <value>The <c>asr_target</c> column value.</value>
		public short Asr_target
		{
			get { return _asr_target; }
			set { _asr_target = value; }
		}

		/// <summary>
		/// Gets or sets the <c>acd_time_window</c> column value.
		/// </summary>
		/// <value>The <c>acd_time_window</c> column value.</value>
		public int Acd_time_window
		{
			get { return _acd_time_window; }
			set { _acd_time_window = value; }
		}

		/// <summary>
		/// Gets or sets the <c>acd_target</c> column value.
		/// </summary>
		/// <value>The <c>acd_target</c> column value.</value>
		public short Acd_target
		{
			get { return _acd_target; }
			set { _acd_target = value; }
		}

		/// <summary>
		/// Gets or sets the <c>next_ep</c> column value.
		/// </summary>
		/// <value>The <c>next_ep</c> column value.</value>
		public byte Next_ep
		{
			get { return _next_ep; }
			set { _next_ep = value; }
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
			dynStr.Append("  Carrier_acct_id=");
			dynStr.Append(Carrier_acct_id);
			dynStr.Append("  Route_id=");
			dynStr.Append(IsRoute_idNull ? (object)"<NULL>" : Route_id);
			dynStr.Append("  Status=");
			dynStr.Append(Status);
			dynStr.Append("  Asr_time_window=");
			dynStr.Append(Asr_time_window);
			dynStr.Append("  Asr_target=");
			dynStr.Append(Asr_target);
			dynStr.Append("  Acd_time_window=");
			dynStr.Append(Acd_time_window);
			dynStr.Append("  Acd_target=");
			dynStr.Append(Acd_target);
			dynStr.Append("  Next_ep=");
			dynStr.Append(Next_ep);
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

	} // End of CarrierRouteRow_Base class
} // End of namespace

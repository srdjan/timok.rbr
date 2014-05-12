// <fileinfo name="Base\RetailRouteRow_Base.cs">
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
	/// The base class for <see cref="RetailRouteRow"/> that 
	/// represents a record in the <c>RetailRoute</c> table.
	/// </summary>
	/// <remarks>
	/// Do not change this source code manually. Update the <see cref="RetailRouteRow"/>
	/// class if you need to add or change some functionality.
	/// </remarks>
	[Serializable]
	public abstract class RetailRouteRow_Base
	{
	#region Timok Custom

		//db field names
		public const string retail_route_id_DbName = "retail_route_id";
		public const string customer_acct_id_DbName = "customer_acct_id";
		public const string route_id_DbName = "route_id";
		public const string status_DbName = "status";
		public const string start_bonus_minutes_DbName = "start_bonus_minutes";
		public const string bonus_minutes_type_DbName = "bonus_minutes_type";
		public const string multiplier_DbName = "multiplier";

		//prop names
		public const string retail_route_id_PropName = "Retail_route_id";
		public const string customer_acct_id_PropName = "Customer_acct_id";
		public const string route_id_PropName = "Route_id";
		public const string status_PropName = "Status";
		public const string start_bonus_minutes_PropName = "Start_bonus_minutes";
		public const string bonus_minutes_type_PropName = "Bonus_minutes_type";
		public const string multiplier_PropName = "Multiplier";

		//db field display names
		public const string retail_route_id_DisplayName = "retail route id";
		public const string customer_acct_id_DisplayName = "customer acct id";
		public const string route_id_DisplayName = "route id";
		public const string status_DisplayName = "status";
		public const string start_bonus_minutes_DisplayName = "start bonus minutes";
		public const string bonus_minutes_type_DisplayName = "bonus minutes type";
		public const string multiplier_DisplayName = "multiplier";
	#endregion Timok Custom


		private int _retail_route_id;
		private short _customer_acct_id;
		private int _route_id;
		private bool _route_idNull = true;
		private byte _status;
		private short _start_bonus_minutes;
		private byte _bonus_minutes_type;
		private short _multiplier;

		/// <summary>
		/// Initializes a new instance of the <see cref="RetailRouteRow_Base"/> class.
		/// </summary>
		public RetailRouteRow_Base()
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
		/// Gets or sets the <c>customer_acct_id</c> column value.
		/// </summary>
		/// <value>The <c>customer_acct_id</c> column value.</value>
		public short Customer_acct_id
		{
			get { return _customer_acct_id; }
			set { _customer_acct_id = value; }
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
		/// Gets or sets the <c>start_bonus_minutes</c> column value.
		/// </summary>
		/// <value>The <c>start_bonus_minutes</c> column value.</value>
		public short Start_bonus_minutes
		{
			get { return _start_bonus_minutes; }
			set { _start_bonus_minutes = value; }
		}

		/// <summary>
		/// Gets or sets the <c>bonus_minutes_type</c> column value.
		/// </summary>
		/// <value>The <c>bonus_minutes_type</c> column value.</value>
		public byte Bonus_minutes_type
		{
			get { return _bonus_minutes_type; }
			set { _bonus_minutes_type = value; }
		}

		/// <summary>
		/// Gets or sets the <c>multiplier</c> column value.
		/// </summary>
		/// <value>The <c>multiplier</c> column value.</value>
		public short Multiplier
		{
			get { return _multiplier; }
			set { _multiplier = value; }
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
			dynStr.Append("  Customer_acct_id=");
			dynStr.Append(Customer_acct_id);
			dynStr.Append("  Route_id=");
			dynStr.Append(IsRoute_idNull ? (object)"<NULL>" : Route_id);
			dynStr.Append("  Status=");
			dynStr.Append(Status);
			dynStr.Append("  Start_bonus_minutes=");
			dynStr.Append(Start_bonus_minutes);
			dynStr.Append("  Bonus_minutes_type=");
			dynStr.Append(Bonus_minutes_type);
			dynStr.Append("  Multiplier=");
			dynStr.Append(Multiplier);
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

	} // End of RetailRouteRow_Base class
} // End of namespace

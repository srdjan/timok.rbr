// <fileinfo name="Base\WholesaleRouteRow_Base.cs">
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
	/// The base class for <see cref="WholesaleRouteRow"/> that 
	/// represents a record in the <c>WholesaleRoute</c> table.
	/// </summary>
	/// <remarks>
	/// Do not change this source code manually. Update the <see cref="WholesaleRouteRow"/>
	/// class if you need to add or change some functionality.
	/// </remarks>
	[Serializable]
	public abstract class WholesaleRouteRow_Base
	{
	#region Timok Custom

		//db field names
		public const string wholesale_route_id_DbName = "wholesale_route_id";
		public const string service_id_DbName = "service_id";
		public const string route_id_DbName = "route_id";
		public const string status_DbName = "status";

		//prop names
		public const string wholesale_route_id_PropName = "Wholesale_route_id";
		public const string service_id_PropName = "Service_id";
		public const string route_id_PropName = "Route_id";
		public const string status_PropName = "Status";

		//db field display names
		public const string wholesale_route_id_DisplayName = "wholesale route id";
		public const string service_id_DisplayName = "service id";
		public const string route_id_DisplayName = "route id";
		public const string status_DisplayName = "status";
	#endregion Timok Custom


		private int _wholesale_route_id;
		private short _service_id;
		private int _route_id;
		private bool _route_idNull = true;
		private byte _status;

		/// <summary>
		/// Initializes a new instance of the <see cref="WholesaleRouteRow_Base"/> class.
		/// </summary>
		public WholesaleRouteRow_Base()
		{
			// EMPTY
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
		/// Gets or sets the <c>service_id</c> column value.
		/// </summary>
		/// <value>The <c>service_id</c> column value.</value>
		public short Service_id
		{
			get { return _service_id; }
			set { _service_id = value; }
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
		/// Returns the string representation of this instance.
		/// </summary>
		/// <returns>The string representation of this instance.</returns>
		public override string ToString()
		{
			System.Text.StringBuilder dynStr = new System.Text.StringBuilder(GetType().Name);
			dynStr.Append(':');
			dynStr.Append("  Wholesale_route_id=");
			dynStr.Append(Wholesale_route_id);
			dynStr.Append("  Service_id=");
			dynStr.Append(Service_id);
			dynStr.Append("  Route_id=");
			dynStr.Append(IsRoute_idNull ? (object)"<NULL>" : Route_id);
			dynStr.Append("  Status=");
			dynStr.Append(Status);
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

	} // End of WholesaleRouteRow_Base class
} // End of namespace

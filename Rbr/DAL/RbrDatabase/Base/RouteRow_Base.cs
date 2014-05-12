// <fileinfo name="RouteRow_Base.cs">
//		<copyright>
//			All rights reserved.
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
	/// The base class for <see cref="RouteRow"/> that 
	/// represents a record in the <c>Route</c> table.
	/// </summary>
	/// <remarks>
	/// Do not change this source code manually. Update the <see cref="RouteRow"/>
	/// class if you need to add or change some functionality.
	/// </remarks>
	[Serializable]
	public abstract class RouteRow_Base
	{
	#region Timok Custom

		//db field names
		public const string route_id_DbName = "route_id";
		public const string name_DbName = "name";
		public const string status_DbName = "status";
		public const string calling_plan_id_DbName = "calling_plan_id";
		public const string country_id_DbName = "country_id";
		public const string version_DbName = "version";
		public const string routing_number_DbName = "routing_number";

		//prop names
		public const string route_id_PropName = "Route_id";
		public const string name_PropName = "Name";
		public const string status_PropName = "Status";
		public const string calling_plan_id_PropName = "Calling_plan_id";
		public const string country_id_PropName = "Country_id";
		public const string version_PropName = "Version";
		public const string routing_number_PropName = "Routing_number";

		//db field display names
		public const string route_id_DisplayName = "route id";
		public const string name_DisplayName = "name";
		public const string status_DisplayName = "status";
		public const string calling_plan_id_DisplayName = "calling plan id";
		public const string country_id_DisplayName = "country id";
		public const string version_DisplayName = "version";
		public const string routing_number_DisplayName = "routing_number";
	#endregion Timok Custom


		private int _route_id;
		private string _name;
		private byte _status;
		private int _calling_plan_id;
		private int _country_id;
		private int _version;
		private int _routing_number;
		private bool _routing_numberNull = true;

		/// <summary>
		/// Initializes a new instance of the <see cref="RouteRow_Base"/> class.
		/// </summary>
		public RouteRow_Base()
		{
			// EMPTY
		}

		/// <summary>
		/// Gets or sets the <c>route_id</c> column value.
		/// </summary>
		/// <value>The <c>route_id</c> column value.</value>
		public int Route_id
		{
			get { return _route_id; }
			set { _route_id = value; }
		}

		/// <summary>
		/// Gets or sets the <c>name</c> column value.
		/// </summary>
		/// <value>The <c>name</c> column value.</value>
		public string Name
		{
			get { return _name; }
			set { _name = value; }
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
		/// Gets or sets the <c>calling_plan_id</c> column value.
		/// </summary>
		/// <value>The <c>calling_plan_id</c> column value.</value>
		public int Calling_plan_id
		{
			get { return _calling_plan_id; }
			set { _calling_plan_id = value; }
		}

		/// <summary>
		/// Gets or sets the <c>country_id</c> column value.
		/// </summary>
		/// <value>The <c>country_id</c> column value.</value>
		public int Country_id
		{
			get { return _country_id; }
			set { _country_id = value; }
		}

		/// <summary>
		/// Gets or sets the <c>version</c> column value.
		/// </summary>
		/// <value>The <c>version</c> column value.</value>
		public int Version
		{
			get { return _version; }
			set { _version = value; }
		}

		/// <summary>
		/// Gets or sets the <c>routing_number</c> column value.
		/// This column is nullable.
		/// </summary>
		/// <value>The <c>routing_number</c> column value.</value>
		public int Routing_number
		{
			get
			{
				//if(IsRouting_numberNull)
				//  throw new InvalidOperationException("Cannot get value because it is DBNull.");
				return _routing_number;
			}
			set
			{
				_routing_numberNull = false;
				_routing_number = value;
			}
		}

		/// <summary>
		/// Indicates whether the <see cref="Routing_number"/>
		/// property value is null.
		/// </summary>
		/// <value>true if the property value is null, otherwise false.</value>
		public bool IsRouting_numberNull
		{
			get { return _routing_numberNull; }
			set { _routing_numberNull = value; }
		}

		/// <summary>
		/// Returns the string representation of this instance.
		/// </summary>
		/// <returns>The string representation of this instance.</returns>
		public override string ToString()
		{
			System.Text.StringBuilder dynStr = new System.Text.StringBuilder(GetType().Name);
			dynStr.Append(':');
			dynStr.Append("  Route_id=");
			dynStr.Append(Route_id);
			dynStr.Append("  Name=");
			dynStr.Append(Name);
			dynStr.Append("  Status=");
			dynStr.Append(Status);
			dynStr.Append("  Calling_plan_id=");
			dynStr.Append(Calling_plan_id);
			dynStr.Append("  Country_id=");
			dynStr.Append(Country_id);
			dynStr.Append("  Version=");
			dynStr.Append(Version);
			dynStr.Append("  Routing_number=");
			dynStr.Append(IsRouting_numberNull ? (object)"<NULL>" : Routing_number);
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

	} // End of RouteRow_Base class
} // End of namespace

// <fileinfo name="Base\TerminationChoiceRow_Base.cs">
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
	/// The base class for <see cref="TerminationChoiceRow"/> that 
	/// represents a record in the <c>TerminationChoice</c> table.
	/// </summary>
	/// <remarks>
	/// Do not change this source code manually. Update the <see cref="TerminationChoiceRow"/>
	/// class if you need to add or change some functionality.
	/// </remarks>
	[Serializable]
	public abstract class TerminationChoiceRow_Base
	{
	#region Timok Custom

		//db field names
		public const string termination_choice_id_DbName = "termination_choice_id";
		public const string routing_plan_id_DbName = "routing_plan_id";
		public const string route_id_DbName = "route_id";
		public const string priority_DbName = "priority";
		public const string carrier_route_id_DbName = "carrier_route_id";
		public const string version_DbName = "version";

		//prop names
		public const string termination_choice_id_PropName = "Termination_choice_id";
		public const string routing_plan_id_PropName = "Routing_plan_id";
		public const string route_id_PropName = "Route_id";
		public const string priority_PropName = "Priority";
		public const string carrier_route_id_PropName = "Carrier_route_id";
		public const string version_PropName = "Version";

		//db field display names
		public const string termination_choice_id_DisplayName = "termination choice id";
		public const string routing_plan_id_DisplayName = "routing plan id";
		public const string route_id_DisplayName = "route id";
		public const string priority_DisplayName = "priority";
		public const string carrier_route_id_DisplayName = "carrier route id";
		public const string version_DisplayName = "version";
	#endregion Timok Custom


		private int _termination_choice_id;
		private int _routing_plan_id;
		private int _route_id;
		private byte _priority;
		private int _carrier_route_id;
		private int _version;

		/// <summary>
		/// Initializes a new instance of the <see cref="TerminationChoiceRow_Base"/> class.
		/// </summary>
		public TerminationChoiceRow_Base()
		{
			// EMPTY
		}

		/// <summary>
		/// Gets or sets the <c>termination_choice_id</c> column value.
		/// </summary>
		/// <value>The <c>termination_choice_id</c> column value.</value>
		public int Termination_choice_id
		{
			get { return _termination_choice_id; }
			set { _termination_choice_id = value; }
		}

		/// <summary>
		/// Gets or sets the <c>routing_plan_id</c> column value.
		/// </summary>
		/// <value>The <c>routing_plan_id</c> column value.</value>
		public int Routing_plan_id
		{
			get { return _routing_plan_id; }
			set { _routing_plan_id = value; }
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
		/// Gets or sets the <c>priority</c> column value.
		/// </summary>
		/// <value>The <c>priority</c> column value.</value>
		public byte Priority
		{
			get { return _priority; }
			set { _priority = value; }
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
		/// Gets or sets the <c>version</c> column value.
		/// </summary>
		/// <value>The <c>version</c> column value.</value>
		public int Version
		{
			get { return _version; }
			set { _version = value; }
		}

		/// <summary>
		/// Returns the string representation of this instance.
		/// </summary>
		/// <returns>The string representation of this instance.</returns>
		public override string ToString()
		{
			System.Text.StringBuilder dynStr = new System.Text.StringBuilder(GetType().Name);
			dynStr.Append(':');
			dynStr.Append("  Termination_choice_id=");
			dynStr.Append(Termination_choice_id);
			dynStr.Append("  Routing_plan_id=");
			dynStr.Append(Routing_plan_id);
			dynStr.Append("  Route_id=");
			dynStr.Append(Route_id);
			dynStr.Append("  Priority=");
			dynStr.Append(Priority);
			dynStr.Append("  Carrier_route_id=");
			dynStr.Append(Carrier_route_id);
			dynStr.Append("  Version=");
			dynStr.Append(Version);
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

	} // End of TerminationChoiceRow_Base class
} // End of namespace

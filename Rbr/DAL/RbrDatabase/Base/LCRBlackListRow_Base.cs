// <fileinfo name="Base\LCRBlackListRow_Base.cs">
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
	/// The base class for <see cref="LCRBlackListRow"/> that 
	/// represents a record in the <c>LCRBlackList</c> table.
	/// </summary>
	/// <remarks>
	/// Do not change this source code manually. Update the <see cref="LCRBlackListRow"/>
	/// class if you need to add or change some functionality.
	/// </remarks>
	[Serializable]
	public abstract class LCRBlackListRow_Base
	{
	#region Timok Custom

		//db field names
		public const string routing_plan_id_DbName = "routing_plan_id";
		public const string route_id_DbName = "route_id";
		public const string carrier_acct_id_DbName = "carrier_acct_id";
		public const string version_DbName = "version";

		//prop names
		public const string routing_plan_id_PropName = "Routing_plan_id";
		public const string route_id_PropName = "Route_id";
		public const string carrier_acct_id_PropName = "Carrier_acct_id";
		public const string version_PropName = "Version";

		//db field display names
		public const string routing_plan_id_DisplayName = "routing plan id";
		public const string route_id_DisplayName = "route id";
		public const string carrier_acct_id_DisplayName = "carrier acct id";
		public const string version_DisplayName = "version";
	#endregion Timok Custom


		private int _routing_plan_id;
		private int _route_id;
		private short _carrier_acct_id;
		private int _version;

		/// <summary>
		/// Initializes a new instance of the <see cref="LCRBlackListRow_Base"/> class.
		/// </summary>
		public LCRBlackListRow_Base()
		{
			// EMPTY
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
		/// Gets or sets the <c>carrier_acct_id</c> column value.
		/// </summary>
		/// <value>The <c>carrier_acct_id</c> column value.</value>
		public short Carrier_acct_id
		{
			get { return _carrier_acct_id; }
			set { _carrier_acct_id = value; }
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
			dynStr.Append("  Routing_plan_id=");
			dynStr.Append(Routing_plan_id);
			dynStr.Append("  Route_id=");
			dynStr.Append(Route_id);
			dynStr.Append("  Carrier_acct_id=");
			dynStr.Append(Carrier_acct_id);
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

	} // End of LCRBlackListRow_Base class
} // End of namespace

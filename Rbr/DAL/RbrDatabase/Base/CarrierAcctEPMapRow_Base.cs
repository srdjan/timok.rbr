// <fileinfo name="Base\CarrierAcctEPMapRow_Base.cs">
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
	/// The base class for <see cref="CarrierAcctEPMapRow"/> that 
	/// represents a record in the <c>CarrierAcctEPMap</c> table.
	/// </summary>
	/// <remarks>
	/// Do not change this source code manually. Update the <see cref="CarrierAcctEPMapRow"/>
	/// class if you need to add or change some functionality.
	/// </remarks>
	[Serializable]
	public abstract class CarrierAcctEPMapRow_Base
	{
	#region Timok Custom

		//db field names
		public const string carrier_acct_EP_map_id_DbName = "carrier_acct_EP_map_id";
		public const string carrier_route_id_DbName = "carrier_route_id";
		public const string end_point_id_DbName = "end_point_id";
		public const string priority_DbName = "priority";
		public const string carrier_acct_id_DbName = "carrier_acct_id";

		//prop names
		public const string carrier_acct_EP_map_id_PropName = "Carrier_acct_EP_map_id";
		public const string carrier_route_id_PropName = "Carrier_route_id";
		public const string end_point_id_PropName = "End_point_id";
		public const string priority_PropName = "Priority";
		public const string carrier_acct_id_PropName = "Carrier_acct_id";

		//db field display names
		public const string carrier_acct_EP_map_id_DisplayName = "carrier acct EP map id";
		public const string carrier_route_id_DisplayName = "carrier route id";
		public const string end_point_id_DisplayName = "end point id";
		public const string priority_DisplayName = "priority";
		public const string carrier_acct_id_DisplayName = "carrier acct id";
	#endregion Timok Custom


		private int _carrier_acct_EP_map_id;
		private int _carrier_route_id;
		private short _end_point_id;
		private byte _priority;
		private short _carrier_acct_id;

		/// <summary>
		/// Initializes a new instance of the <see cref="CarrierAcctEPMapRow_Base"/> class.
		/// </summary>
		public CarrierAcctEPMapRow_Base()
		{
			// EMPTY
		}

		/// <summary>
		/// Gets or sets the <c>carrier_acct_EP_map_id</c> column value.
		/// </summary>
		/// <value>The <c>carrier_acct_EP_map_id</c> column value.</value>
		public int Carrier_acct_EP_map_id
		{
			get { return _carrier_acct_EP_map_id; }
			set { _carrier_acct_EP_map_id = value; }
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
		/// Gets or sets the <c>end_point_id</c> column value.
		/// </summary>
		/// <value>The <c>end_point_id</c> column value.</value>
		public short End_point_id
		{
			get { return _end_point_id; }
			set { _end_point_id = value; }
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
		/// Gets or sets the <c>carrier_acct_id</c> column value.
		/// </summary>
		/// <value>The <c>carrier_acct_id</c> column value.</value>
		public short Carrier_acct_id
		{
			get { return _carrier_acct_id; }
			set { _carrier_acct_id = value; }
		}

		/// <summary>
		/// Returns the string representation of this instance.
		/// </summary>
		/// <returns>The string representation of this instance.</returns>
		public override string ToString()
		{
			System.Text.StringBuilder dynStr = new System.Text.StringBuilder(GetType().Name);
			dynStr.Append(':');
			dynStr.Append("  Carrier_acct_EP_map_id=");
			dynStr.Append(Carrier_acct_EP_map_id);
			dynStr.Append("  Carrier_route_id=");
			dynStr.Append(Carrier_route_id);
			dynStr.Append("  End_point_id=");
			dynStr.Append(End_point_id);
			dynStr.Append("  Priority=");
			dynStr.Append(Priority);
			dynStr.Append("  Carrier_acct_id=");
			dynStr.Append(Carrier_acct_id);
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

	} // End of CarrierAcctEPMapRow_Base class
} // End of namespace

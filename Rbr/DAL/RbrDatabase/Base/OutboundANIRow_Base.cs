// <fileinfo name="Base\OutboundANIRow_Base.cs">
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
	/// The base class for <see cref="OutboundANIRow"/> that 
	/// represents a record in the <c>OutboundANI</c> table.
	/// </summary>
	/// <remarks>
	/// Do not change this source code manually. Update the <see cref="OutboundANIRow"/>
	/// class if you need to add or change some functionality.
	/// </remarks>
	[Serializable]
	public abstract class OutboundANIRow_Base
	{
	#region Timok Custom

		//db field names
		public const string outbound_ani_id_DbName = "outbound_ani_id";
		public const string ANI_DbName = "ANI";
		public const string carrier_route_id_DbName = "carrier_route_id";
		public const string version_DbName = "version";

		//prop names
		public const string outbound_ani_id_PropName = "Outbound_ani_id";
		public const string ANI_PropName = "ANI";
		public const string carrier_route_id_PropName = "Carrier_route_id";
		public const string version_PropName = "Version";

		//db field display names
		public const string outbound_ani_id_DisplayName = "outbound ani id";
		public const string ANI_DisplayName = "ANI";
		public const string carrier_route_id_DisplayName = "carrier route id";
		public const string version_DisplayName = "version";
	#endregion Timok Custom


		private int _outbound_ani_id;
		private long _ani;
		private int _carrier_route_id;
		private int _version;

		/// <summary>
		/// Initializes a new instance of the <see cref="OutboundANIRow_Base"/> class.
		/// </summary>
		public OutboundANIRow_Base()
		{
			// EMPTY
		}

		/// <summary>
		/// Gets or sets the <c>outbound_ani_id</c> column value.
		/// </summary>
		/// <value>The <c>outbound_ani_id</c> column value.</value>
		public int Outbound_ani_id
		{
			get { return _outbound_ani_id; }
			set { _outbound_ani_id = value; }
		}

		/// <summary>
		/// Gets or sets the <c>ANI</c> column value.
		/// </summary>
		/// <value>The <c>ANI</c> column value.</value>
		public long ANI
		{
			get { return _ani; }
			set { _ani = value; }
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
			dynStr.Append("  Outbound_ani_id=");
			dynStr.Append(Outbound_ani_id);
			dynStr.Append("  ANI=");
			dynStr.Append(ANI);
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

	} // End of OutboundANIRow_Base class
} // End of namespace

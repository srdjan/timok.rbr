// <fileinfo name="Base\IPAddressRow_Base.cs">
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
	/// The base class for <see cref="IPAddressRow"/> that 
	/// represents a record in the <c>IPAddress</c> table.
	/// </summary>
	/// <remarks>
	/// Do not change this source code manually. Update the <see cref="IPAddressRow"/>
	/// class if you need to add or change some functionality.
	/// </remarks>
	[Serializable]
	public abstract class IPAddressRow_Base
	{
	#region Timok Custom

		//db field names
		public const string IP_address_DbName = "IP_address";
		public const string end_point_id_DbName = "end_point_id";

		//prop names
		public const string IP_address_PropName = "IP_address";
		public const string end_point_id_PropName = "End_point_id";

		//db field display names
		public const string IP_address_DisplayName = "IP address";
		public const string end_point_id_DisplayName = "end point id";
	#endregion Timok Custom


		private int _iP_address;
		private short _end_point_id;

		/// <summary>
		/// Initializes a new instance of the <see cref="IPAddressRow_Base"/> class.
		/// </summary>
		public IPAddressRow_Base()
		{
			// EMPTY
		}

		/// <summary>
		/// Gets or sets the <c>IP_address</c> column value.
		/// </summary>
		/// <value>The <c>IP_address</c> column value.</value>
		public int IP_address
		{
			get { return _iP_address; }
			set { _iP_address = value; }
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
		/// Returns the string representation of this instance.
		/// </summary>
		/// <returns>The string representation of this instance.</returns>
		public override string ToString()
		{
			System.Text.StringBuilder dynStr = new System.Text.StringBuilder(GetType().Name);
			dynStr.Append(':');
			dynStr.Append("  IP_address=");
			dynStr.Append(IP_address);
			dynStr.Append("  End_point_id=");
			dynStr.Append(End_point_id);
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

	} // End of IPAddressRow_Base class
} // End of namespace

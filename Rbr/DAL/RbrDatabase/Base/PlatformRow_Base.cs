// <fileinfo name="Base\PlatformRow_Base.cs">
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
	/// The base class for <see cref="PlatformRow"/> that 
	/// represents a record in the <c>Platform</c> table.
	/// </summary>
	/// <remarks>
	/// Do not change this source code manually. Update the <see cref="PlatformRow"/>
	/// class if you need to add or change some functionality.
	/// </remarks>
	[Serializable]
	public abstract class PlatformRow_Base
	{
	#region Timok Custom

		//db field names
		public const string platform_id_DbName = "platform_id";
		public const string location_DbName = "location";
		public const string status_DbName = "status";
		public const string platform_config_DbName = "platform_config";

		//prop names
		public const string platform_id_PropName = "Platform_id";
		public const string location_PropName = "Location";
		public const string status_PropName = "Status";
		public const string platform_config_PropName = "Platform_config";

		//db field display names
		public const string platform_id_DisplayName = "platform id";
		public const string location_DisplayName = "location";
		public const string status_DisplayName = "status";
		public const string platform_config_DisplayName = "platform config";
	#endregion Timok Custom


		private short _platform_id;
		private string _location;
		private byte _status;
		private int _platform_config;

		/// <summary>
		/// Initializes a new instance of the <see cref="PlatformRow_Base"/> class.
		/// </summary>
		public PlatformRow_Base()
		{
			// EMPTY
		}

		/// <summary>
		/// Gets or sets the <c>platform_id</c> column value.
		/// </summary>
		/// <value>The <c>platform_id</c> column value.</value>
		public short Platform_id
		{
			get { return _platform_id; }
			set { _platform_id = value; }
		}

		/// <summary>
		/// Gets or sets the <c>location</c> column value.
		/// </summary>
		/// <value>The <c>location</c> column value.</value>
		public string Location
		{
			get { return _location; }
			set { _location = value; }
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
		/// Gets or sets the <c>platform_config</c> column value.
		/// </summary>
		/// <value>The <c>platform_config</c> column value.</value>
		public int Platform_config
		{
			get { return _platform_config; }
			set { _platform_config = value; }
		}

		/// <summary>
		/// Returns the string representation of this instance.
		/// </summary>
		/// <returns>The string representation of this instance.</returns>
		public override string ToString()
		{
			System.Text.StringBuilder dynStr = new System.Text.StringBuilder(GetType().Name);
			dynStr.Append(':');
			dynStr.Append("  Platform_id=");
			dynStr.Append(Platform_id);
			dynStr.Append("  Location=");
			dynStr.Append(Location);
			dynStr.Append("  Status=");
			dynStr.Append(Status);
			dynStr.Append("  Platform_config=");
			dynStr.Append(Platform_config);
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

	} // End of PlatformRow_Base class
} // End of namespace

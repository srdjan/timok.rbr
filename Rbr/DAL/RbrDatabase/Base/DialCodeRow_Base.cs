// <fileinfo name="Base\DialCodeRow_Base.cs">
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
	/// The base class for <see cref="DialCodeRow"/> that 
	/// represents a record in the <c>DialCode</c> table.
	/// </summary>
	/// <remarks>
	/// Do not change this source code manually. Update the <see cref="DialCodeRow"/>
	/// class if you need to add or change some functionality.
	/// </remarks>
	[Serializable]
	public abstract class DialCodeRow_Base
	{
	#region Timok Custom

		//db field names
		public const string calling_plan_id_DbName = "calling_plan_id";
		public const string dial_code_DbName = "dial_code";
		public const string route_id_DbName = "route_id";
		public const string version_DbName = "version";

		//prop names
		public const string calling_plan_id_PropName = "Calling_plan_id";
		public const string dial_code_PropName = "Dial_code";
		public const string route_id_PropName = "Route_id";
		public const string version_PropName = "Version";

		//db field display names
		public const string calling_plan_id_DisplayName = "calling plan id";
		public const string dial_code_DisplayName = "dial code";
		public const string route_id_DisplayName = "route id";
		public const string version_DisplayName = "version";
	#endregion Timok Custom


		private int _calling_plan_id;
		private long _dial_code;
		private int _route_id;
		private int _version;

		/// <summary>
		/// Initializes a new instance of the <see cref="DialCodeRow_Base"/> class.
		/// </summary>
		public DialCodeRow_Base()
		{
			// EMPTY
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
		/// Gets or sets the <c>dial_code</c> column value.
		/// </summary>
		/// <value>The <c>dial_code</c> column value.</value>
		public long Dial_code
		{
			get { return _dial_code; }
			set { _dial_code = value; }
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
			var dynStr = new System.Text.StringBuilder(GetType().Name);
			dynStr.Append(':');
			dynStr.Append("  Calling_plan_id=");
			dynStr.Append(Calling_plan_id);
			dynStr.Append("  Dial_code=");
			dynStr.Append(Dial_code);
			dynStr.Append("  Route_id=");
			dynStr.Append(Route_id);
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

	} // End of DialCodeRow_Base class
} // End of namespace

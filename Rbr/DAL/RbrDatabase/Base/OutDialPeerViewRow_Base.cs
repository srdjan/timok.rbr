// <fileinfo name="Base\OutDialPeerViewRow_Base.cs">
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
	/// The base class for <see cref="OutDialPeerViewRow"/> that 
	/// represents a record in the <c>OutDialPeerView</c> view.
	/// </summary>
	/// <remarks>
	/// Do not change this source code manually. Update the <see cref="OutDialPeerViewRow"/>
	/// class if you need to add or change some functionality.
	/// </remarks>
	[Serializable]
	public abstract class OutDialPeerViewRow_Base
	{
	#region Timok Custom

		//db field names
		public const string end_point_id_DbName = "end_point_id";
		public const string carrier_acct_id_DbName = "carrier_acct_id";
		public const string alias_DbName = "alias";
		public const string carrier_acct_name_DbName = "carrier_acct_name";
		public const string carrier_acct_status_DbName = "carrier_acct_status";
		public const string prefix_out_DbName = "prefix_out";
		public const string partner_id_DbName = "partner_id";
		public const string partner_name_DbName = "partner_name";
		public const string partner_status_DbName = "partner_status";

		//prop names
		public const string end_point_id_PropName = "End_point_id";
		public const string carrier_acct_id_PropName = "Carrier_acct_id";
		public const string alias_PropName = "Alias";
		public const string carrier_acct_name_PropName = "Carrier_acct_name";
		public const string carrier_acct_status_PropName = "Carrier_acct_status";
		public const string prefix_out_PropName = "Prefix_out";
		public const string partner_id_PropName = "Partner_id";
		public const string partner_name_PropName = "Partner_name";
		public const string partner_status_PropName = "Partner_status";

		//db field display names
		public const string end_point_id_DisplayName = "end point id";
		public const string carrier_acct_id_DisplayName = "carrier acct id";
		public const string alias_DisplayName = "alias";
		public const string carrier_acct_name_DisplayName = "carrier acct name";
		public const string carrier_acct_status_DisplayName = "carrier acct status";
		public const string prefix_out_DisplayName = "prefix out";
		public const string partner_id_DisplayName = "partner id";
		public const string partner_name_DisplayName = "partner name";
		public const string partner_status_DisplayName = "partner status";
	#endregion Timok Custom


		private short _end_point_id;
		private short _carrier_acct_id;
		private string _alias;
		private string _carrier_acct_name;
		private byte _carrier_acct_status;
		private string _prefix_out;
		private int _partner_id;
		private string _partner_name;
		private byte _partner_status;

		/// <summary>
		/// Initializes a new instance of the <see cref="OutDialPeerViewRow_Base"/> class.
		/// </summary>
		public OutDialPeerViewRow_Base()
		{
			// EMPTY
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
		/// Gets or sets the <c>carrier_acct_id</c> column value.
		/// </summary>
		/// <value>The <c>carrier_acct_id</c> column value.</value>
		public short Carrier_acct_id
		{
			get { return _carrier_acct_id; }
			set { _carrier_acct_id = value; }
		}

		/// <summary>
		/// Gets or sets the <c>alias</c> column value.
		/// </summary>
		/// <value>The <c>alias</c> column value.</value>
		public string Alias
		{
			get { return _alias; }
			set { _alias = value; }
		}

		/// <summary>
		/// Gets or sets the <c>carrier_acct_name</c> column value.
		/// </summary>
		/// <value>The <c>carrier_acct_name</c> column value.</value>
		public string Carrier_acct_name
		{
			get { return _carrier_acct_name; }
			set { _carrier_acct_name = value; }
		}

		/// <summary>
		/// Gets or sets the <c>carrier_acct_status</c> column value.
		/// </summary>
		/// <value>The <c>carrier_acct_status</c> column value.</value>
		public byte Carrier_acct_status
		{
			get { return _carrier_acct_status; }
			set { _carrier_acct_status = value; }
		}

		/// <summary>
		/// Gets or sets the <c>prefix_out</c> column value.
		/// </summary>
		/// <value>The <c>prefix_out</c> column value.</value>
		public string Prefix_out
		{
			get { return _prefix_out; }
			set { _prefix_out = value; }
		}

		/// <summary>
		/// Gets or sets the <c>partner_id</c> column value.
		/// </summary>
		/// <value>The <c>partner_id</c> column value.</value>
		public int Partner_id
		{
			get { return _partner_id; }
			set { _partner_id = value; }
		}

		/// <summary>
		/// Gets or sets the <c>partner_name</c> column value.
		/// </summary>
		/// <value>The <c>partner_name</c> column value.</value>
		public string Partner_name
		{
			get { return _partner_name; }
			set { _partner_name = value; }
		}

		/// <summary>
		/// Gets or sets the <c>partner_status</c> column value.
		/// </summary>
		/// <value>The <c>partner_status</c> column value.</value>
		public byte Partner_status
		{
			get { return _partner_status; }
			set { _partner_status = value; }
		}

		/// <summary>
		/// Returns the string representation of this instance.
		/// </summary>
		/// <returns>The string representation of this instance.</returns>
		public override string ToString()
		{
			System.Text.StringBuilder dynStr = new System.Text.StringBuilder(GetType().Name);
			dynStr.Append(':');
			dynStr.Append("  End_point_id=");
			dynStr.Append(End_point_id);
			dynStr.Append("  Carrier_acct_id=");
			dynStr.Append(Carrier_acct_id);
			dynStr.Append("  Alias=");
			dynStr.Append(Alias);
			dynStr.Append("  Carrier_acct_name=");
			dynStr.Append(Carrier_acct_name);
			dynStr.Append("  Carrier_acct_status=");
			dynStr.Append(Carrier_acct_status);
			dynStr.Append("  Prefix_out=");
			dynStr.Append(Prefix_out);
			dynStr.Append("  Partner_id=");
			dynStr.Append(Partner_id);
			dynStr.Append("  Partner_name=");
			dynStr.Append(Partner_name);
			dynStr.Append("  Partner_status=");
			dynStr.Append(Partner_status);
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

	} // End of OutDialPeerViewRow_Base class
} // End of namespace

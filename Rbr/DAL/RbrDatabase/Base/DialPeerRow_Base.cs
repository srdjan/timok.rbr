// <fileinfo name="Base\DialPeerRow_Base.cs">
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
	/// The base class for <see cref="DialPeerRow"/> that 
	/// represents a record in the <c>DialPeer</c> table.
	/// </summary>
	/// <remarks>
	/// Do not change this source code manually. Update the <see cref="DialPeerRow"/>
	/// class if you need to add or change some functionality.
	/// </remarks>
	[Serializable]
	public abstract class DialPeerRow_Base
	{
	#region Timok Custom

		//db field names
		public const string end_point_id_DbName = "end_point_id";
		public const string prefix_in_DbName = "prefix_in";
		public const string customer_acct_id_DbName = "customer_acct_id";

		//prop names
		public const string end_point_id_PropName = "End_point_id";
		public const string prefix_in_PropName = "Prefix_in";
		public const string customer_acct_id_PropName = "Customer_acct_id";

		//db field display names
		public const string end_point_id_DisplayName = "end point id";
		public const string prefix_in_DisplayName = "prefix in";
		public const string customer_acct_id_DisplayName = "customer acct id";
	#endregion Timok Custom


		private short _end_point_id;
		private string _prefix_in;
		private short _customer_acct_id;

		/// <summary>
		/// Initializes a new instance of the <see cref="DialPeerRow_Base"/> class.
		/// </summary>
		public DialPeerRow_Base()
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
		/// Gets or sets the <c>prefix_in</c> column value.
		/// </summary>
		/// <value>The <c>prefix_in</c> column value.</value>
		public string Prefix_in
		{
			get { return _prefix_in; }
			set { _prefix_in = value; }
		}

		/// <summary>
		/// Gets or sets the <c>customer_acct_id</c> column value.
		/// </summary>
		/// <value>The <c>customer_acct_id</c> column value.</value>
		public short Customer_acct_id
		{
			get { return _customer_acct_id; }
			set { _customer_acct_id = value; }
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
			dynStr.Append("  Prefix_in=");
			dynStr.Append(Prefix_in);
			dynStr.Append("  Customer_acct_id=");
			dynStr.Append(Customer_acct_id);
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

	} // End of DialPeerRow_Base class
} // End of namespace

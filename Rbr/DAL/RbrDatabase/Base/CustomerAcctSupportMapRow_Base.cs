// <fileinfo name="Base\CustomerAcctSupportMapRow_Base.cs">
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
	/// The base class for <see cref="CustomerAcctSupportMapRow"/> that 
	/// represents a record in the <c>CustomerAcctSupportMap</c> table.
	/// </summary>
	/// <remarks>
	/// Do not change this source code manually. Update the <see cref="CustomerAcctSupportMapRow"/>
	/// class if you need to add or change some functionality.
	/// </remarks>
	[Serializable]
	public abstract class CustomerAcctSupportMapRow_Base
	{
	#region Timok Custom

		//db field names
		public const string customer_acct_id_DbName = "customer_acct_id";
		public const string vendor_id_DbName = "vendor_id";

		//prop names
		public const string customer_acct_id_PropName = "Customer_acct_id";
		public const string vendor_id_PropName = "Vendor_id";

		//db field display names
		public const string customer_acct_id_DisplayName = "customer acct id";
		public const string vendor_id_DisplayName = "vendor id";
	#endregion Timok Custom


		private short _customer_acct_id;
		private int _vendor_id;

		/// <summary>
		/// Initializes a new instance of the <see cref="CustomerAcctSupportMapRow_Base"/> class.
		/// </summary>
		public CustomerAcctSupportMapRow_Base()
		{
			// EMPTY
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
		/// Gets or sets the <c>vendor_id</c> column value.
		/// </summary>
		/// <value>The <c>vendor_id</c> column value.</value>
		public int Vendor_id
		{
			get { return _vendor_id; }
			set { _vendor_id = value; }
		}

		/// <summary>
		/// Returns the string representation of this instance.
		/// </summary>
		/// <returns>The string representation of this instance.</returns>
		public override string ToString()
		{
			System.Text.StringBuilder dynStr = new System.Text.StringBuilder(GetType().Name);
			dynStr.Append(':');
			dynStr.Append("  Customer_acct_id=");
			dynStr.Append(Customer_acct_id);
			dynStr.Append("  Vendor_id=");
			dynStr.Append(Vendor_id);
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

	} // End of CustomerAcctSupportMapRow_Base class
} // End of namespace

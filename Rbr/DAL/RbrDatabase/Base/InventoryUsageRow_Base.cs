// <fileinfo name="Base\InventoryUsageRow_Base.cs">
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
	/// The base class for <see cref="InventoryUsageRow"/> that 
	/// represents a record in the <c>InventoryUsage</c> table.
	/// </summary>
	/// <remarks>
	/// Do not change this source code manually. Update the <see cref="InventoryUsageRow"/>
	/// class if you need to add or change some functionality.
	/// </remarks>
	[Serializable]
	public abstract class InventoryUsageRow_Base
	{
	#region Timok Custom

		//db field names
		public const string service_id_DbName = "service_id";
		public const string customer_acct_id_DbName = "customer_acct_id";
		public const string timestamp_DbName = "timestamp";
		public const string first_used_DbName = "first_used";
		public const string total_used_DbName = "total_used";
		public const string balance_depleted_DbName = "balance_depleted";
		public const string expired_DbName = "expired";

		//prop names
		public const string service_id_PropName = "Service_id";
		public const string customer_acct_id_PropName = "Customer_acct_id";
		public const string timestamp_PropName = "Timestamp";
		public const string first_used_PropName = "First_used";
		public const string total_used_PropName = "Total_used";
		public const string balance_depleted_PropName = "Balance_depleted";
		public const string expired_PropName = "Expired";

		//db field display names
		public const string service_id_DisplayName = "service id";
		public const string customer_acct_id_DisplayName = "customer acct id";
		public const string timestamp_DisplayName = "timestamp";
		public const string first_used_DisplayName = "first used";
		public const string total_used_DisplayName = "total used";
		public const string balance_depleted_DisplayName = "balance depleted";
		public const string expired_DisplayName = "expired";
	#endregion Timok Custom


		private short _service_id;
		private short _customer_acct_id;
		private System.DateTime _timestamp;
		private int _first_used;
		private int _total_used;
		private int _balance_depleted;
		private int _expired;

		/// <summary>
		/// Initializes a new instance of the <see cref="InventoryUsageRow_Base"/> class.
		/// </summary>
		public InventoryUsageRow_Base()
		{
			// EMPTY
		}

		/// <summary>
		/// Gets or sets the <c>service_id</c> column value.
		/// </summary>
		/// <value>The <c>service_id</c> column value.</value>
		public short Service_id
		{
			get { return _service_id; }
			set { _service_id = value; }
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
		/// Gets or sets the <c>timestamp</c> column value.
		/// </summary>
		/// <value>The <c>timestamp</c> column value.</value>
		public System.DateTime Timestamp
		{
			get { return _timestamp; }
			set { _timestamp = value; }
		}

		/// <summary>
		/// Gets or sets the <c>first_used</c> column value.
		/// </summary>
		/// <value>The <c>first_used</c> column value.</value>
		public int First_used
		{
			get { return _first_used; }
			set { _first_used = value; }
		}

		/// <summary>
		/// Gets or sets the <c>total_used</c> column value.
		/// </summary>
		/// <value>The <c>total_used</c> column value.</value>
		public int Total_used
		{
			get { return _total_used; }
			set { _total_used = value; }
		}

		/// <summary>
		/// Gets or sets the <c>balance_depleted</c> column value.
		/// </summary>
		/// <value>The <c>balance_depleted</c> column value.</value>
		public int Balance_depleted
		{
			get { return _balance_depleted; }
			set { _balance_depleted = value; }
		}

		/// <summary>
		/// Gets or sets the <c>expired</c> column value.
		/// </summary>
		/// <value>The <c>expired</c> column value.</value>
		public int Expired
		{
			get { return _expired; }
			set { _expired = value; }
		}

		/// <summary>
		/// Returns the string representation of this instance.
		/// </summary>
		/// <returns>The string representation of this instance.</returns>
		public override string ToString()
		{
			System.Text.StringBuilder dynStr = new System.Text.StringBuilder(GetType().Name);
			dynStr.Append(':');
			dynStr.Append("  Service_id=");
			dynStr.Append(Service_id);
			dynStr.Append("  Customer_acct_id=");
			dynStr.Append(Customer_acct_id);
			dynStr.Append("  Timestamp=");
			dynStr.Append(Timestamp);
			dynStr.Append("  First_used=");
			dynStr.Append(First_used);
			dynStr.Append("  Total_used=");
			dynStr.Append(Total_used);
			dynStr.Append("  Balance_depleted=");
			dynStr.Append(Balance_depleted);
			dynStr.Append("  Expired=");
			dynStr.Append(Expired);
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

	} // End of InventoryUsageRow_Base class
} // End of namespace

// <fileinfo name="Base\LoadBalancingMapRow_Base.cs">
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
	/// The base class for <see cref="LoadBalancingMapRow"/> that 
	/// represents a record in the <c>LoadBalancingMap</c> table.
	/// </summary>
	/// <remarks>
	/// Do not change this source code manually. Update the <see cref="LoadBalancingMapRow"/>
	/// class if you need to add or change some functionality.
	/// </remarks>
	[Serializable]
	public abstract class LoadBalancingMapRow_Base
	{
	#region Timok Custom

		//db field names
		public const string node_id_DbName = "node_id";
		public const string customer_acct_id_DbName = "customer_acct_id";
		public const string max_calls_DbName = "max_calls";
		public const string current_calls_DbName = "current_calls";

		//prop names
		public const string node_id_PropName = "Node_id";
		public const string customer_acct_id_PropName = "Customer_acct_id";
		public const string max_calls_PropName = "Max_calls";
		public const string current_calls_PropName = "Current_calls";

		//db field display names
		public const string node_id_DisplayName = "node id";
		public const string customer_acct_id_DisplayName = "customer acct id";
		public const string max_calls_DisplayName = "max calls";
		public const string current_calls_DisplayName = "current calls";
	#endregion Timok Custom


		private short _node_id;
		private short _customer_acct_id;
		private int _max_calls;
		private int _current_calls;

		/// <summary>
		/// Initializes a new instance of the <see cref="LoadBalancingMapRow_Base"/> class.
		/// </summary>
		public LoadBalancingMapRow_Base()
		{
			// EMPTY
		}

		/// <summary>
		/// Gets or sets the <c>node_id</c> column value.
		/// </summary>
		/// <value>The <c>node_id</c> column value.</value>
		public short Node_id
		{
			get { return _node_id; }
			set { _node_id = value; }
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
		/// Gets or sets the <c>max_calls</c> column value.
		/// </summary>
		/// <value>The <c>max_calls</c> column value.</value>
		public int Max_calls
		{
			get { return _max_calls; }
			set { _max_calls = value; }
		}

		/// <summary>
		/// Gets or sets the <c>current_calls</c> column value.
		/// </summary>
		/// <value>The <c>current_calls</c> column value.</value>
		public int Current_calls
		{
			get { return _current_calls; }
			set { _current_calls = value; }
		}

		/// <summary>
		/// Returns the string representation of this instance.
		/// </summary>
		/// <returns>The string representation of this instance.</returns>
		public override string ToString()
		{
			System.Text.StringBuilder dynStr = new System.Text.StringBuilder(GetType().Name);
			dynStr.Append(':');
			dynStr.Append("  Node_id=");
			dynStr.Append(Node_id);
			dynStr.Append("  Customer_acct_id=");
			dynStr.Append(Customer_acct_id);
			dynStr.Append("  Max_calls=");
			dynStr.Append(Max_calls);
			dynStr.Append("  Current_calls=");
			dynStr.Append(Current_calls);
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

	} // End of LoadBalancingMapRow_Base class
} // End of namespace

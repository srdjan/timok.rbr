// <fileinfo name="Base\BatchRow_Base.cs">
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
	/// The base class for <see cref="BatchRow"/> that 
	/// represents a record in the <c>Batch</c> table.
	/// </summary>
	/// <remarks>
	/// Do not change this source code manually. Update the <see cref="BatchRow"/>
	/// class if you need to add or change some functionality.
	/// </remarks>
	[Serializable]
	public abstract class BatchRow_Base
	{
	#region Timok Custom

		//db field names
		public const string batch_id_DbName = "batch_id";
		public const string status_DbName = "status";
		public const string first_serial_DbName = "first_serial";
		public const string last_serial_DbName = "last_serial";
		public const string request_id_DbName = "request_id";
		public const string box_id_DbName = "box_id";
		public const string customer_acct_id_DbName = "customer_acct_id";

		//prop names
		public const string batch_id_PropName = "Batch_id";
		public const string status_PropName = "Status";
		public const string first_serial_PropName = "First_serial";
		public const string last_serial_PropName = "Last_serial";
		public const string request_id_PropName = "Request_id";
		public const string box_id_PropName = "Box_id";
		public const string customer_acct_id_PropName = "Customer_acct_id";

		//db field display names
		public const string batch_id_DisplayName = "batch id";
		public const string status_DisplayName = "status";
		public const string first_serial_DisplayName = "first serial";
		public const string last_serial_DisplayName = "last serial";
		public const string request_id_DisplayName = "request id";
		public const string box_id_DisplayName = "box id";
		public const string customer_acct_id_DisplayName = "customer acct id";
	#endregion Timok Custom


		private int _batch_id;
		private byte _status;
		private long _first_serial;
		private long _last_serial;
		private int _request_id;
		private int _box_id;
		private bool _box_idNull = true;
		private short _customer_acct_id;
		private bool _customer_acct_idNull = true;

		/// <summary>
		/// Initializes a new instance of the <see cref="BatchRow_Base"/> class.
		/// </summary>
		public BatchRow_Base()
		{
			// EMPTY
		}

		/// <summary>
		/// Gets or sets the <c>batch_id</c> column value.
		/// </summary>
		/// <value>The <c>batch_id</c> column value.</value>
		public int Batch_id
		{
			get { return _batch_id; }
			set { _batch_id = value; }
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
		/// Gets or sets the <c>first_serial</c> column value.
		/// </summary>
		/// <value>The <c>first_serial</c> column value.</value>
		public long First_serial
		{
			get { return _first_serial; }
			set { _first_serial = value; }
		}

		/// <summary>
		/// Gets or sets the <c>last_serial</c> column value.
		/// </summary>
		/// <value>The <c>last_serial</c> column value.</value>
		public long Last_serial
		{
			get { return _last_serial; }
			set { _last_serial = value; }
		}

		/// <summary>
		/// Gets or sets the <c>request_id</c> column value.
		/// </summary>
		/// <value>The <c>request_id</c> column value.</value>
		public int Request_id
		{
			get { return _request_id; }
			set { _request_id = value; }
		}

		/// <summary>
		/// Gets or sets the <c>box_id</c> column value.
		/// This column is nullable.
		/// </summary>
		/// <value>The <c>box_id</c> column value.</value>
		public int Box_id
		{
			get
			{
				//if(IsBox_idNull)
					//throw new InvalidOperationException("Cannot get value because it is DBNull.");
				return _box_id;
			}
			set
			{
				_box_idNull = false;
				_box_id = value;
			}
		}

		/// <summary>
		/// Indicates whether the <see cref="Box_id"/>
		/// property value is null.
		/// </summary>
		/// <value>true if the property value is null, otherwise false.</value>
		public bool IsBox_idNull
		{
			get { return _box_idNull; }
			set { _box_idNull = value; }
		}

		/// <summary>
		/// Gets or sets the <c>customer_acct_id</c> column value.
		/// This column is nullable.
		/// </summary>
		/// <value>The <c>customer_acct_id</c> column value.</value>
		public short Customer_acct_id
		{
			get
			{
				//if(IsCustomer_acct_idNull)
					//throw new InvalidOperationException("Cannot get value because it is DBNull.");
				return _customer_acct_id;
			}
			set
			{
				_customer_acct_idNull = false;
				_customer_acct_id = value;
			}
		}

		/// <summary>
		/// Indicates whether the <see cref="Customer_acct_id"/>
		/// property value is null.
		/// </summary>
		/// <value>true if the property value is null, otherwise false.</value>
		public bool IsCustomer_acct_idNull
		{
			get { return _customer_acct_idNull; }
			set { _customer_acct_idNull = value; }
		}

		/// <summary>
		/// Returns the string representation of this instance.
		/// </summary>
		/// <returns>The string representation of this instance.</returns>
		public override string ToString()
		{
			System.Text.StringBuilder dynStr = new System.Text.StringBuilder(GetType().Name);
			dynStr.Append(':');
			dynStr.Append("  Batch_id=");
			dynStr.Append(Batch_id);
			dynStr.Append("  Status=");
			dynStr.Append(Status);
			dynStr.Append("  First_serial=");
			dynStr.Append(First_serial);
			dynStr.Append("  Last_serial=");
			dynStr.Append(Last_serial);
			dynStr.Append("  Request_id=");
			dynStr.Append(Request_id);
			dynStr.Append("  Box_id=");
			dynStr.Append(IsBox_idNull ? (object)"<NULL>" : Box_id);
			dynStr.Append("  Customer_acct_id=");
			dynStr.Append(IsCustomer_acct_idNull ? (object)"<NULL>" : Customer_acct_id);
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

	} // End of BatchRow_Base class
} // End of namespace

// <fileinfo name="Base\CarrierAcctRow_Base.cs">
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
	/// The base class for <see cref="CarrierAcctRow"/> that 
	/// represents a record in the <c>CarrierAcct</c> table.
	/// </summary>
	/// <remarks>
	/// Do not change this source code manually. Update the <see cref="CarrierAcctRow"/>
	/// class if you need to add or change some functionality.
	/// </remarks>
	[Serializable]
	public abstract class CarrierAcctRow_Base
	{
	#region Timok Custom

		//db field names
		public const string carrier_acct_id_DbName = "carrier_acct_id";
		public const string name_DbName = "name";
		public const string status_DbName = "status";
		public const string rating_type_DbName = "rating_type";
		public const string prefix_out_DbName = "prefix_out";
		public const string strip_1plus_DbName = "strip_1plus";
		public const string intl_dial_code_DbName = "intl_dial_code";
		public const string partner_id_DbName = "partner_id";
		public const string calling_plan_id_DbName = "calling_plan_id";
		public const string max_call_length_DbName = "max_call_length";

		//prop names
		public const string carrier_acct_id_PropName = "Carrier_acct_id";
		public const string name_PropName = "Name";
		public const string status_PropName = "Status";
		public const string rating_type_PropName = "Rating_type";
		public const string prefix_out_PropName = "Prefix_out";
		public const string strip_1plus_PropName = "Strip_1plus";
		public const string intl_dial_code_PropName = "Intl_dial_code";
		public const string partner_id_PropName = "Partner_id";
		public const string calling_plan_id_PropName = "Calling_plan_id";
		public const string max_call_length_PropName = "Max_call_length";

		//db field display names
		public const string carrier_acct_id_DisplayName = "carrier acct id";
		public const string name_DisplayName = "name";
		public const string status_DisplayName = "status";
		public const string rating_type_DisplayName = "rating type";
		public const string prefix_out_DisplayName = "prefix out";
		public const string strip_1plus_DisplayName = "strip 1plus";
		public const string intl_dial_code_DisplayName = "intl dial code";
		public const string partner_id_DisplayName = "partner id";
		public const string calling_plan_id_DisplayName = "calling plan id";
		public const string max_call_length_DisplayName = "max call length";
	#endregion Timok Custom


		private short _carrier_acct_id;
		private string _name;
		private byte _status;
		private byte _rating_type;
		private string _prefix_out;
		private short _strip_1plus;
		private string _intl_dial_code;
		private int _partner_id;
		private int _calling_plan_id;
		private short _max_call_length;

		/// <summary>
		/// Initializes a new instance of the <see cref="CarrierAcctRow_Base"/> class.
		/// </summary>
		public CarrierAcctRow_Base()
		{
			// EMPTY
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
		/// Gets or sets the <c>name</c> column value.
		/// </summary>
		/// <value>The <c>name</c> column value.</value>
		public string Name
		{
			get { return _name; }
			set { _name = value; }
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
		/// Gets or sets the <c>rating_type</c> column value.
		/// </summary>
		/// <value>The <c>rating_type</c> column value.</value>
		public byte Rating_type
		{
			get { return _rating_type; }
			set { _rating_type = value; }
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
		/// Gets or sets the <c>strip_1plus</c> column value.
		/// </summary>
		/// <value>The <c>strip_1plus</c> column value.</value>
		public short Strip_1plus
		{
			get { return _strip_1plus; }
			set { _strip_1plus = value; }
		}

		/// <summary>
		/// Gets or sets the <c>intl_dial_code</c> column value.
		/// </summary>
		/// <value>The <c>intl_dial_code</c> column value.</value>
		public string Intl_dial_code
		{
			get { return _intl_dial_code; }
			set { _intl_dial_code = value; }
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
		/// Gets or sets the <c>calling_plan_id</c> column value.
		/// </summary>
		/// <value>The <c>calling_plan_id</c> column value.</value>
		public int Calling_plan_id
		{
			get { return _calling_plan_id; }
			set { _calling_plan_id = value; }
		}

		/// <summary>
		/// Gets or sets the <c>max_call_length</c> column value.
		/// </summary>
		/// <value>The <c>max_call_length</c> column value.</value>
		public short Max_call_length
		{
			get { return _max_call_length; }
			set { _max_call_length = value; }
		}

		/// <summary>
		/// Returns the string representation of this instance.
		/// </summary>
		/// <returns>The string representation of this instance.</returns>
		public override string ToString()
		{
			System.Text.StringBuilder dynStr = new System.Text.StringBuilder(GetType().Name);
			dynStr.Append(':');
			dynStr.Append("  Carrier_acct_id=");
			dynStr.Append(Carrier_acct_id);
			dynStr.Append("  Name=");
			dynStr.Append(Name);
			dynStr.Append("  Status=");
			dynStr.Append(Status);
			dynStr.Append("  Rating_type=");
			dynStr.Append(Rating_type);
			dynStr.Append("  Prefix_out=");
			dynStr.Append(Prefix_out);
			dynStr.Append("  Strip_1plus=");
			dynStr.Append(Strip_1plus);
			dynStr.Append("  Intl_dial_code=");
			dynStr.Append(Intl_dial_code);
			dynStr.Append("  Partner_id=");
			dynStr.Append(Partner_id);
			dynStr.Append("  Calling_plan_id=");
			dynStr.Append(Calling_plan_id);
			dynStr.Append("  Max_call_length=");
			dynStr.Append(Max_call_length);
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

	} // End of CarrierAcctRow_Base class
} // End of namespace

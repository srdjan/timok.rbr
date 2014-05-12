// <fileinfo name="Base\AccessNumberListRow_Base.cs">
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
	/// The base class for <see cref="AccessNumberListRow"/> that 
	/// represents a record in the <c>AccessNumberList</c> table.
	/// </summary>
	/// <remarks>
	/// Do not change this source code manually. Update the <see cref="AccessNumberListRow"/>
	/// class if you need to add or change some functionality.
	/// </remarks>
	[Serializable]
	public abstract class AccessNumberListRow_Base
	{
	#region Timok Custom

		//db field names
		public const string access_number_DbName = "access_number";
		public const string service_id_DbName = "service_id";
		public const string language_DbName = "language";
		public const string surcharge_DbName = "surcharge";
		public const string surcharge_type_DbName = "surcharge_type";
		public const string customer_acct_id_DbName = "customer_acct_id";
		public const string script_type_DbName = "script_type";

		//prop names
		public const string access_number_PropName = "Access_number";
		public const string service_id_PropName = "Service_id";
		public const string language_PropName = "Language";
		public const string surcharge_PropName = "Surcharge";
		public const string surcharge_type_PropName = "Surcharge_type";
		public const string customer_acct_id_PropName = "Customer_acct_id";
		public const string script_type_PropName = "Script_type";

		//db field display names
		public const string access_number_DisplayName = "access number";
		public const string service_id_DisplayName = "service id";
		public const string language_DisplayName = "language";
		public const string surcharge_DisplayName = "surcharge";
		public const string surcharge_type_DisplayName = "surcharge type";
		public const string customer_acct_id_DisplayName = "customer acct id";
		public const string script_type_DisplayName = "script type";
	#endregion Timok Custom


		private long _access_number;
		private short _service_id;
		private bool _service_idNull = true;
		private byte _language;
		private decimal _surcharge;
		private byte _surcharge_type;
		private short _customer_acct_id;
		private bool _customer_acct_idNull = true;
		private int _script_type;

		/// <summary>
		/// Initializes a new instance of the <see cref="AccessNumberListRow_Base"/> class.
		/// </summary>
		public AccessNumberListRow_Base()
		{
			// EMPTY
		}

		/// <summary>
		/// Gets or sets the <c>access_number</c> column value.
		/// </summary>
		/// <value>The <c>access_number</c> column value.</value>
		public long Access_number
		{
			get { return _access_number; }
			set { _access_number = value; }
		}

		/// <summary>
		/// Gets or sets the <c>service_id</c> column value.
		/// This column is nullable.
		/// </summary>
		/// <value>The <c>service_id</c> column value.</value>
		public short Service_id
		{
			get
			{
				//if(IsService_idNull)
					//throw new InvalidOperationException("Cannot get value because it is DBNull.");
				return _service_id;
			}
			set
			{
				_service_idNull = false;
				_service_id = value;
			}
		}

		/// <summary>
		/// Indicates whether the <see cref="Service_id"/>
		/// property value is null.
		/// </summary>
		/// <value>true if the property value is null, otherwise false.</value>
		public bool IsService_idNull
		{
			get { return _service_idNull; }
			set { _service_idNull = value; }
		}

		/// <summary>
		/// Gets or sets the <c>language</c> column value.
		/// </summary>
		/// <value>The <c>language</c> column value.</value>
		public byte Language
		{
			get { return _language; }
			set { _language = value; }
		}

		/// <summary>
		/// Gets or sets the <c>surcharge</c> column value.
		/// </summary>
		/// <value>The <c>surcharge</c> column value.</value>
		public decimal Surcharge
		{
			get { return _surcharge; }
			set { _surcharge = value; }
		}

		/// <summary>
		/// Gets or sets the <c>surcharge_type</c> column value.
		/// </summary>
		/// <value>The <c>surcharge_type</c> column value.</value>
		public byte Surcharge_type
		{
			get { return _surcharge_type; }
			set { _surcharge_type = value; }
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
		/// Gets or sets the <c>script_type</c> column value.
		/// </summary>
		/// <value>The <c>script_type</c> column value.</value>
		public int Script_type
		{
			get { return _script_type; }
			set { _script_type = value; }
		}

		/// <summary>
		/// Returns the string representation of this instance.
		/// </summary>
		/// <returns>The string representation of this instance.</returns>
		public override string ToString()
		{
			System.Text.StringBuilder dynStr = new System.Text.StringBuilder(GetType().Name);
			dynStr.Append(':');
			dynStr.Append("  Access_number=");
			dynStr.Append(Access_number);
			dynStr.Append("  Service_id=");
			dynStr.Append(IsService_idNull ? (object)"<NULL>" : Service_id);
			dynStr.Append("  Language=");
			dynStr.Append(Language);
			dynStr.Append("  Surcharge=");
			dynStr.Append(Surcharge);
			dynStr.Append("  Surcharge_type=");
			dynStr.Append(Surcharge_type);
			dynStr.Append("  Customer_acct_id=");
			dynStr.Append(IsCustomer_acct_idNull ? (object)"<NULL>" : Customer_acct_id);
			dynStr.Append("  Script_type=");
			dynStr.Append(Script_type);
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

	} // End of AccessNumberListRow_Base class
} // End of namespace

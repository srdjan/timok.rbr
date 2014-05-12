// <fileinfo name="Base\ContactInfoRow_Base.cs">
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
	/// The base class for <see cref="ContactInfoRow"/> that 
	/// represents a record in the <c>ContactInfo</c> table.
	/// </summary>
	/// <remarks>
	/// Do not change this source code manually. Update the <see cref="ContactInfoRow"/>
	/// class if you need to add or change some functionality.
	/// </remarks>
	[Serializable]
	public abstract class ContactInfoRow_Base
	{
	#region Timok Custom

		//db field names
		public const string contact_info_id_DbName = "contact_info_id";
		public const string address1_DbName = "address1";
		public const string address2_DbName = "address2";
		public const string city_DbName = "city";
		public const string state_DbName = "state";
		public const string zip_code_DbName = "zip_code";
		public const string email_DbName = "email";
		public const string home_phone_number_DbName = "home_phone_number";
		public const string cell_phone_number_DbName = "cell_phone_number";
		public const string work_phone_number_DbName = "work_phone_number";

		//prop names
		public const string contact_info_id_PropName = "Contact_info_id";
		public const string address1_PropName = "Address1";
		public const string address2_PropName = "Address2";
		public const string city_PropName = "City";
		public const string state_PropName = "State";
		public const string zip_code_PropName = "Zip_code";
		public const string email_PropName = "Email";
		public const string home_phone_number_PropName = "Home_phone_number";
		public const string cell_phone_number_PropName = "Cell_phone_number";
		public const string work_phone_number_PropName = "Work_phone_number";

		//db field display names
		public const string contact_info_id_DisplayName = "contact info id";
		public const string address1_DisplayName = "address1";
		public const string address2_DisplayName = "address2";
		public const string city_DisplayName = "city";
		public const string state_DisplayName = "state";
		public const string zip_code_DisplayName = "zip code";
		public const string email_DisplayName = "email";
		public const string home_phone_number_DisplayName = "home phone number";
		public const string cell_phone_number_DisplayName = "cell phone number";
		public const string work_phone_number_DisplayName = "work phone number";
	#endregion Timok Custom


		private int _contact_info_id;
		private string _address1;
		private string _address2;
		private string _city;
		private string _state;
		private string _zip_code;
		private string _email;
		private long _home_phone_number;
		private long _cell_phone_number;
		private long _work_phone_number;

		/// <summary>
		/// Initializes a new instance of the <see cref="ContactInfoRow_Base"/> class.
		/// </summary>
		public ContactInfoRow_Base()
		{
			// EMPTY
		}

		/// <summary>
		/// Gets or sets the <c>contact_info_id</c> column value.
		/// </summary>
		/// <value>The <c>contact_info_id</c> column value.</value>
		public int Contact_info_id
		{
			get { return _contact_info_id; }
			set { _contact_info_id = value; }
		}

		/// <summary>
		/// Gets or sets the <c>address1</c> column value.
		/// </summary>
		/// <value>The <c>address1</c> column value.</value>
		public string Address1
		{
			get { return _address1; }
			set { _address1 = value; }
		}

		/// <summary>
		/// Gets or sets the <c>address2</c> column value.
		/// </summary>
		/// <value>The <c>address2</c> column value.</value>
		public string Address2
		{
			get { return _address2; }
			set { _address2 = value; }
		}

		/// <summary>
		/// Gets or sets the <c>city</c> column value.
		/// </summary>
		/// <value>The <c>city</c> column value.</value>
		public string City
		{
			get { return _city; }
			set { _city = value; }
		}

		/// <summary>
		/// Gets or sets the <c>state</c> column value.
		/// </summary>
		/// <value>The <c>state</c> column value.</value>
		public string State
		{
			get { return _state; }
			set { _state = value; }
		}

		/// <summary>
		/// Gets or sets the <c>zip_code</c> column value.
		/// </summary>
		/// <value>The <c>zip_code</c> column value.</value>
		public string Zip_code
		{
			get { return _zip_code; }
			set { _zip_code = value; }
		}

		/// <summary>
		/// Gets or sets the <c>email</c> column value.
		/// </summary>
		/// <value>The <c>email</c> column value.</value>
		public string Email
		{
			get { return _email; }
			set { _email = value; }
		}

		/// <summary>
		/// Gets or sets the <c>home_phone_number</c> column value.
		/// </summary>
		/// <value>The <c>home_phone_number</c> column value.</value>
		public long Home_phone_number
		{
			get { return _home_phone_number; }
			set { _home_phone_number = value; }
		}

		/// <summary>
		/// Gets or sets the <c>cell_phone_number</c> column value.
		/// </summary>
		/// <value>The <c>cell_phone_number</c> column value.</value>
		public long Cell_phone_number
		{
			get { return _cell_phone_number; }
			set { _cell_phone_number = value; }
		}

		/// <summary>
		/// Gets or sets the <c>work_phone_number</c> column value.
		/// </summary>
		/// <value>The <c>work_phone_number</c> column value.</value>
		public long Work_phone_number
		{
			get { return _work_phone_number; }
			set { _work_phone_number = value; }
		}

		/// <summary>
		/// Returns the string representation of this instance.
		/// </summary>
		/// <returns>The string representation of this instance.</returns>
		public override string ToString()
		{
			System.Text.StringBuilder dynStr = new System.Text.StringBuilder(GetType().Name);
			dynStr.Append(':');
			dynStr.Append("  Contact_info_id=");
			dynStr.Append(Contact_info_id);
			dynStr.Append("  Address1=");
			dynStr.Append(Address1);
			dynStr.Append("  Address2=");
			dynStr.Append(Address2);
			dynStr.Append("  City=");
			dynStr.Append(City);
			dynStr.Append("  State=");
			dynStr.Append(State);
			dynStr.Append("  Zip_code=");
			dynStr.Append(Zip_code);
			dynStr.Append("  Email=");
			dynStr.Append(Email);
			dynStr.Append("  Home_phone_number=");
			dynStr.Append(Home_phone_number);
			dynStr.Append("  Cell_phone_number=");
			dynStr.Append(Cell_phone_number);
			dynStr.Append("  Work_phone_number=");
			dynStr.Append(Work_phone_number);
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

	} // End of ContactInfoRow_Base class
} // End of namespace

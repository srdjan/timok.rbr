// <fileinfo name="Base\PersonRow_Base.cs">
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
	/// The base class for <see cref="PersonRow"/> that 
	/// represents a record in the <c>Person</c> table.
	/// </summary>
	/// <remarks>
	/// Do not change this source code manually. Update the <see cref="PersonRow"/>
	/// class if you need to add or change some functionality.
	/// </remarks>
	[Serializable]
	public abstract class PersonRow_Base
	{
	#region Timok Custom

		//db field names
		public const string person_id_DbName = "person_id";
		public const string name_DbName = "name";
		public const string login_DbName = "login";
		public const string password_DbName = "password";
		public const string permission_DbName = "permission";
		public const string is_reseller_DbName = "is_reseller";
		public const string status_DbName = "status";
		public const string registration_status_DbName = "registration_status";
		public const string salt_DbName = "salt";
		public const string partner_id_DbName = "partner_id";
		public const string retail_acct_id_DbName = "retail_acct_id";
		public const string group_id_DbName = "group_id";
		public const string virtual_switch_id_DbName = "virtual_switch_id";
		public const string contact_info_id_DbName = "contact_info_id";

		//prop names
		public const string person_id_PropName = "Person_id";
		public const string name_PropName = "Name";
		public const string login_PropName = "Login";
		public const string password_PropName = "Password";
		public const string permission_PropName = "Permission";
		public const string is_reseller_PropName = "Is_reseller";
		public const string status_PropName = "Status";
		public const string registration_status_PropName = "Registration_status";
		public const string salt_PropName = "Salt";
		public const string partner_id_PropName = "Partner_id";
		public const string retail_acct_id_PropName = "Retail_acct_id";
		public const string group_id_PropName = "Group_id";
		public const string virtual_switch_id_PropName = "Virtual_switch_id";
		public const string contact_info_id_PropName = "Contact_info_id";

		//db field display names
		public const string person_id_DisplayName = "person id";
		public const string name_DisplayName = "name";
		public const string login_DisplayName = "login";
		public const string password_DisplayName = "password";
		public const string permission_DisplayName = "permission";
		public const string is_reseller_DisplayName = "is reseller";
		public const string status_DisplayName = "status";
		public const string registration_status_DisplayName = "registration status";
		public const string salt_DisplayName = "salt";
		public const string partner_id_DisplayName = "partner id";
		public const string retail_acct_id_DisplayName = "retail acct id";
		public const string group_id_DisplayName = "group id";
		public const string virtual_switch_id_DisplayName = "virtual switch id";
		public const string contact_info_id_DisplayName = "contact info id";
	#endregion Timok Custom


		private int _person_id;
		private string _name;
		private string _login;
		private string _password;
		private byte _permission;
		private byte _is_reseller;
		private byte _status;
		private byte _registration_status;
		private string _salt;
		private int _partner_id;
		private bool _partner_idNull = true;
		private int _retail_acct_id;
		private bool _retail_acct_idNull = true;
		private int _group_id;
		private bool _group_idNull = true;
		private int _virtual_switch_id;
		private bool _virtual_switch_idNull = true;
		private int _contact_info_id;
		private bool _contact_info_idNull = true;

		/// <summary>
		/// Initializes a new instance of the <see cref="PersonRow_Base"/> class.
		/// </summary>
		public PersonRow_Base()
		{
			// EMPTY
		}

		/// <summary>
		/// Gets or sets the <c>person_id</c> column value.
		/// </summary>
		/// <value>The <c>person_id</c> column value.</value>
		public int Person_id
		{
			get { return _person_id; }
			set { _person_id = value; }
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
		/// Gets or sets the <c>login</c> column value.
		/// </summary>
		/// <value>The <c>login</c> column value.</value>
		public string Login
		{
			get { return _login; }
			set { _login = value; }
		}

		/// <summary>
		/// Gets or sets the <c>password</c> column value.
		/// </summary>
		/// <value>The <c>password</c> column value.</value>
		public string Password
		{
			get { return _password; }
			set { _password = value; }
		}

		/// <summary>
		/// Gets or sets the <c>permission</c> column value.
		/// </summary>
		/// <value>The <c>permission</c> column value.</value>
		public byte Permission
		{
			get { return _permission; }
			set { _permission = value; }
		}

		/// <summary>
		/// Gets or sets the <c>is_reseller</c> column value.
		/// </summary>
		/// <value>The <c>is_reseller</c> column value.</value>
		public byte Is_reseller
		{
			get { return _is_reseller; }
			set { _is_reseller = value; }
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
		/// Gets or sets the <c>registration_status</c> column value.
		/// </summary>
		/// <value>The <c>registration_status</c> column value.</value>
		public byte Registration_status
		{
			get { return _registration_status; }
			set { _registration_status = value; }
		}

		/// <summary>
		/// Gets or sets the <c>salt</c> column value.
		/// </summary>
		/// <value>The <c>salt</c> column value.</value>
		public string Salt
		{
			get { return _salt; }
			set { _salt = value; }
		}

		/// <summary>
		/// Gets or sets the <c>partner_id</c> column value.
		/// This column is nullable.
		/// </summary>
		/// <value>The <c>partner_id</c> column value.</value>
		public int Partner_id
		{
			get
			{
				//if(IsPartner_idNull)
					//throw new InvalidOperationException("Cannot get value because it is DBNull.");
				return _partner_id;
			}
			set
			{
				_partner_idNull = false;
				_partner_id = value;
			}
		}

		/// <summary>
		/// Indicates whether the <see cref="Partner_id"/>
		/// property value is null.
		/// </summary>
		/// <value>true if the property value is null, otherwise false.</value>
		public bool IsPartner_idNull
		{
			get { return _partner_idNull; }
			set { _partner_idNull = value; }
		}

		/// <summary>
		/// Gets or sets the <c>retail_acct_id</c> column value.
		/// This column is nullable.
		/// </summary>
		/// <value>The <c>retail_acct_id</c> column value.</value>
		public int Retail_acct_id
		{
			get
			{
				//if(IsRetail_acct_idNull)
					//throw new InvalidOperationException("Cannot get value because it is DBNull.");
				return _retail_acct_id;
			}
			set
			{
				_retail_acct_idNull = false;
				_retail_acct_id = value;
			}
		}

		/// <summary>
		/// Indicates whether the <see cref="Retail_acct_id"/>
		/// property value is null.
		/// </summary>
		/// <value>true if the property value is null, otherwise false.</value>
		public bool IsRetail_acct_idNull
		{
			get { return _retail_acct_idNull; }
			set { _retail_acct_idNull = value; }
		}

		/// <summary>
		/// Gets or sets the <c>group_id</c> column value.
		/// This column is nullable.
		/// </summary>
		/// <value>The <c>group_id</c> column value.</value>
		public int Group_id
		{
			get
			{
				//if(IsGroup_idNull)
					//throw new InvalidOperationException("Cannot get value because it is DBNull.");
				return _group_id;
			}
			set
			{
				_group_idNull = false;
				_group_id = value;
			}
		}

		/// <summary>
		/// Indicates whether the <see cref="Group_id"/>
		/// property value is null.
		/// </summary>
		/// <value>true if the property value is null, otherwise false.</value>
		public bool IsGroup_idNull
		{
			get { return _group_idNull; }
			set { _group_idNull = value; }
		}

		/// <summary>
		/// Gets or sets the <c>virtual_switch_id</c> column value.
		/// This column is nullable.
		/// </summary>
		/// <value>The <c>virtual_switch_id</c> column value.</value>
		public int Virtual_switch_id
		{
			get
			{
				//if(IsVirtual_switch_idNull)
					//throw new InvalidOperationException("Cannot get value because it is DBNull.");
				return _virtual_switch_id;
			}
			set
			{
				_virtual_switch_idNull = false;
				_virtual_switch_id = value;
			}
		}

		/// <summary>
		/// Indicates whether the <see cref="Virtual_switch_id"/>
		/// property value is null.
		/// </summary>
		/// <value>true if the property value is null, otherwise false.</value>
		public bool IsVirtual_switch_idNull
		{
			get { return _virtual_switch_idNull; }
			set { _virtual_switch_idNull = value; }
		}

		/// <summary>
		/// Gets or sets the <c>contact_info_id</c> column value.
		/// This column is nullable.
		/// </summary>
		/// <value>The <c>contact_info_id</c> column value.</value>
		public int Contact_info_id
		{
			get
			{
				//if(IsContact_info_idNull)
					//throw new InvalidOperationException("Cannot get value because it is DBNull.");
				return _contact_info_id;
			}
			set
			{
				_contact_info_idNull = false;
				_contact_info_id = value;
			}
		}

		/// <summary>
		/// Indicates whether the <see cref="Contact_info_id"/>
		/// property value is null.
		/// </summary>
		/// <value>true if the property value is null, otherwise false.</value>
		public bool IsContact_info_idNull
		{
			get { return _contact_info_idNull; }
			set { _contact_info_idNull = value; }
		}

		/// <summary>
		/// Returns the string representation of this instance.
		/// </summary>
		/// <returns>The string representation of this instance.</returns>
		public override string ToString()
		{
			System.Text.StringBuilder dynStr = new System.Text.StringBuilder(GetType().Name);
			dynStr.Append(':');
			dynStr.Append("  Person_id=");
			dynStr.Append(Person_id);
			dynStr.Append("  Name=");
			dynStr.Append(Name);
			dynStr.Append("  Login=");
			dynStr.Append(Login);
			dynStr.Append("  Password=");
			dynStr.Append(Password);
			dynStr.Append("  Permission=");
			dynStr.Append(Permission);
			dynStr.Append("  Is_reseller=");
			dynStr.Append(Is_reseller);
			dynStr.Append("  Status=");
			dynStr.Append(Status);
			dynStr.Append("  Registration_status=");
			dynStr.Append(Registration_status);
			dynStr.Append("  Salt=");
			dynStr.Append(Salt);
			dynStr.Append("  Partner_id=");
			dynStr.Append(IsPartner_idNull ? (object)"<NULL>" : Partner_id);
			dynStr.Append("  Retail_acct_id=");
			dynStr.Append(IsRetail_acct_idNull ? (object)"<NULL>" : Retail_acct_id);
			dynStr.Append("  Group_id=");
			dynStr.Append(IsGroup_idNull ? (object)"<NULL>" : Group_id);
			dynStr.Append("  Virtual_switch_id=");
			dynStr.Append(IsVirtual_switch_idNull ? (object)"<NULL>" : Virtual_switch_id);
			dynStr.Append("  Contact_info_id=");
			dynStr.Append(IsContact_info_idNull ? (object)"<NULL>" : Contact_info_id);
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

	} // End of PersonRow_Base class
} // End of namespace

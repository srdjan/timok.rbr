// <fileinfo name="Base\AccessListViewRow_Base.cs">
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
	/// The base class for <see cref="AccessListViewRow"/> that 
	/// represents a record in the <c>AccessListView</c> view.
	/// </summary>
	/// <remarks>
	/// Do not change this source code manually. Update the <see cref="AccessListViewRow"/>
	/// class if you need to add or change some functionality.
	/// </remarks>
	[Serializable]
	public abstract class AccessListViewRow_Base
	{
	#region Timok Custom

		//db field names
		public const string end_point_id_DbName = "end_point_id";
		public const string prefix_in_DbName = "prefix_in";
		public const string customer_acct_id_DbName = "customer_acct_id";
		public const string alias_DbName = "alias";
		public const string with_alias_authentication_DbName = "with_alias_authentication";
		public const string status_DbName = "status";
		public const string type_DbName = "type";
		public const string protocol_DbName = "protocol";
		public const string port_DbName = "port";
		public const string registration_DbName = "registration";
		public const string is_registered_DbName = "is_registered";
		public const string ip_address_range_DbName = "ip_address_range";
		public const string max_calls_DbName = "max_calls";
		public const string password_DbName = "password";
		public const string prefix_in_type_id_DbName = "prefix_in_type_id";
		public const string prefix_type_descr_DbName = "prefix_type_descr";
		public const string prefix_type_length_DbName = "prefix_type_length";
		public const string prefix_type_delimiter_DbName = "prefix_type_delimiter";

		//prop names
		public const string end_point_id_PropName = "End_point_id";
		public const string prefix_in_PropName = "Prefix_in";
		public const string customer_acct_id_PropName = "Customer_acct_id";
		public const string alias_PropName = "Alias";
		public const string with_alias_authentication_PropName = "With_alias_authentication";
		public const string status_PropName = "Status";
		public const string type_PropName = "Type";
		public const string protocol_PropName = "Protocol";
		public const string port_PropName = "Port";
		public const string registration_PropName = "Registration";
		public const string is_registered_PropName = "Is_registered";
		public const string ip_address_range_PropName = "Ip_address_range";
		public const string max_calls_PropName = "Max_calls";
		public const string password_PropName = "Password";
		public const string prefix_in_type_id_PropName = "Prefix_in_type_id";
		public const string prefix_type_descr_PropName = "Prefix_type_descr";
		public const string prefix_type_length_PropName = "Prefix_type_length";
		public const string prefix_type_delimiter_PropName = "Prefix_type_delimiter";

		//db field display names
		public const string end_point_id_DisplayName = "end point id";
		public const string prefix_in_DisplayName = "prefix in";
		public const string customer_acct_id_DisplayName = "customer acct id";
		public const string alias_DisplayName = "alias";
		public const string with_alias_authentication_DisplayName = "with alias authentication";
		public const string status_DisplayName = "status";
		public const string type_DisplayName = "type";
		public const string protocol_DisplayName = "protocol";
		public const string port_DisplayName = "port";
		public const string registration_DisplayName = "registration";
		public const string is_registered_DisplayName = "is registered";
		public const string ip_address_range_DisplayName = "ip address range";
		public const string max_calls_DisplayName = "max calls";
		public const string password_DisplayName = "password";
		public const string prefix_in_type_id_DisplayName = "prefix in type id";
		public const string prefix_type_descr_DisplayName = "prefix type descr";
		public const string prefix_type_length_DisplayName = "prefix type length";
		public const string prefix_type_delimiter_DisplayName = "prefix type delimiter";
	#endregion Timok Custom


		private short _end_point_id;
		private string _prefix_in;
		private short _customer_acct_id;
		private string _alias;
		private byte _with_alias_authentication;
		private byte _status;
		private byte _type;
		private byte _protocol;
		private int _port;
		private byte _registration;
		private byte _is_registered;
		private string _ip_address_range;
		private int _max_calls;
		private string _password;
		private short _prefix_in_type_id;
		private string _prefix_type_descr;
		private byte _prefix_type_length;
		private byte _prefix_type_delimiter;

		/// <summary>
		/// Initializes a new instance of the <see cref="AccessListViewRow_Base"/> class.
		/// </summary>
		public AccessListViewRow_Base()
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
		/// Gets or sets the <c>alias</c> column value.
		/// </summary>
		/// <value>The <c>alias</c> column value.</value>
		public string Alias
		{
			get { return _alias; }
			set { _alias = value; }
		}

		/// <summary>
		/// Gets or sets the <c>with_alias_authentication</c> column value.
		/// </summary>
		/// <value>The <c>with_alias_authentication</c> column value.</value>
		public byte With_alias_authentication
		{
			get { return _with_alias_authentication; }
			set { _with_alias_authentication = value; }
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
		/// Gets or sets the <c>type</c> column value.
		/// </summary>
		/// <value>The <c>type</c> column value.</value>
		public byte Type
		{
			get { return _type; }
			set { _type = value; }
		}

		/// <summary>
		/// Gets or sets the <c>protocol</c> column value.
		/// </summary>
		/// <value>The <c>protocol</c> column value.</value>
		public byte Protocol
		{
			get { return _protocol; }
			set { _protocol = value; }
		}

		/// <summary>
		/// Gets or sets the <c>port</c> column value.
		/// </summary>
		/// <value>The <c>port</c> column value.</value>
		public int Port
		{
			get { return _port; }
			set { _port = value; }
		}

		/// <summary>
		/// Gets or sets the <c>registration</c> column value.
		/// </summary>
		/// <value>The <c>registration</c> column value.</value>
		public byte Registration
		{
			get { return _registration; }
			set { _registration = value; }
		}

		/// <summary>
		/// Gets or sets the <c>is_registered</c> column value.
		/// </summary>
		/// <value>The <c>is_registered</c> column value.</value>
		public byte Is_registered
		{
			get { return _is_registered; }
			set { _is_registered = value; }
		}

		/// <summary>
		/// Gets or sets the <c>ip_address_range</c> column value.
		/// </summary>
		/// <value>The <c>ip_address_range</c> column value.</value>
		public string Ip_address_range
		{
			get { return _ip_address_range; }
			set { _ip_address_range = value; }
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
		/// Gets or sets the <c>password</c> column value.
		/// </summary>
		/// <value>The <c>password</c> column value.</value>
		public string Password
		{
			get { return _password; }
			set { _password = value; }
		}

		/// <summary>
		/// Gets or sets the <c>prefix_in_type_id</c> column value.
		/// </summary>
		/// <value>The <c>prefix_in_type_id</c> column value.</value>
		public short Prefix_in_type_id
		{
			get { return _prefix_in_type_id; }
			set { _prefix_in_type_id = value; }
		}

		/// <summary>
		/// Gets or sets the <c>prefix_type_descr</c> column value.
		/// </summary>
		/// <value>The <c>prefix_type_descr</c> column value.</value>
		public string Prefix_type_descr
		{
			get { return _prefix_type_descr; }
			set { _prefix_type_descr = value; }
		}

		/// <summary>
		/// Gets or sets the <c>prefix_type_length</c> column value.
		/// </summary>
		/// <value>The <c>prefix_type_length</c> column value.</value>
		public byte Prefix_type_length
		{
			get { return _prefix_type_length; }
			set { _prefix_type_length = value; }
		}

		/// <summary>
		/// Gets or sets the <c>prefix_type_delimiter</c> column value.
		/// </summary>
		/// <value>The <c>prefix_type_delimiter</c> column value.</value>
		public byte Prefix_type_delimiter
		{
			get { return _prefix_type_delimiter; }
			set { _prefix_type_delimiter = value; }
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
			dynStr.Append("  Alias=");
			dynStr.Append(Alias);
			dynStr.Append("  With_alias_authentication=");
			dynStr.Append(With_alias_authentication);
			dynStr.Append("  Status=");
			dynStr.Append(Status);
			dynStr.Append("  Type=");
			dynStr.Append(Type);
			dynStr.Append("  Protocol=");
			dynStr.Append(Protocol);
			dynStr.Append("  Port=");
			dynStr.Append(Port);
			dynStr.Append("  Registration=");
			dynStr.Append(Registration);
			dynStr.Append("  Is_registered=");
			dynStr.Append(Is_registered);
			dynStr.Append("  Ip_address_range=");
			dynStr.Append(Ip_address_range);
			dynStr.Append("  Max_calls=");
			dynStr.Append(Max_calls);
			dynStr.Append("  Password=");
			dynStr.Append(Password);
			dynStr.Append("  Prefix_in_type_id=");
			dynStr.Append(Prefix_in_type_id);
			dynStr.Append("  Prefix_type_descr=");
			dynStr.Append(Prefix_type_descr);
			dynStr.Append("  Prefix_type_length=");
			dynStr.Append(Prefix_type_length);
			dynStr.Append("  Prefix_type_delimiter=");
			dynStr.Append(Prefix_type_delimiter);
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

	} // End of AccessListViewRow_Base class
} // End of namespace

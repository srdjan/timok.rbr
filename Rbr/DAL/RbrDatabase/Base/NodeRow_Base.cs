// <fileinfo name="Base\NodeRow_Base.cs">
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
	/// The base class for <see cref="NodeRow"/> that 
	/// represents a record in the <c>Node</c> table.
	/// </summary>
	/// <remarks>
	/// Do not change this source code manually. Update the <see cref="NodeRow"/>
	/// class if you need to add or change some functionality.
	/// </remarks>
	[Serializable]
	public abstract class NodeRow_Base
	{
	#region Timok Custom

		//db field names
		public const string node_id_DbName = "node_id";
		public const string platform_id_DbName = "platform_id";
		public const string description_DbName = "description";
		public const string node_config_DbName = "node_config";
		public const string transport_type_DbName = "transport_type";
		public const string user_name_DbName = "user_name";
		public const string password_DbName = "password";
		public const string ip_address_DbName = "ip_address";
		public const string port_DbName = "port";
		public const string status_DbName = "status";
		public const string billing_export_frequency_DbName = "billing_export_frequency";
		public const string cdr_publishing_frequency_DbName = "cdr_publishing_frequency";

		//prop names
		public const string node_id_PropName = "Node_id";
		public const string platform_id_PropName = "Platform_id";
		public const string description_PropName = "Description";
		public const string node_config_PropName = "Node_config";
		public const string transport_type_PropName = "Transport_type";
		public const string user_name_PropName = "User_name";
		public const string password_PropName = "Password";
		public const string ip_address_PropName = "Ip_address";
		public const string port_PropName = "Port";
		public const string status_PropName = "Status";
		public const string billing_export_frequency_PropName = "Billing_export_frequency";
		public const string cdr_publishing_frequency_PropName = "Cdr_publishing_frequency";

		//db field display names
		public const string node_id_DisplayName = "node id";
		public const string platform_id_DisplayName = "platform id";
		public const string description_DisplayName = "description";
		public const string node_config_DisplayName = "node config";
		public const string transport_type_DisplayName = "transport type";
		public const string user_name_DisplayName = "user name";
		public const string password_DisplayName = "password";
		public const string ip_address_DisplayName = "ip address";
		public const string port_DisplayName = "port";
		public const string status_DisplayName = "status";
		public const string billing_export_frequency_DisplayName = "billing export frequency";
		public const string cdr_publishing_frequency_DisplayName = "cdr publishing frequency";
	#endregion Timok Custom


		private short _node_id;
		private short _platform_id;
		private string _description;
		private int _node_config;
		private byte _transport_type;
		private string _user_name;
		private string _password;
		private int _ip_address;
		private int _port;
		private byte _status;
		private int _billing_export_frequency;
		private int _cdr_publishing_frequency;

		/// <summary>
		/// Initializes a new instance of the <see cref="NodeRow_Base"/> class.
		/// </summary>
		public NodeRow_Base()
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
		/// Gets or sets the <c>platform_id</c> column value.
		/// </summary>
		/// <value>The <c>platform_id</c> column value.</value>
		public short Platform_id
		{
			get { return _platform_id; }
			set { _platform_id = value; }
		}

		/// <summary>
		/// Gets or sets the <c>description</c> column value.
		/// </summary>
		/// <value>The <c>description</c> column value.</value>
		public string Description
		{
			get { return _description; }
			set { _description = value; }
		}

		/// <summary>
		/// Gets or sets the <c>node_config</c> column value.
		/// </summary>
		/// <value>The <c>node_config</c> column value.</value>
		public int Node_config
		{
			get { return _node_config; }
			set { _node_config = value; }
		}

		/// <summary>
		/// Gets or sets the <c>transport_type</c> column value.
		/// </summary>
		/// <value>The <c>transport_type</c> column value.</value>
		public byte Transport_type
		{
			get { return _transport_type; }
			set { _transport_type = value; }
		}

		/// <summary>
		/// Gets or sets the <c>user_name</c> column value.
		/// </summary>
		/// <value>The <c>user_name</c> column value.</value>
		public string User_name
		{
			get { return _user_name; }
			set { _user_name = value; }
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
		/// Gets or sets the <c>ip_address</c> column value.
		/// </summary>
		/// <value>The <c>ip_address</c> column value.</value>
		public int Ip_address
		{
			get { return _ip_address; }
			set { _ip_address = value; }
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
		/// Gets or sets the <c>status</c> column value.
		/// </summary>
		/// <value>The <c>status</c> column value.</value>
		public byte Status
		{
			get { return _status; }
			set { _status = value; }
		}

		/// <summary>
		/// Gets or sets the <c>billing_export_frequency</c> column value.
		/// </summary>
		/// <value>The <c>billing_export_frequency</c> column value.</value>
		public int Billing_export_frequency
		{
			get { return _billing_export_frequency; }
			set { _billing_export_frequency = value; }
		}

		/// <summary>
		/// Gets or sets the <c>cdr_publishing_frequency</c> column value.
		/// </summary>
		/// <value>The <c>cdr_publishing_frequency</c> column value.</value>
		public int Cdr_publishing_frequency
		{
			get { return _cdr_publishing_frequency; }
			set { _cdr_publishing_frequency = value; }
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
			dynStr.Append("  Platform_id=");
			dynStr.Append(Platform_id);
			dynStr.Append("  Description=");
			dynStr.Append(Description);
			dynStr.Append("  Node_config=");
			dynStr.Append(Node_config);
			dynStr.Append("  Transport_type=");
			dynStr.Append(Transport_type);
			dynStr.Append("  User_name=");
			dynStr.Append(User_name);
			dynStr.Append("  Password=");
			dynStr.Append(Password);
			dynStr.Append("  Ip_address=");
			dynStr.Append(Ip_address);
			dynStr.Append("  Port=");
			dynStr.Append(Port);
			dynStr.Append("  Status=");
			dynStr.Append(Status);
			dynStr.Append("  Billing_export_frequency=");
			dynStr.Append(Billing_export_frequency);
			dynStr.Append("  Cdr_publishing_frequency=");
			dynStr.Append(Cdr_publishing_frequency);
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

	} // End of NodeRow_Base class
} // End of namespace

// <fileinfo name="Base\ResidentialVoIPRow_Base.cs">
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
	/// The base class for <see cref="ResidentialVoIPRow"/> that 
	/// represents a record in the <c>ResidentialVoIP</c> table.
	/// </summary>
	/// <remarks>
	/// Do not change this source code manually. Update the <see cref="ResidentialVoIPRow"/>
	/// class if you need to add or change some functionality.
	/// </remarks>
	[Serializable]
	public abstract class ResidentialVoIPRow_Base
	{
	#region Timok Custom

		//db field names
		public const string user_id_DbName = "user_id";
		public const string status_DbName = "status";
		public const string date_first_used_DbName = "date_first_used";
		public const string date_last_used_DbName = "date_last_used";
		public const string password_DbName = "password";
		public const string allow_inbound_calls_DbName = "allow_inbound_calls";
		public const string service_id_DbName = "service_id";
		public const string retail_acct_id_DbName = "retail_acct_id";

		//prop names
		public const string user_id_PropName = "User_id";
		public const string status_PropName = "Status";
		public const string date_first_used_PropName = "Date_first_used";
		public const string date_last_used_PropName = "Date_last_used";
		public const string password_PropName = "Password";
		public const string allow_inbound_calls_PropName = "Allow_inbound_calls";
		public const string service_id_PropName = "Service_id";
		public const string retail_acct_id_PropName = "Retail_acct_id";

		//db field display names
		public const string user_id_DisplayName = "user id";
		public const string status_DisplayName = "status";
		public const string date_first_used_DisplayName = "date first used";
		public const string date_last_used_DisplayName = "date last used";
		public const string password_DisplayName = "password";
		public const string allow_inbound_calls_DisplayName = "allow inbound calls";
		public const string service_id_DisplayName = "service id";
		public const string retail_acct_id_DisplayName = "retail acct id";
	#endregion Timok Custom


		private string _user_id;
		private byte _status;
		private System.DateTime _date_first_used;
		private bool _date_first_usedNull = true;
		private System.DateTime _date_last_used;
		private bool _date_last_usedNull = true;
		private string _password;
		private byte _allow_inbound_calls;
		private short _service_id;
		private bool _service_idNull = true;
		private int _retail_acct_id;

		/// <summary>
		/// Initializes a new instance of the <see cref="ResidentialVoIPRow_Base"/> class.
		/// </summary>
		public ResidentialVoIPRow_Base()
		{
			// EMPTY
		}

		/// <summary>
		/// Gets or sets the <c>user_id</c> column value.
		/// </summary>
		/// <value>The <c>user_id</c> column value.</value>
		public string User_id
		{
			get { return _user_id; }
			set { _user_id = value; }
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
		/// Gets or sets the <c>date_first_used</c> column value.
		/// This column is nullable.
		/// </summary>
		/// <value>The <c>date_first_used</c> column value.</value>
		public System.DateTime Date_first_used
		{
			get
			{
				//if(IsDate_first_usedNull)
					//throw new InvalidOperationException("Cannot get value because it is DBNull.");
				return _date_first_used;
			}
			set
			{
				_date_first_usedNull = false;
				_date_first_used = value;
			}
		}

		/// <summary>
		/// Indicates whether the <see cref="Date_first_used"/>
		/// property value is null.
		/// </summary>
		/// <value>true if the property value is null, otherwise false.</value>
		public bool IsDate_first_usedNull
		{
			get { return _date_first_usedNull; }
			set { _date_first_usedNull = value; }
		}

		/// <summary>
		/// Gets or sets the <c>date_last_used</c> column value.
		/// This column is nullable.
		/// </summary>
		/// <value>The <c>date_last_used</c> column value.</value>
		public System.DateTime Date_last_used
		{
			get
			{
				//if(IsDate_last_usedNull)
					//throw new InvalidOperationException("Cannot get value because it is DBNull.");
				return _date_last_used;
			}
			set
			{
				_date_last_usedNull = false;
				_date_last_used = value;
			}
		}

		/// <summary>
		/// Indicates whether the <see cref="Date_last_used"/>
		/// property value is null.
		/// </summary>
		/// <value>true if the property value is null, otherwise false.</value>
		public bool IsDate_last_usedNull
		{
			get { return _date_last_usedNull; }
			set { _date_last_usedNull = value; }
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
		/// Gets or sets the <c>allow_inbound_calls</c> column value.
		/// </summary>
		/// <value>The <c>allow_inbound_calls</c> column value.</value>
		public byte Allow_inbound_calls
		{
			get { return _allow_inbound_calls; }
			set { _allow_inbound_calls = value; }
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
		/// Gets or sets the <c>retail_acct_id</c> column value.
		/// </summary>
		/// <value>The <c>retail_acct_id</c> column value.</value>
		public int Retail_acct_id
		{
			get { return _retail_acct_id; }
			set { _retail_acct_id = value; }
		}

		/// <summary>
		/// Returns the string representation of this instance.
		/// </summary>
		/// <returns>The string representation of this instance.</returns>
		public override string ToString()
		{
			System.Text.StringBuilder dynStr = new System.Text.StringBuilder(GetType().Name);
			dynStr.Append(':');
			dynStr.Append("  User_id=");
			dynStr.Append(User_id);
			dynStr.Append("  Status=");
			dynStr.Append(Status);
			dynStr.Append("  Date_first_used=");
			dynStr.Append(IsDate_first_usedNull ? (object)"<NULL>" : Date_first_used);
			dynStr.Append("  Date_last_used=");
			dynStr.Append(IsDate_last_usedNull ? (object)"<NULL>" : Date_last_used);
			dynStr.Append("  Password=");
			dynStr.Append(Password);
			dynStr.Append("  Allow_inbound_calls=");
			dynStr.Append(Allow_inbound_calls);
			dynStr.Append("  Service_id=");
			dynStr.Append(IsService_idNull ? (object)"<NULL>" : Service_id);
			dynStr.Append("  Retail_acct_id=");
			dynStr.Append(Retail_acct_id);
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

	} // End of ResidentialVoIPRow_Base class
} // End of namespace

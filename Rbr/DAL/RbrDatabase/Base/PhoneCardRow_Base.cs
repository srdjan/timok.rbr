// <fileinfo name="Base\PhoneCardRow_Base.cs">
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
	/// The base class for <see cref="PhoneCardRow"/> that 
	/// represents a record in the <c>PhoneCard</c> table.
	/// </summary>
	/// <remarks>
	/// Do not change this source code manually. Update the <see cref="PhoneCardRow"/>
	/// class if you need to add or change some functionality.
	/// </remarks>
	[Serializable]
	public abstract class PhoneCardRow_Base
	{
	#region Timok Custom

		//db field names
		public const string service_id_DbName = "service_id";
		public const string pin_DbName = "pin";
		public const string retail_acct_id_DbName = "retail_acct_id";
		public const string serial_number_DbName = "serial_number";
		public const string status_DbName = "status";
		public const string inventory_status_DbName = "inventory_status";
		public const string date_loaded_DbName = "date_loaded";
		public const string date_to_expire_DbName = "date_to_expire";
		public const string date_active_DbName = "date_active";
		public const string date_first_used_DbName = "date_first_used";
		public const string date_last_used_DbName = "date_last_used";
		public const string date_deactivated_DbName = "date_deactivated";
		public const string date_archived_DbName = "date_archived";

		//prop names
		public const string service_id_PropName = "Service_id";
		public const string pin_PropName = "Pin";
		public const string retail_acct_id_PropName = "Retail_acct_id";
		public const string serial_number_PropName = "Serial_number";
		public const string status_PropName = "Status";
		public const string inventory_status_PropName = "Inventory_status";
		public const string date_loaded_PropName = "Date_loaded";
		public const string date_to_expire_PropName = "Date_to_expire";
		public const string date_active_PropName = "Date_active";
		public const string date_first_used_PropName = "Date_first_used";
		public const string date_last_used_PropName = "Date_last_used";
		public const string date_deactivated_PropName = "Date_deactivated";
		public const string date_archived_PropName = "Date_archived";

		//db field display names
		public const string service_id_DisplayName = "service id";
		public const string pin_DisplayName = "pin";
		public const string retail_acct_id_DisplayName = "retail acct id";
		public const string serial_number_DisplayName = "serial number";
		public const string status_DisplayName = "status";
		public const string inventory_status_DisplayName = "inventory status";
		public const string date_loaded_DisplayName = "date loaded";
		public const string date_to_expire_DisplayName = "date to expire";
		public const string date_active_DisplayName = "date active";
		public const string date_first_used_DisplayName = "date first used";
		public const string date_last_used_DisplayName = "date last used";
		public const string date_deactivated_DisplayName = "date deactivated";
		public const string date_archived_DisplayName = "date archived";
	#endregion Timok Custom


		private short _service_id;
		private long _pin;
		private int _retail_acct_id;
		private long _serial_number;
		private byte _status;
		private byte _inventory_status;
		private System.DateTime _date_loaded;
		private System.DateTime _date_to_expire;
		private System.DateTime _date_active;
		private bool _date_activeNull = true;
		private System.DateTime _date_first_used;
		private bool _date_first_usedNull = true;
		private System.DateTime _date_last_used;
		private bool _date_last_usedNull = true;
		private System.DateTime _date_deactivated;
		private bool _date_deactivatedNull = true;
		private System.DateTime _date_archived;
		private bool _date_archivedNull = true;

		/// <summary>
		/// Initializes a new instance of the <see cref="PhoneCardRow_Base"/> class.
		/// </summary>
		public PhoneCardRow_Base()
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
		/// Gets or sets the <c>pin</c> column value.
		/// </summary>
		/// <value>The <c>pin</c> column value.</value>
		public long Pin
		{
			get { return _pin; }
			set { _pin = value; }
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
		/// Gets or sets the <c>serial_number</c> column value.
		/// </summary>
		/// <value>The <c>serial_number</c> column value.</value>
		public long Serial_number
		{
			get { return _serial_number; }
			set { _serial_number = value; }
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
		/// Gets or sets the <c>inventory_status</c> column value.
		/// </summary>
		/// <value>The <c>inventory_status</c> column value.</value>
		public byte Inventory_status
		{
			get { return _inventory_status; }
			set { _inventory_status = value; }
		}

		/// <summary>
		/// Gets or sets the <c>date_loaded</c> column value.
		/// </summary>
		/// <value>The <c>date_loaded</c> column value.</value>
		public System.DateTime Date_loaded
		{
			get { return _date_loaded; }
			set { _date_loaded = value; }
		}

		/// <summary>
		/// Gets or sets the <c>date_to_expire</c> column value.
		/// </summary>
		/// <value>The <c>date_to_expire</c> column value.</value>
		public System.DateTime Date_to_expire
		{
			get { return _date_to_expire; }
			set { _date_to_expire = value; }
		}

		/// <summary>
		/// Gets or sets the <c>date_active</c> column value.
		/// This column is nullable.
		/// </summary>
		/// <value>The <c>date_active</c> column value.</value>
		public System.DateTime Date_active
		{
			get
			{
				//if(IsDate_activeNull)
					//throw new InvalidOperationException("Cannot get value because it is DBNull.");
				return _date_active;
			}
			set
			{
				_date_activeNull = false;
				_date_active = value;
			}
		}

		/// <summary>
		/// Indicates whether the <see cref="Date_active"/>
		/// property value is null.
		/// </summary>
		/// <value>true if the property value is null, otherwise false.</value>
		public bool IsDate_activeNull
		{
			get { return _date_activeNull; }
			set { _date_activeNull = value; }
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
		/// Gets or sets the <c>date_deactivated</c> column value.
		/// This column is nullable.
		/// </summary>
		/// <value>The <c>date_deactivated</c> column value.</value>
		public System.DateTime Date_deactivated
		{
			get
			{
				//if(IsDate_deactivatedNull)
					//throw new InvalidOperationException("Cannot get value because it is DBNull.");
				return _date_deactivated;
			}
			set
			{
				_date_deactivatedNull = false;
				_date_deactivated = value;
			}
		}

		/// <summary>
		/// Indicates whether the <see cref="Date_deactivated"/>
		/// property value is null.
		/// </summary>
		/// <value>true if the property value is null, otherwise false.</value>
		public bool IsDate_deactivatedNull
		{
			get { return _date_deactivatedNull; }
			set { _date_deactivatedNull = value; }
		}

		/// <summary>
		/// Gets or sets the <c>date_archived</c> column value.
		/// This column is nullable.
		/// </summary>
		/// <value>The <c>date_archived</c> column value.</value>
		public System.DateTime Date_archived
		{
			get
			{
				//if(IsDate_archivedNull)
					//throw new InvalidOperationException("Cannot get value because it is DBNull.");
				return _date_archived;
			}
			set
			{
				_date_archivedNull = false;
				_date_archived = value;
			}
		}

		/// <summary>
		/// Indicates whether the <see cref="Date_archived"/>
		/// property value is null.
		/// </summary>
		/// <value>true if the property value is null, otherwise false.</value>
		public bool IsDate_archivedNull
		{
			get { return _date_archivedNull; }
			set { _date_archivedNull = value; }
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
			dynStr.Append("  Pin=");
			dynStr.Append(Pin);
			dynStr.Append("  Retail_acct_id=");
			dynStr.Append(Retail_acct_id);
			dynStr.Append("  Serial_number=");
			dynStr.Append(Serial_number);
			dynStr.Append("  Status=");
			dynStr.Append(Status);
			dynStr.Append("  Inventory_status=");
			dynStr.Append(Inventory_status);
			dynStr.Append("  Date_loaded=");
			dynStr.Append(Date_loaded);
			dynStr.Append("  Date_to_expire=");
			dynStr.Append(Date_to_expire);
			dynStr.Append("  Date_active=");
			dynStr.Append(IsDate_activeNull ? (object)"<NULL>" : Date_active);
			dynStr.Append("  Date_first_used=");
			dynStr.Append(IsDate_first_usedNull ? (object)"<NULL>" : Date_first_used);
			dynStr.Append("  Date_last_used=");
			dynStr.Append(IsDate_last_usedNull ? (object)"<NULL>" : Date_last_used);
			dynStr.Append("  Date_deactivated=");
			dynStr.Append(IsDate_deactivatedNull ? (object)"<NULL>" : Date_deactivated);
			dynStr.Append("  Date_archived=");
			dynStr.Append(IsDate_archivedNull ? (object)"<NULL>" : Date_archived);
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

	} // End of PhoneCardRow_Base class
} // End of namespace

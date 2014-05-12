// <fileinfo name="Base\ResidentialPSTNRow_Base.cs">
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
	/// The base class for <see cref="ResidentialPSTNRow"/> that 
	/// represents a record in the <c>ResidentialPSTN</c> table.
	/// </summary>
	/// <remarks>
	/// Do not change this source code manually. Update the <see cref="ResidentialPSTNRow"/>
	/// class if you need to add or change some functionality.
	/// </remarks>
	[Serializable]
	public abstract class ResidentialPSTNRow_Base
	{
	#region Timok Custom

		//db field names
		public const string service_id_DbName = "service_id";
		public const string ANI_DbName = "ANI";
		public const string status_DbName = "status";
		public const string date_first_used_DbName = "date_first_used";
		public const string date_last_used_DbName = "date_last_used";
		public const string retail_acct_id_DbName = "retail_acct_id";

		//prop names
		public const string service_id_PropName = "Service_id";
		public const string ANI_PropName = "ANI";
		public const string status_PropName = "Status";
		public const string date_first_used_PropName = "Date_first_used";
		public const string date_last_used_PropName = "Date_last_used";
		public const string retail_acct_id_PropName = "Retail_acct_id";

		//db field display names
		public const string service_id_DisplayName = "service id";
		public const string ANI_DisplayName = "ANI";
		public const string status_DisplayName = "status";
		public const string date_first_used_DisplayName = "date first used";
		public const string date_last_used_DisplayName = "date last used";
		public const string retail_acct_id_DisplayName = "retail acct id";
	#endregion Timok Custom


		private short _service_id;
		private long _ani;
		private byte _status;
		private System.DateTime _date_first_used;
		private bool _date_first_usedNull = true;
		private System.DateTime _date_last_used;
		private bool _date_last_usedNull = true;
		private int _retail_acct_id;

		/// <summary>
		/// Initializes a new instance of the <see cref="ResidentialPSTNRow_Base"/> class.
		/// </summary>
		public ResidentialPSTNRow_Base()
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
		/// Gets or sets the <c>ANI</c> column value.
		/// </summary>
		/// <value>The <c>ANI</c> column value.</value>
		public long ANI
		{
			get { return _ani; }
			set { _ani = value; }
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
			dynStr.Append("  Service_id=");
			dynStr.Append(Service_id);
			dynStr.Append("  ANI=");
			dynStr.Append(ANI);
			dynStr.Append("  Status=");
			dynStr.Append(Status);
			dynStr.Append("  Date_first_used=");
			dynStr.Append(IsDate_first_usedNull ? (object)"<NULL>" : Date_first_used);
			dynStr.Append("  Date_last_used=");
			dynStr.Append(IsDate_last_usedNull ? (object)"<NULL>" : Date_last_used);
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

	} // End of ResidentialPSTNRow_Base class
} // End of namespace

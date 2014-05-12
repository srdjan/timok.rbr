// <fileinfo name="Base\CustomerSupportGroupRow_Base.cs">
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
	/// The base class for <see cref="CustomerSupportGroupRow"/> that 
	/// represents a record in the <c>CustomerSupportGroup</c> table.
	/// </summary>
	/// <remarks>
	/// Do not change this source code manually. Update the <see cref="CustomerSupportGroupRow"/>
	/// class if you need to add or change some functionality.
	/// </remarks>
	[Serializable]
	public abstract class CustomerSupportGroupRow_Base
	{
	#region Timok Custom

		//db field names
		public const string group_id_DbName = "group_id";
		public const string description_DbName = "description";
		public const string role_DbName = "role";
		public const string max_amount_DbName = "max_amount";
		public const string allow_status_change_DbName = "allow_status_change";
		public const string vendor_id_DbName = "vendor_id";

		//prop names
		public const string group_id_PropName = "Group_id";
		public const string description_PropName = "Description";
		public const string role_PropName = "Role";
		public const string max_amount_PropName = "Max_amount";
		public const string allow_status_change_PropName = "Allow_status_change";
		public const string vendor_id_PropName = "Vendor_id";

		//db field display names
		public const string group_id_DisplayName = "group id";
		public const string description_DisplayName = "description";
		public const string role_DisplayName = "role";
		public const string max_amount_DisplayName = "max amount";
		public const string allow_status_change_DisplayName = "allow status change";
		public const string vendor_id_DisplayName = "vendor id";
	#endregion Timok Custom


		private int _group_id;
		private string _description;
		private int _role;
		private decimal _max_amount;
		private byte _allow_status_change;
		private int _vendor_id;

		/// <summary>
		/// Initializes a new instance of the <see cref="CustomerSupportGroupRow_Base"/> class.
		/// </summary>
		public CustomerSupportGroupRow_Base()
		{
			// EMPTY
		}

		/// <summary>
		/// Gets or sets the <c>group_id</c> column value.
		/// </summary>
		/// <value>The <c>group_id</c> column value.</value>
		public int Group_id
		{
			get { return _group_id; }
			set { _group_id = value; }
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
		/// Gets or sets the <c>role</c> column value.
		/// </summary>
		/// <value>The <c>role</c> column value.</value>
		public int Role
		{
			get { return _role; }
			set { _role = value; }
		}

		/// <summary>
		/// Gets or sets the <c>max_amount</c> column value.
		/// </summary>
		/// <value>The <c>max_amount</c> column value.</value>
		public decimal Max_amount
		{
			get { return _max_amount; }
			set { _max_amount = value; }
		}

		/// <summary>
		/// Gets or sets the <c>allow_status_change</c> column value.
		/// </summary>
		/// <value>The <c>allow_status_change</c> column value.</value>
		public byte Allow_status_change
		{
			get { return _allow_status_change; }
			set { _allow_status_change = value; }
		}

		/// <summary>
		/// Gets or sets the <c>vendor_id</c> column value.
		/// </summary>
		/// <value>The <c>vendor_id</c> column value.</value>
		public int Vendor_id
		{
			get { return _vendor_id; }
			set { _vendor_id = value; }
		}

		/// <summary>
		/// Returns the string representation of this instance.
		/// </summary>
		/// <returns>The string representation of this instance.</returns>
		public override string ToString()
		{
			System.Text.StringBuilder dynStr = new System.Text.StringBuilder(GetType().Name);
			dynStr.Append(':');
			dynStr.Append("  Group_id=");
			dynStr.Append(Group_id);
			dynStr.Append("  Description=");
			dynStr.Append(Description);
			dynStr.Append("  Role=");
			dynStr.Append(Role);
			dynStr.Append("  Max_amount=");
			dynStr.Append(Max_amount);
			dynStr.Append("  Allow_status_change=");
			dynStr.Append(Allow_status_change);
			dynStr.Append("  Vendor_id=");
			dynStr.Append(Vendor_id);
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

	} // End of CustomerSupportGroupRow_Base class
} // End of namespace

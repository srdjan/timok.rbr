// <fileinfo name="Base\BalanceAdjustmentReasonRow_Base.cs">
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
	/// The base class for <see cref="BalanceAdjustmentReasonRow"/> that 
	/// represents a record in the <c>BalanceAdjustmentReason</c> table.
	/// </summary>
	/// <remarks>
	/// Do not change this source code manually. Update the <see cref="BalanceAdjustmentReasonRow"/>
	/// class if you need to add or change some functionality.
	/// </remarks>
	[Serializable]
	public abstract class BalanceAdjustmentReasonRow_Base
	{
	#region Timok Custom

		//db field names
		public const string balance_adjustment_reason_id_DbName = "balance_adjustment_reason_id";
		public const string description_DbName = "description";
		public const string type_DbName = "type";

		//prop names
		public const string balance_adjustment_reason_id_PropName = "Balance_adjustment_reason_id";
		public const string description_PropName = "Description";
		public const string type_PropName = "Type";

		//db field display names
		public const string balance_adjustment_reason_id_DisplayName = "balance adjustment reason id";
		public const string description_DisplayName = "description";
		public const string type_DisplayName = "type";
	#endregion Timok Custom


		private int _balance_adjustment_reason_id;
		private string _description;
		private byte _type;

		/// <summary>
		/// Initializes a new instance of the <see cref="BalanceAdjustmentReasonRow_Base"/> class.
		/// </summary>
		public BalanceAdjustmentReasonRow_Base()
		{
			// EMPTY
		}

		/// <summary>
		/// Gets or sets the <c>balance_adjustment_reason_id</c> column value.
		/// </summary>
		/// <value>The <c>balance_adjustment_reason_id</c> column value.</value>
		public int Balance_adjustment_reason_id
		{
			get { return _balance_adjustment_reason_id; }
			set { _balance_adjustment_reason_id = value; }
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
		/// Gets or sets the <c>type</c> column value.
		/// </summary>
		/// <value>The <c>type</c> column value.</value>
		public byte Type
		{
			get { return _type; }
			set { _type = value; }
		}

		/// <summary>
		/// Returns the string representation of this instance.
		/// </summary>
		/// <returns>The string representation of this instance.</returns>
		public override string ToString()
		{
			System.Text.StringBuilder dynStr = new System.Text.StringBuilder(GetType().Name);
			dynStr.Append(':');
			dynStr.Append("  Balance_adjustment_reason_id=");
			dynStr.Append(Balance_adjustment_reason_id);
			dynStr.Append("  Description=");
			dynStr.Append(Description);
			dynStr.Append("  Type=");
			dynStr.Append(Type);
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

	} // End of BalanceAdjustmentReasonRow_Base class
} // End of namespace

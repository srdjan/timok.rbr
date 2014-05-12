// <fileinfo name="Base\PayphoneSurchargeRow_Base.cs">
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
	/// The base class for <see cref="PayphoneSurchargeRow"/> that 
	/// represents a record in the <c>PayphoneSurcharge</c> table.
	/// </summary>
	/// <remarks>
	/// Do not change this source code manually. Update the <see cref="PayphoneSurchargeRow"/>
	/// class if you need to add or change some functionality.
	/// </remarks>
	[Serializable]
	public abstract class PayphoneSurchargeRow_Base
	{
	#region Timok Custom

		//db field names
		public const string payphone_surcharge_id_DbName = "payphone_surcharge_id";
		public const string surcharge_DbName = "surcharge";
		public const string surcharge_type_DbName = "surcharge_type";

		//prop names
		public const string payphone_surcharge_id_PropName = "Payphone_surcharge_id";
		public const string surcharge_PropName = "Surcharge";
		public const string surcharge_type_PropName = "Surcharge_type";

		//db field display names
		public const string payphone_surcharge_id_DisplayName = "payphone surcharge id";
		public const string surcharge_DisplayName = "surcharge";
		public const string surcharge_type_DisplayName = "surcharge type";
	#endregion Timok Custom


		private int _payphone_surcharge_id;
		private decimal _surcharge;
		private byte _surcharge_type;

		/// <summary>
		/// Initializes a new instance of the <see cref="PayphoneSurchargeRow_Base"/> class.
		/// </summary>
		public PayphoneSurchargeRow_Base()
		{
			// EMPTY
		}

		/// <summary>
		/// Gets or sets the <c>payphone_surcharge_id</c> column value.
		/// </summary>
		/// <value>The <c>payphone_surcharge_id</c> column value.</value>
		public int Payphone_surcharge_id
		{
			get { return _payphone_surcharge_id; }
			set { _payphone_surcharge_id = value; }
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
		/// Returns the string representation of this instance.
		/// </summary>
		/// <returns>The string representation of this instance.</returns>
		public override string ToString()
		{
			System.Text.StringBuilder dynStr = new System.Text.StringBuilder(GetType().Name);
			dynStr.Append(':');
			dynStr.Append("  Payphone_surcharge_id=");
			dynStr.Append(Payphone_surcharge_id);
			dynStr.Append("  Surcharge=");
			dynStr.Append(Surcharge);
			dynStr.Append("  Surcharge_type=");
			dynStr.Append(Surcharge_type);
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

	} // End of PayphoneSurchargeRow_Base class
} // End of namespace

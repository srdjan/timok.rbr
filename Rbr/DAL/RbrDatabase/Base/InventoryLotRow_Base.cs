// <fileinfo name="Base\InventoryLotRow_Base.cs">
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
	/// The base class for <see cref="InventoryLotRow"/> that 
	/// represents a record in the <c>InventoryLot</c> table.
	/// </summary>
	/// <remarks>
	/// Do not change this source code manually. Update the <see cref="InventoryLotRow"/>
	/// class if you need to add or change some functionality.
	/// </remarks>
	[Serializable]
	public abstract class InventoryLotRow_Base
	{
	#region Timok Custom

		//db field names
		public const string lot_id_DbName = "lot_id";
		public const string service_id_DbName = "service_id";
		public const string denomination_DbName = "denomination";

		//prop names
		public const string lot_id_PropName = "Lot_id";
		public const string service_id_PropName = "Service_id";
		public const string denomination_PropName = "Denomination";

		//db field display names
		public const string lot_id_DisplayName = "lot id";
		public const string service_id_DisplayName = "service id";
		public const string denomination_DisplayName = "denomination";
	#endregion Timok Custom


		private int _lot_id;
		private short _service_id;
		private decimal _denomination;

		/// <summary>
		/// Initializes a new instance of the <see cref="InventoryLotRow_Base"/> class.
		/// </summary>
		public InventoryLotRow_Base()
		{
			// EMPTY
		}

		/// <summary>
		/// Gets or sets the <c>lot_id</c> column value.
		/// </summary>
		/// <value>The <c>lot_id</c> column value.</value>
		public int Lot_id
		{
			get { return _lot_id; }
			set { _lot_id = value; }
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
		/// Gets or sets the <c>denomination</c> column value.
		/// </summary>
		/// <value>The <c>denomination</c> column value.</value>
		public decimal Denomination
		{
			get { return _denomination; }
			set { _denomination = value; }
		}

		/// <summary>
		/// Returns the string representation of this instance.
		/// </summary>
		/// <returns>The string representation of this instance.</returns>
		public override string ToString()
		{
			System.Text.StringBuilder dynStr = new System.Text.StringBuilder(GetType().Name);
			dynStr.Append(':');
			dynStr.Append("  Lot_id=");
			dynStr.Append(Lot_id);
			dynStr.Append("  Service_id=");
			dynStr.Append(Service_id);
			dynStr.Append("  Denomination=");
			dynStr.Append(Denomination);
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

	} // End of InventoryLotRow_Base class
} // End of namespace

// <fileinfo name="Base\BoxRow_Base.cs">
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
	/// The base class for <see cref="BoxRow"/> that 
	/// represents a record in the <c>Box</c> table.
	/// </summary>
	/// <remarks>
	/// Do not change this source code manually. Update the <see cref="BoxRow"/>
	/// class if you need to add or change some functionality.
	/// </remarks>
	[Serializable]
	public abstract class BoxRow_Base
	{
	#region Timok Custom

		//db field names
		public const string box_id_DbName = "box_id";
		public const string date_created_DbName = "date_created";
		public const string date_activated_DbName = "date_activated";

		//prop names
		public const string box_id_PropName = "Box_id";
		public const string date_created_PropName = "Date_created";
		public const string date_activated_PropName = "Date_activated";

		//db field display names
		public const string box_id_DisplayName = "box id";
		public const string date_created_DisplayName = "date created";
		public const string date_activated_DisplayName = "date activated";
	#endregion Timok Custom


		private int _box_id;
		private System.DateTime _date_created;
		private System.DateTime _date_activated;
		private bool _date_activatedNull = true;

		/// <summary>
		/// Initializes a new instance of the <see cref="BoxRow_Base"/> class.
		/// </summary>
		public BoxRow_Base()
		{
			// EMPTY
		}

		/// <summary>
		/// Gets or sets the <c>box_id</c> column value.
		/// </summary>
		/// <value>The <c>box_id</c> column value.</value>
		public int Box_id
		{
			get { return _box_id; }
			set { _box_id = value; }
		}

		/// <summary>
		/// Gets or sets the <c>date_created</c> column value.
		/// </summary>
		/// <value>The <c>date_created</c> column value.</value>
		public System.DateTime Date_created
		{
			get { return _date_created; }
			set { _date_created = value; }
		}

		/// <summary>
		/// Gets or sets the <c>date_activated</c> column value.
		/// This column is nullable.
		/// </summary>
		/// <value>The <c>date_activated</c> column value.</value>
		public System.DateTime Date_activated
		{
			get
			{
				//if(IsDate_activatedNull)
					//throw new InvalidOperationException("Cannot get value because it is DBNull.");
				return _date_activated;
			}
			set
			{
				_date_activatedNull = false;
				_date_activated = value;
			}
		}

		/// <summary>
		/// Indicates whether the <see cref="Date_activated"/>
		/// property value is null.
		/// </summary>
		/// <value>true if the property value is null, otherwise false.</value>
		public bool IsDate_activatedNull
		{
			get { return _date_activatedNull; }
			set { _date_activatedNull = value; }
		}

		/// <summary>
		/// Returns the string representation of this instance.
		/// </summary>
		/// <returns>The string representation of this instance.</returns>
		public override string ToString()
		{
			System.Text.StringBuilder dynStr = new System.Text.StringBuilder(GetType().Name);
			dynStr.Append(':');
			dynStr.Append("  Box_id=");
			dynStr.Append(Box_id);
			dynStr.Append("  Date_created=");
			dynStr.Append(Date_created);
			dynStr.Append("  Date_activated=");
			dynStr.Append(IsDate_activatedNull ? (object)"<NULL>" : Date_activated);
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

	} // End of BoxRow_Base class
} // End of namespace

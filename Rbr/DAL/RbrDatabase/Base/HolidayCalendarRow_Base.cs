// <fileinfo name="Base\HolidayCalendarRow_Base.cs">
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
	/// The base class for <see cref="HolidayCalendarRow"/> that 
	/// represents a record in the <c>HolidayCalendar</c> table.
	/// </summary>
	/// <remarks>
	/// Do not change this source code manually. Update the <see cref="HolidayCalendarRow"/>
	/// class if you need to add or change some functionality.
	/// </remarks>
	[Serializable]
	public abstract class HolidayCalendarRow_Base
	{
	#region Timok Custom

		//db field names
		public const string rate_info_id_DbName = "rate_info_id";
		public const string holiday_day_DbName = "holiday_day";
		public const string name_DbName = "name";

		//prop names
		public const string rate_info_id_PropName = "Rate_info_id";
		public const string holiday_day_PropName = "Holiday_day";
		public const string name_PropName = "Name";

		//db field display names
		public const string rate_info_id_DisplayName = "rate info id";
		public const string holiday_day_DisplayName = "holiday day";
		public const string name_DisplayName = "name";
	#endregion Timok Custom


		private int _rate_info_id;
		private System.DateTime _holiday_day;
		private string _name;

		/// <summary>
		/// Initializes a new instance of the <see cref="HolidayCalendarRow_Base"/> class.
		/// </summary>
		public HolidayCalendarRow_Base()
		{
			// EMPTY
		}

		/// <summary>
		/// Gets or sets the <c>rate_info_id</c> column value.
		/// </summary>
		/// <value>The <c>rate_info_id</c> column value.</value>
		public int Rate_info_id
		{
			get { return _rate_info_id; }
			set { _rate_info_id = value; }
		}

		/// <summary>
		/// Gets or sets the <c>holiday_day</c> column value.
		/// </summary>
		/// <value>The <c>holiday_day</c> column value.</value>
		public System.DateTime Holiday_day
		{
			get { return _holiday_day; }
			set { _holiday_day = value; }
		}

		/// <summary>
		/// Gets or sets the <c>name</c> column value.
		/// </summary>
		/// <value>The <c>name</c> column value.</value>
		public string Name
		{
			get { return _name; }
			set { _name = value; }
		}

		/// <summary>
		/// Returns the string representation of this instance.
		/// </summary>
		/// <returns>The string representation of this instance.</returns>
		public override string ToString()
		{
			System.Text.StringBuilder dynStr = new System.Text.StringBuilder(GetType().Name);
			dynStr.Append(':');
			dynStr.Append("  Rate_info_id=");
			dynStr.Append(Rate_info_id);
			dynStr.Append("  Holiday_day=");
			dynStr.Append(Holiday_day);
			dynStr.Append("  Name=");
			dynStr.Append(Name);
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

	} // End of HolidayCalendarRow_Base class
} // End of namespace

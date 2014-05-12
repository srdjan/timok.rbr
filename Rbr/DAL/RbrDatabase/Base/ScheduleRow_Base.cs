// <fileinfo name="Base\ScheduleRow_Base.cs">
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
	/// The base class for <see cref="ScheduleRow"/> that 
	/// represents a record in the <c>Schedule</c> table.
	/// </summary>
	/// <remarks>
	/// Do not change this source code manually. Update the <see cref="ScheduleRow"/>
	/// class if you need to add or change some functionality.
	/// </remarks>
	[Serializable]
	public abstract class ScheduleRow_Base
	{
	#region Timok Custom

		//db field names
		public const string schedule_id_DbName = "schedule_id";
		public const string type_DbName = "type";
		public const string day_of_week_DbName = "day_of_week";
		public const string day_of_the_month_1_DbName = "day_of_the_month_1";
		public const string day_of_the_month_2_DbName = "day_of_the_month_2";

		//prop names
		public const string schedule_id_PropName = "Schedule_id";
		public const string type_PropName = "Type";
		public const string day_of_week_PropName = "Day_of_week";
		public const string day_of_the_month_1_PropName = "Day_of_the_month_1";
		public const string day_of_the_month_2_PropName = "Day_of_the_month_2";

		//db field display names
		public const string schedule_id_DisplayName = "schedule id";
		public const string type_DisplayName = "type";
		public const string day_of_week_DisplayName = "day of week";
		public const string day_of_the_month_1_DisplayName = "day of the month 1";
		public const string day_of_the_month_2_DisplayName = "day of the month 2";
	#endregion Timok Custom


		private int _schedule_id;
		private byte _type;
		private short _day_of_week;
		private int _day_of_the_month_1;
		private int _day_of_the_month_2;

		/// <summary>
		/// Initializes a new instance of the <see cref="ScheduleRow_Base"/> class.
		/// </summary>
		public ScheduleRow_Base()
		{
			// EMPTY
		}

		/// <summary>
		/// Gets or sets the <c>schedule_id</c> column value.
		/// </summary>
		/// <value>The <c>schedule_id</c> column value.</value>
		public int Schedule_id
		{
			get { return _schedule_id; }
			set { _schedule_id = value; }
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
		/// Gets or sets the <c>day_of_week</c> column value.
		/// </summary>
		/// <value>The <c>day_of_week</c> column value.</value>
		public short Day_of_week
		{
			get { return _day_of_week; }
			set { _day_of_week = value; }
		}

		/// <summary>
		/// Gets or sets the <c>day_of_the_month_1</c> column value.
		/// </summary>
		/// <value>The <c>day_of_the_month_1</c> column value.</value>
		public int Day_of_the_month_1
		{
			get { return _day_of_the_month_1; }
			set { _day_of_the_month_1 = value; }
		}

		/// <summary>
		/// Gets or sets the <c>day_of_the_month_2</c> column value.
		/// </summary>
		/// <value>The <c>day_of_the_month_2</c> column value.</value>
		public int Day_of_the_month_2
		{
			get { return _day_of_the_month_2; }
			set { _day_of_the_month_2 = value; }
		}

		/// <summary>
		/// Returns the string representation of this instance.
		/// </summary>
		/// <returns>The string representation of this instance.</returns>
		public override string ToString()
		{
			System.Text.StringBuilder dynStr = new System.Text.StringBuilder(GetType().Name);
			dynStr.Append(':');
			dynStr.Append("  Schedule_id=");
			dynStr.Append(Schedule_id);
			dynStr.Append("  Type=");
			dynStr.Append(Type);
			dynStr.Append("  Day_of_week=");
			dynStr.Append(Day_of_week);
			dynStr.Append("  Day_of_the_month_1=");
			dynStr.Append(Day_of_the_month_1);
			dynStr.Append("  Day_of_the_month_2=");
			dynStr.Append(Day_of_the_month_2);
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

	} // End of ScheduleRow_Base class
} // End of namespace

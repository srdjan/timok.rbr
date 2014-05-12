// <fileinfo name="Base\TimeOfDayPeriodRow_Base.cs">
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
	/// The base class for <see cref="TimeOfDayPeriodRow"/> that 
	/// represents a record in the <c>TimeOfDayPeriod</c> table.
	/// </summary>
	/// <remarks>
	/// Do not change this source code manually. Update the <see cref="TimeOfDayPeriodRow"/>
	/// class if you need to add or change some functionality.
	/// </remarks>
	[Serializable]
	public abstract class TimeOfDayPeriodRow_Base
	{
	#region Timok Custom

		//db field names
		public const string rate_info_id_DbName = "rate_info_id";
		public const string type_of_day_choice_DbName = "type_of_day_choice";
		public const string start_hour_DbName = "start_hour";
		public const string stop_hour_DbName = "stop_hour";
		public const string time_of_day_DbName = "time_of_day";

		//prop names
		public const string rate_info_id_PropName = "Rate_info_id";
		public const string type_of_day_choice_PropName = "Type_of_day_choice";
		public const string start_hour_PropName = "Start_hour";
		public const string stop_hour_PropName = "Stop_hour";
		public const string time_of_day_PropName = "Time_of_day";

		//db field display names
		public const string rate_info_id_DisplayName = "rate info id";
		public const string type_of_day_choice_DisplayName = "type of day choice";
		public const string start_hour_DisplayName = "start hour";
		public const string stop_hour_DisplayName = "stop hour";
		public const string time_of_day_DisplayName = "time of day";
	#endregion Timok Custom


		private int _rate_info_id;
		private byte _type_of_day_choice;
		private short _start_hour;
		private short _stop_hour;
		private byte _time_of_day;

		/// <summary>
		/// Initializes a new instance of the <see cref="TimeOfDayPeriodRow_Base"/> class.
		/// </summary>
		public TimeOfDayPeriodRow_Base()
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
		/// Gets or sets the <c>type_of_day_choice</c> column value.
		/// </summary>
		/// <value>The <c>type_of_day_choice</c> column value.</value>
		public byte Type_of_day_choice
		{
			get { return _type_of_day_choice; }
			set { _type_of_day_choice = value; }
		}

		/// <summary>
		/// Gets or sets the <c>start_hour</c> column value.
		/// </summary>
		/// <value>The <c>start_hour</c> column value.</value>
		public short Start_hour
		{
			get { return _start_hour; }
			set { _start_hour = value; }
		}

		/// <summary>
		/// Gets or sets the <c>stop_hour</c> column value.
		/// </summary>
		/// <value>The <c>stop_hour</c> column value.</value>
		public short Stop_hour
		{
			get { return _stop_hour; }
			set { _stop_hour = value; }
		}

		/// <summary>
		/// Gets or sets the <c>time_of_day</c> column value.
		/// </summary>
		/// <value>The <c>time_of_day</c> column value.</value>
		public byte Time_of_day
		{
			get { return _time_of_day; }
			set { _time_of_day = value; }
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
			dynStr.Append("  Type_of_day_choice=");
			dynStr.Append(Type_of_day_choice);
			dynStr.Append("  Start_hour=");
			dynStr.Append(Start_hour);
			dynStr.Append("  Stop_hour=");
			dynStr.Append(Stop_hour);
			dynStr.Append("  Time_of_day=");
			dynStr.Append(Time_of_day);
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

	} // End of TimeOfDayPeriodRow_Base class
} // End of namespace

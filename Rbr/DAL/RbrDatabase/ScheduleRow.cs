// <fileinfo name="ScheduleRow.cs">
//		<copyright>
//			Copyright © 2002-2007 Timok ES LLC. All rights reserved.
//		</copyright>
//		<remarks>
//			You can update this source code manually. If the file
//			already exists it will not be rewritten by the generator.
//		</remarks>
//		<generator rewritefile="False" infourl="http://www.SharpPower.com">RapTier</generator>
// </fileinfo>

using System;
using Timok.Rbr.DAL.RbrDatabase.Base;
using System.Runtime.Serialization;
using System.Xml.Serialization;
using Timok.Rbr.Core.Config;

namespace Timok.Rbr.DAL.RbrDatabase {
	/// <summary>
	/// Represents a record in the <c>Schedule</c> table.
	/// </summary>
	[Serializable]
	public class ScheduleRow : ScheduleRow_Base {

    public const string ScheduleType_PropName = "ScheduleType";
    public const string ScheduleType_DisplayName = "Schedule Type";

    public const string DayOfWeek_PropName = "DayOfWeek";
    public const string DayOfWeek_DisplayName = "Day Of Week";

    [XmlIgnore]
    public ScheduleType ScheduleType {
      get { return (ScheduleType) this.Type; }
      set { this.Type = (byte) value; }
    }

    [XmlIgnore]
    public DayOfWeek DayOfWeek {
      get { return (DayOfWeek) this.Day_of_week; }
      set { this.Day_of_week = (short) value; }
    }
    /// <summary>
		/// Initializes a new instance of the <see cref="ScheduleRow"/> class.
		/// </summary>
		public ScheduleRow(){
			// EMPTY
		}

		public override bool Equals(object obj) {
			return base.Equals(obj);
		}

		public override int GetHashCode() {
			return base.GetHashCode ();
		}

	} // End of ScheduleRow class
} // End of namespace

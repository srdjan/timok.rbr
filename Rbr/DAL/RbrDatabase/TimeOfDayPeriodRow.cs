// <fileinfo name="TimeOfDayPeriodRow.cs">
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
using System.Runtime.Serialization;
using Timok.Rbr.DAL.RbrDatabase.Base;
using Timok.Rbr.Core.Config;

namespace Timok.Rbr.DAL.RbrDatabase {
	/// <summary>
	/// Represents a record in the <c>TimeOfDayPeriod</c> table.
	/// </summary>
	[Serializable]
	public class TimeOfDayPeriodRow : TimeOfDayPeriodRow_Base {

		public TypeOfDayChoice TypeOfDayChoice {
			get { return (TypeOfDayChoice) Type_of_day_choice; }
			set { Type_of_day_choice = (byte) value; }
		}
		
		public TimeOfDay TimeOfDay {
			get { return (TimeOfDay) Time_of_day; }
			set { Time_of_day = (byte) value; }
		}
		/// <summary>
		/// Initializes a new instance of the <see cref="TimeOfDayPeriodRow"/> class.
		/// </summary>
		public TimeOfDayPeriodRow(){
			// EMPTY
		}

		public override bool Equals(object obj) {
			return base.Equals(obj);
		}

		public override int GetHashCode() {
			return base.GetHashCode ();
		}

	} // End of TimeOfDayPeriodRow class
} // End of namespace

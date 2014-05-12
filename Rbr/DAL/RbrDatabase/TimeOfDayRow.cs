// <fileinfo name="TimeOfDayRow.cs">
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
	/// Represents a record in the <c>TimeOfDay</c> table.
	/// </summary>
	[Serializable]
	public class TimeOfDayRow : TimeOfDayRow_Base {
						
		public TimeOfDay TimeOfDay {
			get { return (TimeOfDay) this.Time_of_day; }
			set { this.Time_of_day = (byte) value; }
		}
						
		public TimeOfDayPolicy TimeOfDayPolicy {
			get { return (TimeOfDayPolicy) this.Time_of_day_policy; }
			set { this.Time_of_day_policy = (byte) value; }
		}
		/// <summary>
		/// Initializes a new instance of the <see cref="TimeOfDayRow"/> class.
		/// </summary>
		public TimeOfDayRow(){
			// EMPTY
		}

		public override bool Equals(object obj) {
			return base.Equals(obj);
		}

		public override int GetHashCode() {
			return base.GetHashCode ();
		}

	} // End of TimeOfDayRow class
} // End of namespace

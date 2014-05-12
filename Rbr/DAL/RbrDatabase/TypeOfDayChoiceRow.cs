// <fileinfo name="TypeOfDayChoiceRow.cs">
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
	/// Represents a record in the <c>TypeOfDayChoice</c> table.
	/// </summary>
	[Serializable]
	public class TypeOfDayChoiceRow : TypeOfDayChoiceRow_Base {
						
		public TypeOfDayChoice TypeOfDayChoice {
			get { return (TypeOfDayChoice) this.Type_of_day_choice; }
			set { this.Type_of_day_choice = (byte) value; }
		}
		
		/// <summary>
		/// Initializes a new instance of the <see cref="TypeOfDayChoiceRow"/> class.
		/// </summary>
		public TypeOfDayChoiceRow(){
			// EMPTY
		}

		public override bool Equals(object obj) {
			return base.Equals(obj);
		}

		public override int GetHashCode() {
			return base.GetHashCode ();
		}

	} // End of TypeOfDayChoiceRow class
} // End of namespace

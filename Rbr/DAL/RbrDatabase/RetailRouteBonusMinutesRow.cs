// <fileinfo name="RetailRouteBonusMinutesRow.cs">
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

namespace Timok.Rbr.DAL.RbrDatabase {
	/// <summary>
	/// Represents a record in the <c>RetailRouteBonusMinutes</c> table.
	/// </summary>
	[Serializable]
	public class RetailRouteBonusMinutesRow : RetailRouteBonusMinutesRow_Base {
		/// <summary>
		/// Initializes a new instance of the <see cref="RetailRouteBonusMinutesRow"/> class.
		/// </summary>
		public RetailRouteBonusMinutesRow(){
			// EMPTY
		}

		public override bool Equals(object obj) {
			return base.Equals(obj);
		}

		public override int GetHashCode() {
			return base.GetHashCode ();
		}

	} // End of RetailRouteBonusMinutesRow class
} // End of namespace

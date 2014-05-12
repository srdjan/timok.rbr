// <fileinfo name="CountryRow.cs">
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
	/// Represents a record in the <c>Country</c> table.
	/// </summary>
	[Serializable]
	public class CountryRow : CountryRow_Base {
		/// <summary>
		/// Initializes a new instance of the <see cref="CountryRow"/> class.
		/// </summary>
		public CountryRow(){
			// EMPTY
		}

		public bool IsNameChanged(CountryRow pOriginal) {
			return Name != pOriginal.Name;
		}

		public override bool Equals(object obj) {
			return base.Equals(obj);
		}

		public override int GetHashCode() {
			return base.GetHashCode ();
		}

	} // End of CountryRow class
} // End of namespace

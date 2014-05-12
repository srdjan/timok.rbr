// <fileinfo name="CDRRow.cs">
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
using System.Collections.Generic;
using System.Text;
using Timok.Rbr.DAL.CdrDatabase.Base;

namespace Timok.Rbr.DAL.CdrDatabase {
	/// <summary>
	/// Represents a record in the <c>CDR</c> table.
	/// </summary>
	[Serializable]
	public class CDRRow : CDRRow_Base {
		/// <summary>
		/// Initializes a new instance of the <see cref="CDRRow"/> class.
		/// </summary>
		public CDRRow() {
			// EMPTY
		}

		public new string Id {
			get { return base.Id; }
			set { base.Id = StripSpaces(value); }
		}

		public static string StripSpaces(IEnumerable<char> pValue) {
			StringBuilder builder = new StringBuilder();
			foreach (char _c in pValue) {
				if ( ! char.IsWhiteSpace(_c)) {
					builder.Append(_c);
				}
			}
			return builder.ToString();
		}
		
	}// End of CDRRow class
} // End of namespace

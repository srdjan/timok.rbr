// <fileinfo name="CdrExportMapDetailRow.cs">
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

namespace Timok.Rbr.DAL.RbrDatabase {
	/// <summary>
	/// Represents a record in the <c>CdrExportMapDetail</c> table.
	/// </summary>
	[Serializable]
	public class CdrExportMapDetailRow : CdrExportMapDetailRow_Base {
		public bool IsDateTimeField {
			get {
				if (Field_name == "start") {
					return true;
				}
				else if (Field_name == "date_logged") {
					return true;
				}
				else {
					return false;
				}
			}
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="CdrExportMapDetailRow"/> class.
		/// </summary>
		public CdrExportMapDetailRow() {
			// EMPTY
		}

		public override string ToString() {
			if (Format_type != null && Format_type.Trim().Length > 0) {
				return Field_name + "  [" + Format_type + "]";
			}
			else {
				return Field_name;
			}
		}

		public override bool Equals(object obj) {
			return base.Equals(obj);
		}

		public override int GetHashCode() {
			return base.GetHashCode();
		}
	} // End of CdrExportMapDetailRow class
} // End of namespace
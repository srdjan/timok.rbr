// <fileinfo name="CdrExportMapRow.cs">
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
using System.Xml.Serialization;
using Timok.Rbr.DAL.RbrDatabase.Base;
using Timok.Rbr.Core.Config;

namespace Timok.Rbr.DAL.RbrDatabase {
	/// <summary>
	/// Represents a record in the <c>CdrExportMap</c> table.
	/// </summary>
	[Serializable]
	public class CdrExportMapRow : CdrExportMapRow_Base {

    //public const string CdrExportDelimeter_PropName = "CdrExportDelimeter";
    //public const string CdrExportDelimeter_DisplayName = "Delimeter";

		[XmlIgnore]
		public CdrExportDelimeter CdrExportDelimeter {
			get { return (CdrExportDelimeter) this.Delimiter; }
			set { this.Delimiter = (byte) value; }
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="CdrExportMapRow"/> class.
		/// </summary>
		public CdrExportMapRow(){
			// EMPTY
		}

		public override bool Equals(object obj) {
			return base.Equals(obj);
		}

		public override int GetHashCode() {
			return base.GetHashCode ();
		}

		public override string ToString() {
			return this.Name + "  [" + this.CdrExportDelimeter.ToString() + " - delimited]";
		}


	} // End of CdrExportMapRow class
} // End of namespace

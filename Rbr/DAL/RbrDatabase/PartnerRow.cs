// <fileinfo name="PartnerRow.cs">
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
using Timok.Rbr.Core.Config;
using Timok.Rbr.DAL.RbrDatabase.Base;
using System.Runtime.Serialization;
using System.Xml.Serialization;

namespace Timok.Rbr.DAL.RbrDatabase {
	/// <summary>
	/// Represents a record in the <c>Partner</c> table.
	/// </summary>
	[Serializable]
	public class PartnerRow : PartnerRow_Base {
		public const string AccountStatus_PropName = "AccountStatus";
		public const string AccountStatus_DisplayName = "Status";

		[XmlIgnore]
		public Status AccountStatus {
			get { return (Status)this.Status; }
			set { this.Status = (byte) value; }
		}

    //[XmlIgnore]
    //public bool IsWebAccessEnabled {
    //  get { return ! this.IsWeb_access_info_idNull; }
    //}

		/// <summary>
		/// Initializes a new instance of the <see cref="PartnerRow"/> class.
		/// </summary>
		public PartnerRow(){
			// EMPTY
		}

		public override bool Equals(object obj) {
			return base.Equals(obj);
		}

		public override int GetHashCode() {
			return base.GetHashCode ();
		}

		public override string ToString() {
			System.Text.StringBuilder _sb = new System.Text.StringBuilder();
			_sb.Append(this.Name);
			//			_sb.Append(" [");
			//			_sb.Append((AccountStatus)this.Status);
			//			_sb.Append("]");

			return _sb.ToString();
		}

	} // End of PartnerRow class
} // End of namespace

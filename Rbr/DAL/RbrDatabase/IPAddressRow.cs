// <fileinfo name="IPAddressRow.cs">
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
using Timok.NetworkLib;

namespace Timok.Rbr.DAL.RbrDatabase {
	/// <summary>
	/// Represents a record in the <c>IPAddress</c> table.
	/// </summary>
	[Serializable]
	public class IPAddressRow : IPAddressRow_Base {
		[XmlIgnore]
		public string DottedIPAddress {
			get {return IPUtil.ToString(this.IP_address);}
			set {
				IP_address = IPUtil.ToInt32(value);
			}
		}

		//[XmlIgnore]
		public const string _lastByte_PropName = "_lastByte";
		[XmlIgnore]
		public byte _lastByte {
			get {
				if(!string.IsNullOrEmpty(this.DottedIPAddress)){
					return byte.Parse(this.DottedIPAddress.Split('.')[3]);
				}
				return 0;
			}
		}
		
		/// <summary>
		/// Initializes a new instance of the <see cref="IPAddressRow"/> class.
		/// </summary>
		public IPAddressRow(){
			// EMPTY
		}

		public override string ToString() {
			var _sb = new System.Text.StringBuilder();
			_sb.Append(this.DottedIPAddress);
			_sb.Append(" [EP");
			_sb.Append(this.End_point_id);
			_sb.Append("]");

			return _sb.ToString();
		}

		public override bool Equals(object obj) {
			return base.Equals(obj);
		}

		public override int GetHashCode() {
			return base.GetHashCode ();
		}

	} // End of IPAddressRow class
} // End of namespace

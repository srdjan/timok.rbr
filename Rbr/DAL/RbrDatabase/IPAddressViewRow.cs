// <fileinfo name="IPAddressViewRow.cs">
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
using Timok.NetworkLib;

namespace Timok.Rbr.DAL.RbrDatabase {
	/// <summary>
	/// Represents a record in the <c>IPAddressView</c> view.
	/// </summary>
	[Serializable]
	public class IPAddressViewRow : IPAddressViewRow_Base {
		[XmlIgnore]
		public string DottedIPAddress {
			get { return IPUtil.ToString(this.IP_address); }
			set {
				this.IP_address = IPUtil.ToInt32(value);
			}
		}

		public bool WithPrefixes {
			get {
				return (this.Prefix_length > 0 || this.Prefix_delimiter > 0);
			}
		}

		[XmlIgnore]
		public EndPointType EndPointType {
			get { return (EndPointType) this.Type; }
			set { this.Type = (byte) value; }
		}

		[XmlIgnore]
		public bool RequiredRegistration {
			get { return (this.Registration == (byte) EPRegistration.Required); }
			set {
				if (value == true) {
					this.Registration = (byte) EPRegistration.Required; 
				}
				else {
					this.Registration = (byte) EPRegistration.Permanent; 
				}
			}
		}

		public Status EPStatus {
			get {return (Status) this.Status;}
		}

		[XmlIgnore]
		public bool IsRegistered {
			get { return this.Is_registered > 0; }
		}

		[XmlIgnore]
		public bool WithAliasAuthentication {
			get { return this.With_alias_authentication > 0; }
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="IPAddressViewRow"/> class.
		/// </summary>
		public IPAddressViewRow(){
			// EMPTY
		}

		public override bool Equals(object obj) {
			return base.Equals(obj);
		}

		public override int GetHashCode() {
			return base.GetHashCode ();
		}

		public IPAddressRow ToIPAddressRow(){
			IPAddressRow _ipAddressRow = new IPAddressRow();

			_ipAddressRow.IP_address = this.IP_address;
			_ipAddressRow.End_point_id = this.End_point_id;

			return _ipAddressRow;
		}

		public override string ToString() {
			System.Text.StringBuilder _sb = new System.Text.StringBuilder();
			_sb.Append(this.Dot_IP_address);
			_sb.Append(" [");
			_sb.Append(this.Alias);
			_sb.Append("]");
			_sb.Append(" [EP");
			_sb.Append(this.End_point_id);
			_sb.Append("]");

			return _sb.ToString();
		}

	} // End of IPAddressViewRow class
} // End of namespace

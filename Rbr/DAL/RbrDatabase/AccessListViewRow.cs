// <fileinfo name="AccessListViewRow.cs">
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
using Timok.Core;
using Timok.Rbr.Core.Config;

namespace Timok.Rbr.DAL.RbrDatabase {
	/// <summary>
	/// Represents a record in the <c>AccessListView</c> view.
	/// </summary>
	[Serializable]
	public class AccessListViewRow : AccessListViewRow_Base {

		[XmlIgnore]
		public bool WithPrefixes {
			get { return this.Prefix_in_type_id > 0; }
		}

		[XmlIgnore]
		public bool IsRegistered {
			get { return this.Is_registered > 0; }
		}

		[XmlIgnore]
		public bool WithAliasAuthentication {
			get { return this.With_alias_authentication > 0; }
		}

		[XmlIgnore]
		public Status TGStatus {
			get {return (Status)this.Status;}
		}

//		public const string IPAddressView_PropName = "IPAddressView";
//		[XmlIgnore]
//		public string IPAddressView {
//			get {
//				IPAddressRow[] _IPAddresses = null;
//				string _ipAddressView = string.Empty;
//
//				using (Rbr_Db db = new Rbr_Db()){
//					_IPAddresses = db.IPAddressCollection.GetByEnd_point_id(this.End_point_id);
//				}
//
//				if(_IPAddresses != null && _IPAddresses.Length > 0){
//					if(_IPAddresses.Length == 1){
//						_ipAddressView = _IPAddresses[0].DottedIPAddress;
//					}
//					else {
//						Array.Sort(_IPAddresses, 
//							new ObjectComparer(IPAddressRow._lastByte_PropName, false));
//
//						string _ip = _IPAddresses[0].DottedIPAddress;
//						//byte _first = _IPAddresses[0]._lastByte;
//						byte _last = _IPAddresses[_IPAddresses.Length-1]._lastByte;
//
//						_ipAddressView = _ip + "-" + _last;
//					}
//				}
//				return _ipAddressView;
//			}
//		}
		
		/// <summary>
		/// Initializes a new instance of the <see cref="AccessListViewRow"/> class.
		/// </summary>
		public AccessListViewRow(){
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
			_sb.Append("[EP");
			_sb.Append(this.End_point_id);
			_sb.Append("] ");
			_sb.Append(this.Alias);

//			_sb.Append(this.Alias);
//			_sb.Append(" [TG");
//			_sb.Append(this.Trunk_group_id);
//			_sb.Append("]");
			return _sb.ToString();
		}

	} // End of AccessListViewRow class
} // End of namespace

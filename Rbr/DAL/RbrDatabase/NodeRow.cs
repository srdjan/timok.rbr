// <fileinfo name="NodeRow.cs">
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
using Timok.NetworkLib;
using Timok.Rbr.Core.Config;
using Timok.Rbr.DAL.RbrDatabase.Base;
using System.Runtime.Serialization;
using System.Xml.Serialization;

namespace Timok.Rbr.DAL.RbrDatabase {

	/// <summary>
	/// Represents a record in the <c>Node</c> table.
	/// </summary>
	[Serializable]
	public class NodeRow : NodeRow_Base {
		public const string DottedIPAddress_PropName = "DottedIPAddress";
		public const string DottedIPAddress_DisplayName = "IP Address";
		public const string NodeConfiguration_PropName = "NodeConfiguration";
		public const string NodeConfiguration_DisplayName = "Node Config";
		public const string IsGuiHost_PropName = "IsGuiHost";
		public const string IsGuiHost_DisplayName = "Gui Host";
						
		[XmlIgnore]
		public string DottedIPAddress {
			get { return IPUtil.ToString(this.Ip_address); }
			set { this.Ip_address = IPUtil.ToInt32(value); }
		}

		[XmlIgnore]
		public NodeRole NodeRole {
			get { return (NodeRole) this.Node_config; }
			set { this.Node_config = (int) value; }
		}

		//[XmlIgnore]
		//public bool IsGuiHost {
		//  get { return (this.NodeRole == NodeRole.Admin || this.NodeRole == NodeRole.Standalone); }
		//}

		/// <summary>
		/// Initializes a new instance of the <see cref="NodeRow"/> class.
		/// </summary>
		public NodeRow(){
			// EMPTY
		}

		public override string ToString() {
			return this.Description;
		}

		public override bool Equals(object obj) {
			return base.Equals(obj);
		}

		public override int GetHashCode() {
			return base.GetHashCode ();
		}

	} // End of NodeRow class
} // End of namespace

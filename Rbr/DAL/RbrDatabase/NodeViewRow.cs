// <fileinfo name="NodeViewRow.cs">
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
using System.Xml.Serialization;
using Timok.Rbr.Core.Config;

namespace Timok.Rbr.DAL.RbrDatabase {
	/// <summary>
	/// Represents a record in the <c>NodeView</c> view.
	/// </summary>
	[Serializable]
	public class NodeViewRow : NodeViewRow_Base {
		public const string NodeStatus_PropName = "NodeStatus";
		public const string NodeStatus_DisplayName = "Status";

		public const string NodeConfiguration_PropName = "NodeConfiguration";
		public const string NodeConfiguration_DisplayName = "Node Config";
		public const string IsGuiHost_PropName = "IsGuiHost";
		public const string IsGuiHost_DisplayName = "Gui Host";
		public const string PlatformConfig_PropName = "PlatformConfiguration";
		public const string PlatformConfig_DisplayName = "Platform Config";


		[XmlIgnore]
		public NodeRole NodeConfiguration {
			get { return (NodeRole) this.Node_config; }
		}

		[XmlIgnore]
		public bool IsGuiHost {
			get { return (this.NodeConfiguration == NodeRole.Admin || this.PlatformConfiguration == PlatformConfig.Standalone); }
		}

		[XmlIgnore]
		public PlatformConfig PlatformConfiguration {
			get { return (PlatformConfig) this.Platform_config; }
		}

		[XmlIgnore]
		public Status NodeStatus {
			get { return (Status) this.Node_status; }
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="NodeViewRow"/> class.
		/// </summary>
		public NodeViewRow(){
			// EMPTY
		}

		public NodeRow ToNodeRow(){
			NodeRow _nodeRow = new NodeRow();

			_nodeRow.Node_id = this.Node_id;
			_nodeRow.Platform_id = this.Platform_id;

			if ( this.Description != null ) {
				_nodeRow.Description = (string)this.Description.Clone();
			}
			_nodeRow.Node_config = this.Node_config;
			_nodeRow.Transport_type = this.Transport_type;
			_nodeRow.User_name = this.User_name;
			_nodeRow.Password = this.Password;
			_nodeRow.Ip_address = this.Ip_address;
			_nodeRow.Port = this.Port;
			_nodeRow.Status = this.Node_status;
			_nodeRow.Billing_export_frequency = this.Billing_export_frequency;
			_nodeRow.Cdr_publishing_frequency = this.Cdr_publishing_frequency;

			return _nodeRow;
		}

		public override bool Equals(object obj) {
			return base.Equals(obj);
		}

		public override int GetHashCode() {
			return base.GetHashCode ();
		}

	} // End of NodeViewRow class
} // End of namespace

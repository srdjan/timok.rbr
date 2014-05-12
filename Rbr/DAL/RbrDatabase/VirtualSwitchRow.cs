// <fileinfo name="VirtualSwitchRow.cs">
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
using Timok.Rbr.Core.Config;

namespace Timok.Rbr.DAL.RbrDatabase {
	/// <summary>
	/// Represents a record in the <c>VirtualSwitch</c> table.
	/// </summary>
	[Serializable]
	public class VirtualSwitchRow : VirtualSwitchRow_Base {

    public Status SwitchStatus {
      get { return (Status) this.Status; }
      set { this.Status = (byte) value; }
    }
    
    /// <summary>
		/// Initializes a new instance of the <see cref="VirtualSwitchRow"/> class.
		/// </summary>
		public VirtualSwitchRow(){
			// EMPTY
		}

		public override bool Equals(object obj) {
			return base.Equals(obj);
		}

		public override int GetHashCode() {
			return base.GetHashCode ();
		}

	} // End of VirtualSwitchRow class
} // End of namespace

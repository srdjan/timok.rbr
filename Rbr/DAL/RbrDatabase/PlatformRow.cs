// <fileinfo name="PlatformRow.cs">
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
	/// Represents a record in the <c>Platform</c> table.
	/// </summary>
	[Serializable]
	public class PlatformRow : PlatformRow_Base {
		public const string PlatformConfig_PropName = "PlatformConfiguration";
		public const string PlatformConfig_DisplayName = "Platform Config";

		[XmlIgnore]
		public PlatformConfig PlatformConfiguration {
			get { return (PlatformConfig) this.Platform_config; }
			set { this.Platform_config = (int) value; }
		}
		
		/// <summary>
		/// Initializes a new instance of the <see cref="PlatformRow"/> class.
		/// </summary>
		public PlatformRow(){
			// EMPTY
		}

		public override bool Equals(object obj) {
			return base.Equals(obj);
		}

		public override int GetHashCode() {
			return base.GetHashCode ();
		}

	} // End of PlatformRow class
} // End of namespace

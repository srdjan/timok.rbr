// <fileinfo name="LoadBalancingMapRow.cs">
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

namespace Timok.Rbr.DAL.RbrDatabase {
	/// <summary>
	/// Represents a record in the <c>LoadBalancingMap</c> table.
	/// </summary>
	[Serializable]
	public class LoadBalancingMapRow : LoadBalancingMapRow_Base {
		/// <summary>
		/// Initializes a new instance of the <see cref="LoadBalancingMapRow"/> class.
		/// </summary>
		public LoadBalancingMapRow(){
			// EMPTY
		}

		public override bool Equals(object obj) {
			return base.Equals(obj);
		}

		public override int GetHashCode() {
			return base.GetHashCode ();
		}

	} // End of LoadBalancingMapRow class
} // End of namespace

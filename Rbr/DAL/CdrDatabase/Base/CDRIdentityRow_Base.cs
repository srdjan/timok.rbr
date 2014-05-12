// <fileinfo name="Base\CDRIdentityRow_Base.cs">
//		<copyright>
//			Copyright © 2002-2006 Timok ES LLC. All rights reserved.
//		</copyright>
//		<remarks>
//			Do not change this source code manually. Changes to this file may 
//			cause incorrect behavior and will be lost if the code is regenerated.
//		</remarks>
//		<generator rewritefile="True" infourl="http://www.SharpPower.com">RapTier</generator>
// </fileinfo>

using System;
using System.Runtime.Serialization;
using Timok.Core;

namespace Timok.Rbr.DAL.CdrDatabase.Base
{
	/// <summary>
	/// The base class for <see cref="CDRIdentityRow"/> that 
	/// represents a record in the <c>CDRIdentity</c> table.
	/// </summary>
	/// <remarks>
	/// Do not change this source code manually. Update the <see cref="CDRIdentityRow"/>
	/// class if you need to add or change some functionality.
	/// </remarks>
	[Serializable]
	public abstract class CDRIdentityRow_Base
	{
	#region Timok Custom

		//db field names
		public const string id_DbName = "id";

		//prop names
		public const string id_PropName = "Id";

		//db field display names
		public const string id_DisplayName = "id";
	#endregion Timok Custom


		private string _id;

		/// <summary>
		/// Initializes a new instance of the <see cref="CDRIdentityRow_Base"/> class.
		/// </summary>
		public CDRIdentityRow_Base()
		{
			// EMPTY
		}

		/// <summary>
		/// Gets or sets the <c>id</c> column value.
		/// </summary>
		/// <value>The <c>id</c> column value.</value>
		public string Id
		{
			get { return _id; }
			set { _id = value; }
		}

		/// <summary>
		/// Returns the string representation of this instance.
		/// </summary>
		/// <returns>The string representation of this instance.</returns>
		public override string ToString()
		{
			System.Text.StringBuilder dynStr = new System.Text.StringBuilder(GetType().Name);
			dynStr.Append(':');
			dynStr.Append("  Id=");
			dynStr.Append(Id);
			return dynStr.ToString();
		}

	#region Timok Custom

		public override bool Equals(object obj) {
			if(obj == null || obj.GetType() != this.GetType())
				return false;

			return ObjectComparer.AreEqual(this, obj);
		}

		public override int GetHashCode() {
			//TODO: finish it, get hashes for all fields
			return this.ToString().GetHashCode();
		}
	#endregion Timok Custom

	} // End of CDRIdentityRow_Base class
} // End of namespace

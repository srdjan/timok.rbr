// <fileinfo name="Base\PrefixInTypeRow_Base.cs">
//		<copyright>
//			Copyright © 2002-2007 Timok ES LLC. All rights reserved.
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

namespace Timok.Rbr.DAL.RbrDatabase.Base
{
	/// <summary>
	/// The base class for <see cref="PrefixInTypeRow"/> that 
	/// represents a record in the <c>PrefixInType</c> table.
	/// </summary>
	/// <remarks>
	/// Do not change this source code manually. Update the <see cref="PrefixInTypeRow"/>
	/// class if you need to add or change some functionality.
	/// </remarks>
	[Serializable]
	public abstract class PrefixInTypeRow_Base
	{
	#region Timok Custom

		//db field names
		public const string prefix_in_type_id_DbName = "prefix_in_type_id";
		public const string description_DbName = "description";
		public const string length_DbName = "length";
		public const string delimiter_DbName = "delimiter";

		//prop names
		public const string prefix_in_type_id_PropName = "Prefix_in_type_id";
		public const string description_PropName = "Description";
		public const string length_PropName = "Length";
		public const string delimiter_PropName = "Delimiter";

		//db field display names
		public const string prefix_in_type_id_DisplayName = "prefix in type id";
		public const string description_DisplayName = "description";
		public const string length_DisplayName = "length";
		public const string delimiter_DisplayName = "delimiter";
	#endregion Timok Custom


		private short _prefix_in_type_id;
		private string _description;
		private byte _length;
		private byte _delimiter;

		/// <summary>
		/// Initializes a new instance of the <see cref="PrefixInTypeRow_Base"/> class.
		/// </summary>
		public PrefixInTypeRow_Base()
		{
			// EMPTY
		}

		/// <summary>
		/// Gets or sets the <c>prefix_in_type_id</c> column value.
		/// </summary>
		/// <value>The <c>prefix_in_type_id</c> column value.</value>
		public short Prefix_in_type_id
		{
			get { return _prefix_in_type_id; }
			set { _prefix_in_type_id = value; }
		}

		/// <summary>
		/// Gets or sets the <c>description</c> column value.
		/// </summary>
		/// <value>The <c>description</c> column value.</value>
		public string Description
		{
			get { return _description; }
			set { _description = value; }
		}

		/// <summary>
		/// Gets or sets the <c>length</c> column value.
		/// </summary>
		/// <value>The <c>length</c> column value.</value>
		public byte Length
		{
			get { return _length; }
			set { _length = value; }
		}

		/// <summary>
		/// Gets or sets the <c>delimiter</c> column value.
		/// </summary>
		/// <value>The <c>delimiter</c> column value.</value>
		public byte Delimiter
		{
			get { return _delimiter; }
			set { _delimiter = value; }
		}

		/// <summary>
		/// Returns the string representation of this instance.
		/// </summary>
		/// <returns>The string representation of this instance.</returns>
		public override string ToString()
		{
			System.Text.StringBuilder dynStr = new System.Text.StringBuilder(GetType().Name);
			dynStr.Append(':');
			dynStr.Append("  Prefix_in_type_id=");
			dynStr.Append(Prefix_in_type_id);
			dynStr.Append("  Description=");
			dynStr.Append(Description);
			dynStr.Append("  Length=");
			dynStr.Append(Length);
			dynStr.Append("  Delimiter=");
			dynStr.Append(Delimiter);
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

	} // End of PrefixInTypeRow_Base class
} // End of namespace

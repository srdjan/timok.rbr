// <fileinfo name="Base\TableSequenceRow_Base.cs">
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
	/// The base class for <see cref="TableSequenceRow"/> that 
	/// represents a record in the <c>TableSequence</c> table.
	/// </summary>
	/// <remarks>
	/// Do not change this source code manually. Update the <see cref="TableSequenceRow"/>
	/// class if you need to add or change some functionality.
	/// </remarks>
	[Serializable]
	public abstract class TableSequenceRow_Base
	{
	#region Timok Custom

		//db field names
		public const string TableName_DbName = "TableName";
		public const string Value_DbName = "Value";

		//prop names
		public const string TableName_PropName = "TableName";
		public const string Value_PropName = "Value";

		//db field display names
		public const string TableName_DisplayName = "TableName";
		public const string Value_DisplayName = "Value";
	#endregion Timok Custom


		private string _tableName;
		private long _value;

		/// <summary>
		/// Initializes a new instance of the <see cref="TableSequenceRow_Base"/> class.
		/// </summary>
		public TableSequenceRow_Base()
		{
			// EMPTY
		}

		/// <summary>
		/// Gets or sets the <c>TableName</c> column value.
		/// </summary>
		/// <value>The <c>TableName</c> column value.</value>
		public string TableName
		{
			get { return _tableName; }
			set { _tableName = value; }
		}

		/// <summary>
		/// Gets or sets the <c>Value</c> column value.
		/// </summary>
		/// <value>The <c>Value</c> column value.</value>
		public long Value
		{
			get { return _value; }
			set { _value = value; }
		}

		/// <summary>
		/// Returns the string representation of this instance.
		/// </summary>
		/// <returns>The string representation of this instance.</returns>
		public override string ToString()
		{
			System.Text.StringBuilder dynStr = new System.Text.StringBuilder(GetType().Name);
			dynStr.Append(':');
			dynStr.Append("  TableName=");
			dynStr.Append(TableName);
			dynStr.Append("  Value=");
			dynStr.Append(Value);
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

	} // End of TableSequenceRow_Base class
} // End of namespace

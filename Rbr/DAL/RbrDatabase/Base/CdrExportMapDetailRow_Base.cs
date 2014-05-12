// <fileinfo name="Base\CdrExportMapDetailRow_Base.cs">
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
	/// The base class for <see cref="CdrExportMapDetailRow"/> that 
	/// represents a record in the <c>CdrExportMapDetail</c> table.
	/// </summary>
	/// <remarks>
	/// Do not change this source code manually. Update the <see cref="CdrExportMapDetailRow"/>
	/// class if you need to add or change some functionality.
	/// </remarks>
	[Serializable]
	public abstract class CdrExportMapDetailRow_Base
	{
	#region Timok Custom

		//db field names
		public const string map_detail_id_DbName = "map_detail_id";
		public const string map_id_DbName = "map_id";
		public const string sequence_DbName = "sequence";
		public const string field_name_DbName = "field_name";
		public const string format_type_DbName = "format_type";

		//prop names
		public const string map_detail_id_PropName = "Map_detail_id";
		public const string map_id_PropName = "Map_id";
		public const string sequence_PropName = "Sequence";
		public const string field_name_PropName = "Field_name";
		public const string format_type_PropName = "Format_type";

		//db field display names
		public const string map_detail_id_DisplayName = "map detail id";
		public const string map_id_DisplayName = "map id";
		public const string sequence_DisplayName = "sequence";
		public const string field_name_DisplayName = "field name";
		public const string format_type_DisplayName = "format type";
	#endregion Timok Custom


		private int _map_detail_id;
		private int _map_id;
		private int _sequence;
		private string _field_name;
		private string _format_type;

		/// <summary>
		/// Initializes a new instance of the <see cref="CdrExportMapDetailRow_Base"/> class.
		/// </summary>
		public CdrExportMapDetailRow_Base()
		{
			// EMPTY
		}

		/// <summary>
		/// Gets or sets the <c>map_detail_id</c> column value.
		/// </summary>
		/// <value>The <c>map_detail_id</c> column value.</value>
		public int Map_detail_id
		{
			get { return _map_detail_id; }
			set { _map_detail_id = value; }
		}

		/// <summary>
		/// Gets or sets the <c>map_id</c> column value.
		/// </summary>
		/// <value>The <c>map_id</c> column value.</value>
		public int Map_id
		{
			get { return _map_id; }
			set { _map_id = value; }
		}

		/// <summary>
		/// Gets or sets the <c>sequence</c> column value.
		/// </summary>
		/// <value>The <c>sequence</c> column value.</value>
		public int Sequence
		{
			get { return _sequence; }
			set { _sequence = value; }
		}

		/// <summary>
		/// Gets or sets the <c>field_name</c> column value.
		/// </summary>
		/// <value>The <c>field_name</c> column value.</value>
		public string Field_name
		{
			get { return _field_name; }
			set { _field_name = value; }
		}

		/// <summary>
		/// Gets or sets the <c>format_type</c> column value.
		/// This column is nullable.
		/// </summary>
		/// <value>The <c>format_type</c> column value.</value>
		public string Format_type
		{
			get { return _format_type; }
			set { _format_type = value; }
		}

		/// <summary>
		/// Returns the string representation of this instance.
		/// </summary>
		/// <returns>The string representation of this instance.</returns>
		public override string ToString()
		{
			System.Text.StringBuilder dynStr = new System.Text.StringBuilder(GetType().Name);
			dynStr.Append(':');
			dynStr.Append("  Map_detail_id=");
			dynStr.Append(Map_detail_id);
			dynStr.Append("  Map_id=");
			dynStr.Append(Map_id);
			dynStr.Append("  Sequence=");
			dynStr.Append(Sequence);
			dynStr.Append("  Field_name=");
			dynStr.Append(Field_name);
			dynStr.Append("  Format_type=");
			dynStr.Append(Format_type);
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

	} // End of CdrExportMapDetailRow_Base class
} // End of namespace

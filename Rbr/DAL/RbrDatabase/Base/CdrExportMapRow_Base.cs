// <fileinfo name="Base\CdrExportMapRow_Base.cs">
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
	/// The base class for <see cref="CdrExportMapRow"/> that 
	/// represents a record in the <c>CdrExportMap</c> table.
	/// </summary>
	/// <remarks>
	/// Do not change this source code manually. Update the <see cref="CdrExportMapRow"/>
	/// class if you need to add or change some functionality.
	/// </remarks>
	[Serializable]
	public abstract class CdrExportMapRow_Base
	{
	#region Timok Custom

		//db field names
		public const string map_id_DbName = "map_id";
		public const string name_DbName = "name";
		public const string delimiter_DbName = "delimiter";
		public const string target_dest_folder_DbName = "target_dest_folder";

		//prop names
		public const string map_id_PropName = "Map_id";
		public const string name_PropName = "Name";
		public const string delimiter_PropName = "Delimiter";
		public const string target_dest_folder_PropName = "Target_dest_folder";

		//db field display names
		public const string map_id_DisplayName = "map id";
		public const string name_DisplayName = "name";
		public const string delimiter_DisplayName = "delimiter";
		public const string target_dest_folder_DisplayName = "target dest folder";
	#endregion Timok Custom


		private int _map_id;
		private string _name;
		private byte _delimiter;
		private string _target_dest_folder;

		/// <summary>
		/// Initializes a new instance of the <see cref="CdrExportMapRow_Base"/> class.
		/// </summary>
		public CdrExportMapRow_Base()
		{
			// EMPTY
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
		/// Gets or sets the <c>name</c> column value.
		/// </summary>
		/// <value>The <c>name</c> column value.</value>
		public string Name
		{
			get { return _name; }
			set { _name = value; }
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
		/// Gets or sets the <c>target_dest_folder</c> column value.
		/// </summary>
		/// <value>The <c>target_dest_folder</c> column value.</value>
		public string Target_dest_folder
		{
			get { return _target_dest_folder; }
			set { _target_dest_folder = value; }
		}

		/// <summary>
		/// Returns the string representation of this instance.
		/// </summary>
		/// <returns>The string representation of this instance.</returns>
		public override string ToString()
		{
			System.Text.StringBuilder dynStr = new System.Text.StringBuilder(GetType().Name);
			dynStr.Append(':');
			dynStr.Append("  Map_id=");
			dynStr.Append(Map_id);
			dynStr.Append("  Name=");
			dynStr.Append(Name);
			dynStr.Append("  Delimiter=");
			dynStr.Append(Delimiter);
			dynStr.Append("  Target_dest_folder=");
			dynStr.Append(Target_dest_folder);
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

	} // End of CdrExportMapRow_Base class
} // End of namespace

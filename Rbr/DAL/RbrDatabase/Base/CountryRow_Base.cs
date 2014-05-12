// <fileinfo name="Base\CountryRow_Base.cs">
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
	/// The base class for <see cref="CountryRow"/> that 
	/// represents a record in the <c>Country</c> table.
	/// </summary>
	/// <remarks>
	/// Do not change this source code manually. Update the <see cref="CountryRow"/>
	/// class if you need to add or change some functionality.
	/// </remarks>
	[Serializable]
	public abstract class CountryRow_Base
	{
	#region Timok Custom

		//db field names
		public const string country_id_DbName = "country_id";
		public const string name_DbName = "name";
		public const string country_code_DbName = "country_code";
		public const string status_DbName = "status";
		public const string version_DbName = "version";

		//prop names
		public const string country_id_PropName = "Country_id";
		public const string name_PropName = "Name";
		public const string country_code_PropName = "Country_code";
		public const string status_PropName = "Status";
		public const string version_PropName = "Version";

		//db field display names
		public const string country_id_DisplayName = "country id";
		public const string name_DisplayName = "name";
		public const string country_code_DisplayName = "country code";
		public const string status_DisplayName = "status";
		public const string version_DisplayName = "version";
	#endregion Timok Custom


		private int _country_id;
		private string _name;
		private int _country_code;
		private byte _status;
		private int _version;

		/// <summary>
		/// Initializes a new instance of the <see cref="CountryRow_Base"/> class.
		/// </summary>
		public CountryRow_Base()
		{
			// EMPTY
		}

		/// <summary>
		/// Gets or sets the <c>country_id</c> column value.
		/// </summary>
		/// <value>The <c>country_id</c> column value.</value>
		public int Country_id
		{
			get { return _country_id; }
			set { _country_id = value; }
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
		/// Gets or sets the <c>country_code</c> column value.
		/// </summary>
		/// <value>The <c>country_code</c> column value.</value>
		public int Country_code
		{
			get { return _country_code; }
			set { _country_code = value; }
		}

		/// <summary>
		/// Gets or sets the <c>status</c> column value.
		/// </summary>
		/// <value>The <c>status</c> column value.</value>
		public byte Status
		{
			get { return _status; }
			set { _status = value; }
		}

		/// <summary>
		/// Gets or sets the <c>version</c> column value.
		/// </summary>
		/// <value>The <c>version</c> column value.</value>
		public int Version
		{
			get { return _version; }
			set { _version = value; }
		}

		/// <summary>
		/// Returns the string representation of this instance.
		/// </summary>
		/// <returns>The string representation of this instance.</returns>
		public override string ToString()
		{
			System.Text.StringBuilder dynStr = new System.Text.StringBuilder(GetType().Name);
			dynStr.Append(':');
			dynStr.Append("  Country_id=");
			dynStr.Append(Country_id);
			dynStr.Append("  Name=");
			dynStr.Append(Name);
			dynStr.Append("  Country_code=");
			dynStr.Append(Country_code);
			dynStr.Append("  Status=");
			dynStr.Append(Status);
			dynStr.Append("  Version=");
			dynStr.Append(Version);
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

	} // End of CountryRow_Base class
} // End of namespace

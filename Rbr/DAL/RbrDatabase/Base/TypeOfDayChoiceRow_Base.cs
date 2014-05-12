// <fileinfo name="Base\TypeOfDayChoiceRow_Base.cs">
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
	/// The base class for <see cref="TypeOfDayChoiceRow"/> that 
	/// represents a record in the <c>TypeOfDayChoice</c> table.
	/// </summary>
	/// <remarks>
	/// Do not change this source code manually. Update the <see cref="TypeOfDayChoiceRow"/>
	/// class if you need to add or change some functionality.
	/// </remarks>
	[Serializable]
	public abstract class TypeOfDayChoiceRow_Base
	{
	#region Timok Custom

		//db field names
		public const string type_of_day_choice_DbName = "type_of_day_choice";
		public const string name_DbName = "name";

		//prop names
		public const string type_of_day_choice_PropName = "Type_of_day_choice";
		public const string name_PropName = "Name";

		//db field display names
		public const string type_of_day_choice_DisplayName = "type of day choice";
		public const string name_DisplayName = "name";
	#endregion Timok Custom


		private byte _type_of_day_choice;
		private string _name;

		/// <summary>
		/// Initializes a new instance of the <see cref="TypeOfDayChoiceRow_Base"/> class.
		/// </summary>
		public TypeOfDayChoiceRow_Base()
		{
			// EMPTY
		}

		/// <summary>
		/// Gets or sets the <c>type_of_day_choice</c> column value.
		/// </summary>
		/// <value>The <c>type_of_day_choice</c> column value.</value>
		public byte Type_of_day_choice
		{
			get { return _type_of_day_choice; }
			set { _type_of_day_choice = value; }
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
		/// Returns the string representation of this instance.
		/// </summary>
		/// <returns>The string representation of this instance.</returns>
		public override string ToString()
		{
			System.Text.StringBuilder dynStr = new System.Text.StringBuilder(GetType().Name);
			dynStr.Append(':');
			dynStr.Append("  Type_of_day_choice=");
			dynStr.Append(Type_of_day_choice);
			dynStr.Append("  Name=");
			dynStr.Append(Name);
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

	} // End of TypeOfDayChoiceRow_Base class
} // End of namespace

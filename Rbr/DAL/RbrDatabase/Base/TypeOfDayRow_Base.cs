// <fileinfo name="Base\TypeOfDayRow_Base.cs">
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
	/// The base class for <see cref="TypeOfDayRow"/> that 
	/// represents a record in the <c>TypeOfDay</c> table.
	/// </summary>
	/// <remarks>
	/// Do not change this source code manually. Update the <see cref="TypeOfDayRow"/>
	/// class if you need to add or change some functionality.
	/// </remarks>
	[Serializable]
	public abstract class TypeOfDayRow_Base
	{
	#region Timok Custom

		//db field names
		public const string rate_info_id_DbName = "rate_info_id";
		public const string type_of_day_choice_DbName = "type_of_day_choice";
		public const string time_of_day_policy_DbName = "time_of_day_policy";

		//prop names
		public const string rate_info_id_PropName = "Rate_info_id";
		public const string type_of_day_choice_PropName = "Type_of_day_choice";
		public const string time_of_day_policy_PropName = "Time_of_day_policy";

		//db field display names
		public const string rate_info_id_DisplayName = "rate info id";
		public const string type_of_day_choice_DisplayName = "type of day choice";
		public const string time_of_day_policy_DisplayName = "time of day policy";
	#endregion Timok Custom


		private int _rate_info_id;
		private byte _type_of_day_choice;
		private byte _time_of_day_policy;

		/// <summary>
		/// Initializes a new instance of the <see cref="TypeOfDayRow_Base"/> class.
		/// </summary>
		public TypeOfDayRow_Base()
		{
			// EMPTY
		}

		/// <summary>
		/// Gets or sets the <c>rate_info_id</c> column value.
		/// </summary>
		/// <value>The <c>rate_info_id</c> column value.</value>
		public int Rate_info_id
		{
			get { return _rate_info_id; }
			set { _rate_info_id = value; }
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
		/// Gets or sets the <c>time_of_day_policy</c> column value.
		/// </summary>
		/// <value>The <c>time_of_day_policy</c> column value.</value>
		public byte Time_of_day_policy
		{
			get { return _time_of_day_policy; }
			set { _time_of_day_policy = value; }
		}

		/// <summary>
		/// Returns the string representation of this instance.
		/// </summary>
		/// <returns>The string representation of this instance.</returns>
		public override string ToString()
		{
			System.Text.StringBuilder dynStr = new System.Text.StringBuilder(GetType().Name);
			dynStr.Append(':');
			dynStr.Append("  Rate_info_id=");
			dynStr.Append(Rate_info_id);
			dynStr.Append("  Type_of_day_choice=");
			dynStr.Append(Type_of_day_choice);
			dynStr.Append("  Time_of_day_policy=");
			dynStr.Append(Time_of_day_policy);
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

	} // End of TypeOfDayRow_Base class
} // End of namespace

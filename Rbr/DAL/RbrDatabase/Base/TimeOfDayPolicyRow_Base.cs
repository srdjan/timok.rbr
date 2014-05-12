// <fileinfo name="Base\TimeOfDayPolicyRow_Base.cs">
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
	/// The base class for <see cref="TimeOfDayPolicyRow"/> that 
	/// represents a record in the <c>TimeOfDayPolicy</c> table.
	/// </summary>
	/// <remarks>
	/// Do not change this source code manually. Update the <see cref="TimeOfDayPolicyRow"/>
	/// class if you need to add or change some functionality.
	/// </remarks>
	[Serializable]
	public abstract class TimeOfDayPolicyRow_Base
	{
	#region Timok Custom

		//db field names
		public const string time_of_day_policy_DbName = "time_of_day_policy";
		public const string name_DbName = "name";

		//prop names
		public const string time_of_day_policy_PropName = "Time_of_day_policy";
		public const string name_PropName = "Name";

		//db field display names
		public const string time_of_day_policy_DisplayName = "time of day policy";
		public const string name_DisplayName = "name";
	#endregion Timok Custom


		private byte _time_of_day_policy;
		private string _name;

		/// <summary>
		/// Initializes a new instance of the <see cref="TimeOfDayPolicyRow_Base"/> class.
		/// </summary>
		public TimeOfDayPolicyRow_Base()
		{
			// EMPTY
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
			dynStr.Append("  Time_of_day_policy=");
			dynStr.Append(Time_of_day_policy);
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

	} // End of TimeOfDayPolicyRow_Base class
} // End of namespace

// <fileinfo name="Base\RateRow_Base.cs">
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
	/// The base class for <see cref="RateRow"/> that 
	/// represents a record in the <c>Rate</c> table.
	/// </summary>
	/// <remarks>
	/// Do not change this source code manually. Update the <see cref="RateRow"/>
	/// class if you need to add or change some functionality.
	/// </remarks>
	[Serializable]
	public abstract class RateRow_Base
	{
	#region Timok Custom

		//db field names
		public const string rate_info_id_DbName = "rate_info_id";
		public const string type_of_day_choice_DbName = "type_of_day_choice";
		public const string time_of_day_DbName = "time_of_day";
		public const string first_incr_length_DbName = "first_incr_length";
		public const string add_incr_length_DbName = "add_incr_length";
		public const string first_incr_cost_DbName = "first_incr_cost";
		public const string add_incr_cost_DbName = "add_incr_cost";

		//prop names
		public const string rate_info_id_PropName = "Rate_info_id";
		public const string type_of_day_choice_PropName = "Type_of_day_choice";
		public const string time_of_day_PropName = "Time_of_day";
		public const string first_incr_length_PropName = "First_incr_length";
		public const string add_incr_length_PropName = "Add_incr_length";
		public const string first_incr_cost_PropName = "First_incr_cost";
		public const string add_incr_cost_PropName = "Add_incr_cost";

		//db field display names
		public const string rate_info_id_DisplayName = "rate info id";
		public const string type_of_day_choice_DisplayName = "type of day choice";
		public const string time_of_day_DisplayName = "time of day";
		public const string first_incr_length_DisplayName = "first incr length";
		public const string add_incr_length_DisplayName = "add incr length";
		public const string first_incr_cost_DisplayName = "first incr cost";
		public const string add_incr_cost_DisplayName = "add incr cost";
	#endregion Timok Custom


		private int _rate_info_id;
		private byte _type_of_day_choice;
		private byte _time_of_day;
		private byte _first_incr_length;
		private byte _add_incr_length;
		private decimal _first_incr_cost;
		private decimal _add_incr_cost;

		/// <summary>
		/// Initializes a new instance of the <see cref="RateRow_Base"/> class.
		/// </summary>
		public RateRow_Base()
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
		/// Gets or sets the <c>time_of_day</c> column value.
		/// </summary>
		/// <value>The <c>time_of_day</c> column value.</value>
		public byte Time_of_day
		{
			get { return _time_of_day; }
			set { _time_of_day = value; }
		}

		/// <summary>
		/// Gets or sets the <c>first_incr_length</c> column value.
		/// </summary>
		/// <value>The <c>first_incr_length</c> column value.</value>
		public byte First_incr_length
		{
			get { return _first_incr_length; }
			set { _first_incr_length = value; }
		}

		/// <summary>
		/// Gets or sets the <c>add_incr_length</c> column value.
		/// </summary>
		/// <value>The <c>add_incr_length</c> column value.</value>
		public byte Add_incr_length
		{
			get { return _add_incr_length; }
			set { _add_incr_length = value; }
		}

		/// <summary>
		/// Gets or sets the <c>first_incr_cost</c> column value.
		/// </summary>
		/// <value>The <c>first_incr_cost</c> column value.</value>
		public decimal First_incr_cost
		{
			get { return _first_incr_cost; }
			set { _first_incr_cost = value; }
		}

		/// <summary>
		/// Gets or sets the <c>add_incr_cost</c> column value.
		/// </summary>
		/// <value>The <c>add_incr_cost</c> column value.</value>
		public decimal Add_incr_cost
		{
			get { return _add_incr_cost; }
			set { _add_incr_cost = value; }
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
			dynStr.Append("  Time_of_day=");
			dynStr.Append(Time_of_day);
			dynStr.Append("  First_incr_length=");
			dynStr.Append(First_incr_length);
			dynStr.Append("  Add_incr_length=");
			dynStr.Append(Add_incr_length);
			dynStr.Append("  First_incr_cost=");
			dynStr.Append(First_incr_cost);
			dynStr.Append("  Add_incr_cost=");
			dynStr.Append(Add_incr_cost);
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

	} // End of RateRow_Base class
} // End of namespace

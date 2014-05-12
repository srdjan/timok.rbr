// <fileinfo name="Base\RetailRouteBonusMinutesRow_Base.cs">
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
	/// The base class for <see cref="RetailRouteBonusMinutesRow"/> that 
	/// represents a record in the <c>RetailRouteBonusMinutes</c> table.
	/// </summary>
	/// <remarks>
	/// Do not change this source code manually. Update the <see cref="RetailRouteBonusMinutesRow"/>
	/// class if you need to add or change some functionality.
	/// </remarks>
	[Serializable]
	public abstract class RetailRouteBonusMinutesRow_Base
	{
	#region Timok Custom

		//db field names
		public const string retail_acct_id_DbName = "retail_acct_id";
		public const string retail_route_id_DbName = "retail_route_id";
		public const string start_bonus_minutes_DbName = "start_bonus_minutes";
		public const string current_bonus_minutes_DbName = "current_bonus_minutes";

		//prop names
		public const string retail_acct_id_PropName = "Retail_acct_id";
		public const string retail_route_id_PropName = "Retail_route_id";
		public const string start_bonus_minutes_PropName = "Start_bonus_minutes";
		public const string current_bonus_minutes_PropName = "Current_bonus_minutes";

		//db field display names
		public const string retail_acct_id_DisplayName = "retail acct id";
		public const string retail_route_id_DisplayName = "retail route id";
		public const string start_bonus_minutes_DisplayName = "start bonus minutes";
		public const string current_bonus_minutes_DisplayName = "current bonus minutes";
	#endregion Timok Custom


		private int _retail_acct_id;
		private int _retail_route_id;
		private short _start_bonus_minutes;
		private short _current_bonus_minutes;

		/// <summary>
		/// Initializes a new instance of the <see cref="RetailRouteBonusMinutesRow_Base"/> class.
		/// </summary>
		public RetailRouteBonusMinutesRow_Base()
		{
			// EMPTY
		}

		/// <summary>
		/// Gets or sets the <c>retail_acct_id</c> column value.
		/// </summary>
		/// <value>The <c>retail_acct_id</c> column value.</value>
		public int Retail_acct_id
		{
			get { return _retail_acct_id; }
			set { _retail_acct_id = value; }
		}

		/// <summary>
		/// Gets or sets the <c>retail_route_id</c> column value.
		/// </summary>
		/// <value>The <c>retail_route_id</c> column value.</value>
		public int Retail_route_id
		{
			get { return _retail_route_id; }
			set { _retail_route_id = value; }
		}

		/// <summary>
		/// Gets or sets the <c>start_bonus_minutes</c> column value.
		/// </summary>
		/// <value>The <c>start_bonus_minutes</c> column value.</value>
		public short Start_bonus_minutes
		{
			get { return _start_bonus_minutes; }
			set { _start_bonus_minutes = value; }
		}

		/// <summary>
		/// Gets or sets the <c>current_bonus_minutes</c> column value.
		/// </summary>
		/// <value>The <c>current_bonus_minutes</c> column value.</value>
		public short Current_bonus_minutes
		{
			get { return _current_bonus_minutes; }
			set { _current_bonus_minutes = value; }
		}

		/// <summary>
		/// Returns the string representation of this instance.
		/// </summary>
		/// <returns>The string representation of this instance.</returns>
		public override string ToString()
		{
			System.Text.StringBuilder dynStr = new System.Text.StringBuilder(GetType().Name);
			dynStr.Append(':');
			dynStr.Append("  Retail_acct_id=");
			dynStr.Append(Retail_acct_id);
			dynStr.Append("  Retail_route_id=");
			dynStr.Append(Retail_route_id);
			dynStr.Append("  Start_bonus_minutes=");
			dynStr.Append(Start_bonus_minutes);
			dynStr.Append("  Current_bonus_minutes=");
			dynStr.Append(Current_bonus_minutes);
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

	} // End of RetailRouteBonusMinutesRow_Base class
} // End of namespace

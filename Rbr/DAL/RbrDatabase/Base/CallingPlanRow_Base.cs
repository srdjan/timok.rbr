// <fileinfo name="Base\CallingPlanRow_Base.cs">
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
	/// The base class for <see cref="CallingPlanRow"/> that 
	/// represents a record in the <c>CallingPlan</c> table.
	/// </summary>
	/// <remarks>
	/// Do not change this source code manually. Update the <see cref="CallingPlanRow"/>
	/// class if you need to add or change some functionality.
	/// </remarks>
	[Serializable]
	public abstract class CallingPlanRow_Base
	{
	#region Timok Custom

		//db field names
		public const string calling_plan_id_DbName = "calling_plan_id";
		public const string name_DbName = "name";
		public const string virtual_switch_id_DbName = "virtual_switch_id";
		public const string version_DbName = "version";

		//prop names
		public const string calling_plan_id_PropName = "Calling_plan_id";
		public const string name_PropName = "Name";
		public const string virtual_switch_id_PropName = "Virtual_switch_id";
		public const string version_PropName = "Version";

		//db field display names
		public const string calling_plan_id_DisplayName = "calling plan id";
		public const string name_DisplayName = "name";
		public const string virtual_switch_id_DisplayName = "virtual switch id";
		public const string version_DisplayName = "version";
	#endregion Timok Custom


		private int _calling_plan_id;
		private string _name;
		private int _virtual_switch_id;
		private int _version;

		/// <summary>
		/// Initializes a new instance of the <see cref="CallingPlanRow_Base"/> class.
		/// </summary>
		public CallingPlanRow_Base()
		{
			// EMPTY
		}

		/// <summary>
		/// Gets or sets the <c>calling_plan_id</c> column value.
		/// </summary>
		/// <value>The <c>calling_plan_id</c> column value.</value>
		public int Calling_plan_id
		{
			get { return _calling_plan_id; }
			set { _calling_plan_id = value; }
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
		/// Gets or sets the <c>virtual_switch_id</c> column value.
		/// </summary>
		/// <value>The <c>virtual_switch_id</c> column value.</value>
		public int Virtual_switch_id
		{
			get { return _virtual_switch_id; }
			set { _virtual_switch_id = value; }
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
			dynStr.Append("  Calling_plan_id=");
			dynStr.Append(Calling_plan_id);
			dynStr.Append("  Name=");
			dynStr.Append(Name);
			dynStr.Append("  Virtual_switch_id=");
			dynStr.Append(Virtual_switch_id);
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

	} // End of CallingPlanRow_Base class
} // End of namespace

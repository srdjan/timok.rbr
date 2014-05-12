// <fileinfo name="Base\TerminationRouteViewRow_Base.cs">
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
	/// The base class for <see cref="TerminationRouteViewRow"/> that 
	/// represents a record in the <c>TerminationRouteView</c> view.
	/// </summary>
	/// <remarks>
	/// Do not change this source code manually. Update the <see cref="TerminationRouteViewRow"/>
	/// class if you need to add or change some functionality.
	/// </remarks>
	[Serializable]
	public abstract class TerminationRouteViewRow_Base
	{
	#region Timok Custom

		//db field names
		public const string carrier_route_id_DbName = "carrier_route_id";
		public const string route_name_DbName = "route_name";
		public const string carrier_acct_id_DbName = "carrier_acct_id";
		public const string carrier_acct_name_DbName = "carrier_acct_name";
		public const string calling_plan_id_DbName = "calling_plan_id";
		public const string rating_type_DbName = "rating_type";
		public const string base_route_id_DbName = "base_route_id";
		public const string partial_coverage_DbName = "partial_coverage";

		//prop names
		public const string carrier_route_id_PropName = "Carrier_route_id";
		public const string route_name_PropName = "Route_name";
		public const string carrier_acct_id_PropName = "Carrier_acct_id";
		public const string carrier_acct_name_PropName = "Carrier_acct_name";
		public const string calling_plan_id_PropName = "Calling_plan_id";
		public const string rating_type_PropName = "Rating_type";
		public const string base_route_id_PropName = "Base_route_id";
		public const string partial_coverage_PropName = "Partial_coverage";

		//db field display names
		public const string carrier_route_id_DisplayName = "carrier route id";
		public const string route_name_DisplayName = "route name";
		public const string carrier_acct_id_DisplayName = "carrier acct id";
		public const string carrier_acct_name_DisplayName = "carrier acct name";
		public const string calling_plan_id_DisplayName = "calling plan id";
		public const string rating_type_DisplayName = "rating type";
		public const string base_route_id_DisplayName = "base route id";
		public const string partial_coverage_DisplayName = "partial coverage";
	#endregion Timok Custom


		private int _carrier_route_id;
		private string _route_name;
		private short _carrier_acct_id;
		private bool _carrier_acct_idNull = true;
		private string _carrier_acct_name;
		private int _calling_plan_id;
		private byte _rating_type;
		private bool _rating_typeNull = true;
		private int _base_route_id;
		private byte _partial_coverage;
		private bool _partial_coverageNull = true;

		/// <summary>
		/// Initializes a new instance of the <see cref="TerminationRouteViewRow_Base"/> class.
		/// </summary>
		public TerminationRouteViewRow_Base()
		{
			// EMPTY
		}

		/// <summary>
		/// Gets or sets the <c>carrier_route_id</c> column value.
		/// </summary>
		/// <value>The <c>carrier_route_id</c> column value.</value>
		public int Carrier_route_id
		{
			get { return _carrier_route_id; }
			set { _carrier_route_id = value; }
		}

		/// <summary>
		/// Gets or sets the <c>route_name</c> column value.
		/// </summary>
		/// <value>The <c>route_name</c> column value.</value>
		public string Route_name
		{
			get { return _route_name; }
			set { _route_name = value; }
		}

		/// <summary>
		/// Gets or sets the <c>carrier_acct_id</c> column value.
		/// This column is nullable.
		/// </summary>
		/// <value>The <c>carrier_acct_id</c> column value.</value>
		public short Carrier_acct_id
		{
			get
			{
				//if(IsCarrier_acct_idNull)
					//throw new InvalidOperationException("Cannot get value because it is DBNull.");
				return _carrier_acct_id;
			}
			set
			{
				_carrier_acct_idNull = false;
				_carrier_acct_id = value;
			}
		}

		/// <summary>
		/// Indicates whether the <see cref="Carrier_acct_id"/>
		/// property value is null.
		/// </summary>
		/// <value>true if the property value is null, otherwise false.</value>
		public bool IsCarrier_acct_idNull
		{
			get { return _carrier_acct_idNull; }
			set { _carrier_acct_idNull = value; }
		}

		/// <summary>
		/// Gets or sets the <c>carrier_acct_name</c> column value.
		/// </summary>
		/// <value>The <c>carrier_acct_name</c> column value.</value>
		public string Carrier_acct_name
		{
			get { return _carrier_acct_name; }
			set { _carrier_acct_name = value; }
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
		/// Gets or sets the <c>rating_type</c> column value.
		/// This column is nullable.
		/// </summary>
		/// <value>The <c>rating_type</c> column value.</value>
		public byte Rating_type
		{
			get
			{
				//if(IsRating_typeNull)
					//throw new InvalidOperationException("Cannot get value because it is DBNull.");
				return _rating_type;
			}
			set
			{
				_rating_typeNull = false;
				_rating_type = value;
			}
		}

		/// <summary>
		/// Indicates whether the <see cref="Rating_type"/>
		/// property value is null.
		/// </summary>
		/// <value>true if the property value is null, otherwise false.</value>
		public bool IsRating_typeNull
		{
			get { return _rating_typeNull; }
			set { _rating_typeNull = value; }
		}

		/// <summary>
		/// Gets or sets the <c>base_route_id</c> column value.
		/// </summary>
		/// <value>The <c>base_route_id</c> column value.</value>
		public int Base_route_id
		{
			get { return _base_route_id; }
			set { _base_route_id = value; }
		}

		/// <summary>
		/// Gets or sets the <c>partial_coverage</c> column value.
		/// This column is nullable.
		/// </summary>
		/// <value>The <c>partial_coverage</c> column value.</value>
		public byte Partial_coverage
		{
			get
			{
				//if(IsPartial_coverageNull)
					//throw new InvalidOperationException("Cannot get value because it is DBNull.");
				return _partial_coverage;
			}
			set
			{
				_partial_coverageNull = false;
				_partial_coverage = value;
			}
		}

		/// <summary>
		/// Indicates whether the <see cref="Partial_coverage"/>
		/// property value is null.
		/// </summary>
		/// <value>true if the property value is null, otherwise false.</value>
		public bool IsPartial_coverageNull
		{
			get { return _partial_coverageNull; }
			set { _partial_coverageNull = value; }
		}

		/// <summary>
		/// Returns the string representation of this instance.
		/// </summary>
		/// <returns>The string representation of this instance.</returns>
		public override string ToString()
		{
			System.Text.StringBuilder dynStr = new System.Text.StringBuilder(GetType().Name);
			dynStr.Append(':');
			dynStr.Append("  Carrier_route_id=");
			dynStr.Append(Carrier_route_id);
			dynStr.Append("  Route_name=");
			dynStr.Append(Route_name);
			dynStr.Append("  Carrier_acct_id=");
			dynStr.Append(IsCarrier_acct_idNull ? (object)"<NULL>" : Carrier_acct_id);
			dynStr.Append("  Carrier_acct_name=");
			dynStr.Append(Carrier_acct_name);
			dynStr.Append("  Calling_plan_id=");
			dynStr.Append(Calling_plan_id);
			dynStr.Append("  Rating_type=");
			dynStr.Append(IsRating_typeNull ? (object)"<NULL>" : Rating_type);
			dynStr.Append("  Base_route_id=");
			dynStr.Append(Base_route_id);
			dynStr.Append("  Partial_coverage=");
			dynStr.Append(IsPartial_coverageNull ? (object)"<NULL>" : Partial_coverage);
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

	} // End of TerminationRouteViewRow_Base class
} // End of namespace

// <fileinfo name="Base\GenerationRequestRow_Base.cs">
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
	/// The base class for <see cref="GenerationRequestRow"/> that 
	/// represents a record in the <c>GenerationRequest</c> table.
	/// </summary>
	/// <remarks>
	/// Do not change this source code manually. Update the <see cref="GenerationRequestRow"/>
	/// class if you need to add or change some functionality.
	/// </remarks>
	[Serializable]
	public abstract class GenerationRequestRow_Base
	{
	#region Timok Custom

		//db field names
		public const string request_id_DbName = "request_id";
		public const string date_requested_DbName = "date_requested";
		public const string date_to_process_DbName = "date_to_process";
		public const string date_completed_DbName = "date_completed";
		public const string number_of_batches_DbName = "number_of_batches";
		public const string batch_size_DbName = "batch_size";
		public const string lot_id_DbName = "lot_id";

		//prop names
		public const string request_id_PropName = "Request_id";
		public const string date_requested_PropName = "Date_requested";
		public const string date_to_process_PropName = "Date_to_process";
		public const string date_completed_PropName = "Date_completed";
		public const string number_of_batches_PropName = "Number_of_batches";
		public const string batch_size_PropName = "Batch_size";
		public const string lot_id_PropName = "Lot_id";

		//db field display names
		public const string request_id_DisplayName = "request id";
		public const string date_requested_DisplayName = "date requested";
		public const string date_to_process_DisplayName = "date to process";
		public const string date_completed_DisplayName = "date completed";
		public const string number_of_batches_DisplayName = "number of batches";
		public const string batch_size_DisplayName = "batch size";
		public const string lot_id_DisplayName = "lot id";
	#endregion Timok Custom


		private int _request_id;
		private System.DateTime _date_requested;
		private System.DateTime _date_to_process;
		private System.DateTime _date_completed;
		private bool _date_completedNull = true;
		private int _number_of_batches;
		private int _batch_size;
		private int _lot_id;

		/// <summary>
		/// Initializes a new instance of the <see cref="GenerationRequestRow_Base"/> class.
		/// </summary>
		public GenerationRequestRow_Base()
		{
			// EMPTY
		}

		/// <summary>
		/// Gets or sets the <c>request_id</c> column value.
		/// </summary>
		/// <value>The <c>request_id</c> column value.</value>
		public int Request_id
		{
			get { return _request_id; }
			set { _request_id = value; }
		}

		/// <summary>
		/// Gets or sets the <c>date_requested</c> column value.
		/// </summary>
		/// <value>The <c>date_requested</c> column value.</value>
		public System.DateTime Date_requested
		{
			get { return _date_requested; }
			set { _date_requested = value; }
		}

		/// <summary>
		/// Gets or sets the <c>date_to_process</c> column value.
		/// </summary>
		/// <value>The <c>date_to_process</c> column value.</value>
		public System.DateTime Date_to_process
		{
			get { return _date_to_process; }
			set { _date_to_process = value; }
		}

		/// <summary>
		/// Gets or sets the <c>date_completed</c> column value.
		/// This column is nullable.
		/// </summary>
		/// <value>The <c>date_completed</c> column value.</value>
		public System.DateTime Date_completed
		{
			get
			{
				//if(IsDate_completedNull)
					//throw new InvalidOperationException("Cannot get value because it is DBNull.");
				return _date_completed;
			}
			set
			{
				_date_completedNull = false;
				_date_completed = value;
			}
		}

		/// <summary>
		/// Indicates whether the <see cref="Date_completed"/>
		/// property value is null.
		/// </summary>
		/// <value>true if the property value is null, otherwise false.</value>
		public bool IsDate_completedNull
		{
			get { return _date_completedNull; }
			set { _date_completedNull = value; }
		}

		/// <summary>
		/// Gets or sets the <c>number_of_batches</c> column value.
		/// </summary>
		/// <value>The <c>number_of_batches</c> column value.</value>
		public int Number_of_batches
		{
			get { return _number_of_batches; }
			set { _number_of_batches = value; }
		}

		/// <summary>
		/// Gets or sets the <c>batch_size</c> column value.
		/// </summary>
		/// <value>The <c>batch_size</c> column value.</value>
		public int Batch_size
		{
			get { return _batch_size; }
			set { _batch_size = value; }
		}

		/// <summary>
		/// Gets or sets the <c>lot_id</c> column value.
		/// </summary>
		/// <value>The <c>lot_id</c> column value.</value>
		public int Lot_id
		{
			get { return _lot_id; }
			set { _lot_id = value; }
		}

		/// <summary>
		/// Returns the string representation of this instance.
		/// </summary>
		/// <returns>The string representation of this instance.</returns>
		public override string ToString()
		{
			System.Text.StringBuilder dynStr = new System.Text.StringBuilder(GetType().Name);
			dynStr.Append(':');
			dynStr.Append("  Request_id=");
			dynStr.Append(Request_id);
			dynStr.Append("  Date_requested=");
			dynStr.Append(Date_requested);
			dynStr.Append("  Date_to_process=");
			dynStr.Append(Date_to_process);
			dynStr.Append("  Date_completed=");
			dynStr.Append(IsDate_completedNull ? (object)"<NULL>" : Date_completed);
			dynStr.Append("  Number_of_batches=");
			dynStr.Append(Number_of_batches);
			dynStr.Append("  Batch_size=");
			dynStr.Append(Batch_size);
			dynStr.Append("  Lot_id=");
			dynStr.Append(Lot_id);
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

	} // End of GenerationRequestRow_Base class
} // End of namespace

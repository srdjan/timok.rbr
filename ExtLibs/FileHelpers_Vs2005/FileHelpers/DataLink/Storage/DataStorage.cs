#region "  � Copyright 2005-06 to Marcos Meli - http://www.marcosmeli.com.ar" 

// Errors, suggestions, contributions, send a mail to: marcosdotnet[at]yahoo.com.ar.

#endregion

using System;
using System.ComponentModel;
using System.Data;

namespace FileHelpers.DataLink
{
	/// <summary>Base class for all the Storage classes of the library or the custom Storage classes.</summary>
	public abstract class DataStorage
	{

		/// <summary>For internal Use.</summary>
		/// <param name="handler"></param>
		/// <param name="mode"></param>
		/// <param name="current"></param>
		/// <param name="total"></param>
		[EditorBrowsable(EditorBrowsableState.Never)]
		protected void Notify(ProgressChangeHandler handler, ProgressMode mode, int current, int total)
		{
			ProgressHelper.Notify(handler, mode, current, total);
		}

		/// <summary>Indicates the way to notify the progress.</summary>
		protected ProgressMode mProgressMode = ProgressMode.DontNotify;

		/// <summary>You method handler used to notify progress.</summary>
		protected ProgressChangeHandler mNotifyHandler = null;

		/// <summary>Set the handler to the engine used to notify progress into the operations.</summary>
		/// <param name="handler">The <see cref="ProgressChangeHandler"/></param>
		public void SetProgressHandler(ProgressChangeHandler handler)
		{
			SetProgressHandler(handler, ProgressMode.NotifyRecords);
		}

		/// <summary>Set the handler to the engine used to notify progress into the operations.</summary>
		/// <param name="handler">Your <see cref="ProgressChangeHandler"/> method.</param>
		/// <param name="mode">The <see cref="ProgressMode"/> to use.</param>
		public void SetProgressHandler(ProgressChangeHandler handler, ProgressMode mode)
		{
			mNotifyHandler = handler;

			if (mode == ProgressMode.NotifyBytes)
				throw new NotImplementedException("Not implemented yet. Planed for version 1.5.0");

			mProgressMode = mode;
		}

		/// <summary>Returns the class that represent the records in the file.</summary>
		public abstract Type RecordType { get; }

		/// <summary>Must Return the records from the DataSource (DB, Excel, etc)</summary>
		/// <returns>The extracted records.</returns>
		public abstract object[] ExtractRecords();

		/// <summary>Must Return the records from the DataSource (DB, Excel, etc)</summary>
		/// <returns>The extracted records.</returns>
		public DataTable ExtractRecordsAsDT()
		{
			RecordInfo ri = new RecordInfo(RecordType);
			return ri.RecordsToDataTable(ExtractRecords());
		}

		/// <summary>Must Insert the records in a DataSource (DB, Excel, etc)</summary>
		/// <param name="records">The records to insert.</param>
		public abstract void InsertRecords(object[] records);


		/// <summary>The Object responsable for manage the errors.</summary>
		protected ErrorManager mErrorManager = new ErrorManager();

		/// <summary>The Object responsable for manage the errors.</summary>
		public ErrorManager ErrorManager
		{
			get { return mErrorManager; }
		}

		/// <summary>Add an error to the ErrorCollection.</summary>
		/// <param name="lineNumber">The line when the error occurs.</param>
		/// <param name="ex">The exception throwed, can be null.</param>
		protected void AddError(int lineNumber, Exception ex)
		{
			ErrorInfo e = new ErrorInfo();
			e.mLineNumber = lineNumber;
//			e.mColumnNumber = colNum;
			e.mExceptionInfo = ex;

			mErrorManager.AddError(e);
		}

		/// <summary>Creates an instance of this class.</summary>
		protected DataStorage()
		{
		}
/// <summary>Creates an instance of the RecordInfo class. This method is used because hte constructor of the record info is internal.</summary>
/// <param name="recordClass">The class passed to the RecordInfo constructor.</param>
/// <returns>A RecordInfo instance.</returns>

		protected static RecordInfo CreateRecordInfo(Type recordClass)
		{
			return new RecordInfo(recordClass);
		}
	}
}
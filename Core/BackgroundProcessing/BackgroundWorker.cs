// © 2004 IDesign Inc. All rights reserved 
//Questions? Comments? go to 
//http://www.idesign.net

// 2005 Timok.es	No rights reserved 
//Questions? Comments? go to 
//http://www.timok.com

using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Runtime.Remoting.Messaging;

namespace Timok.Core.BackgroundProcessing {   

	public class BackgroundWorker : IBackgroundWorker {
		private IAsyncResult ar;
		private bool cancelPending = false;
		
		#region public properties
		public bool CancellationPending { get { lock(this) { return cancelPending; } } }
		#endregion public properties
      
		#region private events
		private event DoWorkEventHandler DoWork;
		#endregion private events
		
		#region public events
		public event ProgressChangedEventHandler ProgressChanged;
		public event StatusChangedEventHandler StatusChanged;
		public event DataReceivedEventHandler DataReceived;
		public event WorkCompletedEventHandler WorkCompleted;
		#endregion public events

		#region ctor
		BackgroundWorker() { }

		public BackgroundWorker(ITask pTask) {
      pTask.Host = this;
			DoWork += new DoWorkEventHandler(pTask.Run);
		}
		#endregion ctor


		#region publics methods

		public void RunWorkerAsync()  {
			RunWorkerAsync(null);
		}
      
		public void RunWorkerAsync(object pArgument) {
			cancelPending = false;
			if (DoWork != null) {
				WorkerEventArgs _args = new WorkerEventArgs(pArgument);
				AsyncCallback _callback = new AsyncCallback(reportCompletion);
				//NOTE: ? why do we need ar here ?
				ar = DoWork.BeginInvoke(this, _args, _callback, _args);
			}
		}

		public void CancelAsync() { 
			lock(this) { cancelPending = true; } 
		}
      
		public void ReportProgress(int pPercent)  {
			ProgressChangedEventArgs progressArgs = new ProgressChangedEventArgs(pPercent);
			onProgressChanged(progressArgs);
		}
	  
		public void ReportStatus(string pStatus)  {
			StatusChangedEventArgs _statusArgs = new StatusChangedEventArgs(pStatus);
			onStatusChanged(_statusArgs);
		}

		public void ReportDataReceived(object pData) {
			DataReceivedEventArgs _dataReceivedArgs = new DataReceivedEventArgs(pData);
			onDataReceived(_dataReceivedArgs);
		}

		public void ReportWorkCompleted(object pResult, Exception pError, bool pCancel) {
			WorkCompletedEventArgs _dataReceivedArgs = new WorkCompletedEventArgs(pResult, pError, pCancel);
			onWorkerCompleted(_dataReceivedArgs);
		}
		
		#endregion publics methods

		#region virtual protected methods

		protected virtual void onWorkerCompleted(WorkCompletedEventArgs pCompletedArgs) {
			ProcessDelegate(WorkCompleted, this, pCompletedArgs);
		}
      
		protected virtual void onProgressChanged(ProgressChangedEventArgs pProgressArgs) {
			ProcessDelegate(ProgressChanged, this, pProgressArgs);
		}

		protected virtual void onStatusChanged(StatusChangedEventArgs pStatusArgs) {
			ProcessDelegate(StatusChanged, this, pStatusArgs);
		}

		protected virtual void onDataReceived(DataReceivedEventArgs pDataReceivedArgs) {
			ProcessDelegate(DataReceived, this, pDataReceivedArgs);
		}
		#endregion virtual protected methods

		#region privates methods

		protected void ProcessDelegate(Delegate pDelegate, params object[] pArgs) {
			Delegate _temp = pDelegate;
			if (_temp == null) {
				return;
			}
			Delegate[] _delegates = _temp.GetInvocationList();
			foreach (Delegate _handler in _delegates) {
				invokeDelegate(_handler, pArgs);
			}
		}
      
		private void invokeDelegate(Delegate pDelegate, object[] pArgs) {
			ISynchronizeInvoke _synchronizer  = pDelegate.Target as ISynchronizeInvoke;
			if (_synchronizer != null) { //A Windows Forms object
				if (_synchronizer.InvokeRequired == false) {
					pDelegate.DynamicInvoke(pArgs);
					return;
				}
				try {
					_synchronizer.Invoke(pDelegate, pArgs);
				}
				catch (Exception _ex) {
					Debug.WriteLine(_ex);
				}
			}
			else { //Not a Windows Forms object
				pDelegate.DynamicInvoke(pArgs);
			}
		}

		private void reportCompletion(IAsyncResult pAsyncResult) {
			AsyncResult _asyncResult = (AsyncResult)pAsyncResult;
			DoWorkEventHandler _del = (DoWorkEventHandler) _asyncResult.AsyncDelegate;
         
			WorkerEventArgs _doWorkArgs = (WorkerEventArgs) pAsyncResult.AsyncState;
			object _result = null;
			Exception _error = null;

			try {
				_del.EndInvoke(pAsyncResult);
				_result = _doWorkArgs.Result;
			}
			catch (Exception _ex) {
				_error = _ex;
			}

			WorkCompletedEventArgs _completedArgs = new WorkCompletedEventArgs(_result, _error, _doWorkArgs.Cancel);
			onWorkerCompleted(_completedArgs);
		}
		#endregion privates methods
	} 
}

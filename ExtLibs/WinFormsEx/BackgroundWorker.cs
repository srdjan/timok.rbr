// © 2004 IDesign Inc. All rights reserved 
//Questions? Comments? go to 
//http://www.idesign.net

using System;
using System.Drawing;
using System.ComponentModel;
using System.Threading;
using System.Diagnostics;
using System.Runtime.Remoting.Messaging;

namespace WinFormsEx {   
	[ToolboxBitmap(typeof(BackgroundWorker),"bw.bmp")]
	public class BackgroundWorker : Component  {
		bool m_CancelPending = false;
		bool m_ReportsProgress = false;
		bool m_ReportsStatus = false;
		bool m_SupportsCancellation = false;
		public event DoWorkEventHandler DoWork;
		public event ProgressChangedEventHandler ProgressChanged;
		public event StatusChangedEventHandler StatusChanged;
		public event WorkCompletedEventHandler WorkCompleted;

//		public BackgroundWorker(DoWorkEventHandler DoWork, ProgressChangedEventHandler ProgressChanged, StatusChangedEventHandler StatusChanged, WorkerCompletedEventHandler RunWorkerCompleted) {
//		}

		void ProcessDelegate(Delegate del, params object[] args) {
			Delegate temp = del;
			if(temp == null) {
				return;
			}
			Delegate[] delegates = temp.GetInvocationList();
			foreach(Delegate handler in delegates) {
				InvokeDelegate(handler,args);
			}
		}
      
		void InvokeDelegate(Delegate del,object[] args) {
			ISynchronizeInvoke synchronizer  = del.Target as ISynchronizeInvoke;
			if (synchronizer != null) { //A Windows Forms object
				if(synchronizer.InvokeRequired == false) {
					del.DynamicInvoke(args);
					return;
				}
				try {
					synchronizer.Invoke(del,args);
				}
				catch {}
			}
			else { //Not a Windows Forms object
				del.DynamicInvoke(args);
			}
		}

		void ReportCompletion(IAsyncResult asyncResult) {
			AsyncResult ar = (AsyncResult)asyncResult;
			DoWorkEventHandler del  = (DoWorkEventHandler) ar.AsyncDelegate;
         
			DoWorkEventArgs doWorkArgs = (DoWorkEventArgs) asyncResult.AsyncState;
			object result = null;
			Exception error = null;

			try {
				del.EndInvoke(asyncResult);
				result = doWorkArgs.Result;
			}
			catch(Exception exception) {
				error = exception;
			}

			WorkCompletedEventArgs completedArgs = new WorkCompletedEventArgs(result, error, doWorkArgs.Cancel);
			OnWorkerCompleted(completedArgs);
		}

		protected virtual void OnWorkerCompleted(WorkCompletedEventArgs completedArgs) {
			ProcessDelegate(WorkCompleted, this, completedArgs);
		}

		public void RunWorkerAsync()  {
			RunWorkerAsync(null);
		}
      
		public void RunWorkerAsync(object argument) {
			m_CancelPending = false;
			if(DoWork != null) {
				DoWorkEventArgs args = new DoWorkEventArgs(argument);
				AsyncCallback callback;
				callback = new  AsyncCallback(ReportCompletion);
				DoWork.BeginInvoke(this, args, callback, args);
			}
		}
      
		public void ReportProgress(int percent)  {
			if (WorkerReportsProgress) {
				ProgressChangedEventArgs progressArgs = new ProgressChangedEventArgs(percent);
				OnProgressChanged(progressArgs);
			}
		}
	  
		public void ReportStatus(string pStatus)  {
			if (WorkerReportsStatus) {
				StatusChangedEventArgs _statusArgs = new StatusChangedEventArgs(pStatus);
				OnStatusChanged(_statusArgs);
			}
		}
      
		protected virtual void OnProgressChanged(ProgressChangedEventArgs progressArgs) {
			ProcessDelegate(ProgressChanged,this,progressArgs);
		}

		protected virtual void OnStatusChanged(StatusChangedEventArgs pStatusArgs) {
			ProcessDelegate(StatusChanged, this, pStatusArgs);
		}

		public void CancelAsync() {
			lock(this) {
				m_CancelPending = true;
			}
		}
      
		public bool CancellationPending {
			get {
				lock(this) {
					return m_CancelPending;
				}
			}
		}
      
		public bool WorkerSupportsCancellation  {
			get  {
				lock(this) {
					return m_SupportsCancellation;
				}
			}

			set {
				lock(this) {
					m_SupportsCancellation = value;
				}
			}
		}

		public bool WorkerReportsProgress  {
			get  {
				lock(this)  {
					return m_ReportsProgress;
				}
			}

			set {
				lock(this) {
					m_ReportsProgress = value;
				}
			}
		} 
		public bool WorkerReportsStatus  {
			get  {
				lock(this)  {
					return m_ReportsStatus;
				}
			}

			set {
				lock(this) {
					m_ReportsStatus = value;
				}
			}
		} 
	} 
}

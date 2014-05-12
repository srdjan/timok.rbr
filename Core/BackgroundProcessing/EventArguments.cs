// © 2004 IDesign Inc. All rights reserved 
//Questions? Comments? go to 
//http://www.idesign.net

// 2005 Timok.es	No rights reserved 
//Questions? Comments? go to 
//http://www.timok.com

using System;

namespace Timok.Core.BackgroundProcessing
{
  public class CancelEventArgs : EventArgs {
    protected bool cancel;
    public bool Cancel { get { return cancel; } set { cancel = value; } }
  }

	public class WorkerEventArgs : CancelEventArgs  {
      object result;
      public object Result { get { return result; } set { result = value; } }
      public readonly object Argument; 
      public WorkerEventArgs(object pArgument) {
         Argument = pArgument;
      }
   }

	public class ProgressChangedEventArgs : EventArgs {
		public readonly int ProgressPercentage; 
		public ProgressChangedEventArgs(int pPercentage) {
			ProgressPercentage = pPercentage;
		}
	}

	public class StatusChangedEventArgs : EventArgs {
		public readonly string Status; 
		public StatusChangedEventArgs(string pStatus) {
			Status = pStatus;
		}
	}

	public class DataReceivedEventArgs : EventArgs {
		public readonly object Data; 
		public DataReceivedEventArgs(object pData) {
			Data = pData;
		}
	}

   public class AsyncCompletedEventArgs : EventArgs {
      public readonly Exception Error;
      public readonly bool Cancelled;

      public AsyncCompletedEventArgs(Exception pError, bool pCancel) {
         Error = pError;
         Cancelled = pCancel;
      }
   }

   public class WorkCompletedEventArgs : AsyncCompletedEventArgs  {
      public readonly object Result;
      public WorkCompletedEventArgs(object pResult, Exception pError, bool pCancel) : base(pError, pCancel) {
         Result = pResult;
      }
   }
    
	public delegate void DoWorkEventHandler(object sender, WorkerEventArgs e);
	public delegate void ProgressChangedEventHandler(object sender, ProgressChangedEventArgs e);
	public delegate void StatusChangedEventHandler(object sender, StatusChangedEventArgs e);
	public delegate void DataReceivedEventHandler(object sender, DataReceivedEventArgs e);
	public delegate void WorkCompletedEventHandler(object sender, WorkCompletedEventArgs e);
}
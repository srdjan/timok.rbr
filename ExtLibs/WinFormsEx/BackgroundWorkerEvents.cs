// © 2004 IDesign Inc. All rights reserved 
//Questions? Comments? go to 
//http://www.idesign.net

using System;
using System.Diagnostics;

namespace WinFormsEx
{
   public class CancelEventArgs : EventArgs  {
      protected bool m_Cancel; 
	  public bool Cancel {
         get  {
            return m_Cancel;
         }   
         set  {
            m_Cancel = value;
         }   
      }
   }

	public class DoWorkEventArgs : CancelEventArgs  {
      object m_Result;
      public object Result {
         get {
            return m_Result;
         }   
         set {
            m_Result = value;
         }   
      }
      public readonly object Argument; 
      public DoWorkEventArgs(object argument) {
         Argument = argument;
      }
   }

	public class ProgressChangedEventArgs : EventArgs {
		public readonly int ProgressPercentage; 
		public ProgressChangedEventArgs(int percentage) {
			ProgressPercentage = percentage;
		}
	}

	public class StatusChangedEventArgs : EventArgs {
		public readonly string Status; 
		public StatusChangedEventArgs(string pStatus) {
			Status = pStatus;
		}
	}

   public class AsyncCompletedEventArgs : EventArgs {
      public readonly Exception Error;
      public readonly bool Cancelled;

      public AsyncCompletedEventArgs(Exception error, bool cancel) {
         Error = error;
         Cancelled = cancel;
      }
   }

   public class WorkCompletedEventArgs : AsyncCompletedEventArgs  {
      public readonly object Result;
      public WorkCompletedEventArgs(object result, Exception error, bool cancel) : base(error, cancel) {
         Result = result;
      }
   }
    
	public delegate void DoWorkEventHandler(object sender, DoWorkEventArgs e);
	public delegate void ProgressChangedEventHandler(object sender, ProgressChangedEventArgs e);
	public delegate void StatusChangedEventHandler(object sender, StatusChangedEventArgs e);
	public delegate void WorkCompletedEventHandler(object sender, WorkCompletedEventArgs e);
}
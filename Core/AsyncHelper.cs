using System;
using System.Diagnostics;
using System.Threading;

namespace Timok.Core
{
	public class AsyncHelper {
		class TargetInfo {
			internal TargetInfo( Delegate d, object[] args )   {
				Target = d;
				Args = args;
			}
			internal readonly Delegate Target;
			internal readonly object[] Args;
		}

		private static readonly WaitCallback dynamicInvokeShim = DynamicInvokeShim;

		public static void FireAndForget( Delegate d, params object[] args ) {
			ThreadPool.QueueUserWorkItem(dynamicInvokeShim, new TargetInfo(d, args));
		}

		static void DynamicInvokeShim( object o ) {
			try {
				var ti = (TargetInfo) o;
				ti.Target.DynamicInvoke(ti.Args);
			}
			catch (Exception _ex) {
				try { 
					EventLog.WriteEntry("Application", string.Format("\r\nDynamicIvocation failed:\r\n{0}", (_ex.InnerException ?? _ex)), EventLogEntryType.Error, 1); } 
				catch { }
			}
		}
	}
}

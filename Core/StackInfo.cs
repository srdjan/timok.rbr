using System;
using System.Diagnostics;

namespace Timok.Core {
	[Serializable]
	public class CallingMethodInfo {
		public string AssemblyFullName;
		public string TypeFullName;
		public string FullName;
		public object[] MethodParameters;
	}

	public static class StackInfo {
		public static CallingMethodInfo Get(int pSkipFrames) {
			CallingMethodInfo _info = null;
			var _stackTrace = new StackTrace(pSkipFrames, true);
			
			var _stackFrame = _stackTrace.GetFrame(0);
			if (_stackFrame !=  null) {
				var _stackFrameMethod = _stackFrame.GetMethod();

				_info = new CallingMethodInfo();
				_info.AssemblyFullName = _stackFrameMethod.ReflectedType.Assembly.FullName;
				_info.TypeFullName = _stackFrameMethod.ReflectedType.FullName;
				_info.FullName = _stackFrameMethod.Name;
			}
			return _info;
		}
	}
}

using System;
using System.Reflection;
using Timok.Core;
using Timok.Core.Logging;
using Timok.Rbr.Core;
using Timok.Rbr.Core.Config;

using T = Timok.Core.Logger.TimokLogger;

namespace Timok.IVR.Scripting {
	internal static class SessionFactory {
		public static IScriptHandler CreateFlowHandler(ScriptInfo pScript, ISession pSession) {
			if (pScript == null || pSession == null) {
				throw new Exception("SessionFactory.Createinstance Error: Script or Session == null");
			}

			var _instance = Activator<ScriptType, IScriptHandler>.CreateInstance(pScript.ScriptType, new object[] { pScript, pSession });
			if (_instance == null) {	// Create a type and Add it to the generic Factory, so it can be cached and returned next time.
				var _typeName = string.Format("Timok.IVR.Scripting.{0}CallScript", pScript.ScriptType);
				
				var _type = Assembly.GetCallingAssembly().GetType(_typeName);
				if (_type == null) {
					T.LogRbr(LogSeverity.Error, "SessionFactory.Createinstance", string.Format("Error creating type={0}", _typeName));
					return null;
				}
				Activator<ScriptType, ISessionHandler>.Add(pScript.ScriptType, _type);

				_instance = Activator<ScriptType, IScriptHandler>.CreateInstance(pScript.ScriptType, new object[] { pScript, pSession });
			}

			return _instance;
		}
	}
}
using System;
using System.Reflection;
using Timok.Core;
using Timok.Logger;
using Timok.Rbr.Core;
using Timok.Rbr.Core.Config;

namespace Timok.Rbr.Service {
	public class SessionDispatcher : ISessionDispatcher {
		readonly CallStatistics callStatistics;
		ScriptInfo script;
		ISession session;

		public SessionDispatcher(CallStatistics pCallStatistics) {
			callStatistics = pCallStatistics;
		}

		public bool Run(ISessionChannel pChannel) {
			try {
				session = new Session(pChannel);

				script = (new ScriptFactory()).Create(session);
				if (script == null) {
					TimokLogger.Instance.LogRbr(LogSeverity.Error, "ScriptFactory.Run, Error: ScriptInfo == null");
					return false;
				}

				var _script = createScript();
				return _script.Run();
			}
			catch (Exception _ex) {
				TimokLogger.Instance.LogRbr(LogSeverity.Critical, "SessionDispatcher.create", string.Format("SessionDispatcher.Run, Exception:\r\n{0}", _ex));
			}
			return false;
		}

		IScript createScript() {
			var _script = ObjectFactory<ScriptType, IScript>.Create(script.ScriptType, new object[] { script, session, callStatistics });
			if (_script == null) {		// not in cache 
				var _typeName = string.Format("Timok.IVR.Scripting.{0}CallScript", script.ScriptType);
				
				var _type = Assembly.GetCallingAssembly().GetType(_typeName);
				if (_type == null) {
					TimokLogger.Instance.LogRbr(LogSeverity.Error, "ScriptFactory.createinstance", string.Format("Error creating type={0}", _typeName));
					return null;
				}

				ObjectFactory<ScriptType, ISessionHandler>.Add(script.ScriptType, _type);
				_script = ObjectFactory<ScriptType, IScript>.Create(script.ScriptType, new object[] { script, session });
			}

			return _script;
		}
	}
}
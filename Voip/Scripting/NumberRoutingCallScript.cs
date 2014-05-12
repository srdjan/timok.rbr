using System;
using System.Collections.Generic;
using System.IO;
using System.Threading;
using Timok.Core.Logging;
using Timok.Rbr.Core;
using Timok.Rbr.Core.Config;

using T = Timok.Core.Logger.TimokLogger;

namespace Timok.IVR.Scripting {
	internal class NumberRoutingCallScript : ScriptBase {
		static readonly object padlock = new object();
		static readonly int[] pressOneMenuChoice = new[] { 1 };

		static Dictionary<string, string> routingTable;
		static int callCount;

		static NumberRoutingCallScript() {
			T.LogRbr(LogSeverity.Status, "NumberRoutingCallScript.StaticCtor", string.Format("RoutingTable file: {0}", Configuration.IVR.NumberRoutingFilePath));
			NumberRoutingState.ShouldReload = true;
			callCount = 0;
			loadMappingTable();
		}

		public NumberRoutingCallScript(ScriptInfo pScript, ISession pSession) : base(pScript, pSession) {
			T.LogRbr(LogSeverity.Status, "NumberRoutingCallScript.Ctor", string.Format("AccessNumber: {0}", pSession.AccessNumber));
			Info.AuthenticationType = ScriptAuthenticationType.None;
		}

		protected override void ExecuteScript() {
			//REFACTOR: needs new implementation!
			T.LogRbr(LogSeverity.Error, "NumberRoutingCallScript.ExecuteScript", "Not Activated!, reject");
			throw new Exception("Number Routing Not Activeted!");
			if (callCount > 2) {
				T.LogRbr(LogSeverity.Debug, "NumberRoutingCallScript.ExecuteScript", "More then 2 calls active!, reject");
				return;			
			}

			Interlocked.Increment(ref callCount);
			try {
				#region TODO: 
				/*
				if ((DateTime.Now.Minutes % 5 == 0) {
					reloadMapingTable();
				}
				*/
				#endregion

				string[] _mappingFields = getMappingFields();
				if (_mappingFields != null) {
					T.LogRbr(LogSeverity.Debug, "NumberRoutingCallScript.ExecuteScript", "Main menu choice");
					if (GetUserChoice(PromptManager.MainMenuChoicePrompt(pressOneMenuChoice), pressOneMenuChoice, 1) == 1) {
						T.LogRbr(LogSeverity.Debug, "NumberRoutingCallScript.ExecuteScript", "Making Call out");
						MakeCall();
					}

					T.LogRbr(LogSeverity.Debug, "NumberRoutingCallScript.ExecuteScript", "Call ended");
				}
				else {
					T.LogRbr(LogSeverity.Error, "NumberRoutingCallScript.ExecuteScript", "RoutingTable is EMPTY");
				}
			}
			finally {
				Interlocked.Decrement(ref callCount);
			}
		}

		//----------------------------------- Private -------------------------------------------------
		string[] getMappingFields() {
			if (routingTable.Count > 0) {
				string _routingPair = routingTable[Session.AccessNumber];
				return _routingPair.Split('-');
			}
			return null;
		}

		static void loadMappingTable() {
			lock (padlock) {
				if ( ! NumberRoutingState.ShouldReload) {
					T.LogRbr(LogSeverity.Debug, "NumberRoutingCallScript.ExecuteScript", "RoutingTable should not reload, no changes present.");
					return;
				}
				NumberRoutingState.ShouldReload = false;
				
				routingTable = new Dictionary<string, string>();

				if (File.Exists(Configuration.IVR.NumberRoutingFilePath)) {
					using (TextReader _tr = new StreamReader(Configuration.IVR.NumberRoutingFilePath)) {
						string _line;
						while (( _line = _tr.ReadLine() ) != null) {
							if (_line.Length <= 0) {
								T.LogRbr(LogSeverity.Status, "NumberRoutingCallScript.StaticCtor", "Empty Line!");
								break;
							}
							string[] _fields = _line.Split(',');
							routingTable.Add(_fields[0], _fields[1]);
						}
					}
				}
			}
		}
	}
}
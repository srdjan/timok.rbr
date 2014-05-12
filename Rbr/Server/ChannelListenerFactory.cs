using System;
using System.IO;
using System.Threading;
using Timok.Core;
using Timok.Logger;
using Timok.Rbr.Core;
using Timok.Rbr.Core.Config;
using Timok.Rbr.Service;

namespace Timok.Rbr.Server {
	static class ChannelListenerFactory {
		static ChannelListener[] channelListenerList;
		static IIVRPlugin ivr;

		public static void Create(string pRbrProcessFilePath, SessionDispatcher pSessionDispatcher) {
			try {
				if (channelListenerList != null) {
					throw new Exception("ChannelListenerFactory.Create was already called!");
				}

				var _rbrBin = Path.Combine(pRbrProcessFilePath, "bin") ;
				var _ivrDllPath = Path.Combine(_rbrBin, "Timok.Ivr.dll");
				var _ivrAssembly = Utils.TryLoadIvrAssembly(_ivrDllPath);
				ivr = (IIVRPlugin) Activator.CreateInstance(_ivrAssembly.GetType("Timok_IVR.IVR"));
				ivr.Start(Configuration.Instance.IVR as IVRConfiguration);

				var _numberOfChannels = 0;
				channelListenerList = new ChannelListener[Configuration.Instance.IVR.MaxNumberOfCallsPerModule * Configuration.Instance.IVR.NumberOfModules];
				for (var _countModules = 0; _countModules < Configuration.Instance.IVR.NumberOfModules; _countModules++) {
					for (var _countChannels = 0; _countChannels < Configuration.Instance.IVR.MaxNumberOfCallsPerModule; _countChannels++) {
						channelListenerList[_numberOfChannels] = new ChannelListener(ivr, _countModules, _numberOfChannels, pSessionDispatcher);
						_numberOfChannels++;
					}
				}
			  Thread.Sleep(3000);
			}
			catch(Exception _ex) {
				throw new Exception(string.Format("ChannelListenerFactory.Start, exception: {0}", _ex));
			}
		}

		public static void Destroy() {
			ivr.Shutdown = true;
			Thread.Sleep(1000);

			var _complete = false;
			for (var _i = 0; _i < 10; _i++) {
				Thread.Sleep(1000);
				if (ivr.NumberOfChannels == 0) {
					_complete = true;
					break;
				}
			}

			if (! _complete) {
				TimokLogger.Instance.LogRbr(LogSeverity.Critical, "ChannelListenerFactory.Destroy:", "NOT All Calls have cleared !!!");
			}

			if (ivr == null) {
				TimokLogger.Instance.LogRbr(LogSeverity.Critical, "ChannelListenerFactory.Destroy:", "IVR == null");
			}
			else {
				ivr.Stop();
				ivr = null;
			}
		}
	}	
}

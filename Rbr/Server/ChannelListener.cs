using System;
using System.Threading;
using Timok.Rbr.Core;
using Timok.Rbr.Core.Config;

namespace Timok.Rbr.Server {
	class ChannelListener {
		static readonly object padlock = new object();
		readonly IIVRPlugin ivr;
		readonly ISessionDispatcher sessionDispatcher;
		readonly int moduleNumber;
		readonly int channelNumber;
		readonly ISessionChannel channel;
		readonly Thread thread;

		public ChannelListener(IIVRPlugin pIVR, int pModuleNumber, int pChannelNumber, ISessionDispatcher pSessionDispatcher) {
			ivr = pIVR;
			moduleNumber = pModuleNumber;
			channelNumber = pChannelNumber;
			sessionDispatcher = pSessionDispatcher;

			channel = ivr.CreateChannel(moduleNumber, channelNumber);

			thread = new Thread(channelListener, Convert.ToInt32(Configuration.Instance.IVR.ThreadStackSize));
      thread.Name = string.Format("Channel_{0}{1}", moduleNumber, channelNumber);
			thread.Start();
		}

		//----------------------------------- Private 
		void channelListener() {
			lock (padlock) {
				ivr.NumberOfChannels++;
			}

			while (! ivr.Shutdown) {
				if (!channel.WaitForCall()) {
					channel.DisconnectLeg1();
					continue;
				}

				if (!sessionDispatcher.Run(channel)) {
					channel.DisconnectLeg1();
				}
			}

			lock (padlock) {
				ivr.NumberOfChannels--;
			}
		}
	}
}

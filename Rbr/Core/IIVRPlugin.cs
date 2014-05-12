using Timok.Rbr.Core.Config;

namespace Timok.Rbr.Core {
	public interface IIVRPlugin {
		int NumberOfChannels { get; set; }
		void Start(IVRConfiguration pIVRConfiguration);
		void Stop();
		bool Shutdown { get; set; }
		ISessionChannel CreateChannel(int pModuleNumber, int pChannelNumber);
	}
}
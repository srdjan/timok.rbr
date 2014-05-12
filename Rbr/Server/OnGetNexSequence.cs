using Timok.Rbr.Core;

namespace Timok.Rbr.Server {
	public class OnGetNextSequence : UdpCommand {
		public OnGetNextSequence(IRbrDispatcher pDispatcher) : base() { 
			Dispatcher = pDispatcher;
			nameAndVersion = RbrApi.OnGetNextSequence;
		}

		protected override void execute() {
			var _nextSequence = Dispatcher.GetNextSequence();
			AddResponseField("true", false);
			AddResponseField(_nextSequence.ToString(), true);
		}
	}
}

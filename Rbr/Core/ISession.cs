using Timok.Rbr.Core.Config;

namespace Timok.Rbr.Core {
	public interface ISession {
		string Id { get; }
		ISessionChannel Channel { get; }
		SessionState State { get; set; }

		string CallId { get; }
		string OrigIPAddress { get; }
		string AccessNumber { get; set;}
		byte InfoDigits { get; }
		string ANI { get; }
		string DestNumber { get; set; }
		string CardNumber { get; set; }
		short CustomerAcctId { get; set; }	//TODO: remove from this interface - legIn already has it!
	}
}
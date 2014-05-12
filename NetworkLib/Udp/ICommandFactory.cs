
namespace Timok.NetworkLib.Udp {
	public interface ICommandFactory {
		string APIName { get; }
		ICommand GetCommand(string pFullRequest);
	}
}
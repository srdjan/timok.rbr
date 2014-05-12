namespace Timok.NetworkLib.Tcp {
	public interface IChannel	{
		//void Connect();
		//void Disconnect();
		void Send(string pRequest);
		string Receive(int pLength);
	}
}
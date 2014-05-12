namespace Timok.NetworkLib.Tcp {
	public interface IPoolableChannel : IChannel	{
		string IPAndPort { get; }
		bool Acquire();
		bool Release();
		int Index { get; } 
	}
}
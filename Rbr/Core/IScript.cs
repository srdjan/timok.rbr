namespace Timok.Rbr.Core {
	public interface IScript {
		ScriptInfo Script { get; }
		bool Run();
	}
}
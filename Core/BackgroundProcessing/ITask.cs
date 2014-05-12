namespace Timok.Core.BackgroundProcessing {
	public interface ITask {
    BackgroundWorker Host { set; }
		void Run(object pSender, WorkerEventArgs pArgs);
		void CancelAsync();
	}
}

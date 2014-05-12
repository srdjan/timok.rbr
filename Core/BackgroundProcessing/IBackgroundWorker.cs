namespace Timok.Core.BackgroundProcessing {
	public interface IBackgroundWorker {
		bool CancellationPending { get; }
		void ReportProgress(int pPercent);
		void ReportStatus(string pStatus);
	}

	public class DummyBackgroundWorker : IBackgroundWorker {
		public bool CancellationPending { get { return false; } }

		public void ReportProgress(int pPercent) {
			return;
		}

		public void ReportStatus(string pStatus) {
			return;
		}
	}
}
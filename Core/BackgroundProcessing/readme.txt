	
implementation example for XTask object
	
	
	public class TestTask	: ITask {
		public TestTask() { }

		private BackgroundWorker host;
		public BackgroundWorker Host {
			get { return host; }
			set { host = value; } 
		}

		public void Run(object sender, WinFormsEx.DoWorkEventArgs e) {
			int count = (int) e.Argument;
			e.Result = null;

			for (int progress=0; progress <= count; progress += count/10) {
				if (host.CancellationPending) {
					e.Cancel = true;
					break;
				}

				//-- do some work
				System.Threading.Thread.Sleep(500);

				host.ReportProgress(progress);
				host.ReportStatus(progress.ToString());
			}       
		}	
	}

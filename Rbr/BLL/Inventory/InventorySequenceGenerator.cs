using System.IO;

namespace Timok.Rbr.BLL.Inventory {
	public class InventorySequenceGenerator {
		object padlock = new object();
		string filePath;
		long sequence;

		private InventorySequenceGenerator() { }

		public InventorySequenceGenerator(string pFilePath) {
			filePath = pFilePath;      
			sequence = read();
		}
	
		public long GetNextSequence() {
			lock (padlock) {
				write(++sequence);
				return sequence;
			}
		}

		public void SetNextSequence(long pSequence) {
			lock (padlock) {
				sequence = pSequence;
				write(pSequence);
			}
		}

		public long GetCurrentSequence() {
			return sequence;
		}

		//------------------------------- Private ------------------------------------
		private long read() {
			long _sequence = 0;

			if (File.Exists(filePath)) {
				using (TextReader _tr = new StreamReader(filePath)) {
					_sequence = long.Parse(_tr.ReadLine());
				}
			}
			return _sequence;
		}

		private void write(long pSequence) {
			using (var _fs = new FileStream(filePath, FileMode.OpenOrCreate, FileAccess.Write)) {
				using(var _sw = new StreamWriter(_fs)) {
					_sw.WriteLine(pSequence.ToString());
				}
			}
		}
	}
}
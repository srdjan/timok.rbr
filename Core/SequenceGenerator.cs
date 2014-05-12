using System;
using System.Collections;
using System.IO;

namespace Timok.Core {
	public class SequenceGenerator {
		static readonly SortedList filePaths;
		static readonly object padlock = new object();
		readonly string filePath;
		long sequence;

		static SequenceGenerator() {
			filePaths = new SortedList();
		}

		public SequenceGenerator(string pFilePath) {
			filePath = pFilePath;

			var _key = filePath.GetHashCode();
			if (filePaths.Contains(_key)) {
				throw new Exception("Sequence Generator for: " + filePath + " already exist");
			}
			filePaths.Add(_key, filePath);

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
		long read() {
			long _sequence = 0;

			if (File.Exists(filePath)) {
				using (TextReader _tr = new StreamReader(filePath)) {
					_sequence = long.Parse(_tr.ReadLine());
				}
			}
			return _sequence;
		}

		void write(long pSequence) {
			using (var _fs = new FileStream(filePath, FileMode.OpenOrCreate, FileAccess.Write)) {
				using (var _sw = new StreamWriter(_fs)) {
					_sw.WriteLine(pSequence);
				}
			}
		}
	}
}
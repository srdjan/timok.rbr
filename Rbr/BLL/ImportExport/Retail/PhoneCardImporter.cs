using System;
using System.Collections;
using System.Diagnostics;
using System.IO;
using Timok.Core.BackgroundProcessing;
using Timok.Logger;
using Timok.Rbr.BLL.Controllers;
using Timok.Rbr.DTO;

namespace Timok.Rbr.BLL.ImportExport.Retail {
	public class PhoneCardImporter : ITask {
		string filePath;
		PhoneCardBatch phoneCardBatch;

		#region ITask Members

		BackgroundWorker host;
		public BackgroundWorker Host { get { return host; } set { host = value; } }

		public void Run(object sender, WorkerEventArgs e) {
			var _args = (PhoneCardImporterArgs) e.Argument;
			filePath = _args.FilePath;
			phoneCardBatch = _args.PhoneCardBatch;
			try {
				import();
				save();
			}
			catch (Exception _ex) {
				e.Cancel = true;
				e.Result = new Exception("Import failed. \r\nError: " + _ex.Message, _ex);
				TimokLogger.Instance.LogRbr(LogSeverity.Critical, "PhoneController.Run", string.Format("Import failed. Exception: {0}", _ex));
			}
		}

		public void CancelAsync() {
			host.CancelAsync();
		}

		void onProgressChanged(int pProgressPercentage) {
			if (host != null) {
				host.ReportProgress(pProgressPercentage);
			}
		}

		void onStatusChanged(string pStatus) {
			if (host != null) {
				host.ReportStatus(pStatus);
			}
		}

		void onWorkCompleted(WorkCompletedEventArgs e) {}

		#endregion

		//FORMAT
		//SerialNumber|PIN

		void import() {
			try {
				int _totalLineCount;
				countLines(filePath, out _totalLineCount);

				using (StreamReader _sr = new StreamReader(filePath)) {
					phoneCardBatch.PhoneCards = new ArrayList();
					checkPendingCancellation();
					onStatusChanged("Processing File...");

					int _index = 0;
					string _line;
					while (( _line = _sr.ReadLine() ) != null) {
						checkPendingCancellation();
						parsePhoneCard(_line);
						_index++;
						onProgressChanged(_index * 100 / _totalLineCount);
						if (_index % 100 == 0) {
							onStatusChanged("Processed File Line: " + _index);
						}
					}
				}
			}
			catch (Exception _ex) {
				TimokLogger.Instance.LogRbr(LogSeverity.Critical, "PhoneCardController.import", string.Format("import exception: {0}", _ex));
				throw;
			}
		}

		void save() {
			Debug.WriteLine("PhoneCards.Count = " + phoneCardBatch.PhoneCards.Count);
			onStatusChanged("Importing Data...");
			onProgressChanged(0);
			RetailAccountController.Import(phoneCardBatch, host);
		}

		//-------------------------------- Privates ------------------------------------------------

		void checkPendingCancellation() {
			if (host != null && host.CancellationPending) {
				throw new Exception("Import canceled");
			}
		}

		void parsePhoneCard(string pLine) {
			string[] _fields = pLine.Split(Constants.CommaDelimiter[0]);

			PhoneCardDto _phoneCard = new PhoneCardDto();
			_phoneCard.SerialNumber = long.Parse(_fields[0]);
			_phoneCard.Pin = long.Parse(_fields[1]);
			_phoneCard.ServiceId = phoneCardBatch.ServiceId;
			_phoneCard.InventoryStatus = phoneCardBatch.InventoryStatus;

			phoneCardBatch.PhoneCards.Add(_phoneCard);
		}

		void countLines(string pFilePath, out int pTotalLineCount) {
			pTotalLineCount = 0;
			using (StreamReader _sr = new StreamReader(pFilePath)) {
				while (_sr.Peek() != -1) {
					_sr.ReadLine();
					pTotalLineCount++;
				}
			}
		}
	}
}
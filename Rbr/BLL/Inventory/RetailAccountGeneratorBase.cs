using System;
using System.Data.Hsql;
using System.IO;
using System.Security.Cryptography;
using Timok.Rbr.Core.Config;
using WinFormsEx;

namespace Timok.Rbr.BLL.Inventory {
	public abstract class RetailAccountGeneratorBase : IRetailAccountGenerator {
		private const string allowedChars = "1234567890";
		protected int counter;
		protected RNGCryptoServiceProvider rand;
		protected InventorySequenceGenerator serialGenerator;
		protected InventorySequenceGenerator batchIdGenerator;

		public RetailAccountGeneratorBase() {
			counter = 0;
			rand = new RNGCryptoServiceProvider();
			serialGenerator = new InventorySequenceGenerator(Path.Combine(Configuration.Instance.Folders.InventoryFolder, "last_serial.txt"));
			batchIdGenerator = new InventorySequenceGenerator(Path.Combine(Configuration.Instance.Folders.InventoryFolder, "last_batchId.txt"));
		}

		protected bool generateBatch(CancelEventArgs pEvtArg, BackgroundWorker pBWGenCards, int pBatchId, int pBatchSize, int pPinLength, out string[] pCardArray) {
			var _result = false;
			pCardArray = new string[pBatchSize];
			var _cardArrayIndex = 0;
			var _numberOfGeneratedCards = 0;

			try {
				using (var _conn = new SharpHsqlConnection("Initial Catalog=.;User Id=sa;Pwd=;")) {
					_conn.Open();
					SharpHsqlTransaction _tran = _conn.BeginTransaction();
					var _cmd = new SharpHsqlCommand("", _conn);
					int _dupsCounter = 0;

					try {
						while (_numberOfGeneratedCards < pBatchSize) {
							if (pBWGenCards.CancellationPending) {
								pEvtArg.Cancel = true;
								_tran.Rollback();
								return false;
							}

							long _pin = createRandomNumber(pPinLength);
							string _cardNumber = _pin.ToString("d" + pPinLength);
							if (! isValidCheckDigit(_cardNumber)) {
								continue;
							}

							try {
								_cmd.CommandText = "INSERT INTO \"CardInventory\" (\"pan\") VALUES (" + _pin + ");";
								_cmd.ExecuteNonQuery();
							}
							catch (Exception _ex) {
								if (_ex.Message.IndexOf("23000") == 0) {
									if (++_dupsCounter%100 == 0) {
										pBWGenCards.ReportStatus("Dups: " + (-_dupsCounter));
									}
									continue;
								}
								pBWGenCards.ReportStatus("INSERT Exception: " + _ex.Message);
								throw;
							}
							_numberOfGeneratedCards++;
							pCardArray[_cardArrayIndex++] = _cardNumber;
							if (++counter%1000 == 0) {
								pBWGenCards.ReportProgress(counter);
							}
						}
						_tran.Commit();
						pBWGenCards.ReportStatus("Completed BatchId: " + pBatchId.ToString("D6") + " Number Of cards: " + pBatchSize);
						_result = true;
					}
					catch (Exception _ex) {
						_tran.Rollback();
						pBWGenCards.ReportStatus("Generate(loop) Card Numbers Exception: " + _ex.Message);
					}
				}
			}
			catch (Exception _ex) {
				pBWGenCards.ReportStatus("Generate Card Numbers Exception: " + _ex.Message);
			}
			return _result;
		}

		protected void writeBatchToFile(string pFilePath, string[] pCardNumberArray, long pFirstSerial) {
			using (var _sw = new StreamWriter(pFilePath, true)) {
				_sw.AutoFlush = false;

				for (int _j = 0; _j < pCardNumberArray.Length; _j++) {
					_sw.WriteLine((pFirstSerial + _j).ToString("d10") + InventoryController.FieldDelimiter + pCardNumberArray[_j]);
				}
				_sw.Flush();
			}
		}


		private bool isValidCheckDigit(string CreditCardNumber) {
			// Credit Cards return a 0 as a valid check digit
			// LUHN: calculate check digit and Mod 10 for validation 
			// Used for Credit Cards, Merchant Numbers, and other validation
			int cd = 0, b, t, NumLength = CreditCardNumber.Length - 1;
			for (int x = NumLength; x >= 0; x--) {
				b = Int32.Parse(CreditCardNumber[x].ToString());
				t = ((NumLength - x)%2 == 0) ? b : b*2;
				cd += (t > 9) ? t - 9 : t;
			}
			return (cd%10 == 0) ? true : false;
		}

		private long createRandomNumber(int pLength) {
			var _randomBytes = new byte[pLength];
			rand.GetBytes(_randomBytes);
			var _chars = new char[pLength];
			int _allowedCharCount = allowedChars.Length;

			for (int _i = 0; _i < pLength; _i++) {
				_chars[_i] = allowedChars[_randomBytes[_i]%_allowedCharCount];
			}
			return long.Parse(new string(_chars));
		}

		public abstract void Generate(object sender, DoWorkEventArgs pArg);
	}
}
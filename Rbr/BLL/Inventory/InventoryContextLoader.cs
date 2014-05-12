using System;
using System.Data.Hsql;
using System.Diagnostics;
using System.IO;
using Timok.Rbr.Core.Config;
using WinFormsEx;

namespace Timok.Rbr.BLL.Inventory {
	public class InventoryContextLoader {
		readonly int pinLength;
		readonly string serviceFolder;
		readonly short serviceId;

		public InventoryContextLoader(short pServiceId, int pPinLength) {
			createMainDb();

			serviceId = pServiceId;
			serviceFolder = Configuration.Instance.Folders.GetInventoryServiceFolder(serviceId); //Path.Combine(Folders.InventoryFolder, serviceId.ToString("00000"));

			pinLength = pPinLength;
		}

		public void LoadContext(object sender, DoWorkEventArgs pArg) {
			var bwLoadDb = sender as BackgroundWorker;
			Debug.Assert(bwLoadDb != null);
			pArg.Result = true;
			pArg.Cancel = false;

			int _counter = 0;
			const int _dupsCounter = 0;

			if (Directory.Exists(serviceFolder)) {
				foreach (string _lotFolder in Directory.GetDirectories(serviceFolder)) {
					string[] _files = Directory.GetFiles(_lotFolder, "*.txt");
					foreach (string _fileName in _files) {
						bwLoadDb.ReportStatus(string.Format("Loaded File: {0}", _fileName.Substring(_fileName.LastIndexOf(@"\") + 1)));
						if (bwLoadDb.CancellationPending) {
							pArg.Cancel = true;
							return;
						}

						try {
							using (var _conn = new SharpHsqlConnection("Initial Catalog=.;User Id=sa;Pwd=;")) {
								_conn.Open();
								using (SharpHsqlTransaction _tran = _conn.BeginTransaction()) {
									var _cmd = new SharpHsqlCommand("", _conn);

									using (StreamReader _sr = File.OpenText(Path.Combine(_lotFolder, _fileName))) {
										try {
											string _input;
											while ((_input = _sr.ReadLine()) != null) {
												if (bwLoadDb.CancellationPending) {
													pArg.Cancel = true;
													throw new Exception("Canceled");
												}
												if (++_counter % 1000 == 0) {
													bwLoadDb.ReportProgress(_counter);
												}

												if (!char.IsDigit(_input[0])) {
													bwLoadDb.ReportStatus(string.Format("Not number ?: {0} file: {1}", _input[0], _fileName));
													throw new Exception("Serial NOT a number");
												}

												string[] _fields = _input.Split(InventoryController.FieldDelimiter);
												if (_fields[1].Length != pinLength) {
													bwLoadDb.ReportStatus(string.Format("Invalid PIN Length: {0} file: {1}", _input[0], _fileName));
													throw new Exception("Invalid PIN Length");
												}

												long _pan;
												long.TryParse(_fields[1], out _pan);
												if (_pan == 0) {
													bwLoadDb.ReportStatus(string.Format("PIN Not a number? Pin: {0}, file: {1}", _fields[1], _fileName));
													throw new Exception("PIN NOT a number");
												}

												try {
													_cmd.CommandText = "INSERT INTO \"CardInventory\" (\"pan\") VALUES (" + _pan + ");";
													_cmd.ExecuteNonQuery();
												}
												catch (Exception _ex) {
													bwLoadDb.ReportStatus(String.Format("INSERT Exception: {0}", _ex.Message));
													throw;
												}
											}
										}
										catch {
											_tran.Rollback();
											throw;
										}
										_tran.Commit();
									}
								}
							}
						}
						catch (Exception _ex) {
							pArg.Result = false;
							bwLoadDb.ReportStatus("Exception Processing file: " + _fileName + " [" + _ex.Message + "]");
							break;
						}
					}
				}
			}
			bwLoadDb.ReportStatus("Total Scaned: " + _counter + " Dups: " + _dupsCounter + " Loaded: " + (_counter - _dupsCounter));
		}

		//------------------------------------- Private -------------------------------------------------------
		string createMainDb() {
			try {
				using (var _conn = new SharpHsqlConnection("Initial Catalog=.;User Id=sa;Pwd=;")) {
					_conn.Open();

					//-- drop the table if it already exist, and then create new:
					var _cmd = new SharpHsqlCommand("", _conn);
					_cmd.CommandText = "DROP TABLE IF EXIST \"CardInventory\";";
					_cmd.CommandText += "CREATE CACHED TABLE \"CardInventory\" (\"pan\" BIGINT NOT NULL PRIMARY KEY);";
					_cmd.ExecuteNonQuery();
				}
			}
			catch (Exception _ex) {
				return _ex.Message;
			}
			return "Main Db created";
		}
	}
}
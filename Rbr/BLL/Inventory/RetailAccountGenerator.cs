using System;
using System.Diagnostics;
using System.IO;
using WinFormsEx;

namespace Timok.Rbr.BLL.Inventory {
	public class RetailAccountGenerator : RetailAccountGeneratorBase {
		public override void Generate(object sender, DoWorkEventArgs pArg) {
			var bwGenCards = sender as BackgroundWorker;
			Debug.Assert(bwGenCards != null);
			pArg.Result = false;
			pArg.Cancel = false;

			var _request = (RetailAccountGenRequest) pArg.Argument;

			long _firstSerial = serialGenerator.GetNextSequence();
			long _lastSerial = _firstSerial;

			string[] _cardNumberArray = null;
			long _batchIdLong = batchIdGenerator.GetNextSequence();
			if (_batchIdLong >= int.MaxValue) {
				//TODO: log and return instead of throwing ?
				throw new Exception("BatchId out of Range");
			}
			var _firstBatchId = (int) _batchIdLong;
			bool _result = true;

			//-- generate cards
			int _batchListIndx = 0;
			var _batchList = new Batch[_request.NumberOfBatches];
			for (int _batchId = _firstBatchId; _batchId < _firstBatchId + _request.NumberOfBatches; _batchId++) {
				if (generateBatch(pArg, bwGenCards, _batchId, _request.BatchSize, _request.PinLength, out _cardNumberArray)) {
					string _batchFilePath = _request.BatchFilePath(_batchId);
					writeBatchToFile(_batchFilePath, _cardNumberArray, _firstSerial);
					_lastSerial = _firstSerial + _cardNumberArray.Length - 1;
					_batchList[_batchListIndx++] = new Batch(_batchId, _firstSerial, _lastSerial);

					_firstSerial = _lastSerial + 1; //set FirstSerial here for the next batch
				}
				else {
					_result = false;
					for (int _i = _firstBatchId; _i < _batchId; _i++) {
						string _batchFilePath = _request.BatchFilePath(_i);
						File.Delete(_batchFilePath);
						bwGenCards.ReportStatus("Deleted: " + _batchFilePath);
					}
					break;
				}
			}
			pArg.Result = new RetailAccountGenResponse(_request, _result, _batchList);

			//-- check result
			if (_result) {
				batchIdGenerator.SetNextSequence(_firstBatchId + _request.NumberOfBatches);
				serialGenerator.SetNextSequence(_lastSerial);
			}
			else {
				bwGenCards.ReportStatus("Error");
			}
		}
	}
}
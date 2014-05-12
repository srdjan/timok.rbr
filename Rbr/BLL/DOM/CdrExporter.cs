using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Threading;
using Timok.Core;
using Timok.Logger;
using Timok.Rbr.BLL.Entities;
using Timok.Rbr.Core;
using Timok.Rbr.Core.Config;
using Timok.Rbr.DAL.CdrDatabase;

namespace Timok.Rbr.BLL.DOM {
	public sealed class CdrExporter {
		readonly IConfiguration configuration;
		readonly ILogger T;
		readonly int frequency; // in minutes
		Timer exportTimer { get; set; }

		public CdrExporter(int pFrequency, IConfiguration pConfiguration, ILogger pLogger) {
			configuration = pConfiguration;
			T = pLogger;
			frequency = pFrequency;
			exportTimer = new Timer(export, null, exportTimerInterval, Timeout.Infinite);
		}

		//------------------------- Private Methods --------------------------------------------------------
		void export(object pState) {
			Thread.Sleep(0); //Fix for SP1 bug, when timer stops firing after a while

			try {
				var _now = DateTime.Now;
				T.LogRbr(LogSeverity.Status, "CdrExporter.export", string.Format("Started: {0}", _now.ToString("MM/dd/yyyy HH:mm:ss.fff")));
				CDRRow[] _cdrRows = null;
				//retry 3 times to get CDRs
				for (var _retryCount = 1; _retryCount <= 3; _retryCount++) {
					try {
						_cdrRows = getCDRFromDb(_now);
						break; //all ok - exit retry loop
					}
					catch (Exception _sqlEx) {
						T.LogRbr(LogSeverity.Critical, "CdrExporter.export", string.Format("Failed to get CDRs, count: {0}, Exception:\r\n{1}", _retryCount, _sqlEx));
						Thread.Sleep(15000);
					}
				}

				if (_cdrRows == null || _cdrRows.Length == 0) {
					T.LogRbr(LogSeverity.Status, "CdrExporter.export", string.Format("No CDR records found in db for previous period: {0}", _now.ToString("MM/dd/yyyy HH:mm:ss.fff")));
					return;
				}

				saveToFile(_now, _cdrRows);
			}
			catch (Exception _ex) {
				T.LogRbr(LogSeverity.Critical, "RbrExporter.export", string.Format("Exception:\r\n{0}", Utils.GetFullExceptionInfo(_ex)));
			}
			finally {
				exportTimer = new Timer(export, null, exportTimerInterval, Timeout.Infinite);
			}
		}

		CDRRow[] getCDRFromDb(DateTime pNow) {
			var _endDateLogged = new DateTime(pNow.Year, pNow.Month, pNow.Day, pNow.Hour, pNow.Minute, 0, 0).Subtract(TimeSpan.FromMinutes(1));
			var _temp = _endDateLogged.Subtract(TimeSpan.FromMinutes(frequency - 1));
			var _startDateLogged = new DateTime(_temp.Year, _temp.Month, _temp.Day, _temp.Hour, _temp.Minute, 0, 0);
			var _previousDayDate = _startDateLogged.AddDays(-1);

			validateTimer(pNow, _startDateLogged, _endDateLogged);

			var _cdrList = new List<CDRRow>();
			using (var _db = new Cdr_Db(_startDateLogged)) {
				_cdrList.AddRange(_db.CDRCollection.GetByDateLoggedRange(_startDateLogged, _endDateLogged));
			}
			if (_previousDayDate.Month != _startDateLogged.Month) {
				//on the Month change, pick CDRs from previous month's Db by the same DateLoggedRange
				using (var _db = new Cdr_Db(_previousDayDate)) {
					_cdrList.AddRange(_db.CDRCollection.GetByDateLoggedRange(_startDateLogged, _endDateLogged));
				}
			}
			T.LogRbr(LogSeverity.Status, "CdrExporter.getCDRFromDb", string.Format("Got call records count: {0}", _cdrList.Count));

			return _cdrList.ToArray();
		}

		void saveToFile(DateTime pNow, CDRRow[] pCDRRows) {
			var _targetNodes = Node.GetNodes(NodeRole.Admin);
			if (_targetNodes == null || _targetNodes.Length != 1) {
				T.LogRbr(LogSeverity.Critical, "CdrExporter.saveToFile", "CdrExport: Admin server doesn't exist");
				return;
			}

			var _folder = Path.Combine(configuration.Folders.CdrPublishingFolder, _targetNodes[0].UserName);
			if (! Directory.Exists(_folder)) {
				Directory.CreateDirectory(_folder);
			}

			//-- (1) Create a FULL AND 'temp' file name:
			var _fileName = (new CurrentNode()).Id.ToString("D2") + "-" + pNow.ToString(AppConstants.CdrFileNameFormat);
			var _filePath = Path.Combine(_folder, _fileName + AppConstants.CdrFileExtension);
			if (File.Exists(_filePath)) {
				T.LogRbr(LogSeverity.Critical, "CdrExporter.saveToFile", string.Format("File already exist: {0}", _filePath));
				return;
			}

			//-- (2) Serialize and save:
			using (Stream _stream = new FileStream(_filePath, FileMode.Create)) {
				IFormatter _formatter = new BinaryFormatter(); //SoapFormatter();
				_formatter.Serialize(_stream, pCDRRows);
				_stream.Flush();
				_stream.Close();
			}

			//-- (3) rename (by appending ".pending")
			FileHelper.AddExtension(_filePath, AppConstants.PendingExtension);
		}

		int exportTimerInterval {
			get {
				var _minutes = DateTime.Now.Minute;
				var _seconds = DateTime.Now.Second;

				//-- calculate timeout in minutes
				int _timeout = frequency - _minutes % frequency;
				if (_timeout == 0) {
					_timeout = frequency;
				}

				//-- convert minutes to seconds
				_timeout *= 60;

				//-- add 20 second offset
				if (_seconds <= 20) {
					_timeout += 20;
				}
				else if (_seconds > 20) {
					_timeout -= _seconds - 20;
				}

				//-- convert to miliseconds
				_timeout *= 1000;
				return _timeout;
			}
		}

		void validateTimer(DateTime pNow, DateTime pStart, DateTime pStop) {
			//TODO: make sure we got correct CDR db for the month at the change of the month
			//TODO: if months are different we should go for CDRs in to two CDRDBs
			//very low probability, but, this could heppen if timer fires at the wrong minute
			T.LogRbr(LogSeverity.Status, "CdrExporter.validateTimer", string.Format("Frequency: {0}", frequency));
			T.LogRbr(LogSeverity.Status, "CdrExporter.validateTimer", string.Format("DateLogged between: {0} AND {1}", pStart.ToString("MM/dd/yyyy HH:mm:ss.fff"), pStop.ToString("MM/dd/yyyy HH:mm:ss.fff")));

			if (pNow.Minute % frequency != 0) {
				T.LogRbr(LogSeverity.Critical, "CdrExporter.validateTimer", string.Format("Start minute should be multiple of 'frequency'. Minute:={0}, Frequency={1}", pNow.Minute, frequency));
			}

			Debug.Assert(pStart.Month == pStop.Month, "Start Date and End Date should have same Month!!!");
			if (pStart.Month != pStop.Month) {
				T.LogRbr(LogSeverity.Critical, "CdrExporter.validateTimer", string.Format("Start Date and End Date should have same Month!!! {0}, {1}", pStop.Month, pStop.Month));
			}
		}
	}
}
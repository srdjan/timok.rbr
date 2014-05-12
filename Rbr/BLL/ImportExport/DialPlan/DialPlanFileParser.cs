using System;
using System.Collections.Generic;
using System.IO;
using Timok.Core.BackgroundProcessing;
using Timok.Logger;
using Timok.Rbr.Core.Config;

namespace Timok.Rbr.BLL.ImportExport.DialPlan {
	public class DialPlanFileParser {
		readonly IBackgroundWorker host;

		public DialPlanFileParser(IBackgroundWorker pHost) { host = pHost; }

		public IBackgroundWorker Host { get { return host ?? new DummyBackgroundWorker(); } }

		/*
     * DialCodes
    Brazil|55|ROC_CEL
    551281234512|551281234512|551281234512|551281234512|551281234512|551281234512
    551281234512|551281234512
    Brazil|55|ROC_LOCAL
    551281234512|551281234512|551281234512|551281234512|551281234512|551281234512
    551281234512|551281234512|551281234512|551281234512|551281234512|551281234512
    551281234512|551281234512
    Mexico|52|Acapoulco
    551281234512|551281234512|551281234512|551281234512|551281234512|551281234512
    551281234512|551281234512|551281234512|551281234512|551281234512|551281234512
		
     * or Rates
		
    //TODO: TO BE REMOVED old format
    Brazil|55|55_ROC_LOC|R|30/6|0am|0.1320000,0.0264000
    Brazil|55|55_ROC_LOC|W|30/6|0am,2pm|0.1110000,0.1164000|0.2220000,0.2264000
    Brazil|55|55_ROC_LOC|H|30/6|0am,2pm,10pm|0.1110000,0.1164000|0.2220000,0.2264000|0.3330000,0.3364000
    Mexico|52|Acapulco|R|30/6|0am|0.1320000,0.0264000
    Mexico|52|Cancun|R|30/6|0am,2pm|0.1110000,0.1164000|0.2220000,0.2264000
    Mexico|52|Cancun|H|30/6|0am,2pm,10pm|0.1110000,0.1164000|0.2220000,0.2264000|0.3330000,0.3364000
    Mexico|52|Mexico City|R|30/6|0am,2pm|0.1110000,0.1164000
    Mexico|52|Mexico City|W|60/6|0am,2pm,10pm|0.1110000,0.1164000|0.2220000,0.2264000|0.3330000,0.3364000
    //TODO: END TO BE REMOVED

     * 
    //NOTE: LATEST Rates Export/Import FORMAT
    Brazil|55|ROC_CEL|Regular|Blocked|0am-1am
    Brazil|55|ROC_CEL|Regular|Night|11pm-11pm|30/6|0.1110000|0.1164000
    Brazil|55|ROC_CEL|Regular|Day|2pm-9pm|1/1|0.2220000|0.2264000
    Brazil|55|ROC_CEL|Regular|Eve|10pm-10pm|60/60|0.3330000|0.3364000

    Brazil|55|ROC_CEL|Weekend|Blocked|0am-1am
    Brazil|55|ROC_CEL|Weekend|Peak|8am-8pm|30/6|0.1110000|0.1164000
    Brazil|55|ROC_CEL|Weekend|OffPeak|2am-7am,9pm-11pm|1/1|0.2220000|0.2264000
     

    Brazil|55|ROC_CEL|Holiday|Blocked|0am-1am
    Brazil|55|ROC_CEL|Holiday|Flat|2am-11pm|30/6|0.1110000|0.1164000
    */

		public IList<CountryRecord> Process(ImportExportFilter pImportExportFilter, string pFilePath) {
			var _countries = new SortedList<string, CountryRecord>();
			int _lineNumber = 0;

			Host.ReportStatus(string.Format("Started File Parsing Process... File: {0}", pFilePath));
			Host.ReportProgress(0);

			try {
				int _totalLineCount;
				countLines(pFilePath, out _totalLineCount);

				using (var _sr = new StreamReader(pFilePath)) {
					var _countryLines = new List<string>();
					string _line;
					string _previousCountryName = null;
					while ((_line = _sr.ReadLine()) != null) {
						Host.ReportProgress(_lineNumber++ * 100 / _totalLineCount);

						if (_line.StartsWith("Country")) {
							//Rates file header
							continue;
						}
						if (!char.IsLetter(_line[0]) && !char.IsNumber(_line[0])) {
							throw new Exception(string.Format("Invalid File format. \r\nLine#: {0} \r\nLine: {1}", _lineNumber, _line));
						}

						if (Host.CancellationPending) {
							throw new Exception("File parsing canceled");
						}

						if (char.IsLetter(_line[0])) {
							string _currentCountryName = _line.Split(AppConstants.ImportExport_FieldDelimiter)[0];
							if (_currentCountryName != _previousCountryName) {
								if (_previousCountryName != null) {
									var _countryRecord = new CountryRecord(pImportExportFilter, _countryLines.ToArray());
									_countries.Add(_countryRecord.Name, _countryRecord);

									_countryLines = new List<string>();
								}
								_previousCountryName = _currentCountryName;
							}
						}
						_countryLines.Add(_line); //collect country's route line
					}

					//-- collect last country
					if (_countryLines.Count > 0) {
						var _countryRecord = new CountryRecord(pImportExportFilter, _countryLines.ToArray());
						_countries.Add(_countryRecord.Name, _countryRecord);
					}
				}
			}
			catch (Exception _ex) {
				TimokLogger.Instance.LogRbr(LogSeverity.Critical, "DialPlanFileParser.Process", string.Format("Failed parsing the file\r\n{0}", _ex));
				throw;
			}
			return _countries.Values;
		}

		void countLines(string pFilePath, out int pTotalLineCount) {
			pTotalLineCount = 0;
			using (var _sr = new StreamReader(pFilePath)) {
				while (_sr.Peek() != -1) {
					_sr.ReadLine();
					pTotalLineCount++;
				}
			}
		}
	}
}
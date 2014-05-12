using System;
using System.Collections.Generic;
using Timok.Rbr.Core.Config;

namespace Timok.Rbr.BLL.ImportExport.DialPlan {
	public class CountryRecord {
		public int Code;
		public string Name;
		public SortedList<string, RouteRecord> Routes;

		public CountryRecord(int pCountryCode, string pName) {
			Code = pCountryCode;
			Name = pName;
			Routes = new SortedList<string, RouteRecord>();
		}

		/* or Rates
		//NOTE: LATEST Rates Export/Import FORMAT
		Brazil|55|ROC_CEL|Regular|Blocked|0am-1am|0/0|0.0|0.0
		Brazil|55|ROC_CEL|Regular|Night|11pm-11pm|30/6|0.1110000|0.1164000
		Brazil|55|ROC_CEL|Regular|Day|2pm-9pm|1/1|0.2220000|0.2264000
		Brazil|55|ROC_CEL|Regular|Eve|10pm-10pm|60/60|0.3330000|0.3364000

		 * single blocked line
		GOOD: 
		Brazil|55|ROC_11|Regular|Blocked|0am-11pm|0/0|0.0|0.0
		
		BAD: 
		Brazil|55|ROC_11|Regular|Blocked|0am-5pm|0/0|0.0|0.0
		 * 
		BAD: Brazil|55|ROC_11|Regular|Blocked|0am-5pm, 6pm-11pm|0/0|0.0|0.0
		 
		 * bad for all, even if together with other rate lines:
		 * 
		BAD: Brazil|55|ROC_11|Regular|Blocked|0am-11pm|60/60|0.0|0.0
		 * 
		BAD: Brazil|55|ROC_11|Regular|Blocked|0am-11pm|0/0|1.0|1.0

		Brazil|55|ROC_CEL|Weekend|Blocked|0am-1am|0/0|0.0|0.0
		Brazil|55|ROC_CEL|Weekend|Peak|8am-8pm|30/6|0.1110000|0.1164000
		Brazil|55|ROC_CEL|Weekend|OffPeak|2am-7am,9pm-11pm|1/1|0.2220000|0.2264000

		Brazil|55|ROC_CEL|Holiday|Blocked|0am-1am|0/0|0.0|0.0
		Brazil|55|ROC_CEL|Holiday|Flat|2am-11pm|30/6|0.1110000|0.1164000
	 */

		public CountryRecord(ImportExportFilter pImportExportFilter, string[] pRouteLines) {
			if (pRouteLines == null || pRouteLines.Length == 0) {
				throw new Exception("RouteLines is null or zero!");
			}
			Name = pRouteLines[0].Split(AppConstants.ImportExport_FieldDelimiter)[0];
			Code = int.Parse(pRouteLines[0].Split(AppConstants.ImportExport_FieldDelimiter)[1]);
			Routes = new SortedList<string, RouteRecord>();

			var _routeLines = new List<string>();
			var _dialCodes = new List<string>();
			string _previousRouteName = null;
			foreach (string _line in pRouteLines) {
				if (char.IsLetter(_line[0])) {
					string _currentRouteName = _line.Split(AppConstants.ImportExport_FieldDelimiter)[2];
					if (_currentRouteName != _previousRouteName) {
						if (_previousRouteName != null) {
							parse(pImportExportFilter, _routeLines.ToArray(), _dialCodes);
							_routeLines = new List<string>();
							_dialCodes = new List<string>();
						}
						_previousRouteName = _currentRouteName;
					}
					_routeLines.Add(_line); //collect country's route line
				}
				else {
					_dialCodes.AddRange(_line.Split(AppConstants.ImportExport_FieldDelimiter));
				}
			}

			if (_routeLines.Count > 0 || _dialCodes.Count > 0) {
				parse(pImportExportFilter, _routeLines.ToArray(), _dialCodes);
			}
		}

		public string ExportString { get { return Name + AppConstants.ImportExport_FieldDelimiter + Code; } }

		//-------------------------- Private --------------------------------------------

		/*
		DialCodes
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
		*/

		void parse(ImportExportFilter pImportExportFilter, string[] pRouteLines, List<string> pDialCodes) {
			string _breakoutName = pRouteLines[0].Split(AppConstants.ImportExport_FieldDelimiter)[2];
			RouteRecord _routeRecord = null;
			if (pImportExportFilter == ImportExportFilter.DialPlan) {
				_routeRecord = new RouteRecord(Code, Name, _breakoutName, pDialCodes.ToArray(), null);
			}
			else if (pImportExportFilter == ImportExportFilter.Rates) {
				_routeRecord = new RouteRecord(Code, Name, _breakoutName, null, pRouteLines);
			}
			if (_routeRecord != null) {
				Routes.Add(_routeRecord.FullName, _routeRecord);
			}
		}
	}
}
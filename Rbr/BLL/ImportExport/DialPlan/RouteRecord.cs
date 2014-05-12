using System;
using System.Collections.Generic;
using System.Text;
using Timok.Rbr.BLL.DTO;
using Timok.Rbr.Core.Config;

namespace Timok.Rbr.BLL.ImportExport.DialPlan {
	[Serializable]
	public class RouteRecord {
		public string FullName;
		public string CountryName;
		public int CountryCode;
		public SortedList<long, long> DialCodes;
		public RatingInfoDto RatingInfo;
		public string[] BadRouteLines;

		public string BreakoutName {
			get {
				if (FullName != null && FullName.IndexOf(AppConstants.SubRouteSeparator) > 0) {
					return FullName.Split(AppConstants.SubRouteSeparator.ToCharArray())[1];
				}
				return string.Empty;
			}
		}

		public bool IsProper { get { return BreakoutName.ToUpper() == AppConstants.ProperNameSuffix.ToUpper(); } }
		public bool IsRatingEnabled { get { return RatingInfo != null; } }

		//NOTE: LATEST FORMAT
		/*
     Brazil|55|ROC_CEL|RegularDay|Blocked|0am-1am|0/0|0.0|0.0
     Brazil|55|ROC_CEL|RegularDay|Night|11pm-11pm|30/6|0.1110000|0.1164000
     Brazil|55|ROC_CEL|RegularDay|Day|2pm-9pm|1/1|0.2220000|0.2264000
     Brazil|55|ROC_CEL|RegularDay|Eve|10pm-10pm|60/60|0.3330000|0.3364000

     Brazil|55|ROC_CEL|Weekend|Blocked|0am-1am|0/0|0.0|0.0
     Brazil|55|ROC_CEL|Weekend|Peak|8am-8pm|30/6|0.1110000|0.1164000
     Brazil|55|ROC_CEL|Weekend|OffPeak|2am-7am,9pm-11pm|1/1|0.2220000|0.2264000
   

     Brazil|55|ROC_CEL|Holiday|Blocked|0am-1am|0/0|0.0|0.0
     Brazil|55|ROC_CEL|Holiday|Flat|2am-11pm|30/6|0.1110000|0.1164000
    */
		public string GetRatesAsString(bool pPerMinute) {
			var _sb = new StringBuilder();

			if (IsRatingEnabled) {
				var _rateExportLines = RatingInfo.GetExportLines(pPerMinute);
				if (_rateExportLines != null) {
					foreach (var _rateLine in _rateExportLines) {
						_sb.Append(CountryName);
						_sb.Append(AppConstants.ImportExport_FieldDelimiter);
						_sb.Append(CountryCode);
						_sb.Append(AppConstants.ImportExport_FieldDelimiter);
						_sb.Append(BreakoutName);
						_sb.Append(AppConstants.ImportExport_FieldDelimiter);
						_sb.Append(_rateLine);
						_sb.Append(Environment.NewLine);
					}
				}
			}
			return _sb.ToString();
		}

		/*
    CountryName|CountryCode|BreakoutName1
    551281234512|551281234512|551281234512|551281234512|551281234512|551281234512
    551281234512|551281234512|551281234512|551281234512|551281234512|551281234512
    551281234512|551281234512|551281234512
    CountryName|CountryCode|BreakoutName2
    551281234512|551281234512
    CountryName|CountryCode|BreakoutName3
    551281234512|551281234512|551281234512|551281234512|551281234512|551281234512
    */
		public string DialCodesAsString {
			get {
				var _sb = new StringBuilder();
				_sb.Append(CountryName);
				_sb.Append(AppConstants.ImportExport_FieldDelimiter);
				_sb.Append(CountryCode);
				_sb.Append(AppConstants.ImportExport_FieldDelimiter);
				_sb.Append(BreakoutName);
				_sb.Append(Environment.NewLine);

				int _countPerLine = 0;
				int _totalCount = 0;
				foreach (long _dialCode in DialCodes.Values) {
					++_totalCount;
					_sb.Append(_dialCode);

					if (_countPerLine++ > 7) {
						_countPerLine = 0;
						_sb.Append(Environment.NewLine);
					}
					else {
						if (_totalCount < DialCodes.Values.Count) {
							_sb.Append(AppConstants.ImportExport_FieldDelimiter);
						}
					}
				}

				if (_countPerLine != 0) {
					_sb.Append(Environment.NewLine);
				}
				return _sb.ToString();
			}
		}

		public RouteRecord() {
			DialCodes = new SortedList<long, long>();
		}

		public RouteRecord(string pFullName) {
			FullName = pFullName;
			CountryName = FullName.Split(AppConstants.SubRouteSeparator.ToCharArray())[0];
			DialCodes = new SortedList<long, long>();
		}

		public RouteRecord(int pCCode, string pCountryName, string pBreakoutName, string[] pDialCodes, ICollection<string> pRouteLines) {
			var _badRouteLines = new List<string>();
			CountryName = pCountryName;
			CountryCode = pCCode;
			FullName = string.Concat(pCountryName, AppConstants.SubRouteSeparator, pBreakoutName);
			DialCodes = new SortedList<long, long>();

			if (pDialCodes != null && pDialCodes.Length > 0) {
				AddDialCodes(pDialCodes);
			}
			else {
				RatingInfo = new RatingInfoDto(pRouteLines);
				if (RatingInfo.BadRouteLines.Length > 0) {
					foreach (string _routeLine in pRouteLines) {
						if (Array.IndexOf(RatingInfo.BadRouteLines, _routeLine) > -1) {
							_badRouteLines.Add("*" + _routeLine);
						}
						else {
							_badRouteLines.Add(_routeLine);
						}
					}
				}
				BadRouteLines = _badRouteLines.ToArray();
			}
		}

		/* DialCodes
		Brazil|55|ROC_CEL
		551281234512|551281234512|551281234512|551281234512|551281234512|551281234512
		551281234512|551281234512
		*/
		public void AddDialCodes(string[] pDialCodes) {
			try {
				foreach (string _dialCodeString in pDialCodes) {
					long _dialCode = long.Parse(_dialCodeString);
					DialCodes.Add(_dialCode, _dialCode);
				}
			}
			catch (Exception _ex) {
				throw new Exception(string.Format("Invalid DialCodes {0}\r\nException:\r\n{1}", pDialCodes, _ex));
			}
		}
	}
}
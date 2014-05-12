using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using Timok.Rbr.Core.Config;
using Timok.Rbr.DTO;

namespace Timok.Rbr.BLL.DTO {
	[Serializable]
	public class RatingInfoDto {
		ArrayList badRouteLines;
		DayTypeDto holiday;
		HolidayListDto holidayList;
		TypeOfDayRateEntryDto holidayRateEntry;
		decimal perCallCost;
		short perCallTimeLimit;
		int rateInfoId;
		DayTypeDto regularDay;
		TypeOfDayRateEntryDto regularDayRateEntry;
		DayTypeDto weekend;
		TypeOfDayRateEntryDto weekendRateEntry;

		public RatingInfoDto() {
			init();
		}

		public RatingInfoDto(int pRateInfoId, DayTypeDto[] pDayTypes, TypeOfDayRateEntryDto[] pTypeOfDayRateEntries) {
			perCallCost = decimal.Zero;
			perCallTimeLimit = 0;

			rateInfoId = pRateInfoId;

			regularDay = pDayTypes[0];
			weekend = pDayTypes[1];
			holiday = pDayTypes[2];

			regularDayRateEntry = pTypeOfDayRateEntries[0];
			weekendRateEntry = pTypeOfDayRateEntries[1];
			holidayRateEntry = pTypeOfDayRateEntries[2];

			holidayList = new HolidayListDto();

			badRouteLines = new ArrayList();
		}

		public RatingInfoDto(ICollection<string> pRouteLines) {
			init();
			parse(pRouteLines);
		}

		public int RatedRouteId { get; set; }

		public string RouteName { get; set; }

		public int RateInfoId {
			get { return rateInfoId; }
			set {
				rateInfoId = value;
				if (holidayList != null) {
					holidayList.RateInfoId = rateInfoId;
				}

				if (regularDayRateEntry != null) {
					regularDayRateEntry.RateInfoId = rateInfoId;
				}
				if (weekendRateEntry != null) {
					weekendRateEntry.RateInfoId = rateInfoId;
				}
				if (holidayRateEntry != null) {
					holidayRateEntry.RateInfoId = rateInfoId;
				}
			}
		}

		public decimal PerCallCost { get { return perCallCost; } set { perCallCost = value; } }

		public short PerCallTimeLimit { get { return perCallTimeLimit; } set { perCallTimeLimit = value; } }

		public DayTypeDto RegularDay { get { return regularDay; } set { regularDay = value; } }

		public DayTypeDto Weekend { get { return weekend; } set { weekend = value; } }

		public DayTypeDto Holiday { get { return holiday; } set { holiday = value; } }

		public HolidayListDto HolidayList { get { return holidayList; } set { holidayList = value; } }

		public TypeOfDayRateEntryDto RegularDayRateEntry { get { return regularDayRateEntry; } set { regularDayRateEntry = value; } }

		public TypeOfDayRateEntryDto WeekendRateEntry { get { return weekendRateEntry; } set { weekendRateEntry = value; } }

		public TypeOfDayRateEntryDto HolidayRateEntry { get { return holidayRateEntry; } set { holidayRateEntry = value; } }

		public bool AllRatesValid { get { return validateRates(); } }

		//TODO: do we nned this one here
		public bool AllValid { get { return validate(); } }

		public string[] BadRouteLines { get { return (string[]) badRouteLines.ToArray(typeof (string)); } }

		//-------------------------------- ctors -------------------------------------------------------

		void init() {
			perCallCost = decimal.Zero;
			perCallTimeLimit = 0;

			regularDay = new DayTypeDto(new DayTimeDto[0], TimeOfDayPolicy.Flat, false, false);
			weekend = new DayTypeDto(new DayTimeDto[0], TimeOfDayPolicy.Flat, false, false);
			holiday = new DayTypeDto(new DayTimeDto[0], TimeOfDayPolicy.Flat, false, false);

			regularDayRateEntry = new TypeOfDayRateEntryDto();

			weekendRateEntry = new TypeOfDayRateEntryDto { TypeOfDayChoice = TypeOfDayChoice.Weekend };
			foreach (var _rate in weekendRateEntry.Rates) {
				_rate.TypeOfDayChoice = weekendRateEntry.TypeOfDayChoice;
			}

			holidayRateEntry = new TypeOfDayRateEntryDto {TypeOfDayChoice = TypeOfDayChoice.Holiday};
			foreach (var _rate in holidayRateEntry.Rates) {
				_rate.TypeOfDayChoice = holidayRateEntry.TypeOfDayChoice;
			}

			holidayList = new HolidayListDto();

			badRouteLines = new ArrayList();
		}

		public void ClearRates() {
			perCallCost = decimal.Zero;
			perCallTimeLimit = 0;

			if (holidayRateEntry != null) {
				foreach (RateDto _rate in holidayRateEntry.Rates) {
					_rate.FirstIncrCost = decimal.Zero;
					_rate.AddIncrCost = decimal.Zero;
				}
			}

			if (regularDayRateEntry != null) {
				foreach (RateDto _rate in regularDayRateEntry.Rates) {
					_rate.FirstIncrCost = decimal.Zero;
					_rate.AddIncrCost = decimal.Zero;
				}
			}

			if (weekendRateEntry != null) {
				foreach (RateDto _rate in weekendRateEntry.Rates) {
					_rate.FirstIncrCost = decimal.Zero;
					_rate.AddIncrCost = decimal.Zero;
				}
			}
		}

		public string[] GetExportLines(bool pPerMinute) {
			var _list = new List<string>();
			if (regularDay != null && regularDay.IsSelected) {
				if (regularDayRateEntry != null && regularDayRateEntry.Rates != null && regularDayRateEntry.Rates.Length > 0) {
					//RegularDay|Flat|2pm-9pm|30/6|0.2220000|0.2264000
					_list.AddRange(getRateLines(regularDay, regularDayRateEntry, pPerMinute));
				}
			}

			if (weekend != null && weekend.IsSelected) {
				if (weekendRateEntry != null && weekendRateEntry.Rates != null && weekendRateEntry.Rates.Length > 0) {
					//Weekend|Eve|10pm-10pm|60/60|0.3330000|0.3364000
					_list.AddRange(getRateLines(weekend, weekendRateEntry, pPerMinute));
				}
			}

			if (holiday != null && holiday.IsSelected) {
				if (holidayRateEntry != null && holidayRateEntry.Rates != null && holidayRateEntry.Rates.Length > 0) {
					//Holiday|Night|11pm-11pm|30/6|0.1110000|0.1164000
					_list.AddRange(getRateLines(holiday, holidayRateEntry, pPerMinute));
				}
			}
			return _list.ToArray();
		}

		//--------------------------- privates --------------------------------------

		bool validate() {
			//TODO: do we nned this one here
			//if (!validateRates(regularDayRateEntry)) {
			//  return false;
			//}

			//if (weekendRateEntry != null) {
			//  if (!validateRates(weekendRateEntry)) {
			//    return false;
			//  }
			//}

			//if (holidayRateEntry != null) {
			//  if (!validateRates(holidayRateEntry)) {
			//    return false;
			//  }
			//}
			return true;
		}

		#region Export helpers

		//return example: 
		//Brazil|55|ROC_CEL|Holiday|Blocked|0am-2am,10pm-10pm|0/0|0.0|0.0
		//Brazil|55|ROC_CEL|Holiday|Night|3am-9pm,11pm-11pm|30/6|0.1110000|0.1164000
		string[] getRateLines(DayTypeDto pDayType, TypeOfDayRateEntryDto pTypeOfDayRateEntry, bool pPerMinute) {
			var _rateLines = new List<string>();
			foreach (var _rate in pTypeOfDayRateEntry.Rates) {
				//NOTE: skip Blocked* 
				var _rateLine = new StringBuilder();
				_rateLine.Append(_rate.TypeOfDayChoice); //RegularDay|
				_rateLine.Append(AppConstants.ImportExport_FieldDelimiter);

				if (_rate.TimeOfDay == TimeOfDay.BlockedFlat || _rate.TimeOfDay == TimeOfDay.BlockedPeakOffPeak || _rate.TimeOfDay == TimeOfDay.BlockedNightDayEve) {
					_rateLine.Append(_rate.TimeOfDay.ToString().Replace(pDayType.TODPolicy.ToString(), string.Empty)); //just leave "Blocked"
				}
				else {
					_rateLine.Append(_rate.TimeOfDay.ToString()); //|Flat| or |Night| ...
				}

				_rateLine.Append(AppConstants.ImportExport_FieldDelimiter);

				string _timeRanges = getTimeRanges(pDayType, _rate.TimeOfDay);
				_rateLine.Append(_timeRanges); //|2am-7am,9pm-11pm|

				_rateLine.Append(AppConstants.ImportExport_FieldDelimiter);

				_rateLine.Append(_rate.FirstIncrLen); //|30/6|
				_rateLine.Append(AppConstants.ImportExport_RateIncrementsDelimiter);
				_rateLine.Append(_rate.AddIncrLen); //|30/6|
				_rateLine.Append(AppConstants.ImportExport_FieldDelimiter);

				if (pPerMinute) {
					_rateLine.Append(_rate.GetPerMinuteCost()); //|0.1110000
				}
				else {
					_rateLine.Append(_rate.FirstIncrCost); //|0.1110000|0.1164000
					_rateLine.Append(AppConstants.ImportExport_FieldDelimiter);
					_rateLine.Append(_rate.AddIncrCost); //|0.1110000|0.1164000
				}
				_rateLines.Add(_rateLine.ToString());
			}
			return _rateLines.ToArray();
		}

		string getTimeRanges(DayTypeDto pDayType, TimeOfDay pTimeOfDay) {
			var _timeRanges = new List<string>();
			foreach (var _dayTime in pDayType.DayTimePeriods) {
				if (_dayTime.TimeOfDay == pTimeOfDay) {
					// 2am-7am
					// 9pm-11pm
					_timeRanges.Add(convertTo12h(_dayTime.Start) + "-" + convertTo12h(_dayTime.Stop));
				}
			}
			// 2am-7am,9pm-11pm
			return string.Join(AppConstants.ImportExport_RateTimePeriodsDelimiter.ToString(), _timeRanges.ToArray());
		}

		//IN:
		//0 6 10 12 15 23
		//OUT:
		//"0am" "6am" "10am" "12pm" "3pm" "11pm"
		string convertTo12h(int pHour) {
			if (pHour >= 0 && pHour < 12) {
				return pHour + "am";
			}
			if (pHour > 12 && pHour <= 23) {
				return (pHour - 12) + "pm";
			}
			if (pHour == 12) {
				return pHour + "pm";
			}
			return string.Empty;
		}

		#endregion Export helpers

		#region Import helpers

		#region Format example

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

		#endregion Format example

		void parse(ICollection<string> pRouteLines) {
			if (pRouteLines == null || pRouteLines.Count == 0) {
				return;
			}
			var _regularDayRouteLines = new List<string>();
			var _weekendRouteLines = new List<string>();
			var _holidayRouteLines = new List<string>();

			foreach (var _routeLine in pRouteLines) {
				var _typeOfDayChoiceString = _routeLine.Split(AppConstants.ImportExport_FieldDelimiter)[RouteLineFields.TypeOfDayChoice];
				if (!Enum.IsDefined(typeof (TypeOfDayChoice), _typeOfDayChoiceString)) {
					badRouteLines.Add(_routeLine);
					//TODO: T.LogRbr(LogSeverity.Error, "RatingInfoDto.parse", string.Format("Invalid TypeOfDayChoice: {0}", _typeOfDayChoiceString));
					return;
				}

				var _typeOfDayChoice = (TypeOfDayChoice) Enum.Parse(typeof (TypeOfDayChoice), _typeOfDayChoiceString);

				if (_typeOfDayChoice == TypeOfDayChoice.RegularDay) {
					_regularDayRouteLines.Add(_routeLine);
				}
				else if (_typeOfDayChoice == TypeOfDayChoice.Weekend) {
					_weekendRouteLines.Add(_routeLine);
				}
				else if (_typeOfDayChoice == TypeOfDayChoice.Holiday) {
					_holidayRouteLines.Add(_routeLine);
				}
			}

			if (!parseDayChoice(TypeOfDayChoice.RegularDay, _regularDayRouteLines.ToArray())) {
				return;
			}

			if (_weekendRouteLines.Count > 0) {
				if (!parseDayChoice(TypeOfDayChoice.Weekend, _weekendRouteLines.ToArray())) {
					return;
				}
			}

			if (_holidayRouteLines.Count > 0) {
				if (!parseDayChoice(TypeOfDayChoice.Holiday, _holidayRouteLines.ToArray())) {
					return;
				}
			}
		}

		/*
    Brazil|55|ROC_CEL|RegularDay|Blocked|0am-1am|0/0|0.0|0.0
    Brazil|55|ROC_CEL|RegularDay|Night|11pm-11pm|30/6|0.1110000|0.1164000
    Brazil|55|ROC_CEL|RegularDay|Day|2pm-9pm|1/1|0.2220000|0.2264000
    Brazil|55|ROC_CEL|RegularDay|Eve|10pm-10pm|60/60|0.3330000|0.3364000
    
    OR
    
    Brazil|55|ROC_CEL|Weekend|Blocked|0am-1am|0/0|0.0|0.0
    Brazil|55|ROC_CEL|Weekend|Peak|8am-8pm|30/6|0.1110000|0.1164000
    Brazil|55|ROC_CEL|Weekend|OffPeak|2am-7am,9pm-11pm|1/1|0.2220000|0.2264000
    
    OR
    
    Brazil|55|ROC_CEL|Holiday|Blocked|0am-1am|0/0|0.0|0.0
    Brazil|55|ROC_CEL|Holiday|Flat|2am-11pm|30/6|0.1110000|0.1164000
    */

		bool parseDayChoice(TypeOfDayChoice pTypeOfDayChoice, string[] pRouteLines) {
			TimeOfDayPolicy _timeOfDayPolicy;
			bool _isPotentiallyUnknownDayPolicy;
			if (!parsePolicy(pRouteLines, out _timeOfDayPolicy, out _isPotentiallyUnknownDayPolicy)) {
				return false;
			}

			var _dayTimeList = new List<DayTimeDto>();
			var _rateList = new List<RateDto>();
			foreach (string _routeLine in pRouteLines) {
				try {
					//string[] _rateFields = _routeLine.Split(Configuration.Configuration.Instance.ImportExport_FieldDelimiter);
					TimeOfDay _timeOfDay = parseTimeOfDay(_timeOfDayPolicy, _routeLine);

					_dayTimeList.AddRange(parseDayTimeList(_timeOfDay, _routeLine));

					_rateList.Add(parseRate(pTypeOfDayChoice, _timeOfDay, _routeLine));
				}
				catch (Exception _ex) {
					badRouteLines.Add(_routeLine);
					//TODO: T.LogRbr(LogSeverity.Error, "RatingInfoDto.parseDayChoice", string.Format("Exception:\r\n{0}", _ex));
					return false;
				}
			}

			try {
				setRates(pTypeOfDayChoice, _timeOfDayPolicy, _dayTimeList.ToArray(), _rateList.ToArray(), _isPotentiallyUnknownDayPolicy);
			}
			catch (Exception _ex) {
				badRouteLines.AddRange(pRouteLines);
				//TODO: T.LogRbr(LogSeverity.Error, "RatingInfoDto.parseDayChoice", string.Format("Exception2:\r\n{0}", _ex));
				return false;
			}
			return true;
		}

		bool parsePolicy(string[] pRouteLines, out TimeOfDayPolicy pTimeOfDayPolicy, out bool pIsPotentiallyUnknownDayPolicy) {
			pTimeOfDayPolicy = TimeOfDayPolicy.Flat;
			pIsPotentiallyUnknownDayPolicy = false;
			foreach (string _routeLine in pRouteLines) {
				string _timeOfDayString = _routeLine.Split(AppConstants.ImportExport_FieldDelimiter)[RouteLineFields.TimeOfDay];
				if (_timeOfDayString == "Blocked") {
					if (pRouteLines.Length == 1) {
						pTimeOfDayPolicy = TimeOfDayPolicy.Flat;
						pIsPotentiallyUnknownDayPolicy = true;
						return true;
					}
					continue;
				}

				var _timeOfDay = (TimeOfDay) Enum.Parse(typeof (TimeOfDay), _timeOfDayString);
				if (_timeOfDay == TimeOfDay.Flat) {
					pTimeOfDayPolicy = TimeOfDayPolicy.Flat;
					return true;
				}
				if (_timeOfDay == TimeOfDay.Peak || _timeOfDay == TimeOfDay.OffPeak) {
					pTimeOfDayPolicy = TimeOfDayPolicy.PeakOffPeak;
					return true;
				}
				if (_timeOfDay == TimeOfDay.Night || _timeOfDay == TimeOfDay.Day || _timeOfDay == TimeOfDay.Eve) {
					pTimeOfDayPolicy = TimeOfDayPolicy.NightDayEve;
					return true;
				}
				badRouteLines.Add(_routeLine);
				//TODO: T.LogRbr(LogSeverity.Error, "RatingInfoDto.parsePolicy", string.Format("Cannot determine TimeOfDayPolicy and/or TypeOfDayChoice in Route line: {0}", _routeLine));
				return false;
			}
			badRouteLines.AddRange(pRouteLines);
			//TODO: T.LogRbr(LogSeverity.Error, "RatingInfoDto.parsePolicy", "Cannot determine" + " TimeOfDayPolicy and/or TypeOfDayChoice in any Route line. Lines Count: " + pRouteLines.Length);
			return false;
		}

		TimeOfDay parseTimeOfDay(TimeOfDayPolicy pTimeOfDayPolicy, string pRouteLine) {
			string _timeOfDayString = pRouteLine.Split(AppConstants.ImportExport_FieldDelimiter)[RouteLineFields.TimeOfDay];
			if (_timeOfDayString == "Blocked") {
				return (TimeOfDay) Enum.Parse(typeof (TimeOfDay), _timeOfDayString + pTimeOfDayPolicy);
			}
			if (Enum.IsDefined(typeof (TimeOfDay), _timeOfDayString)) {
				return (TimeOfDay) Enum.Parse(typeof (TimeOfDay), _timeOfDayString);
			}
			throw new ArgumentException("Invalid TimeOfDay in Route line: " + pRouteLine);
		}

		DayTimeDto[] parseDayTimeList(TimeOfDay pTimeOfDay, string pRouteLine) {
			var _timePeriodsString = pRouteLine.Split(AppConstants.ImportExport_FieldDelimiter)[RouteLineFields.TimePeriods];
			var _dayTimeList = new List<DayTimeDto>();
			var _timePeriods = _timePeriodsString.Split(AppConstants.ImportExport_RateTimePeriodsDelimiter);
			foreach (var _timePariod in _timePeriods) {
				string[] _startStopTimes = _timePariod.Split('-');
				int _startTime = convertTo24h(_startStopTimes[0]);
				int _stopTime = convertTo24h(_startStopTimes[1]);
				_dayTimeList.Add(new DayTimeDto(pTimeOfDay, _startTime, _stopTime));
			}
			return _dayTimeList.ToArray();
		}

		RateDto parseRate(TypeOfDayChoice pTypeOfDayChoice, TimeOfDay pTimeOfDay, string pRouteLine) {
			var _routeFields = pRouteLine.Split(AppConstants.ImportExport_FieldDelimiter);
			byte _firstIncr;
			byte _addIncr;
			parseIncrements(_routeFields[RouteLineFields.Increments], out _firstIncr, out _addIncr);
			if (!validateIncrement(pTimeOfDay, _firstIncr, _addIncr)) {
				throw new ArgumentException(string.Format("Invalid Increment: [{0}/{1}] for TypeOfDayChoice={2}, TimeOfDay={3}, Route line={4}", _firstIncr, _addIncr, pTypeOfDayChoice, pTimeOfDay, pRouteLine));
			}

			if (_routeFields.Length == 9) {
				//Increments
				decimal _firstIncrCost;
				decimal.TryParse(_routeFields[RouteLineFields.FirstIncrCost], out _firstIncrCost);
				decimal _addIncrCost;
				decimal.TryParse(_routeFields[RouteLineFields.AddIncrCost], out _addIncrCost);
				if (!validateCost(pTimeOfDay, _firstIncrCost, _addIncrCost)) {
					throw new ArgumentException(string.Format("Invalid Cost [{0},{1}] for TypeOfDayChoice={2}, TimeOfDay={3}, Route line={4}", _firstIncrCost, _addIncrCost, pTypeOfDayChoice, pTimeOfDay, pRouteLine));
				}

				var _rate = new RateDto
				            {
				            	TypeOfDayChoice = pTypeOfDayChoice, 
				            	TimeOfDay = pTimeOfDay, 
				            	FirstIncrLen = _firstIncr, 
				            	AddIncrLen = _addIncr, 
				            	FirstIncrCost = _firstIncrCost, 
				            	AddIncrCost = _addIncrCost
				            };
				return _rate;
			}
			
			if (_routeFields.Length == 8) {
				//PerMinute
				decimal _perMinuteCost;
				decimal.TryParse(_routeFields[RouteLineFields.FirstIncrCost], out _perMinuteCost);
				var _rate = new RateDto
				            {
				            	TypeOfDayChoice = pTypeOfDayChoice, 
				            	TimeOfDay = pTimeOfDay, 
				            	FirstIncrLen = _firstIncr, 
				            	AddIncrLen = _addIncr
				            };
				_rate.FirstIncrCost = _rate.GetFirstIncrAmount(_perMinuteCost, _firstIncr, _addIncr);
				_rate.AddIncrCost = _rate.GetAddIncrAmount(_perMinuteCost, _firstIncr, _addIncr);
				if (!validateCost(pTimeOfDay, _rate.FirstIncrCost, _rate.AddIncrCost)) {
					throw new ArgumentException(string.Format("Invalid Cost [{0},{1}] for TypeOfDayChoice={2} TimeOfDay={3}, Route line={4}", _rate.FirstIncrCost, _rate.AddIncrCost, pTypeOfDayChoice, pTimeOfDay, pRouteLine));
				}
				return _rate;
			}
			throw new Exception(string.Format("Number of RouteFields incorrect, {0}", pRouteLine));
		}

		void parseIncrements(string pIncrString, out byte pFirstIncr, out byte pAddIncr) {
			string[] _incrFields = pIncrString.Split(AppConstants.ImportExport_RateIncrementsDelimiter);
			byte.TryParse(_incrFields[0], out pFirstIncr);
			byte.TryParse(_incrFields[1], out pAddIncr);
		}

		void setRates(TypeOfDayChoice pTypeOfDayChoice, TimeOfDayPolicy pTimeOfDayPolicy, DayTimeDto[] pDayTimePeriods, RateDto[] pRateList, bool pIsPotentiallyUnknownDayPolicy) {
			if (!validateTimePeriods(pDayTimePeriods, pTimeOfDayPolicy)) {
				throw new ArgumentException("Invalid TimePeriod(s) for TypeOfDayChoice: [" + pTypeOfDayChoice + "] TimeOfDayPolicy: [" + pTimeOfDayPolicy + "]");
			}

			if (pTypeOfDayChoice == TypeOfDayChoice.RegularDay) {
				regularDay = new DayTypeDto(pDayTimePeriods, pTimeOfDayPolicy, true, pIsPotentiallyUnknownDayPolicy);

				regularDayRateEntry = new TypeOfDayRateEntryDto {TypeOfDayChoice = pTypeOfDayChoice, Rates = pRateList};
			}
			if (pTypeOfDayChoice == TypeOfDayChoice.Weekend) {
				weekend = new DayTypeDto(pDayTimePeriods, pTimeOfDayPolicy, true, pIsPotentiallyUnknownDayPolicy);

				weekendRateEntry = new TypeOfDayRateEntryDto {TypeOfDayChoice = pTypeOfDayChoice, Rates = pRateList};
			}
			if (pTypeOfDayChoice == TypeOfDayChoice.Holiday) {
				holiday = new DayTypeDto(pDayTimePeriods, pTimeOfDayPolicy, true, pIsPotentiallyUnknownDayPolicy);

				holidayRateEntry = new TypeOfDayRateEntryDto {TypeOfDayChoice = pTypeOfDayChoice, Rates = pRateList};
			}
		}

		//IN:
		//"0am" "6am" "10am" "12pm" "3pm" "11pm"
		//OUT:
		//0 6 10 12 15 23
		int convertTo24h(string pHourAmPm) {
			string _AmPmHour = pHourAmPm.Remove(pHourAmPm.IndexOf('m') - 1);
			int _hour24h = int.Parse(_AmPmHour);
			if (pHourAmPm.ToLower().Contains("pm")) {
				if (_hour24h < 12) {
					_hour24h += 12;
				}
			}
			if (_hour24h < 0 || _hour24h > 23) {
				throw new ArgumentException("Invalid Hour: [" + pHourAmPm + "]");
			}
			return _hour24h;
		}

		#region Import Rate validation

		bool validateIncrement(TimeOfDay pTimeOfDay, byte pFirstIncr, byte pAddIncr) {
			if (pTimeOfDay == TimeOfDay.BlockedFlat || pTimeOfDay == TimeOfDay.BlockedPeakOffPeak || pTimeOfDay == TimeOfDay.BlockedNightDayEve) {
				if (pFirstIncr != 0 || pAddIncr != 0) {
					return false;
				}
			}
			else {
				if (pFirstIncr <= 0 || pAddIncr <= 0) {
					return false;
				}
			}
			return true;
		}

		bool validateCost(TimeOfDay pTimeOfDay, decimal pFirstIncrCost, decimal pAddIncrCost) {
			if (pTimeOfDay == TimeOfDay.BlockedFlat || pTimeOfDay == TimeOfDay.BlockedPeakOffPeak || pTimeOfDay == TimeOfDay.BlockedNightDayEve) {
				if (pFirstIncrCost != decimal.Zero || pAddIncrCost != decimal.Zero) {
					return false;
				}
			}
			else {
				//NOTE: it's OK to have 0.0 but NOT less than 0 
				if (pFirstIncrCost < decimal.Zero || pAddIncrCost < decimal.Zero) {
					return false;
				}
			}
			return true;
		}

		bool validateTimePeriods(DayTimeDto[] pDayTimePeriods, TimeOfDayPolicy pTimeOfDayPolicy) {
			if (pDayTimePeriods == null || pDayTimePeriods.Length == 0) {
				return false;
				//throw new ArgumentException("No Time periods provided");
			}
			if (pDayTimePeriods.Length == 1 && (pDayTimePeriods[0].TimeOfDay == TimeOfDay.BlockedFlat || pDayTimePeriods[0].TimeOfDay == TimeOfDay.BlockedPeakOffPeak || pDayTimePeriods[0].TimeOfDay == TimeOfDay.BlockedNightDayEve)) {
				if (pDayTimePeriods[0].Start != 0 && pDayTimePeriods[0].Stop != 23) {
					return false; //NOTE: if only one TimePeriod and if this TimePeriod is for Blocked* - it has to cover whole Day 0-23 hours
				}
			}

			var _sorted = new SortedList<int, DayTimeDto>();
			foreach (DayTimeDto _dayTime in pDayTimePeriods) {
				if (!validateTimePeriod(_dayTime, pTimeOfDayPolicy)) {
					return false;
				}

				if (_sorted.ContainsKey(_dayTime.Start)) {
					return false;
					//throw new ArgumentException("Duplicates in Time Start. Start: [" + _dayTime.Start + "] ; Stop: [" + _dayTime.Stop + "]");
				}
				_sorted.Add(_dayTime.Start, _dayTime);
			}

			int _lastStop = -1;
			foreach (DayTimeDto _dayTime in _sorted.Values) {
				if (_lastStop + 1 != _dayTime.Start) {
					return false;
					//throw new ArgumentException("Gaps are not allowed in Time ranges. Start: [" + _dayTime.Start + "] ; Stop: [" + _dayTime.Stop + "]");
				}
				_lastStop = _dayTime.Stop;
			}
			if (_lastStop != 23) {
				return false;
				//throw new ArgumentException("Gaps are not allowed in Time ranges. Stop: [" + _lastStop + "]");
			}
			return true;
		}

		bool validateTimePeriod(DayTimeDto pDayTimePeriod, TimeOfDayPolicy pTimeOfDayPolicy) {
			if (pDayTimePeriod.Start > pDayTimePeriod.Stop) {
				return false;
				//throw new ArgumentException("Time Start cannot be grater then Time Stop. Start: [" + pDayTimePeriod.Start + "] ; Stop: [" + pDayTimePeriod.Stop + "]");
			}
			if (pTimeOfDayPolicy == TimeOfDayPolicy.Flat) {
				if (pDayTimePeriod.TimeOfDay != TimeOfDay.BlockedFlat && pDayTimePeriod.TimeOfDay != TimeOfDay.Flat) {
					return false;
					//throw new ArgumentException("Invalid DayTimePeriod.TimeOfDay: [" + pDayTimePeriod.TimeOfDay + "] for TimeOfDayPolicy: [" + pTimeOfDayPolicy + "]");
				}
			}
			else if (pTimeOfDayPolicy == TimeOfDayPolicy.PeakOffPeak) {
				if (pDayTimePeriod.TimeOfDay != TimeOfDay.BlockedPeakOffPeak && pDayTimePeriod.TimeOfDay != TimeOfDay.Peak && pDayTimePeriod.TimeOfDay != TimeOfDay.OffPeak) {
					return false;
					//throw new ArgumentException("Invalid DayTimePeriod.TimeOfDay: [" + pDayTimePeriod.TimeOfDay + "] for TimeOfDayPolicy: [" + pTimeOfDayPolicy + "]");
				}
			}
			else if (pTimeOfDayPolicy == TimeOfDayPolicy.NightDayEve) {
				if (pDayTimePeriod.TimeOfDay != TimeOfDay.BlockedNightDayEve && pDayTimePeriod.TimeOfDay != TimeOfDay.Night && pDayTimePeriod.TimeOfDay != TimeOfDay.Day && pDayTimePeriod.TimeOfDay != TimeOfDay.Eve) {
					return false;
					//throw new ArgumentException("Invalid DayTimePeriod.TimeOfDay: [" + pDayTimePeriod.TimeOfDay + "] for TimeOfDayPolicy: [" + pTimeOfDayPolicy + "]");
				}
			}
			return true;
		}

		#endregion Import Rate validation

		//Brazil|55|ROC_CEL|Holiday|Blocked|0am-2am,10pm-10pm|0/0|0.0|0.0
		struct RouteLineFields {
			public const int AddIncrCost = 8;
			public const int BreakoutName = 2;
			public const int CountryCode = 1;
			public const int CountryName = 0;
			public const int FirstIncrCost = 7;
			public const int Increments = 6;
			public const int TimeOfDay = 4;
			public const int TimePeriods = 5;
			public const int TypeOfDayChoice = 3;
		}

		#endregion Import helpers

		#region More than "0" Rate validation

		bool validateRates() {
			if (!validateRates(regularDayRateEntry)) {
				return false;
			}

			if (weekendRateEntry != null) {
				if (!validateRates(weekendRateEntry)) {
					return false;
				}
			}

			if (holidayRateEntry != null) {
				if (!validateRates(holidayRateEntry)) {
					return false;
				}
			}
			return true;
		}

		bool validateRates(TypeOfDayRateEntryDto pTypeOfDayRateEntry) {
			//if (pTypeOfDayRateEntry ==  null) {
			//  return true;
			//}
			foreach (RateDto _rate in pTypeOfDayRateEntry.Rates) {
				if (_rate.TimeOfDay == TimeOfDay.BlockedFlat || _rate.TimeOfDay == TimeOfDay.BlockedPeakOffPeak || _rate.TimeOfDay == TimeOfDay.BlockedNightDayEve) {
					continue; //skip validation for Blocked periods
				}
				if (_rate.FirstIncrCost <= decimal.Zero || _rate.AddIncrCost <= decimal.Zero) {
					return false;
				}
			}
			return true;
		}

		#endregion More than "0" Rate validation

		#region Cloning

		public RatingInfoDto Clone() {
			var _clone = new RatingInfoDto
			             {
			             	RateInfoId = 0, 
			             	PerCallCost = perCallCost, 
			             	PerCallTimeLimit = perCallTimeLimit, 
			             	HolidayList = holidayList.MakeClone(), 
			             	Holiday = holiday
			             };

			if (holidayRateEntry != null) {
				_clone.HolidayRateEntry = holidayRateEntry.MakeClone();
			}

			_clone.RegularDay = regularDay;
			if (regularDayRateEntry != null) {
				_clone.RegularDayRateEntry = regularDayRateEntry.MakeClone();
			}

			_clone.Weekend = weekend;
			if (weekendRateEntry != null) {
				_clone.WeekendRateEntry = weekendRateEntry.MakeClone();
			}
			return _clone;
		}

		public RatingInfoDto CloneWithNoRates() {
			var _clone = new RatingInfoDto
			             {
			             	RateInfoId = 0, 
			             	PerCallCost = decimal.Zero, 
			             	PerCallTimeLimit = 0, 
			             	HolidayList = holidayList.MakeClone(), 
			             	Holiday = holiday
			             };

			if (holidayRateEntry != null) {
				_clone.HolidayRateEntry = holidayRateEntry.MakeCloneWithNoRates();
			}

			_clone.RegularDay = regularDay;
			if (regularDayRateEntry != null) {
				_clone.RegularDayRateEntry = regularDayRateEntry.MakeCloneWithNoRates();
			}

			_clone.Weekend = weekend;
			if (weekendRateEntry != null) {
				_clone.WeekendRateEntry = weekendRateEntry.MakeCloneWithNoRates();
			}

			return _clone;
		}

		#endregion Cloning
	}
}
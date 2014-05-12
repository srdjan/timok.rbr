using System;
using System.ComponentModel;
using Timok.Core;
using Timok.Rbr.BLL.DTO;
using Timok.Rbr.Core.Config;
using Timok.Rbr.DTO.Interfaces;

namespace Timok.Rbr.DTO {
	[Serializable]
	public class RatingHistoryEntry : INotifyPropertyChanged, IRatingHistoryEntry {
		public const string FirstDate_PropName = "FirstDate";
		public const string LastDate_PropName = "LastDate";
		public const string RateInfoId_PropName = "RateInfoId";
		public const string RatingInfo_PropName = "RatingInfo";
		public const string RouteId_PropName = "RouteId";
		protected DateTime firstDate;

		protected bool hasChanged;
		protected DateTime lastDate;
		protected int ratedRouteId;
		protected RatingInfoDto ratingInfo;

		public RatingHistoryEntry() {
			firstDate = DateTime.Today;
			lastDate = DateTime.Today.AddYears(10);
		}

		public RatingHistoryEntry(RouteType pRouteType, int pRatedRouteId, DateTime pFirstDate, DateTime pLastDate, RatingInfoDto pRatingInfo) {
			if (pFirstDate > pLastDate) {
				throw new ArgumentOutOfRangeException("pFirstDate", "'First Date' cannot be grater then 'Last Date'.");
			}
			RouteType = pRouteType;
			ratedRouteId = pRatedRouteId;
			firstDate = trimToDay(pFirstDate);
			lastDate = trimToDay(pLastDate);
			ratingInfo = pRatingInfo;
		}

		#region INotifyPropertyChanged Members

		[field : NonSerialized]
		public event PropertyChangedEventHandler PropertyChanged;

		#endregion

		#region IRatingHistoryEntry Members

		public bool HasChanged { get { return hasChanged; } set { hasChanged = value; } }

		public RouteType RouteType { get; set; }

		public int RatedRouteId {
			get { return ratedRouteId; }
			set {
				if (ratedRouteId != value) {
					ratedRouteId = value;
					OnPropertyChanged(RouteId_PropName);
				}
			}
		}

		public DateTime FirstDate {
			get { return firstDate; }
			set {
				if (value > Configuration.Instance.Db.SqlSmallDateTimeMaxValue) {
					throw new ArgumentOutOfRangeException("Date 'From' cannot be grater then " + Configuration.Instance.Db.SqlSmallDateTimeMaxValue.ToString("MM/dd/yyyy"), "FirstDate");
				}

				if (value < Configuration.Instance.Db.SqlSmallDateTimeMinValue) {
					throw new ArgumentOutOfRangeException("Date 'From' cannot be less then " + Configuration.Instance.Db.SqlSmallDateTimeMinValue.ToString("MM/dd/yyyy"), "FirstDate");
				}

				if (firstDate != value) {
					firstDate = trimToDay(value);
					OnPropertyChanged(FirstDate_PropName);
				}
			}
		}

		public DateTime LastDate {
			get { return lastDate; }
			set {
				if (value > Configuration.Instance.Db.SqlSmallDateTimeMaxValue) {
					throw new ArgumentOutOfRangeException("Date 'To' cannot be grater then " + Configuration.Instance.Db.SqlSmallDateTimeMaxValue.ToString("MM/dd/yyyy"), "LastDate");
				}

				if (value < Configuration.Instance.Db.SqlSmallDateTimeMinValue) {
					throw new ArgumentOutOfRangeException("Date 'To' cannot be less then " + Configuration.Instance.Db.SqlSmallDateTimeMinValue.ToString("MM/dd/yyyy"), "LastDate");
				}
				if (lastDate != value) {
					lastDate = trimToDay(value);
					OnPropertyChanged(LastDate_PropName);
				}
			}
		}

		public int RateInfoId {
			get { return ratingInfo != null ? ratingInfo.RateInfoId : 0; }
			set { ratingInfo.RateInfoId = value; }
		}

		public RatingInfoDto RatingInfo {
			get { return ratingInfo; }
			set {
				if (ratingInfo != value) {
					ratingInfo = value;
					OnPropertyChanged(RatingInfo_PropName);
				}
			}
		}

		//NOTE: make it public, to be able to deserialize

		//NOTE: compare object's values (not refs)
		public override bool Equals(object obj) {
			if (obj == null || obj.GetType() != GetType()) {
				return false;
			}

			return ObjectComparer.AreEqual(this, obj);
		}

		public override int GetHashCode() {
			//TODO: finish it, get hashes for all fields
			return RateInfoId.GetHashCode();
		}

		#endregion

		protected void OnPropertyChanged(string propertyName) {
			OnPropertyChanged(new PropertyChangedEventArgs(propertyName));
		}

		protected void OnPropertyChanged(PropertyChangedEventArgs e) {
			hasChanged = true;
			if (PropertyChanged != null) {
				PropertyChanged(this, e);
			}
		}

		//------------------------- private ---------------------------------------
		DateTime trimToDay(DateTime pDate) {
			return new DateTime(pDate.Year, pDate.Month, pDate.Day, 0, 0, 0);
		}
	}
}
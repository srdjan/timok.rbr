using System;
using Timok.Rbr.BLL.DTO;
using Timok.Rbr.Core.Config;

namespace Timok.Rbr.DTO.Interfaces {
	public interface IRatingHistoryEntry {
		RouteType RouteType { get; set; }

		/// <summary>
		///  NOTE: this is NOT a BaseRouteId
		/// </summary>
		int RatedRouteId { get; set; }

		DateTime FirstDate { get; set; }
		bool HasChanged { get; set; }
		DateTime LastDate { get; set; }
		event System.ComponentModel.PropertyChangedEventHandler PropertyChanged;
		int RateInfoId { get; set; }
		RatingInfoDto RatingInfo { get; set; }
		bool Equals(object obj);
		int GetHashCode();
	}
}
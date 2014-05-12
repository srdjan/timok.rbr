using System;

using Timok.Rbr.Core.Config;

namespace Timok.Rbr.DTO {
	[Serializable]
	public class DayTypeDto {
		public bool IsSelected;
		public DayTimeDto[] DayTimePeriods;
		public TimeOfDayPolicy TODPolicy;
    public bool IsPotentiallyUnknownDayPolicy;

    public DayTypeDto(DayTimeDto[] pDayTimePeriods, TimeOfDayPolicy pTODPolicy, bool pIsSelected, bool pIsPotentiallyUnknownDayPolicy) {
			IsSelected = pIsSelected;
			DayTimePeriods = pDayTimePeriods;
      TODPolicy = pTODPolicy;
      IsPotentiallyUnknownDayPolicy = pIsPotentiallyUnknownDayPolicy;
		}
	}
}

using System;

using Timok.Rbr.Core.Config;

namespace Timok.Rbr.DTO {
	[Serializable]
	public class DayTimeDto {
		public int Start;
		public int Stop;
		public TimeOfDay TimeOfDay;

		public DayTimeDto(TimeOfDay pTimeOfDay, int pStart, int pStop) {
			TimeOfDay = pTimeOfDay;
			Start = pStart;
			Stop = pStop;
		}
	}
}

using System;
using Timok.Rbr.Core.Config;

namespace Timok.Rbr.DTO {
	[Serializable]
	public class ScheduleDto {
		public int ScheduleId { get; set; }

		public ScheduleType ScheduleType { get; set; }

		public DayOfWeek DayOfWeek { get; set; }

		public int DayOfTheMonth1 { get; set; }

		public int DayOfTheMonth2 { get; set; }
	}
}
using Timok.Rbr.DAL.RbrDatabase;
using Timok.Rbr.DTO;

namespace Timok.Rbr.BLL.Managers {
	internal class ScheduleManager {
		ScheduleManager() {}

		#region Getters

		internal static ScheduleDto Get(Rbr_Db pDb, int pScheduleId) {
			ScheduleRow _scheduleRow = pDb.ScheduleCollection.GetByPrimaryKey(pScheduleId);
			return mapToSchedule(_scheduleRow);
		}

		#endregion Getters

		#region Actions

		internal static void Save(Rbr_Db pDb, ScheduleDto pSchedule) {
			ScheduleRow _scheduleRow = mapToScheduleRow(pSchedule);
			if (_scheduleRow != null) {
				if (_scheduleRow.Schedule_id == 0) {
					pDb.ScheduleCollection.Insert(_scheduleRow);
					pSchedule.ScheduleId = _scheduleRow.Schedule_id;
				}
				else {
					pDb.ScheduleCollection.Update(_scheduleRow);
				}
			}
		}

		internal static void Delete(Rbr_Db pDb, int pScheduleId) {
			pDb.ScheduleCollection.DeleteByPrimaryKey(pScheduleId);
		}

		#endregion Actions

		#region mappings

		static ScheduleRow mapToScheduleRow(ScheduleDto pSchedule) {
			if (pSchedule == null) {
				return null;
			}
			ScheduleRow _scheduleRow = new ScheduleRow();
			_scheduleRow.Schedule_id = pSchedule.ScheduleId;
			_scheduleRow.ScheduleType = pSchedule.ScheduleType;
			_scheduleRow.DayOfWeek = pSchedule.DayOfWeek;
			_scheduleRow.Day_of_the_month_1 = pSchedule.DayOfTheMonth1;
			_scheduleRow.Day_of_the_month_2 = pSchedule.DayOfTheMonth2;

			return _scheduleRow;
		}

		static ScheduleDto mapToSchedule(ScheduleRow pScheduleRow) {
			if (pScheduleRow == null) {
				return null;
			}
			ScheduleDto _schedule = new ScheduleDto();
			_schedule.ScheduleId = pScheduleRow.Schedule_id;
			_schedule.ScheduleType = pScheduleRow.ScheduleType;
			_schedule.DayOfWeek = pScheduleRow.DayOfWeek;
			_schedule.DayOfTheMonth1 = pScheduleRow.Day_of_the_month_1;
			_schedule.DayOfTheMonth2 = pScheduleRow.Day_of_the_month_2;

			return _schedule;
		}

		#endregion mappings
	}
}
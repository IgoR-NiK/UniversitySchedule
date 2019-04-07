using System;
using System.Collections.Generic;
using System.Text;

using DataLayer.Models;
using DbSchedule = Repository.Entities.Schedule;

namespace DataLayer.Converters
{
	public static class ScheduleConverter
	{
		public static Schedule Convert(DbSchedule dbSchedule)
		{
			if (dbSchedule == null) return null;

			return new Schedule()
			{
				ClassroomId = dbSchedule.ClassroomId,
				Classroom = ClassroomConverter.Convert(dbSchedule.Classroom),

				PeriodTimeslotId = dbSchedule.PeriodTimeslotId,
				PeriodTimeslot = PeriodTimeslotConverter.Convert(dbSchedule.PeriodTimeslot),

				TeachingUnitId = dbSchedule.TeachingUnitId,
				TeachingUnit = TeachingUnitConverter.Convert(dbSchedule.TeachingUnit)
			};
		}

		public static DbSchedule Convert(Schedule schedule)
		{
			if (schedule == null) return null;

			return new DbSchedule()
			{
				ClassroomId = schedule.ClassroomId,
				PeriodTimeslotId = schedule.PeriodTimeslotId,
				TeachingUnitId = schedule.TeachingUnitId,
			};
		}
	}
}
